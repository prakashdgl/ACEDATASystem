using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class WorkFromHomeDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    public DateTime InTime { get; set; }

    public DateTime Outime { get; set; }

    public int WorkFromHome { get; set; }

    public DateTime StartDate { get; set; }

    public Nullable<DateTime> EndDate { get; set; }

    public new int AddEditOption { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Decides whether to Call Add/Edit/Delete method
    /// And Calls the appropriate method
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
              //Adding Employee
              result = AddWorkFromHomeEmployee(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              //Editing Employee
              result = EditWorkFromHomeEmployee(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Job Start Time
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeJST()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeDetail";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "GetEmployeeJST", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Work From Home Histoy
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeWorkFromHomeHistoy()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spWorkFromHomeHistory";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "GetEmployeeWorkFromHomeHistoy", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// AddWorkFromHomeEmployee
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddWorkFromHomeEmployee(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEmployeeWorkFromHome";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "InTime", DbType.DateTime, InTime);
        db.AddInParameter(dbCommand, "OutTime", DbType.DateTime, Outime);
        db.AddInParameter(dbCommand, "WorkFromHome", DbType.Int32, WorkFromHome);
        db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, StartDate);
        db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, EndDate);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                          DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding Employee");
        else
          return new TransactionResult(TransactionStatus.Success, "Employee Successfully added");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "AddWorkFromHomeEmployee", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// EditWorkFromHomeEmployee
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult EditWorkFromHomeEmployee(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spUpdateWorkFromHome";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, StartDate);
        db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, EndDate);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                         DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure updating Employee");
        else
          return new TransactionResult(TransactionStatus.Success, "Employee Successfully updated");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "EditWorkFromHomeEmployee", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Get Employee Work From Home Histoy
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeWorkFromHomeByEmpID()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spWorkFromHomeHistoryByEmpID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "WorkFromHomeDL.cs", "GetEmployeeWorkFromHomeHistoy", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}

