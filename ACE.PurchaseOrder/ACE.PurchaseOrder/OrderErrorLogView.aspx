<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderErrorLogView.aspx.cs"
  Inherits="ACE.PurchaseOrder.OrderErrorLogView" MasterPageFile="~/Order.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <div class="pageTitleDiv">
    <asp:Label ID="lblEmpAdditionalInfo" runat="server" CssClass="pageTitleLabel" Text="Error Log"></asp:Label><hr />
  </div>
  <table id="tblViewErrorLog" runat="server" cellpadding="0" cellspacing="0" width="100%"
    class="tableHeaderGridview">
    <tr id="trSearchAndAddRow" runat="server">
      <td style="width: 32%; padding-right: 15px" class="tdLabelTitle">
        <asp:Label ID="lblSearch" CssClass="LabelTitle" runat="server" Text="Search"></asp:Label>
        <asp:TextBox ID="txtSearchID" runat="server" autocomplete="off" class="search"></asp:TextBox>
      </td>
    </tr>
  </table>
  <table id="Table2" runat="server" cellpadding="0" cellspacing="0" width="100%">
    <tr id="trGridRow" runat="server">
      <td colspan="2">
        <%--  <div style="overflow:scroll;">--%>
        <asp:GridView ID="gvErrorLog" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvErrorLog_RowDataBound"
          OnRowCreated="gvErrorLog_RowCreated" Width="100%">
          <Columns>
            <asp:TemplateField HeaderText="S.No" ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              HeaderStyle-Width="1%">
              <ItemTemplate>
                <asp:Label ID="lblSerial" runat="server"></asp:Label>
              </ItemTemplate>
              <ItemStyle HorizontalAlign="Right" />
              <HeaderStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              Visible="false" ItemStyle-Width="1%" HeaderStyle-Width="1%" ItemStyle-CssClass="Gridview"
              DataField="ErrorMessageLogID" HeaderText="ErrorMessageLogID">
              <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              HeaderStyle-Width="1%" ItemStyle-CssClass="Gridview" DataField="PageName" HeaderText="Page Name"
              ItemStyle-Width="1%">
              <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              HeaderStyle-Width="1%" ItemStyle-Width="1%" ItemStyle-CssClass="Gridview" DataField="ClassName"
              HeaderText="Class Name">
              <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              ItemStyle-CssClass="Gridview" DataField="EventName" HeaderText="Event Name" ItemStyle-Width="1%"
              HeaderStyle-Width="1%">
              <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              ItemStyle-CssClass="Gridview" DataField="ErrorMessage" HeaderText="Error Message"
              HeaderStyle-Width="5%" ItemStyle-Width="25%">
              <ItemStyle HorizontalAlign="Left" Width="28%" />
            </asp:BoundField>
            <asp:BoundField ControlStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
              HeaderStyle-Width="1%" ItemStyle-CssClass="Gridview" DataField="AuditDate" HeaderText="Audit Date"
              ItemStyle-Width="1%">
              <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
          </Columns>
        </asp:GridView>
        <%--</div>--%>
      </td>
    </tr>
  </table>
  <script type="text/javascript">

    $(document).ready(function () {
      Initialize();
    }); // ready fn

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Initialize);

    function Initialize() {
      $('#<%=gvErrorLog.ClientID%>').tablePagination({
      firstArrow : (new Image()).src="Images/first.png",  
			prevArrow: (new Image()).src = "Images/prev.png",
			lastArrow: (new Image()).src = "Images/last.png",
			nextArrow: (new Image()).src = "Images/next.png"
      });
      //Quick search

      $('#<%=txtSearchID.ClientID%>').keyup(function (event) {

        var searchKey = $(this).val().toLowerCase();
        if (searchKey.length > 0) {
          $("#<%=gvErrorLog.ClientID%> tr.row").each(function () {
            var cellText = $(this).text().toLowerCase();
            if (cellText.indexOf(searchKey) >= 0) {
              $(this).show();
              $("#NoRecords").remove();
            } //if
            else {
              $(this).hide();
            } // else
          }); //child fn
          if ($("#<%=gvErrorLog.ClientID%> tbody:first tr:visible").length == 1) {
            $("#<%=gvErrorLog.ClientID%> tbody:first").append('<tr id="NoRecords" rowspan="3" valign="middle" class="tdLabelCenter"><td colspan="10" class="tdNoRecords"><h3> No matching records found </h3></td></tr>');
          }
        }
        else if (searchKey.length == 0) {
          $("#NoRecords").remove();
          $('#<%=gvErrorLog.ClientID%>').tablePagination();
        }
        //To Clear the seach text
        $("#tablePagination_rowsPerPage").change(function () {
          $('#<%=txtSearchID.ClientID%>').val('');
        });
      });  // event fn
    }
  </script>
</asp:Content>
