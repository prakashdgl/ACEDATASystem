using System;
using System.Web.UI;
using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;

namespace ACE.PurchaseOrder
{
  public partial class ChangePassword : System.Web.UI.Page
  {
    #region Variables

    UsersDL _usersDL;
    int _companyID = 0;
    int _userID = 0;
    #endregion

    #region Page Events

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        Page.Form.DefaultButton = btnSubmit.UniqueID;
        Page.Form.DefaultFocus = txtOldPwd.ClientID;

        if (!IsPostBack)
        {
          //if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
          if (SessionControl.CompanyID != 0)
          {
            _companyID = Convert.ToInt32(Session["CompanyID"]);
            _userID = Convert.ToInt32(Session["UserID"]);
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


          txtpwd.Attributes.Add("onblur", "LengthValidation('" + txtpwd.ClientID + "')");
          txtOldPwd.Attributes.Add("onblur", "LengthValidation('" + txtOldPwd.ClientID + "')");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ChangePassword.aspx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        Utilities objPwd = new Utilities();
        _usersDL = new UsersDL();
        _usersDL.UserName = Convert.ToInt32(Session["UserName"].ToString());
        _usersDL.OldPassword = objPwd.EncryptText(txtOldPwd.Text);
        _usersDL.Password = objPwd.EncryptText(txtpwd.Text);
        if (_usersDL.ChangePassword())
        {
          lblmsg.Text = "Password Successfully Updated.";
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('Password Successfully Updated.');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        }
        else
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('Incorrect existing password!');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ChangePassword.aspx", "", "btnSubmit_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        txtpwd.Text = "";
        txtconfpwd.Text = "";
        txtOldPwd.Text = "";
        Response.Redirect("~/Default.aspx", false);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("ChangePassword.aspx", "", "btnCancel_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion
  }
}
