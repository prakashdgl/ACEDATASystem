using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class MaritalStatusDL : CommonForAllDL
  {
    #region Properties

    public int MaritalStatusID { get; set; }

    public string MaritalStatusDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get MaritalStatus List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetMaritalStatusList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMaritalStatusList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MaritalStatusDL.cs", "GetMaritalStatusList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
