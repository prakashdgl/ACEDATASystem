using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class LeaveApplicationDL : CommonForAllDL
  {
    #region Properties

    public string EmployeeFName { get; set; }

    public string OfficeEmailID { get; set; }

    public string leaveStatus { get; set; }

    public int LeaveApplicationID { get; set; }

    public int EmployeeID { get; set; }

    public Nullable<DateTime> FromDate { get; set; }

    public Nullable<DateTime> ToDate { get; set; }

    public Nullable<DateTime> DateApplied { get; set; }

    public string ReasonForLeave { get; set; }

    public int LeaveReasonID { get; set; }

    public string ContactNumber { get; set; }

    public int Leavecount { get; set; }

    public List<LeaveApplicationDetailDL> LeaveDays { get; set; }

    #endregion

    #region Constructors

    public LeaveApplicationDL()
    {
      LeaveDays = new List<LeaveApplicationDetailDL>();
    }

    public LeaveApplicationDL(int leaveApplicationID, bool getAllProperties)
    {
      LeaveApplicationID = leaveApplicationID;
      if (getAllProperties)
      {
        LeaveDays = new List<LeaveApplicationDetailDL>();
        GetLeaveApplication();
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
              //Adding or Editing LeaveApplication
              result = AddEditLeaveApplication(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              result = UpdateLeaveApplication(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
              result = DeleteLeaveApplication(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Request Failure");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// CommitMobile
    /// </summary>
    /// <returns></returns>
    public TransactionResult CommitMobile()
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
              //Adding or Editing LeaveApplication
              result = AddEditLeaveApplication(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              result = UpdateLeaveApplication(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
              result = DeleteLeaveApplication(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Add", ex.Message, _myConnection);

            return new TransactionResult(TransactionStatus.Failure, "Request Failure");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Edit", ex.Message, _myConnection);

            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "Commit For Delete", ex.Message, _myConnection);

            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Leave Application For All Employees
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public DataSet GetLeaveApplicationForAllEmployees(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationForAllEmployees";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "GetLeaveApplicationForAllEmployees", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplication Applied AND Approved List
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public DataSet GetLeaveApplicationAppliedANDApprovedList(int companyID, int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplicationAppliedANDApprovedList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "GetLeaveApplicationAppliedANDApprovedList", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get LeaveApplication
    /// </summary>
    public void GetLeaveApplication()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLeaveApplication";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, LeaveApplicationID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              LeaveApplicationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LeaveApplicationID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              FromDate = Convert.ToDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("FromDate")));
              ToDate = Convert.ToDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("ToDate")));
              DateApplied = Convert.ToDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("DateApplied")));
              ReasonForLeave = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ReasonForLeave")));
              ContactNumber = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ContactNumber")));
              EmployeeFName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Fname")));
              leaveStatus = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LeaveStatusDescription")));
              OfficeEmailID = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("OfficeEmailID")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveApplicationDL.cs", "GetLeaveApplication", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit LeaveApplication 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditLeaveApplication(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditLeaveApplication";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, LeaveApplicationID);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);

      if (FromDate != null)
      {
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
      }
      if (ToDate != null)
      {
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
      }

      if (DateApplied != null)
      {
        db.AddInParameter(dbCommand, "DateApplied", DbType.DateTime, DateApplied);
      }
      db.AddInParameter(dbCommand, "ReasonForLeave", DbType.String, ReasonForLeave);
      db.AddInParameter(dbCommand, "LeaveReasonID", DbType.Int32, LeaveReasonID);
      db.AddInParameter(dbCommand, "ContactNumber", DbType.String, ContactNumber);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      LeaveApplicationID = returnValue;
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
        {
          if (LeaveDays != null)
          {
            foreach (LeaveApplicationDetailDL aLeave in LeaveDays)
            {
              aLeave.LeaveApplicationID = LeaveApplicationID;
              aLeave.AddEditLeaveApplicationDetail(db, transaction);
            }
          }

          return new TransactionResult(TransactionStatus.Success, "Successfully Requested");
        }
      }
    }

    /// <summary>
    /// Update LeaveApplication 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult UpdateLeaveApplication(Database db, DbTransaction transaction)
    {
      if (LeaveDays != null)
      {
        foreach (LeaveApplicationDetailDL aLeave in LeaveDays)
        {
          aLeave.UpdateLeaveAppproval(db, transaction);
        }
      }
      return new TransactionResult(TransactionStatus.Success, "Successfully Requested");
    }

    /// <summary>
    /// Delete LeaveApplication
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteLeaveApplication(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteLeaveApplication");
      db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, LeaveApplicationID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Cancelling");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Cancelled");
    }

    /// <summary>
    /// GetEmployeeExistsLeaveCount For Mobile App
    /// </summary>
    /// <param name="leaveApplicationID"></param>
    /// <returns></returns>
    public int GetEmployeeExistsLeaveCountForMobileApp(int EmployeeID,DateTime FromDate,DateTime ToDate)
    {
      DataSet ds = new DataSet();
      int leaveCount = 0;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spLeaveCheckForMobileApp";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        leaveCount = Convert.ToInt32(db.ExecuteScalar(dbCommand));
        return leaveCount;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeExistsLeaveCountForMobileApp", ex.Message, _myConnection);
      }
      return leaveCount;
    }

    #endregion
  }
}
