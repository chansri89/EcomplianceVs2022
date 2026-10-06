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
        internal void StartMe(bool console)
        {
            if (console) { Console.WriteLine("Starting GaniniMailingService..."); }
            MailingService = new Thread(new ThreadStart(DoWork));
            MailingService.Name = "MailingService";
            MailingService.Start();
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

        internal void StopMe(bool console)
        {
            if (console) Console.WriteLine("Stopping GaniniMailingService...");
            ThreadStatus = ThreadState.Stopped;
            if (console) Console.WriteLine("GaniniMailingService stopped.");
        }

        private void DoWork()
        {
            try
            {
                do
                {
                    MailingClass Mail = new MailingClass();
                    Mail.RemainCVS();
                    Mail.RemainFNHead();
                    Mail.RemainTopManagement();
                    Thread.Sleep(TimeSpan.FromSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["SleepingTime"])));
                }
                while (ThreadStatus == ThreadState.Started);
            }
            catch (Exception Ex)
            {
                string chk = Ex.StackTrace;
            }
            finally
            {
                 
            }
        }
    }
}
