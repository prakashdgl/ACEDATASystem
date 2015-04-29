using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Data;

namespace ACE.PurchaseOrder.Employee
{
  public partial class EmployeeProfileView : System.Web.UI.Page
  {
    #region Private Variables

    // The Employee instance for viewing
    EmployeeDL _currentEmployee;
    string _dateFormat;
    int _companyID = 0;

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
        _dateFormat = Convert.ToString(Session["DateFormat"]);

        if (_dateFormat == null || _dateFormat == "")
        {
          Response.Redirect("~/Login.aspx", false);
        }

        if (!IsPostBack)
        {


          int userID = 0;
          if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
          {
            _companyID = Convert.ToInt32(Session["CompanyID"]);
            userID = Convert.ToInt32(Session["UserID"]);
            UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(userID, _companyID, 16, true);
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
        //  GridViewProperties.AssignGridViewProperties(gvEmployeeDesignationHistory);

         

          //// Get the EmployeeID QueryString Value [if QueryString exists]
          //if (Request.QueryString["EmployeeID"] != null && Request.QueryString["EmployeeID"] != "null")
          //{
          //  hdfEmployeeID.Value = Request.QueryString["EmployeeID"].ToString();

          //  // Get the details of the Employee with the EmployeeID QueryString Value
          //  GetEmployeeDetails(Convert.ToInt32(Request.QueryString["EmployeeID"].ToString()), true);

          //  trEmployeeDetailView.Visible = true;
          //}
          //else if (Session["EmployeeID"] != null && Session["EmployeeID"].ToString() != "null")
          //{
          //  hdfEmployeeID.Value = Session["EmployeeID"].ToString();

          //  // Get the details of the Employee with the EmployeeID Session Variable
          //  GetEmployeeDetails(Convert.ToInt32(Session["EmployeeID"].ToString()), true);

          //  trEmployeeDetailView.Visible = true;
          //}
          //else
          //{
          //  trEmployeeDetailView.Visible = false;
          //}

         
          LoadEmployeeDropDown();
          trEmployeeDetailView.Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion

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
        //  ddlTitleEdit.ClearSelection();
        //  ddlTitleEdit.Items.FindByValue(Convert.ToString(hdfTitleID.Value)).Selected = true;
        }

        hdfNationalityID.Value = _currentEmployee.NationalityID.ToString();
        lblNationalityValue.Text = Common.CheckBlank(_currentEmployee.NationalityDescription);
        if (Convert.ToInt32(hdfNationalityID.Value) != 0)
        {
         // ddlNationalityEdit.ClearSelection();
         // ddlNationalityEdit.Items.FindByValue(Convert.ToString(hdfNationalityID.Value)).Selected = true;
        }

        hdfReligionID.Value = _currentEmployee.ReligionID.ToString();
        lblReligionValue.Text = Common.CheckBlank(_currentEmployee.ReligionDescription);
        if (Convert.ToInt32(hdfReligionID.Value) != 0)
        {
         // ddlReligionEdit.ClearSelection();
          //ddlReligionEdit.Items.FindByValue(Convert.ToString(hdfReligionID.Value)).Selected = true;
        }

        hdfBloodGroupID.Value = _currentEmployee.BloodGroupID.ToString();
        lblBloodGroupValue.Text = Common.CheckBlank(_currentEmployee.BloodGroupDescription);
        if (Convert.ToInt32(hdfBloodGroupID.Value) != 0)
        {
         // ddlBloodGroupEdit.ClearSelection();
         // ddlBloodGroupEdit.Items.FindByValue(Convert.ToString(hdfBloodGroupID.Value)).Selected = true;
        }

        hdfMaritalStatusID.Value = _currentEmployee.MaritalStatusID.ToString();
        lblMaritalStatusValue.Text = Common.CheckBlank(_currentEmployee.MaritalStatusDescription);
        if (Convert.ToInt32(hdfMaritalStatusID.Value) != 0)
        {
         // ddlMaritalStatusEdit.ClearSelection();
         // ddlMaritalStatusEdit.Items.FindByValue(Convert.ToString(hdfMaritalStatusID.Value)).Selected = true;
        }
        //if (ddlMaritalStatusEdit.SelectedItem.Text != "Married")
        //{
          //txtWeddingDateEdit.Enabled = false;
          //lblWeddingAnniversaryValue.Text = "";
          //hdfWeddingAnniversaryDate.Value = "";
          //txtWeddingDateEdit.Text = "";
        //}
       // else
        //{
          //if (_currentEmployee.WeddingDate != null)
          //{
            //tempDt = (DateTime)_currentEmployee.WeddingDate;
            //lblWeddingAnniversaryValue.Text = tempDt.ToString(dtFormat);
            //hdfWeddingAnniversaryDate.Value = tempDt.ToString(dtFormat);
            //txtWeddingDateEdit.Text = tempDt.ToString(dtFormat);
          //}
        

      //  hdfHomeTown.Value = _currentEmployee.HomeTown;
       // lblHomeTownValue.Text = Common.CheckBlank(_currentEmployee.HomeTown);
       // txtHomeTownEdit.Text = hdfHomeTown.Value.ToString();

       // hdfMobile.Value = _currentEmployee.Mobile;
        //lblMobileValue.Text = Common.CheckBlank(_currentEmployee.Mobile);
       // txtMobileEdit.Text = hdfMobile.Value.ToString();

        //
        //ddlPresentAddressCountry.SelectedIndex = 0;
        //ddlPermanentAddressCountry.SelectedIndex = 0;

        // Address Info

       // hdfIsPresentAndPermanentAddressSame.Value = _currentEmployee.IsPresentAndPermanentAddressSame.ToString();
       // chkIsPresentAddrSameAsPermanent.Checked = _currentEmployee.IsPresentAndPermanentAddressSame;
        AddressType addrType = new AddressType();
        foreach (EmployeeAddressDL eAddress in _currentEmployee.EmployeeAddresses)
        {
          addrType = (AddressType)eAddress.AddressTypeID;
          if (addrType == AddressType.PresentAddress)
          {
            hdfPresentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

            hdfPresentAddress1.Value = eAddress.AddressInfo.Address1;
            lblPresentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
           // txtPresentAddress1.Text = hdfPresentAddress1.Value.ToString();

            hdfPresentAddress2.Value = eAddress.AddressInfo.Address2;
            lblPresentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
          //  txtPresentAddress2.Text = eAddress.AddressInfo.Address2;

            hdfPresentAddress3.Value = eAddress.AddressInfo.Address3;
            lblPresentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
           // txtPresentAddress3.Text = eAddress.AddressInfo.Address3;

            hdfCity1.Value = eAddress.AddressInfo.CityDescription;
            lblCityValue1.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
          //  txtPresentAddressCity.Text = eAddress.AddressInfo.CityDescription;

            hdfPinCode1.Value = eAddress.AddressInfo.PostalCode;
            lblPinCodeValue1.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
         //   txtPresentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

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
            //  cddPresentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
            //  cddPresentAddressStates.ContextKey = sID.ToString();
            }

            hdfPresentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
            lblDistrictValue1.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
            if (eAddress.AddressInfo.DistrictID != 0)
            {
              int dID = eAddress.AddressInfo.DistrictID;
              HttpContext.Current.Cache.Add("DistrictID", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            //  cddPresentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
             // cddPresentAddressDistricts.ContextKey = dID.ToString();
            }

            hdfPhone1.Value = eAddress.Phone;
            lblPhoneValue1.Text = Common.CheckBlank(eAddress.Phone);
            //txtPresentAddressPhone.Text = eAddress.Phone;

            // Copy to Permanent
            //if (chkIsPresentAddrSameAsPermanent.Checked == true)
            //{
            //  hdfPermanentEmployeeAddressID.Value = "0";

            //  hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
            //  lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
            //  //txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

            //  hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
            //  lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
            // // txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

            //  hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
            //  lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
            // // txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

            //  hdfCity2.Value = eAddress.AddressInfo.CityDescription;
            //  lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
            //  //txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

            //  hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
            //  lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
            //  //txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

            //  hdfPermanentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
            //  lblCountryValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
            //  if (eAddress.AddressInfo.CountryID != 0)
            //  {
            //    int ctryID = eAddress.AddressInfo.CountryID;
            //    HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            //        new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            //  }

            //  hdfPermanentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
            //  lblStateValue2.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
            //  if (eAddress.AddressInfo.StateID != 0)
            //  {
            //    int sID = eAddress.AddressInfo.StateID;
            //    HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            //        new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            //  //  cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
            //   // cddPermanentAddressStates.ContextKey = sID.ToString();
            //  }

            //  hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
            //  lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
            //  if (eAddress.AddressInfo.DistrictID != 0)
            //  {
            //    int dID = eAddress.AddressInfo.DistrictID;
            //    HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
            //        new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
            //   // cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
            //   // cddPermanentAddressDistricts.ContextKey = dID.ToString();
            //  }

            //  hdfPhone2.Value = eAddress.Phone;
            //  lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
            ////  txtPermanentAddressPhone.Text = eAddress.Phone;
            //}
          }
        
          if (addrType == AddressType.PermanentAddress)
          {
            hdfPermanentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

            hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
            lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
           // txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

            hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
            lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
            //txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

            hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
            lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
          //  txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

            hdfCity2.Value = eAddress.AddressInfo.CityDescription;
            lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
          //  txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

            hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
            lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
            //txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

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
            //  cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
            //cddPermanentAddressStates.ContextKey = sID.ToString();
            }

            hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
            lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
            if (eAddress.AddressInfo.DistrictID != 0)
            {
              int dID = eAddress.AddressInfo.DistrictID;
              HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                  new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
              //cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
              //cddPermanentAddressDistricts.ContextKey = dID.ToString();
            }

            hdfPhone2.Value = eAddress.Phone;
            lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
            //txtPermanentAddressPhone.Text = eAddress.Phone;
          }
        }

        // Additional Info
        hdfPAN.Value = _currentEmployee.Pan;
        lblPANValue.Text = Common.CheckBlank(_currentEmployee.Pan);
        //txtPANEdit.Text = _currentEmployee.Pan;

        hdfBankAccountNumber.Value = _currentEmployee.BankAccountNumber;
        lblBankAccountNumberValue.Text = Common.CheckBlank(_currentEmployee.BankAccountNumber);
        //txtBankAccountNumberEdit.Text = _currentEmployee.BankAccountNumber;

        hdfBankName.Value = _currentEmployee.BankName;
        lblBankNameValue.Text = Common.CheckBlank(_currentEmployee.BankName);
        //txtBankNameEdit.Text = _currentEmployee.BankName;

        hdfPassportNumber.Value = _currentEmployee.PassportNo;
        lblPassportNumberValue.Text = Common.CheckBlank(_currentEmployee.PassportNo);
        //txtPassportNumberEdit.Text = _currentEmployee.PassportNo;

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
          //tempDt = (DateTime)_currentEmployee.PassportDateIssue;
          //lblDateOfIssueValue.Text = tempDt.ToString(dtFormat);
         // hdfDateOfIssue.Value = tempDt.ToString(dtFormat);
         // txtPassportDateOfIssueEdit.Text = tempDt.ToString(dtFormat);
        }
        else
        {
          hdfDateOfIssue.Value = "";
          lblDateOfIssueValue.Text = "";
          //txtPassportDateOfIssueEdit.Text = "";
        }
        if (_currentEmployee.PassportDateExpiry != null)
        {
         // tempDt = (DateTime)_currentEmployee.PassportDateExpiry;
        //  lblDateOfExpiryValue.Text = tempDt.ToString(dtFormat);
         // hdfDateOfExpiry.Value = tempDt.ToString(dtFormat);
         // txtPassportDateOfExpiryEdit.Text = tempDt.ToString(dtFormat);
        }
        else
        {
          lblDateOfExpiryValue.Text = "";
          hdfDateOfExpiry.Value = "";
          //txtPassportDateOfExpiryEdit.Text = "";
        }

        hdfPlaceOfIssue.Value = _currentEmployee.PassportIssuePlace;
        lblPlaceOfIssueValue.Text = Common.CheckBlank(_currentEmployee.PassportIssuePlace);
       // txtPassportPlaceOfIssueEdit.Text = _currentEmployee.PassportIssuePlace;

        hdfPersonalEmailID.Value = _currentEmployee.PersonalEmailID;
        lblPersonalEmailIDValue.Text = Common.CheckBlank(_currentEmployee.PersonalEmailID);
        //txtPersonalMailIDEdit.Text = _currentEmployee.PersonalEmailID;

        hdfMessengerID.Value = _currentEmployee.MessengerID;
        lblMessengerIDValue.Text = Common.CheckBlank(_currentEmployee.MessengerID);
        //txtMessengerIDEdit.Text = _currentEmployee.MessengerID;

        hdfICEName1.Value = _currentEmployee.IceName;
        lblICENameValue1.Text = Common.CheckBlank(_currentEmployee.IceName);
        //txtICENameEdit1.Text = _currentEmployee.IceName;

        hdfICERelationshipID1.Value = _currentEmployee.IceRelationshipID.ToString();
        lblICERelationshipValue1.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription);
        if (_currentEmployee.IceRelationshipID != 0)
        {
         // ddlICERelationshipEdit1.ClearSelection();
         // ddlICERelationshipEdit1.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID)).Selected = true;
        }

        hdfICEPhone1.Value = _currentEmployee.IceMobile;
        lblICEPhoneValue1.Text = Common.CheckBlank(_currentEmployee.IceMobile);
        //txtICEPhoneEdit1.Text = _currentEmployee.IceMobile;

        hdfICEName2.Value = _currentEmployee.IceName2;
        lblICENameValue2.Text = Common.CheckBlank(_currentEmployee.IceName2);
       // txtICENameEdit2.Text = _currentEmployee.IceName2;

        hdfICERelationshipID2.Value = _currentEmployee.IceRelationshipID2.ToString();
        lblICERelationshipValue2.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription2);
        if (_currentEmployee.IceRelationshipID2 != 0)
        {
          //ddlICERelationshipEdit2.ClearSelection();
          //ddlICERelationshipEdit2.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID2)).Selected = true;
        }

        hdfICEPhone2.Value = _currentEmployee.IceMobile2;
        lblICEPhoneValue2.Text = Common.CheckBlank(_currentEmployee.IceMobile2);
        //txtICEPhoneEdit2.Text = _currentEmployee.IceMobile2;

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
        //txtOnsiteLocation.Text = "";

        // Assign the list of Employee Present Employer Project Details to the grid
        gvEmployeePresentProject.DataSource = _currentEmployee.EmployeePresentProjects;
        gvEmployeePresentProject.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "AssignValues", ex.Message.ToString(), new ACEConnection());
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
        if (ddlEmployee.SelectedValue.ToString() != "" && ddlEmployee.SelectedValue.ToString() != null)
        {

          getEmployeeDesignationHistory(Convert.ToInt32(ddlEmployee.SelectedValue));
          GetEmployeeDetails(Convert.ToInt32(ddlEmployee.SelectedValue), true);
        
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "ddlEmployee_SelectedIndexChanged", ex.Message, new ACEConnection());
      }
    }

   
    /// <summary>
    /// Load List For Employee
    /// </summary>
    private void LoadEmployeeDropDown()
    {
      try
      {
        // Load Employee
        ddlEmployee.Items.Clear();
        ddlEmployee.DataSource = new EmployeeDL().GetEmployeeListByCompanyID(_companyID).Tables[0];
        ddlEmployee.DataTextField = "EmployeeName";
        ddlEmployee.DataValueField = "EmployeeID";
        ddlEmployee.DataBind();
        ddlEmployee.Items.Insert(0, "-- Select One --");
        ddlEmployee.Items[0].Value = "";

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "LoadEmployeeDropDown", ex.Message, new ACEConnection());
      }
    }


    /// <summary>
    /// getEmployeeDesignationHistory
    /// </summary>
    protected void getEmployeeDesignationHistory(int EmpID)
    {
      EmployeeDL getEmployeeInfo = new EmployeeDL();
      getEmployeeInfo.EmployeeID = EmpID;
      DataTable dt0 = getEmployeeInfo.GetEmployeeDesignationHistory();
      if (dt0.Rows.Count == 0)
      {
        dt0.Rows.Add(dt0.NewRow());
       // gvEmployeeDesignationHistory.DataSource = dt0;
        //gvEmployeeDesignationHistory.DataBind();
      //  int columncount = gvEmployeeDesignationHistory.Rows[0].Cells.Count;
       // gvEmployeeDesignationHistory.Rows[0].Cells.Clear();
        //gvEmployeeDesignationHistory.Rows[0].Cells.Add(new TableCell());
        //gvEmployeeDesignationHistory.Rows[0].Cells[0].ColumnSpan = columncount;
        //gvEmployeeDesignationHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
        //gvEmployeeDesignationHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
        //gvEmployeeDesignationHistory.Rows[0].Cells[0].Text = "Currently there are no entries to display";
      }
      else
      {
        //gvEmployeeDesignationHistory.DataSource = dt0;
        //gvEmployeeDesignationHistory.DataBind();
      }
    }
    
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeLanguages_RowDataBound", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeLanguages_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }


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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeFamily_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeFamily_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }
    

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeEducation_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeEducation_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeCertification_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeCertification_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeSkill_RowDataBound", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeSkill_Sorting", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeExperience_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion


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

        // Sort the list based on the columns 
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeExperience_Sorting", ex.Message.ToString(), new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeeExperience_SelectedIndexChanged", ex.Message.ToString(), new ACEConnection());
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
         // lblSerial.Text = ((gvEmployeeDesignationHistory.PageIndex * gvEmployeeDesignationHistory.PageSize) + e.Row.RowIndex + 1).ToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeDesignationHistory_RowDataBound", ex.Message, new ACEConnection());
      }
    }

   

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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeePreviousProject_RowDataBound", ex.Message.ToString(), new ACEConnection());
      }
    }
    #endregion


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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeePreviousProject_Sorting", ex.Message.ToString(), new ACEConnection());
      }
    }

    
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeePresentProject_RowDataBound", ex.Message, new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "gvEmployeePresentProject_Sorting", ex.Message.ToString(), new ACEConnection());
      }
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
        trEmployeeDetailView.Visible = true;
        _currentEmployee = new EmployeeDL(employeeID, isProperties);
         AssignValues();

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "GetEmployeeDetails(int employeeID, bool isProperties)", ex.Message, new ACEConnection());
      }
    }

    ///// <summary>
    ///// Values of the form are set
    ///// </summary
    //private void AssignValues()
    //{
    //  try
    //  {
    //    // Assign the Employee Details to the Form Labels
    //    lblEmployeeCodeValue.Text = Common.CheckBlank(_currentEmployee.EmployeeCode);
    //    lblFNameValue.Text = Common.CheckBlank(_currentEmployee.FName) + " " + _currentEmployee.LName;
    //    lblInitialValue.Text = Common.CheckBlank(_currentEmployee.Initial);
    //    lblGenderValue.Text = Common.CheckBlank(_currentEmployee.GenderDescription);
    //    lblDsgDesc.Text = Common.CheckBlank(_currentEmployee.DesignationDescription);
    //    // lblClientNameValue.Text = Common.CheckBlank(_currentEmployee.ClientName);
    //    EmployeeJobStatusDL _employeeJobStatus = new EmployeeJobStatusDL();
    //    string jobstatus = _employeeJobStatus.GetEmployeeJobStatusByEmployeeID(Convert.ToInt32(hdfEmployeeID.Value)).ToString();

    //    lblEmployeeStatus.Text = jobstatus.ToString();

    //    DateTime tempDt = (DateTime)_currentEmployee.Dob;

    //    //string dFormat = Session["dFormat"]; 
    //    string dtFormat = _dateFormat;
    //    lblDateOfBirthValue.Text = tempDt.ToString(dtFormat);
    //    lblDepartmentValue.Text = Common.CheckBlank(_currentEmployee.DepartmentDescription);

    //    if (_currentEmployee.Doj != null)
    //    {
    //      tempDt = (DateTime)_currentEmployee.Doj;
    //      lblDateOfJoiningValue.Text = tempDt.ToString(dtFormat);
    //    }
    //    lblOfficialMailIDValue.Text = Common.CheckBlank(_currentEmployee.OfficeEmailID);

    //    // General Info

    //    hdfTitleID.Value = _currentEmployee.TitleID.ToString();
    //    lblTitleValue.Text = Common.CheckBlank(_currentEmployee.TitleDescription);
    //    if (Convert.ToInt32(hdfTitleID.Value) != 0)
    //    {
    //      ddlTitleEdit.ClearSelection();
    //      ddlTitleEdit.Items.FindByValue(Convert.ToString(hdfTitleID.Value)).Selected = true;
    //    }

    //    hdfNationalityID.Value = _currentEmployee.NationalityID.ToString();
    //    lblNationalityValue.Text = Common.CheckBlank(_currentEmployee.NationalityDescription);
    //    if (Convert.ToInt32(hdfNationalityID.Value) != 0)
    //    {
    //      ddlNationalityEdit.ClearSelection();
    //      ddlNationalityEdit.Items.FindByValue(Convert.ToString(hdfNationalityID.Value)).Selected = true;
    //    }

    //    hdfReligionID.Value = _currentEmployee.ReligionID.ToString();
    //    lblReligionValue.Text = Common.CheckBlank(_currentEmployee.ReligionDescription);
    //    if (Convert.ToInt32(hdfReligionID.Value) != 0)
    //    {
    //      ddlReligionEdit.ClearSelection();
    //      ddlReligionEdit.Items.FindByValue(Convert.ToString(hdfReligionID.Value)).Selected = true;
    //    }

    //    hdfBloodGroupID.Value = _currentEmployee.BloodGroupID.ToString();
    //    lblBloodGroupValue.Text = Common.CheckBlank(_currentEmployee.BloodGroupDescription);
    //    if (Convert.ToInt32(hdfBloodGroupID.Value) != 0)
    //    {
    //      ddlBloodGroupEdit.ClearSelection();
    //      ddlBloodGroupEdit.Items.FindByValue(Convert.ToString(hdfBloodGroupID.Value)).Selected = true;
    //    }

    //    hdfMaritalStatusID.Value = _currentEmployee.MaritalStatusID.ToString();
    //    lblMaritalStatusValue.Text = Common.CheckBlank(_currentEmployee.MaritalStatusDescription);
    //    if (Convert.ToInt32(hdfMaritalStatusID.Value) != 0)
    //    {
    //      ddlMaritalStatusEdit.ClearSelection();
    //      ddlMaritalStatusEdit.Items.FindByValue(Convert.ToString(hdfMaritalStatusID.Value)).Selected = true;
    //    }
    //    if (ddlMaritalStatusEdit.SelectedItem.Text != "Married")
    //    {
    //      txtWeddingDateEdit.Enabled = false;
    //      lblWeddingAnniversaryValue.Text = "";
    //      hdfWeddingAnniversaryDate.Value = "";
    //      txtWeddingDateEdit.Text = "";
    //    }
    //    else
    //    {
    //      if (_currentEmployee.WeddingDate != null)
    //      {
    //        tempDt = (DateTime)_currentEmployee.WeddingDate;
    //        lblWeddingAnniversaryValue.Text = tempDt.ToString(dtFormat);
    //        hdfWeddingAnniversaryDate.Value = tempDt.ToString(dtFormat);
    //        txtWeddingDateEdit.Text = tempDt.ToString(dtFormat);
    //      }
    //    }

    //    hdfHomeTown.Value = _currentEmployee.HomeTown;
    //    lblHomeTownValue.Text = Common.CheckBlank(_currentEmployee.HomeTown);
    //    txtHomeTownEdit.Text = hdfHomeTown.Value.ToString();

    //    hdfMobile.Value = _currentEmployee.Mobile;
    //    lblMobileValue.Text = Common.CheckBlank(_currentEmployee.Mobile);
    //    txtMobileEdit.Text = hdfMobile.Value.ToString();

    //    //
    //    ddlPresentAddressCountry.SelectedIndex = 0;
    //    ddlPermanentAddressCountry.SelectedIndex = 0;

    //    // Address Info

    //    hdfIsPresentAndPermanentAddressSame.Value = _currentEmployee.IsPresentAndPermanentAddressSame.ToString();
    //    chkIsPresentAddrSameAsPermanent.Checked = _currentEmployee.IsPresentAndPermanentAddressSame;
    //    AddressType addrType = new AddressType();
    //    foreach (EmployeeAddressDL eAddress in _currentEmployee.EmployeeAddresses)
    //    {
    //      addrType = (AddressType)eAddress.AddressTypeID;
    //      if (addrType == AddressType.PresentAddress)
    //      {
    //        hdfPresentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

    //        hdfPresentAddress1.Value = eAddress.AddressInfo.Address1;
    //        lblPresentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
    //        txtPresentAddress1.Text = hdfPresentAddress1.Value.ToString();

    //        hdfPresentAddress2.Value = eAddress.AddressInfo.Address2;
    //        lblPresentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
    //        txtPresentAddress2.Text = eAddress.AddressInfo.Address2;

    //        hdfPresentAddress3.Value = eAddress.AddressInfo.Address3;
    //        lblPresentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
    //        txtPresentAddress3.Text = eAddress.AddressInfo.Address3;

    //        hdfCity1.Value = eAddress.AddressInfo.CityDescription;
    //        lblCityValue1.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
    //        txtPresentAddressCity.Text = eAddress.AddressInfo.CityDescription;

    //        hdfPinCode1.Value = eAddress.AddressInfo.PostalCode;
    //        lblPinCodeValue1.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
    //        txtPresentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

    //        hdfPresentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
    //        lblCountryValue1.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
    //        if (eAddress.AddressInfo.CountryID != 0)
    //        {
    //          int ctryID = eAddress.AddressInfo.CountryID;
    //          HttpContext.Current.Cache.Add("CountryID", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //        }

    //        hdfPresentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
    //        lblStateValue1.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
    //        if (eAddress.AddressInfo.StateID != 0)
    //        {
    //          int sID = eAddress.AddressInfo.StateID;
    //          HttpContext.Current.Cache.Add("StateID", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //          cddPresentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
    //          cddPresentAddressStates.ContextKey = sID.ToString();
    //        }

    //        hdfPresentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
    //        lblDistrictValue1.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
    //        if (eAddress.AddressInfo.DistrictID != 0)
    //        {
    //          int dID = eAddress.AddressInfo.DistrictID;
    //          HttpContext.Current.Cache.Add("DistrictID", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //          cddPresentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
    //          cddPresentAddressDistricts.ContextKey = dID.ToString();
    //        }

    //        hdfPhone1.Value = eAddress.Phone;
    //        lblPhoneValue1.Text = Common.CheckBlank(eAddress.Phone);
    //        txtPresentAddressPhone.Text = eAddress.Phone;

    //        // Copy to Permanent
    //        if (chkIsPresentAddrSameAsPermanent.Checked == true)
    //        {
    //          hdfPermanentEmployeeAddressID.Value = "0";

    //          hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
    //          lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
    //          txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

    //          hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
    //          lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
    //          txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

    //          hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
    //          lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
    //          txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

    //          hdfCity2.Value = eAddress.AddressInfo.CityDescription;
    //          lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
    //          txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

    //          hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
    //          lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
    //          txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

    //          hdfPermanentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
    //          lblCountryValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
    //          if (eAddress.AddressInfo.CountryID != 0)
    //          {
    //            int ctryID = eAddress.AddressInfo.CountryID;
    //            HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //                new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //          }

    //          hdfPermanentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
    //          lblStateValue2.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
    //          if (eAddress.AddressInfo.StateID != 0)
    //          {
    //            int sID = eAddress.AddressInfo.StateID;
    //            HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //                new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //            cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
    //            cddPermanentAddressStates.ContextKey = sID.ToString();
    //          }

    //          hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
    //          lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
    //          if (eAddress.AddressInfo.DistrictID != 0)
    //          {
    //            int dID = eAddress.AddressInfo.DistrictID;
    //            HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //                new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //            cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
    //            cddPermanentAddressDistricts.ContextKey = dID.ToString();
    //          }

    //          hdfPhone2.Value = eAddress.Phone;
    //          lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
    //          txtPermanentAddressPhone.Text = eAddress.Phone;
    //        }
    //      }
    //      if (addrType == AddressType.PermanentAddress)
    //      {
    //        hdfPermanentEmployeeAddressID.Value = eAddress.EmployeeAddressID.ToString();

    //        hdfPermanentAddress1.Value = eAddress.AddressInfo.Address1;
    //        lblPermanentAddressValue1.Text = Common.CheckBlank(eAddress.AddressInfo.Address1);
    //        txtPermanentAddress1.Text = eAddress.AddressInfo.Address1;

    //        hdfPermanentAddress2.Value = eAddress.AddressInfo.Address2;
    //        lblPermanentAddressValue2.Text = Common.CheckBlank(eAddress.AddressInfo.Address2);
    //        txtPermanentAddress2.Text = eAddress.AddressInfo.Address2;

    //        hdfPermanentAddress3.Value = eAddress.AddressInfo.Address3;
    //        lblPermanentAddressValue3.Text = Common.CheckBlank(eAddress.AddressInfo.Address3);
    //        txtPermanentAddress3.Text = eAddress.AddressInfo.Address3;

    //        hdfCity2.Value = eAddress.AddressInfo.CityDescription;
    //        lblCityValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CityDescription);
    //        txtPermanentAddressCity.Text = eAddress.AddressInfo.CityDescription;

    //        hdfPinCode2.Value = eAddress.AddressInfo.PostalCode;
    //        lblPinCodeValue2.Text = Common.CheckBlank(eAddress.AddressInfo.PostalCode);
    //        txtPermanentAddressPinCode.Text = eAddress.AddressInfo.PostalCode;

    //        hdfPermanentAddressCountryID.Value = eAddress.AddressInfo.CountryID.ToString();
    //        lblCountryValue2.Text = Common.CheckBlank(eAddress.AddressInfo.CountryName);
    //        if (eAddress.AddressInfo.CountryID != 0)
    //        {
    //          int ctryID = eAddress.AddressInfo.CountryID;
    //          HttpContext.Current.Cache.Add("CountryID2", ctryID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //        }

    //        hdfPermanentAddressStateID.Value = eAddress.AddressInfo.StateID.ToString();
    //        lblStateValue2.Text = Common.CheckBlank(eAddress.AddressInfo.StateName);
    //        if (eAddress.AddressInfo.StateID != 0)
    //        {
    //          int sID = eAddress.AddressInfo.StateID;
    //          HttpContext.Current.Cache.Add("StateID2", sID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //          cddPermanentAddressStates.SelectedValue = eAddress.AddressInfo.CountryID.ToString();
    //          cddPermanentAddressStates.ContextKey = sID.ToString();
    //        }

    //        hdfPermanentAddressDistrictID.Value = eAddress.AddressInfo.DistrictID.ToString();
    //        lblDistrictValue2.Text = Common.CheckBlank(eAddress.AddressInfo.DistrictName);
    //        if (eAddress.AddressInfo.DistrictID != 0)
    //        {
    //          int dID = eAddress.AddressInfo.DistrictID;
    //          HttpContext.Current.Cache.Add("DistrictID2", dID, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
    //              new TimeSpan(0, 0, 1, 0), System.Web.Caching.CacheItemPriority.NotRemovable, null);
    //          cddPermanentAddressDistricts.SelectedValue = eAddress.AddressInfo.DistrictID.ToString();
    //          cddPermanentAddressDistricts.ContextKey = dID.ToString();
    //        }

    //        hdfPhone2.Value = eAddress.Phone;
    //        lblPhoneValue2.Text = Common.CheckBlank(eAddress.Phone);
    //        txtPermanentAddressPhone.Text = eAddress.Phone;
    //      }
    //    }

    //    // Additional Info
    //    hdfPAN.Value = _currentEmployee.Pan;
    //    lblPANValue.Text = Common.CheckBlank(_currentEmployee.Pan);
    //    txtPANEdit.Text = _currentEmployee.Pan;

    //    hdfBankAccountNumber.Value = _currentEmployee.BankAccountNumber;
    //    lblBankAccountNumberValue.Text = Common.CheckBlank(_currentEmployee.BankAccountNumber);
    //    txtBankAccountNumberEdit.Text = _currentEmployee.BankAccountNumber;

    //    hdfBankName.Value = _currentEmployee.BankName;
    //    lblBankNameValue.Text = Common.CheckBlank(_currentEmployee.BankName);
    //    txtBankNameEdit.Text = _currentEmployee.BankName;

    //    hdfPassportNumber.Value = _currentEmployee.PassportNo;
    //    lblPassportNumberValue.Text = Common.CheckBlank(_currentEmployee.PassportNo);
    //    txtPassportNumberEdit.Text = _currentEmployee.PassportNo;

    //    DateTime dt = new DateTime();

    //    if (_currentEmployee.JobStartTime != null)
    //    {
    //      dt = _currentEmployee.JobStartTime.Value;

    //      string strMinute = "";

    //      if (dt.Minute.ToString() == "0")
    //      {
    //        strMinute = "00";
    //      }
    //      else
    //      {
    //        strMinute = dt.Minute.ToString();
    //      }
    //      lblJobStartingTime.Text = dt.Hour.ToString() + ":" + strMinute;
    //    }
    //    else
    //    {
    //      lblJobStartingTime.Text = "--";
    //    }

    //    if (_currentEmployee.PassportDateIssue != null)
    //    {
    //      tempDt = (DateTime)_currentEmployee.PassportDateIssue;
    //      lblDateOfIssueValue.Text = tempDt.ToString(dtFormat);
    //      hdfDateOfIssue.Value = tempDt.ToString(dtFormat);
    //      txtPassportDateOfIssueEdit.Text = tempDt.ToString(dtFormat);
    //    }
    //    else
    //    {
    //      hdfDateOfIssue.Value = "";
    //      lblDateOfIssueValue.Text = "";
    //      txtPassportDateOfIssueEdit.Text = "";
    //    }
    //    if (_currentEmployee.PassportDateExpiry != null)
    //    {
    //      tempDt = (DateTime)_currentEmployee.PassportDateExpiry;
    //      lblDateOfExpiryValue.Text = tempDt.ToString(dtFormat);
    //      hdfDateOfExpiry.Value = tempDt.ToString(dtFormat);
    //      txtPassportDateOfExpiryEdit.Text = tempDt.ToString(dtFormat);
    //    }
    //    else
    //    {
    //      lblDateOfExpiryValue.Text = "";
    //      hdfDateOfExpiry.Value = "";
    //      txtPassportDateOfExpiryEdit.Text = "";
    //    }

    //    hdfPlaceOfIssue.Value = _currentEmployee.PassportIssuePlace;
    //    lblPlaceOfIssueValue.Text = Common.CheckBlank(_currentEmployee.PassportIssuePlace);
    //    txtPassportPlaceOfIssueEdit.Text = _currentEmployee.PassportIssuePlace;

    //    hdfPersonalEmailID.Value = _currentEmployee.PersonalEmailID;
    //    lblPersonalEmailIDValue.Text = Common.CheckBlank(_currentEmployee.PersonalEmailID);
    //    txtPersonalMailIDEdit.Text = _currentEmployee.PersonalEmailID;

    //    hdfMessengerID.Value = _currentEmployee.MessengerID;
    //    lblMessengerIDValue.Text = Common.CheckBlank(_currentEmployee.MessengerID);
    //    txtMessengerIDEdit.Text = _currentEmployee.MessengerID;

    //    hdfICEName1.Value = _currentEmployee.IceName;
    //    lblICENameValue1.Text = Common.CheckBlank(_currentEmployee.IceName);
    //    txtICENameEdit1.Text = _currentEmployee.IceName;

    //    hdfICERelationshipID1.Value = _currentEmployee.IceRelationshipID.ToString();
    //    lblICERelationshipValue1.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription);
    //    if (_currentEmployee.IceRelationshipID != 0)
    //    {
    //      ddlICERelationshipEdit1.ClearSelection();
    //      ddlICERelationshipEdit1.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID)).Selected = true;
    //    }

    //    hdfICEPhone1.Value = _currentEmployee.IceMobile;
    //    lblICEPhoneValue1.Text = Common.CheckBlank(_currentEmployee.IceMobile);
    //    txtICEPhoneEdit1.Text = _currentEmployee.IceMobile;

    //    hdfICEName2.Value = _currentEmployee.IceName2;
    //    lblICENameValue2.Text = Common.CheckBlank(_currentEmployee.IceName2);
    //    txtICENameEdit2.Text = _currentEmployee.IceName2;

    //    hdfICERelationshipID2.Value = _currentEmployee.IceRelationshipID2.ToString();
    //    lblICERelationshipValue2.Text = Common.CheckBlank(_currentEmployee.IceRelationshipDescription2);
    //    if (_currentEmployee.IceRelationshipID2 != 0)
    //    {
    //      ddlICERelationshipEdit2.ClearSelection();
    //      ddlICERelationshipEdit2.Items.FindByValue(Convert.ToString(_currentEmployee.IceRelationshipID2)).Selected = true;
    //    }

    //    hdfICEPhone2.Value = _currentEmployee.IceMobile2;
    //    lblICEPhoneValue2.Text = Common.CheckBlank(_currentEmployee.IceMobile2);
    //    txtICEPhoneEdit2.Text = _currentEmployee.IceMobile2;

    //    // Assign the list of Employee Languages Known to the grid
    //    gvEmployeeLanguages.DataSource = _currentEmployee.EmployeeLanguages;
    //    gvEmployeeLanguages.DataBind();

    //    // Assign the list of Employee Family to the grid
    //    gvEmployeeFamily.DataSource = _currentEmployee.EmployeeFamilyMembers;
    //    gvEmployeeFamily.DataBind();

    //    // Assign the list of Employee Education Details to the grid
    //    gvEmployeeEducation.DataSource = _currentEmployee.EmployeeEducationDetails;
    //    gvEmployeeEducation.DataBind();

    //    // Assign the list of Employee Certifications to the grid
    //    gvEmployeeCertification.DataSource = _currentEmployee.EmployeeCertifications;
    //    gvEmployeeCertification.DataBind();

    //    // Assign the list of Employee Skills to the grid
    //    gvEmployeeSkill.DataSource = _currentEmployee.EmployeeSkills;
    //    gvEmployeeSkill.DataBind();

    //    // Assign the list of Employee Experience Details to the grid
    //    gvEmployeeExperience.DataSource = _currentEmployee.EmployeeExperienceDetails;
    //    gvEmployeeExperience.DataBind();

    //    // Assign the list of Employee Previous Employers Project Details to the grid
    //    gvEmployeePreviousProject.DataSource = _currentEmployee.EmployeePreviousProjects;
    //    gvEmployeePreviousProject.DataBind();

    //    //// check whether isonsite = true in the popup Previous Projects
    //    txtOnsiteLocation.Text = "";

    //    // Assign the list of Employee Present Employer Project Details to the grid
    //    gvEmployeePresentProject.DataSource = _currentEmployee.EmployeePresentProjects;
    //    gvEmployeePresentProject.DataBind();
    //  }
    //  catch (Exception ex)
    //  {
    //    ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "AssignValues", ex.Message.ToString(), new ACEConnection());
    //  }
    //}






    ///// <summary>
    ///// Load Lists For Language, Relationship 
    ///// </summary>
    //private void LoadDropDownLists()
    //{
    //  try
    //  {
    //    ProjectDL _project = new ProjectDL();
    //    // Load Language
    //    ddlLanguage.Items.Clear();
    //    ddlLanguage.DataSource = new LanguagesDL().GetLanguageListNotInEmployeeLanguageKnown(Convert.ToInt32(hdfEmployeeID.Value)).Tables[0];
    //    ddlLanguage.DataTextField = "LanguageDescription";
    //    ddlLanguage.DataValueField = "LanguageID";
    //    ddlLanguage.DataBind();
    //    ddlLanguage.Items.Insert(0, "-- Select One --");
    //    ddlLanguage.Items[0].Value = "";

    //    // Load Relationships
    //    ddlRelationship.Items.Clear();
    //    ddlRelationship.DataSource = new RelationshipDL().GetRelationshipList().Tables[0];
    //    ddlRelationship.DataTextField = "RelationshipDescription";
    //    ddlRelationship.DataValueField = "RelationshipID";
    //    ddlRelationship.DataBind();
    //    ddlRelationship.Items.Insert(0, "-- Select One --");
    //    ddlRelationship.Items[0].Value = "";

    //    // Load Genders
    //    ddlGender.Items.Clear();
    //    ddlGender.DataSource = new GenderDL().GetGenderList().Tables[0];
    //    ddlGender.DataTextField = "GenderDescription";
    //    ddlGender.DataValueField = "GenderID";
    //    ddlGender.DataBind();
    //    ddlGender.Items.Insert(0, "-- Select One --");
    //    ddlGender.Items[0].Value = "";

    //    // Load Qualification
    //    ddlQualification.Items.Clear();
    //    ddlQualification.DataSource = new QualificationDL().GetQualificationList().Tables[0];
    //    ddlQualification.DataTextField = "QualificationDescription";
    //    ddlQualification.DataValueField = "QualificationID";
    //    ddlQualification.DataBind();
    //    ddlQualification.Items.Insert(0, "-- Select One --");
    //    ddlQualification.Items[0].Value = "";

    //    // Load Major
    //    ddlMajor.Items.Clear();
    //    ddlMajor.DataSource = new MajorDL().GetMajorList().Tables[0];
    //    ddlMajor.DataTextField = "MajorDescription";
    //    ddlMajor.DataValueField = "MajorID";
    //    ddlMajor.DataBind();
    //    ddlMajor.Items.Insert(0, "-- Select One --");
    //    ddlMajor.Items[0].Value = "";

    //    // Load University
    //    ddlUniversity.Items.Clear();
    //    ddlUniversity.DataSource = new UniversityDL().GetUniversityList().Tables[0];
    //    ddlUniversity.DataTextField = "UniversityDescription";
    //    ddlUniversity.DataValueField = "UniversityID";
    //    ddlUniversity.DataBind();
    //    ddlUniversity.Items.Insert(0, "-- Select One --");
    //    ddlUniversity.Items[0].Value = "";

    //    // Load Certification Technology
    //    ddlCertificationTechnology.Items.Clear();
    //    ddlCertificationTechnology.DataSource = new TechnologyDL().GetTechnologyList().Tables[0];
    //    ddlCertificationTechnology.DataTextField = "TechnologyDescription";
    //    ddlCertificationTechnology.DataValueField = "TechnologyID";
    //    ddlCertificationTechnology.DataBind();
    //    ddlCertificationTechnology.Items.Insert(0, "-- Select One --");
    //    ddlCertificationTechnology.Items[0].Value = "";

    //    // Load Skill Technology
    //    ddlSkillTechnology.Items.Clear();
    //    ddlSkillTechnology.DataSource = new TechnologyDL().GetTechnologyList().Tables[0];
    //    ddlSkillTechnology.DataTextField = "TechnologyDescription";
    //    ddlSkillTechnology.DataValueField = "TechnologyID";
    //    ddlSkillTechnology.DataBind();
    //    ddlSkillTechnology.Items.Insert(0, "-- Select One --");
    //    ddlSkillTechnology.Items[0].Value = "";

    //    // Load Skill Level
    //    ddlSkillLevel.Items.Clear();
    //    ddlSkillLevel.DataSource = new SkillLevelDL().GetSkillLevelList().Tables[0];
    //    ddlSkillLevel.DataTextField = "SkillLevelDescription";
    //    ddlSkillLevel.DataValueField = "SkillLevelID";
    //    ddlSkillLevel.DataBind();
    //    ddlSkillLevel.Items.Insert(0, "-- Select One --");
    //    ddlSkillLevel.Items[0].Value = "";

    //    // Load Present Project Names
    //    ddlPresentProjectName.Items.Clear();
    //    _project.EmployeeID = Convert.ToInt32(hdfEmployeeID.Value.ToString());
    //    ddlPresentProjectName.DataSource = _project.GetProjectList().Tables[0];
    //    ddlPresentProjectName.DataTextField = "ProjectName";
    //    ddlPresentProjectName.DataValueField = "ProjectID";
    //    ddlPresentProjectName.DataBind();
    //    ddlPresentProjectName.Items.Insert(0, "-- Select One --");
    //    ddlPresentProjectName.Items[0].Value = "";

    //    // Load Present Project Job Roles
    //    ddlPresentProjectJobRole.Items.Clear();
    //    ddlPresentProjectJobRole.DataSource = new JobRoleDL().GetJobRoleList().Tables[0];
    //    ddlPresentProjectJobRole.DataTextField = "JobRoleDescription";
    //    ddlPresentProjectJobRole.DataValueField = "JobRoleID";
    //    ddlPresentProjectJobRole.DataBind();
    //    ddlPresentProjectJobRole.Items.Insert(0, "-- Select One --");
    //    ddlPresentProjectJobRole.Items[0].Value = "";

    //  }
    //  catch (Exception ex)
    //  {
    //    ErrorLog.LogErrorMessageToDB("EmployeeProfileView.aspx", "", "LoadDropDownLists", ex.Message, new ACEConnection());
    //  }
    //}


   

   

    #endregion
  }
}



   