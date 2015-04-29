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
    public partial class ManageCustomers : System.Web.UI.Page
    {
        private ContactDL _currentContact;

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
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "ContactName";
                    this.hfCompanyID.Value = Convert.ToString(base.Session["CompanyID"]);
                    bl = this.hfCompanyID.Value != "";
                    if (!bl)
                    {
                        this.hfCompanyID.Value = "0";
                    }
                    GridViewProperties.AssignGridViewProperties(this.gvContactDetails);
                    bl = base.Request.QueryString["ContactID"] == null ? true : !(base.Request.QueryString["ContactID"] != "null");
                    if (!bl)
                    {
                        GetContactDetails(Convert.ToInt32(base.Request.QueryString["ContactID"].ToString()), Convert.ToInt32(this.hfCompanyID.Value), true);
                        this.trContactDetailView.Visible = true;
                        this.trContactDetailTabs.Visible = true;
                        this.trUpdateCancelButtonRow.Visible = true;
                        this.trSearchAndAddRow.Visible = false;
                        this.trGridRow.Visible = false;
                    }
                    else
                    {
                        GetContactDetails(0, Convert.ToInt32(this.hfCompanyID.Value), "");
                        this.trContactDetailView.Visible = false;
                        this.trContactDetailTabs.Visible = false;
                        this.trUpdateCancelButtonRow.Visible = false;
                        this.trSearchAndAddRow.Visible = true;
                        this.trGridRow.Visible = true;
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void btnCreate_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Server.Transfer("../Modules/AddEditCustomer.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "btnCreate_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Server.Transfer("~/Modules/ManageCustomers.aspx?ContactID=null");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                GetContactDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
                base.Page.Form.DefaultButton = this.btnSearch.UniqueID;
                base.Page.Form.DefaultFocus = this.txtSearchID.UniqueID;
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "btnSearch_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        //To add the identifier of row
        protected void gvContactDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "row";
            }
        }


        protected void gvContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((this.gvContactDetails.PageIndex * this.gvContactDetails.PageSize) + e.Row.RowIndex) + 1).ToString();
                    //string str = (e.Row.Cells[1].Controls[0] as LinkButton).Text;
                    //str = str.Replace("\'", "\\\'");
                    //(e.Row.FindControl("ibtnDeleteContact") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    ////(e.Row.FindControl("ibtnDeleteContact") as ImageButton).Visible = false;
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvContactDetails_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                base.ViewState["SortDirection"] = base.ViewState["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
                base.ViewState["SortExpression"] = e.SortExpression.ToString();
                GetContactDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_Sorting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvContactDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                base.Server.Transfer(string.Concat("../Modules/AddEditCustomer.aspx?ContactID=", Convert.ToInt32(this.gvContactDetails.DataKeys[e.NewEditIndex].Value).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Response.Redirect("../Modules/PurchaseEntryTabsItem.aspx?ContactID=" + Convert.ToInt32(this.gvContactDetails.DataKeys[e.RowIndex].Value).ToString());
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    Contact contact = new Contact(Convert.ToInt32(this.gvContactDetails.DataKeys[e.RowIndex].Value), Convert.ToInt32(this.hfCompanyID.Value), false);
            //    contact.ScreenMode = ScreenMode.Delete;
            //    TransactionResult transactionResult = contact.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    if (transactionResult.Status == TransactionStatus.Success)
            //    {
            //        GetContactDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_RowDeleting", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvContactDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = (int)this.gvContactDetails.SelectedValue;
                GetContactDetails(i, Convert.ToInt32(this.hfCompanyID.Value), true);
                this.trContactDetailView.Visible = true;
                this.trContactDetailTabs.Visible = true;
                this.trUpdateCancelButtonRow.Visible = true;
                this.trSearchAndAddRow.Visible = false;
                this.trGridRow.Visible = false;
                base.ViewState["ContactID"] = i;
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvContactDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvContactDetails.PageIndex = e.NewPageIndex;
                GetContactDetails(0, Convert.ToInt32(this.hfCompanyID.Value), this.txtSearchID.Text);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "gvContactDetails_PageIndexChanging", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetContactDetails(int contactID, int companyID, string searchText)
        {
            try
            {
                ContactDL contact = new ContactDL();
            //    DataView dataView = contact.GetContactDetails(contactID, companyID, searchText).Tables[0].DefaultView;
            //    dataView.Sort = string.Concat(base.ViewState["SortExpression"].ToString(), " ", base.ViewState["SortDirection"].ToString());

                DataTable ds = contact.GetContactDetails(contactID, companyID, searchText).Tables[0];
                if (ds.Rows.Count == 0)
                {
                    ds.Rows.Add(ds.NewRow());
                    gvContactDetails.DataSource = ds;
                    gvContactDetails.DataBind();
                    int columncount = gvContactDetails.Rows[0].Cells.Count;
                    gvContactDetails.Rows[0].Cells.Clear();
                    gvContactDetails.Rows[0].Cells.Add(new TableCell());
                    gvContactDetails.Rows[0].Cells[0].ColumnSpan = columncount;
                    gvContactDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
                    gvContactDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
                    gvContactDetails.Rows[0].Cells[0].Text = "Currently there are no entries to display";
                }
                else
                {
                    gvContactDetails.DataSource = ds;
                    gvContactDetails.DataBind();
                }
                
                //gvContactDetails.DataSource = dataView;
                //gvContactDetails.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "GetContactDetails(int contactID, string searchText)", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetContactDetails(int contactID, int companyID, bool isProperties)
        {
            try
            {
                this._currentContact = new ContactDL(contactID, companyID, isProperties);
                AssignValues();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "GetContactDetails(int contactID, bool isProperties)", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void AssignValues()
        {
            try
            {
                this.lblLastNameValue.Text = Common.CheckBlank(this._currentContact.LName);
                this.lblFirstNameValue.Text = Common.CheckBlank(this._currentContact.FName);
                this.lblAddressValue.Text = Common.CheckBlank(this._currentContact.ContactAddress.Address1);
                this.lblAddress2Value.Text = Common.CheckBlank(this._currentContact.ContactAddress.Address2);
                this.lblCountryValue.Text = Common.CheckBlank(this._currentContact.ContactAddress.CountryName);
                this.lblStateValue.Text = Common.CheckBlank(this._currentContact.ContactAddress.StateName);
                if (_currentContact.ContactAddress.CityDescription != null)
                    this.lblCityValue.Text = Common.CheckBlank(this._currentContact.ContactAddress.CityDescription);

                this.lblZipCodeValue.Text = Common.CheckBlank(this._currentContact.ContactAddress.PostalCode);
                this.lblEmailIDValue.Text = Common.CheckBlank(this._currentContact.HomeEmail);
                this.lblSecondEmailIDValue.Text = Common.CheckBlank(this._currentContact.WorkEmail);
                this.lblHomePhoneValue.Text = Common.CheckBlank(this._currentContact.HomePhone);
                this.lblWorkPhoneValue.Text = Common.CheckBlank(this._currentContact.WorkPhone);
                this.lblMobilePhoneValue.Text = Common.CheckBlank(this._currentContact.MobilePhone);
                this.lblCommentsValue.Text = Common.CheckBlank(this._currentContact.Comments);
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageCustomers.aspx", "", "AssignValues", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
    }
}