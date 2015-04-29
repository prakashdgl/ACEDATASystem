using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class ProjectStatusDL : CommonForAllDL
  {
    #region Properties

    public int ProjectID { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get Project Status Getting in Dropdown List Control
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetProjectStatusList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetProjectStatusList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ProjectStatusDL.cs", "GetProjectStatusList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    #endregion
  }
}
