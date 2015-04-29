using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class TitlesDL : CommonForAllDL
  {
    #region Properties

    public int TitleID { get; set; }

    public string TitleDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Title List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetTitleList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetTitlesList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "TitlesDL.cs", "GetTitlesList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
