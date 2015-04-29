using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class FeedbackPriorityDL : CommonForAllDL
  {
    #region Properties

    public int FeedbackPriorityID { get; set; }

    public string FeedbackPriorityDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get FeedbackPriority List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetFeedbackPriorityList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackPriorityList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackPriorityDL.cs", "GetFeedbackPriorityList", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
