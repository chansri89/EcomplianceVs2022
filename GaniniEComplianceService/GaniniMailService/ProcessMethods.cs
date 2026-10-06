using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using Ganini.Lib;
using System.IO;
using System.Data.SqlClient;

namespace GaniniExcellClassLibrary
{
    public class ProcessMethods
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

        public DataSet CheckForRemainder(int Days)
        {
            DataSet ds = new DataSet();
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
                sda.Fill(ds, "RemainderForTask");
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
            return ds;
        }
        
        #endregion
    }
}
