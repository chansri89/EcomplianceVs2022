using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ganini.Lib;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ProcessBus
/// </summary>
public class ProcessBus
{
    public ProcessBus()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    Validation Isvalid = new Validation();
    #region Connection

    private ConnectionClass MconnectionClass = null;
    private ConnectionClass Connection
    {
        get
        {
            if (null == MconnectionClass)
            {
                MconnectionClass = new Ganini.Lib.ConnectionClass();
            }
            return MconnectionClass;
        }

    }

    #endregion

    #region MasterSelect

    public List<CompanyMessage> CompanyMasterSelect(EmployeeMasterMsg Emp)
    {
        List<CompanyMessage> CompanyList = new List<CompanyMessage>();
        using (Connection.con)
        {

            try
            {

                Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CompanyMessage CompanyMessage = new CompanyMessage();
                        CompanyMessage.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        CompanyMessage.CompanyName = sdr["CompanyName"].ToString().Trim();
                        CompanyMessage.CompanyShortName = sdr["CompanyShortName"].ToString().Trim();
                        CompanyMessage.CompanyFlag = sdr["CompanyFlag"].ToString().Trim();
                        CompanyMessage.ParentCompanyName = sdr["ParentCompanyName"].ToString().Trim();
                        CompanyMessage.StateID = Convert.ToInt32(sdr["StateID"].ToString().Trim());
                        CompanyMessage.StateName = sdr["StateName"].ToString().Trim();
                        CompanyMessage.StateShortName = sdr["StateShortName"].ToString().Trim();
                        CompanyMessage.LocationID = Convert.ToInt32(sdr["LocationID"].ToString().Trim());
                        CompanyMessage.LocationName = sdr["LocationName"].ToString().Trim();
                        CompanyMessage.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        CompanyMessage.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        CompanyList.Add(CompanyMessage);
                    }
                }


            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return CompanyList;

    }
    public List<DocumentTypeMasterMsg> DocumentTypeMasterSelectSp()
    {
        List<DocumentTypeMasterMsg> DocList = new List<DocumentTypeMasterMsg>();
        using (Connection.con)
        {

            try
            {

                Connection.cmd.CommandText = "MasDocumentTypeMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
                        Doc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                        Doc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
                        Doc.DocumentTypeShortName = sdr["DocumentTypeShortName"].ToString().Trim();
                        Doc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Doc.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Doc.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Doc.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Doc.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        DocList.Add(Doc);
                    }
                }


            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return DocList;

    }

    //public List<ActivityForCompanyMasterMsg> ActMasterSelect(EmployeeMasterMsg Emp)
    //{
    //    List<ActivityForCompanyMasterMsg> ActivityCompanyList = new List<ActivityForCompanyMasterMsg>();
    //    using (Connection.con)
    //    {

    //        try
    //        {

    //            Connection.cmd.CommandText = "MasActMasterSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    ActivityForCompanyMasterMsg ActivityCompanyMsg = new ActivityForCompanyMasterMsg();
    //                    ActivityCompanyMsg.ActId = sdr["CompanyCode"].ToString();
    //                    ActivityCompanyMsg.CompanyName = sdr["CompanyName"].ToString();
    //                    ActivityCompanyMsg.CompanyShortName = sdr["CompanyShortName"].ToString();
    //                    ActivityCompanyMsg.CompanyFlag = sdr["CompanyFlag"].ToString();
    //                    ActivityCompanyMsg.ParentCompanyCode = sdr["ParentCompanyCode"].ToString();
    //                    ActivityCompanyMsg.ParentCompanyName = sdr["ParentCompanyName"].ToString();
    //                    ActivityCompanyList.Add(ActivityCompanyMsg);
    //                }
    //            }


    //        }

    //        catch (Exception ex)
    //        {
    //            //MessageBox.Show(ex.ToString());
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            //return null;
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //    }
    //    return ActivityCompanyList;

    //}


    public List<EmployeeMasterMsg> EmployeeMasterSelect(EmployeeMasterMsg Emp)
    {
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasEmployeeMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        EmployeeMasterMsg Employee = new EmployeeMasterMsg();
                        Employee.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        Employee.CompanyName = sdr["CompanyName"].ToString().Trim();
                        Employee.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                        Employee.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                        Employee.EmailId = sdr["EmailId"].ToString().Trim();
                        Employee.Password = sdr["PassWord"].ToString().Trim();
                        Employee.ManagerCode = sdr["ManagerCode"].ToString().Trim();
                        Employee.ManagerName = sdr["ManagerName"].ToString().Trim();
                        Employee.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        //Employee.RoleId = Convert.ToInt32(sdr["RoleId"].ToString().Trim());
                        //Employee.RoleName = sdr["RoleName"].ToString().Trim();
                        Employee.ActivityDesignation = sdr["ActivityDesignation"].ToString().Trim();
                        Employee.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Employee.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Employee.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Employee.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        EmpList.Add(Employee);
                    }
                }


            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return EmpList;
        }
    }

    public List<DepartmentMsg> DepartmentMasterSelect(EmployeeMasterMsg Emp)
    {
        List<DepartmentMsg> DeptList = new List<DepartmentMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasDepartmentMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        DepartmentMsg Dept = new DepartmentMsg();
                        Dept.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                        Dept.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                        Dept.DepartmentShortName = sdr["DepartmentShortName"].ToString().Trim();
                        Dept.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Dept.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Dept.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Dept.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Dept.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        DeptList.Add(Dept);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return DeptList;
        }
    }

    public List<RoleMsg> AdmAvailandAssignerRolesSelect(EmployeeMasterMsg emp)
    {
        List<RoleMsg> RolesList = new List<RoleMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmUserAssignedRolesSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@LoginEmployeeCode", emp.LoginEmployeeCode);//added by abinayaa 021913
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleMsg rolesMsg = new RoleMsg();
                        rolesMsg.AvRoleId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        rolesMsg.RoleName = sdr["RoleName"].ToString().Trim();
                        rolesMsg.AsgRoleId = Convert.ToInt32(sdr["RoleId"].ToString().Trim());
                        RolesList.Add(rolesMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return RolesList;
    }

    public int AdmUserRolesInsert(string EmployeeCode, ArrayList RoleIdList)
    {
        SqlTransaction transaction = null;
        List<RoleMsg> RolesList = new List<RoleMsg>();
        int Result = 0;
        int NewCount = 0;
        bool IsSuccess = true;
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.CommandText = "AdmUserRolesInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                foreach (string roleId in RoleIdList)
                {
                    if (IsSuccess)
                    {
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                        Connection.cmd.Parameters.AddWithValue("@RoleId", roleId);
                        Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        Result = Convert.ToInt32(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                        NewCount = 1;
                        if (Result != 0)
                        {
                            IsSuccess = false;
                        }
                    }
                }
                if (IsSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return 1;
            }
            finally
            {
                transaction.Dispose();
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return Result;
    }

    public List<CategoryMasterMsg> CategoryMasterSelect(EmployeeMasterMsg Emp)
    {
        List<CategoryMasterMsg> CtgryList = new List<CategoryMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasCategoryMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        CategoryMasterMsg Ctgry = new CategoryMasterMsg();
                        Ctgry.CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString().Trim());
                        Ctgry.CategoryName = sdr["CategoryName"].ToString().Trim();
                        Ctgry.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Ctgry.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Ctgry.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Ctgry.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Ctgry.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        CtgryList.Add(Ctgry);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return CtgryList;
        }
    }

    public List<FrequencyMasterMsg> FrequencyMasterSelect(EmployeeMasterMsg Emp)
    {
        List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasFrequencyMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        FrequencyMasterMsg Fncy = new FrequencyMasterMsg();
                        Fncy.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                        Fncy.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                        Fncy.FrequencyShortName = sdr["FrequencyShortName"].ToString().Trim();
                        Fncy.FrequencyDays = Convert.ToInt32(sdr["FrequencyDays"].ToString().Trim());
                        Fncy.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Fncy.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Fncy.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Fncy.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Fncy.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        FncyList.Add(Fncy);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return FncyList;
        }
    }

    public List<SeverityMasterMsg> SeverityMasterSelect(EmployeeMasterMsg Emp)
    {
        List<SeverityMasterMsg> SvrtyList = new List<SeverityMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasSeverityMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        SeverityMasterMsg Svrty = new SeverityMasterMsg();
                        Svrty.SeverityId = Convert.ToInt32(sdr["SeverityId"].ToString().Trim());
                        Svrty.SeverityName = sdr["SeverityName"].ToString().Trim();
                        Svrty.SeverityShortName = sdr["SeverityShortName"].ToString().Trim();
                        Svrty.Remarks = (sdr["Remarks"].ToString().Trim());
                        Svrty.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Svrty.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Svrty.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Svrty.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Svrty.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        SvrtyList.Add(Svrty);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return SvrtyList;
        }
    }

    //public List<DocumentTypeMasterMsg> ActivityDocumentTypeMasterSelect(ActivityForCompanyMasterMsg Activitycomp)
    //{
    //    List<DocumentTypeMasterMsg> DocList = new List<DocumentTypeMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            Connection.cmd.CommandText = "MasActivityDocumentTypeMasterSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", Activitycomp.CompanyActivityId);
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
    //                    //Doc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
    //                    //Doc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
    //                    //Doc.DocumentTypeShortName = sdr["DocumentTypeShortName"].ToString().Trim();
    //                    //Doc.CreatedBy = sdr["CreatedBy"].ToString().Trim();
    //                    //Doc.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
    //                    //Doc.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
    //                    //Doc.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
    //                    //Doc.Remarks = sdr["Remarks"].ToString().Trim();
    //                    //Doc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());


    //                    Doc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
    //                    Doc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
    //                    //Doc.DocumentTypeShortName = Convert.ToChar(sdr["DocumentTypeShortName"].ToString().Trim());
    //                    Doc.ActDoctypeId = Convert.ToInt32(sdr["ActDoctypeId"].ToString().Trim());
    //                    Doc.CompanyActivityId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
    //                    Doc.ActivityName = sdr["ActivityName"].ToString().Trim();
    //                    //Doc.ToBeMaintained = Convert.ToChar(sdr["ToBeMaintained"].ToString().Trim());
    //                    //Doc.ToBeSubmitted = Convert.ToChar(sdr["ToBeSubmitted"].ToString().Trim());
    //                    Doc.MasterPDFName = sdr["MasterPDFName"].ToString().Trim();
    //                    Doc.MasterPDFName = sdr["AccessPath"].ToString().Trim();
    //                    //Doc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
    //                    Doc.Remarks = sdr["Remarks"].ToString().Trim();
    //                    DocList.Add(Doc);
    //                }
    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //        return DocList;
    //    }
    //}

    public List<ActMasterMsg> ActMasterSelect(EmployeeMasterMsg Emp)
    {
        List<ActMasterMsg> ActList = new List<ActMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActId",Emp.ActId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActMasterMsg Act = new ActMasterMsg();
                        Act.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                        Act.ActName = sdr["ActName"].ToString().Trim();
                        Act.Chapter = sdr["Chapter"].ToString().Trim();
                        Act.Head = sdr["Head"].ToString().Trim();
                        Act.Section = sdr["Section"].ToString().Trim();
                        Act.ActRule = sdr["ActRule"].ToString().Trim();
                        Act.Description = sdr["Description"].ToString().Trim();
                        Act.IsActive=Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Act.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Act.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Act.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Act.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        ActList.Add(Act);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActList;
        }
    }

    public List<NonComplianceTaskMasterMsg> NonComplianceTaskMasterSelect(ActivityForCompanyMasterMsg ActivityComp)
    {
        List<NonComplianceTaskMasterMsg> NonCmpTaskList = new List<NonComplianceTaskMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityNonComplianceTaskMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityComp.CompanyActivityId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        NonComplianceTaskMasterMsg NonCmpTask = new NonComplianceTaskMasterMsg();
                        NonCmpTask.NonComplianceTaskId = Convert.ToInt32(sdr["NonComplianceTaskId"].ToString().Trim());
                        NonCmpTask.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                        NonCmpTask.ComplianceTaskName = sdr["ComplianceTaskName"].ToString().Trim();
                        NonCmpTask.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());                     
                        NonCmpTaskList.Add(NonCmpTask);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return NonCmpTaskList;
        }
    }

    public List<ActivityMasterMsg> ActivityMasterSelect(EmployeeMasterMsg Emp,int ActId)
    {
        List<ActivityMasterMsg> ActivityList = new List<ActivityMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@ActId", ActId);
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);         
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActivityMasterMsg Activity = new ActivityMasterMsg();
                        Activity.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                        Activity.ActivityName = sdr["ActivityName"].ToString().Trim();
                        Activity.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                        Activity.ActName = sdr["ActName"].ToString().Trim();
                        Activity.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                        Activity.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                        Activity.CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString().Trim());
                        Activity.CategoryName = sdr["CategoryName"].ToString().Trim();
                        Activity.SeverityId = Convert.ToInt32(sdr["SeverityId"].ToString().Trim());
                        Activity.SeverityName = sdr["SeverityName"].ToString().Trim();
                        Activity.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        Activity.NonComplianceTaskAvailableFlag = Convert.ToChar(sdr["NonComplianceTaskAvailableFlag"].ToString().Trim());
                        Activity.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                        Activity.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                        Activity.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        Activity.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        ActivityList.Add(Activity);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActivityList;
        }
    }

    public List<ActivityForCompanyMasterMsg> ActivityForCompanyMasterSelect(EmployeeMasterMsg Emp)
    {
        List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityForCompanyMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@CompanyCode", Emp.CompanyCode);
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@ActId", Emp.ActId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActivityForCompanyMasterMsg ActivityForCmpny = new ActivityForCompanyMasterMsg();
                        ActivityForCmpny.CompanyActivityId = Convert.ToInt32(sdr["CompanyActivityId"].ToString().Trim());
                        ActivityForCmpny.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                        ActivityForCmpny.ActivityName = sdr["ActivityName"].ToString().Trim();
                        ActivityForCmpny.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        ActivityForCmpny.CompanyName = sdr["CompanyName"].ToString().Trim();
                        ActivityForCmpny.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                        ActivityForCmpny.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                        ActivityForCmpny.DueMonth = Convert.ToInt32(sdr["DueMonth"].ToString().Trim());
                        ActivityForCmpny.DueDate = Convert.ToInt32(sdr["DueDate"].ToString().Trim());
                        ActivityForCmpny.DueDay = sdr["DueDay"].ToString().Trim();
                        ActivityForCmpny.TriggerMonth = Convert.ToInt32(sdr["TriggerMonth"].ToString().Trim());
                        ActivityForCmpny.TriggerDate = Convert.ToInt32(sdr["TriggerDate"].ToString().Trim());
                        ActivityForCmpny.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                        ActivityForCmpny.ExecutionEmployeeCode = (sdr["ExecutionEmployeeCode"].ToString().Trim());
                        ActivityForCmpny.Executioner = sdr["Executioner"].ToString().Trim();
                        ActivityForCmpny.ReviewEmployeeCode = (sdr["ReviewEmployeeCode"].ToString().Trim());
                        ActivityForCmpny.Reviewer = sdr["Reviewer"].ToString().Trim();
                        ActivityForCmpny.HeadEmployeeCode = (sdr["HeadEmployeeCode"].ToString().Trim());
                        ActivityForCmpny.HeadEmployeeName = sdr["HeadEmployeeName"].ToString().Trim();
                        ActivityForCmpny.UltimateEmployeeCode = (sdr["UltimateEmployeeCode"].ToString().Trim());
                        ActivityForCmpny.UltimateEmployeeName = sdr["UltimateEmployeeName"].ToString().Trim();
                        ActivityForCmpny.ActivityOutsideStackFlag = Convert.ToChar(sdr["ActivityOutsideStackFlag"].ToString().Trim());
                        ActivityForCmpny.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        ActivityForCmpny.NonComplianceTaskAvailableFlag = Convert.ToChar(sdr["NonComplianceTaskAvailableFlag"].ToString().Trim());//Added by Abinayaa 261012
                        ActivityForCmpny.ResponsibleGroupName = sdr["ResponsibleGroupName"].ToString().Trim();
                        ActivityForCmpny.LocationDepartmentId = Convert.ToInt32(sdr["AssigneeMatrixId"].ToString().Trim());
                        //ActivityForCmpny.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                        //ActivityForCmpny.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                        ActivityForCmpnyList.Add(ActivityForCmpny);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActivityForCmpnyList;
        }
    }

    public ActivityForCompanyMasterMsg ActivityForCompanyFreqeuncyMasterSelect(ActivityForCompanyMasterMsg ActivityforCompanyMsg)
    {
        ActivityForCompanyMasterMsg ActivityForCmpny = new ActivityForCompanyMasterMsg();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityForCompanyFrequencySelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActivityId", ActivityforCompanyMsg.ActivityId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActivityForCmpny.ActivityId = ActivityforCompanyMsg.ActivityId;//Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                        ActivityForCmpny.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                        ActivityForCmpny.FrqRemarks=sdr["FrequencyRemarks"].ToString().Trim();
                        ActivityForCmpny.DueMonth = Convert.ToInt32(sdr["DueMonth"].ToString().Trim());
                        ActivityForCmpny.DueDate = Convert.ToInt32(sdr["DueDate"].ToString().Trim());
                        ActivityForCmpny.DueDay = sdr["DueDay"].ToString().Trim();
                        ActivityForCmpny.TriggerMonth = Convert.ToInt32(sdr["TriggerMonth"].ToString().Trim());
                        ActivityForCmpny.TriggerDate = Convert.ToInt32(sdr["TriggerDate"].ToString().Trim());
                        ActivityForCmpny.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActivityForCmpny;
        }
    }



    public List<ActivityDocumentTypeMasterMsg> ActivityDocumentTypeMasterSelect(ActivityForCompanyMasterMsg ActivityComp)
    {
        List<ActivityDocumentTypeMasterMsg> ActivityForDocList = new List<ActivityDocumentTypeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityDocumentTypeMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityComp.CompanyActivityId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActivityDocumentTypeMasterMsg ActivityForDoc = new ActivityDocumentTypeMasterMsg();
                        ActivityForDoc.ActDocTypeId = Convert.ToInt32(sdr["ActDoctypeId"].ToString().Trim());
                        ActivityForDoc.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                        ActivityForDoc.ActivityName = sdr["ActivityName"].ToString().Trim();
                        ActivityForDoc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                        ActivityForDoc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
                        ActivityForDoc.ToBeMaintained = sdr["ToBeMaintained"].ToString().Trim().Trim();
                        ActivityForDoc.ToBeSubmitted = sdr["ToBeSubmitted"].ToString().Trim().Trim();
                        ActivityForDoc.MasterPDFName = sdr["MasterPDFName"].ToString().Trim();
                        ActivityForDoc.AccessPath = sdr["AccessPath"].ToString().Trim();
                        ActivityForDoc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        ActivityForDocList.Add(ActivityForDoc);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActivityForDocList;
        }
    }
    public List<StateMasterMsg> StateMasterSelect(EmployeeMasterMsg Emp)
    {
        List<StateMasterMsg> StateList = new List<StateMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasStateMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        StateMasterMsg St = new StateMasterMsg();
                        St.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                        St.StateName = sdr["StateName"].ToString().Trim();
                        St.StateShortName = sdr["StateShortName"].ToString().Trim();
                        StateList.Add(St);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return StateList;
        }
    }
    public List<LocationMsg> LocationSelect()
    {
        List<LocationMsg> LocationList = new List<LocationMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasLocationSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LocationMsg LocMsg = new LocationMsg();
                        LocMsg.LocationId = Convert.ToInt32(sdr["LocationId"].ToString().Trim());
                        LocMsg.LocationName = sdr["LocationName"].ToString().Trim();
                        LocMsg.LocationShortName = sdr["LocationShortName"].ToString().Trim();
                        LocationList.Add(LocMsg);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return LocationList;
        }
    }
    /// <summary>
    /// To Select Roles
    /// </summary>
    /// <param name="Emp"></param>
    /// <returns></returns>
    /// 
    //Commented by abinayaa 020813
    //public List<RoleMsg> AdmRolesSelect()
    //{
    //    List<RoleMsg> RolesList = new List<RoleMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {
    //            Connection.cmd.CommandText = "AdmRolesSelectSp";
    //            Connection.cmd.CommandType = CommandType.StoredProcedure;
    //            Connection.cmd.Connection = Connection.con;
    //            using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //            {
    //                while (sdr.Read())
    //                {
    //                    RoleMsg rolesMsg = new RoleMsg();
    //                    rolesMsg.RoleId = Convert.ToInt32(sdr["Id"].ToString().Trim());
    //                    rolesMsg.RoleName = sdr["RoleName"].ToString().Trim();
    //                    RolesList.Add(rolesMsg);
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //    }
    //    return RolesList;
    //}
    //Commented by abinayaa 020813
    public List<RoleMsg> AdmRolesSelect(RoleMsg RoleMsg)//added by abinayaa 020913
    {
        List<RoleMsg> RolesList = new List<RoleMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmRolesSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", RoleMsg.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleMsg rolesMsg = new RoleMsg();
                        rolesMsg.RoleId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        rolesMsg.RoleName = sdr["RoleName"].ToString().Trim();
                        RolesList.Add(rolesMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return RolesList;
    }
    /// <summary>
    /// To Select Program for the Role
    /// </summary>
    /// <param name="Emp"></param>
    /// <returns></returns>
    public List<RoleProgramsMsg> AdmRoleProgramsSelect(int RoleId)
    {
        List<RoleProgramsMsg> RoleProgramsList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmRoleProgramSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@RoleId", RoleId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        RoleProgramsMsg roleProgramsMsg = new RoleProgramsMsg();
                        roleProgramsMsg.Result = "0";
                        roleProgramsMsg.ProgramId = Convert.ToInt32(sdr["ProgramId"].ToString().Trim());
                        roleProgramsMsg.ProgramName = sdr["ProgramName"].ToString().Trim();
                        roleProgramsMsg.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                        roleProgramsMsg.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                        roleProgramsMsg.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                        roleProgramsMsg.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                        roleProgramsMsg.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                        RoleProgramsList.Add(roleProgramsMsg);
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return RoleProgramsList;
    }

    public List<ProgramMsg> AdmUserAccessProgramsSelect(EmployeeMasterMsg emp)
    {
        List<ProgramMsg> ProgramMsgList = new List<ProgramMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "AdmUserAccessProgramsSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ProgramMsg programMsg = new ProgramMsg();
                        programMsg.Result = "0";
                        programMsg.ProgramId = Convert.ToInt32(sdr["Id"].ToString().Trim());
                        programMsg.ProgramName = sdr["ProgramName"].ToString().Trim();
                        programMsg.ProgramAccessPath = sdr["ProgramAccessPath"].ToString().Trim();
                        programMsg.MainMenu = sdr["MainMenu"].ToString().Trim();
                        programMsg.SubMenu = sdr["SubMenu"].ToString().Trim();
                        programMsg.ChildMenu = sdr["ChildMenu"].ToString().Trim();
                        programMsg.ProgramSequence = Convert.ToInt32(sdr["ProgramSequence"].ToString().Trim());
                        programMsg.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                        programMsg.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                        programMsg.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                        programMsg.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                        programMsg.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                        ProgramMsgList.Add(programMsg);
                    }
                }
                if (Connection.cmd.Parameters["@Result"].Value.ToString().Trim() != "0")
                {
                    ProgramMsgList = new List<ProgramMsg>();
                    ProgramMsg program = new ProgramMsg();
                    program.Result = Connection.cmd.Parameters["@Result"].Value.ToString().Trim();
                    ProgramMsgList.Add(program);
                    return ProgramMsgList;
                }

            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return ProgramMsgList;
    }

    #endregion

    #region Master Insert,Update,Delete

    //To insert,update,delete and select Company Master
      public List<CompanyMessage> MasCompanyInsertUpdateandDelete(CompanyMessage Company, EmployeeMasterMsg EmpMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CompanyMessage> CompanyList = new List<CompanyMessage>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Company.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCompanyMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Company.Flag);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Company.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@CompanyName", Company.CompanyName);
                    Connection.cmd.Parameters.AddWithValue("@CompanyShortName", Company.CompanyShortName);
                    Connection.cmd.Parameters.AddWithValue("@LocationTypeId", Company.LocationTypeID);
                    Connection.cmd.Parameters.AddWithValue("@CompanyFlag", Company.CompanyFlag);
                    Connection.cmd.Parameters.AddWithValue("@ParentCompanyCode", Company.ParentCompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@StateId", Company.StateID);
                    Connection.cmd.Parameters.AddWithValue("@LocationId", Company.LocationID);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Company.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Company.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmpMsg.EmployeeCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CompanyMessage CompanyMessage = new CompanyMessage();
                            CompanyMessage.CompanyResult = Result;
                            CompanyMessage.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            CompanyMessage.CompanyName = sdr["CompanyName"].ToString().Trim();
                            CompanyMessage.CompanyShortName = sdr["CompanyShortName"].ToString().Trim();
                            CompanyMessage.LocationTypeID = Convert.ToInt32(sdr["LocationTypeId"].ToString().Trim());
                            CompanyMessage.LocationTypeShortName = sdr["LocationTypeShortName"].ToString().Trim();
                            CompanyMessage.CompanyFlag = sdr["CompanyFlag"].ToString().Trim();
                            CompanyMessage.ParentCompanyName = sdr["ParentCompanyName"].ToString().Trim();
                            CompanyMessage.StateID = Convert.ToInt32(sdr["StateID"].ToString().Trim());
                            CompanyMessage.StateName = sdr["StateName"].ToString().Trim();
                            CompanyMessage.StateShortName = sdr["StateShortName"].ToString().Trim();
                            CompanyMessage.LocationID = Convert.ToInt32(sdr["LocationID"].ToString().Trim());
                            CompanyMessage.LocationName = sdr["LocationName"].ToString().Trim();
                            CompanyMessage.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            CompanyMessage.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            CompanyList.Add(CompanyMessage);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CompanyMessage CompanyMessage = new CompanyMessage();
                    CompanyMessage.CompanyResult = Result;
                    CompanyList.Add(CompanyMessage);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return CompanyList;
        }
    }

    //To insert,update,delete and select Employee Master
    public List<EmployeeMasterMsg> MasEmployeeInsertUpdateandDelete(EmployeeMasterMsg Employee)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Employee.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasEmployeeMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Employee.Flag);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Employee.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeName", Employee.EmployeeName);
                    Connection.cmd.Parameters.AddWithValue("@EmailId", Employee.EmailId);
                    Connection.cmd.Parameters.AddWithValue("@PassWord", Employee.Password);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Employee.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@ManagerCode", Employee.ManagerCode);
                    Connection.cmd.Parameters.AddWithValue("@ActivityDesignation", Employee.ActivityDesignation);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Employee.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@IsCompanyAdmin", Employee.IsCompanyAdmin);
                    //Connection.cmd.Parameters.AddWithValue("@RoleId", Employee.RoleId);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Employee.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasEmployeeMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Employee.LoginEmployeeCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            EmployeeMasterMsg EmpMessage = new EmployeeMasterMsg();
                            EmpMessage.EmployeeResult = Result;
                            EmpMessage.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            EmpMessage.CompanyName = sdr["CompanyName"].ToString().Trim();
                            EmpMessage.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            EmpMessage.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            EmpMessage.EmailId = sdr["EmailId"].ToString().Trim();
                            EmpMessage.Password = sdr["PassWord"].ToString().Trim();
                            EmpMessage.ManagerCode = sdr["ManagerCode"].ToString().Trim();
                            EmpMessage.ManagerName = sdr["ManagerName"].ToString().Trim();
                            EmpMessage.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            //EmpMessage.RoleId = Convert.ToInt32(sdr["RoleId"].ToString());
                            //EmpMessage.RoleName = sdr["RoleName"].ToString();
                            EmpMessage.ActivityDesignation = sdr["ActivityDesignation"].ToString().Trim();
                            EmpMessage.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            EmpMessage.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            EmpMessage.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            EmpMessage.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            EmpMessage.IsCompanyAdmin = Convert.ToBoolean(sdr["IsCompanyAdmin"].ToString().Trim());
                            EmpList.Add(EmpMessage);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    EmployeeMasterMsg EmpMessage = new EmployeeMasterMsg();
                    EmpMessage.EmployeeResult = Result;
                    EmpList.Add(EmpMessage);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return EmpList;
        }
    }

    //To insert,update,delete and select department Master\
    public List<DepartmentMsg> MasDepartmentInsertUpdateandDelete(DepartmentMsg Department)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<DepartmentMsg> DeptList = new List<DepartmentMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Department.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasDepartmentMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Department.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WDId", Department.DepartmentId);
                    Connection.cmd.Parameters.AddWithValue("@DepartmentName", Department.DepartmentName);
                    Connection.cmd.Parameters.AddWithValue("@DepartmentShortName", Department.DepartmentShortName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Department.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Department.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasDepartmentMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            DepartmentMsg Dept = new DepartmentMsg();
                            Dept.DeptResult = Result;
                            Dept.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            Dept.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                            Dept.DepartmentShortName = sdr["DepartmentShortName"].ToString().Trim();
                            Dept.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Dept.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Dept.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Dept.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Dept.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            DeptList.Add(Dept);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    DepartmentMsg Dept = new DepartmentMsg();
                    Dept.DeptResult = Result;
                    DeptList.Add(Dept);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return DeptList;
        }
    }

    //To insert,update,delete and select Category Master
   public List<CategoryMasterMsg> MasCategoryInsertUpdateandDelete(CategoryMasterMsg Category)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<CategoryMasterMsg> CtgryList = new List<CategoryMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Category.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasCategoryMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Category.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WCId", Category.CategoryId);
                    Connection.cmd.Parameters.AddWithValue("@CategoryName", Category.CategoryName);
                    Connection.cmd.Parameters.AddWithValue("@CategoryFullName", Category.CategoryFullName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Category.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Category.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasCategoryMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            CategoryMasterMsg Ctgry = new CategoryMasterMsg();
                            Ctgry.CategoryResult = Result;
                            Ctgry.CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString().Trim());
                            Ctgry.CategoryName = sdr["CategoryName"].ToString().Trim();
                            Ctgry.CategoryFullName = sdr["CategoryFullName"].ToString().Trim();
                            Ctgry.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Ctgry.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Ctgry.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Ctgry.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Ctgry.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            CtgryList.Add(Ctgry);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    CategoryMasterMsg Ctgry = new CategoryMasterMsg();
                    Ctgry.CategoryResult = Result;
                    CtgryList.Add(Ctgry);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return CtgryList;
        }
    }
    //To insert,update,delete and select FreQuency Master
    public List<FrequencyMasterMsg> MasFrequencyInsertUpdateandDelete(FrequencyMasterMsg FreQuency)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<FrequencyMasterMsg> FncyList = new List<FrequencyMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (FreQuency.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasFrequencyMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", FreQuency.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WFId", FreQuency.FrequencyId);
                    Connection.cmd.Parameters.AddWithValue("@FrequencyName", FreQuency.FrequencyName);
                    Connection.cmd.Parameters.AddWithValue("@FrequencyShortName", FreQuency.FrequencyShortName);
                    Connection.cmd.Parameters.AddWithValue("@FrequencyDays", FreQuency.FrequencyDays);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", FreQuency.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", FreQuency.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasFrequencyMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            FrequencyMasterMsg Fncy = new FrequencyMasterMsg();
                            Fncy.FrequencyResult = Result;
                            Fncy.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                            Fncy.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                            Fncy.FrequencyShortName = sdr["FrequencyShortName"].ToString().Trim();
                            Fncy.FrequencyDays = Convert.ToInt32(sdr["FrequencyDays"].ToString().Trim());
                            Fncy.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Fncy.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Fncy.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Fncy.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Fncy.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            FncyList.Add(Fncy);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    FrequencyMasterMsg Fncy = new FrequencyMasterMsg();
                    Fncy.FrequencyResult = Result;
                    FncyList.Add(Fncy);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return FncyList;
        }
    }
    //To insert,update,delete and select Severity Master
    public List<SeverityMasterMsg> MasSeverityInsertUpdateandDelete(SeverityMasterMsg Severity)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<SeverityMasterMsg> SvrtyList = new List<SeverityMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Severity.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasSeverityMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Severity.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WSId", Severity.SeverityId);
                    Connection.cmd.Parameters.AddWithValue("@SeverityName", Severity.SeverityName);
                    Connection.cmd.Parameters.AddWithValue("@SeverityShortName", Severity.SeverityShortName);
                    Connection.cmd.Parameters.AddWithValue("@Remarks", Severity.Remarks);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Severity.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Severity.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasSeverityMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            SeverityMasterMsg Svrty = new SeverityMasterMsg();
                            Svrty.SeverityResult = Result;
                            Svrty.SeverityId = Convert.ToInt32(sdr["SeverityId"].ToString().Trim());
                            Svrty.SeverityName = sdr["SeverityName"].ToString().Trim();
                            Svrty.SeverityShortName = sdr["SeverityShortName"].ToString().Trim();
                            Svrty.Remarks = (sdr["Remarks"].ToString().Trim());
                            Svrty.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Svrty.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Svrty.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Svrty.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Svrty.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            SvrtyList.Add(Svrty);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    SeverityMasterMsg Svrty = new SeverityMasterMsg();
                    Svrty.SeverityResult = Result;
                    SvrtyList.Add(Svrty);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return SvrtyList;
        }
    }
    //public List<SeverityMasterMsg> MasActivityDocumentInsert(SeverityMasterMsg Severity)
    //{
    //    SqlTransaction transaction = null;
    //    string Result = "0";
    //    List<SeverityMasterMsg> SvrtyList = new List<SeverityMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {

    //            transaction = Connection.con.BeginTransaction();
    //            Connection.cmd.Transaction = transaction;
    //            Connection.cmd.Connection = Connection.con;
    //            if (Severity.Flag != "R")
    //            {
    //                Connection.cmd.CommandText = "MasSeverityMasterInsertandUpdateSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;

    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@Flag", Severity.Flag);
    //                Connection.cmd.Parameters.AddWithValue("@WSId", Severity.SeverityId);
    //                Connection.cmd.Parameters.AddWithValue("@SeverityName", Severity.SeverityName);
    //                Connection.cmd.Parameters.AddWithValue("@SeverityShortName", Severity.SeverityShortName);
    //                Connection.cmd.Parameters.AddWithValue("@Remarks", Severity.Remarks);
    //                Connection.cmd.Parameters.AddWithValue("@IsActive", Severity.IsActive);
    //                Connection.cmd.Parameters.AddWithValue("@CreatedBy", Severity.CreatedBy);
    //                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
    //                Connection.cmd.ExecuteNonQuery();
    //                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString());
    //            }
    //            if (Result == "0")
    //            {
    //                Connection.cmd.CommandText = "MasSeverityMasterSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {

    //                        SeverityMasterMsg Svrty = new SeverityMasterMsg();
    //                        Svrty.SeverityResult = Result;
    //                        Svrty.SeverityId = Convert.ToInt32(sdr["SeverityId"].ToString());
    //                        Svrty.SeverityName = sdr["SeverityName"].ToString();
    //                        Svrty.SeverityShortName = sdr["SeverityShortName"].ToString();
    //                        Svrty.Remarks = (sdr["Remarks"].ToString());
    //                        Svrty.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString());
    //                        Svrty.CreatedBy = sdr["CreatedBy"].ToString();
    //                        Svrty.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString());
    //                        Svrty.ModifiedBy = sdr["ModifiedBy"].ToString();
    //                        Svrty.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString());
    //                        SvrtyList.Add(Svrty);
    //                    }

    //                }
    //                transaction.Commit();
    //                Connection.cmd.Parameters.Clear();
    //                Connection.con.Close();
    //            }
    //            else
    //            {
    //                SeverityMasterMsg Svrty = new SeverityMasterMsg();
    //                Svrty.SeverityResult = Result;
    //                SvrtyList.Add(Svrty);

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            if (transaction != null)
    //            {
    //                transaction.Rollback();
    //            }
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

    //        }
    //        finally
    //        {
    //            Connection.cmd.Parameters.Clear();
    //        }

    //        return SvrtyList;
    //    }
    //}
    public List<DocumentTypeMasterMsg> MasDocumentTypeInsertUpdateandDelete(DocumentTypeMasterMsg DocType)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<DocumentTypeMasterMsg> DocList = new List<DocumentTypeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (DocType.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasDocumentTypeMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", DocType.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WDTId", DocType.DocumentTypeId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeName", DocType.DocumentTypeName);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeShortName", DocType.DocumentTypeShortName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", DocType.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", DocType.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasDocumentTypeMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
                            Doc.DocTypeResult = Result;
                            Doc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                            Doc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
                            Doc.DocumentTypeShortName = sdr["DocumentTypeShortName"].ToString().Trim();
                            Doc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Doc.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Doc.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Doc.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Doc.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            DocList.Add(Doc);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    DocumentTypeMasterMsg Doc = new DocumentTypeMasterMsg();
                    Doc.DocTypeResult = Result;
                    DocList.Add(Doc);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return DocList;
        }
    }
    public List<ActivityDocumentMsg> MasActivityDocumentInsertUpdateandDelete(ActivityDocumentMsg ActivityDocMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ActivityDocumentMsg> AcgtivityDocList = new List<ActivityDocumentMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (ActivityDocMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityDocumentMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", ActivityDocMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ActivityId", ActivityDocMsg.ActivityId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityDocumentId", ActivityDocMsg.ActivityDocumentId);
                    Connection.cmd.Parameters.AddWithValue("@AccessPath", ActivityDocMsg.AccessPath);
                    Connection.cmd.Parameters.AddWithValue("@DocumentName", ActivityDocMsg.DocumentName);
                    Connection.cmd.Parameters.AddWithValue("@LocationId", ActivityDocMsg.LocationId);
                    Connection.cmd.Parameters.AddWithValue("@StateId", ActivityDocMsg.StateId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeId", ActivityDocMsg.DocumentTypeId);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", ActivityDocMsg.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivityDocMsg.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityDocumentMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActivityId", ActivityDocMsg.ActivityId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityDocumentId", ActivityDocMsg.ActivityDocumentId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ActivityDocumentMsg ActivityDoc = new ActivityDocumentMsg();
                            ActivityDoc.ActivityDocResult = Result;
                            ActivityDoc.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                            ActivityDoc.ActivityDocumentId = Convert.ToInt32(sdr["ActivityDocumentId"].ToString().Trim());
                            //ActivityDoc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString());
                            ActivityDoc.DocumentName = sdr["DocumentName"].ToString().Trim();
                            ActivityDoc.LocationId = Convert.ToInt32(sdr["LocationId"].ToString().Trim());
                            ActivityDoc.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                            ActivityDoc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                            ActivityDoc.DocumentType = sdr["DocumentTypeName"].ToString().Trim();
                            ActivityDoc.State = sdr["StateName"].ToString().Trim();
                            ActivityDoc.Location = sdr["Location"].ToString().Trim();
                            ActivityDoc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            AcgtivityDocList.Add(ActivityDoc);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ActivityDocumentMsg Doc = new ActivityDocumentMsg();
                    Doc.ActivityDocResult = Result;
                    AcgtivityDocList.Add(Doc);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return AcgtivityDocList;
        }
    }
    public List<ActivityActionMsg> ActivityActionSelect(EmployeeMasterMsg Emp, ActivityActionMsg ActivityActionmsg)
    {
        List<ActivityActionMsg> ActivityActionList = new List<ActivityActionMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityActionSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActId", Emp.ActId);
                Connection.cmd.Parameters.AddWithValue("@CompanyCode", Emp.CompanyCode);
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                //Connection.cmd.Parameters.AddWithValue("@AsandwhenFlag", ActivityActionmsg.AsandwhenFlag); //Commented By Sathish 25122012
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ActivityActionMsg ActivityAction = new ActivityActionMsg();
                        ActivityAction.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                        ActivityAction.ActName = sdr["ActName"].ToString().Trim();
                        ActivityAction.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                        ActivityAction.ActivityName = sdr["ActivityName"].ToString().Trim();
                        ActivityAction.IsStateSpecific = Convert.ToBoolean(sdr["IsStateSpecific"].ToString().Trim());
                        ActivityAction.IsRegularActivity = Convert.ToBoolean(sdr["IsRegularActivity"].ToString().Trim());
                        //ActivityAction.IsStateSpecific = Convert.ToBoolean(sdr[""].ToString().Trim());
                        ActivityAction.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                        ActivityAction.ActivityActionId = Convert.ToInt64(sdr["ActivityActionId"].ToString().Trim());
                        ActivityAction.ReminderYear = Convert.ToInt32(sdr["ReminderYear"].ToString().Trim());
                        ActivityAction.ReminderMonth = Convert.ToInt32(sdr["ReminderMonth"].ToString().Trim());
                        ActivityAction.ReminderDate = Convert.ToInt32(sdr["ReminderDate"].ToString().Trim());
                        ActivityAction.ReminderDay = sdr["ReminderDay"].ToString().Trim();
                        ActivityAction.TriggerMonth = Convert.ToInt32(sdr["TriggerMonth"].ToString().Trim());
                        ActivityAction.TriggerDate = Convert.ToInt32(sdr["TriggerDate"].ToString().Trim());
                        ActivityAction.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                        ActivityAction.CompletedDate = Convert.ToDateTime(sdr["CompletedDate"].ToString().Trim());
                        ActivityAction.Remarks = sdr["Remarks"].ToString().Trim();
                        ActivityAction.ExecutionEmployeeCode = (sdr["ExecutionEmployeeCode"].ToString().Trim());
                        ActivityAction.reviewEmployeeCode = sdr["reviewEmployeeCode"].ToString().Trim();
                        ActivityAction.HeadEmployeeCode = (sdr["HeadEmployeeCode"].ToString().Trim());
                        ActivityAction.ExecutionEmployeeName = (sdr["ExecutionEmployeeName"].ToString().Trim());
                        ActivityAction.reviewEmployeeName = sdr["ReviewEmployeeName"].ToString().Trim();
                        ActivityAction.HeadEmployeeName = (sdr["HeadEmployeeName"].ToString().Trim());
                        ActivityAction.UltimateEmployeeCode = sdr["UltimateEmployeeCode"].ToString().Trim();
                        ActivityAction.UltimateEmployeeName = sdr["UltimateEmployeeName"].ToString().Trim();
                        ActivityAction.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                        ActivityAction.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                        ActivityAction.FrequencyDays = sdr["FrequencyDays"].ToString().Trim();
                        ActivityAction.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        ActivityAction.AsandwhenFlag = Convert.ToBoolean(sdr["AsandwhenFlag"].ToString().Trim());
                        ActivityAction.SeverityShortName = sdr["SeverityShortName"].ToString().Trim();
                        ActivityAction.SeverityName = sdr["SeverityName"].ToString().Trim();
                        ActivityAction.NonComplianceTaskAvailableFlag = sdr["NonComplianceTaskAvailableFlag"].ToString().Trim();//Abinayaa 200712 Added for NonComplianceTask
                        ActivityAction.docAvbl = Convert.ToInt16(sdr["docAvbl"].ToString().Trim());// added by Abinayaa 040213--Check doc is available or not ----
                        ActivityAction.IsdocReq = Convert.ToString(sdr["IsdocReq"].ToString().Trim());// added by Abinayaa 040213--Check doc is available or not ----
                        ActivityActionList.Add(ActivityAction);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ActivityActionList;
        }
    }
    public List<ActivityActionMsg> MasActivityActionInsert(ActivityActionMsg ActivtyAction, EmployeeMasterMsg Emp)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ActivityActionMsg> ActionList = new List<ActivityActionMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (ActivtyAction.Flag == "U")
                {
                    Connection.cmd.CommandText = "MasActivityActionInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@WActivityActionId", ActivtyAction.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivtyAction.ActivityForCompanyId);
                    Connection.cmd.Parameters.AddWithValue("@CompletedDate", ActivtyAction.CompletedDate);
                    Connection.cmd.Parameters.AddWithValue("@Remarks", ActivtyAction.Remarks);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivtyAction.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                else
                {
                    Connection.cmd.CommandText = "MasActivityActionInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@WActivityActionId", ActivtyAction.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivtyAction.ActivityForCompanyId);
                    Connection.cmd.Parameters.AddWithValue("@CompletedDate", ActivtyAction.CompletedDate);
                    Connection.cmd.Parameters.AddWithValue("@Remarks", ActivtyAction.Remarks);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivtyAction.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@ExecutionEmployeeCode", ActivtyAction.ExecutionEmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@DueYear", ActivtyAction.ReminderYear);
                    Connection.cmd.Parameters.AddWithValue("@DueDay", ActivtyAction.ReminderDay);
                    Connection.cmd.Parameters.AddWithValue("@DueMonth", ActivtyAction.ReminderMonth);
                    Connection.cmd.Parameters.AddWithValue("@DueDate", ActivtyAction.ReminderDate);
                    Connection.cmd.Parameters.AddWithValue("@TriggerDay", ActivtyAction.TriggerDay);
                    Connection.cmd.Parameters.AddWithValue("@TriggerMonth", ActivtyAction.TriggerMonth);
                    Connection.cmd.Parameters.AddWithValue("@TriggerDate", ActivtyAction.TriggerDate);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }


                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityActionSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActId", Emp.ActId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Emp.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@AsandwhenFlag", ActivtyAction.AsandwhenFlag);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            ActivityActionMsg ActivityAction = new ActivityActionMsg();
                            ActivityAction.ActionResult = Result;
                            ActivityAction.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                            ActivityAction.ActName = sdr["ActName"].ToString().Trim();
                            ActivityAction.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                            ActivityAction.ActivityName = sdr["ActivityName"].ToString().Trim();
                            ActivityAction.IsStateSpecific = Convert.ToBoolean(sdr["IsStateSpecific"].ToString().Trim());
                            ActivityAction.IsRegularActivity = Convert.ToBoolean(sdr["IsRegularActivity"].ToString().Trim());
                            ActivityAction.NonComplianceTaskAvailableFlag = sdr["NonComplianceTaskAvailableFlag"].ToString().Trim();//Abinayaa 200712 Added for NonComplianceTask
                            //ActivityAction.IsStateSpecific = Convert.ToBoolean(sdr[""].ToString().Trim());
                            ActivityAction.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                            ActivityAction.ActivityActionId = Convert.ToInt64(sdr["ActivityActionId"].ToString().Trim());
                            ActivityAction.ReminderYear = Convert.ToInt32(sdr["ReminderYear"].ToString().Trim());
                            ActivityAction.ReminderMonth = Convert.ToInt32(sdr["ReminderMonth"].ToString().Trim());
                            ActivityAction.ReminderDate = Convert.ToInt32(sdr["ReminderDate"].ToString().Trim());
                            ActivityAction.ReminderDay = sdr["ReminderDay"].ToString().Trim();
                            ActivityAction.TriggerMonth = Convert.ToInt32(sdr["TriggerMonth"].ToString().Trim());
                            ActivityAction.TriggerDate = Convert.ToInt32(sdr["TriggerDate"].ToString().Trim());
                            ActivityAction.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                            ActivityAction.CompletedDate = Convert.ToDateTime(sdr["CompletedDate"].ToString().Trim());
                            ActivityAction.Remarks = sdr["Remarks"].ToString().Trim();
                            ActivityAction.ExecutionEmployeeCode = (sdr["ExecutionEmployeeCode"].ToString().Trim());
                            ActivityAction.reviewEmployeeCode = sdr["reviewEmployeeCode"].ToString().Trim();
                            ActivityAction.HeadEmployeeCode = (sdr["HeadEmployeeCode"].ToString().Trim());
                            ActivityAction.ExecutionEmployeeName = (sdr["ExecutionEmployeeName"].ToString().Trim());
                            ActivityAction.reviewEmployeeName = sdr["ReviewEmployeeName"].ToString().Trim();
                            ActivityAction.HeadEmployeeName = (sdr["HeadEmployeeName"].ToString().Trim());
                            ActivityAction.UltimateEmployeeCode = sdr["UltimateEmployeeCode"].ToString().Trim();
                            ActivityAction.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                            ActivityAction.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                            ActivityAction.FrequencyDays = sdr["FrequencyDays"].ToString().Trim();
                            ActionList.Add(ActivityAction);
                        }
                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ActivityActionMsg ActivityAction = new ActivityActionMsg();
                    ActivityAction.ActionResult = Result;
                    ActionList.Add(ActivityAction);

                }

            }

            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActionList;
        }
    }
    public List<DocumentTypeActionMsg> ActionDocumentTypeInsertUpdatesp(DocumentTypeActionMsg docAction, EmployeeMasterMsg Emp)//ActivityForCompanyMasterMsg ActivityCom
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int ActivityDocId = 0;
        List<DocumentTypeActionMsg> ActiondocList = new List<DocumentTypeActionMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (docAction.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityActionDocumentInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", docAction.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ActivityActionId", docAction.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityActionDocId", docAction.ActivityActionDocId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeId", docAction.DocumentTypeId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeName", docAction.DocumentTypeName);
                    Connection.cmd.Parameters.AddWithValue("@DocumentName", docAction.DocumentName);
                    Connection.cmd.Parameters.AddWithValue("@StoragePath", docAction.StoragePath);
                    Connection.cmd.Parameters.AddWithValue("@Remarks", docAction.Remarks);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", docAction.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.Parameters.Add("@ActivityDocId", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    ActivityDocId =Convert.ToInt32(Connection.cmd.Parameters["@ActivityDocId"].Value.ToString().Trim());
                }
                if (Result == "0")
                {

                    Connection.cmd.CommandText = "MasActivityActionDocSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActivityActionId", docAction.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            DocumentTypeActionMsg ActiomDocMsg = new DocumentTypeActionMsg();
                            ActiomDocMsg.DocuActionResult = Result;
                            ActiomDocMsg.ActivityDocId = ActivityDocId;
                            ActiomDocMsg.ActivityActionId = Convert.ToInt64(sdr["ActivityActionId"].ToString().Trim());
                            ActiomDocMsg.ActivityActionDocId = Convert.ToInt64(sdr["ActivityActionDocId"].ToString().Trim());
                            ActiomDocMsg.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeID"].ToString().Trim());
                            ActiomDocMsg.DocumentType = sdr["DocumentTypeName"].ToString().Trim();
                            ActiomDocMsg.DocumentName = sdr["DocumentName"].ToString().Trim();
                            ActiomDocMsg.StoragePath = sdr["StoragePath"].ToString().Trim();
                            ActiomDocMsg.Remarks = sdr["Remarks"].ToString().Trim();
                            ActiondocList.Add(ActiomDocMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    DocumentTypeActionMsg DocActionMsg = new DocumentTypeActionMsg();
                    DocActionMsg.DocuActionResult = Result;
                    ActiondocList.Add(DocActionMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActiondocList;
        }
    }
    public List<NonComplianceTaskMasterMsg> MasNonComplianceTaskInsertUpdateandDelete(NonComplianceTaskMasterMsg NonCompTask, ActivityForCompanyMasterMsg ActivityComp)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<NonComplianceTaskMasterMsg> NonCompList = new List<NonComplianceTaskMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                //if (NonCompTask.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasactivityNonComplianceTaskMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();

                    Connection.cmd.Parameters.AddWithValue("@WNCTId", NonCompTask.NonComplianceTaskId);
                    // Connection.cmd.Parameters.AddWithValue("@ActivityId", NonCompTask.ActivityId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", NonCompTask.ActivityForCompanyId);
                    Connection.cmd.Parameters.AddWithValue("@ComplianceTaskName", NonCompTask.ComplianceTaskName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", NonCompTask.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", NonCompTask.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Connection.cmd.Parameters["@Result"].Value.ToString().Trim();
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityNonComplianceTaskMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", NonCompTask.ActivityForCompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            NonComplianceTaskMasterMsg NonCmpTask = new NonComplianceTaskMasterMsg();
                            NonCmpTask.NonComplianceTaskId = Convert.ToInt32(sdr["NonComplianceTaskId"].ToString().Trim());
                            NonCmpTask.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                            NonCmpTask.ComplianceTaskName = sdr["ComplianceTaskName"].ToString().Trim();
                            NonCmpTask.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());                         
                            NonCmpTask.NonCompResult = Result;
                            NonCompList.Add(NonCmpTask);

                        }
                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    NonComplianceTaskMasterMsg NonCompMsg = new NonComplianceTaskMasterMsg();
                    NonCompMsg.NonCompResult = Result;
                    NonCompList.Add(NonCompMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return NonCompList;
        }
    }
    public List<NonComplianceTaskMasterMsg> MasNonComplianceMasterSelect(NonComplianceTaskMasterMsg NonCompTask, ActivityForCompanyMasterMsg ActivityComp)
    {
        List<NonComplianceTaskMasterMsg> NonCompList = new List<NonComplianceTaskMasterMsg>();
        using (Connection.con)
        {
            try
            {

                Connection.cmd.CommandText = "MasActivityNonComplianceTaskMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityComp.CompanyActivityId);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        NonComplianceTaskMasterMsg NonCompMsg = new NonComplianceTaskMasterMsg();
                        NonCompMsg.NonComplianceTaskId = Convert.ToInt32(sdr["NonComplianceTaskId"].ToString().Trim());
                        NonCompMsg.ComplianceTaskName = sdr["ComplianceTaskName"].ToString().Trim();
                        NonCompMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        NonCompList.Add(NonCompMsg);
                    }
                }
            }

            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return NonCompList;
        }
    }
    //To insert,update,delete and select State Master
    public List<StateMasterMsg> MasStateInsertUpdateandDelete(StateMasterMsg State)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<StateMasterMsg> StateList = new List<StateMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasStateMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", State.Flag);
                    Connection.cmd.Parameters.AddWithValue("@StateId", State.StateId);
                    Connection.cmd.Parameters.AddWithValue("@StateName", State.StateName);
                    Connection.cmd.Parameters.AddWithValue("@StateShortName", State.StateShortName);
                    //Connection.cmd.Parameters.AddWithValue("@IsActive", DocType.IsActive);
                    //Connection.cmd.Parameters.AddWithValue("@CreatedBy", DocType.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasStateMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            StateMasterMsg StateMsg = new StateMasterMsg();
                            StateMsg.StateResult = Result;
                            StateMsg.StateId = Convert.ToInt32(sdr["StateId"].ToString().Trim());
                            StateMsg.StateName = sdr["StateName"].ToString().Trim();
                            StateMsg.StateShortName = sdr["StateShortName"].ToString().Trim();
                            StateList.Add(StateMsg);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    StateMasterMsg StateMsg = new StateMasterMsg();
                    StateMsg.StateResult = Result;
                    StateList.Add(StateMsg);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return StateList;
        }
    }
    //To insert,update,delete and select Acts Master
    public List<ActMasterMsg> MasActsInsertUpdateandDelete(ActMasterMsg Acts)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ActMasterMsg> ActList = new List<ActMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Acts.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Acts.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WAId", Acts.ActId);
                    Connection.cmd.Parameters.AddWithValue("@ActDtlId", Acts.ActDtlId);
                    Connection.cmd.Parameters.AddWithValue("@ActName", Acts.ActName);
                    Connection.cmd.Parameters.AddWithValue("@ClassificationAct", Acts.ClassificationAct);
                    Connection.cmd.Parameters.AddWithValue("@Chapter", Acts.Chapter);
                    Connection.cmd.Parameters.AddWithValue("@Head", Acts.Head);
                    Connection.cmd.Parameters.AddWithValue("@Section", Acts.Section);
                    Connection.cmd.Parameters.AddWithValue("@ActRule", Acts.ActRule);
                    Connection.cmd.Parameters.AddWithValue("@Description", Acts.Description);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Acts.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Acts.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                   Connection.cmd.Parameters.AddWithValue("@ActId", Acts.ActId);

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ActMasterMsg Act = new ActMasterMsg();
                            Act.ActResult = Result;
                            Act.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                            Act.ActDtlId = Convert.ToInt32(sdr["ActDtlId"].ToString().Trim());
                            Act.ActName = sdr["ActName"].ToString().Trim();
                            Act.ClassificationAct = sdr["ClassificationAct"].ToString().Trim();
                            Act.Chapter = sdr["Chapter"].ToString().Trim();
                            Act.Head = sdr["Head"].ToString().Trim();
                            Act.Section = sdr["Section"].ToString().Trim();
                            Act.ActRule = sdr["ActRule"].ToString().Trim();
                            Act.Description = sdr["Description"].ToString().Trim();
                            Act.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Act.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Act.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Act.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Act.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            ActList.Add(Act);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ActMasterMsg Act = new ActMasterMsg();
                    Act.ActResult = Result;
                    ActList.Add(Act);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActList;
        }
    }
    //To insert,update,delete and select Activities Master
    public List<ActivityMasterMsg> MasActivitiesInsertUpdateandDelete(ActivityMasterMsg Activities)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int LastDocId=0;
        int LastActivityId = 0;
        List<ActivityMasterMsg> ActivityList = new List<ActivityMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Activities.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                   Connection.cmd.Parameters.AddWithValue("@Flag", Activities.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WAVId", Activities.ActivityId);
                    Connection.cmd.Parameters.AddWithValue("@ActDtlId", Activities.ActDtlId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityName", Activities.ActivityName);
                    //010812 One Form For One Activity-- as discused in RDC on 300712--Begin
                    Connection.cmd.Parameters.AddWithValue("@DocumentFlag", Activities.DocumentFlag);
                    Connection.cmd.Parameters.AddWithValue("@ActivityDocumentId", Activities.ActivityDocumentId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentName", Activities.DocumentName);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeId", Activities.DocumentTypeId);
                    Connection.cmd.Parameters.AddWithValue("@AccessPath", Activities.AccessPath);
                    //010812 One Form For One Activity-- as discused in RDC on 300712--Ends
                    Connection.cmd.Parameters.AddWithValue("@DepartmentId", Activities.DepartmentId);
                    Connection.cmd.Parameters.AddWithValue("@CategoryId", Activities.CategoryId);
                    Connection.cmd.Parameters.AddWithValue("@SeverityId", Activities.SeverityId);
                    Connection.cmd.Parameters.AddWithValue("@IsStateSpecific", Activities.IsStateSpecific);
                    Connection.cmd.Parameters.AddWithValue("@IsLocationSpecific", Activities.IsLocationSpecific);
                    Connection.cmd.Parameters.AddWithValue("@IsRegularActivity", Activities.IsRegularActivity);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Activities.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@NonComplianceTaskAvailableFlag", Activities.NonComplianceTaskAvailableFlag);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Activities.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.Parameters.Add("@LastDocumentId", SqlDbType.Int, 0).Direction = ParameterDirection.Output;
                    Connection.cmd.Parameters.Add("@LastActivityId", SqlDbType.Int, 0).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    LastDocId = Convert.ToInt32(Connection.cmd.Parameters["@LastDocumentId"].Value.ToString().Trim());
                    LastActivityId = Convert.ToInt32(Connection.cmd.Parameters["@LastActivityId"].Value.ToString().Trim());

                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActId", Activities.ActId);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Activities.EmployeeCode);         
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ActivityMasterMsg Activity = new ActivityMasterMsg();
                            Activity.ActivitiesResult = Result;
                            Activity.LastDocumentId = LastDocId;
                            Activity.LastActivityId = LastActivityId;
                            Activity.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                            Activity.ActivityName = sdr["ActivityName"].ToString().Trim();
                            //010812 One Form For One Activity-- as discused in RDC on 300712--Begin
                            Activity.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                            Activity.ActivityDocumentId = Convert.ToInt32(sdr["ActivityDocumentId"].ToString().Trim());
                            Activity.DocumentName = sdr["DocumentName"].ToString().Trim();
                            Activity.AccessPath = sdr["AccessPath"].ToString().Trim();
                            //010812 One Form For One Activity-- as discused in RDC on 300712--Ends
                            Activity.ActId = Convert.ToInt32(sdr["ActId"].ToString().Trim());
                            Activity.ActName = sdr["ActName"].ToString().Trim();
                            Activity.ActDtlId = Convert.ToInt32(sdr["ActDtlId"].ToString().Trim());
                            Activity.Chapter = sdr["Chapter"].ToString().Trim();
                            Activity.Head = sdr["Head"].ToString().Trim();
                            Activity.Section = sdr["Section"].ToString().Trim();
                            Activity.ActRule = sdr["ActRule"].ToString().Trim();
                            Activity.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            Activity.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                            Activity.CategoryId = Convert.ToInt32(sdr["CategoryId"].ToString().Trim());
                            Activity.CategoryName = sdr["CategoryName"].ToString().Trim();
                            Activity.SeverityId = Convert.ToInt32(sdr["SeverityId"].ToString().Trim());
                            Activity.SeverityName = sdr["SeverityName"].ToString().Trim();
                            Activity.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Activity.IsStateSpecific = Convert.ToBoolean(sdr["IsStateSpecific"].ToString().Trim());
                            Activity.IsLocationSpecific = Convert.ToBoolean(sdr["IsLocationSpecific"].ToString().Trim());
                            Activity.IsRegularActivity = Convert.ToBoolean(sdr["IsRegularActivity"].ToString().Trim());
                            Activity.NonComplianceTaskAvailableFlag = Convert.ToChar(sdr["NonComplianceTaskAvailableFlag"].ToString().Trim());
                            Activity.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            Activity.CreatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString().Trim());
                            Activity.ModifiedBy = sdr["ModifiedBy"].ToString().Trim();
                            Activity.ModifiedDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString().Trim());
                            Activity.Administrator = sdr["Administrator"].ToString().Trim();//added only Administrator can deactivate an activity 030913 abinayaa
                            Activity.ActivityDocId = Convert.ToInt32(sdr["ActivityDocId"].ToString().Trim());
                            Activity.NonComplianceCompanyId = Convert.ToInt32(sdr["NonComplianceCompanyId"].ToString().Trim());
                            ActivityList.Add(Activity);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ActivityMasterMsg Activity = new ActivityMasterMsg();
                    Activity.ActivitiesResult = Result;
                    ActivityList.Add(Activity);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActivityList;
        }
    }
    public List<ActivityDocumentTypeMasterMsg> MasActivityDocumentTypeInsertUpdateandDelete(ActivityDocumentTypeMasterMsg ActivityDocType, ActivityForCompanyMasterMsg ActivityComp)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ActivityDocumentTypeMasterMsg> ActivityDocList = new List<ActivityDocumentTypeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                // if (DocType.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityDocumentTypeMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    // Connection.cmd.Parameters.AddWithValue("@Flag", DocType.Flag);
                    Connection.cmd.Parameters.AddWithValue("@WADTId", ActivityDocType.ActDocTypeId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityDocType.ActivityForCompanyId);
                    Connection.cmd.Parameters.AddWithValue("@DocumentTypeId", ActivityDocType.DocumentTypeId);
                    Connection.cmd.Parameters.AddWithValue("@ToBeMaintained", ActivityDocType.ToBeMaintained);
                    Connection.cmd.Parameters.AddWithValue("@ToBeSubmitted", ActivityDocType.ToBeSubmitted);
                    Connection.cmd.Parameters.AddWithValue("@MasterPDFName", ActivityDocType.MasterPDFName);
                    Connection.cmd.Parameters.AddWithValue("@AccessPath", ActivityDocType.AccessPath);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", ActivityDocType.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivityDocType.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityDocumentTypeMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityDocType.ActivityForCompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            ActivityDocumentTypeMasterMsg ActivityForDoc = new ActivityDocumentTypeMasterMsg();
                            ActivityForDoc.ActDocTypeId = Convert.ToInt64(sdr["ActDoctypeId"].ToString().Trim());
                            ActivityForDoc.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                            ActivityForDoc.ActivityName = sdr["ActivityName"].ToString().Trim();
                            ActivityForDoc.DocumentTypeId = Convert.ToInt32(sdr["DocumentTypeId"].ToString().Trim());
                            ActivityForDoc.DocumentTypeName = sdr["DocumentTypeName"].ToString().Trim();
                            ActivityForDoc.ToBeMaintained = sdr["ToBeMaintained"].ToString().Trim();
                            ActivityForDoc.ToBeSubmitted = sdr["ToBeSubmitted"].ToString().Trim();
                            ActivityForDoc.MasterPDFName = sdr["MasterPDFName"].ToString().Trim();
                            ActivityForDoc.AccessPath = sdr["AccessPath"].ToString().Trim();
                            ActivityForDoc.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            //ActivityForDoc.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            ActivityForDoc.ActivityDocResult = Result;
                            ActivityDocList.Add(ActivityForDoc);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    ActivityDocumentTypeMasterMsg ActivityDoc = new ActivityDocumentTypeMasterMsg();
                    ActivityDoc.ActivityDocResult = Result;
                    ActivityDocList.Add(ActivityDoc);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActivityDocList;
        }
    }
    //To insert,update,delete and select RoleProgram
    public List<RoleProgramsMsg> AdmRoleProgramsInsertUpdateandDelete(RoleProgramsMsg roleProgramsMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int NewCount = 1;
        List<RoleProgramsMsg> RoleProgramsList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                if (roleProgramsMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmProgramsToRoleInsertSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", roleProgramsMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@RoleId", roleProgramsMsg.RoleId);
                    Connection.cmd.Parameters.AddWithValue("@RoleName", roleProgramsMsg.RoleName);
                    Connection.cmd.Parameters.AddWithValue("@ProgramId", roleProgramsMsg.ProgramId);
                    Connection.cmd.Parameters.AddWithValue("@CanAccess", roleProgramsMsg.CanAccess);
                    Connection.cmd.Parameters.AddWithValue("@CanCreate", roleProgramsMsg.CanCreate);
                    Connection.cmd.Parameters.AddWithValue("@CanEdit", roleProgramsMsg.CanEdit);
                    Connection.cmd.Parameters.AddWithValue("@CanDelete", roleProgramsMsg.CanDelete);
                    Connection.cmd.Parameters.AddWithValue("@CanPrint", roleProgramsMsg.CanPrint);
                    Connection.cmd.Parameters.AddWithValue("@UserCode", roleProgramsMsg.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                    Connection.cmd.Parameters.Add("@Return", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());

                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "AdmRoleProgramSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@RoleId", roleProgramsMsg.RoleId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RoleProgramsMsg rolePrograms = new RoleProgramsMsg();
                            rolePrograms.Result = Result;
                            rolePrograms.ProgramId = Convert.ToInt32(sdr["ProgramId"].ToString().Trim());
                            rolePrograms.ProgramName = sdr["ProgramName"].ToString().Trim();
                            rolePrograms.CanAccess = Convert.ToBoolean(sdr["CanAccess"].ToString().Trim());
                            rolePrograms.CanCreate = Convert.ToBoolean(sdr["CanCreate"].ToString().Trim());
                            rolePrograms.CanDelete = Convert.ToBoolean(sdr["CanDelete"].ToString().Trim());
                            rolePrograms.CanEdit = Convert.ToBoolean(sdr["CanEdit"].ToString().Trim());
                            rolePrograms.CanPrint = Convert.ToBoolean(sdr["CanPrint"].ToString().Trim());
                            RoleProgramsList.Add(rolePrograms);
                        }
                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    RoleProgramsMsg roles = new RoleProgramsMsg();
                    roles.Result = Result;
                    RoleProgramsList.Add(roles);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return RoleProgramsList;
        }
    }
    public List<RoleProgramsMsg> AdmRoleProgramsInsertUpdateandDelete(List<RoleProgramsMsg> RoleProgramsList)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        int NewCount = 0;
        int RoleId = 0;
        bool IsSuccess = true;
        List<RoleProgramsMsg> rolePromgramMsgList = new List<RoleProgramsMsg>();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmProgramsToRoleInsertSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                foreach (RoleProgramsMsg roleProgramsMsg in RoleProgramsList)
                {
                    if (IsSuccess == true)
                    {
                        Connection.cmd.Parameters.Clear();
                        Connection.cmd.Parameters.AddWithValue("@Flag", roleProgramsMsg.Flag);
                        Connection.cmd.Parameters.AddWithValue("@RoleId", RoleId);
                        Connection.cmd.Parameters.AddWithValue("@RoleName", roleProgramsMsg.RoleName);
                        Connection.cmd.Parameters.AddWithValue("@ProgramId", roleProgramsMsg.ProgramId);
                        Connection.cmd.Parameters.AddWithValue("@CanAccess", roleProgramsMsg.CanAccess);
                        Connection.cmd.Parameters.AddWithValue("@CanCreate", roleProgramsMsg.CanCreate);
                        Connection.cmd.Parameters.AddWithValue("@CanEdit", roleProgramsMsg.CanEdit);
                        Connection.cmd.Parameters.AddWithValue("@CanDelete", roleProgramsMsg.CanDelete);
                        Connection.cmd.Parameters.AddWithValue("@CanPrint", roleProgramsMsg.CanPrint);
                        Connection.cmd.Parameters.AddWithValue("@UserCode", roleProgramsMsg.CreatedBy);
                        if (roleProgramsMsg.RoleId == 0 && NewCount == 0)
                        {
                            Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        }
                        else
                        {
                            NewCount = 1;
                            Connection.cmd.Parameters.AddWithValue("@NewCount", NewCount);
                        }
                        Connection.cmd.Parameters.Add("@Return", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                        Connection.cmd.ExecuteNonQuery();
                        if (RoleId == 0 && NewCount == 0)
                        {
                            if (Isvalid.IsIntegerAndPositive(Connection.cmd.Parameters["@Return"].Value.ToString().Trim()))
                            {
                                RoleId = Convert.ToInt32(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                                NewCount = 1;
                            }
                            else
                            {
                                Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                                IsSuccess = false;
                            }
                        }
                        else if (RoleId > 0)
                        {
                            Result = Convert.ToString(Connection.cmd.Parameters["@Return"].Value.ToString().Trim());
                            if (Result != "0")
                            {
                                IsSuccess = false;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (IsSuccess)
                {
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                    rolePromgramMsgList = AdmRoleProgramsSelect(RoleId);
                }
                else
                {
                    RoleProgramsMsg roles = new RoleProgramsMsg();
                    roles.Result = Result;
                    rolePromgramMsgList.Add(roles);
                    transaction.Rollback();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                transaction.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return rolePromgramMsgList;
    }
    //To insert,update,delete and select AivityForCompany Master
    public List<ActivityForCompanyMasterMsg> MasActivityForCompanyMasterInsertandUpdate(ActivityForCompanyMasterMsg ActivityForCmpny, EmployeeMasterMsg emp)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "MasActivityForCompanyMasterInsertandUpdateSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@WAFCId", ActivityForCmpny.CompanyActivityId);
                Connection.cmd.Parameters.AddWithValue("@ActivityId", ActivityForCmpny.ActivityId);
                Connection.cmd.Parameters.AddWithValue("@CompanyCode", ActivityForCmpny.CompanyCode);
                Connection.cmd.Parameters.AddWithValue("@FrequencyId", ActivityForCmpny.FrequencyId);
                Connection.cmd.Parameters.AddWithValue("@FrequencyRemarks", ActivityForCmpny.FrqRemarks);//added by Abinayaa 161112
                Connection.cmd.Parameters.AddWithValue("@DueMonth", ActivityForCmpny.DueMonth);
                Connection.cmd.Parameters.AddWithValue("@DueDate", ActivityForCmpny.DueDate);
                Connection.cmd.Parameters.AddWithValue("@DueDay", ActivityForCmpny.DueDay);
                Connection.cmd.Parameters.AddWithValue("@TriggerMonth", ActivityForCmpny.TriggerMonth);
                Connection.cmd.Parameters.AddWithValue("@TriggerDate", ActivityForCmpny.TriggerDate);
                Connection.cmd.Parameters.AddWithValue("@TriggerDay", ActivityForCmpny.TriggerDay);
                Connection.cmd.Parameters.AddWithValue("@ExecutionEmployeeCode", ActivityForCmpny.ExecutionEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@ReviewEmployeeCode", ActivityForCmpny.ReviewEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@HeadEmployeeCode", ActivityForCmpny.HeadEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@UltimateEmployeeCode", ActivityForCmpny.UltimateEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@IsActive", ActivityForCmpny.IsActive);
                Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivityForCmpny.CreatedBy);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityForCompanyMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", emp.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@ActId", emp.ActId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ActivityForCompanyMasterMsg ActivityForCompany = new ActivityForCompanyMasterMsg();
                            ActivityForCompany.CompanyActivityId = Convert.ToInt32(sdr["CompanyActivityId"].ToString().Trim());
                            ActivityForCompany.ActivityId = Convert.ToInt32(sdr["ActivityId"].ToString().Trim());
                            ActivityForCompany.ActivityName = sdr["ActivityName"].ToString().Trim();
                            ActivityForCompany.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            ActivityForCompany.CompanyName = sdr["CompanyName"].ToString().Trim();
                            ActivityForCompany.FrequencyId = Convert.ToInt32(sdr["FrequencyId"].ToString().Trim());
                            ActivityForCompany.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                            ActivityForCompany.FrqRemarks = sdr["FrequencyRemarks"].ToString().Trim();//added by Abinayaa 161112
                            ActivityForCompany.DueMonth = Convert.ToInt32(sdr["DueMonth"].ToString().Trim());
                            ActivityForCompany.DueDate = Convert.ToInt32(sdr["DueDate"].ToString().Trim());
                            ActivityForCompany.DueDay = sdr["DueDay"].ToString().Trim();
                            ActivityForCompany.TriggerMonth = Convert.ToInt32(sdr["TriggerMonth"].ToString().Trim());
                            ActivityForCompany.TriggerDate = Convert.ToInt32(sdr["TriggerDate"].ToString().Trim());
                            ActivityForCompany.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                            ActivityForCompany.ExecutionEmployeeCode = (sdr["ExecutionEmployeeCode"].ToString().Trim());
                            ActivityForCompany.Executioner = sdr["Executioner"].ToString().Trim();
                            ActivityForCompany.ReviewEmployeeCode = (sdr["ReviewEmployeeCode"].ToString().Trim());
                            ActivityForCompany.Reviewer = sdr["Reviewer"].ToString().Trim();
                            ActivityForCompany.HeadEmployeeCode = (sdr["HeadEmployeeCode"].ToString().Trim());
                            ActivityForCompany.HeadEmployeeName = sdr["HeadEmployeeName"].ToString().Trim();
                            ActivityForCompany.UltimateEmployeeCode = (sdr["UltimateEmployeeCode"].ToString().Trim());
                            ActivityForCompany.UltimateEmployeeName = sdr["UltimateEmployeeName"].ToString().Trim();
                            ActivityForCompany.ActivityOutsideStackFlag = Convert.ToChar(sdr["ActivityOutsideStackFlag"].ToString().Trim());
                            ActivityForCompany.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            ActivityForCmpny.NonComplianceTaskAvailableFlag = Convert.ToChar(sdr["NonComplianceTaskAvailableFlag"].ToString().Trim());//Added by Abinayaa 261012
                            ActivityForCompany.ActivityCompanyResult = Result;
                            ActivityForCmpnyList.Add(ActivityForCompany);
                        }
                    }
                }
                else
                {
                    ActivityForCompanyMasterMsg ActivityForCompany = new ActivityForCompanyMasterMsg();
                    ActivityForCmpny.ActivityCompanyResult = Result;
                    ActivityForCmpnyList.Add(ActivityForCompany);
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ActivityForCmpnyList;
        }
    }
    public TranNonComplianceMsg MasActivityactionRootCauseInsertUpdateandSelect(TranNonComplianceMsg tranNonComplianceMsg)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        TranNonComplianceMsg TranNonCompliance = new TranNonComplianceMsg();
        using (Connection.con)
        {
            try
            {
                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;

                if (tranNonComplianceMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityactionRootCauseInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@WActivityActionId", tranNonComplianceMsg.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@WActivityActionRootCauseId", tranNonComplianceMsg.ActivityActionRootCauseId);
                    Connection.cmd.Parameters.AddWithValue("@ApprovedBy", tranNonComplianceMsg.ApprovedBy);
                    Connection.cmd.Parameters.AddWithValue("@CorrectiveAction", tranNonComplianceMsg.CorrectiveAction);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", tranNonComplianceMsg.CreatedBy);
                    Connection.cmd.Parameters.AddWithValue("@Flag", tranNonComplianceMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@PreventiveAction", tranNonComplianceMsg.PreventiveAction);
                    Connection.cmd.Parameters.AddWithValue("@RootCause", tranNonComplianceMsg.RootCause);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasActivityactionRootCauseSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActivityAction", tranNonComplianceMsg.ActivityActionId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            TranNonCompliance.NonComplianceResult = Result;
                            TranNonCompliance.ActivityActionId = Convert.ToInt32(sdr["ActivityActionId"].ToString().Trim());
                            TranNonCompliance.ActivityActionRootCauseId = Convert.ToInt32(sdr["ActivityActionRootCauseId"].ToString().Trim());
                            TranNonCompliance.ApprovedBy = sdr["ApprovedBy"].ToString().Trim();
                            TranNonCompliance.CorrectiveAction = sdr["CorrectiveAction"].ToString().Trim();
                            TranNonCompliance.PreventiveAction = sdr["PreventiveAction"].ToString().Trim();
                            TranNonCompliance.RootCause = sdr["RootCause"].ToString().Trim();
                        }
                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    TranNonCompliance.NonComplianceResult = Result;
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
            return TranNonCompliance;
        }
    }
    public List<LocationDepartmentMasterMsg> MasLocationDeptInsertUpdateand(LocationDepartmentMasterMsg LocationDept)
    {
        SqlTransaction transaction = null;
        string Result = "0";
       
        List<LocationDepartmentMasterMsg> LocationList = new List<LocationDepartmentMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (LocationDept.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasLocationDepartmentMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", LocationDept.Flag);
                    Connection.cmd.Parameters.AddWithValue("@LocationDeptId", LocationDept.LocationDeptId);
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", LocationDept.CompanyCode);
                    Connection.cmd.Parameters.AddWithValue("@DepartmentId", LocationDept.DepartmentId);                   
                    Connection.cmd.Parameters.AddWithValue("@ExecutionEmployeeCode", LocationDept.ExecutionEmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@reviewEmployeeCode", LocationDept.reviewEmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@HeadEmployeeCode", LocationDept.HeadEmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@UltimateEmployeeCode", LocationDept.UltimateEmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@ResponsibleGroupName", LocationDept.ResponsibleGrpName);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;                   
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                    
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasLocationDepartmentMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LocationDept.EmployeeCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
                            Location.LocationDeptResult = Result;
                            Location.LocationDeptId = Convert.ToInt32(sdr["LocationDeptId"].ToString().Trim());
                            Location.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            Location.CompanyName = sdr["CompanyName"].ToString().Trim();
                            Location.DepartmentId = Convert.ToInt32(sdr["DepartmentId"].ToString().Trim());
                            Location.DepartmentName = sdr["DepartmentName"].ToString().Trim();
                            Location.ExecutionEmployeeCode = sdr["ExecutionEmployeeCode"].ToString().Trim();
                            Location.ExecutionEmployeeName = sdr["ExecutionEmployeeName"].ToString().Trim();
                            Location.reviewEmployeeCode = sdr["reviewEmployeeCode"].ToString().Trim();
                            Location.reviewEmployeeName = sdr["reviewEmployeeName"].ToString().Trim();
                            Location.HeadEmployeeCode = sdr["HeadEmployeeCode"].ToString().Trim();
                            Location.HeadEmployeeName = sdr["HeadEmployeeName"].ToString().Trim();
                            Location.UltimateEmployeeCode = sdr["UltimateEmployeeCode"].ToString().Trim();
                            Location.UltimateEmployeeName = sdr["UltimateEmployeeName"].ToString().Trim();
                            Location.ResponsibleGrpName = sdr["ResponsibleGroupName"].ToString().Trim();
                            LocationList.Add(Location);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    LocationDepartmentMasterMsg Location = new LocationDepartmentMasterMsg();
                    Location.LocationDeptResult = Result;
                    LocationList.Add(Location);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return LocationList;
        }
    }
    public List<LocationinStateMasterMsg> MasLocationinStateInsertUpdateandDelete(LocationinStateMasterMsg LocState)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<LocationinStateMasterMsg> LocstateList = new List<LocationinStateMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (LocState.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasLocationinStateInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", LocState.Flag);
                    Connection.cmd.Parameters.AddWithValue("@LocationinStateId", LocState.LocationinStateId);
                    Connection.cmd.Parameters.AddWithValue("@LocationinState", LocState.LocationinState);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", LocState.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", LocState.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasLocationinStateSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            LocationinStateMasterMsg LocinState = new LocationinStateMasterMsg();
                            LocinState.LocationinStateResult = Result;
                            LocinState.LocationinStateId = Convert.ToInt32(sdr["LocationinStateId"].ToString().Trim());
                            LocinState.LocationinState = sdr["LocationinState"].ToString().Trim();
                            LocinState.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            LocinState.CreatedBy = sdr["CreatedBy"].ToString().Trim();
                            LocstateList.Add(LocinState);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    LocationinStateMasterMsg LocinState = new LocationinStateMasterMsg();
                    LocinState.LocationinStateResult = Result;
                    LocstateList.Add(LocinState);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return LocstateList;
        }
    }
    public List<LocationTypeMasterMsg> MasLocationTypeInsertUpdateandDelete(LocationTypeMasterMsg Locationtype)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<LocationTypeMasterMsg> LocationTypeList = new List<LocationTypeMasterMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (Locationtype.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasLocationtypeMasterInsertandUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", Locationtype.Flag);
                    Connection.cmd.Parameters.AddWithValue("@LocationTypeId", Locationtype.LocationTypeId);
                    Connection.cmd.Parameters.AddWithValue("@LocationTypeName", Locationtype.LocationTypeName);
                    Connection.cmd.Parameters.AddWithValue("@LocationTypeShortName", Locationtype.LocationTypeShortName);
                    Connection.cmd.Parameters.AddWithValue("@IsActive", Locationtype.IsActive);
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", Locationtype.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasLocationtypeMasterSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            LocationTypeMasterMsg Location = new LocationTypeMasterMsg();
                            Location.LocationTypeResult = Result;
                            Location.LocationTypeId = Convert.ToInt32(sdr["LocationTypeId"].ToString().Trim());
                            Location.LocationTypeName = sdr["LocationTypeName"].ToString().Trim();
                            Location.LocationTypeShortName = sdr["LocationTypeShortName"].ToString().Trim();
                            Location.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            Location.CreatedBy = sdr["CreatedBy"].ToString().Trim();                           
                            LocationTypeList.Add(Location);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    LocationTypeMasterMsg Location = new LocationTypeMasterMsg();
                    Location.LocationTypeResult = Result;
                    LocationTypeList.Add(Location);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return LocationTypeList;
        }
    }
    #endregion

    #region Login
    public LoginInfoMsg CheckLogin(LoginInfoMsg LoginInfo)
    {
        string Result = "0";
        LoginInfoMsg LoginInfoResult = new LoginInfoMsg();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmChkLoginSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                Connection.cmd.Parameters.AddWithValue("@Password", LoginInfo.Password);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                if (Result == "0" || Result == "1")
                {
                    Connection.cmd.CommandText = "AdmUserInfoSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                    Connection.cmd.Parameters.AddWithValue("@MachineIp", LoginInfo.MachineIP);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            LoginInfoResult.Result = Result;
                            LoginInfoResult.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            LoginInfoResult.CompanyName = sdr["CompanyShortName"].ToString().Trim();
                            LoginInfoResult.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                            LoginInfoResult.UserSessionId = Convert.ToInt32(sdr["UserSessionId"].ToString().Trim());
                        }
                    }
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                    //LoginInfoResult.EmployeeName = "Sathish";
                    //LoginInfoResult.CompanyCode = "3000";
                    //LoginInfoResult.CompanyName = "RBL";
                    //LoginInfoResult.Result = "0";
                }
                else
                {
                    LoginInfoResult.Result = Result;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
            return LoginInfoResult;
        }
    }
    public void UpdateUserLogoffInfo(LoginInfoMsg LoginInfo)
    {
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "AdmUserLogOffInfoSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", LoginInfo.UserName);
                Connection.cmd.Parameters.AddWithValue("@UserSessionId", LoginInfo.UserSessionId);
                Connection.cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
        }
    }
    public List<ChangePasswordMsg> AdmChangePaswordUpdateSp(ChangePasswordMsg ChangePwd)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        List<ChangePasswordMsg> ChangePwdList = new List<ChangePasswordMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                //if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmChangePaswordUpdateSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@NewPassword", ChangePwd.NewPassword);
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", ChangePwd.EmployeeCode);
                    Connection.cmd.Parameters.AddWithValue("@OldPassword", ChangePwd.OldPassword);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                transaction.Commit();
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
                ChangePasswordMsg ChangePwdMsg = new ChangePasswordMsg();
                ChangePwdMsg.ChangePwdResult = Result;
                ChangePwdList.Add(ChangePwdMsg);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return ChangePwdList;
        }
    }
    public EmployeeMasterMsg GetForgotPassword(EmployeeMasterMsg Emp)
    {
        EmployeeMasterMsg employee = new EmployeeMasterMsg();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                //if (State.Flag != "R")
                {
                    Connection.cmd.CommandText = "AdmForgotPaswordSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();

                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employee.EmployeeResult = sdr["Result"].ToString().Trim();
                            employee.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            employee.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            employee.Password = sdr["Password"].ToString().Trim();
                            employee.EmailId = sdr["EmailId"].ToString().Trim();
                        }
                    }
                }

                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return employee;
        }
    }
    #endregion

    //#region Data
    //public DataTable CompanyMasterSelect()
    //{
    //    DataTable Company = new DataTable();
    //    Company.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
    //    Company.Columns.Add(new DataColumn("CompanyName", typeof(string)));
    //    Company.Columns.Add(new DataColumn("CompanyShortName", typeof(string))); 
    //    Company.Columns.Add(new DataColumn("CompanyFlag", typeof(string)));
    //    Company.Columns.Add(new DataColumn("ParentCompanyCode", typeof(string)));
    //    Company.Columns.Add(new DataColumn("ParentCompanyName", typeof(string)));

    //    //--1
    //    DataRow dr1=Company.NewRow();
    //    dr1["CompanyCode"] = "4001";
    //    dr1["CompanyName"] = "REVLAlandurPlant";
    //    dr1["CompanyShortName"] = "Al";
    //    dr1["CompanyFlag"] = "PL";
    //    dr1["ParentCompanyCode"] = "4000";
    //    dr1["ParentCompanyName"] = "REVLHO";
    //    Company.Rows.Add(dr1);

    //    //--2
    //    DataRow dr2=Company.NewRow();
    //    dr2["CompanyCode"] = "4000";
    //    dr2["CompanyName"] = "REVLHO";
    //    dr2["CompanyShortName"] = "HO";
    //    dr2["CompanyFlag"] = "HO";
    //    dr2["ParentCompanyCode"] = "";
    //    dr2["ParentCompanyName"] = "";
    //    Company.Rows.Add(dr2);

    //    //--3
    //    DataRow dr3=Company.NewRow();
    //    dr3["CompanyCode"] = "4002";
    //    dr3["CompanyName"] = "REVLPonneriPlant";
    //    dr3["CompanyShortName"] = "Po";
    //    dr3["CompanyFlag"] = "PL";
    //    dr3["ParentCompanyCode"] = "4000";
    //    dr3["ParentCompanyName"] = "REVLHO";
    //    Company.Rows.Add(dr3);

    //    return Company;

    //}

    ////public DataTable EmployeeMasterSelect()
    ////{
    ////    DataTable Employee = new DataTable();
    ////    Employee.Columns.Add(new DataColumn("EmployeeCode", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("EmailId", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("PassWord", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("CompanyName", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("ManagerCode", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("ManagerName", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("ActivityDesignation", typeof(string)));
    ////    Employee.Columns.Add(new DataColumn("IsActive", typeof(bool)));
    ////    Employee.Columns.Add(new DataColumn("RoleId", typeof(int)));
    ////    Employee.Columns.Add(new DataColumn("RoleName", typeof(string)));

    ////    //--1
    ////    DataRow dr1 = Employee.NewRow();
    ////    dr1["EmployeeCode"] = "102";
    ////    dr1["EmailId"] = "Sathish@ganini.com";
    ////    dr1["PassWord"] = "Sathish";
    ////    dr1["CompanyCode"] = "4001";
    ////    dr1["CompanyName"] = "REVLAlandurPlant";
    ////    dr1["ManagerCode"] = "101";
    ////    dr1["ManagerName"] = "SCS@ganini.com";
    ////    dr1["ActivityDesignation"] = "TL";
    ////    dr1["IsActive"] = "true";
    ////    dr1["RoleId"] = "2";
    ////    dr1["RoleName"] = "Executioner";
    ////    Employee.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = Employee.NewRow();
    ////    dr2["EmployeeCode"] = "103";
    ////    dr2["EmailId"] = "Abinayaa@ganini.com";
    ////    dr2["PassWord"] = "Abinayaa";
    ////    dr2["CompanyCode"] = "4002";
    ////    dr2["CompanyName"] = "REVLPonneriPlant";
    ////    dr2["ManagerCode"] = "102";
    ////    dr2["ManagerName"] = "Sathish@ganini.com";
    ////    dr2["ActivityDesignation"] = "Developer";
    ////    dr2["IsActive"] = "true";
    ////    dr2["RoleId"] = "3";
    ////    dr2["RoleName"] = "Reviewer";
    ////    Employee.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = Employee.NewRow();
    ////    dr3["EmployeeCode"] = "104";
    ////    dr3["EmailId"] = "Shoobana@ganini.com";
    ////    dr3["PassWord"] = "shoobana";
    ////    dr3["CompanyCode"] = "4002";
    ////    dr3["CompanyName"] = "REVLPonneriPlant";
    ////    dr3["ManagerCode"] = "102";
    ////    dr3["ManagerName"] = "Sathish@ganini.com";
    ////    dr3["ActivityDesignation"] = "developer";
    ////    dr3["IsActive"] = "true";
    ////    dr3["RoleId"] = "3";
    ////    dr3["RoleName"] = "Reviewer";
    ////    Employee.Rows.Add(dr3);

    ////    //--4
    ////    DataRow dr4 = Employee.NewRow();
    ////    dr4["EmployeeCode"] = "101";
    ////    dr4["EmailId"] = "SCS@ganini.com";
    ////    dr4["PassWord"] = "SCS";
    ////    dr4["CompanyCode"] = "4000";
    ////    dr4["CompanyName"] = "REVLHO";
    ////    dr4["ManagerCode"] = "";
    ////    dr4["ManagerName"] = "";
    ////    dr4["ActivityDesignation"] = "Manager";
    ////    dr4["IsActive"] = "true";
    ////    dr4["RoleId"] = "1";
    ////    dr4["RoleName"] = "Administrator";
    ////    Employee.Rows.Add(dr4);

    ////    return Employee;

    ////}

    ////public DataTable DepartmentMasterSelect()
    ////{
    ////    DataTable Dept = new DataTable();
    ////    Dept.Columns.Add(new DataColumn("DepartmentId", typeof(string)));
    ////    Dept.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
    ////    Dept.Columns.Add(new DataColumn("DepartmentShortName", typeof(string)));

    ////    //1
    ////    DataRow dr1 = Dept.NewRow();
    ////    dr1["DepartmentId"] = "2";
    ////    dr1["DepartmentName"] = "Finance";
    ////    dr1["DepartmentShortName"] = "FIN";
    ////    Dept.Rows.Add(dr1);
    ////    //2
    ////    DataRow dr2 = Dept.NewRow();
    ////    dr2["DepartmentId"] = "1";
    ////    dr2["DepartmentName"] = "HumanResource";
    ////    dr2["DepartmentShortName"] = "HR ";
    ////    Dept.Rows.Add(dr2);

    ////    //3
    ////    DataRow dr3 = Dept.NewRow();
    ////    dr3["DepartmentId"] = "3";
    ////    dr3["DepartmentName"] = "PLE";
    ////    dr3["DepartmentShortName"] = "PLE";
    ////    Dept.Rows.Add(dr3);

    ////    return Dept;
    ////}

    ////public DataTable CategoryMasterSelect()
    ////{
    ////    DataTable Ctgry = new DataTable();
    ////    Ctgry.Columns.Add(new DataColumn("CategoryId", typeof(int)));
    ////    Ctgry.Columns.Add(new DataColumn("CategoryName", typeof(string)));

    ////    //1
    ////    DataRow dr1 = Ctgry.NewRow();
    ////    dr1["CategoryId"] = "1";
    ////    dr1["CategoryName"] = "ER";
    ////    Ctgry.Rows.Add(dr1);

    ////    //2
    ////    DataRow dr2 = Ctgry.NewRow();
    ////    dr2["CategoryId"] = "4";
    ////    dr2["CategoryName"] = "S";
    ////    Ctgry.Rows.Add(dr2);

    ////    //3
    ////    DataRow dr3 = Ctgry.NewRow();
    ////    dr3["CategoryId"] = "3";
    ////    dr3["CategoryName"] = "SF";
    ////    Ctgry.Rows.Add(dr3);

    ////    //4
    ////    DataRow dr4 = Ctgry.NewRow();
    ////    dr4["CategoryId"] = "2";
    ////    dr4["CategoryName"] = "SS";
    ////    Ctgry.Rows.Add(dr4);

    ////    return Ctgry;
    ////}

    ////public DataTable FrequencyMasterSelect()
    ////{
    ////    DataTable Frequency = new DataTable();
    ////    Frequency.Columns.Add(new DataColumn("FrequencyId", typeof(int)));
    ////    Frequency.Columns.Add(new DataColumn("FrequencyName", typeof(string)));
    ////    Frequency.Columns.Add(new DataColumn("FrequencyShortName", typeof(string)));
    ////    Frequency.Columns.Add(new DataColumn("FrequencyDays", typeof(int)));

    ////    //--1
    ////    DataRow dr2 = Frequency.NewRow();
    ////    dr2["FrequencyId"] = "1";
    ////    dr2["FrequencyName"] = "Weekly";
    ////    dr2["FrequencyShortName"] = "Wk";
    ////    dr2["FrequencyDays"] = "7";
    ////    Frequency.Rows.Add(dr2);

    ////    // --2
    ////    DataRow dr3 = Frequency.NewRow();
    ////    dr3["FrequencyId"] = "2";
    ////    dr3["FrequencyName"] = "Monthly";
    ////    dr3["FrequencyShortName"] = "Mn";
    ////    dr3["FrequencyDays"] = "30";
    ////    Frequency.Rows.Add(dr3);

    ////    // --3
    ////    DataRow dr1 = Frequency.NewRow();
    ////    dr1["FrequencyId"] = "3";
    ////    dr1["FrequencyName"] = "Quarterly";
    ////    dr1["FrequencyShortName"] = "Qt";
    ////    dr1["FrequencyDays"] = "90";
    ////    Frequency.Rows.Add(dr1);

    ////    //--4
    ////    DataRow dr4 = Frequency.NewRow();
    ////    dr4["FrequencyId"] = "4";
    ////    dr4["FrequencyName"] = "Half Yearly";
    ////    dr4["FrequencyShortName"] = "HY";
    ////    dr4["FrequencyDays"] = "180";
    ////    Frequency.Rows.Add(dr4);

    ////    return Frequency;
    ////}

    ////public DataTable SeverityMasterSelect()
    ////{
    ////    DataTable Severity = new DataTable();
    ////    Severity.Columns.Add(new DataColumn("SeverityId", typeof(int)));
    ////    Severity.Columns.Add(new DataColumn("SeverityName", typeof(string)));
    ////    Severity.Columns.Add(new DataColumn("SeverityShortName", typeof(string)));
    ////    Severity.Columns.Add(new DataColumn("Remarks", typeof(string)));

    ////    //--1
    ////    DataRow dr1 = Severity.NewRow();
    ////    dr1["SeverityId"] = "1";
    ////    dr1["SeverityName"] = "DOCUMENT / RECORD / REGISTERS";
    ////    dr1["SeverityShortName"] = "A ";
    ////    dr1["Remarks"] = "will lead to show cause notice";
    ////    Severity.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = Severity.NewRow();
    ////    dr2["SeverityId"] = "2";
    ////    dr2["SeverityName"] = "periodical Updation";
    ////    dr2["SeverityShortName"] = "B ";
    ////    dr2["Remarks"] = "lead to notice to the Factory ";
    ////    Severity.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = Severity.NewRow();
    ////    dr3["SeverityId"] = "3";
    ////    dr3["SeverityName"] = "Records/Registers ";
    ////    dr3["SeverityShortName"] = "C ";
    ////    dr3["Remarks"] = "ead to warning by the authorit";
    ////    Severity.Rows.Add(dr3);

    ////    return Severity;
    ////}

    ////public DataTable DocumentTypeMasterSelect()
    ////{
    ////    DataTable DocType = new DataTable();
    ////    DocType.Columns.Add(new DataColumn("DocumentTypeId", typeof(int)));
    ////    DocType.Columns.Add(new DataColumn("DocumentTypeName", typeof(string)));
    ////    DocType.Columns.Add(new DataColumn("DocumentTypeShortName", typeof(string)));

    ////    // --1
    ////    DataRow dr1 = DocType.NewRow();
    ////    dr1["DocumentTypeId"] = "1";
    ////    dr1["DocumentTypeName"] = "Forms";
    ////    dr1["DocumentTypeShortName"] = "F ";
    ////    DocType.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = DocType.NewRow();
    ////    dr2["DocumentTypeId"] = "2";
    ////    dr2["DocumentTypeName"] = "Registers";
    ////    dr2["DocumentTypeShortName"] = "R ";
    ////    DocType.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = DocType.NewRow();
    ////    dr3["DocumentTypeId"] = "3";
    ////    dr3["DocumentTypeName"] = "Challan";
    ////    dr3["DocumentTypeShortName"] = "C ";
    ////    DocType.Rows.Add(dr3);

    ////    return DocType;
    ////}

    ////public DataTable ActMasterSelect()
    ////{
    ////    DataTable Act = new DataTable();
    ////    Act.Columns.Add(new DataColumn("ActId", typeof(int)));
    ////    Act.Columns.Add(new DataColumn("ActName", typeof(string)));
    ////    Act.Columns.Add(new DataColumn("Chapter", typeof(string)));
    ////    Act.Columns.Add(new DataColumn("Head", typeof(string)));
    ////    Act.Columns.Add(new DataColumn("Section", typeof(string)));
    ////    Act.Columns.Add(new DataColumn("ActRule", typeof(string)));
    ////    Act.Columns.Add(new DataColumn("Description", typeof(string)));

    ////    //--1
    ////    DataRow dr1 = Act.NewRow();
    ////    dr1["ActId"] = "1";
    ////    dr1["ActName"] = "ApprenticeAct";
    ////    dr1["Chapter"] = "11";
    ////    dr1["Head"] = "2";
    ////    dr1["Section"] = "2.6";
    ////    dr1["ActRule"] = "none";
    ////    dr1["Description"] = "Apprentice Act";
    ////    Act.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = Act.NewRow();
    ////    dr2["ActId"] = "2";
    ////    dr2["ActName"] = "LabourAct";
    ////    dr2["Chapter"] = "15";
    ////    dr2["Head"] = "6";
    ////    dr2["Section"] = "6.5";
    ////    dr2["ActRule"] = "none";
    ////    dr2["Description"] = "Labour Act";
    ////    Act.Rows.Add(dr2);

    ////    return Act;

    ////}

    ////public DataTable NonComplianceTaskMasterSelect()
    ////{
    ////    DataTable NoncomTask = new DataTable();
    ////    NoncomTask.Columns.Add(new DataColumn("NonComplianceTaskId", typeof(int)));
    ////    NoncomTask.Columns.Add(new DataColumn("ActivityId", typeof(int)));
    ////    NoncomTask.Columns.Add(new DataColumn("ActivityName", typeof(string)));
    ////    NoncomTask.Columns.Add(new DataColumn("ComplianceTaskName", typeof(string)));

    ////    //--1 
    ////    DataRow dr1 = NoncomTask.NewRow();
    ////    dr1["NonComplianceTaskId"] = "1";
    ////    dr1["ActivityId"] = "1";
    ////    dr1["ActivityName"] = "Allotment of apprentices quota";
    ////    dr1["ComplianceTaskName"] = "task1";
    ////    NoncomTask.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = NoncomTask.NewRow();
    ////    dr2["NonComplianceTaskId"] = "2";
    ////    dr2["ActivityId"] = "2";
    ////    dr2["ActivityName"] = "Intimation of engagement of Ap";
    ////    dr2["ComplianceTaskName"] = "task2";
    ////    NoncomTask.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = NoncomTask.NewRow();
    ////    dr3["NonComplianceTaskId"] = "3";
    ////    dr3["ActivityId"] = "2";
    ////    dr3["ActivityName"] = "Intimation of engagement of Ap";
    ////    dr3["ComplianceTaskName"] = "task3";
    ////    NoncomTask.Rows.Add(dr3);

    ////    //--4
    ////    DataRow dr4 = NoncomTask.NewRow();
    ////    dr4["NonComplianceTaskId"] = "4";
    ////    dr4["ActivityId"] = "3";
    ////    dr4["ActivityName"] = "Wage Slip";
    ////    dr4["ComplianceTaskName"] = "task4";
    ////    NoncomTask.Rows.Add(dr4);

    ////    //--5
    ////    DataRow dr5 = NoncomTask.NewRow();
    ////    dr5["NonComplianceTaskId"] = "5";
    ////    dr5["ActivityId"] = "3";
    ////    dr5["ActivityName"] = "Wage Slip";
    ////    dr5["ComplianceTaskName"] = "task5";
    ////    NoncomTask.Rows.Add(dr5);

    ////    //--6
    ////    DataRow dr6 = NoncomTask.NewRow();
    ////    dr6["NonComplianceTaskId"] = "6";
    ////    dr6["ActivityId"] = "3";
    ////    dr6["ActivityName"] = "Wage Slip";
    ////    dr6["ComplianceTaskName"] = "task6";
    ////    NoncomTask.Rows.Add(dr6);

    ////    //--7
    ////    DataRow dr7 = NoncomTask.NewRow();
    ////    dr7["NonComplianceTaskId"] = "7";
    ////    dr7["ActivityId"] = "4";
    ////    dr7["ActivityName"] = " Annual Return by Contractor/s";
    ////    dr7["ComplianceTaskName"] = "task7";
    ////    NoncomTask.Rows.Add(dr7);

    ////    return NoncomTask;
    ////}

    ////public DataTable ActivityMasterSelect()
    ////{
    ////    DataTable Activity = new DataTable();
    ////    Activity.Columns.Add(new DataColumn("ActivityId", typeof(int)));
    ////    Activity.Columns.Add(new DataColumn("ActId", typeof(int)));
    ////    Activity.Columns.Add(new DataColumn("ActName", typeof(string)));
    ////    Activity.Columns.Add(new DataColumn("ActivityName", typeof(string)));
    ////    Activity.Columns.Add(new DataColumn("DepartmentId", typeof(int)));
    ////    Activity.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
    ////    Activity.Columns.Add(new DataColumn("CategoryId", typeof(int)));
    ////    Activity.Columns.Add(new DataColumn("CategoryName", typeof(string)));
    ////    Activity.Columns.Add(new DataColumn("SeverityId", typeof(int)));
    ////    Activity.Columns.Add(new DataColumn("SeverityName", typeof(string)));
    ////    Activity.Columns.Add(new DataColumn("IsActive", typeof(bool)));
    ////    Activity.Columns.Add(new DataColumn("NonComplianceTaskAvailableFlag", typeof(char)));

    ////    //--1
    ////    DataRow dr1 = Activity.NewRow();
    ////    dr1["ActivityId"] = "1";
    ////    dr1["ActId"] = "1";
    ////    dr1["ActName"] = "ApprenticeAct";
    ////    dr1["ActivityName"] = "Allotment of apprentices quota";
    ////    dr1["DepartmentId"] = "1";
    ////    dr1["DepartmentName"] = "HumanResource";
    ////    dr1["CategoryId"] = "3";
    ////    dr1["CategoryName"] = "SF";
    ////    dr1["SeverityId"] = "1";
    ////    dr1["SeverityName"] = "DOCUMENT / RECORD / REGISTERS";
    ////    dr1["IsActive"] = "true";
    ////    dr1["NonComplianceTaskAvailableFlag"] = "Y";
    ////    Activity.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = Activity.NewRow();
    ////    dr2["ActivityId"] = "2";
    ////    dr2["ActId"] = "1";
    ////    dr2["ActName"] = "ApprenticeAct";
    ////    dr2["ActivityName"] = "Intimation of engagement of Ap";
    ////    dr2["DepartmentId"] = "1";
    ////    dr2["DepartmentName"] = "HumanResource";
    ////    dr2["CategoryId"] = "3";
    ////    dr2["CategoryName"] = "SF";
    ////    dr2["SeverityId"] = "1";
    ////    dr2["SeverityName"] = "DOCUMENT / RECORD / REGISTERS";
    ////    dr2["IsActive"] = "true";
    ////    dr2["NonComplianceTaskAvailableFlag"] = "Y";
    ////    Activity.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = Activity.NewRow();
    ////    dr3["ActivityId"] = "3";
    ////    dr3["ActId"] = "2";
    ////    dr3["ActName"] = "LabourAct";
    ////    dr3["ActivityName"] = "Wage Slip";
    ////    dr3["DepartmentId"] = "2";
    ////    dr3["DepartmentName"] = "Finance";
    ////    dr3["CategoryId"] = "2";
    ////    dr3["CategoryName"] = "SS";
    ////    dr3["SeverityId"] = "2";
    ////    dr3["SeverityName"] = "periodical Updation";
    ////    dr3["IsActive"] = "true";
    ////    dr3["NonComplianceTaskAvailableFlag"] = "N";
    ////    Activity.Rows.Add(dr3);

    ////    //--4
    ////    DataRow dr4 = Activity.NewRow();
    ////    dr4["ActivityId"] = "4";
    ////    dr4["ActId"] = "2";
    ////    dr4["ActName"] = "LabourAct";
    ////    dr4["ActivityName"] = " Annual Return by Contractor/s";
    ////    dr4["DepartmentId"] = "2";
    ////    dr4["DepartmentName"] = "Finance";
    ////    dr4["CategoryId"] = "2";
    ////    dr4["CategoryName"] = "SS";
    ////    dr4["SeverityId"] = "2";
    ////    dr4["SeverityName"] = "periodical Updation";
    ////    dr4["IsActive"] = "true";
    ////    dr4["NonComplianceTaskAvailableFlag"] = "N";
    ////    Activity.Rows.Add(dr4);

    ////    return Activity;
    ////}

    ////public DataTable ActivityForCompanyMasterSelect()
    ////{
    ////    DataTable ActivityForCmpny = new DataTable();
    ////    ActivityForCmpny.Columns.Add(new DataColumn("CompanyActivityId", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("ActivityId", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("ActivityName", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("CompanyName", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("FrequencyId", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("FrequencyName", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("DueMonth", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("DueDate", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("TriggerMonth", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("TriggerDate", typeof(int)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("ExecutionEmployeeCode", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("Executioner", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("ReviewEmployeeCode", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("Reviewer", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("HeadEmployeeCode", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("HeadEmployeeName", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("UltimateEmployeeCode", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("UltimateEmployeeName", typeof(string)));
    ////    ActivityForCmpny.Columns.Add(new DataColumn("ActivityOutsideEComplianceFlag", typeof(char)));

    ////    //--1
    ////    DataRow dr1 = ActivityForCmpny.NewRow();
    ////    dr1["CompanyActivityId"] = "1";
    ////    dr1["ActivityId"] = "1";
    ////    dr1["ActivityName"] = "Allotment of apprentices quota";
    ////    dr1["CompanyCode"] = "4000";
    ////    dr1["CompanyName"] = "REVLHO";
    ////    dr1["FrequencyId"] = "1";
    ////    dr1["FrequencyName"] = "Weekly";
    ////    dr1["DueMonth"] = "0";
    ////    dr1["DueDate"] = "0";
    ////    dr1["TriggerMonth"] = "0";
    ////    dr1["TriggerDate"] = "0";
    ////    dr1["ExecutionEmployeeCode"] = "103";
    ////    dr1["Executioner"] = "Abinayaa@ganini.com";
    ////    dr1["Reviewer"] = "Sathish@ganini.com";
    ////    dr1["ReviewEmployeeCode"] = "102";
    ////    dr1["HeadEmployeeCode"] = "101";
    ////    dr1["HeadEmployeeName"] = "SCS@ganini.com";
    ////    dr1["UltimateEmployeeCode"] = "";
    ////    dr1["UltimateEmployeeName"] = "";
    ////    dr1["ActivityOutsideEComplianceFlag"] = "N";
    ////    ActivityForCmpny.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = ActivityForCmpny.NewRow();
    ////    dr2["CompanyActivityId"] = "2";
    ////    dr2["ActivityId"] = "3";
    ////    dr2["ActivityName"] = "Wage Slip";
    ////    dr2["CompanyCode"] = "4002";
    ////    dr2["CompanyName"] = "REVLPonneriPlant";
    ////    dr2["FrequencyId"] = "2";
    ////    dr2["FrequencyName"] = "Monthly";
    ////    dr2["DueMonth"] = "0";
    ////    dr2["DueDate"] = "0";
    ////    dr2["TriggerMonth"] = "0";
    ////    dr2["TriggerDate"] = "0";
    ////    dr2["ExecutionEmployeeCode"] = "104";
    ////    dr2["Executioner"] = "Shoobana@ganini.com";
    ////    dr2["Reviewer"] = "Sathish@ganini.com";
    ////    dr2["ReviewEmployeeCode"] = "102";
    ////    dr2["HeadEmployeeCode"] = "101";
    ////    dr2["HeadEmployeeName"] = "SCS@ganini.com";
    ////    dr2["UltimateEmployeeCode"] = "";
    ////    dr2["UltimateEmployeeName"] = "";
    ////    dr2["ActivityOutsideEComplianceFlag"] = "N";
    ////    ActivityForCmpny.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = ActivityForCmpny.NewRow();
    ////    dr3["CompanyActivityId"] = "3";
    ////    dr3["ActivityId"] = "2";
    ////    dr3["ActivityName"] = "Intimation of engagement of Ap";
    ////    dr3["CompanyCode"] = "4001";
    ////    dr3["CompanyName"] = "REVLAlandurPlant";
    ////    dr3["FrequencyId"] = "3";
    ////    dr3["FrequencyName"] = "Quarterly";
    ////    dr3["DueMonth"] = "3";
    ////    dr3["DueDate"] = "10";
    ////    dr3["TriggerMonth"] = "3";
    ////    dr3["TriggerDate"] = "10";
    ////    dr3["ExecutionEmployeeCode"] = "102";
    ////    dr3["Executioner"] = "Sathish@ganini.com";
    ////    dr3["Reviewer"] = "SCS@ganini.com";
    ////    dr3["ReviewEmployeeCode"] = "101";
    ////    dr3["HeadEmployeeCode"] = "";
    ////    dr3["HeadEmployeeName"] = "";
    ////    dr3["UltimateEmployeeCode"] = "";
    ////    dr3["UltimateEmployeeName"] = "";
    ////    dr3["ActivityOutsideEComplianceFlag"] = "Y";
    ////    ActivityForCmpny.Rows.Add(dr3);

    ////    //--4
    ////    DataRow dr4 = ActivityForCmpny.NewRow();
    ////    dr4["CompanyActivityId"] = "4";
    ////    dr4["ActivityId"] = "4";
    ////    dr4["ActivityName"] = " Annual Return by Contractor/s";
    ////    dr4["CompanyCode"] = "4002";
    ////    dr4["CompanyName"] = "REVLPonneriPlant";
    ////    dr4["FrequencyId"] = "4";
    ////    dr4["FrequencyName"] = "Half Yearly";
    ////    dr4["DueMonth"] = "6";
    ////    dr4["DueDate"] = "15";
    ////    dr4["TriggerMonth"] = "6";
    ////    dr4["TriggerDate"] = "15";
    ////    dr4["ExecutionEmployeeCode"] = "101";
    ////    dr4["Executioner"] = "SCS@ganini.com";
    ////    dr4["Reviewer"] = "SCS@ganini.com";
    ////    dr4["ReviewEmployeeCode"] = "101";
    ////    dr4["HeadEmployeeCode"] = "";
    ////    dr4["HeadEmployeeName"] = "";
    ////    dr4["UltimateEmployeeCode"] = "";
    ////    dr4["UltimateEmployeeName"] = "";
    ////    dr4["ActivityOutsideEComplianceFlag"] = "Y";
    ////    ActivityForCmpny.Rows.Add(dr4);

    ////    return ActivityForCmpny;
    ////}

    ////public DataTable ActivityDocumentTypeMasterSelect()
    ////{
    ////    DataTable ActivityDocType = new DataTable();
    ////    ActivityDocType.Columns.Add(new DataColumn("ActDocypeId", typeof(int)));
    ////    ActivityDocType.Columns.Add(new DataColumn("ActivityForCompanyId", typeof(int)));
    ////    ActivityDocType.Columns.Add(new DataColumn("ActivityName", typeof(string)));
    ////    ActivityDocType.Columns.Add(new DataColumn("DocumentTypeId", typeof(int)));
    ////    ActivityDocType.Columns.Add(new DataColumn("DocumentTypeName", typeof(string)));
    ////    ActivityDocType.Columns.Add(new DataColumn("ToBeMaintained", typeof(char)));
    ////    ActivityDocType.Columns.Add(new DataColumn("ToBeSubmitted", typeof(char)));
    ////    ActivityDocType.Columns.Add(new DataColumn("MasterPDFName", typeof(string)));
    ////    ActivityDocType.Columns.Add(new DataColumn("AccessPath", typeof(string)));
    ////    ActivityDocType.Columns.Add(new DataColumn("IsActive", typeof(bool)));

    ////    //--1
    ////    DataRow dr1 = ActivityDocType.NewRow();
    ////    dr1["ActDocypeId"] = "1";
    ////    dr1["ActivityForCompanyId"] = "1";
    ////    dr1["ActivityName"] = "Allotment of apprentices quota";
    ////    dr1["DocumentTypeId"] = "1";
    ////    dr1["ToBeMaintained"] = "Y";
    ////    dr1["ToBeSubmitted"] = "N";
    ////    dr1["MasterPDFName"] = "none";
    ////    dr1["AccessPath"] = "none";
    ////    dr1["IsActive"] = "true";
    ////    ActivityDocType.Rows.Add(dr1);

    ////    //--2
    ////    DataRow dr2 = ActivityDocType.NewRow();
    ////    dr2["ActDocypeId"] = "2";
    ////    dr2["ActivityForCompanyId"] = "2";
    ////    dr2["ActivityName"] = "Wage Slip";
    ////    dr2["DocumentTypeId"] = "2";
    ////    dr2["ToBeMaintained"] = "N";
    ////    dr2["ToBeSubmitted"] = "Y";
    ////    dr2["MasterPDFName"] = "none";
    ////    dr2["AccessPath"] = "none";
    ////    dr2["IsActive"] = "true";
    ////    ActivityDocType.Rows.Add(dr2);

    ////    //--3
    ////    DataRow dr3 = ActivityDocType.NewRow();
    ////    dr2["ActDocypeId"] = "3";
    ////    dr2["ActivityForCompanyId"] = "3";
    ////    dr2["ActivityName"] = "Intimation of engagement of Ap";
    ////    dr2["DocumentTypeId"] = "3";
    ////    dr2["ToBeMaintained"] = "N";
    ////    dr2["ToBeSubmitted"] = "N";
    ////    dr2["MasterPDFName"] = "none";
    ////    dr2["AccessPath"] = "none";
    ////    dr2["IsActive"] = "true";
    ////    ActivityDocType.Rows.Add(dr3);

    ////    //--4
    ////    DataRow dr4 = ActivityDocType.NewRow();
    ////    dr2["ActDocypeId"] = "4";
    ////    dr2["ActivityForCompanyId"] = "4";
    ////    dr2["ActivityName"] = " Annual Return by Contractor/s";
    ////    dr2["DocumentTypeId"] = "1";
    ////    dr2["ToBeMaintained"] = "Y";
    ////    dr2["ToBeSubmitted"] = "Y";
    ////    dr2["MasterPDFName"] = "none";
    ////    dr2["AccessPath"] = "none";
    ////    dr2["IsActive"] = "true";
    ////    ActivityDocType.Rows.Add(dr4);

    ////    return ActivityDocType;
    ////}
    // #endregion

    public DataTable GetParentCompanyName(EmployeeMasterMsg EmpMsg)
    {
        DataTable dt = new DataTable();

        using (Connection.con)
        {
            try
            {
                Connection.cmd.CommandText = "MasCompanyMasterSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", EmpMsg.EmployeeCode);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                sda.Fill(dt);
            }
            catch (Exception Exe)
            {
                return null;
            }
            return dt;
        }

    }
    public List<EmployeeMasterMsg> ExecutionerSelect(EmployeeMasterMsg Emp)
    {
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {
                {
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.CommandText = "MasEmployeeListSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@CompanyCode", Emp.CompanyCode);

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
                            EmpMsg.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            EmpMsg.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            EmpMsg.IsActive =Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            EmpList.Add(EmpMsg);
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString().Trim());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return EmpList;

    }
    public List<LocationDepartmentMasterMsg> MasLocEmployeeListSelect(LocationDepartmentMasterMsg Location)
    {
        List<LocationDepartmentMasterMsg> LocationList = new List<LocationDepartmentMasterMsg>();
        using (Connection.con)
        {
            try
            {
                {
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.CommandText = "MasLocEmployeeListSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Location.EmployeeCode);

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            LocationDepartmentMasterMsg LocationMsg = new LocationDepartmentMasterMsg();
                            LocationMsg.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            LocationMsg.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            LocationMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            LocationList.Add(LocationMsg);
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString().Trim());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return LocationList;

    }
    public List<EmployeeMasterMsg> MasLocEmployeeListSelectsp(EmployeeMasterMsg Emp)
    {
        List<EmployeeMasterMsg> EmpList = new List<EmployeeMasterMsg>();
        using (Connection.con)
        {
            try
            {
                {
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.CommandText = "MasLocEmployeeListSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Emp.EmployeeCode); 

                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            EmployeeMasterMsg EmpMsg = new EmployeeMasterMsg();
                            EmpMsg.EmployeeCode = sdr["EmployeeCode"].ToString().Trim();
                            EmpMsg.EmployeeName = sdr["EmployeeName"].ToString().Trim();
                            EmpMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            EmpList.Add(EmpMsg);
                        }

                    }

                }
            }

            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString().Trim());
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                //return null;
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
        }
        return EmpList;

    }
    //public List<NonComplianceTaskMasterMsg> MasNonComplianceTaskMasterSelect(ActivityForCompanyMasterMsg ActivityComp)
    //{
    //    List<EmployeeMasterMsg> NonCompList = new List<EmployeeMasterMsg>();
    //    using (Connection.con)
    //    {
    //        try
    //        {
    //            {
    //                Connection.cmd.Connection = Connection.con;
    //                Connection.cmd.CommandText = "MasActivityNonComplianceTaskMasterSelectSp";
    //                Connection.cmd.CommandType = CommandType.StoredProcedure;
    //                Connection.cmd.Parameters.Clear();
    //                Connection.cmd.Parameters.AddWithValue("@ActivityForCompanyId", ActivityComp.CompanyActivityId);
    //                Connection.cmd.Parameters.AddWithValue("@Result", ActivityComp.Result);

    //                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
    //                {
    //                    while (sdr.Read())
    //                    {
    //                        NonComplianceTaskMasterMsg NonCompMsg = new NonComplianceTaskMasterMsg();
    //                        NonCompMsg.NonComplianceTaskId =Convert.ToInt32(sdr["NonComplianceTaskId"].ToString().Trim());
    //                        NonCompMsg.ComplianceTaskName = sdr["ComplianceTaskName"].ToString().Trim();
    //                        NonCompMsg.IsActive =Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
    //                        NonCompList.Add(NonCompMsg);
    //                    }

    //                }

    //            }
    //        }

    //        catch (Exception ex)
    //        {
    //            //MessageBox.Show(ex.ToString().Trim());
    //            ExceptionHandling eh = new ExceptionHandling();
    //            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
    //            //return null;
    //            return null;
    //        }
    //        finally
    //        {
    //            Connection.cmd.Dispose();
    //            Connection.cmd.Parameters.Clear();
    //        }
    //    }
    //    return NonCompList;


    //public List<CompanyMessage> MasCompanyInsertUpdateandDelete(CompanyMessage Cmp, EmployeeMasterMsg EmpMsg)
    //{
    //    throw new NotImplementedException();
    //}
    public List<MailDetailMsg> GetActivityMailDetails(int QuarterlyMonth, bool QuarterlyTrigger, int HalfyearlyMonth, bool HalfyearlyTrigger)
    {
        List<MailDetailMsg> MailDetailMsgList = new List<MailDetailMsg>();
        try
        {
            using (Connection.con)
            {
                try
                {
                    Connection.cmd.CommandText = "MailDetailsforAllActivitysp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@QuarterlyMonth", QuarterlyMonth);
                    Connection.cmd.Parameters.AddWithValue("@QuarterlyTrigger", QuarterlyTrigger);
                    Connection.cmd.Parameters.AddWithValue("@HalfyearlyMonth", HalfyearlyMonth);
                    Connection.cmd.Parameters.AddWithValue("@HalfyearlyTrigger", HalfyearlyTrigger);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            MailDetailMsg mailDetailMsg = new MailDetailMsg();
                            mailDetailMsg.ActName = sdr["ActName"].ToString().Trim();
                            mailDetailMsg.ActivityName = sdr["ActivityName"].ToString().Trim();
                            mailDetailMsg.ExecutionEmployeeName = sdr["ExecutionEmployee"].ToString().Trim();
                            mailDetailMsg.ToEmail = sdr["ToEmail"].ToString().Trim();
                            mailDetailMsg.ReviewEmployeeName = sdr["ReviewEmployee"].ToString().Trim();
                            mailDetailMsg.CCMail = sdr["CCMail"].ToString().Trim();
                            mailDetailMsg.Status = sdr["Status"].ToString().Trim();
                            mailDetailMsg.DueDate = sdr["DueDate"].ToString().Trim();
                            mailDetailMsg.DueMonth = sdr["DueMonth"].ToString().Trim();
                            mailDetailMsg.DueDay = sdr["DueDay"].ToString().Trim();
                            mailDetailMsg.FrequencyName = sdr["FrequencyName"].ToString().Trim();
                            mailDetailMsg.TriggerMonth = sdr["TriggerMonth"].ToString().Trim();
                            mailDetailMsg.TriggerDay = sdr["TriggerDay"].ToString().Trim();
                            mailDetailMsg.TriggerDate = sdr["TriggerDate"].ToString().Trim();
                            MailDetailMsgList.Add(mailDetailMsg);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    ExceptionHandling eh = new ExceptionHandling();
                    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    return null;
                }
                finally
                {
                    Connection.cmd.Parameters.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
            return null;
        }
        return MailDetailMsgList;
    }
    public void NewActivityActionInsert()
    {
        try
        {
            using (Connection.con)
            {
                try
                {
                    Connection.cmd.CommandText = "ActivityActionAutoCreatesp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.ExecuteNonQuery();
                }
                catch (Exception Ex)
                {
                    ExceptionHandling eh = new ExceptionHandling();
                    eh.HandleException(Ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
                    return;
                }
                finally
                {
                    Connection.cmd.Parameters.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            ExceptionHandling eh = new ExceptionHandling();
            eh.HandleException(ex, ExceptionHandling.HandlePolicy.Logonly, ExceptionHandling.Wrap.Business);
            return;
        }
    }
    public List<TranNonComplianceMsg> TranEDCInsertUpdate(TranNonComplianceMsg TranNoncomlMsg)
    {
        SqlTransaction transaction = null;
        System.Globalization.DateTimeFormatInfo dateinfo = new System.Globalization.DateTimeFormatInfo();
        dateinfo.ShortDatePattern = "dd/MM/yyyy";
        string Result = "0";
        List<TranNonComplianceMsg> TranList = new List<TranNonComplianceMsg>();
        using (Connection.con)
        {
            try
            {

                transaction = Connection.con.BeginTransaction();
                Connection.cmd.Transaction = transaction;
                Connection.cmd.Connection = Connection.con;
                if (TranNoncomlMsg.Flag != "R")
                {
                    Connection.cmd.CommandText = "MasActivityNonComplianceTasksInsertSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;

                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@Flag", TranNoncomlMsg.Flag);
                    Connection.cmd.Parameters.AddWithValue("@ActivityNonComplainceTaskId", TranNoncomlMsg.ActivityNonComplianceTaskId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityNonComplainceTaskMasterId", TranNoncomlMsg.ActivityNonComplianceTaskMasterId);
                    Connection.cmd.Parameters.AddWithValue("@ActivityActionId", TranNoncomlMsg.ActivityActionId);
                    Connection.cmd.Parameters.AddWithValue("@ExpectedDateOfCompletion", Convert.ToDateTime(TranNoncomlMsg.ExpectedDateOfCompletion, dateinfo));
                    Connection.cmd.Parameters.AddWithValue("@CompletedDate", TranNoncomlMsg.CompletedDate == string.Empty ? Convert.ToDateTime("1/1/1990") : Convert.ToDateTime(TranNoncomlMsg.CompletedDate, dateinfo));
                    Connection.cmd.Parameters.AddWithValue("@CreatedBy", TranNoncomlMsg.CreatedBy);
                    Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                    Connection.cmd.ExecuteNonQuery();
                    Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                }
                if (Result == "0")
                {
                    Connection.cmd.CommandText = "MasNonComplianceTaskForActivityForCompanySelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Parameters.Clear();
                    Connection.cmd.Parameters.AddWithValue("@ActivityCompanyId", TranNoncomlMsg.ActivityForCompanyId);
                    using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            TranNonComplianceMsg TranNonComp = new TranNonComplianceMsg();
                            TranNonComp.NonComplianceResult = Result;
                            TranNonComp.ActivityNonComplianceTaskMasterId = Convert.ToInt32(sdr["ActivityNonComplianceTaskMasterId"].ToString().Trim());
                            TranNonComp.ActivityForCompanyId = Convert.ToInt32(sdr["ActivityForCompanyId"].ToString().Trim());
                            TranNonComp.ComplianceTaskName = sdr["ComplianceTaskName"].ToString().Trim();
                            TranNonComp.ActivityNonComplianceTaskId = Convert.ToInt32(sdr["ActivityNonComplianceTasksId"].ToString().Trim());
                            TranNonComp.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                            TranNonComp.ExpectedDateOfCompletion =sdr["ExpectedDateOfCompletion"].ToString().Trim();
                            TranNonComp.CompletedDate = sdr["CompletedDate"].ToString().Trim();
                            TranList.Add(TranNonComp);
                        }

                    }
                    transaction.Commit();
                    Connection.cmd.Parameters.Clear();
                    Connection.con.Close();
                }
                else
                {
                    TranNonComplianceMsg TranNoncomp = new TranNonComplianceMsg();
                    TranNoncomp.NonComplianceResult = Result;
                    TranList.Add(TranNoncomp);

                }

            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);

            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return TranList;
        }
    }

    #region Activity Change
    public List<LocationDepartmentMasterMsg> SelectAssigneeMatrix(LocationDepartmentMasterMsg Location)
    {
        List<LocationDepartmentMasterMsg> LocationDepartmentList = new List<LocationDepartmentMasterMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "MasResponsibleGroupForActivitySelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@DepartmentId", Location.DepartmentId);
                Connection.cmd.Parameters.AddWithValue("@ActivityId", Location.ActivityId);
                Connection.cmd.Parameters.AddWithValue("@EmployeeCode", Location.EmployeeCode);
                using (SqlDataReader sdr = Connection.cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        LocationDepartmentMasterMsg LocationDeptMsg = new LocationDepartmentMasterMsg();
                        LocationDeptMsg.CompanyCode = sdr["CompanyCode"].ToString().Trim();
                        LocationDeptMsg.CompanyName = sdr["CompanyName"].ToString().Trim();
                        LocationDeptMsg.ResponsibleGrpName = sdr["ResponsibleGroupName"].ToString().Trim();
                        LocationDeptMsg.LocationDeptId = Convert.ToInt32(sdr["LocationDeptId"].ToString().Trim());
                        LocationDeptMsg.IsActive = Convert.ToBoolean(sdr["IsActive"].ToString().Trim());
                        LocationDeptMsg.CompanyActivityId = Convert.ToInt32(sdr["CompanyActivityId"].ToString().Trim());
                        LocationDepartmentList.Add(LocationDeptMsg);
                    }
                }
                Connection.cmd.Parameters.Clear();
                Connection.con.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandling eh = new ExceptionHandling();
                eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
                return null;
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }
        }
        return LocationDepartmentList;
    }
    public bool MasActivityForCompanyMasterInsertandUpdate(ActivityForCompanyMasterMsg ActivityForCmpny)
    {
        SqlTransaction transaction = null;
        string Result = "0";
        bool Success = false;
        List<ActivityForCompanyMasterMsg> ActivityForCmpnyList = new List<ActivityForCompanyMasterMsg>();
        using (Connection.con)
        {
            try
            {
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.CommandText = "MasActivityForCompanyMasterInsertandUpdateSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@WAFCId", ActivityForCmpny.CompanyActivityId);
                Connection.cmd.Parameters.AddWithValue("@ActivityId", ActivityForCmpny.ActivityId);
                Connection.cmd.Parameters.AddWithValue("@CompanyCode", ActivityForCmpny.CompanyCode);
                Connection.cmd.Parameters.AddWithValue("@FrequencyId", ActivityForCmpny.FrequencyId);
                Connection.cmd.Parameters.AddWithValue("@FrequencyRemarks", ActivityForCmpny.FrqRemarks);//added by Abinayaa 161112
                Connection.cmd.Parameters.AddWithValue("@DueMonth", ActivityForCmpny.DueMonth);
                Connection.cmd.Parameters.AddWithValue("@DueDate", ActivityForCmpny.DueDate);
                Connection.cmd.Parameters.AddWithValue("@DueDay", ActivityForCmpny.DueDay);
                Connection.cmd.Parameters.AddWithValue("@TriggerMonth", ActivityForCmpny.TriggerMonth);
                Connection.cmd.Parameters.AddWithValue("@TriggerDate", ActivityForCmpny.TriggerDate);
                Connection.cmd.Parameters.AddWithValue("@TriggerDay", ActivityForCmpny.TriggerDay);
                Connection.cmd.Parameters.AddWithValue("@ExecutionEmployeeCode", ActivityForCmpny.ExecutionEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@ReviewEmployeeCode", ActivityForCmpny.ReviewEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@HeadEmployeeCode", ActivityForCmpny.HeadEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@UltimateEmployeeCode", ActivityForCmpny.UltimateEmployeeCode);
                Connection.cmd.Parameters.AddWithValue("@IsActive", ActivityForCmpny.IsActive);
                Connection.cmd.Parameters.AddWithValue("@CreatedBy", ActivityForCmpny.CreatedBy);
                Connection.cmd.Parameters.AddWithValue("@AssigneeMatrixId", ActivityForCmpny.LocationDepartmentId);
                Connection.cmd.Parameters.Add("@Result", SqlDbType.VarChar, 256).Direction = ParameterDirection.Output;
                Connection.cmd.ExecuteNonQuery();
                Result = Convert.ToString(Connection.cmd.Parameters["@Result"].Value.ToString().Trim());
                if (Result == "0")
                {
                    Success = true;
                }
                else
                {
                    Success = false;
                }
            }
            catch (Exception ex)
            {
                return Success = false;
                //ExceptionHandling eh = new ExceptionHandling();
                //eh.HandleException(ex, ExceptionHandling.HandlePolicy.Propgate, ExceptionHandling.Wrap.Business);
            }
            finally
            {
                Connection.cmd.Parameters.Clear();
            }

            return Success;
        }
    }
    #endregion
}
   
  

