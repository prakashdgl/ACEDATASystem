using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EPFRevisionFormEntryDL : CommonForAllDL
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

    #endregion

    #region Methods

    /// <summary>
    /// Get Employee EPF Revision By CompanyID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="searchText"></param>
    /// <param name="activatedDeactivated"></param>
    /// <returns>DataSet Containing the List of Employees By CompanyID</returns>
    public DataSet GetEmployeeEPFRevisionByCompanyID(int companyID, string searchText, bool activatedDeactivated)
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
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EPFRevisionFormEntryDL.cs", "GetEmployeeEPFRevisionByCompanyID", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the employees EPF Details based on CompanyID and EmployeeID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <returns>percentage value</returns>
    public string GetEmployeeEPFDetailsByCompanyID(int companyID, int employeeID)
    {
      string percentageVal="";
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEPFContributionByCompanyIDAndEmployeeID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        percentageVal = Convert.ToString(db.ExecuteScalar(dbCommand));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EPFRevisionFormEntryDL.cs", "spGetEPFContributionByCompanyIDAndEmployeeID", ex.Message.ToString(), _myConnection);
      }
      return percentageVal;
    }

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
              //Adding EPF Details
              result = AddEditEPFRevision(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
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
        }
      }
      return null;
    }

    /// <summary>
    /// Add Edit EPF Revision
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEPFRevision(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditEPFRevision";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      // ID, Code
      db.AddInParameter(dbCommand, "EmployeePFPercentageID", DbType.Int32, EmployeeBasicSalaryId);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      if (EffectFrom != null)
        db.AddInParameter(dbCommand, "EffectFrom", DbType.Date, EffectFrom);

      db.AddInParameter(dbCommand, "PercentageValue", DbType.Decimal, PercentageValue);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      EmployeeID = returnValue;

      if (returnValue == -1)
      {
        return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      else
      {
        return new TransactionResult(TransactionStatus.Success, "Successfully Added");
      }
    }

    #endregion
  }
}
