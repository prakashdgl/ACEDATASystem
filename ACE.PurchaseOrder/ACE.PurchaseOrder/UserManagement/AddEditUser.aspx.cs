using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System.Net.Mail;
using System.Text;

namespace ACE.PurchaseOrder
{
  public partial class AddEditUser : System.Web.UI.Page
  {
    #region Private Variables

    // For listings       
    EmployeeDL _employee = new EmployeeDL();
    CompanyDL _company = new CompanyDL();
    int _companyID = 0;
    int _userID = 0;
    // The User instance for adding/editing
    UsersDL _currentUser = new UsersDL();

    MailMessage _msg = new MailMessage();

    #endregion

    #region Events - Page, Button
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
          //if (Session["CompanyID"] == null && Session["CompanyID"].ToString() == "")
          if (SessionControl.CompanyID == 0)
          {
            Response.Redirect("~/Login.aspx", false);
          }

          // Set ViewState Variables
          ViewState["SortDirection"] = "ASC";
          ViewState["SortExpression"] = "UserName";

          // Assign Common GridView Properties to all GridViews used in the page
          GridViewProperties.AssignGridViewProperties(gvUserCompanies);

          // Load the Employee


          // Load the Roles
          LoadRoleDropDown();

          // Hide certain rows
          trUserNameRow.Visible = false;
          trPasswordRow.Visible = false;
          trUserIDRow.Visible = false;


          if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"] != "null")
          {
            // Get the UserID Querystring Value
            int uID = Convert.ToInt32(Request.QueryString["UserID"]);
            if (uID != 0)
            {
              GetUserDetails(uID, true);
              trRoleRow.Visible = false;
            }
          }
          else
          {
            chkValid.Checked = true;
            tcntAllUserTabs.Visible = true;
          }

          // Load several Drop Down Lists needed for the page
          LoadDropDownLists();

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
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
        StringBuilder sbMail = new StringBuilder();

        // Create a new User Object
        _currentUser = new UsersDL();

        // Set whether Add / Edit
        _currentUser.AddEditOption = 1;

        if (chkValid.Checked == true)
        {
          Utilities objPwd = new Utilities();
          // From Address
          _msg.From = new MailAddress("hr@ACE.in");

          // To Address
          _msg.To.Add(new MailAddress(hdfOfficeEmailID.Value.ToString()));
          // Subject
          _msg.Subject = "Welcome to ACE";

          // Body
          sbMail.Append("Dear All,");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("We are happy to welcome you to the ACE family and are delighted to give you a login to Order.");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);
          sbMail.Append("Order is our intranet system, where you can update your personal details, check your ");
          sbMail.Append("attendance, interact with fellow employees, share information, blog etc. To start ");
          sbMail.Append("with, we would like you to post personal information about yourself and keep ");
          sbMail.Append("updating information, as and when required.");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("Kindly login with your employee ID and make sure that you change your password for ");
          sbMail.Append("information security reasons.");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("Please get back to us with your suggestions, comments and feedback.");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("Your login information is given below to access our intranet site,");
          sbMail.Append(Environment.NewLine);

          sbMail.Append("http://ACE.com/Order");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("User ID : " + txtUserName.Text);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("Password : " + objPwd.DecryptText(txtPassword.Text));
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("Thank you,");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          sbMail.Append("HR.");
          sbMail.Append(Environment.NewLine);
          sbMail.Append(Environment.NewLine);

          _msg.Body = sbMail.ToString();

        }

        // Assign Values to the User Object
        _currentUser.UserID = Convert.ToInt32(txtUserID.Text);
        _currentUser.UserName = Convert.ToInt32(txtUserName.Text);
        _currentUser.Password = txtPassword.Text;

        if (lblEmployeeID.Text != "" && lblEmployeeID.Text.ToString() != null)
          _currentUser.EmployeeID = Convert.ToInt32(lblEmployeeID.Text);

        if (chkValid.Checked == true)
          _currentUser.IsValid = true;
        else
          _currentUser.IsValid = false;

        _currentUser.AuditUserID = Convert.ToInt32(Session["UserID"]);

        // Add / Edit the User
        TransactionResult result;
        _currentUser.ScreenMode = ScreenMode.Add;
        result = _currentUser.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved User
        if (result.Status == TransactionStatus.Success)
        {
          if (hdfOfficeEmailID.Value.ToString() != "")
          {
            //to send mail
            if (chkValid.Checked == true)
            {
              if (OrderSettings.SendMail)
              {
                SmtpClient client = new SmtpClient();
                client.Send(_msg);
              }
            }
          }

          lblRole.Visible = false;
          ddlRole.Visible = false;
          GetUserDetails(_currentUser.UserID, true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "btnSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
      try
      {
        // Go to ViewUser.aspx
        Response.Redirect("~/UserManagement/ViewUser.aspx", false);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Companies Tab

    /// <summary>
    /// gvUserCompanies_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvUserCompanies.PageIndex * gvUserCompanies.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDeleteUserCompany");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          // Is Default                    
          CheckBox chkDef = (CheckBox)e.Row.FindControl("IsDefault"); ;
          if (chkDef.Checked == true)
            lnkDelete.Visible = false;

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[3].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[3].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "gvUserCompanies_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserCompanies_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserCompanies_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get User Details
        GetUserDetails(Convert.ToInt32(txtUserID.Text), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        // Sort the list based on the columns 
        if (e.SortExpression.ToString() == "CompanyName")
        {
          //Sort by Company                
          _currentUser.UserCompanies.Sort(new CompanyXUsersComparer_byCompanyName());
        }
        else if (e.SortExpression.ToString() == "RoleName")
        {
          //Sort by Role                
          _currentUser.UserCompanies.Sort(new CompanyXUsersComparer_byRoleName());
        }

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentUser.UserCompanies.Reverse();

        // Assign the list of User Companies after sorting to the grid 
        gvUserCompanies.DataSource = _currentUser.UserCompanies;
        gvUserCompanies.DataBind();

        // Set the Active Tab
        tcntAllUserTabs.ActiveTab = tpnlCompanies;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "gvUserCompanies_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserCompanies_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserCompanies_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Active Tab
        tcntAllUserTabs.ActiveTab = tpnlCompanies;

        // Set the Control Values in the Popup from the Selected Grid Row

        txtCompanyID.Text = gvUserCompanies.Rows[e.NewEditIndex].Cells[1].Text;

        ddlCompany.ClearSelection();
        ddlCompany.Items.Insert(0, gvUserCompanies.Rows[e.NewEditIndex].Cells[2].Text.ToString());
        ddlCompany.Items[0].Value = gvUserCompanies.Rows[e.NewEditIndex].Cells[1].Text.ToString();
        ddlCompany.SelectedValue = gvUserCompanies.Rows[e.NewEditIndex].Cells[1].Text.ToString();

        ddlCompany.Enabled = false;

        ddlCompanyRole.ClearSelection();
        ddlCompanyRole.SelectedValue = gvUserCompanies.Rows[e.NewEditIndex].Cells[3].Text.ToString();

        // Is Default                
        CheckBox chkDef = (CheckBox)gvUserCompanies.Rows[e.NewEditIndex].FindControl("IsDefault"); ;
        if (chkDef.Checked == true)
        {
          chkDefault.Checked = true;
          chkDefault.Enabled = false;
        }
        else
        {
          chkDefault.Checked = false;
          chkDefault.Enabled = true;
        }
        lblPopupHeading.Text = "Edit User Company";
        btnUserCompanyAdd.Text = "Edit";
        // Show the Popup
        mpeUserCompany.Show();
        mpeUserCompany.Focus();
        e.Cancel = true;

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "gvUserCompanies_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserCompanies_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserCompanies_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's company id 
        int companyIDToDelete = Convert.ToInt32(gvUserCompanies.DataKeys[e.RowIndex].Value);
        int userIDToDelete = Convert.ToInt32(txtUserID.Text);

        // Delete the selected user company
        CompanyXUsersDL deleteUserCompany = new CompanyXUsersDL();
        deleteUserCompany.UserID = userIDToDelete;
        deleteUserCompany.CompanyID = companyIDToDelete;
        deleteUserCompany.ScreenMode = ScreenMode.Delete;
        deleteUserCompany.AddEditOption = 2; // Added on 20/06/2014 
        result = deleteUserCompany.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, get the user details
        if (result.Status == TransactionStatus.Success)
        {
          GetUserDetails(Convert.ToInt32(txtUserID.Text), true);
          tcntAllUserTabs.ActiveTab = tpnlCompanies;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "gvUserCompanies_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnUserCompanyAdd_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUserCompanyAdd_Click(object sender, EventArgs e)
    {
      try
      {
        // Create a new CompanyXUsersDL Object
        CompanyXUsersDL compUser = new CompanyXUsersDL();

        // Set whether Add / Edit
        if (txtCompanyID.Text != "0")
          compUser.AddEditOption = 1;
        else
          compUser.AddEditOption = 0;

        // Assign values to the CompanyXUsersDL Object
        compUser.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
        compUser.UserID = Convert.ToInt32(txtUserID.Text);
        compUser.RoleID = Convert.ToInt32(ddlCompanyRole.SelectedValue);
        if (chkDefault.Checked == true)
          compUser.IsDefault = true;
        else
          compUser.IsDefault = false;

        // Add / Edit the User Company
        TransactionResult result;
        compUser.ScreenMode = ScreenMode.Add;
        result = compUser.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the user details
        if (result.Status == TransactionStatus.Success)
        {
          txtCompanyID.Text = "0";
          ddlCompany.ClearSelection();
          ddlCompany.Enabled = true;
          ddlCompanyRole.ClearSelection();
          chkDefault.Checked = false;
          chkDefault.Enabled = true;
          tcntAllUserTabs.ActiveTab = tpnlCompanies;
          LoadDropDownLists();
          GetUserDetails(Convert.ToInt32(txtUserID.Text.ToString()), true);
          lblPopupHeading.Text = "Add User Company";
          btnUserCompanyAdd.Text = "Save";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "btnUserCompanyAdd_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnUserCompanyCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUserCompanyCancel_Click(object sender, EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        txtCompanyID.Text = "0";
        ddlCompany.ClearSelection();
        ddlCompany.Enabled = true;
        ddlCompanyRole.ClearSelection();
        chkDefault.Checked = false;
        chkDefault.Enabled = true;
        tcntAllUserTabs.ActiveTab = tpnlCompanies;
        lblPopupHeading.Text = "Add User Company";
        LoadDropDownLists();
        mpeUserCompany.Hide();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "btnUserCompanyCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Load Lists For Company, Role  
    /// </summary>
    private void LoadDropDownLists()
    {
      try
      {
        // Load Company
        ddlCompany.Items.Clear();
        ddlCompany.DataSource = _company.GetCompanyListNotInCompanyXUsers(Convert.ToInt32(txtUserID.Text)).Tables[0];
        ddlCompany.DataTextField = "CompanyName";
        ddlCompany.DataValueField = "CompanyID";
        ddlCompany.DataBind();
        ddlCompany.Items.Insert(0, "--Select One--");
        ddlCompany.Items[0].Value = "";

        // Load Company Role
        ddlCompanyRole.Items.Clear();
        ddlCompanyRole.DataSource = new RolesDL().GetRolesList().Tables[0];
        ddlCompanyRole.DataTextField = "RoleName";
        ddlCompanyRole.DataValueField = "RoleID";
        ddlCompanyRole.DataBind();
        ddlCompanyRole.Items.Insert(0, "--Select One--");
        ddlCompanyRole.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "LoadDropDownLists", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// To retrieve the details of a particular user
    /// </summary>
    /// <param name="userID">userID</param>
    /// <param name="isProperties">Whether all properties</param>
    private void GetUserDetails(int userID, bool isProperties)
    {
      try
      {
        _currentUser = new UsersDL(userID, isProperties);
        AssignValues();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "GetUserDetails(int userID, bool isProperties)", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// Values of the form are set
    /// </summary
    private void AssignValues()
    {
      try
      {
        // Assign the User Details to the Form Controls
        txtUserID.Text = _currentUser.UserID.ToString();
        txtUserName.Text = _currentUser.UserName.ToString();
        txtPassword.Text = Common.CheckBlank(_currentUser.Password);

        if (_currentUser.EmployeeID != 0)
        {
          EmployeeDL employee = new EmployeeDL(_currentUser.EmployeeID, true);

          lblEmployeeName.Text = employee.EmployeeName;

          //lblEmployeeCode.Text = employee.EmployeeCode.ToString();
          //lblEmployeeID.Text = employee.EmployeeID.ToString();

          lblEmployeeCode.Text = employee.EmployeeCode.cxToString();
          lblEmployeeID.Text = employee.EmployeeID.cxToString();

          hdfOfficeEmailID.Value = employee.OfficeEmailID;

          // Generate a User Name and Password    
          GenerateUserNameAndPassword(employee.EmployeeID);

        }

        chkValid.Checked = _currentUser.IsValid;

        // Assign the list of User Companies to the grid
        gvUserCompanies.DataSource = _currentUser.UserCompanies;
        gvUserCompanies.DataBind();

        if (txtUserID.Text.ToString() != "0" && txtUserID.Text.ToString() != "")
        {
          tcntAllUserTabs.Visible = true;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "AssignValues", ex.Message.ToString(), new ACEConnection());
      }
    }


    /// <summary>
    /// Generate a UserName and Password
    /// </summary>
    /// <param name="employeeID">employeeID</param>        
    public void GenerateUserNameAndPassword(int employeeID)
    {
      try
      {
        // Get the details of the employee using the employee id
        EmployeeDL employee = new EmployeeDL(employeeID, true);

        // Store the Employee's Company ID in a hidden variable
        //hdfEmployeeCompanyID.Value = employee.CompanyID.ToString();

        hdfEmployeeCompanyID.Value = employee.CompanyID.cxToString();

        // Store the Employee's Office Email Id in a hidden variable
        hdfOfficeEmailID.Value = employee.OfficeEmailID;

        // Generate the user name - company id + employee code
        //txtUserName.Text = employee.CompanyID.ToString().Trim() + employee.EmployeeCode.Trim();

        txtUserName.Text = employee.CompanyID.cxToString().Trim() + employee.EmployeeCode.cxToString().Trim();

        // Generate the user password - 
        Utilities objPwd = new Utilities();

        if (employee.Dob != null)
        {
          DateTime dob = (DateTime)employee.Dob;
          txtPassword.Text = objPwd.EncryptText(Convert.ToString(dob.ToString("dd") + employee.EmployeeCode.ToString() + dob.ToString("MM")));
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "GenerateUserNameAndPassword", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// Load the Role Drop Down List
    /// </summary>
    private void LoadRoleDropDown()
    {
      try
      {
        ddlRole.DataSource = new RolesDL().GetRolesList().Tables[0];
        ddlRole.DataTextField = "RoleName";
        ddlRole.DataValueField = "RoleID";
        ddlRole.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("AddEditUser.aspx", "", "LoadRoleDropDown", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

  }
}
