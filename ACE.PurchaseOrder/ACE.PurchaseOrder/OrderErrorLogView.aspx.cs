using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Data;

namespace ACE.PurchaseOrder
{
  public partial class OrderErrorLogView : System.Web.UI.Page
  {
    
    int _companyID = 0;
    int _userID = 0;
    string _dateFormat;

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      _companyID = Convert.ToInt32(Session["CompanyID"]);
      _userID = Convert.ToInt32(Session["UserID"]);
      _dateFormat = Convert.ToString(Session["DateFormat"]);
      GridViewProperties.AssignGridViewProperties(gvErrorLog);
      if (_dateFormat == null || _dateFormat == "")
      {
        Response.Redirect("~/Login.aspx", false);
      }
      if (!IsPostBack)
      {
        if (Session["CompanyID"] != null && Session["CompanyID"].ToString() != "")
        {

          UsersXPagesDL _usersXPagesDL = new UsersXPagesDL(_userID, _companyID, CommonDL.GetPageID(), true);
          if (!_usersXPagesDL.IsAddorEdit)
          {
            Response.Redirect("~/Login.aspx", false);
          }
          LoadErrorLogList();
        }
        //else
        //{
        //  Response.Redirect("~/Login.aspx", false);
        //}
      }
    }

    /// <summary>
    /// gvErrorLog_RowCreated
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvErrorLog_RowCreated(object sender, GridViewRowEventArgs e)
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
        ErrorLog.LogErrorMessageToDB("OrderErrorLogView.aspx", "", "gvErrorLog_RowCreated", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// gvErrorLog_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvErrorLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {

      if (e.Row.RowType == DataControlRowType.DataRow)
      {

        // Set the Serial Number
        Label lblSerial = (Label)e.Row.FindControl("lblSerial");
        lblSerial.Text = ((gvErrorLog.PageIndex * gvErrorLog.PageSize) + e.Row.RowIndex + 1).ToString();
      }
       
    }

    public void LoadErrorLogList()
    {

      DataTable ds = ErrorLog.GetErrorLogList().Tables[0];
      if (ds.Rows.Count == 0)
      {
        ds.Rows.Add(ds.NewRow());
        gvErrorLog.DataSource = ds;
        gvErrorLog.DataBind();
        int columncount = gvErrorLog.Rows[0].Cells.Count;
        gvErrorLog.Rows[0].Cells.Clear();
        gvErrorLog.Rows[0].Cells.Add(new TableCell());
        gvErrorLog.Rows[0].Cells[0].ColumnSpan = columncount;
        gvErrorLog.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
        gvErrorLog.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
        gvErrorLog.Rows[0].Cells[0].Text = "Currently there are no entries to display";
      }
      else
      {
        gvErrorLog.DataSource = ds;
        gvErrorLog.DataBind();
      }
    }
  }
}