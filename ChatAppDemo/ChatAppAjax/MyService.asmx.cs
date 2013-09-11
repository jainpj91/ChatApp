using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Text;

namespace ChatAppAjax
{
    /// <summary>
    /// Summary description for MyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            string[] val = new string[2];
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(val);
        }

        [WebMethod]
        public string Hi()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize("hi");
        }

        [WebMethod]
        public string InsertMess(string text, string roomId, string chatUsrId)
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();

                Message message = new Message();
                message.RoomId = roomId;
                message.UserId = chatUsrId;

                if (text == null || text == "null")
                {
                    message.Text = ConfigurationManager.AppSettings["ChatLoggedInText"] + "" + DateTime.Now.ToString();
                }
                else
                {
                    message.Text = text;
                }
                message.Color = "grey";
                message.ToUserId = "TEST";

                message.TimeEntered = DateTime.Now;
                dbChatAppDemoEntities.AddToMessages(message);
                dbChatAppDemoEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();// Use this when formatting the data as JSON
            return js.Serialize("hi");
        }

        [WebMethod]
        public string GetMess(string roomId, string dtCheck)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtCheck);
                JavaScriptSerializer js = new JavaScriptSerializer();
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();

                var mess = (from message in dbChatAppDemoEntities.Messages
                            where message.RoomId == roomId
                            && message.TimeEntered >= dt
                            orderby message.TimeEntered descending
                            select message).Take(20).OrderBy(messages => messages.TimeEntered);

                if (mess.Count() != 0)
                {
                    string[,] arrMess = new string[mess.Count(), 2];
                    var row = 0;

                    foreach (var message in mess)
                    {
                        Console.WriteLine(message.TimeEntered);
                        arrMess[row, 0] = message.UserId;
                        arrMess[row, 1] = message.Text;
                        row++;
                    }
                    return js.Serialize(arrMess);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        [WebMethod]
        public string[] GetLoggenInUser(string roomId, string chatUsrId)
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();
                
                var user = from u in dbChatAppDemoEntities.LoggedInUseIds
                            where u.UserId == chatUsrId
                            && u.RoomId == roomId
                            select u;

                if (user.Count() == 0)
                {
                    LoggedInUseId LoggedInUseIds = new LoggedInUseId();
                    LoggedInUseIds.UserId = chatUsrId;
                    LoggedInUseIds.RoomId = roomId;
                    LoggedInUseIds.LoggedInUserId = chatUsrId;

                    dbChatAppDemoEntities.AddToLoggedInUseIds(LoggedInUseIds);
                    dbChatAppDemoEntities.SaveChanges();
                }

                StringBuilder sb = new StringBuilder();

                var loggedInUsers = from logInUser in dbChatAppDemoEntities.LoggedInUseIds
                                    where logInUser.RoomId == roomId
                                    select logInUser;

                string[] arrSendLogInUID = new string[loggedInUsers.Count()];
                var i = 0;
                foreach (var loggedInUser in loggedInUsers)
                {
                    arrSendLogInUID[i] = loggedInUser.UserId;
                    i++;
                }
                return arrSendLogInUID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        [WebMethod]
        public void LogoutUser(string roomId, string chatUsrId)
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();
                string chatUserID = chatUsrId;

                var loggedInUser = (from lIU in dbChatAppDemoEntities.LoggedInUseIds
                                    where lIU.UserId == chatUserID
                                    && lIU.RoomId == roomId
                                    select lIU).First();

                dbChatAppDemoEntities.DeleteObject(loggedInUser);
                dbChatAppDemoEntities.SaveChanges();

                this.InsertMess("Just Logged Out! " + DateTime.Now.ToString(), roomId, chatUsrId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [WebMethod]
        public string InsertNewUser(string uid, string fname, string lname, string uname, string pass, string sex)
        {
            try
            {
                ChatAppDemoEntities dbChatAppDemoEntities = new ChatAppDemoEntities();
                UserInfo userInfo = new UserInfo();
                userInfo.UserId = uid;
                userInfo.Firstname = fname;
                userInfo.Lastname = lname;
                userInfo.Password = pass;
                userInfo.Sex = sex;

                dbChatAppDemoEntities.AddToUserInfoes(userInfo);
                dbChatAppDemoEntities.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();// Use this when formatting the data as JSON
            return js.Serialize("hi");
        }

        [WebMethod]
        public string CheckFarji(bool uid)
        {
            return "Prabhat Jain";
        }
    }
}
