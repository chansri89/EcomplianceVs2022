using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MailDetailMsg
{
    public MailDetailMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string ActName { get; set; }
    public string ActivityName { get; set; }
    public string ExecutionEmployeeName { get; set; }
    public string ToEmail { get; set; }
    public string ReviewEmployeeName { get; set; }
    public string CCMail { get; set; }
    public string Status { get; set; }
    public string DueDate { get; set; }
    public string DueMonth { get; set; }
    public string DueDay { get; set; }
    public string BodyMessage { get; set; }
    public string FrequencyName { get; set; }
    public string TriggerDate { get; set; }
    public string TriggerMonth { get; set; }
    public string TriggerDay { get; set; }
}
