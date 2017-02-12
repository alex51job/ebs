<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="applyNew.aspx.cs" Inherits="ebs.Register.applyNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td align="right">
                <asp:Label ID="Label1" runat="server" Text="UserName :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="*"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label2" runat="server" Text="Password :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPsd" runat="server" TextMode="Password" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPsd" ErrorMessage="*"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
         <tr>
            <td align="right">
                <asp:Label ID="Label5" runat="server" Text="Role :"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dpRole" runat="server">
                    <asp:ListItem Value="1">Admin</asp:ListItem>
                    <asp:ListItem Value="2">Manager</asp:ListItem>
                    <asp:ListItem Value="3">Sales</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label3" runat="server" Text="MailAddress :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMail" runat="server" Width="150px"></asp:TextBox><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMail" ErrorMessage="*"
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="Label4" runat="server" Text="Phone :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" 
                    style="width: 60px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
