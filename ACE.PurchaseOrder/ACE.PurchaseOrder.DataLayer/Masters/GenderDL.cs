using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class GenderDL : CommonForAllDL
  {
    #region Properties

    public int GenderID { get; set; }

    public string GenderDescription { get; set; }

    #endregion

    #region Private Methods

    /// <summary>
    /// Get Gender List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetGenderList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetGenderList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "GenderDL.cs", "GetGenderList", ex.Message.ToString(), _myConnection);
        throw;
      }
      return ds;
    }

    #endregion
  }
}
