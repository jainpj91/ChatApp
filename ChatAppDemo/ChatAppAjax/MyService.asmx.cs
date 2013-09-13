using System;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Configuration;

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
    public class MyService : WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            var val = new string[2];
            var js = new JavaScriptSerializer();
            return js.Serialize(val);
        }

        [WebMethod]
        public string Hi()
        {
            var js = new JavaScriptSerializer();
            return js.Serialize("hi");
        }

        [WebMethod]
        public void InsertMess(string text, string roomId, string chatUsrId)
        {
            try
            {
                var dbChatAppDemoEntities = new ChatAppDemoEntities();

                var message = new Message { RoomId = roomId, UserId = chatUsrId };

                if (text == null || text == "null")
                {
                    message.Text = ConfigurationManager.AppSettings["ChatLoggedInText"] + "" + DateTime.Now;
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
        }

        [WebMethod]
        public string GetMess(string roomId, string dtCheck)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(dtCheck);
                var js = new JavaScriptSerializer();
                var dbChatAppDemoEntities = new ChatAppDemoEntities();

                var mess = (from message in dbChatAppDemoEntities.Messages
                            where message.RoomId == roomId
                            && message.TimeEntered >= dt
                            orderby message.TimeEntered descending
                            select message).Take(20).OrderBy(messages => messages.TimeEntered);

                if (mess.Count() != 0)
                {
                    var arrMess = new string[mess.Count(), 2];
                    var row = 0;

                    foreach (var message in mess)
                    {
                        Console.WriteLine(message.TimeEntered);
                        Message message1 = message;
                        var usr = (from usrInfo in dbChatAppDemoEntities.UserInfoes
                                   where usrInfo.UserId == message1.UserId
                                   select usrInfo.Username);

                        arrMess[row, 0] = usr.First();
                        arrMess[row, 1] = message.Text;
                        row++;
                    }
                    return js.Serialize(arrMess);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        [WebMethod]
        public string GetLoggenInUser(string roomId, string chatUsrId)
        {
            try
            {
                var js = new JavaScriptSerializer();
                var dbChatAppDemoEntities = new ChatAppDemoEntities();

                var user = from u in dbChatAppDemoEntities.LoggedInUseIds
                           where u.UserId == chatUsrId
                           && u.RoomId == roomId
                           select u;

                if (!user.Any())
                {
                    var loggedInUseIds = new LoggedInUseId
                    {
                        UserId = chatUsrId,
                        RoomId = roomId,
                        LoggedInUserId = chatUsrId
                    };

                    dbChatAppDemoEntities.AddToLoggedInUseIds(loggedInUseIds);
                    dbChatAppDemoEntities.SaveChanges();
                }
                
                var loggedInUsers = dbChatAppDemoEntities.LoggedInUseIds.Where(logInUser => logInUser.RoomId == roomId);

                var arrSendLogInUid = new string[loggedInUsers.Count(), 2];
                var i = 0;

                foreach (var loggedInUser in loggedInUsers)
                {
                    arrSendLogInUid[i, 0] = loggedInUser.UserId;
                    var sdf = loggedInUser.UserId;

                    var usr = (dbChatAppDemoEntities.UserInfoes.Where(usrInfo => usrInfo.UserId == sdf)
                        .Select(usrInfo => usrInfo.Username));

                    arrSendLogInUid[i, 1] = usr.First();

                    i++;
                }
                return js.Serialize(arrSendLogInUid);
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
                var dbChatAppDemoEntities = new ChatAppDemoEntities();
                string chatUserId = chatUsrId;

                var loggedInUser = (dbChatAppDemoEntities.LoggedInUseIds.Where(lIu => lIu.UserId == chatUserId
                                                                                      && lIu.RoomId == roomId)).First();

                dbChatAppDemoEntities.DeleteObject(loggedInUser);
                dbChatAppDemoEntities.SaveChanges();

                InsertMess("Just Logged Out! " + DateTime.Now.ToString(CultureInfo.InvariantCulture), roomId, chatUsrId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        [WebMethod]
        public bool InsertNewUser(string uid, string fname, string lname, string uname, string pass, string sex)
        {
            try
            {
                var dbChatAppDemoEntities = new ChatAppDemoEntities();
                var userInfo = new UserInfo
                {
                    UserId = uid,
                    Firstname = fname,
                    Lastname = lname,
                    Username = uname,
                    Password = pass,
                    Sex = sex
                };

                dbChatAppDemoEntities.AddToUserInfoes(userInfo);
                dbChatAppDemoEntities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}
