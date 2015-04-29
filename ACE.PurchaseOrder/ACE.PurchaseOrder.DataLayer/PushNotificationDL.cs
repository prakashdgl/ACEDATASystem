using System;
using System.Data;
using System.Data.Common;
using ECGroup.ECafe.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ECGroup.ECafe.DataLayer
{
  public class PushNotificationDL : CommonForAllDL
  {
    #region Public Properties

    public string DeviceToken { get; set; }
    public string DeviceName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    #endregion

    #region Private Methods
    /// <summary>
    /// To Add / Edit a PushNotification
    /// </summary>

    public void AddEditPushNotification(string DeviceToken, string DeviceName, string UserName)
    {
      int returnValue = 0;
      string sqlCommand = "spIOSDeviceDetails";
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "DeviceToken", DbType.String, DeviceToken);
      db.AddInParameter(dbCommand, "DeviceName", DbType.String, DeviceName);
      db.AddInParameter(dbCommand, "Username", DbType.String, Username);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

    }
    public DataSet IOSGetDeviceTokenDetails(int userid)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spIOSDeviceTokenDetails";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserName", DbType.Int32, userid);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PushNotificationDL.cs", "IOSGetDeviceTokenDetails", ex.Message, _myConnection);
      }
      return ds;
    }

    public DataSet IOSGetDeviceTokenDetailsforPushNotificationPanel(int userid)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spIOSDeviceTokenDetailsforPushNotificationPanel";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserName", DbType.Int32, userid);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PushNotificationDL.cs", "IOSGetDeviceTokenDetails", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee List For Push Notification
    /// </summary>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListForPushNotification()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListForPushNotification";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PushNotificationDL.cs", "GetEmployeeListForPushNotification", ex.Message, _myConnection);
      }
      return ds;
    }
  }
}
    #endregion