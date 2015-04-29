<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true" CodeBehind="AddEditEnquiryRegister.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.AddEditEnquiryRegister" %>

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
                            <td colspan="2" class="LabelHeader">Manage Enquiry Review Checklist Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfEnquiryRegister" runat="server" Value="0" />
                                <asp:HiddenField ID="hfEnquiryReviewChecklistID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlStatusEdit" runat="server" Width="800px">
                                    <table style="width: 100%; text-align: center" border="0" cellpadding="5" cellspacing="6">

                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label1" CssClass="Label" runat="server" Text="Enquiry No#"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:Label ID="lblEnquiryNo" CssClass="Label" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Customer"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:Label ID="lblCustomerName" CssClass="Label" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Product Description"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:Label ID="lblProductDescription" CssClass="Label" runat="server" Text="Product Description"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblRegretLetter" CssClass="Label" runat="server" Text="Regret Letter"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtRegretLetter" Width="200px" MaxLength="150" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>     
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblLegalReq" CssClass="Label" runat="server" Text="Legal Reqmts, if any"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtLegalReq" Width="200px" MaxLength="150" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>   
                                         <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblQ1Reqm" CssClass="Label" runat="server" Text="Q1 Reqmts, if any"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtQ1Reqm" Width="200px" MaxLength="150" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>     
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblAPI6A" CssClass="Label" runat="server" Text="API 6A Reqmts, if any"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtAPI6A" Width="200px" MaxLength="150" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblPODetails" CssClass="Label" runat="server" Text="PO Details"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPODetails" Width="200px" MaxLength="50" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Status"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtStatus" Width="200px" MaxLength="50" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>                                             
                                        <tr>
                                            <td colspan="2">
                                                <center>
                                                    <br />
                                                    <asp:ImageButton ID="btnEnquiryReviewChecklistAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                        CausesValidation="true" ValidationGroup="ValidateEnquiryReviewChecklist" TabIndex="102" OnClick="btnEnquiryReviewChecklistAdd_Click" />&nbsp;
                                    <asp:ImageButton ID="btnEnquiryReviewChecklistCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnEnquiryReviewChecklistCancel_Click" />&nbsp;
                                                </center>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>

                        </tr>
                        <tr id="trUpdateCancelButtonRow" runat="server">
                            <td align="center" colspan="2" valign="top" style="height: 15px;">
                                <br />
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                    ImageAlign="Middle" TabIndex="103" Visible="false" OnClick="btnCancel_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>

                    </div>
                </td>
            </tr>
        </table>
</asp:Content>
