using System.Data;
using System.Data.Common;
using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
  public class MenuDL : CommonForAllDL
  {
    #region Properties

    public int UserID { get; set; }

    public int CompanyID { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// GetMenuItems
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="CompanyID"></param>
    /// <returns></returns>
    public DataTable GetMenuItems(int UserID, int CompanyID)
    {
      DataSet ds = new DataSet();
      try
      {
        // Query the database for site map nodes
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMenuItems";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
        return ds.Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MenuDL.cs", "GetMenuItems", ex.Message.ToString(), _myConnection);
        return ds.Tables[0];
      }
    }

    /// <summary>
    /// GetMenuParentItems
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="CompanyID"></param>
    /// <returns></returns>
    public DataSet GetMenuParentItems(int UserID, int CompanyID)
    {
      // Query the database for site map nodes
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMenuParentItems";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
        return ds;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MenuDL.cs", "GetMenuItems", ex.Message.ToString(), _myConnection);
        return ds;
      }
    }

    /// <summary>
    /// GetMenuChildItems
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="CompanyID"></param>
    /// <returns></returns>
    public DataSet GetMenuChildItems(int UserID, int CompanyID)
    {
      // Query the database for site map nodes
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMenuChildItems";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
        return ds;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MenuDL.cs", "GetMenuChildItems", ex.Message.ToString(), _myConnection);
        return ds;
      }
    }

    #endregion
  }
}
