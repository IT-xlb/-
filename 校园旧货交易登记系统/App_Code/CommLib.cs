using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using 校园旧货交易登记系统.App_Code;

namespace 校园旧货交易登记系统.App_Code
{
    public class DBCommLIb
    {
        //保存连接信息的字串
        static string connString;
        public DBCommLIb()
        {
            //从配置文件中获取连接信息
            connString = ConfigurationManager.ConnectionStrings["DBConnStr"].ToString();
        }

        //建立面向SQL SERVER的连接对象
        public SqlConnection getSqlConn()
        {
            //使用连接字串创建连接对象
            return new SqlConnection(connString);
        }

        /* 功能：传入SQL查询语句，调用Command方法，执行SQL命令，结果放到DataReader中。
          * 传入参数：SQL语句；参数列表：参数名与参数值
          * 返回数据：包含查询结果的DataReader
          * */
        public SqlDataReader getSdr(string strSql, string[,] paras)
        {
            //以下语句的解释见ExecCommand与updateRecordWithSQL方法
            SqlCommand sqlComm = new SqlCommand();
            sqlComm.Connection = getSqlConn();
            sqlComm.CommandType = CommandType.Text;
            sqlComm.CommandText = strSql;
            if (paras.Length > 1 && paras[0, 0] != "-1")
            {
                for (int i = 0; i < paras.Length / 2; i++)
                {
                    if (paras[i, 0] != "-1")
                    {
                        sqlComm.Parameters.AddWithValue(paras[i, 0], paras[i, 1]);
                    }
                }
            }
            try
            {
                if (sqlComm.Connection.State == ConnectionState.Closed)
                    sqlComm.Connection.Open();
                //执行SQL的Select命令，返回结果放到结果集DataReader中并返回
                return sqlComm.ExecuteReader();
            }
            catch
            {
                //如果程序错误，则返回NULL
                return null;
            }
        }

        /* 功能：传入SQL查询语句，调用Command方法，执行SQL命令，结果放到DataSet中。
         * 传入参数：SQL语句；DataTable表名
         * 返回数据：包含查询结果的DataSet
         * */
        public DataSet getDS(string strSql, string[,] paras)
        {
            try
            {
                //使用SQL语句与连接字串实例化一个SqlDataAdapter对象
                SqlDataAdapter sqlDA = new SqlDataAdapter(strSql, connString);
                //为SqlDataAdapter中的参数列表添加参数
                if (paras.Length > 1 && paras[0, 0] != "-1")
                {
                    for (int i = 0; i < paras.Length / 2; i++)
                    {
                        if (paras[i, 0] != "-1")
                        {
                            sqlDA.SelectCommand.Parameters.AddWithValue(paras[i, 0], paras[i, 1]);
                        }
                    }
                }
                //实例化DataSet            
                DataSet ds = new DataSet();
                //执行封装在SqlDataAdapter中的SQL语句，结果填充到DataSet中
                sqlDA.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }

        /* 功能：传入SQL查询语句，调用Command方法，执行SQL命令，结果放到DataDataTable中。
         * 传入参数：SQL语句；参数列表：参数名与参数值
         * 返回数据：包含查询结果的DataTable
         * */
        public DataTable getDT(string strSql, string[,] paras)
        {
            try
            {
                //创建一个Command对象并赋值
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = getSqlConn();
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = strSql;
                if (paras.Length > 1 && paras[0, 0] != "-1")
                {
                    for (int i = 0; i < paras.Length / 2; i++)
                    {
                        if (paras[i, 0] != "-1")
                        {
                            sqlComm.Parameters.AddWithValue(paras[i, 0], paras[i, 1]);
                        }
                    }
                }
                //用Command对象实例化SqlDataAdapter对象
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
                //实例化一个DataTable
                DataTable dt = new DataTable();
                //执行封装在SqlDataAdapter中的SQL语句，把执行结果集填充到DataTable中
                sqlDA.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        /*注：本方法只是为了演示简单的执行SQL语句的过程，不是推荐的使用方法。
         * 功能：传入SQL查询语句，调用Command方法，执行SQL命令，返回一个查询结果。
         * 传入参数：SQL语句
         * 返回数据：执行结果
         * */
        public string ExecCommand(string Ls_SqlCmd)
        {
            //直接把连接字串写在方法中
            string connStr = "Data Source=(local);Initial Catalog=LearnDB;User ID=sa;Password=123456";
            //利用连接字串创建连接对象
            SqlConnection conn = new SqlConnection(connStr);
            //创建Command对象，为传递SQL命令做准备
            SqlCommand sqlComm = new SqlCommand();
            //把连接对象赋值给Command的Connection属性，建立Command与Connection对象的绑定
            sqlComm.Connection = conn;
            try
            {
                //SQL命令赋值给Command
                sqlComm.CommandText = Ls_SqlCmd;
                //判断连接对象是否打开，如果未打开，打开之
                if (sqlComm.Connection.State == ConnectionState.Closed)
                    sqlComm.Connection.Open();
                //执行SQL语句，返回数据集0行0列的数值
                return sqlComm.ExecuteScalar().ToString();
            }
            catch
            {
                //如果执行出现异常，关闭连接对象，返回空字串
                if (sqlComm.Connection.State.ToString().Trim() == "Opened")
                    sqlComm.Connection.Close();
                return "";
            }
        }
        public string ExecCommand(string Ls_SqlCmd, string[,] paras)
        {
            //直接把连接字串写在方法中
            string connStr = "Data Source=(local);Initial Catalog=LearnDB;User ID=sa;Password=123456";
            //利用连接字串创建连接对象
            SqlConnection conn = new SqlConnection(connStr);
            //创建Command对象，为传递SQL命令做准备
            SqlCommand sqlComm = new SqlCommand();
            //把连接对象赋值给Command的Connection属性，建立Command与Connection对象的绑定
            sqlComm.Connection = conn;
            try
            {
                //SQL命令赋值给Command
                sqlComm.CommandText = Ls_SqlCmd;
                //如果没有参数，则参数数组的[0,0]值为-1；如果有参数，则[*,0]为参数名，[*,1]为参数值
                if (paras.Length > 1 && paras[0, 0] != "-1")
                {
                    for (int i = 0; i < paras.Length / 2; i++)
                    {
                        if (paras[i, 0] != "-1")
                        {
                            sqlComm.Parameters.AddWithValue(paras[i, 0], paras[i, 1]);
                        }
                    }
                }
                //判断连接对象是否打开，如果未打开，打开之
                if (sqlComm.Connection.State == ConnectionState.Closed)
                    sqlComm.Connection.Open();
                //执行SQL语句，返回数据集0行0列的数值
                return sqlComm.ExecuteScalar().ToString();
            }
            catch
            {
                //如果执行出现异常，关闭连接对象，返回空字串
                if (sqlComm.Connection.State.ToString().Trim() == "Opened")
                    sqlComm.Connection.Close();
                return "";
            }
        }

        /* 功能：传入SQL语句，调用Command方法，执行SQL命令。
         * 传入参数：SQL语句；参数列表：参数名与参数值
         * 返回数据：执行SQL语句（增、删、改等所有修改数据库的操作）
         * */
        public string updateRecordWithSQL(string Ls_SqlCmd, string[,] paras)
        {
            //调用连接对象创建方法创建连接对象
            //SqlConnection conn = getSqlConn();
            //创建Command对象，为传递SQL命令做准备
            SqlCommand sqlComm = new SqlCommand();
            //把连接对象赋值给Command的Connection属性，建立Command与Connection对象的绑定
            sqlComm.Connection = getSqlConn();
            //指定Command的命令类型的是SQL语句
            sqlComm.CommandType = CommandType.Text;
            //SQL命令赋值给Command
            sqlComm.CommandText = Ls_SqlCmd;
            //如果没有参数，则参数数组的[0,0]值为-1；如果有参数，则[*,0]为参数名，[*,1]为参数值
            if (paras.Length > 1 && paras[0, 0] != "-1")
            {
                for (int i = 0; i < paras.Length / 2; i++)
                {
                    if (paras[i, 0] != "-1")
                    {
                        sqlComm.Parameters.AddWithValue(paras[i, 0], paras[i, 1]);
                    }
                }
            }
            try
            {
                //判断连接对象是否打开，如果未打开，打开之
                if (sqlComm.Connection.State == ConnectionState.Closed)
                    sqlComm.Connection.Open();
                //执行SQL命令（增删改等）
                sqlComm.ExecuteNonQuery();
                //关闭连接
                sqlComm.Connection.Close();
                return "操作完成！";
            }
            catch (Exception ex)
            {
                sqlComm.Connection.Close();
                return ex.Message;
            }
        }

        public DataTable getDT(string strSql)
        {
            try
            {
                //创建一个Command对象并赋值
                SqlCommand sqlComm = new SqlCommand();
                sqlComm.Connection = getSqlConn();
                sqlComm.CommandType = CommandType.Text;
                sqlComm.CommandText = strSql;
                //用Command对象实例化SqlDataAdapter对象
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlComm);
                //实例化一个DataTable
                DataTable dt = new DataTable();
                //执行封装在SqlDataAdapter中的SQL语句，把执行结果集填充到DataTable中
                sqlDA.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        public Boolean updateRecordWithSQL(string Ls_SqlCmd)
        {
            //调用连接对象创建方法创建连接对象
            //SqlConnection conn = getSqlConn();
            //创建Command对象，为传递SQL命令做准备
            SqlCommand sqlComm = new SqlCommand();
            //把连接对象赋值给Command的Connection属性，建立Command与Connection对象的绑定
            sqlComm.Connection = getSqlConn();
            //指定Command的命令类型的是SQL语句
            sqlComm.CommandType = CommandType.Text;
            //SQL命令赋值给Command
            sqlComm.CommandText = Ls_SqlCmd;
            try
            {
                //判断连接对象是否打开，如果未打开，打开之
                if (sqlComm.Connection.State == ConnectionState.Closed)
                    sqlComm.Connection.Open();
                //执行SQL命令（增删改等）
                sqlComm.ExecuteNonQuery();
                //关闭连接
                sqlComm.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                sqlComm.Connection.Close();
                return false;
            }
        }
    }
    public class CommLIb
    {
        DBCommLIb DBCLib = new DBCommLIb();

        public Literal MsgBox(string Msg)
        {
            Literal litMsgBox = new Literal();
            litMsgBox.Text = "<script>alert('" + Msg + "')</script>";
            return litMsgBox;
        }
        public Boolean fillComb(string Ls_SqlCmd, DropDownList ddl)
        {
            try
            {
                DataTable dt = DBCLib.getDT(Ls_SqlCmd, new string[,] { { "-1", "" } });
                ddl.DataSource = dt.DefaultView;
                ddl.DataTextField = dt.Columns[1].Caption.Trim();
                ddl.DataValueField = dt.Columns[0].Caption.Trim();
                ddl.DataBind();
                return true;
            }
            catch { return false; }
        }

        public Boolean FillListBox(string Ls_SqlCmd, ListBox clb)
        {
            try
            {
                DataTable dt = DBCLib.getDT(Ls_SqlCmd, new string[,] { { "-1", "" } });
                clb.DataSource = dt.DefaultView;
                clb.DataTextField = dt.Columns[1].Caption.Trim();
                clb.DataValueField = dt.Columns[0].Caption.Trim();
                clb.DataBind();
                return true;
            }
            catch { return false; }
        }

        public Boolean FillCheckedListBox(string Ls_SqlCmd, CheckBoxList clb)
        {
            try
            {
                DataTable dt = DBCLib.getDT(Ls_SqlCmd, new string[,] { { "-1", "" } });
                clb.DataSource = dt.DefaultView;
                clb.DataTextField = dt.Columns[1].Caption.Trim();
                clb.DataValueField = dt.Columns[0].Caption.Trim();
                clb.DataBind();
                return true;
            }
            catch { return false; }
        }


        private static string String2Json(String s)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                char c = s.ToCharArray()[i];
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\""); break;
                    case '\\':
                        sb.Append("\\\\"); break;
                    case '/':
                        sb.Append("\\/"); break;
                    case '\b':
                        sb.Append("\\b"); break;
                    case '\f':
                        sb.Append("\\f"); break;
                    case '\n':
                        sb.Append("\\n"); break;
                    case '\r':
                        sb.Append("\\r"); break;
                    case '\t':
                        sb.Append("\\t"); break;
                    default:
                        sb.Append(c); break;
                }
            }
            return sb.ToString();
        }
        private static string StringFormat(string str, Type type)
        {
            if (type == typeof(string))
            {
                str = String2Json(str);
                str = "\"" + str + "\"";
            }
            else if (type == typeof(DateTime))
            {
                str = "\"" + str + "\"";
            }
            else if (type == typeof(bool))
            {
                str = str.ToLower();
            }
            else if (type != typeof(string) && string.IsNullOrEmpty(str))
            {
                str = "\"" + str + "\"";
            }
            return str;
        }
        public static string ToJson(DataTable dt)
        {
            System.Text.StringBuilder jsonString = new System.Text.StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = StringFormat(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append(strValue + ",");
                    }
                    else
                    {
                        jsonString.Append(strValue);
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }

        //{"sum":{"spid":"11111","dt":"2015-12-02","tc":"01","wc":"12","maxT":"23.5","minT":"10.2","wk":"23.4"},"mac": [{"mcid":"22"},{"mcid":"23"}],"mat":[{"mtid":"22","qty":"23"},{"mtid":"23","qty":"23"}],"wk":[{"sfid":"111"},{"sfid":"112"}]}
        public string NewTask(string SGSC)
        {
            string msg = "";
            System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            try
            {
                Dictionary<string, object> JsonStr = jss.DeserializeObject(SGSC) as Dictionary<string, object>;
                //读取sum数据
                string[] summary = (JsonStr["SUM"] as Dictionary<string, object>).Values.Select(o => o.ToString()).ToArray<string>();
                //读取mac数据     
                object[] objTemp = JsonStr["mac"] as object[];
                string[] mac = new string[objTemp.Length];
                int i = 0;
                foreach (Dictionary<string, object> obj in objTemp)
                {
                    mac[i++] = obj["mcid"].ToString();
                }

                //读取mat数据
                objTemp = JsonStr["mat"] as object[];
                string[,] mat = new string[objTemp.Length, 2];
                i = 0;
                foreach (Dictionary<string, object> obj in objTemp)
                {
                    mat[i, 0] = obj["mtid"].ToString();
                    mat[i++, 1] = obj["qty"].ToString();
                }

                //读取wk数据
                objTemp = JsonStr["wk"] as object[];
                string[] wk = new string[objTemp.Length];
                i = 0;
                foreach (Dictionary<string, object> obj in objTemp)
                {
                    wk[i++] = obj["sfid"].ToString();
                }
                return "true";
            }
            catch
            {
                return "false";
            }
        }
    }
}