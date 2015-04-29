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
    public partial class ManagePurchaseOrder : System.Web.UI.Page
    {
        private PurchaseOrderDL _currentPurchaseOrder = new PurchaseOrderDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "PurchaseOrder";
                    GridViewProperties.AssignGridViewProperties(gvPurchaseOrder);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvPurchaseOrder.Width = Unit.Percentage(97);
                    //LoadMaterialDropDown();
                    GetPurchaseOrderDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnPurchaseOrderAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditPurchaseOrder.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvPurchaseOrder.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }


            //try
            //{
            //    _currentPurchaseOrder = new PurchaseOrderDL();
            //    bool bl = lblPurchaseOrderID.Text.ToString() == "0";
            //    _currentPurchaseOrder.AddEditOption = !bl ? 1 : 0;
            //    _currentPurchaseOrder.PurchaseOrderID = Convert.ToInt32(lblPurchaseOrderID.Text.ToString());
            //    _currentPurchaseOrder.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);
            //    _currentPurchaseOrder.AuditID = Convert.ToInt32(hfUserID.Value);
            //    _currentPurchaseOrder.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
            //    _currentPurchaseOrder.ScreenMode = ScreenMode.Add;
            //    TransactionResult transactionResult = _currentPurchaseOrder.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    bl = transactionResult.Status != TransactionStatus.Success;
            //    if (!bl)
            //    {
            //        GetPurchaseOrderDetails();
            //        txtAvailablecount.Text = "0";
            //        lblPurchaseOrderID.Text = "0";
            //    }
            //    else
            //    {
            //        txtAvailablecount.Text = "0";
            //        lblPurchaseOrderID.Text = "0";
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderAdd_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void btnPurchaseOrderCancel_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    mpeEdit.Hide();
            //    GetPurchaseOrderDetails();
            //    txtAvailablecount.Text = "0";
            //    lblPurchaseOrderID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void lnkAddPurchaseOrder_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/AddEditPurchaseOrder.aspx");
                //?EnquiryID=" + Convert.ToInt32(this.gvPurchaseOrder.DataKeys[e.RowIndex].Value).ToString()
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }
        protected void gvPurchaseOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvPurchaseOrder.PageIndex * gvPurchaseOrder.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeletePurchaseOrder") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeletePurchaseOrder") as ImageButton).Visible = false;
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
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "gvPurchaseOrder_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvPurchaseOrder_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                base.Server.Transfer(string.Concat("../PuchaseOrder/AddEditPurchaseOrder.aspx?PurchaseOrderID=", Convert.ToInt32(this.gvPurchaseOrder.DataKeys[e.NewEditIndex].Value).ToString()));
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "gvPurchaseOrder_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    lblPurchaseOrderID.Text = Convert.ToString(gvPurchaseOrder.DataKeys[e.NewEditIndex].Value);
            //    txtAvailablecount.Text = gvPurchaseOrder.Rows[e.NewEditIndex].Cells[4].Text.ToString();
            //    mpeEdit.Show();
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "gvPurchaseOrder_RowEditing", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void gvPurchaseOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentPurchaseOrder = new PurchaseOrderDL();
                _currentPurchaseOrder.PurchaseOrderID = Convert.ToInt32(gvPurchaseOrder.DataKeys[e.RowIndex].Value);
                _currentPurchaseOrder.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentPurchaseOrder.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetPurchaseOrderDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "gvPurchaseOrder_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetPurchaseOrderDetails()
        {
            try
            {
                gvPurchaseOrder.DataSource = _currentPurchaseOrder.GetPurchaseOrder().Tables[0];
                gvPurchaseOrder.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "GetPurchaseOrderDetails", exception1.Message.ToString(), new ACEConnection());
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