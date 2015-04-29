using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EmployeeLOPDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    public int EmployeeLOP { get; set; }

    public Nullable<DateTime> EffectFrom { get; set; }

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
              //Adding Or Editing Employee
              result = AddEditEmployeeLOP(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              ////Adding Employee Basic Salary
              //result = AddEmployeeBasicSalary(db, transaction);
              //if (result.Status == TransactionStatus.Failure)
              //{
              //    transaction.Rollback();
              //    return result;
              //}
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeLOPDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeLOPDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Add Edit Employee LOP
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeLOP(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditEmployeeLOP";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "LOPinDays", DbType.Decimal, EmployeeLOP);
      db.AddInParameter(dbCommand, "MonthYear", DbType.Date, EffectFrom);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      EmployeeID = returnValue;

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
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
    /// Get Employee LOP By EmployeeID And Month Year
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns>DataTable Containing the List of Employees By CompanyID and EmployeeID</returns>
    public int GetEmployeeLOPByEmployeeIDAndMonthYear(int employeeID)
    {
      int employeeLOP = 0;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeLOPByEmployeeIDAndMonthYear");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "MonthYear", DbType.Date, EffectFrom);
        employeeLOP = Convert.ToInt32(db.ExecuteScalar(dbCommand));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLOPDL.cs", "spGetEmployeeLOPByEmployeeIDAndMonthYear", ex.Message.ToString(), _myConnection);
      }
      return employeeLOP;
    }

    #endregion
  }
}
