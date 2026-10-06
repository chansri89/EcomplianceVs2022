using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CategoryMasterMsg
/// </summary>
public class CategoryMasterMsg
{
	public CategoryMasterMsg()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string CategoryFullName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string CategoryResult { get; set; }
    public string Flag { get; set; }
    public bool IsActive { get; set; }

}