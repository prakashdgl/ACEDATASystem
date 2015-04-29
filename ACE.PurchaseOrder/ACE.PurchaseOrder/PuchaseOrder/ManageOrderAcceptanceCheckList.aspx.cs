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
    public partial class ManageOrderAcceptanceCheckList : System.Web.UI.Page
    {
        private OrderAcceptanceChecklistDL _currentOrderAcceptanceChecklist = new OrderAcceptanceChecklistDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "OrderAcceptanceChecklist";
                    GridViewProperties.AssignGridViewProperties(gvOrderAcceptanceChecklist);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvOrderAcceptanceChecklist.Width = Unit.Percentage(97);
                    //LoadMaterialDropDown();
                    GetOrderAcceptanceChecklistDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnOrderAcceptanceChecklistAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditOrderAcceptanceCheckList.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvOrderAcceptanceChecklist.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }


            //try
            //{
            //    _currentOrderAcceptanceChecklist = new OrderAcceptanceChecklistDL();
            //    bool bl = lblOrderAcceptanceChecklistID.Text.ToString() == "0";
            //    _currentOrderAcceptanceChecklist.AddEditOption = !bl ? 1 : 0;
            //    _currentOrderAcceptanceChecklist.OrderAcceptanceChecklistID = Convert.ToInt32(lblOrderAcceptanceChecklistID.Text.ToString());
            //    _currentOrderAcceptanceChecklist.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);
            //    _currentOrderAcceptanceChecklist.AuditID = Convert.ToInt32(hfUserID.Value);
            //    _currentOrderAcceptanceChecklist.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
            //    _currentOrderAcceptanceChecklist.ScreenMode = ScreenMode.Add;
            //    TransactionResult transactionResult = _currentOrderAcceptanceChecklist.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    bl = transactionResult.Status != TransactionStatus.Success;
            //    if (!bl)
            //    {
            //        GetOrderAcceptanceChecklistDetails();
            //        txtAvailablecount.Text = "0";
            //        lblOrderAcceptanceChecklistID.Text = "0";
            //    }
            //    else
            //    {
            //        txtAvailablecount.Text = "0";
            //        lblOrderAcceptanceChecklistID.Text = "0";
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void btnOrderAcceptanceChecklistCancel_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    mpeEdit.Hide();
            //    GetOrderAcceptanceChecklistDetails();
            //    txtAvailablecount.Text = "0";
            //    lblOrderAcceptanceChecklistID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceChecklistCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void lnkAddOrderAcceptanceChecklist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditOrderAcceptanceCheckList.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvOrderAcceptanceChecklist.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }
        protected void gvOrderAcceptanceChecklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvOrderAcceptanceChecklist.PageIndex * gvOrderAcceptanceChecklist.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteOrderAcceptanceChecklist") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteOrderAcceptanceChecklist") as ImageButton).Visible = false;
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
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "gvOrderAcceptanceChecklist_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvOrderAcceptanceChecklist_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                base.Server.Transfer(string.Concat("../PuchaseOrder/AddEditOrderAcceptanceCheckList.aspx?OrderAcceptanceChecklistID=", Convert.ToInt32(this.gvOrderAcceptanceChecklist.DataKeys[e.NewEditIndex].Value).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "gvOrderAcceptanceChecklist_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    lblOrderAcceptanceChecklistID.Text = Convert.ToString(gvOrderAcceptanceChecklist.DataKeys[e.NewEditIndex].Value);
            //    txtAvailablecount.Text = gvOrderAcceptanceChecklist.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            //    mpeEdit.Show();
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "gvOrderAcceptanceChecklist_RowEditing", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvOrderAcceptanceChecklist_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentOrderAcceptanceChecklist = new OrderAcceptanceChecklistDL();
                _currentOrderAcceptanceChecklist.OrderAcceptanceCheckListID = Convert.ToInt32(gvOrderAcceptanceChecklist.DataKeys[e.RowIndex].Value);
                _currentOrderAcceptanceChecklist.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentOrderAcceptanceChecklist.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetOrderAcceptanceChecklistDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "gvOrderAcceptanceChecklist_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetOrderAcceptanceChecklistDetails()
        {
            try
            {
                gvOrderAcceptanceChecklist.DataSource = _currentOrderAcceptanceChecklist.GetOrderAcceptanceCheckList().Tables[0];
                gvOrderAcceptanceChecklist.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "GetOrderAcceptanceChecklistDetails", exception1.Message.ToString(), new ACEConnection());
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