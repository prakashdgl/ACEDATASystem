<%@ Page Title="Change Password" Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.ChangePassword" MasterPageFile="~/Order.Master" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <!--Content Area Start-->
  <div class="pageTitleDiv">
    <asp:Label ID="Label4" runat="server" CssClass="pageTitleLabel" Text="Change Password"></asp:Label><hr />
  </div>
  <table border="0" cellpadding="0" cellspacing="0" width="99%">
    <tr>
      <td id="Td1" colspan="3" runat="server">
        <cc1:ValidatorCalloutExtender ID="vceCompany" runat="Server" HighlightCssClass="validatorCalloutHighlight"
          TargetControlID="rfvOldPwd">
        </cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
          TargetControlID="rfvPwd">
        </cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
          TargetControlID="rfvconfpwd">
        </cc1:ValidatorCalloutExtender>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="Server" HighlightCssClass="validatorCalloutHighlight"
          TargetControlID="cvpwd">
        </cc1:ValidatorCalloutExtender>
      </td>
    </tr>
    <tr>
      <td colspan="3" style="height: 25px;">
        &nbsp;
      </td>
    </tr>
    <tr>
      <td id="Td2" style="width: 49%;" runat="server" class="LabelTitle tdLabelTitle">
        <asp:Label ID="Label1" runat="server" Text="* " CssClass="LabelMandatory"></asp:Label>Enter
        Current Password
        <br />
      </td>
      <td>
        &nbsp;
      </td>
      <td class="tdTextResults" style="width: 50%;">
        <asp:TextBox ID="txtOldPwd" runat="server" CssClass="textarea" MaxLength="10" TextMode="Password"
          Width="100px"></asp:TextBox>&nbsp;
        <asp:RequiredFieldValidator ID="rfvOldPwd" runat="server" Text="" SetFocusOnError="true"
          ErrorMessage="Please Enter Old Password" Display="none" ControlToValidate="txtOldPwd"
          ValidationGroup="password"></asp:RequiredFieldValidator>
        <br />
      </td>
    </tr>
    <tr>
      <td id="Td3" runat="server" class="LabelTitle tdLabelTitle">
        <br />
        <asp:Label ID="LabelMandatory1" runat="server" Text="* " CssClass="LabelMandatory"></asp:Label>
        Enter New Password<br />
        &nbsp;
      </td>
      <td>
        &nbsp;
      </td>
      <td class="tdTextResults">
        <asp:TextBox ID="txtpwd" runat="server" CssClass="textarea" TextMode="Password" Width="100px"
          MaxLength="10"></asp:TextBox>&nbsp;
        <asp:RequiredFieldValidator ID="rfvPwd" runat="server" Text="" SetFocusOnError="true"
          ErrorMessage="Please Enter New Password" Display="none" ControlToValidate="txtpwd"
          ValidationGroup="password"></asp:RequiredFieldValidator>
        <br />
      </td>
    </tr>
    <tr>
      <td id="Td4" runat="server" class="LabelTitle tdLabelTitle">
        <asp:Label ID="LabelMandatory2" runat="server" Text="* " CssClass="LabelMandatory"></asp:Label>
        Confirm New Password
        <br />
      </td>
      <td>
        &nbsp;
      </td>
      <td id="Td5" runat="server" class="Label tdTextResults">
        <asp:TextBox ID="txtconfpwd" runat="server" CssClass="textarea" TextMode="Password"
          Width="100px" MaxLength="10"></asp:TextBox>&nbsp;
        <asp:RequiredFieldValidator ID="rfvconfpwd" runat="server" Text="" SetFocusOnError="true"
          ErrorMessage="Please Enter Confirm Password" Display="none" ControlToValidate="txtconfpwd"
          ValidationGroup="password"></asp:RequiredFieldValidator>
        <br />
      </td>
    </tr>
    <tr>
      <td class="tdLabelCenter" colspan="3">
        <asp:CompareValidator ID="cvpwd" runat="server" ControlToCompare="txtpwd" Display="none"
          ControlToValidate="txtconfpwd" ErrorMessage="Password Entered is not Matching"
          ValidationGroup="password"></asp:CompareValidator>
      </td>
    </tr>
    <tr>
      <td class="tdLabelCenter" colspan="3">
        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
      </td>
    </tr>
    <tr>
      <td class="tdLabelCenter" colspan="3">
        <br />
        <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/Images/btnSubmit.gif"
          OnClick="btnSubmit_Click" ValidationGroup="password" />
        &nbsp;
        <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="false" ImageUrl="~/Images/btnCancel.gif"
          OnClick="btnCancel_Click" />
      </td>
    </tr>
  </table>
  <!--Content Area End-->
</asp:Content>
