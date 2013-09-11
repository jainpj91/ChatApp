using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;

namespace ChatAppAjax
{
    public partial class ChatPage : System.Web.UI.Page
    {
        DateTime dtCheck;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager1.RegisterAsyncPostBackControl(Btn_Send);
                litMessages.SelectedIndex = litMessages.Items.Count - 1;

                if (!IsPostBack && Session["LoggedIn"] != null && Session["LoggedIn"].ToString() != "true")
                {
                    dtCheck = DateTime.Now;
                    string roomID = Request["roomID"];
                    lblRoomID.Text = roomID;
                    litMessages.SelectedIndex = litMessages.Items.Count - 1;
                    this.GetLoggenInUser();
                }
                else
                {
                    dtCheck = DateTime.Now;
                    string roomID = Request["roomID"];
                    lblRoomID.Text = roomID;
                    litMessages.SelectedIndex = litMessages.Items.Count - 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void InsertMessage(string text)
        {
            try
            {
                Session["LoggedIn"] = true;
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();

                Message message = new Message();
                message.RoomId = lblRoomID.Text;
                message.UserId = Session["ChatUserID"].ToString();

                if (String.IsNullOrEmpty(text))
                {
                    message.Text = txtMessage.Text.Replace("<", "");
                    message.Color = ddlColor.SelectedValue;
                }
                else
                {
                    message.Text = text;
                    message.Color = "grey";
                }
                message.ToUserId = "TEST";

                message.TimeEntered = DateTime.Now;
                dbChatAppDemoEntities.AddToMessages(message);
                dbChatAppDemoEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetMessage()
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();

                var mess = (from message in dbChatAppDemoEntities.Messages
                            where message.RoomId == lblRoomID.Text
                            && message.TimeEntered > dtCheck
                            orderby message.TimeEntered descending
                            select message).Take(20).OrderBy(messages => messages.TimeEntered);

                dtCheck = DateTime.Now;

                if (mess != null)
                {
                    foreach (var message in mess)
                    {
                        litMessages.Items.Add(message.UserId + " : " + message.Text);
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetLoggenInUser()
        {
            try
            {
                string chatUserID = Session["ChatUserID"].ToString();

                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();
                var gg = lblRoomID.Text;

                var user = (from u in dbChatAppDemoEntities.LoggedInUseIds
                            where u.UserId == chatUserID
                            && u.RoomId == lblRoomID.Text
                            select u).First();

                if (user == null)
                {
                    LoggedInUseId LoggedInUseIds = new LoggedInUseId();
                    LoggedInUseIds.UserId = Session["ChatUserID"].ToString();
                    LoggedInUseIds.RoomId = lblRoomID.Text;
                    LoggedInUseIds.LoggedInUserId = Session["ChatUserID"].ToString();

                    dbChatAppDemoEntities.AddToLoggedInUseIds(LoggedInUseIds);
                    dbChatAppDemoEntities.SaveChanges();
                }

                StringBuilder sb = new StringBuilder();

                var loggedInUsers = from logInUser in dbChatAppDemoEntities.LoggedInUseIds
                                    where logInUser.RoomId == lblRoomID.Text
                                    select logInUser;
                int check = 0;

                foreach (var loggedInUser in loggedInUsers)
                {
                    //sb.Append("<a href='#'>" + loggedInUser.UserId + "</a><br>");
                    for (int i = 0; i < litUsers.Items.Count; i++)
                    {
                        if (litUsers.Items[i].ToString() == loggedInUser.UserId)
                        {
                            check = 1;
                        }
                    }

                    if (check == 0)
                    {
                        //litUsers.Items.Add(loggedInUser.UserId);
                    }
                }
                //litUsers.Items.Add(sb.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void Btn_Send_Click(object sender, EventArgs e)
        {
            InsertMessage(null);
            GetMessage();
            litMessages.SelectedIndex = litMessages.Items.Count - 1;

            ScriptManager1.SetFocus(txtMessage.ClientID);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            //GetMessage();
            //GetLoggenInUser();
            //litMessages.SelectedIndex = litMessages.Items.Count - 1;

            //ScriptManager1.SetFocus(txtMessage.ClientID);
        }

        protected void butLogout_Click(object sender, EventArgs e)
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();
                string chatUserID = Session["ChatUserID"].ToString();

                var loggedInUser = (from lIU in dbChatAppDemoEntities.LoggedInUseIds
                                    where lIU.UserId == chatUserID
                                    && lIU.RoomId == lblRoomID.Text
                                    select lIU).First();

                dbChatAppDemoEntities.DeleteObject(loggedInUser);
                dbChatAppDemoEntities.SaveChanges();

                litUsers.Items.Remove(loggedInUser.UserId.ToString());

                this.InsertMessage("Just Logged Out! " + DateTime.Now.ToString());
                this.GetMessage();

                Session.RemoveAll();
                Session.Abandon();

                //Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}