using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class AttendanceDL : CommonForAllDL
  {
    #region Public Properties

    public int EmployeeID { get; set; }
    public int EmployeeCode { get; set; }
    public int CompanyID { get; set; }
    public string DepartmentName { get; set; }
    public int DepartmentID { get; set; }
    public int ReportType { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string AllorShortHours { get; set; }
    public string NewInTime { get; set; }
    public string NewOutTime { get; set; }
    public string OldInTime { get; set; }
    public string OldOutTime { get; set; }
    public DateTime EventDate { get; set; }
    public string OldWorkStatus { get; set; }
    public string NewWorkStatus { get; set; }
    public int AddEditOption { get; set; }
    public int UpdateBy { get; set; }
    public DateTime UpdateOn { get; set; }
    public string SearchChar { get; set; }

    #endregion

    #region Constructor(s)
    //Empty Constructor
    public AttendanceDL() { }

    //Parameter Constructor
    public AttendanceDL(int EmployeeID, bool getAllProperties)
    {
      if (getAllProperties)
      {
      }
    }

    #endregion

    #region  Method(s)

    /// <summary>
    /// Decides whether to Call Add/Edit/Delete method
    /// And Calls the appropriate method
    /// </summary>
    /// <returns>TransactionResult - Success / Failure</returns>
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
              //Adding Employee 
              result = AddManualAttendanceHistory(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              //Adding Or Editing Employee Attendance
              result = EditAttendance(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;

          }
          transaction.Commit();
          return result;
        }
        catch (Exception ex)
        {
          transaction.Rollback();

          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }

        }
      }
      return null;
    }

    /// <summary>
    /// Get EmployeeList From Attendance By CompanyID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="fromDate"></param>
    /// <param name="toDate"></param>
    /// <returns></returns>
    public DataSet GetEmployeeListFromAttendanceByCompanyID(int companyID, DateTime fromDate, DateTime toDate)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListFromAttendanceByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, fromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, toDate);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetEmployeeListFromAttendanceByCompanyID", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Daily Attendance List by Employee
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetDailyAttendanceListByEmployee()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetDailyAttendanceByEmployee";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "ReportType", DbType.Int16, ReportType);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "AllorShortHours", DbType.String, AllorShortHours);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetDailyAttendanceListByEmployee", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Daily Attendance List by Administration
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataTable GetDailyAttendanceByAdministration()
    {
      DataTable dt = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetDailyAttendanceByAdministration";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, EmployeeCode);
        db.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, DepartmentID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "AllorShortHours", DbType.String, AllorShortHours);
        dt = db.ExecuteDataSet(dbCommand).Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetDailyAttendanceByAdministration", ex.Message.ToString(), _myConnection);
      }
      return dt;
    }

    /// <summary>
    /// Get Daily Attendance List by Account
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataTable GetDailyAttendanceByAccount()
    {
      DataTable dt = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetDailyAttendanceByAccount";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        dt = db.ExecuteDataSet(dbCommand).Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetDailyAttendanceByAccount", ex.Message.ToString(), _myConnection);
      }
      return dt;
    }

    /// <summary>
    /// Get All Employee Late Hours
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetAllEmployeeLateHours()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetAllEmployeeLateHours";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "DepartmentName", DbType.String, DepartmentName);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetAllEmployeeLateHours", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Monthly Attendance
    /// </summary>
    /// <returns></returns>
    public DataSet GetMonthlyAttendance()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spMonthlyAttendance";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetMonthlyAttendance", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Monthly Attendance
    /// </summary>
    /// <returns></returns>
    public DataSet GetMonthlyAttendanceBySearchChar()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spMonthlyAttendanceBySearchChar";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "SearchChar", DbType.String, SearchChar);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetMonthlyAttendanceBySearchChar", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Monthly Attendance Detail
    /// </summary>
    /// <returns></returns>
    public DataSet GetMonthlyAttendanceDetail()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spMonthlyAttendanceDetails";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetMonthlyAttendance", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// EditAttendence - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public TransactionResult EditAttendance(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "ManualAttendance_MERGE";//sp_ManualAttendance
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "InTime", DbType.String, NewInTime);
        db.AddInParameter(dbCommand, "OutTime", DbType.String, NewOutTime);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeCode);
        db.AddInParameter(dbCommand, "EventDate", DbType.DateTime, EventDate);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int32, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Editing Attendence");
        else
          return new TransactionResult(TransactionStatus.Success, "Attendence Successfully updated");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendenceDL.cs", "EditAttendance", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// sp_AddManualAttendanceHistory - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public TransactionResult AddManualAttendanceHistory(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "AddManualAttendanceHistory_AddNew_INSERT";//sp_AddManualAttendanceHistory
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "OldInTime", DbType.String, OldInTime);
        db.AddInParameter(dbCommand, "OldOutTime", DbType.String, OldOutTime);
        db.AddInParameter(dbCommand, "NewInTime", DbType.String, NewInTime);
        db.AddInParameter(dbCommand, "NewOutTime", DbType.String, NewOutTime);
        db.AddInParameter(dbCommand, "OldWorkStatus", DbType.String, OldWorkStatus);
        db.AddInParameter(dbCommand, "EventDate", DbType.DateTime, EventDate);
        db.AddInParameter(dbCommand, "UpdatedBy", DbType.Int32, UpdateBy);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int32, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding Attendence");
        else
          return new TransactionResult(TransactionStatus.Success, "Attendence Successfully added");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendenceDL.cs", "AddManualAttendanceHistory", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Get ManualAttendance History - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <returns></returns>
    public DataSet GetManualAttendanceHistory()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "ManualAttendanceHistory_SELECT";//sp_GetManualAttendanceHistory
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetManualAttendanceHistory", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetAttendanceData
    /// </summary>
    /// <returns></returns>
    public DataSet GetAttendanceData()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetAttendence";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, EmployeeCode);
        db.AddInParameter(dbCommand, "EventDate", DbType.DateTime, EventDate);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetAttendanceData", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Weekly Leave Repot
    /// </summary>
    /// <returns></returns>

    public DataSet GetWeeklyLeaveReport()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeWeeklyLeaveReport";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetWeeklyLeaveReport", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Availed Leave Report
    /// </summary>
    /// <returns></returns>

    public DataSet GetAvaliedLeaveReport()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeAvailedLeaveReport";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetAvaliedLeaveReport", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetDailyAttendanceListByEmployeeCode
    /// </summary>
    /// <returns></returns>
    public DataSet GetDailyAttendanceListByEmployeeCode()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetDailyAttendanceByEmployeeCode";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, EmployeeCode);
        db.AddInParameter(dbCommand, "ReportType", DbType.Int16, ReportType);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "AllorShortHours", DbType.String, AllorShortHours);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetDailyAttendanceListByEmployee", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Daily AttendanceList By EmployeeCode For Mobile App
    /// </summary>
    /// <returns></returns>
    public DataSet GetDailyAttendanceListByEmployeeCodeForMobileApp()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetDailyAttendanceByEmployeeCodeForMobileApp";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, EmployeeCode);
        db.AddInParameter(dbCommand, "ReportType", DbType.Int16, ReportType);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        db.AddInParameter(dbCommand, "AllorShortHours", DbType.String, AllorShortHours);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetDailyAttendanceListByEmployeeCodeForMobileApp", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    ///GetWorked Hours Average By Date
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetWorkedHoursAverageByDate()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "WorkedHoursAverageByDate_SELECT";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetWorkedHoursAverageByDate", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    ///GetWorked Hours By Date
    /// </summary>
    /// <param name="ACE">Instance of the ACE</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetWorkedHoursByDate()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "WorkedHoursByDate_SELECT";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AttendanceDL.cs", "GetWorkedHoursByDate", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }
    #endregion

  }
}
