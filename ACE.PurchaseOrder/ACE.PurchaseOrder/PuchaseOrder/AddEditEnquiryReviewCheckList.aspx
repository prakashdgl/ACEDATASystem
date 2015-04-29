<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true" CodeBehind="AddEditEnquiryReviewCheckList.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.AddEditEnquiryReviewCheckList" %>

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
                                <asp:HiddenField ID="hfEnquiryReviewChecklistID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlStatusEdit" runat="server" Width="800px">
                                    <table style="width: 100%; text-align: center" border="0" cellpadding="5" cellspacing="6">
                                        <tr>
                                            <td colspan="2" style="width: 100%; text-align: left">

                                                <asp:RequiredFieldValidator ID="rfvEnquiryReviewChecklist" runat="server" ValidationGroup="ValidateEnquiryReviewChecklist"
                                                    ControlToValidate="ddlContact" ErrorMessage="Please select customer" SetFocusOnError="True">
                                                    <li style="list-style-type: circle">
                                                        <asp:Label ID="lblCustomererrorlabel" runat="server" ForeColor="red" Text="Please select Customer"></asp:Label></li>
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvPartDescription" runat="server" ValidationGroup="ValidateEnquiryReviewChecklist"
                                                    ControlToValidate="txtPartDescription" ErrorMessage="Please enter Part Description" SetFocusOnError="True">
                                                    <li style="list-style-type: circle">
                                                        <asp:Label ID="Label1" runat="server" ForeColor="red" Text="Please enter Part Description"></asp:Label></li>
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
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblPartDescription" CssClass="Label" runat="server" Text="Part Description"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPartDescription" Width="340px" MaxLength="300" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblEnquiryNo" CssClass="Label" runat="server" Text="Enquiry No"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtEnquiryNo" Width="150px" MaxLength="25" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdLabelTitle LabelTitle" style="width: 40%" valign="middle">
                                                <asp:Label ID="Label6" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                <asp:Label ID="Label7" CssClass="LabelTitle" runat="server" Text="Enquiry Date"></asp:Label>
                                            </td>
                                            <td style="width: 60%" class="tdTextResults" valign="middle">
                                                <asp:TextBox ID="txtEnquiryDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                    TabIndex="57" />
                                                <cc1:CalendarExtender ID="ceEnquiryDate" runat="server" CssClass="MyCalendar"
                                                    TargetControlID="txtEnquiryDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="rfvEnquiryDate" runat="server" ValidationGroup="ValidateEnquiryReviewChecklist"
                                                    ControlToValidate="txtEnquiryDate" ErrorMessage="Please Enter the Enquiry Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meeEnquiryDate" runat="server" TargetControlID="txtEnquiryDate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblCustEnqRef" CssClass="Label" runat="server" Text="Cust Enq Ref"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtCustEnqRef" Width="150px" MaxLength="50" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblDrawingNoIssue" CssClass="Label" runat="server" Text="Drawing No / Issue"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtDrawingNo_Issue" Width="340px" MaxLength="100" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblScopeofSupply" CssClass="Label" runat="server" Text="Scope of Supply"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtScopeofSupply" Width="340px" MaxLength="300" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvReviewDetails" AutoGenerateColumns="true"
                                                    DataKeyNames="ReviewDetailID" BorderWidth="1" ItemStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                                    runat="server" OnRowCreated="gvReviewDetails_RowCreated" OnRowDataBound="gvReviewDetails_RowDataBound" Width="100%">
                                                    <RowStyle CssClass="row" />
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#CDDCF1">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle BackColor="White" Width="5%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ReviewDetailID" HeaderText="ReviewDetailID" HeaderStyle-BackColor="#CDDCF1" />
                                                        <asp:BoundField DataField="ReviewSlNo" HeaderText="S.No" HeaderStyle-BackColor="#CDDCF1" />
                                                        <asp:BoundField DataField="ReviewDetailDescription" HeaderText="Review Detail"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#CDDCF1" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="60%" BackColor="White" Height="60%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnquiryReviewListID" AutoPostBack="false" Text="0" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="YES">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbCheckYes" AutoPostBack="false" GroupName="stee" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NO">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbCheckNo" AutoPostBack="false" GroupName="stee" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NA">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbCheckNA" AutoPostBack="false" GroupName="stee" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARKS">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server" AutoPostBack="false" EnableViewState="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr align="left">
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblSpecialTerms" CssClass="Label" runat="server" Text="Special Terms & Conditions:"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtSpecialTermsConditions" Width="550px" MaxLength="1000" TextMode="MultiLine" Rows="4" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" align="left" colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="rbAccepted" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="ACCEPTED" />
                                                                        <br />
                                                                        <table>
                                                                            <tr>
                                                                                <td>&nbsp;</td>
                                                                                <td>
                                                                                    <asp:Label ID="Label2" CssClass="Label" runat="server" Text="QUOTE REF"></asp:Label></td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtQuoteRef" Width="140px" MaxLength="20" CssClass="textarea" Text="" runat="server">
                                                                                    </asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="rbNotAccepted" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="NOT ACCEPTED. (Apologize to Customer)" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:RadioButton ID="rbClarifications" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="CLARIFICATIONS / AMEND REQD" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label3" CssClass="Label" runat="server" Text="REMARKS / COMMENTS"></asp:Label>
                                                            <asp:TextBox ID="txtComments" Width="200px" MaxLength="500" TextMode="MultiLine" Rows="4" CssClass="textarea" Text="" runat="server">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Reviewed By:"></asp:Label>
                                                            <asp:DropDownList ID="ddlReviewedBy" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                                runat="server">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>

                                                </table>

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
