using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratingMail.Messages
{
    public class MailDetailMsg
    {
        public string ActName { get; set; }
        public string ActivityName { get; set; }
        public string ExecutionEmployeeName { get; set; }
        public string ToEmail { get; set; }
        public string ReviewEmployeeName { get; set; }
        public string CCMail { get; set; }
        public string Status { get; set; }
        public string DueDate{ get; set; }
        public string DueMonth { get; set; }
        public string DueDay { get; set; }
        public string BodyMessage { get; set; }
        public string FrequencyName { get; set; }
        public string TriggerDate { get; set; }
        public string TriggerMonth { get; set; }
        public string TriggerDay { get; set; }
        public string LocationName { get; set; }
        public string SeverityName { get; set; }
        public string CompanyHRMailId { get; set; } //scs 150315 RDC PO 100315
        public string CompanyCode { get; set; } //scs 150315 RDC PO 100315
        public string ParentCompanyCode { get; set; } //scs 150315 RDC PO 100315
        public string EmpType { get; set; } //scs 150315 RDC PO 100315 
        public string ActivityCategorization { get; set; }
        public string DueYear { get; set; }
    }
    public class AutoVersionMailMsg
    {
        public string MailTo { get; set; }
        public string MailCC { get; set; }
        public string LastMailDate { get; set; }
        public string PreviousDownLoadDate { get; set; }
        public int Kount { get; set; }
        public int MailFrequencyinDays { get; set; }
        public string TxtMsg { get; set; }
        public Int64 CompanyLinkedToWebServerId { get; set; }
      
    }
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

    public class CoordinatorMailSummary
    {
        public string CompanyShortName { get; set; }
        public string CompanyCode { get; set; }
        public string ActionsCreated { get; set; }
        public string ActionsCompleted { get; set; }
        public string ActionsPending { get; set; }
        public string OpBalPending { get; set; }
        public string FinancialQuarter { get; set; }
        public string ToEmail { get; set; }
        public string CoordinatorEmployeeName { get; set; }
    }
    public class CoordinatorMailDetail
    {
        public string CompanyShortName { get; set; }
        public string ToEmail { get; set; }
        public string CoordinatorEmployeeName { get; set; }
        public string ExecutionEmployeeName { get; set; }
        public string ReviewEmployeeName { get; set; }
        public string ActName { get; set; }
        public string ActivityName { get; set; }
        public string FrequencyName { get; set; }
        public string SeverityName { get; set; }
        public string DueOn { get; set; }
        public string TriggerDate { get; set; }
        public string TriggerLeadTime { get; set; }
        
        
    }
}
