using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Location
/// </summary>
public class LocationMsg
{
    public LocationMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int LocationId { get; set; }
    public string LocationName { get; set; }
    public string LocationShortName { get; set; }
    public string LocationResult { get; set; }
}