using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryMasterMsg
/// </summary>
public class LocationinStateMasterMsg
{
    public LocationinStateMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int LocationinStateId { get; set; }
    public string LocationinState { get; set; }    
    public string CreatedBy { get; set; }   
    public string LocationinStateResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }

}