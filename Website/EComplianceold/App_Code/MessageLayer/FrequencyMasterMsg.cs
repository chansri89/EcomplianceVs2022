using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryMasterMsg
/// </summary>
public class FrequencyMasterMsg
{
    public FrequencyMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int FrequencyId { get; set; }
    public string FrequencyName { get; set; }
    public string FrequencyShortName{get;set;}
    public int FrequencyDays { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string FrequencyResult { get; set; }

}