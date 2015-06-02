<%@ Page Title="Master Table Entry Form" Language="C#" AutoEventWireup="true" CodeBehind="AddEditMasters.aspx.cs"
    Inherits="ACE.PurchaseOrder.AddEditMasters" MasterPageFile="~/Order.Master" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <table border="0" cellpadding="1" cellspacing="2" width="100%">
        <tr>
            <td style="height: 25px;" colspan="3">&nbsp;
        <cc1:ValidatorCalloutExtender ID="vceddlSearchTable" runat="Server" HighlightCssClass="validatorCalloutHighlight"
            TargetControlID="rfvddlSearchTable">
        </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="vceMasterData" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="rfvMasterData">
                </cc1:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr id="trSearchRow" runat="server">
            <td class="Label">
                <b>&nbsp;&nbsp;<asp:Label ID="lblMasterTable" runat="server" Text="Master Table"></asp:Label>
                </b>&nbsp;
        <asp:DropDownList ID="ddlSearchTable" runat="server" CssClass="Dropdownlist" AutoPostBack="true"
            OnSelectedIndexChanged="ddlSearchTable_SelectedIndexChanged">
            <asp:ListItem Value="">-- Select One --</asp:ListItem>
            <asp:ListItem Value="Designation">Designation</asp:ListItem>
            <asp:ListItem Value="Department">Department</asp:ListItem>
        </asp:DropDownList>
                &nbsp;
        <asp:RequiredFieldValidator ID="rfvddlSearchTable" runat="server" ValidationGroup="SaveReqFields"
            ControlToValidate="ddlSearchTable" Display="none" ErrorMessage="Please Select the Master Table"
            SetFocusOnError="True">
        </asp:RequiredFieldValidator>
            </td>
            <td>&nbsp;
      
            </td>
            <td style="text-align: right;">
                <asp:ImageButton ID="btnCreate" runat="server" ImageUrl="../Images/btnCreateNew.gif"
                    ImageAlign="Top" TabIndex="3" ValidationGroup="SaveReqFields" OnClick="btnCreate_Click" />
                <cc1:ModalPopupExtender Drag="false" ID="mpeMasterTable" runat="server" BackgroundCssClass="modalBackground"
                    PopupControlID="pnlMasterTable" TargetControlID="btnCreate" DynamicServicePath=""
                    Enabled="true">
                </cc1:ModalPopupExtender>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table border="0" cellpadding="1" cellspacing="2" width="100%">
                    <tr>
                        <td style="height: 25px;">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gvMasterTable" runat="server" Width="99%">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TableID" />
                                    <asp:BoundField DataField="Description" />
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%" align="left">
                                                        <asp:ImageButton ID="ibtnEditMasterData" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                            AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                    </td>
                                                    <td style="width: 50%" align="right">
                                                        <asp:ImageButton ID="ibtnDeleteMasterData" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
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
                        <td colspan="2">
                            <asp:Panel ID="pnlMasterTable" runat="server" CssClass="modalPopup" Width="360px"
                                Style="display: none" DefaultButton="ibtnMasterDataSave">
                                <table border="0" cellpadding="1" cellspacing="2" width="360px">
                                    <tr id="tr1">
                                        <td colspan="2">
                                            <asp:HiddenField ID="hdfMasterDataID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <asp:Label ID="lblPopupHeading" runat="server" Text="Add Blood group" CssClass="PopupPanelHeading"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center;">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="middle" style="width: 35%">
                                            <asp:Label ID="lblMasterDataMandatory" CssClass="LabelMandatory" Text="*" runat="server"></asp:Label>
                                            <asp:Label ID="lblMasterDataDescription" CssClass="LabelTitle" runat="server" Text="Description"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle" style="width: 65%">
                                            <asp:TextBox ID="txtMasterDataDescription" runat="server" MaxLength="100" Width="80px"
                                                CssClass="textarea" />
                                            <asp:RequiredFieldValidator ID="rfvMasterData" runat="server" ValidationGroup="ValidateMasterData"
                                                ControlToValidate="txtMasterDataDescription" Display="none" ErrorMessage="Please Enter the Description"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <center>
                                                <br />
                                                <asp:ImageButton ID="ibtnMasterDataSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                                    CausesValidation="true" ValidationGroup="ValidateMasterData" TabIndex="3" />&nbsp;
                        <asp:ImageButton ID="ibtnMasterDataCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                            CausesValidation="false" ImageAlign="Middle" TabIndex="4" />&nbsp;
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
</asp:Content>
