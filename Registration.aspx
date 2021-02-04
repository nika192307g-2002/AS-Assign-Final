<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="AS_Assign_Final.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_password.ClientID %>').value;
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
            width: 100%;
        }
        .auto-style6 {
            height: 26px;
            width: 215px;
        }
        .auto-style7 {
            width: 215px;
        }
        .auto-style8 {
            width: 890px;
        }
        .auto-style11 {
            text-align: center;
        }
        .auto-style13 {
            width: 215px;
            height: 50px;
        }
        .auto-style15 {
            width: 215px;
            height: 54px;
        }
        .auto-style16 {
            height: 26px;
            width: 105px;
        }
        .auto-style18 {
            width: 105px;
            height: 50px;
        }
        .auto-style19 {
            width: 105px;
            height: 54px;
        }
        .auto-style20 {
            width: 105px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <header class="auto-style8">
                    <h1>Registration</h1>
                    <br />
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style16">First Name</td>
                            <td class="auto-style6">
                                <br />
                                <asp:TextBox ID="tb_fname" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Last Name</td>
                            <td class="auto-style7">
                                <br />
                                <asp:TextBox ID="tb_lname" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Date of Birth</td>
                            <td class="auto-style7">
                                <br />
                                <asp:Calendar ID="cal_dob" runat="server"></asp:Calendar>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Credit Card Number</td>
                            <td class="auto-style7">
                                <br />
                                <asp:TextBox ID="tb_cardNo" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style18">CVV</td>
                            <td class="auto-style13">
                                <br />
                                <asp:TextBox ID="tb_cardCVV" runat="server"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style19">Credit Card Expiry Date</td>
                            <td class="auto-style15">
                                <asp:Calendar ID="cal_expirydate" runat="server"></asp:Calendar>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Email Address</td>
                            <td class="auto-style7">
                                <br />
                                <asp:TextBox ID="tb_email" runat="server" type="email"></asp:TextBox>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style20">Password</td>
                            <td class="auto-style7">
                                <br />
                                <asp:TextBox ID="tb_password" runat="server" TextMode="Password" onkeyup="javascript:validate()"></asp:TextBox>
                                <br />
                                <br />
                                <asp:Label ID="pwdchecker" runat="server"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11" colspan="2">
                                <br />
                                <asp:Button ID="btn_register" runat="server" Text="Register" OnClick="btn_register_Click" />
                                <br />
                                <br />
                                <asp:Label ID="lbl_error" runat="server"></asp:Label>
                                <br />
                                <br />
                            </td>
                        </tr>
                    </table>
                    If you have an existing account, login at : <asp:Button ID="btn_login" runat="server" OnClick="btn_login_Click" Text="Login" />
                    <br />
                </header>
            </fieldset>
        </div>
    </form>
</body>
</html>
