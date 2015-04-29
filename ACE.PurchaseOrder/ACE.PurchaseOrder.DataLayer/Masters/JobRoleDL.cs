using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;

namespace ACE.PurchaseOrder.DataLayer
{
  public class JobRoleDL : CommonForAllDL
  {
    #region Properties

    public int JobRoleID { get; set; }

    public string JobRoleDescription { get; set; }

    #endregion

    #region Private Methods

    /// <summary>
    /// Get JobRole List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetJobRoleList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetJobRoleList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "JobRoleDL.cs", "GetJobRoleList", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
