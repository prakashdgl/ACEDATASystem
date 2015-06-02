<%@ Page Language="C#" AutoEventWireup="true" Title="Manage Employee" CodeBehind="ManageEmployee.aspx.cs"
    EnableEventValidation="false" Inherits="ACE.PurchaseOrder.ManageEmployee" MasterPageFile="~/Order.Master" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <div class="pageTitleDiv">
        <asp:Label runat="server" CssClass="pageTitleLabel" Text="Manage Employee"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="searchbar">
                <div class="rightdiv">
                    <%--   <asp:ImageButton ID="btnCreate" runat="server" ImageUrl="../Images/btnCreateNew.gif"
            OnClientClick="btnCreateClick();" ImageAlign="Top" TabIndex="4" />--%>
                    <asp:Button ID="btnCreate" runat="server" Text="Create New" OnClientClick="btnCreateClick();"
                        CssClass="btnnew" TabIndex="4" />
                </div>
            </div>
            <table id="tblUser" runat="server" cellpadding="0" cellspacing="0" width="100%" class="tableHeaderGridview">
                <tr id="trSearchAndAddRow" runat="server">
                    <td class="tdTextResults">
                        <asp:RadioButtonList ID="rblReportOption" CssClass="LabelTitle" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" TabIndex="4" OnSelectedIndexChanged="rblReportOption_SelectedIndexChanged">
                            <asp:ListItem Text="Activated" Value="true" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Deactivated" Value="false"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 32%; padding-right: 15px" class="tdLabelTitle">
                        <asp:Label ID="lblSearch" CssClass="LabelTitle" runat="server" Text="Search"></asp:Label>
                        <asp:TextBox ID="txtSearchID" runat="server" autocomplete="off" class="search"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table runat="server" cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr id="trGridRow" runat="server">
                    <td colspan="2" style="width: 100%">
                        <asp:GridView ID="gvEmployeeDetails" runat="server" DataKeyNames="EmployeeID" HeaderStyle-HorizontalAlign="Left"
                            TabIndex="5" OnSelectedIndexChanged="gvEmployeeDetails_SelectedIndexChanged" HeaderStyle-CssClass="GridviewHeader"
                            OnRowEditing="gvEmployeeDetails_RowEditing" OnRowDataBound="gvEmployeeDetails_RowDataBound"
                            AllowSorting="true" OnSorting="gvEmployeeDetails_Sorting" OnRowCreated="gvEmployeeDetails_RowCreated"
                            OnPreRender="gvEmployeeDetails_PreRender">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%" HeaderStyle-CssClass="GridviewHeader">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:ButtonField DataTextField="EmployeeCode" HeaderText="Emp Code" CommandName="select"
                                    ItemStyle-ForeColor="Black" SortExpression="EmployeeCode" ItemStyle-Width="8%"
                                    ControlStyle-CssClass="Gridview">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="20%"
                                    SortExpression="EmployeeName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" ItemStyle-Width="0%"
                                    SortExpression="EmployeeID" ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                    ItemStyle-CssClass="Gridview">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DOJ" HeaderText="Date of Joining" ItemStyle-Width="10%"
                                    SortExpression="DOJ" ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview"
                                    HeaderStyle-CssClass="GridviewHeader">
                                    <HeaderStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mobile" HeaderText="Mobile" ItemStyle-Width="11%" SortExpression="Mobile"
                                    ControlStyle-CssClass="Gridview" ItemStyle-CssClass="Gridview">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="EMail ID" ItemStyle-Width="16%" SortExpression="OfficeEmailID"
                                    ControlStyle-CssClass="Gridview">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text='<%# Eval("OfficeEmailID") %>' NavigateUrl='<%# Eval("OfficeEmailID", "mailto:{0}") %>'
                                            ID="hlnkOfficeEmailID" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Profile" ItemStyle-Width="2%" ControlStyle-CssClass="Gridview"
                                    HeaderStyle-CssClass="GridviewHeader">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <table width="45px">
                                            <tr>
                                                <td align="right">
                                                    <asp:Image ID="imgEmployeeProfileCount" runat="server" ImageUrl="../images/pic_completeness_1x12.png"
                                                        Height="10px" />
                                                    <asp:Label ID="lblEmployeeProfileCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%" ControlStyle-CssClass="Gridview"
                                    HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="tdLabelCenter">
                                                    <asp:ImageButton ID="ibtnEditEmployee" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                        AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
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
                        <cc1:ModalPopupExtender Drag="false" ID="mpeManageEmployee" runat="server" BackgroundCssClass="modalBackground"
                            PopupControlID="pnlManageEmployee" TargetControlID="btnCreate" Enabled="true">
                        </cc1:ModalPopupExtender>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlManageEmployee" runat="server" CssClass="modalPopup" Style="display: none"
                Width="400px" DefaultButton="ibtnManageEmployeeSave">
                <center>
                    <table style="width: 100%;" border="0" cellpadding="1" cellspacing="2" class="tdLabelCenter">
                        <tr>
                            <td colspan="2" style="width: 100%;" class="tdTextResults">
                                <cc1:ValidatorCalloutExtender ID="vceCompany" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvCompany">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceEmployeeCode" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvEmployeeCode">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceEmployeeName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvEmployeeName">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceGender" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvGender">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceDOB" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvDOB">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceDOJ" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvDOJ">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceDepartment" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvDepartment">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceRole" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvRole">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceValidOfficialMailID" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="revOfficeMailID">
                                </cc1:ValidatorCalloutExtender>
                                <cc1:ValidatorCalloutExtender ID="vceOfficeMailID" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                    TargetControlID="rfvOfficeMailID">
                                </cc1:ValidatorCalloutExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdstyle tdLabelCenter">
                                <asp:Label ID="lblPopupHeading" runat="server" Text="Add Employee" CssClass="PopupPanelHeading"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdstyle tdLabelCenter">
                                <hr />
                            </td>
                        </tr>
                        <tr id="tr1" visible="false" runat="server">
                            <td id="td1" valign="top" runat="server" class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblEmployeeID" CssClass="Label" runat="server" Text="EmployeeID" Visible="False"></asp:Label>
                            </td>
                            <td id="td2" runat="server" class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtEmployeeID" Width="120px" CssClass="textarea" Text="0" Visible="False"
                                    runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trCompany" runat="server">
                            <td style="width: 40%" class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblCompMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblCompany" CssClass="LabelTitle" runat="server" Text="Company"></asp:Label>
                            </td>
                            <td style="width: 60%" class="tdstyle tdTextResults">
                                <asp:DropDownList ID="ddlCompany" AppendDataBoundItems="True" Width="175px" CssClass="Dropdownlist"
                                    runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                                    TabIndex="5">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="ddlCompany" ErrorMessage="Please Select Company" Display="none"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblEmpCodeMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblEmployeeCode" CssClass="LabelTitle" runat="server" Text="Employee Code"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtEmployeeCode" Width="75px" CssClass="textarea" MaxLength="10"
                                    runat="server" TabIndex="6"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmployeeCode" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="txtEmployeeCode" Display="none" ErrorMessage="Please Enter the Employee Code"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblNameMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblName" CssClass="LabelTitle" runat="server" Text="Initial / Name"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtEmployeeInitial" Width="20px" CssClass="textarea" MaxLength="3"
                                    runat="server" TabIndex="7"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtEmployeeName" Width="135px" CssClass="textarea" MaxLength="50"
                    runat="server" TabIndex="8"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEmployeeName" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="txtEmployeeName" Display="none" ErrorMessage="Please Enter the Employee Name"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblGenderMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblGender" CssClass="LabelTitle" runat="server" Text="Gender"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:DropDownList ID="ddlGender" AppendDataBoundItems="True" Width="100px" CssClass="Dropdownlist"
                                    runat="server" TabIndex="9">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvGender" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="ddlGender" Display="none" ErrorMessage="Please Select Gender"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblDOBMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblDOB" CssClass="LabelTitle" runat="server" Text="Date of Birth"></asp:Label>
                            </td>
                            <td valign="middle" class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtDOB" Width="75" CssClass="textarea" autocomplete="off" MaxLength="12"
                                    runat="server" TabIndex="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDOB" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="txtDOB" Display="none" ErrorMessage="Please Enter the Date of Birth"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <asp:Panel ID="pnlDOB" runat="server" CssClass="popupCalendarControl">
                                    <center>
                                        <cc1:CalendarExtender ID="ceDOB" CssClass="MyCalendar" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}"
                                            runat="server" TargetControlID="txtDOB" />
                                    </center>
                                </asp:Panel>
                                <cc1:PopupControlExtender ID="pceDOB" runat="server" TargetControlID="txtDOB" PopupControlID="pnlDOB"
                                    Position="Bottom" />
                                <cc1:MaskedEditExtender ID="meePassportDateOfBirth" runat="server" TargetControlID="txtDOB"
                                    Mask="99/99/9999" MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblDOJMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblDOJ" CssClass="LabelTitle" runat="server" Text="Date of Joining"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtDOJ" Width="75px" CssClass="textarea" autocomplete="off" MaxLength="12"
                                    runat="server" TabIndex="11"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvDOJ" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="txtDOJ" Display="none" ErrorMessage="Please Enter the Date of Joining"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                <asp:Panel ID="pnlDOJ" runat="server" CssClass="popupCalendarControl">
                                    <center>
                                        <cc1:CalendarExtender ID="ceDOJ" CssClass="MyCalendar" OnClientDateSelectionChanged="function(sender, e) {sender.hide();}"
                                            runat="server" TargetControlID="txtDOJ" />
                                    </center>
                                </asp:Panel>
                                <cc1:PopupControlExtender ID="pceDOJ" runat="server" TargetControlID="txtDOJ" PopupControlID="pnlDOJ"
                                    Position="Bottom" />
                                <cc1:MaskedEditExtender ID="meeDOJ" runat="server" TargetControlID="txtDOJ" Mask="99/99/9999"
                                    MessageValidatorTip="true" CultureName="en-US" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" MaskType="Date" DisplayMoney="Left" AcceptNegative="Left"
                                    ErrorTooltipEnabled="True" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblDepartmentMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblDepartment" CssClass="LabelTitle" runat="server" Text="Department"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:DropDownList ID="ddlDepartment" AppendDataBoundItems="True" Width="175px" CssClass="Dropdownlist"
                                    runat="server" TabIndex="12">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="ddlDepartment" Display="none" ErrorMessage="Please Select Department"
                                    SetFocusOnError="True">                                          
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trRole" runat="server" class="tdstyle">
                            <td class="tdLabelTitle">
                                <asp:Label ID="lblRoleMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblRole" CssClass="LabelTitle" runat="server" Text="Role"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:DropDownList ID="ddlRole" AppendDataBoundItems="True" Width="175px" CssClass="Dropdownlist"
                                    runat="server" TabIndex="13">
                                    <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvRole" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="ddlRole" Display="none" ErrorMessage="Please Select Role" SetFocusOnError="True">                                          
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstyle tdLabelTitle">
                                <asp:Label ID="lblOfficeMailIDMandatory" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
                                <asp:Label ID="lblOfficeMailID" CssClass="LabelTitle" runat="server" Text="Official EMail ID"></asp:Label>
                            </td>
                            <td class="tdstyle tdTextResults">
                                <asp:TextBox ID="txtOfficeMailID" Width="170px" CssClass="textarea" MaxLength="75"
                                    runat="server" TabIndex="14"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="revOfficeMailID" runat="server" ControlToValidate="txtOfficeMailID"
                                    ErrorMessage="Invalid Official Mail ID" Display="none" Text="*" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"
                                    ValidationGroup="ValidateManageEmployee" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="rfvOfficeMailID" runat="server" ValidationGroup="ValidateManageEmployee"
                                    ControlToValidate="txtOfficeMailID" Display="none" Text="*" ErrorMessage="Please Enter the Official Mail ID"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdstyle tdLabelTitle">
                                <font color="red" size="2">
                                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label></font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="tdstyle">
                                <center>
                                    <br />
                                    <asp:Button ID="ibtnManageEmployeeSave" runat="server" ValidationGroup="ValidateManageEmployee" Text="Save" CssClass="btn"
                                        CausesValidation="true" TabIndex="15" OnClick="ibtnManageEmployeeSave_Click" />&nbsp;
                  <asp:Button ID="ibtnManageEmployeeCancel" runat="server" CausesValidation="False" Text="Cancel" CssClass="btn"
                      TabIndex="16" OnClick="ibtnManageEmployeeCancel_Click" />&nbsp;
                                </center>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            Initialize();
        }); // ready fn

        function Initialize() {
            $('#<%=gvEmployeeDetails.ClientID%>').tablePagination();
        //Quick search

        $('#<%=txtSearchID.ClientID%>').keyup(function (event) {
            var searchKey = $(this).val().toLowerCase();
            if (searchKey.length > 0) {
                $("#tablePagination").hide();
                $("#<%=gvEmployeeDetails.ClientID%> tr.row").each(function () {
              var cellText = $(this).text().toLowerCase();
              if (cellText.indexOf(searchKey) >= 0) {
                  $(this).show();
                  $("#NoRecords").remove();
              } //if
              else {
                  $(this).hide();
              } // else
          }); //child fn
          if ($("#<%=gvEmployeeDetails.ClientID%> tbody:first tr:visible").length == 0) {
                $("#<%=gvEmployeeDetails.ClientID%> tbody:first").append('<tr id="NoRecords" rowspan="3" valign="middle" class="tdLabelCenter"><td colspan="10" class="tdNoRecords"><h3> No matching records found </h3></td></tr>');
          }
      }
      else if (searchKey.length == 0) {
          $("#NoRecords").remove();
          $('#<%=gvEmployeeDetails.ClientID%>').tablePagination();
          $("#tablePagination").show();
      }
      });  // event fn

        //To Clear the seach text
    $("#tablePagination_rowsPerPage").change(function () {
        $('#<%=txtSearchID.ClientID%>').val('');
      });
  }
  Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Initialize);


  function repagination() {
      var endRequest = function () {
          Sys.WebForms.PageRequestManager.getInstance().remove_endRequest(endRequest);
          var txt = $('#<%=txtSearchID.ClientID%>');
        if ($.trim(txt.val()).length > 0) {
            txt.keyup();
        }
      }
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequest);
}

function btnCreateClick() {
    $('#<%=txtSearchID.ClientID%>').val('');
        var txt = $('#<%=txtSearchID.ClientID%>');
        txt.keyup();
    }
    </script>
</asp:Content>
