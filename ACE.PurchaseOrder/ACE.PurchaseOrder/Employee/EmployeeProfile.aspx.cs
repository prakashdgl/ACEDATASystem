using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class EmployeeProfile : System.Web.UI.Page
  {
    #region Private Variables

    // The Employee instance for viewing
    EmployeeDL _currentEmployee;
    string _dateFormat;
    EmployeeDL getEmployeeInfo = new EmployeeDL();

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
        // commented on 17/04/2014
        // _dateFormat = Convert.ToString(Session["DateFormat"]);
        _dateFormat = SessionControl.DateFormat.ToString();

        if (_dateFormat == null || _dateFormat == "")
        {
          Response.Redirect("~/Login.aspx", false);
        }
        cextCalDOB.Format = _dateFormat;
        cextCalPassportDateOfExpiryEdit.Format = _dateFormat;
        cextCalPassportDateOfIssueEdit.Format = _dateFormat;
        cextCalPresentProjectFromDate.Format = _dateFormat;
        cextCalPresentProjectToDate.Format = _dateFormat;
        cextCalWeddingDateEdit.Format = _dateFormat;

        if (!IsPostBack)
        {

          int companyID = 0;
          int userID = 0;

          //Added on 21/04/2014
          companyID = SessionControl.CompanyID.cxToInt32();
          userID = SessionControl.UserID.cxToInt32();

          //Commented on 17/04/2014
          //if (Session["CompanyID"]  != null && Session["CompanyID"].ToString() != "")
          if (SessionControl.CompanyID != 0)
          {
            //commented on 17/04/2014
            //companyID = Convert.ToInt32(Session["CompanyID"]);
            //userID = Convert.ToInt32(Session["UserID"]);

            UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(userID, companyID, 16, true);
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
          ViewState["SortExpression"] = "";

          // Assign Common GridView Properties to all GridViews used in the page                   
          GridViewProperties.AssignGridViewProperties(gvEmployeeLanguages);
          GridViewProperties.AssignGridViewProperties(gvEmployeeFamily);
          GridViewProperties.AssignGridViewProperties(gvEmployeeEducation);
          GridViewProperties.AssignGridViewProperties(gvEmployeeCertification);
          GridViewProperties.AssignGridViewProperties(gvEmployeeSkill);
          GridViewProperties.AssignGridViewProperties(gvEmployeeExperience);
          GridViewProperties.AssignGridViewProperties(gvEmployeePreviousProject);
          GridViewProperties.AssignGridViewProperties(gvEmployeePresentProject);
          GridViewProperties.AssignGridViewProperties(gvEmployeeDesignationHistory);


          // Load Drop Downs
          LoadICERelationshipsDropDown();
          LoadTitleDropDown();
          LoadNationalityDropDown();
          LoadReligionDropDown();
          LoadDropDownSkillList();
          LoadBloodGroupDropDown();
          LoadMaritalStatusDropDown();
          LoadDropDownYearList();


          //Get Employee Designation History

          //int EmployeeIdent = 0;
          //hdfEmployeeID.Value = Session["EmployeeID"].ToString();
          //EmployeeIdent = Convert.ToInt32(hdfEmployeeID.Value);


          //added nelwy on 05/12/2013
          //if (Session["EmployeeID"].ToString() != "" && Session["EmployeeID"].ToString() != null)
          //{
          //  getEmployeeInfo.EmployeeID = Convert.ToInt32(Session["EmployeeID"]);
          //  getEmployeeDesignationHistory();
          //}


          // Get the EmployeeID QueryString Value [if QueryString exists]
          if (Request.QueryString["EmployeeID"] != null && Request.QueryString["EmployeeID"] != "null")
          {
            hdfEmployeeID.Value = Request.QueryString["EmployeeID"].ToString();

            // Get the details of the Employee with the EmployeeID QueryString Value
            GetEmployeeDetails(Convert.ToInt32(Request.QueryString["EmployeeID"].ToString()), true);

            trEmployeeDetailView.Visible = true;

            getEmployeeInfo.EmployeeID = Convert.ToInt32(Request.QueryString["EmployeeID"]);
            getEmployeeDesignationHistory();
          }

          //else if (Session["EmployeeID"] != null && Session["EmployeeID"].ToString() != "null")
          else if (SessionControl.EmployeeID.ToString() != null)
          {
            // commented on17/04/2014
            // hdfEmployeeID.Value = Session["EmployeeID"].ToString();
            hdfEmployeeID.Value = SessionControl.EmployeeID.ToString();

            // Get the details of the Employee with the EmployeeID Session Variable
            //GetEmployeeDetails(Convert.ToInt32(Session["EmployeeID"].ToString()), true);
            GetEmployeeDetails(hdfEmployeeID.Value.cxToInt32(), true);

            // getEmployeeInfo.EmployeeID = Convert.ToInt32(Session["EmployeeID"]);
            getEmployeeInfo.EmployeeID = hdfEmployeeID.Value.cxToInt32();
            getEmployeeDesignationHistory();

            trEmployeeDetailView.Visible = true;
          }
          else
          {
            trEmployeeDetailView.Visible = false;
          }

          // Load Drop Downs - Based on the EmployeeID
          LoadDropDownLists();

          // For setting the focus to the first control in the modal popups
          imgGeneralEdit.Attributes.Add("onclick", "fnSetFocus('" + ddlTitleEdit.ClientID + "');");
          imgAddressEdit.Attributes.Add("onclick", "fnSetFocus('" + txtPresentAddress1.ClientID + "');");
          imgAdditionalEdit.Attributes.Add("onclick", "fnSetFocus('" + txtPANEdit.ClientID + "');");
          lbtnLanguage.Attributes.Add("onclick", "fnSetFocus('" + ddlLanguage.ClientID + "');");
          lbtnFamily.Attributes.Add("onclick", "fnSetFocus('" + txtFamilyMemberName.ClientID + "');");
          lbtnEducation.Attributes.Add("onclick", "fnSetFocus('" + ddlQualification.ClientID + "');");
          lbtnCertification.Attributes.Add("onclick", "fnSetFocus('" + txtCertification.ClientID + "');");
          lbtnSkill.Attributes.Add("onclick", "fnSetFocus('" + ddlSkillTechnology.ClientID + "');");
          lbtnExperience.Attributes.Add("onclick", "fnSetFocus('" + txtExperienceOrganization.ClientID + "');");
          lbtnPreviousProject.Attributes.Add("onclick", "fnSetFocus('" + txtPreviousProjectName.ClientID + "');");
          lbtnPresentProject.Attributes.Add("onclick", "fnSetFocus('" + ddlPresentProjectName.ClientID + "');");

          chkIsOnsite.Attributes.Add("onclick", "hideOrShowContent();");

        }


      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// getEmployeeDesignationHistory
    /// </summary>
    protected void getEmployeeDesignationHistory()
    {
      //EmployeeDL getEmployeeInfo = new EmployeeDL();
      DataTable dt0 = getEmployeeInfo.GetEmployeeDesignationHistory();
      if (dt0.Rows.Count == 0)
      {
        dt0.Rows.Add(dt0.NewRow());
        gvEmployeeDesignationHistory.DataSource = dt0;
        gvEmployeeDesignationHistory.DataBind();
        int columncount = gvEmployeeDesignationHistory.Rows[0].Cells.Count;
        gvEmployeeDesignationHistory.Rows[0].Cells.Clear();
        gvEmployeeDesignationHistory.Rows[0].Cells.Add(new TableCell());
        gvEmployeeDesignationHistory.Rows[0].Cells[0].ColumnSpan = columncount;
        gvEmployeeDesignationHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
        gvEmployeeDesignationHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
        gvEmployeeDesignationHistory.Rows[0].Cells[0].Text = "Currently there are no entries to display";
      }
      else
      {
        gvEmployeeDesignationHistory.DataSource = dt0;
        gvEmployeeDesignationHistory.DataBind();
      }
    }

    #endregion

    #region General Information
    /// <summary>
    /// ibtnGeneralSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnGeneralSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in General Popup Form
        if (!GeneralPopupValidation()) { return; }

        // Create a new Employee Object
        _currentEmployee = new EmployeeDL(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Set the Edit option
        _currentEmployee.AddEditOption = 1;

        if (ddlTitleEdit.SelectedValue.ToString() != "" && ddlTitleEdit.SelectedValue.ToString() != "0" && ddlTitleEdit.SelectedValue.ToString() != null)
          _currentEmployee.TitleID = Convert.ToInt32(ddlTitleEdit.SelectedValue);
        else
          _currentEmployee.TitleID = 0;

        if (ddlNationalityEdit.SelectedValue.ToString() != "" && ddlNationalityEdit.SelectedValue.ToString() != "0" && ddlNationalityEdit.SelectedValue.ToString() != null)
          _currentEmployee.NationalityID = Convert.ToInt32(ddlNationalityEdit.SelectedValue);
        else
          _currentEmployee.NationalityID = 0;

        if (ddlReligionEdit.SelectedValue.ToString() != "" && ddlReligionEdit.SelectedValue.ToString() != "0" && ddlReligionEdit.SelectedValue.ToString() != null)
          _currentEmployee.ReligionID = Convert.ToInt32(ddlReligionEdit.SelectedValue);
        else
          _currentEmployee.ReligionID = 0;

        if (ddlBloodGroupEdit.SelectedValue.ToString() != "" && ddlBloodGroupEdit.SelectedValue.ToString() != "0" && ddlBloodGroupEdit.SelectedValue.ToString() != null)
          _currentEmployee.BloodGroupID = Convert.ToInt32(ddlBloodGroupEdit.SelectedValue);
        else
          _currentEmployee.BloodGroupID = 0;

        _currentEmployee.HomeTown = txtHomeTownEdit.Text;

        if (ddlMaritalStatusEdit.SelectedValue.ToString() != "" && ddlMaritalStatusEdit.SelectedValue.ToString() != "0" && ddlMaritalStatusEdit.SelectedValue.ToString() != null)
          _currentEmployee.MaritalStatusID = Convert.ToInt32(ddlMaritalStatusEdit.SelectedValue);
        else
          _currentEmployee.MaritalStatusID = 0;

        if (txtWeddingDateEdit.Text != "" && txtWeddingDateEdit.Text != null)
        {
          string dFormat = _dateFormat;
          DateTime dTime;
          dTime = DateTime.ParseExact(txtWeddingDateEdit.Text, dFormat, null);
          _currentEmployee.WeddingDate = dTime;
        }
        else
          _currentEmployee.WeddingDate = null;

        _currentEmployee.Mobile = txtMobileEdit.Text;

        _currentEmployee.EmployeeAddresses.Clear();
        // Add / Edit the Employee
        TransactionResult result;
        _currentEmployee.ScreenMode = ScreenMode.Add;
        result = _currentEmployee.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved Employee
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(_currentEmployee.EmployeeID, true);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnGeneralSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnGeneralCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnGeneralCancel_Click(object sender, EventArgs e)
    {
      try
      {
        if (Convert.ToInt32(hdfTitleID.Value) != 0)
        {
          ddlTitleEdit.ClearSelection();
          ddlTitleEdit.Items.FindByValue(Convert.ToString(hdfTitleID.Value)).Selected = true;
        }
        else
        {
          ddlTitleEdit.SelectedIndex = 0;
        }

        if (Convert.ToInt32(hdfNationalityID.Value) != 0)
        {
          ddlNationalityEdit.ClearSelection();
          ddlNationalityEdit.Items.FindByValue(Convert.ToString(hdfNationalityID.Value)).Selected = true;
        }
        else
        {
          ddlNationalityEdit.SelectedIndex = 0;
        }

        if (Convert.ToInt32(hdfReligionID.Value) != 0)
        {
          ddlReligionEdit.ClearSelection();
          ddlReligionEdit.Items.FindByValue(Convert.ToString(hdfReligionID.Value)).Selected = true;
        }
        else
        {
          ddlReligionEdit.SelectedIndex = 0;
        }

        if (Convert.ToInt32(hdfBloodGroupID.Value) != 0)
        {
          ddlBloodGroupEdit.ClearSelection();
          ddlBloodGroupEdit.Items.FindByValue(Convert.ToString(hdfBloodGroupID.Value)).Selected = true;
        }
        else
        {
          ddlBloodGroupEdit.SelectedIndex = 0;
        }

        if (Convert.ToInt32(hdfMaritalStatusID.Value) != 0)
        {
          ddlMaritalStatusEdit.ClearSelection();
          ddlMaritalStatusEdit.Items.FindByValue(Convert.ToString(hdfMaritalStatusID.Value)).Selected = true;
        }
        else
        {
          ddlMaritalStatusEdit.SelectedIndex = 0;
        }

        if (Convert.ToString(hdfWeddingAnniversaryDate.Value) != null && Convert.ToString(hdfWeddingAnniversaryDate.Value) != "")
        {
          string dFormat = _dateFormat;
          DateTime dTime;
          dTime = DateTime.ParseExact(hdfWeddingAnniversaryDate.Value, dFormat, null);
          txtWeddingDateEdit.Text = dTime.ToString(dFormat);
        }
        else
        {
          txtWeddingDateEdit.Text = "";
        }

        txtHomeTownEdit.Text = hdfHomeTown.Value.ToString();
        txtMobileEdit.Text = hdfMobile.Value.ToString();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnGeneralCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ddlMaritalStatusEdit_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlMaritalStatusEdit_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (ddlMaritalStatusEdit.SelectedItem.Text != "Married")
      {
        txtWeddingDateEdit.Enabled = false;
        txtWeddingDateEdit.Text = "";
      }
      else
      {
        txtWeddingDateEdit.Enabled = true;
      }
      mpeGeneralEdit.Show();
    }

    #endregion

    #region Address Information
    /// <summary>
    /// chkIsPresentAddrSameAsPermanent_CheckedChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void chkIsPresentAddrSameAsPermanent_CheckedChanged(object sender, EventArgs e)
    {
      if (chkIsPresentAddrSameAsPermanent.Checked == true)
      {
        txtPermanentAddress1.Text = txtPresentAddress1.Text;
        txtPermanentAddress2.Text = txtPresentAddress2.Text;
        txtPermanentAddress3.Text = txtPresentAddress3.Text;
        txtPermanentAddressCity.Text = txtPresentAddressCity.Text;
        txtPermanentAddressPinCode.Text = txtPresentAddressPinCode.Text;

        if (Convert.ToString(ddlPresentAddressCountry.SelectedValue) != "")
        {
          int ctryID = Convert.ToInt32(ddlPresentAddressCountry.SelectedValue);
          cddPermanentAddressCountries.SelectedValue = ctryID.ToString();
        }
        if (Convert.ToString(ddlPresentAddressState.SelectedValue) != "")
        {
          int sID = Convert.ToInt32(ddlPresentAddressState.SelectedValue);
          cddPermanentAddressStates.SelectedValue = sID.ToString();
        }
        if (Convert.ToString(ddlPresentAddressDistrict.SelectedValue) != "")
        {
          int dID = Convert.ToInt32(ddlPresentAddressDistrict.SelectedValue);
          cddPermanentAddressDistricts.SelectedValue = dID.ToString();
        }

        txtPermanentAddressPhone.Text = txtPresentAddressPhone.Text;
      }
      else
      {
        txtPermanentAddress1.Text = "";
        txtPermanentAddress2.Text = "";
        txtPermanentAddress3.Text = "";
        txtPermanentAddressCity.Text = "";
        txtPermanentAddressPinCode.Text = "";

        int ctryID = 0;
        HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
        cddPermanentAddressCountries.SelectedValue = "0";

        int sID = 0;
        HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
        cddPermanentAddressStates.SelectedValue = ddlPermanentAddressCountry.SelectedValue;
        cddPermanentAddressStates.ContextKey = sID.ToString();

        int dID = 0;
        HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
        cddPermanentAddressDistricts.SelectedValue = ddlPermanentAddressState.SelectedValue;
        cddPermanentAddressDistricts.ContextKey = dID.ToString();

        txtPermanentAddressPhone.Text = "";
      }

      mpeAddressEdit.Show();
    }

    /// <summary>
    /// ibtnAddressSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddressSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Create a new Employee Object
        _currentEmployee = new EmployeeDL(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Set the Edit option
        _currentEmployee.AddEditOption = 1;
        _currentEmployee.EmployeeAddresses.Clear();
        _currentEmployee.IsPresentAndPermanentAddressSame = chkIsPresentAddrSameAsPermanent.Checked;


        // Create a new EmployeeAddress Object (for the present address)
        EmployeeAddressDL empPresentAddr = new EmployeeAddressDL();

        // Set whether Add / Edit
        if (hdfPresentEmployeeAddressID.Value.ToString() != "0")
        {
          empPresentAddr.AddEditOption = 1;

        }
        else
          empPresentAddr.AddEditOption = 0;

        empPresentAddr.EmployeeAddressID = Convert.ToInt32(hdfPresentEmployeeAddressID.Value);
        empPresentAddr.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empPresentAddr.AddressTypeID = Convert.ToInt32(AddressType.PresentAddress);
        empPresentAddr.AddressInfo.Address1 = txtPresentAddress1.Text;
        empPresentAddr.AddressInfo.Address2 = txtPresentAddress2.Text;
        empPresentAddr.AddressInfo.Address3 = txtPresentAddress3.Text;
        empPresentAddr.AddressInfo.CityDescription = txtPresentAddressCity.Text;
        empPresentAddr.AddressInfo.PostalCode = txtPresentAddressPinCode.Text;

        if (ddlPresentAddressCountry.SelectedValue.ToString() != "" && ddlPresentAddressCountry.SelectedValue.ToString() != "0" && ddlPresentAddressCountry.SelectedValue.ToString() != null)
        {
          empPresentAddr.AddressInfo.CountryID = Convert.ToInt32(ddlPresentAddressCountry.SelectedValue);
        }

        if (ddlPresentAddressState.SelectedValue.ToString() != "" && ddlPresentAddressState.SelectedValue.ToString() != "0" && ddlPresentAddressState.SelectedValue.ToString() != null)
        {
          empPresentAddr.AddressInfo.StateID = Convert.ToInt32(ddlPresentAddressState.SelectedValue);
        }

        if (ddlPresentAddressDistrict.SelectedValue.ToString() != "" && ddlPresentAddressDistrict.SelectedValue.ToString() != "0" && ddlPresentAddressDistrict.SelectedValue.ToString() != null)
        {
          empPresentAddr.AddressInfo.DistrictID = Convert.ToInt32(ddlPresentAddressDistrict.SelectedValue);
        }

        empPresentAddr.Phone = txtPresentAddressPhone.Text;

        _currentEmployee.EmployeeAddresses.Add(empPresentAddr);

        if (chkIsPresentAddrSameAsPermanent.Checked == false)
        {

          // Create a new EmployeeAddress Object (for the permanent address)
          EmployeeAddressDL empPermanentAddr = new EmployeeAddressDL();

          // Set whether Add / Edit
          if (hdfPermanentEmployeeAddressID.Value.ToString() != "0")
          {
            empPermanentAddr.AddEditOption = 1;
          }
          else
            empPermanentAddr.AddEditOption = 0;

          empPermanentAddr.EmployeeAddressID = Convert.ToInt32(hdfPermanentEmployeeAddressID.Value);
          empPermanentAddr.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
          empPermanentAddr.AddressTypeID = Convert.ToInt32(AddressType.PermanentAddress);
          empPermanentAddr.AddressInfo.Address1 = txtPermanentAddress1.Text;
          empPermanentAddr.AddressInfo.Address2 = txtPermanentAddress2.Text;
          empPermanentAddr.AddressInfo.Address3 = txtPermanentAddress3.Text;
          empPermanentAddr.AddressInfo.CityDescription = txtPermanentAddressCity.Text;
          empPermanentAddr.AddressInfo.PostalCode = txtPermanentAddressPinCode.Text;

          if (ddlPermanentAddressCountry.SelectedValue.ToString() != "" && ddlPermanentAddressCountry.SelectedValue.ToString() != "0" && ddlPermanentAddressCountry.SelectedValue.ToString() != null)
          {
            empPermanentAddr.AddressInfo.CountryID = Convert.ToInt32(ddlPermanentAddressCountry.SelectedValue);
          }

          if (ddlPermanentAddressState.SelectedValue.ToString() != "" && ddlPermanentAddressState.SelectedValue.ToString() != "0" && ddlPermanentAddressState.SelectedValue.ToString() != null)
          {
            empPermanentAddr.AddressInfo.StateID = Convert.ToInt32(ddlPermanentAddressState.SelectedValue);
          }

          if (ddlPermanentAddressDistrict.SelectedValue.ToString() != "" && ddlPermanentAddressDistrict.SelectedValue.ToString() != "0" && ddlPermanentAddressDistrict.SelectedValue.ToString() != null)
          {
            empPermanentAddr.AddressInfo.DistrictID = Convert.ToInt32(ddlPermanentAddressDistrict.SelectedValue);
          }

          empPermanentAddr.Phone = txtPermanentAddressPhone.Text;

          _currentEmployee.EmployeeAddresses.Add(empPermanentAddr);

        }
        else
        {
          // Create a new EmployeeAddress Object (for the permanent address)
          EmployeeAddressDL empPermanentAddr = new EmployeeAddressDL();

          if (hdfPermanentEmployeeAddressID.Value.ToString() != "0")
          {
            empPermanentAddr.AddEditOption = 2;
            empPermanentAddr.EmployeeAddressID = Convert.ToInt32(hdfPermanentEmployeeAddressID.Value);
            empPermanentAddr.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
            _currentEmployee.EmployeeAddresses.Add(empPermanentAddr);
          }
        }

        // Add / Edit the Employee
        TransactionResult result;
        _currentEmployee.ScreenMode = ScreenMode.Add;
        result = _currentEmployee.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved Employee
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(_currentEmployee.EmployeeID, true);
          mpeAddressEdit.Hide();
        }
        else
        {
          mpeAddressEdit.Show();
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnAddressSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnAddressCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAddressCancel_Click(object sender, EventArgs e)
    {
      try
      {
        _currentEmployee = new EmployeeDL(Convert.ToInt32(hdfEmployeeID.Value), true);
        GetEmployeeDetails(_currentEmployee.EmployeeID, true);
        mpeAddressEdit.Hide();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnAddressCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Additional Information
    /// <summary>
    /// ibtnAdditionalSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAdditionalSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Additional Popup Form
        if (!AdditionalPopupValidation()) { return; }


        // Create a new Employee Object
        _currentEmployee = new EmployeeDL(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Set the Edit option
        _currentEmployee.AddEditOption = 1;

        _currentEmployee.Pan = txtPANEdit.Text;
        _currentEmployee.BankAccountNumber = txtBankAccountNumberEdit.Text;
        _currentEmployee.BankName = txtBankNameEdit.Text;
        _currentEmployee.PassportNo = txtPassportNumberEdit.Text;
        string dFormat = _dateFormat;
        DateTime dTime;
        if (txtPassportDateOfIssueEdit.Text != "" && txtPassportDateOfIssueEdit.Text != null)
        {
          dTime = DateTime.ParseExact(txtPassportDateOfIssueEdit.Text, dFormat, null);
          _currentEmployee.PassportDateIssue = dTime;
        }
        else
          _currentEmployee.PassportDateIssue = null;

        if (txtPassportDateOfExpiryEdit.Text != "" && txtPassportDateOfExpiryEdit.Text != null)
        {
          dTime = DateTime.ParseExact(txtPassportDateOfExpiryEdit.Text, dFormat, null);
          _currentEmployee.PassportDateExpiry = dTime;
        }
        else
          _currentEmployee.PassportDateExpiry = null;

        _currentEmployee.PassportIssuePlace = txtPassportPlaceOfIssueEdit.Text;
        _currentEmployee.PersonalEmailID = txtPersonalMailIDEdit.Text;
        _currentEmployee.MessengerID = txtMessengerIDEdit.Text;
        _currentEmployee.IceName = txtICENameEdit1.Text;
        if (ddlICERelationshipEdit1.SelectedValue.ToString() != "" && ddlICERelationshipEdit1.SelectedValue.ToString() != "0" && ddlICERelationshipEdit1.SelectedValue.ToString() != null)
        {
          _currentEmployee.IceRelationshipID = Convert.ToInt32(ddlICERelationshipEdit1.SelectedValue);
        }
        else
        {
          _currentEmployee.IceRelationshipID = 0;
        }
        _currentEmployee.IceMobile = txtICEPhoneEdit1.Text;
        _currentEmployee.IceName2 = txtICENameEdit2.Text;
        if (ddlICERelationshipEdit2.SelectedValue.ToString() != "" && ddlICERelationshipEdit2.SelectedValue.ToString() != "0" && ddlICERelationshipEdit2.SelectedValue.ToString() != null)
        {
          _currentEmployee.IceRelationshipID2 = Convert.ToInt32(ddlICERelationshipEdit2.SelectedValue);
        }
        else
        {
          _currentEmployee.IceRelationshipID2 = 0;
        }
        _currentEmployee.IceMobile2 = txtICEPhoneEdit2.Text;

        _currentEmployee.EmployeeAddresses.Clear();

        // Add / Edit the Employee
        TransactionResult result;
        _currentEmployee.ScreenMode = ScreenMode.Add;
        result = _currentEmployee.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved Employee
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(_currentEmployee.EmployeeID, true);
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnAdditionalSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnAdditionalCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnAdditionalCancel_Click(object sender,EventArgs e)
    {
      try
      {
        txtPANEdit.Text = hdfPAN.Value.ToString();
        txtBankAccountNumberEdit.Text = hdfBankAccountNumber.Value.ToString();
        txtBankNameEdit.Text = hdfBankName.Value.ToString();
        txtPassportNumberEdit.Text = hdfPassportNumber.Value.ToString();

        string dFormat = _dateFormat;
        DateTime dTime;
        if (hdfDateOfIssue.Value.ToString() != "" && hdfDateOfIssue.Value != null)
        {
          dTime = DateTime.ParseExact(hdfDateOfIssue.Value, dFormat, null);
          txtPassportDateOfIssueEdit.Text = dTime.ToString(dFormat);
        }
        else
        {
          txtPassportDateOfIssueEdit.Text = "";
        }
        if (hdfDateOfExpiry.Value.ToString() != "" && hdfDateOfExpiry.Value != null)
        {
          dTime = DateTime.ParseExact(hdfDateOfExpiry.Value, dFormat, null);
          txtPassportDateOfExpiryEdit.Text = dTime.ToString(dFormat);
        }
        else
        {
          txtPassportDateOfExpiryEdit.Text = "";
        }

        txtPassportPlaceOfIssueEdit.Text = hdfPlaceOfIssue.Value.ToString();
        txtPersonalMailIDEdit.Text = hdfPersonalEmailID.Value.ToString();
        txtMessengerIDEdit.Text = hdfMessengerID.Value.ToString();
        txtICENameEdit1.Text = hdfICEName1.Value.ToString();

        if (Convert.ToInt32(hdfICERelationshipID1.Value) != 0)
        {
          ddlICERelationshipEdit1.ClearSelection();
          ddlICERelationshipEdit1.Items.FindByValue(Convert.ToString(hdfICERelationshipID1.Value)).Selected = true;
        }
        else
          ddlICERelationshipEdit1.SelectedIndex = 0;

        txtICEPhoneEdit1.Text = hdfPhone1.Value.ToString();

        txtICENameEdit2.Text = hdfICEName2.Value.ToString();
        if (Convert.ToInt32(hdfICERelationshipID2.Value) != 0)
        {
          ddlICERelationshipEdit2.ClearSelection();
          ddlICERelationshipEdit2.Items.FindByValue(Convert.ToString(hdfICERelationshipID2.Value)).Selected = true;
        }
        else
          ddlICERelationshipEdit2.SelectedIndex = 0;

        txtICEPhoneEdit2.Text = hdfPhone2.Value.ToString();

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnAdditionalCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Languages Known Grid
    /// <summary>
    /// gvEmployeeLanguages_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeLanguages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeLanguages.PageIndex * gvEmployeeLanguages.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeLanguages_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeLanguages_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeLanguages_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //if (e.SortExpression.ToString() == "LanguageDescription")
        //{
        //  //Sort by Language Description                
        //  _currentEmployee.EmployeeLanguages.Sort(new EmployeeLanguageKnownComparer_byLanguageDesc());
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeLanguages.Reverse();

        // Assign the list of Employee Languages after sorting to the grid 
        gvEmployeeLanguages.DataSource = _currentEmployee.EmployeeLanguages;
        gvEmployeeLanguages.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeLanguages_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeLanguages_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeLanguages_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeLanguageKnownID.Value = gvEmployeeLanguages.Rows[e.NewEditIndex].Cells[1].Text;
        ddlLanguage.ClearSelection();
        ddlLanguage.Items.Insert(0, gvEmployeeLanguages.Rows[e.NewEditIndex].Cells[4].Text.ToString());
        ddlLanguage.Items[0].Value = gvEmployeeLanguages.Rows[e.NewEditIndex].Cells[3].Text.ToString();
        ddlLanguage.SelectedValue = gvEmployeeLanguages.Rows[e.NewEditIndex].Cells[3].Text.ToString();

        // Is Read                
        CheckBox chkTempIsRead = (CheckBox)gvEmployeeLanguages.Rows[e.NewEditIndex].FindControl("chkIsRead"); ;
        if (chkTempIsRead.Checked == true)
        {
          chkRead.Checked = true;
        }
        else
        {
          chkRead.Checked = false;
        }

        // Is Write                
        CheckBox chkTempIsWrite = (CheckBox)gvEmployeeLanguages.Rows[e.NewEditIndex].FindControl("chkIsWrite"); ;
        if (chkTempIsWrite.Checked == true)
        {
          chkWrite.Checked = true;
        }
        else
        {
          chkWrite.Checked = false;
        }

        // Is Speak                
        CheckBox chkTempIsSpeak = (CheckBox)gvEmployeeLanguages.Rows[e.NewEditIndex].FindControl("chkIsSpeak"); ;
        if (chkTempIsSpeak.Checked == true)
        {
          chkSpeak.Checked = true;
        }
        else
        {
          chkSpeak.Checked = false;
        }

        lblPopupLanguageHeading.Text = "Edit Language";
        // Show the Popup
        mpeEditLanguageKnown.Show();

        e.Cancel = true;

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeLanguages_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeLanguages_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeLanguages_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Language Known id
        int eLanguageIDToDelete = Convert.ToInt32(gvEmployeeLanguages.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Language Known
        EmployeeLanguageKnownDL deleteEmployeeLanguage = new EmployeeLanguageKnownDL(eLanguageIDToDelete, false);
        deleteEmployeeLanguage.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeLanguage.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeLanguages_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnLanguageSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnLanguageSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Language Popup Form
        if (!LanguagePopupValidation()) { return; }

        // Create a new EmployeeLanguageKnown Object
        EmployeeLanguageKnownDL empLanguage = new EmployeeLanguageKnownDL();

        // Set whether Add / Edit
        if (hdfEmployeeLanguageKnownID.Value.ToString() != "0")
        {
          empLanguage.AddEditOption = 1;

        }
        else
          empLanguage.AddEditOption = 0;

        // Assign values to the EmployeeLanguageKnown Object
        empLanguage.EmployeeLanguageKnownID = Convert.ToInt32(hdfEmployeeLanguageKnownID.Value);
        empLanguage.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empLanguage.LanguageID = Convert.ToInt32(ddlLanguage.SelectedValue);
        empLanguage.IsRead = chkRead.Checked;
        empLanguage.IsWrite = chkWrite.Checked;
        empLanguage.IsSpeak = chkSpeak.Checked;

        // Add / Edit the Employee Language Known
        TransactionResult result;
        empLanguage.ScreenMode = ScreenMode.Add;
        result = empLanguage.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Organization details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeLanguageKnownID.Value = "0";
          ddlLanguage.ClearSelection();

          chkRead.Checked = false;
          chkWrite.Checked = false;
          chkSpeak.Checked = false;

          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
          lblPopupLanguageHeading.Text = "Add Language";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnLanguageSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnLanguageCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnLanguageCancel_Click(object sender,EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        hdfEmployeeLanguageKnownID.Value = "0";
        ddlLanguage.ClearSelection();

        chkRead.Checked = false;
        chkWrite.Checked = false;
        chkSpeak.Checked = false;

        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
        LoadDropDownLists();
        lblPopupLanguageHeading.Text = "Add Language";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnLanguageCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Family Grid
    /// <summary>
    /// gvEmployeeFamily_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeFamily_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeFamily.PageIndex * gvEmployeeFamily.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          // Display the Date in dd/MM/yyyy format
          string dtFormat = _dateFormat;
          DateTime dTime;
          if (e.Row.Cells[8].Text != "&nbsp;")
          {
            dTime = Convert.ToDateTime(e.Row.Cells[8].Text);
            e.Row.Cells[8].Text = dTime.ToString(dtFormat);
          }

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
          e.Row.Cells[6].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
          e.Row.Cells[6].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeFamily_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeFamily_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeFamily_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "Name":
        //    //Sort by Name                                  
        //    _currentEmployee.EmployeeFamilyMembers.Sort(new EmployeeFamilyComparer_byName());
        //    break;

        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeFamilyMembers.Reverse();

        // Assign the list of Employee Family after sorting to the grid 
        gvEmployeeFamily.DataSource = _currentEmployee.EmployeeFamilyMembers;
        gvEmployeeFamily.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeFamily_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeFamily_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeFamily_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeFamilyID.Value = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[1].Text;
        txtFamilyMemberName.Text = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[3].Text;

        ddlGender.ClearSelection();
        ddlGender.Items.Insert(0, gvEmployeeFamily.Rows[e.NewEditIndex].Cells[5].Text);
        ddlGender.Items[0].Value = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[4].Text;
        ddlGender.SelectedValue = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[4].Text;

        ddlRelationship.ClearSelection();
        ddlRelationship.Items.Insert(0, gvEmployeeFamily.Rows[e.NewEditIndex].Cells[7].Text);
        ddlRelationship.Items[0].Value = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[6].Text;
        ddlRelationship.SelectedValue = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[6].Text;

        txtDOB.Text = gvEmployeeFamily.Rows[e.NewEditIndex].Cells[8].Text;
        if (txtDOB.Text == "&nbsp;")
          txtDOB.Text = "";

        lblPopupHeaderFamily.Text = "Edit Family Member";

        // Show the Popup
        mpeEditFamily.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeFamily_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeFamily_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeFamily_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Family id
        int eFamilyIDToDelete = Convert.ToInt32(gvEmployeeFamily.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Family
        EmployeeFamilyDL deleteEmployeeFamily = new EmployeeFamilyDL(eFamilyIDToDelete, false);
        deleteEmployeeFamily.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeFamily.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeFamily_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnFamilySave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnFamilySave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Family Popup Form
        if (!FamilyPopupValidation()) { return; }

        // Create a new EmployeeFamily Object
        EmployeeFamilyDL empFamily = new EmployeeFamilyDL();

        // Set whether Add / Edit
        if (hdfEmployeeFamilyID.Value.ToString() != "0")
          empFamily.AddEditOption = 1;
        else
          empFamily.AddEditOption = 0;

        // Assign values to the EmployeeFamily Object
        empFamily.EmployeeFamilyID = Convert.ToInt32(hdfEmployeeFamilyID.Value);
        empFamily.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empFamily.Name = txtFamilyMemberName.Text;
        empFamily.GenderID = Convert.ToInt32(ddlGender.SelectedValue);
        empFamily.RelationshipID = Convert.ToInt32(ddlRelationship.SelectedValue);

        if (txtDOB.Text != "" && txtDOB.Text != null)
        {
          // Display the Date in dd/MM/yyyy format
          string dtFormat = _dateFormat;
          DateTime dTime;
          dTime = DateTime.ParseExact(txtDOB.Text, dtFormat, null);
          empFamily.DOB = dTime;
        }
        else
          empFamily.DOB = null;

        // Add / Edit the Employee Family
        TransactionResult result;
        empFamily.ScreenMode = ScreenMode.Add;
        result = empFamily.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeFamilyID.Value = "0";
          txtFamilyMemberName.Text = "";
          ddlGender.ClearSelection();
          ddlRelationship.ClearSelection();
          txtDOB.Text = "";

          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
          lblPopupHeaderFamily.Text = "Add Family Member";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnFamilySave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnFamilyCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnFamilyCancel_Click(object sender, EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        hdfEmployeeFamilyID.Value = "0";
        txtFamilyMemberName.Text = "";
        ddlGender.ClearSelection();
        ddlRelationship.ClearSelection();
        txtDOB.Text = "";
        lblPopupHeaderFamily.Text = "Add Family Member";

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnFamilyCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Education Grid
    /// <summary>
    /// gvEmployeeEducation_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeEducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeEducation.PageIndex * gvEmployeeEducation.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          if (e.Row.Cells[10].Text == "&nbsp;")
          {
            e.Row.Cells[10].Text = "";
          }
          if (e.Row.Cells[7].Text == "0")
          {
            e.Row.Cells[7].Text = "";
          }

          if (e.Row.Cells[11].Text == "&nbsp;")
          {
            e.Row.Cells[11].Text = "";
          }
          TextBox tb = new TextBox();

          tb.ID = "str1";
          tb.Text = " 'str'" + " runat= 'server'";

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[5].Visible = false;
          e.Row.Cells[8].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[5].Visible = false;
          e.Row.Cells[8].Visible = false;
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeEducation_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeEducation_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeEducation_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "QualificationDescription":
        //    //Sort by Qualification Description                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byQualificationDesc());
        //    break;
        //  case "MajorDescription":
        //    //Sort by Major Description                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byMajorDesc());
        //    break;
        //  case "YearOfPass":
        //    //Sort by Year Of Pass                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byYearOfPass());
        //    break;
        //  case "UniversityDescription":
        //    //Sort by University Description                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byUniversityDesc());
        //    break;
        //  case "ClassObtained":
        //    //Sort by Class Obtained                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byClassObtained());
        //    break;
        //  case "InstitutionDescription":
        //    //Sort by Institution Description                                  
        //    _currentEmployee.EmployeeEducationDetails.Sort(new EmployeeEducationComparer_byInstitutionDesc());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeEducationDetails.Reverse();

        // Assign the list of Employee Education Details after sorting to the grid 
        gvEmployeeEducation.DataSource = _currentEmployee.EmployeeEducationDetails;
        gvEmployeeEducation.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeEducation_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeEducation_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeEducation_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeEducationID.Value = gvEmployeeEducation.Rows[e.NewEditIndex].Cells[1].Text;

        ddlQualification.ClearSelection();
        ListItem liQualification = ddlQualification.Items.FindByValue(gvEmployeeEducation.Rows[e.NewEditIndex].Cells[3].Text);
        ddlQualification.SelectedIndex = ddlQualification.Items.IndexOf(liQualification);

        ddlMajor.ClearSelection();
        ListItem liMajor = ddlMajor.Items.FindByValue(gvEmployeeEducation.Rows[e.NewEditIndex].Cells[5].Text);
        ddlMajor.SelectedIndex = ddlMajor.Items.IndexOf(liMajor);

        if (gvEmployeeEducation.Rows[e.NewEditIndex].Cells[7].Text.ToString() != "")
          ddlEducationYearOfPass.SelectedValue = gvEmployeeEducation.Rows[e.NewEditIndex].Cells[7].Text;
        else
          ddlEducationYearOfPass.SelectedIndex = 0;

        ddlUniversity.ClearSelection();
        ListItem liUniversity = ddlUniversity.Items.FindByValue(gvEmployeeEducation.Rows[e.NewEditIndex].Cells[8].Text);
        ddlUniversity.SelectedIndex = ddlUniversity.Items.IndexOf(liUniversity);

        txtInstitution.Text = gvEmployeeEducation.Rows[e.NewEditIndex].Cells[10].Text;
        txtEducationClassObtained.Text = gvEmployeeEducation.Rows[e.NewEditIndex].Cells[11].Text;

        if (txtDOB.Text == "&nbsp;")
          txtDOB.Text = "";

        lblPopupHeadingEducation.Text = "Edit Education";

        // Show the Popup
        mpeEditEducation.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeEducation_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeEducation_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeEducation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Education id
        int eEducationIDToDelete = Convert.ToInt32(gvEmployeeEducation.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Education
        EmployeeEducationDL deleteEmployeeEducation = new EmployeeEducationDL(eEducationIDToDelete, false);
        deleteEmployeeEducation.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeEducation.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeEducation_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnEducationSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEducationSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Create a new EmployeeEducation Object
        EmployeeEducationDL empEducation = new EmployeeEducationDL();

        // Set whether Add / Edit
        if (hdfEmployeeEducationID.Value.ToString() != "0")
          empEducation.AddEditOption = 1;
        else
          empEducation.AddEditOption = 0;

        // Assign values to the EmployeeEducation Object
        empEducation.EmployeeEducationID = Convert.ToInt32(hdfEmployeeEducationID.Value);
        empEducation.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        if (ddlQualification.SelectedValue.ToString() != "" && ddlQualification.SelectedValue.ToString() != "0" && ddlQualification.SelectedValue.ToString() != null)
        {
          empEducation.QualificationID = Convert.ToInt32(ddlQualification.SelectedValue);
        }
        if (ddlMajor.SelectedValue.ToString() != "" && ddlMajor.SelectedValue.ToString() != "0" && ddlMajor.SelectedValue.ToString() != null)
        {
          empEducation.MajorID = Convert.ToInt32(ddlMajor.SelectedValue);
        }
        empEducation.YearOfPass = Convert.ToInt32(ddlEducationYearOfPass.SelectedValue);
        if (ddlUniversity.SelectedValue.ToString() != "" && ddlUniversity.SelectedValue.ToString() != "0" && ddlUniversity.SelectedValue.ToString() != null)
        {
          empEducation.UniversityID = Convert.ToInt32(ddlUniversity.SelectedValue);
        }
        empEducation.InstitutionDescription = txtInstitution.Text;
        empEducation.ClassObtained = txtEducationClassObtained.Text;

        // Add / Edit the Employee Education
        TransactionResult result;
        empEducation.ScreenMode = ScreenMode.Add;
        result = empEducation.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeEducationID.Value = "0";
          ddlQualification.ClearSelection();
          ddlMajor.ClearSelection();
          ddlEducationYearOfPass.SelectedIndex = 0;
          ddlUniversity.ClearSelection();
          txtInstitution.Text = "";
          txtEducationClassObtained.Text = "";

          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
          lblPopupHeadingEducation.Text = "Add Education";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnEducationSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnEducationCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEducationCancel_Click(object sender, EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        hdfEmployeeEducationID.Value = "0";
        ddlQualification.ClearSelection();
        ddlMajor.ClearSelection();
        ddlEducationYearOfPass.SelectedIndex = 0;
        ddlUniversity.ClearSelection();
        txtInstitution.Text = "";
        txtEducationClassObtained.Text = "";

        lblPopupHeadingEducation.Text = "Add Education";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnEducationCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Certification Grid
    /// <summary>
    /// gvEmployeeCertification_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeCertification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeCertification.PageIndex * gvEmployeeCertification.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");
          if (e.Row.Cells[6].Text == "0")
          {
            e.Row.Cells[6].Text = "";
          }
          if (e.Row.Cells[7].Text == "&nbsp;")
          {
            e.Row.Cells[7].Text = "";
          }
          if (e.Row.Cells[8].Text == "&nbsp;")
          {
            e.Row.Cells[8].Text = "";
          }
          if (e.Row.Cells[9].Text == "&nbsp;")
          {
            e.Row.Cells[9].Text = "";
          }

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[4].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeCertification_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeCertification_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeCertification_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "Certification":
        //    //Sort by Certification                                  
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byCertification());
        //    break;
        //  case "TechnologyDescription":
        //    //Sort by Technology Description                                  
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byTechnologyDesc());
        //    break;
        //  case "YearOfPass":
        //    //Sort by Year Of Pass                                  
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byYearOfPass());
        //    break;
        //  case "IssuedBy":
        //    //Sort by IssuedBy                                  
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byIssuedBy());
        //    break;
        //  case "ClassObtained":
        //    //Sort by Class Obtained                                  
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byClassObtained());
        //    break;
        //  case "TranscriptID":
        //    //Sort by TranscriptID                                 
        //    _currentEmployee.EmployeeCertifications.Sort(new EmployeeCertificationComparer_byTranscriptID());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeCertifications.Reverse();

        // Assign the list of Employee Certifications after sorting to the grid 
        gvEmployeeCertification.DataSource = _currentEmployee.EmployeeCertifications;
        gvEmployeeCertification.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeCertification_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeCertification_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeCertification_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeCertificationID.Value = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[1].Text;

        txtCertification.Text = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[3].Text;

        ddlCertificationTechnology.ClearSelection();
        ListItem liCertificationTechnology = ddlCertificationTechnology.Items.FindByValue(gvEmployeeCertification.Rows[e.NewEditIndex].Cells[4].Text);
        ddlCertificationTechnology.SelectedIndex = ddlCertificationTechnology.Items.IndexOf(liCertificationTechnology);

        if (gvEmployeeCertification.Rows[e.NewEditIndex].Cells[6].Text.ToString() != "")
          ddlCertificationYearOfPass.SelectedValue = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[6].Text;
        else
          ddlCertificationYearOfPass.SelectedIndex = 0;

        txtIssuedBy.Text = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[7].Text;
        txtCertificationClassObtained.Text = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[8].Text;
        txtTranscriptID.Text = gvEmployeeCertification.Rows[e.NewEditIndex].Cells[9].Text;

        lblPopupHeadingCertify.Text = "Edit Certification";
        // Show the Popup
        mpeEditCertification.Show();
        e.Cancel = true;

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeCertification_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeCertification_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeCertification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Certification id
        int eCertificationIDToDelete = Convert.ToInt32(gvEmployeeCertification.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Certification
        EmployeeCertificationDL deleteEmployeeCertification = new EmployeeCertificationDL(eCertificationIDToDelete, false);
        deleteEmployeeCertification.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeCertification.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeCertification_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnCertificationSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnCertificationSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Create a new EmployeeCertification Object
        EmployeeCertificationDL empCertification = new EmployeeCertificationDL();

        // Set whether Add / Edit
        if (hdfEmployeeCertificationID.Value.ToString() != "0")
          empCertification.AddEditOption = 1;
        else
          empCertification.AddEditOption = 0;

        // Assign values to the EmployeeCertification Object
        empCertification.EmployeeCertificationID = Convert.ToInt32(hdfEmployeeCertificationID.Value);
        empCertification.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empCertification.Certification = txtCertification.Text;
        if (ddlCertificationTechnology.SelectedValue.ToString() != "" && ddlCertificationTechnology.SelectedValue.ToString() != "0" && ddlCertificationTechnology.SelectedValue.ToString() != null)
        {
          empCertification.TechnologyID = Convert.ToInt32(ddlCertificationTechnology.SelectedValue);
        }

        empCertification.YearOfPass = Convert.ToInt32(ddlCertificationYearOfPass.SelectedValue);
        empCertification.IssuedBy = txtIssuedBy.Text;
        empCertification.ClassObtained = txtCertificationClassObtained.Text;
        empCertification.TranscriptID = txtTranscriptID.Text;

        // Add / Edit the Employee Certification
        TransactionResult result;
        empCertification.ScreenMode = ScreenMode.Add;
        result = empCertification.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeCertificationID.Value = "0";
          txtCertification.Text = "";
          ddlCertificationTechnology.ClearSelection();
          ddlCertificationYearOfPass.SelectedIndex = 0;
          txtIssuedBy.Text = "";
          txtCertificationClassObtained.Text = "";
          txtTranscriptID.Text = "";

          lblPopupHeadingCertify.Text = "Add Certification";
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnCertificationSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnCertificationCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnCertificationCancel_Click(object sender,EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        hdfEmployeeCertificationID.Value = "0";
        txtCertification.Text = "";
        ddlCertificationTechnology.ClearSelection();
        ddlCertificationYearOfPass.SelectedIndex = 0;
        txtIssuedBy.Text = "";
        txtCertificationClassObtained.Text = "";
        txtTranscriptID.Text = "";
        lblPopupHeadingCertify.Text = "Add Certification";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnCertificationCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Skill Grid
    /// <summary>
    /// gvEmployeeSkill_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeSkill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeSkill.PageIndex * gvEmployeeSkill.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[5].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[5].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeSkill_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeSkill_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeSkill_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "TechnologyDescription":
        //    //Sort by Technology Description                                  
        //    _currentEmployee.EmployeeSkills.Sort(new EmployeeSkillComparer_byTechnologyDesc());
        //    break;
        //  case "SkillLevelDescription":
        //    //Sort by SkillLevel Description                                  
        //    _currentEmployee.EmployeeSkills.Sort(new EmployeeSkillComparer_bySkillLevelDesc());
        //    break;
        //  case "ExperienceInYears":
        //    //Sort by Experience In Years                                  
        //    _currentEmployee.EmployeeSkills.Sort(new EmployeeSkillComparer_byExpInYrs());
        //    break;
        //  case "ExperienceInMonths":
        //    //Sort by Experience In Months                                  
        //    _currentEmployee.EmployeeSkills.Sort(new EmployeeSkillComparer_byExpInMons());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeSkills.Reverse();

        // Assign the list of Employee Skills after sorting to the grid 
        gvEmployeeSkill.DataSource = _currentEmployee.EmployeeSkills;
        gvEmployeeSkill.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeSkill_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeSkill_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeSkill_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeSkillID.Value = gvEmployeeSkill.Rows[e.NewEditIndex].Cells[1].Text;

        ddlSkillTechnology.ClearSelection();
        ListItem liSkillTechnology = ddlSkillTechnology.Items.FindByValue(gvEmployeeSkill.Rows[e.NewEditIndex].Cells[3].Text);
        ddlSkillTechnology.SelectedIndex = ddlSkillTechnology.Items.IndexOf(liSkillTechnology);

        ddlSkillLevel.ClearSelection();
        ListItem liSkillLevel = ddlSkillLevel.Items.FindByValue(gvEmployeeSkill.Rows[e.NewEditIndex].Cells[5].Text);
        ddlSkillLevel.SelectedIndex = ddlSkillLevel.Items.IndexOf(liSkillLevel);

        ddlSkillExperienceYears.SelectedValue = gvEmployeeSkill.Rows[e.NewEditIndex].Cells[7].Text;
        ddlSkillExperienceMonths.SelectedValue = gvEmployeeSkill.Rows[e.NewEditIndex].Cells[8].Text;

        lblPopHeadingSkill.Text = "Edit Skill";

        // Show the Popup
        mpeEditSkill.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeSkill_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeSkill_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeSkill_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Skill id
        int eSkillIDToDelete = Convert.ToInt32(gvEmployeeSkill.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Skill
        EmployeeSkillDL deleteEmployeeSkill = new EmployeeSkillDL(eSkillIDToDelete, false);
        deleteEmployeeSkill.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeSkill.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeSkill_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnSkillSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnSkillSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Skill Popup Form
        if (!SkillPopupValidation()) { return; }

        // Create a new EmployeeSkill Object
        EmployeeSkillDL empSkill = new EmployeeSkillDL();

        // Set whether Add / Edit
        if (hdfEmployeeSkillID.Value.ToString() != "0")
          empSkill.AddEditOption = 1;
        else
          empSkill.AddEditOption = 0;

        // Assign values to the EmployeeSkill Object
        empSkill.EmployeeSkillID = Convert.ToInt32(hdfEmployeeSkillID.Value);
        empSkill.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        if (ddlSkillTechnology.SelectedValue.ToString() != "" && ddlSkillTechnology.SelectedValue.ToString() != "0" && ddlSkillTechnology.SelectedValue.ToString() != null)
        {
          empSkill.TechnologyID = Convert.ToInt32(ddlSkillTechnology.SelectedValue);
        }
        if (ddlSkillLevel.SelectedValue.ToString() != "" && ddlSkillLevel.SelectedValue.ToString() != "0" && ddlSkillLevel.SelectedValue.ToString() != null)
        {
          empSkill.SkillLevelID = Convert.ToInt32(ddlSkillLevel.SelectedValue);
        }
        empSkill.ExperienceInYears = Convert.ToInt32(ddlSkillExperienceYears.SelectedValue);
        empSkill.ExperienceInMonths = Convert.ToInt32(ddlSkillExperienceMonths.SelectedValue);

        // Add / Edit the Employee Skill
        TransactionResult result;
        empSkill.ScreenMode = ScreenMode.Add;
        result = empSkill.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeSkillID.Value = "0";
          ddlSkillTechnology.ClearSelection();
          ddlSkillLevel.ClearSelection();
          ddlSkillExperienceYears.SelectedValue = "0";
          ddlSkillExperienceMonths.SelectedValue = "0";
          lblPopHeadingSkill.Text = "Add Skill";

          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnSkillSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnSkillCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnSkillCancel_Click(object sender, EventArgs e)
    {
      try
      {
        hdfEmployeeSkillID.Value = "0";
        ddlSkillTechnology.ClearSelection();
        ddlSkillLevel.ClearSelection();
        ddlSkillExperienceYears.SelectedValue = "0";
        ddlSkillExperienceMonths.SelectedValue = "0";
        lblPopHeadingSkill.Text = "Add Skill";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnSkillCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Employee Experience Grid
    /// <summary>
    /// gvEmployeeExperience_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeExperience_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeExperience.PageIndex * gvEmployeeExperience.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          if (e.Row.Cells[5].Text == "&nbsp;")
            e.Row.Cells[5].Text = "";
          if (e.Row.Cells[8].Text == "0")
            e.Row.Cells[8].Text = "";
          if (e.Row.Cells[11].Text == "0")
            e.Row.Cells[11].Text = "";
          if (e.Row.Cells[13].Text == "&nbsp;")
            e.Row.Cells[13].Text = "";

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[6].Visible = false;
          e.Row.Cells[7].Visible = false;
          e.Row.Cells[9].Visible = false;
          e.Row.Cells[10].Visible = false;
          e.Row.Cells[13].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[6].Visible = false;
          e.Row.Cells[7].Visible = false;
          e.Row.Cells[9].Visible = false;
          e.Row.Cells[10].Visible = false;
          e.Row.Cells[13].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeExperience_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeExperience_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeExperience_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "OrganizationName":
        //    //Sort by Organization Name                                  
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byOrganizationName());
        //    break;
        //  case "Location":
        //    //Sort by Location                               
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byLocation());
        //    break;
        //  case "Designation":
        //    //Sort by Designation                               
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byDesignation());
        //    break;
        //  case "FromMonthAndYear":
        //    //Sort by FromMonthAndYear                                  
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byFromMonthAndYear());
        //    break;
        //  case "ToMonthAndYear":
        //    //Sort by ToMonthAndYear                                 
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byToMonthAndYear());
        //    break;
        //  case "CTC":
        //    //Sort by CTC                                  
        //    _currentEmployee.EmployeeExperienceDetails.Sort(new EmployeeExperienceComparer_byCTC());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeExperienceDetails.Reverse();

        // Assign the list of Employee Experience Details after sorting to the grid 
        gvEmployeeExperience.DataSource = _currentEmployee.EmployeeExperienceDetails;
        gvEmployeeExperience.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeExperience_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeExperience_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeExperience_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeExperienceID.Value = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[1].Text;

        Label lbtnOrgName = (Label)gvEmployeeExperience.Rows[e.NewEditIndex].FindControl("lbtnOrganizationName");
        txtExperienceOrganization.Text = lbtnOrgName.Text;
        //txtExperienceOrganization.Text = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[3].Text;
        txtOrganizationLocation.Text = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[4].Text;

        txtOrganizationDesignation.Text = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[5].Text;
        ddlExperienceFromMonth.SelectedValue = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[6].Text;
        ddlExperienceFromYear.SelectedValue = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[7].Text;

        ddlExperienceToMonth.SelectedValue = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[9].Text;
        ddlExperienceToYear.SelectedValue = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[10].Text;

        txtCTC.Text = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[12].Text;
        txtJobProfile.Text = gvEmployeeExperience.Rows[e.NewEditIndex].Cells[13].Text;

        lblPopupHeadingExperience.Text = "Edit Experience";
        // Show the Popup
        mpeEditExperience.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeExperience_RowEditing", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeExperience_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeExperience_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Experience id
        int eExperienceIDToDelete = Convert.ToInt32(gvEmployeeExperience.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Experience
        EmployeeExperienceDL deleteEmployeeExperience = new EmployeeExperienceDL(eExperienceIDToDelete, false);
        deleteEmployeeExperience.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeExperience.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeExperience_RowDeleting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeExperience_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeExperience_SelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        // Get the details of the selected employee experience
        int selEmployeeExperienceID = (int)gvEmployeeExperience.SelectedValue;
        string jProfile = gvEmployeeExperience.SelectedRow.Cells[10].Text.ToString();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeeExperience_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnExperienceSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnExperienceSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Experience Popup Form
        if (!ExperiencePopupValidation()) { return; }

        // Create a new EmployeeExperience Object
        EmployeeExperienceDL empExperience = new EmployeeExperienceDL();

        // Set whether Add / Edit
        if (hdfEmployeeExperienceID.Value.ToString() != "0")
          empExperience.AddEditOption = 1;
        else
          empExperience.AddEditOption = 0;

        // Assign values to the EmployeeExperience Object
        empExperience.EmployeeExperienceID = Convert.ToInt32(hdfEmployeeExperienceID.Value);
        empExperience.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empExperience.OrganizationName = txtExperienceOrganization.Text;
        empExperience.Location = txtOrganizationLocation.Text;
        empExperience.Designation = txtOrganizationDesignation.Text;
        empExperience.FromMonth = Convert.ToInt32(ddlExperienceFromMonth.SelectedValue);
        empExperience.FromYear = Convert.ToInt32(ddlExperienceFromYear.SelectedValue);
        empExperience.ToMonth = Convert.ToInt32(ddlExperienceToMonth.SelectedValue);
        empExperience.ToYear = Convert.ToInt32(ddlExperienceToYear.SelectedValue);
        empExperience.CTC = Convert.ToDecimal(txtCTC.Text);
        empExperience.JobProfile = txtJobProfile.Text;

        // Add / Edit the Employee Experience
        TransactionResult result;
        empExperience.ScreenMode = ScreenMode.Add;
        result = empExperience.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeExperienceID.Value = "0";
          txtExperienceOrganization.Text = "";
          txtOrganizationLocation.Text = "";
          ddlExperienceFromMonth.SelectedIndex = 0;
          ddlExperienceToMonth.SelectedIndex = 0;
          ddlExperienceFromYear.SelectedIndex = 0;
          ddlExperienceToYear.SelectedIndex = 0;
          txtCTC.Text = "";
          txtJobProfile.Text = "";
          txtOrganizationDesignation.Text = "";

          lblPopupHeadingExperience.Text = "Add Experience";

          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnExperienceSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnExperienceCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnExperienceCancel_Click(object sender, EventArgs e)
    {
      try
      {
        hdfEmployeeExperienceID.Value = "0";
        txtExperienceOrganization.Text = "";
        txtOrganizationLocation.Text = "";
        ddlExperienceFromMonth.SelectedIndex = 0;
        ddlExperienceToMonth.SelectedIndex = 0;
        ddlExperienceFromYear.SelectedIndex = 0;
        ddlExperienceToYear.SelectedIndex = 0;

        txtCTC.Text = "";
        txtJobProfile.Text = "";
        txtOrganizationDesignation.Text = "";

        lblPopupHeadingExperience.Text = "Add Experience";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnExperienceCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeDesignationHistory_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDesignationHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          string dtFormat = _dateFormat;
          DateTime dFromTime;
          DateTime dToTime;
          if (e.Row.Cells[3].Text != "&nbsp;" && e.Row.Cells[4].Text != "&nbsp;")
          {
            dFromTime = Convert.ToDateTime(e.Row.Cells[3].Text);
            dToTime = Convert.ToDateTime(e.Row.Cells[4].Text);
            e.Row.Cells[3].Text = dFromTime.ToString(dtFormat);
            e.Row.Cells[4].Text = dToTime.ToString(dtFormat);
          }
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeDesignationHistory.PageIndex * gvEmployeeDesignationHistory.PageSize) + e.Row.RowIndex + 1).ToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeDesignationHistory_RowDataBound", ex.Message, new ACEConnection());
      }
    }

    #endregion

    #region Employee Previous Employers Projects Grid
    /// <summary>
    /// gvEmployeePreviousProject_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePreviousProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeePreviousProject.PageIndex * gvEmployeePreviousProject.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          if (e.Row.Cells[4].Text == "&nbsp;")
          {
            e.Row.Cells[4].Text = "";
          }
          if (e.Row.Cells[5].Text == "False")
          {
            e.Row.Cells[5].Text = "No";
          }
          else
          {
            e.Row.Cells[5].Text = "Yes";
          }

          if (e.Row.Cells[6].Text == "&nbsp;")
          {
            e.Row.Cells[6].Text = "";
          }
          if (e.Row.Cells[7].Text == "&nbsp;")
          {
            e.Row.Cells[7].Text = "";
          }

          if (e.Row.Cells[8].Text == "&nbsp;")
          {
            e.Row.Cells[8].Text = "";
          }
          if (e.Row.Cells[11].Text == "0")
          {
            e.Row.Cells[11].Text = "";
          }
          if (e.Row.Cells[14].Text == "0")
          {
            e.Row.Cells[14].Text = "";
          }
          if (e.Row.Cells[16].Text == "&nbsp;")
          {
            e.Row.Cells[16].Text = "";
          }

          if (e.Row.Cells[17].Text == "&nbsp;")
          {
            e.Row.Cells[17].Text = "";
          }

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[9].Visible = false;
          e.Row.Cells[10].Visible = false;
          e.Row.Cells[12].Visible = false;
          e.Row.Cells[13].Visible = false;
          e.Row.Cells[17].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[9].Visible = false;
          e.Row.Cells[10].Visible = false;
          e.Row.Cells[12].Visible = false;
          e.Row.Cells[13].Visible = false;
          e.Row.Cells[17].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePreviousProject_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePreviousProject_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePreviousProject_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "ProjectName":
        //    //Sort by Project Name                                 
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byProjectName());
        //    break;
        //  case "ClientName":
        //    //Sort by Client Name                               
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byClientName());
        //    break;
        //  case "Technology":
        //    //Sort by Technology                                 
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byTechnology());
        //    break;
        //  case "Domain":
        //    //Sort by Domain                                  
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byDomain());
        //    break;
        //  case "FromMonthAndYear":
        //    //Sort by FromMonthAndYear                                  
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byFromMonthAndYear());
        //    break;
        //  case "ToMonthAndYear":
        //    //Sort by ToMonthAndYear                                 
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byToMonthAndYear());
        //    break;
        //  case "TeamSize":
        //    //Sort by Team Size                                  
        //    _currentEmployee.EmployeePreviousProjects.Sort(new EmployeePreviousEmployersProjectComparer_byTeamSize());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeePreviousProjects.Reverse();

        // Assign the list of Employee Previous Employers Project Details after sorting to the grid 
        gvEmployeePreviousProject.DataSource = _currentEmployee.EmployeePreviousProjects;
        gvEmployeePreviousProject.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePreviousProject_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePreviousProject_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePreviousProject_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeePreviousEmployersProjectID.Value = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[1].Text;

        LinkButton lbtnProjName = (LinkButton)gvEmployeePreviousProject.Rows[e.NewEditIndex].FindControl("lbtnProjectName");
        txtPreviousProjectName.Text = lbtnProjName.Text;
        txtPreviousProjectClientName.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[4].Text;

        // Is Onsite

        bool changeType = false;

        if (gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[5].Text.ToString() == "Yes")
          changeType = true;

        chkIsOnsite.Checked = changeType;

        txtOnsiteLocation.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[6].Text;

        txtPreviousProjectTechnology.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[7].Text;
        txtPreviousProjectDomain.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[8].Text;


        ddlPreviousProjectFromMonth.SelectedValue = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[9].Text;
        ddlPreviousProjectFromYear.SelectedValue = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[10].Text;

        ddlPreviousProjectToMonth.SelectedValue = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[12].Text;
        ddlPreviousProjectToYear.SelectedValue = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[13].Text;

        txtPreviousProjectTeamSize.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[15].Text;
        txtPreviousProjectRolePlayed.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[16].Text;

        txtPreviousProjectDescription.Text = gvEmployeePreviousProject.Rows[e.NewEditIndex].Cells[17].Text;

        lblPopupHeadingPreProject.Text = "Edit Previous Project";
        // Show the Popup
        mpeEditPreviousProject.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePreviousProject_RowEditing", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePreviousProject_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePreviousProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Previous Employers Project id
        int ePreviousProjectIDToDelete = Convert.ToInt32(gvEmployeePreviousProject.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Previous Employers Project
        EmployeePreviousEmployersProjectDL deletePreviousProject = new EmployeePreviousEmployersProjectDL(ePreviousProjectIDToDelete, false);
        deletePreviousProject.ScreenMode = ScreenMode.Delete;
        result = deletePreviousProject.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePreviousProject_RowDeleting", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnPreviousProjectSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnPreviousProjectSave_Click(object sender,EventArgs e)
    {
      try
      {
        // Validate the entries in Previous Projects Popup Form
        if (!PreviousProjectsPopupValidation()) { return; }

        // Create a new EmployeePreviousEmployersProject Object
        EmployeePreviousEmployersProjectDL empPrevProject = new EmployeePreviousEmployersProjectDL();

        // Set whether Add / Edit
        if (hdfEmployeePreviousEmployersProjectID.Value.ToString() != "0")
          empPrevProject.AddEditOption = 1;
        else
          empPrevProject.AddEditOption = 0;

        // Assign values to the EmployeePreviousEmployersProject Object
        empPrevProject.EmployeePreviousEmployersProjectID = Convert.ToInt32(hdfEmployeePreviousEmployersProjectID.Value);
        empPrevProject.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);
        empPrevProject.ProjectName = txtPreviousProjectName.Text;
        empPrevProject.ClientName = txtPreviousProjectClientName.Text;
        empPrevProject.IsOnsite = chkIsOnsite.Checked;
        if (chkIsOnsite.Checked == true)
        {
          empPrevProject.OnsiteLocation = txtOnsiteLocation.Text;
        }
        else
        {
          empPrevProject.OnsiteLocation = "";
        }
        empPrevProject.Technology = txtPreviousProjectTechnology.Text;
        empPrevProject.Domain = txtPreviousProjectDomain.Text;
        empPrevProject.FromMonth = Convert.ToInt32(ddlPreviousProjectFromMonth.SelectedValue);
        empPrevProject.FromYear = Convert.ToInt32(ddlPreviousProjectFromYear.SelectedValue);
        empPrevProject.ToMonth = Convert.ToInt32(ddlPreviousProjectToMonth.SelectedValue);
        empPrevProject.ToYear = Convert.ToInt32(ddlPreviousProjectToYear.SelectedValue);
        empPrevProject.TeamSize = Convert.ToInt32(txtPreviousProjectTeamSize.Text);
        empPrevProject.RolePlayed = txtPreviousProjectRolePlayed.Text;
        empPrevProject.ProjectDescription = txtPreviousProjectDescription.Text;

        // Add / Edit the Employee Previous Project
        TransactionResult result;
        empPrevProject.ScreenMode = ScreenMode.Add;
        result = empPrevProject.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeePreviousEmployersProjectID.Value = "0";
          txtPreviousProjectName.Text = "";
          txtPreviousProjectClientName.Text = "";
          chkIsOnsite.Checked = false;
          txtOnsiteLocation.Text = "";
          txtPreviousProjectTechnology.Text = "";
          txtPreviousProjectDomain.Text = "";
          ddlPreviousProjectFromMonth.SelectedIndex = 0;
          ddlPreviousProjectToMonth.SelectedIndex = 0;
          ddlPreviousProjectFromYear.SelectedIndex = 0;
          ddlPreviousProjectToYear.SelectedIndex = 0;
          txtPreviousProjectTeamSize.Text = "";
          txtPreviousProjectRolePlayed.Text = "";
          txtPreviousProjectDescription.Text = "";

          lblPopupHeadingPreProject.Text = "Add Previous Project";
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnPreviousProjectSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnPreviousProjectCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnPreviousProjectCancel_Click(object sender, EventArgs e)
    {
      try
      {
        hdfEmployeePreviousEmployersProjectID.Value = "0";
        txtPreviousProjectName.Text = "";
        txtPreviousProjectClientName.Text = "";
        chkIsOnsite.Checked = false;
        txtOnsiteLocation.Text = "";
        txtPreviousProjectTechnology.Text = "";
        txtPreviousProjectDomain.Text = "";
        ddlPreviousProjectFromMonth.SelectedIndex = 0;
        ddlPreviousProjectToMonth.SelectedIndex = 0;
        ddlPreviousProjectFromYear.SelectedIndex = 0;
        ddlPreviousProjectToYear.SelectedIndex = 0;
        txtPreviousProjectTeamSize.Text = "";
        txtPreviousProjectRolePlayed.Text = "";
        txtPreviousProjectDescription.Text = "";
        lblPopupHeadingPreProject.Text = "Add Previous Project";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnPreviousProjectCancel_Click", ex.Message, new ACEConnection());
      }
    }

    #endregion

    #region Employee Present Employer Projects Grid
    /// <summary>
    /// gvEmployeePresentProject_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePresentProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeePresentProject.PageIndex * gvEmployeePresentProject.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          // Display the Date in dd/MM/yyyy format
          string dtFormat = _dateFormat;
          DateTime dTime;
          if (e.Row.Cells[7].Text != "&nbsp;")
          {
            dTime = Convert.ToDateTime(e.Row.Cells[7].Text);
            e.Row.Cells[7].Text = dTime.ToString(dtFormat);
          }
          if (e.Row.Cells[8].Text != "&nbsp;")
          {
            dTime = Convert.ToDateTime(e.Row.Cells[8].Text);
            e.Row.Cells[8].Text = dTime.ToString(dtFormat);
          }

          // Hide Columns
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[9].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.Header)
        {
          // Hide Column Headers
          e.Row.Cells[1].Visible = false;
          e.Row.Cells[2].Visible = false;
          e.Row.Cells[3].Visible = false;
          e.Row.Cells[9].Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePresentProject_RowDataBound", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePresentProject_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePresentProject_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);

        // Select the Order of Sorting - Whether Ascending or Descending
        if (ViewState["SortDirection"].ToString() == "ASC")
          ViewState["SortDirection"] = "DESC";
        else
          ViewState["SortDirection"] = "ASC";

        // The column to sort on
        ViewState["SortExpression"] = e.SortExpression.ToString();

        //// Sort the list based on the columns 
        //switch (e.SortExpression.ToString())
        //{
        //  case "ProjectName":
        //    //Sort by Project Name                                 
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byProjectName());
        //    break;
        //  case "ClientName":
        //    //Sort by Client Name                               
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byClientName());
        //    break;
        //  case "Domain":
        //    //Sort by Domain                                  
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byDomain());
        //    break;
        //  case "FromDate":
        //    //Sort by FromDate                                  
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byFromDate());
        //    break;
        //  case "ToDate":
        //    //Sort by ToMonthAndYear                                 
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byFromDate());
        //    break;
        //  case "JobRoleDescription":
        //    //Sort by JobRole Description                                  
        //    _currentEmployee.EmployeePresentProjects.Sort(new EmployeePresentEmployerProjectComparer_byJobRoleDesc());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeePresentProjects.Reverse();

        // Assign the list of Employee Present Employer Project Details after sorting to the grid 
        gvEmployeePresentProject.DataSource = _currentEmployee.EmployeePresentProjects;
        gvEmployeePresentProject.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePresentProject_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePresentProject_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePresentProject_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeePresentEmployerProjectID.Value = gvEmployeePresentProject.Rows[e.NewEditIndex].Cells[1].Text;

        ddlPresentProjectName.ClearSelection();
        ListItem liPresentProjectName = ddlPresentProjectName.Items.FindByValue(gvEmployeePresentProject.Rows[e.NewEditIndex].Cells[3].Text);
        ddlPresentProjectName.SelectedIndex = ddlPresentProjectName.Items.IndexOf(liPresentProjectName);

        txtPresentProjectFromDate.Text = gvEmployeePresentProject.Rows[e.NewEditIndex].Cells[7].Text;
        if (txtPresentProjectFromDate.Text == "&nbsp;")
          txtPresentProjectFromDate.Text = "";

        txtPresentProjectToDate.Text = gvEmployeePresentProject.Rows[e.NewEditIndex].Cells[8].Text;
        if (txtPresentProjectToDate.Text == "&nbsp;")
          txtPresentProjectToDate.Text = "";

        ddlPresentProjectJobRole.ClearSelection();
        ListItem liPresentProjectJobRole = ddlPresentProjectJobRole.Items.FindByValue(gvEmployeePresentProject.Rows[e.NewEditIndex].Cells[9].Text);
        ddlPresentProjectJobRole.SelectedIndex = ddlPresentProjectJobRole.Items.IndexOf(liPresentProjectJobRole);

        lblPopupHeadingPresProject.Text = "Edit Present Project";
        // Show the Popup
        mpeEditPresentProject.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePresentProject_RowEditing", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeePresentProject_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeePresentProject_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee Present Employer Project id
        int ePresentProjectIDToDelete = Convert.ToInt32(gvEmployeePresentProject.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee Present Employer Project
        EmployeePresentEmployerProjectDL deletePresentProject = new EmployeePresentEmployerProjectDL(ePresentProjectIDToDelete, false);
        deletePresentProject.ScreenMode = ScreenMode.Delete;
        result = deletePresentProject.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "gvEmployeePresentProject_RowDeleting", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnPresentProjectSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnPresentProjectSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in Present Projects Popup Form
        if (!PresentProjectsPopupValidation()) { return; }

        // Create a new EmployeePresentEmployerProject Object
        EmployeePresentEmployerProjectDL empCurrProject = new EmployeePresentEmployerProjectDL();

        // Set whether Add / Edit
        if (hdfEmployeePresentEmployerProjectID.Value.ToString() != "0")
          empCurrProject.AddEditOption = 1;
        else
          empCurrProject.AddEditOption = 0;

        // Assign values to the EmployeePresentEmployerProject Object
        if (hdfEmployeePresentEmployerProjectID.Value.ToString() != "0")
        {
          empCurrProject.EmployeePresentEmployerProjectID = Convert.ToInt32(hdfEmployeePresentEmployerProjectID.Value);
        }
        else
        {
          empCurrProject.EmployeePresentEmployerProjectID = Convert.ToInt32(ddlPresentProjectName.SelectedValue);
        }

        empCurrProject.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value);

        //if (ddlPresentProjectName.SelectedValue.ToString() != "" && ddlPresentProjectName.SelectedValue.ToString() != "0" && ddlPresentProjectName.SelectedValue.ToString() != null)
        if (ddlPresentProjectName.SelectedIndex > 0)
        {
          empCurrProject.ProjectID = Convert.ToInt32(ddlPresentProjectName.SelectedValue);
        }

        string dtFormat = _dateFormat;
        //DateTime FromDate =  DateTime.ParseExact(txtPresentProjectFromDate.Text,dtFormat,null);
        String FromDate = txtPresentProjectFromDate.Text.ToString();
        String ToDate = "";

        if (FromDate.ToString() != "" && FromDate.ToString() != null)
        {
          empCurrProject.FromDate = DateTime.ParseExact(FromDate, dtFormat, null);
        }
        else
        {
          empCurrProject.FromDate = null;
        }

        if (txtPresentProjectToDate.Text.ToString() != "" && txtPresentProjectToDate.Text.ToString() != null)
        {
          ToDate = txtPresentProjectToDate.Text.ToString();
          empCurrProject.ToDate = DateTime.ParseExact(ToDate, dtFormat, null);
        }
        else
        {
          empCurrProject.ToDate = null;
        }

        if (FromDate.ToString() != "" && FromDate.ToString() != null && ToDate.ToString() != "" && ToDate.ToString() != null)
        {
          if (DateTime.ParseExact(ToDate, dtFormat, null) < DateTime.ParseExact(FromDate, dtFormat, null))
          {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "End Date Should Be Greater Than or Equal To Start Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            return;
          }
        }

        //Verfiy Employee Project Start Date with that of the Project's Start date 

        String ProjectStartDate;
        String ProjectEndDate = null; ;

        DataTable dt = empCurrProject.VerifyEmployeeProjectDate();

        ProjectStartDate = dt.Rows[0]["StartDate"].ToString();

        if (ProjectStartDate == "" || ProjectStartDate == null)
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Warning: Project start date is not available. Failure Adding." + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          return;
        }

        if (dt.Rows[0]["EndDate"].ToString() != "" && dt.Rows[0]["EndDate"].ToString() != null)
        {
          ProjectEndDate = dt.Rows[0]["EndDate"].ToString();
        }

        if (FromDate.ToString() != "" && FromDate.ToString() != null)
        {
          if (DateTime.ParseExact(FromDate, dtFormat, null) < DateTime.ParseExact(ProjectStartDate, dtFormat, null))
          {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Employee Project Start Date Should Be Greater Than or Equal To Project Start Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            return;
          }
        }
        if (ToDate.ToString() != "" && ToDate.ToString() != null)
        {
          if (DateTime.ParseExact(ToDate, dtFormat, null) > DateTime.ParseExact(ProjectEndDate, dtFormat, null))
          {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Employee Project End Date Should Be Lesser Than or Equal To Project End Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            return;
          }
        }

        if (ddlPresentProjectJobRole.SelectedIndex > 0)
        {
          empCurrProject.JobRoleID = Convert.ToInt32(ddlPresentProjectJobRole.SelectedValue);
        }

        // Add / Edit the Employee Previous Project
        TransactionResult result;
        empCurrProject.ScreenMode = ScreenMode.Add;
        result = empCurrProject.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
        sb1.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb1.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {

          hdfEmployeePresentEmployerProjectID.Value = "0";
          ddlPresentProjectName.ClearSelection();
          txtPresentProjectFromDate.Text = "";
          txtPresentProjectToDate.Text = "";
          ddlPresentProjectJobRole.ClearSelection();

          lblPopupHeadingPresProject.Text = "Add Present Project";
          GetEmployeeDetails(Convert.ToInt32(hdfEmployeeID.Value), true);
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ibtnPresentProjectSave_Click", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnPresentProjectCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnPresentProjectCancel_Click(object sender,EventArgs e)
    {

    }

    /// <summary>
    /// lbtnPresentProject_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnPresentProject_Click(object sender, EventArgs e)
    {
      ddlPresentProjectName.ClearSelection();
      txtPresentProjectFromDate.Text = "";
      txtPresentProjectToDate.Text = "";
      ddlPresentProjectJobRole.ClearSelection();
      lblPopupHeadingPresProject.Text = "Add Present Project";
      mpeEditPresentProject.Show();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// To retrieve the details of a particular employee
    /// </summary>
    /// <param name="employeeID">employeeID</param>
    /// <param name="isProperties">Whether all properties</param>
    private void GetEmployeeDetails(int employeeID, bool isProperties)
    {
      try
      {
        _currentEmployee = new EmployeeDL(employeeID, isProperties);
        AssignValues();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "GetEmployeeDetails(int employeeID, bool isProperties)", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Values of the form are set
    /// </summary
    private void AssignValues()
    {
      try
      {
        // Assign the Employee Details to the Form Labels
        lblEmployeeCodeValue.Text = Common.CheckBlank(_currentEmployee.EmployeeCode);
        lblFNameValue.Text = Common.CheckBlank(_currentEmployee.FName) + " " + _currentEmployee.LName;
        lblInitialValue.Text = Common.CheckBlank(_currentEmployee.Initial);
        lblGenderValue.Text = Common.CheckBlank(_currentEmployee.GenderDescription);
        lblDsgDesc.Text = Common.CheckBlank(_currentEmployee.DesignationDescription);
        // lblClientNameValue.Text = Common.CheckBlank(_currentEmployee.ClientName);
        EmployeeJobStatusDL _employeeJobStatus = new EmployeeJobStatusDL();
        string jobstatus = _employeeJobStatus.GetEmployeeJobStatusByEmployeeID(Convert.ToInt32(hdfEmployeeID.Value)).ToString();

        lblEmployeeStatus.Text = jobstatus.ToString();

        DateTime tempDt = (DateTime)_currentEmployee.Dob;

        //string dFormat = Session["dFormat"]; 
        string dtFormat = _dateFormat;
        lblDateOfBirthValue.Text = tempDt.ToString(dtFormat);
        lblDepartmentValue.Text = Common.CheckBlank(_currentEmployee.DepartmentDescription);

        if (_currentEmployee.Doj != null)
        {
          tempDt = (DateTime)_currentEmployee.Doj;
          lblDateOfJoiningValue.Text = tempDt.ToString(dtFormat);
        }
        lblOfficialMailIDValue.Text = Common.CheckBlank(_currentEmployee.OfficeEmailID);

        // General Info

        hdfTitleID.Value = _currentEmployee.TitleID.ToString();
        lblTitleValue.Text = Common.CheckBlank(_currentEmployee.TitleDescription);
        if (Convert.ToInt32(hdfTitleID.Value) != 0)
        {
          ddlTitleEdit.ClearSelection();
          ddlTitleEdit.Items.FindByValue(Convert.ToString(hdfTitleID.Value)).Selected = true;
        }

        hdfNationalityID.Value = _currentEmployee.NationalityID.ToString();
        lblNationalityValue.Text = Common.CheckBlank(_currentEmployee.NationalityDescription);
        if (Convert.ToInt32(hdfNationalityID.Value) != 0)
        {
          ddlNationalityEdit.ClearSelection();
          ddlNationalityEdit.Items.FindByValue(Convert.ToString(hdfNationalityID.Value)).Selected = true;
        }

        hdfReligionID.Value = _currentEmployee.ReligionID.ToString();
        lblReligionValue.Text = Common.CheckBlank(_currentEmployee.ReligionDescription);
        if (Convert.ToInt32(hdfReligionID.Value) != 0)
        {
          ddlReligionEdit.ClearSelection();
          ddlReligionEdit.Items.FindByValue(Convert.ToString(hdfReligionID.Value)).Selected = true;
        }

        hdfBloodGroupID.Value = _currentEmployee.BloodGroupID.ToString();
        lblBloodGroupValue.Text = Common.CheckBlank(_currentEmployee.BloodGroupDescription);
        if (Convert.ToInt32(hdfBloodGroupID.Value) != 0)
        {
          ddlBloodGroupEdit.ClearSelection();
          ddlBloodGroupEdit.Items.FindByValue(Convert.ToString(hdfBloodGroupID.Value)).Selected = true;
        }

        hdfMaritalStatusID.Value = _currentEmployee.MaritalStatusID.ToString();
        lblMaritalStatusValue.Text = Common.CheckBlank(_currentEmployee.MaritalStatusDescription);
        if (Convert.ToInt32(hdfMaritalStatusID.Value) != 0)
        {
          ddlMaritalStatusEdit.ClearSelection();
          ddlMaritalStatusEdit.Items.FindByValue(Convert.ToString(hdfMaritalStatusID.Value)).Selected = true;
        }
        if (ddlMaritalStatusEdit.SelectedItem.Text != "Married")
        {
          txtWeddingDateEdit.Enabled = false;
          lblWeddingAnniversaryValue.Text = "";
          hdfWeddingAnniversaryDate.Value = "";
          txtWeddingDateEdit.Text = "";
        }
        else
        {
          if (_currentEmployee.WeddingDate != null)
          {
            tempDt = (DateTime)_currentEmployee.WeddingDate;
            lblWeddingAnniversaryValue.Text = tempDt.ToString(dtFormat);
            hdfWeddingAnniversaryDate.Value = tempDt.ToString(dtFormat);
            txtWeddingDateEdit.Text = tempDt.ToString(dtFormat);
          }
        }

        hdfHomeTown.Value = _currentEmployee.HomeTown;
        lblHomeTownValue.Text = Common.CheckBlank(_currentEmployee.HomeTown);
        txtHomeTownEdit.Text = hdfHomeTown.Value.ToString();

        hdfMobile.Value = _currentEmployee.Mobile;
        lblMobileValue.Text = Common.CheckBlank(_currentEmployee.Mobile);
        txtMobileEdit.Text = hdfMobile.Value.ToString();

        //
        ddlPresentAddressCountry.SelectedIndex = 0;
        ddlPermanentAddressCountry.SelectedIndex = 0;

        // Address Info

        hdfIsPresentAndPermanentAddressSame.Value = _currentEmployee.IsPresentAndPermanentAddressSame.ToString();
        chkIsPresentAddrSameAsPermanent.Checked = _currentEmployee.IsPresentAndPermanentAddressSame;
        AddressType addrType = new AddressType();
        foreach (EmployeeAddressDL eAddress in _currentEmployee.EmployeeAddresses)
        {
          addrType = (AddressType)eAddress.AddressTypeID;
          if (addrType == AddressType.PresentAddress)
          {
            hdfPresentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

            hdfPresentAddress1.Value = eAddress.AddressInfo.Address1;
            lblPresentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
            txtPresentAddress1.Text = hdfPresentAddress1.Value.ToString();

            hdfPresentAddress2.Value = eAddress.AddressInfo.Address2;
            lblPresentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
            txtPresentAddress2.Text = eAddress.AddressInfo.Address2;

            hdfPresentAddress3.Value = eAddress.AddressInfo.Address3;
            lblPresentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
            txtPresentAddress3.Text = eAddress.AddressInfo.Address3;

            hdfCity1.Value = eAddress.AddressInfo.CityDescription;
            lblCityValue1.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
            txtPresentAddressCity.Text = eAddress.AddressInfo.CityDescription;

            hdfPinCode1.Value = eAddress.AddressInfo.PostalCode;
            lblPinCodeValue1.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
            txtPresentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

            hdfPresentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
            lblCountryValue1.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
            if (eAddress.AddressInfo.CountryID != 0)
            {
              int ctryID = eAddress.AddressInfo.CountryID;
              HttpContext.Current.Cache.Add("CountryID", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }

            hdfPresentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
            lblStateValue1.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
            if (eAddress.AddressInfo.StateID != 0)
            {
              int sID = eAddress.AddressInfo.StateID;
              HttpContext.Current.Cache.Add("StateID", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              cddPresentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
              cddPresentAddressStates.ContextKey = sID.ToString();
            }

            hdfPresentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
            lblDistrictValue1.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
            if (eAddress.AddressInfo.DistrictID != 0)
            {
              int dID = eAddress.AddressInfo.DistrictID;
              HttpContext.Current.Cache.Add("DistrictID", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              cddPresentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
              cddPresentAddressDistricts.ContextKey = dID.ToString();
            }

            hdfPhone1.Value = eAddress.Phone;
            lblPhoneValue1.Text = Common.CheckBlank(eAddress.Phone);
            txtPresentAddressPhone.Text = eAddress.Phone;

            // Copy to Permanent
            if (chkIsPresentAddrSameAsPermanent.Checked == true)
            {
              hdfPermanentEmployeeAddressID.Value = "0";

              hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
              lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
              txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

              hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
              lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
              txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

              hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
              lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
              txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

              hdfCity2.Value = eAddress.AddressInfo.CityDescription;
              lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
              txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

              hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
              lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
              txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

              hdfPermanentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
              lblCountryValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
              if (eAddress.AddressInfo.CountryID != 0)
              {
                int ctryID = eAddress.AddressInfo.CountryID;
                HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              }

              hdfPermanentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
              lblStateValue2.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
              if (eAddress.AddressInfo.StateID != 0)
              {
                int sID = eAddress.AddressInfo.StateID;
                HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
                cddPermanentAddressStates.ContextKey = sID.ToString();
              }

              hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
              lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
              if (eAddress.AddressInfo.DistrictID != 0)
              {
                int dID = eAddress.AddressInfo.DistrictID;
                HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                    new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
                cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
                cddPermanentAddressDistricts.ContextKey = dID.ToString();
              }

              hdfPhone2.Value = eAddress.Phone;
              lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
              txtPermanentAddressPhone.Text = eAddress.Phone;
            }
          }
          if (addrType == AddressType.PermanentAddress)
          {
            hdfPermanentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

            hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
            lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
            txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

            hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
            lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
            txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

            hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
            lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
            txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

            hdfCity2.Value = eAddress.AddressInfo.CityDescription;
            lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
            txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

            hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
            lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
            txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

            hdfPermanentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
            lblCountryValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
            if (eAddress.AddressInfo.CountryID != 0)
            {
              int ctryID = eAddress.AddressInfo.CountryID;
              HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            }

            hdfPermanentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
            lblStateValue2.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
            if (eAddress.AddressInfo.StateID != 0)
            {
              int sID = eAddress.AddressInfo.StateID;
              HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
              cddPermanentAddressStates.ContextKey = sID.ToString();
            }

            hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
            lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
            if (eAddress.AddressInfo.DistrictID != 0)
            {
              int dID = eAddress.AddressInfo.DistrictID;
              HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
              cddPermanentAddressDistricts.ContextKey = dID.ToString();
            }

            hdfPhone2.Value = eAddress.Phone;
            lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
            txtPermanentAddressPhone.Text = eAddress.Phone;
          }
        }

        // Additional Info
        hdfPAN.Value = _currentEmployee.Pan;
        lblPANValue.Text = Common.CheckBlank(_currentEmployee.Pan);
        txtPANEdit.Text = _currentEmployee.Pan;

        hdfBankAccountNumber.Value = _currentEmployee.BankAccountNumber;
        lblBankAccountNumberValue.Text = Common.CheckBlank(_currentEmployee.BankAccountNumber);
        txtBankAccountNumberEdit.Text = _currentEmployee.BankAccountNumber;

        hdfBankName.Value = _currentEmployee.BankName;
        lblBankNameValue.Text = Common.CheckBlank(_currentEmployee.BankName);
        txtBankNameEdit.Text = _currentEmployee.BankName;

        hdfPassportNumber.Value = _currentEmployee.PassportNo;
        lblPassportNumberValue.Text = Common.CheckBlank(_currentEmployee.PassportNo);
        txtPassportNumberEdit.Text = _currentEmployee.PassportNo;

        DateTime dt = new DateTime();

        if (_currentEmployee.JobStartTime != null)
        {
          dt = _currentEmployee.JobStartTime.Value;

          string strMinute = "";

          if (dt.Minute.ToString() == "0")
          {
            strMinute = "00";
          }
          else
          {
            strMinute = dt.Minute.ToString();
          }
          lblJobStartingTime.Text = dt.Hour.ToString() + ":" + strMinute;
        }
        else
        {
          lblJobStartingTime.Text = "--";
        }

        if (_currentEmployee.PassportDateIssue != null)
        {
          tempDt = (DateTime)_currentEmployee.PassportDateIssue;
          lblDateOfIssueValue.Text = tempDt.ToString(dtFormat);
          hdfDateOfIssue.Value = tempDt.ToString(dtFormat);
          txtPassportDateOfIssueEdit.Text = tempDt.ToString(dtFormat);
        }
        else
        {
          hdfDateOfIssue.Value = "";
          lblDateOfIssueValue.Text = "";
          txtPassportDateOfIssueEdit.Text = "";
        }
        if (_currentEmployee.PassportDateExpiry != null)
        {
          tempDt = (DateTime)_currentEmployee.PassportDateExpiry;
          lblDateOfExpiryValue.Text = tempDt.ToString(dtFormat);
          hdfDateOfExpiry.Value = tempDt.ToString(dtFormat);
          txtPassportDateOfExpiryEdit.Text = tempDt.ToString(dtFormat);
        }
        else
        {
          lblDateOfExpiryValue.Text = "";
          hdfDateOfExpiry.Value = "";
          txtPassportDateOfExpiryEdit.Text = "";
        }

        hdfPlaceOfIssue.Value = _currentEmployee.PassportIssuePlace;
        lblPlaceOfIssueValue.Text = Common.CheckBlank(_currentEmployee.PassportIssuePlace);
        txtPassportPlaceOfIssueEdit.Text = _currentEmployee.PassportIssuePlace;

        hdfPersonalEmailID.Value = _currentEmployee.PersonalEmailID;
        lblPersonalEmailIDValue.Text = Common.CheckBlank(_currentEmployee.PersonalEmailID);
        txtPersonalMailIDEdit.Text = _currentEmployee.PersonalEmailID;

        hdfMessengerID.Value = _currentEmployee.MessengerID;
        lblMessengerIDValue.Text = Common.CheckBlank(_currentEmployee.MessengerID);
        txtMessengerIDEdit.Text = _currentEmployee.MessengerID;

        hdfICEName1.Value = _currentEmployee.IceName;
        lblICENameValue1.Text = Common.CheckBlank(_currentEmployee.IceName);
        txtICENameEdit1.Text = _currentEmployee.IceName;

        hdfICERelationshipID1.Value = _currentEmployee.IceRelationshipID.ToString();
        lblICERelationshipValue1.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription);
        if (_currentEmployee.IceRelationshipID != 0)
        {
          ddlICERelationshipEdit1.ClearSelection();
          ddlICERelationshipEdit1.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID)).Selected = true;
        }

        hdfICEPhone1.Value = _currentEmployee.IceMobile;
        lblICEPhoneValue1.Text = Common.CheckBlank(_currentEmployee.IceMobile);
        txtICEPhoneEdit1.Text = _currentEmployee.IceMobile;

        hdfICEName2.Value = _currentEmployee.IceName2;
        lblICENameValue2.Text = Common.CheckBlank(_currentEmployee.IceName2);
        txtICENameEdit2.Text = _currentEmployee.IceName2;

        hdfICERelationshipID2.Value = _currentEmployee.IceRelationshipID2.ToString();
        lblICERelationshipValue2.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription2);
        if (_currentEmployee.IceRelationshipID2 != 0)
        {
          ddlICERelationshipEdit2.ClearSelection();
          ddlICERelationshipEdit2.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID2)).Selected = true;
        }

        hdfICEPhone2.Value = _currentEmployee.IceMobile2;
        lblICEPhoneValue2.Text = Common.CheckBlank(_currentEmployee.IceMobile2);
        txtICEPhoneEdit2.Text = _currentEmployee.IceMobile2;

        // Assign the list of Employee Languages Known to the grid
        gvEmployeeLanguages.DataSource = _currentEmployee.EmployeeLanguages;
        gvEmployeeLanguages.DataBind();

        // Assign the list of Employee Family to the grid
        gvEmployeeFamily.DataSource = _currentEmployee.EmployeeFamilyMembers;
        gvEmployeeFamily.DataBind();

        // Assign the list of Employee Education Details to the grid
        gvEmployeeEducation.DataSource = _currentEmployee.EmployeeEducationDetails;
        gvEmployeeEducation.DataBind();

        // Assign the list of Employee Certifications to the grid
        gvEmployeeCertification.DataSource = _currentEmployee.EmployeeCertifications;
        gvEmployeeCertification.DataBind();

        // Assign the list of Employee Skills to the grid
        gvEmployeeSkill.DataSource = _currentEmployee.EmployeeSkills;
        gvEmployeeSkill.DataBind();

        // Assign the list of Employee Experience Details to the grid
        gvEmployeeExperience.DataSource = _currentEmployee.EmployeeExperienceDetails;
        gvEmployeeExperience.DataBind();

        // Assign the list of Employee Previous Employers Project Details to the grid
        gvEmployeePreviousProject.DataSource = _currentEmployee.EmployeePreviousProjects;
        gvEmployeePreviousProject.DataBind();

        //// check whether isonsite = true in the popup Previous Projects
        txtOnsiteLocation.Text = "";

        // Assign the list of Employee Present Employer Project Details to the grid
        gvEmployeePresentProject.DataSource = _currentEmployee.EmployeePresentProjects;
        gvEmployeePresentProject.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "AssignValues", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Title
    /// </summary>
    private void LoadTitleDropDown()
    {
      try
      {
        // Load Title
        ddlTitleEdit.Items.Clear();
        ddlTitleEdit.DataSource = new TitlesDL().GetTitleList().Tables[0];
        ddlTitleEdit.DataTextField = "TitleDescription";
        ddlTitleEdit.DataValueField = "TitleID";
        ddlTitleEdit.DataBind();
        ddlTitleEdit.Items.Insert(0, "-- Select One --");
        ddlTitleEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadTitleDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Nationality 
    /// </summary>
    private void LoadNationalityDropDown()
    {
      try
      {
        // Load Nationality
        ddlNationalityEdit.Items.Clear();
        ddlNationalityEdit.DataSource = new NationalityDL().GetNationalityList().Tables[0];
        ddlNationalityEdit.DataTextField = "NationalityDescription";
        ddlNationalityEdit.DataValueField = "NationalityID";
        ddlNationalityEdit.DataBind();
        ddlNationalityEdit.Items.Insert(0, "-- Select One --");
        ddlNationalityEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadNationalityDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Religion 
    /// </summary>
    private void LoadReligionDropDown()
    {
      try
      {
        // Load Religion
        ddlReligionEdit.Items.Clear();
        ddlReligionEdit.DataSource = new ReligionDL().GetReligionList().Tables[0];
        ddlReligionEdit.DataTextField = "ReligionDescription";
        ddlReligionEdit.DataValueField = "ReligionID";
        ddlReligionEdit.DataBind();
        ddlReligionEdit.Items.Insert(0, "-- Select One --");
        ddlReligionEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadReligionDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For BloodGroup
    /// </summary>
    private void LoadBloodGroupDropDown()
    {
      try
      {
        // Load BloodGroup
        ddlBloodGroupEdit.Items.Clear();
        ddlBloodGroupEdit.DataSource = new BloodGroupDL().GetBloodGroupList().Tables[0];
        ddlBloodGroupEdit.DataTextField = "BloodGroupDescription";
        ddlBloodGroupEdit.DataValueField = "BloodGroupID";
        ddlBloodGroupEdit.DataBind();
        ddlBloodGroupEdit.Items.Insert(0, "-- Select One --");
        ddlBloodGroupEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadBloodGroupDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For MaritalStatus
    /// </summary>
    private void LoadMaritalStatusDropDown()
    {
      try
      {
        // Load MaritalStatus
        ddlMaritalStatusEdit.Items.Clear();
        ddlMaritalStatusEdit.DataSource = new MaritalStatusDL().GetMaritalStatusList().Tables[0];
        ddlMaritalStatusEdit.DataTextField = "MaritalStatusDescription";
        ddlMaritalStatusEdit.DataValueField = "MaritalStatusID";
        ddlMaritalStatusEdit.DataBind();
        ddlMaritalStatusEdit.Items.Insert(0, "-- Select One --");
        ddlMaritalStatusEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadMaritalStatusDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Relationship
    /// </summary>
    private void LoadICERelationshipsDropDown()
    {
      try
      {
        // Load Relationships
        ddlICERelationshipEdit1.Items.Clear();
        ddlICERelationshipEdit1.DataSource = new RelationshipDL().GetRelationshipList().Tables[0];
        ddlICERelationshipEdit1.DataTextField = "RelationshipDescription";
        ddlICERelationshipEdit1.DataValueField = "RelationshipID";
        ddlICERelationshipEdit1.DataBind();
        ddlICERelationshipEdit1.Items.Insert(0, "-- Select One --");
        ddlICERelationshipEdit1.Items[0].Value = "";

        ddlICERelationshipEdit2.Items.Clear();
        ddlICERelationshipEdit2.DataSource = new RelationshipDL().GetRelationshipList().Tables[0];
        ddlICERelationshipEdit2.DataTextField = "RelationshipDescription";
        ddlICERelationshipEdit2.DataValueField = "RelationshipID";
        ddlICERelationshipEdit2.DataBind();
        ddlICERelationshipEdit2.Items.Insert(0, "-- Select One --");
        ddlICERelationshipEdit2.Items[0].Value = "";

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadICERelationshipsDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load Lists For Language, Relationship 
    /// </summary>
    private void LoadDropDownLists()
    {
      try
      {
        ProjectDL _project = new ProjectDL();
        // Load Language
        ddlLanguage.Items.Clear();
        ddlLanguage.DataSource = new LanguagesDL().GetLanguageListNotInEmployeeLanguageKnown(Convert.ToInt32(hdfEmployeeID.Value)).Tables[0];
        ddlLanguage.DataTextField = "LanguageDescription";
        ddlLanguage.DataValueField = "LanguageID";
        ddlLanguage.DataBind();
        ddlLanguage.Items.Insert(0, "-- Select One --");
        ddlLanguage.Items[0].Value = "";

        // Load Relationships
        ddlRelationship.Items.Clear();
        ddlRelationship.DataSource = new RelationshipDL().GetRelationshipList().Tables[0];
        ddlRelationship.DataTextField = "RelationshipDescription";
        ddlRelationship.DataValueField = "RelationshipID";
        ddlRelationship.DataBind();
        ddlRelationship.Items.Insert(0, "-- Select One --");
        ddlRelationship.Items[0].Value = "";

        // Load Genders
        ddlGender.Items.Clear();
        ddlGender.DataSource = new GenderDL().GetGenderList().Tables[0];
        ddlGender.DataTextField = "GenderDescription";
        ddlGender.DataValueField = "GenderID";
        ddlGender.DataBind();
        ddlGender.Items.Insert(0, "-- Select One --");
        ddlGender.Items[0].Value = "";

        // Load Qualification
        ddlQualification.Items.Clear();
        ddlQualification.DataSource = new QualificationDL().GetQualificationList().Tables[0];
        ddlQualification.DataTextField = "QualificationDescription";
        ddlQualification.DataValueField = "QualificationID";
        ddlQualification.DataBind();
        ddlQualification.Items.Insert(0, "-- Select One --");
        ddlQualification.Items[0].Value = "";

        // Load Major
        ddlMajor.Items.Clear();
        ddlMajor.DataSource = new MajorDL().GetMajorList().Tables[0];
        ddlMajor.DataTextField = "MajorDescription";
        ddlMajor.DataValueField = "MajorID";
        ddlMajor.DataBind();
        ddlMajor.Items.Insert(0, "-- Select One --");
        ddlMajor.Items[0].Value = "";

        // Load University
        ddlUniversity.Items.Clear();
        ddlUniversity.DataSource = new UniversityDL().GetUniversityList().Tables[0];
        ddlUniversity.DataTextField = "UniversityDescription";
        ddlUniversity.DataValueField = "UniversityID";
        ddlUniversity.DataBind();
        ddlUniversity.Items.Insert(0, "-- Select One --");
        ddlUniversity.Items[0].Value = "";

        // Load Certification Technology
        ddlCertificationTechnology.Items.Clear();
        ddlCertificationTechnology.DataSource = new TechnologyDL().GetTechnologyList().Tables[0];
        ddlCertificationTechnology.DataTextField = "TechnologyDescription";
        ddlCertificationTechnology.DataValueField = "TechnologyID";
        ddlCertificationTechnology.DataBind();
        ddlCertificationTechnology.Items.Insert(0, "-- Select One --");
        ddlCertificationTechnology.Items[0].Value = "";

        // Load Skill Technology
        ddlSkillTechnology.Items.Clear();
        ddlSkillTechnology.DataSource = new TechnologyDL().GetTechnologyList().Tables[0];
        ddlSkillTechnology.DataTextField = "TechnologyDescription";
        ddlSkillTechnology.DataValueField = "TechnologyID";
        ddlSkillTechnology.DataBind();
        ddlSkillTechnology.Items.Insert(0, "-- Select One --");
        ddlSkillTechnology.Items[0].Value = "";

        // Load Skill Level
        ddlSkillLevel.Items.Clear();
        ddlSkillLevel.DataSource = new SkillLevelDL().GetSkillLevelList().Tables[0];
        ddlSkillLevel.DataTextField = "SkillLevelDescription";
        ddlSkillLevel.DataValueField = "SkillLevelID";
        ddlSkillLevel.DataBind();
        ddlSkillLevel.Items.Insert(0, "-- Select One --");
        ddlSkillLevel.Items[0].Value = "";

        // Load Present Project Names
        ddlPresentProjectName.Items.Clear();
        _project.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value.ToString());
        ddlPresentProjectName.DataSource = _project.GetProjectList().Tables[0];
        ddlPresentProjectName.DataTextField = "ProjectName";
        ddlPresentProjectName.DataValueField = "ProjectID";
        ddlPresentProjectName.DataBind();
        ddlPresentProjectName.Items.Insert(0, "-- Select One --");
        ddlPresentProjectName.Items[0].Value = "";

        // Load Present Project Job Roles
        ddlPresentProjectJobRole.Items.Clear();
        ddlPresentProjectJobRole.DataSource = new JobRoleDL().GetJobRoleList().Tables[0];
        ddlPresentProjectJobRole.DataTextField = "JobRoleDescription";
        ddlPresentProjectJobRole.DataValueField = "JobRoleID";
        ddlPresentProjectJobRole.DataBind();
        ddlPresentProjectJobRole.Items.Insert(0, "-- Select One --");
        ddlPresentProjectJobRole.Items[0].Value = "";

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LoadDropDownLists", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// LoadDropDownYearList
    /// </summary>
    private void LoadDropDownYearList()
    {
      int index = 1;
      int currentYear = 0;

      currentYear = Convert.ToInt32(DateTime.Now.Year.ToString());

      for (int i = 1970; i <= currentYear; i++)
      {

        ddlEducationYearOfPass.Items.Insert(index, i.ToString());
        ddlEducationYearOfPass.Items[index].Value = i.ToString();

        ddlCertificationYearOfPass.Items.Insert(index, i.ToString());
        ddlCertificationYearOfPass.Items[index].Value = i.ToString();

        ddlExperienceFromYear.Items.Insert(index, i.ToString());
        ddlExperienceFromYear.Items[index].Value = i.ToString();

        ddlExperienceToYear.Items.Insert(index, i.ToString());
        ddlExperienceToYear.Items[index].Value = i.ToString();

        ddlPreviousProjectFromYear.Items.Insert(index, i.ToString());
        ddlPreviousProjectFromYear.Items[index].Value = i.ToString();

        ddlPreviousProjectToYear.Items.Insert(index, i.ToString());
        ddlPreviousProjectToYear.Items[index].Value = i.ToString();

        index = index + 1;
      }

    }

    /// <summary>
    /// LoadDropDownSkillList
    /// </summary>
    private void LoadDropDownSkillList()
    {
      int index = 1;
      int index2 = 1;
      for (int i = 0; i <= 30; i++)
      {

        ddlSkillExperienceYears.Items.Insert(index, i.ToString());
        ddlSkillExperienceYears.Items[index].Value = i.ToString();

        index = index + 1;
      }


      for (int i = 0; i <= 11; i++)
      {

        ddlSkillExperienceMonths.Items.Insert(index2, i.ToString());
        ddlSkillExperienceMonths.Items[index2].Value = i.ToString();

        index2 = index2 + 1;
      }


    }

    /// <summary>
    /// Language Popup Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool LanguagePopupValidation()
    {
      try
      {
        bool rValue = true;

        if (chkRead.Checked == false && chkWrite.Checked == false && chkSpeak.Checked == false)
        {
          mpeEditLanguageKnown.Show();
          chkRead.Focus();
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Atleast One Should be Checked" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          rValue = false;
        }

        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "LanguagePopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }

    /// <summary>
    /// Skill Popup Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool SkillPopupValidation()
    {
      try
      {
        bool rValue = true;

        if (Convert.ToInt32(ddlSkillExperienceYears.SelectedValue) == 0 && Convert.ToInt32(ddlSkillExperienceMonths.SelectedValue) == 0)
        {
          mpeEditSkill.Show();
          ddlSkillExperienceYears.Focus();
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Experience In Years and Months - Both cannot be zero." + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          rValue = false;
        }

        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "SkillPopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }

    /// <summary>
    /// Family Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool FamilyPopupValidation()
    {
      try
      {
        bool rValue = true;
        string dtFormat = _dateFormat;
        DateTime dTime = new DateTime();
        DateTime todayDate = new DateTime();
        string sTodayDate;
        //DateTime dTime, todayDate;

        if (txtDOB.Text.ToString() != "" && txtDOB.Text.ToString() != null)
        {
          // Family Member Data of Birth Validation

          // Whether correct date format
          if (!Common.ValidateDate(txtDOB.Text.ToString(), dtFormat))
          {

            txtDOB.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - Date Of Birth" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            mpeEditFamily.Show();
            rValue = false;
          }
          else if (rValue)
          {
            sTodayDate = Convert.ToString(DateTime.Now.ToString(_dateFormat));
            dTime = DateTime.ParseExact(txtDOB.Text, dtFormat, null);
            todayDate = DateTime.ParseExact(sTodayDate, dtFormat, null);
            if (dTime > todayDate)
            {
              txtDOB.Focus();
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("<script>alert('" + "Date Of Birth - Cannot Be Greater Than Today Date" + ".');");
              sb.Append("</script>");
              ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
              rValue = false;
              mpeEditFamily.Show();
            }
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
    /// Experience Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool ExperiencePopupValidation()
    {
      try
      {
        bool rValue = true;

        // Experience From Year and To Year Validation  
        if (ddlExperienceFromYear.Text.ToString() != "" && ddlExperienceFromYear.Text.ToString() != null &&
            ddlExperienceToYear.Text.ToString() != "" && ddlExperienceToYear.Text.ToString() != null)
        {
          if (Convert.ToInt32(ddlExperienceToYear.Text) < Convert.ToInt32(ddlExperienceFromYear.Text))
          {
            mpeEditExperience.Show();
            ddlExperienceToYear.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "[To Year] Should be Greater than or Equal to [From Year]" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
          else if (Convert.ToInt32(ddlExperienceToYear.Text) == Convert.ToInt32(ddlExperienceFromYear.Text))
          {
            if (Convert.ToInt32(ddlExperienceToMonth.Text) < Convert.ToInt32(ddlExperienceFromMonth.Text))
            {
              mpeEditExperience.Show();
              ddlExperienceToYear.Focus();
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("<script>alert('" + "[To Month] Should be Greater than or Equal to [From Month] - When Years are same" + ".');");
              sb.Append("</script>");
              ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
              rValue = false;
              return rValue;
            }
          }
        }
        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "ExperiencePopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }


    /// <summary>
    /// Previous Projects Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool PreviousProjectsPopupValidation()
    {
      try
      {
        bool rValue = true;

        // Previous Projects From Year and To Year Validation                
        if (ddlPreviousProjectFromYear.SelectedValue.ToString() != "" && ddlPreviousProjectFromYear.SelectedValue.ToString() != null &&
            ddlPreviousProjectToYear.SelectedValue.ToString() != "" && ddlPreviousProjectToYear.SelectedValue.ToString() != null)
        {
          if (Convert.ToInt32(ddlPreviousProjectToYear.SelectedValue) < Convert.ToInt32(ddlPreviousProjectFromYear.SelectedValue))
          {
            mpeEditPreviousProject.Show();
            ddlPreviousProjectToYear.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "[To Year] Should be Greater than or Equal to [From Year]" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
          else if (Convert.ToInt32(ddlPreviousProjectToYear.SelectedValue) == Convert.ToInt32(ddlPreviousProjectFromYear.SelectedValue))
          {
            if (Convert.ToInt32(ddlPreviousProjectToMonth.SelectedValue) < Convert.ToInt32(ddlPreviousProjectFromMonth.SelectedValue))
            {
              mpeEditPreviousProject.Show();
              ddlPreviousProjectToYear.Focus();
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("<script>alert('" + "[To Month] Should be Greater than or Equal to [From Month] - When Years are same" + ".');");
              sb.Append("</script>");
              ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
              rValue = false;
              return rValue;
            }
          }
        }
        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "PreviousProjectsPopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }

    /// <summary>
    /// Present Projects Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool PresentProjectsPopupValidation()
    {
      try
      {
        bool rValue = true;
        string dtFormat = _dateFormat;

        // Present Projects From Date Validation

        if (txtPresentProjectFromDate.Text.ToString() != "" && txtPresentProjectFromDate.Text.ToString() != null)
        {
          // Whether correct date format
          if (!Common.ValidateDate(txtPresentProjectFromDate.Text.ToString(), dtFormat))
          {
            mpeEditPresentProject.Show();
            txtPresentProjectFromDate.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - From Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
        }
        if (txtPresentProjectToDate.Text.ToString() != "" && txtPresentProjectToDate.Text.ToString() != null)
        {
          // Whether correct date format
          if (!Common.ValidateDate(txtPresentProjectToDate.Text.ToString(), dtFormat))
          {
            mpeEditPresentProject.Show();
            txtPresentProjectToDate.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - To Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
        }
        if (txtPresentProjectFromDate.Text.ToString() != "" && txtPresentProjectFromDate.Text.ToString() != null &&
            txtPresentProjectToDate.Text.ToString() != "" && txtPresentProjectToDate.Text.ToString() != null)
        {

          DateTime FromDate = DateTime.ParseExact(txtPresentProjectFromDate.Text, dtFormat, null);
          DateTime ToDate = DateTime.ParseExact(txtPresentProjectToDate.Text, dtFormat, null);

          if (ToDate < FromDate)
          {
            mpeEditPresentProject.Show();
            txtPresentProjectToDate.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "To Date Should Be Greater Than or Equal To From Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
        }

        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "PresentProjectsPopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }


    /// <summary>
    /// Additional Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool AdditionalPopupValidation()
    {
      try
      {
        bool rValue = true;
        string dtFormat = _dateFormat;
        DateTime dTime = new DateTime();
        DateTime todayDate = new DateTime();
        string sTodayDate;

        // Passport Date of Issue Validation

        if (txtPassportDateOfIssueEdit.Text.ToString() != "" && txtPassportDateOfIssueEdit.Text.ToString() != null)
        {
          // Whether correct date format
          if (!Common.ValidateDate(txtPassportDateOfIssueEdit.Text.ToString(), dtFormat))
          {
            mpeAdditionalEdit.Show();
            txtPassportDateOfIssueEdit.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - Date Of Issue" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }

          sTodayDate = Convert.ToString(DateTime.Now.ToString(_dateFormat));
          dTime = DateTime.ParseExact(txtPassportDateOfIssueEdit.Text, dtFormat, null);
          todayDate = DateTime.ParseExact(sTodayDate, dtFormat, null);
          if (dTime > todayDate)
          {
            txtPassportDateOfIssueEdit.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Date Of Issue - Cannot be Greater than Today Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            mpeAdditionalEdit.Show();
            return rValue;
          }

        }
        if (txtPassportDateOfExpiryEdit.Text.ToString() != "" && txtPassportDateOfExpiryEdit.Text.ToString() != null)
        {
          // Whether correct date format
          if (!Common.ValidateDate(txtPassportDateOfExpiryEdit.Text.ToString(), dtFormat))
          {
            mpeAdditionalEdit.Show();
            txtPassportDateOfExpiryEdit.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - Date Of Expiry" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
        }
        if (txtPassportDateOfIssueEdit.Text.ToString() != "" && txtPassportDateOfIssueEdit.Text.ToString() != null &&
            txtPassportDateOfExpiryEdit.Text.ToString() != "" && txtPassportDateOfExpiryEdit.Text.ToString() != null)
        {

          DateTime FromDate = DateTime.ParseExact(txtPassportDateOfIssueEdit.Text, dtFormat, null);
          DateTime ToDate = DateTime.ParseExact(txtPassportDateOfExpiryEdit.Text, dtFormat, null);

          if (ToDate < FromDate)
          {
            mpeAdditionalEdit.Show();
            txtPassportDateOfExpiryEdit.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Date of Expiry should be greater than or equal to Date of Issue" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            rValue = false;
            return rValue;
          }
        }

        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "PresentProjectsPopupValidation", ex.Message, new ACEConnection());
        return false;
      }

    }

    /// <summary>
    /// General Popup Form Validation
    /// </summary>
    /// <returns>Boolean - True if Valid, Else False</returns>
    private bool GeneralPopupValidation()
    {
      try
      {
        bool rValue = true;
        string dtFormat = _dateFormat;
        DateTime dTime = new DateTime();
        DateTime todayDate = new DateTime();
        string sTodayDate;
        //DateTime dTime, todayDate;

        if (txtWeddingDateEdit.Text != "" && txtDOB.Text != null)
        {
          // Wedding Date Validation


          // Whether correct date format
          if (!Common.ValidateDate(txtWeddingDateEdit.Text, dtFormat))
          {

            txtWeddingDateEdit.Focus();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script>alert('" + "Incorrect Date - Wedding Date" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
            mpeGeneralEdit.Show();
            rValue = false;
          }
          else if (rValue)
          {
            sTodayDate = Convert.ToString(DateTime.Now.ToString(_dateFormat));
            dTime = DateTime.ParseExact(txtWeddingDateEdit.Text, dtFormat, null);
            todayDate = DateTime.ParseExact(sTodayDate, dtFormat, null);
            if (dTime > todayDate)
            {
              txtWeddingDateEdit.Focus();
              System.Text.StringBuilder sb = new System.Text.StringBuilder();
              sb.Append("<script>alert('" + "Wedding Date - Cannot Be Greater Than Today Date" + ".');");
              sb.Append("</script>");
              ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
              rValue = false;
              mpeGeneralEdit.Show();
            }
          }
        }
        return rValue;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfile.aspx", "", "GeneralPopupValidation", ex.Message, new ACEConnection());
        return false;
      }
    }

    #endregion

  }
}
