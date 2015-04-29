using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class LeaveTypeDL : CommonForAllDL
  {
    #region Properties

    public int LeaveTypeID { get; set; }

    public string LeaveTypeDescription { get; set; }

    #endregion

    #region Constructor

    public LeaveTypeDL()
    {
    }

    public LeaveTypeDL(int leaveTypeID, bool getAllProperties)
    {
      LeaveTypeID = leaveTypeID;
      if (getAllProperties)
      {
        GetLeaveType();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Get LeaveType List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveTypeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveTypeList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveTypeDL.cs", "GetLeaveTypeList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get Leave Type
    /// </summary>
    private void GetLeaveType()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveType";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, LeaveTypeID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              LeaveTypeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LeaveTypeID")));
              LeaveTypeDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveTypeDescription")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveTypeDL.cs", "GetLeaveType", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    #endregion
  }
}
