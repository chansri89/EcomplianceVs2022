using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocumentTypeAction
/// </summary>
public class DocumentTypeActionMsg
{
	public DocumentTypeActionMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     public long ActivityActionId { get; set; }
     public long ActivityActionDocId { get; set; }
    public int DocumentTypeId { get; set; }
    public int ActivityDocId { get; set; }
    public string DocumentTypeName { get; set; }
    public string DocumentType { get; set; }
    public string DocumentName{ get;set;}
    public string StoragePath{ get;set;}
    public string Notes { get; set; }
    public string DocuActionResult{ get;set;}
     public string Flag{ get;set;}
     public string CreatedBy { get; set; }
     public string Remarks { get; set; }
    
    
}
