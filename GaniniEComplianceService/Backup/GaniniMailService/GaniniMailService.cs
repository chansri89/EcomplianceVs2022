using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace GaniniMailService
{
    public partial class GaniniMailService : ServiceBase
    {
        public GaniniMailService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            ProgramStarter myStarter = new ProgramStarter();
            myStarter.StartMe(false);
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            ProgramStarter myStarter = new ProgramStarter();
            myStarter.StopMe(false);
        }
    }
}
