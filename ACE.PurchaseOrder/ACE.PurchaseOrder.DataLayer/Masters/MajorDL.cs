using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class MajorDL : CommonForAllDL
  {
    #region Properties

    public int MajorID { get; set; }

    public string MajorDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Major List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetMajorList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMajorList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MajorDL.cs", "GetMajorList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
