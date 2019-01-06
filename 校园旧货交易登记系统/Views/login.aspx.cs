using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 校园旧货交易登记系统.App_Code;

namespace 校园旧货交易登记系统.Views
{
    public partial class login : System.Web.UI.Page
    {
        CommLIb CLib = new CommLIb();
        DBCommLIb dbCLib = new DBCommLIb();
        校园旧货交易登记系统.Models.NetEntities1 net = new Models.NetEntities1();        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx?", false);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = tbusername.Text.Trim();
            string pwd = tbpsw.Text.Trim();
            var info = net.sys_user.FirstOrDefault();
            info = net.sys_user.Where(o => o.user_name == username).FirstOrDefault();
            if (info.user_password==pwd)
            {
                Response.Redirect("main.aspx?", false);
            }
            else
                Response.Write(@"<script>alert('用户名或者密码错误!');</script>");
        }
    }
}