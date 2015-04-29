using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class QualificationDL : CommonForAllDL
  {
    #region Properties

    public int QualificationID { get; set; }

    public string QualificationDescription { get; set; }

    public int Period { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Qualification List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetQualificationList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetQualificationList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "QualificationDL.cs", "GetQualificationList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
