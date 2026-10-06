using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Ganini.Lib;
using System.IO;
using System.Data.SqlClient;
using GeneratingMail.Messages;
using System.Linq;
using System.Web;

namespace GeneratingMail
{
    public class ProcessBus
    {
        #region Connection

        //To Create an Object for Connection Class  
        private ConnectionClass mConnection = null;
        private ConnectionClass Connection
        {
            get
            {
                if (null == mConnection)
                {
                    mConnection = new ConnectionClass();
                }
                return mConnection;
            }
        }

        #endregion
       
        //string TestFileName = Config.GetAppsetting("TestFile");
        string TestFileName = Config.GetAppsetting("TestFile") + " " + DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd") + ".txt";
        string TestingFlag = Config.GetAppsetting("TestingFlag");
        private void ErrorException(string Message, string ex)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string dat = System.DateTime.Now.ToString("ddMMMyyyy-");
                string path = System.Configuration.ConfigurationManager.AppSettings["LogFile"].ToString();
                string FileName = path + dat + ".txt";
                if (File.Exists(FileName) == false)
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat  + "\r\n");
                    sb.Append(ex + "\r\n");
                    string AppDate = sb.ToString();
                    File.AppendAllText(FileName, AppDate);
                }
                else
                {
                    sb = new StringBuilder();
                    sb.Append(Message + " " + dat  + "\r\n");
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
        }
        private void AddTestFileData(string TextMsg)
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
        # region Operations

        public List<MailDetailMsg> GetActivityMailDetails()
        {
            List<MailDetailMsg> MailDetailMsgList=new List<MailDetailMsg>();
            try
            {
                
                //    AddTestFileData("Calling for MailDetailSelectSp ");
               
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "MailDetailSelectSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                MailDetailMsg mailDetailMsg = new MailDetailMsg();
                                mailDetailMsg.ActName = sdr["ActName"].ToString();
                                mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                                mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                                mailDetailMsg.ToEmail = sdr["ToEmail"].ToString();
                                mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                                mailDetailMsg.CCMail = sdr["CCMail"].ToString();
                                mailDetailMsg.Status = sdr["Status"].ToString();
                                mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                                mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                                mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Error data for MailDetailSelectSp "+ ex.Message.ToString());
                //ErrorException("Error data for MailDetailSelectSp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Log data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return MailDetailMsgList;
        }
        public List<CoordinatorMailSummary> MailCoordinatorActionCompanySummary()
        {
            List<CoordinatorMailSummary> CoordSummary = new List<CoordinatorMailSummary>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{

                    //    AddTestFileData("Calling MailDetailsforAllActivitysp" + "\r\n");

                    Connection.cmd.CommandText = "MailCoordinatorActionCompanySummary";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    //Connection.cmd.Parameters.AddWithValue("@QuarterlyMonth", QuarterlyMonth);
                    //Connection.cmd.Parameters.AddWithValue("@QuarterlyTrigger", QuarterlyTrigger);
                    //Connection.cmd.Parameters.AddWithValue("@HalfyearlyMonth", HalfyearlyMonth);
                    //Connection.cmd.Parameters.AddWithValue("@HalfyearlyTrigger", HalfyearlyTrigger);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CoordinatorMailSummary Coordmsg = new CoordinatorMailSummary();
                            Coordmsg.CompanyShortName = sdr["CompanyShortName"].ToString();
                            Coordmsg.ActionsCreated = sdr["ActionsCreated"].ToString();
                            Coordmsg.ActionsCompleted = sdr["ActionsCompleted"].ToString();
                            Coordmsg.ActionsPending = sdr["ActionsPending"].ToString();
                            Coordmsg.OpBalPending = sdr["OpBalPending"].ToString();
                            Coordmsg.FinancialQuarter = sdr["FinancialQuarter"].ToString();
                            Coordmsg.ToEmail = sdr["ToEmail"].ToString(); //SCS 210916 email for all
                            Coordmsg.CoordinatorEmployeeName = sdr["CoordinatorEmployeeName"].ToString(); //SCS 210916 email for all
                            CoordSummary.Add(Coordmsg);
                        }
                    }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ErrorException("Log data for MailDetailsforAllActivitysp ", Ex.Message.ToString());
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //    Connection.con.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Error data for MailDetailSelectSp " + ex.Message.ToString());
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            AddTestFileData("Coordinator Summary count " + CoordSummary.Count.ToString()+"  "); // scs 210921
            return CoordSummary;
        }
        public List<CoordinatorMailDetail> MailCoordinatorActionCompanywiseDetail()
        {
            List<CoordinatorMailDetail> CoordDtl = new List<CoordinatorMailDetail>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{

                    //    AddTestFileData("Calling MailDetailsforAllActivitysp" + "\r\n");

                    Connection.cmd.CommandText = "MailCoordinatorActionCompanywiseDetail";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    //Connection.cmd.Parameters.AddWithValue("@QuarterlyMonth", QuarterlyMonth);
                    //Connection.cmd.Parameters.AddWithValue("@QuarterlyTrigger", QuarterlyTrigger);
                    //Connection.cmd.Parameters.AddWithValue("@HalfyearlyMonth", HalfyearlyMonth);
                    //Connection.cmd.Parameters.AddWithValue("@HalfyearlyTrigger", HalfyearlyTrigger);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CoordinatorMailDetail Coordmsg = new CoordinatorMailDetail();
                            Coordmsg.CompanyShortName = sdr["CompanyShortName"].ToString();
                            Coordmsg.ToEmail = sdr["ToEmail"].ToString();
                            Coordmsg.CoordinatorEmployeeName = sdr["CoordinatorEmployeeName"].ToString();
                            Coordmsg.ExecutionEmployeeName = sdr["ExecutionEmployeeName"].ToString();
                            Coordmsg.ReviewEmployeeName = sdr["ReviewEmployeeName"].ToString();
                            Coordmsg.ActName = sdr["ActName"].ToString();

                            Coordmsg.ActivityName = sdr["ActivityName"].ToString();
                            Coordmsg.FrequencyName = sdr["FrequencyName"].ToString();
                            Coordmsg.SeverityName = sdr["SeverityName"].ToString();
                            Coordmsg.DueOn = sdr["DueOn"].ToString();
                            Coordmsg.TriggerDate = sdr["TriggerDate"].ToString();
                            Coordmsg.TriggerLeadTime = sdr["TriggerLeadTime"].ToString();

                            CoordDtl.Add(Coordmsg);
                        }
                    }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ErrorException("Log data for MailDetailsforAllActivitysp ", Ex.Message.ToString());
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //    Connection.con.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Error data for MailCoordinatorActionCompanywiseDetail " + ex.Message.ToString());
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            AddTestFileData("Coordinator Summary count " + CoordDtl.Count.ToString() + "  "); // scs 210921
            return CoordDtl;
        }


        public List<MailDetailMsg> GetActivityMailDetails(int QuarterlyMonth,bool QuarterlyTrigger,int HalfyearlyMonth,bool HalfyearlyTrigger)
        {
            List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        
                        //    AddTestFileData("Calling MailDetailsforAllActivitysp" + "\r\n");
                      
                        Connection.cmd.CommandText = "MailDetailsforAllActivitysp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@QuarterlyMonth",QuarterlyMonth);
                        Connection.cmd.Parameters.AddWithValue("@QuarterlyTrigger",QuarterlyTrigger);
                        Connection.cmd.Parameters.AddWithValue("@HalfyearlyMonth",HalfyearlyMonth);
                        Connection.cmd.Parameters.AddWithValue("@HalfyearlyTrigger", HalfyearlyTrigger);
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                MailDetailMsg mailDetailMsg = new MailDetailMsg();
                                mailDetailMsg.ActName = sdr["ActName"].ToString();
                                mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                                mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                                mailDetailMsg.ToEmail = sdr["ToEmail"].ToString();
                                mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                                mailDetailMsg.CCMail = sdr["CCMail"].ToString();
                                mailDetailMsg.Status = sdr["Status"].ToString();
                                mailDetailMsg.DueYear = sdr["DueYear"].ToString(); //scs190216
                                mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                                mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                                mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                                mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString();
                                mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString();
                                mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString();
                                mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString();
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ErrorException("Log data for MailDetailsforAllActivitysp ", Ex.Message.ToString());
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //    Connection.con.Close();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Error data for MailDetailSelectSp " + ex.Message.ToString());
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return MailDetailMsgList;
        }

        public List<MailDetailMsg> GetActivityMailForRedAlert()
        {
            List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            try
            {
               
                //    AddTestFileData("Calling for MailDetailsforRedAlertActivitysp ");
               
                using (Connection.con)
                {
                    //try
                    //{
                    Connection.cmd.CommandText = "MailDetailsforRedAlertActivitysp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            MailDetailMsg mailDetailMsg = new MailDetailMsg();
                            mailDetailMsg.ActName = sdr["ActName"].ToString();
                            mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                            mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                            mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                            mailDetailMsg.Status = sdr["Status"].ToString();
                            mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                            mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                            mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                            mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString();
                            mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString();
                            mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString();
                            mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString();
                            mailDetailMsg.LocationName = sdr["CompanyName"].ToString();
                            mailDetailMsg.SeverityName = sdr["SeverityName"].ToString();
                            mailDetailMsg.CompanyHRMailId = sdr["CompanyHRMailId"].ToString(); //scs 150315 PO100315
                            mailDetailMsg.CompanyCode = sdr["CompanyCode"].ToString();//scs 150315 PO100315
                            mailDetailMsg.EmpType = sdr["EmpType"].ToString(); //scs 150315 PO100315
                            mailDetailMsg.ActivityCategorization = sdr["ActivityCategorization"].ToString(); //scs 150315 PO100315
                            MailDetailMsgList.Add(mailDetailMsg);
                        }
                    }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for MailDetailsforRedAlertActivitysp "+ ex.Message.ToString());
                //ErrorException("Log data for MailDetailsforRedAlertActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Log data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return MailDetailMsgList;
        }
        public List<MailDetailMsg> GetGroupActivityMailForRedAlert()
        {
            List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            try
            {
                
                //    AddTestFileData("Calling for MailDetailsforRedAlertGroupLevelActivitysp ");
               
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "MailDetailsforRedAlertGroupLevelActivitysp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                MailDetailMsg mailDetailMsg = new MailDetailMsg();
                                mailDetailMsg.ActName = sdr["ActName"].ToString();
                                mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                                mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                                mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                                mailDetailMsg.Status = sdr["Status"].ToString();
                                mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                                mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                                mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                                mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString();
                                mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString();
                                mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString();
                                mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString();
                                mailDetailMsg.LocationName = sdr["CompanyName"].ToString();
                                mailDetailMsg.SeverityName = sdr["SeverityName"].ToString();
                                mailDetailMsg.CompanyHRMailId = sdr["CompanyHRMailId"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.CompanyCode = sdr["CompanyCode"].ToString();//scs 150315 PO100315
                                mailDetailMsg.EmpType = sdr["EmpType"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.ActivityCategorization = sdr["ActivityCategorization"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.DueYear = sdr["DueYear"].ToString();
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for MailDetailsforRedAlertGroupLevelActivitysp " + ex.Message.ToString());
               // ErrorException("Log data for MailDetailsforRedAlertGroupLevelActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Log data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return MailDetailMsgList;
        }
        public List<MailDetailMsg> GetCompAdminActivityMailForRedAlert()
        {
            List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "MailDetailsforRedAlertCompanyAdminLevelActivitysp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                MailDetailMsg mailDetailMsg = new MailDetailMsg();
                                mailDetailMsg.ActName = sdr["ActName"].ToString();
                                mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                                mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                                mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                                mailDetailMsg.Status = sdr["Status"].ToString();
                                mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                                mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                                mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                                mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString();
                                mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString();
                                mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString();
                                mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString();
                                mailDetailMsg.LocationName = sdr["CompanyName"].ToString();
                                mailDetailMsg.SeverityName = sdr["SeverityName"].ToString();
                                mailDetailMsg.CompanyHRMailId = sdr["CompanyHRMailId"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.ParentCompanyCode = sdr["ParentCompanyCode"].ToString();//scs 150315 PO100315
                                mailDetailMsg.CompanyCode = sdr["CompanyCode"].ToString();//scs 150315 PO100315
                                mailDetailMsg.EmpType = sdr["EmpType"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.ActivityCategorization = sdr["ActivityCategorization"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.DueYear = sdr["DueYear"].ToString();
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for MailDetailsforRedAlertCompanyAdminLevelActivitysp " + ex.Message.ToString());
                //ErrorException("Log data for MailDetailsforRedAlertCompanyAdminLevelActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Log data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }

            return MailDetailMsgList;
        }

        public List<MailDetailMsg> GetPlantActivityMailForRedAlert()
        {
            List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "MailDetailsforRedAlertPlantLevelActivitysp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                MailDetailMsg mailDetailMsg = new MailDetailMsg();
                                mailDetailMsg.ActName = sdr["ActName"].ToString();
                                mailDetailMsg.ActivityName = sdr["ActivityName"].ToString();
                                mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString();
                                mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString();
                                mailDetailMsg.Status = sdr["Status"].ToString();
                                mailDetailMsg.DueYear = sdr["DueYear"].ToString(); //scs 180216 added as due year was not shown
                                mailDetailMsg.DueDate = sdr["DueDate"].ToString();
                                mailDetailMsg.DueMonth = sdr["DueMonth"].ToString();
                                mailDetailMsg.DueDay = sdr["DueDay"].ToString();
                                mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString();
                                mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString();
                                mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString();
                                mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString();
                                mailDetailMsg.LocationName = sdr["CompanyName"].ToString();
                                mailDetailMsg.SeverityName = sdr["SeverityName"].ToString();
                                mailDetailMsg.CompanyHRMailId = sdr["CompanyHRMailId"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.CompanyCode = sdr["CompanyCode"].ToString();//scs 150315 PO100315
                                mailDetailMsg.EmpType = sdr["EmpType"].ToString(); //scs 150315 PO100315
                                mailDetailMsg.ActivityCategorization = sdr["ActivityCategorization"].ToString(); //scs 150315 PO100315
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for MailDetailsforRedAlertPlantLevelActivitysp " + ex.Message.ToString());
                //ErrorException("Log data for MailDetailsforRedAlertPlantLevelActivitysp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                //ErrorException("Log data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return MailDetailMsgList;
        }

        public void NewActivityActionInsert()
        {
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "ActivityActionAutoCreatesp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.ExecuteNonQuery();
                    }
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                    //    return;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                    //}
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for ActivityActionAutoCreatesp " + ex.Message.ToString());
                //ErrorException("Error data for ActivityActionAutoCreatesp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return;
            }
            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }
        public void CreateNewActivityActionWeekly()
        {
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "ActivityActionAutoCreateWeekly";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.ExecuteNonQuery();
                    }
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                //}
            }
            catch (Exception ex)
            {
                AddTestFileData("Log data for ActivityActionAutoCreateWeekly " + ex.Message.ToString());
                //ErrorException("Error data for ActivityActionAutoCreateWeekly ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return;
            }
            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
        }

        public List<AutoVersionMailMsg> GetActVersionChangeMail()
        {
            List<AutoVersionMailMsg> MailDetailMsgList = new List<AutoVersionMailMsg>();
            try
            {
                using (Connection.con)
                {
                    //try
                    //{
                        Connection.cmd.CommandText = "AutoVersionChangeMailSelectSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Connection = Connection.con;
                        Connection.cmd.Parameters.Clear();
                        using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                AutoVersionMailMsg mailDetailMsg = new AutoVersionMailMsg();
                                mailDetailMsg.MailTo = sdr["MailTo"].ToString();
                                mailDetailMsg.MailCC = sdr["MailCC"].ToString();
                                mailDetailMsg.LastMailDate = sdr["LastMailDate"].ToString();
                                mailDetailMsg.PreviousDownLoadDate = sdr["PrevDownLoadDate"].ToString();
                                mailDetailMsg.Kount = Convert.ToInt32(sdr["Kount"].ToString());
                                mailDetailMsg.MailFrequencyinDays = Convert.ToInt32(sdr["MailFrequencyinDays"].ToString());
                                mailDetailMsg.TxtMsg = sdr["TxtMsg"].ToString();
                                mailDetailMsg.CompanyLinkedToWebServerId = Convert.ToInt64(sdr["CompanyLinkedToWebServerId"].ToString());
                                MailDetailMsgList.Add(mailDetailMsg);
                            }
                        }
                    //}
                    //catch (Exception Ex)
                    //{
                    //    ExceptionHandling eh = new ExceptionHandling();
                    //    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    //    return null;
                    //}
                    //finally
                    //{
                    //    Connection.cmd.Parameters.Clear();
                    //}
                }
            }
            catch (Exception ex)
            {
                AddTestFileData("Error data for AutoVersionChangeMailSelectSp " + ex.Message.ToString());
                //ErrorException("Error data for AutoVersionChangeMailSelectSp ", ex.Message.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return MailDetailMsgList;
        }
        public string AutoMailDataInsert(Int64 CmpLinkedToWebServerId)
        {
            string InsertResult = "";
            try
            {
                using (Connection.con)
                {
                    //Mail.AddTestFileData("Calling AutoMailDataInsertSp ");
                   
                    Connection.cmd.CommandText = "AutoMailDataInsertSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CmpLinkedToWebServerId",CmpLinkedToWebServerId);
                    Connection.cmd.Parameters.Add("@InsertResult", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    InsertResult = Connection.cmd.Parameters["@InsertResult"].Value.ToString().Trim();
                 }
                   
            }
            catch (Exception Ex)
            {
                AddTestFileData("Error data for AutoMailDataInsertSp " + Ex.Message.ToString());
               // ErrorException("Error data for AutoMailDataInsertSp ", Ex.Message.ToString());
               ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                InsertResult = Ex.ToString();
            }

            finally
            {
                //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            return InsertResult;
        }
        public string WCFACTDataInsert(List<ActMasterMsg> ActList)
        {
            SqlTransaction transaction = null;
            string Result = "";
            //int Recknt = 0;
            //Int64 WParameterId = 0;
            //int FirstRec = 1;
            //Recknt = ActList.Count;
            try
            {
                using (Connection.con)
                {
                    transaction = Connection.con.BeginTransaction();
                    Connection.cmd.Transaction = transaction;
                    Connection.cmd.Connection = Connection.con;
                    foreach (ActMasterMsg Acts in ActList)
                    {
                        Connection.cmd.CommandText = "WCFACTDataInsertSp";
                        Connection.cmd.CommandType = CommandType.StoredProcedure;
                        Connection.cmd.Parameters.Clear();
                        // Connection.cmd.Parameters.AddWithValue("@Flag", Acts.Flag);
                        Connection.cmd.Parameters.AddWithValue("@WAId", Acts.ActId);
                        Connection.cmd.Parameters.AddWithValue("@ActDtlId", Acts.ActDtlId);
                        Connection.cmd.Parameters.AddWithValue("@ActName", Acts.ActName);
                        Connection.cmd.Parameters.AddWithValue("@ClassificationAct", Acts.ClassificationAct);
                        Connection.cmd.Parameters.AddWithValue("@Chapter", Acts.Chapter);
                        Connection.cmd.Parameters.AddWithValue("@Head", Acts.Head);
                        Connection.cmd.Parameters.AddWithValue("@Section", Acts.Section);
                        Connection.cmd.Parameters.AddWithValue("@ActRule", Acts.ActRule);
                        Connection.cmd.Parameters.AddWithValue("@Description", Acts.Description);
                        Connection.cmd.Parameters.AddWithValue("@IsActive", Acts.IsActive);
                        //Connection.cmd.Parameters.AddWithValue("@CreatedBy", Acts.CreatedBy);
                        // scs 030116
                        Connection.cmd.Parameters.AddWithValue("@Frequency", Acts.Frequency);
                        Connection.cmd.Parameters.AddWithValue("@Implication", Acts.Implication);
                        Connection.cmd.Parameters.AddWithValue("@ImplicationSection", Acts.ImplicationSection);
                        Connection.cmd.Parameters.AddWithValue("@Liability", Acts.Liability);
                        Connection.cmd.Parameters.AddWithValue("@AffectedPerson", Acts.AffectedPerson);
                        Connection.cmd.Parameters.AddWithValue("@Importance", Acts.Importance);
                        Connection.cmd.Parameters.AddWithValue("@VersionNumber", Acts.VersionNumber);
                        Connection.cmd.Parameters.AddWithValue("@ValidityStatus", Acts.ValidityStatus);
                        Connection.cmd.Parameters.AddWithValue("@IsNew", Acts.IsNew);
                        Connection.cmd.Parameters.AddWithValue("@IsUpdated", Acts.IsUpdated);
                        Connection.cmd.Parameters.AddWithValue("@EffectiveDate", Acts.EffectiveDate);
                        Connection.cmd.Parameters.AddWithValue("@ExpiryDate", Acts.ExpiryDate);
                        Connection.cmd.Parameters.AddWithValue("@AuditUpLoadId", Acts.AuditUpLoadId);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                        if (Result != "0")
                        {
                            break;
                        }
                    } //For each loop
                    if (Result == "0")
                    {
                        transaction.Commit();
                        Connection.cmd.Parameters.Clear();
                        Connection.con.Close();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                  } //using connection ends
                }
                catch (Exception ex)
                {
                    Result = ex.ToString();
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    AddTestFileData("Error data for WCFACTDataInsertSp " + ex.Message.ToString());
                    //ErrorException("Error data for WCFACTDataInsertSp ", ex.Message.ToString());
                    ExceptionHandling eh = new ExceptionHandling();
                    eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

                }
                finally
                {
                    //ErrorException("Error data for MailDetailsforAllActivitysp ", ex.Message.ToString());
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }

                return Result;
            }
        #endregion
    }
}
