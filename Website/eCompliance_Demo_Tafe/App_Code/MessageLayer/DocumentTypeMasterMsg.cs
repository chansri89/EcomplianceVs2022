using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocumentTypeMasterMsg
/// </summary>
public class DocumentTypeMasterMsg
{
	public DocumentTypeMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int CompanyActivityId { get; set; }
    public int DocumentTypeId{get;set;}
    public int ActivityDocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; }
    public string DocumentTypeShortName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string DocTypeResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public string Remarks { get; set; }
    public int ActDoctypeId { get; set; }
    public string ActivityName { get; set; }
    public char ToBeMaintained { get; set; }
    public char ToBeSubmitted { get; set; }
    public string MasterPDFName { get; set; }
    public string AccessPath { get; set; }
}