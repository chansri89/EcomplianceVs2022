using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for CompanyMessage
/// </summary>
public class CompanyMessage
{
	public CompanyMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string CompanyCode { get; set; }
    public string CompanyName { get; set; }
    public string CompanyShortName { get; set; }
    public int LocationTypeID { get; set; }
    public string LocationTypeShortName { get; set; }
    public string CompanyFlag { get; set; }
    public string ParentCompanyCode { get; set; }
    public string ParentCompanyName{ get; set; }
    public string CompanyResult { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; } 
    public string Flag { get; set; }
    public bool IsActive { get; set; }
    public string StateShortName { get; set; }
    public int StateID { get; set; }
    public string StateName { get; set; }
    public int LocationID { get; set; }
    public string LocationName { get; set; }
}