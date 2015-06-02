<%@ Page Title="" Language="C#" MasterPageFile="~/Order.Master" AutoEventWireup="true" CodeBehind="ManageStockMaintanence.aspx.cs" Inherits="ACE.PurchaseOrder.Modules.ManageStockMaintanence" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="750" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader">Stock Master Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="lnkAddStock" runat="server" ImageUrl="~/Images/btnCreateNew.gif" OnClick="lnkAddStock_Click" />
                                            <cc1:ModalPopupExtender ID="mpeEdit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="false" PopupControlID="pnlStatusEdit" PopupDragHandleControlID="pnlStatusEdit"
                                                TargetControlID="lnkAddStock">
                                            </cc1:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="100%" Height="500px" ScrollBars="vertical">
                                                <asp:GridView ID="gvStock" runat="server" DataKeyNames="StockID" AllowSorting="true"
                                                    OnRowDataBound="gvStock_RowDataBound" OnRowDeleting="gvStock_RowDeleting"
                                                    OnRowEditing="gvStock_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="StockID" HeaderText="StockID" ReadOnly="true" />
                                                        <asp:BoundField DataField="MaterialID" HeaderText="MaterialID" ReadOnly="true" />
                                                        <asp:BoundField DataField="MaterialName" ReadOnly="true" ItemStyle-Width="64%" HeaderText="Material Description">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="MaterialQuantity" ReadOnly="true" ItemStyle-Width="34%" HeaderText="Stock available Count">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditStock" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeleteStock" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trUpdateCancelButtonRow" runat="server">
                            <td align="center" colspan="2" valign="top" style="height: 15px;">
                                <br />
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                    ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlStatusEdit" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="500px">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="5" cellspacing="6">
                        <tr>
                            <td colspan="2" style="width: 100%; text-align: left">

                                <asp:RequiredFieldValidator ID="rfvStock" runat="server" ValidationGroup="ValidateStock"
                                    ControlToValidate="txtAvailablecount" ErrorMessage="Please Enter the Quantity" SetFocusOnError="True">
                                    <li style="list-style-type: circle">
                                        <asp:Label ID="lblStockValidation" runat="server" ForeColor="red" Text="Please Enter Stock Quantity"></asp:Label></li>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Tr2" visible="false" runat="server">
                            <td style="width: 40%" align="right">
                                <asp:Label ID="lblStocktextID" CssClass="Label" runat="server" Text="StockID" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="lblStockID" Width="120px" CssClass="textarea" Text="0" Visible="false"
                                    runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trRoleRow" runat="server">
                            <td align="right" class="Label" style="width: 40%">
                                <asp:Label ID="lblMaterial" runat="server" Text="Material" CssClass="Label"></asp:Label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:DropDownList ID="ddlMaterial" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                    runat="server">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%" align="right">
                                <asp:Label ID="lblAvailablecount" CssClass="Label" runat="server" Text="Item Quantity"></asp:Label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtAvailablecount" Width="240px" MaxLength="6" CssClass="textarea" Text="0" runat="server">
                                </asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="ftbeAvailablecount" runat="server" TargetControlID="txtAvailablecount"
                                    FilterType="Custom" ValidChars="1234567890"
                                    Enabled="True" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <center>
                                    <br />
                                    <asp:ImageButton ID="btnStockAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                        CausesValidation="true" ValidationGroup="ValidateStock" TabIndex="102" OnClick="btnStockAdd_Click" />&nbsp;
                                    <asp:ImageButton ID="btnStockCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnStockCancel_Click" />&nbsp;
                                </center>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
