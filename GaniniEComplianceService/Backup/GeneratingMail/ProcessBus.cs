using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Ganini.Lib;
using System.IO;
using System.Data.SqlClient;

namespace GeneratingMail
{
    public class ProcessBus
    {
        //To Create an Object for Connection Class 
        private ConnectionClass mConnection = null;
        private ConnectionClass Connection
        {
            get
            {
                if (null == mConnection)
                {
                    mConnection = new ConnectionClass();
                }
                return mConnection;
            }
        }

        # region Operations

        public DataTable CheckForRemainder(int Days)
        {
            DataTable dt = new DataTable();
            try
            {
                Connection.cmd.CommandText = "MailforRemainingActionSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@Days", Days);
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                sda.Fill(dt);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return dt;
        }

        public DataTable SelectDepartment()
        {
            DataTable dt = new DataTable();
            {
                try
                {
                    //SQLSelect = "select Id,DepartmentName,DepartmentShortName,IsActive from tbol_Department order by id asc";
                    Connection.cmd.CommandText = "MasDepartmentSelectSp";
                    Connection.cmd.CommandType = CommandType.StoredProcedure;
                    Connection.cmd.Connection = Connection.con;
                    Connection.cmd.Parameters.Clear();
                    SqlDataAdapter sda = new SqlDataAdapter();
                    sda.SelectCommand = Connection.cmd;
                    sda.SelectCommand.ExecuteNonQuery();
                    sda.Fill(dt);
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
                finally
                {
                    Connection.cmd.Dispose();
                    Connection.cmd.Parameters.Clear();
                }
            }
            return dt;
        }
        public string SelectTopMgmtEmailId()
        {
            string ToMailId = null;
            try
            {
                Connection.cmd.CommandText = "MailfortheTopMgmtSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                SqlDataReader sdr = Connection.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    ToMailId=sdr["EmailId"].ToString();
                }
                sdr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return ToMailId;
        }
        public DataTable SelectManagementDepartmentwiseReport(int DepartmenCode, DateTime Fromdate, DateTime Todate)
        {
            DataTable dt = new DataTable();
            try
            {
                Connection.cmd.CommandText = "TranManagementReportDepartmentwiseSelectSp";
                Connection.cmd.CommandType = CommandType.StoredProcedure;
                Connection.cmd.Connection = Connection.con;
                Connection.cmd.Parameters.Clear();
                Connection.cmd.Parameters.AddWithValue("@Fromdate", Fromdate);
                Connection.cmd.Parameters.AddWithValue("@DepartmentCode", DepartmenCode);
                Connection.cmd.Parameters.AddWithValue("@Todate", Todate);
                Connection.cmd.Parameters.AddWithValue("@loginid", "0");
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = Connection.cmd;
                sda.SelectCommand.ExecuteNonQuery();
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                Connection.cmd.Dispose();
                Connection.cmd.Parameters.Clear();
            }
            return dt;
        }
        #endregion
    }
}
