using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Text;
using System.Net.Mail;

namespace ACE.PurchaseOrder
{
  public partial class Login : System.Web.UI.Page
  {
    #region Variables

    UsersDL _usersLogin;
    MailMessage _msg = new MailMessage();

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
        if (!IsPostBack)
        {
         
          if (Request.Cookies["myCookie"] != null)
          {
            HttpCookie cookie = Request.Cookies.Get("myCookie");
            String UserName = cookie.Values["username"];
            chkRememberMe.Checked = !(String.IsNullOrEmpty(UserName));
                     
            txtUserName.Text = Request.QueryString["EmpID"];
          }

          if (Request.QueryString["LeaveEmail"] == "Y")
          {
            txtUserName.Text = Request.QueryString["EmpID"];
            Session["SessionEmployeeID"] = Request.QueryString["EmpID"];
            Session["SessionCompanyID"] = Request.QueryString["CompanyID"];
            Session["SessionLeaveAppID"] = Request.QueryString["LeaveAppID"];
          }

          Session["UserID"] = 0;
          Session["CompanyID"] = 0;

        }
        
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Login.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// btnSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      try
      {
        bool isChecked = false;

        if ((txtUserName.Text != "") && (txtPassword.Text != ""))
        {
          FormsAuthentication.Initialize();
          Utilities objPwd = new Utilities();

          _usersLogin = new UsersDL();

          _usersLogin.UserName = txtUserName.Text.cxToInt32();
          _usersLogin.Password = objPwd.EncryptText(txtPassword.Text);


          if (_usersLogin.GetLogin())
          {
            isChecked = chkRememberMe.Checked;
            if (isChecked)
            {
              HttpCookie myCookie = new HttpCookie("myCookie");
              Response.Cookies.Remove("myCookie");
              Response.Cookies.Add(myCookie);
              myCookie.Values.Add("username", txtUserName.Text.cxToString());
              DateTime dtExpiry = DateTime.Now.AddDays(15); //you can add years and months too here
              Response.Cookies["myCookie"].Expires = dtExpiry;
            }
            else
            {
              HttpCookie myCookie = new HttpCookie("myCookie");
              Response.Cookies.Remove("myCookie");
              Response.Cookies.Add(myCookie);
              myCookie.Values.Add("username", txtUserName.Text.cxToString());
              DateTime dtExpiry = DateTime.Now.AddSeconds(1); //you can add years and months too here
              Response.Cookies["myCookie"].Expires = dtExpiry;
            }
                         
            TransactionResult result;            
            result = new UsersDL().UpdateLastLoginDate(_usersLogin.UserID);

            if (!_usersLogin.IsPasswordChanged)
            {

              //commented on 16/04/2014
              //Session["UserName"] = _usersLogin.UserName;
              //Session["UserID"] = _usersLogin.UserID;
              //Session["CompanyID"] = _usersLogin.CompanyID;
              //Session["CompanyName"] = _usersLogin.CompanyName;
              //Session["EmployeeName"] = _usersLogin.EmployeeName;
              //Session["EmployeeID"] = _usersLogin.EmployeeID;
              //Session["EmployeeDOB"] = _usersLogin.EmployeeDOB;
              //Session["EmployeeWeddingDate"] = _usersLogin.EmployeeWeddingDate;
              //Session["SpouseName"] = _usersLogin.SpouseName;
              //Session["DateFormat"] = _usersLogin.DateFormat;
              //Session["Role"] = _usersLogin.RoleName;
              //Session["DepartmentID"] = _usersLogin.DepartmentID;
              //Session["DepartmentName"] = _usersLogin.DepartmentName;

              SessionControl.UserName = _usersLogin.UserName;
              SessionControl.UserID = _usersLogin.UserID;
              SessionControl.CompanyID = _usersLogin.CompanyID;
              SessionControl.CompanyName = _usersLogin.CompanyName;
              SessionControl.EmployeeName = _usersLogin.EmployeeName;
              SessionControl.EmployeeID = _usersLogin.EmployeeID;
              SessionControl.EmployeeDOB = _usersLogin.EmployeeDOB.ToString();
              SessionControl.EmployeeWeddingDate = _usersLogin.EmployeeWeddingDate.ToString();
              SessionControl.SpouseName = _usersLogin.SpouseName;
              SessionControl.DateFormat = _usersLogin.DateFormat;
              SessionControl.Role = _usersLogin.RoleName;
              SessionControl.DepartmentID = _usersLogin.DepartmentID;
              SessionControl.DepartmentName = _usersLogin.DepartmentName;


              FormsAuthenticationTicket _faTicket = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(30), false, _usersLogin.Password, FormsAuthentication.FormsCookieName);
              string hash = FormsAuthentication.Encrypt(_faTicket);
              HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
              if (_faTicket.IsPersistent) cookie.Expires = _faTicket.Expiration;

              Response.Cookies.Add(cookie);

              if (Request.QueryString["LeaveEmail"] == "Y")
              {
                Response.Redirect("~/Leave/LeaveApproval.aspx", false);
              }
              if (_usersLogin.DepartmentName == "MT-HBT")
              {
                Response.Redirect("../ECTranscription/Login.aspx?UserID=" + _usersLogin.UserID.cxToString(), false);
              }
              if (_usersLogin.DepartmentName == "MT-Trainee")
              {
                Response.Redirect("../ECTranscription/Login.aspx?UserID=" + _usersLogin.UserID.cxToString(), false);
              }
              if (_usersLogin.DepartmentName == "Proofer-HBT")
              {
                Response.Redirect("../ECTranscription/Login.aspx?UserID=" + _usersLogin.UserID.cxToString() + "&UserDepartmentName=" + _usersLogin.DepartmentName.cxToString(), false);
              }
              if (_usersLogin.DepartmentName == "QC-HBT")
              {
                Response.Redirect("../ECTranscription/Login.aspx?UserID=" + _usersLogin.UserID.cxToString() + "&UserDepartmentName=" + _usersLogin.DepartmentName.cxToString(), false);
              }
              else
              {
                Response.Redirect("~/Default.aspx", false);
              }
            }
            else
            {
              mvLogin.ActiveViewIndex = 2;
            }
          }
          else
          {
            lblmsg.Text = "Invalid UserName / Password";
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Login.aspx", "", "btnSubmit_Click", ex.Message, new ACEConnection());
      }

    }

    /// <summary>
    /// lbtnForgotPassword_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbtnForgotPassword_Click(object sender, EventArgs e)
    {
      mvLogin.ActiveViewIndex = 1;
    }

    /// <summary>
    /// ibtnSubmitForgotPassword_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnSubmitForgotPassword_Click(object sender, EventArgs e)
    {
      try
      {
        StringBuilder sbMail = new StringBuilder();
        StringBuilder sb = new StringBuilder();
        DataSet dsUser = new DataSet();
        UsersDL userDLObj = new UsersDL();
        dsUser = userDLObj.GetUserDetails(txtForgotUserName.Text);
        if (dsUser.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = dsUser.Tables[0].Rows[0];

          string regeneratePassword = "";
          string emailID = "";
          string employeeName = "";
          int userID = 0;

          if (dRow["OfficeEmailID"] != DBNull.Value)
            emailID = dRow["OfficeEmailID"].ToString();
          else
            emailID = "";

          if (dRow["FName"] != DBNull.Value)
            employeeName = dRow["FName"].ToString();
          else
            employeeName = "";

          userID = Convert.ToInt32(dRow["UserID"].ToString());

          regeneratePassword = CreateRandomPassword(8);

          Utilities util = new Utilities();
          regeneratePassword = util.EncryptText(regeneratePassword);

          if (txtEmailID.Text.ToUpper().ToString() == emailID.ToUpper().ToString())
          {
            TransactionResult result = new UsersDL().UserForgotPassword(userID, regeneratePassword);

            if (result.Status == TransactionStatus.Success)
            {
              UsersDL theUser = new UsersDL(userID, true);

              // From Address
              _msg.From = new MailAddress("hr@ACE.in");

              // To Address
              _msg.To.Add(new MailAddress(txtEmailID.Text));

              // Subject
              _msg.Subject = "ACE - Forgot Password Assistance";

              // Body
              sbMail.Append("Dear " + employeeName + ",");
              sbMail.Append(Environment.NewLine);
              sbMail.Append(Environment.NewLine);

              sbMail.Append("Your password had been changed.");

              sbMail.Append(Environment.NewLine);
              sbMail.Append(Environment.NewLine);

              sbMail.Append("Kindly login with your Username and the new password. ");
              sbMail.Append("Please make sure you change your password for information security reasons.");
              sbMail.Append(Environment.NewLine);
              sbMail.Append(Environment.NewLine);

              sbMail.Append("Your new login information is given below to access the intranet site,");
              sbMail.Append(Environment.NewLine);

              sbMail.Append("http://192.2.200.2/Order");
              sbMail.Append(Environment.NewLine);
              sbMail.Append(Environment.NewLine);

              sbMail.Append("Username : " + txtForgotUserName.Text);
              sbMail.Append(Environment.NewLine);

              sbMail.Append("Password : " + util.DecryptText(regeneratePassword));
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

              mvLogin.ActiveViewIndex = 0;
            }
            sb.Append("<script>alert('" + "Recovery Email Sent - sent to your " + txtEmailID.Text + "  email address" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          }
          else
          {
            sb.Append("<script>alert('" + "Invalid email address" + ".');");
            sb.Append("</script>");
            ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          }

          txtForgotUserName.Text = "";
          txtEmailID.Text = "";
        }
        else
        {
          sb.Append("<script>alert('" + "Invalid User. Please Enter valid Username" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Login.aspx", "", "ibtnSubmitForgotPassword_Click", ex.Message, new ACEConnection());
      }

    }

    /// <summary>
    /// CreateRandomPassword
    /// </summary>
    /// <param name="passwordLength"></param>
    /// <returns></returns>
    private string CreateRandomPassword(int passwordLength)
    {
      string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
      char[] chars = new char[passwordLength];
      Random rd = new Random();

      for (int i = 0; i < passwordLength; i++)
      {
        chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
      }

      return new string(chars);
    }

    /// <summary>
    /// ibtnCancelForgotPassword_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnCancelForgotPassword_Click(object sender, EventArgs e)
    {
      txtForgotUserName.Text = "";
      txtEmailID.Text = "";
      mvLogin.ActiveViewIndex = 0;
    }

    /// <summary>
    /// ibtnChangePasswordSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnChangePasswordSubmit_Click(object sender, EventArgs e)
    {
      try
      {
        Utilities objPwd = new Utilities();
        _usersLogin = new UsersDL();
        _usersLogin.UserName = Convert.ToInt32(txtUserName.Text);
        _usersLogin.OldPassword = objPwd.EncryptText(txtOldPwd.Text);
        _usersLogin.Password = objPwd.EncryptText(txtpwd.Text);
        if (_usersLogin.ChangePassword())
        {
          lblmsg.Text = "Password Successfully Updated.";
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('Password Successfully Updated.');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          mvLogin.ActiveViewIndex = 0;
          Response.Redirect("~/Login.aspx", false);
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
        ErrorLog.LogErrorMessageToDB("Login.aspx", "", "ibtnChangePasswordSubmit_Click", ex.Message, new ACEConnection());
      }
    }
    #endregion
  }
}
