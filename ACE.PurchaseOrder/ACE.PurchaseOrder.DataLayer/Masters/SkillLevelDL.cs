using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class SkillLevelDL : CommonForAllDL
  {
    #region Properties

    public int SkillLevelID { get; set; }

    public string SkillLevelDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get SkillLevel List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetSkillLevelList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSkillLevelList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SkillLevelDL.cs", "GetSkillLevelList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
