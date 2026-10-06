using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ActivityDocumentMsg
/// </summary>
public class ActivityDocumentMsg
{
	public ActivityDocumentMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int ActivityId { get; set; }
    public int ActivityActionId { get; set; }
    public int DocumentTypeId { get; set; }
    public string DocumentType { get; set; }
    public string DocumentName { get; set; }
    public string AccessPath { get; set; }
    public int LocationId { get; set; }
    public string Location { get; set; }
    public string State { get; set; }
    public int StateId { get; set; }
    public bool IsActive{ get; set; }
    public string CreatedBy { get; set; }
    public string ActivityDocResult { get; set; }
    public string Flag { get; set; }
    public int ActivityDocumentId { get; set; }
    

}