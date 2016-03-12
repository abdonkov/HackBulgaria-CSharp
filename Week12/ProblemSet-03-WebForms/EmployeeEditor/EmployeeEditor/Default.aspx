<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EmployeeEditor.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="employeeGridView" runat="server" AutoGenerateColumns="false"
            DataKeyNames="EmployeeID" CellPadding="10" CellSpacing="0" ShowFooter="true"
            OnRowCommand="employeeGridView_RowCommand" OnRowEditing="employeeGridView_RowEditing"
            OnRowCancelingEdit="employeeGridView_RowCancelingEdit" OnRowUpdating="employeeGridView_RowUpdating"
            OnRowDeleting="employeeGridView_RowDeleting">

            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>First Name</HeaderTemplate>
                    <ItemTemplate><%#Eval("FirstName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="firstNameTextBoxEdit" runat="server"
                            Text='<%#Bind("FirstName")%>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstNameEdit" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Edit"
                            ControlToValidate="firstNameTextBoxEdit"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="firstNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save"
                            ControlToValidate="firstNameTextBox"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Last Name</HeaderTemplate>
                    <ItemTemplate><%#Eval("LastName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="lastNameTextBoxEdit" runat="server"
                            Text='<%#Bind("LastName")%>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLastNameEdit" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Edit"
                            ControlToValidate="lastNameTextBoxEdit"></asp:RequiredFieldValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="lastNameTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save"
                            ControlToValidate="lastNameTextBox"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Email</HeaderTemplate>
                    <ItemTemplate><%#Eval("Email")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="emailTextBoxEdit" runat="server"
                            Text='<%#Bind("Email")%>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmailEdit" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Edit"
                            ControlToValidate="emailTextBoxEdit"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmailEdit" runat="server"
                            ErrorMessage="Email format is invalid!" ForeColor="Red"
                            Display="Dynamic" ValidationGroup="Edit"
                            ControlToValidate="emailTextBoxEdit"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save"
                            ControlToValidate="emailTextBox"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ErrorMessage="Email format is invalid!" ForeColor="Red"
                            Display="Dynamic" ValidationGroup="Save" ControlToValidate="emailTextBox"
                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Birth Date</HeaderTemplate>
                    <ItemTemplate><%#Eval("BirthDate", "{0:yyyy-MM-dd}")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="birthDateTextBoxEdit" runat="server"
                            Text='<%#Bind("BirthDate", "{0:yyyy-MM-dd}")%>'></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBirthDateEdit" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Edit"
                            ControlToValidate="birthDateTextBoxEdit"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvBirthDateEdit" runat="server" ForeColor="Red"
                            ErrorMessage="Date format is invalid!" Display="Dynamic"
                            Type="Date" Operator="DataTypeCheck" ValidationGroup="Edit"
                            ControlToValidate="birthDateTextBoxEdit"></asp:CompareValidator>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="birthDateTextBox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBirthDate" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save"
                            ControlToValidate="birthDateTextBox"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvBirthDate" runat="server" ForeColor="Red"
                            ErrorMessage="Date format is invalid!" Display="Dynamic"
                            Type="Date" Operator="DataTypeCheck" ValidationGroup="Save"
                            ControlToValidate="birthDateTextBox"></asp:CompareValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Manager</HeaderTemplate>
                    <ItemTemplate><%#Eval("ManagerName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="managerLabel" runat="server"
                            Text='<%#Bind("ManagerName")%>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="managerList" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Select manager:" Value="0"/>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvManagerList" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save" ControlToValidate="birthDateTextBox"
                            InitialValue="0"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>Department</HeaderTemplate>
                    <ItemTemplate><%#Eval("DepartmentName")%></ItemTemplate>
                    <EditItemTemplate>
                        <asp:Label ID="departmentLabel" runat="server"
                            Text='<%#Bind("DepartmentName")%>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:DropDownList ID="departmentList" runat="server" AutoPostBack="true">
                            <asp:ListItem Text="Select department:" Value="0"/>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDepartmentList" runat="server"
                            ErrorMessage="Requred!" ForeColor="Red" Display="Dynamic"
                            ValidationGroup="Save" ControlToValidate="birthDateTextBox"
                            InitialValue="0"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="editLinkButton" CommandName="Edit" runat="server">Edit</asp:LinkButton>
                        &nbsp;|&nbsp;
                        <asp:LinkButton ID="deleteLinkButton" CommandName="Delete" runat="server"
                            OnClientClick="return confirm('Are you sure you want to delete this employee?')">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="editLinkButton" CommandName="Update" runat="server">Update</asp:LinkButton>
                        &nbsp;|&nbsp;
                        <asp:LinkButton ID="deleteLinkButton" CommandName="Cancel" runat="server">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="saveButton" runat="server" Text="Save"
                            CommandName="Save" ValidationGroup="Save"/>
                    </FooterTemplate>
                </asp:TemplateField>
                
            </Columns>

        </asp:GridView>
    </div>
    <div>
        <br/>
        <asp:Button ID="employeeListViewPageButton" runat="server"
            Text="Go to four list views web page." OnClick="employeeListViewPageButton_Click"/>
    </div>
    </form>
</body>
</html>
