using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System.Net.Mail;
using System.Text;

namespace ACE.PurchaseOrder
{
  public partial class ViewUser : System.Web.UI.Page
  {
    #region Private Variables

    // For listings               
    CompanyDL _company = new CompanyDL();

    // The User instance for viewing
    UsersDL _currentUser;

    MailMessage _msg = new MailMessage();
    int _userID = 0;
    int _companyID = 0;

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
        //_companyID = Convert.ToInt32(Session["CompanyID"]);
        //_userID = Convert.ToInt32(Session["UserID"]);

        _companyID = SessionControl.CompanyID.cxToInt32();
        _userID = SessionControl.UserID.cxToInt32();

        ViewState["PageID"] = CommonDL.GetPageID();
        if (!IsPostBack)
        {
          //if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
          if (SessionControl.CompanyID != 0)
          {
            UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(_userID, _companyID, CommonDL.GetPageID(), true);
            if (!_usersXPagesDL.IsAddorEdit)
            {
              //Response.Redirect("~/Login.aspx", false);
              gvUserDetails.Columns[10].Visible = false;
            }

          }
          else
          {
            Response.Redirect("~/Login.aspx", false);
          }
          // Set ViewState Variables
          ViewState["SortDirection"] = "ASC";
          ViewState["SortExpression"] = "UserName";

          // Assign Common GridView Properties to all GridViews used in the page
          GridViewProperties.AssignGridViewProperties(gvUserDetails);
          GridViewProperties.AssignGridViewProperties(gvUserCompanies);


          // Get the UserID QueryString Value [if QueryString exists]
          if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"] != "null")
          {
            // Get the details of the User with the UserID QueryString Value
            GetUserDetails(Convert.ToInt32(Request.QueryString["UserID"].ToString()), true);

            trUserDetailView.Visible = true;
            trUserDetailTabs.Visible = true;
            trUpdateCancelButtonRow.Visible = true;
            trSearchAndAddRow.Visible = false;
            trGridRow.Visible = false;
            tblUser.Visible = false;
          }
          else
          {
            // Get the list of users
            GetUserDetails(Convert.ToString(txtSearchID.Text.ToString()), Convert.ToBoolean(rblReportOption.SelectedValue));
            // Show only the list of users and hide an individual user detail view
            trUserDetailView.Visible = false;
            trUserDetailTabs.Visible = false;
            trUpdateCancelButtonRow.Visible = false;
            trSearchAndAddRow.Visible = true;
            trGridRow.Visible = true;
            tblUser.Visible = true;

          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
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
        // Transfer Control to View User Form
        Response.Redirect("~/UserManagement/ViewUser.aspx?UserID=null", false);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// rblReportOption_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblReportOption_SelectedIndexChanged(object sender, EventArgs e)
    {
      GetUserDetails(Convert.ToString(txtSearchID.Text.ToString()), Convert.ToBoolean(rblReportOption.SelectedValue));
    }

    #endregion

    #region List of users - Grid

    /// <summary>
    /// gvUserDetails_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvUserDetails.PageIndex * gvUserDetails.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button

          e.Row.Cells[7].Attributes.Add("OnClick", "return confirm('Are you sure you want to reset the password?');");
          if (e.Row.Cells[9].Text != "True")
          {
            e.Row.Cells[7].Enabled = false;
            e.Row.Cells[7].Font.Underline = false;
          }

          // Hide Columns
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[9].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[9].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserDetails_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        // Get User Details
        GetUserDetails(Convert.ToString(txtSearchID.Text.ToString()), Convert.ToBoolean(rblReportOption.SelectedValue));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserDetails_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Get the selected row's user id
        int userIDToEdit = Convert.ToInt32(gvUserDetails.DataKeys[e.NewEditIndex].Value);
        
        trSearchAndAddRow.Visible = false;
        // Transfer Control to Add / Edit User Form with the user id as querystring 
        Response.Redirect("~/UserManagement/AddEditUser.aspx?UserID=" + userIDToEdit.ToString(), false);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserDetails_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        // Get the details of the selected user
        int selUserID = (int)gvUserDetails.SelectedValue;
        GetUserDetails(selUserID, true);

        // Show the details of the selected user
        trUserDetailView.Visible = true;
        trUserDetailTabs.Visible = true;
        trUpdateCancelButtonRow.Visible = true;
        trSearchAndAddRow.Visible = false;
        trGridRow.Visible = false;
        tblUser.Visible = false;
        chkIsValidValue.Enabled = false;

        // Store in ViewState
        ViewState["UserID"] = selUserID;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserDetails_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
      try
      {
        if (e.CommandName == "ResetPassword")
        {
          StringBuilder sbMail = new StringBuilder();

          Control source = e.CommandSource as Control;
          GridViewRow row = source.NamingContainer as GridViewRow;
          int rowIndex = int.Parse(e.CommandArgument.ToString());
          // Get the selected row's user id, emp id
          int userIDToReset = Convert.ToInt32(gvUserDetails.DataKeys[rowIndex].Values["UserID"]);
          int employeeID = Convert.ToInt32(gvUserDetails.DataKeys[rowIndex].Values["EmployeeID"]);

          EmployeeDL employee = new EmployeeDL(employeeID, true);

          Utilities objPwd = new Utilities();

          string password = "";
          DateTime dob = (DateTime)employee.Dob;
          password = objPwd.EncryptText(Convert.ToString(dob.ToString("dd") + employee.EmployeeCode.ToString() + dob.ToString("MM")));

          TransactionResult result = new UsersDL().UserResetPassword(userIDToReset, password);

          StringBuilder sb = new StringBuilder();
          sb.Append("<script>alert('" + result.Message.ToString() + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

          // If reset password had been successful
          if (result.Status == TransactionStatus.Success)
          {
            UsersDL theUser = new UsersDL(userIDToReset, true);

            // From Address
            _msg.From = new MailAddress("hr@ACE.in");

            // To Address
            _msg.To.Add(new MailAddress(employee.OfficeEmailID));

            // Subject
            _msg.Subject = "ACE: Password has been Reset";

            // Body
            sbMail.Append("Dear " + employee.EmployeeName + ",");
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Your password had been reset.");

            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Kindly login with your employee ID and the new password. ");
            sbMail.Append("Please make sure you change your password for information security reasons.");
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Your new login information is given below to access the intranet site,");
            sbMail.Append(Environment.NewLine);

            sbMail.Append("http://192.2.200.2/Order");

            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("User ID : " + theUser.UserName);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Password : " + objPwd.DecryptText(theUser.Password));
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Thank you,");
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            sbMail.Append("Daniel Jacob.");
            sbMail.Append(Environment.NewLine);
            sbMail.Append(Environment.NewLine);

            _msg.Body = sbMail.ToString();

            if (OrderSettings.SendMail)
            {
              SmtpClient client = new SmtpClient();
              client.Send(_msg);
            }
          }

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_RowCommand", ex.Message.ToString(), new ACEConnection());
      }
    }


    //To add the identifier of row
    /// <summary>
    /// gvUserDetails_RowCreated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_RowCreated(object sender, GridViewRowEventArgs e)
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
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_RowCreated", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvUserDetails_PreRender
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvUserDetails_PreRender(object sender, EventArgs e)
    {
      try
      {
        gvUserDetails.UseAccessibleHeader = true;
        gvUserDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
      }

      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserDetails_PreRender", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserCompanies_RowDataBound", ex.Message.ToString(), new ACEConnection());
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
        GetUserDetails(Convert.ToInt32(ViewState["UserID"]), true);

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
          //Sort by Company Name              
          _currentUser.UserCompanies.Sort(new CompanyXUsersComparer_byCompanyName());
        }
        else if (e.SortExpression.ToString() == "RoleName")
        {
          //Sort by Role Name                
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
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "gvUserCompanies_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// To get the list of available users
    /// </summary>
    /// <param name="userID">It is set as zero to select all users</param>
    /// <param name="searchText">The text to search</param>
    private void GetUserDetails(string searchText, bool activatedDeactivated)
    {
      try
      {
        //Convert.ToInt32(Session["CompanyID"]);
        SessionControl.CompanyID.cxToInt32();
        // Get the list of users
        UsersDL userDetails = new UsersDL();

        DataView dView = userDetails.GetUserListByCompanyID(_companyID, searchText, activatedDeactivated).Tables[0].DefaultView;
        gvUserDetails.DataSource = dView;
        gvUserDetails.DataBind();

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "GetUserDetails(int userID, string searchText)", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "GetUserDetails(int userID, bool isProperties)", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// Values of the form are set
    /// </summary
    private void AssignValues()
    {
      try
      {
        // Assign the User Details to the Form Labels
        lblUserNameValue.Text = _currentUser.UserName.ToString();

        Utilities objPwd = new Utilities();
        lblPasswordValue.Text = objPwd.DecryptText(_currentUser.Password);

        if (_currentUser.EmployeeID != 0)
        {
          EmployeeDL employee = new EmployeeDL(_currentUser.EmployeeID, true);
          //lblEmployeeCodeValue.Text = Common.CheckBlank(employee.EmployeeCode);
          //lblEmployeeNameValue.Text = Common.CheckBlank(employee.EmployeeName);

          lblEmployeeCodeValue.Text = employee.EmployeeCode.cxToString();
          lblEmployeeNameValue.Text = employee.EmployeeName.cxToString();

          chkIsValidValue.Checked = _currentUser.IsValid;
        }

        // Assign the list of User Companies to the grid
        gvUserCompanies.DataSource = _currentUser.UserCompanies;
        gvUserCompanies.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ViewUser.aspx", "", "AssignValues", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

  }
}
