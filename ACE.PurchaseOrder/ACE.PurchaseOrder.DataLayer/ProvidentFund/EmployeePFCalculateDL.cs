using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EmployeePFCalculateDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Generate EPF 
    /// </summary>
    /// <param name="userID">userID</param>
    /// <param name="monthYear">monthYear</param>
    public void GenerateEPF(int userID, DateTime monthYear)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGenerateEPF";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "GeneratedUserID", DbType.Int32, userID);
        db.AddInParameter(dbCommand, "MonthYear", DbType.Date, monthYear);

        db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeePFCalculateDL.cs", "GenerateEPF", ex.Message, _myConnection);
        throw;
      }
    }

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="userID">userID</param>
    /// <param name="monthYear">monthYear</param>
    public int IsSalaryGenerated(DateTime monthYear)
    {
      int rows;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spIsSalaryGenerated";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "MonthYear", DbType.Date, monthYear);

        rows = (int)db.ExecuteScalar(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeePFCalculateDL.cs", "IsSalaryGenerated", ex.Message, _myConnection);
        throw;
      }
      return rows;
    }

    #endregion
  }
}
