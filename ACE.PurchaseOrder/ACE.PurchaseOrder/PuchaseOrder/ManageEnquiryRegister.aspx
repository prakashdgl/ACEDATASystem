<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true" CodeBehind="ManageEnquiryRegister.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.ManageEnquiryRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="850" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader">Manage Enquiry Register Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table runat="server" id="viewManageList" border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="lnkAddEnquiryRegisterList" runat="server" ImageUrl="~/Images/btnCreateNew.gif" OnClick="lnkAddEnquiryRegisterList_Click" />

                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="800px" Direction="LeftToRight" Height="700px" ScrollBars="Both">
                                                <asp:GridView ID="gvEnquiryRegister" runat="server" DataKeyNames="EnquiryRegisterID" AllowSorting="true"
                                                    OnRowDataBound="gvEnquiryRegister_RowDataBound" OnRowDeleting="gvEnquiryRegister_RowDeleting"
                                                    OnRowEditing="gvEnquiryRegister_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EnquiryRegisterID" HeaderText="EnquiryRegisterID" ReadOnly="true" />
                                                        <asp:BoundField DataField="EnquiryReviewCheckListID" HeaderText="EnquiryReviewCheckListID" ReadOnly="true" />
                                                        <asp:BoundField DataField="EnquiryNo" ReadOnly="true" ItemStyle-Width="100px" HeaderText="EnquiryNo">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="EnquiryDate" ReadOnly="true" ItemStyle-Width="100px" HeaderText="EnquiryDate">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CustEnqRef" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Cust Enq Ref">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="200px" ReadOnly="true" />
                                                        <asp:BoundField DataField="PartDescription" ReadOnly="true" ItemStyle-Width="200px" HeaderText="Product Details">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QuoteRef" ReadOnly="true" ItemStyle-Width="150px" HeaderText="Quotation Ref..No.">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="QuoatationSendBy" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Quotation Date">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="RegretLetter" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Regret Letter">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="LegalReqmts" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Legal Reqmts, if any">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Q1Reqmts" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Q1 Reqmts, if any">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="API6AReqmts" ReadOnly="true" ItemStyle-Width="100px" HeaderText="API 6A Reqmts, if any">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PODetails" ReadOnly="true" ItemStyle-Width="100px" HeaderText="PO Details">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Status" ReadOnly="true" ItemStyle-Width="100px" HeaderText="Status">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditEnquiryRegisterList" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeleteEnquiryRegisterList" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
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
