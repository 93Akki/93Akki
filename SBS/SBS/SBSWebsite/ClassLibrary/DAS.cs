using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary
{
    public class DataAccessService
    {

        string Query_PreFix = "";
        string Query_PostFix = "";
        private IDAS_SyncHelper DASSyncHelper;
        private SqlConnection ConSQL;
        private Int16 iEmpID = 0;
        private XElement GlobalParam = new XElement("QueryParam", null);
        private string ConnectionStr = "";



        public DataAccessService()
        {
            try
            {
                ConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnSBS"].ConnectionString;
                ConSQL = new SqlConnection();
                ConSQL.ConnectionString = ConnectionStr;

            }
            catch
            { }
        }
        public DataAccessService(string ConStr)
        {
            try
            {
                ConSQL = new SqlConnection();
                ConSQL.ConnectionString = ConStr;
                //ConSQL.Open();
            }
            catch
            { }
        }
        public DataAccessService(ref IDAS_SyncHelper DASSyncHelper)
            : this(0, ref DASSyncHelper, 0, 0)
        {
        }
        public DataAccessService(Int64 TransNo, ref IDAS_SyncHelper DataAccessServiceSyncHelper, Int16 iCurrMenuID, Int16 iEmployeeID)
        {
            this.DASSyncHelper = DataAccessServiceSyncHelper;
            this.iEmpID = iEmployeeID;
            ConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            ConSQL = new SqlConnection();

            ConSQL.ConnectionString = ConnectionStr;


            //GlobalParam.Add(
            //                   new XElement("MenuID", iCurrMenuID.ToString()),
            //                   new XElement("EmployeeID", iEmployeeID.ToString()),
            //                   new XElement("LoginTransNo", TransNo.ToString())
            //                );
        }


        public DataTable GetDataTable(string SPName, XElement QueryParam)
        {
            DataTable dt = null;
            Exception e = null;
            DataSet ds = null;
            ds = SQLGetData_Load(SPName, QueryParam, ref e, true);
            if (ds == null)
                return null;
            else
            {
                dt = ds.Tables[0];
                ds.Tables.Remove(dt);
                return dt;
            }
        }

        public DataSet GetDataSet(string SPName, XElement QueryParam)
        {
            Exception e = null;
            DataSet ds = null;
            ds = SQLGetData_Load(SPName, QueryParam, ref e, true);
            return ds;
        }

        private DataSet SQLGetData_Load(string SPName, XElement QueryParam, ref Exception e, bool bFlag)
        {
            DataTable DataBaseCheck = new DataTable();
            string Query = "";
            SQLGetQuery(ref Query, SPName, QueryParam);
            DataSet ds = new DataSet();


            bool flg = false;
            do
            {
                flg = false;
                SqlDataAdapter SQLDA = new SqlDataAdapter(Query, ConSQL);
                try
                {
                    if (this.ConSQL.State == ConnectionState.Closed)
                        ConSQL.Open();
                    SQLDA.SelectCommand.CommandTimeout = 0;
                    SQLDA.Fill(ds);
                }
                catch
                {
                    return null;
                }
                finally
                {
                    SQLDA.SelectCommand.Connection.Close();
                    SQLDA.Dispose();
                    //SQLDA = Nothing
                    if (this.ConSQL.State == ConnectionState.Open)
                        this.ConSQL.Close();
                }
            } while (flg);
            try
            {
                string returnValue = "";
                try
                {


                    returnValue = ds.Tables[0].Rows[0][0].ToString();
                }
                catch
                {

                    returnValue = "";
                }
            }
            catch
            {
                return null;
            }
            if (ds != null && ds.Tables.Count > 1)
            {
                string s = "";
                DataTable xdt = ds.Tables[ds.Tables.Count - 1];

                if (xdt.Columns.Contains("CurrentDateTime"))
                {
                    s = xdt.Rows[0]["CurrentDateTime"].ToString();
                    this.DASSyncHelper.LocalDateTime = s;
                    ds.Tables.Remove(xdt);
                }
                DataTable xdt1 = ds.Tables[ds.Tables.Count - 1];
                if (xdt1.Columns.Count == 1 && bFlag && xdt1.Columns.Contains("error_number"))
                {
                    s = xdt1.Rows[0]["error_number"].ToString();
                    string[] myArr = s.Split('|');
                    string msg = string.Format("Some unhandled error occurred in application. Please contact your application administrator for resolution!!</br> Error Number :{0}</br>Error Log ID:{1}", myArr[1], myArr[2]);
                    ds.Tables.Remove(xdt1);
                    ds = null;
                }
            }


            return ds;
        }

        private void SQLGetQuery(ref string SQLQuery, string SPName, XElement QueryParam)
        {
            //if (iEmpID != 0)
            //{
            //    SQLQuery = string.Format("EXEC rel.rspLoginCheck @ExecSPName = N'{0}',  @ExecQueryParam=N'{1}', @GlobalParam = N'{2}' ", SPName, QueryParam.ToString().Replace("&apos;", "''"), GlobalParam.ToString().Replace("'", "''"));
            //}
            //else
            SQLQuery = string.Format("EXEC " + SPName + "  @SPParamList='N{0}' ", QueryParam.ToString().Replace("&apos;", "''"));
        }

        public string SimpleCrypt(string Text)
        {
            string strTempChar = "";
            string TempText = "";
            int i = 0;
            for (i = 1; i <= Text.Length; i++)
            {
                char c = Text.ToCharArray(i - 1, 1)[0];
                if (Convert.ToInt32(c) > 128)
                    strTempChar = ((char)(c - 128)).ToString();
                strTempChar = ((char)c).ToString();
                TempText = TempText + strTempChar;
            }
            return TempText;
        }

        public string GetQueryXML(Dictionary<string, string> QueryParam)
        {
            return new XElement("QueryParam", QueryParam.Select(i => new XElement(i.Key, i.Value))).ToString();
        }

        #region[Work For Insert]
        public DataSet NewGetDataSet(string Query)
        {
            if (this.ConSQL.State == ConnectionState.Closed)
                this.ConSQL.Open();
            //Query = Query + sCompanyID_FinYear;
            DataSet ds = new DataSet();
            SqlDataAdapter SQLDA = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 300;
            cmd.Connection = ConSQL;
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            SQLDA.SelectCommand = cmd;
            //SQLDA.SelectCommand.ExecuteNonQuery();
            try { SQLDA.Fill(ds); }
            catch (SqlException ex)
            {
                //System.Windows.Forms.MessageBox.Show("Failed to Execute Command! \n\tError Detail:\n \t" + ex.Message, "Data Access Error!", MessageBoxButtons.OK);
            }
            finally
            {
                SQLDA.SelectCommand.Connection.Close();
                SQLDA = null;
                if (this.ConSQL.State == ConnectionState.Open)
                    this.ConSQL.Close();
            }
            return ds;
        }
        public DataSet SelectDataSet(string Query)
        {
            if (this.ConSQL.State == ConnectionState.Closed)
                this.ConSQL.Open();
            //Query = Query + sCompanyID_FinYear;
            DataSet ds = new DataSet();
            SqlDataAdapter SQLDA = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 300;
            cmd.Connection = ConSQL;
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            SQLDA.SelectCommand = cmd;
            SQLDA.SelectCommand.ExecuteNonQuery();
            try { SQLDA.Fill(ds); }
            catch (SqlException ex)
            {
                //System.Windows.Forms.MessageBox.Show("Failed to Execute Command! \n\tError Detail:\n \t" + ex.Message, "Data Access Error!", MessageBoxButtons.OK);
            }
            finally
            {
                SQLDA.SelectCommand.Connection.Close();
                SQLDA = null;
                if (this.ConSQL.State == ConnectionState.Open)
                    this.ConSQL.Close();
            }
            return ds;
        }
        public DataTable SelectDataTable(string Query)
        {
            if (this.ConSQL.State == ConnectionState.Closed)
                this.ConSQL.Open();
            //Query = Query + sCompanyID_FinYear;
            DataTable dt = new DataTable();
            SqlDataAdapter SQLDA = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 300;
            cmd.Connection = ConSQL;
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            SQLDA.SelectCommand = cmd;
            SQLDA.SelectCommand.ExecuteNonQuery();
            try
            {
                SQLDA.Fill(dt);
            }
            catch (SqlException ex)
            {
                //System.Windows.Forms.MessageBox.Show("Failed to Execute Command! \n\tError Detail:\n \t" + ex.Message, "Data Access Error!", MessageBoxButtons.OK);
            }
            finally
            {
                SQLDA.SelectCommand.Connection.Close();
                SQLDA = null;
                if (this.ConSQL.State == ConnectionState.Open)
                    this.ConSQL.Close();
            }
            return dt;
        }
        public DataTable NewGetDataTable(string Query)
        {
            if (this.ConSQL.State == ConnectionState.Closed)
                this.ConSQL.Open();
            //Query = Query + sCompanyID_FinYear;
            DataTable dt = new DataTable();
            SqlDataAdapter SQLDA = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 300;
            cmd.Connection = ConSQL;
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            SQLDA.SelectCommand = cmd;
            try
            {
                SQLDA.Fill(dt);
            }
            catch (SqlException ex)
            {
                //System.Windows.Forms.MessageBox.Show("Failed to Execute Command! \n\tError Detail:\n \t" + ex.Message, "Data Access Error!", MessageBoxButtons.OK);
            }
            finally
            {
                SQLDA.SelectCommand.Connection.Close();
                SQLDA = null;
                if (this.ConSQL.State == ConnectionState.Open)
                    this.ConSQL.Close();
            }
            return dt;
        }
        private string GetSQLQuery(string SQLQuery)
        {

            if (this.Query_PreFix.Trim().Length > 0) SQLQuery = this.Query_PreFix + " @Query=N'" + SQLQuery.Replace("'", "''") + "'" + Query_PostFix;
            return SQLQuery;
        }
        public string ExecSQLQuery(string Query)
        {

            if (this.ConSQL.State == ConnectionState.Closed)
                this.ConSQL.Open();
            Query = GetSQLQuery(Query);
            SqlCommand SQLCmd = new SqlCommand(Query, ConSQL);
            SQLCmd.CommandTimeout = 0;
            string returnValue = "";
            try
            {
                returnValue = (SQLCmd.ExecuteScalar()).ToString();
            }
            catch (InvalidOperationException ex)
            {
                returnValue = ex.Message;
            }
            catch (SqlException ex1)
            {
                returnValue = ex1.Number.ToString();
            }

            catch (NullReferenceException ex2)
            {
                returnValue = "";
            }
            finally
            {
                SQLCmd.Connection.Close();
                SQLCmd = null;
                if (this.ConSQL.State == ConnectionState.Open)
                    this.ConSQL.Close();
            }
            return returnValue;
        }
        #endregion
    }
    public interface IDAS_SyncHelper
    {
        DateTime DateTimeNow { get; }
        string DateTimeNowString { get; }
        string LocalDateTime { get; set; }
    }
}
