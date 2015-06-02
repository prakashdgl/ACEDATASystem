<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeProfileView.aspx.cs"
  Inherits="ACE.PurchaseOrder.Employee.EmployeeProfileView" MasterPageFile="~/Order.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
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
    <asp:Label ID="Label1" runat="server" CssClass="pageTitleLabel" Text="Employee Profile View"></asp:Label><hr />
  </div>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <table id="Table1" runat="server" cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
          <td colspan="2" style="height: 25px;">
            &nbsp;
          </td>
        </tr>
        <tr>
          <td style="width: 13%" align="right">
            <asp:Label ID="lblEmployee" runat="server" CssClass="LabelTitle" Text="Employee Name" />
          </td>
          <td style="width: 91%" class="tdTextResults">
            <asp:DropDownList ID="ddlEmployee" Width="220px" CssClass="Dropdownlist" runat="server"
              AutoPostBack="True" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" TabIndex="0">
            </asp:DropDownList>
          </td>
        </tr>
        <tr id="trEmployeeDetailView" runat="server">
          <td colspan="2">
            <table cellpadding="2" cellspacing="2" border="0" width="100%">
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
                        <asp:Label ID="lblDateOfBirth" runat="server" Text="Date Of Birth" CssClass="LabelTitle"></asp:Label>
                      </td>
                      <td class="tdTextResults">
                        <asp:Label ID="lblDateOfBirthValue" runat="server" Text="Date Of Birth Date" CssClass="TextResults"></asp:Label>
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
                        <asp:Label ID="lblOfficialMailID" runat="server" Text="Official Mail ID" CssClass="LabelTitle"></asp:Label>
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
                                <asp:Label ID="lblBloodGroup" runat="server" Text="BloodGroup" CssClass="LabelTitle"></asp:Label>
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
                                <asp:Label ID="lblPinCode1" runat="server" Text="PinCode" CssClass="LabelTitle"></asp:Label>
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
                                <asp:Label ID="lblPinCode2" runat="server" Text="PinCode" CssClass="LabelTitle"></asp:Label>
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
                                <asp:Label ID="lblDateOfIssue" runat="server" Text="Date Of Issue" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfDateOfIssue" runat="server" />
                                <asp:Label ID="lblDateOfIssueValue" runat="server" Text="Date Of Issue Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblDateOfExpiry" runat="server" Text="Date Of Expiry" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfDateOfExpiry" runat="server" />
                                <asp:Label ID="lblDateOfExpiryValue" runat="server" Text="Date Of Expiry Description"
                                  CssClass="TextResults"></asp:Label>
                              </td>
                            </tr>
                            <tr>
                              <td class="tdLabelTitle">
                                <asp:Label ID="lblPlaceOfIssue" runat="server" Text="Place Of Issue" CssClass="LabelTitle"></asp:Label>
                              </td>
                              <td class="tdTextResults">
                                <asp:HiddenField ID="hdfPlaceOfIssue" runat="server" />
                                <asp:Label ID="lblPlaceOfIssueValue" runat="server" Text="Place Of Issue Description"
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
                                <asp:Label ID="lblPersonalEmailID" runat="server" Text="Personal Mail ID" CssClass="LabelTitle"></asp:Label>
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
              <tr>
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
                          <asp:GridView ID="gvEmployeeLanguages" runat="server" DataKeyNames="EmployeeLanguageKnownID,EmployeeID,LanguageID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeLanguages_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeLanguages_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeLanguageKnownID"
                                HeaderText="EmployeeLanguageKnownID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="LanguageID" HeaderText="LanguageID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="LanguageDescription" HeaderText="Language"
                                ItemStyle-Width="49%" HeaderStyle-HorizontalAlign="Left" SortExpression="LanguageDescription">
                              </asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Read" ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsRead" Checked='<%# Eval("IsRead") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="Write" ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsWrite" Checked='<%# Eval("IsWrite") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
                              </asp:TemplateField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="Speak" ItemStyle-Width="15%">
                                <ItemTemplate>
                                  <asp:CheckBox ID="chkIsSpeak" Checked='<%# Eval("IsSpeak") %>' runat="server" Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" />
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
                          <table id="Table2" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                          <asp:GridView ID="gvEmployeeFamily" runat="server" DataKeyNames="EmployeeFamilyID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeFamily_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeFamily_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeFamilyID" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="EmployeeFamilyID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="EmployeeID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Name" HeaderStyle-HorizontalAlign="Left"
                                HeaderText="Name" ItemStyle-Width="33%" SortExpression="Name"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="GenderID" HeaderStyle-HorizontalAlign="Left"
                                Visible="false" HeaderText="GenderID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" DataField="GenderDescription"
                                HeaderText="Gender" ItemStyle-Width="20%" SortExpression="GenderDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                Visible="false" HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview"
                                DataField="RelationshipID" HeaderText="RelationshipID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" DataField="RelationshipDescription"
                                HeaderText="Relationship" ItemStyle-Width="25%" SortExpression="RelationshipDescription">
                              </asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                                HtmlEncode="false" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="DOB" ItemStyle-Width="16%" HeaderText="Date Of Birth"
                                SortExpression="DOB" />
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
                          <asp:GridView ID="gvEmployeeEducation" runat="server" DataKeyNames="EmployeeEducationID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeEducation_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeEducation_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                ItemStyle-HorizontalAlign="Right" HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeEducationID" HeaderText="EmployeeEducationID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="QualificationID" HeaderText="Name"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="QualificationDescription" HeaderText="Qualification"
                                ItemStyle-Width="10%" SortExpression="QualificationDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="MajorID" HeaderText="MajorID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="MajorDescription" HeaderText="Major" ItemStyle-Width="20%"
                                SortExpression="MajorDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="Gridview" DataField="YearOfPass" HeaderText="Year Of Pass"
                                ItemStyle-Width="10%" SortExpression="YearOfPass"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="UniversityID" HeaderText="UniversityID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="UniversityDescription" HeaderText="University"
                                ItemStyle-Width="20%" SortExpression="UniversityDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="InstitutionDescription" HeaderText="Institution"
                                ItemStyle-Width="24%" SortExpression="InstitutionDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="Gridview" DataField="ClassObtained" HeaderText="Class Obtained"
                                ItemStyle-Width="10%" SortExpression="ClassObtained"></asp:BoundField>
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
                          <table id="Table3" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                          <asp:GridView ID="gvEmployeeCertification" runat="server" DataKeyNames="EmployeeCertificationID,EmployeeID"
                            CellPadding="5" HeaderStyle-CssClass="GridviewHeader" HeaderStyle-HorizontalAlign="Left"
                            OnRowDataBound="gvEmployeeCertification_RowDataBound" AllowSorting="true" OnSorting="gvEmployeeCertification_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeCertificationID"
                                HeaderText="EmployeeCertificationID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Certification" HeaderText="Certification"
                                ItemStyle-Width="23%" SortExpression="Certification"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="TechnologyID" HeaderText="TechnologyID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="TechnologyDescription" HeaderText="Technology"
                                ItemStyle-Width="15%" SortExpression="TechnologyDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="center"
                                ItemStyle-CssClass="Gridview" DataField="YearOfPass" HeaderText="Year Of Pass"
                                ItemStyle-Width="7%" SortExpression="YearOfPass"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="IssuedBy" HeaderText="Issued By" ItemStyle-Width="25%"
                                SortExpression="IssuedBy"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-CssClass="Gridview" DataField="ClassObtained" HeaderText="Class Obtained"
                                ItemStyle-Width="10%" SortExpression="ClassObtained"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="TranscriptID" HeaderText="Transcript ID"
                                ItemStyle-Width="14%" SortExpression="TranscriptID"></asp:BoundField>
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
                          <table id="Table4" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                          <asp:GridView ID="gvEmployeeSkill" runat="server" DataKeyNames="EmployeeSkillID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeSkill_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeSkill_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeSkillID" HeaderText="EmployeeSkillID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="TechnologyID" HeaderText="TechnologyID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="TechnologyDescription" HeaderText="Skill"
                                ItemStyle-Width="34%" SortExpression="TechnologyDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="SkillLevelID" HeaderText="SkillLevelID"
                                Visible="false" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="SkillLevelDescription" HeaderText="Skill Level"
                                ItemStyle-Width="20%" SortExpression="SkillLevelDescription"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ExperienceInYears" HeaderText="Experience In Years"
                                ItemStyle-Width="20%" SortExpression="ExperienceInYears"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ExperienceInMonths" HeaderText="Experience In Months"
                                ItemStyle-Width="20%" SortExpression="ExperienceInMonths"></asp:BoundField>
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
                          <table id="Table5" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                          <asp:GridView ID="gvEmployeeExperience" runat="server" DataKeyNames="EmployeeExperienceID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeeExperience_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeExperience_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeExperienceID"
                                HeaderText="EmployeeExperienceID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%"></asp:BoundField>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Organization Name" SortExpression="OrganizationName"
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
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Location" HeaderText="Location" ItemStyle-Width="10%"
                                SortExpression="Location"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="Designation" HeaderText="Designation"
                                ItemStyle-Width="35%" SortExpression="Designation"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="FromMonth" HeaderText="FromMonth"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="FromYear" HeaderText="FromYear"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromMonthAndYear" HeaderText="From" ItemStyle-Width="10%"
                                SortExpression="FromMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ToMonth" HeaderText="ToMonth"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ToYear" HeaderText="ToYear"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ToMonthAndYear" HeaderText="To" ItemStyle-Width="10%"
                                SortExpression="ToMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="CTC" HeaderText="CTC" ItemStyle-Width="6%"
                                SortExpression="CTC" DataFormatString="{0:N2}" HtmlEncode="False"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="JobProfile" HeaderText="JobProfile"
                                ItemStyle-Width="0%" />
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
                          <table id="Table6" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                          <asp:GridView ID="gvEmployeePreviousProject" runat="server" DataKeyNames="EmployeePreviousEmployersProjectID,EmployeeID"
                            CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnRowDataBound="gvEmployeePreviousProject_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeePreviousProject_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeePreviousEmployersProjectID"
                                HeaderText="EmployeePreviousEmployersProjectID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%" />
                              <asp:TemplateField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" HeaderText="Project Name" SortExpression="ProjectName"
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
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="ClientName" HeaderText="Client Name" ItemStyle-Width="13%"
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
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="FromMonth" HeaderText="FromMonth"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="FromYear" HeaderText="FromYear"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="FromMonthAndYear" HeaderText="From" ItemStyle-Width="10%"
                                SortExpression="FromMonthAndYear"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ToMonth" HeaderText="ToMonth"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ToYear" HeaderText="ToYear"
                                ItemStyle-Width="0%" />
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
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ProjectDescription" HeaderText="Project Description"
                                ItemStyle-Width="0%"></asp:BoundField>
                            </Columns>
                          </asp:GridView>
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
                            AllowSorting="true" OnSorting="gvEmployeePresentProject_Sorting">
                            <Columns>
                              <asp:TemplateField ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                HeaderText="S.No" ItemStyle-Width="2%">
                                <ItemTemplate>
                                  <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Left" />
                              </asp:TemplateField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeePresentEmployerProjectID"
                                HeaderText="EmployeePreviousEmployersProjectID" ItemStyle-Width="0%"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="EmployeeID" HeaderText="EmployeeID"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="ProjectID" HeaderText="ProjectID"
                                ItemStyle-Width="0%" />
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
                                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="Gridview" DataField="FromDate"
                                HeaderText="From Date" ItemStyle-Width="10%" SortExpression="FromDate"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                DataFormatString="{0:dd/MM/yyyy}" ItemStyle-CssClass="Gridview" DataField="ToDate"
                                HeaderText="To Date" ItemStyle-Width="10%" SortExpression="ToDate"></asp:BoundField>
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                Visible="false" ItemStyle-CssClass="Gridview" DataField="JobRoleID" HeaderText="JobRoleID"
                                ItemStyle-Width="0%" />
                              <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                ItemStyle-CssClass="Gridview" DataField="JobRoleDescription" HeaderText="Role"
                                ItemStyle-Width="15%" SortExpression="JobRoleDescription"></asp:BoundField>
                            </Columns>
                          </asp:GridView>
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>
              <%--  Employee Additional Details--%>
              <%--  Employee Designation History--%>
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
                          <table id="Table10" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
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
                          <table id="Table13" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                                      <asp:Label ID="Label3" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>&nbsp;<asp:Label
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
                                <asp:ImageButton ID="currentDesignationSubmitBtn" runat="server" ImageAlign="Middle"
                                  ImageUrl="../Images/btnSubmit.gif" TabIndex="106" OnClick="ibtnDesignationSubmit_Click"
                                  CausesValidation="true" ValidationGroup="ValidateDesignation" />
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
                  <cc1:CollapsiblePanelExtender ID="cpeClientInfo" runat="server" 
                    AutoCollapse="false" AutoExpand="false" CollapseControlID="pnlClientInfoTitle" 
                    Collapsed="True" CollapsedImage="~/Images/expand.jpg" CollapsedSize="0" 
                    CollapsedText="Show Details..." ExpandControlID="pnlClientInfoTitle" 
                    ExpandDirection="Vertical" ExpandedImage="~/Images/collapse.jpg" 
                    ExpandedSize="0" ExpandedText="Hide Details" 
                    ImageControlID="imgClientInfoExpandOrCollapse" ScrollContents="false" 
                    SuppressPostBack="true" TargetControlID="pnlClientInfoContent" 
                    TextLabelID="lblClientInfoShowOrHide" />
                  <asp:Panel ID="pnlClientInfoTitle" runat="server" 
                    CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table ID="tblClientInfo" runat="server" border="0" cellpadding="0" 
                            cellspacing="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblEmpClientProInfo" runat="server" 
                                  Text="Employee Client/Project Info." />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgClientInfoExpandOrCollapse" runat="server" 
                                  ImageUrl="~/Images/expand.jpg" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblClientInfoShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td class="tdLabelTitle" style="width: 5%">
                          <table ID="tblTitle" runat="server" border="0" cellpadding="0" cellspacing="0">
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
                  <asp:Panel ID="pnlClientInfoContent" runat="server" CssClass="collapsePanel" 
                    ScrollBars="None">
                    <table ID="tblClintInfo" runat="server" border="0" cellpadding="2" 
                      cellspacing="2" width="100%">
                      <tr>
                        <td>
                          <table ID="tblClintInfoContent" runat="server" border="0" cellpadding="2" 
                            cellspacing="2" width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="lblClientBameMandatory" runat="server" CssClass="LabelMandatory" 
                                        Text="*"></asp:Label>
                                      &nbsp;<asp:Label ID="lblClientNameTitle" runat="server" CssClass="LabelTitle" 
                                        Text="Client Name" />
                                    </td>
                                    <td class="tdTextResults" style="width: 70%" valign="middle">
                                      <asp:DropDownList ID="ddlClientName" runat="server" AutoPostBack="true" 
                                        CssClass="Dropdownlist" 
                                        OnSelectedIndexChanged="ddlClientName_SelectedIndexChanged" Width="220px">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvClientName" runat="server" 
                                        ControlToValidate="ddlClientName" Display="none" 
                                        ErrorMessage="Please Select the Client Name" ValidationGroup="ValidateClient">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceClientName" runat="Server" 
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvClientName">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                  <tr>
                                    <td class="tdLabelTitle LabelTitle" style="width: 30%" valign="middle">
                                      <asp:Label ID="lblProjectNameMandatory" runat="server" 
                                        CssClass="LabelMandatory" Text="*"></asp:Label>
                                      &nbsp;<asp:Label ID="lblProjectName" runat="server" CssClass="LabelTitle" 
                                        Text="Project Name" />
                                    </td>
                                    <td class="tdTextResults" style="width: 70%" valign="middle">
                                      <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="Dropdownlist" 
                                        Width="220px">
                                      </asp:DropDownList>
                                      <asp:RequiredFieldValidator ID="rfvProjectName" runat="server" 
                                        ControlToValidate="ddlProjectName" Display="none" 
                                        ErrorMessage="Please Select the Project Name" ValidationGroup="ValidateClient">
                                      </asp:RequiredFieldValidator>
                                      <cc1:ValidatorCalloutExtender ID="vceProjectName" runat="Server" 
                                        HighlightCssClass="validatorCalloutHighlight" TargetControlID="rfvProjectName">
                                      </cc1:ValidatorCalloutExtender>
                                    </td>
                                  </tr>
                                </table>
                                <br />
                              </td>
                              <td style="width: 50%; padding-top: 10px;" valign="top">
                                <asp:LinkButton ID="viewClientProjectHistoryLinkBtn" runat="server">View Client/Project History</asp:LinkButton>
                                <cc1:ModalPopupExtender ID="mpeClientProjectHistory" runat="server" 
                                  BackgroundCssClass="modalBackground" Drag="false" DynamicServicePath="" 
                                  Enabled="True" PopupControlID="pnlViewClientProjectHistory" 
                                  TargetControlID="viewClientProjectHistoryLinkBtn">
                                </cc1:ModalPopupExtender>
                                <br />
                                <asp:ImageButton ID="CPSubmitBtn" runat="server" CausesValidation="true" 
                                  ImageAlign="Middle" ImageUrl="../Images/btnSubmit.gif" 
                                  OnClick="CPSubmitBtn_Click" TabIndex="106" ValidationGroup="ValidateClient" />
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
              <%-- <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeEmpDesignHistory" runat="server" TargetControlID="pnlViewDesignationcontent"
                    CollapsedSize="0" ExpandedSize="0" Collapsed="True" ExpandControlID="pnlViewDesignationTitle"
                    CollapseControlID="pnlViewDesignationTitle" AutoCollapse="false" AutoExpand="false"
                    ScrollContents="false" TextLabelID="lblProjectShowOrHide" CollapsedText="Show Details..."
                    ExpandedText="Hide Details" ImageControlID="imgProjectExpandOrCollapse" ExpandedImage="~/Images/collapse.jpg"
                    CollapsedImage="~/Images/expand.jpg" ExpandDirection="Vertical" SuppressPostBack="true" />
                  <asp:Panel ID="pnlViewDesignationTitle" runat="server" CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table id="Table7" runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="Label2" runat="server" Text="Employee Designation History" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="Image1" ImageUrl="~/Images/expand.jpg" runat="server" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblEmpDesiShowHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td style="width: 5%" class="tdLabelTitle">
                          <table id="Table9" runat="server" cellpadding="0" cellspacing="0" border="0">
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
                  <asp:Panel ID="pnlViewDesignationcontent" runat="server" CssClass="collapsePanel"
                    ScrollBars="None">
                    <table style="width: 100%;" class="tdLabelCenter" border="0" cellpadding="1" cellspacing="2">
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
                                ItemStyle-Width="5%"></asp:BoundField>
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
                        </td>
                      </tr>
                    </table>
                  </asp:Panel>
                </td>
              </tr>--%>
              <tr>
                <td colspan="2">
                  <cc1:CollapsiblePanelExtender ID="cpeLeaveApprovar" runat="server" 
                    AutoCollapse="false" AutoExpand="false" 
                    CollapseControlID="pnlLeaveApprovarTitle" Collapsed="True" 
                    CollapsedImage="~/Images/expand.jpg" CollapsedSize="0" 
                    CollapsedText="Show Details..." ExpandControlID="pnlLeaveApprovarTitle" 
                    ExpandDirection="Vertical" ExpandedImage="~/Images/collapse.jpg" 
                    ExpandedSize="0" ExpandedText="Hide Details" 
                    ImageControlID="imgLeaveApprovarExpandOrCollapse" ScrollContents="false" 
                    SuppressPostBack="true" TargetControlID="pnlLeaveApprovarContent" 
                    TextLabelID="lblLeaveApprovarShowOrHide" />
                  <asp:Panel ID="pnlLeaveApprovarTitle" runat="server" 
                    CssClass="collapsePanelHeader">
                    <table border="0" style="width: 100%">
                      <tr>
                        <td style="width: 95%">
                          <table ID="tblLeaveAppTitle" runat="server" border="0" cellpadding="0" 
                            cellspacing="0" width="100%">
                            <tr>
                              <td class="tdTextResults" style="width: 80%">
                                <asp:Label ID="lblLeaveApprovar" runat="server" Text="Leave Approver" />
                              </td>
                              <td class="tdLabelTitle" style="width: 5%">
                                <asp:Image ID="imgLeaveApprovarExpandOrCollapse" runat="server" 
                                  ImageUrl="~/Images/expand.jpg" />
                              </td>
                              <td class="tdTextResults" style="width: 15%">
                                &nbsp;<asp:Label ID="lblLeaveApprovarShowOrHide" runat="server" Text="" />
                              </td>
                            </tr>
                          </table>
                        </td>
                        <td class="tdLabelTitle" style="width: 5%">
                          <table ID="tblLeave" runat="server" border="0" cellpadding="0" cellspacing="0">
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
                  <asp:Panel ID="pnlLeaveApprovarContent" runat="server" CssClass="collapsePanel" 
                    ScrollBars="None">
                    <table ID="tblLeaveApprovarContent" runat="server" border="0" cellpadding="2" 
                      cellspacing="2" width="100%">
                      <tr>
                        <td>
                          <table ID="tblLeaveApprovar" runat="server" border="0" cellpadding="2" 
                            cellspacing="2" width="100%">
                            <tr>
                              <td style="width: 50%" valign="middle">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                  <tr>
                                    <td class="LabelTitle tdLabelTitle" style="width: 40%" valign="middle">
                                      <asp:Label ID="lblLeaveApprovarEdit" runat="server" CssClass="LabelTitle" 
                                        Text="Leave Approver Mail ID" />
                                    </td>
                                    <td class="tdTextResults" style="width: 70%" valign="middle">
                                      <asp:Label ID="txtLeaveApprovarEdit" runat="server" CssClass="LabelTitle" 
                                        Text="Leave Approver Mail ID" />
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
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
