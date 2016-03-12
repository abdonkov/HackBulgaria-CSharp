<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClickCounter.aspx.cs" Inherits="ClickCounter.ClickCounter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="margin-left: 360px" >
            <asp:Label ID="welcomeLabel" runat="server"
                Font-Size="25" Font-Bold="true">
                Welcome, <%: Session["clickerName"] %>!
            </asp:Label>
        </div>
    </div>
    <div>
        <asp:Button ID="clickButton" runat="server" Text="Click here, please!"
                    Font-Size="20" Font-Italic="true" BackColor="SkyBlue" BorderStyle="None"
                    style="margin-left: 300px;" OnClick="clickButton_Click"/>
    </div>
    <div>
        <asp:Label ID="curUserLabel" runat="server"
                   style="margin: 20px 0 0 360px;" Font-Size="15">
            Number of clicks: <%: Application[(string)Session["clickerName"]] %>
        </asp:Label>
    </div>
    <div>
        <asp:Label ID="allUsersLabel" runat="server"
                   style="margin: 20px 0 0 360px;" Font-Size="15">
            Total number of clicks: <%: Application["totalClicks"] %>
        </asp:Label>
    </div>
    <div>
        <table style="width: 100%;">
            <%
                Response.Write("<tr>");
                Response.Write($"<td> User </td>");
                Response.Write($"<td> Number of clicks </td>");
                Response.Write("</tr>");

                foreach (string item in Application.AllKeys)
                {
                    if (!item.Equals("totalClicks"))
                    {
                        Response.Write("<tr>");
                        Response.Write($"<td> {item} </td>");
                        Response.Write($"<td> {Application[item]} </td>");
                        Response.Write("</tr>");
                    }
                }
                 %>            
        </table>
    </div>
    </form>
</body>
</html>
