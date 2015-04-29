<%@ Page Language="C#" AutoEventWireup="true" Title="Manage Agent" CodeBehind="ManageAgent.aspx.cs"
    EnableEventValidation="false" Inherits="ACE.PurchaseOrder.Masters.ManageAgent" MasterPageFile="~/Order.Master" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="950" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader">
                                Agent Master Page
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table border="0" style="text-align: left; width: 97%">
                                    <tr>
                                        <td align="right">
                                            <asp:ImageButton ID="lnkAddAgent" runat="server" ImageUrl="~/Images/btnCreateNew.gif" OnClick="lnkAddAgent_Click" />
                                            <cc1:ModalPopupExtender ID="mpeEdit" runat="server" BackgroundCssClass="modalBackground"
                                                DropShadow="false" PopupControlID="pnlStatusEdit" PopupDragHandleControlID="pnlStatusEdit"
                                                TargetControlID="lnkAddAgent">
                                            </cc1:ModalPopupExtender>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td valign="top" align="left">
                                            <asp:Panel ID="pnlGridView" runat="Server" Width="100%" Height="400px" ScrollBars="vertical">
                                                <asp:GridView ID="gvAgent" runat="server" DataKeyNames="AgentID" AllowSorting="true"
                                                    OnRowDataBound="gvAgent_RowDataBound" OnRowDeleting="gvAgent_RowDeleting"
                                                    OnRowEditing="gvAgent_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AgentID" HeaderText="AgentID" />
                                                        <asp:BoundField DataField="AgentName" ReadOnly="true" ItemStyle-Width="94%" HeaderText="Agent">
                                                            <ItemStyle />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                                            <ItemTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td style="width: 50%" align="left">
                                                                            <asp:ImageButton ID="ibtnEditAgent" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                                        </td>
                                                                        <td style="width: 50%" align="right">
                                                                            <asp:ImageButton ID="ibtnDeleteAgent" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trUpdateCancelButtonRow" runat="server">
                            <td align="center" colspan="2" valign="top" style="height: 15px;">
                                <br />
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                    ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="pnlStatusEdit" runat="server" CssClass="modalPopup" Style="display: none"
                    Width="300px">
                    <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="3">
                        <tr>
                            <td colspan="2" style="width: 100%; text-align: left">
                                <asp:RequiredFieldValidator ID="rfvAgent" runat="server" ValidationGroup="ValidateAgent"
                                    ControlToValidate="txtAgent" ErrorMessage="Please Enter the Agent" SetFocusOnError="True">
                                    <li style="list-style-type: circle">
                                        <asp:Label ID="lblAgentValidation" runat="server" ForeColor="red" Text="Please Enter the Agent"></asp:Label></li>
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="Tr2" visible="false" runat="server">
                            <td style="width: 40%" align="right">
                                <asp:Label ID="lblAgentID" CssClass="Label" runat="server" Text="AgentID" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtAgentID" Width="120px" CssClass="textarea" Text="0" Visible="false"
                                    runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 40%" align="right">
                                <asp:Label ID="lblAgent" CssClass="Label" runat="server" Text="Agent"></asp:Label>
                            </td>
                            <td style="width: 60%" align="left">
                                <asp:TextBox ID="txtAgent" Width="240px" MaxLength="200" CssClass="textarea" runat="server">
                                </asp:TextBox>
                               <%-- <cc1:FilteredTextBoxExtender ID="ftbeAgent" runat="server" TargetControlID="txtAgent"
                                    FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                    Enabled="True" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <center>
                                    <br />
                                    <asp:ImageButton ID="btnAgentAdd" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                        CausesValidation="true" ValidationGroup="ValidateAgent" TabIndex="102" OnClick="btnAgentAdd_Click" />&nbsp;
                                    <asp:ImageButton ID="btnAgentCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        CausesValidation="false" ImageAlign="Middle" TabIndex="103" OnClick="btnAgentCancel_Click" />&nbsp;
                                </center>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
