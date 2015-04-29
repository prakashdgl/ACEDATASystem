using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class PolicyCategoryDL : CommonForAllDL
  {
    #region Properties

    public int PolicyCategoryID { get; set; }

    public string PolicyCategoryDescription { get; set; }

    #endregion

    #region Methods

    /// <summary>
    /// Get PolicyCategory List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetPolicyCategoryList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicyCategoryList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyCategoryDL.cs", "GetPolicyCategoryList", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    #endregion
  }
}
