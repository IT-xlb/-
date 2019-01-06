using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using 校园旧货交易登记系统.App_Code;

namespace 校园旧货交易登记系统.Views
{
    public partial class register : System.Web.UI.Page
    {
        CommLIb CLib = new CommLIb();
        DBCommLIb dbCLib = new DBCommLIb();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btsubmit_Click(object sender, EventArgs e)
        {
            if (tbuserpwd.Text.Trim() == null || tbusername.Text.Trim() == null || tbuseraddress.Text.Trim() == null || tbusertele.Text.Trim() == null)
                Response.Write(@"<script>alert('注册表不完整!');</script>");
            else if (tbuserpwd.Text.Trim().Length < 6)
                Response.Write(@"<script>alert('密码小于6位!');</script>");
            else if (tbusertele.Text.Trim().Length != 11)
                Response.Write(@"<script>alert('手机号码位数不对!');</script>");
            else
            {
                string InsertSQL = "Insert sys_user(user_name,user_password,user_tele,user_address,user_sex) values('" + tbusername.Text.Trim() + "','" + tbuserpwd.Text.Trim() + "','" + tbusertele.Text.Trim() + "','"+
                tbuseraddress.Text.Trim() + "','" + rblGender.SelectedValue +"')";
                if(dbCLib.updateRecordWithSQL(InsertSQL))
                    Response.Redirect("login.aspx?", false);
            }                
        }
       
    }
}