using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class TimeSheetDL : CommonForAllDL
  {
    #region Properties

    public int TimeSheetID { get; set; }

    public DateTime DateWorked { get; set; }

    public int EmployeeID { get; set; }

    public int ProjectID { get; set; }

    public int ProjectTaskID { get; set; }

    public int ActivityID { get; set; }

    public string TimeSheetDescription { get; set; }

    public int DurationInHours { get; set; }

    public Decimal DurationInMinutes { get; set; }

    public bool IsBillable { get; set; }

    public int BillableHours { get; set; }

    public int BillableMinutes { get; set; }

    public TimeSheetStatus TimeSheetStatusID { get; set; }

    public Nullable<DateTime> DateSubmitted { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Commit
    /// </summary>
    /// <returns>TransactionResult - Success / Failure</returns>
    public TransactionResult Commit()
    {
      TransactionResult result = null;
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      using (DbConnection connection = db.CreateConnection())
      {
        connection.Open();
        DbTransaction transaction = connection.BeginTransaction();
        try
        {
          switch (ScreenMode)
          {
            case ScreenMode.Add:
              //Adding Or Editing TimeSheetEntry
              result = AddEditTimeSheet(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            //NOT EDIT - Code added for Timesheet update - by Palani
            case ScreenMode.Edit:
              result = UpdateTimeSheet(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
              result = DeleteTimeSheet(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.View:
              break;
          }
          transaction.Commit();
          return result;
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          if (ScreenMode == ScreenMode.Add)
          {
            ErrorLog.LogErrorMessageToDB("", "TimeSheetDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "TimeSheetDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "TimeSheetDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get TimeSheet List By EmployeeID And DateWorked
    /// </summary>
    /// <param name="employeeID">EmployeeID</param>
    /// <param name="dateWorked">DateWorked</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetTimeSheetListByEmployeeIDAndDateWorked(int employeeID, DateTime dateWorked)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetTimeSheetListByEmployeeIDAndDateWorked";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "DateWorked", DbType.DateTime, dateWorked);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "TimeSheetDL.cs", "GetTimeSheetListByEmployeeIDAndDateWorked", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Add Edit TimeSheet
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditTimeSheet(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditTimeSheet";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      // ID
      db.AddInParameter(dbCommand, "TimeSheetID", DbType.Int32, TimeSheetID);
      db.AddInParameter(dbCommand, "DateWorked", DbType.DateTime, DateWorked);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);

      // Project Details
      db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
      db.AddInParameter(dbCommand, "ProjectTaskID", DbType.Int32, ProjectTaskID);
      db.AddInParameter(dbCommand, "ActivityID", DbType.Int32, ActivityID);

      db.AddInParameter(dbCommand, "DurationInHours", DbType.Int32, DurationInHours);
      db.AddInParameter(dbCommand, "DurationInMinutes", DbType.Decimal, DurationInMinutes);

      db.AddInParameter(dbCommand, "IsBillable", DbType.Boolean, IsBillable);
      if (IsBillable == true)
      {
        db.AddInParameter(dbCommand, "BillableHours", DbType.Decimal, BillableHours);
        db.AddInParameter(dbCommand, "BillableMinutes", DbType.Decimal, BillableMinutes);
      }

      db.AddInParameter(dbCommand, "TimeSheetDescription", DbType.String, TimeSheetDescription);

      db.AddInParameter(dbCommand, "TimeSheetStatusID", DbType.Int32, TimeSheetStatusID);
      if (DateSubmitted != null)
        db.AddInParameter(dbCommand, "DateSubmitted", DbType.DateTime, DateSubmitted);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      TimeSheetID = returnValue;

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      else
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Added");
      }
    }

    /// <summary>
    /// Delete TimeSheet
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteTimeSheet(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteTimeSheet");
      db.AddInParameter(dbCommand, "TimeSheetID", DbType.Int32, TimeSheetID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    /// <summary>
    /// Update TimeSheet
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult UpdateTimeSheet(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spUpdateTimeSheet");
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "TimeSheetStatusID", DbType.Int32, TimeSheetStatusID);
      db.AddInParameter(dbCommand, "TimeSheetID", DbType.Int32, TimeSheetID);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");

    }

    #endregion
  }
}
