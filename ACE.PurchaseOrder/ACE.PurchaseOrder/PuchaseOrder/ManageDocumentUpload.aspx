<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true" CodeBehind="ManageDocumentUpload.aspx.cs" Inherits="ACE.PurchaseOrder.PuchaseOrder.ManageDocumentUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">

    <table cellpadding="0" cellspacing="0" border="0" width="890" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader"></td>
                        </tr>
                        <tr>
                            <td style="text-align: left;">
                                <h4>Document upload and viewer</h4>
                            </td>
                            <td style="text-align: right">
                                <div class="more">
                                    <asp:LinkButton ID="lbtnBack" runat="server" Text="Back" OnClick="lbtnBack_Click"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" style="text-align: left; width: 100%">
                                    <tr>
                                        <td>
                                            <%--<asp:UpdatePanel runat="server" ID="update" UpdateMode="Conditional">
                                                <ContentTemplate>--%>
                                                    <cc1:CollapsiblePanelExtender ID="cpeAdditional" runat="server" TargetControlID="pnlAdditionalContent"
                                                        CollapsedSize="0" ExpandedSize="0" Collapsed="false" ExpandControlID="tblAdditionalTitleLeft"
                                                        CollapseControlID="tblAdditionalTitleLeft" AutoCollapse="False" AutoExpand="False"
                                                        ScrollContents="false" TextLabelID="lblAdditionalShowOrHide" CollapsedText="Show Details..."
                                                        ExpandedText="Hide Details" ImageControlID="imgAdditionalExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                                                        CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                                                    <asp:Panel ID="pnlAdditionalTitle" runat="server" CssClass="collapsePanelHeader">
                                                        <table border="0" style="width: 100%; margin-top: 12px;">
                                                            <tr>
                                                                <td style="width: 95%">
                                                                    <table id="tblAdditionalTitleLeft" runat="server" cellpadding="0" cellspacing="0"
                                                                        border="0" width="100%">
                                                                        <tr>
                                                                            <td align="left" style="width: 78%">
                                                                                <b>
                                                                                    <asp:Label ID="lblAdditionalInformation" runat="server" Text="Document upload" /></b>
                                                                            </td>
                                                                            <td align="right" style="width: 10%">
                                                                                <asp:Image ID="imgAdditionalExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                                                                            </td>
                                                                            <td align="left" style="width: 12%">
                                                                                <asp:Label ID="lblAdditionalShowOrHide" runat="server" Text="" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td style="text-align: right; width: 5%">
                                                                    <table id="tblAdditionalTitleRight" runat="server" cellpadding="0" cellspacing="0"
                                                                        border="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:HiddenField ID="hfContactID" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hfWorkOrderDocumentID" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                                                                <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlAdditionalContent" runat="server" ScrollBars="None">
                                                        <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                                    <tr>
                                                                                        <td width="130">
                                                                                            <b>Select Customer</b><asp:Label ID="Label2" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlListContact" runat="server" AutoPostBack="true" Width="200px" OnSelectedIndexChanged="ddlListContact_SelectedIndexChanged">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvddlListContact" runat="server" ValidationGroup="ValidateUploadFile"
                                                                                                ErrorMessage="Please Select Customer" ControlToValidate="ddlListContact" Display="None"></asp:RequiredFieldValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="vcerfvddlListContact" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                                                                TargetControlID="rfvddlListContact">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="130">
                                                                                            <b>Select Customer</b><asp:Label ID="Label4" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:DropDownList ID="ddlPONO" runat="server" AutoPostBack="false" Width="200px">
                                                                                            </asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvPONO" runat="server" ValidationGroup="ValidateUploadFile"
                                                                                                ErrorMessage="Please Select PONO" ControlToValidate="ddlPONO" Display="None"></asp:RequiredFieldValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                                                                TargetControlID="rfvPONO">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="130">
                                                                                            <b>Upload File</b><asp:Label ID="Label1" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:FileUpload ID="fileUpload" Width="200px" runat="server" />
                                                                                            <asp:RequiredFieldValidator ID="rfvFileUpload" runat="server" ValidationGroup="ValidateUploadFile"
                                                                                                ErrorMessage="Please Select the Upload File!" ControlToValidate="fileUpload"
                                                                                                Display="None"></asp:RequiredFieldValidator>
                                                                                            <cc1:ValidatorCalloutExtender ID="vceReqValFileUpload" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                                                                TargetControlID="rfvFileUpload">
                                                                                            </cc1:ValidatorCalloutExtender>
                                                                                            <%--<asp:RegularExpressionValidator runat="server" ID="revValfileUpload" ControlToValidate="fileUpload"
                                                                                        ErrorMessage="Select Document File Only (.doc, .txt, .rtf)" ValidationGroup="ValidateUploadFile"
                                                                                        Display="None" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.doc|.txt|.rtf)$" />--%>
                                                                                            <%--<cc1:ValidatorCalloutExtender ID="vceFileUpload" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                                                                        TargetControlID="revValfileUpload">
                                                                                    </cc1:ValidatorCalloutExtender>--%>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="130">
                                                                                            <b>File Description</b>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtFileDescription" runat="server" Text="" MaxLength="999" Width="400px"
                                                                                                TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td colspan="2" style="text-align: center">
                                                                                            <div class="more">
                                                                                                <asp:LinkButton ID="PI_lbtnSave" runat="server" Text="Upload" ValidationGroup="ValidateUploadFile"
                                                                                                    CausesValidation="true" OnClick="PI_lbtnSave_Click"></asp:LinkButton>
                                                                                            </div>
                                                                                            <div class="more">
                                                                                                <asp:LinkButton ID="PI_lbtnCancel" runat="server" Text="Cancel" OnClick="PI_lbtnCancel_Click"></asp:LinkButton>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                <%--</ContentTemplate>
                                                <Triggers>
                                                    
                                                    <asp:PostBackTrigger ControlID="PI_lbtnSave" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                            <%--<asp:AsyncPostBackTrigger ControlID="uiAsynch" EventName="click" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="pnlAdditionalContent1"
                                                CollapsedSize="0" ExpandedSize="0" Collapsed="false" ExpandControlID="tblAdditionalTitleLeft1"
                                                CollapseControlID="tblAdditionalTitleLeft1" AutoCollapse="False" AutoExpand="False"
                                                ScrollContents="false" TextLabelID="lblAdditionalShowOrHide1" CollapsedText="Show Details..."
                                                ExpandedText="Hide Details" ImageControlID="imgAdditionalExpandOrCollapse1" ExpandedImage="~/Images/collapse.jpg"
                                                CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                                            <asp:Panel ID="pnlAdditionalTitle1" runat="server" CssClass="collapsePanelHeader">
                                                <table border="0" style="width: 100%; margin-top: 12px;">
                                                    <tr>
                                                        <td style="width: 95%">
                                                            <table id="tblAdditionalTitleLeft1" runat="server" cellpadding="0" cellspacing="0"
                                                                border="0" width="100%">
                                                                <tr>
                                                                    <td align="left" style="width: 78%">
                                                                        <b>
                                                                            <asp:Label ID="lblAdditionalInformation1" runat="server" Text="Document View" /></b>
                                                                    </td>
                                                                    <td align="right" style="width: 10%">
                                                                        <asp:Image ID="imgAdditionalExpandOrCollapse1" ImageUrl="~/Images/expand.jpg" runat="server" />
                                                                    </td>
                                                                    <td align="left" style="width: 12%">
                                                                        <asp:Label ID="lblAdditionalShowOrHide1" runat="server" Text="" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="text-align: right; width: 5%">
                                                            <table id="tblAdditionalTitleRight1" runat="server" cellpadding="0" cellspacing="0"
                                                                border="0">
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlAdditionalContent1" runat="server" ScrollBars="None">
                                                <table cellpadding="2" cellspacing="2" border="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                <tr>
                                                                    <td>
                                                                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                                                                            <tr>
                                                                                <td width="130">
                                                                                    <b>Select Customer</b><asp:Label ID="Label3" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" Width="200px"
                                                                                        OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:GridView ID="gvWorkOrderDocument" runat="server" DataKeyNames="WorkOrderDocumentID" ShowHeader="true"
                                                                                        ShowFooter="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="false"
                                                                                        OnRowCommand="gvWorkOrderDocument_RowCommand" OnRowDataBound="gvWorkOrderDocument_RowDataBound"
                                                                                        OnRowEditing="gvWorkOrderDocument_RowEditing">
                                                                                        <Columns>
                                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                                                                <ItemTemplate>
                                                                                                    <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="WorkOrderDocumentID"></asp:BoundField>
                                                                                            <asp:BoundField DataField="FileName" HeaderText="File Name"></asp:BoundField>
                                                                                            <asp:TemplateField HeaderText="File" ItemStyle-Width="4%">
                                                                                                <ItemTemplate>
                                                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 100%" align="left">
                                                                                                                <asp:LinkButton ID="lbtnDocument" CommandName="Edit" runat="server" Text="Download"></asp:LinkButton>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="FileDescription" HeaderText="File Description" ItemStyle-Width="8%" />
                                                                                            <asp:TemplateField HeaderText="File" ItemStyle-Width="4%">
                                                                                                <ItemTemplate>
                                                                                                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 100%" align="center">
                                                                                                                <asp:HyperLink ID="hlDocumentDownload" runat="server" Text="Download" Target="_blank" NavigateUrl=""></asp:HyperLink>
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                        </Columns>
                                                                                        <FooterStyle BorderStyle="none" BorderColor="white" />
                                                                                    </asp:GridView>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
