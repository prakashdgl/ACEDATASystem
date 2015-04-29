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
    public partial class ManageStockMaintanence : System.Web.UI.Page
    {
        private StockDL _currentStock = new StockDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    //base.ViewState["SortDirection"] = "ASC";
                    //base.ViewState["SortExpression"] = "Stock";
                    GridViewProperties.AssignGridViewProperties(gvStock);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvStock.Width = Unit.Percentage(97);
                    LoadMaterialDropDown();
                    GetStockDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnStockAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentStock = new StockDL();
                bool bl = lblStockID.Text.ToString() == "0";
                _currentStock.AddEditOption = !bl ? 1 : 0;
                _currentStock.StockID = Convert.ToInt32(lblStockID.Text.ToString());
                _currentStock.MaterialID = Convert.ToInt32(ddlMaterial.SelectedValue);
                _currentStock.AuditID = Convert.ToInt32(hfUserID.Value);
                _currentStock.AvailableCount = Convert.ToInt32(txtAvailablecount.Text);
                _currentStock.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentStock.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    GetStockDetails();
                    txtAvailablecount.Text = "0";
                    lblStockID.Text = "0";
                }
                else
                {
                    txtAvailablecount.Text = "0";
                    lblStockID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "btnStockAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnStockCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                GetStockDetails();
                txtAvailablecount.Text = "0";
                lblStockID.Text = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "btnStockCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void lnkAddStock_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtAvailablecount.Text = "0";
                lblStockID.Text = "";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "lnkAddStock_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvStock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvStock.PageIndex * gvStock.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteStock") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteStock") as ImageButton).Visible = false;
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
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "gvStock_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvStock_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                lblStockID.Text = Convert.ToString(gvStock.DataKeys[e.NewEditIndex].Value);
                txtAvailablecount.Text = gvStock.Rows[e.NewEditIndex].Cells[4].Text.ToString();
                mpeEdit.Show();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "gvStock_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvStock_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentStock = new StockDL();
                _currentStock.StockID = Convert.ToInt32(gvStock.DataKeys[e.RowIndex].Value);
                _currentStock.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentStock.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetStockDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "gvStock_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetStockDetails()
        {
            try
            {
                gvStock.DataSource = _currentStock.GetStockList().Tables[0];
                gvStock.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageStock.aspx", "", "GetStockDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
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