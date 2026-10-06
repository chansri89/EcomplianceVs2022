using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DepartmentMsg
/// </summary>
public class UploadResultMsg
{
    public UploadResultMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Result { get; set; }
    public Int64 WParameterId { get; set; }

}

public class ExcelUploadMsg
{
    public ExcelUploadMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Column1 { get; set; }
    public string Column2 { get; set; }
    public string Column3 { get; set; }
    public string Column5 { get; set; }
    public string Column4 { get; set; }
    public string Column6 { get; set; }
    public string Column7 { get; set; }
    public string Column8 { get; set; }
    public string Column9 { get; set; }
    public string Column10 { get; set; }
    public string Column11 { get; set; }
    public string Column12 { get; set; }
    public string Column13 { get; set; }
    public string Column14 { get; set; }
    public string Column15 { get; set; }
    public string Column16 { get; set; }
    public string Column17 { get; set; }
    public string Column18 { get; set; }
    public string Column19 { get; set; }
    public string Column20 { get; set; }
    public string Column21 { get; set; }
    public string Column22 { get; set; }
    public string Column23 { get; set; }
    public string Column24 { get; set; }
    public string Column25 { get; set; }
    public string Column26 { get; set; }
    public string Column27 { get; set; }
    public string Column28 { get; set; }

    public string Column29 { get; set; }
    public string Column30 { get; set; }
    public string Column31 { get; set; }
    public string Column32 { get; set; }

    public string Column33 { get; set; }
    public string Column34 { get; set; }
    public string Column35 { get; set; }

    public int UploadType { get; set; }
}

public class DataErrorMsg
{
    public DataErrorMsg()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string Result { get; set; }
    public string RKount { get; set; }
    public string OrganizationCode { get; set; }
    public string OrganizationName { get; set; }
    public string PartNumber { get; set; }

}

public class ORGUpLoadStatus
{
    public ORGUpLoadStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string LocationName { get; set; }
    public string LocationCode { get; set; }
    public string LocationType { get; set; }
    public string LocationShortName { get; set; }
    public string LocationParentCode { get; set; }
    public string StateShortName { get; set; }
    public string Reason { get; set; }
    
}
public class SchemeUpLoadStatus
{
    public SchemeUpLoadStatus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string SchemeName { get; set; }
    public string IsSpecial { get; set; }
    public string ProductCategoryName { get; set; }
    public string PartNumber { get; set; }
    public string PartName { get; set; }
    public string IncentivePoint { get; set; }
    public string StartingDate { get; set; }
    public string EndDate { get; set; }
    public string Reason { get; set; }

}

