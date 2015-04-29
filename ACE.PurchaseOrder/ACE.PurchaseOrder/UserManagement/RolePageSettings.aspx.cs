using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class RolePageSettings : System.Web.UI.Page
  {
    #region Private Variables

    RolesDL _roles = new RolesDL();
    RolesXPagesDL _rolesXPages = new RolesXPagesDL();
    TemplateField _fieldSelectAll = new TemplateField();
    TemplateField _fieldAdd = new TemplateField();
    TemplateField _fieldDelete = new TemplateField();
    string _selectedModule;
    #endregion

    #region Private Event(s)

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!IsPostBack)
      {
        int companyID = 0;
        int userID = 0;
        //if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
        if(SessionControl.CompanyID!=0)
        {
          //companyID = Convert.ToInt32(Session["CompanyID"]);
          //userID = Convert.ToInt32(Session["UserID"]);

          companyID = SessionControl.CompanyID.cxToInt32();
          userID = SessionControl.UserID.cxToInt32();

          UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(userID, companyID, CommonDL.GetPageID(), true);
          if (_usersXPagesDL.IsAddorEdit)
          {

          }
          else
          {
            Response.Redirect("~/Login.aspx", false);
          }
        }
        else
        {
          Response.Redirect("~/Login.aspx", false);
        }
        GridViewProperties.AssignGridViewProperties(gvRoleSettings);
        LoadDropDownList();
        gvRoleSettings.AllowPaging = false;
        btnSave.Visible = false;
        btnCancel.Visible = false;
      }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (ddlRole.SelectedValue != "" && ddlRole.SelectedValue != null)
      {
        _rolesXPages.RoleID = Convert.ToInt32(ddlRole.SelectedValue);

        gvRoleSettings.DataSource = _rolesXPages.GetRolesPagesList(Convert.ToInt32(ddlRole.SelectedValue)).Tables[0].DefaultView;
        gvRoleSettings.DataBind();
        btnSave.Visible = true;
        btnCancel.Visible = true;
      }
      else
      {
        gvRoleSettings.DataSource = null;
        gvRoleSettings.DataBind();
        btnSave.Visible = false;
        btnCancel.Visible = false;
      }
    }

    protected void gvRoleSettings_RowCreated(object sender, GridViewRowEventArgs e)
    {

      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        ObjectDataSource ctrl = e.Row.FindControl("odsRoleSettings") as ObjectDataSource;

        if (ctrl != null && e.Row.DataItem != null)
        {
          if ((_selectedModule != null) && (_selectedModule != ""))
            CheckAllState(_selectedModule);

          ctrl.SelectParameters["RoleID"].DefaultValue = ddlRole.SelectedValue.ToString();
          ctrl.SelectParameters["ModuleName"].DefaultValue = ((DataRowView)e.Row.DataItem)["ModuleName"].ToString();

          _selectedModule = ((DataRowView)e.Row.DataItem)["ModuleName"].ToString();
        }

      }
    }

    protected void gvUpdateRoleSettings_RowCommand(object s, GridViewCommandEventArgs e)
    {
      if (e.CommandName == "CheckOrUncheckAll")
      {
        Control source = e.CommandSource as Control;
        GridViewRow row = source.NamingContainer as GridViewRow;
        string dispStr = "Row " + (row.RowIndex + 1).ToString() + "=" + e.CommandArgument;
        CheckBox cbSampleBox = (CheckBox)row.FindControl("cbCheckAll");
        CheckBox cbAddEdit = (CheckBox)row.FindControl("cbCheckBoxAdd");
        CheckBox cbDelete = (CheckBox)row.FindControl("cbCheckBoxDelete");
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

      if (e.CommandName == "CheckBoxAdd")
      {
        Control source = e.CommandSource as Control;
        GridViewRow row = source.NamingContainer as GridViewRow;
        string dispStr = "Row " + (row.RowIndex + 1).ToString() + "=" + e.CommandArgument;
        CheckBox cbSampleBox = (CheckBox)row.FindControl("cbCheckAll");
        CheckBox cbAddEdit = (CheckBox)row.FindControl("cbCheckBoxAdd");
        CheckBox cbDelete = (CheckBox)row.FindControl("cbCheckBoxDelete");
        if (cbAddEdit.Checked == true && cbDelete.Checked == true)
        {
          cbSampleBox.Checked = true;
        }
        else
        {
          cbSampleBox.Checked = false;
        }
      }

      if (e.CommandName == "CheckBoxDelete")
      {
        Control source = e.CommandSource as Control;
        GridViewRow row = source.NamingContainer as GridViewRow;
        string dispStr = "Row " + (row.RowIndex + 1).ToString() + "=" + e.CommandArgument;
        CheckBox cbSampleBox = (CheckBox)row.FindControl("cbCheckAll");
        CheckBox cbAddEdit = (CheckBox)row.FindControl("cbCheckBoxAdd");
        CheckBox cbDelete = (CheckBox)row.FindControl("cbCheckBoxDelete");
        if (cbAddEdit.Checked == true && cbDelete.Checked == true)
        {
          cbSampleBox.Checked = true;
        }
        else
        {
          cbSampleBox.Checked = false;
        }
      }
    }

    protected void gvUpdateRoleSettings_Init(Object sender, EventArgs e)
    {

      //Template Field Add

      _fieldAdd = new TemplateField();

      _fieldAdd.HeaderText = "Add & Edit";
      _fieldAdd.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
      _fieldAdd.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
      _fieldAdd.ItemStyle.BackColor = System.Drawing.Color.White;
      _fieldAdd.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

      ((GridView)sender).Columns.Add(_fieldAdd);
      _fieldAdd.ItemTemplate = new CommandCheckBoxTemplateAdd();

      //Template Field Delete

      _fieldDelete = new TemplateField();
      _fieldDelete.ItemStyle.BackColor = System.Drawing.Color.White;
      _fieldDelete.HeaderText = "Delete";
      _fieldDelete.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
      _fieldDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

      _fieldDelete.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
      ((GridView)sender).Columns.Add(_fieldDelete);
      _fieldDelete.ItemTemplate = new CommandCheckBoxTemplateDelete();

      //Template Field Select All
      _fieldSelectAll = new TemplateField();
      _fieldSelectAll.ItemStyle.BackColor = System.Drawing.Color.White;
      _fieldSelectAll.HeaderText = "Select All";
      _fieldSelectAll.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
      _fieldSelectAll.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

      _fieldSelectAll.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
      ((GridView)sender).Columns.Add(_fieldSelectAll);
      _fieldSelectAll.ItemTemplate = new CommandCheckBoxTemplate();

    }

    protected void gvUpdateRoleSettings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
          e.Row.Cells[5].Visible = false;
          CheckBox cbSampleBox = (CheckBox)e.Row.FindControl("cbCheckAll");
          CheckBox cbAddEdit = (CheckBox)e.Row.FindControl("cbCheckBoxAdd");
          CheckBox cbDelete = (CheckBox)e.Row.FindControl("cbCheckBoxDelete");
          if (Convert.ToBoolean(e.Row.Cells[4].Text.ToString()) == true && Convert.ToBoolean(e.Row.Cells[5].Text.ToString()) == true)
          {
            cbAddEdit.Checked = true;
            cbDelete.Checked = true;
            cbSampleBox.Checked = true;
          }
          else
          {
            cbAddEdit.Checked = false;
            cbDelete.Checked = false;
            cbSampleBox.Checked = false;
          }
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
          e.Row.Cells[5].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("RoleSettings.aspx", "", "gvRole_RowDataBound", ex.Message.ToString(), new ACEConnection());
        throw;
      }

    }

    private void CheckAllState(string Module)
    {
      CheckBox chkIsAddEdit;
      CheckBox chkIsDelete;
      CheckBox outerSelectModule;
      foreach (GridViewRow outerRow in gvRoleSettings.Rows)
      {
        outerSelectModule = (CheckBox)outerRow.FindControl("cbSelectModule");
        HiddenField outerModuleName = (HiddenField)outerRow.FindControl("hfModuleName");
        if (outerModuleName.Value.ToString() == Module)
        {
          bool hasFalse = false;
          GridView outerGrid = (GridView)outerRow.FindControl("gvUpdateRoleSettings");
          foreach (GridViewRow innerRow in outerGrid.Rows)
          {

            chkIsAddEdit = (CheckBox)(innerRow.Cells[6].FindControl("cbCheckBoxAdd"));
            chkIsDelete = (CheckBox)(innerRow.Cells[7].FindControl("cbCheckBoxDelete"));
            if (chkIsAddEdit.Checked == false || chkIsDelete.Checked == false)
            {
              hasFalse = true;
              break;
            }
          }
          if (hasFalse)
            outerSelectModule.Checked = false;
          else
            outerSelectModule.Checked = true;
        }
      }
    }

    private void CheckAllState(string Module, bool Checked)
    {
      CheckBox outerSelectModule;
      foreach (GridViewRow outerRow in gvRoleSettings.Rows)
      {
        outerSelectModule = (CheckBox)outerRow.Cells[0].FindControl("cbSelectModule");
        HiddenField outerModuleName = (HiddenField)outerRow.FindControl("hfModuleName");
        if (outerModuleName.Value.ToString() == Module)
        {
          //outerSelectModule.AutoPostBack = false;
          string str = outerSelectModule.UniqueID.ToString();
          outerSelectModule.Checked = false;
        }

      }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Adding RolesXPages
        TransactionResult result;
        CheckBox chkIsAddEdit;
        CheckBox chkIsDelete;
        foreach (GridViewRow outerRow in gvRoleSettings.Rows)
        {
          GridView outerGrid = (GridView)outerRow.FindControl("gvUpdateRoleSettings");
          foreach (GridViewRow innerRow in outerGrid.Rows)
          {
            _rolesXPages = new RolesXPagesDL();

            _rolesXPages.RoleID = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            _rolesXPages.PageID = Convert.ToInt32(innerRow.Cells[1].Text.ToString());
            chkIsAddEdit = (CheckBox)(innerRow.Cells[6].FindControl("cbCheckBoxAdd"));
            _rolesXPages.IsAddorEdit = chkIsAddEdit.Checked;
            chkIsDelete = (CheckBox)(innerRow.Cells[7].FindControl("cbCheckBoxDelete"));
            _rolesXPages.IsDelete = chkIsDelete.Checked;
            _rolesXPages.ScreenMode = ScreenMode.Add;
            _roles.RolePages.Add(_rolesXPages);
            // Add / Edit the RolesXPages
          }
        }
        //If successful, get the RolesXPages details
        _roles.ScreenMode = ScreenMode.Edit;
        result = _roles.Commit();
        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        ddlRole_SelectedIndexChanged(sender, e);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("RoleSettings.aspx", "", "btnSave_Click", ex.Message.ToString(), new ACEConnection());
        throw;
      }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/UserManagement/RolePageSettings.aspx", false);
    }

    protected void cbSelectModule_CheckedChanged(object sender, EventArgs e)
    {
      CheckBox chkIsAddEdit;
      CheckBox chkIsDelete;
      CheckBox chkSelectAll;
      CheckBox selectedCheckbox;
      selectedCheckbox = (CheckBox)sender;
      foreach (GridViewRow outerRow in gvRoleSettings.Rows)
      {
        if (selectedCheckbox.Checked == true)
        {
          GridView outerGrid = (GridView)selectedCheckbox.FindControl("gvUpdateRoleSettings");
          foreach (GridViewRow innerRow in outerGrid.Rows)
          {
            chkIsAddEdit = (CheckBox)(innerRow.Cells[4].FindControl("cbCheckBoxAdd"));
            chkIsDelete = (CheckBox)(innerRow.Cells[5].FindControl("cbCheckBoxDelete"));
            chkSelectAll = (CheckBox)(innerRow.Cells[6].FindControl("cbCheckAll"));
            chkIsAddEdit.Checked = true;
            chkIsDelete.Checked = true;
            chkSelectAll.Checked = true;
          }
        }
        else
        {
          GridView outerGrid = (GridView)selectedCheckbox.FindControl("gvUpdateRoleSettings");
          foreach (GridViewRow innerRow in outerGrid.Rows)
          {
            chkIsAddEdit = (CheckBox)(innerRow.Cells[4].FindControl("cbCheckBoxAdd"));
            chkIsDelete = (CheckBox)(innerRow.Cells[5].FindControl("cbCheckBoxDelete"));
            chkSelectAll = (CheckBox)(innerRow.Cells[6].FindControl("cbCheckAll"));

            chkIsAddEdit.Checked = false;
            chkIsDelete.Checked = false;
            chkSelectAll.Checked = false;
          }

        }
      }
    }

    #endregion

    #region Private Method(s)
    /// <summary>
    /// BindGrid
    /// </summary>
    /// <param name="Grid"></param>
    /// <param name="str"></param>
    private void BindGrid(GridView Grid, string str)
    {
      Grid.DataSource = _rolesXPages.GetRolesPagesListByModules(Convert.ToInt32(ddlRole.SelectedValue), str);
      Grid.DataBind();
    }

    /// <summary>
    /// LoadDropDownList
    /// </summary>
    private void LoadDropDownList()
    {
      ddlRole.DataSource = _roles.GetRolesList();
      ddlRole.DataTextField = "RoleName";
      ddlRole.DataValueField = "RoleID";
      ddlRole.DataBind();
      ddlRole.Items.Insert(0, "--Select One--");
      ddlRole.Items[0].Value = "";
    }

    #endregion

  }
}
