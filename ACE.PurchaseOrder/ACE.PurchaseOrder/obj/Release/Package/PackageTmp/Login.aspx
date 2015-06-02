<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ACE.PurchaseOrder.Login" %>

<%@ Register Src="UserControls/Footer.ascx" TagName="OrderFooter" TagPrefix="uc3" %>
<%@ Register Src="UserControls/Header.ascx" TagName="OrderHeader" TagPrefix="uc1" %>
<%--<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
  Namespace="System.Web.UI" TagPrefix="asp" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="hdLogin" runat="server">
  <title>Login</title>
  <style type="text/css">
    #container #content, #container #contentBg
    {
      width: 100% !important;
      height: 600px;
    }
    #container .loginbg
    {
      /* Safari 4-5, Chrome 1-9 */
      background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#9c9eff), to(#848af7)); /* Safari 5.1, Chrome 10+ */
      background: -webkit-linear-gradient(top, #848af7, #9c9eff); /* Firefox 3.6+ */
      background: -moz-linear-gradient(top, #848af7, #9c9eff); /* IE 10 */
      background: -ms-linear-gradient(top, #848af7, #9c9eff); /* Opera 11.10+ */
      background: -o-linear-gradient(top, #848af7, #9c9eff);
    }
  </style>
  <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.01)" />
  <link href="Style/OrderStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="frmLogin" defaultbutton="btnSubmit" runat="server">
  <asp:ScriptManager ID="SMLogin" runat="server">
  </asp:ScriptManager>
  <div id="container">
    <div id="headerBg">
      <div id="headerbgBox">
        <!--    <div id="headerInfo">
        <uc1:OrderHeader ID="OrderHeader" runat="server" />
            </div>
</div>-->
      </div>
      <div id="contentBg" style="box-shadow: 0 1px 8px 0 #fff;">
        <div id="content" class="loginbg" style="background-color: #848af7;">
          <table border="0" cellpadding="0" cellspacing="0" class="HeaderTable">
            <!--<tr class="HeaderRow">
        <td colspan="2">
          <uc1:OrderHeader ID="OrderHeader1" runat="server" />
        </td>
      </tr>-->
            <tr>
              <td style="width: 497px; height: 500px; vertical-align: middle;">
                <asp:MultiView ID="mvLogin" runat="server" ActiveViewIndex="0">
                  <asp:View ID="vLogin" runat="server">
                    <table border="0" cellspacing="0" cellpadding="0" class="login" style="text-align: left;
                      width: 60%">
                      <tr>
                        <td style="font-size: 18px; text-transform: uppercase; font-weight: bold; letter-spacing: 1px; color:White;">
                          Login
                        </td>
                      </tr>
                      <tr>
                        <td>
                          <asp:Panel ID="panelLogin" runat="server" Width="95%">
                            <table cellpadding="1" cellspacing="0" style="width: 100%">
                              <tr>
                          <td align="center" colspan="2">
                            &nbsp;<cc1:ValidatorCalloutExtender ID="vceUserName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                              TargetControlID="rfvUserID">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:ValidatorCalloutExtender ID="vcePassword" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                              TargetControlID="rfvPassword">
                            </cc1:ValidatorCalloutExtender>
                          </td>
                        </tr>
                              <tr>
                                <td align="right" style="padding-top: 18px;">
                                  <strong style="color: #000;">User Name &nbsp;</strong>
                                </td>
                                <td>
                                  <asp:TextBox ID="txtUserName" runat="server" Text="" Width="79px" CssClass="textarea"
                                    MaxLength="10"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserName"
                                    ErrorMessage="Please Enter the Username" Text="*" SetFocusOnError="true" Display="none"
                                    ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                  <cc1:FilteredTextBoxExtender ID="fteUserName" runat="server" TargetControlID="txtUserName"
                                    FilterType="Custom" ValidChars="0123456789" Enabled="True" />
                                </td>
                              </tr>
                              <tr>
                                <td align="right" style="padding-top: 18px;">
                                  <strong style="color: #000;">Password &nbsp;</strong>
                                </td>
                                <td>
                                  <asp:TextBox ID="txtPassword" runat="server" Width="79px" Text="" CssClass="textarea"
                                    MaxLength="10" TextMode="Password"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please Enter the Password"
                                    ControlToValidate="txtPassword" Text="*" SetFocusOnError="true" Display="none"
                                    ValidationGroup="btnSubmit"></asp:RequiredFieldValidator>
                                </td>
                              </tr>
                              <tr>
                                <td align="center" colspan="2">
                                  <asp:CheckBox ID="chkRememberMe" runat="server" Text="Remember Me" Checked="false" />
                                </td>
                              </tr>
                              <tr>
                                <td align="center" colspan="2">
                                  <asp:Button ID="btnSubmit" runat="server" Text="Sign In" CssClass="btn" ValidationGroup="btnSubmit"
                                    OnClick="btnSubmit_Click" />
                                </td>
                              </tr>
                              <tr>
                                <td align="center" colspan="2">
                                  <asp:LinkButton ID="lbtnForgotPassword" runat="server" Text="Forgot Your Password?"
                                    ToolTip="Forgot Your Password?" OnClick="lbtnForgotPassword_Click"></asp:LinkButton>
                                </td>
                              </tr>
                              <tr>
                                <td align="center" colspan="2">
                                  <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </td>
                              </tr>
                              <%--<tr>
                          <td style="z-index: 101; left: 450px; position: absolute; top: 300px" colspan="2">
                            <div id="List" style="display: none" onmouseout="document.getElementById('List').style.display='none';">
                              &nbsp;</div>
                          </td>
                        </tr>--%>
                            </table>
                          </asp:Panel>
                          <cc1:RoundedCornersExtender ID="rce" runat="server" TargetControlID="panelLogin"
                            Radius="10" Corners="All" BorderColor="#99ccff" />
                        </td>
                      </tr>
                    </table>
                  </asp:View>
                  <asp:View ID="vForgotPassword" runat="server">
                    <table border="0" cellspacing="0" cellpadding="0" style="text-align: left; width: 95%">
                      <tr>
                        <td>
                          <asp:Panel ID="pnlForgotPassword" runat="server" Width="99%">
                            <table cellpadding="1" cellspacing="0" style="width: 90%">
                              <tr>
                                <td style="font-size: 18px; padding-bottom: 10px; text-transform: uppercase; font-weight: bold;
                                  letter-spacing: 1px; text-align: left; color:White;">
                                  Forgot your password
                                </td>
                              </tr>
                              <tr>
                                <td>
                                  <asp:Panel ID="pnlInnerTable" runat="server" Width="98%">
                                    <table cellpadding="3" cellspacing="3" style="width: 100%">
                                     <%-- <tr>
                                        <td align="left" colspan="2">
                                          &nbsp;
                                        </td>
                                      </tr>--%>
                                      <tr>
                                        <td colspan="2">
                                          &nbsp;<cc1:ValidatorCalloutExtender ID="vceForgotUserName" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="rfvForgotUserName">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceForgotEmailID" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="rfvEmailID">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceValidateEmailID" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="revOfficeMailID">
                                          </cc1:ValidatorCalloutExtender>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="right" style="padding-top: 15px;">
                                          <strong style="color: #000;">Username &nbsp;</strong>
                                        </td>
                                        <td>
                                          <asp:TextBox ID="txtForgotUserName" runat="server" Text="" Width="150px" CssClass="textarea"
                                            MaxLength="10"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvForgotUserName" runat="server" ControlToValidate="txtForgotUserName"
                                            ErrorMessage="Please Enter the Username" Text="*" SetFocusOnError="true" Display="none"
                                            ValidationGroup="ValidateForgotPassword"></asp:RequiredFieldValidator>
                                          <cc1:FilteredTextBoxExtender ID="ftbeForgotUserName" runat="server" TargetControlID="txtForgotUserName"
                                            FilterType="Custom" ValidChars="0123456789" Enabled="True" />
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="right" style="padding-top: 15px;">
                                          <strong style="color: #000;">Email ID &nbsp;</strong>
                                        </td>
                                        <td>
                                          <asp:TextBox ID="txtEmailID" runat="server" Width="150px" Text="" CssClass="textarea"
                                            MaxLength="75"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ErrorMessage="Please Enter the EmailID. <br> To reset your password, type the full email address you use to sign in to your Google Account"
                                            ControlToValidate="txtEmailID" Text="*" SetFocusOnError="true" Display="none" ValidationGroup="ValidateForgotPassword"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="revOfficeMailID" runat="server" ControlToValidate="txtEmailID"
                                            ErrorMessage="Invalid Email address format" Display="none" Text="*" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$"
                                            ValidationGroup="ValidateForgotPassword" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="center">
                                          &nbsp;
                                        </td>
                                        <td>
                                          <asp:Button ID="ibtnSubmitForgotPassword" runat="server" Text="Submit" CssClass="btn" 
                                            ValidationGroup="ValidateForgotPassword" CausesValidation="true" OnClick="ibtnSubmitForgotPassword_Click" />
                                          &nbsp;
                                          <asp:Button ID="ibtnCancelForgotPassword" runat="server" CssClass="btn" Text="Cancel" 
                                            CausesValidation="false" OnClick="ibtnCancelForgotPassword_Click" />
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="center" colspan="2">
                                          <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                      </tr>
                                    </table>
                                  </asp:Panel>
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                          <%--<cc1:RoundedCornersExtender ID="rceForgotPassword" runat="server" TargetControlID="pnlInnerTable"
                            Radius="10" Corners="All" BorderColor="#99ccff" />--%>
                        </td>
                      </tr>
                    </table>
                  </asp:View>
                  <asp:View ID="vChangePassword" runat="server">
                    <table border="0" cellspacing="0" cellpadding="0" style="text-align: left; width: 95%">
                      <tr>
                        <td>
                          <asp:Panel ID="pnlChangePassword" runat="server" Width="99%">
                            <table cellpadding="1" cellspacing="0" border="0" style="width: 90%">
                              <tr>
                                <td align="left">
                                  &nbsp;
                                  <h3>
                                    Change your password</h3>
                                </td>
                              </tr>
                              <tr>
                                <td>
                                  <asp:Panel ID="pnlInnerChangePassword" runat="server" Width="98%">
                                    <table border="0" cellpadding="0" cellspacing="0" style="text-align: left; width: 98%">
                                      <tr>
                                        <td id="Td1" colspan="3" runat="server">
                                          <cc1:ValidatorCalloutExtender ID="vceChangePassword" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="rfvOldPwd">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceChangePassword1" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="rfvPwd">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceChangePassword2" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="rfvconfpwd">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceChangePassword3" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="cvpwd">
                                          </cc1:ValidatorCalloutExtender>
                                          <cc1:ValidatorCalloutExtender ID="vceChangePassword4" runat="Server" HighlightCssClass="validatorCalloutHighlight"
                                            TargetControlID="revPasswordLengthRegEx">
                                          </cc1:ValidatorCalloutExtender>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td id="Td2" align="right" style="width: 49%;" runat="server" class="LabelTitle">
                                          <strong style="color: #DC611B;">Enter Current Password</strong>
                                        </td>
                                        <td>
                                          &nbsp;
                                        </td>
                                        <td align="left" style="width: 50%;">
                                          <asp:TextBox ID="txtOldPwd" runat="server" CssClass="textarea" MaxLength="10" TextMode="Password"
                                            Width="100px"></asp:TextBox>&nbsp;
                                          <asp:RequiredFieldValidator ID="rfvOldPwd" runat="server" Text="" SetFocusOnError="true"
                                            ErrorMessage="Please Enter Old Password" Display="none" ControlToValidate="txtOldPwd"></asp:RequiredFieldValidator>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td id="Td3" align="right" runat="server" class="LabelTitle">
                                          <strong style="color: #DC611B;">Enter New Password</strong>
                                        </td>
                                        <td>
                                          &nbsp;
                                        </td>
                                        <td align="left">
                                          <asp:TextBox ID="txtpwd" runat="server" CssClass="textarea" TextMode="Password" Width="100px"
                                            MaxLength="10"></asp:TextBox>&nbsp;
                                          <asp:RequiredFieldValidator ID="rfvPwd" runat="server" Text="" SetFocusOnError="true"
                                            ErrorMessage="Please Enter New Password" Display="none" ControlToValidate="txtpwd"></asp:RequiredFieldValidator>
                                          <asp:RegularExpressionValidator ID="revPasswordLengthRegEx" runat="server" ControlToValidate="txtpwd"
                                            ErrorMessage="Password must be min:6, max:15 characters." Display="none" SetFocusOnError="true"
                                            ToolTip="Password format is not correct" ValidationExpression="^(?=.{6,15}).*$">
                                          </asp:RegularExpressionValidator>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td id="Td4" align="right" runat="server" class="LabelTitle">
                                          <strong style="color: #DC611B;">Confirm New Password</strong>
                                        </td>
                                        <td>
                                          &nbsp;
                                        </td>
                                        <td id="Td5" align="left" runat="server" class="Label">
                                          <asp:TextBox ID="txtconfpwd" runat="server" CssClass="textarea" TextMode="Password"
                                            Width="100px" MaxLength="10"></asp:TextBox>&nbsp;
                                          <asp:RequiredFieldValidator ID="rfvconfpwd" runat="server" Text="" SetFocusOnError="true"
                                            ErrorMessage="Please Enter Confirm Password" Display="none" ControlToValidate="txtconfpwd"></asp:RequiredFieldValidator>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="center" colspan="3">
                                          <asp:CompareValidator ID="cvpwd" runat="server" ControlToCompare="txtpwd" Display="none"
                                            ControlToValidate="txtconfpwd" ErrorMessage="Password Entered is not Matching"></asp:CompareValidator>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="center" colspan="3">
                                          <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="red"></asp:Label>
                                        </td>
                                      </tr>
                                      <tr>
                                        <td align="center" colspan="3">
                                          <br />
                                          <asp:Button ID="ibtnChangePasswordSubmit" runat="server" Text="Submit" CssClass="btn" 
                                            OnClick="ibtnChangePasswordSubmit_Click" />
                                        </td>
                                      </tr>
                                    </table>
                                  </asp:Panel>
                                </td>
                              </tr>
                            </table>
                          </asp:Panel>
                        <%--  <cc1:RoundedCornersExtender ID="rceChangePassword" runat="server" TargetControlID="pnlInnerChangePassword"
                            Radius="10" Corners="All" BorderColor="#99ccff" />--%>
                        </td>
                      </tr>
                    </table>
                  </asp:View>
                </asp:MultiView>
              </td>
            </tr>
            <%--<tr>
        <td colspan="2">
          <uc3:OrderFooter ID="OrderFooter1" runat="server" />
        </td>
      </tr>--%>
          </table>
          <div style="clear: both">
          </div>
        </div>
      </div>
      <div id="footer">
        <uc3:OrderFooter ID="OrderFooter" runat="server" />
      </div>
    </div>
  </form>
</body>
</html>
