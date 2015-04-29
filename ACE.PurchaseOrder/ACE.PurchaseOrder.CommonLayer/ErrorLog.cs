using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;

namespace ACE.PurchaseOrder.CommonLayer
{
  public static class ErrorLog
  {
    public static ACEConnection _myConnection = new ACEConnection();
    /// <summary>
    /// LogErrorMessageToDB
    /// </summary>
    /// <param name="pageName"></param>
    /// <param name="className"></param>
    /// <param name="eventName"></param>
    /// <param name="errorMessage"></param>
    /// <param name="_aceConnection"></param>
    public static void LogErrorMessageToDB(string pageName, string className, string eventName, string errorMessage, ACEConnection _aceConnection)
    {
      try
      {
        //if (errorMessage != "Thread was being aborted.")
        //{
        Database db = DatabaseFactory.CreateDatabase(_aceConnection.DatabaseName);
        string sqlCommand = "spAddLogErrorMessageToDB";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "PageName", DbType.String, pageName);
        db.AddInParameter(dbCommand, "ClassName", DbType.String, className);
        db.AddInParameter(dbCommand, "EventName", DbType.String, eventName);
        db.AddInParameter(dbCommand, "ErrorMessage", DbType.String, errorMessage);
        db.ExecuteNonQuery(dbCommand);
        // }

      }
      catch { }
    }

    public static DataSet GetErrorLogList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetErrorLog";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ErrorLog.cs", "GetErrorLogList", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }
  }
}