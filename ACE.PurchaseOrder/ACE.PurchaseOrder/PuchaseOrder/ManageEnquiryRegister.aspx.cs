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
    public partial class ManageEnquiryRegister : System.Web.UI.Page
    {
        private EnquiryRegisterDL _currentEnquiryRegisterList = new EnquiryRegisterDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    lnkAddEnquiryRegisterList.Visible = false;
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "EnquiryRegisterList";
                    GridViewProperties.AssignGridViewProperties(gvEnquiryRegister);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvEnquiryRegister.Width = Unit.Percentage(97);
                    //LoadMaterialDropDown();
                    GetEnquiryRegisterListDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnEnquiryRegisterListAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditEnquiryRegister.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvEnquiryRegister.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "btnEnquiryRegisterListAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }


            //try
            //{
            //    _currentEnquiryRegisterList = new EnquiryRegisterListDL();
            //    bool bl = lblEnquiryRegisterListID.Text.ToString() == "0";
            //    _currentEnquiryRegisterList.AddEditOption = !bl ? 1 : 0;
            //    _currentEnquiryRegisterList.EnquiryRegisterListID = Convert.ToInt32(lblEnquiryRegisterListID.Text.ToString());
            //    _currentEnquiryRegisterList.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);
            //    _currentEnquiryRegisterList.AuditID = Convert.ToInt32(hfUserID.Value);
            //    _currentEnquiryRegisterList.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
            //    _currentEnquiryRegisterList.ScreenMode = ScreenMode.Add;
            //    TransactionResult transactionResult = _currentEnquiryRegisterList.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    bl = transactionResult.Status != TransactionStatus.Success;
            //    if (!bl)
            //    {
            //        GetEnquiryRegisterListDetails();
            //        txtAvailablecount.Text = "0";
            //        lblEnquiryRegisterListID.Text = "0";
            //    }
            //    else
            //    {
            //        txtAvailablecount.Text = "0";
            //        lblEnquiryRegisterListID.Text = "0";
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "btnEnquiryRegisterListAdd_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void btnEnquiryRegisterListCancel_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    mpeEdit.Hide();
            //    GetEnquiryRegisterListDetails();
            //    txtAvailablecount.Text = "0";
            //    lblEnquiryRegisterListID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "btnEnquiryRegisterListCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void lnkAddEnquiryRegisterList_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditEnquiryRegister.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvEnquiryRegister.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "btnEnquiryRegisterListAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }
        protected void gvEnquiryRegister_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvEnquiryRegister.PageIndex * gvEnquiryRegister.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteEnquiryRegisterList") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteEnquiryRegisterList") as ImageButton).Visible = false;
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
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "gvEnquiryRegister_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvEnquiryRegister_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                string enquiryRegister = "0";

                if (gvEnquiryRegister.DataKeys[e.NewEditIndex].Value != null)
                    enquiryRegister = gvEnquiryRegister.DataKeys[e.NewEditIndex].Value.ToString();

                if (enquiryRegister == "")
                    enquiryRegister = "0";

                base.Server.Transfer(string.Concat("../PuchaseOrder/AddEditEnquiryRegister.aspx?EnquiryRegisterID=" + Convert.ToInt32(enquiryRegister).ToString() + "&EnquiryReviewCheckListID=", Convert.ToInt32(gvEnquiryRegister.Rows[e.NewEditIndex].Cells[2].Text).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "gvEnquiryRegister_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    lblEnquiryRegisterListID.Text = Convert.ToString(gvEnquiryRegister.DataKeys[e.NewEditIndex].Value);
            //    txtAvailablecount.Text = gvEnquiryRegister.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            //    mpeEdit.Show();
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "gvEnquiryRegister_RowEditing", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvEnquiryRegister_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentEnquiryRegisterList = new EnquiryRegisterDL();
                _currentEnquiryRegisterList.EnquiryRegisterID = Convert.ToInt32(gvEnquiryRegister.DataKeys[e.RowIndex].Value);
                _currentEnquiryRegisterList.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentEnquiryRegisterList.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetEnquiryRegisterListDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "gvEnquiryRegister_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetEnquiryRegisterListDetails()
        {
            try
            {
                gvEnquiryRegister.DataSource = _currentEnquiryRegisterList.GetEnquiryRegisterList().Tables[0];
                gvEnquiryRegister.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryRegister.aspx", "", "GetEnquiryRegisterListDetails", exception1.Message.ToString(), new ACEConnection());
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