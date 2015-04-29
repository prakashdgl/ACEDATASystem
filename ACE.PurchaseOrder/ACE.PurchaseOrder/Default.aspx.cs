using System;
using System.Web;
using System.Web.UI;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class Default : System.Web.UI.Page
  {
    #region Private Variables

    CommonDL _commonDL = new CommonDL();
    string _dateFormat;

    #endregion

    #region Event(s)

    #region Page Pre-Init: force uplevel browser setting
    /// <summary>
    /// Page_PreInit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreInit(object sender, EventArgs e)
    {
      if (BrowserCompatibility.IsUplevel)
      {
        Page.ClientTarget = "uplevel";
      }
    }
    #endregion

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      //throw new Exception();
      try
      {
        _dateFormat = Convert.ToString(Session["DateFormat"]);

        if (_dateFormat == null || _dateFormat == "")
        {
          Response.Redirect("~/Login.aspx", false);
        }
        if (!IsPostBack)
        {

          //tblAdministratorView.Visible = false;
          //tblEmployeeView.Visible = false;

          //string role = Session["Role"].ToString();
          //hdfRole.Value = role.ToString();
          //if (hdfRole.Value.ToString() == "Administrator" || hdfRole.Value.ToString() == "HR")
          //{
          //  tblAdministratorView.Visible = true;
          //  tblEmployeeView.Visible = false;
          //}
          //else
          //{
          //  tblAdministratorView.Visible = false;
          //  tblEmployeeView.Visible = true;
          //}


          InspirationQuoteDL _quoteDL = new InspirationQuoteDL();

          //lblInspirationWordLineOne.Text = Convert.ToString(_quoteDL.QuotesLine1);
          //lblInspirationWordLineTwo.Text = Convert.ToString(_quoteDL.QuotesLine2);
          //lblInspirationAuthor.Text = Convert.ToString(_quoteDL.Author);

          int CompanyID = Convert.ToInt32(Session["CompanyID"]);
          string CompanyName = Convert.ToString(Session["CompanyName"]);
          if (CompanyName != "" && CompanyName != null)
          {
            //lblGreetCompany.Text = "Welcome to " + Session["CompanyName"].ToString();
          }
          else
          {
            //lblGreetCompany.Text = "Welcome to Order";
          }


          DateTime CurrentDate = DateTime.Now.Date;
          DateTime dob;
          dob = Convert.ToDateTime(Session["EmployeeDOB"].ToString());
          if (dob.Year != CurrentDate.Year)
          {
            if (dob.Day == CurrentDate.Day && dob.Month == CurrentDate.Month)
            {
              lblMessage.Text = "EC Group Wishes a Very Happy Birth Day";
              lblName.Text = Session["EmployeeName"].ToString();
              mpeWishes.Show();

            }
          }
          DateTime WeddingDate;

          if (Session["EmployeeWeddingDate"] != null)
          {
            WeddingDate = Convert.ToDateTime(Session["EmployeeWeddingDate"].ToString());

            if (WeddingDate.Year != CurrentDate.Year)
            {
              if (WeddingDate.Day == CurrentDate.Day && WeddingDate.Month == CurrentDate.Month)
              {
                lblMessage.Text = "EC Group Wishes a Very Happy Anniversary To";//'Session["WeddingDay"].ToString();
                if (Session["SpouseName"].ToString() != "")
                  lblName.Text = Session["EmployeeName"].ToString() + " and " + Session["SpouseName"].ToString();
                else
                  lblName.Text = Session["EmployeeName"].ToString();

                lblName2.Text = Convert.ToDateTime(Session["EmployeeWeddingDate"].ToString()).ToString(_dateFormat);
                mpeWishes.Show();

              }
            }
          }
          //gvBirthDay.DataSource = _commonDL.GetEmployeeUpcomingBirthday(Convert.ToInt32(CompanyID));
          //gvBirthDay.DataBind();

          //gvAnniversaries.DataSource = _commonDL.GetEmployeeUpcomingWeddingDay(Convert.ToInt32(CompanyID));
          //gvAnniversaries.DataBind();

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Default.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    #endregion

  }

  /// <summary>
  /// ResponseHelper class
  /// </summary>
  public static class ResponseHelper
  {

    public static void Redirect(string url, string target, string windowFeatures)
    {
      HttpContext context = HttpContext.Current;

      if ((String.IsNullOrEmpty(target) ||
          target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
          String.IsNullOrEmpty(windowFeatures))
      {

        context.Response.Redirect(url);
      }
      else
      {
        Page page = (Page)context.Handler;
        if (page == null)
        {
          throw new InvalidOperationException(
              "Cannot redirect to new window outside Page context.");
        }
        url = page.ResolveClientUrl(url);

        string script;
        if (!String.IsNullOrEmpty(windowFeatures))
        {
          script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
        }
        else
        {
          script = @"window.open(""{0}"", ""{1}"");";
        }

        script = String.Format(script, url, target, windowFeatures);
        ScriptManager.RegisterStartupScript(page,
            typeof(Page),
            "Redirect",
            script,
            true);
      }
    }

    /// <summary>
    /// Redirect
    /// </summary>
    /// <param name="response"></param>
    /// <param name="url"></param>
    /// <param name="target"></param>
    /// <param name="windowFeatures"></param>
    public static void Redirect(HttpResponse response, string url, string target, string windowFeatures)
    {

      if ((String.IsNullOrEmpty(target) ||
          target.Equals("_self", StringComparison.OrdinalIgnoreCase)) &&
          String.IsNullOrEmpty(windowFeatures))
      {

        response.Redirect(url);
      }
      else
      {
        Page page = (Page)HttpContext.Current.Handler;
        if (page == null)
        {
          throw new InvalidOperationException(
              "Cannot redirect to new window outside Page context.");
        }
        url = page.ResolveClientUrl(url);

        string script;
        if (!String.IsNullOrEmpty(windowFeatures))
        {
          script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
        }
        else
        {
          script = @"window.open(""{0}"", ""{1}"");";
        }

        script = String.Format(script, url, target, windowFeatures);
        ScriptManager.RegisterStartupScript(page,
            typeof(Page),
            "Redirect",
            script,
            true);
      }
    }
  }
}