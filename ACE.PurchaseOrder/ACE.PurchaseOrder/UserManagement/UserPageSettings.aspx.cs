using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class UserPageSettings : System.Web.UI.Page
  {

    #region Private Variables
    UsersDL _users = new UsersDL();
    UsersXPagesDL _usersXPages = new UsersXPagesDL();
    CompanyDL _company = new CompanyDL();
    TemplateField _fieldSelectAll = new TemplateField();
    TemplateField _fieldAdd = new TemplateField();
    TemplateField _fieldDelete = new TemplateField();
    string _selectedModule;
    int _userID = 0;
    #endregion

    #region Private Event(s)

    /// <summary>
    /// OnInit
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
    }

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        if (!IsPostBack)
        {
          int companyID = 0;
          int userID = 0;

          //if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
          if (SessionControl.CompanyID != 0)
          {
            //companyID = Convert.ToInt32(Session["CompanyID"]);
            //userID = Convert.ToInt32(Session["UserID"]);


            companyID = SessionControl.CompanyID.cxToInt32();
            userID = SessionControl.UserID.cxToInt32();

            //UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(userID, companyID, CommonDL.GetPageID(), true);
            //if (!_usersXPagesDL.IsAddorEdit)
            //{
            //  Response.Redirect("~/Login.aspx", false);
            //}
          }
          else
          {
            Response.Redirect("~/Login.aspx", false);
          }
         // gvUserPageSettings.Visible = false;
          GridViewProperties.AssignGridViewProperties(gvUserPageSettings);
          LoadDropDownList();
          gvUserPageSettings.AllowPaging = false;
         // btnSave.Visible = false;
         // btnCancel.Visible = false;
          int CompanyID = 0;
          //CompanyID = Convert.ToInt32(Session["CompanyID"]);
          //_userID = Convert.ToInt32(Session["UserID"]);

          CompanyID = SessionControl.CompanyID.cxToInt32();
          _userID = SessionControl.UserID.cxToInt32();
        //  ddlCompany.Visible = false;
          ddlCompany.SelectedValue = Convert.ToString(CompanyID);
          ddlCompany_SelectedIndexChanged(sender, e);
        }
        else
        {
          //_userID = Convert.ToInt32(Session["UserID"]);
          _userID = SessionControl.UserID.cxToInt32();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ddlCompany_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        gvUserPageSettings.Visible = true;
        if (ddlCompany.SelectedValue != "" && ddlCompany.SelectedValue != null)
        {
          ddlEmployee.DataSource = _usersXPages.GetUsersListByCompanyID(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), _userID).Tables[0];
          ddlEmployee.DataTextField = "EmployeeName";
          ddlEmployee.DataValueField = "UserID";
          ddlEmployee.DataBind();
          ddlEmployee.Items.Insert(0, "--Select One--");
          ddlEmployee.Items[0].Value = "";

          gvUserPageSettings.DataSource = null;
          gvUserPageSettings.DataBind();
        }
        else
        {
          ddlEmployee.Items.Clear();
          ddlEmployee.Items.Insert(0, "--Select One--");
          ddlEmployee.Items[0].Value = "";
          gvUserPageSettings.DataSource = null;
          gvUserPageSettings.DataBind();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "ddlCompany_SelectedIndexChanged", ex.Message, new ACEConnection());
      }

    }

    /// <summary>
    /// ddlEmployee_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        if (ddlEmployee.SelectedValue != "" && ddlEmployee.SelectedValue != null)
        {
          int CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
          int UserID = Convert.ToInt32(ddlEmployee.SelectedValue);
          _users = new UsersDL();
          _users.GetUserRole(UserID, CompanyID);
          lblRoleDescription.Text = "Role : ";
          lblRoleName.Text = _users.RoleName;
          _usersXPages.UserID = Convert.ToInt32(ddlEmployee.SelectedValue);
          gvUserPageSettings.DataSource = _usersXPages.GetUsersPagesList(UserID, CompanyID).Tables[0].DefaultView;
          gvUserPageSettings.DataBind();
          btnSave.Visible = true;
          btnCancel.Visible = true;
        }
        else
        {
          gvUserPageSettings.DataSource = null;
          gvUserPageSettings.DataBind();
          btnSave.Visible = false;
          btnCancel.Visible = false;
          lblRoleDescription.Text = "";
          lblRoleName.Text = "";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "ddlEmployee_SelectedIndexChanged", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserPageSettings_RowCreated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserPageSettings_RowCreated(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          ObjectDataSource ctrl = e.Row.FindControl("odsUserPageSettings") as ObjectDataSource;

          if (ctrl != null && e.Row.DataItem != null)
          {
            if ((_selectedModule != null) && (_selectedModule != ""))
              CheckAllState(_selectedModule);

            ctrl.SelectParameters["UserID"].DefaultValue = ddlEmployee.SelectedValue.ToString();
            ctrl.SelectParameters["CompanyID"].DefaultValue = ddlCompany.SelectedValue.ToString();
            ctrl.SelectParameters["ModuleName"].DefaultValue = ((DataRowView)e.Row.DataItem)["ModuleName"].ToString();

            _selectedModule = ((DataRowView)e.Row.DataItem)["ModuleName"].ToString();
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "gvUserPageSettings_RowCreated", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvUpdateUserPageSettings_RowCommand
    /// </summary>
    /// <param name="s"></param>
    /// <param name="e"></param>
    protected void gvUpdateUserPageSettings_RowCommand(object s, GridViewCommandEventArgs e)
    {
      try
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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "gvUpdateUserPageSettings_RowCommand", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvUpdateUserPageSettings_Init
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUpdateUserPageSettings_Init(Object sender, EventArgs e)
    {
      try
      {
        //Template Field Add
        _fieldAdd = new TemplateField();

        _fieldAdd.HeaderText = "Add & Edit";
        _fieldAdd.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        _fieldAdd.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
        _fieldAdd.ItemStyle.BackColor = System.Drawing.Color.White;
        _fieldAdd.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

        ((GridView)sender).Columns.Add(_fieldAdd);
        _fieldAdd.ItemTemplate = new CommandCheckBoxTemplateAdd();

        //Template Field Delete

        _fieldDelete = new TemplateField();

        _fieldDelete.HeaderText = "Delete";
        _fieldDelete.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        _fieldDelete.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
        _fieldDelete.ItemStyle.BackColor = System.Drawing.Color.White;
        _fieldDelete.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ((GridView)sender).Columns.Add(_fieldDelete);
        _fieldDelete.ItemTemplate = new CommandCheckBoxTemplateDelete();

        //Template Field Select All

        _fieldSelectAll = new TemplateField();

        _fieldSelectAll.HeaderText = "Select All";
        _fieldSelectAll.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
        _fieldSelectAll.HeaderStyle.BackColor = System.Drawing.Color.FromName("#CDDCF1");
        _fieldSelectAll.ItemStyle.BackColor = System.Drawing.Color.White;
        _fieldSelectAll.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        ((GridView)sender).Columns.Add(_fieldSelectAll);
        _fieldSelectAll.ItemTemplate = new CommandCheckBoxTemplate();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "gvUpdateUserPageSettings_Init", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvUpdateUserPageSettings_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUpdateUserPageSettings_RowDataBound(object sender, GridViewRowEventArgs e)
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
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "gvUpdateUserPageSettings_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }

    }

    /// <summary>
    /// btnCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/UserManagement/UserPageSettings.aspx");
    }

    /// <summary>
    /// btnSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Adding RolesXPages
        TransactionResult result;
        CheckBox chkIsAddEdit;
        CheckBox chkIsDelete;
        _users = new UsersDL();
        foreach (GridViewRow outerRow in gvUserPageSettings.Rows)
        {
          GridView outerGrid = (GridView)outerRow.FindControl("gvUpdateUserPageSettings");
          foreach (GridViewRow innerRow in outerGrid.Rows)
          {
            _usersXPages = new UsersXPagesDL();

            _usersXPages.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue.ToString());
            _usersXPages.UserID = Convert.ToInt32(ddlEmployee.SelectedValue.ToString());
            _usersXPages.PageID = Convert.ToInt32(innerRow.Cells[1].Text.ToString());
            chkIsAddEdit = (CheckBox)(innerRow.Cells[6].FindControl("cbCheckBoxAdd"));
            _usersXPages.IsAddorEdit = chkIsAddEdit.Checked;
            chkIsDelete = (CheckBox)(innerRow.Cells[7].FindControl("cbCheckBoxDelete"));
            _usersXPages.IsDelete = chkIsDelete.Checked;
            _usersXPages.ScreenMode = ScreenMode.Add;
            _users.UserPages.Add(_usersXPages);
            // Add / Edit the UsersXPages
          }
        }
        //If successful, get the RolesXPages details
        _users.ScreenMode = ScreenMode.Edit;
        result = _users.Commit();
        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        ddlEmployee_SelectedIndexChanged(sender, e);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "btnSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// cbSelectModule_CheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cbSelectModule_CheckedChanged(object sender, EventArgs e)
    {
      try
      {
        CheckBox chkIsAddEdit;
        CheckBox chkIsDelete;
        CheckBox chkSelectAll;
        CheckBox selectedCheckbox;
        selectedCheckbox = (CheckBox)sender;
        foreach (GridViewRow outerRow in gvUserPageSettings.Rows)
        {
          if (selectedCheckbox.Checked == true)
          {
            GridView outerGrid = (GridView)selectedCheckbox.FindControl("gvUpdateUserPageSettings");
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
            GridView outerGrid = (GridView)selectedCheckbox.FindControl("gvUpdateUserPageSettings");
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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "cbSelectModule_CheckedChanged", ex.Message, new ACEConnection());
      }
    }

    #endregion

    #region Private Method(s)

    /// <summary>
    /// LoadDropDownList
    /// </summary>
    private void LoadDropDownList()
    {
      try
      {
        ddlCompany.DataSource = _company.GetCompanyList().Tables[0];
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "--Select One--");
        ddlCompany.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "LoadDropDownList", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// CheckAllState
    /// </summary>
    /// <param name="Module"></param>
    private void CheckAllState(string Module)
    {
      try
      {
        CheckBox chkIsAddEdit;
        CheckBox chkIsDelete;
        CheckBox outerSelectModule;
        foreach (GridViewRow outerRow in gvUserPageSettings.Rows)
        {
          outerSelectModule = (CheckBox)outerRow.FindControl("cbSelectModule");
          HiddenField outerModuleName = (HiddenField)outerRow.FindControl("hfModuleName");
          if (outerModuleName.Value.ToString() == Module)
          {
            bool hasFalse = false;
            GridView outerGrid = (GridView)outerRow.FindControl("gvUpdateUserPageSettings");
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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "CheckAllState(string Module)", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// CheckAllState
    /// </summary>
    /// <param name="Module"></param>
    /// <param name="Checked"></param>
    private void CheckAllState(string Module, bool Checked)
    {
      try
      {
        CheckBox outerSelectModule;
        foreach (GridViewRow outerRow in gvUserPageSettings.Rows)
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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("UserPageSettings.aspx", "", "CheckAllState(string Module, bool Checked)", ex.Message, new ACEConnection());
      }
    }

    #endregion
  }
}
