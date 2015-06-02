<%@ Page Title="User Role" Language="C#" AutoEventWireup="true" CodeBehind="UserPageSettings.aspx.cs"
  EnableEventValidation="false" Inherits="ACE.PurchaseOrder.UserPageSettings" MasterPageFile="~/Order.Master" %>

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
    <asp:Label runat="server" CssClass="pageTitleLabel" Text="User Page Settings"></asp:Label>
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
            &nbsp;&nbsp;<asp:Label ID="lblUser" CssClass="LabelTitle" runat="server" Text="Employee Name"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="Dropdownlist" Width="190px"
              AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
              <asp:ListItem Value="">-- Select One --</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:DropDownList ID="ddlEmployee" runat="server" Width="180px" AutoPostBack="True"
              CssClass="Dropdownlist" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
              <asp:ListItem Value="">-- Select One --</asp:ListItem>
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRoleDescription" Text="" CssClass="LabelTitle"
              runat="server"></asp:Label>&nbsp;
            <asp:Label ID="lblRoleName" Text="" CssClass="Label" runat="server"></asp:Label>
            <br />
          </td>
          <td style="width: 35%" class="tdLabelTitle">
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
          <td style="width: 100%;" colspan="2">
            <br />
            <asp:Panel CssClass="grid" ID="pnlCust" runat="server">
              <asp:UpdatePanel ID="pnlUpdate" UpdateMode="Conditional" ChildrenAsTriggers="false"
                runat="server">
                <ContentTemplate>
                  <asp:GridView Width="100%" AllowPaging="False" ID="gvUserPageSettings" AutoGenerateColumns="False"
                    runat="server" ShowHeader="false" GridLines="None" ShowFooter="false" OnRowCreated="gvUserPageSettings_RowCreated">
                    <Columns>
                      <asp:TemplateField>
                        <ItemTemplate>
                          <asp:Panel CssClass="group" ID="pnlUserPageSettings" Enabled="true" Width="100%"
                            runat="server">
                            <table width="100%">
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
                                  <asp:CheckBox ID="cbSelectModule" runat="server" AutoPostBack="true" Checked="false"
                                    Text="Select All Pages" Visible="False" Width="150px" OnCheckedChanged="cbSelectModule_CheckedChanged" />
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                          <asp:ObjectDataSource ID="odsUserPageSettings" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetUserXPagesListByModule" TypeName="ACE.PurchaseOrder.ACEPetroDSTableAdapters.spGetUsersXPagesListByModulesTableAdapter">
                            <SelectParameters>
                              <asp:Parameter Name="UserID" Type="int32" DefaultValue="" />
                              <asp:Parameter Name="CompanyID" Type="int32" DefaultValue="" />
                              <asp:Parameter Name="ModuleName" Type="String" DefaultValue="" />
                            </SelectParameters>
                            <UpdateParameters>
                              <asp:ControlParameter Name="PageID" Type="Int32" ControlID="gvUpdateUserPageSettings"
                                PropertyName="SelectedValue" />
                            </UpdateParameters>
                          </asp:ObjectDataSource>
                          <asp:Panel Style="margin-left: 0px; margin-right: 0px;" ID="pnlUserPage" runat="server">
                            <asp:UpdatePanel ID="pnlUpdateUserPageSettings" UpdateMode="Always" runat="server">
                              <ContentTemplate>
                                <table border="0" style="width: 100%;" class="tdTextResults">
                                  <tr>
                                    <td>
                                      <asp:GridView ID="gvUpdateUserPageSettings" DataSourceID="odsUserPageSettings" AutoGenerateColumns="False"
                                        DataKeyNames="PageID" BorderWidth="1" CssClass="grid" runat="server" HeaderStyle-CssClass="GridviewHeader"
                                        OnInit="gvUpdateUserPageSettings_Init" OnRowCommand="gvUpdateUserPageSettings_RowCommand"
                                        OnRowDataBound="gvUpdateUserPageSettings_RowDataBound" Width="100%">
                                        <RowStyle CssClass="row" />
                                        <AlternatingRowStyle CssClass="altrow" />
                                        <Columns>
                                          <asp:TemplateField HeaderText="S.No." HeaderStyle-BackColor="#CDDCF1" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                              <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle BackColor="White" Width="5%" HorizontalAlign="Right" />
                                          </asp:TemplateField>
                                          <asp:BoundField DataField="PageID" HeaderText="PageID" HeaderStyle-BackColor="#CDDCF1" />
                                          <asp:BoundField DataField="ModuleName" HeaderText="Module Name" HeaderStyle-BackColor="#CDDCF1" />
                                          <asp:BoundField DataField="PageName" HeaderText="Page Name" ItemStyle-HorizontalAlign="Left"
                                            HeaderStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#CDDCF1">
                                            <ItemStyle Width="60%" BackColor="White" height="80%"/>
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
                          <cc1:CollapsiblePanelExtender ID="cpe" runat="Server" TargetControlID="pnlUserPage"
                            CollapsedSize="0" Collapsed="True" ExpandControlID="imgCollapsible" CollapseControlID="imgCollapsible"
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
