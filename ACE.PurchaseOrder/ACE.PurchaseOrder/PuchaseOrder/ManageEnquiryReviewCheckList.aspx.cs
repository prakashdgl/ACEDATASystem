using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ACE.PurchaseOrder.PuchaseOrder
{
    public partial class ManageEnquiryReviewCheckList : System.Web.UI.Page
    {
        private EnquiryReviewChecklistDL _currentEnquiryReviewChecklist = new EnquiryReviewChecklistDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "EnquiryReviewChecklist";
                    GridViewProperties.AssignGridViewProperties(gvEnquiryReviewChecklist);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvEnquiryReviewChecklist.Width = Unit.Percentage(97);
                    //LoadMaterialDropDown();
                    GetEnquiryReviewChecklistDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Response.Redirect("~/Default.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnEnquiryReviewChecklistAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditEnquiryReviewCheckList.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvEnquiryReviewChecklist.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }


            //try
            //{
            //    _currentEnquiryReviewChecklist = new EnquiryReviewChecklistDL();
            //    bool bl = lblEnquiryReviewChecklistID.Text.ToString() == "0";
            //    _currentEnquiryReviewChecklist.AddEditOption = !bl ? 1 : 0;
            //    _currentEnquiryReviewChecklist.EnquiryReviewChecklistID = Convert.ToInt32(lblEnquiryReviewChecklistID.Text.ToString());
            //    _currentEnquiryReviewChecklist.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);
            //    _currentEnquiryReviewChecklist.AuditID = Convert.ToInt32(hfUserID.Value);
            //    _currentEnquiryReviewChecklist.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
            //    _currentEnquiryReviewChecklist.ScreenMode = ScreenMode.Add;
            //    TransactionResult transactionResult = _currentEnquiryReviewChecklist.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    bl = transactionResult.Status != TransactionStatus.Success;
            //    if (!bl)
            //    {
            //        GetEnquiryReviewChecklistDetails();
            //        txtAvailablecount.Text = "0";
            //        lblEnquiryReviewChecklistID.Text = "0";
            //    }
            //    else
            //    {
            //        txtAvailablecount.Text = "0";
            //        lblEnquiryReviewChecklistID.Text = "0";
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void btnEnquiryReviewChecklistCancel_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    mpeEdit.Hide();
            //    GetEnquiryReviewChecklistDetails();
            //    txtAvailablecount.Text = "0";
            //    lblEnquiryReviewChecklistID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void lnkAddEnquiryReviewChecklist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditEnquiryReviewCheckList.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvEnquiryReviewChecklist.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }
        protected void gvEnquiryReviewChecklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvEnquiryReviewChecklist.PageIndex * gvEnquiryReviewChecklist.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteEnquiryReviewChecklist") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteEnquiryReviewChecklist") as ImageButton).Visible = false;
                }
                else
                {
                    bl = e.Row.RowType != DataControlRowType.Header;
                    if (!bl)
                    {
                        e.Row.Cells[1].Visible = false;
                        e.Row.Cells[2].Visible = false;
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "gvEnquiryReviewChecklist_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvEnquiryReviewChecklist_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                base.Server.Transfer(string.Concat("../PuchaseOrder/AddEditEnquiryReviewCheckList.aspx?EnquiryReviewChecklistID=", Convert.ToInt32(this.gvEnquiryReviewChecklist.DataKeys[e.NewEditIndex].Value).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "gvEnquiryReviewChecklist_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    lblEnquiryReviewChecklistID.Text = Convert.ToString(gvEnquiryReviewChecklist.DataKeys[e.NewEditIndex].Value);
            //    txtAvailablecount.Text = gvEnquiryReviewChecklist.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            //    mpeEdit.Show();
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "gvEnquiryReviewChecklist_RowEditing", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvEnquiryReviewChecklist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentEnquiryReviewChecklist = new EnquiryReviewChecklistDL();
                _currentEnquiryReviewChecklist.EnquiryReviewCheckListID = Convert.ToInt32(gvEnquiryReviewChecklist.DataKeys[e.RowIndex].Value);
                _currentEnquiryReviewChecklist.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentEnquiryReviewChecklist.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetEnquiryReviewChecklistDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "gvEnquiryReviewChecklist_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetEnquiryReviewChecklistDetails()
        {
            try
            {
                gvEnquiryReviewChecklist.DataSource = _currentEnquiryReviewChecklist.GetEnquiryReviewCheckList().Tables[0];
                gvEnquiryReviewChecklist.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "GetEnquiryReviewChecklistDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        ///// <summary>
        ///// Load the Material Drop Down List
        ///// </summary>
        //private void LoadCustomerDropDown()
        //{
        //    try
        //    {
        //        // Load Customer
                
        //        ddlCustomer.Items.Clear();
        //        ddlCustomer.DataSource = new ContactDL().GetContactList(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
        //        ddlCustomer.DataTextField = "MaterialName";
        //        ddlCustomer.DataValueField = "MaterialID";
        //        ddlCustomer.DataBind();
        //        ddlCustomer.Items.Insert(0, "--Select One--");
        //        ddlCustomer.Items[0].Value = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "LoadRoleDropDown", ex.Message.ToString(), new ACEConnection());
        //    }
        //}
    }
}