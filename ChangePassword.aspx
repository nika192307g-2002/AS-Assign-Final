<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="AS_Assign_Final.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

     <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_newPwd.ClientID %>').value;
            if (str.length < 8) {
                document.getElementById("pwdchecker").innerHTML = "Password Length must be at least 8 characters";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password require atleast 1 number";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_number")
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password Requires At Least 1 Uppercase Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_lowercase");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password Requires At Least 1 Lowercase Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_uppercase");
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("pwdchecker").innerHTML = "Password Requires At Least 1 Special Character";
                document.getElementById("pwdchecker").style.color = "Red";
                return ("no_special");
            }

            document.getElementById("pwdchecker").innerHTML = "Excellent!"
            document.getElementById("pwdchecker").style.color = "Blue";
        }
     </script>

    <style type="text/css">
        .auto-style1 {
            width: 29%;
            height: 289px;
        }
        .auto-style2 {
            width: 147px;
        }
        .auto-style3 {
            text-align: center;
        }
        .auto-style4 {
            width: 463px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>


                <h1>Change Password</h1>
                <br />
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style2">Email:</td>
                        <td class="auto-style4">
                            <br />
                            <asp:Label ID="lbl_email" runat="server"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">New Password:</td>
                        <td class="auto-style4">
                            <br />
                            <asp:TextBox ID="tb_newPwd" runat="server" TextMode="Password"></asp:TextBox>
                            &nbsp;<asp:Label ID="pwdchecker" runat="server"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Confirm New Password:</td>
                        <td class="auto-style4">
                            <br />
                            <asp:TextBox ID="tb_cfmNewPwd" runat="server" TextMode="Password"></asp:TextBox>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3" colspan="2">
                            <br />
                            <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label>
                            <br />
                            <br />
                            <asp:Button ID="btnSavePwd" runat="server" OnClick="btnSavePwd_Click" Text="Save New Password" />
                            <br />
                        </td>
                    </tr>
                </table>
                <br />


            </fieldset>
        </div>
    </form>
</body>
</html>
