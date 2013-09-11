using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace ChatAppDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        SqlConnection conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(
                @"Data Source=MINDFIRE11-PC\SQLEXPRESS;Initial Catalog=ChatAppDemo;Integrated Security=True;Pooling=False");
        }

        protected void butSubmit_Click(object sender, EventArgs e)
        {
            ChatAppDemoEntities db = new ChatAppDemoEntities();
            var users = (from u in db.UserInfoes
                         where u.Username == txtUserName.Text
                         && u.Password == txtPass.Text
                         select u).First();

            if (users != null)
            {
                Session["ChatUserID"] = users.UserId;
                Session["ChatUsername"] = users.Username;
                Session["LoggedIn"] = false;
                Login1_LoggedIn();
            }
        }
        //protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        //{
        //    ChatAppDemoEntities db = new ChatAppDemoEntities();
        //    var users = (from u in db.Users
        //                 where u.Username == txtUserName.Text
        //                 && u.Password == txtPass.Text
        //                 select u).SingleOrDefault();

        //    if (users != null)
        //    {
        //        e.Authenticated = true;
        //        Session["ChatUserID"] = users.UserID;
        //        Session["ChatUsername"] = users.Username;
        //    }
        //    else
        //    {
        //        e.Authenticated = false;
        //    }
        //}

        protected void Login1_LoggedIn()
        {
            Response.Redirect("ChatPage.aspx?roomId=1");
        }

        protected void butNewUser_Click(object sender, EventArgs e)
        {
            panelNewUser.Visible = true;
        }

        protected void butSaveNewUser_Click(object sender, EventArgs e)
        {

        }
    }
}