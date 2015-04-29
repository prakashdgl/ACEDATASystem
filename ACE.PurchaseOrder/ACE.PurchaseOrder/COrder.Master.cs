using System;
using System.Web.UI;
using ACE.PurchaseOrder.DataLayer;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.CommonLayer;


namespace ACE.PurchaseOrder
{
  public partial class COrder : System.Web.UI.MasterPage
  {
    #region Private Variables

    CommonDL _commonDL = new CommonDL();
    string _dateFormat;

    #endregion

    #region Event(s)

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      HtmlHead head = (this.FindControl("Head1") as HtmlHead);

      Literal l = new Literal();
      //l.Text = "<script type=\"text/javascript\" src='" + Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath  +"Javascript/jquery-1.8.3.js" + "'>  </script>";
      l.Text = "<script type=\"text/javascript\" src='" + ResolveUrl("~/Javascript/jquery-1.8.3.js") + "'>  </script>";
      l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/Javascript/jquery-ui-1.9.2.custom.js") + "'>  </script>";
      l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/Javascript/jquery.blockUI.js") + "'>  </script>";
      l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/Javascript/jquery.tablePagination.0.1.js") + "'>  </script>";
      l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/Javascript/Common.js") + "'>  </script>";
      //l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/fancybox/source/jquery.fancybox.js") + "'>  </script>";
      //l.Text += "<script type=\"text/javascript\" src='" + ResolveUrl("~/fancybox/source/helpers/jquery.fancybox-buttons.js") + "'>  </script>";
      head.Controls.Add(l);

      try
      {
          _dateFormat =  "dd/MM/yyyy"; //Convert.ToString(Session["DateFormat"]);
        
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Order.Master.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// AddedControl
    /// </summary>
    /// <param name="control"></param>
    /// <param name="index"></param>
    protected override void AddedControl(Control control, int index)
    {
      try
      {
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 

        // Add this to the code in your master page. 

        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)

          this.Page.ClientTarget = "uplevel";

        base.AddedControl(control, index);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Order.Master.aspx", "", "AddedControl", ex.Message, new ACEConnection());
      }
    }

    #endregion
  }
}
