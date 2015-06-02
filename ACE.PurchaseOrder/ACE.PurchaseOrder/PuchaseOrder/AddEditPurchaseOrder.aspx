<%@ Page Title="" Language="C#" MasterPageFile="~/Order.Master" AutoEventWireup="true" CodeBehind="AddEditPurchaseOrder.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.AddEditPurchaseOrder" %>

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
                            <td colspan="2" class="LabelHeader">Manage Purchase Order Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfPurchaseOrderID" runat="server" Value="0" />
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
                                                <asp:Label ID="lblCustomer" runat="server" Text="Buyer" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlContact" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label10" CssClass="Label" runat="server" Text="PO No"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPONO" Width="150px" MaxLength="25" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabelTitle LabelTitle" style="width: 40%" valign="middle">
                                                <asp:Label ID="Label6" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                <asp:Label ID="Label7" CssClass="LabelTitle" runat="server" Text="PO Date"></asp:Label>
                                            </td>
                                            <td style="width: 60%" class="tdTextResults" valign="middle">
                                                <asp:TextBox ID="txtPODate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                    TabIndex="57" />
                                                <cc1:CalendarExtender ID="cePODate" runat="server" CssClass="MyCalendar"
                                                    TargetControlID="txtPODate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvPODate" runat="server" ValidationGroup="ValidatePurchaseOrder"
                                                    ControlToValidate="txtPODate" ErrorMessage="Please Enter the PO Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meePODate" runat="server" TargetControlID="txtPODate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
                                            </td>
                                        </tr>
                                        <tr id="tr2" runat="server">
                                            <td align="right" class="Label" style="width: 40%">
                                                <asp:Label ID="Label1" runat="server" Text="Currency" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlCurrency" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                    <asp:ListItem Value="USD">USD</asp:ListItem>
                                                    <asp:ListItem Value="EURO">EURO</asp:ListItem>
                                                    <asp:ListItem Value="INR">INR</asp:ListItem>
                                                    <asp:ListItem Value="Pound">Pound</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr id="tr3" runat="server">
                                            <td align="right" class="Label" style="width: 40%">
                                                <asp:Label ID="Label5" runat="server" Text="Shipment" CssClass="Label"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:DropDownList ID="ddlShipment" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                    runat="server">
                                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                    <asp:ListItem Value="Sea freight">Sea freight</asp:ListItem>
                                                    <asp:ListItem Value="Transport">Transport</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblTel" CssClass="Label" runat="server" Text="Tel"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtTel" Width="140px" MaxLength="20" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTotalCost" Text="0" runat="server"></asp:Label>
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
                                            <cc1:ModalPopupExtender ID="mpeEdit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="false" PopupControlID="pnlStatusEdit" PopupDragHandleControlID="pnlStatusEdit"
                                                TargetControlID="lnkAddPurchaseWorkOrder">
                                            </cc1:ModalPopupExtender>
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
                                                        <asp:BoundField DataField="ItemNo" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Item No">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PartNo" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Part No">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Description" ReadOnly="true" ItemStyle-Width="17%" HeaderText="Description">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Qty" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Qty">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnitPrice" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Unit Price">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TotalPrice" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Total">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ReqatSpore" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Req at S'pore">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DTofStock" ReadOnly="true" ItemStyle-Width="14%" HeaderText="DT of Stock">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DTofDispatch" ReadOnly="true" ItemStyle-Width="14%" HeaderText="DT of Dispatch">
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
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPurchaseWorkOrder"
                                                    FilterType="Custom" ValidChars="1234567890."
                                                    Enabled="True" />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Item no"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtItemNo" Width="240px" MaxLength="10" Text="" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Part no"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPartNo" Width="240px" MaxLength="30" CssClass="textarea" runat="server">
                                                </asp:TextBox>
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
                                                <asp:Label ID="Label9" CssClass="Label" runat="server" Text="Qty"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtQty" Width="240px" AutoPostBack="true" MaxLength="5" OnTextChanged="txtQty_TextChanged" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="ftbeQty" runat="server" TargetControlID="txtQty"
                                                    FilterType="Custom" ValidChars="1234567890"
                                                    Enabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label11" CssClass="Label" runat="server" Text="Unit Price"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtUnitPrice" Width="240px" AutoPostBack="true" OnTextChanged="txtUnitPrice_TextChanged" Text="" MaxLength="12" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtUnitPrice"
                                                    FilterType="Custom" ValidChars="1234567890."
                                                    Enabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label12" CssClass="Label" runat="server" Text="Total Price"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtTotalPrice" Width="240px" Enabled="false" MaxLength="15" CssClass="textarea" runat="server">
                                                </asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtTotalPrice"
                                                    FilterType="Custom" ValidChars="1234567890."
                                                    Enabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label13" CssClass="Label" runat="server" Text="Req at S'pore"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtReqatSpore" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                    TabIndex="57" />
                                                <cc1:CalendarExtender ID="ceReqatSpore" runat="server" CssClass="MyCalendar"
                                                    TargetControlID="txtReqatSpore" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvReqatSpore" runat="server" ValidationGroup="ValidatePurchaseOrderDetail"
                                                    ControlToValidate="txtReqatSpore" ErrorMessage="Please Enter the PO Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meeReqatSpore" runat="server" TargetControlID="txtReqatSpore"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label14" CssClass="Label" runat="server" Text="DT of Stock"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtDTofStock" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                    TabIndex="57" />
                                                <cc1:CalendarExtender ID="ceDTofStock" runat="server" CssClass="MyCalendar"
                                                    TargetControlID="txtDTofStock" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvDTofStock" runat="server" ValidationGroup="ValidatePurchaseOrderDetail"
                                                    ControlToValidate="txtDTofStock" ErrorMessage="Please Enter the PO Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meeDTofStock" runat="server" TargetControlID="txtDTofStock"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label15" CssClass="Label" runat="server" Text="DT of Dispatch"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtDTofDispatch" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                    TabIndex="57" />
                                                <cc1:CalendarExtender ID="ceDTofDispatch" runat="server" CssClass="MyCalendar"
                                                    TargetControlID="txtDTofDispatch" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvDTofDispatch" runat="server" ValidationGroup="ValidatePurchaseOrderDetail"
                                                    ControlToValidate="txtDTofDispatch" ErrorMessage="Please Enter the PO Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meeDTofDispatch" runat="server" TargetControlID="txtDTofDispatch"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
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
