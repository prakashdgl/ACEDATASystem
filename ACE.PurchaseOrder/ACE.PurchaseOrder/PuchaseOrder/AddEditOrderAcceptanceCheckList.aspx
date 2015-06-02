<%@ Page Title="" Language="C#" MasterPageFile="~/Order.Master" AutoEventWireup="true" CodeBehind="AddEditOrderAcceptanceCheckList.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.AddEditOrderAcceptanceCheckList" %>

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
                            <td colspan="2" class="LabelHeader">Manage Order Acceptance Checklist Page
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                                <asp:HiddenField ID="hfOrderAcceptanceCheckListID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="pnlStatusEdit" runat="server" Width="800px">
                                    <table style="width: 100%; text-align: center" border="0" cellpadding="5" cellspacing="6">
                                        <tr>
                                            <td colspan="2" style="width: 100%; text-align: left">

                                                <asp:RequiredFieldValidator ID="rfvOrderAcceptanceCheckList" runat="server" ValidationGroup="ValidateOrderAcceptanceCheckList"
                                                    ControlToValidate="ddlContact" ErrorMessage="Please select customer" SetFocusOnError="True">
                                                    <li style="list-style-type: circle">
                                                        <asp:Label ID="lblCustomererrorlabel" runat="server" ForeColor="red" Text="Please select Customer"></asp:Label></li>
                                                </asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="rfvPartDescription" runat="server" ValidationGroup="ValidateOrderAcceptanceCheckList"
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
                                                <asp:Label ID="lblPONo" CssClass="Label" runat="server" Text="PO No"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPONo" Width="150px" MaxLength="25" CssClass="textarea" Text="" runat="server">
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
                                                <asp:RequiredFieldValidator ID="rfvEnquiryDate" runat="server" ValidationGroup="ValidateOrderAcceptanceCheckList"
                                                    ControlToValidate="txtPODate" ErrorMessage="Please Enter the PO Date" Display="none"
                                                    SetFocusOnError="True">
                                                </asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="meePODate" runat="server" TargetControlID="txtPODate"
                                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                    ErrorTooltipEnabled="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblPOAmendRef" CssClass="Label" runat="server" Text="PO Amend Ref"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtPOAmendRef" Width="150px" MaxLength="50" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 40%" align="right">
                                                <asp:Label ID="lblQuotationRef" CssClass="Label" runat="server" Text="Quotation Ref"></asp:Label>
                                            </td>
                                            <td style="width: 60%" align="left">
                                                <asp:TextBox ID="txtQuotationRef" Width="150px" MaxLength="50" CssClass="textarea" Text="" runat="server">
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
                                            <td style="width: 100%" colspan="2" align="left">
                                                <asp:Label ID="lblNatureofReview" CssClass="Label" runat="server" Text="NATURE OF REVIEW"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="background-color: lightsalmon;">
                                            <td style="width: 100%" colspan="2" align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton runat="server" ID="rbFPO" Text="FIRST PURCHASE ORDER" ViewStateMode="Enabled" AutoPostBack="false" GroupName="NatureofReview" EnableViewState="true" ValidationGroup="ValidateOrderAcceptanceCheckList" TextAlign="Right" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton runat="server" ID="rbRPO" Text="REGULAR PURCHASE ORDER" ViewStateMode="Enabled" AutoPostBack="false" GroupName="NatureofReview" EnableViewState="true" ValidationGroup="ValidateOrderAcceptanceCheckList" TextAlign="Right" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton runat="server" ID="rbAmend" Text="AMENDMENTS" ViewStateMode="Enabled" AutoPostBack="false" GroupName="NatureofReview" EnableViewState="true" ValidationGroup="ValidateOrderAcceptanceCheckList" TextAlign="Right" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr style="background-color: aquamarine;">
                                            <td style="width: 100%" colspan="2" align="left">
                                                <asp:Label ID="Label8" CssClass="Label" runat="server" Text="CHECK FOR"></asp:Label>
                                                <br />
                                                <asp:RadioButton runat="server" ID="rbCheck1" Text="Differences Between Quotation" ViewStateMode="Enabled" AutoPostBack="false" GroupName="CHECKFOR1" EnableViewState="true" TextAlign="Right" />
                                                <asp:RadioButton runat="server" ID="rbCheck2" Text="PO (in case of First PO)" ViewStateMode="Enabled" AutoPostBack="false" GroupName="CHECKFOR1" EnableViewState="true" TextAlign="Right" />
                                                <br />
                                                <center><b>- OR - </b></center>
                                                <br />
                                                <asp:RadioButton runat="server" ID="rbCheck3" Text="Differences Between Earlier PO" ViewStateMode="Enabled" AutoPostBack="false" GroupName="CHECKFOR2" EnableViewState="true" TextAlign="Right" />
                                                <asp:RadioButton runat="server" ID="rbCheck4" Text="Current (in case of Regular / Amendments)" ViewStateMode="Enabled" AutoPostBack="false" GroupName="CHECKFOR2" EnableViewState="true" TextAlign="Right" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="gvOrderAcceptanceParticularsList" AutoGenerateColumns="true"
                                                    DataKeyNames="ParticularsID" BorderWidth="1" ItemStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                                    runat="server" OnRowCreated="gvOrderAcceptanceParticularsLists_RowCreated" OnRowDataBound="gvOrderAcceptanceParticularsLists_RowDataBound" Width="100%">
                                                    <RowStyle CssClass="row" />
                                                    <AlternatingRowStyle CssClass="altrow" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#CDDCF1">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                            <ItemStyle BackColor="White" Width="5%" HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ParticularsID" HeaderText="ParticularsID" HeaderStyle-BackColor="#CDDCF1" />
                                                        <asp:BoundField DataField="ParticularsDescription" HeaderText="PARTICULARS"
                                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#CDDCF1" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="40%" BackColor="White" Height="60%" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderAcceptanceParticularsListID" AutoPostBack="false" Text="0" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <asp:RadioButton ID="rbCheckYes" AutoPostBack="false" GroupName="stee1" EnableViewState="true" runat="server" />
                                                                <asp:RadioButton ID="rbCheckNo" AutoPostBack="false" GroupName="stee1" EnableViewState="true" runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40%" BackColor="pink" Height="100%" />
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
                                        <tr style="vertical-align: top;">
                                            <td colspan="2" style="vertical-align: top;">
                                                <asp:Label ID="Label3" CssClass="Label" Style="vertical-align: top;" runat="server" Text="COMMENTS IF ANY"></asp:Label>
                                                <asp:TextBox ID="txtComments" Width="500px" MaxLength="500" TextMode="MultiLine" Rows="4" CssClass="textarea" Text="" runat="server">
                                                </asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top; text-align: left;">
                                            <td colspan="2" style="vertical-align: top; text-align: left;">
                                                <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Review Status"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="vertical-align: top; text-align: left;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButton ID="rbToBeResolved" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="TO BE RESOLVED" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbNotToBeResolved" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="NOT TO BE RESOLVED" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbAccepted" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="ACCEPTED" />
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton ID="rbNotAccepted" runat="server" GroupName="rbenquiry" TextAlign="Right" Text="NOT ACCEPTED" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%; vertical-align: top; text-align: left;" align="left" colspan="2">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblReviewedDate" CssClass="LabelTitle" runat="server" Text="Reviewed Date"></asp:Label>

                                                            <asp:TextBox ID="txtReviewedDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                                                TabIndex="57" />
                                                            <cc1:CalendarExtender ID="ceReviewedDate" runat="server" CssClass="MyCalendar"
                                                                TargetControlID="txtReviewedDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                                            </cc1:CalendarExtender>
                                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ValidateOrderAcceptanceCheckList"
                                                                ControlToValidate="txtReviewedDate" ErrorMessage="Please Enter the PO Date" Display="none"
                                                                SetFocusOnError="True">
                                                            </asp:RequiredFieldValidator>--%>
                                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtReviewedDate"
                                                                Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                                                ErrorTooltipEnabled="True" />


                                                        </td>

                                                        <td>
                                                            <asp:Label ID="Label4" CssClass="Label" runat="server" Text="Reviewed By:"></asp:Label>
                                                            <asp:DropDownList ID="ddlReviewedBy" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                                                                runat="server">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label9" CssClass="Label" runat="server" Text="Approved By:"></asp:Label>
                                                            <asp:DropDownList ID="ddlApprovedBy" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
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
                                                    <asp:ImageButton ID="btnOrderAcceptanceCheckListAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                        CausesValidation="true" ValidationGroup="ValidateOrderAcceptanceCheckList" TabIndex="102" OnClick="btnOrderAcceptanceCheckListAdd_Click" />&nbsp;
                                    <asp:ImageButton ID="btnOrderAcceptanceCheckListCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnOrderAcceptanceCheckListCancel_Click" />&nbsp;
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
