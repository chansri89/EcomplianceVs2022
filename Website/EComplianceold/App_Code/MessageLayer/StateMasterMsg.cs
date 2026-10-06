using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StateMasterMsg
/// </summary>
public class StateMasterMsg
{
	public StateMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   
    public int StateId { get; set; }
    public string StateName { get; set; }
    public string StateShortName { get; set; }
    public string Flag { get; set; }
    public string StateResult { get; set; }
}