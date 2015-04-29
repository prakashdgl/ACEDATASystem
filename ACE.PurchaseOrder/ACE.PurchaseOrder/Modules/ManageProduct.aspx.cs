using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACE.PurchaseOrder.Modules
{
    public partial class ManageProduct : System.Web.UI.Page
    {
        private ProductDL _currentProduct = new ProductDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "Product";
                    GridViewProperties.AssignGridViewProperties(gvProduct);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvProduct.Width = Unit.Percentage(97);
                    LoadMaterialDropDown();
                    GetProductDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnProductAdd_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    _currentProduct = new ProductDL();
            //    bool bl = lblProductID.Text.ToString() == "0";
            //    _currentProduct.AddEditOption = !bl ? 1 : 0;
            //    _currentProduct.ProductID = Convert.ToInt32(lblProductID.Text.ToString());
            //    _currentProduct.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);


            //    _currentProduct.AuditID = Convert.ToInt32(hfUserID.Value);
            //    _currentProduct.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
            //    _currentProduct.ScreenMode = ScreenMode.Add;
            //    TransactionResult transactionResult = _currentProduct.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    bl = transactionResult.Status != TransactionStatus.Success;
            //    if (!bl)
            //    {
            //        GetProductDetails();
            //        txtAvailablecount.Text = "0";
            //        lblProductID.Text = "0";
            //    }
            //    else
            //    {
            //        txtAvailablecount.Text = "0";
            //        lblProductID.Text = "0";
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "btnProductAdd_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        protected void btnProductCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                GetProductDetails();
                txtAvailablecount.Text = "0";
                lblProductID.Text = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "btnProductCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void lnkAddProduct_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtAvailablecount.Text = "0";
                lblProductID.Text = "";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "lnkAddProduct_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvProduct.PageIndex * gvProduct.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteProduct") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteProduct") as ImageButton).Visible = false;
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
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "gvProduct_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblProductID.Text = Convert.ToString(gvProduct.DataKeys[e.NewEditIndex].Value);
                txtAvailablecount.Text = gvProduct.Rows[e.NewEditIndex].Cells[4].Text.ToString();
                mpeEdit.Show();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "gvProduct_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //try
            //{
            //    _currentProduct = new ProductDL();
            //    _currentProduct.ProductID = Convert.ToInt32(gvProduct.DataKeys[e.RowIndex].Value);
            //    _currentProduct.ScreenMode = ScreenMode.Delete;
            //    TransactionResult transactionResult = _currentProduct.Commit();
            //    StringBuilder stringBuilder = new StringBuilder();
            //    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
            //    stringBuilder.Append("</script>");
            //    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
            //    if (transactionResult.Status == TransactionStatus.Success)
            //    {
            //        GetProductDetails();
            //    }
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "gvProduct_RowDeleting", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }
        private void GetProductDetails()
        {
            //try
            //{
            //    gvProduct.DataSource = _currentProduct.GetProductList().Tables[0];
            //    gvProduct.DataBind();
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageProduct.aspx", "", "GetProductDetails", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }

        /// <summary>
        /// Load the Material Drop Down List
        /// </summary>
        private void LoadMaterialDropDown()
        {
            try
            {
                // Load Material
                ddlMaterial.Items.Clear();
                ddlMaterial.DataSource = new MaterialDL().GetMaterialList().Tables[0];
                ddlMaterial.DataTextField = "MaterialName";
                ddlMaterial.DataValueField = "MaterialID";
                ddlMaterial.DataBind();
                ddlMaterial.Items.Insert(0, "--Select One--");
                ddlMaterial.Items[0].Value = "";
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "LoadRoleDropDown", ex.Message.ToString(), new ACEConnection());
            }
        }
    }
}