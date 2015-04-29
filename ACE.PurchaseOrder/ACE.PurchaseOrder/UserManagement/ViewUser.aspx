<%@ Page Title="User Information" Language="C#" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.ViewUser" MasterPageFile="~/Order.Master" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <div class="pageTitleDiv">
    <asp:Label runat="server" CssClass="pageTitleLabel" Text="User Details"></asp:Label><hr />
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
        <asp:TextBox ID="txtSearchID" runat="server" class="search"></asp:TextBox>
      </td>
    </tr>
  </table>
  <table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr id="trGridRow" runat="server">
      <td colspan="2" style="width: 100%">
        <asp:GridView ID="gvUserDetails" runat="server" DataKeyNames="UserID,EmployeeID"
          PageSize="10" CellPadding="5" HeaderStyle-HorizontalAlign="Left" OnSelectedIndexChanged="gvUserDetails_SelectedIndexChanged"
          OnRowEditing="gvUserDetails_RowEditing" OnRowDataBound="gvUserDetails_RowDataBound"
          OnRowCommand="gvUserDetails_RowCommand" OnRowCreated="gvUserDetails_RowCreated"
          OnPreRender="gvUserDetails_PreRender" EnableModelValidation="True">
          <Columns>
            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="1%">
              <ItemTemplate>
                <asp:Label ID="lblSerial" runat="server"></asp:Label>
              </ItemTemplate>
              <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
              <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:TemplateField>
            <asp:ButtonField DataTextField="UserName" HeaderText="User ID" CommandName="select"
              SortExpression="UserName" ItemStyle-Width="5%">
              <ItemStyle HorizontalAlign="Right" />
              <ControlStyle CssClass="Gridview"></ControlStyle>
              <HeaderStyle HorizontalAlign="Left" />
            </asp:ButtonField>
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password">
              <ControlStyle CssClass="Gridview"></ControlStyle>
              <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
              <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" SortExpression="EmployeeID">
            </asp:BoundField>
            <asp:BoundField DataField="EmployeeCode" HeaderText="Emp Code" ItemStyle-Width="6%"
              SortExpression="EmployeeCode">
              <ItemStyle HorizontalAlign="Right" />
              <ControlStyle CssClass="Gridview"></ControlStyle>
              <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-Width="28%"
              SortExpression="EmployeeName">
              <ControlStyle CssClass="Gridview"></ControlStyle>
              <HeaderStyle HorizontalAlign="Left" />
              <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
            </asp:BoundField>
            <asp:CheckBoxField DataField="IsValid" HeaderText="Activate / Deactivate" ItemStyle-Width="7%">
              <ItemStyle HorizontalAlign="center" />
              <HeaderStyle HorizontalAlign="center" />
              <ControlStyle CssClass="Gridview"></ControlStyle>
            </asp:CheckBoxField>
            <asp:ButtonField Text="Reset" CommandName="ResetPassword" HeaderText="Reset Password"
              ItemStyle-Width="7%">
              <ItemStyle ForeColor="Red" HorizontalAlign="center" />
              <HeaderStyle HorizontalAlign="center" />
              <ControlStyle CssClass="Gridview"></ControlStyle>
            </asp:ButtonField>
            <asp:BoundField DataField="JobStatusDescription" HeaderText="Status" ItemStyle-Width="7%"
              SortExpression="JobStatusDescription">
              <ItemStyle HorizontalAlign="Left" />
              <HeaderStyle HorizontalAlign="Left" />
              <ControlStyle CssClass="Gridview"></ControlStyle>
            </asp:BoundField>
            <asp:BoundField DataField="IsValid" HeaderText="IsValid" SortExpression="IsValid">
            </asp:BoundField>
            <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%" ControlStyle-CssClass="Gridview"
              ItemStyle-CssClass="Gridview gridItemStyle">
              <ItemTemplate>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                  <tr>
                    <td class="tdLabelCenter">
                      <asp:ImageButton ID="ibtnEditUser" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                        AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                    </td>
                  </tr>
                </table>
              </ItemTemplate>
              <ControlStyle CssClass="Gridview"></ControlStyle>
              <HeaderStyle HorizontalAlign="center" />
            </asp:TemplateField>
          </Columns>
        </asp:GridView>
      </td>
    </tr>
    <tr id="trUserDetailView" runat="server" valign="top">
      <td colspan="2">
        <table border="0" cellpadding="3" cellspacing="1" width="100%">
          <tr>
            <td class="LabelTitle tdLabelTitle">
              <asp:Label ID="lblEmployeeCode" CssClass="LabelTitle" runat="server" Text="Employee Code"></asp:Label>
            </td>
            <td class="tdTextResults">
              <asp:Label ID="lblEmployeeCodeValue" CssClass="TextResults" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="LabelTitle tdLabelTitle">
              <asp:Label ID="lblEmployeeName" CssClass="LabelTitle" runat="server" Text="Employee Name"></asp:Label>
            </td>
            <td class="tdTextResults">
              <asp:Label ID="lblEmployeeNameValue" CssClass="TextResults" runat="server"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="LabelTitle tdLabelTitle" style="width: 50%">
              <asp:Label ID="lblUserName" runat="server" Text="User Name" CssClass="LabelTitle"></asp:Label>
            </td>
            <td style="width: 50%" class="tdTextResults">
              <asp:Label ID="lblUserNameValue" runat="server" Text="User Name Description" CssClass="TextResults"></asp:Label>
            </td>
          </tr>
          <tr id="Tr1" runat="server" visible="false">
            <td class="LabelTitle tdLabelTitle">
              <asp:Label ID="lblPassword" runat="server" Text="Password" CssClass="LabelTitle"></asp:Label>
            </td>
            <td class="tdTextResults">
              <asp:Label ID="lblPasswordValue" runat="server" Text="Password Value" CssClass="TextResults"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="Label tdLabelCenter">
              <asp:Label ID="lblIsValid" CssClass="LabelTitle" runat="server" Text="Activate / Deactivate"></asp:Label>
            </td>
            <td class="tdTextResults">
              <asp:CheckBox ID="chkIsValidValue" runat="server" Enabled="true"></asp:CheckBox>
            </td>
          </tr>
          <tr>
            <td colspan="2">
              &nbsp;
            </td>
          </tr>
        </table>
      </td>
    </tr>
    <tr id="trUserDetailTabs" runat="server">
      <td colspan="2" class="tdTextResults">
        <cc1:TabContainer ID="tcntAllUserTabs" runat="server" ActiveTabIndex="0" ScrollBars="Vertical"
          Height="150px" Width="100%">
          <cc1:TabPanel ID="tpnlCompanies" runat="server" HeaderText="Company" Width="100%">
            <ContentTemplate>
              <table border="0" style="width: 97%" class="tdTextResults">
                <tr valign="top">
                  <td valign="top">
                    <asp:GridView ID="gvUserCompanies" runat="server" DataKeyNames="CompanyID,RoleID"
                      AllowSorting="True" HeaderStyle-HorizontalAlign="Left" OnSorting="gvUserCompanies_Sorting"
                      OnRowDataBound="gvUserCompanies_RowDataBound">
                      <Columns>
                        <asp:TemplateField HeaderText="S.No">
                          <ItemTemplate>
                            <asp:Label ID="lblSerial" runat="server"></asp:Label>
                          </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" />
                          <ItemStyle Width="5%" HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CompanyID" HeaderText="Company ID" SortExpression="CompanyID"
                          ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" />
                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName"
                          ItemStyle-Width="36%" ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left"
                          ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="RoleID" HeaderText="Role ID" SortExpression="RoleID" ControlStyle-CssClass="Gridview"
                          HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview" />
                        <asp:BoundField DataField="RoleName" HeaderText="Role" SortExpression="RoleName"
                          ControlStyle-CssClass="Gridview" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left"
                          ItemStyle-Width="58%" />
                        <asp:TemplateField HeaderText="Default" ItemStyle-Width="2%" SortExpression="IsDefault"
                          ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader" ItemStyle-CssClass="Gridview">
                          <ItemTemplate>
                            <asp:CheckBox ID="IsDefault" Checked='<%# Eval("IsDefault") %>' runat="server" Enabled="false" />
                          </ItemTemplate>
                        </asp:TemplateField>
                      </Columns>
                    </asp:GridView>
                  </td>
                </tr>
              </table>
            </ContentTemplate>
            <HeaderTemplate>
              Companies
            </HeaderTemplate>
          </cc1:TabPanel>
        </cc1:TabContainer>
      </td>
    </tr>
    <tr id="trUpdateCancelButtonRow" runat="server">
      <td class="tdHomeLabel" colspan="2" valign="top" style="height: 15px;">
        <br />
        <%--<asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
          ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" />--%>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" TabIndex="103"
          OnClick="btnCancel_Click" />
      </td>
    </tr>
  </table>
  <script type="text/javascript">
    $('#<%=gvUserDetails.ClientID%>').tablePagination();

    $(document).ready(function () {
      //Quick search 
      $('#<%=txtSearchID.ClientID%>').keyup(function (event) {

        var searchKey = $(this).val().toLowerCase();
        if (searchKey.length > 0) {
          $("#<%=gvUserDetails.ClientID%> tr.row").each(function () {
            var cellText = $(this).text().toLowerCase();
            if (cellText.indexOf(searchKey) >= 0) {
              $("#tablePagination").hide();
              $(this).show();
              $("#NoRecords").remove();
            } //if
            else {
              $(this).hide();
            } // else
          }); //child fn
          if ($("#<%=gvUserDetails.ClientID%> tbody:first tr:visible").length == 0) {
            $("#<%=gvUserDetails.ClientID%> tbody:first").append('<tr id="NoRecords" rowspan="3" valign="middle" class="tdLabelCenter"><td colspan="10" class="tdNoRecords"><h3> No matching records found </h3></td></tr>');
          }
        }
        else if (searchKey.length == 0) {
          $("#NoRecords").remove();
          $('#<%=gvUserDetails.ClientID%>').tablePagination();
          $("#tablePagination").show();
        }
      }); // event fn
      //To Clear the seach text
      $("#tablePagination_rowsPerPage").change(function () {
        $('#<%=txtSearchID.ClientID%>').val('');
      });
    });  // ready fn

  </script>
</asp:Content>
