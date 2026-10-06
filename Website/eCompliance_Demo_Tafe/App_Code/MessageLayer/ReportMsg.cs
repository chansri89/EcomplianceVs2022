using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeMasterMsg
/// </summary>
public class ReportMsg
{
    public ReportMsg()
	{
        //
        // TODO: Add constructor logic here
        //
        
	}
    public string EmployeeCode { get; set; }
    public string CompanyCode { get; set; }
    public int FYear { get; set; }
    public int FMonth { get; set; }
    public int YearMonth { get; set; }
    public int FMYearMonth { get; set; }
    public int ToYearMonth { get; set; }
    public string FormType { get; set; }
    public int WorkingDays { get; set; }
    public int completeddays { get; set; }
    public int DefaultFlag { get; set; }
    public string CommissionType { get; set; }
    public DateTime ReportDate { get; set; }
    public int FisicalYear { get; set; }
    public string ParameterFlag { get; set; }
    public int PrdnSupGrpId { get; set; }
    public string PlantCode { get; set; }
    public int Year { get; set; }
    public bool Recalculation { get; set; }
    public string Quarter { get; set; }
    public string SalesType { get; set; }
    public string ActionStatus { get; set; } //scs 110415 rdc mail dt 090415

    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public int ActId { get; set; }
    public int SeverityId { get; set; }
    public int WeighmentMachine { get; set; }
    public int Device { get; set; }
    public string WeighmentLocation { get; set; }
    //public DateTime ReceiptDate { get; set; }
    //public int MachineNo { get; set; }

}
