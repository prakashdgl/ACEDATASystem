<%@ Page Title="Role Settings" Language="C#" AutoEventWireup="true" CodeBehind="RolePageSettings.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.RolePageSettings" MasterPageFile="~/Order.Master" %>

<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
  <style type="text/css">
    .popupMenu
    {
      position: absolute;
      visibility: hidden;
      background-color: #F5F7F8;
      opacity: .9;
      filter: alpha(opacity=90);
    }
    
    .popupHover
    {
      background-image: url(img/header-opened.png);
      background-repeat: repeat-x;
      background-position: left top;
      background-color: #F5F7F8;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
  <div class="pageTitleDiv">
    <asp:Label runat="server" CssClass="pageTitleLabel" Text="Role Settings"></asp:Label><hr />
  </div>
  <asp:UpdatePanel runat="server" UpdateMode="Always">
    <ContentTemplate>
      <table border="0" cellpadding="3" cellspacing="2" width="100%">
       <%-- <tr>
          <td style="height: 25px;" colspan="2">
            &nbsp;
          </td>
        </tr>--%>
        <tr>
          <td>
            &nbsp;&nbsp;
            <asp:Label ID="lblRole" CssClass="LabelTitle" runat="server" Text="Select Role"></asp:Label>
            &nbsp;<asp:DropDownList ID="ddlRole" runat="server" CssClass="Dropdownlist" Width="150px"
              AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
              <asp:ListItem Value="">-- Select One --</asp:ListItem>
            </asp:DropDownList>
          </td>
          <td style="width: 50%" class="tdLabelTitle">
            <%--<asp:ImageButton ID="btnSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
              CausesValidation="true" TabIndex="12" OnClick="btnSave_Click" />&nbsp;
            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
              CausesValidation="False" ImageAlign="Middle" TabIndex="13" OnClick="btnCancel_Click" />--%>
              <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn"
              CausesValidation="true" TabIndex="12" OnClick="btnSave_Click" />&nbsp;
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn"
              CausesValidation="False"  TabIndex="13" OnClick="btnCancel_Click" />
          </td>
        </tr>
        <tr>
          <td colspan="2" style="width: 100%">
            <br />
            <asp:Panel CssClass="grid" ID="pnlHeaderRoleSettings" runat="server">
              <asp:UpdatePanel ID="pnlUpdate" UpdateMode="Conditional" runat="server" RenderMode="Inline">
                <ContentTemplate>
                  <asp:GridView ID="gvRoleSettings" Width="100%" AllowPaging="False" AutoGenerateColumns="False"
                    runat="server" ShowHeader="false" GridLines="None" ShowFooter="false" OnRowCreated="gvRoleSettings_RowCreated">
                    <Columns>
                      <asp:TemplateField>
                        <ItemTemplate>
                          <asp:Panel CssClass="group" ID="pnlRoleSettings" Enabled="true" Width="100%" runat="server">
                            <table border="0" width="100%">
                              <tr>
                                <td style="vertical-align: bottom; width: 5%;" class="tdTextResults">
                                  <asp:Image ID="imgCollapsible" CssClass="first" ImageUrl="../Images/collapse.jpg"
                                    Style="margin-right: 5px;" runat="server" />
                                </td>
                                <td style="vertical-align: bottom; width: 55%;" class="tdTextResults">
                                  <b><span id="Span1" class="header" enableviewstate="true" runat="server">
                                    <%#Eval("ModuleName")%>
                                  </span></b>
                                  <asp:HiddenField ID="hfModuleName" runat="server" Value='<%#Eval("ModuleName")%>' />
                                </td>
                                <td style="vertical-align: bottom; width: 40%;" class="tdLabelTitle">
                                  &nbsp;
                                  <asp:CheckBox ID="cbSelectModule" Visible="False" runat="server" AutoPostBack="true"
                                    Text="Select Entire Module " Width="200px" OnCheckedChanged="cbSelectModule_CheckedChanged" />
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                          <asp:ObjectDataSource ID="odsRoleSettings" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetRoleSettingsByModule" TypeName="ACE.PurchaseOrder.ACEPetroDSTableAdapters.spGetRolesPagesListByModulesTableAdapter">
                            <SelectParameters>
                              <asp:Parameter Name="RoleID" Type="int32" DefaultValue="" />
                              <asp:Parameter Name="ModuleName" Type="String" DefaultValue="" />
                            </SelectParameters>
                            <UpdateParameters>
                              <asp:ControlParameter Name="PageID" Type="Int32" ControlID="gvUpdateRoleSettings"
                                PropertyName="SelectedValue" />
                            </UpdateParameters>
                          </asp:ObjectDataSource>
                          <asp:Panel ID="pnlRole" Style="margin-left: 0px; margin-right: 0px;" runat="server">
                            <asp:UpdatePanel ID="pnlUpdateRoleSettings" UpdateMode="Always" runat="server">
                              <ContentTemplate>
                                <table border="0" style="width: 100%;" class="tdTextResults">
                                  <tr>
                                    <td>
                                      <asp:GridView ID="gvUpdateRoleSettings" DataSourceID="odsRoleSettings" AutoGenerateColumns="False"
                                        DataKeyNames="PageID" BorderWidth="1" ItemStyle-CssClass="Gridview" HeaderStyle-CssClass="GridviewHeader"
                                        runat="server" OnInit="gvUpdateRoleSettings_Init" OnRowCommand="gvUpdateRoleSettings_RowCommand"
                                        OnRowDataBound="gvUpdateRoleSettings_RowDataBound" Width="100%">
                                        <RowStyle CssClass="row" />
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <Columns>
                                          <asp:TemplateField HeaderText="S.No."  HeaderStyle-BackColor="#CDDCF1">
                                            <ItemTemplate>
                                              <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="White" Width="5%" HorizontalAlign="Right" />
                                          </asp:TemplateField>
                                          <asp:BoundField DataField="PageID" HeaderText="PageID" HeaderStyle-BackColor="#CDDCF1" />
                                          <asp:BoundField DataField="ModuleName" HeaderText="Module Name" HeaderStyle-BackColor="#CDDCF1" />
                                          <asp:BoundField DataField="PageName" HeaderText="Page Name" 
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#CDDCF1" ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle Width="60%" BackColor="White" Height="60%" />
                                          </asp:BoundField>
                                          <asp:BoundField DataField="IsAddOrEdit" ItemStyle-CssClass="Gridview" HeaderStyle-BackColor="#CDDCF1" />
                                          <asp:BoundField DataField="IsDelete" ItemStyle-CssClass="Gridview" HeaderStyle-BackColor="#CDDCF1" />
                                        </Columns>
                                      </asp:GridView>
                                    </td>
                                  </tr>
                                </table>
                              </ContentTemplate>
                            </asp:UpdatePanel>
                          </asp:Panel>
                          <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlRole" CollapsedSize="0"
                            Collapsed="True" ExpandControlID="imgCollapsible" CollapseControlID="imgCollapsible"
                            AutoCollapse="False" AutoExpand="False" ScrollContents="false" ImageControlID="imgCollapsible"
                            ExpandedImage="../Images/collapse.jpg" CollapsedImage="../Images/expand.jpg" ExpandDirection="Vertical" />
                        </ItemTemplate>
                      </asp:TemplateField>
                    </Columns>
                  </asp:GridView>
                </ContentTemplate>
              </asp:UpdatePanel>
            </asp:Panel>
          </td>
        </tr>
      </table>
    </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
