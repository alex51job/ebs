<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pwService.aspx.cs" Inherits="MyProject.Register.pwService" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body >
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                please input your mail address :
                <asp:TextBox ID="txtMail" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMail" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:Button ID="btnSend"
                    runat="server" Text="Send" onclick="btnSend_Click" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
