using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class LeaveStatusDL : CommonForAllDL
  {
    #region Properties

    public int LeaveStatusID { get; set; }

    public string LeaveStatusDescription { get; set; }

    #endregion

    #region Constructors

    public LeaveStatusDL()
    {
    }

    public LeaveStatusDL(int leaveStatusID, bool getAllProperties)
    {
      LeaveStatusID = leaveStatusID;
      if (getAllProperties)
      {
        GetLeaveStatus();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get LeaveStatus List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveStatusList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveStatusList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveStatusDL.cs", "GetLeaveStatusList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get Leave Status
    /// </summary>
    private void GetLeaveStatus()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, LeaveStatusID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              LeaveStatusID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LeaveStatusID")));
              LeaveStatusDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveStatusDescription")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveStatusDL.cs", "GetLeaveStatus", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    #endregion
  }
}
