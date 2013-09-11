<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatPage.aspx.cs" Inherits="ChatAppAjax.ChatPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        var chatUsrId = '<%= Session["ChatUserID"] %>';

        function SetScrollPosition() {
            var div = document.getElementById('litMessages');
            div.scrollTop = 100000000000;
        }

        $(function () {
            var chatUsrId = '<%= Session["ChatUserID"] %>';
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{text:' + null + ', roomId:"' + document.getElementById("lblRoomID").value + '",chatUsrId:"' + chatUsrId + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/InsertMess",
                success: function (data, status) {
                },
                error: function (err) {
                    alert('faliure');
                    alert(err);
                }
            });
        });

        var dtChck = new Date();
        var dt = dtChck.getDate().toString().split(' ');
        var mh = dtChck.getMonth().toString().split(' ');
        var hr = dtChck.getHours().toString().split(' ');
        var mn = dtChck.getMinutes().toString().split(' ');
        var sc = dtChck.getSeconds().toString().split(' ');

        if (dt[0].length == 1)
            dt[0] = '0' + dt[0];

        if (mh[0].length == 1)
            mh[0] = '0' + mh[0];

        if (hr[0].length == 1)
            hr[0] = '0' + hr[0];

        if (mn[0].length == 1)
            mn[0] = '0' + mn[0];

        if (sc[0].length == 1)
            sc[0] = '0' + sc[0];

        var sendDt = dtChck.getFullYear() + '-' + '09' + '-' + dt[0] + ' ' + hr[0] + ':' + mn[0] + ':' + sc[0];

        setInterval(function () {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{roomId:"' + document.getElementById("lblRoomID").value + '",dtCheck:"' + sendDt + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/GetMess",
                success: function (data, status) {
                    PrepareDate();
                    SetListData(data.d);
                },
                error: function (err) {
                    alert('faliure');
                }
            });
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{roomId:"' + document.getElementById("lblRoomID").value + '",chatUsrId:"' + chatUsrId + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/GetLoggenInUser",
                success: function (data, status) {
                    SetLoggedInListData(data.d)
                },
                error: function (err) {
                    alert('Failure GetLoggenInUser' + err.d);
                }
            });
        }, 1000);

        function SetLoggedInListData(data) {

            if (data != null || typeof (data) != 'undefined' || data != '') {

                var litU = document.getElementById("litUsers");
                var present = false;
                for (var count = 0; count < data.length; count++) {

                    for (var i = 0; i < litU.options.length; ++i) {
                        if (litU.options[i].value == data[count]) {
                            present = true;
                        }
                    }

                    if (!present) {
                        var oOption = document.createElement("OPTION");
                        litU.options.add(oOption);
                        oOption.text = data[count];
                        oOption.value = data[count];
                    }
                }
            }
        }

        function SetListData(data) {
            if (data != null || typeof (data) != 'undefined' || data != '') {
                var setData = JSON.parse(data);
                var litM = document.getElementById("litMessages");

                for (var count = 0; count < setData.length; count++) {
                    var oOption = document.createElement("OPTION");
                    litM.options.add(oOption);
                    var dd = setData[count] + ' : ' + setData[++count];
                    oOption.text = dd;
                    oOption.value = dd;
                }
            }
        }

        function PrepareDate() {
            dtChck = new Date();
            dt = dtChck.getDate().toString().split(' ');
            mh = dtChck.getMonth().toString().split(' ');
            hr = dtChck.getHours().toString().split(' ');
            mn = dtChck.getMinutes().toString().split(' ');
            sc = dtChck.getSeconds().toString().split(' ');

            if (dt[0].length == 1)
                dt[0] = '0' + dt[0];

            if (mh[0].length == 1)
                mh[0] = '0' + mh[0];

            if (hr[0].length == 1)
                hr[0] = '0' + hr[0];

            if (mn[0].length == 1)
                mn[0] = '0' + mn[0];

            if (sc[0].length == 1)
                sc[0] = '0' + sc[0];

            sendDt = dt[0] + '-' + '09' + '-' + dtChck.getFullYear() + ' ' + hr[0] + ':' + mn[0] + ':' + sc[0];
        }

        function CallAjax() {

            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{text:"' + document.getElementById("txtMessage").value + '", roomId:"' + document.getElementById("lblRoomID").value + '",chatUsrId:"' + chatUsrId + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/InsertMess",
                success: function (data, status) {
                    alert(data.d);
                },
                error: function (err) {
                    alert('faliure');
                    alert(err);
                }
            });
        }

        function LogoutUser() {
            
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{roomId:"' + document.getElementById("lblRoomID").value + '",chatUsrId:"' + chatUsrId + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/LogoutUser",
                success: function (data, status) {
                <%Session["ChatUserID"] = null;%>
                <%Session["ChatUsername"] = null;%>
                    alert('You have been successfully logged out!!');
                    window.location = "http://localhost:51048/Default.aspx?val=logout";
                },
                error: function (err) {
                    alert('faliure');
                }
            });
        }
    </script>
</head>
<body style="background-color: gainsboro;" onload="SetScrollPosition()" onunload="LogoutUser()">
    <form id="form1" defaultbutton="Btn_Send" defaultfocus="txtMessage" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:ListBox ID="litMessages" runat="server" Style="height: 300px; width: 474px;
                float: left" AutoPostBack="false"></asp:ListBox>
            <asp:ListBox ID="litUsers" runat="server" Style="height: 300px; width: 100px; float: right">
            </asp:ListBox>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:TextBox ID="txtMessage" runat="server" Width="331px">
    </asp:TextBox>
    <asp:Button ID="Btn_Send" runat="server" Text="Send" OnClick="Btn_Send_Click" />
    <asp:Label ID="Label1" runat="server" Text="Color: "></asp:Label>
    <asp:DropDownList ID="ddlColor" runat="server">
    </asp:DropDownList>
    <asp:Button ID="butLogout" runat="server" Text="Logout" OnClick="butLogout_Click" />
    <asp:TextBox ID="lblRoomID" runat="server"></asp:TextBox>
    <input type="button" value="Click Me" onclick="javascript:CallAjax();" />
    <input type="button" value="Log Me Out" onclick="javascript:LogoutUser();" />
    </form>
    <div id="testdiv">
    </div>
</body>
</html>
