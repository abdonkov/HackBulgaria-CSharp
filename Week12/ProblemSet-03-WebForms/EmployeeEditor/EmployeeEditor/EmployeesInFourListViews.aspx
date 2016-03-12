<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeesInFourListViews.aspx.cs" Inherits="EmployeeEditor.EmployeesInFourListViews" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="employeeRepeater" runat="server">
            <ItemTemplate>
                <asp:Label ID="headerLabel" runat="server"
                    Text="Employees with names starting from "><%#Eval("Key")%></asp:Label>
                <asp:ListView ID="employeesListView" runat="server"
                    DataSource='<%#Eval("Value")%>'>
                    <LayoutTemplate>
                        <table>
                            <thead>
                                <tr>
                                    <th>First Name</th>
                                    <th>Last Name</th>
                                    <th>Email</th>
                                    <th>Birth Date</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:PlaceHolder ID="itemPlaceHolder" runat="server"/>
                            </tbody>
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("FirstName") %></td>
                            <td><%# Eval("LastName") %></td>
                            <td><%# Eval("Email") %></td>
                            <td><%# Eval("BirthDate", CalculateBirthDateFormat(Container.DataItem)) %></td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <br/>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Button ID="defaulPageButton" runat="server"
            Text="Go to default page" OnClick="defaulPageButton_Click"/>
    </div>
    </form>
</body>
</html>
