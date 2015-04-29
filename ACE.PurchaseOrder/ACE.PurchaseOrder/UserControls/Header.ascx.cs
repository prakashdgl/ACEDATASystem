using System;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class Header : System.Web.UI.UserControl
  {
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        //currentTime.InnerText = DateTime.Now.ToString("ddd, MMM d, yyyy hh:mm tt");

        // Don't initially hook up the extender
        if (!IsPostBack)
          //avce.Enabled = false;

          if (!IsPostBack)
          {
            DateTime theDate = new DateTime();
            theDate = DateTime.Now;
            int UserID = Convert.ToInt32(Session["UserID"]);
            string UserName = Convert.ToString(Session["EmployeeName"]);
            if (UserName != "")
            {
              lblEmpName.Text = "Welcome " + UserName;
            }
            else
            {
              lblEmpName.Text = "Welcome Super User";
            }
            if (UserID != 0)
            {
              //lblCompanyName.Text = Convert.ToString(Session["CompanyName"]);
              //lbLogout.Visible = true;
              //imgProfile.Visible = true;
            }
            else
            {
              //lblEmpName.Text = "";
              //lbLogout.Visible = false;
              // imgProfile.Visible = false;
            }
            //lblDate.Text = theDate.ToString("ddd, MMM d, yyyy hh:mm tt");
          }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Header.ascx", "", "GetLeaveDetails", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// lbLogout_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbLogout_Click(object sender, EventArgs e)
    {
      Session["UserID"] = 0;
      Session["CompanyID"] = 0;
      Session["DateFormat"] = "";
      Response.Redirect("~/Login.aspx", false);
    }

  }
}