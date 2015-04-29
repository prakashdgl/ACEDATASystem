using System;
using System.Web.UI;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class Settings : System.Web.UI.Page
  {

    #region Private Variable(s)

    SettingsDL _currentSettingsDL = new SettingsDL();
    int _companyID = 0;
    int _userID = 0;

    #endregion

    #region Page Event(s)

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
          int UserID = 0;
          //UserID = Convert.ToInt32(Session["UserID"]);
          UserID = SessionControl.UserID.cxToInt32();
          hfUserID.Value = UserID.ToString();
          LoadDateFormatDropDown();
          GetUserSettings(Convert.ToInt32(hfUserID.Value), true);

          //_companyID = Convert.ToInt32(Session["CompanyID"]);
          //_userID = Convert.ToInt32(Session["UserID"]);

          _companyID = SessionControl.CompanyID.cxToInt32();
          _userID = SessionControl.UserID.cxToInt32();

          UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(_userID, _companyID, CommonDL.GetPageID(), true);
          if (!_usersXPagesDL.IsAddorEdit)
          {
            Response.Redirect("~/Login.aspx", false);
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Settings.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnSettingsSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnSettingsSave_Click(object sender, EventArgs e)
    {
      try
      {
        _currentSettingsDL = new SettingsDL();

        // Set whether Add / Edit
        if (hfUserSettingID.Value.ToString() != "0")
        {
          _currentSettingsDL.UserSettingsID = Convert.ToInt32(hfUserSettingID.Value);
          _currentSettingsDL.AddEditOption = 1;
        }
        else
        {
          _currentSettingsDL.AddEditOption = 0;
          _currentSettingsDL.UserSettingsID = 0;
        }

        _currentSettingsDL.UserID = Convert.ToInt32(hfUserID.Value);
        _currentSettingsDL.DateFormatID = Convert.ToInt32(ddlDateFormat.SelectedValue);
        _currentSettingsDL.BackgroundColorID = 0;
        // Add / Edit the User Settings
        TransactionResult result;
        _currentSettingsDL.ScreenMode = ScreenMode.Add;
        result = _currentSettingsDL.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful get and display the saved User Settings
        if (result.Status == TransactionStatus.Success)
        {
          GetUserSettings(Convert.ToInt32(hfUserID.Value), true);

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Settings.aspx", "", "ibtnSettingsSave_Click", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnSettingsCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnSettingsCancel_Click(object sender, EventArgs e)
    {
      Response.Redirect("~/Default.aspx");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// To retrieve the details of a particular user settings
    /// </summary>
    /// <param name="UserID">UserID</param>
    /// <param name="isProperties">Whether all properties</param>
    private void GetUserSettings(int userID, bool isProperties)
    {
      try
      {
        _currentSettingsDL = new SettingsDL(userID, isProperties);
        hfUserID.Value = _currentSettingsDL.UserID.ToString();
        hfUserSettingID.Value = _currentSettingsDL.UserSettingsID.ToString();
        ddlDateFormat.SelectedValue = Convert.ToString(_currentSettingsDL.DateFormatID);

        if (_currentSettingsDL.DateFormatID != 0)
        {
          DateFormatDL _dateformatDL = new DateFormatDL(Convert.ToInt32(_currentSettingsDL.DateFormatID), true);
          Session["DateFormat"] = _dateformatDL.DateFormatValue.ToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Settings.aspx", "", "GetUserSettings(int UserID, bool isProperties)", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Date Format
    /// </summary>
    private void LoadDateFormatDropDown()
    {
      try
      {
        // Load Title
        ddlDateFormat.DataSource = new DateFormatDL().GetDateFormatList().Tables[0];
        ddlDateFormat.DataTextField = "DateFormatText";
        ddlDateFormat.DataValueField = "DateFormatID";
        ddlDateFormat.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Settings.aspx", "", "LoadDateFormatDropDown", ex.Message, new ACEConnection());
      }
    }

    #endregion
  }
}
