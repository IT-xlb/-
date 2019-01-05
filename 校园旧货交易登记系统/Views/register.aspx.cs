using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace 校园旧货交易登记系统.Views
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btsubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx?", false);
        }
    }
}