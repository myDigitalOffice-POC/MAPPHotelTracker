using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MAPPHotelTracker.Models;
using MimeKit;
using Color = System.Drawing.Color;

namespace MAPPOnBoardingStats
{


    public partial class Form1 : Form
    {
        List<PRIInfo> ThisPRIInfo = new List<PRIInfo>();
        List<HSMCInfo> ThisHSMCInfo = new List<HSMCInfo>();
        Dictionary<string, int> OrgIDLkp = new Dictionary<string, int>();
        Dictionary<string, int> HotelIDLkp = new Dictionary<string, int>();
        List<AuditMessage> TheAuditMessages = new List<AuditMessage>();
        Dictionary<string, Tuple<int, int>> HotelEmailToGroupdHotelId = new Dictionary<string, Tuple<int, int>>();



        private static string[] Scopes = {
            GmailService.Scope.GmailReadonly
        };
        
        private void InitializeOrgLookUp()
        {
            string strGConnectionString = @"Data Source=169.53.175.125;UID=mypuser;PWD=IKWWaISbjuOBx9w;DATABASE=Perspective";

            SqlDataReader rdr = null;

            // Get the list of Hotels for the Current Group
            using (SqlConnection conn = new SqlConnection(strGConnectionString))
                try
                {
                    conn.Open();
                    string strSQL = "select  distinct RTRIM(CompanyName), OrganizationID from HotelMarketView  where isactive = 1";
                    strSQL = strSQL + " and OrganizationID not in (2,47,56,105,985,993,1128,1871,2061,2210,2818,2522) order by RTRIM(CompanyName)";
                    Console.WriteLine(strSQL);
                    SqlCommand cmd = new SqlCommand(strSQL, conn);

                    // get query results
                    rdr = cmd.ExecuteReader();

                    // Generate the List Hotels for the Group
                    while (rdr.Read())
                    {
                        if (OrgIDLkp.ContainsKey(rdr[0].ToString().ToUpper().Trim()))
                        {
                            //Key already exists so do Nothing
                        }
                        else
                        {
                            OrgIDLkp.Add(rdr[0].ToString().ToUpper().Trim(), (int)rdr[1]);
                            CBoxGroupName.Items.Add(rdr[0].ToString().Trim());
                        }

                        //MessageBox.Show(HotelIDLkp.Count.ToString() + ":: HoteCode: " + rdr[0].ToString() + ", ID: " + rdr[1].ToString());
                        //Console.WriteLine("HOTELCODE and Name: " + rdr[0].ToString() + " :: " +  rdr[1].ToString());
                    }
                }
                finally
                {
                    // close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    // Close the connection
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
        }

        private void RetrieveTAMessages(string receivedConnectionString)
        {
            string strGConnectionString = receivedConnectionString;

            SqlDataReader rdr = null;
           

            // Get the list of Hotels for the Current Group
            using (SqlConnection conn = new SqlConnection(strGConnectionString))
                try
                {
                    conn.Open();
                    // Add a varaible for the date and build the date to be passed to the SQL
                    // Since the TA is on UTC, we need to add the 4 hrs.
                    // Four hr difference, the date is the current date
                    var today = DateTime.Today.ToString("yyyy-MM-dd");
                    var tomorrow = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"); 
                    string strSQL = "select ProcessedDate,MessageDate,Subject,FromAddress,";
                    strSQL = strSQL + " ToAddress,AttachmentNames,Headers from MessageStore";
                    strSQL = strSQL + $" where MessageDate >= '{today} 04:00:01'  ";
                    strSQL = strSQL + $" and MessageDate <= '{tomorrow} 03:59:59'";
                    strSQL = strSQL + " and (upper(Subject) like 'REVENUE REPORT%' or upper(Subject) like 'BOOKING STATISTICS%' ";
                    strSQL = strSQL + " or upper(Subject) like '%ON THE BOOKS%'";
                    strSQL = strSQL + " or upper(Subject) like '%BOOKING%' or upper(Subject) ";
                    strSQL = strSQL + " like 'FORECAST%' or upper(Subject) like '%HISTORY%') order by MessageDate ";
                    SqlCommand cmd = new SqlCommand(strSQL, conn);

                    // get query results
                    rdr = cmd.ExecuteReader();
                    //MessageBox.Show("SQL Statement: " + strSQL);
                    // Generate the List Hotels for the Group
                    while (rdr.Read())
                    {
                        var a1 = new AuditMessage();
                        var groupId = 9999;
                        var hotelId = 9999;
                        var fromEmailAddress = rdr[3].ToString();
                        if (HotelEmailToGroupdHotelId.ContainsKey(fromEmailAddress.Trim().ToUpper()))
                        {
                            var groupHotelId = HotelEmailToGroupdHotelId[fromEmailAddress.Trim().ToUpper()];
                            groupId = groupHotelId.Item1;
                            hotelId = groupHotelId.Item2;
                        }
                        a1.GroupId = groupId;
                        a1.HotelId = hotelId;
                        a1.TAProcessedDateTime = rdr[0].ToString();
                        a1.TAMessageDateTime = rdr[1].ToString();
                        a1.MYPProcessedDateTime = "TBD";
                        a1.MAPPControlProcessedDateTime = "TBD";
                        a1.MAPPControlDate = "TBD";
                        a1.MAPPControlCodeId = "TBD";
                        a1.MAPPLastActionStatus = "TBD";
                        a1.Subject = rdr[2].ToString();
                        a1.FromAddress = fromEmailAddress;
                        a1.ToAddress = rdr[4].ToString();
                        a1.AttachmentNames = rdr[5].ToString();
                        a1.Headers = rdr[6].ToString();
                        TheAuditMessages.Add(a1);
                        //rdr.NextResult();
                    }
                }
                finally
                {
                    // close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    // Close the connection
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
        }
        private void InitializeHotelIdLookUp()
        {

            string strGConnectionString = @"Data Source=169.53.175.125;UID=mypuser;PWD=IKWWaISbjuOBx9w;DATABASE=Perspective";

            SqlDataReader rdr = null;

            // Get the list of Hotels for the Current Group
            using (SqlConnection conn = new SqlConnection(strGConnectionString))
                try
                {
                    conn.Open();
                    string strSQL = "SELECT RTRIM(HotelCode),ID  FROM hotels where isactive = 1 and hotelcode not in ('BOSTRN','EWRNA','SLCZS')";
                    strSQL = strSQL + " and organizationid not in (2,47,56,105,985,993,1128,1871,2061,2210,2818,2522) order by HotelCode";
                    SqlCommand cmd = new SqlCommand(strSQL, conn);

                    // get query results
                    rdr = cmd.ExecuteReader();

                    // Generate the List Hotels for the Group
                    while (rdr.Read())
                    {
                        if (OrgIDLkp.ContainsKey(rdr[0].ToString().ToUpper().Trim()))
                        {
                            //Key already exists so do Nothing
                        }
                        else
                        {
                            HotelIDLkp.Add(rdr[0].ToString().ToUpper().Trim(), (int)rdr[1]);
                        }
                            
                        //MessageBox.Show(HotelIDLkp.Count.ToString() + ":: HoteCode: " + rdr[0].ToString() + ", ID: " + rdr[1].ToString());
                            //Console.WriteLine("HOTELCODE and Name: " + rdr[0].ToString() + " :: " +  rdr[1].ToString());
                    }
                }
                finally
                {
                    // close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    // Close the connection
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }

        }
        public Form1()
        {
            InitializeComponent();
            this.AutoSize = true;
        }
        private void GetPaceRptIArray()
        {
            try
            {
                var filePace_Report_Instructions = "1YIQejjcFCcQsAIqzMNr7zKhsTD75ULUmBEMo-0_aP88";
                {
                    string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
                    string ApplicationName = "Google Sheets API .NET Quickstart";
                    UserCredential credential;
                    //PRI-credentials.json
                    using (var stream =
                        new FileStream("C:\\MDO\\credentials.json", FileMode.Open, FileAccess.Read))
                    {
                        // The file token.json stores the user's access and refresh tokens, and is created
                        // automatically when the authorization flow completes for the first time.
                        //string credPath = "PaceRptI-token.json";
                        string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PaceRptI-token.json");
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            Scopes,
                            "user",
                            CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                        Console.WriteLine("Credential file saved to: " + credPath);
                    }

                    // Create Google Sheets API service.
                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,
                    });

                    // Define request parameters.

                    String spreadsheetId = filePace_Report_Instructions;
                    String range = "A1:G";
                    Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(spreadsheetId, range);

                    // Prints the names and other details of Hotels not Submitting the Data for the MAPP Reports:
                    ValueRange response = request.Execute();
                    IList<IList<Object>> values = response.Values;
                    if (values != null && values.Count > 0)
                    {
                        var data = response.Values;

                        // Get the headers and get the index of the ldap and the approval status
                        // using the names you use in the headers
                        var headers = values[0];
                        var PMSIndex = headers.IndexOf("PMS");
                        var RequiredReportsIndex = headers.IndexOf("Required Reports");

                        //foreach (var row in values)
                        for (int i = 0; i < values.Count; i++)
                        {
                            // Print columns A and E, which correspond to indices 0 and 4.
                            var row = values[i];
                            var c1 = new PRIInfo();
                            c1.PMSName = row[PMSIndex].ToString().ToUpper();
                            c1.Report1 = row[RequiredReportsIndex].ToString();
                            ThisPRIInfo.Add(c1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to Get the Data for PMS ReportNames.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }

        
        private void GetGroupEmailCredsIArray()
        {
            try
            {
                var fileHSM_Companion = "1Kw0SjpOwPeXR7tV_a76kYUVoGGMjFzKrfdeGWm6HLJs";
                {
                    string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
                    string ApplicationName = "Google Sheets API .NET Quickstart";
                    UserCredential credential;
                    //HSMC-credentials.json
                    using (var stream =
                        new FileStream("C:\\MDO\\credentials.json", FileMode.Open, FileAccess.Read))
                    {
                        // The file token.json stores the user's access and refresh tokens, and is created
                        // automatically when the authorization flow completes for the first time.
                        //string credPath = "HSM_Companion-token.json";
                        string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HSM_Companion-token.json");
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            Scopes,
                            "user",
                            CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                        Console.WriteLine("Credential file saved to: " + credPath);
                    }

                    // Create Google Sheets API service.
                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,
                    });

                    // Define request parameters.

                    String spreadsheetId = fileHSM_Companion;
                    String range = "A1:G";
                    Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(spreadsheetId, range);

                    // Prints the names and other details of Hotels not Submitting the Data for the MAPP Reports:
                    ValueRange response = request.Execute();
                    System.Collections.Generic.IList<System.Collections.Generic.IList<Object>> values = response.Values;
                    if (values != null && values.Count > 0)
                    {
                        var data = response.Values;

                        // Get the headers and get the index of the ldap and the approval status
                        // using the names you use in the headers
                        var headers = values[0];
                        var GroupIndex = headers.IndexOf("Group");
                        var GmAdressIndex = headers.IndexOf("MDO Gmail address");
                        var GmPwdIndex = headers.IndexOf("Gmail Password");


                        //foreach (var row in values)
                        for (int i = 0; i < values.Count; i++)
                        {
                            // Print columns A and E, which correspond to indices 0 and 4.
                            var row = values[i];
                            var c1 = new HSMCInfo();
                            c1.GroupName = row[GroupIndex].ToString().ToUpper();
                            c1.GroupEmail = row[GmAdressIndex].ToString();
                            c1.GroupEmailPwd = row[GmPwdIndex].ToString();
                            ThisHSMCInfo.Add(c1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to Get the Data for PMS ReportNames.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }
        }

        private string GetPMSReportName(string strPMSName)
        {
            var strReportNames = "Report";
            var strAllPMSNames = "ReportName";

            foreach (PRIInfo PRI in ThisPRIInfo)
            {
                strAllPMSNames = strAllPMSNames + PRI.PMSName + "\\n";
                if (PRI.PMSName.ToUpper() == strPMSName.ToUpper())
                {
                    strReportNames = PRI.Report1;
                    break;
                }
            }
            return strReportNames;
        }

        private string GetGroupEmailCreds(string strGroupEmail)
        {
            var strEmailPwd = "Report";
            var strAllGroupNames = "GroupName";

            foreach (HSMCInfo PRI in ThisHSMCInfo)
            {
                strAllGroupNames = strAllGroupNames + PRI.GroupName + "\\n";
                if (PRI.GroupEmail == strGroupEmail)
                {
                    strEmailPwd = PRI.GroupEmailPwd;
                }
            }
            //MessageBox.Show("All Group Names: \n" + strAllGroupNames);
            return strEmailPwd;
        }

        private void BtnGetAllSubmittals_Click(object sender, EventArgs e)
        {
            //Initialize the Search Column Name Combo Box
            CBoxColumnName.Text = "Group Name";
            GroupNameAutoCompleteBox.AutoCompleteCustomSource?.Clear();
            //Would need the Lookup values only on the first time around
            if (OrgIDLkp.Count == 0)
            {
                // Preload the Report Names for reference later
                GetPaceRptIArray();

                // Preload the Email Credentials for reference later
                GetGroupEmailCredsIArray();

                // Initialize the Dictionary Values for the OrgId Lookup
                InitializeOrgLookUp();

                // Initialize the HotelId Lookup Dictionary
                InitializeHotelIdLookUp();
            }
                        
            try
            {
                HashSet<string> uniqueGroupName = new HashSet<string>();
                var fileMAPP_HSM = "1EkkGy5pZjFQnnSOdL9z4ELc4oiJ9ovG-edT3UsA9TUA";
                {
                    string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
                    string ApplicationName = "Google Sheets API .NET Quickstart";
                    UserCredential credential;

                    using (var stream =
                        new FileStream("C:\\MDO\\credentials.json", FileMode.Open, FileAccess.Read))
                    {
                        // The file token.json stores the user's access and refresh tokens, and is created
                        // automatically when the authorization flow completes for the first time.
                        //string credPath = "token.json";
                        string credPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "token.json");
                        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                            GoogleClientSecrets.Load(stream).Secrets,
                            Scopes,
                            "user",
                            CancellationToken.None,
                            new FileDataStore(credPath, true)).Result;
                        Console.WriteLine("Credential file saved to: " + credPath);
                    }

                    // Create Google Sheets API service.
                    var service = new SheetsService(new BaseClientService.Initializer()
                    {
                        HttpClientInitializer = credential,
                        ApplicationName = ApplicationName,
                    });

                    // Define request parameters.

                    String spreadsheetId = fileMAPP_HSM;
                    String range = "A1:AJ";

                    Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource.GetRequest request =
                            service.Spreadsheets.Values.Get(spreadsheetId, range);

                    // Prints the names and other details of Hotels not Submitting the Data for the MAPP Reports:
                    ValueRange response = request.Execute();
                    System.Collections.Generic.IList<System.Collections.Generic.IList<Object>> values = response.Values;
                    if (values != null && values.Count > 0)
                    {
                        var data = response.Values;
                        //MessageBox.Show(data.Count.ToString());

                        // Get the headers and get the index of the fields
                        // using the names you use in the headers
                        var GridViewRowCount = 0;
                        var headers = values[0];
                        var GroupNameIndex = headers.IndexOf("Hotel Management Group");
                        var GroupEmailIndex = headers.IndexOf("@mdo GROUP email address");
                        //var PMSIndex = headers.IndexOf("PMS");
                        var PaceReportIndex = headers.IndexOf("Pace Report");
                        var HotelCodeIndex = headers.IndexOf("Hotel Code");
                        var HotelNameIndex = headers.IndexOf("Hotel Name");
                        var SubmissionMethodIndex = headers.IndexOf("Submission Method");
                        var SubmittingPaceIndex = headers.IndexOf("Receiving Pace Reports?");
                        var HotelMDOEmailIndex = headers.IndexOf("MDO Email Address");
                        var SearchKeyIndex = headers.IndexOf("Search Key");
                        var strEmailPwd = "";
                        var strReportName1 = "";
                        var strReportName2 = "";
                        var strReportName3 = "";
                        var strReportName4 = "";
                        var strReportName = "";
                        var intGroupID = 9999;
                        var intHotelID = 9999;

                        // Clear the Grid, in Case this is a resubmit
                        if (dataGridView1.Rows.Count > 1)
                        {
                            dataGridView1.Rows.Clear();
                        }
                        

                        //foreach (var row in values)
                        for (int i = 0; i < values.Count; i++)
                        {
                            // Print columns A and E, which correspond to indices 0 and 4.
                            var row = values[i];
                            //MessageBox.Show(row.Count.ToString());
                            if (i == 0)
                            {
                                dataGridView1.Columns.Add("Group ID", "Group ID");
                                dataGridView1.Columns.Add("Group Name", row[GroupNameIndex].ToString());
                                dataGridView1.Columns.Add("Group Email", row[GroupEmailIndex].ToString());
                                dataGridView1.Columns.Add("Group MDO Email", row[HotelMDOEmailIndex].ToString());
                                dataGridView1.Columns.Add("Hotel ID", "HOTEL ID");
                                dataGridView1.Columns.Add("Hotel Code", row[HotelCodeIndex].ToString());
                                dataGridView1.Columns.Add("Hotel Name", row[HotelNameIndex].ToString());

                                dataGridView1.Columns.Add("Group Email PD", "Group Email PD");
                                // Changed from PMSIndex to PaceReportIndex
                                dataGridView1.Columns.Add("Group PMS", row[PaceReportIndex].ToString());
                                dataGridView1.Columns.Add("PMS Report Name1", "PMS Report Name1");
                                dataGridView1.Columns.Add("PMS Report Name2", "PMS Report Name2");
                                dataGridView1.Columns.Add("PMS Report Name3", "PMS Report Name3");
                                dataGridView1.Columns.Add("Submission Method", row[SubmissionMethodIndex].ToString());
                                dataGridView1.Columns.Add("Receiving Pace Reports", row[SubmittingPaceIndex].ToString());
                                dataGridView1.Columns.Add("Found Reports", "Found Reports");
                                dataGridView1.Columns.Add("SheetRowIndex", i.ToString());
                                dataGridView1.Columns.Add("EmailSearch", "Email Search Result");
                                //MessageBox.Show(row[SearchKeyIndex].ToString());
                                dataGridView1.Columns.Add("Search Key", row[SearchKeyIndex].ToString());
                                dataGridView1.Columns.Add("Errors", "Errors");
                                DataGridViewLinkColumn dataGridViewLinkColumn = new DataGridViewLinkColumn();
                                dataGridViewLinkColumn.HeaderText = "Resolutions";
                                dataGridViewLinkColumn.Name = "Resolutions";
                                dataGridView1.Columns.Add(dataGridViewLinkColumn);
                                //var GridViewRowCount = 1;
                                //AddTestSampleRow();
                            }
                            else
                            {
                                if ((row[GroupNameIndex].ToString() != "") 
                                        && (row[SubmittingPaceIndex].ToString().ToUpper() != "NO"))
                                {
                                    strReportName = "";
                                    strReportName1 = "";
                                    strReportName2 = "";
                                    strReportName3 = "";
                                    strReportName4 = "";

                                    dataGridView1.Rows.Add();

                                    try
                                    {
                                        if (HotelIDLkp.ContainsKey(row[HotelCodeIndex].ToString().Trim().ToUpper()))
                                        {
                                            int value_For_Key = HotelIDLkp[row[HotelCodeIndex].ToString().Trim().ToUpper()];
                                            intHotelID = value_For_Key;
                                        }
                                        else
                                        {
                                            intHotelID = 9999;
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        intHotelID = 9999;
                                    }

                                    try
                                    {
                                        if (OrgIDLkp.ContainsKey(row[GroupNameIndex].ToString().Trim().ToUpper()))
                                        {
                                            int value_For_Key1 = OrgIDLkp[row[GroupNameIndex].ToString().Trim().ToUpper()];
                                            intGroupID = value_For_Key1;
                                        }
                                        else
                                        {
                                            intGroupID = 9999;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        intGroupID = 9999;
                                    }
                                    
                                    var hotelMdoEmailAddress = row[HotelMDOEmailIndex].ToString();
                                    if(!string.IsNullOrWhiteSpace(hotelMdoEmailAddress))
                                    {
                                        if(!HotelEmailToGroupdHotelId.ContainsKey(hotelMdoEmailAddress.Trim().ToUpper()))
                                        {
                                            HotelEmailToGroupdHotelId[hotelMdoEmailAddress.Trim().ToUpper()] = new Tuple<int, int>(intGroupID, intHotelID);
                                        }
                                    }
                                    var groupName = row[GroupNameIndex].ToString();
                                    uniqueGroupName.Add(groupName);
                                    dataGridView1.Rows[GridViewRowCount].Cells[0].Value = intGroupID;
                                    dataGridView1.Rows[GridViewRowCount].Cells[1].Value = groupName;
                                    dataGridView1.Rows[GridViewRowCount].Cells[2].Value = row[GroupEmailIndex].ToString();
                                    strEmailPwd = GetGroupEmailCreds(row[GroupEmailIndex].ToString());
                                    dataGridView1.Rows[GridViewRowCount].Cells[3].Value = hotelMdoEmailAddress;
                                    dataGridView1.Rows[GridViewRowCount].Cells[4].Value = intHotelID;
                                    dataGridView1.Rows[GridViewRowCount].Cells[5].Value = row[HotelCodeIndex].ToString();
                                    dataGridView1.Rows[GridViewRowCount].Cells[6].Value = row[HotelNameIndex].ToString();
                                    dataGridView1.Rows[GridViewRowCount].Cells[7].Value = strEmailPwd;
                                    dataGridView1.Rows[GridViewRowCount].Cells[8].Value = row[PaceReportIndex].ToString();
                                    //strReportName = GetPMSReportName(row[PaceReportIndex].ToString());
                                    strReportName = row[PaceReportIndex].ToString();
                                    //strSearchKey = row[SearchKeyIndex].ToString(); 
                                    string[] splittedReports = strReportName.Split('\n');

                                    for (int isk = 0; isk < splittedReports.Length; isk++)
                                    {
                                        if (isk == 0 && splittedReports[isk].ToString() != "" ) strReportName1 = splittedReports[isk].ToString();
                                        if (isk == 1 && splittedReports[isk].ToString() != "") strReportName2 = splittedReports[isk].ToString();
                                        if (isk == 2 && splittedReports[isk].ToString() != "") strReportName3 = splittedReports[isk].ToString();
                                        if (isk == 3 && splittedReports[isk].ToString() != "") strReportName4 = splittedReports[isk].ToString();
                                    }

                                        //if (splittedReports[0] != "") { strReportName1 = splittedReports[0]; } else { strReportName1 = ""; }
                                        //if (splittedReports.Length > 1) { if (splittedReports[1] != "") { strReportName2 = splittedReports[1]; } else { strReportName2 = ""; } }
                                        //if (splittedReports.Length > 2) { if (splittedReports[2] != "") { strReportName3 = splittedReports[2]; } else { strReportName3 = ""; } }


                                    dataGridView1.Rows[GridViewRowCount].Cells[9].Value = strReportName1;
                                    dataGridView1.Rows[GridViewRowCount].Cells[10].Value = strReportName2;
                                    dataGridView1.Rows[GridViewRowCount].Cells[11].Value = strReportName3;
                                    dataGridView1.Rows[GridViewRowCount].Cells[12].Value = row[SubmissionMethodIndex].ToString();
                                    dataGridView1.Rows[GridViewRowCount].Cells[13].Value = row[SubmittingPaceIndex].ToString();
                                    dataGridView1.Rows[GridViewRowCount].Cells[14].Value = "FALSE";
                                    dataGridView1.Rows[GridViewRowCount].Cells[15].Value = (i + 1).ToString();
                                    //dataGridView1.Rows[GridViewRowCount].Cells["SheetRowIndex"].Value = (i + 1).ToString();
                                    if (row.Count == 36)
                                    { 
                                        dataGridView1.Rows[GridViewRowCount].Cells[17].Value = row[SearchKeyIndex].ToString(); 
                                    }
                                    //dataGridView1.Rows[GridViewRowCount].Cells[17].Value = row[SearchKeyIndex].ToString();
                                    //MessageBox.Show(row[27].ToString());

                                    dataGridView1.Rows[GridViewRowCount].Cells["Resolutions"].Value = "Sample Resolution" + GridViewRowCount;

                                    var paceReportStatus = row[SubmittingPaceIndex].ToString();
                                    if(string.Equals(paceReportStatus, "error", StringComparison.OrdinalIgnoreCase) ||
                                        string.Equals(paceReportStatus, "errors", StringComparison.OrdinalIgnoreCase))
                                    {
                                        dataGridView1.Rows[GridViewRowCount].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#CE352C");
                                        // dataGridView1.Rows[GridViewRowCount].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#ffffff");
                                    }
                                    if (string.Equals(paceReportStatus, "na", StringComparison.OrdinalIgnoreCase) ||
                                        string.Equals(paceReportStatus, "n/a", StringComparison.OrdinalIgnoreCase))
                                    {
                                        dataGridView1.Rows[GridViewRowCount].DefaultCellStyle.BackColor = Color.LightGray; // ColorTranslator.FromHtml("#505050");
                                        // dataGridView1.Rows[GridViewRowCount].DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#ffffff");
                                    }

                                    GridViewRowCount = GridViewRowCount + 1;
                                }
                            }
                        }
                        textBoxNonSubmittalCount.Text = "Hotels Submitting Report is: " + GridViewRowCount.ToString();
                        AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                        col.AddRange(uniqueGroupName.ToArray());
                        GroupNameAutoCompleteBox.AutoCompleteCustomSource = col;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            row.HeaderCell.Value = (row.Index + 1).ToString();
                        }

                        dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            //dataGridView1.Rows[GridViewRowCount].Cells[10].Value = ProcessEmailInbox3(row[1].ToString(), strEmailPwd, row[HotelMDOEmailIndex].ToString(), "Revenue Report", "", "").ToString();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No data found.");
                    }
                }

                TheAuditMessages.Clear();

                RetrieveTAMessages(@"Data Source=13.56.146.174;UID=amar;PWD=pntvRwm2u5JPUCPm;DATABASE=ThinkAutomationV4");

               

                UpdateGroupAndHotelIds();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to Get the Data for Non Submittals.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
            }

        }


        
            
        private void BtnProcessSelectedEmailAdr_Click(object sender, EventArgs e)
        {
            UpdateAuditTrail(RowUpdateMode.SelectedGroup);
        }

        private void ProcessAllRowsBtn_Click(object sender, EventArgs e)
        {
            UpdateAuditTrail(RowUpdateMode.All);
        }       

       
        private void BtnClearSelection_Click(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            //cleanup  
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Application.Exit();
        }

      
        private void BtnEmailAuditTrail_Click(object sender, EventArgs e)
        {
            
            UpdateAuditTrail(RowUpdateMode.SelectedRow);
           
        }

        private void UpdateGroupAndHotelIds()
        {
            //MessageBox.Show("AuditRow Count:" + TheAuditMessages.Count.ToString());
            TextBoxTAMessage.Text = "TA Processed Messages Count: " + TheAuditMessages.Count.ToString();
        }

        private void AddAuditRows()
        {
            var GridViewRowCount = 0;
            
            if (TheAuditMessages.Count() > 1)
            {
                dataGridViewAuditRows.Columns.Clear();
                dataGridViewAuditRows.Columns.Add("Group ID", "Group ID");
                dataGridViewAuditRows.Columns.Add("Hotel ID", "Hotel Id");
                dataGridViewAuditRows.Columns.Add("TAProcessedDateTime", "TAProcessedDateTime");
                dataGridViewAuditRows.Columns.Add("TAMessageDateTime", "TAMessageDateTime");
                dataGridViewAuditRows.Columns.Add("MYPProcessedDateTime", "MYPProcessedDateTime");
                dataGridViewAuditRows.Columns.Add("MAPPControlProcessedDateTime", "MAPPControlProcessedDateTime");
                dataGridViewAuditRows.Columns.Add("MAPPControlDate", "MAPPControlDate");
                dataGridViewAuditRows.Columns.Add("MAPPControlCodeId", "MAPPControlCodeId");
                dataGridViewAuditRows.Columns.Add("MAPPLastActionStatus", "MAPPLastActionStatus");
                dataGridViewAuditRows.Columns.Add("Subject", "Subject");
                dataGridViewAuditRows.Columns.Add("From Address", "From Address");
                dataGridViewAuditRows.Columns.Add("To Address", "To Address");
                dataGridViewAuditRows.Columns.Add("AttachmentNames", "AttachmentNames");
                dataGridViewAuditRows.Columns.Add("Headers", "Headers");
                var selectedRowFilter = false;
                int selectedGroupId = -1;
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    selectedRowFilter = true;
                    var selectedFirstRow = dataGridView1.SelectedRows[0];
                    var groupdIdString = selectedFirstRow.Cells["Group ID"]?.Value?.ToString();
                    if (!string.IsNullOrWhiteSpace(groupdIdString))
                    {
                        int.TryParse(groupdIdString, out selectedGroupId);
                    }

                }
                dataGridViewAuditRows.Rows.Clear();
                for (int i = 0; i < TheAuditMessages.Count; i++)                
                {                  

                    var row = TheAuditMessages[i];
                    if (selectedRowFilter)
                    {
                        if (row.GroupId != selectedGroupId)
                        {
                            continue;
                        }
                    }
                    dataGridViewAuditRows.Rows.Add();

                    //MessageBox.Show(row.ToAddress.ToString());

                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[0].Value = row.GroupId.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[1].Value = row.HotelId.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[2].Value = row.TAProcessedDateTime.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[3].Value = row.TAMessageDateTime.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[4].Value = row.MYPProcessedDateTime.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[5].Value = row.MAPPControlProcessedDateTime.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[6].Value = row.MAPPControlDate.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[7].Value = row.MAPPControlCodeId.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[8].Value = row.MAPPLastActionStatus.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[9].Value = row.Subject.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[10].Value = row.FromAddress.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[11].Value = row.ToAddress.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[12].Value = row.AttachmentNames.ToString();
                    dataGridViewAuditRows.Rows[GridViewRowCount].Cells[13].Value = row.Headers.ToString();

                    GridViewRowCount = GridViewRowCount + 1;
                }
                
            }
            else
            {
                MessageBox.Show("There are no Audit Records to Display, have the AuditRecords been retrieved from the Database?");
            }
        }

        private void UpdateAuditTrail(RowUpdateMode rowUpdateMode)
        {
            try
            {
                var hotelAuditInfoList = new List<HotelAuditInfo>();
                var groupEmails = new Dictionary<string, string>();
                if (rowUpdateMode == RowUpdateMode.SelectedGroup || rowUpdateMode == RowUpdateMode.SelectedRow)
                {
                    if (dataGridView1.SelectedRows.Count <= 0)
                    {
                        MessageBox.Show("Please Select the row that you would like to process !");
                        return;
                    }

                }
                if (rowUpdateMode == RowUpdateMode.SelectedRow)
                {
                    var row = dataGridView1.SelectedRows[0];
                    var groupEmailInRow = row.Cells["Group Email"]?.Value?.ToString().ToLower();
                    var groupEmailPwd = row.Cells["Group Email PD"]?.Value?.ToString();
                    groupEmails.Add(groupEmailInRow, groupEmailPwd);
                }
                if (rowUpdateMode == RowUpdateMode.All)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        var row = dataGridView1.Rows[i];
                        var groupEmailInRow = row.Cells["Group Email"]?.Value?.ToString().ToLower();
                        if (string.IsNullOrWhiteSpace(groupEmailInRow))
                        {
                            row.Cells["EmailSearch"].Value = "No group email";
                            continue;
                        }
                        var groupEmailPwd = row.Cells["Group Email PD"]?.Value?.ToString();

                        if (string.IsNullOrWhiteSpace(groupEmailPwd))
                        {
                            row.Cells["EmailSearch"].Value = "No Password";
                        }
                        if (groupEmails.ContainsKey(groupEmailInRow))
                        {
                            continue;
                        }
                        groupEmails.Add(groupEmailInRow, groupEmailPwd);
                    }
                }
                if (rowUpdateMode == RowUpdateMode.SelectedGroup)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];
                    var selectedGroupId = selectedRow.Cells["Group Id"].Value.ToString();
                    var selectedGroupEmail = selectedRow.Cells["Group Email"].Value.ToString();
                    var selectedGroupPwd = selectedRow.Cells["Group Email PD"].Value.ToString();
                    if (string.IsNullOrEmpty(selectedGroupEmail) || string.IsNullOrWhiteSpace(selectedGroupPwd))
                    {
                        MessageBox.Show("No group email or password on selected row");
                        return;
                    }
                    groupEmails.Add(selectedGroupEmail, selectedGroupPwd);
                }

                foreach (var gEmailKeyValuePair in groupEmails)
                {
                    var gEmailId = gEmailKeyValuePair.Key;
                    try
                    {
                        var emailRowsToProcess = new List<DataGridViewRow>();
                        var otherRowsToProcess = new List<DataGridViewRow>();
                        if (rowUpdateMode == RowUpdateMode.SelectedRow)
                        {
                            var rowTP = dataGridView1.SelectedRows[0];
                            if (IsEmailRow(rowTP))
                            {
                                emailRowsToProcess.Add(dataGridView1.SelectedRows[0]);
                            }
                            else
                            {
                                otherRowsToProcess.Add(rowTP);
                            }
                        }
                        if (rowUpdateMode == RowUpdateMode.All)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                var selectedRow = dataGridView1.Rows[i];
                                if (IsEmailRow(selectedRow))
                                {
                                    if (selectedRow.Cells["Group Email"].Value != null)
                                    {
                                        if (selectedRow.Cells["Group Email"].Value.ToString().Equals(gEmailId, StringComparison.OrdinalIgnoreCase))
                                        {
                                            emailRowsToProcess.Add(selectedRow);
                                        }
                                    }
                                    else
                                    {
                                        selectedRow.Cells["EmailSearch"].Value = "Group email is empty";
                                    }
                                }
                                else
                                {
                                    otherRowsToProcess.Add(selectedRow);
                                }
                            }
                        }

                        if (rowUpdateMode == RowUpdateMode.SelectedGroup)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                var selectedRow = dataGridView1.Rows[i];
                                if (IsEmailRow(selectedRow))
                                {
                                    if (selectedRow.Cells["Group Email"].Value != null)
                                    {
                                        if (selectedRow.Cells["Group Email"].Value.ToString().Equals(gEmailId, StringComparison.OrdinalIgnoreCase))
                                        {
                                            emailRowsToProcess.Add(selectedRow);
                                        }
                                    }
                                    else
                                    {
                                        selectedRow.Cells["EmailSearch"].Value = "Group email is empty";
                                    }
                                }
                                
                            }
                        }


                        otherRowsToProcess?.ForEach(row =>
                        {
                            var hotelAuditInfo = GetAuditInfo(row);
                            hotelAuditInfoList.Add(hotelAuditInfo);
                        });
                        var ProcessedFolder = "processed";
                        string GroupEmailId = gEmailId.Substring(0, gEmailId.IndexOf("@")).ToUpper();
                        var processedFolderSettingValue = ConfigurationManager.AppSettings.Get(GroupEmailId);
                        if (!string.IsNullOrWhiteSpace(processedFolderSettingValue))
                        {
                            //ProcessedFolder = "processed"; //processedFolderSettingValue;
                            ProcessedFolder = processedFolderSettingValue;
                        }
                        var pwd = gEmailKeyValuePair.Value;
                        using (var client = new ImapClient())
                        {
                            client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                            client.Authenticate(gEmailId, pwd);

                            
                            emailRowsToProcess.ForEach(selectedRow =>
                            {
                                try
                                {
                                    var grupEmailInRow = selectedRow.Cells["Group Email"].Value?.ToString();
                                    if (string.IsNullOrWhiteSpace(grupEmailInRow))
                                    {
                                        selectedRow.Cells["EmailSearch"].Value = "Processed; Group email is empty";
                                        return;
                                    }
                                    var selectedGroupEmail = selectedRow.Cells["Group Email"].Value.ToString();
                                    var selectedGroupPwd = selectedRow.Cells["Group Email PD"].Value.ToString();
                                    int selectedGroupID = (int)selectedRow.Cells["Group ID"].Value;
                                    string selectedGroupName = selectedRow.Cells["Group Name"].Value.ToString();
                                    int intHotelId = (int)selectedRow.Cells["Hotel ID"].Value;
                                    var selectedHotelCode = selectedRow.Cells["Hotel Code"].Value.ToString();
                                    var selectedHotelName = selectedRow.Cells["Hotel Name"].Value.ToString();
                                    var selectedPMS = selectedRow.Cells["Group PMS"].Value.ToString();
                                    var selectedPMSRpt1 = selectedRow.Cells["PMS Report Name1"].Value.ToString();
                                    var selectedPMSRpt2 = selectedRow.Cells["PMS Report Name2"].Value.ToString();
                                    var selectedPMSRpt3 = selectedRow.Cells["PMS Report Name3"].Value.ToString();
                                    var selectedSearchKey = selectedRow.Cells["Search Key"].Value.ToString();

                                    string[] SearchKeywords = selectedRow.Cells["Search Key"].Value.ToString().Split(',');

                                    //for (int isk = 0; isk < SearchKeywords.Length; isk++)
                                    //{
                                    //    MessageBox.Show(SearchKeywords[isk].ToString());
                                    //}

                                    string strReportSubmissionMethod = selectedRow.Cells["Submission Method"].Value.ToString();

                                    if (string.IsNullOrEmpty(selectedGroupEmail) || string.IsNullOrWhiteSpace(selectedGroupPwd))
                                    {
                                        selectedRow.Cells["EmailSearch"].Value += "No group email or password on selected row";
                                        return;
                                    }
                                    var strHotelEmailAddress = selectedRow.Cells["Group MDO Email"].Value.ToString();
                                    if (string.IsNullOrEmpty(strHotelEmailAddress))
                                    {
                                        selectedRow.Cells["EmailSearch"].Value += "Group MDO Email is empty";
                                        return;
                                    }

                                    var emailAuditInfoInbox = GetEmailAuditResult(client.Inbox, selectedGroupEmail, strHotelEmailAddress
                                        , selectedPMSRpt1, selectedPMSRpt2, selectedPMSRpt3, selectedHotelName, SearchKeywords);
                                    var emailAuditInfoProcessed = new List<EmailAuditInfo>();
                                    try
                                    {
                                        var processedFolder = client.GetFolder(ProcessedFolder);
                                        emailAuditInfoProcessed = GetEmailAuditResult(processedFolder, selectedGroupEmail, strHotelEmailAddress
                                            , selectedPMSRpt1, selectedPMSRpt2, selectedPMSRpt3, selectedHotelName, SearchKeywords);
                                    }
                                    catch(Exception e)
                                    {

                                    }
                                    var combinedResults = emailAuditInfoInbox.Concat(emailAuditInfoProcessed).ToList();

                                    if (combinedResults.Count() > 0)
                                    {

                                         var hotelAuditInfo = new HotelAuditInfo()
                                        {
                                            GroupId = selectedGroupID,
                                            GroupName = selectedGroupName,
                                            OrganizationMDOEmail = selectedGroupEmail,
                                            ReportSubmissionMethod = strReportSubmissionMethod,
                                            HotelMDOEmail = strHotelEmailAddress,
                                            HotelId = intHotelId,
                                            HotelCode = selectedHotelCode,
                                            HotelName = selectedHotelName,
                                            PMS = selectedPMS,
                                            EmailAuditInfoList = combinedResults
                                        };
                                        hotelAuditInfoList.Add(hotelAuditInfo);

                                    }
                                    else
                                    {
                                        selectedRow.Cells["EmailSearch"].Value += $"No email found in {selectedGroupEmail} from {strHotelEmailAddress} for today";
                                        return;
                                    }


                                }
                                catch (Exception ex)
                                {
                                    selectedRow.Cells["EmailSearch"].Value += ex.Message;
                                }
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        {
                            var row = dataGridView1.Rows[i];
                            if (row.Cells["Group Email"].Value?.ToString().ToUpper() == gEmailId.ToUpper())
                            {
                                row.Cells["EmailSearch"].Value += ex.Message;
                            }
                        }
                    }
                }
                UpdateDbEmailAudit(hotelAuditInfoList);                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in checking the email: ${ex.Message}");
            }

        }

        private bool IsEmailRow(DataGridViewRow row)
        {
            string strReportSubmissionMethod = row.Cells["Submission Method"].Value?.ToString();
            return string.Equals(strReportSubmissionMethod, "Email", StringComparison.OrdinalIgnoreCase);
        }

        private HotelAuditInfo GetAuditInfo(DataGridViewRow row)
        {           
            var selectedGroupEmail = row.Cells["Group Email"].Value?.ToString();            
            int selectedGroupID =  row.Cells["Group ID"].Value != null ? (int)row.Cells["Group ID"].Value : -1;
            string selectedGroupName = row.Cells["Group Name"].Value?.ToString();
            int intHotelId = row.Cells["Hotel ID"].Value != null ? (int)row.Cells["Hotel ID"].Value : -1;
            var selectedHotelCode = row.Cells["Hotel Code"].Value?.ToString();
            var selectedHotelName = row.Cells["Hotel Name"].Value?.ToString();
            var selectedPMS = row.Cells["Group PMS"].Value?.ToString();
            string strReportSubmissionMethod = row.Cells["Submission Method"].Value?.ToString();
            var strHotelEmailAddress = row.Cells["Group MDO Email"].Value?.ToString();

            var hotelAuditInfo = new HotelAuditInfo()
            {
                GroupId = selectedGroupID,
                GroupName = selectedGroupName,
                OrganizationMDOEmail = selectedGroupEmail,
                ReportSubmissionMethod = strReportSubmissionMethod,
                HotelMDOEmail = strHotelEmailAddress,
                HotelId = intHotelId,
                HotelCode = selectedHotelCode,
                HotelName = selectedHotelName,
                PMS = selectedPMS,
               
            };

            return hotelAuditInfo;
        }
        

        private static void UpdateDbEmailAudit(List<HotelAuditInfo> hotelAuditInfos)
        {
            try
            {
                int NumRowsInserted = 0;
                string strGConnectionString = ConfigurationManager.AppSettings.Get("AuditTrialDBConnectionString"); 
                SqlConnection con = new SqlConnection(strGConnectionString);
                con.Open();

                try
                {
                    hotelAuditInfos?.ForEach(h =>
                    {
                        try
                        {
                            //Build the Insert Statement
                            bool hasEmailData = h.EmailAuditInfoList != null && h.EmailAuditInfoList.Count > 0;
                            string query = "INSERT INTO MAPPEmailAuditTrail (OrganizationID, OrganizationName";
                            query = query + " , OrganizationMDOEmail, ReportSubmissionMethod, HotelMDOEmail, HotelID, HotelName, HotelCode,";
                            query = query + "PMS," + (hasEmailData ? "EmailReceivedDateTime, FoundInFolder, FromAddress, ToAddress, " : string.Empty);
                            query = query + (hasEmailData ? " Subject, MessageBody,NumOfAttachments, Attachments," : string.Empty) + "UpdatedBy, UpdatedDateTime) ";
                            query = query + " VALUES(@OrganizationID, @OrganizationName, ";
                            query = query + " @OrganizationMDOEmail, @ReportSubmissionMethod, @HotelMDOEmail, @HotelID, @HotelName, @HotelCode, ";
                            query = query + " @PMS," + (hasEmailData ? "@EmailReceivedDateTime, @FoundInFolder, @FromAddress, @ToAddress, " : string.Empty);
                            query = query + (hasEmailData ? " @Subject, @MessageBody, @NumOfAttachments, @Attachments," : string.Empty) + "@UpdatedBy, @UpdatedDateTime)";



                            if (h.EmailAuditInfoList != null && h.EmailAuditInfoList.Count > 0)
                            {

                                h.EmailAuditInfoList?.ForEach(e =>
                                {
                                    var strMessageBody = "";

                                    if (e.Body.Length > 0)
                                    {
                                        strMessageBody = e.Body.Length.ToString();
                                    }
                                //Create the Command Object 
                                SqlCommand cmd = new SqlCommand(query, con);

                                //Pass values to Parameters
                                cmd.Parameters.AddWithValue("@OrganizationID", h.GroupId);
                                    cmd.Parameters.AddWithValue("@OrganizationName", h.GroupName);
                                    cmd.Parameters.AddWithValue("@OrganizationMDOEmail", h.OrganizationMDOEmail);
                                    cmd.Parameters.AddWithValue("@ReportSubmissionMethod", h.ReportSubmissionMethod);
                                    cmd.Parameters.AddWithValue("@HotelMDOEmail", h.HotelMDOEmail);
                                    cmd.Parameters.AddWithValue("@HotelID", h.HotelId);
                                    cmd.Parameters.AddWithValue("@HotelCode", h.HotelCode);
                                    cmd.Parameters.AddWithValue("@HotelName", h.HotelName);
                                    cmd.Parameters.AddWithValue("@PMS", h.PMS);
                                    cmd.Parameters.AddWithValue("@UpdatedBy", "MAPPEmailAudit");
                                    cmd.Parameters.AddWithValue("@UpdatedDateTime", Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy HH:mm:ss"));

                                    cmd.Parameters.AddWithValue("@EmailReceivedDateTime", Convert.ToDateTime(e.DateReceived).ToString("MM-dd-yyyy HH:mm:ss"));
                                    cmd.Parameters.AddWithValue("@FoundInFolder", e.ContainerFolder);
                                    cmd.Parameters.AddWithValue("@FromAddress", e.FromEmailAddress);
                                    cmd.Parameters.AddWithValue("@ToAddress", e.ToEmailAddress);
                                    cmd.Parameters.AddWithValue("@Subject", e.Subject);
                                    cmd.Parameters.AddWithValue("@MessageBody", strMessageBody);
                                    cmd.Parameters.AddWithValue("@NumOfAttachments", e.Attachments.Count);
                                    cmd.Parameters.AddWithValue("@Attachments", "Attachment List");

                                    cmd.ExecuteNonQuery();
                                    NumRowsInserted += 1;
                                    cmd.Parameters.Clear();
                                });
                            }
                            else
                            {
                                //Create the Command Object 
                                SqlCommand cmd = new SqlCommand(query, con);

                                //Pass values to Parameters
                                cmd.Parameters.AddWithValue("@OrganizationID", h.GroupId);
                                cmd.Parameters.AddWithValue("@OrganizationName", h.GroupName);
                                cmd.Parameters.AddWithValue("@OrganizationMDOEmail", h.OrganizationMDOEmail);
                                cmd.Parameters.AddWithValue("@ReportSubmissionMethod", h.ReportSubmissionMethod);
                                cmd.Parameters.AddWithValue("@HotelMDOEmail", h.HotelMDOEmail);
                                cmd.Parameters.AddWithValue("@HotelID", h.HotelId);
                                cmd.Parameters.AddWithValue("@HotelCode", h.HotelCode);
                                cmd.Parameters.AddWithValue("@HotelName", h.HotelName);
                                cmd.Parameters.AddWithValue("@PMS", h.PMS);
                                cmd.Parameters.AddWithValue("@UpdatedBy", "MAPPEmailAudit");
                                cmd.Parameters.AddWithValue("@UpdatedDateTime", Convert.ToDateTime(DateTime.Now).ToString("MM-dd-yyyy HH:mm:ss"));
                                //cmd.Parameters.AddWithValue("@EmailReceivedDateTime", null);
                                //cmd.Parameters.AddWithValue("@FoundInFolder", string.Empty);
                                //cmd.Parameters.AddWithValue("@FromAddress", string.Empty);
                                //cmd.Parameters.AddWithValue("@ToAddress", string.Empty);
                                //cmd.Parameters.AddWithValue("@Subject", string.Empty);
                                //cmd.Parameters.AddWithValue("@MessageBody", string.Empty);
                                //cmd.Parameters.AddWithValue("@NumOfAttachments", string.Empty);
                                //cmd.Parameters.AddWithValue("@Attachments", string.Empty);
                                cmd.ExecuteNonQuery();
                                NumRowsInserted += 1;
                                cmd.Parameters.Clear();
                            }
                        }
                        catch(Exception ex)
                        {

                        }
                        
                    });
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Generated. Details: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

                MessageBox.Show("DataBase update operation is complete, a total of " + NumRowsInserted.ToString() + " were inserted.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private static List<EmailAuditInfo> GetEmailAuditResult(IMailFolder emailFolder, string strGroupEmailAddress, string strHotelEmailAddress
            , string selectedPMSRpt1, string selectedPMSRpt2, string selectedPMSRpt3, string selectedHotelName, string[] SearchKeywords)
        {
            var emailAuditList = new List<EmailAuditInfo>();
            try
            {

                string folderToProcess = emailFolder.Name;
                emailFolder.Open(FolderAccess.ReadOnly);

                selectedPMSRpt1 = StringLeft(selectedPMSRpt1, selectedPMSRpt1.IndexOf("."));
                selectedPMSRpt2 = StringLeft(selectedPMSRpt2, selectedPMSRpt2.IndexOf("."));

                //MessageBox.Show(selectedPMSRpt1 + " :: " + selectedPMSRpt2);

                var dateFilter = SearchQuery.DeliveredOn(DateTime.Today)
                                            .And(SearchQuery.ToContains(strHotelEmailAddress));

                var searchResultsPFolder = emailFolder.Search(dateFilter);

                //MessageBox.Show("Found a total of : " + searchResultsPFolder.Count.ToString() + " Messages Matching the Criteria. " );

                for (int idx = 0; idx < searchResultsPFolder.Count; idx++)
                //for (int idx = 0; idx < 5; idx++)
                {
                    var emailAuditInfo = new EmailAuditInfo();
                    emailAuditInfo.Attachments = new List<string>();
                    var message = emailFolder.GetMessage(searchResultsPFolder[idx]);
                    emailAuditInfo.DateReceived = message.Date.ToString();
                    emailAuditInfo.FromEmailAddress = message.From.ToString();
                    emailAuditInfo.ToEmailAddress = message.To.ToString();
                    emailAuditInfo.Subject = message.Subject.ToString();
                    emailAuditInfo.Body = message.Body.ToString();
                    emailAuditInfo.ContainerFolder = folderToProcess;

                    //MessageBox.Show(message.From.ToString() + " :: " + message.To.ToString() + " :: " + message.Subject.ToString());
                    foreach (MimeEntity attachment in message.Attachments)
                    {
                        var fileName = attachment.ContentDisposition?.FileName ?? attachment.ContentType.Name;
                        
                        
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            emailAuditInfo.Attachments.Add(fileName);
                        }
                    }
                    //MessageBox.Show(message.Date.ToString() + " From: " + message.From.ToString() + " :: " + " To: " + message.To.ToString() + " :: " + message.Subject.ToString() + " :Body: " + message.Body.ToString());

                    if (strGroupEmailAddress.ToUpper().Contains("EXTERNAL"))
                    {
                        if (SearchKeywords.Length == 1)
                        {
                            if (message.To.ToString().Contains(strHotelEmailAddress)
                                && (message.Subject.ToString().Contains(SearchKeywords[0].ToString())))
                            {
                                emailAuditList.Add(emailAuditInfo);
                            }
                        }

                        if (SearchKeywords.Length == 2)
                        {
                            if (message.To.ToString().Contains(strHotelEmailAddress)
                                && (message.Subject.ToString().Contains(SearchKeywords[0].ToString())
                                   || message.Subject.ToString().Contains(SearchKeywords[1].ToString())))
                            {
                                emailAuditList.Add(emailAuditInfo);
                            }
                        }

                        if (SearchKeywords.Length == 3)
                        {
                            if (message.To.ToString().Contains(strHotelEmailAddress)
                                && (message.Subject.ToString().Contains(SearchKeywords[0].ToString())
                                   || message.Subject.ToString().Contains(SearchKeywords[1].ToString())
                                   || message.Subject.ToString().Contains(SearchKeywords[2].ToString())))
                            {
                                emailAuditList.Add(emailAuditInfo);
                            }
                        }
                        
                    }
                    else
                    {
                        if (selectedPMSRpt1 != "")
                        {
                            if (message.To.ToString().Contains(strHotelEmailAddress)
                                && (message.Subject.ToString().Contains(selectedPMSRpt1)
                                   || message.Subject.ToString().Contains(SearchKeywords[0].ToString())))
                            {
                                emailAuditList.Add(emailAuditInfo);
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            return emailAuditList;
        }
        
        public static string StringLeft(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }

        private void btnGetAuditRows_Click(object sender, EventArgs e)
        {
            AddAuditRows();
        }

        private void TxtContainsText_TextChanged(object sender, EventArgs e)
        {
            var searchText = CommonSearchTextBox.Text.Trim();
            FilterGrid(searchText, CBoxColumnName.Text, dataGridView1);
        }

       
        private const int MinimumSearchTextLength = 2;

        private void FilterGrid(string searchText, string headerName, DataGridView dataGridView)
        {
            if(string.IsNullOrWhiteSpace(headerName))
            {
                MessageBox.Show("Please select Search By Option");
                return;
            }
            if (!string.IsNullOrEmpty(searchText) && searchText.Length >= MinimumSearchTextLength)
            {              
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    if (r.Cells[headerName].Value?.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        dataGridView.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dataGridView.CurrentCell = null;
                        dataGridView.Rows[r.Index].Visible = false;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow r in dataGridView.Rows)
                {
                    dataGridView.Rows[r.Index].Visible = true;
                }               
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var highlightText = CommonSearchTextBox.Text.Trim();
            var highlightColumn = CBoxColumnName.Text;
            if (!string.IsNullOrWhiteSpace(highlightText) && !string.IsNullOrWhiteSpace(highlightColumn) && highlightText.Length >= MinimumSearchTextLength)
            {
                HighlightSearchText(e, highlightText, highlightColumn, dataGridView1);
            }
        }

        private void HighlightSearchText(DataGridViewCellPaintingEventArgs e, string highlightText, string highlightColumn, DataGridView dataGridView)
        {
            if (!string.IsNullOrWhiteSpace(highlightColumn) && !string.IsNullOrWhiteSpace(highlightText))
            {
                // High light and searching apply over selective fields of grid.  
                if (e.RowIndex > -1 && e.ColumnIndex > -1 && dataGridView.Columns[e.ColumnIndex].Name == highlightColumn)
                {
                    // Check data for search  
                    if (!String.IsNullOrWhiteSpace(highlightText))
                    {
                        String gridCellValue = e.FormattedValue.ToString();
                        // check the index of search text into grid cell.  
                        int startIndexInCellValue = gridCellValue.IndexOf(highlightText, StringComparison.OrdinalIgnoreCase);
                        // IF search text is exists inside grid cell then startIndexInCellValue value will be greater then 0 or equal to 0  
                        if (startIndexInCellValue >= 0)
                        {
                            e.Handled = true;
                            e.PaintBackground(e.CellBounds, true);
                            //the highlite rectangle  
                            Rectangle hl_rect = new Rectangle();
                            hl_rect.Y = e.CellBounds.Y + 2;
                            hl_rect.Height = e.CellBounds.Height - 5;
                            //find the size of the text before the search word in grid cell data.  
                            String sBeforeSearchword = gridCellValue.Substring(0, startIndexInCellValue);
                            //size of the search word in the grid cell data  
                            String sSearchWord = gridCellValue.Substring(startIndexInCellValue, highlightText.Length);
                            Size s1 = TextRenderer.MeasureText(e.Graphics, sBeforeSearchword, e.CellStyle.Font, e.CellBounds.Size);
                            Size s2 = TextRenderer.MeasureText(e.Graphics, sSearchWord, e.CellStyle.Font, e.CellBounds.Size);
                            if (s1.Width > 5)
                            {
                                hl_rect.X = e.CellBounds.X + s1.Width - 5;
                                hl_rect.Width = s2.Width - 6;
                            }
                            else
                            {
                                hl_rect.X = e.CellBounds.X + 2;
                                hl_rect.Width = s2.Width - 6;
                            }
                            //color for showing highlighted text in grid cell  
                            SolidBrush hl_brush;
                            hl_brush = new SolidBrush(System.Drawing.Color.Yellow);
                            //paint the background behind the search word  
                            e.Graphics.FillRectangle(hl_brush, hl_rect);
                            hl_brush.Dispose();
                            e.PaintContent(e.CellBounds);
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
                dataGridView1.ContextMenuStrip = contextMenuStrip1;
            this.ResolutionsDataGrid.Visible = true;
        }



        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceControl = (sender as ContextMenuStrip).SourceControl;
            var dataGridViewSource = sourceControl as DataGridView;
            if (dataGridViewSource != null)
            {
                CopyToClipboard(dataGridViewSource);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceControl = (sender as ContextMenuStrip).SourceControl;
            var dataGridViewSource = sourceControl as DataGridView;
            //Perform paste Operation
            PasteClipboardValue(dataGridViewSource);
        }

        private void CopyToClipboard(DataGridView dataGridViewSource)
        {
           // MessageBox.Show(dataGridView1.SelectedCells.GetType.);
            //MessageBox.Show(dataGridView1.GetClipboardContent().ToString());
            //Copy to clipboard
            DataObject dataObj = dataGridViewSource.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void PasteClipboardValue(DataGridView dataGridViewSource)
        {
            //Show Error if no cell is selected
            if (dataGridViewSource.SelectedCells.Count == 0)
            {
                MessageBox.Show("Please select a cell", "Paste",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Get the starting Cell
            DataGridViewCell startCell = GetStartCell(dataGridViewSource);
            //Get the clipboard value in a dictionary
            Dictionary<int, Dictionary<int, string>> cbValue =
                    ClipBoardValues(Clipboard.GetText());

            int iRowIndex = startCell.RowIndex;
            foreach (int rowKey in cbValue.Keys)
            {
                int iColIndex = startCell.ColumnIndex;
                foreach (int cellKey in cbValue[rowKey].Keys)
                {
                    //Check if the index is within the limit
                    if (iColIndex <= dataGridViewSource.Columns.Count - 1
                    && iRowIndex <= dataGridViewSource.Rows.Count - 1)
                    {
                        DataGridViewCell cell = dataGridViewSource[iColIndex, iRowIndex];

                        //Copy to selected cells if 'chkPasteToSelectedCells' is checked
                        //if ((chkPasteToSelectedCells.Checked && cell.Selected) ||
                        //    (!chkPasteToSelectedCells.Checked))
                        //    cell.Value = cbValue[rowKey][cellKey];
                    }
                    iColIndex++;
                }
                iRowIndex++;
            }
        }

        private DataGridViewCell GetStartCell(DataGridView dgView)
        {
            //get the smallest row,column index
            if (dgView.SelectedCells.Count == 0)
                return null;

            int rowIndex = dgView.Rows.Count - 1;
            int colIndex = dgView.Columns.Count - 1;

            foreach (DataGridViewCell dgvCell in dgView.SelectedCells)
            {
                if (dgvCell.RowIndex < rowIndex)
                    rowIndex = dgvCell.RowIndex;
                if (dgvCell.ColumnIndex < colIndex)
                    colIndex = dgvCell.ColumnIndex;
            }

            return dgView[colIndex, rowIndex];
        }

        private Dictionary<int, Dictionary<int, string>> ClipBoardValues(string clipboardValue)
        {
            Dictionary<int, Dictionary<int, string>>
            copyValues = new Dictionary<int, Dictionary<int, string>>();

            String[] lines = clipboardValue.Split('\n');

            for (int i = 0; i <= lines.Length - 1; i++)
            {
                copyValues[i] = new Dictionary<int, string>();
                String[] lineContent = lines[i].Split('\t');

                //if an empty cell value copied, then set the dictionary with an empty string
                //else Set value to dictionary
                if (lineContent.Length == 0)
                    copyValues[i][0] = string.Empty;
                else
                {
                    for (int j = 0; j <= lineContent.Length - 1; j++)
                        copyValues[i][j] = lineContent[j];
                }
            }
            return copyValues;
        }
        

        private void CBoxColumnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Will Initialize the Following Column to be the Search Column." + CBoxColumnName.Text);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            //MessageBox.Show("Please select a cell");
        }
       

        private void AuditGridFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            var searchText = AuditGridFilterTextBox.Text.Trim();
            FilterGrid(searchText, AuditGridFilterComboBox.Text, dataGridViewAuditRows);
        }

        private void AuditGridFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewAuditRows_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var highlightText = AuditGridFilterTextBox.Text.Trim();
            var highlightColumn = AuditGridFilterComboBox.Text;
            if (!string.IsNullOrWhiteSpace(highlightText) && !string.IsNullOrWhiteSpace(highlightColumn) && highlightText.Length >= MinimumSearchTextLength)
            {
                HighlightSearchText(e, highlightText, highlightColumn, dataGridViewAuditRows);
            }
        }

        private void dataGridViewAuditRows_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridViewAuditRows.SelectedCells.Count > 0)
                dataGridViewAuditRows.ContextMenuStrip = contextMenuStrip1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if(e.ColumnIndex >= 0 && senderGrid.Columns[e.ColumnIndex].Name =="Resolutions" && e.RowIndex >= 0)
            {
                ResolutionsDataGrid.Rows.Clear();
                var currentRow = dataGridView1.Rows[e.RowIndex];
                var hmg = currentRow.Cells["Group Name"].Value;
                var hn = currentRow.Cells["Hotel Name"].Value;
                var resolutions = currentRow.Cells["Resolutions"].Value.ToString();
                ResolutionsDataGrid.Rows.Add(hmg, hn, resolutions);
            }
        }

        private void textBoxNonSubmittalCount_TextChanged(object sender, EventArgs e)
        {

        }

        private void GroupNameAutoCompleteBox_TextChanged(object sender, EventArgs e)
        {
            if (GroupNameAutoCompleteBox.AutoCompleteCustomSource.Contains(GroupNameAutoCompleteBox.Text) || string.IsNullOrWhiteSpace(GroupNameAutoCompleteBox.Text))
            {
                var searchText = GroupNameAutoCompleteBox.Text;
                FilterGrid(searchText, "Group Name", dataGridView1);
            }
        }

        private void CBoxGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBoxColumnName.SelectedItem = CBoxColumnName.Items[0];
            CommonSearchTextBox.Text = CBoxGroupName.SelectedItem.ToString();
        }
    }


    internal class PRIInfo
    {
        public string PMSName { get; set; }
        public string Report1 { get; set; }
        public string Report2 { get; set; }
        public string Report3 { get; set; }
        public string Report4 { get; set; }
        public string Report5 { get; set; }
    }

    internal class HSMCInfo
    {
        public string GroupName { get; set; }
        public string GroupEmail { get; set; }
        public string GroupEmailPwd { get; set; }
    }

    internal class AuditMessage
    {
        public int GroupId { get; set; }
        public int HotelId { get; set; }
        public string TAProcessedDateTime { get; set; }
        public string TAMessageDateTime { get; set; }
        public string MYPProcessedDateTime { get; set; }
        public string MAPPControlProcessedDateTime { get; set; }
        public string MAPPControlDate { get; set; }
        public string MAPPControlCodeId { get; set; }
        public string MAPPLastActionStatus { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string AttachmentNames { get; set; }
        public string Headers { get; set; }
    }


}

