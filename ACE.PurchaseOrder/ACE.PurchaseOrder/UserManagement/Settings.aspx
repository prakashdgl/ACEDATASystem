<%@ Page Title="General Settings" Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.Settings" MasterPageFile="~/Order.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <div class="pageTitleDiv">
    <asp:Label ID="lblPageTitle" runat="server" CssClass="pageTitleLabel" Text="General Settings"></asp:Label>
  </div>
  <table border="0" cellpadding="2" cellspacing="2" style="width: 99%">
    <tr>
      <td colspan="2" class="tdLabelCenter">
        <asp:HiddenField ID="hfUserSettingID" runat="server" Value="0" />
        <asp:HiddenField ID="hfUserID" runat="server" Value="0" />
        <cc1:ValidatorCalloutExtender ID="vceDateFormat" runat="Server" HighlightCssClass="validatorCalloutHighlight"
          TargetControlID="rfvDateFormat">
        </cc1:ValidatorCalloutExtender>
      </td>
    </tr>
    <tr>
      <td class="tdLabelTitle" style="width: 40%">
        <asp:Label ID="LabelMandatory1" runat="server" Text="*" CssClass="LabelMandatory"></asp:Label>
        <asp:Label ID="lblDateFormat" runat="server" Text="Date Format" CssClass="LabelTitle"></asp:Label>
      </td>
      <td class="tdTextResults" style="width: 60%">
        <asp:DropDownList ID="ddlDateFormat" AppendDataBoundItems="true" Width="211px" CssClass="Dropdownlist"
          runat="server">
          <asp:ListItem Value="">-- Select One --</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvDateFormat" runat="server" ValidationGroup="ValidateSettings"
          ControlToValidate="ddlDateFormat" ErrorMessage="Please Select Date Format" Display="none"
          SetFocusOnError="True">
        </asp:RequiredFieldValidator>
      </td>
    </tr>
    <tr>
      <td colspan="2" class="tdLabelCenter">
        <br />
        <asp:Button ID="ibtnSettingsSave" runat="server" Text="Save" CssClass="btn"
          CausesValidation="true" ValidationGroup="ValidateSettings" TabIndex="25" OnClick="ibtnSettingsSave_Click" />&nbsp;
        <asp:Button ID="ibtnSettingsCancel" runat="server" Text="Cancel" CssClass="btn"
          CausesValidation="false"  TabIndex="26" OnClick="ibtnSettingsCancel_Click" />
      </td>
    </tr>
  </table>
</asp:Content>
