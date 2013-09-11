<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ChatAppAjax._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            text-decoration: underline;
            height: 47px;
        }
    </style>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/javascript">

        function CallFarji() {
            var dd = true;
            $.ajax({
                type: "POST",
                contentType: "application/json",
                data: '{uid:"' + dd + '"}',
                dataType: "json",
                url: "http://localhost:51048/MyService.asmx/CheckFarji",
                success: function(data, status) {
                    alert(data.d);
                },
                error: function(err) {
                    alert('faliure');
                    alert(err);
                }
            });
        }
        
        function LoginUser() {

            if (ValidateUser()) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    data: '{uid:"' + document.getElementById('txtLoginUI').value + '",fname: "' + document.getElementById('txtLoginFN').value + '", lname:"' + document.getElementById('txtLoginLN').value + '", uname:"' + document.getElementById('txtLoginUN').value + '", pass:"' + document.getElementById('txtLoginPW').value + '", sex:"' + document.getElementById('dropdwnLoginSEX').value + '"}',
                    dataType: "json",
                    url: "http://localhost:51048/MyService.asmx/InsertNewUser",
                    success: function (data, status) {
                    },
                    error: function (err) {
                        alert('faliure');
                        alert(err);
                    }
                });
            }
        }

        function ValidateUser() {
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 584px">
        <br />
        <br />
        <table class="style1" width="50%" style="background-color: #CCFF99">
            <tr>
                <td colspan="2" align="center" class="style2">
                    <strong>Welcome to Chat App</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Usename"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="butSubmit" runat="server" OnClick="butSubmit_Click" Text="Submit" />
                </td>
                <td>
                    <asp:Button ID="butNewUser" runat="server" OnClick="butNewUser_Click" Text="New User" />
                </td>
            </tr>
            <tr>
                <td>
                    <input type="button" value="Check" onclick="javascript:CallFarji();" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblLoginError" runat="server" Text="Sorry, Username or password is wrong."
            Visible="False"></asp:Label>
        <asp:Panel ID="panelNewUser" runat="server" Height="299px" Style="margin-top: 68px"
            Visible="False" Width="504px" BackColor="#CCFF99">
            <br />
            User Id&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoginUI" runat="server"></asp:TextBox>
            <br />
            <br />
            First Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoginFN" runat="server"></asp:TextBox>
            <br />
            <br />
            Last Name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoginLN" runat="server"></asp:TextBox>
            <br />
            <br />
            UserName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoginUN" runat="server"></asp:TextBox>
            <br />
            <br />
            Password&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtLoginPW" runat="server"></asp:TextBox>
            <br />
            <br />
            Sex&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dropdwnLoginSEX" runat="server">
                <asp:ListItem Selected="True">M</asp:ListItem>
                <asp:ListItem>F</asp:ListItem>
            </asp:DropDownList>
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" id="butSaveNewUser" value="Save" onclick="javascript:LoginUser();" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </asp:Panel>
    </div>
    </form>
</body>
</html>
