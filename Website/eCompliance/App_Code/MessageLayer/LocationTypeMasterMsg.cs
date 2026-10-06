using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryMasterMsg
/// </summary>
public class LocationTypeMasterMsg
{
    public LocationTypeMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int LocationTypeId { get; set; }
    public string LocationTypeName { get; set; }
    public string LocationTypeShortName { get; set; }
    public string CreatedBy { get; set; }   
    public string ModifiedBy { get; set; }
    public string LocationTypeResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }

}