using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace ACE.Order.DataLayer
{
  public class LeaveApplicationDetailDL : CommonForAllDL
  {
    #region Properties

    public int LeaveApplicationDetailID { get; set; }

    public int LeaveApplicationID { get; set; }

    public Nullable<DateTime> LeaveDate { get; set; }

    public LeaveSessionDL LeaveSessionDetails { get; set; }

    public Decimal DayCount { get; set; }

    public LeaveStatusDL LeaveStatusDetails { get; set; }

    public LeaveTypeDL LeaveTypeDetails { get; set; }

    #endregion

    #region Constructors

    public LeaveApplicationDetailDL()
    {
      LeaveStatusDetails = new LeaveStatusDL();
      LeaveSessionDetails = new LeaveSessionDL();
      LeaveTypeDetails = new LeaveTypeDL();
    }

    public LeaveApplicationDetailDL(int leaveApplicationDetailID, bool getAllProperties)
    {
      LeaveApplicationDetailID = leaveApplicationDetailID;
      if (getAllProperties)
      {
        LeaveStatusDetails = new LeaveStatusDL();
        LeaveSessionDetails = new LeaveSessionDL();
        LeaveTypeDetails = new LeaveTypeDL();
        GetLeaveApplicationDetail();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Commit
    /// </summary>
    /// <returns></returns>
    public TransactionResult Commit()
    {
      TransactionResult result = null;
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      using (DbConnection connection = db.CreateConnection())
      {
        connection.Open();
        DbTransaction transaction = connection.BeginTransaction();
        try
        {
          switch (ScreenMode)
          {
            case ScreenMode.Add:
              //Adding or Editing LeaveApplicationDetail
              result = AddEditLeaveApplicationDetail(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:

              result = UpdateLeaveAppproval(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
              result = DeleteLeaveApplicationDetail(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.View:
              break;
          }
          transaction.Commit();
          return result;
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          if (ScreenMode == ScreenMode.Add)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Request Failure");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailList(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailList(employeeID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetLeaveApplicationDetailListByLeaveApplication
    /// </summary>
    /// <param name="leaveApplicationID"></param>
    /// <returns></returns>
    public DataSet GetLeaveApplicationDetailListByLeaveApplication(int leaveApplicationID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, leaveApplicationID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailListByLeaveApplication(leaveApplicationID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID AND FinancialYear
    /// </summary>    
    /// <param name="employeeID">To get a financial year's leave details for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailList(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeIDAndFinancialYear";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailList(employeeID,fromFinancialYear,toFinancialYear)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID, FinancialYear, LeaveStatus, LeaveType
    /// </summary>    
    /// <param name="employeeID">To get a financial year's leave details for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailList(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID, int leaveTypeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndStatusAndType";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);
        db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, leaveTypeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailList(employeeID,fromFinancialYear,toFinancialYear,leaveStatusID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID, FinancialYear, LeaveStatus
    /// </summary>    
    /// <param name="employeeID">To get a financial year's leave details for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndStatus(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndStatus", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID, FinancialYear, LeaveType
    /// </summary>    
    /// <param name="employeeID">To get a financial year's leave details for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndType(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveTypeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndType";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, leaveTypeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndType", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetCheckLeaveDateAvailability
    /// </summary>
    /// <param name="checkedDate"></param>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public DataSet GetCheckLeaveDateAvailability(DateTime checkedDate, int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spCheckLeaveDateAvailability";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveDate", DbType.DateTime, checkedDate);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetCheckLeaveDateAvailability()", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetAvailedLeaveApplicationDetailList
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <param name="leaveStatusID"></param>
    /// <param name="leaveTypeID"></param>
    /// <returns></returns>
    public DataSet GetAvailedLeaveApplicationDetailList(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID, int leaveTypeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetAvailedLeaveApplicationDetailList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);
        db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, leaveTypeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailList(employeeID,fromFinancialYear,toFinancialYear,leaveStatusID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetLeaveHistoryDetails
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <param name="leaveStatusID"></param>
    /// <returns></returns>
    public DataSet GetLeaveHistoryDetails(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveHistoryDetails";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailList(employeeID,fromFinancialYear,toFinancialYear,leaveStatusID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeDetailsForLeaveCancel
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeDetailsForLeaveCancel(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeDetailByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetEmployeeDetailsForLeaveCancel(employeeID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// CancelLeaveApplicationDetail
    /// </summary>
    /// <param name="leaveApplicationDetailID"></param>
    /// <returns></returns>
    public TransactionResult CancelLeaveApplicationDetail(int leaveApplicationDetailID)
    {
      int returnValue = 0;
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spCancelLeaveApplicationDetail";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, leaveApplicationDetailID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Cancelling");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Cancelled");
    }

    /// <summary>
    /// GetLeaveApplicationDetail
    /// </summary>
    private void GetLeaveApplicationDetail()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetail";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, LeaveApplicationDetailID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              LeaveApplicationDetailID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LeaveApplicationDetailID")));
              LeaveApplicationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LeaveApplicationID")));
              LeaveDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("LeaveDate")));
              DayCount = Convert.ToDecimal(dataReader.GetString(dataReader.GetOrdinal("DayCount")));
              LeaveSessionDetails.LeaveSessionID = Common.CheckIntNull(dataReader.GetString(dataReader.GetOrdinal("LeaveSessionID")));
              LeaveSessionDetails.LeaveSessionDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveSessionDescription")));
              LeaveStatusDetails.LeaveStatusID = Common.CheckIntNull(dataReader.GetString(dataReader.GetOrdinal("LeaveStatusID")));
              LeaveStatusDetails.LeaveStatusDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveStatusDescription")));
              LeaveTypeDetails.LeaveTypeID = Common.CheckIntNull(dataReader.GetString(dataReader.GetOrdinal("LeaveTypeID")));
              LeaveTypeDetails.LeaveTypeDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveTypeDescription")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "GetLeaveApplication", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// AddEditLeaveApplicationDetail
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    internal TransactionResult AddEditLeaveApplicationDetail(Database db, DbTransaction transaction)
    {
      try
      {

        int returnValue = 0;
        string sqlCommand = "spAddEditLeaveApplicationDetail";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, LeaveApplicationDetailID);
        db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, LeaveApplicationID);

        if (LeaveDate != null)
        {
          db.AddInParameter(dbCommand, "LeaveDate", DbType.DateTime, LeaveDate);
        }

        db.AddInParameter(dbCommand, "LeaveSessionID", DbType.Int32, LeaveSessionDetails.LeaveSessionID);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, LeaveStatusDetails.LeaveStatusID);

        if (LeaveTypeDetails.LeaveTypeID != 0)
          db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, LeaveTypeDetails.LeaveTypeID);

        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
        {
          if (AddEditOption == 1)
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          else
            return new TransactionResult(TransactionStatus.Failure, "Request Failure");
        }
        else
        {
          if (AddEditOption == 1)
            return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
          else
            return new TransactionResult(TransactionStatus.Success, "Successfully Requested");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "AddEditLeaveApplicationDetail", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }

    }

    /// <summary>
    /// UpdateLeaveAppproval
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    internal TransactionResult UpdateLeaveAppproval(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spUpdateLeaveApproval";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, LeaveApplicationDetailID);
        db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, LeaveApplicationID);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, LeaveStatusDetails.LeaveStatusID);


        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
        {
          return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
        }
        else
        {
          return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "UpdateLeaveAppproval", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }

    }

    /// <summary>
    /// DeleteLeaveApplicationDetail
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteLeaveApplicationDetail(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteLeaveApplicationDetail");
        db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, LeaveApplicationDetailID);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Cancelling");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Cancelled");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "DeleteLeaveApplicationDetail", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// GetLeaveHistoryDetailsForMobile
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <returns></returns>
    public DataSet GetLeaveHistoryDetailsForMobile(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveHistoryDetailsForMobile";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveHistoryDetailsForMobile(employeeID,fromFinancialYear,toFinancialYear)", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplicationDetail List By EmployeeID, FinancialYear, LeaveStatus, LeaveType
    /// </summary>    
    /// <param name="employeeID">To get a financial year's leave details for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveApplicationDetailListForMobileApp(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID, int leaveTypeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationDetailListByEmployeeIDAndFinancialYearAndStatusAndTypeForMobileApp";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);
        db.AddInParameter(dbCommand, "LeaveTypeID", DbType.Int32, leaveTypeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDetailDL.cs", "GetLeaveApplicationDetailListForMobileApp(employeeID,fromFinancialYear,toFinancialYear,leaveStatusID)", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// CancelLeaveApplicationDetailForMobileApp
    /// </summary>
    /// <param name="leaveApplicationDetailID"></param>
    /// <returns></returns>
    public TransactionResult CancelLeaveApplicationDetailForMobileApp(int leaveApplicationDetailID)
    {
      int returnValue = 0;
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spCancelLeaveApplicationDetailForMobile";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "LeaveApplicationDetailID", DbType.Int32, leaveApplicationDetailID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == 1)
        return new TransactionResult(TransactionStatus.Success, "Successfully Cancelled");
      //if (returnValue == -1)
      else
        return new TransactionResult(TransactionStatus.Failure, "Failure Cancelling");
    }

    #endregion
  }
}
