<%@ Page Title="" Language="C#" MasterPageFile="~/Order.Master" AutoEventWireup="true" CodeBehind="ManagePurchaseOrder.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.ManagePurchaseOrder" %>
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
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table runat="server" id="viewManageList" border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="lnkAddPurchaseOrder" runat="server" ImageUrl="~/Images/btnCreateNew.gif" OnClick="lnkAddPurchaseOrder_Click" />
                                           
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="100%" Height="500px" ScrollBars="vertical">
                                                <asp:GridView ID="gvPurchaseOrder" runat="server" DataKeyNames="PurchaseOrderID" AllowSorting="true"
                                                    OnRowDataBound="gvPurchaseOrder_RowDataBound" OnRowDeleting="gvPurchaseOrder_RowDeleting"
                                                    OnRowEditing="gvPurchaseOrder_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="PurchaseOrderID" HeaderText="PurchaseOrderID" ReadOnly="true" />
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name (Buyer)" ReadOnly="true" />
                                                        <asp:BoundField DataField="PurchaseOrderNo" ReadOnly="true" ItemStyle-Width="14%" HeaderText="PO No">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PurchaseOrderDate" ReadOnly="true" ItemStyle-Width="14%" HeaderText="PO Date">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TelNo" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Tel No">
                                                            <ItemStyle />
                                                        </asp:BoundField> 
                                                        <asp:BoundField DataField="ItemCount" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Item Count">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ItemTotalCost" ReadOnly="true" ItemStyle-Width="14%" HeaderText="Total Cost">
                                                            <ItemStyle />
                                                        </asp:BoundField>                                                         
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditPurchaseOrder" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeletePurchaseOrder" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
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
    </table>
</asp:Content>
