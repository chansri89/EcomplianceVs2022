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
    //scs 010316 for implications
    public string ImplicationSection { get; set; }
    public string Implication { get; set; }
    public string Frequency { get; set; }
    public string Liability { get; set; }
    public string AffectedPerson { get; set; }
   
    public string Importance { get; set; }
    public string VersionNumber { get; set; }
    public bool ValidityStatus { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsNew { get; set; }
    public bool IsUpdated { get; set; }
    public int AuditUpLoadId { get; set; }
    public string MaxVersion { get; set; }
}
