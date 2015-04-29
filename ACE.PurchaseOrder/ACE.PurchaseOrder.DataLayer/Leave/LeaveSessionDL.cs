using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class LeaveSessionDL : CommonForAllDL
  {
    #region Properties

    public int LeaveSessionID { get; set; }

    public string LeaveSessionDescription { get; set; }

    public new int AddEditOption { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get LeaveSession List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveSessionList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveSessionList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveSessionDL.cs", "GetLeaveSessionList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveSession List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveSessionList(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveSessionListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveSessionDL.cs", "GetLeaveSessionListByCompanyID", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    #endregion
  }
}
