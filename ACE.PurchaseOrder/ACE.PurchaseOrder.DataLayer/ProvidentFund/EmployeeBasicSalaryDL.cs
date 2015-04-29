using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EmployeeBasicSalaryDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    public int EmployeeBasicSalaryId { get; set; }

    public decimal BasicSalary { get; set; }

    public Nullable<DateTime> EffectFrom { get; set; }

    public Nullable<DateTime> EffectTo { get; set; }

    public bool IsActive { get; set; }

    public decimal PercentageValue { get; set; }

    public bool IsDefault { get; set; }

    public string PfAccountNumber { get; set; }

    public DataSet EmployeeDataSet { get; set; }

    #endregion

    #region Constructors

    public EmployeeBasicSalaryDL()
    {
    }

    public EmployeeBasicSalaryDL(int employeeID, bool getAllProperties)
    {
      EmployeeID = employeeID;

      if (getAllProperties)
      {
        EmployeeDataSet = GetEmployeePF();
      }
    }

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
              result = AddEditEmployeeBasicSalary(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              //Adding Employee Basic Salary
              result = AddEmployeeBasicSalary(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Add Edit Employee Basic Salary
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeBasicSalary(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditEmployeeBasicSalary";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      // ID, Code
      db.AddInParameter(dbCommand, "EmployeeBasicSalaryID", DbType.Int32, EmployeeBasicSalaryId);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);

      db.AddInParameter(dbCommand, "PFAccountNumber", DbType.String, PfAccountNumber);

      db.AddInParameter(dbCommand, "BasicSalary", DbType.Decimal, BasicSalary);

      db.AddInParameter(dbCommand, "PercentageValue", DbType.Decimal, PercentageValue);

      if (EffectFrom != null)
        db.AddInParameter(dbCommand, "EffectFrom", DbType.Date, EffectFrom);

      if (EffectTo != null)
        db.AddInParameter(dbCommand, "EffectTo", DbType.DateTime, EffectTo);

      db.AddInParameter(dbCommand, "IsActive", DbType.Boolean, IsActive);

      db.AddInParameter(dbCommand, "IsDefault", DbType.Boolean, IsDefault);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

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
    /// Add Employee Basic Salary
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEmployeeBasicSalary(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEmployeeBasicSalary";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "BasicSalary", DbType.Decimal, BasicSalary);
      db.AddInParameter(dbCommand, "EffectFrom", DbType.Date, EffectFrom);
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
    /// Gets the list of employees that match the specified search text (in any of the selected fields) By CompanyID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="searchText"></param>
    /// <param name="activatedDeactivated"></param>
    /// <returns>DataSet Containing the List of Employees By CompanyID</returns>
    public DataSet GetEmployeeBasicSalaryByCompanyID(int companyID, string searchText, bool activatedDeactivated)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeBasicSalaryByCompanyID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "SearchText", DbType.String, searchText);
        db.AddInParameter(dbCommand, "ActivatedDeactivated", DbType.Boolean, activatedDeactivated);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "GetEmployeeBasicSalaryByCompanyID", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListBasicSalaryByCompanyID(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListBasicSalaryByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "GetEmployeeListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the list of employees's Basic Salary Increment history By CompanyID and EmployeeID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <returns>DataTable Containing the List of Employees By CompanyID and EmployeeID</returns>
    public DataTable GetEmployeeBasicSalaryByCompanyIDAndEmployeeID(int companyID, int employeeID)
    {
      DataSet ds = new DataSet();
      DataTable dt = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeBasicSalaryByCompanyIDAndEmployeeID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
        dt = ds.Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "spGetEmployeeBasicSalaryByCompanyIDAndEmployeeID", ex.Message.ToString(), _myConnection);
      }
      return dt;
    }

    /// <summary>
    /// Gets the employees's Current Basic Salary ByEmployeeID
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns>DataTable Containing the List of Employees By CompanyID and EmployeeID</returns>
    public Decimal GetEmployeeCurrentBasicSalaryByEmployeeID(int employeeID)
    {
      Decimal currentBasicSalary = 0 ;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeCurrentBasicSalaryByEmployeeID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        currentBasicSalary = Convert.ToDecimal(db.ExecuteScalar(dbCommand));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "spGetEmployeeCurrentBasicSalaryByEmployeeID", ex.Message.ToString(), _myConnection);
      }
      return currentBasicSalary;
    }

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeePFAddingListByCompanyID(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeePFAddingListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "GetEmployeePFAddingListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee PF
    /// </summary>
    /// <returns></returns>
    private DataSet GetEmployeePF()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeePFAddingListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, EmployeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeBasicSalaryDL.cs", "GetEmployeePFAddingListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
