using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActivityMasterMsg
/// </summary>
public class ActivityMasterMsg
{
	public ActivityMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   public int ActivityId { get; set; }
    public int ActDtlId { get; set; }
    public string ActivityName { get; set; }
    public int ActId { get; set; }
    public string ActName { get; set; }
    public string Chapter { get; set; }
    public string Head { get; set; }
    public string Section { get; set; }
    public string ActRule { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int SeverityId { get; set; }
    public string SeverityName { get; set; }
    public bool IsActive { get; set; }
    public char NonComplianceTaskAvailableFlag { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string Flag { get; set; }
    public string DocumentFlag { get; set; }
    public string ActivitiesResult { get; set; }
    public bool IsStateSpecific { get; set; }
    public bool IsRegularActivity { get; set; }
    public bool IsLocationSpecific { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentType { get; set; }
    public int ActivityDocumentId { get; set; }
    public string DocumentName { get; set; }
    public string AccessPath { get; set; }
    public string Form { get; set; }
    public string ExistingDocumentPath { get; set; }
    public int LastDocumentId { get; set; }
    public int LastActivityId { get; set; }
    public string EmployeeCode { get; set; }
    public string Administrator { get; set; }//added by abinayaa 030913
    public int ActivityDocId { get; set; }  //added by abinayaa 030913
    public int NonComplianceCompanyId { get; set; }  //added by abinayaa 030913
}