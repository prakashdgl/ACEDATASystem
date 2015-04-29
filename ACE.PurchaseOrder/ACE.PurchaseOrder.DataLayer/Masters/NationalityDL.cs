using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class NationalityDL : CommonForAllDL
  {
    #region Properties

    public int NationalityID { get; set; }

    public string NationalityDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Nationality List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetNationalityList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetNationalityList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "NationalityDL.cs", "GetNationalityList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
