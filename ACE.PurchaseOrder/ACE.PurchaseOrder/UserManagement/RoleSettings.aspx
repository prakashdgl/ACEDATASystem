<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RoleSettings.aspx.cs" Inherits="ACE.PurchaseOrder.RoleSettings" %>

<%@ Register Src="~/UserControls/Footer.ascx" TagName="ECafeFooter" TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Menu.ascx" TagName="ECafeMenu" TagPrefix="uc2" %>
<%@ Register Src="~/UserControls/Header.ascx" TagName="ECafeHeader" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Role Settings</title>
    <link href="~/Style/ECafeStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table cellpadding="0" cellspacing="0" class="HeaderTable" style="text-align: center;">
            <tr>
                <td colspan="2" style="width: 100%">
                    <uc1:ECafeHeader ID="ECafeHeader" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%;" valign="top">
                    <uc2:ECafeMenu ID="ECafeMenu" runat="server" />
                </td>
                <td valign="top">
                    <table cellpadding="2" cellspacing="2" border="0" width="100%">
                        <tr>
                            <td colspan="2" align="left">
                                <asp:ValidationSummary ID="vsSummary" runat="server" ShowSummary="true" DisplayMode="BulletList" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; width: 10%">
                                <asp:Label ID="lblRole" runat="server" Text=" Role"></asp:Label>
                            </td>
                            <td style="text-align: left; width: 90%">
                                <asp:DropDownList ID="ddlRole" runat="server" Width="250" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                    <asp:ListItem>-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRoleValidator" runat="server" ControlToValidate="ddlRole"
                                    ErrorMessage="Please Select The Role" Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="false" GridLines="Both"
                                    AllowPaging="false" OnRowDataBound="gvRole_RowDataBound" OnRowCommand="gvRole_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="4%" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PageID" HeaderText="PageID" />
                                        <asp:BoundField DataField="ModuleName" HeaderText="Module Name">
                                            <ItemStyle Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PageName" HeaderText="Page Name">
                                            <ItemStyle Width="30%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Add &amp; Edit">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbAdd" Checked='<%# Eval("IsAddOrEdit") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="center" />
                                            <ItemStyle Width="10%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbDelete" Checked='<%# Eval("IsDelete") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="10%" HorizontalAlign="center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <center>
                                    <br />
                                    <asp:ImageButton ID="btnSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                        CausesValidation="true" TabIndex="12" OnClick="btnSave_Click" />&nbsp;
                                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="False" ImageAlign="Middle" TabIndex="13" OnClick="btnCancel_Click" />&nbsp;
                                </center>
                            </td>
                        </tr>
                    </table>
            </tr>
        </table>
        <uc3:ECafeFooter ID="ECafeFooter" runat="server" />
    </form>
</body>
</html>
