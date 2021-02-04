<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Success.aspx.cs" Inherits="AS_Assign_Final.Success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 35%;
            height: 140px;
        }
        .auto-style2 {
            width: 350px;
        }
        .auto-style3 {
            width: 350px;
            height: 26px;
        }
        .auto-style4 {
            width: 605px;
        }
        .auto-style5 {
            width: 605px;
            height: 26px;
        }
        .auto-style6 {
            width: 350px;
            height: 25px;
        }
        .auto-style7 {
            width: 605px;
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <fieldset>
                <header>
                    <h1>My Account</h1>
                    <br />
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style2">Last Name:</td>
                            <td class="auto-style4">
                                <asp:Label ID="lbl_lname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">First Name:</td>
                            <td class="auto-style4">
                                <asp:Label ID="lbl_fname" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">Email:</td>
                            <td class="auto-style7">
                                <asp:Label ID="lbl_email" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">&nbsp;</td>
                            <td class="auto-style4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style3"></td>
                            <td class="auto-style5"></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Button ID="btnchngPwd" runat="server" OnClick="btnchngPwd_Click" Text="Change Password" />
                    <br />
                    <br />
                    <asp:Button ID="btn_logout" runat="server" OnClick="btn_logout_Click" Text="Logout" />
                </header>
            </fieldset>

        </div>
    </form>
</body>
</html>
