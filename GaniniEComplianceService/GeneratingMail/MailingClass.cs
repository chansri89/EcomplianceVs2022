using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using Ganini.Lib;
using GeneratingMail.Messages;
//using Microsoft.Office.Interop.Excel;
using System.Web;
using Microsoft.CSharp;
#region cryptWeb
using GeneratingMail.WebActService;
using Ganini.Security;
using System.Security.Cryptography;
#endregion

namespace GeneratingMail
{
    public class MailingClass
    {
        ProcessBus Bus = new ProcessBus();
        string ErrorFlag = null;
        string FromMail = ConfigurationManager.AppSettings["FromMail"].ToString();
        string IP = ConfigurationManager.AppSettings["IP"].ToString();
        int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["_Port"].ToString());
        string path = System.Configuration.ConfigurationManager.AppSettings["LogFile"].ToString();
        string TestingFlag = Config.GetAppsetting("TestingFlag");
        int QuarterlyMonth = 0;
        bool QuarterlyTrigger = false;
        int HalfyearlyMonth = 0;
        bool HalfyearlyTrigger = false;
        int RedAlertDay = Convert.ToInt32(Config.GetAppsetting("RedAlertDay"));
        //string TestFileName = Config.GetAppsetting("TestFile");
        string TestFileName = Config.GetAppsetting("TestFile") + " " + DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd") + ".txt";
        StringBuilder sb = new StringBuilder();
        StringBuilder SbSummary = new StringBuilder();
        List<CoordinatorMailSummary> CoordSummaryLst = new List<CoordinatorMailSummary>();
        List<CoordinatorMailDetail> CoordDtlLst = new List<CoordinatorMailDetail>();
        #region webRef
        WebActService.Service1Client cli = new WebActService.Service1Client();
        private static WebActService.ClientChk ServerKey = new WebActService.ClientChk();
        WebActService.ClientChk Cliinf = new WebActService.ClientChk();
        WebActService.TransactMsg Tran = new WebActService.TransactMsg();
        List<WebActService.TransactMsg> TranList = new List<WebActService.TransactMsg>();
        private static List<ActMasterMsg> ActList = new List<ActMasterMsg>();
        Ganini.Lib.Util.Security Secur = new Ganini.Lib.Util.Security();
        string FirstColumn = "";
        KeyGen Key = new KeyGen();
        #endregion
        #region CoordinatorMail
        public void SendCoordinatorMail()
        {
            Int32 WSlNo = 0;
            AddTestFileData("Calling SendCoordinatorMail ");
            try
            {
                MonthGeneration();
                SbSummary = CoordSummary();
             
                var CoordVar = (from mail in CoordSummaryLst // Added this for sending Mail for 0 pending action and commented what was kept in detail below
                                select new { mail.CoordinatorEmployeeName, mail.ToEmail, mail.CompanyShortName }).Distinct().ToList();
                var SQuarter = (from mail in CoordSummaryLst
                                select new { mail.FinancialQuarter }).Distinct().ToList();
                string Subject = string.Empty;
                foreach (var xy in SQuarter)
                {
                    Subject = "Location Wise Activity Summary for Year " + xy.FinancialQuarter.ToString();
                    break;
                }

                CoordDtlLst = Bus.MailCoordinatorActionCompanywiseDetail();
                AddTestFileData("Activity details for Coordinator Got ");
                if (CoordDtlLst.Count > 0) //scs 190227 added If to ensure date is in MailDetailList
                {
                    //var CoordVar = (from mail in CoordDtlLst
                    //                            select new { mail.CoordinatorEmployeeName, mail.ToEmail,mail.CompanyShortName }).Distinct().ToList();
                    MailAddressCollection CCMail = new MailAddressCollection();

                    
                    #region MailConstruct
                    foreach (var Cdtl in CoordVar)
                    {
                        WSlNo = 0; int CordDetailMailId = 0; // set CordDetailMailId 0 to indicate no details avbl
                        sb = new StringBuilder();
                        sb.Append(SbSummary);
                        foreach (CoordinatorMailDetail cordMail in CoordDtlLst)
                        {
                            if (Cdtl.ToEmail == cordMail.ToEmail)
                            {
                                CordDetailMailId = 1;
                                break;
                            }
                        }

                        sb.Append("<H7>" + "Pending Activites Detail for Location:  " + Cdtl.CompanyShortName + "</br>"+ "</H7>" + "</br>");
                        sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><BR><table border=" + '"' + 5 + '"' + "><tr><th> SlNo </th><th>Execution Employee </th><th>Review Employee</th><th>Act</th><th>Activity</th><th>Seve rity</th><th>Frequency</th><th> Trigger Date </th><th> Due on Date </th></tr>");
                        if (!string.IsNullOrEmpty(Cdtl.CoordinatorEmployeeName))
                        {
                            CCMail = new MailAddressCollection();
                            //Subject = " Pending Activity Actions Mail to Coordinator " + SQuarter.ToString();
                            foreach (CoordinatorMailDetail mailDetails in CoordDtlLst)
                            {
                                if (CordDetailMailId == 1) // when Pending action is there show details scs 210916
                                {
                                    if (Cdtl.ToEmail == mailDetails.ToEmail)
                                    {
                                        WSlNo = WSlNo + 1;
                                        sb.Append("<tr>");
                                        sb.Append("<td>" + WSlNo.ToString() + "</td>");
                                        sb.Append("<td>" + mailDetails.ExecutionEmployeeName + "</td>");
                                        sb.Append("<td>" + mailDetails.ReviewEmployeeName + "</td>");
                                        sb.Append("<td>" + mailDetails.ActName + "</td>"); //scs190216
                                        sb.Append("<td>" + mailDetails.ActivityName + "</td>");
                                        sb.Append("<td>" + mailDetails.SeverityName + "</td>");
                                        sb.Append("<td>" + mailDetails.FrequencyName + "</td>");
                                        sb.Append("<td>" + mailDetails.TriggerDate.Substring(0, 10) + "</td>");
                                        sb.Append("<td>" + mailDetails.DueOn.Substring(0, 10) + "</td>");

                                        //sb.Append("<td>" + mailDetails.TriggerLeadTime + "</td>");
                                        sb.Append("</tr>");
                                    }
                                }
                                else// when Pending action is there show details scs 210916
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td>" + "" + "</td>");
                                    sb.Append("<td>" + "No Pending Activity" + "</td>");
                                    sb.Append("</tr>");
                                    break;
                                }
                            }
                            sb.Append("</table><BR><BR><BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                            int Result = SendMail(FromMail, Cdtl.ToEmail, CCMail, sb.ToString(), Subject , IP, _Port);
                            string CCMailsent = "";
                            CCMailsent = CCMail.ToString();
                            if (Result == 8)
                            {
                                AddTestFileData("Mail sent Successfully to " + Cdtl.ToEmail + " CC to " + CCMailsent);
                                ErrorException("Mail Sent Successfully to ", Cdtl.ToEmail);
                            }
                            else
                            {
                                AddTestFileData("Failure in Sending Mail " + Cdtl.ToEmail + " CC to " + CCMailsent);
                                ErrorException("Failure in Sending Mail to ", Cdtl.ToEmail);
                            }
                        }
                    }
                    #endregion
                } //scs added If to ensure date is in MailDetailList
                else
                {
                    ErrorException(" Bus.MailCoordinatorActionCompanywiseDetail did not send any data from database ", "");
                }
            }
            catch (Exception ex)
            {
                ErrorException("Error data for SendAllActivityMail ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
                
            }

        }
        private StringBuilder CoordSummary()
        {
            AddTestFileData("Getting Activity Summary for Coordinator ");
            StringBuilder SbSummary = new StringBuilder();
            string Heading = string.Empty;
            CoordSummaryLst = Bus.MailCoordinatorActionCompanySummary();
          
            var SQuarter = (from mail in CoordSummaryLst
                            select new { mail.FinancialQuarter }).Distinct().ToList();
            foreach(var xy in SQuarter)
            {
                Heading = "Location Wise Activity Summary for Year " + xy.FinancialQuarter.ToString();
                break;
            }
           //scs 230207 request to suppress duplicate record mai from kavviya 230206
            string PrevCompanyShortName = "";
            SbSummary.Append("<H7>" + Heading + "</H7>" + "</br>");
            SbSummary.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><BR><table border=" + '"' + 5 + '"' + "><tr><th>Location </th><th>Created</th><th>Completed</th><th>Pending</th><th>OpBal Pending</th></tr>");
            foreach (CoordinatorMailSummary mailDetails in CoordSummaryLst)
            {
                if (mailDetails.CompanyShortName != PrevCompanyShortName)
                {
                    PrevCompanyShortName = mailDetails.CompanyShortName;
                    SbSummary.Append("<tr>");
                    SbSummary.Append("<td>" + mailDetails.CompanyShortName + "</td>");
                    SbSummary.Append("<td>" + mailDetails.ActionsCreated + "</td>");
                    SbSummary.Append("<td>" + mailDetails.ActionsCompleted + "</td>");
                    SbSummary.Append("<td>" + mailDetails.ActionsPending + "</td>");
                    SbSummary.Append("<td>" + mailDetails.OpBalPending + "</td>");
                }

            }
            SbSummary.Append("</table><BR><BR>");
            return SbSummary;
        }
        #endregion
        #region Activity Mail
        public void SendAllActivityMail()
        {
            try
            {
                int CurrentDate = Convert.ToInt32(System.DateTime.Now.AddDays(RedAlertDay).ToString("yyyyMMdd")); // get the current date when mail message to change in red
                AddTestFileData("Calling SendAllActivityMail ");
                StringBuilder sb = new StringBuilder();
                List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
                MonthGeneration();
                MailDetailMsgList = Bus.GetActivityMailDetails(QuarterlyMonth, QuarterlyTrigger, HalfyearlyMonth, HalfyearlyTrigger);
                if (MailDetailMsgList.Count > 0) //scs 190227 added If to ensure date is in MailDetailList
                {
                    var ExectionEmployeeName = (from mail in MailDetailMsgList
                                                select new { mail.ExecutionEmployeeName, mail.ToEmail }).Distinct().ToList();
                    MailAddressCollection CCMail = new MailAddressCollection();

                    string Subject = string.Empty;
                    #region MailConstruct
                    foreach (var mailDetailMsg in ExectionEmployeeName)
                    {
                        sb = new StringBuilder();
                        sb.Append("<H7>" + "Activities pending beyond due dates will be sent to GroupLevel Person for GroupLevel Activity," + "</br>" + " CompanyAdmin for CompanyLevel Activity and Plant designated person for plant Level Activity" + "</H7>" + "</br>");
                        if (Config.GetAppsetting("ReviewerColRequired") == "Y" && Config.GetAppsetting("ExecutionColRequired") == "Y")
                        {
                            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Activities to be Completed are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Execution Employee</th><th>Review Employee</th><th>Due Year</th><th>Due Month</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>");
                        }
                        else if (Config.GetAppsetting("ReviewerColRequired") == "Y")
                        {
                            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Activities to be Completed are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Review Employee</th><th>Due Year</th><th>Due Month</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>");
                        }
                        else if (Config.GetAppsetting("ExecutionColRequired") == "Y")
                        {
                            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Activities to be Completed are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Execution Employee</th<th>Due Year</th>><th>Due Month</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>");
                        }
                        else
                        {
                            sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Activities to be Completed are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Due Year</th><th>Due Month</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>");
                        }

                        if (!string.IsNullOrEmpty(mailDetailMsg.ExecutionEmployeeName))
                        {
                            CCMail = new MailAddressCollection();
                            foreach (MailDetailMsg mailDetails in MailDetailMsgList)
                            {
                                if (mailDetailMsg.ExecutionEmployeeName == mailDetails.ExecutionEmployeeName)
                                {
                                    //Subject = mailDetails.FrequencyName; --commeted by abinayaa 2203413
                                    Subject = "Activities";
                                    MailAddress ma = new MailAddress(mailDetails.CCMail);
                                    if (!CCMail.Contains(ma) && mailDetails.CCMail != mailDetailMsg.ToEmail)
                                    {
                                        CCMail.Add(mailDetails.CCMail);
                                    }
                                    int DueDate = Convert.ToInt32(mailDetails.DueYear) * 10000 + Convert.ToInt32(mailDetails.DueMonth) * 100 + Convert.ToInt32(mailDetails.DueDate);
                                    if (DueDate < CurrentDate) //scs19022017 red should be displayed somany days before the due date
                                    //if (Convert.ToInt32(mailDetails.DueDate) <= RedAlertDay + Convert.ToInt32(mailDetails.TriggerDate) && Convert.ToInt32(mailDetails.DueDate) != 0)
                                    {
                                        sb.Append("<tr style=" + '"' + "color:Red" + '"' + ">");
                                    }
                                    else
                                    {
                                        sb.Append("<tr>");
                                    }
                                    sb.Append("<td>" + mailDetails.ActName+ " - " + mailDetailMsg.ToEmail + "</td>");
                                    sb.Append("<td>" + mailDetails.ActivityName + "</td>");
                                    if (Config.GetAppsetting("ExecutionColRequired") == "Y")
                                    {
                                        sb.Append("<td>" + mailDetails.ExecutionEmployeeName + "</td>");
                                    }
                                    if (Config.GetAppsetting("ReviewerColRequired") == "Y")
                                    {
                                        sb.Append("<td>" + mailDetails.ReviewEmployeeName + "</td>");
                                    }
                                    if (mailDetails.DueMonth == "0")
                                    {
                                        mailDetails.DueMonth = "NA";
                                    }
                                    if (mailDetails.DueDate == "0")
                                    {
                                        mailDetails.DueDate = "NA";
                                    }
                                    sb.Append("<td>" + mailDetails.DueYear + "</td>"); //scs190216
                                    sb.Append("<td>" + mailDetails.DueMonth + "</td>");
                                    sb.Append("<td>" + mailDetails.DueDate + "</td>");
                                    sb.Append("<td>" + mailDetails.DueDay + "</td>");
                                    sb.Append("<td>" + mailDetails.FrequencyName + "</td>");
                                    sb.Append("</tr>");
                                }
                            }
                            sb.Append("</table><BR><BR><BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                            int Result = SendMail(FromMail, mailDetailMsg.ToEmail, CCMail, sb.ToString(), Subject + " - Reminder Mail", IP, _Port);
                            string CCMailsent = "";
                            CCMailsent = CCMail.ToString();
                            if (Result == 8)
                            {
                                AddTestFileData("Mail sent Successfully to " + mailDetailMsg.ToEmail + " CC to " + CCMailsent);
                                ErrorException("Mail Sent Successfully to ", mailDetailMsg.ToEmail);
                            }
                            else
                            {
                                AddTestFileData("Failure in Sending Mail " + mailDetailMsg.ToEmail + " CC to " + CCMailsent);
                                ErrorException("Failure in Sending Mail to ", mailDetailMsg.ToEmail);
                            }
                        }
                    }
                    #endregion
                } //scs added If to ensure date is in MailDetailList
                else
                {
                    ErrorException(" Bus.GetActivityMailDetails did not send any data ", "");
                }
            }
            catch (Exception ex)
            {
                ErrorException("Error data for SendAllActivityMail ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
                
            }
        }
        /// <summary>
        /// To created a New Activity action for Monthly,Half Yearly,Quaterly and Yearly
        /// </summary>
        public void CreateNewActivityAction()
        {
            AddTestFileData("Calling NewActivityActionInsert");
            try
            {
               
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Calling NewActivityActionInsert" + "\r\n");
                //}
                Bus.NewActivityActionInsert();
            }
            catch (Exception ex)
            {
                ErrorException("Log data for Creating New ActivityAciton ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public void SendRedAlertMailForActivity()
        {
            try
            {
                SendRedAlertMailForGroupActivity();
                SendRedAlertMailForCompanyAdminActivity();
                SendRedAlertMailForPlantActivity();
            }
            catch (Exception ex)
            {
                ErrorException("Log data for SendRedAlertMailForActivity ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public void SendRedAlertMailForGroupActivity()
        {
            AddTestFileData("Calling Send Red Alert Mail For Group Level Activity ");
            try
            {
                //scs 160315 commented below to take the MailTo from data CompanyHRMailId
                //if(Config.GetAppsetting("MailTo")==string.Empty)
                //{
                //    //return;
                //}
                String CompanyHRMailId = "";
                StringBuilder sb = new StringBuilder();
               
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Calling Send Red Alert Mail For Group Level Activity" + "\r\n");
                //}
                List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
                MailDetailMsgList = Bus.GetGroupActivityMailForRedAlert();
                MailAddressCollection CCMail = new MailAddressCollection();

                var LocationName = (from mail in MailDetailMsgList
                                    select new { mail.LocationName, mail.SeverityName }).Distinct().ToList();

                string Subject = string.Empty;
                if (Config.GetAppsetting("MailCC") != string.Empty)
                {
                    CCMail.Add(Config.GetAppsetting("MailCC"));
                }

                foreach (var Location in LocationName)
                {
                    //var MailDetailList = (from mail in MailDetailMsgList
                    //                      where mail.LocationName.ToLower() == Location.LocationName.ToLower()
                                          //select mail).ToList();
                    //send all grplevel activity to all grp level employees
                    var MailDetailList = (from mail in MailDetailMsgList where 1 == 1  select mail).ToList();
                    sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Group Level Activities in Severity " + '"' + Location.SeverityName + '"' + " are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Location Code </th><th>Execution Employee</th><th>Review Employee</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>"); //<th>Due Date</th> //+ " for " + '"' + Location.LocationName + '"'
                    foreach (var mailList in MailDetailList)
                    {
                        if (mailList.EmpType.ToLower() == "grp")
                        {
                            CompanyHRMailId = mailList.CompanyHRMailId;
                        }
                        else
                        {
                            CompanyHRMailId = Config.GetAppsetting("MailTo");
                        }
                        sb.Append("<tr style=" + '"' + "color:Red" + '"' + ">");
                        sb.Append("<td>" + mailList.ActName + "</td>");
                        sb.Append("<td>" + mailList.ActivityName + "</td>");
                        sb.Append("<td>" + mailList.LocationName + "</td>");
                        sb.Append("<td>" + mailList.ExecutionEmployeeName + "</td>");
                        sb.Append("<td>" + mailList.ReviewEmployeeName + "</td>");
                        if (mailList.DueMonth == "0")
                        {
                            mailList.DueMonth = "NA";
                        }
                        if (mailList.DueDate == "0")
                        {
                            mailList.DueDate = "NA";
                        }
                        if (mailList.DueDay == "0")
                        {
                            mailList.DueDay = "NA";
                        }
                        sb.Append("<td>" + mailList.DueYear + "-" + mailList.DueMonth.PadLeft(2,'0')  +"-" + mailList.DueMonth.PadLeft(2,'0') + "</td>");
                        //sb.Append("<td>" + mailList.DueMonth + "</td>");
                        //sb.Append("<td>" + mailList.DueDate + "</td>");
                        sb.Append("<td>" + mailList.DueDay + "</td>");
                        sb.Append("<td>" + mailList.FrequencyName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("<BR>");

                    if (sb.ToString() != string.Empty)
                    {
                        sb.Append("<BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                        //scs 160315 commented below line added one below
                        // int Result = SendMail(FromMail, ConfigurationManager.AppSettings["MailTo"].ToString(), CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        int Result = SendMail(FromMail, CompanyHRMailId, CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        string CCMailsent = "";
                        CCMailsent = CCMail.ToString();
                        if (CCMailsent == string.Empty)
                        {
                            CCMailsent = "None";
                        }

                        if (Result == 8)
                        {
                            AddTestFileData("Successfully Sent RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent);
                            ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                            sb.Clear();
                        }
                        else
                        {
                            AddTestFileData("Failure in Sending RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent); ;
                        }
                        //if (Result == 8)
                        //{
                        //    ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                        //}
                    }
                    break;
               }
            }
            catch (Exception ex)
            {
                ErrorException("Log data for SendRedAlertMailForActivity ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public void SendRedAlertMailForCompanyAdminActivity()
        {
            AddTestFileData("Calling SendRedAlertMailForCompanyActivity ");
            try
            {
                //scs 160315 commented below to take the MailTo from data CompanyHRMailId
                //if(Config.GetAppsetting("MailTo")==string.Empty)
                //{
                //    //return;
                //}
                String CompanyHRMailId = "";
                
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Calling SendRedAlertMailForCompanyActivity" + "\r\n");
                //}
                List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
                MailDetailMsgList = Bus.GetCompAdminActivityMailForRedAlert();
                MailAddressCollection CCMail = new MailAddressCollection();

                //var LocationName = (from mail in MailDetailMsgList
                //                    select new { mail.LocationName, mail.SeverityName }).Distinct().ToList();
                var LocationName = (from mail in MailDetailMsgList
                                    select new { mail.ParentCompanyCode, mail.SeverityName }).Distinct().ToList(); 
                string Subject = string.Empty;
                if (Config.GetAppsetting("MailCC") != string.Empty)
                {
                    CCMail.Add(Config.GetAppsetting("MailCC"));
                }

                foreach (var Location in LocationName)
                {
                    StringBuilder sb = new StringBuilder();
                    var MailDetailList = (from mail in MailDetailMsgList
                                          where mail.ParentCompanyCode.ToLower() == Location.ParentCompanyCode.ToLower()
                                          select mail).ToList();
                    sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending company Level Activities in Severity " + '"' + Location.SeverityName + '"' + " for " + " are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Location Code </th><th>Execution Employee</th><th>Review Employee</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>"); //<th>Due Month</th>
//                    sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Group Level Activities in Severity " + '"' + Location.SeverityName + '"' + " are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Location Code </th><th>Execution Employee</th><th>Review Employee</th><th>Due Date</th><th>Due Day</th><th>Frequency Name</th></tr>"); //<th>Due Date</th> //+ " for " + '"' + Location.LocationName + '"'
                    foreach (var mailList in MailDetailList)
                    {
                        if (mailList.EmpType.ToLower() == "ca")
                        {
                            CompanyHRMailId = mailList.CompanyHRMailId;
                        }
                        else
                        {
                            CompanyHRMailId = Config.GetAppsetting("MailTo");
                        }
                        sb.Append("<tr style=" + '"' + "color:Red" + '"' + ">");
                        sb.Append("<td>" + mailList.ActName + "</td>");
                        sb.Append("<td>" + mailList.ActivityName + "</td>");
                        sb.Append("<td>" + mailList.LocationName + "</td>");
                        sb.Append("<td>" + mailList.ExecutionEmployeeName + "</td>");
                        sb.Append("<td>" + mailList.ReviewEmployeeName + "</td>");
                        if (mailList.DueMonth == "0")
                        {
                            mailList.DueMonth = "NA";
                        }
                        if (mailList.DueDate == "0")
                        {
                            mailList.DueDate = "NA";
                        }
                        if (mailList.DueDay == "0")
                        {
                            mailList.DueDay = "NA";
                        }
                        sb.Append("<td>" + mailList.DueYear + "-" + mailList.DueMonth.PadLeft(2, '0') + "-" + mailList.DueMonth.PadLeft(2, '0') + "</td>");
                        //sb.Append("<td>" + mailList.DueMonth + "</td>");
                        //sb.Append("<td>" + mailList.DueDate + "</td>");
                        sb.Append("<td>" + mailList.DueDay + "</td>");
                        sb.Append("<td>" + mailList.FrequencyName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("<BR>");

                    if (sb.ToString() != string.Empty)
                    {
                        sb.Append("<BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                        //scs 160315 commented below line added one below
                        // int Result = SendMail(FromMail, ConfigurationManager.AppSettings["MailTo"].ToString(), CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        int Result = SendMail(FromMail, CompanyHRMailId, CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        string CCMailsent = "";
                        CCMailsent = CCMail.ToString();
                        if (CCMailsent == string.Empty)
                        {
                            CCMailsent = "None";
                        }
                        if (Result == 8)
                        {
                            AddTestFileData("Successfully Sent RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent);
                            ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                            sb.Clear();
                        }
                        else
                        {
                            AddTestFileData("Failure in Sending RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent); ;
                        }
                        //if (Result == 8)
                        //{
                        //    ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorException("Log data for SendRedAlertMailForActivity ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public void SendRedAlertMailForPlantActivity()
        {
            AddTestFileData("Calling SendRedAlertMailForActivity ");
            try
            {
                //scs 160315 commented below to take the MailTo from data CompanyHRMailId
                //if(Config.GetAppsetting("MailTo")==string.Empty)
                //{
                //    //return;
                //}
               
                String CompanyHRMailId = "";
               
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Calling SendRedAlertMailForActivity" + "\r\n");
                //}
                List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
                MailDetailMsgList = Bus.GetPlantActivityMailForRedAlert();
                MailAddressCollection CCMail = new MailAddressCollection();

                var LocationName = (from mail in MailDetailMsgList
                                    select new { mail.LocationName, mail.SeverityName }).Distinct().ToList();

                string Subject = string.Empty;
                if (Config.GetAppsetting("MailCC") != string.Empty)
                {
                    CCMail.Add(Config.GetAppsetting("MailCC"));
                }

                foreach (var Location in LocationName)
                {
                    StringBuilder sb = new StringBuilder();
                    var MailDetailList = (from mail in MailDetailMsgList
                                          where mail.LocationName.ToLower() == Location.LocationName.ToLower()
                                          select mail).ToList();
                    sb.Append("<style type=" + '"' + "text/css" + '"' + ">           table {font-size: 75%;}           </style><H7>The List of pending Activities in Severity " + '"' + Location.SeverityName + '"' + " are as follows :</H7><BR><BR><table border=" + '"' + 5 + '"' + "><tr><th>Act Name </th><th>Activity Name</th><th>Location Code </th><th>Execution Employee</th><th>Review Employee</th><th>Due Month</th><th>Due Date</th><th>Frequency Name</th></tr>"); //<th>Due Day</th> //+ " for " + '"' + Location.LocationName + '"'
                    foreach (var mailList in MailDetailList)
                    {
                        if (mailList.EmpType.ToLower() == "pl")
                        {
                            CompanyHRMailId = mailList.CompanyHRMailId;
                        }
                        else
                        {
                            CompanyHRMailId = Config.GetAppsetting("MailTo");
                        }
                        sb.Append("<tr style=" + '"' + "color:Red" + '"' + ">");
                        sb.Append("<td>" + mailList.ActName + "</td>");
                        sb.Append("<td>" + mailList.ActivityName + "</td>");
                        sb.Append("<td>" + mailList.LocationName + "</td>");
                        sb.Append("<td>" + mailList.ExecutionEmployeeName + "</td>");
                        sb.Append("<td>" + mailList.ReviewEmployeeName + "</td>");
                        if (mailList.DueYear == "0") //scs190216
                        {
                            mailList.DueYear = "NA";
                        } 
                        if (mailList.DueMonth == "0")
                        {
                            mailList.DueMonth = "NA";
                        }
                        if (mailList.DueDate == "0")
                        {
                            mailList.DueDate = "NA";
                        }
                        if (mailList.DueDay == "0")
                        {
                            mailList.DueDay = "NA";
                        }
                        //sb.Append("<td>" + mailList.DueYear + "-" + mailList.DueMonth.PadLeft(2, '0') + "-" + mailList.DueMonth.PadLeft(2, '0') + "</td>"); //scs180616
                        sb.Append("<td>" + mailList.DueYear + "-" + mailList.DueMonth.PadLeft(2, '0') + "-" + mailList.DueDate.PadLeft(2, '0') + "</td>");
                        //sb.Append("<td>" + mailList.DueMonth + "</td>");
                        //sb.Append("<td>" + mailList.DueDate + "</td>");
                        sb.Append("<td>" + mailList.DueDay + "</td>");
                        sb.Append("<td>" + mailList.FrequencyName + "</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("<BR>");

                    if (sb.ToString() != string.Empty)
                    {
                        sb.Append("<BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                        //scs 160315 commented below line added one below
                        // int Result = SendMail(FromMail, ConfigurationManager.AppSettings["MailTo"].ToString(), CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        int Result = SendMail(FromMail, CompanyHRMailId, CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                        string CCMailsent = "";
                        CCMailsent = CCMail.ToString();
                        if (CCMailsent == string.Empty)
                        {
                            CCMailsent = "None";
                        }
                        if (Result == 8)
                        {
                            AddTestFileData("Successfully Sent RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent);
                            ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                            sb.Clear();
                        }
                        else
                        {
                            AddTestFileData("Failure in Sending RedAlertMail to " + CompanyHRMailId + " CC to " + CCMailsent); ;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorException("Log data for SendRedAlertMailForActivity ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public void SendRedAlertMailForActivitywithExcel()
        {
            AddTestFileData("Calling SendRedAlertMailForActivitywithExcel ");
            //try
            //{
            //    if (Config.GetAppsetting("MailTo") == string.Empty)
            //    {
            //        return;
            //    }
               
            //    //if (Config.GetAppsetting("TestingFlag") == "Y")
            //    //{
            //    //    File.AppendAllText(TestFileName, "Calling SendRedAlertMailForActivitywithExcel" + "\r\n");
            //    //}
            //    List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            //    MailDetailMsgList = Bus.GetActivityMailForRedAlert();
            //    MailAddressCollection CCMail = new MailAddressCollection();
                
            //    var LocationName = (from mail in MailDetailMsgList
            //                        select new { mail.LocationName, mail.SeverityName }).Distinct().ToList();
            //    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //    Workbook xlb;
            //    _Worksheet xls;
            //    Range oCells;
            //    object m = Type.Missing;
                
            //    xlb = xla.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //    xls = (_Worksheet)xlb.Worksheets.Add(m, m, m, m);
            //    xls.Activate();
            //    xla.Cells.Font.Size = 8;
            //    string Subject = string.Empty;
            //    if (Config.GetAppsetting("MailCC") != string.Empty)
            //    {
            //        CCMail.Add(Config.GetAppsetting("MailCC"));
            //    }

            //    int Row = 1;
            //    foreach (var Location in LocationName)
            //    {
            //        ((Range)xls.Cells[Row, 2]).EntireRow.Font.Bold = true;
            //        xls.Cells[Row, 2] = "The List of pending Activities in Severity " + Location.SeverityName + " for " + Location.LocationName + " are as follows:";

            //        Row = Row + 1;
            //        ((Range)xls.Cells[Row, 1]).EntireColumn.ColumnWidth = 10.71;
            //        ((Range)xls.Cells[Row, 1]).Font.Size = 9;
            //        xls.Cells[Row, 1] = "Act Name";
            //        ((Range)xls.Cells[Row, 1]).EntireRow.Font.Bold = true;
            //        ((Range)xls.Cells[Row, 2]).EntireColumn.ColumnWidth = 7.14;
            //        ((Range)xls.Cells[Row, 2]).Font.Size = 9;
            //        xls.Cells[Row, 2] = "Actiivity Name";
            //        ((Range)xls.Cells[Row, 3]).EntireColumn.ColumnWidth = 7.14;
            //        ((Range)xls.Cells[Row, 3]).Font.Size = 9;
            //        xls.Cells[Row, 3] = "Execution Employee";
            //        ((Range)xls.Cells[Row, 4]).Font.Size = 9;
            //        ((Range)xls.Cells[Row, 4]).EntireColumn.ColumnWidth = 11.57;
            //        xls.Cells[Row, 4] = "Review Employee";
            //        ((Range)xls.Cells[Row, 5]).Font.Size = 9;
            //        ((Range)xls.Cells[Row, 5]).EntireColumn.ColumnWidth = 13.71;
            //        xls.Cells[Row, 5] = "Due Month";
            //        ((Range)xls.Cells[Row, 6]).Font.Size = 9;
            //        ((Range)xls.Cells[Row, 6]).EntireColumn.ColumnWidth = 8.66;
            //        xls.Cells[Row, 6] = "Due Date";
            //        ((Range)xls.Cells[Row, 7]).Font.Size = 9;
            //        ((Range)xls.Cells[Row, 7]).EntireColumn.ColumnWidth = 20.17;
            //        xls.Cells[Row, 7] = "Due Day";
            //        ((Range)xls.Cells[Row, 8]).Font.Size = 9;
            //        ((Range)xls.Cells[Row, 8]).EntireColumn.ColumnWidth = 11.86;
            //        xls.Cells[Row, 8] = "Frequency Name";
            //        Row = Row + 1;
            //        var MailDetailList = (from mail in MailDetailMsgList
            //                              where mail.LocationName.ToLower() == Location.LocationName.ToLower()
            //                              select mail).ToList();
            //        foreach (var mailList in MailDetailList)
            //        {
            //            ((Range)xls.Cells[Row, 1]).EntireRow.Font.Color = System.Drawing.Color.Red;
            //            if (mailList.DueMonth == "0")
            //            {
            //                mailList.DueMonth = "NA";
            //            }
            //            if (mailList.DueDate == "0")
            //            {
            //                mailList.DueDate = "NA";
            //            }
            //            if (mailList.DueDay == "0")
            //            {
            //                mailList.DueDay = "NA";
            //            }
            //            xls.Cells[Row, 1] = mailList.ActName;
            //            xls.Cells[Row, 2] = mailList.ActivityName;
            //            xls.Cells[Row, 3] = mailList.ExecutionEmployeeName;
            //            xls.Cells[Row, 4] = mailList.ReviewEmployeeName;
            //            xls.Cells[Row, 5] = mailList.DueMonth;
            //            xls.Cells[Row, 6] = mailList.DueDate;
            //            xls.Cells[Row, 7] = mailList.DueDay;
            //            xls.Cells[Row, 8] = mailList.FrequencyName;
            //            Row = Row + 1;
            //        }
            //        Row = Row + 1;
            //    }
            //    if (File.Exists(@Config.GetAppsetting("AttachFilePath").ToString()))
            //    {
            //        File.Delete(@Config.GetAppsetting("AttachFilePath").ToString());
            //    }
            //    xlb.SaveAs(@Config.GetAppsetting("AttachFilePath").ToString() + "", m, m, m, m, m, XlSaveAsAccessMode.xlShared, m, m, m, m, m);
            //    if (File.Exists(@Config.GetAppsetting("AttachFilePath").ToString()))
            //    {
            //        Attachment attach = new Attachment(@Config.GetAppsetting("AttachFilePath").ToString());
            //        StringBuilder sb = new StringBuilder();
            //        sb.Append("<H3>"+Config.GetAppsetting("BodyMessage")+"</H3>");
            //        sb.Append("<BR><BR><H7>"+'"'+Config.GetAppsetting("BodyMessage2")+'"'+"</H7>");
            //        int Result = SendMail(FromMail, ConfigurationManager.AppSettings["MailTo"].ToString(), CCMail, sb.ToString(), Config.GetAppsetting("Subject"), IP, _Port, attach);
            //        string CCMailsent = "";
            //        CCMailsent = CCMail.ToString();
            //        if (CCMailsent == string.Empty)
            //        {
            //            CCMailsent = "None";
            //        }
            //        if (Result == 8)
            //        {
            //            AddTestFileData("Successfully Sent RedAlertMail to " + ConfigurationManager.AppSettings["MailTo"].ToString() + " CC to " + CCMailsent);
            //            ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
            //            sb.Clear();
            //        }
            //        else
            //        {
            //            AddTestFileData("Failure in Sending RedAlertMail to " + ConfigurationManager.AppSettings["MailTo"].ToString() + " CC to " + CCMailsent); ;
            //        }
            //        //if (Result == 8)
            //        //{
            //        //    ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
            //        //}
            //        foreach (System.Diagnostics.Process pro in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            //        {
            //            pro.Kill();
            //        }
            //        File.Delete(@Config.GetAppsetting("AttachFilePath").ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorException("Log data for SendRedAlertMailForActivitywithExcel ", ex.Message.ToString());
            //    ExceptionHandling eh = new ExceptionHandling();
            //    eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            //}
            //finally
            //{
            //}
        }
        /// <summary>
        /// To created a New Activity action for Weekly
        /// </summary>
        public void CreateNewActivityActionWeekly()
        {
            try
            {
                Bus.CreateNewActivityActionWeekly();
            }
            catch (Exception ex)
            {
                ErrorException("Log data for Creating New ActivityAciton ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }
        public int SendMail(string FromMail, string ToMail,MailAddressCollection CC, string BodyMessage, string Subject, string IP, int Port)
        {
            int Result = 0;
            try
            {
                AddTestFileData("Sending Mail To " + ToMail);
                MailMessage Mail = new MailMessage(FromMail, ToMail);
                if (CC.ToString() != string.Empty)
                {
                    Mail.CC.Add(CC.ToString());
                }
                Mail.Subject = Subject;
                Mail.Body = BodyMessage;
                Mail.IsBodyHtml = true;
                SmtpClient Client = new SmtpClient(IP, Port);
                Client.UseDefaultCredentials = true;
                Client.Send(Mail);
                Mail.Dispose();
                Result = 8;

            }
            catch (Exception ex)
            {
                ErrorException("Log for Mail Failure", ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
                
            }
            return Result;
        }
        public int SendMail(string FromMail, string ToMail, MailAddressCollection CC, string BodyMessage, string Subject, string IP, int Port,Attachment attachfile)
        {
            int Result = 0;
            try
            {
                AddTestFileData("Sending Mail ");
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Sending Mail" + "\r\n");
                //}
                MailMessage Mail = new MailMessage(FromMail, ToMail);
                if (CC.ToString() != string.Empty)
                {
                    Mail.CC.Add(CC.ToString());
                }
                Mail.Subject = Subject;
                Mail.Body = BodyMessage;
                Mail.IsBodyHtml = true;
                Mail.Attachments.Add(attachfile);
                SmtpClient Client = new SmtpClient(IP, Port);
                Client.UseDefaultCredentials = true;
                Client.Send(Mail);
                Mail.Dispose();
                Result = 8;
            }
            catch (Exception ex)
            {
                ErrorException("Log for Mail Failure", ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            return Result;
        }
        public void ErrorException(string Message,string ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string dat = System.DateTime.Now.ToString("ddMMMyyyy-");
               // string path = System.Configuration.ConfigurationManager.AppSettings["LogFile"].ToString();
                string FileName = path + dat + ".txt";
                if (File.Exists(FileName) == false)
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat + ErrorFlag + "\r\n");
                    sb.Append(ex + "\r\n");
                    string AppDate = sb.ToString();
                    File.AppendAllText(FileName, AppDate);
                }
                else
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat + ErrorFlag + "\r\n");
                    sb.Append(ex + "\r\n");
                    string AppDate = sb.ToString();
                    File.AppendAllText(FileName, AppDate);
                }
            }
            catch (Exception Ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
                
            }
        }
        //public void AddTestFileData(string Message)
        //{
        //    try
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        string dat = System.DateTime.Now.ToString("ddMMMyyyy");
        //        string path = System.Configuration.ConfigurationManager.AppSettings["TestFile"].ToString();
        //        string FileName = path + dat + ".txt";
        //        //if (File.Exists(FileName) == false)
        //        //{
        //            sb = new StringBuilder();
        //            sb.Append(Message + " " + dat  + "\r\n");
        //            //sb.Append(ex + "\r\n");
        //            string AppDate = sb.ToString();
        //            File.AppendAllText(FileName, AppDate);
        //        //}
        //        //else
        //        //{
        //        //    sb = new StringBuilder();
        //        //    sb.Append(Message + " " + dat  + "\r\n");
        //        //   // sb.Append(ex + "\r\n");
        //        //    string AppDate = sb.ToString();
        //        //    File.AppendAllText(FileName, AppDate);
        //        //}
        //    }
        //    catch (Exception Ex)
        //    {
        //        ExceptionHandling eh = new ExceptionHandling();
        //        eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
        //    }
        //    finally
        //    {

        //    }
        //}

        public void AddTestFileData(string TextMsg)
        {
            //if (Config.GetAppsetting("TestingFlag") == "Y") 
            if (TestingFlag == "Y") // gettig Testing Flag once from top of this document
            {
                try
                {
                    // File.AppendAllText(TestFileName, "\r\n" + TextMsg + System.DateTime.Now + "\r\n");
                    File.AppendAllText(TestFileName, TextMsg + System.DateTime.Now + "\r\n"); // scs 190219 file.append with direct string writing does not work. hence commented and added below
                    //ErrorException(TextMsg + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm"), "");
                    
                }
                catch
                {
                    ExceptionHandling eh = new ExceptionHandling();
                    eh.HandleException(TextMsg, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Service);
                }
            }
        }


        public void MonthGeneration()
        {
            int i=0;
            //Quaterly Trigger
            for (i = 1; i <= 12; i = i + 3)
            {
                int Month = 0;
                Month = i;
                if (Month > 12)
                {
                    Month = 1;
                }
                if (System.DateTime.Now.Month == Month)
                {
                    QuarterlyTrigger = true;
                    QuarterlyMonth = Month;
                    break;
                }
            }
            i = 0;
            //HalfYear Trigger
            for (i = 3; i <= 12; i = i + 6)
            {
                if (System.DateTime.Now.Month == i)
                {
                    HalfyearlyMonth = i;
                    HalfyearlyTrigger = true;
                    break;
                }
            }
        }
        public void ExcelGenration(List<MailDetailMsg> MailDetailMsgList)
        {
            //Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            //Workbook xlb;
            //_Worksheet xls;
            //Range oCells;
            //object m = Type.Missing;
            //xlb = xla.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //xls = (_Worksheet)xlb.Worksheets.Add(m, m, m, m);
            //xls.Activate();
            //xla.Cells.Font.Size = 8;
            //var LocationName = (from mail in MailDetailMsgList
            //                    select new { mail.LocationName, mail.SeverityName }).Distinct().ToList();
            //int Row = 1;
            //foreach (var Location in LocationName)
            //{
            //    ((Range)xls.Cells[Row, 1]).EntireRow.Font.Bold = true;
            //    xls.Cells[Row, 1] = "The List of pending Activities in Severity "+Location.SeverityName+" for "+Location.LocationName+" are as follows:";
            //    Row = Row + 1;
                
            //    ((Range)xls.Cells[Row, 1]).EntireColumn.ColumnWidth = 10.71;
            //    ((Range)xls.Cells[Row, 1]).Font.Size = 9;
            //    xls.Cells[Row, 1] = "Act Name";
            //    ((Range)xls.Cells[Row, 1]).EntireRow.Font.Bold = true;
            //    ((Range)xls.Cells[Row, 2]).EntireColumn.ColumnWidth = 7.14;
            //    ((Range)xls.Cells[Row, 2]).Font.Size = 9;
            //    xls.Cells[Row, 2] = "Actiivity Name";
            //    ((Range)xls.Cells[Row, 3]).EntireColumn.ColumnWidth = 7.14;
            //    ((Range)xls.Cells[Row, 3]).Font.Size = 9;
            //    xls.Cells[Row, 3] = "Execution Employee";
                
            //    ((Range)xls.Cells[Row, 4]).Font.Size = 9;
            //    ((Range)xls.Cells[Row, 4]).EntireColumn.ColumnWidth = 11.57;
            //    xls.Cells[Row, 4] = "Review Employee";
            //    ((Range)xls.Cells[Row, 5]).Font.Size = 9;
            //    ((Range)xls.Cells[Row, 5]).EntireColumn.ColumnWidth = 13.71;
            //    xls.Cells[Row, 5] = "Due Month";
            //    ((Range)xls.Cells[Row, 6]).Font.Size = 9;
            //    ((Range)xls.Cells[Row, 6]).EntireColumn.ColumnWidth = 8.66;
            //    xls.Cells[Row, 6] = "Due Date";
            //    ((Range)xls.Cells[Row, 7]).Font.Size = 9;
            //    ((Range)xls.Cells[Row, 7]).EntireColumn.ColumnWidth = 20.17;
            //    xls.Cells[Row, 7] = "Due Day";
            //    ((Range)xls.Cells[Row, 8]).Font.Size = 9;
            //    ((Range)xls.Cells[Row, 8]).EntireColumn.ColumnWidth = 11.86;
            //    xls.Cells[Row, 8] = "Frequency Name";
            //    var MailDetailList = (from mail in MailDetailMsgList
            //                          where mail.LocationName.ToLower() == Location.LocationName.ToLower()
            //                          select mail).ToList();
            //    foreach (var mailList in MailDetailList)
            //    {
            //        ((Range)xls.Cells[Row, 1]).EntireRow.Font.Color = System.Drawing.Color.Red;
            //        if (mailList.DueMonth == "0")
            //        {
            //            mailList.DueMonth = "NA";
            //        }
            //        if (mailList.DueDate == "0")
            //        {
            //            mailList.DueDate = "NA";
            //        }
            //        if (mailList.DueDay == "0")
            //        {
            //            mailList.DueDay = "NA";
            //        }
            //        xls.Cells[Row, 1] = mailList.ActName;
            //        xls.Cells[Row, 2] = mailList.ActivityName;
            //        xls.Cells[Row, 3] = mailList.ExecutionEmployeeName;
            //        xls.Cells[Row, 4] = mailList.ReviewEmployeeName;
            //        xls.Cells[Row, 5] = mailList.DueMonth ;
            //        xls.Cells[Row, 6] = mailList.DueDate;
            //        xls.Cells[Row, 7] = mailList.DueDay;
            //        xls.Cells[Row, 8] = mailList.FrequencyName;
            //        Row = Row + 1;
            //    }
            //    Row = Row + 1;
            //}
            //xlb.SaveAs(@"D:\Temp.xls" + "", m, m, m, m, m, XlSaveAsAccessMode.xlShared, m, m, m, m, m);
            //if (File.Exists(@"D:\Temp.xls"))
            //{
            //    //Attachment attach = new Attachment(@"D:\Temp.xls");
            //    //int Result = SendMail(FromMail, ConfigurationManager.AppSettings["MailTo"].ToString(), CCMail, sb.ToString(), Subject + " - Remainder Mail", IP, _Port);
            //    //if (Result == 8)
            //    //{
            //    //    ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
            //    //}
            //    //File.Delete(@"D:\Temp.xls");
            //}
        }
        #endregion

        public void ActVersionChangeMail()
        {
            try
            {
                Int64 CompanyLinkedToWebServerId = 0;
                int WSendMail = 0;
                String MailTo = "";
                StringBuilder sb = new StringBuilder();
                AddTestFileData("Sending Act Version Change Mail ");
                List<AutoVersionMailMsg> MailDetailMsgList = new List<AutoVersionMailMsg>();
                MailDetailMsgList = Bus.GetActVersionChangeMail();
                if (MailDetailMsgList.Count > 0)
                {
                    MailAddressCollection CCMail = new MailAddressCollection();
                    var LocationName = (from mail in MailDetailMsgList select new { mail.CompanyLinkedToWebServerId }).Distinct().ToList();
                    string Subject = string.Empty;
                    foreach (var Location in LocationName)
                    {
                        var MailDetailList = (from mail in MailDetailMsgList where 1 == 1 select mail).ToList();
                        foreach (var mailList in MailDetailList)
                        {
                            if (mailList.Kount > 0)
                            {
                                WSendMail = 1;
                            }
                            else
                            { //check if mailfrequency is met for sending Mails on synchronized ACTs
                                DateTime WDate = Convert.ToDateTime(mailList.LastMailDate.ToString());
                                DateTime WToday = System.DateTime.UtcNow.AddMinutes(330);
                                int Wdatediff = Convert.ToInt32((WToday.Date - WDate.Date).TotalDays); //Convert.ToInt32(WToday - WDate);
                                if (Wdatediff == mailList.MailFrequencyinDays)
                                {
                                    WSendMail = 1;
                                }
                                else
                                {
                                    WSendMail = 0;
                                }
                            }
                            if (WSendMail == 1)
                            {
                                CompanyLinkedToWebServerId = mailList.CompanyLinkedToWebServerId;
                                //string[] tos = mailList.MailCC.Split(';');
                                //foreach (string to in tos)
                                //{
                                CCMail.Add(mailList.MailCC);
                                //}

                                MailTo = mailList.MailTo;
                                sb.Append(mailList.TxtMsg + "\r\n");
                            }
                        }
                        if (sb.ToString() != string.Empty)
                        {
                            sb.Append("<BR><BR><H7>This is an auto-generated mail, kindly do not reply on this mail.</H7>");
                            int Result = SendMail(FromMail, MailTo, CCMail, sb.ToString(), Subject + Config.GetAppsetting("Subject").ToString(), IP, _Port);
                            if (Result == 8)
                            {
                                ErrorException("Mail Sent Successfully to ", ConfigurationManager.AppSettings["MailTo"].ToString());
                            }
                            string InsertResult = Bus.AutoMailDataInsert(CompanyLinkedToWebServerId);
                            AddTestFileData("Mail "+InsertResult + " For " + CompanyLinkedToWebServerId.ToString()+".");
                        }
                        break;
                    }
                }
                else //No Mail data list avbl
                {
                    AddTestFileData("No Mail list sent from web server ");
                }
            }
            catch (Exception ex)
            {
                ErrorException("Log data for SendRedAlertMailForActivity ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
            finally
            {
            }
        }

        #region AutoDownLoad ACT
        public void SynchronizeActData()
        {
            LoadWebACTMaster();
            if (FirstColumn != "0") // correct data has come
            {
                SaveWebACTDataLocally(ActList);
            }
        }
        private void LoadWebACTMaster()
        {
            string Result = CliInfo(); // Establish Handshake with Webservice 
            if (Result == "OK") // Authorised to access
            {
                try
                {
                    TranList = cli.SynchActMasterData(ServerKey); // Generate ACT Data
                    if (TranList.Count == 0) // No data for ACT
                    {
                        AddTestFileData("No data received from Web server  ");
                    }
                    else // data from web decrypt and store data in list 
                    {
                        ActList = DecryptACTData(TranList); //Decrypt and split the data
                        if (FirstColumn == "0")
                        {
                            AddTestFileData("On Decryption No data found  ");
                        }
                        else
                        {
                            AddTestFileData("Data from Web server Decrypted ");
                        }
                    }
                }
                catch
                {
                    AddTestFileData("Web Access when fetching Act Data Not OK.. Pls Check Internet Connection ");
                }

            }
            else
            {
                AddTestFileData("Web Access Not OK.. Access May Not be from Valid System ");
            }

        }
        private void SaveWebACTDataLocally(List<ActMasterMsg> PActList)
        {
            string LocalSaveResult = "";
            LocalSaveResult = Bus.WCFACTDataInsert(PActList); //Update local data base for merged status
            if (LocalSaveResult == "0")
            {
                ActDownLoadedWebUpdate(); //update web for having downloaded.
                AddTestFileData("Saving Web ACT Data to Local System Successful ");
            }
            else //Payment Advice Download FAILED on Local System 
            {
                AddTestFileData("Saving Web ACT Data to Local System FAILED ");
               
            }
        }
        private void ActDownLoadedWebUpdate()
        {
            try
            {
                string UpdateResult = cli.ActDataDownLoaded(ServerKey); // Generate ACT Data
                if (UpdateResult == "0") // No data for ACT
                {
                    AddTestFileData("Act DownLoaded-- Updated Web Successfully ");
                }
                else
                {
                    AddTestFileData(UpdateResult+" ");
                }
            }
            catch
            {
                AddTestFileData("Web Access Not OK..Pls Check Internet Connection ");
            }
        }

        #endregion

        #region WCF
        private string CliInfo()
        { //Only when Server Key is OK we can do processing
            string SKey = "NO";
            try
            {
                string ClientName = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ClGud"]);
                Cliinf.ClGud = Key.EncryptPwd(ClientName); // Client Key Information to be sent
                string Bhadram = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Bhadram"]);// (Config.GetAppsetting("Bhadram").ToString());
                string BhadramEncrypt = Secur.Encrypt(Bhadram);
                //string BhadramDecrypt = Secur.Decrypt(BhadramEncrypt);
                ServerKey = cli.CliDet(Cliinf, BhadramEncrypt); //Send your conn data
                if (ServerKey.Result == "1")
                {
                    AddTestFileData("Connected to Web Service for Client Check ");
                    if (ServerKey.ClientId.Trim().Length > 0 && ServerKey.GuId.Trim().Length > 0 && ServerKey.ClGud == Cliinf.ClGud)
                    {
                        SKey = "OK"; // transact with server 
                    }
                    else
                    {
                        SKey = "NO"; // do not do any transaction server key is not found
                    }
                }
                else
                {
                    SKey = "NO"; // do not do any transaction Client is not valid
                }
            }
            catch
            {
               // //ScriptManager.RegisterStartupScript(this, typeof(string), "Alert", "alert('" + "Web Access Not OK.. Pls Check Internet Connection " + "');", true);
                SKey = "NO";
                AddTestFileData("Web Access Not OK.. Pls Check Internet Connection ");
            }
            return SKey;
        }
        private List<ActMasterMsg> DecryptACTData(List<WebActService.TransactMsg> tCp)
        {
            List<ActMasterMsg> PActList = new List<ActMasterMsg>();
            foreach (WebActService.TransactMsg Tmsg in tCp)
            {
                string CoupMsg = "";
                CoupMsg = DecryptData(Tmsg.TransactString);
                string[] split = CoupMsg.Split(new char[] { '^' });
                ActMasterMsg CMsg = new ActMasterMsg(); // Do not change the order below
                FirstColumn = (split[0] == null ? string.Empty : split[0].ToString());
                if (FirstColumn != "")
                {
                    CMsg.ActId = Convert.ToInt32(FirstColumn);
                    CMsg.ActName = (split[1] == null ? string.Empty : split[1].ToString());
                    CMsg.ClassificationAct = (split[2] == null ? string.Empty : split[2].ToString());
                    CMsg.ActDtlId = Convert.ToInt32(split[3] == null ? string.Empty : split[3].ToString());
                    CMsg.Chapter = split[4] == null ? string.Empty : split[4].ToString();
                    CMsg.Head = (split[5] == null ? string.Empty : split[5].ToString());
                    CMsg.Section = (split[6] == null ? string.Empty : split[6].ToString());

                    CMsg.ActRule = (split[7] == null ? string.Empty : split[7].ToString());
                    CMsg.Description = (split[8] == null ? string.Empty : split[8].ToString());
                    CMsg.Frequency = (split[9] == null ? string.Empty : split[9].ToString());
                    CMsg.Implication = (split[10] == null ? string.Empty : split[10].ToString());
                    CMsg.ImplicationSection = (split[11] == null ? string.Empty : split[11].ToString());
                    CMsg.Liability = (split[12] == null ? string.Empty : split[12].ToString());
                    CMsg.AffectedPerson = (split[13] == null ? string.Empty : split[13].ToString());
                    CMsg.Importance = split[14] == null ? string.Empty : split[14].ToString();
                    CMsg.VersionNumber = (split[15] == null ? string.Empty : split[15].ToString());
                    CMsg.EffectiveDate = Convert.ToDateTime(split[16] == null ? string.Empty : split[16].ToString());
                    CMsg.ExpiryDate = Convert.ToDateTime(split[17] == null ? string.Empty : split[17].ToString());

                    CMsg.IsActive = Convert.ToBoolean(split[18] == null ? string.Empty : split[18].ToString());
                    CMsg.ValidityStatus = Convert.ToBoolean(split[19] == null ? string.Empty : split[19].ToString());
                    CMsg.IsNew = Convert.ToBoolean(split[20] == null ? string.Empty : split[20].ToString());
                    CMsg.IsUpdated = Convert.ToBoolean(split[21] == null ? string.Empty : split[21].ToString());
                    CMsg.MaxVersion = (split[22] == null ? string.Empty : split[22].ToString()); //
                    CMsg.AuditUpLoadId = Convert.ToInt32(split[23] == null ? string.Empty : split[23].ToString()); //
                    //txtWebVersion.Text = CMsg.MaxVersion;
                    PActList.Add(CMsg);

                }
                else /// empty data not allowed shows some error 
                {
                    FirstColumn = "0";
                    break;
                }
            }
            return PActList;
        }
        private string EncryptData(string clearText)
        {
            string EncryptionKey = "AdhanP@d1Nada";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        private string DecryptData(string cipherText)
        {
            string EncryptionKey = "AdhanP@d1Nada";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        #endregion 
    }
}
