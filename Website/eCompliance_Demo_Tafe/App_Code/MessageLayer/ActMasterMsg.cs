using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActMasterMsg
/// </summary>
public class ActMasterMsg
{
	public ActMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ActId { get; set; }
    public int ActDtlId { get; set; }
    public string ActName { get; set; }
    public string ClassificationAct { get; set; }
    public string Chapter { get; set; }
    public string Head { get; set; }
    public string Section { get; set; }
    public string ActRule { get; set; }
    public string Description { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public string Flag { get; set; }
    public string ActResult { get; set; }
}