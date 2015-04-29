using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class UniversityDL : CommonForAllDL
  {
    #region Properties

    public int UniversityID { get; set; }

    public string UniversityDescription { get; set; }

    #endregion

    #region Private Methods

    /// <summary>
    /// Get University List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>

    public DataSet GetUniversityList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetUniversityList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "UniversityDL.cs", "GetUniversityList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
