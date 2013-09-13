using System;
using System.Linq;

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
            var db = new ChatAppDemoEntities();
            var users = db.UserInfoes.Where(u => u.Username == txtUserName.Text
                                                 && u.Password == txtPass.Text);

            if (users.Any())
            {
                Session["ChatUserID"] = users.First().UserId;
                Session["ChatUsername"] = users.First().Username;
                Session["LoggedIn"] = false;
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "my", "child_open();", true);
                Login1_LoggedIn();
            }
            else
            {
                lblLoginError.Visible = true;
            }
        }
        
        protected void Login1_LoggedIn()
        {
            

            //Response.Redirect("ChatPage.aspx?roomId=1");
        }

        protected void butNewUser_Click(object sender, EventArgs e)
        {
            panelNewUser.Visible = true;
        }

    }
}