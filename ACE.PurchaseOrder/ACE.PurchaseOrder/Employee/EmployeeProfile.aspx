<%@ Page Language="C#" Title="Employee Profile" AutoEventWireup="true" CodeBehind="EmployeeProfile.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.EmployeeProfile" MasterPageFile="~/Order.Master" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <script type="text/javascript">
    function closeDesignationPopup() {
      $('#<%= mpeViewDesignationHistory.ClientID%>').hide();
      return false;
    }

    $(document).ready(function () {
      Initialize();
    });

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Initialize);

    function Initialize() {
      $("#<% =ddlSkillExperienceYears.ClientID %> > option[value=header]").attr("disabled", "disabled");
      $("#<% =ddlSkillExperienceMonths.ClientID %> > option[value=header]").attr("disabled", "disabled");
    }




    function OpenProjectName(e) {
      //Find the AnimationExtender and the Info panel in the same level.
      var animationID = e.id.replace("lbtnProjectName", "aeOpenProjectDescription");
      var infoID = e.id.replace("lbtnProjectName", "divProjectDescription");

      if ($find(animationID) && $get(infoID)) {
        //Move the info panel on top of the Button.
        $get(infoID).style.position = 'absolute';
        $get(infoID).style.left = Sys.UI.DomElement.getLocation(e).x + 'px';
        $get(infoID).style.top = Sys.UI.DomElement.getLocation(e).y + 'px';

        $find(animationID)._onClick.play();
      }
    }

    function CloseProjectName(e) {
      var animationID = e.id.replace("lbtnProjectName", "aeCloseProjectDescription");
      var infoID = e.id.replace("lbtnProjectName", "divProjectDescription");
      if ($find(animationID) && $get(infoID)) {
        $find(animationID)._onClick.play();
      }
    }
  </script>
  <script type="text/javascript" language="javascript">
    var clientid;
    function fnSetFocus(txtClientId) {
      clientid = txtClientId;
      setTimeout("fnFocus()", 1000);
    }

    function fnFocus() {
      eval("document.getElementById('" + clientid + "').focus()");
    }
  </script>
  <div class="pageTitleDiv">
    <asp:Label ID="Label1" runat="server" CssClass="pageTitleLabel" Text="My Profile"></asp:Label><hr />
  </div>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table id="Table1" runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
          <td colspan="2" style="height: 25px;">
            <div style="height:16px;background-color:#c9dfc6;padding:5px;">
               <asp:Label ID="lblbasic" runat="server" Text="Employee Information" style="font-weight:bold;margin:3px;" />
               </div>
          </td>
        </tr>
        <tr id="trEmployeeDetailView" runat="server">
          <td colspan="2">
            <table runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
              <tr>
                <td style="width: 50%" valign="top">
                  <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                      <td class="tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td style="width: 70%" class="tdTextResults">
                        <asp:HiddenField ID="hdfEmployeeID" runat="server" />
                        <asp:Label ID="lblEmployeeCodeValue" runat="server" Text="Employee Code Description"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblFName" CssClass="LabelTitle" runat="server" Text="Employee Name"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblFNameValue" CssClass="TextResults" runat="server"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblInitial" runat="server" Text="Initial" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblInitialValue" runat="server" Text="Initial Description" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblGender" CssClass="LabelTitle" runat="server" Text="Gender"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblGenderValue" runat="server" Text="Gender Description" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblDesignation" CssClass="LabelTitle" runat="server" Text="Designation"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblDsgDesc" runat="server" Text="Designation Description" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                  </table>
                </td>
                <td style="width: 50%" valign="top">
                  <table border="0" cellpadding="3" cellspacing="1" width="100%">
                    <tr>
                      <td class="tdLabelTitle" style="width: 30%">
                        <asp:Label ID="lblDateOfBirth" runat="server" Text="Date of Birth" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblDateOfBirthValue" runat="server" Text="Date of Birth Date" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblDepartment" runat="server" Text="Department" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblDepartmentValue" runat="server" Text="Department Description" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblDateOfJoining" runat="server" Text="Date of Joining" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblDateOfJoiningValue" runat="server" Text="Date of Joining Description"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle">
                        <asp:Label ID="lblOfficialMailID" runat="server" Text="Official EMail ID" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblOfficialMailIDValue" runat="server" Text="Official Mail ID Description"
                          CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle" style="width: 30%;">
                        <asp:Label ID="lblTitleEmployeeStatus" runat="server" Text="Job Status" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblEmployeeStatus" runat="server" Text="" CssClass="TextResults"></asp:Label>
                      </td>
                    </tr>
                    <tr>
                      <td class="tdLabelTitle" style="width: 30%;">
                      </td>
                      <td class="tdTextResults" style="float: right; padding-right: 20px;">
                        <asp:LinkButton ID="ViewHistoryLinkBtn" runat="server">View Designation History</asp:LinkButton>
                        <cc1:ModalPopupExtender Drag="false" ID="mpeViewDesignationHistory" runat="server"
                          BackgroundCssClass="modalBackground" PopupControlID="pnlViewDesignationHistory"
                          DynamicServicePath="" TargetControlID="ViewHistoryLinkBtn" Enabled="True">
                        </cc1:ModalPopupExtender>
                      </td>
                    </tr>
                  </table>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeGeneral" runat="Server" TargetControlID="pnlGeneralContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="False" ExpandControlID="tblGeneralTitleLeft"
                    CollapseControlID="tblGeneralTitleLeft" AutoCollapse="False" AutoExpand="False"
                    ScrollContents="False" TextLabelID="lblGeneralShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgGeneralExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlGeneralTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table runat="server" id="tblGeneralTitleLeft" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblGeneralInformation" runat="server" Text="General Information" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgGeneralExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblGeneralShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblGeneralTitleRight" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                <asp:Image ID="imgGeneralEdit" ImageUrl="~/Images/icon_edit.gif" runat="server" ToolTip="Edit General Information" />
                                <cc1:ModalPopupExtender Drag="false" ID="mpeGeneralEdit" runat="server" BackgroundCssClass="modalBackground"
                                  PopupControlID="pnlGeneralEdit" TargetControlID="imgGeneralEdit" DynamicServicePath=""
                                  Enabled="True">
                                </cc1:ModalPopupExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlGeneralContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblGeneralContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td style="width: 46%">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 30%">
                                <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults" width="70%">
                                <asp:HiddenField ID="hdfTitleID" runat="server" />
                                <asp:Label ID="lblTitleValue" runat="server" Text="Title Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblNationality" runat="server" Text="Nationality" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfNationalityID" runat="server" />
                                <asp:Label ID="lblNationalityValue" runat="server" Text="Nationality Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblReligion" runat="server" Text="Religion" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfReligionID" runat="server" />
                                <asp:Label ID="lblReligionValue" runat="server" Text="Religion Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfBloodGroupID" runat="server" />
                                <asp:Label ID="lblBloodGroupValue" runat="server" Text="BloodGroup Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 54%">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 35%">
                                <asp:Label ID="lblHomeTown" runat="server" Text="Home Town" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td style="width: 65%" class="tdTextResults">
                                <asp:HiddenField ID="hdfHomeTown" runat="server" />
                                <asp:Label ID="lblHomeTownValue" runat="server" Text="Home Town Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfMaritalStatusID" runat="server" />
                                <asp:Label ID="lblMaritalStatusValue" runat="server" Text="Marital Status Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblWeddingAnniversary" runat="server" Text="Wedding Anniversary" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfWeddingAnniversaryDate" runat="server" />
                                <asp:Label ID="lblWeddingAnniversaryValue" runat="server" Text="Wedding Anniversary Date"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblMobile" runat="server" Text="Mobile" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfMobile" runat="server" />
                                <asp:Label ID="lblMobileValue" runat="server" Text="Mobile Description" CssClass="TextResults"></asp:Label>
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
                  <cc1:CollapsiblePanelExtender ID="cpeAddress" runat="server" TargetControlID="pnlAddressContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="tblAddressTitleLeft"
                    CollapseControlID="tblAddressTitleLeft" AutoCollapse="False" AutoExpand="False"
                    ScrollContents="false" TextLabelID="lblAddressShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgAddressExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlAddressTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0" width="100%" id="tblAddressTitleLeft">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblAddressInformation" runat="server" Text="Address Information" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgAddressExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblAddressShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblAddressTitleRight" runat="server" cellpadding="0" cellspacing="0" border="0">
                            <tr>
                              <td>
                                <asp:Image ID="imgAddressEdit" ImageUrl="~/Images/icon_edit.gif" runat="server" ToolTip="Edit Address Information" />
                                <cc1:ModalPopupExtender Drag="false" ID="mpeAddressEdit" runat="server" BackgroundCssClass="modalBackground"
                                  PopupControlID="pnlAddressEdit" TargetControlID="imgAddressEdit" DynamicServicePath=""
                                  Enabled="True">
                                </cc1:ModalPopupExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlAddressContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblAddressContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td style="width: 50%; vertical-align: top;">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 30%">
                                <asp:Label ID="lblPresentAddress" runat="server" Text="Present Address" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td style="width: 70%" class="tdTextResults">
                                <asp:HiddenField ID="hdfIsPresentAndPermanentAddressSame" runat="server" />
                                <asp:HiddenField ID="hdfPresentEmployeeAddressID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfPresentAddress1" runat="server" />
                                <asp:Label ID="lblPresentAddressValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPresentAddress2" runat="server" />
                                <asp:Label ID="lblPresentAddressValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPresentAddress3" runat="server" />
                                <asp:Label ID="lblPresentAddressValue3" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblCity1" runat="server" Text="City" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfCity1" runat="server" />
                                <asp:Label ID="lblCityValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPinCode1" runat="server" Text="Pin code" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPinCode1" runat="server" />
                                <asp:Label ID="lblPinCodeValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblCountry1" runat="server" Text="Country" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPresentAddressCountryID" runat="server" />
                                <asp:Label ID="lblCountryValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblState1" runat="server" Text="State" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPresentAddressStateID" runat="server" />
                                <asp:Label ID="lblStateValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblDistrict1" runat="server" Text="District" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPresentAddressDistrictID" runat="server" />
                                <asp:Label ID="lblDistrictValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPhone1" runat="server" Text="Phone" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPhone1" runat="server" />
                                <asp:Label ID="lblPhoneValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 50%">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 30%">
                                <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td style="width: 70%" class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentEmployeeAddressID" runat="server" Value="0" />
                                <asp:HiddenField ID="hdfPermanentAddress1" runat="server" />
                                <asp:Label ID="lblPermanentAddressValue1" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentAddress2" runat="server" />
                                <asp:Label ID="lblPermanentAddressValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentAddress3" runat="server" />
                                <asp:Label ID="lblPermanentAddressValue3" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblCity2" runat="server" Text="City" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfCity2" runat="server" />
                                <asp:Label ID="lblCityValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPinCode2" runat="server" Text="Pin code" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPinCode2" runat="server" />
                                <asp:Label ID="lblPinCodeValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblCountry2" runat="server" Text="Country" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentAddressCountryID" runat="server" />
                                <asp:Label ID="lblCountryValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblState2" runat="server" Text="State" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentAddressStateID" runat="server" />
                                <asp:Label ID="lblStateValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblDistrict2" runat="server" Text="District" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPermanentAddressDistrictID" runat="server" />
                                <asp:Label ID="lblDistrictValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPhone2" runat="server" Text="Phone" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPhone2" runat="server" />
                                <asp:Label ID="lblPhoneValue2" runat="server" Text="--" CssClass="TextResults"></asp:Label>
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
                  <cc1:CollapsiblePanelExtender ID="cpeAdditional" runat="server" TargetControlID="pnlAdditionalContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="tblAdditionalTitleLeft"
                    CollapseControlID="tblAdditionalTitleLeft" AutoCollapse="False" AutoExpand="False"
                    ScrollContents="false" TextLabelID="lblAdditionalShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgAdditionalExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlAdditionalTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblAdditionalTitleLeft" runat="server" cellpadding="0" cellspacing="0"
                            border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblAdditionalInformation" runat="server" Text="Additional Information" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgAdditionalExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblAdditionalShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblAdditionalTitleRight" runat="server" cellpadding="0" cellspacing="0"
                            border="0">
                            <tr>
                              <td>
                                <asp:Image ID="imgAdditionalEdit" ImageUrl="~/Images/icon_edit.gif" runat="server"
                                  ToolTip="Edit Additional Information" />
                                <cc1:ModalPopupExtender Drag="false" ID="mpeAdditionalEdit" runat="server" BackgroundCssClass="modalBackground"
                                  PopupControlID="pnlAdditionalEdit" TargetControlID="imgAdditionalEdit" DynamicServicePath=""
                                  Enabled="True">
                                </cc1:ModalPopupExtender>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                  <asp:Panel ID="pnlAdditionalContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblAdditionalContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td style="width: 50%; vertical-align: top;">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 30%">
                                <asp:Label ID="lblPAN" runat="server" Text="PAN" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td style="width: 70%" class="tdTextResults">
                                <asp:HiddenField ID="hdfPAN" runat="server" />
                                <asp:Label ID="lblPANValue" runat="server" Text="PAN Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblBankAccountNumber" runat="server" Text="Bank Account No." CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfBankAccountNumber" runat="server" />
                                <asp:Label ID="lblBankAccountNumberValue" runat="server" Text="Bank Account Number Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblBankName" runat="server" Text="Bank Name" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfBankName" runat="server" />
                                <asp:Label ID="lblBankNameValue" runat="server" Text="Bank Name Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPassportNumber" runat="server" Text="Passport Number" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPassportNumber" runat="server" />
                                <asp:Label ID="lblPassportNumberValue" runat="server" Text="Passport Number Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblDateOfIssue" runat="server" Text="Date of Issue" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfDateOfIssue" runat="server" />
                                <asp:Label ID="lblDateOfIssueValue" runat="server" Text="Date Of Issue Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblDateOfExpiry" runat="server" Text="Date of Expiry" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfDateOfExpiry" runat="server" />
                                <asp:Label ID="lblDateOfExpiryValue" runat="server" Text="Date Of Expiry Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPlaceOfIssue" runat="server" Text="Place of Issue" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPlaceOfIssue" runat="server" />
                                <asp:Label ID="lblPlaceOfIssueValue" runat="server" Text="Place of Issue Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblJobStartTime" runat="server" Text="Job Starting Time" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:Label ID="lblJobStartingTime" runat="server" Text="" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 50%; vertical-align: top;">
                          <table border="0" cellpadding="3" cellspacing="1" width="100%">
                            <tr>
                              <td class="tdLabelTitle" style="width: 30%">
                                <asp:Label ID="lblPersonalEmailID" runat="server" Text="Personal EMail ID" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td style="width: 70%" class="tdTextResults">
                                <asp:HiddenField ID="hdfPersonalEmailID" runat="server" />
                                <asp:Label ID="lblPersonalEmailIDValue" runat="server" Text="Personal Mail ID Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblMessengerID" runat="server" Text="Messenger ID" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfMessengerID" runat="server" />
                                <asp:Label ID="lblMessengerIDValue" runat="server" Text="Messenger ID" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICEName1" runat="server" Text="ICE Name 1" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICEName1" runat="server" />
                                <asp:Label ID="lblICENameValue1" runat="server" Text="ICE Name Description1" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICERelationship1" runat="server" Text="Relationship" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICERelationshipID1" runat="server" />
                                <asp:Label ID="lblICERelationshipValue1" runat="server" Text="Relationship Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICEPhone1" runat="server" Text="Phone" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICEPhone1" runat="server" />
                                <asp:Label ID="lblICEPhoneValue1" runat="server" Text="Phone Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICEName2" runat="server" Text="ICE Name 2" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICEName2" runat="server" />
                                <asp:Label ID="lblICENameValue2" runat="server" Text="ICE Name Description2" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICERelationship2" runat="server" Text="Relationship" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICERelationshipID2" runat="server" />
                                <asp:Label ID="lblICERelationshipValue2" runat="server" Text="Relationship Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblICEPhone2" runat="server" Text="Phone" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfICEPhone2" runat="server" />
                                <asp:Label ID="lblICEPhoneValue2" runat="server" Text="Phone Description" CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                          </table>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeLanguage" runat="server" TargetControlID="pnlLanguageContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlLanguageTitle"
                    CollapseControlID="pnlLanguageTitle" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                    TextLabelID="lblLanguageShowOrHide" CollapsedText="Show Details..." ExpandedText="Hide Details"
                    ImageControlID="imgLanguageExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlLanguageTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblLangTitle" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblLanguagesKnown" runat="server" Text="Languages Known" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgLanguageExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblLanguageShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="tblEmpty" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlLanguageContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblLangContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnLanguage" runat="server" Text="Add a Language" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditLanguageKnown" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditLanguage" TargetControlID="lbtnLanguage" Enabled="True">
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
                          <asp:GridView ID="gvEmployeeLanguages" runat="server" DataKeyNames="EmployeeLanguageKnownID,EmployeeID,LanguageID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeLanguages_RowDataBound"
                            OnRowDeleting="gvEmployeeLanguages_RowDeleting" OnRowEditing="gvEmployeeLanguages_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeeLanguages_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderText="S.No" ItemStyle-Width="2%"
                                HeaderStyle-CssClass="GridviewHeader">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeLanguageKnownID" HeaderText="EmployeeLanguageKnownID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="LanguageID" HeaderText="LanguageID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" DataField="LanguageDescription"
                                HeaderText="Language" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="49%" HeaderStyle-HorizontalAlign="Left"
                                SortExpression="LanguageDescription"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderStyle-CssClass="GridviewHeader" HeaderStyle-HorizontalAlign="Center" HeaderText="Read"
                                ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsRead" Checked='<%# Eval("IsRead") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderStyle-CssClass="GridviewHeader" HeaderText="Write" ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsWrite" Checked='<%# Eval("IsWrite") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderStyle-CssClass="GridviewHeader" HeaderText="Speak" ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsSpeak" Checked='<%# Eval("IsSpeak") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeFamily" runat="server" TargetControlID="pnlFamilyContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlFamilyTitle"
                    CollapseControlID="pnlFamilyTitle" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                    TextLabelID="lblFamilyShowOrHide" CollapsedText="Show Details..." ExpandedText="Hide Details"
                    ImageControlID="imgFamilyExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlFamilyTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table8" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblFamilyKnown" runat="server" Text="Family" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgFamilyExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblFamilyShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td class="tdLabelTitle" style="width: 5%">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlFamilyContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblFamilyContent" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnFamily" runat="server" Text="Add a Family Member" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditFamily" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditFamily" TargetControlID="lbtnFamily" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeeFamily" runat="server" DataKeyNames="EmployeeFamilyID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeFamily_RowDataBound"
                            OnRowDeleting="gvEmployeeFamily_RowDeleting" OnRowEditing="gvEmployeeFamily_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeeFamily_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeFamilyID" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="EmployeeFamilyID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="EmployeeID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" ItemStyle-HorizontalAlign="left"
                                DataField="Name" HeaderStyle-HorizontalAlign="Left" HeaderText="Name" ItemStyle-Width="33%"
                                SortExpression="Name"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="GenderID" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="GenderID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="GenderDescription" ItemStyle-HorizontalAlign="left" HeaderText="Gender"
                                ItemStyle-Width="20%" SortExpression="GenderDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" DataField="RelationshipID"
                                HeaderText="RelationshipID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="left" DataField="RelationshipDescription" HeaderText="Relationship"
                                ItemStyle-Width="25%" SortExpression="RelationshipDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="left" DataField="DOB" ItemStyle-Width="16%" HeaderText="Date of Birth"
                                SortExpression="DOB" />
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" HeaderText="Action"
                                ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeEducation" runat="server" TargetControlID="pnlEducationContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlEducationTitle"
                    CollapseControlID="pnlEducationTitle" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                    TextLabelID="lblEducationShowOrHide" CollapsedText="Show Details..." ExpandedText="Hide Details"
                    ImageControlID="imgEducationExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlEducationTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="tblEducation" runat="server" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblEducation" runat="server" Text="Education" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgEducationExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblEducationShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="Table21" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlEducationContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="Table11" runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnEducation" runat="server" Text="Add Education" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditEducation" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditEducation" TargetControlID="lbtnEducation" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeeEducation" runat="server" DataKeyNames="EmployeeEducationID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeEducation_RowDataBound"
                            OnRowDeleting="gvEmployeeEducation_RowDeleting" OnRowEditing="gvEmployeeEducation_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeeEducation_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-HorizontalAlign="Right" HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeEducationID" HeaderText="EmployeeEducationID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="QualificationID" HeaderText="Name" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                DataField="QualificationDescription" HeaderText="Qualification" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="10%" SortExpression="QualificationDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="MajorID" HeaderText="MajorID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="MajorDescription" HeaderText="Major"
                                ItemStyle-Width="20%" SortExpression="MajorDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="YearOfPass" HeaderText="Year of Pass"
                                ItemStyle-Width="15%" SortExpression="YearOfPass"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="UniversityID" HeaderText="UniversityID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="UniversityDescription" HeaderText="University"
                                ItemStyle-Width="20%" SortExpression="UniversityDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="InstitutionDescription" HeaderText="Institution"
                                ItemStyle-Width="24%" SortExpression="InstitutionDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-HorizontalAlign="Left" DataField="ClassObtained" HeaderText="Class Obtained"
                                ItemStyle-Width="10%" SortExpression="ClassObtained"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeCertification" runat="server" TargetControlID="pnlCertificationContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlCertificationTitle"
                    CollapseControlID="pnlCertificationTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblCertificationShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgCertificationExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlCertificationTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table12" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblCertification" runat="server" Text="Certifications" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgCertificationExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblCertificationShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlCertificationContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblCertification" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnCertification" runat="server" Text="Add a Certification" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditCertification" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditCertification" TargetControlID="lbtnCertification" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeeCertification" runat="server" DataKeyNames="EmployeeCertificationID,EmployeeID"
                            CellPadding="5" HeaderStyle-CssClass="GridviewHeader" HeaderStyle-HorizontalAlign="Left"
                            OnRowDataBound="gvEmployeeCertification_RowDataBound" OnRowDeleting="gvEmployeeCertification_RowDeleting"
                            OnRowEditing="gvEmployeeCertification_RowEditing" AllowSorting="true" OnSorting="gvEmployeeCertification_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeCertificationID" HeaderText="EmployeeCertificationID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="Certification" HeaderText="Certification"
                                ItemStyle-Width="23%" SortExpression="Certification"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="TechnologyID" HeaderText="TechnologyID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="TechnologyDescription" HeaderText="Technology"
                                ItemStyle-Width="15%" SortExpression="TechnologyDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="YearOfPass" ItemStyle-HorizontalAlign="Left" HeaderText="Year of Pass"
                                ItemStyle-Width="15%" SortExpression="YearOfPass"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="IssuedBy" HeaderText="Issued by" ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Left"
                                SortExpression="IssuedBy"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="ClassObtained" ItemStyle-HorizontalAlign="Left" HeaderText="Class Obtained"
                                ItemStyle-Width="10%" SortExpression="ClassObtained"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="TranscriptID" HeaderText="Transcript ID"
                                ItemStyle-Width="20%" SortExpression="TranscriptID"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeSkill" runat="server" TargetControlID="pnlSkillContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlSkillTitle"
                    CollapseControlID="pnlSkillTitle" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                    TextLabelID="lblSkillShowOrHide" CollapsedText="Show Details..." ExpandedText="Hide Details"
                    ImageControlID="imgSkillExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlSkillTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table14" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblSkill" runat="server" Text="Skills" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgSkillExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblSkillShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlSkillContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="Table15" runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnSkill" runat="server" Text="Add a Skill" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditSkill" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditSkill" TargetControlID="lbtnSkill" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeeSkill" runat="server" DataKeyNames="EmployeeSkillID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeSkill_RowDataBound"
                            OnRowDeleting="gvEmployeeSkill_RowDeleting" OnRowEditing="gvEmployeeSkill_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeeSkill_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeSkillID" HeaderText="EmployeeSkillID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="TechnologyID" HeaderText="TechnologyID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="TechnologyDescription" HeaderText="Skill" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="34%" SortExpression="TechnologyDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="SkillLevelID" HeaderText="SkillLevelID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="SkillLevelDescription" HeaderText="Skill Level" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="20%" SortExpression="SkillLevelDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="ExperienceInYears" HeaderText="Experience in Years" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="20%" SortExpression="ExperienceInYears"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="ExperienceInMonths" HeaderText="Experience in Months" ItemStyle-HorizontalAlign="Right"
                                ItemStyle-Width="20%" SortExpression="ExperienceInMonths"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeExperience" runat="server" TargetControlID="pnlExperienceContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlExperienceTitle"
                    CollapseControlID="pnlExperienceTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblExperienceShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgExperienceExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlExperienceTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table16" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblExperience" runat="server" Text="Experience" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgExperienceExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblExperienceShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlExperienceContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblExperience" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnExperience" runat="server" Text="Add Experience" />
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditExperience" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditExperience" TargetControlID="lbtnExperience" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeeExperience" runat="server" DataKeyNames="EmployeeExperienceID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeExperience_RowDataBound"
                            OnRowDeleting="gvEmployeeExperience_RowDeleting" OnRowEditing="gvEmployeeExperience_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeeExperience_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeExperienceID" HeaderText="EmployeeExperienceID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%">
                              </asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Organization Name" SortExpression="OrganizationName" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="23%">
                                <ItemTemplate>
                                  <asp:Label ID="lbtnOrganizationName" runat="server" Text='<%#Bind("OrganizationName")%>' />
                                  <div id="divJobProfile" runat="server" class="flyOutDiv">
                                    <div style="float: right;">
                                    </div>
                                    <br />
                                    <p>
                                      <asp:Label ID="lblJobProfile" runat="server" Text='<%#Eval("JobProfile")%>'></asp:Label>
                                    </p>
                                  </div>
                                  <cc1:AnimationExtender ID="aeOpenJobProfile" runat="server" TargetControlID="lbtnOrganizationName">
                                    <Animations>
                                                                                   <OnClick>
                                                                                      <Sequence>
                                                                                           <EnableAction Enabled="true"></EnableAction>
                                                                                           <StyleAction AnimationTarget="divJobProfile" Attribute="display" Value="block"/>
                                                                                           <Parallel AnimationTarget="divJobProfile" Duration=".5" Fps="30">
                                                                                                <Move Horizontal="-50" Vertical="50"></Move>
                                                                                                <FadeIn Duration=".5"/>
                                                                                           </Parallel>
                                                                                           <Parallel AnimationTarget="divJobProfile" Duration=".5">
                                                                                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                                                                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                                                                           </Parallel>
                                                                                      </Sequence>
                                                                                   </OnClick>
                                    </Animations>
                                  </cc1:AnimationExtender>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="Location" HeaderText="Location" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left"
                                SortExpression="Location"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="Designation" HeaderText="Designation" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="35%" SortExpression="Designation"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromMonth" HeaderText="FromMonth" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromYear" HeaderText="FromYear" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="FromMonthAndYear" HeaderText="From"
                                ItemStyle-Width="10%" SortExpression="FromMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToMonth" HeaderText="ToMonth" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToYear" HeaderText="ToYear" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="ToMonthAndYear" HeaderText="To" ItemStyle-Width="10%"
                                SortExpression="ToMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                ItemStyle-HorizontalAlign="Left" DataField="CTC" HeaderText="CTC" ItemStyle-Width="6%"
                                SortExpression="CTC" DataFormatString="{0:N2}" HtmlEncode="False"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobProfile" HeaderText="JobProfile" ItemStyle-Width="0%" />
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
              <tr style="visibility:hidden;">
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeProject" runat="server" TargetControlID="pnlProjectContent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlProjectTitle"
                    CollapseControlID="pnlProjectTitle" AutoCollapse="false" AutoExpand="false" ScrollContents="false"
                    TextLabelID="lblProjectShowOrHide" CollapsedText="Show Details..." ExpandedText="Hide Details"
                    ImageControlID="imgProjectExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlProjectTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table18" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblProject" runat="server" Text="Projects" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgProjectExpandOrCollapse" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblProjectShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlProjectContent" runat="server" CssClass="collapsePanel" ScrollBars="None">
                    <table id="tblProject" runat="server" cellpadding="2" cellspacing="2" border="0"
                      width="100%">
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnPreviousProject" runat="server" Text="Add Project " />
                          <asp:Label ID="lblPreviousProjectTitles" CssClass="LabelTitle" runat="server" Text="- Previous Projects"></asp:Label>
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditPreviousProject" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditPreviousProject" TargetControlID="lbtnPreviousProject" DynamicServicePath=""
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
                          <asp:GridView ID="gvEmployeePreviousProject" runat="server" DataKeyNames="EmployeePreviousEmployersProjectID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeePreviousProject_RowDataBound"
                            OnRowDeleting="gvEmployeePreviousProject_RowDeleting" OnRowEditing="gvEmployeePreviousProject_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeePreviousProject_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeePreviousEmployersProjectID" HeaderText="EmployeePreviousEmployersProjectID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%" />
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Project Name" SortExpression="ProjectName" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="23%">
                                <ItemTemplate>
                                  <asp:LinkButton ID="lbtnProjectName" runat="server" Text='<%#Bind("ProjectName")%>'
                                    OnClientClick="OpenProjectName(this);return false;" />
                                  <div id="divProjectDescription" runat="server" class="flyOutDiv">
                                    <div style="float: right;">
                                      <asp:LinkButton ID="lbtnClose" runat="server" Text="x" OnClientClick="CloseProjectName(this);return false;"
                                        CssClass="flyOutDivCloseX" />
                                    </div>
                                    <br />
                                    <p>
                                      <asp:Label ID="lblProjectDescription" runat="server" Text='<%#Eval("ProjectDescription")%>'></asp:Label>
                                    </p>
                                  </div>
                                  <cc1:AnimationExtender ID="aeOpenProjectDescription" runat="server" TargetControlID="lbtnProjectName">
                                    <Animations>
                                                                                   <OnClick>
                                                                                      <Sequence>
                                                                                           <EnableAction Enabled="true"></EnableAction>
                                                                                           <StyleAction AnimationTarget="divProjectDescription" Attribute="display" Value="block"/>
                                                                                           <Parallel AnimationTarget="divProjectDescription" Duration=".5" Fps="30">
                                                                                                <Move Horizontal="-50" Vertical="50"></Move>
                                                                                                <FadeIn Duration=".5"/>
                                                                                           </Parallel>
                                                                                           <Parallel AnimationTarget="divProjectDescription" Duration=".5">
                                                                                                <Color PropertyKey="color" StartValue="#666666" EndValue="#FF0000" />
                                                                                                <Color PropertyKey="borderColor" StartValue="#666666" EndValue="#FF0000" />
                                                                                           </Parallel>
                                                                                      </Sequence>
                                                                                   </OnClick>
                                    </Animations>
                                  </cc1:AnimationExtender>
                                  <cc1:AnimationExtender ID="aeCloseProjectDescription" runat="server" TargetControlID="lbtnClose">
                                    <Animations>
                                                                                   <OnClick>
                                                                                       <Sequence AnimationTarget="divProjectDescription">
                                                                                            <Parallel AnimationTarget="divProjectDescription" Duration=".7" Fps="20">
                                                                                                 <Move Horizontal="50" Vertical="-50"></Move>
                                                                                                 <Scale ScaleFactor="0.05" FontUnit="px" />
                                                                                                 <Color PropertyKey="color" StartValue="#FF0000" EndValue="#666666" />
                                                                                                 <Color PropertyKey="borderColor" StartValue="#FF0000" EndValue="#666666" />
                                                                                                 <FadeOut />
                                                                                            </Parallel>
                                                                                            <StyleAction Attribute="display" Value="none"/>
                                                                                            <StyleAction Attribute="height" Value=""/>
                                                                                            <StyleAction Attribute="width" Value="400px"/>
                                                                                            <StyleAction Attribute="fontSize" Value="14px"/>
                                                                                            <EnableAction AnimationTarget="lbtnClose" Enabled="true" />
                                                                                       </Sequence>
                                                                                   </OnClick>
                                    </Animations>
                                  </cc1:AnimationExtender>
                                </ItemTemplate>
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                DataField="ClientName" HeaderText="Client Name" ItemStyle-Width="13%" ItemStyle-HorizontalAlign="Left"
                                SortExpression="ClientName"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="IsOnsite" HeaderText="Onsite (Yes/No)"
                                ItemStyle-Width="6%" SortExpression="IsOnsite"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="OnsiteLocation" HeaderText="Onsite Location"
                                ItemStyle-Width="10%" SortExpression="OnsiteLocation"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Technology" HeaderText="Technology" ItemStyle-Width="10%"
                                SortExpression="Technology"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Domain" HeaderText="Domain" ItemStyle-Width="10%"
                                SortExpression="Domain"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromMonth" HeaderText="FromMonth" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromYear" HeaderText="FromYear" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromMonthAndYear" HeaderText="From" ItemStyle-Width="10%"
                                SortExpression="FromMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToMonth" HeaderText="ToMonth" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToYear" HeaderText="ToYear" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToMonthAndYear" HeaderText="To" ItemStyle-Width="10%"
                                SortExpression="ToMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="TeamSize" HeaderText="Team Size" ItemStyle-Width="5%"
                                SortExpression="TeamSize"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="RolePlayed" HeaderText="Role" ItemStyle-Width="10%"
                                SortExpression="RolePlayed"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ProjectDescription" HeaderText="Project Description"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
                      <tr>
                        <td>
                          <asp:LinkButton ID="lbtnPresentProject" runat="server" Text="Add Project " OnClick="lbtnPresentProject_Click" />
                          <asp:Label ID="lblPresentProjectsTitles" CssClass="LabelTitle" runat="server" Text="- Present Projects"></asp:Label>
                          <cc1:ModalPopupExtender Drag="false" ID="mpeEditPresentProject" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlEditPresentProject" TargetControlID="hdfPresentProjectForModalPopup"
                            DynamicServicePath="" Enabled="True">
                          </cc1:ModalPopupExtender>
                          <asp:HiddenField ID="hdfPresentProjectForModalPopup" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td>
                          &nbsp;
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:GridView ID="gvEmployeePresentProject" runat="server" DataKeyNames="EmployeePresentEmployerProjectID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeePresentProject_RowDataBound"
                            OnRowDeleting="gvEmployeePresentProject_RowDeleting" OnRowEditing="gvEmployeePresentProject_RowEditing"
                            AllowSorting="true" OnSorting="gvEmployeePresentProject_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderStyle-CssClass="GridviewHeader" HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeePresentEmployerProjectID" HeaderText="EmployeePreviousEmployersProjectID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ProjectID" HeaderText="ProjectID" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ProjectName" HeaderText="Project Name"
                                ItemStyle-Width="28%" SortExpression="ProjectName"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ClientName" HeaderText="Client Name" ItemStyle-Width="20%"
                                SortExpression="ClientName"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Domain" HeaderText="Domain" ItemStyle-Width="16%"
                                SortExpression="Domain"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromDate" HeaderText="From Date" ItemStyle-Width="10%"
                                SortExpression="FromDate"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToDate" HeaderText="To Date" ItemStyle-Width="10%"
                                SortExpression="ToDate"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobRoleID" HeaderText="JobRoleID" ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobRoleDescription" HeaderText="Role"
                                ItemStyle-Width="15%" SortExpression="JobRoleDescription"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Action" ItemStyle-Width="4%">
                                <ItemTemplate>
                                  <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                    <tr>
                                      <td style="width: 50%" class="tdLabelTitle">
                                        <asp:ImageButton ID="ibtnEdit" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                          AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                      </td>
                                      <td style="width: 50%" class="tdLabelTitle">
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
                  <asp:Panel ID="pnlGeneralEdit" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="320px" DefaultButton="ibtnGeneralSave">
                    <center>
                      <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                        <tr>
                          <td colspan="2" style="text-align: center;" class="tdstyle">
                            <asp:Label ID="lblPopupHeading" runat="server" Text="General Information" CssClass="PopupPanelHeading"></asp:Label>
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2" style="text-align: center;" class="tdstyle">
                            <hr />
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblTitleEdit" CssClass="LabelTitle" runat="server" Text="Title"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:DropDownList ID="ddlTitleEdit" AppendDataBoundItems="true" Width="120px" CssClass="Dropdownlist"
                              runat="server" TabIndex="1">
                              <asp:ListItem Value="">-- Select One --</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblNationalityEdit" CssClass="LabelTitle" runat="server" Text="Nationality"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:DropDownList ID="ddlNationalityEdit" AppendDataBoundItems="true" Width="120px"
                              CssClass="Dropdownlist" runat="server" TabIndex="2">
                              <asp:ListItem Value="">-- Select One --</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblReligionEdit" CssClass="LabelTitle" runat="server" Text="Religion"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:DropDownList ID="ddlReligionEdit" AppendDataBoundItems="true" Width="120px"
                              CssClass="Dropdownlist" runat="server" TabIndex="3">
                              <asp:ListItem Value="">-- Select One --</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblBloodGroupEdit" CssClass="LabelTitle" runat="server" Text="Blood Group"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:DropDownList ID="ddlBloodGroupEdit" AppendDataBoundItems="true" Width="120px"
                              CssClass="Dropdownlist" runat="server" TabIndex="4">
                              <asp:ListItem Value="">-- Select One --</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblHomeTownEdit" CssClass="LabelTitle" runat="server" Text="Home Town"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:TextBox ID="txtHomeTownEdit" Width="160px" CssClass="textarea" Text="" runat="server"
                              MaxLength="50" TabIndex="5"></asp:TextBox>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblMaritalStatusEdit" CssClass="LabelTitle" runat="server" Text="Marital Status"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:DropDownList ID="ddlMaritalStatusEdit" AppendDataBoundItems="true" Width="120px"
                              CssClass="Dropdownlist" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMaritalStatusEdit_SelectedIndexChanged"
                              TabIndex="6">
                              <asp:ListItem Value="">-- Select One --</asp:ListItem>
                            </asp:DropDownList>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblWeddingDateEdit" CssClass="LabelTitle" runat="server" Text="Wedding Date"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                              <ContentTemplate>
                                <asp:TextBox ID="txtWeddingDateEdit" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                                  TabIndex="7" />
                                <cc1:CalendarExtender ID="cextCalWeddingDateEdit" CssClass="MyCalendar" runat="server"
                                  TargetControlID="txtWeddingDateEdit" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="meeWeddingDate" runat="server" TargetControlID="txtWeddingDateEdit"
                                  Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                  OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                  ErrorTooltipEnabled="True" />
                              </ContentTemplate>
                              <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMaritalStatusEdit" EventName="SelectedIndexChanged" />
                              </Triggers>
                            </asp:UpdatePanel>
                          </td>
                        </tr>
                        <tr>
                          <td valign="middle" class="tdstyle tdLabelTitle">
                            <asp:Label ID="lblMobileEdit" CssClass="LabelTitle" runat="server" Text="Mobile"></asp:Label>
                          </td>
                          <td valign="middle" class="tdstyle tdTextResults">
                            <asp:TextBox ID="txtMobileEdit" Width="160px" CssClass="textarea" Text="" runat="server"
                              MaxLength="15" TabIndex="8"></asp:TextBox>
                            <cc1:FilteredTextBoxExtender ID="ftbeMobileEdit" runat="server" TargetControlID="txtMobileEdit"
                              FilterType="Custom" ValidChars="+()-0123456789" Enabled="True" />
                          </td>
                        </tr>
                        <tr>
                          <td colspan="2" class="tdstyle">
                            <center>
                              <br />
                              <asp:Button ID="ibtnGeneralSave" runat="server" Text="Save" CssClass="btn" CausesValidation="false"
                                TabIndex="9" OnClick="ibtnGeneralSave_Click" />&nbsp;
                              <asp:Button ID="ibtnGeneralCancel" runat="server" Text="Cancel" CssClass="btn" ImageAlign="Middle"
                                TabIndex="10" OnClick="ibtnGeneralCancel_Click" />&nbsp;
                            </center>
                          </td>
                        </tr>
                      </table>
                    </center>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlAddressEdit" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="370px" DefaultButton="ibtnAddressSave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="Label211" runat="server" Text="Address Information" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 40%" class="tdLabelTitle" valign="middle">
                          <asp:Label ID="lblPresentAddressEdit" CssClass="LabelTitle" runat="server" Text="Present Address"></asp:Label>
                        </td>
                        <td style="width: 60%" class="tdTextResults" valign="middle">
                          <asp:TextBox ID="txtPresentAddress1" CssClass="textarea" Text="" MaxLength="100"
                            runat="server" Width="200px" TabIndex="11" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                        </td>
                        <td class="tdTextResults" valign="middle">
                          <asp:TextBox ID="txtPresentAddress2" CssClass="textarea" Text="" MaxLength="100"
                            runat="server" Width="200px" TabIndex="12" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentAddress3" CssClass="textarea" Text="" MaxLength="100"
                            runat="server" Width="200px" TabIndex="13" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressCity" CssClass="LabelTitle" runat="server" Text="City"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentAddressCity" CssClass="textarea" Text="" MaxLength="75"
                            runat="server" TabIndex="14" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressPinCode" CssClass="LabelTitle" runat="server" Text="Pin code"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentAddressPinCode" CssClass="textarea" MaxLength="10" Text=""
                            runat="server" TabIndex="15" />
                          <cc1:FilteredTextBoxExtender ID="ftePresentAddressPinCode" runat="server" TargetControlID="txtPresentAddressPinCode"
                            FilterType="Custom" ValidChars="0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressCountry" CssClass="LabelTitle" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPresentAddressCountry" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="16">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPresentAddressCountries" runat="server" Category="PresentAddressCountries"
                            TargetControlID="ddlPresentAddressCountry" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetCountries" ServicePath="../CountryService.asmx" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressState" CssClass="LabelTitle" runat="server" Text="State"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPresentAddressState" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="17">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPresentAddressStates" runat="server" Category="PresentAddressCountries"
                            ParentControlID="ddlPresentAddressCountry" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetStatesByCountryId" ServicePath="../CountryService.asmx" TargetControlID="ddlPresentAddressState" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressDistrict" CssClass="LabelTitle" runat="server" Text="District"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPresentAddressDistrict" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="18">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPresentAddressDistricts" runat="server" Category="PresentAddressDistricts"
                            ParentControlID="ddlPresentAddressState" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetDistrictsByStateId" ServicePath="../CountryService.asmx" TargetControlID="ddlPresentAddressDistrict" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentAddressPhone" CssClass="LabelTitle" runat="server" Text="Phone"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentAddressPhone" CssClass="textarea" MaxLength="20" Text=""
                            runat="server" TabIndex="19" />
                          <cc1:FilteredTextBoxExtender ID="ftePresentAddressPhone" runat="server" TargetControlID="txtPresentAddressPhone"
                            FilterType="Custom" ValidChars="+()-0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <center>
                            <hr />
                          </center>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdTextResults" valign="middle">
                          <asp:Label ID="lblIsPresentAddrSameAsPermanent" CssClass="LabelTitle" runat="server"
                            Text="" />
                          &nbsp;&nbsp;
                          <asp:CheckBox ID="chkIsPresentAddrSameAsPermanent" CssClass="LabelTitle" runat="server"
                            AutoPostBack="true" Checked="false" Text="Is Present Address Same As Permanent?"
                            TextAlign="Left" OnCheckedChanged="chkIsPresentAddrSameAsPermanent_CheckedChanged"
                            TabIndex="20" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <hr />
                          </center>
                        </td>
                      </tr>
                      <tr>
                        <td style="width: 40%" class="tdLabelTitle" valign="middle">
                          <asp:Label ID="lblPermanentAddressEdit" CssClass="LabelTitle" runat="server" Text="Permanent Address"></asp:Label>
                        </td>
                        <td style="width: 60%" class="tdTextResults" valign="middle">
                          <asp:TextBox ID="txtPermanentAddress1" CssClass="textarea" Text="" runat="server"
                            MaxLength="100" Width="200px" TabIndex="21" />
                        </td>
                      </tr>
                      <tr>
                        <td class="tdLabelTitle" valign="middle">
                        </td>
                        <td class="tdTextResults" valign="middle">
                          <asp:TextBox ID="txtPermanentAddress2" CssClass="textarea" Text="" runat="server"
                            MaxLength="100" Width="200px" TabIndex="22" />
                        </td>
                      </tr>
                      <tr>
                        <td class="tdstyle tdLabelTitle">
                        </td>
                        <td class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPermanentAddress3" CssClass="textarea" Text="" runat="server"
                            MaxLength="100" Width="200px" TabIndex="23" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressCity" CssClass="LabelTitle" runat="server" Text="City"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPermanentAddressCity" CssClass="textarea" MaxLength="75" Text=""
                            runat="server" TabIndex="24" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressPinCode" CssClass="LabelTitle" runat="server" Text="Pin code"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPermanentAddressPinCode" CssClass="textarea" Text="" MaxLength="10"
                            runat="server" TabIndex="25" />
                          <cc1:FilteredTextBoxExtender ID="ftetxtPermanentAddressPinCode" runat="server" TargetControlID="txtPermanentAddressPinCode"
                            FilterType="Custom" ValidChars="0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressCountry" CssClass="LabelTitle" runat="server" Text="Country"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPermanentAddressCountry" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="26">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPermanentAddressCountries" runat="server" Category="PermanentAddressCountries"
                            TargetControlID="ddlPermanentAddressCountry" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetCountries2" ServicePath="../CountryService.asmx" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressState" CssClass="LabelTitle" runat="server" Text="State"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPermanentAddressState" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="27">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPermanentAddressStates" runat="server" Category="PermanentAddressCountries"
                            ParentControlID="ddlPermanentAddressCountry" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetStatesByCountryId2" ServicePath="../CountryService.asmx" TargetControlID="ddlPermanentAddressState" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressDistrict" CssClass="LabelTitle" runat="server"
                            Text="District"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPermanentAddressDistrict" Width="200px" CssClass="Dropdownlist"
                            runat="server" TabIndex="27">
                          </asp:DropDownList>
                          <cc1:CascadingDropDown ID="cddPermanentAddressDistricts" runat="server" Category="PermanentAddressDistricts"
                            ParentControlID="ddlPermanentAddressState" LoadingText="Please wait..." PromptText="-- Select One --"
                            ServiceMethod="GetDistrictsByStateId2" ServicePath="../CountryService.asmx" TargetControlID="ddlPermanentAddressDistrict" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPermanentAddressPhone" CssClass="LabelTitle" runat="server" Text="Phone"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPermanentAddressPhone" CssClass="textarea" MaxLength="20" Text=""
                            runat="server" TabIndex="28" />
                          <cc1:FilteredTextBoxExtender ID="ftePermanentAddressPhone" runat="server" TargetControlID="txtPermanentAddressPhone"
                            FilterType="Custom" ValidChars="+()-0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnAddressSave" runat="server" Text="Save" CssClass="btn" TabIndex="29"
                              OnClick="ibtnAddressSave_Click" />&nbsp;
                            <asp:Button ID="ibtnAddressCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                              TabIndex="30" OnClick="ibtnAddressCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlAdditionalEdit" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="380px" DefaultButton="ibtnAdditionalSave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="Label3" runat="server" Text="Additional Information" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPANEdit" CssClass="LabelTitle" runat="server" Text="PAN"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPANEdit" CssClass="textarea" Text="" runat="server" Width="210px"
                            MaxLength="10" TabIndex="31" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblBankAccountNumberEdit" CssClass="LabelTitle" runat="server" Text="Bank Account Number"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtBankAccountNumberEdit" Width="150px" CssClass="textarea" runat="server"
                            MaxLength="20" TabIndex="32"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblBankNameEdit" CssClass="LabelTitle" runat="server" Text="Bank Name"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtBankNameEdit" Width="210px" CssClass="textarea" runat="server"
                            MaxLength="20" TabIndex="33"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPassportNumberEdit" CssClass="LabelTitle" runat="server" Text="Passport Number"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPassportNumberEdit" Width="150px" CssClass="textarea" runat="server"
                            MaxLength="20" TabIndex="34"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPassportDateOfIssueEdit" CssClass="LabelTitle" runat="server" Text="Date of Issue"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPassportDateOfIssueEdit" runat="server" MaxLength="10" Width="80px"
                            CssClass="textarea" TabIndex="35" />
                          <cc1:CalendarExtender CssClass="MyCalendar" ID="cextCalPassportDateOfIssueEdit" runat="server"
                            TargetControlID="txtPassportDateOfIssueEdit" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                          </cc1:CalendarExtender>
                          <cc1:MaskedEditExtender ID="meePassportDateOfIssueEdit" runat="server" TargetControlID="txtPassportDateOfIssueEdit"
                            Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPassportDateOfExpiryEdit" CssClass="LabelTitle" runat="server"
                            Text="Date of Expiry"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPassportDateOfExpiryEdit" runat="server" MaxLength="10" Width="80px"
                            CssClass="textarea" TabIndex="36" />
                          <cc1:CalendarExtender ID="cextCalPassportDateOfExpiryEdit" CssClass="MyCalendar"
                            runat="server" TargetControlID="txtPassportDateOfExpiryEdit" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                          </cc1:CalendarExtender>
                          <cc1:MaskedEditExtender ID="meePassportDateOfExpiryEdit" runat="server" TargetControlID="txtPassportDateOfExpiryEdit"
                            Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPassportPlaceOfIssueEdit" CssClass="LabelTitle" runat="server"
                            Text="Place of Issue"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPassportPlaceOfIssueEdit" Width="210px" MaxLength="50" CssClass="textarea"
                            runat="server" TabIndex="37"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPersonalMailIDEdit" CssClass="LabelTitle" runat="server" Text="Personal EMail ID"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPersonalMailIDEdit" Width="210px" MaxLength="50" CssClass="textarea"
                            runat="server" TabIndex="38"></asp:TextBox>
                          <asp:RegularExpressionValidator ID="revRejex" runat="server" ValidationGroup="ValidateAdditionalInformation"
                            ControlToValidate="txtPersonalMailIDEdit" Display="none" SetFocusOnError="true"
                            ErrorMessage="Please Enter a Valid Email Address" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$">
                          </asp:RegularExpressionValidator><br />
                          <cc1:ValidatorCalloutExtender ID="vceOfficeMailID" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="revRejex">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblMessengerIDEdit" CssClass="LabelTitle" runat="server" Text="Messenger ID"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtMessengerIDEdit" Width="210px" MaxLength="50" CssClass="textarea"
                            runat="server" TabIndex="39"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICENameEdit1" CssClass="LabelTitle" runat="server" Text="ICE Name 1"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtICENameEdit1" Width="210px" MaxLength="50" CssClass="textarea"
                            runat="server" TabIndex="40"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICERelationshipEdit1" CssClass="LabelTitle" runat="server" Text="Relationship"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlICERelationshipEdit1" AppendDataBoundItems="true" Width="156px"
                            CssClass="Dropdownlist" runat="server" TabIndex="41">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICEPhoneEdit1" CssClass="LabelTitle" runat="server" Text="Phone"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtICEPhoneEdit1" Width="150px" MaxLength="15" CssClass="textarea"
                            runat="server" TabIndex="42"></asp:TextBox>
                          <cc1:FilteredTextBoxExtender ID="fteICEPhoneEdit1" runat="server" TargetControlID="txtICEPhoneEdit1"
                            FilterType="Custom" ValidChars="+()-0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICENameEdit2" CssClass="LabelTitle" runat="server" Text="ICE Name 2"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtICENameEdit2" Width="210px" MaxLength="50" CssClass="textarea"
                            runat="server" TabIndex="43"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICERelationshipEdit2" CssClass="LabelTitle" runat="server" Text="Relationship"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlICERelationshipEdit2" AppendDataBoundItems="true" Width="156px"
                            CssClass="Dropdownlist" runat="server" TabIndex="44">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblICEPhoneEdit2" CssClass="LabelTitle" runat="server" Text="Phone"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtICEPhoneEdit2" Width="150px" MaxLength="15" CssClass="textarea"
                            runat="server" TabIndex="45"></asp:TextBox>
                          <cc1:FilteredTextBoxExtender ID="fteICEPhoneEdit2" runat="server" TargetControlID="txtICEPhoneEdit2"
                            FilterType="Custom" ValidChars="+()-0123456789" Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnAdditionalSave" runat="server" Text="Save" CssClass="btn" ValidationGroup="ValidateAdditionalInformation"
                              CausesValidation="true" TabIndex="46" OnClick="ibtnAdditionalSave_Click" />&nbsp;
                            <asp:Button ID="ibtnAdditionalCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" TabIndex="47" OnClick="ibtnAdditionalCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditLanguage" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="320px" DefaultButton="ibtnLanguageSave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;">
                          <asp:Label ID="lblPopupLanguageHeading" runat="server" Text="Add Language" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2">
                          <cc1:ValidatorCalloutExtender ID="vceLanguage" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvLanguage">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trLanguageHiddenRow" visible="false" runat="server">
                        <td id="tdLanguageHiddenColumn" runat="server" colspan="2">
                          <asp:HiddenField ID="hdfEmployeeLanguageKnownID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label5" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblLanguageKnown" CssClass="LabelTitle" runat="server" Text="Language"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlLanguage" AppendDataBoundItems="true" Width="225px" CssClass="Dropdownlist"
                            runat="server" TabIndex="48">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvLanguage" runat="server" ValidationGroup="ValidateEmployeeLanguage"
                            ControlToValidate="ddlLanguage" ErrorMessage="Please Select Language" Display="none"
                            SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                    </table>
                    <div style="text-align: center">
                      <asp:Label ID="lblRead" CssClass="LabelTitle" runat="server" Text="Read"></asp:Label>
                      <asp:CheckBox ID="chkRead" CssClass="textarea" runat="server" TabIndex="49"></asp:CheckBox>
                      &nbsp;
                      <asp:Label ID="lblWrite" CssClass="LabelTitle" runat="server" Text="Write"></asp:Label>
                      <asp:CheckBox ID="chkWrite" CssClass="textarea" runat="server" TabIndex="50"></asp:CheckBox>
                      &nbsp;
                      <asp:Label ID="lblSpeak" CssClass="LabelTitle" runat="server" Text="Speak"></asp:Label>
                      <asp:CheckBox ID="chkSpeak" CssClass="textarea" runat="server" TabIndex="51"></asp:CheckBox>
                      <div style="text-align: center;">
                        <br />
                        <asp:Button ID="ibtnLanguageSave" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                          ValidationGroup="ValidateEmployeeLanguage" OnClick="ibtnLanguageSave_Click" TabIndex="52" />&nbsp;
                        <asp:Button ID="ibtnLanguageCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                          TabIndex="53" OnClick="ibtnLanguageCancel_Click" />&nbsp;
                      </div>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditFamily" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="330px" DefaultButton="ibtnFamilySave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeaderFamily" runat="server" Text="Add Family Member" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vceFamilyMemberName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvFamilyMemberName">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceRelationship" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvRelationship">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceGender" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvGender">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trFamilyHiddenRow" visible="false" runat="server">
                        <td id="tdFamilyHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeeFamilyID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label4" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblFamilyMemberName" CssClass="LabelTitle" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtFamilyMemberName" runat="server" MaxLength="50" Width="230px"
                            CssClass="textarea" TabIndex="54" />
                          <asp:RequiredFieldValidator ID="rfvFamilyMemberName" runat="server" ValidationGroup="ValidateEmployeeFamily"
                            ControlToValidate="txtFamilyMemberName" ErrorMessage="Please Enter Name" Display="none"
                            SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                          <cc1:FilteredTextBoxExtender ID="fteFamilyMemberName" runat="server" TargetControlID="txtFamilyMemberName"
                            ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJECKLMNOPQRSTUVWXYZ " Enabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label10" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="Label2" CssClass="LabelTitle" runat="server" Text="Gender"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlGender" AppendDataBoundItems="true" Width="150px" CssClass="Dropdownlist"
                            runat="server" TabIndex="55">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvGender" runat="server" ValidationGroup="ValidateEmployeeFamily"
                            ControlToValidate="ddlGender" ErrorMessage="Please Select Gender" Display="none"
                            SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label11" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblRelationship" CssClass="LabelTitle" runat="server" Text="Relationship"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlRelationship" AppendDataBoundItems="true" Width="150px"
                            CssClass="Dropdownlist" runat="server" TabIndex="56">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvRelationship" runat="server" ValidationGroup="ValidateEmployeeFamily"
                            ControlToValidate="ddlRelationship" ErrorMessage="Please Select Relationship" Display="none"
                            SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblDOB" CssClass="LabelTitle" runat="server" Text="Date of Birth"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtDOB" runat="server" MaxLength="10" Width="80px" CssClass="textarea"
                            TabIndex="57" />
                          <cc1:CalendarExtender ID="cextCalDOB" runat="server" CssClass="MyCalendar" TargetControlID="txtDOB"
                            OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                          </cc1:CalendarExtender>
                          <cc1:MaskedEditExtender ID="meeDOB" runat="server" TargetControlID="txtDOB" Mask="99/99/9999"
                            MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnFamilySave" runat="server" CausesValidation="true" ValidationGroup="ValidateEmployeeFamily"
                              TabIndex="58" Text="Save" CssClass="btn" OnClick="ibtnFamilySave_Click" />&nbsp;
                            <asp:Button ID="ibtnFamilyCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                              TabIndex="59" OnClick="ibtnFamilyCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditEducation" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="350px" DefaultButton="ibtnEducationSave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeadingEducation" runat="server" Text="Add Education" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vceQualification" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvQualification">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trEducationHiddenRow" visible="false" runat="server">
                        <td id="tdEducationHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeeEducationID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label12" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblEducationQualification" CssClass="LabelTitle" runat="server" Text="Qualification"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlQualification" AppendDataBoundItems="true" Width="211px"
                            CssClass="Dropdownlist" runat="server" TabIndex="60">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvQualification" runat="server" ValidationGroup="ValidateEmployeeEducation"
                            ControlToValidate="ddlQualification" ErrorMessage="Please Select the Qualification"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblMajor" CssClass="LabelTitle" runat="server" Text="Major"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlMajor" AppendDataBoundItems="true" Width="211px" CssClass="Dropdownlist"
                            runat="server" TabIndex="61">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblUniversity" CssClass="LabelTitle" runat="server" Text="University" />
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlUniversity" AppendDataBoundItems="true" Width="211px" CssClass="Dropdownlist"
                            runat="server" TabIndex="62">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblInstitution" CssClass="LabelTitle" runat="server" Text="College / Institution"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtInstitution" MaxLength="200" Width="205px" CssClass="textarea"
                            runat="server" TabIndex="63" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblEducationClassObtained" CssClass="LabelTitle" runat="server" Text="Class Obtained"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtEducationClassObtained" MaxLength="50" Width="205px" CssClass="textarea"
                            runat="server" TabIndex="64" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblEducationYearOfPass" CssClass="LabelTitle" runat="server" Text="Year of Pass"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlEducationYearOfPass" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="70">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnEducationSave" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                              ValidationGroup="ValidateEmployeeEducation" TabIndex="66" OnClick="ibtnEducationSave_Click" />&nbsp;
                            <asp:Button ID="ibtnEducationCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" ImageAlign="Middle" TabIndex="67" OnClick="ibtnEducationCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditCertification" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="340px" DefaultButton="ibtnCertificationSave">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeadingCertify" runat="server" Text="Add Certification" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vceCertification" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvCertification">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trCertificationHiddenRow" visible="false" runat="server">
                        <td id="tdCertificationHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeeCertificationID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label18" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblEducationCertification" CssClass="LabelTitle" runat="server" Text="Certification"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtCertification" Width="205px" MaxLength="100" CssClass="textarea"
                            runat="server" TabIndex="68" />
                          <asp:RequiredFieldValidator ID="rfvCertification" runat="server" ValidationGroup="ValidateEmployeeCertification"
                            ControlToValidate="txtCertification" ErrorMessage="Please Enter the Certification"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblCertificationTechnology" CssClass="LabelTitle" runat="server" Text="Technology"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlCertificationTechnology" AppendDataBoundItems="true" Width="210px"
                            CssClass="Dropdownlist" runat="server" TabIndex="69">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblCertificationYearOfPass" CssClass="LabelTitle" runat="server" Text="Year of Pass"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlCertificationYearOfPass" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="70">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblIssuedBy" CssClass="LabelTitle" runat="server" Text="Issued by"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtIssuedBy" MaxLength="200" Width="205px" CssClass="textarea" runat="server"
                            TabIndex="71" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblCertificationClassObtained" CssClass="LabelTitle" runat="server"
                            Text="Class Obtained"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtCertificationClassObtained" MaxLength="50" Width="205px" CssClass="textarea"
                            runat="server" TabIndex="72" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblTranscriptID" CssClass="LabelTitle" runat="server" Text="Transcript ID" />
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtTranscriptID" MaxLength="20" Width="80px" CssClass="textarea"
                            runat="server" TabIndex="73" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnCertificationSave" runat="server" Text="Save" CssClass="btn"
                              CausesValidation="true" ValidationGroup="ValidateEmployeeCertification" TabIndex="74"
                              OnClick="ibtnCertificationSave_Click" />&nbsp;
                            <asp:Button ID="ibtnCertificationCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" TabIndex="75" OnClick="ibtnCertificationCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditSkill" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="300px" DefaultButton="ibtnSkillAdd">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopHeadingSkill" runat="server" Text="Add Skill" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vceSkillTechnology" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvSkillTechnology">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceSkillLevel" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvSkillLevel">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trSkillHiddenRow" visible="false" runat="server">
                        <td id="tdSkillHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeeSkillID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label15" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblSkillTechnology" CssClass="LabelTitle" runat="server" Text="Technology"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlSkillTechnology" AppendDataBoundItems="true" Width="205px"
                            CssClass="Dropdownlist" runat="server" TabIndex="76">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvSkillTechnology" runat="server" ValidationGroup="ValidateEmployeeSkill"
                            ControlToValidate="ddlSkillTechnology" ErrorMessage="Please Select the Technology"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label21" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblSkillLevel" CssClass="LabelTitle" runat="server" Text="Skill Level"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlSkillLevel" AppendDataBoundItems="true" Width="205px" CssClass="Dropdownlist"
                            runat="server" TabIndex="77">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvSkillLevel" runat="server" ValidationGroup="ValidateEmployeeSkill"
                            ControlToValidate="ddlSkillLevel" ErrorMessage="Please Select the Skill Level"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblSkillExperience" CssClass="LabelTitle" runat="server" Text="Experience"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlSkillExperienceYears" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="86">
                            <asp:ListItem Value="header">--Years--</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddlSkillExperienceMonths" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="87">
                            <asp:ListItem Value="header">--Months--</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                        </td>
                        <td class="tdTextResults" valign="middle" style="padding-top: 5px;" class="tdstyle">
                          <asp:Label ID="lblInYears" CssClass="LabelTitle" runat="server" Text="(in years)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"></asp:Label>
                          <asp:Label ID="lblInMonths" CssClass="LabelTitle" runat="server" Text="&nbsp;&nbsp;&nbsp;&nbsp;(in months)"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnSkillAdd" runat="server" CssClass="btn" Text="Save" CausesValidation="true"
                              ValidationGroup="ValidateEmployeeSkill" TabIndex="80" OnClick="ibtnSkillSave_Click" />&nbsp;
                            <asp:Button ID="ibtnSkillCancel" runat="server" Text="Cancel" CssClass="btn" CausesValidation="false"
                              ImageAlign="Middle" TabIndex="81" OnClick="ibtnSkillCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditExperience" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="380px" DefaultButton="ibtnExperienceAdd">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeadingExperience" runat="server" Text="Add Experience" CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vceExperienceOrganization" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvExperienceOrganization">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceOrganizationLocation" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvOrganizationLocation">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceExperienceCTC_1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvExperienceCTC">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceExperienceCTC_2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="cmvExperienceCTC">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vceExperienceCTC_3" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="revExperienceCTC">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trExperienceHiddenRow" visible="false" runat="server">
                        <td id="tdExperienceHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeeExperienceID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label22" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblExperienceOrganization" CssClass="LabelTitle" runat="server" Text="Organization"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtExperienceOrganization" Width="265px" MaxLength="200" CssClass="textarea"
                            runat="server" TabIndex="82" />
                          <asp:RequiredFieldValidator ID="rfvExperienceOrganization" runat="server" ValidationGroup="ValidateEmployeeExperience"
                            ControlToValidate="txtExperienceOrganization" ErrorMessage="Please Enter the Organization"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label25" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblOrganizationLocation" CssClass="LabelTitle" runat="server" Text="Location"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtOrganizationLocation" Width="265px" MaxLength="100" CssClass="textarea"
                            runat="server" TabIndex="83" />
                          <asp:RequiredFieldValidator ID="rfvOrganizationLocation" runat="server" ValidationGroup="ValidateEmployeeExperience"
                            ControlToValidate="txtOrganizationLocation" ErrorMessage="Please Enter the Location"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblOrganizationDesignation" CssClass="LabelTitle" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtOrganizationDesignation" Width="265px" MaxLength="100" CssClass="textarea"
                            runat="server" TabIndex="84" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblExperienceFrom" CssClass="LabelTitle" runat="server" Text="From"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlExperienceFromMonth" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="85">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddlExperienceFromYear" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="86">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblExperienceTo" CssClass="LabelTitle" runat="server" Text="To"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlExperienceToMonth" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="87">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddlExperienceToYear" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="88">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label26" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblCTC" CssClass="LabelTitle" runat="server" Text="CTC (In Lakhs)"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtCTC" Width="64px" MaxLength="6" Text="" runat="server" TabIndex="89" />
                          <cc1:FilteredTextBoxExtender ID="ftbeCTC" runat="server" TargetControlID="txtCTC"
                            FilterType="Custom" ValidChars=".0123456789" Enabled="True" />
                          <asp:RequiredFieldValidator ID="rfvExperienceCTC" runat="server" ValidationGroup="ValidateEmployeeExperience"
                            ControlToValidate="txtCTC" ErrorMessage="Please Enter the CTC" Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                          <asp:CompareValidator ID="cmvExperienceCTC" runat="server" ValidationGroup="ValidateEmployeeExperience"
                            ControlToValidate="txtCTC" ErrorMessage="CTC Should Be Greater Than Zero." ValueToCompare="0"
                            Operator="GreaterThan" Type="Double" Display="none" SetFocusOnError="True" />
                          <asp:RegularExpressionValidator ID="revExperienceCTC" runat="server" ValidationExpression="^\d{1,3}(\.\d{1,2})?$"
                            ControlToValidate="txtCTC" ErrorMessage="The Format is <999.99>" ValidationGroup="ValidateEmployeeExperience"
                            Display="none" SetFocusOnError="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="top" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblJobProfile" CssClass="LabelTitle" runat="server" Text="Job Profile"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtJobProfile" Width="260px" MaxLength="2000" TextMode="MultiLine"
                            Rows="6" CssClass="textarea" runat="server" TabIndex="90"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnExperienceAdd" runat="server" Text="Save" CssClass="btn" CausesValidation="true"
                              ValidationGroup="ValidateEmployeeExperience" TabIndex="91" OnClick="ibtnExperienceSave_Click" />&nbsp;
                            <asp:Button ID="ibtnExperienceCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" TabIndex="92" OnClick="ibtnExperienceCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditPreviousProject" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="410px" DefaultButton="ibtnPreviousProjectAdd">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeadingPreProject" runat="server" Text="Add Previous Project"
                            CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vcePreviousProjectName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvPreviousProjectName">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vcePreviousProjectClientName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvPreviousProjectClientName">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vcePreviousProjectTeamSize" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvPreviousProjectTeamSize">
                          </cc1:ValidatorCalloutExtender>
                          <cc1:ValidatorCalloutExtender ID="vcePreviousProjectTeamSize2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="cmvPreviousProjectTeamSize">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trPreviousProjectHiddenRow" visible="false" runat="server">
                        <td id="tdPreviousProjectHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeePreviousEmployersProjectID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label27" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblPreviousProjectName" CssClass="LabelTitle" runat="server" Text="Project Name"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectName" Width="260px" MaxLength="100" CssClass="textarea"
                            runat="server" TabIndex="93" />
                          <asp:RequiredFieldValidator ID="rfvPreviousProjectName" runat="server" ValidationGroup="ValidateEmployeePreviousProject"
                            ControlToValidate="txtPreviousProjectName" ErrorMessage="Please Enter the Project Name"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label30" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblPreviousProjectClientName" CssClass="LabelTitle" runat="server"
                            Text="Client Name"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectClientName" MaxLength="100" Width="200px" Text=""
                            CssClass="textarea" runat="server" TabIndex="94" />
                          <asp:RequiredFieldValidator ID="rfvPreviousProjectClientName" runat="server" ValidationGroup="ValidateEmployeePreviousProject"
                            ControlToValidate="txtPreviousProjectClientName" ErrorMessage="Please Enter the Client Name"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                          <asp:CheckBox ID="chkIsOnsite" CssClass="LabelTitle" runat="server" TabIndex="95">
                          </asp:CheckBox>
                          <asp:Label ID="lblIsOnsite" CssClass="LabelTitle" runat="server" Text="Onsite"></asp:Label>
                        </td>
                      </tr>
                      <tr id="trOnsiteLocationRow" visible="true" runat="server">
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblOnsiteLocation" CssClass="LabelTitle" runat="server" Text="Onsite Location" />
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtOnsiteLocation" MaxLength="100" Width="260px" Text="" runat="server"
                            CssClass="textarea" TabIndex="96" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectTechnology" CssClass="LabelTitle" runat="server"
                            Text="Technology"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectTechnology" MaxLength="1000" Width="260px" Text=""
                            runat="server" TabIndex="97" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectDomain" CssClass="LabelTitle" runat="server" Text="Domain"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectDomain" MaxLength="500" Width="260px" Text=""
                            runat="server" TabIndex="98" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectFrom" CssClass="LabelTitle" runat="server" Text="From"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPreviousProjectFromMonth" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="99">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddlPreviousProjectFromYear" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="100">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectTo" CssClass="LabelTitle" runat="server" Text="To"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPreviousProjectToMonth" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="101">
                            <asp:ListItem Value="0">Month</asp:ListItem>
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                          </asp:DropDownList>
                          <asp:DropDownList ID="ddlPreviousProjectToYear" AppendDataBoundItems="true" Width="75px"
                            CssClass="Dropdownlist" runat="server" TabIndex="102">
                            <asp:ListItem Value="0">Year</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label31" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblPreviousProjectTeamSize" CssClass="LabelTitle" runat="server" Text="Team Size"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectTeamSize" Width="40px" Text="" runat="server"
                            MaxLength="3" TabIndex="103" />
                          <cc1:FilteredTextBoxExtender ID="ftbePreviousProjectTeamSize" runat="server" TargetControlID="txtPreviousProjectTeamSize"
                            FilterType="Custom" ValidChars="1234567890" Enabled="True" />
                          <asp:RequiredFieldValidator ID="rfvPreviousProjectTeamSize" runat="server" ValidationGroup="ValidateEmployeePreviousProject"
                            ControlToValidate="txtPreviousProjectTeamSize" ErrorMessage="Please Enter The Team Size."
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                          <asp:CompareValidator ID="cmvPreviousProjectTeamSize" runat="server" ValidationGroup="ValidateEmployeePreviousProject"
                            ControlToValidate="txtPreviousProjectTeamSize" ErrorMessage="Team Size Should Be Greater Than Zero."
                            ValueToCompare="0" Operator="GreaterThan" Display="none" SetFocusOnError="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectRolePlayed" CssClass="LabelTitle" runat="server"
                            Text="Role Played"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectRolePlayed" Width="260px" MaxLength="250" CssClass="textarea"
                            runat="server" TabIndex="104" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="top" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPreviousProjectDescription" CssClass="LabelTitle" runat="server"
                            Text="Project Description"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPreviousProjectDescription" Width="260px" TextMode="MultiLine"
                            MaxLength="2000" Rows="6" CssClass="textarea" runat="server" TabIndex="105"></asp:TextBox>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnPreviousProjectAdd" runat="server" Text="Save" CssClass="btn"
                              CausesValidation="true" ValidationGroup="ValidateEmployeePreviousProject" TabIndex="106"
                              OnClick="ibtnPreviousProjectSave_Click" />&nbsp;
                            <asp:Button ID="ibtnPreviousProjectCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" TabIndex="107" OnClick="ibtnPreviousProjectCancel_Click" />&nbsp;
                          </center>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <tr>
                <td colspan="2">
                  <asp:Panel ID="pnlEditPresentProject" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="330px" DefaultButton="ibtnPresentProjectAdd">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <asp:Label ID="lblPopupHeadingPresProject" runat="server" Text="Add Present Project"
                            CssClass="PopupPanelHeading"></asp:Label>
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="text-align: center;" class="tdstyle">
                          <hr />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" style="width: 100%;" class="tdstyle tdTextResults">
                          <cc1:ValidatorCalloutExtender ID="vcePresentProject" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                            TargetControlID="rfvPresentProjectName">
                          </cc1:ValidatorCalloutExtender>
                        </td>
                      </tr>
                      <tr id="trPresentProjectHiddenRow" visible="false" runat="server">
                        <td id="tdPresentProjectHiddenColumn" runat="server" colspan="2" class="tdstyle">
                          <asp:HiddenField ID="hdfEmployeePresentEmployerProjectID" runat="server" Value="0" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label32" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblPresentProjectName" CssClass="LabelTitle" runat="server" Text="Project Name"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPresentProjectName" AppendDataBoundItems="true" Width="225px"
                            CssClass="Dropdownlist" runat="server" TabIndex="108">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="rfvPresentProjectName" runat="server" ValidationGroup="ValidateEmployeePresentProject"
                            ControlToValidate="ddlPresentProjectName" ErrorMessage="Please Select the Project Name"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentProjectJobRole" CssClass="LabelTitle" runat="server" Text="Job Role"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:DropDownList ID="ddlPresentProjectJobRole" AppendDataBoundItems="true" Width="225px"
                            CssClass="Dropdownlist" runat="server" TabIndex="109">
                            <asp:ListItem Value="">-- Select One --</asp:ListItem>
                          </asp:DropDownList>
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="Label6" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                          <asp:Label ID="lblPresentProjectFromDate" CssClass="LabelTitle" runat="server" Text="From Date"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentProjectFromDate" runat="server" MaxLength="10" Width="80px"
                            CssClass="textarea" TabIndex="110" />
                          <cc1:CalendarExtender ID="cextCalPresentProjectFromDate" CssClass="MyCalendar" runat="server"
                            TargetControlID="txtPresentProjectFromDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                          </cc1:CalendarExtender>
                          <asp:RequiredFieldValidator ID="rfvPresentProjectStartDate" runat="server" ValidationGroup="ValidateEmployeePresentProject"
                            ControlToValidate="txtPresentProjectFromDate" ErrorMessage="Please Enter the From Date"
                            Display="none" SetFocusOnError="True">
                          </asp:RequiredFieldValidator>
                          <cc1:MaskedEditExtender ID="meePresentProjectFromDate" runat="server" TargetControlID="txtPresentProjectFromDate"
                            Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td valign="middle" class="tdstyle tdLabelTitle">
                          <asp:Label ID="lblPresentProjectToDate" CssClass="LabelTitle" runat="server" Text="To Date"></asp:Label>
                        </td>
                        <td valign="middle" class="tdstyle tdTextResults">
                          <asp:TextBox ID="txtPresentProjectToDate" runat="server" MaxLength="10" Width="80px"
                            CssClass="textarea" TabIndex="111" />
                          <cc1:CalendarExtender ID="cextCalPresentProjectToDate" CssClass="MyCalendar" runat="server"
                            TargetControlID="txtPresentProjectToDate" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}">
                          </cc1:CalendarExtender>
                          <cc1:MaskedEditExtender ID="meePresentProjectToDate" runat="server" TargetControlID="txtPresentProjectToDate"
                            Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                            ErrorTooltipEnabled="True" />
                        </td>
                      </tr>
                      <tr>
                        <td colspan="2" class="tdstyle">
                          <center>
                            <br />
                            <asp:Button ID="ibtnPresentProjectAdd" runat="server" Text="Save" CssClass="btn"
                              CausesValidation="true" ValidationGroup="ValidateEmployeePresentProject" TabIndex="112"
                              OnClick="ibtnPresentProjectSave_Click" />&nbsp;
                            <asp:Button ID="ibtnPresentProjectCancel" runat="server" Text="Cancel" CssClass="btn"
                              CausesValidation="false" ImageAlign="Middle" TabIndex="113" OnClick="ibtnPresentProjectCancel_Click" />&nbsp;
                          </center>
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
      <%--View Designation History Popup Panel--%>
      <asp:Panel ID="pnlViewDesignationHistory" runat="server" CssClass="modalPopup" Style="display: none"
        Width="400px" DefaultButton="">
        <table style="width: 100%;" class="tdLabelCenter" border="0" cellpadding="1" cellspacing="2">
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <asp:Label ID="Label8" runat="server" Text="Employee Designation History" CssClass="PopupPanelHeading"></asp:Label>
            </td>
          </tr>
          <tr>
            <td colspan="2" class="tdLabelCenter">
              <hr />
            </td>
          </tr>
          <tr>
            <td valign="middle" class="tdstyle tdLabelTitle">
              <asp:GridView ID="gvEmployeeDesignationHistory" runat="server" AutoGenerateColumns="false"
                CellPadding="5" HeaderStyle-HorizontalAlign="Left" Width="100%" HeaderStyle-CssClass="GridviewHeader"
                OnRowDataBound="gvEmployeeDesignationHistory_RowDataBound">
                <Columns>
                  <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" HeaderText="S.No" ItemStyle-Width="2%">
                    <ItemTemplate>
                      <asp:Label ID="lblSerial" runat="server"></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="Description" HeaderText="Designation"
                    ItemStyle-Width="0%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="DesignationID" HeaderText="" Visible="false"
                    ItemStyle-Width="0%"></asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="FromDate" HeaderText="From Date" ItemStyle-Width="0%">
                  </asp:BoundField>
                  <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                    ItemStyle-CssClass="Gridview" DataField="ToDate" HeaderText="To Date" ItemStyle-Width="0%">
                  </asp:BoundField>
                </Columns>
              </asp:GridView>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              <center>
                <asp:Button ID="Button1" OnClientClick="closeDesignationPopup();" runat="server"
                  CssClass="btn" Text="Close" />
              </center>
            </td>
          </tr>
          <tr>
            <td>
            </td>
          </tr>
        </table>
      </asp:Panel>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
