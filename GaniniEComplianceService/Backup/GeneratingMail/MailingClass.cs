using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Configuration;

namespace GeneratingMail
{
    public class MailingClass
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        int CVSHeadDays = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RemainCVSHead"].ToString());
        int FNHeadDays = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RemainFNHead"].ToString());
        string RemainderPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["RemainderPath"].ToString());
        string TopManagementPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TopManagementPath"].ToString());
        int Frequency = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Frequency"].ToString());
        ProcessBus Bus = new ProcessBus();
        DateTime date = System.DateTime.Now.Date;
        string Date1, Date2, Date3, Date4, Date5;
        DateTime FromDate;
        DateTime ToDate;
        int Flag = 0;
        string ErrorFlag = null;
        string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
        string IP = ConfigurationManager.AppSettings["IP"].ToString();
        int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["_Port"].ToString());
        string BodyMessage1 = ConfigurationManager.AppSettings["BodyMessage1"].ToString();
        string BodyMessage2 = ConfigurationManager.AppSettings["BodyMessage2"].ToString();
        string BodyMessage3 = ConfigurationManager.AppSettings["BodyMessage3"].ToString();
        string BodyMessage4 = ConfigurationManager.AppSettings["BodyMessage4"].ToString();
        string BodyMessage5 = ConfigurationManager.AppSettings["BodyMessage5"].ToString();
        string BodyMessage6 = ConfigurationManager.AppSettings["BodyMessage6"].ToString();
        string BodyMessage7 = ConfigurationManager.AppSettings["BodyMessage7"].ToString();
        public void RemainCVS()
        {
            ErrorFlag = "CVS Person";
            DataTable RemainderForTask = new DataTable();
            RemainderForTask = Bus.CheckForRemainder(CVSHeadDays);
            CallReport(RemainderForTask,CVSHeadDays);
        }
        public void RemainFNHead()
        {
            ErrorFlag = "FN Head";
            DataTable dt = new DataTable();
            dt = Bus.CheckForRemainder(FNHeadDays);
            CallReport(dt, FNHeadDays);
        }
        public void RemainTopManagement()
        {
            ErrorFlag = "Top Management";
            DataTable dt = new DataTable();
            ConstructDays(Frequency);
            int Flag = DateChecking();
            if (Flag == 1)
            {
                dt = Bus.SelectManagementDepartmentwiseReport(0, FromDate, ToDate);
                CallManagementReport(dt, FromDate, ToDate);
            }
            else
            {

            }
        }
        public void CallReport(DataTable RemainderForTask,int Days)
        {
            try
            {
                DataTable DeptDt = new DataTable();
                DataTable MailDt = new DataTable();
                MailDt.Columns.Add(new DataColumn("TaskID", typeof(string)));
                MailDt.Columns.Add(new DataColumn("CustomerName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("Applicable", typeof(string)));
                MailDt.Columns.Add(new DataColumn("ModelName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("SourceName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("EmailID", typeof(string)));
                MailDt.Columns.Add(new DataColumn("ActionStartDate", typeof(string)));
                MailDt.Columns.Add(new DataColumn("TaskDescription", typeof(string)));
                MailDt.Columns.Add(new DataColumn("ActionId", typeof(string)));
                MailDt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("ActionRequested", typeof(string)));
                MailDt.Columns.Add(new DataColumn("ActionDescription", typeof(string)));
                MailDt.Columns.Add(new DataColumn("StatusName", typeof(string)));
                MailDt.Columns.Add(new DataColumn("Date", typeof(string)));
                string EmailId = null,DepartmentName=null;
                DeptDt = Bus.SelectDepartment();
                foreach (DataRow dr in DeptDt.Rows)
                {
                    MailDt.Rows.Clear();
                    foreach (DataRow dr1 in RemainderForTask.Select("DepartmentName='" + dr["DepartmentName"].ToString() + "'"))
                    {
                        DataRow Maildr = MailDt.NewRow();
                        Maildr["TaskID"] = dr1["TaskID"].ToString();
                        Maildr["CustomerName"] = dr1["CustomerName"].ToString();
                        Maildr["ModelName"] = dr1["ModelName"].ToString();
                        Maildr["CategoryName"] = dr1["CategoryName"].ToString();
                        Maildr["SourceName"] = dr1["SourceName"].ToString();
                        Maildr["EmailID"] = dr1["EmailID"].ToString();
                        EmailId = dr1["EmailID"].ToString();
                        Maildr["ActionStartDate"] = dr1["ActionStartDate"].ToString();
                        Maildr["TaskDescription"] = dr1["TaskDescription"].ToString();
                        Maildr["ActionId"] = dr1["ActionId"].ToString();
                        Maildr["DepartmentName"] = dr1["DepartmentName"].ToString();
                        DepartmentName=dr1["DepartmentName"].ToString();
                        Maildr["ActionRequested"] = dr1["ActionRequested"].ToString();
                        Maildr["ActionDescription"] = dr1["ActionDescription"].ToString();
                        Maildr["StatusName"] = dr1["StatusName"].ToString();
                        Maildr["Date"] = dr1["Date"].ToString();
                        MailDt.Rows.Add(Maildr);
                    }
                    if(MailDt.Rows.Count>0 && EmailId!=null)
                    {
                        oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        oReport.Load(System.Configuration.ConfigurationManager.AppSettings["ReportDirectory"].ToString().Trim() + "RemainderForTask.rpt");
                        oReport.SetDataSource(MailDt);
                        oReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, RemainderPath);
                        oReport.Close();
                        oReport.Dispose();
                        string Subject = ConfigurationManager.AppSettings["Subject"].ToString();
                        StringBuilder sb = new StringBuilder();
                        sb.Append(BodyMessage1 + "\r\n");
                        sb.Append(BodyMessage2 + Days.ToString() + BodyMessage3+"\r\n");
                        sb.Append(BodyMessage4 + "\r\n\r\n\r\n");
                        sb.Append("   " + BodyMessage5 + "\r\n");
                        sb.Append(BodyMessage6 + "\r\n");
                        string BodyMessage = sb.ToString();
                        int Result=SendMail(FromMail, EmailId, RemainderPath, BodyMessage, Subject, IP, _Port);
                        if (Result == 8)// Send Success Msg If result=8
                        {
                            ErrorException("Remainder Mail Sent to "+DepartmentName," SuccessFully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorException("Log for RemainderMail", ex.ToString());
            }
            finally
            {

            }
        }
        public int SendMail(string FromMail,string ToMail,string Attachement,string BodyMessage,string Subject,string IP,int Port)
        {
            int Result = 0;
            try
            {
                MailMessage Mail = new MailMessage(FromMail, ToMail);
                Attachment att = new Attachment(Attachement);
                Mail.Attachments.Add(att);
                Mail.Subject = Subject;
                Mail.Body = BodyMessage;
                SmtpClient Client = new SmtpClient(IP, Port);
                Client.UseDefaultCredentials = true;
                Client.Send(Mail);
                Mail.Dispose();
                Result = 8;
            }
            catch (Exception ex)
            {
                ErrorException("Log for Mail Failure", ex.ToString());
            }
            finally
            {

            }
            return Result;
        }
        public void CallManagementReport(DataTable MgmtDt,DateTime FromDate,DateTime ToDate)
        {
            int Result = 0;
            try
            {
                oReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                oReport.Load(System.Configuration.ConfigurationManager.AppSettings["ReportDirectory"].ToString().Trim() + "TopManagement.rpt");
                oReport.SetDataSource(MgmtDt);
                oReport.SetParameterValue("@FromDate", FromDate.Date);
                oReport.SetParameterValue("@ToDate", ToDate.Date);
                oReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, TopManagementPath);
                oReport.Close();
                string Subject = ConfigurationManager.AppSettings["TopMgmtSubject"].ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(BodyMessage1 + "\r\n");
                sb.Append(BodyMessage7 + "\r\n\r\n\r\n");
                sb.Append("   " + BodyMessage5 + "\r\n");
                sb.Append(BodyMessage6 + "\r\n");
                string BodyMessage = sb.ToString();
                string ToMailId=GetTopMgmtEmailID();
                if (ToMailId != null)
                {
                    Result = SendMail(FromMail, ToMailId, TopManagementPath, BodyMessage, Subject, IP, _Port);
                    if (Result == 8)// Send Success Msg If result=8
                    {
                        ErrorException("Mail Send to Top Management", " SuccessFully");
                    }
                }
                else
                {
                    ErrorException("No Person Assigned as TopManagement", " Please Create");
                }
            }
            catch (Exception ex)
            {
                ErrorException("Log Data for TopManagement Report", ex.ToString());
            }
            finally
            {

            }
        }
        public void ConstructDays(int frequency)
        {
            string day = System.DateTime.Now.DayOfWeek.ToString();
            if (frequency == 1)
            {
                Date1 = date.Month.ToString() + "/01/" + date.Year.ToString();
                Date2 = date.Month.ToString() + "/" + DateTime.DaysInMonth(date.Year, date.Month).ToString() + "/" + date.Year.ToString();
            }
            else if (frequency == 2)
            {
                Date1 = date.Month.ToString() + "/01/" + date.Year.ToString();
                Date2 = date.Month.ToString() + "/15/" + date.Year.ToString();
                Date3 = date.Month.ToString() + "/"+DateTime.DaysInMonth(date.Year, date.Month).ToString()+"/" + date.Year.ToString();
            }
            else if (frequency == 3)
            {
                Date1 = date.Month.ToString() + "/01/" + date.Year.ToString();
                Date2 = date.Month.ToString() + "/10/" + date.Year.ToString();
                Date3 = date.Month.ToString() + "/20/" + date.Year.ToString();
                Date4 = date.Month.ToString() + "/" + DateTime.DaysInMonth(date.Year, date.Month).ToString() + "/" + date.Year.ToString();
            }
            //For frequency 4 date of the week may or may not saturday, so we no need to assign the Dates
            //else if (frequency == 4 && day == System.Configuration.ConfigurationManager.AppSettings["Dayofweek"].ToString())
            //{

            //    //Date1 = date.Month.ToString() + "/01/" + date.Year.ToString();
            //    //Date2 = date.Month.ToString() + "/07/" + date.Year.ToString();
            //    //Date3 = date.Month.ToString() + "/14/" + date.Year.ToString();
            //    //Date4 = date.Month.ToString() + "/21/" + date.Year.ToString();
            //    //Date5 = date.Month.ToString() + "/" + DateTime.DaysInMonth(date.Year, date.Month).ToString() + "/" + date.Year.ToString();
            //}
        }
        //Check the date is "Saturday" or not..
        public int DateFixing(DateTime Fromdate,DateTime Todate)
        {
            string day = System.DateTime.Now.DayOfWeek.ToString();
            double DayRange = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["DayRange"].ToString());
            int Flag = 0;
            if (day == System.Configuration.ConfigurationManager.AppSettings["Dayofweek"].ToString()&& Frequency==4)
            {
                ToDate = System.DateTime.Now;
                FromDate = ToDate.Date.AddDays(DayRange);//We are add -7 days to arrive at from date
                Flag = 1;
            }
            if(Frequency!=4)
            {
                FromDate = Convert.ToDateTime(Fromdate);
                ToDate = Convert.ToDateTime(Todate);
                Flag = 1;
            }
            return Flag;
        }
        // To check the selected date matches "Saturdays date"...
        public int DateChecking() 
        {
            //int Flag = 0;
            if (Frequency != 4)
            {
                if (System.DateTime.Now.Date == Convert.ToDateTime(Date2).Date && Date2 != null)
                {
                    Flag = DateFixing(Convert.ToDateTime(Date1), Convert.ToDateTime(Date2));
                }
                else if (System.DateTime.Now.Date == Convert.ToDateTime(Date3).Date && Date3 != null)
                {
                    Flag = DateFixing(Convert.ToDateTime(Date2), Convert.ToDateTime(Date3));
                }
                else if (System.DateTime.Now.Date == Convert.ToDateTime(Date4).Date && Date4 != null)
                {
                    Flag = DateFixing(Convert.ToDateTime(Date3), Convert.ToDateTime(Date4));
                }
                else if (System.DateTime.Now.Date == Convert.ToDateTime(Date5).Date && Date5 != null)
                {
                    Flag = DateFixing(Convert.ToDateTime(Date4), Convert.ToDateTime(Date5));
                }
                else
                {
                    Flag = 0;
                }
            }
            else
            {
                Flag = DateFixing(System.DateTime.Now.Date, System.DateTime.Now.Date);
            }
            return Flag;
        }
        public void ErrorException(string Message,string ex)
        {
            StringBuilder sb = new StringBuilder();
            string dat = System.DateTime.Now.ToString("ddMMMyyyy-");
            string path = System.Configuration.ConfigurationManager.AppSettings["LogFile"].ToString();
            string FileName = path + dat + ".txt";
            if (File.Exists(FileName) == false)
            {
                sb = new StringBuilder();
                sb.Append(Message +" "+ dat + ErrorFlag+"\r\n");
                sb.Append(ex+"\r\n");
                string AppDate = sb.ToString();
                File.AppendAllText(FileName, AppDate);
            }
            else
            {
                sb = new StringBuilder();
                sb.Append(Message +" "+ dat + ErrorFlag+"\r\n");
                sb.Append(ex + "\r\n");
                string AppDate = sb.ToString();
                File.AppendAllText(FileName, AppDate);
            }
        }
        public string GetTopMgmtEmailID()
        {
            string ToMail = Bus.SelectTopMgmtEmailId();
            return ToMail;
        }
    }
}
