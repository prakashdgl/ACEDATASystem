using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Text;

namespace ACE.PurchaseOrder
{
  public partial class ManageEmployee : System.Web.UI.Page
  {
    #region Private Variables

    // The Employee instance for viewing
    EmployeeDL _currentEmployee;
    CompanyDL _company = new CompanyDL();

    int _companyID = 0;
    int _userID = 0;
    string _dateFormat;

    #endregion

    #region Page Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        //Commentedd on 17/04/2014
        //_companyID = Convert.ToInt32(Session["CompanyID"]);
        //_userID = Convert.ToInt32(Session["UserID"]);
        //_dateFormat = Convert.ToString(Session["DateFormat"]);

        _companyID = SessionControl.CompanyID.cxToInt32();
        _userID = SessionControl.UserID.cxToInt32();
        _dateFormat = SessionControl.DateFormat.ToString();

        if (_dateFormat == null || _dateFormat == "")
        {
          Response.Redirect("~/Login.aspx", false);
        }

        ceDOB.Format = _dateFormat;
        ceDOJ.Format = _dateFormat;
        if (!IsPostBack)
        {
          //commented on 17/04/2014
          //if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
          if (_companyID != 0)
          {
            UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(_userID, _companyID, CommonDL.GetPageID(), true);
            if (!_usersXPagesDL.IsAddorEdit)
            {
              Response.Redirect("~/Login.aspx", false);
            }

          }
          else
          {
            Response.Redirect("~/Login.aspx", false);
          }


          // Set ViewState Variables
          ViewState["SortDirection"] = "ASC";
          ViewState["SortExpression"] = "EmployeeCode";

          // Page.Form.DefaultButton = btnSearch.UniqueID;
          Page.Form.DefaultFocus = txtSearchID.UniqueID;

          // Assign Common GridView Properties to all GridViews used in the page
          GridViewProperties.AssignGridViewProperties(gvEmployeeDetails);
          LoadDropDownList();
          GetEmployeeDetails("", Convert.ToBoolean(rblReportOption.SelectedValue));
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Events - Page, Button
    /// <summary>
    /// btnSearch_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        // Get Employee Details based on the Search Text entered
        GetEmployeeDetails(Convert.ToString(txtSearchID.Text.ToString()), Convert.ToBoolean(rblReportOption.SelectedValue));
        Page.Form.DefaultFocus = txtSearchID.UniqueID;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "btnSearch_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// rblReportOption_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblReportOption_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        GetEmployeeDetails(Convert.ToString(txtSearchID.Text.ToString()), Convert.ToBoolean(rblReportOption.SelectedValue));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "rblReportOption_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region List of Employees Grid
    /// <summary>
    /// gvEmployeeDetails_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeDetails.PageIndex * gvEmployeeDetails.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          //ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteEmployee");
          //lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          ImageButton btnEditEmployee = (ImageButton)e.Row.FindControl("ibtnEditEmployee");
          btnEditEmployee.Attributes.Add("OnClick", "repagination();");


          // Display the Dates in dd/MM/yyyy format
          DateTime dTime;
          if (e.Row.Cells[4].Text != "&nbsp;")
          {
            dTime = Convert.ToDateTime(e.Row.Cells[4].Text.ToString());
            e.Row.Cells[4].Text = dTime.ToString(_dateFormat);
          }

          if (e.Row.Cells[5].Text != "")
          {
            string phoneStr = e.Row.Cells[5].Text;

            if (phoneStr.Length > 10)
            {
              string str1 = phoneStr.Remove(0, phoneStr.Length - 10);
              string str2 = str1.Remove(0, 5);
              string str3 = str1.Remove(5, 5);
              string str4 = str3 + " " + str2;
              e.Row.Cells[5].Text = str4;
            }
            if (phoneStr.Length == 10)
            {
              string str5 = e.Row.Cells[5].Text;
              string str6 = str5.Remove(0, 5);
              string str7 = str5.Remove(5, 5);
              string str8 = str7 + " " + str6;
              e.Row.Cells[5].Text = str8;
            }

          }
          // Hide Columns
          e.Row.Cells[3].Visible = false;
          Image imgProfileCount = (Image)e.Row.FindControl("imgEmployeeProfileCount");
          EmployeeDL employeeDL = new EmployeeDL();
          imgProfileCount.Width = Unit.Percentage(Convert.ToInt32(Convert.ToString(employeeDL.GetEmployeeProfileCount(Convert.ToInt32(e.Row.Cells[3].Text)))));
          imgProfileCount.AlternateText = imgProfileCount.Width.ToString();
          imgProfileCount.Visible = false;
          Label lblProfileCount = (Label)e.Row.FindControl("lblEmployeeProfileCount");
          lblProfileCount.Text = imgProfileCount.Width.ToString();

        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[3].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeDetails_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        txtSearchID.Text = "";
        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        // Get Employee Details
        GetEmployeeDetails(Convert.ToString(txtSearchID.Text), Convert.ToBoolean(rblReportOption.SelectedValue));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeDetails_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Get the selected row's employee id
        int employeeIDToEdit = Convert.ToInt32(gvEmployeeDetails.DataKeys[e.NewEditIndex].Value);
        txtSearchID.Text = "";
        // Transfer Control to Add / Edit Employee Form with the employee id as querystring 
        _currentEmployee = new EmployeeDL(employeeIDToEdit, true);

        txtEmployeeID.Text = _currentEmployee.EmployeeID.ToString(); ;

        txtEmployeeCode.Text = _currentEmployee.EmployeeCode;
        txtEmployeeInitial.Text = _currentEmployee.Initial;
        txtEmployeeName.Text = _currentEmployee.FName;

        if (_currentEmployee.GenderID != 0)
        {
          ddlGender.SelectedValue = Convert.ToString(_currentEmployee.GenderID);
        }
        DateTime dTime;
        dTime = Convert.ToDateTime(_currentEmployee.Dob);
        txtDOB.Text = Common.CheckBlank(Convert.ToString(dTime.ToString(_dateFormat)));
        dTime =  Convert.ToDateTime(_currentEmployee.Doj);
        txtDOJ.Text = Common.CheckBlank(Convert.ToString(dTime.ToString(_dateFormat)));
        if (_currentEmployee.DepartmentID != 0)
        {
          ddlDepartment.SelectedValue = Convert.ToString(_currentEmployee.DepartmentID);
        }
        trRole.Visible = false;
        txtEmployeeCode.Enabled = false;

        txtOfficeMailID.Text = _currentEmployee.OfficeEmailID;
        lblPopupHeading.Text = "Edit Employee";
        mpeManageEmployee.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }


    /// <summary>
    /// gvEmployeeDetails_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        // Get the details of the selected employee
        int selEmployeeID = (int)gvEmployeeDetails.SelectedValue;

        Response.Redirect("~/Employee/EmployeeProfile.aspx?EmployeeID=" + selEmployeeID.ToString(), false);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
      }
    }


    //To add the identifier of row
    /// <summary>
    /// gvEmployeeDetails_RowCreated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          e.Row.CssClass = "row";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_RowCreated", ex.Message.ToString(), new ACEConnection());
      }

    }

    /// <summary>
    /// gvEmployeeDetails_PreRender
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDetails_PreRender(object sender, EventArgs e)
    {
      try
      {
        gvEmployeeDetails.UseAccessibleHeader = true;
        gvEmployeeDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "gvEmployeeDetails_PreRender", ex.Message.ToString(), new ACEConnection());
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
        if (ddlCompany.SelectedValue != null && ddlCompany.SelectedValue != "")
        {
          int companyID = Convert.ToInt32(ddlCompany.SelectedValue.ToString());
          ddlDepartment.Items.Clear();
          ddlDepartment.DataSource = new DepartmentDL().GetDepartmentListByCompanyID(companyID).Tables[0];
          ddlDepartment.DataTextField = "DepartmentDescription";
          ddlDepartment.DataValueField = "DepartmentID";
          ddlDepartment.DataBind();
          ddlDepartment.Items.Insert(0, "--Select One--");
          ddlDepartment.Items[0].Value = "";
        }
        else
        {
          ddlDepartment.Items.Clear();
          ddlDepartment.DataSource = null;
          ddlDepartment.DataBind();
          ddlDepartment.Items.Insert(0, "--Select One--");
          ddlDepartment.Items[0].Value = "";
        }
        mpeManageEmployee.Show();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "ddlCompany_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnManageEmployeeSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnManageEmployeeSave_Click(object sender,EventArgs e)
    {
      try
      {
        if (!FormValidation()) { return; }

        string dtFormat = _dateFormat;
        DateTime dTime;

        // Create a new Employee Object
        _currentEmployee = new EmployeeDL();

        // Set whether Add / Edit
        if (txtEmployeeID.Text.ToString() != "0")
        {
          _currentEmployee = new EmployeeDL(Convert.ToInt32(txtEmployeeID.Text), true);
          _currentEmployee.EmployeeAddresses.Clear();
          _currentEmployee.AddEditOption = 1;
        }
        else
        {
          _currentEmployee.AddEditOption = 0;
          _currentEmployee.RoleID = Convert.ToInt32(ddlRole.SelectedValue.ToString());
        }

        _currentEmployee.EmployeeID = Convert.ToInt32(txtEmployeeID.Text);

        // Assign Values to the Company Object
        _currentEmployee.CompanyID = _companyID;

        _currentEmployee.EmployeeCode = txtEmployeeCode.Text;

        _currentEmployee.Initial = txtEmployeeInitial.Text;

        _currentEmployee.FName = txtEmployeeName.Text;

        _currentEmployee.GenderID = Convert.ToInt32(ddlGender.SelectedValue.ToString());

        _currentEmployee.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue.ToString());



        dTime = DateTime.ParseExact(txtDOB.Text, _dateFormat, null);
        _currentEmployee.Dob = dTime;
        dTime = DateTime.ParseExact(txtDOJ.Text, _dateFormat, null);
        _currentEmployee.Doj = dTime;

        _currentEmployee.OfficeEmailID = txtOfficeMailID.Text;

        _currentEmployee.CreatorUserID = _userID;
        //
        _currentEmployee.ModifierUserID = _userID;

        DataSet dsEmployee = _currentEmployee.GetEmployeeListByEmployeeCode(_currentEmployee.EmployeeCode, _currentEmployee.CompanyID);
        if ((dsEmployee.Tables[0].Rows.Count > 0) && (_currentEmployee.AddEditOption == 0))
        {
          lblMessage.Text = "EmployeeCode already exists.";
          mpeManageEmployee.Show();
          return;
        }
        // Add / Edit the Company
        TransactionResult result;
        _currentEmployee.ScreenMode = ScreenMode.Add;
        result = _currentEmployee.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved Company
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails("", Convert.ToBoolean(rblReportOption.SelectedValue));
          ClearText();
          lblPopupHeading.Text = "Add Employee";
        }
        trRole.Visible = true;
        txtEmployeeCode.Enabled = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "ibtnManageEmployeeSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnManageEmployeeCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnManageEmployeeCancel_Click(object sender, EventArgs e)
    {
      try
      {
        ClearText();
        lblPopupHeading.Text = "Add Employee";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "ibtnManageEmployeeCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// To get the list of available employees (in the current selected company)
    /// </summary>        
    /// <param name="searchText">The text to search</param>
    private void GetEmployeeDetails(string searchText, bool activatedDeactivated)
    {
      try
      {
        // Get the list of employees
        EmployeeDL employeeDetails = new EmployeeDL();

        DataView dView = employeeDetails.GetEmployeeByCompanyID(_companyID, searchText, activatedDeactivated).Tables[0].DefaultView;
        dView.Sort = ViewState["SortExpression"].ToString() + " " + ViewState["SortDirection"].ToString();
        if (dView.Table.Rows.Count == 0)
        {
          dView.Table.Rows.Add(dView.Table.NewRow());
          gvEmployeeDetails.DataSource = dView;
          gvEmployeeDetails.DataBind();
          int columncount = gvEmployeeDetails.Rows[0].Cells.Count;
          gvEmployeeDetails.Rows[0].Cells.Clear();
          gvEmployeeDetails.Rows[0].Cells.Add(new TableCell());
          gvEmployeeDetails.Rows[0].Cells[0].ColumnSpan = columncount;
          gvEmployeeDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
          gvEmployeeDetails.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
          gvEmployeeDetails.Rows[0].Cells[0].Text = "Currently there are no entries to display";
        }
        else
        {
          gvEmployeeDetails.DataSource = dView;
          gvEmployeeDetails.DataBind();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ManageEmployee.aspx", "", "GetEmployeeDetails(string searchText)", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// FormValidation
    /// </summary>
    /// <returns></returns>
    private bool FormValidation()
    {
      try
      {
        bool rValue = true;
        string dtFormat = _dateFormat;
        DateTime dTime = new DateTime();
        DateTime todayDate = new DateTime();
        DateTime dJTime = new DateTime();
        string sTodayDate;

        // Whether correct date format
        if (!Common.ValidateDate(txtDOB.Text.ToString(), dtFormat))
        {
          txtDOB.Focus();
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Incorrect Date - Date Of Birth" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          rValue = false;
          mpeManageEmployee.Show();
        }
        else if (!Common.ValidateDate(txtDOJ.Text.ToString(), dtFormat))
        {
          txtDOJ.Focus();
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Incorrect Date - Date Of Joining" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          rValue = false;
          mpeManageEmployee.Show();
        }
        else if (rValue)
        {
          sTodayDate = Convert.ToString(DateTime.Now.ToString(_dateFormat));
          dTime = DateTime.ParseExact(txtDOB.Text, dtFormat, null);
          todayDate = DateTime.ParseExact(sTodayDate, dtFormat, null);
          dJTime = DateTime.ParseExact(txtDOJ.Text, dtFormat, null);
          if (dTime > todayDate)
          {
            txtDOB.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Date Of Birth - Cannot Be Greater Than Today Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            mpeManageEmployee.Show();
          }
          else if (dTime > dJTime)
          {
            txtDOJ.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Date Of Joining - Cannot Be Greater Than Birth Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            mpeManageEmployee.Show();
          }
        }

        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "FamilyPopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }

    /// <summary>
    /// LoadDropDownList
    /// </summary>
    private void LoadDropDownList()
    {
      int userID = _userID;
      int companyID = _companyID;
      if (companyID != 0)
      {
        trCompany.Visible = false;

        ddlCompany.Items.Clear();
        ddlCompany.DataSource = _company.GetCompanyListByUserID(userID).Tables[0];
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();

        companyID = _companyID;
        ddlCompany.SelectedValue = companyID.ToString();

        ddlDepartment.Items.Clear();
        ddlDepartment.DataSource = new DepartmentDL().GetDepartmentListByCompanyID(companyID).Tables[0];
        ddlDepartment.DataTextField = "DepartmentDescription";
        ddlDepartment.DataValueField = "DepartmentID";
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, "--Select One--");
        ddlDepartment.Items[0].Value = "";
      }

      ddlGender.Items.Clear();
      ddlGender.DataSource = new GenderDL().GetGenderList().Tables[0];
      ddlGender.DataTextField = "GenderDescription";
      ddlGender.DataValueField = "GenderID";
      ddlGender.DataBind();
      ddlGender.Items.Insert(0, "--Select One--");
      ddlGender.Items[0].Value = "";

      RolesDL _roles = new RolesDL();
      ddlRole.Items.Clear();
      ddlRole.DataSource = _roles.GetRolesListByLevel(_userID, _companyID);
      ddlRole.DataTextField = "RoleName";
      ddlRole.DataValueField = "RoleID";
      ddlRole.DataBind();
      ddlRole.Items.Insert(0, "--Select One--");
      ddlRole.Items[0].Value = "";
    }

    /// <summary>
    /// ClearText
    /// </summary>
    private void ClearText()
    {
      trRole.Visible = true;
      txtEmployeeCode.Enabled = true;
      txtEmployeeCode.Text = "";
      txtEmployeeInitial.Text = "";
      txtEmployeeName.Text = "";
      txtOfficeMailID.Text = "";
      txtDOB.Text = "";
      txtDOJ.Text = "";
      txtEmployeeID.Text = "0";
      ddlDepartment.SelectedIndex = 0;
      ddlGender.SelectedIndex = 0;
      ddlRole.SelectedIndex = 0;
      mpeManageEmployee.Hide();
      txtSearchID.Text = "";
    }


    #endregion
  }
}
