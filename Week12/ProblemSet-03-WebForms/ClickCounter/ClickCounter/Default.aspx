<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClickCounter.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Name: "></asp:Label>
        <asp:TextBox ID="nameTextBox" runat="server" Height="16px" Width="121px"
                     style ="margin: 5px, 0, 0, 5px;"></asp:TextBox>
        <asp:RequiredFieldValidator ID="nameRequiredValidator" runat="server"
                                    ControlToValidate="nameTextBox"
                                    ErrorMessage="You must write your name!" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="Submit" />
    
    </div>
    </form>
</body>
</html>
