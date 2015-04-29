<%@ Control Language="C#" AutoEventWireup="true" Codebehind="UserMenu.ascx.cs" Inherits="ACE.PurchaseOrder.UserControls.UserMenu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Style/ECafeStyleSheet.css" rel="stylesheet" type="text/css" />

<table border="0" cellspacing="0" cellpadding="0" style="width: 100%; margin-top: 0px;
    background-color: #f9f9f9; border-right: #cde5fa thin solid;">
    <tr>
        <td valign="top">
            <asp:DataGrid ID="gvTreeMenu" AutoGenerateColumns="false" ShowFooter="false" ShowHeader="false" Width="100%" BorderStyle="None" runat="server" OnItemCommand="gvTreeMenu_ItemCommand" OnItemDataBound="gvTreeMenu_ItemDataBound">
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:Table ID="tblMenu" CellPadding="0" CellSpacing="0" runat="server">
			                       <asp:TableRow ID="trMenu" runat="server">
			                            <asp:TableCell ID="tdMenu1" runat="server" Width="20" Height="22">
			                                 <img src="../images/HeaderImages/bullet_white.gif" width="12" height="9" />
			                            </asp:TableCell>
			                            <asp:TableCell ID="tdMenu2" runat="server" width="170" height="22" CssClass="menu_blue_text menu_bg">
			                                <asp:LinkButton ID="lnkWebSite" runat="server" Text='<%# Eval("title") %>' CommandName="Select" CommandArgument='<%# Eval("url", "{0}") %>' CausesValidation="false"></asp:LinkButton>
			                            </asp:TableCell>
			                       </asp:TableRow>
			                       <asp:TableRow>
			                            <asp:TableCell Height="2" ColumnSpan="2" BackColor="#1f5cc1"></asp:TableCell>
			                       </asp:TableRow>
                              </asp:Table>
                        </ItemTemplate> 
                    </asp:TemplateColumn> 
                </Columns> 
            </asp:DataGrid>
        </td>
    </tr>
    <tr>
        <td align="center">
            <br />
            &nbsp;&nbsp;&nbsp;<asp:Image ID="imgPicture" runat="server" BorderColor="Red" BorderStyle="Groove"
                Height="173px" ImageUrl="~/images/No_photo.jpg" Width="136px" />
            <br />
        </td>
    </tr>
    <tr>
        <td>
            <asp:ImageButton ID="btnEcafeImage" runat="server" ImageUrl="../images/ecafe.jpg"
                Height="90px" Width="100px" />
            <cc1:AlwaysVisibleControlExtender ID="ace" runat="server" TargetControlID="btnEcafeImage"
                VerticalSide="Bottom" VerticalOffset="30" HorizontalSide="Right" HorizontalOffset="40"
                ScrollEffectDuration=".1" />
        </td>
    </tr>
</table>
