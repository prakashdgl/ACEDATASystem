<%@ Page Title="Employee Additional" Language="C#" MasterPageFile="~/Order.Master"
  AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EmployeeAdditional.aspx.cs"
  Inherits="ACE.PurchaseOrder.EmployeeAdditional" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <div class="pageTitleDiv">
    <asp:Label ID="lblEmpAdditionalInfo" runat="server" CssClass="pageTitleLabel" Text="Employee Additional Information"></asp:Label>
  </div>
  <br />
  <asp:UpdatePanel runat="server" UpdateMode="Always">
    <ContentTemplate>
      <table id="tblEmpAdditional" runat="server" border="0" width="100%">
        <tr>
          <td colspan="2">
            <cc1:ValidatorCalloutExtender ID="vceDurationInHours" runat="Server" HighlightCssClass="validatorCalloutHighlight"
              TargetControlID="rfvDurationInHours">
            </cc1:ValidatorCalloutExtender>
            <cc1:ValidatorCalloutExtender ID="vceDurationInMinutes" runat="Server" HighlightCssClass="validatorCalloutHighlight"
              TargetControlID="rfvDurationInMinutes">
            </cc1:ValidatorCalloutExtender>
          </td>
        </tr>
        <tr>
          <td style="width: 13%" align="right">
            <asp:Label ID="lblEmployee" runat="server" CssClass="LabelTitle" Text="Employee Name" />
          </td>
          <td style="width: 91%" class="tdTextResults">
            <asp:DropDownList ID="ddlEmployee" Width="200px" CssClass="Dropdownlist" runat="server"
              AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" TabIndex="0">
            </asp:DropDownList>
          </td>
        </tr>
        <tr>
          <td colspan="2" style="height: 5px;">
          &nbsp
          </td>
        </tr>
        <tr>
          <td colspan="2" style="height: 25px;">
            <div style="height:16px;background-color:#c9dfc6;padding:5px;" runat="server"  id="divEmp" visible="false" >
               <asp:Label ID="lblbasic" runat="server" Text="Employee Information" Visible="false" style="font-weight:bold;margin:3px;" />
               </div>
          </td>
        </tr>
        <%--<tr style="width: 100%">
          <td>
            <div style="width: 100%; height: 22px; background-color: #c9dfc6;">
              <asp:Label ID="Label3" runat="server" Text="Employee Code" CssClass="LabelTitle"></asp:Label></div>
          </td>
        </tr>--%>
        <tr id="trEmployeeDetails" runat="server">
          <td colspan="2">
            <table id="tblEmpDetails" runat="server" cellpadding="2" cellspacing="2" border="0"
              width="100%">
              <tr>
                <td style="width: 50%" valign="top">
                  <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                      <td class="LabelTitle tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td style="width: 70%" class="tdTextResults">
                        <asp:HiddenField ID="hdfEmployeeID" runat="server" />
                        <asp:Label ID="lblEmployeeCodeValue" runat="server" Text="Employee Code Description"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="Label tdLabelTitle">
                        <asp:Label ID="lblFNameAndInitial" CssClass="LabelTitle" runat="server" Text="Employee Name"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblFNameAndInitialValue" CssClass="TextResults" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="Label tdLabelTitle">
                        <asp:Label ID="lblEmployeeDesignation" CssClass="LabelTitle" runat="server" Text="Designation"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblEmployeeDesignationValue" CssClass="TextResults" Text="" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle" valign="middle">
                        <asp:Label ID="Label1" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>
                        <asp:Label ID="lblJobstartingTime" CssClass="LabelTitle" runat="server" Text="Job Starting Time"></asp:Label>
                      </td>
                      <td class="tdTextResults" valign="middle">
                        <asp:DropDownList ID="ddlDurationInHours" AppendDataBoundItems="true" Width="110px"
                          CssClass="Dropdownlist" runat="server" TabIndex="6">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDurationInHours" runat="server" ValidationGroup="ValidateEmployeeAddditional"
                          ControlToValidate="ddlDurationInHours" ErrorMessage="Please Select the Hours" Display="none"
                          SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlDurationInMinutes" AppendDataBoundItems="true" Width="110px"
                          CssClass="Dropdownlist" runat="server" TabIndex="7">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDurationInMinutes" runat="server" ValidationGroup="ValidateEmployeeAddditional"
                          ControlToValidate="ddlDurationInMinutes" ErrorMessage="Please Select the Minutes"
                          Display="none" SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                      </td>
                    </tr>
                  </table>
                </td>
                <td style="width: 50%" valign="top">
                  <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                      <td class="LabelTitle tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td style="width: 70%" class="tdTextResults">
                        <asp:Label ID="lblDepartmentValue" runat="server" Text="Department Description" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="LabelTitle tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblOfficialMailID" runat="server" Text="Official EMail ID" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td style="width: 70%" class="tdTextResults">
                        <asp:Label ID="lblOfficialMailIDValue" runat="server" Text="Official Mail ID Description"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="LabelTitle tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblReportingToEmployeeTitle" runat="server" Text="Reporting To" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td style="width: 70%" class="tdTextResults">
                        <asp:Label ID="lblReportingToEmployeeName" runat="server" Text="" CssClass="TextResults"></asp:Label>
                        <asp:Label ID="lblReportingToEmployeeID" runat="server" Text="0" Visible="false"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle" style="width: 40%;">
                        <asp:Label ID="lbl1" runat="server" Font-Bold="True" Text="Client Name" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblClientName" runat="server" Text="" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td class="tdLabelTitle" style="padding-right: 50px;">
                  <br />
                  <asp:Button ID="ibtnEmployeeAdditionalSave" runat="server" Text="Submit" CssClass="btn"
                    CausesValidation="true" ValidationGroup="ValidateEmployeeAddditional" TabIndex="8"
                    OnClick="ibtnEmployeeAdditionalSave_Click" />&nbsp;
                </td>
                <td>
                  &nbsp;
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeDesgination" runat="server" TargetControlID="pnlDesignationContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlDesignationTitle"
                    CollapseControlID="pnlDesignationTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblDesignationShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgDesignationExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlDesignationTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table8" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblDesignationTitle" runat="server" Text="Employee Designation" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgDesignationExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblDesignationShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="Table10" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlDesignationContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblDesignation" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          <table id="tblDesignationContent" runat="server" cellpadding="2" cellspacing="2"
                            border="0" width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="Label2" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                                        ID="lblDesignationTitleDescription" runat="server" Text="Designation" CssClass="LabelTitle" />
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:DropDownList ID="ddlDesignation" Width="220px" CssClass="Dropdownlist" runat="server"
                                        TabIndex="0">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvDesignation" runat="server" ErrorMessage="Please Select the Designation"
                                        ControlToValidate="ddlDesignation" ValidationGroup="ValidateDesignation" Display="none"
                                        SetFocusOnError="True">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceDesignation" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="rfvDesignation">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                              <td style="width: 50%" valign="middle">
                                <asp:LinkButton ID="ViewHistoryLinkBtn" runat="server">View Designation History</asp:LinkButton>
                                <cc1:ModalPopupExtender Drag="false" ID="mpeViewDesignationHistory" runat="server"
                                  BackgroundCssClass="modalBackground" PopupControlID="pnlViewDesignationHistory"
                                  DynamicServicePath="" TargetControlID="ViewHistoryLinkBtn" Enabled="True">
                                </cc1:ModalPopupExtender>
                              </td>
                            </tr>
                            <tr>
                              <td>
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="Label6" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                      <asp:Label ID="Label7" CssClass="LabelTitle" runat="server" Text="From Date"></asp:Label>
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:TextBox ID="txtFromDate1" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                        TabIndex="57" />
                                      <cc1:CalendarExtender ID="CalendarExtender1" runat="server" CssClass="MyCalendar"
                                        TargetControlID="txtFromDate1" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                      </cc1:CalendarExtender>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="ValidateDesignation"
                                        ControlToValidate="txtFromDate1" ErrorMessage="Please Enter the From Date" Display="none"
                                        SetFocusOnError="True">
                                      </asp:RequiredFieldValidator>
                                      <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtFromDate1"
                                        Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                        ErrorTooltipEnabled="True" />
                                    </td>
                                  </tr>
                                </table>
                              </td>
                              <td style="width: 50%" valign="middle">
                                <asp:Button ID="currentDesignationSubmitBtn" runat="server" Text="Submit" CssClass="btn"
                                  TabIndex="106" OnClick="ibtnDesignationSubmit_Click" CausesValidation="true" ValidationGroup="ValidateDesignation" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeClientInfo" runat="server" TargetControlID="pnlClientInfoContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlClientInfoTitle"
                    CollapseControlID="pnlClientInfoTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblClientInfoShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgClientInfoExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlClientInfoTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblClientInfo" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblEmpClientProInfo" runat="server" Text="Employee Client / Project Info." />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgClientInfoExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblClientInfoShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblTitle" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlClientInfoContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblClintInfo" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          <table id="tblClintInfoContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                            width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="lblClientBameMandatory" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                                        ID="lblClientNameTitle" runat="server" Text="Client Name" CssClass="LabelTitle" />
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:DropDownList ID="ddlClientName" Width="220px" AutoPostBack="true" CssClass="Dropdownlist"
                                        runat="server" OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvClientName" runat="server" ErrorMessage="Please Select the Client Name"
                                        ControlToValidate="ddlClientName" ValidationGroup="ValidateClient" Display="none">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceClientName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="rfvClientName">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="lblProjectNameMandatory" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                                        ID="lblProjectName" runat="server" Text="Project Name" CssClass="LabelTitle" />
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:DropDownList ID="ddlProjectName" Width="220px" CssClass="Dropdownlist" runat="server">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" ErrorMessage="Please Select the Project Name"
                                        ControlToValidate="ddlProjectName" ValidationGroup="ValidateClient" Display="none">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceProjectName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="rfvProjectName">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                </table>
                                <br />
                              </td>
                              <td style="width: 50%; padding-top: 10px;" valign="top">
                                <asp:LinkButton ID="viewClientProjectHistoryLinkBtn" runat="server">View Client / Project History</asp:LinkButton>
                                <cc1:ModalPopupExtender Drag="false" ID="mpeClientProjectHistory" runat="server"
                                  BackgroundCssClass="modalBackground" PopupControlID="pnlViewClientProjectHistory"
                                  TargetControlID="viewClientProjectHistoryLinkBtn" DynamicServicePath="" Enabled="True">
                                </cc1:ModalPopupExtender>
                                <br />
                                <asp:Button ID="CPSubmitBtn" runat="server" Text="Submit" CssClass="btn" TabIndex="106"
                                  CausesValidation="true" ValidationGroup="ValidateClient" OnClick="CPSubmitBtn_Click" />
                              </td>
                            </tr>
                            <%-- <tr>
                              <td>
                              </td>
                              <td style="width: 50%" valign="middle">
                              </td>
                            </tr>--%>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeReportingTo" runat="server" TargetControlID="pnlReportingToContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlReportingToTitle"
                    CollapseControlID="pnlReportingToTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblReportingToShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgReportingToExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlReportingToTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblReproting" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblReportingTo" runat="server" Text="Employee Reporting To" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgReportingToExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblReportingToShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblReportingToTitle" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlReportingToContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblReportingTo" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          <table id="tblReprotingToContent" runat="server" cellpadding="2" cellspacing="2"
                            border="0" width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="LabelTitle tdLabelTitle" style="width: 30%" valign="middle">
                                      <asp:HiddenField ID="hdfID" runat="server" Value="0" />
                                      <asp:Label ID="Label4" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                                        ID="lblReportingToEmployeeEdit" runat="server" Text="Reporting To" CssClass="LabelTitle" />
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:DropDownList ID="ddlReportingToEmployeeEdit" Width="220px" CssClass="Dropdownlist"
                                        runat="server" TabIndex="0">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvReportingToEmployee" runat="server" ErrorMessage="Please Select the Reporting To"
                                        ControlToValidate="ddlReportingToEmployeeEdit" ValidationGroup="ValidateReportingTo"
                                        Display="none" SetFocusOnError="True">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceReportingToEmployee" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="rfvReportingToEmployee">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                              <td style="width: 50%" valign="middle">
                                <asp:Button ID="ibtnReportingToSubmit" runat="server" Text="Submit" CssClass="btn"
                                  TabIndex="106" OnClick="ibtnReportingToSubmit_Click" CausesValidation="true" ValidationGroup="ValidateReportingTo" />
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeEmployeeJobStatus" runat="server" TargetControlID="pnlEmployeeJobStatusContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlEmployeeJobStatusTitle"
                    CollapseControlID="pnlEmployeeJobStatusTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblEmployeeJobStatusShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgEmployeeJobStatusExpandOrCollapse"
                    ExpandedImage="~/Images/collapse.jpg" CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical"
                    SuppressPostBack="true" />
                  <asp:Panel ID="pnlEmployeeJobStatusTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblJobStatus" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblEmployeeJobStatus" runat="server" Text="Employee Job Status" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="Image1" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblEmployeeJobStatusShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tbl" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlEmployeeJobStatusContent" runat="server" CssClass="collapsePanel"
                    ScrollBars="None">
                    <table id="tblEmpJobStatus" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnEmployeeJobStatus" runat="server" Text="Add Job Status" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditJobStatus" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditJobStatus" TargetControlID="lbtnEmployeeJobStatus" DynamicServicePath=""
                            Enabled="True">
                          </cc1:ModalPopupExtender>
                        </td>
                      </tr>
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:GridView ID="gvEmployeeJobStatus" runat="server" DataKeyNames="EmployeeJobStatusID,EmployeeID"
                            AutoGenerateColumns="false" CellPadding="5" HeaderStyle-HorizontalAlign="Left"
                            OnRowDataBound="gvEmployeeJobStatus_RowDataBound" OnRowDeleting="gvEmployeeJobStatus_RowDeleting"
                            OnRowEditing="gvEmployeeJobStatus_RowEditing" AllowSorting="true" OnSorting="gvEmployeeJobStatus_Sorting"
                            Width="50%">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeJobStatusID" HeaderText="EmployeeJobStatusID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobStatusID" HeaderText="JobStatusID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobStatusDescription" HeaderText="Job Status"
                                ItemStyle-Width="10%" SortExpression="JobStatusDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromDate" ItemStyle-Width="10%" HeaderText="From Date"
                                SortExpression="FromDate" />
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
                                    <tr align="center">
                                      <td style="width:100%;">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                        <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                          AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                      </td>
                                    </tr>
                                  </table>
                                </ItemTemplate>
                              </asp:TemplateField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeLeaveApprovar" runat="server" TargetControlID="pnlLeaveApprovarContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlLeaveApprovarTitle"
                    CollapseControlID="pnlLeaveApprovarTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblLeaveApprovarShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgLeaveApprovarExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlLeaveApprovarTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblLeaveAppTitle" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblLeaveApprovar" runat="server" Text="Leave Approver" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgLeaveApprovarExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblLeaveApprovarShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblLeave" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                &nbsp;
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlLeaveApprovarContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblLeaveApprovarContent" runat="server" cellpadding="2" cellspacing="2"
                      border="0" width="100%">
                      <tr>
                        <td>
                          <table id="tblLeaveApprovar" runat="server" cellpadding="2" cellspacing="2" border="0"
                            width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="LabelTitle tdLabelTitle" style="width: 40%" valign="middle">
                                      <asp:Label ID="lblLeaveApprovarEdit" runat="server" Text="Leave Approver EMail ID"
                                        CssClass="LabelTitle" />
                                    </td>
                                    <td style="width: 70%" class="tdTextResults" valign="middle">
                                      <asp:TextBox ID="txtLeaveApprovarEdit" Width="220px" CssClass="" runat="server" TabIndex="0">
                                      </asp:TextBox>
                                      <asp:RegularExpressionValidator ID="revLeaveApprovar" runat="server" ValidationGroup="ValidateLeaveApprovar"
                                        ControlToValidate="txtLeaveApprovarEdit" Display="none" SetFocusOnError="true"
                                        ErrorMessage="Please Enter a Valid Email Address" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$">
                                      </asp:RegularExpressionValidator><br />
                                      <cc1:ValidatorCalloutExtender ID="vceLeaveApprovar" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                        TargetControlID="revLeaveApprovar">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                </table>
                              </td>
                              <td style="width: 50%" valign="middle">
                                <asp:Button ID="ibtnLeaveApprovarSubmit" runat="server" Text="Submit" CssClass="btn"
                                  TabIndex="106" CausesValidation="true" OnClick="ibtnLeaveApprovarSubmit_Click"
                                  ValidationGroup="ValidateLeaveApprovar" />
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
      <asp:Panel ID="pnlEditJobStatus" runat="server" CssClass="modalPopup" Style="display: none"
        Width="330px" DefaultButton="ibtnJobStatusSave">
        <table style="width: 100%;" class="tdLabelCenter" border="0" cellpadding="1" cellspacing="2">
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <asp:Label ID="lblPopupHeaderJobStatus" runat="server" Text="Add Job Status" CssClass="PopupPanelHeading"></asp:Label>
            </td>
          </tr>
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <hr />
            </td>
          </tr>
          <tr>
            <td colspan="2" style="width: 100%;" class="tdTextResults">
              <cc1:ValidatorCalloutExtender ID="vceJobStatus" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="rfvJobStatus">
              </cc1:ValidatorCalloutExtender>
              <cc1:ValidatorCalloutExtender ID="vceFromDate" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                TargetControlID="rfvFromDate">
              </cc1:ValidatorCalloutExtender>
            </td>
          </tr>
          <tr id="trJobStatusHiddenRow" visible="false" runat="server">
            <td id="tdJobStatusHiddenColumn" runat="server" colspan="2">
              <asp:HiddenField ID="hdfEmployeeJobStatusID" runat="server" Value="0" />
            </td>
          </tr>
          <tr>
            <td valign="middle" class="tdstyle tdLabelTitle">
              <asp:Label ID="Label10" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
              <asp:Label ID="lblJobStatusDescription" CssClass="LabelTitle" runat="server" Text="Job Status"></asp:Label><br />
            </td>
            <td valign="middle" class="tdstyle tdTextResults">
              <asp:DropDownList ID="ddlJobStatus" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                runat="server" TabIndex="55">
                <asp:ListItem Value="">-- Select One --</asp:ListItem>
              </asp:DropDownList>
              <asp:RequiredFieldValidator ID="rfvJobStatus" runat="server" ValidationGroup="ValidateEmployeeJobStatus"
                ControlToValidate="ddlJobStatus" ErrorMessage="Please Select the Job Status" Display="none"
                SetFocusOnError="True">
              </asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td class="tdLabelTitle" valign="middle">
              <asp:Label ID="lblFromDateMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
              <asp:Label ID="lblFromDate" CssClass="LabelTitle" runat="server" Text="From Date"></asp:Label>
            </td>
            <td class="tdTextResults" valign="middle">
              <asp:TextBox ID="txtFromDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                TabIndex="57" />
              <cc1:CalendarExtender ID="cextCalFromDate" runat="server" CssClass="MyCalendar" TargetControlID="txtFromDate"
                OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
              </cc1:CalendarExtender>
              <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ValidationGroup="ValidateEmployeeJobStatus"
                ControlToValidate="txtFromDate" ErrorMessage="Please Enter the From Date" Display="none"
                SetFocusOnError="True">
              </asp:RequiredFieldValidator>
              <cc1:MaskedEditExtender ID="meeFromDate" runat="server" TargetControlID="txtFromDate"
                Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                ErrorTooltipEnabled="True" />
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <center>
                <br />
                <asp:Button ID="ibtnJobStatusSave" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                  ValidationGroup="ValidateEmployeeJobStatus" TabIndex="58" OnClick="ibtnJobStatusSave_Click" />&nbsp;
                <asp:Button ID="ibtnJobStatusCancel" runat="server" Text="Cancel" CssClass="btn"
                  CausesValidation="false" TabIndex="59" OnClick="ibtnJobStatusCancel_Click" />&nbsp;
              </center>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <%--View Designation History Popup Panel--%>
      <asp:Panel ID="pnlViewDesignationHistory" runat="server" CssClass="modalPopup" Style="display: none"
        Width="400px" DefaultButton="">
        <table style="width: 100%;" class="tdLabelCenter" border="0" cellpadding="1" cellspacing="2">
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <asp:Label ID="lblEmpDesHistory" runat="server" Text="Employee Designation History"
                CssClass="PopupPanelHeading"></asp:Label>
            </td>
          </tr>
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <hr />
            </td>
          </tr>
          <tr>
            <td style="text-align: right">
              <asp:LinkButton ID="lbtnAddHistory" OnClientClick="assignvalue()" runat="server">Add Past Designation</asp:LinkButton>
              <cc1:ModalPopupExtender Drag="false" ID="mpeAddEditHistory" runat="server" BackgroundCssClass="modalBackground"
                PopupControlID="pnlAddHistory" TargetControlID="lbtnAddHistory" DynamicServicePath=""
                Enabled="True">
              </cc1:ModalPopupExtender>
              <br />
              <br />
            </td>
          </tr>
          <tr>
            <td valign="middle" class="tdstyle tdLabelTitle">
              <asp:GridView ID="gvEmployeeDesignationHistory" runat="server" AutoGenerateColumns="false"
                CellPadding="5" HeaderStyle-HorizontalAlign="Left" Width="100%" HeaderStyle-CssClass="GridviewHeader"
                OnRowDataBound="gvEmployeeDesignationHistory_RowDataBound" OnRowCommand="gvEmployeeDesignationHistory_RowCommand">
                <Columns>
                  <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    HeaderText="S.No" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                      <asp:Label ID="lblSerial" runat="server"></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="Description" HeaderText="Designation"
                    ItemStyle-Width="10%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="DesignationID" HeaderText="" Visible="false"
                    ItemStyle-Width="0%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="FromDate" HeaderText="From Date" ItemStyle-Width="5%">
                  </asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ToDate" HeaderText="To Date" ItemStyle-Width="5%">
                  </asp:BoundField>
                  <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="3%">
                    <ItemTemplate>
                      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                          <td style="width: 50%">
                            <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                              CommandArgument='<%# Eval("DesignationID") +","+ Eval("FromDate")+","+ Eval("ToDate") %>'
                              CommandName="desgEdit"></asp:ImageButton>
                          </td>
                          <td style="width: 50%" class="tdLabelTitle">
                            <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                              OnClientClick="return confirm('Are you sure you want to delete this entry?');"
                              CommandArgument='<%# Eval("DesignationID") %>' CommandName="desgDelete"></asp:ImageButton>
                          </td>
                        </tr>
                      </table>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
              </asp:GridView>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <center>
                <asp:Button ID="closeBtn" OnClick="closeBtn_Click" runat="server" Text="Close" CssClass="btn" />
              </center>
            </td>
          </tr>
          <tr>
            <td>
              <asp:Panel ID="pnlAddHistory" runat="server" CssClass="modalPopup" Style="display: none"
                Width="400px" DefaultButton="">
                <table id="tblAddHistory" runat="server" cellpadding="2" cellspacing="2" border="0"
                  width="100%">
                  <tr>
                    <td colspan="2" class="tdLabelCenter">
                      <asp:Label ID="desPopUpLbl" runat="server" Text="Add Employee Past Designation" CssClass="PopupPanelHeading"></asp:Label>
                    </td>
                  </tr>
                  <tr>
                    <td colspan="2" class="tdLabelCenter">
                      <hr />
                      <br />
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 50%" valign="middle">
                      <table border="0" cellpadding="3" cellspacing="1" width="100%">
                        <tr>
                          <td class="tdLabelTitle LabelTitle" valign="middle">
                            <asp:Label ID="lblDesignationMandatory" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                              ID="lblDesignation" runat="server" Text="Designation" CssClass="LabelTitle" />
                          </td>
                          <td class="tdTextResults" valign="middle">
                            <asp:DropDownList ID="ddlDesignation2" Width="220px" CssClass="Dropdownlist" runat="server"
                              TabIndex="0">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDesignation2" runat="server" ErrorMessage="Please Select the Designation"
                              ControlToValidate="ddlDesignation2" ValidationGroup="ValidatePastDesignation" Display="none"
                              SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                              TargetControlID="rfvDesignation2">
                            </cc1:ValidatorCalloutExtender>
                          </td>
                        </tr>
                      </table>
                      <br />
                    </td>
                  </tr>
                  <tr>
                    <td>
                      <table border="0" cellpadding="3" cellspacing="1" width="100%">
                        <tr>
                          <td class="tdLabelTitle LabelTitle" valign="middle">
                            <asp:Label ID="lblFromMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                            <asp:Label ID="lblFrom" CssClass="LabelTitle" runat="server" Text="From Date"></asp:Label>
                          </td>
                          <td class="tdTextResults" valign="middle">
                            <asp:TextBox ID="txtPastFromDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                              TabIndex="57" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" CssClass="MyCalendar"
                              TargetControlID="txtPastFromDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="ValidatePastDesignation"
                              ControlToValidate="txtPastFromDate" ErrorMessage="Please Enter the From Date" Display="none"
                              SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txtPastFromDate"
                              Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                              OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                              ErrorTooltipEnabled="True" />
                          </td>
                          <td class="tdLabelTitle LabelTitle" valign="middle">
                            <asp:Label ID="lblToMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                            <asp:Label ID="lblTo" CssClass="LabelTitle" runat="server" Text="To Date"></asp:Label>
                          </td>
                          <td class="tdTextResults" valign="middle">
                            <asp:TextBox ID="txtPastToDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                              TabIndex="57" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" CssClass="MyCalendar"
                              TargetControlID="txtPastToDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                            </cc1:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="ValidatePastDesignation"
                              ControlToValidate="txtPastToDate" ErrorMessage="Please Enter the To Date" Display="none"
                              SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txtPastToDate"
                              Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                              OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                              ErrorTooltipEnabled="True" />
                          </td>
                        </tr>
                      </table>
                      <br />
                    </td>
                  </tr>
                  <tr>
                    <td style="width: 50%" valign="middle">
                      <asp:Button ID="btnPastDesignationSave" runat="server" CssClass="btn" Text="Save"
                        CausesValidation="true" OnClick="btnPastDesignationSave_Click" ValidationGroup="ValidatePastDesignation"
                        TabIndex="58" />&nbsp;
                      <asp:Button ID="ImageButton2" runat="server" CssClass="btn" Text="Cancel" OnClick="ddlEmployee_SelectedIndexChanged"
                        OnClientClick="ClearText();" CausesValidation="false" ImageAlign="Middle" TabIndex="59" />&nbsp;
                    </td>
                  </tr>
                </table>
              </asp:Panel>
            </td>
          </tr>
          <tr>
            <td>
            </td>
          </tr>
        </table>
      </asp:Panel>
      <%--View Client History--%>
      <asp:Panel ID="pnlViewClientProjectHistory" runat="server" CssClass="modalPopup"
        Style="display: none;" Width="500px" DefaultButton="">
        <table style="width: 100%;" class="tdLabelCenter" border="0" cellpadding="1" cellspacing="2">
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <asp:Label ID="lblEmpClient" runat="server" Text="Employee Client / Project History"
                CssClass="PopupPanelHeading"></asp:Label>
            </td>
          </tr>
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <hr />
            </td>
          </tr>
          <tr>
            <td valign="middle" class="tdstyle tdLabelTitle">
              <br />
              <asp:GridView ID="gvEmployeeClientProjectHistory" runat="server" AutoGenerateColumns="false"
                CellPadding="5" HeaderStyle-HorizontalAlign="Left" HeaderStyle-CssClass="GridviewHeader"
                OnRowCommand="gvEmployeeClientProjectHistory_RowCommand" OnRowDataBound="gvEmployeeClientProjectHistory_RowDataBound">
                <Columns>
                  <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    HeaderText="S.No" ItemStyle-Width="1%" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                      <asp:Label ID="lblSerial1" runat="server"></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ClientName" HeaderText="Client Name" ItemStyle-Width="10%">
                  </asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ProjectName" HeaderText="Project Name"
                    ItemStyle-Width="5%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ClientID" HeaderText="" Visible="false"
                    ItemStyle-Width="0%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ProjectID" HeaderText="" Visible="false"
                    ItemStyle-Width="0%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="FromDate" HeaderText="From Date" ItemStyle-Width="0%"
                    Visible="false"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ToDate" HeaderText="To Date" ItemStyle-Width="0%"
                    Visible="false"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="SNo" HeaderText="" Visible="false" ItemStyle-Width="0%">
                  </asp:BoundField>
                  <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="2%">
                    <ItemTemplate>
                      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                          <td style="width: 100%; padding-top: 5px;" class="tdLabelCenter">
                            <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                              OnClientClick="return confirm('Are you sure you want to delete this entry?');"
                              CommandArgument='<%# Eval("ClientID") +","+ Eval("ProjectID")+","+Eval("SNo")  %>'
                              CommandName="CPDelete"></asp:ImageButton>
                          </td>
                        </tr>
                      </table>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
              </asp:GridView>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <center>
                <asp:Button ID="cliseBtn" OnClick="closeBtn_Click" runat="server" Text="Close" CssClass="btn" />
              </center>
            </td>
          </tr>
        </table>
        <asp:HiddenField ID="hfSNo" runat="server" />
      </asp:Panel>
      <%--Edit Employee Designation--%>
      <asp:HiddenField ID="desigStatus" runat="server" />
      <cc1:ModalPopupExtender Drag="false" ID="mpeEditDesignation" runat="server" BackgroundCssClass="modalBackground"
        PopupControlID="pnlEditHistory" DynamicServicePath="" TargetControlID="desigStatus"
        Enabled="True">
      </cc1:ModalPopupExtender>
      <asp:Panel ID="pnlEditHistory" runat="server" CssClass="modalPopup" Style="display: none"
        Width="400px" DefaultButton="">
        <table id="Table23" runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <asp:Label ID="lblEditEmpPastDesg" runat="server" Text="Edit Employee Past Designation"
                CssClass="PopupPanelHeading"></asp:Label>
            </td>
          </tr>
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <hr />
              <br />
            </td>
          </tr>
          <tr>
            <td style="width: 50%" valign="middle">
              <table border="0" cellpadding="3" cellspacing="1" width="100%">
                <tr>
                  <td class="tdLabelTitle LabelTitle" valign="middle">
                    <asp:Label ID="Label26" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
                      ID="lblDesg" runat="server" Text="Designation" CssClass="LabelTitle" />
                  </td>
                  <td class="tdTextResults" valign="middle">
                    <asp:DropDownList ID="ddlDesignation3" Width="220px" CssClass="Dropdownlist" runat="server"
                      TabIndex="0">
                    </asp:DropDownList>
                  </td>
                </tr>
              </table>
              <br />
            </td>
          </tr>
          <tr>
            <td>
              <table border="0" cellpadding="3" cellspacing="1" width="100%">
                <tr>
                  <td class="tdLabelTitle LabelTitle" valign="middle">
                    <asp:Label ID="lblMandatory1" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                    <asp:Label ID="lblFrmDate" CssClass="LabelTitle" runat="server" Text="From Date"></asp:Label>
                  </td>
                  <td class="tdTextResults" valign="middle">
                    <asp:TextBox ID="txtEditFromDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                      TabIndex="57" />
                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" CssClass="MyCalendar"
                      TargetControlID="txtEditFromDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ValidationGroup="ValidateEditPastDesignation"
                      ControlToValidate="txtEditFromDate" ErrorMessage="Please Enter the From Date" Display="none"
                      SetFocusOnError="True">
                    </asp:RequiredFieldValidator>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="txtEditFromDate"
                      Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                      OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                      ErrorTooltipEnabled="True" />
                  </td>
                  <td class="tdLabelTitle LabelTitle" valign="middle">
                    <asp:Label ID="lblMandatory2" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                    <asp:Label ID="lblToDate" CssClass="LabelTitle" runat="server" Text="To Date"></asp:Label>
                  </td>
                  <td class="tdTextResults" valign="middle">
                    <asp:TextBox ID="txtEditToDate" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                      TabIndex="57" />
                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" CssClass="MyCalendar"
                      TargetControlID="txtEditToDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="ValidateEditPastDesignation"
                      ControlToValidate="txtEditToDate" ErrorMessage="Please Enter the To Date" Display="none"
                      SetFocusOnError="True">
                    </asp:RequiredFieldValidator>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="txtEditToDate"
                      Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                      OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                      ErrorTooltipEnabled="True" />
                  </td>
                </tr>
              </table>
              <br />
            </td>
          </tr>
          <tr>
            <td style="width: 100%" align="center" valign="middle">
              <asp:Button ID="editDesigSave" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                OnClick="btnPastDesignationSave_Click" ValidationGroup="ValidateEditPastDesignation"
                TabIndex="58" />&nbsp;
              <asp:Button ID="ImageButton3" runat="server" Text="Cancel" CssClass="btn" OnClick="ddlEmployee_SelectedIndexChanged"
                OnClientClick="ClearText();" CausesValidation="false" ImageAlign="Middle" TabIndex="59" />
            </td>
          </tr>
        </table>
      </asp:Panel>
      <%--Edit Employee Client/Project History--%>
      <asp:HiddenField ID="hfCPHistoryStat" runat="server" />
    </ContentTemplate>
  </asp:UpdatePanel>
  <script type="text/javascript">
    function ClearText() {
      $("#<%=txtPastFromDate.ClientID%>").val('');
      $("#<%=txtPastToDate.ClientID%>").val('');
    }

    function assignvalue() {
      $('#<%= desigStatus.ClientID %>').val("0");
    }

    function assignValueCP() {
      $('#<%= hfCPHistoryStat.ClientID %>').val("0");
    }
  </script>
</asp:Content>
