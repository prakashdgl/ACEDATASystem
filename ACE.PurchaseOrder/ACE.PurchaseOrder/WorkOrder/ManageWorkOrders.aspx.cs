using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Data;
using System.Text;

namespace ACE.PurchaseOrder
{
    public partial class ManageSuppliers : System.Web.UI.Page
    {
        private SupplierDL _currentSupplier;

        protected void Page_Load(object sender, EventArgs e)
        {
            bool bl;
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                base.Page.Form.DefaultButton = this.btnSearch.UniqueID;
                base.Page.Form.DefaultFocus = this.txtSearchID.UniqueID;
                bl = base.IsPostBack;
                if (!bl)
                {
                    Session["CompanyID"] = "11";
                    hfCompanyID.Value = "11";
                    //hfUserID.Value = "19";
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "SupplierName";
                    //this.hfCompanyID.Value = Convert.ToString(base.Session["CompanyID"]);
                    bl = this.hfCompanyID.Value != "";
                    if (!bl)
                    {
                        this.hfCompanyID.Value = "0";
                    }
                    GridViewProperties.AssignGridViewProperties(this.gvSupplierDetails);
                    bl = base.Request.QueryString["SupplierID"] == null ? true : !(base.Request.QueryString["SupplierID"] != "null");
                    if (!bl)
                    {
                        GetSupplierDetails(Convert.ToInt32(base.Request.QueryString["SupplierID"].ToString()), Convert.ToInt32(this.hfCompanyID.Value), true);
                        this.trSupplierDetailView.Visible = true;
                        this.trSupplierDetailTabs.Visible = true;
                        this.trUpdateCancelButtonRow.Visible = true;
                        this.trSearchAndAddRow.Visible = false;
                        this.trGridRow.Visible = false;
                    }
                    else
                    {
                        GetSupplierDetails(0, Convert.ToInt32(this.hfCompanyID.Value), "");
                        this.trSupplierDetailView.Visible = false;
                        this.trSupplierDetailTabs.Visible = false;
                        this.trUpdateCancelButtonRow.Visible = false;
                        this.trSearchAndAddRow.Visible = true;
                        this.trGridRow.Visible = true;
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Server.Transfer("../Supplier/AddEditSupplier.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "btnCreate_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Server.Transfer("~/Supplier/ManageSuppliers.aspx?SupplierID=null");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetSupplierDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
                base.Page.Form.DefaultButton = this.btnSearch.UniqueID;
                base.Page.Form.DefaultFocus = this.txtSearchID.UniqueID;
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "btnSearch_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        //To add the identifier of row
        protected void gvSupplierDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "row";
            }
        }


        protected void gvSupplierDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((this.gvSupplierDetails.PageIndex * this.gvSupplierDetails.PageSize) + e.Row.RowIndex) + 1).ToString();
                    //string str = (e.Row.Cells[1].Controls[0] as LinkButton).Text;
                    //str = str.Replace("\'", "\\\'");
                    //(e.Row.FindControl("ibtnDeleteSupplier") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    ////(e.Row.FindControl("ibtnDeleteSupplier") as ImageButton).Visible = false;
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvSupplierDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                base.ViewState["SortDirection"] = base.ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
                base.ViewState["SortExpression"] = e.SortExpression.ToString();
                GetSupplierDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_Sorting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvSupplierDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                base.Server.Transfer(string.Concat("../Supplier/AddEditSupplier.aspx?SupplierID=", Convert.ToInt32(this.gvSupplierDetails.DataKeys[e.NewEditIndex].Value).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvSupplierDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
               // Response.Redirect("../Modules/PurchaseEntryTabsItem.aspx?SupplierID=" + Convert.ToInt32(this.gvSupplierDetails.DataKeys[e.RowIndex].Value).ToString());
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    Supplier Supplier = new Supplier(Convert.ToInt32(this.gvSupplierDetails.DataKeys[e.RowIndex].Value), Convert.ToInt32(this.hfCompanyID.Value), false);
            //    Supplier.ScreenMode = ScreenMode.Delete;
            //    TransactionResult transactionResult = Supplier.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    if (transactionResult.Status == TransactionStatus.Success)
            //    {
            //        GetSupplierDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_RowDeleting", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvSupplierDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = (int)this.gvSupplierDetails.SelectedValue;
                GetSupplierDetails(i, Convert.ToInt32(this.hfCompanyID.Value), true);
                this.trSupplierDetailView.Visible = true;
                this.trSupplierDetailTabs.Visible = true;
                this.trUpdateCancelButtonRow.Visible = true;
                this.trSearchAndAddRow.Visible = false;
                this.trGridRow.Visible = false;
                base.ViewState["SupplierID"] = i;
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvSupplierDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSupplierDetails.PageIndex = e.NewPageIndex;
                GetSupplierDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "gvSupplierDetails_PageIndexChanging", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetSupplierDetails(int SupplierID, int companyID, string searchText)
        {
            try
            {
                SupplierDL Supplier = new SupplierDL();
            //    DataView dataView = Supplier.GetSupplierDetails(SupplierID, companyID, searchText).Tables[0].DefaultView;
            //    dataView.Sort = string.Concat(base.ViewState["SortExpression"].ToString(), " ", base.ViewState["SortDirection"].ToString());

                DataTable ds = Supplier.GetSupplierDetails(SupplierID, companyID, searchText).Tables[0];
                if (ds.Rows.Count == 0)
                {
                    ds.Rows.Add(ds.NewRow());
                    gvSupplierDetails.DataSource = ds;
                    gvSupplierDetails.DataBind();
                    int columncount = gvSupplierDetails.Rows[0].Cells.Count;
                    gvSupplierDetails.Rows[0].Cells.Clear();
                    gvSupplierDetails.Rows[0].Cells.Add(new TableCell());
                    gvSupplierDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvSupplierDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
                    gvSupplierDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
                    gvSupplierDetails.Rows[0].Cells[0].Text = "Currently there are no entries to display";
                }
                else
                {
                    gvSupplierDetails.DataSource = ds;
                    gvSupplierDetails.DataBind();
                }
                
                //gvSupplierDetails.DataSource = dataView;
                //gvSupplierDetails.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "GetSupplierDetails(int SupplierID, string searchText)", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetSupplierDetails(int SupplierID, int companyID, bool isProperties)
        {
            try
            {
                this._currentSupplier = new SupplierDL(SupplierID, companyID, isProperties);
                AssignValues();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "GetSupplierDetails(int SupplierID, bool isProperties)", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void AssignValues()
        {
            try
            {
                this.lblSalesPersonNameValue.Text = Common.CheckBlank(this._currentSupplier.SalesPersonName);
                this.lblSupplierNameValue.Text = Common.CheckBlank(this._currentSupplier.SupplierName);
                this.lblSupplierCompanyNameValue.Text = Common.CheckBlank(this._currentSupplier.SupplierCompanyName);
                this.lblAddressValue.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.Address1);
                this.lblAddress2Value.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.Address2);
                this.lblCountryValue.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.CountryName);
                this.lblStateValue.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.StateName);
                this.lblCityValue.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.CityDescription);
                this.lblZipCodeValue.Text = Common.CheckBlank(this._currentSupplier.SupplierAddress.PostalCode);
                this.lblEmailIDValue.Text = Common.CheckBlank(this._currentSupplier.HomeEmail);
                this.lblSecondEmailIDValue.Text = Common.CheckBlank(this._currentSupplier.WorkEmail);
                this.lblHomePhoneValue.Text = Common.CheckBlank(this._currentSupplier.HomePhone);
                this.lblWorkPhoneValue.Text = Common.CheckBlank(this._currentSupplier.WorkPhone);
                this.lblMobilePhoneValue.Text = Common.CheckBlank(this._currentSupplier.MobilePhone);
                this.lblCommentsValue.Text = Common.CheckBlank(this._currentSupplier.Comments);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageSuppliers.aspx", "", "AssignValues", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
    }
}