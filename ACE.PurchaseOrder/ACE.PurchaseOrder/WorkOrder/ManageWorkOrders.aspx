<%@ Page Title="" Language="C#" MasterPageFile="~/COrder.Master" AutoEventWireup="true"
    CodeBehind="ManageWorkOrders.aspx.cs" Inherits="ACE.PurchaseOrder.ManageSuppliers" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
     <title>Manage Suppliers</title>
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
    <table cellpadding="0" cellspacing="0" border="0" width="100%" height="100%">
        <tr>
            <td valign="top" height="100%">
                <div style="word-spacing: 2px; line-height: 1.5" align="justify">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr id="Tr1" runat="server" align="left" style="height: 20px">
                            <td colspan="2" class="LabelHeader">
                                <h3>
                                    Dealers Information
                                </h3>
                                <asp:HiddenField ID="hfCompanyID" runat="server" Value="0" />
                            </td>
                        </tr>
                        <tr id="trSearchAndAddRow" class="" runat="server" align="left" style="height: 30px">
                            <td>
                                <asp:TextBox ID="txtSearchID" CssClass="textarea" runat="server" Width="250px" MaxLength="15"
                                    TabIndex="2" Font-Size="Large" Height="21px"></asp:TextBox>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsBottom" ImageUrl="~/Images/btnSearch.gif"
                                    TabIndex="3" OnClick="btnSearch_Click"></asp:ImageButton>&nbsp;
                            </td>
                            <td align="right">
                                <asp:ImageButton ID="btnCreate" runat="server" ImageUrl="../Images/btnCreateNew.gif"
                                    ImageAlign="Top" TabIndex="4" OnClick="btnCreate_Click" />
                            </td>
                        </tr>
                        <tr id="tr6" runat="server">
                            <td colspan="2" style="vertical-align: top; text-align: center;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trGridRow" runat="server">
                            <td colspan="2" style="vertical-align: top; text-align: center;">
                                <asp:GridView ID="gvSupplierDetails" runat="server" DataKeyNames="SupplierID" CellPadding="5"
                                    HeaderStyle-HorizontalAlign="Left" TabIndex="5" OnSelectedIndexChanged="gvSupplierDetails_SelectedIndexChanged"
                                    OnRowDeleting="gvSupplierDetails_RowDeleting" OnRowCreated="gvSupplierDetails_RowCreated" OnRowEditing="gvSupplierDetails_RowEditing"
                                    OnRowDataBound="gvSupplierDetails_RowDataBound" OnPageIndexChanging="gvSupplierDetails_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerial" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField DataTextField="SupplierName" HeaderText="Supplier Name" CommandName="select"
                                            SortExpression="SupplierName" ItemStyle-Width="33%">
                                            <ItemStyle Font-Size="8"></ItemStyle>
                                        </asp:ButtonField>
                                        <%--<asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Width="17%" SortExpression="Country">
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="StateName" HeaderText="State" ItemStyle-Width="13%" SortExpression="State">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CityName" HeaderText="City" ItemStyle-Width="11%" SortExpression="City">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TinNo" HeaderText="Tin No" ItemStyle-Width="11%"></asp:BoundField>
                                        <asp:BoundField DataField="CCRNo" HeaderText="CCR" ItemStyle-Width="11%"></asp:BoundField>
                                        <%--<asp:TemplateField HeaderText="EMail #1" ItemStyle-Width="18%" SortExpression="HomeEmail">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" Text='<%# Eval("HomeEmail") %>' NavigateUrl='<%# Eval("HomeEmail", "mailto:{0}") %>'
                                                    ID="Email1" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:BoundField DataField="MobilePhone" HeaderText="Mobile" ItemStyle-Width="11%">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100%" align="left">
                                                             <asp:ImageButton ID="ibtnEditSupplier" runat="server" ToolTip="Edit" ImageUrl="~/Images/detailButton.gif"
                                                                                            AlternateText="Edit" CommandName="Edit"></asp:ImageButton>
                                                            
                                                        </td>
                                                        <%--<td style="width: 50%" align="right">
                                                            <asp:ImageButton ID="ibtnDeleteSupplier" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Accounts" ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="width: 100%" align="left">
                                                             <%--<asp:ImageButton ID="ibtnViewAccount" runat="server" ToolTip="View Account" ImageUrl="~/Images/detailButton.gif"
                                                                                            AlternateText="View Account" CommandName="Delete"></asp:ImageButton>--%>
                                                            <%--<asp:ImageButton ID="ibtnEditSupplier" runat="server" ToolTip="Edit" ImageUrl="~/Images/icon_edit.gif"
                                                                AlternateText="Edit" CommandName="Edit"></asp:ImageButton>--%>
                                                        </td>
                                                        <%--<td style="width: 50%" align="right">
                                                            <asp:ImageButton ID="ibtnDeleteSupplier" runat="server" ToolTip="Delete" ImageUrl="~/Images/icon_del.gif"
                                                                AlternateText="Delete" CommandName="Delete"></asp:ImageButton>
                                                        </td>--%>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trSupplierDetailView" runat="server">
                            <td style="width: 50%" valign="top">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                    <tr>
                                        <td align="right" class="Label" style="width: 20%">
                                            <asp:Label ID="lblLastName" runat="server" Text="Sales Person Name" CssClass="Label"></asp:Label>
                                        </td>
                                        <td style="width: 80%" align="left">
                                            <asp:Label ID="lblSalesPersonNameValue" runat="server" Text="Sales Person Name Description" CssClass="TextResults"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name" CssClass="Label"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblSupplierNameValue" runat="server" Text="Supplier Name Value" CssClass="TextResults"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblSupplierCompanyName" runat="server" Text="Supplier Company Name" CssClass="Label"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblSupplierCompanyNameValue" runat="server" Text="Supplier Company Name Value" CssClass="TextResults"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblAddress" CssClass="Label" runat="server" Text="Address"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblAddressValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblAddress2Value" CssClass="TextResults" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblCountry" CssClass="Label" runat="server" Text="Country"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCountryValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblState" CssClass="Label" runat="server" Text="State"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblStateValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblCity" CssClass="Label" runat="server" Text="City"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblCityValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblZipCode" CssClass="Label" runat="server" Text="Zip/Pin"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblZipCodeValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblSupplierID" runat="server" Text="Supplier ID" Visible="false" CssClass="Label"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblSupplierIDValue" runat="server" Text="SupplierID" Visible="false"
                                                CssClass="TextResults"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%" valign="top">
                                <table border="0" cellpadding="3" cellspacing="1" width="100%">
                                    <tr valign="top">
                                        <td align="right" class="Label" style="width: 20%">
                                            <asp:Label ID="lblEmailID" CssClass="Label" runat="server" Text="Email #1"></asp:Label>
                                        </td>
                                        <td style="width: 80%" align="left">
                                            <asp:Label ID="lblEmailIDValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblSecondEmailID" CssClass="Label" runat="server" Text="Email #2"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblSecondEmailIDValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblHomePhone" CssClass="Label" runat="server" Text="Home Phone"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblHomePhoneValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblWorkPhone" CssClass="Label" runat="server" Text="Work Phone"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblWorkPhoneValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="Label">
                                            <asp:Label ID="lblMobilePhone" CssClass="Label" runat="server" Text="Mobile Phone"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblMobilePhoneValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top" class="Label">
                                            <asp:Label ID="lblComments" CssClass="Label" runat="server" Text="Comments"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblCommentsValue" CssClass="TextResults" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trSupplierDetailTabs" runat="server">
                            <td colspan="2" align="left" valign="top">
                            </td>
                        </tr>
                        <tr id="trUpdateCancelButtonRow" runat="server">
                            <td align="center" colspan="2" valign="top" style="height: 15px;">
                                <br />
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Images/btnCancel.gif"
                                    ImageAlign="Middle" TabIndex="103" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            Initialize();
        }); // ready fn

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(Initialize);

        function Initialize() {
            $('#<%=gvSupplierDetails.ClientID%>').tablePagination();
            //Quick search 

            $('#<%=txtSearchID.ClientID%>').keyup(function (event) {
                var searchKey = $(this).val().toLowerCase();
                if (searchKey.length > 0) {
                    $("#<%=gvSupplierDetails.ClientID%> tr.row").each(function () {
                        var cellText = $(this).text().toLowerCase();
                        if (cellText.indexOf(searchKey) >= 0) {
                            $(this).show();
                            $("#NoRecords").remove();
                        } //if
                        else {
                            $(this).hide();
                        } // else
                    }); //child fn
                    if ($("#<%=gvSupplierDetails.ClientID%> tbody:first tr:visible").length == 1) {
                        $("#<%=gvSupplierDetails.ClientID%> tbody:first").append('<tr id="NoRecords" rowspan="3" valign="middle" class="tdLabelCenter"><td colspan="10" class="tdNoRecords"><h3> No matching records found </h3></td></tr>');
                    }
                }
                else if (searchKey.length == 0) {
                    $("#NoRecords").remove();
                    $('#<%=gvSupplierDetails.ClientID%>').tablePagination();
                }
            }); // event fn

        }
    </script>
</asp:Content>

