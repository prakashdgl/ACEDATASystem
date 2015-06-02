<%@ Page Title="" Language="C#" MasterPageFile="~/Order.Master" AutoEventWireup="true"
    CodeBehind="AddEditSupplier.aspx.cs" Inherits="ACE.PurchaseOrder.AddEditSupplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <title>Manage Customers</title>
    <script type="text/javascript">

        $(function () {
            $(':text').bind('keydown', function (e) { //on keydown for all textboxes  
                if (e.keyCode == 13) //if this is enter key  
                    e.preventDefault();
            });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPageMainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <img src="../images/progress.gif" alt="Loading..." style="float: left;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
            <table cellpadding="2" cellspacing="2" border="0" style="width: 950px; margin: 2px;"
                width="950px" height="100%">
                <tr>
                    <td valign="top" height="100%">
                        <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                            <tr id="Tr1" runat="server" align="left" style="height: 20px">
                                <td id="Td1" colspan="2" runat="server" class="LabelHeader" style="background-color: #d2ffa4;">Supplier Information<asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfSupplierID" runat="server" Value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ValidationSummary ID="vsSummary" runat="server" ShowSummary="true" DisplayMode="BulletList" />
                                </td>
                            </tr>
                            <tr id="tr2" runat="server">
                                <td colspan="2">
                                    <table border="0" cellpadding="3" cellspacing="3" style="width: 900px;" width="900px">
                                        <tr>
                                            <td style="width: 33%; width: 330px;" valign="top">
                                                <table border="0" cellpadding="3" cellspacing="3" style="width: 630px">
                                                    <tr style="height: 25px;">
                                                        <td colspan="2" class="LabelHeader" style="horiz-align: center;">
                                                            <span class="LabelHeader">ADDRESS</span>
                                                            <asp:Label ID="txtSupplierID" runat="server" Text="0" Visible="False" CssClass="TextResults"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 142px;" class="Label">
                                                            <asp:Label ID="lblSalesPersonName" runat="server" Text="Sales Person Name" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSalesPersonName" runat="server" Text="" Width="180px" MaxLength="50"
                                                                CssClass="textarea"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSalesPersonName" runat="server" ControlToValidate="txtSalesPersonName"
                                                                ErrorMessage="Please Enter Sales Person Name" Text="*"></asp:RequiredFieldValidator>
                                                            <cc1:FilteredTextBoxExtender ID="ftbeSalesPersonName" runat="server" TargetControlID="txtSalesPersonName"
                                                                FilterType="Custom" ValidChars=".()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" CssClass="Label"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSupplierName" runat="server" Text="" Width="180px" MaxLength="50"
                                                                CssClass="textarea"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSupplierName" runat="server" ControlToValidate="txtSupplierName"
                                                                ErrorMessage="Please Enter Supplier Name" Text="*"></asp:RequiredFieldValidator>
                                                            <cc1:FilteredTextBoxExtender ID="ftbeSupplierName" runat="server" TargetControlID="txtSupplierName"
                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblSupplierCompanyName" CssClass="Label" runat="server" Text="Supplier Company Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSupplierCompanyName" Width="180px" MaxLength="200" CssClass="textarea"
                                                                runat="server"></asp:TextBox>

                                                            <asp:RequiredFieldValidator ID="rfvtxtSupplierCompanyName" runat="server" ControlToValidate="txtSupplierCompanyName"
                                                                ErrorMessage="Please Enter Supplier Company Name" Text="*"></asp:RequiredFieldValidator>
                                                            <cc1:FilteredTextBoxExtender ID="ftetxtSupplierCompanyName" runat="server" TargetControlID="txtSupplierCompanyName"
                                                                FilterType="Custom" ValidChars="()-' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_&"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblAddress" CssClass="Label" runat="server" Text="Address 1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress" Width="180px" MaxLength="200" CssClass="textarea" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblAddress2" CssClass="Label" runat="server" Text="Address 2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress2" Width="180px" MaxLength="200" CssClass="textarea" runat="server"
                                                                Text=""></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblAddress3" CssClass="Label" runat="server" Text="Address 3"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAddress3" Width="180px" MaxLength="200" CssClass="textarea" runat="server"
                                                                Text=""></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblCountry" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCountry" AppendDataBoundItems="true" Width="180px" CssClass="Dropdownlist"
                                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblState" CssClass="Label" runat="server" Text="State"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlState" AppendDataBoundItems="true" Width="180px" CssClass="Dropdownlist"
                                                                runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblCity" CssClass="Label" runat="server" Text="City"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlCity" Width="180px" CssClass="Dropdownlist" runat="server">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblZipCode" CssClass="Label" runat="server" Text="Zip/Pin"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtZipCode" Width="80px" MaxLength="10" CssClass="textarea" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbeZipCode" runat="server" TargetControlID="txtZipCode"
                                                                FilterType="Custom" ValidChars=" ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-"
                                                                Enabled="True" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td style="width: 33%;" valign="top">
                                                <table border="0" cellpadding="3" cellspacing="3" style="width: 100%">
                                                    <tr style="height: 25px;">
                                                        <td colspan="2" style="horiz-align: center;">
                                                            <span class="LabelHeader">Supplier</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="width: 142px;" class="Label">
                                                            <asp:Label ID="lblWorkPhone" CssClass="Label" runat="server" Text="Work Phone"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWorkPhone" CssClass="textarea" Width="180px" MaxLength="50" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbWorkPhone" runat="server" TargetControlID="txtWorkPhone"
                                                                FilterType="Custom" ValidChars="()-+,. /0123456789" Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblHomePhone" CssClass="Label" runat="server" Text="Home Phone"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHomePhone" CssClass="textarea" Width="180px" MaxLength="50" runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbHomePhone" runat="server" TargetControlID="txtHomePhone"
                                                                FilterType="Custom" ValidChars="()-+,. /0123456789" Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblMobilePhone" CssClass="Label" runat="server" Text="Mobile Phone"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtMobilePhone" CssClass="textarea" Width="180px" MaxLength="50"
                                                                runat="server"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="ftbMobilePhone" runat="server" TargetControlID="txtMobilePhone"
                                                                FilterType="Custom" ValidChars="()-+,. /0123456789" Enabled="True" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblEmailID" CssClass="Label" runat="server" Text="Email #1"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmailID" CssClass="textarea" Width="180px" MaxLength="100" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblSecondEmailID" CssClass="Label" runat="server" Text="Email #2"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSecondEmailID" CssClass="textarea" Width="180px" MaxLength="100"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblTinono" CssClass="Label" runat="server" Text="GST/VAT Registration No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtTinNo" Width="180px" MaxLength="50" CssClass="textarea" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="visibility:hidden;">
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblCSTNumber" CssClass="Label" runat="server" Text="CST No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCCRNo" Width="180px" MaxLength="50" Text="CCRNo" CssClass="textarea" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblAgentName" CssClass="Label" runat="server" Text="Agent Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlAgentName" Width="180px" CssClass="Dropdownlist" runat="server">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td style="width: 33%" valign="top">
                                                <table border="0" cellpadding="3" cellspacing="3" style="width: 100%">
                                                    <tr style="height: 25px;">
                                                        <td colspan="2" style="horiz-align: center;">
                                                            <span class="LabelHeader">BANK</span>
                                                        </td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblBankName" CssClass="Label" runat="server" Text="Bank Name"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlBank" Width="180px" CssClass="Dropdownlist" runat="server">
                                                                <asp:ListItem Value="">-- Select One --</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblBankRegion" CssClass="Label" runat="server" Text="BankRegion"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankRegion" CssClass="textarea" Width="180px" MaxLength="120" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblBankAccountNumber" CssClass="Label" runat="server" Text="Account No"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankAccountNumber" CssClass="textarea" Width="180px" MaxLength="50"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="Label1" CssClass="Label" runat="server" Text="Bank Branch"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankBranch" CssClass="textarea" Width="180px" MaxLength="50" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="Label2" CssClass="Label" runat="server" Text="Bank Code"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBankCode" CssClass="textarea" Width="180px" MaxLength="50" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="lblBankIfsccode" CssClass="Label" runat="server" Text="IFSC Code"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIFSCCode" CssClass="textarea" Width="180px" MaxLength="50" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="Label">
                                                            <asp:Label ID="Label3" CssClass="Label" runat="server" Text="Branch Address"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBranchAddress" CssClass="textarea" Width="180px" MaxLength="300" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" class="Label">
                                                            <asp:Label ID="lblComments" CssClass="Label" runat="server" Text="Comments"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtComments" Width="220px" TextMode="MultiLine" Rows="6" CssClass="textarea"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" valign="top" class="Label">
                                                            <asp:Label ID="lblRatingSupplier" CssClass="Label" runat="server" Text="Rating"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <cc1:Rating ID="ThaiRating" runat="server" CurrentRating="1" MaxRating="10" StarCssClass="ratingStar"
                                                                WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trSaveButtonRow" runat="server">
                                <td align="center" colspan="2" valign="top" style="height: 15px;">
                                    <br />
                                    <asp:ImageButton ID="btnSave" runat="server" ImageAlign="Middle" ImageUrl="../Images/btnSave.gif"
                                        TabIndex="102" OnClick="btnSave_Click" />&nbsp;
                                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                        ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" CausesValidation="false" />&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <br />
                                    &nbsp;
                                    <hr />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

