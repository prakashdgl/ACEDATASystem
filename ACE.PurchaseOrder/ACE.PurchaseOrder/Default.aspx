<%@ Page Title="Welcome to ACE Intranet" Language="C#" MasterPageFile="~/Order.Master"
  AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ACE.PurchaseOrder.Default"
  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
 
  <div class="otherInfoBox">
   <%-- <ul>
      <li><a href="attendance/ViewAttendance.aspx">
        <img alt="attendance" src="images/NewImages/title-attendance.png" width="183" height="58" /></a>
      </li>
      <li>
        <img src="images/NewImages/title-birthday.png" alt="birthday" width="166" height="64" />
        <span>
          <asp:DataGrid runat="server" ID="gvBirthDay" AllowPaging="false" ShowHeader="false"
            ForeColor="Black" ShowFooter="false" CellPadding="0" CellSpacing="0" GridLines="None">
          </asp:DataGrid></span>
        <img src="images/NewImages/title-wedding.png" alt="wedding" width="207" height="44"
          class="wedding" />
        <span>
          <asp:DataGrid runat="server" ID="gvAnniversaries" AllowPaging="false" ShowHeader="false"
            ForeColor="Black" ShowFooter="false" CellPadding="0" CellSpacing="0" GridLines="None">
          </asp:DataGrid>
        </span></li>     
    </ul>--%>
  </div>
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
    <td>&nbsp;</td>
    </tr>
  </table>
  
  <asp:UpdatePanel runat="server" ID="upWishes">
    <ContentTemplate>
      <asp:LinkButton ID="btnHiddenOk" runat="server"></asp:LinkButton>
      <asp:Panel ID="pnlMessage" BackColor="white" Height="200px" Width="450px" BackImageUrl="~/images/FireworksAni.gif"
        runat="server" HorizontalAlign="Center" Style="display: none" BorderColor="#99ccff"
        BorderWidth="1px">
        <table style="width: 100%" cellspacing="0" cellpadding="2">
          <tr>
            <td>
              &nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              &nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              &nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              &nbsp;
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                ForeColor="Black"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              <asp:Label ID="lblName" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                ForeColor="Black"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              <asp:Label ID="lblName1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                ForeColor="Black"></asp:Label>
            </td>
          </tr>
          <tr>
            <td class="tdLabelCenter">
              <asp:Label ID="lblName2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="12pt"
                ForeColor="Black"></asp:Label>
            </td>
          </tr>
          <tr>
            <td>
              &nbsp;
              <br />
            </td>
          </tr>
          <tr>
            <td>
              <asp:ImageButton ID="btnMessageClose" runat="server" ImageUrl="~/images/continue.png" />
            </td>
          </tr>
        </table>
      </asp:Panel>
      <asp:LinkButton ID="btnHiddenMessage" runat="server"></asp:LinkButton>
      <cc1:ModalPopupExtender Drag="false" ID="mpeWishes" runat="server" TargetControlID="btnHiddenMessage"
        CancelControlID="btnMessageClose" PopupControlID="pnlMessage" BackgroundCssClass="modalBackground"
        DropShadow="false">
      </cc1:ModalPopupExtender>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
