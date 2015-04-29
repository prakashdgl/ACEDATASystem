using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class FeedbackTypeDL : CommonForAllDL
  {
    #region Properties

    public int FeedbackTypeID { get; set; }

    public string FeedbackTypeDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get FeedbackType List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetFeedbackTypeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackTypeList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackTypeDL.cs", "GetFeedbackTypeList", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
