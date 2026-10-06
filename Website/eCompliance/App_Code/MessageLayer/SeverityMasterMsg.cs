using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SeverityMasterMsg
/// </summary>
public class SeverityMasterMsg
{
	public SeverityMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int SeverityId { get; set; }
    public string SeverityName { get; set; }
    public string SeverityShortName { get; set; }
    public string Remarks { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string SeverityResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }
}