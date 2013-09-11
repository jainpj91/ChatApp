<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChatPage.aspx.cs" Inherits="ChatAppDemo.ChatPage"
    MaintainScrollPositionOnPostback="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript">
        function SetScrollPosition() {
            var div = document.getElementById('divMessages');
            div.scrollTop = 100000000000;
        }

        function LogMeOut() {
            LogOutUserCallBack();
        }

        function CallAjax() {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{}',
                dataType: "json",
                url: "http://localhost:57716/MyService.asmx/HelloWorld",
                success: function (data, status) {
                    alert(data.d);
                },
                error: function (err) {
                    alert('faliure');
                    alert(err);
                }
            });
            var litM = document.getElementById("litMessages");
            var oOption = document.createElement("OPTION");
            litM.options.add(oOption);
            oOption.text = "Hello"
            oOption.value = "Hello";
        }
        
    </script>
</head>
<body style="background-color: gainsboro;" onload="SetScrollPosition()" onunload="LogMeOut()">
    <form id="form1" defaultbutton="Btn_Send" defaultfocus="txtMessage" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" UpdateMode="Always">
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
    <asp:Label ID="lblRoomID" runat="server" Text="Label" Visible="False"></asp:Label>
    <%--<asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000">
    </asp:Timer>--%>
    <input type="button" value="Click Me" onclick="javascript:CallAjax();" />
    </form>
</body>
</html>
