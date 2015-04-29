using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class EmployeeAdditional : System.Web.UI.Page
  {
    #region Private Variables

    int _companyID = 0;

    int _userID = 0;

    string _dateFormat = "dd/MM/yyyy";

    // The Employee instance 
    EmployeeDL _currentEmployee;

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
        //Commented on 17/04/2014
        //_dateFormat = Session["DateFormat"].cxToString("");
        _dateFormat = SessionControl.DateFormat.ToString();

        if (_dateFormat == "")
        {
          Response.Redirect("~/Login.aspx", false);
        }

        //commented on 17/04/2014
        //_companyID = Session["CompanyID"].cxToInt32();
        //_userID = Session["UserID"].cxToInt32();

        _companyID = SessionControl.CompanyID.cxToInt32();
        _userID = SessionControl.UserID.cxToInt32();


        if (!IsPostBack)
        {
          //commented on 17/04/2014
          //if (Session["CompanyID"].cxToString() != null && Session["CompanyID"].cxToString() != "")
          if (_companyID != 0)
          {
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
          // Assign Common GridView Properties to all GridViews used in the page                   
          GridViewProperties.AssignGridViewProperties(gvEmployeeJobStatus);
          LoadDurationDropDown();
          LoadEmployeeDropDown();
          trEmployeeDetails.Visible = false;
          cextCalFromDate.Format = _dateFormat;

          ViewState["SortDirection"] = "ASC";
          ViewState["SortExpression"] = "EmployeeJobStatusID";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "Page_Load", ex.Message, new ACEConnection());
      }
    }

    #endregion

    #region Events

    /// <summary>
    /// ddlEmployee_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
      hfSNo.Value = "";
      try
      {
       
        //if (ddlEmployee.SelectedValue.ToString() != "" && ddlEmployee.SelectedValue.ToString() != null)
        string _empValue = ddlEmployee.SelectedValue.cxToString("");
        if (_empValue != "")
        {
          divEmp.Visible = true;
          lblbasic.Visible = true;
          trEmployeeDetails.Visible = true;
          LoadDropDownLists();
          LoadReportingToEmployeeDropDown();
          GetEmployeeDetails();
          GridViewProperties.AssignGridViewProperties(gvEmployeeDesignationHistory);

          //Get Employee Designation History


          int EmployeeIdent = 0;
          EmployeeIdent = ddlEmployee.SelectedValue.cxToInt32();
          LoadEmployeeClientDropDown();
          getEmployeeInfo.EmployeeID = EmployeeIdent;

          DataTable dt0 = getEmployeeInfo.GetEmployeeDesignationHistory();
          txtPastFromDate.Text = "";
          txtPastToDate.Text = "";
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

          //Get Employee Client/Project History

          GridViewProperties.AssignGridViewProperties(gvEmployeeClientProjectHistory);
          DataTable dt1 = getEmployeeInfo.GetEmployeeClientProjectHistory();
          if (dt1.Rows.Count == 0)
          {
            dt1.Rows.Add(dt1.NewRow());
            gvEmployeeClientProjectHistory.DataSource = dt1;
            gvEmployeeClientProjectHistory.DataBind();
            int columncount = gvEmployeeClientProjectHistory.Rows[0].Cells.Count;
            gvEmployeeClientProjectHistory.Rows[0].Cells.Clear();
            gvEmployeeClientProjectHistory.Rows[0].Cells.Add(new TableCell());
            gvEmployeeClientProjectHistory.Rows[0].Cells[0].ColumnSpan = columncount;
            gvEmployeeClientProjectHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("text-align", "Center");
            gvEmployeeClientProjectHistory.Rows[0].Cells[0].Attributes.CssStyle.Add("font-weight", "bold");
            gvEmployeeClientProjectHistory.Rows[0].Cells[0].Text = "Currently there are no entries to display";
          }
          else
          {
            gvEmployeeClientProjectHistory.DataSource = dt1;
            gvEmployeeClientProjectHistory.DataBind();
          }

          //Get Current Employee Client Name

          DataTable dt2 = getEmployeeInfo.GetEmployeeCurrentClient();
          if (dt2.Rows.Count != 0)
          {
            DataRow dr = dt2.Rows[0];
            lblClientName.Text = dr["ClientName"].cxToString();
          }
          else
          {
            lblClientName.Text = "--";
          }

        }
        else
        {
          trEmployeeDetails.Visible = false;
          divEmp.Visible = false;
          lblbasic.Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ddlEmployee_SelectedIndexChanged", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ddlClientName_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (ddlClientName.SelectedIndex > 0)
      {
        int ClientIdent = Convert.ToInt32(ddlClientName.SelectedValue);
        LoadEmployeeClientProjectDropDown(ClientIdent);
      }
      else
      {
        ddlProjectName.Items.Clear();
      }

    }

    /// <summary>
    /// closeBtn_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void closeBtn_Click(object sender, EventArgs e)
    {
      ddlEmployee_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// ibtnReportingToSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnReportingToSubmit_Click(object sender, EventArgs e)
    {
      try
      {
        // Create a new ReportingTo Object
        ReportingToDL empReportingTo = new ReportingToDL();

        // Set as Add
        empReportingTo.AddEditOption = 0;

        // Assign values to the ReportingTo Object
        empReportingTo.ID = hdfID.Value.cxToInt32();
        empReportingTo.EmployeeID = ddlEmployee.SelectedValue.cxToInt32();
        if (ddlReportingToEmployeeEdit.SelectedValue != "0")
        {
          empReportingTo.ReportingToID = ddlReportingToEmployeeEdit.SelectedValue.cxToInt32();
        }
        else
        {
          empReportingTo.ReportingToID = 0;
        }
        empReportingTo.ReportingType = "F";

        // Add / Edit / Delete the ReportingTo
        TransactionResult result;
        empReportingTo.ScreenMode = ScreenMode.Add;
        result = empReportingTo.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully returned, 
        if (result.Status == TransactionStatus.Success)
        {
          ddlEmployee_SelectedIndexChanged(sender, e);
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ibtnReportingToSubmit_Click", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnLeaveApprovarSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnLeaveApprovarSubmit_Click(object sender, EventArgs e)
    {
      try
      {
        TransactionResult result;

        EmployeeDL updateLeaveApprovar = new EmployeeDL();
        updateLeaveApprovar.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        updateLeaveApprovar.LeaveApprovar = txtLeaveApprovarEdit.Text;
        result = updateLeaveApprovar.UpdateLeaveApprovar();

        // Display the status of the return
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully returned, 
        if (result.Status == TransactionStatus.Success)
        {
          ddlEmployee_SelectedIndexChanged(sender, e);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ibtnLeaveApprovarSubmit_Click", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnDesignationSubmit_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnDesignationSubmit_Click(object sender, EventArgs e)
    {
      try
      {
        TransactionResult result;

        EmployeeDL updateEmployeeDesignation = new EmployeeDL();
        updateEmployeeDesignation.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
        updateEmployeeDesignation.DesignationID = Convert.ToInt32(ddlDesignation.SelectedValue);
        updateEmployeeDesignation.DesignationDescription = ddlDesignation.SelectedItem.Text;
        string dtFormat = _dateFormat;
        updateEmployeeDesignation.FromDate = Convert.ToDateTime(txtFromDate1.Text);
        result = updateEmployeeDesignation.UpdateEmployeeDesignation();

        // Display the status of the return
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
        // If successfully returned, 
        if (result.Status == TransactionStatus.Success)
        {
          txtFromDate1.Text = "";
          ddlEmployee_SelectedIndexChanged(sender, e);

          //Get Employee Designation History
          getEmployeeInfo.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
          DataTable dt0 = getEmployeeInfo.GetEmployeeDesignationHistory();
          gvEmployeeDesignationHistory.DataSource = dt0;
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ibtnReportingToSubmit_Click", ex.Message, new ACEConnection());
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

    /// <summary>
    /// gvEmployeeClientProjectHistory_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeClientProjectHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial1");
          lblSerial.Text = ((gvEmployeeDesignationHistory.PageIndex * gvEmployeeDesignationHistory.PageSize) + e.Row.RowIndex + 1).ToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeClientProjectHistory_RowDataBound", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeDesignationHistory_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeDesignationHistory_RowCommand(object sender, CommandEventArgs e)
    {
      try
      {
        if (e.CommandName == "desgEdit")
        {
          string argument = e.CommandArgument.ToString();
          int DesigID = Convert.ToInt32(argument.Split(',')[0]);
          string FromDate = argument.Split(',')[1].ToString();
          string ToDate = argument.Split(',')[2].ToString();
          ddlDesignation3.ClearSelection();
          ddlDesignation3.SelectedValue = DesigID.ToString();
          ddlDesignation3.Enabled = false;
          txtEditFromDate.Text = FromDate;
          txtEditToDate.Text = ToDate;
          desigStatus.Value = "1";
          mpeEditDesignation.Show();
        }
        else if (e.CommandName == "desgDelete")
        {
          int DesignationID = Convert.ToInt32(e.CommandArgument);
          TransactionResult result;
          EmployeeDL deletePastDesignation = new EmployeeDL();
          deletePastDesignation.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
          deletePastDesignation.DesignationID = DesignationID;
          result = deletePastDesignation.DeletePastDesignation();
          System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
          sb1.Append("<script>alert('" + result.Message.ToString() + ".');");
          sb1.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);
          // If successfully returned, 
          if (result.Status == TransactionStatus.Success)
          {
            ddlEmployee_SelectedIndexChanged(sender, e);
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeDesignationHistory_RowCommand", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeClientProjectHistory_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeClientProjectHistory_RowCommand(object sender, CommandEventArgs e)
    {
      try
      {

        if (e.CommandName == "CPDelete")
        {
          string argument = e.CommandArgument.ToString();
          int ClientID = Convert.ToInt32(argument.Split(',')[0]);
          int ProjectID = Convert.ToInt32(argument.Split(',')[1]);
          hfSNo.Value = argument.Split(',')[2].ToString();
          TransactionResult result;
          EmployeeDL deleteClientProjectInfo = new EmployeeDL();
          deleteClientProjectInfo.SNo = Convert.ToInt32(hfSNo.Value);
          result = deleteClientProjectInfo.DeleteClientProjectInfo();
          System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
          sb1.Append("<script>alert('" + "Selected Employee is associated with this project." + ".');");
          sb1.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);
          // If successfully returned, 
          if (result.Status == TransactionStatus.Success)
          {
            ddlEmployee_SelectedIndexChanged(sender, e);
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeClientProjectHistory_RowCommand", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// btnPastDesignationSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPastDesignationSave_Click(object sender, EventArgs e)
    {
      TransactionResult result;
      EmployeeDL addPastDesignation = new EmployeeDL();
      addPastDesignation.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);
      if (desigStatus.Value == "0")
      {
        addPastDesignation.DesignationID = Convert.ToInt32(ddlDesignation2.SelectedValue);
        addPastDesignation.DesignationDescription = ddlDesignation2.SelectedItem.Text;
        DateTime FromDate = Convert.ToDateTime(txtPastFromDate.Text);
        DateTime ToDate = Convert.ToDateTime(txtPastToDate.Text);
        if (ToDate < FromDate)
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Warning: To Date Should Be Greater Than Or Equal To From Date" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          return;
        }
        addPastDesignation.FromDate = FromDate;
        addPastDesignation.ToDate = ToDate;
        int PeriodEntryCount = 0;
        DataTable dc = addPastDesignation.CheckEmployeePastDesignationPeriod();
        foreach (DataRow dRow in dc.Rows)
        {
          PeriodEntryCount = Convert.ToInt32(dRow["EntryExists"]);
        }

        if (PeriodEntryCount > 0)
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Warning: Designation already exists for the mentioned period" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          return;
        }
        int DesignationEntryCount = 0;
        DataTable dd = addPastDesignation.CheckEmployeeDuplicateDesignation();
        foreach (DataRow dRow in dd.Rows)
        {
          DesignationEntryCount = Convert.ToInt32(dRow["EntryExists"]);
        }

        if (DesignationEntryCount > 0)
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Warning: Designation already exists for the selected employee" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          return;
        }
      }
      else
      {
        addPastDesignation.DesignationID = Convert.ToInt32(ddlDesignation3.SelectedValue);
        addPastDesignation.DesignationDescription = ddlDesignation3.SelectedItem.Text;
        DateTime FromDate = Convert.ToDateTime(txtEditFromDate.Text);
        DateTime ToDate = Convert.ToDateTime(txtEditToDate.Text);
        if (ToDate < FromDate)
        {
          System.Text.StringBuilder sb = new System.Text.StringBuilder();
          sb.Append("<script>alert('" + "Warning: To Date Should Be Greater Than Or Equal To From Date" + ".');");
          sb.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);
          return;
        }
        addPastDesignation.FromDate = FromDate;
        addPastDesignation.ToDate = ToDate;
      }
      addPastDesignation.designationStatus = Convert.ToInt32(desigStatus.Value);
      result = addPastDesignation.AddPastDesignation();

      // Display the status of the return
      System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
      sb1.Append("<script>alert('" + result.Message.ToString() + ".');");
      sb1.Append("</script>");
      ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);
      // If successfully returned, 
      if (result.Status == TransactionStatus.Success)
      {
        txtPastFromDate.Text = "";
        txtPastToDate.Text = "";
        ddlEmployee_SelectedIndexChanged(sender, e);
      }
    }

    /// <summary>
    /// CPSubmitBtn_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CPSubmitBtn_Click(object sender, EventArgs e)
    {
      TransactionResult result;
      EmployeeDL addEmployeeClientProjectDetails = new EmployeeDL();
      addEmployeeClientProjectDetails.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);

      if (ddlProjectName.SelectedIndex > 0)
      {
        addEmployeeClientProjectDetails.ProjectID = Convert.ToInt32(ddlProjectName.SelectedValue);
        addEmployeeClientProjectDetails.ProjectName = ddlProjectName.SelectedItem.Text;
      }

      if (hfCPHistoryStat.Value != "")
      {
        addEmployeeClientProjectDetails.EmpClientProjStatus = Convert.ToInt32(hfCPHistoryStat.Value);
      }

      if (ddlClientName.SelectedIndex > 0)
      {
        addEmployeeClientProjectDetails.ClientID = Convert.ToInt32(ddlClientName.SelectedValue);
        addEmployeeClientProjectDetails.ClientName = ddlClientName.SelectedItem.Text;
      }
      result = addEmployeeClientProjectDetails.AddEmployeeClientProjectDetails();
      // Display the status of the return
      System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
      sb1.Append("<script>alert('" + result.Message.ToString() + ".');");
      sb1.Append("</script>");
      ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);
      // If successfully returned, 
      if (result.Status == TransactionStatus.Success)
      {

        ddlProjectName.ClearSelection();
        ddlEmployee_SelectedIndexChanged(sender, e);
      }
    }

    #endregion

    #region Employee Job Status Grid

    /// <summary>
    /// gvEmployeeJobStatus_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeJobStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      try
      {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          // Set the Serial Number
          Label lblSerial = (Label)e.Row.FindControl("lblSerial");
          lblSerial.Text = ((gvEmployeeJobStatus.PageIndex * gvEmployeeJobStatus.PageSize) + e.Row.RowIndex + 1).ToString();

          // Attach Confirmation to the Delete Image Button
          ImageButton lnkDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
          lnkDelete.Attributes.Add("OnClick", "return confirm('Are you sure you want to delete?');");

          string dtFormat = _dateFormat;
          DateTime dTime;
          if (e.Row.Cells[5].Text != "")
          {
            dTime = Convert.ToDateTime(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = Common.CheckBlank(dTime.ToString(dtFormat));
          }

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
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeJobStatus_RowDataBound", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeJobStatus_Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeJobStatus_Sorting(object sender, GridViewSortEventArgs e)
    {
      try
      {
        // Get Employee Details
        GetEmployeeDetails();

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
        //  case "JobStatusDescription":
        //    //Sort by JobStatus Description                                  
        //    _currentEmployee.EmployeeJobStatuses.Sort(new EmployeeJobStatusComparer_byJobStatusDesc());
        //    break;
        //  case "FromDate":
        //    //Sort by From Date                                
        //    _currentEmployee.EmployeeJobStatuses.Sort(new EmployeeJobStatusComparer_byFromDate());
        //    break;
        //}

        // If descending order - reverse the list
        if (ViewState["SortDirection"].ToString() == "DESC")
          _currentEmployee.EmployeeJobStatuses.Reverse();

        // Assign the list of Employee JobStatus after sorting to the grid 
        gvEmployeeJobStatus.DataSource = _currentEmployee.EmployeeJobStatuses;
        gvEmployeeJobStatus.DataBind();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeJobStatus_Sorting", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeJobStatus_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeJobStatus_RowEditing(object sender, GridViewEditEventArgs e)
    {
      try
      {
        // Set the Control Values in the Popup from the Selected Grid Row
        hdfEmployeeJobStatusID.Value = gvEmployeeJobStatus.Rows[e.NewEditIndex].Cells[1].Text;

        ddlJobStatus.ClearSelection();
        ddlJobStatus.Items.Insert(0, gvEmployeeJobStatus.Rows[e.NewEditIndex].Cells[4].Text);
        ddlJobStatus.Items[0].Value = gvEmployeeJobStatus.Rows[e.NewEditIndex].Cells[3].Text;
        ddlJobStatus.SelectedValue = gvEmployeeJobStatus.Rows[e.NewEditIndex].Cells[3].Text;
        ddlJobStatus.Enabled = false;

        txtFromDate.Text = gvEmployeeJobStatus.Rows[e.NewEditIndex].Cells[5].Text;

        lblPopupHeaderJobStatus.Text = "Edit Job Status";

        // Show the Popup
        mpeEditJobStatus.Show();
        e.Cancel = true;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeJobStatus_RowEditing", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// gvEmployeeJobStatus_RowDeleting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEmployeeJobStatus_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
      try
      {
        TransactionResult result;
        // Get the selected row's Employee JobStatus id
        int eJobStatusIDToDelete = Convert.ToInt32(gvEmployeeJobStatus.DataKeys[e.RowIndex].Value);

        // Delete the selected Employee JobStatus
        EmployeeJobStatusDL deleteEmployeeJobStatus = new EmployeeJobStatusDL(eJobStatusIDToDelete, false);
        deleteEmployeeJobStatus.ScreenMode = ScreenMode.Delete;
        result = deleteEmployeeJobStatus.Commit();

        // Display the status of the delete
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successfully deleted, 
        if (result.Status == TransactionStatus.Success)
        {
          GetEmployeeDetails();
          LoadDropDownLists();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "gvEmployeeJobStatus_RowDeleting", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnJobStatusSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnJobStatusSave_Click(object sender, EventArgs e)
    {
      try
      {
        // Validate the entries in JobStatus Popup Form
        //if (!JobStatusPopupValidation()) { return; }

        // Create a new EmployeeJobStatus Object
        EmployeeJobStatusDL empJobStatus = new EmployeeJobStatusDL();

        // Set whether Add / Edit
        if (hdfEmployeeJobStatusID.Value.ToString() != "0")
          empJobStatus.AddEditOption = 1;
        else
          empJobStatus.AddEditOption = 0;

        // Assign values to the EmployeeJobStatus Object
        empJobStatus.EmployeeJobStatusID = Convert.ToInt32(hdfEmployeeJobStatusID.Value);
        empJobStatus.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue.ToString());
        empJobStatus.JobStatusID = Convert.ToInt32(ddlJobStatus.SelectedValue);

        string dtFormat = _dateFormat;
        DateTime dTime;
        dTime = DateTime.ParseExact(Common.CheckBlank(txtFromDate.Text), dtFormat, null);
        empJobStatus.FromDate = dTime;

        empJobStatus.AuditUserID = _userID;

        // Add / Edit the Employee JobStatus
        TransactionResult result;
        empJobStatus.ScreenMode = ScreenMode.Add;
        result = empJobStatus.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");
        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        // If successful, get the Employee details
        if (result.Status == TransactionStatus.Success)
        {
          hdfEmployeeJobStatusID.Value = "0";
          ddlJobStatus.ClearSelection();
          ddlJobStatus.Enabled = true;
          txtFromDate.Text = "";

          GetEmployeeDetails();
          LoadDropDownLists();
          LoadReportingToEmployeeDropDown();
          lblPopupHeaderJobStatus.Text = "Add Job Status";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ibtnJobStatusSave_Click", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// ibtnJobStatusCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnJobStatusCancel_Click(object sender, EventArgs e)
    {
      try
      {
        // Controls cleared on Cancel
        hdfEmployeeJobStatusID.Value = "0";
        ddlJobStatus.ClearSelection();
        ddlJobStatus.Enabled = true;
        txtFromDate.Text = "";
        lblPopupHeaderJobStatus.Text = "Add Job Status";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "ibtnJobStatusCancel_Click", ex.Message, new ACEConnection());
      }
    }


    /// <summary>
    /// Update Employee Job Starting Time
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ibtnEmployeeAdditionalSave_Click(object sender, EventArgs e)
    {
      TransactionResult result;

      EmployeeDL settingJobStatusTime = new EmployeeDL();
      settingJobStatusTime.EmployeeID = Convert.ToInt32(ddlEmployee.SelectedValue);

      string minValue = ddlDurationInMinutes.SelectedValue;

      if (minValue.Split('.')[1] != null)
        minValue = minValue.Split('.')[1].Substring(0, 2);
      else
        minValue = "0";

      DateTime dtSetTime = new DateTime(1900, 1, 1);
      dtSetTime = dtSetTime.AddHours(Convert.ToDouble(ddlDurationInHours.SelectedValue));
      dtSetTime = dtSetTime.AddMinutes(Convert.ToDouble(minValue));

      settingJobStatusTime.JobStartTime = Convert.ToDateTime(dtSetTime);
      result = settingJobStatusTime.SettingJobStartTime();

      // Display the status of the return
      System.Text.StringBuilder sb = new System.Text.StringBuilder();
      sb.Append("<script>alert('" + result.Message.ToString() + ".');");
      sb.Append("</script>");
      ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

      // If successfully returned, 
      if (result.Status == TransactionStatus.Success)
      {
        ddlEmployee_SelectedIndexChanged(sender, e);
      }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// To get the employee additional details - administrative details 
    /// </summary>                
    private void GetEmployeeDetails()
    {
      try
      {
        // Get the selected employee's administrative details               
        _currentEmployee = new EmployeeDL();

        _currentEmployee.GetEmployeeAdditional(Convert.ToInt32(ddlEmployee.SelectedValue));
        //_currentEmployee.
        AssignValues();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "GetEmployeeDetails", ex.Message, new ACEConnection());
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
        lblEmployeeCodeValue.Text = _currentEmployee.EmployeeCode;

        String NameAndInitial = "";
        if (_currentEmployee.Initial.Trim().Length > 0)
          NameAndInitial = _currentEmployee.FName + ' ' + _currentEmployee.Initial;
        else
          NameAndInitial = _currentEmployee.FName;
        lblFNameAndInitialValue.Text = NameAndInitial;

        lblDepartmentValue.Text = Common.CheckBlank(_currentEmployee.DepartmentDescription);
        lblOfficialMailIDValue.Text = Common.CheckBlank(_currentEmployee.OfficeEmailID);
        lblEmployeeDesignationValue.Text = Common.CheckBlank(_currentEmployee.DesignationDescription);

        DateTime dt = new DateTime();

        if (_currentEmployee.JobStartTime != null)
        {
          dt = _currentEmployee.JobStartTime.Value;
          ddlDurationInHours.SelectedValue = dt.Hour.ToString();

          string stMinutes = "0." + dt.Minute.ToString();
          if (stMinutes == "0.0")
          {
            stMinutes = "0.00";
          }
          ddlDurationInMinutes.SelectedValue = stMinutes.ToString();
        }
        else
        {
          ddlDurationInMinutes.SelectedIndex = 0;
          ddlDurationInHours.SelectedIndex = 0;
        }

        // Assign the list of Employee Job Statuses
        gvEmployeeJobStatus.DataSource = _currentEmployee.EmployeeJobStatuses;
        gvEmployeeJobStatus.DataBind();

        lblReportingToEmployeeID.Text = _currentEmployee.ReportingToEmployeeID.ToString();
        lblReportingToEmployeeName.Text = Common.CheckBlank(_currentEmployee.ReportingToEmployeeName.ToString());
        txtLeaveApprovarEdit.Text = _currentEmployee.LeaveApprovar.ToString();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "AssignValues", ex.Message, new ACEConnection());
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
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadEmployeeDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Employee's Client
    /// </summary>
    private void LoadEmployeeClientDropDown()
    {
      try
      {
        // Load Client
        ddlClientName.Items.Clear();
        ddlClientName.DataSource = new EmployeeDL().GetEmployeeClientDetails().Tables[0];
        ddlClientName.DataTextField = "ClientName";
        ddlClientName.DataValueField = "ClientID";
        ddlClientName.DataBind();
        ddlClientName.Items.Insert(0, "-- Select One --");
        ddlClientName.Items[0].Value = "";

        // Load Client
        //ddlEditClient.Items.Clear();
        //ddlEditClient.DataSource = new EmployeeDL().GetEmployeeClientDetails().Tables[0];
        //ddlEditClient.DataTextField = "ClientName";
        //ddlEditClient.DataValueField = "ClientID";
        //ddlEditClient.DataBind();
        //ddlEditClient.Items.Insert(0, "-- Select One --");
        //ddlEditClient.Items[0].Value = "0";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadEmployeeDropDown", ex.Message, new ACEConnection());
      }
    }

    private void LoadProjectDDL(int ClientID)
    {
      // Load Project
      //ddlEditProj.Items.Clear();
      //ddlEditProj.DataSource = new EmployeeDL().GetEmployeeClientProjectDetails(ClientID).Tables[0];
      //ddlEditProj.DataTextField = "ProjectName";
      //ddlEditProj.DataValueField = "ProjectID";
      //ddlEditProj.DataBind();
      //ddlEditProj.Items.Insert(0, "-- Select One --");
      //ddlEditProj.Items[0].Value = "";
    }

    private void LoadEmployeeClientProjectDropDown(int ClientID)
    {
      try
      {

        // Load Project
        ddlProjectName.Items.Clear();

        ddlProjectName.DataSource = new EmployeeDL().GetEmployeeClientProjectDetails(ClientID).Tables[0];
        ddlProjectName.DataTextField = "ProjectName";
        ddlProjectName.DataValueField = "ProjectID";
        ddlProjectName.DataBind();
        ddlProjectName.Items.Insert(0, "-- Select One --");
        ddlProjectName.Items[0].Value = "";

        if (new EmployeeDL().GetEmployeeClientProjectDetails(ClientID).Tables[0].Rows.Count == 0)
        {

          System.Text.StringBuilder sb1 = new System.Text.StringBuilder();
          sb1.Append("<script>alert('" + "There is no project for this client" + ".');");
          sb1.Append("</script>");
          ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb1.ToString(), false);
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadEmployeeDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For Reporting To Employee
    /// </summary>
    private void LoadReportingToEmployeeDropDown()
    {
      try
      {
        // Load ReportingTo Employee

        ddlReportingToEmployeeEdit.Items.Clear();
        DataView dView = new EmployeeDL().GetEmployeeListByCompanyID(_companyID).Tables[0].DefaultView;
        dView.RowFilter = "EmployeeID <> " + ddlEmployee.SelectedValue.ToString();
        ddlReportingToEmployeeEdit.DataSource = dView;
        ddlReportingToEmployeeEdit.DataTextField = "EmployeeName";
        ddlReportingToEmployeeEdit.DataValueField = "EmployeeID";
        ddlReportingToEmployeeEdit.DataBind();
        ddlReportingToEmployeeEdit.Items.Insert(0, "-- Select One --");
        ddlReportingToEmployeeEdit.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadEmployeeDropDown", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load Lists For JobStatus
    /// </summary>
    private void LoadDropDownLists()
    {
      try
      {
        // Load JobStatus
        ddlJobStatus.Items.Clear();
        ddlJobStatus.DataSource = new JobStatusDL().GetJobStatusListByEmployeeJobStatus(Convert.ToInt32(ddlEmployee.SelectedValue)).Tables[0];
        ddlJobStatus.DataTextField = "JobStatusDescription";
        ddlJobStatus.DataValueField = "JobStatusID";
        ddlJobStatus.DataBind();
        ddlJobStatus.Items.Insert(0, "-- Select One --");
        ddlJobStatus.Items[0].Value = "";

        ddlDesignation.Items.Clear();
        ddlDesignation.DataSource = new DesignationDL().GetDesignationList().Tables[0];
        ddlDesignation.DataTextField = "DesignationDescription";
        ddlDesignation.DataValueField = "DesignationID";
        ddlDesignation.DataBind();
        ddlDesignation.Items.Insert(0, "-- Select One --");
        ddlDesignation.Items[0].Value = "";

        ddlDesignation2.Items.Clear();
        ddlDesignation2.DataSource = new DesignationDL().GetDesignationList().Tables[0];
        ddlDesignation2.DataTextField = "DesignationDescription";
        ddlDesignation2.DataValueField = "DesignationID";
        ddlDesignation2.DataBind();
        ddlDesignation2.Items.Insert(0, "-- Select One --");
        ddlDesignation2.Items[0].Value = "";

        ddlDesignation3.Items.Clear();
        ddlDesignation3.DataSource = new DesignationDL().GetDesignationList().Tables[0];
        ddlDesignation3.DataTextField = "DesignationDescription";
        ddlDesignation3.DataValueField = "DesignationID";
        ddlDesignation3.DataBind();
        ddlDesignation3.Items.Insert(0, "-- Select One --");
        ddlDesignation3.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadDropDownLists", ex.Message, new ACEConnection());
      }
    }

    /// <summary>
    /// Load List For DurationInHours, DurationInMinutes
    /// </summary>
    private void LoadDurationDropDown()
    {
      try
      {
        // Load Duration In Hours
        ddlDurationInHours.Items.Clear();
        for (int i = 0; i < 24; i++)
        {
          ddlDurationInHours.Items.Add(i.ToString());
        }
        ddlDurationInHours.Items.Insert(0, "-- Select One --");
        ddlDurationInHours.Items[0].Value = "";

        // Load Duration In Minutes
        ddlDurationInMinutes.Items.Clear();
        ddlDurationInMinutes.Items.Add("0.00");
        ddlDurationInMinutes.Items.Add("0.15");
        ddlDurationInMinutes.Items.Add("0.30");
        ddlDurationInMinutes.Items.Add("0.45");
        ddlDurationInMinutes.Items.Insert(0, "-- Select One --");
        ddlDurationInMinutes.Items[0].Value = "";
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("EmployeeAdditional.aspx", "", "LoadDurationDropDown", ex.Message, new ACEConnection());
      }
    }

    #endregion

  }
}
