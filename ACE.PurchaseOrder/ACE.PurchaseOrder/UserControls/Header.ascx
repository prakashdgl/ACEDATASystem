<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="ACE.PurchaseOrder.Header" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:LinkButton ID="lbLogout" runat="server" CssClass="linkbutton" CausesValidation="false"
    OnClick="lbLogout_Click">Logout</asp:LinkButton>

<h1>
    <asp:Label ID="lblEmpName" runat="server" CssClass="dashboard_title" Font-Bold="False" />

</h1>
