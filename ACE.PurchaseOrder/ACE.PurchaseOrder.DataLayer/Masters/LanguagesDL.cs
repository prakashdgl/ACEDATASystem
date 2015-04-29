using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class LanguagesDL : CommonForAllDL
  {
    #region Properties

    public int LanguageID { get; set; }

    public string LanguageDescription { get; set; }

    #endregion

    #region Methods

     /// <summary>
    /// Get Language List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLanguageList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLanguageList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LanguageDL.cs", "GetLanguageList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Language List Not In EmployeeLanguageKnown For an Employee ID Given
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLanguageListNotInEmployeeLanguageKnown(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLanguageListNotInEmployeeLanguageKnown";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LanguageDL.cs", "GetLanguageListNotInEmployeeLanguageKnown", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
