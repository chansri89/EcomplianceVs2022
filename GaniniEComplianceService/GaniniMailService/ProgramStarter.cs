using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using GaniniExcellClassLibrary;
using System.Configuration;
using System.IO;
using System.Data;
using System.Windows.Forms;
using GeneratingMail;
using Ganini.Lib;

namespace GaniniMailService
{
    class ProgramStarter
    {
        private enum ThreadState
        {
            Started,
            Stopping,
            Stopped
        }
        
        private ThreadState ThreadStatus = ThreadState.Started;
        int Sleepingtime=Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SleepingTime"].ToString());
        Thread MailingService = null;
        //string TestFileName = Config.GetAppsetting("TestFile");
        string TestFileName = Config.GetAppsetting("TestFile") + " " + DateTime.UtcNow.AddMinutes(330).ToString("yyyyMMdd") + ".txt";
        MailingClass Mail = new MailingClass();
        internal void StartMe(bool console)
        {
            try
            {
                if (console) { Console.WriteLine("Starting GaniniMailingService..."); }
                MailingService = new Thread(new ThreadStart(DoWork));
                MailingService.Name = "MailingService";
                MailingService.Start();
                //Mail.AddTestFileData("Starting GaniniMailing Service ");
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Starting GaniniMailingService..." + System.DateTime.Now + "\r\n");
                //}
                if (console)
                {
                    while (true)
                    {
                        string input = Console.ReadLine();
                        if (null != input && input.ToLower() == "q")
                        {
                            break;
                        }
                        System.Threading.Thread.Sleep(Sleepingtime);

                    }
                    StopMe(true);
                }
            }
            catch (Exception ex)
            {
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
                Mail.AddTestFileData(ex.ToString() + "  ");
            }
        }
        internal void StopMe(bool console)
        {
            try
            {
                Mail.AddTestFileData("Stopping GaniniMailing Service ");
                //if (Config.GetAppsetting("TestingFlag") == "Y")
                //{
                //    File.AppendAllText(TestFileName, "Stopping GaniniMailingService..." + System.DateTime.Now + "\r\n");
                //}
                if (console) Console.WriteLine("Stopping GaniniMailingService...");
                ThreadStatus = ThreadState.Stopped;
                if (console) Console.WriteLine("GaniniMailingService stopped.");
            }
            catch (Exception ex)
            {
                Mail.AddTestFileData("StopMe exception  "+ex.ToString() + "  ");
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            }
        }

        private void DoWork()
        {
            //try
            //{
                do
                {
                    Mail.AddTestFileData("\r\n" + "Doing Work Started in GaniniMailing Service ");
                     int MailHour = Convert.ToInt32(ConfigurationManager.AppSettings["MailHour"].ToString());
                    //for web server system set this to Y else anything
                    if (Config.GetAppsetting("WebServer") == "Y") // Send Mails to All Customer when Version Change has been Applied.
                    {
                        Mail.ActVersionChangeMail();
                    }
                    if (Config.GetAppsetting("AutoActUpDate") == "Y") // IF Auto Update ofAct sought do sync operation.
                    {
                        Mail.SynchronizeActData();

                    }
                    int CurrentHr = System.DateTime.UtcNow.AddMinutes(330).Hour;
                    if (CurrentHr == MailHour ) // Mail Hour (odd or even hour) to trigger at particular time SCS 210717
                    {
                        SendActivityMail(); // Applicable at Customer Installation
                        // SCS 210714 for sending auto mail as per Ramkumar mail 210710 dateValue.ToString("ddd")
                        string SWeek = Config.GetAppsetting("WeekDay").Substring(0, 3);
                        if (DateTime.UtcNow.AddMinutes(330).ToString("ddd") == SWeek)
                        {
                            Mail.SendCoordinatorMail();
                        }
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["SleepingTime"]))); //scs 210717 included here
                    // ends
                    //else
                    //{
                        //SendActivityMail(); // Applicable at Customer Installation
                        //if (Config.GetAppsetting("AutoActUpDate") == "Y") // IF Auto Update ofAct sought do sync operation.
                        //{
                        //    Mail.SynchronizeActData();

                        //}
                    //}
                   
                }

                while (ThreadStatus == ThreadState.Started);
            //}
            //catch (Exception Ex)
            //{
            //    string chk = Ex.StackTrace;
            //    //ExceptionHandling eh = new ExceptionHandling();
            //    //eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Service);
            //    Mail.AddTestFileData("Do work Exception  "+chk + "  ");
            //    MailingService = new Thread(new ThreadStart(DoWork)); //scs 01122016
            //}
            //finally
            //{
            //    MailingService = new Thread(new ThreadStart(DoWork)); //scs 01122016
            //}
                MailingService = new Thread(new ThreadStart(DoWork)); //scs 01122016
        }
        private void SendActivityMail()
        {
            Mail.AddTestFileData("\r\n"+"Work Started ");
            
            int CreateActivityCount = 0;
            int SendMailCount = 0;
            //MailingClass Mail = new MailingClass();
            if (Config.GetAppsetting("IsDailyProcess").ToString() == "Y")
            {
                CreateActivityCount++;
            }
            else if (Config.GetAppsetting("IsDailyProcess").ToString() == "N" && System.DateTime.Now.Day.ToString() == Config.GetAppsetting("ProcessDate"))
            {
                CreateActivityCount++;
            }
            if (Config.GetAppsetting("IsDaily").ToString() == "Y")
            {
                SendMailCount++;
            }
            else if (Config.GetAppsetting("IsWeekly").ToString() == "Y")
            {
                if (Config.GetAppsetting("WeekDay").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower() && Config.GetAppsetting("WeekFrequency").ToString() == "1")
                {
                    SendMailCount++;
                }
                if ((Config.GetAppsetting("WeekDay").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower() || Config.GetAppsetting("WeekDay1").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower()) && Config.GetAppsetting("WeekFrequency").ToString() == "2")
                {
                    SendMailCount++;
                }
                if ((Config.GetAppsetting("WeekDay").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower() || Config.GetAppsetting("WeekDay2").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower() || Config.GetAppsetting("WeekDay3").ToString().ToLower() == System.DateTime.Now.DayOfWeek.ToString().ToLower()) && Config.GetAppsetting("WeekFrequency").ToString() == "3")
                {
                    SendMailCount++;
                }
            }
            if (CreateActivityCount > 0)
            {
                Mail.AddTestFileData("Creating ActivityAction ");
                
                Mail.CreateNewActivityAction();
                Mail.CreateNewActivityActionWeekly();
            }
            if (SendMailCount > 0)
            {
                Mail.AddTestFileData("Creating Mail to Send ");
              
                Mail.SendAllActivityMail();
                Mail.AddTestFileData("Start SendRedAlertMail ");
              
                if (Config.GetAppsetting("IsExcel") == "Y")
                {
                    Mail.SendRedAlertMailForActivitywithExcel();
                }
                else
                {
                    Mail.SendRedAlertMailForActivity();
                }
                Mail.AddTestFileData("End SendRedAlertMail ---------------------------------------------------------------");

            }
           // Thread.Sleep(TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["SleepingTime"])));
            // commented above sleep and included in do loop //scs 210717 included here
        }
    }
}
