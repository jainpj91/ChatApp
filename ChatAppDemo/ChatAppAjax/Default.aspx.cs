using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ChatAppAjax
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["val"] != null && Request.QueryString["val"].Equals("logout"))
            {
                Session["ChatUserID"] = null;
                Session["ChatUsername"] = null;
            }

            if (Session["ChatUserID"] != null && Session["ChatUsername"] != null)
            {
                Login1_LoggedIn();
            }
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            ChatAppDemoEntities db = new ChatAppDemoEntities();
            var users = from u in db.UserInfoes
                         where u.Username == txtUserName.Text
                         && u.Password == txtPass.Text
                         select u;

            if (users.Count() > 0)
            {
                Session["ChatUserID"] = users.First().UserId;
                Session["ChatUsername"] = users.First().Username;
                Session["LoggedIn"] = false;
                Login1_LoggedIn();
            }
            else
            {
                lblLoginError.Visible = true;
            }
        }

        protected void Login1_LoggedIn()
        {
            Response.Redirect("ChatPage.aspx?roomId=1");
        }

        protected void butNewUser_Click(object sender, EventArgs e)
        {
            panelNewUser.Visible = true;
        }
    }
}