using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActivityDocumentTypeMasterMsg
/// </summary>
public class ActivityDocumentTypeMasterMsg
{
	public ActivityDocumentTypeMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public long ActDocTypeId { get; set; }
    public int ActivityForCompanyId { get; set; }
    public string ActivityName { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentTypeName { get; set; }
    public string ToBeMaintained { get; set; }
    public string ToBeSubmitted { get; set; }
    public string MasterPDFName { get; set; }
    public string AccessPath { get; set; }
    public bool IsActive { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string ActivityDocResult { get; set; }
    //added only for ActionDocument
}