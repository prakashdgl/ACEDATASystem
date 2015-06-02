<%@ Page Title="User Information" Language="C#" AutoEventWireup="true" CodeBehind="AddEditUser.aspx.cs"
    EnableEventValidation="false" MasterPageFile="~/Order.Master" Inherits="ACE.PurchaseOrder.AddEditUser" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <table border="0" cellpadding="1" cellspacing="3" width="100%">
                <tr visible="false" runat="server">
                    <td>
                        <asp:Label ID="lblEmployeeIDTitle" runat="server" Text="Employee ID" CssClass="LabelTitle"></asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblEmployeeID" runat="server" Text="0" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right; height: 25px;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 50%;">
                        <asp:Label ID="lblEmployeeCodeTitle" runat="server" Text="Employee Code" CssClass="LabelTitle"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: left; width: 50%;">
                        <asp:Label ID="lblEmployeeCode" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="lblEmployeeNameTitle" runat="server" Text="Employee Name" CssClass="LabelTitle"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblEmployeeName" runat="server" CssClass="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        <asp:Label ID="lblValid" runat="server" Text="Activate / Deactivate" CssClass="LabelTitle"></asp:Label>&nbsp;
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="chkValid" runat="server" Text="" CssClass="textarea" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <cc1:TabContainer ID="tcntAllUserTabs" runat="server" Visible="true" Style="text-align: left;"
                            ActiveTabIndex="0" ScrollBars="Vertical" Height="260px" Width="100%">
                            <cc1:TabPanel ID="tpnlCompanies" runat="server" HeaderText="Company" ForeColor="black"
                                Width="100%">
                                <ContentTemplate>
                                    <asp:Table Width="97%">
                                        <asp:TableRow>
                                            <asp:TableCell Style="text-align: left;">
                                                <asp:LinkButton ID="lnkUpdate" runat="server" Text="Add Company" />
                                                <br />
                                                <br />
                                                <cc1:ModalPopupExtender Drag="false" ID="mpeUserCompany" runat="server" BackgroundCssClass="modalBackground"
                                                    PopupControlID="pnlUserCompanyEdit" TargetControlID="lnkUpdate" DynamicServicePath=""
                                                    Enabled="true">
                                                </cc1:ModalPopupExtender>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:GridView ID="gvUserCompanies" runat="server" DataKeyNames="CompanyID,RoleID"
                                                    AllowSorting="True" HeaderStyle-HorizontalAlign="Left" OnSorting="gvUserCompanies_Sorting"
                                                    OnRowDataBound="gvUserCompanies_RowDataBound" OnRowDeleting="gvUserCompanies_RowDeleting"
                                                    OnRowEditing="gvUserCompanies_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CompanyID" HeaderText="Company ID" SortExpression="CompanyID" />
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="CompanyName" ItemStyle-Width="36%" />
                                                        <asp:BoundField DataField="RoleID" HeaderText="Role ID" SortExpression="RoleID" />
                                                        <asp:BoundField DataField="RoleName" HeaderText="Role" HeaderStyle-HorizontalAlign="Left"
                                                            SortExpression="RoleName" ItemStyle-Width="58%" />
                                                        <asp:TemplateField HeaderText="Default" ItemStyle-Width="2%" SortExpression="IsDefault">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="IsDefault" Checked='<%# Eval("IsDefault") %>' runat="server" Enabled="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <asp:Table ID="Table3" runat="server" Width="100%">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell Style="width: 50%; text-align: left;">
                                                                            <asp:ImageButton ID="ibtnEditUserCompany" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Style="width: 50%; text-align: right;">
                                                                            <asp:ImageButton ID="ibtnDeleteUserCompany" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </ContentTemplate>
                                <HeaderTemplate>
                                    Companies
                                </HeaderTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn"
                            TabIndex="102" OnClick="btnSave_Click" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" TabIndex="103" OnClick="btnCancel_Click" CausesValidation="false" />&nbsp;
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="3" style="width: 100%">
                <tr>
                    <td align="right" class="Label" style="width: 30%">&nbsp;
                    </td>
                    <td align="left">&nbsp;
                    </td>
                </tr>
                <tr id="trUserNameRow" runat="server">
                    <td align="right" class="Label" style="width: 30%">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="Label"></asp:Label>
                    </td>
                    <td style="width: 70%" align="left">
                        <asp:TextBox ID="txtUserName" runat="server" Text="" Width="144px" MaxLength="10"
                            CssClass="textarea" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trPasswordRow" visible="false" runat="server">
                    <td align="right" class="Label" style="width: 30%">
                        <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="Label"></asp:Label>
                    </td>
                    <td style="width: 70%" align="left">
                        <asp:TextBox ID="txtPassword" runat="server" Text="" Width="145px" MaxLength="50"
                            CssClass="textarea" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trRoleRow" runat="server">
                    <td align="right" class="Label" style="width: 30%">
                        <asp:Label ID="lblRole" runat="server" Text="Role" CssClass="Label"></asp:Label>
                    </td>
                    <td style="width: 70%" align="left">
                        <asp:DropDownList ID="ddlRole" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                            runat="server">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trUserIDRow" runat="server">
                    <td align="right" class="Label" style="width: 30%">
                        <asp:Label ID="lblUserID" runat="server" Text="User ID" CssClass="Label"></asp:Label>
                    </td>
                    <td style="width: 70%" align="left">
                        <asp:TextBox ID="txtUserID" runat="server" Text="0" Width="145px" CssClass="textarea"
                            Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HiddenField ID="hdfEmployeeCompanyID" Value="0" runat="server"></asp:HiddenField>
                        <asp:HiddenField ID="hdfOfficeEmailID" Value="0" runat="server"></asp:HiddenField>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlUserCompanyEdit" runat="server" CssClass="modalPopup" Style="display: none"
                Width="380px">
                <center>
                    <table runat="server" style="width: 100%; text-align: center" border="0" cellpadding="1"
                        cellspacing="3">
                        <tr>
                            <td colspan="2" style="width: 100%; text-align: left">&nbsp;
                <cc1:ValidatorCalloutExtender ID="vceCompany" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="rfvCompany">
                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceCompanyRole" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvCompanyRole">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <asp:Label ID="lblPopupHeading" runat="server" Text="Add User Company" CssClass="PopupPanelHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <hr />
                            </td>
                        </tr>
                        <tr id="Tr7" visible="false" runat="server">
                            <td class="tdstyle">
                                <asp:Label ID="lblCompanyID" CssClass="Label" runat="server" Text="CompanyID" Visible="false"
                                    Width="72px"></asp:Label>
                            </td>
                            <td class="tdstyle">
                                <asp:TextBox ID="txtCompanyID" Width="120px" CssClass="textarea" Text="0" Visible="false"
                                    runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdstyle">
                                <asp:Label ID="LabelMandatory2" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblCompany" CssClass="Label" runat="server" Text="Company"></asp:Label>
                            </td>
                            <td align="left" class="tdstyle">
                                <asp:DropDownList ID="ddlCompany" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                    runat="server">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ValidationGroup="ValidateCompany"
                                    ControlToValidate="ddlCompany" Display="none" ErrorMessage="Please Select the Company"
                                    Text="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdstyle">
                                <asp:Label ID="LabelMandatory1" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblCompanyRole" CssClass="Label" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td align="left" class="tdstyle">
                                <asp:DropDownList ID="ddlCompanyRole" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                    runat="server">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCompanyRole" runat="server" ValidationGroup="ValidateCompany"
                                    ControlToValidate="ddlCompanyRole" Display="none" ErrorMessage="Please Select the Role"
                                    Text="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdstyle">
                                <asp:Label ID="lblDefault" CssClass="Label" runat="server" Text="Default Company"></asp:Label>
                            </td>
                            <td align="left" class="tdstyle">
                                <asp:CheckBox ID="chkDefault" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdstyle">
                                <center>
                                    <asp:Button ID="btnUserCompanyAdd" runat="server" Text="Save" CssClass="btn"
                                        CausesValidation="true" ValidationGroup="ValidateCompany"
                                        TabIndex="402" OnClick="btnUserCompanyAdd_Click" />&nbsp;
                  <asp:Button ID="btnUserCompanyCancel" runat="server" CausesValidation="false" TabIndex="103" Text="Cancel" CssClass="btn"
                      OnClick="btnUserCompanyCancel_Click" />&nbsp;
                                </center>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
