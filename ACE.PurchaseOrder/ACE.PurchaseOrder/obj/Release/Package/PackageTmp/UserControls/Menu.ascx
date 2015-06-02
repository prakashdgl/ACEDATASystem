<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="ACE.PurchaseOrder.Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Style/Style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
  function PopUpWindowCallBack(returnValue, txtControlID, txtImageID) {
    var txtImageURL = document.getElementById(txtControlID);
    txtImageURL.value = returnValue;

    var txtPhotoURL = document.getElementById(txtImageID);
    txtPhotoURL.src = returnValue;
  }
</script>
<asp:ScriptManagerProxy ID="proxy" runat="server">
  <Scripts>
  </Scripts>
</asp:ScriptManagerProxy>
<input id="RowsPerPageFD" type="hidden" value="10" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
  <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btnSelect" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnUploadPhotoSave" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnUploadCancel" EventName="Click" />
  </Triggers>
  <ContentTemplate>
    <table border="0" cellspacing="0" cellpadding="0" style="width: 100%; vertical-align: top;
      text-align: center;">
      <tr style="text-align: left">
        <td id="tdMenu2" runat="server" style="vertical-align: top;">
          <div id="EasyMenu" style="width: 100%;">
            <asp:Menu ID="ECMenu" Width="180px" runat="server" CssClass="menu" DynamicHorizontalOffset="1"
              StaticDisplayLevels="1" StaticSubMenuIndent="10px" MaximumDynamicDisplayLevels="10"
              StaticEnableDefaultPopOutImage="false" DynamicEnableDefaultPopOutImage="false"
              OnMenuItemDataBound="ECMenu_MenuItemDataBound">
              <StaticSelectedStyle BackColor="LightBlue"
                                BorderStyle="Solid"
                                BorderColor="Black"
                                BorderWidth="1" />
              <StaticMenuStyle CssClass="mainmenu" />
              <DynamicMenuStyle CssClass="submenu" />
              
              <DataBindings>
                <asp:MenuItemBinding DataMember="System.Data.DataRowView" TextField="Text" Text="Text"
                  ValueField="ID" NavigateUrlField="Url" ToolTipField="Text" Value="Text" />
              </DataBindings>
            </asp:Menu>
          </div>
        </td>
      </tr>     
      <tr>
        <td>
          <asp:Panel ID="pnlAddPhoto" runat="server" CssClass="modalPopup" Style="display: none"
            Width="380px">
            <table style="width: 100%; text-align: center" border="0" cellpadding="1" cellspacing="2">
              <tr>
                <td colspan="2" class="tdLabelCenter">
                  <asp:Label ID="lblpnlAddPhoto" runat="server" Text="Select Profile Picture" CssClass="PopupPanelHeading"></asp:Label>
                </td>
              </tr>
              <tr>
                <td colspan="2" class="tdLabelCenter">
                  <hr />
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Image ID="imgAddPhoto" runat="server" BorderColor="Red" BorderStyle="Groove"
                    Height="173px" ImageUrl="~/images/No_photo.jpg" Width="136px" />
                </td>
              </tr>
              <tr>
                <td style="vertical-align: middle;">
                  <table>
                    <tr style="text-align: center; float: left; padding: 15px 0 0 23px;">
                      <td>
                        <div id="divFileUpload" style="vertical-align: middle;">
                          <input type="text" name="txtImageURL" id="txtImageURL" runat="server" readonly="readonly"
                            class="textarea" />
                          <asp:RequiredFieldValidator ID="rfvconfphoto" runat="server" Text="" SetFocusOnError="true"
                            ErrorMessage="Please Select the Photo" Display="none" ValidationGroup="photo" ControlToValidate="txtImageURL"></asp:RequiredFieldValidator>
                        </div>
                      </td>
                      <td>
                        <asp:ImageButton ID="btnSelect" runat="server" ToolTip="Select" ImageUrl="~/Images/btnSelect.gif"
                          CausesValidation="false" OnClick="btnSelect_Click" />
                        <asp:ImageButton runat="server" ID="btnClear" ToolTip="Clear" ImageUrl="~/Images/btnClear.gif"
                          OnClick="btnClear_Click" CausesValidation="false" />
                      </td>
                    </tr>
                  </table>
                  <br />
                </td>
              </tr>
              <tr>
                <td style="width: 100%; text-align: center">
                  &nbsp;<asp:ImageButton ID="btnUploadPhotoSave" runat="server" ImageUrl="~/Images/btnSave.gif"
                    OnClick="btnUploadPhotoSave_Click" ValidationGroup="photo" CausesValidation="true" />
                  <asp:ImageButton ID="btnUploadCancel" runat="server" ImageUrl="~/Images/btnCancel.gif"
                    OnClick="btnUploadCancel_Click" CausesValidation="false" />
                </td>
              </tr>
              <tr>
                <td>
                  <asp:Label ID="lblStatus" Text="" runat="server"></asp:Label>
                  <cc1:ValidatorCalloutExtender ID="vceconfphoto" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                    TargetControlID="rfvconfphoto">
                    <Animations>
                                                                    <OnShow>                                    
                                                                    <Sequence>   
                                                                    <HideAction Visible="true" /> 
                                                                    <FadeIn Duration="0" MinimumOpacity="0" MaximumOpacity="1" />
                                                                    </Sequence>
                                                                    </OnShow>
                                                                    <OnHide>
                                                                    <Sequence>    
                                                                    <FadeOut Duration="0" MinimumOpacity="0" MaximumOpacity="1" />
                                                                    <HideAction Visible="false" />
                                                                    </Sequence>
                                                                    </OnHide>
                    </Animations>
                  </cc1:ValidatorCalloutExtender>
                </td>
              </tr>
            </table>
          </asp:Panel>
        </td>
      </tr>
    </table>
    <asp:HiddenField ID="hfSelectPhotoDummy" runat="server" />
    <cc1:ModalPopupExtender Drag="false" ID="mpSelectPhoto" runat="server" BackgroundCssClass="modalBackground"
      PopupControlID="pnlSelectPhoto" TargetControlID="hfSelectPhotoDummy" DynamicServicePath=""
      Enabled="True">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlSelectPhoto" runat="server" CssClass="modalPopup" Style="display: none;
      height: 610px; width: 1010px">
      <iframe id="ifrSelectPhoto" style="height: 610px; width: 1010px" runat="server">
      </iframe>
    </asp:Panel>
    <table style="background-color:White;">
      <tr id="trProfileCount" runat="server">
        <td style="width:100%;">
          <br />
          <table style="visibility:hidden;" >
            <tr>
              <td align="center">
                <strong>Your Profile is
                  <asp:Label ID="lblProfileCount" Text="" runat="server"></asp:Label>
                  % Complete</strong>
              </td>
            </tr>
            <tr>
              <td style="border-color: Black; border-style: solid; border-width: 1px; vertical-align: middle;
                text-align: left; height: 13px;width:204px;">
                <asp:Image ID="imgEmployeeProfileCount" runat="server" ImageUrl="~/Images/NewImages/pic.jpg"
                  Height="12px" />
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </ContentTemplate>
</asp:UpdatePanel>
