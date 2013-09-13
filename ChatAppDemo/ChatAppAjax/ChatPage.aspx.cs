using System;

namespace ChatAppAjax
{
    public partial class ChatPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["ChatUserID"] == null || Session["ChatUsername"] == null || Request["roomId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    string roomId = Request["roomID"];
                    lblRoomID.Text = roomId;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}