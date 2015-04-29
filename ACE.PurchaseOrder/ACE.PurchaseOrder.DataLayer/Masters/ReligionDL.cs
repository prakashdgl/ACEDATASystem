using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class ReligionDL : CommonForAllDL
  {
    #region Properties

    public int ReligionID { get; set; }

    public string ReligionDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Religion List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetReligionList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetReligionList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ReligionDL.cs", "GetReligionList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
