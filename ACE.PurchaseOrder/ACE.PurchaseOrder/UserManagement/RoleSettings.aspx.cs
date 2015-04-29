using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
    public partial class RoleSettings : System.Web.UI.Page
    {
        RoleSettingsDL _roleSettings = new RoleSettingsDL();

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            TemplateField field = new TemplateField();
            field.HeaderText = "Select All";
            
            field.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            
            field.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
            gvRole.Columns.Add(field);
            field.ItemTemplate = new CommandCheckBoxTemplate();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridViewProperties.AssignGridViewProperties(gvRole);
                LoadDropDownList();
            }
        }

        private void LoadDropDownList()
        {
            ddlRole.DataSource = _roleSettings.GetRolesList();
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "--Select One--");
            ddlRole.Items[0].Value = "";
        }

        protected void gvRole_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text = ((gvRole.PageIndex * gvRole.PageSize) + e.Row.RowIndex + 1).ToString();

                    e.Row.Cells[1].Visible = false;
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[1].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("RoleSettings.aspx", "", "gvRole_RowDataBound", ex.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRole.SelectedValue != "" && ddlRole.SelectedValue != null)
                _roleSettings.RoleID = Convert.ToInt32(ddlRole.SelectedValue);

            gvRole.DataSource = _roleSettings.GetRoleXPageList();
            gvRole.DataBind();
        }

        protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CheckOrUncheckAll")
            {
                Control source = e.CommandSource as Control;
                GridViewRow row = source.NamingContainer as GridViewRow;
                string dispStr = "Row " + (row.RowIndex + 1).ToString() + "=" + e.CommandArgument;
                CheckBox cbSampleBox = (CheckBox)row.FindControl("cbCheckAll");
                CheckBox cbAddEdit = (CheckBox)row.FindControl("cbAdd");
                CheckBox cbDelete = (CheckBox)row.FindControl("cbDelete");
                if (cbSampleBox.Checked == true)
                {
                    cbAddEdit.Checked = true;
                    cbDelete.Checked = true;
                }
                else
                {
                    cbAddEdit.Checked = false;
                    cbDelete.Checked = false;
                }
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            
            try
            {
                // Adding RolesXPages
                TransactionResult result = null;
                CheckBox chkIsAddEdit;
                CheckBox chkIsDelete;
                foreach (GridViewRow rowItem in gvRole.Rows)
                {
                    _roleSettings.RoleID = Convert.ToInt32(ddlRole.SelectedValue.ToString());
                    _roleSettings.PageID = Convert.ToInt32(rowItem.Cells[1].Text.ToString());
                    chkIsAddEdit = (CheckBox)(rowItem.Cells[4].FindControl("cbAdd"));
                    _roleSettings.IsAddorEdit = chkIsAddEdit.Checked;
                    chkIsDelete = (CheckBox)(rowItem.Cells[5].FindControl("cbDelete"));
                    _roleSettings.IsDelete = chkIsDelete.Checked;

                    // Add / Edit the RolesXPages
                    _roleSettings.ScreenMode = ScreenMode.Add;
                    result = _roleSettings.Commit();
                    // Display the Status - Whether successfully saved or not
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script>alert('" + result.Message.ToString() + ".');");
                    sb.Append("</script>");
                    ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

                
                }
                //If successful, get the RolesXPages details
                ddlRole_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("RoleSettings.aspx", "", "btnSave_Click", ex.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}
