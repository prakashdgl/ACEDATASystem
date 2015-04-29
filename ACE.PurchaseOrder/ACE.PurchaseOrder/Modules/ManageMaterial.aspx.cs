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
    public partial class ManageMaterial : System.Web.UI.Page
    {
        private MaterialDL _currentMaterial = new MaterialDL();

        protected void Page_Load(object sender, EventArgs e)
        {
        
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    base.ViewState["SortDirection"] = "ASC";
                    base.ViewState["SortExpression"] = "Material";
                    GridViewProperties.AssignGridViewProperties(gvMaterial);
                    gvMaterial.Width = Unit.Percentage(97);
                    GetMaterialDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnMaterialAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentMaterial = new MaterialDL();
                bool bl = txtMaterialID.Text.ToString() == "0";
                _currentMaterial.AddEditOption = !bl ? 1 : 0;
                _currentMaterial.MaterialID = Convert.ToInt32(txtMaterialID.Text.ToString());
                _currentMaterial.MaterialDescription = txtMaterial.Text.ToString();
                _currentMaterial.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentMaterial.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    GetMaterialDetails();
                    txtMaterial.Text = "";
                    txtMaterialID.Text = "0";
                }
                else
                {
                    txtMaterial.Text = "";
                    txtMaterialID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "btnMaterialAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnMaterialCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                GetMaterialDetails();
                txtMaterial.Text = "";
                txtMaterialID.Text = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "btnMaterialCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void lnkAddMaterial_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtMaterialID.Text = "0";
                txtMaterial.Text = "";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "lnkAddMaterial_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((gvMaterial.PageIndex * gvMaterial.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteMaterial") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteMaterial") as ImageButton).Visible = false;
                }
                else
                {
                    bl = e.Row.RowType != DataControlRowType.Header;
                    if (!bl)
                    {
                        e.Row.Cells[1].Visible = false;
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "gvMaterial_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvMaterial_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                txtMaterialID.Text = Convert.ToString(gvMaterial.DataKeys[e.NewEditIndex].Value);
                txtMaterial.Text = gvMaterial.Rows[e.NewEditIndex].Cells[2].Text.ToString();
                mpeEdit.Show();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "gvMaterial_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvMaterial_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                _currentMaterial = new MaterialDL();
                _currentMaterial.MaterialID = Convert.ToInt32(gvMaterial.DataKeys[e.RowIndex].Value);
                _currentMaterial.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = _currentMaterial.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetMaterialDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "gvMaterial_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetMaterialDetails()
        {
            try
            {
                gvMaterial.DataSource = _currentMaterial.GetMaterialList().Tables[0];
                gvMaterial.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageMaterial.aspx", "", "GetMaterialDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
    }
}