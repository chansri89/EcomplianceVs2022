using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace GaniniMailService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool runAsService = true;
            try
            {
                if (args[0].ToLower() == "c")
                {
                    runAsService = false;
                }
            }
            catch
            {
                // argument is null so it defaults to service
            }
            if (runAsService)
            {
                ServiceBase[] ServicesToRun;
                // More than one user Service may run within the same process. To add
                // another service to this process, change the following line to
                // create a second service object. For example,
                //
                //   ServicesToRun = new ServiceBase[] {new SvcSlingshot(), new MySecondUserService()};
                //
                ServicesToRun = new ServiceBase[] { new GaniniMailService() };

                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                ProgramStarter myStarter = new ProgramStarter();
                myStarter.StartMe(true);
            }
        }
    }
}