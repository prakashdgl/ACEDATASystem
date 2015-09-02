<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true" CodeBehind="AddEditCertificateOfCompliance.aspx.cs" Inherits="ACE.PurchaseOrder.QualityDocuments.AddEditCertificateOfCompliance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="850" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader">Add Edit Certificate of Compliance Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfCOCID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlStatusViewFirst" runat="server" Width="800px">
                                    <table style="width: 100%; text-align: center" border="0" cellpadding="5" cellspacing="6">
                                        <tr>
                                            <td colspan="2" style="width: 100%; text-align: left">
                                                <asp:RequiredFieldValidator ID="rfvPurchaseOrder" runat="server" ValidationGroup="ValidatePurchaseOrder"
                                                    ControlToValidate="ddlContact" ErrorMessage="Please select customer" SetFocusOnError="True">
                                                    <li style="list-style-type: circle">
                                                        <asp:Label ID="lblCustomererrorlabel" runat="server" ForeColor="red" Text="Please select Customer"></asp:Label></li>
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="trRoleRow" runat="server">
                                            <td align="right" class="Label" style="width: 40%">
                                                <asp:Label ID="lblCustomer" runat="server" Text="Customer" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlContact" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="tr2" runat="server">
                                            <td align="right" class="Label" style="width: 40%">
                                                <asp:Label ID="Label1" runat="server" Text="PO No" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlPONo" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Item no"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtItemNo" Width="240px" MaxLength="10" Text="" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label9" CssClass="Label" runat="server" Text="Qty"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtQty" Width="240px" AutoPostBack="true" MaxLength="5" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                                <cc1:filteredtextboxextender id="ftbeQty" runat="server" targetcontrolid="txtQty"
                                                    filtertype="Custom" validchars="1234567890"
                                                    enabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Description"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtDescription" Width="240px" MaxLength="300" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label5" CssClass="Label" runat="server" Text="Part no"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPartNo" Width="240px" MaxLength="30" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label6" CssClass="Label" runat="server" Text="EDS Rev"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtEDSRev" Width="240px" MaxLength="30" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label7" CssClass="Label" runat="server" Text="Drawing No"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtDrawingNo" Width="240px" MaxLength="30" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label10" CssClass="Label" runat="server" Text="REV"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtREV" Width="240px" MaxLength="30" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <center>
                                                    <br />
                                                    <asp:ImageButton ID="btnPurchaseOrderAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                        CausesValidation="true" ValidationGroup="ValidatePurchaseOrder" TabIndex="102" OnClick="btnPurchaseOrderAdd_Click" />&nbsp;
                                                    <asp:ImageButton ID="btnPurchaseOrderCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnPurchaseOrderCancel_Click" />&nbsp;
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr id="trEnablePurchaseWorkOrder" runat="server">
                            <td colspan="2">

                                <table border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="right">
                                            <asp:HiddenField ID="hfPurchaseWorkOrderID" runat="server" Value="0" />
                                            <asp:ImageButton ID="lnkAddPurchaseWorkOrder" runat="server" ImageUrl="~/Images/add.gif" OnClick="lnkAddPurchaseWorkOrder_Click" />
                                            <cc1:modalpopupextender id="mpeEdit" runat="server" backgroundcssclass="modalBackground"
                                                dropshadow="false" popupcontrolid="pnlStatusEdit" popupdraghandlecontrolid="pnlStatusEdit"
                                                targetcontrolid="lnkAddPurchaseWorkOrder">
                                            </cc1:modalpopupextender>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="100%" Height="400px" ScrollBars="vertical">
                                                <asp:GridView ID="gvPurchaseWorkOrder" runat="server" DataKeyNames="PurchaseWorkOrderID" AllowSorting="true"
                                                    OnRowDataBound="gvPurchaseWorkOrder_RowDataBound" OnRowDeleting="gvPurchaseWorkOrder_RowDeleting"
                                                    OnRowEditing="gvPurchaseWorkOrder_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PurchaseOrderID" HeaderText="PurchaseOrderID" />
                                                        <asp:BoundField DataField="PurchaseWorkOrderID" HeaderText="PurchaseWorkOrderID" />
                                                        <asp:BoundField DataField="WorkerNo" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Worker No">
                                                            <ItemStyle />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="Remarks" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Remarks">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditPurchaseWorkOrder" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeletePurchaseWorkOrder" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
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
                                    ImageAlign="Middle" TabIndex="103" Visible="false" OnClick="btnCancel_Click" />&nbsp;
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlStatusEdit" runat="server" CssClass="modalPopup" Style="display: none"
                                    Width="800px">
                                    <table style="width: 100%; text-align: center" border="0" cellpadding="3" cellspacing="3">
                                        <tr>
                                            <td colspan="2" style="width: 100%; text-align: left">
                                                <asp:RequiredFieldValidator ID="rfvPurchaseWorkOrder" runat="server" ValidationGroup="ValidatePurchaseWorkOrder"
                                                    ControlToValidate="txtQty" ErrorMessage="Please Enter the Qty" SetFocusOnError="True">
                                                    <li style="list-style-type: circle">
                                                        <asp:Label ID="lblPurchaseWorkOrderValidation" runat="server" ForeColor="red" Text="Please Enter the Qty"></asp:Label></li>
                                                </asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="Tr8" runat="server">
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblPurchaseWorkOrder" CssClass="Label" runat="server" Text="PurchaseWorkOrder"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPurchaseWorkOrder" Width="240px" MaxLength="25" Text="" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                                <cc1:filteredtextboxextender id="FilteredTextBoxExtender3" runat="server" targetcontrolid="txtPurchaseWorkOrder"
                                                    filtertype="Custom" validchars="1234567890."
                                                    enabled="True" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Item no"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtReference" Width="240px" MaxLength="10" Text="" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label16" CssClass="Label" Style="vertical-align: top;" runat="server" Text="Remarks"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtRemarks" Width="500px" MaxLength="250" TextMode="MultiLine" Rows="4" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label8" CssClass="Label" runat="server" Text="Reviewed By:"></asp:Label></td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlReviewedBy" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <center>
                                                    <br />
                                                    <asp:ImageButton ID="btnPurchaseWorkOrderAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                        CausesValidation="true" ValidationGroup="ValidatePurchaseWorkOrder" TabIndex="102" OnClick="btnPurchaseWorkOrderAdd_Click" />&nbsp;
                                    <asp:ImageButton ID="btnPurchaseWorkOrderCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnPurchaseWorkOrderCancel_Click" />&nbsp;
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
