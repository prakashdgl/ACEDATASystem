using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class FeedbackStatusDL : CommonForAllDL
  {
    #region Properties

    public int FeedbackStatusID { get; set; }

    public string FeedbackStatusDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get FeedbackStatus List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetFeedbackStatusList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackStatusList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackStatusDL.cs", "GetFeedbackStatusList", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
