using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class JobStatusDL : CommonForAllDL
  {
    #region Properties

    public int JobStatusID { get; set; }

    public string JobStatusDescription { get; set; }

    public int LevelNumber { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get JobStatus List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetJobStatusList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetJobStatusList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "JobStatusDL.cs", "GetJobStatusList", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get JobStatus List Not In EmployeeJobStatus For an Employee ID Given
    /// </summary>
    /// <param name="employeeID">Employee ID</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetJobStatusListNotInEmployeeJobStatus(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetJobStatusListNotInEmployeeJobStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "JobStatusDL.cs", "GetJobStatusListNotInEmployeeJobStatus", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get JobStatus based on EmployeeJobStatus For an Employee ID Given
    /// </summary>
    /// <param name="employeeID">Employee ID</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetJobStatusListByEmployeeJobStatus(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetJobStatusListByEmployeeJobStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "JobStatusDL.cs", "GetJobStatusListByEmployeeJobStatus", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
