using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;  //引入数据库的命名空间
using System.Collections;

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
