<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AS_Assign_Final.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 569px;
        }
        .auto-style3 {
            width: 187px;
        }
        .auto-style4 {
            text-align: center;
        }
        </style>

    <script src="https://www.google.com/recaptcha/api.js?render=6LeYPkQaAAAAAPkP_W6Sx3sAy3w8B5uHX1_qVxHX"></script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset class="auto-style2">
                <header>
                    <h1>Login</h1>
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style3">Email: </td>
                            <td>
                                <br />
                                <asp:TextBox ID="tb_login_email" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style3">Password: </td>
                            <td>
                                <br />
                                <asp:TextBox ID="tb_login_pwd" runat="server" TextMode="Password"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4" colspan="2">
                                <br />
                                <asp:Label ID="lbl_msg" runat="server"></asp:Label>
                                
                                <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response" />
                                <br />
                                <br />
                                <asp:Button ID="btn_login" runat="server" OnClick="btn_login_Click" Text="Login" />
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                    If you do not have an account, register at :
                    <asp:Button ID="btn_register" runat="server" OnClick="btn_register_Click" Text="Register" />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </header>
            </fieldset>
        </div>
    </form>

    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LeYPkQaAAAAAPkP_W6Sx3sAy3w8B5uHX1_qVxHX', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
  
</body>
</html>
