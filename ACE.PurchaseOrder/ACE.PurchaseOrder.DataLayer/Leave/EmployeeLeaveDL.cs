using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EmployeeLeaveDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeLeaveID { get; set; }

    public int EmployeeID { get; set; }

    public DateTime FromFinancialYear { get; set; }

    public DateTime ToFinancialYear { get; set; }

    public decimal OriginalPL { get; set; }

    public decimal ActualPL { get; set; }

    public decimal OriginalCL { get; set; }

    public decimal ActualCL { get; set; }

    public decimal OriginalSL { get; set; }

    public decimal ActualSL { get; set; }

    public decimal OpeningBalance { get; set; }

    public decimal EligibleLeave { get; set; }

    public string AuditUserID { get; set; }

    public string CreditDescription { get; set; }
    
    public string Notes { get; set; }

    public List<EmployeeLeaveDL> EmployeeLeave { get; set; }

    #endregion

    #region Constructors

    public EmployeeLeaveDL()
    {
    }

    public EmployeeLeaveDL(int employeeLeaveID, bool getAllProperties)
    {
      EmployeeLeaveID = employeeLeaveID;
      if (getAllProperties)
      {
        GetEmployeeLeave();
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
              result = AddEditEmployeeLeave(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              result = UpdateLeaveStatusJob(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:

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
            ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get EmployeeLeave List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLeave List By EmployeeID
    /// </summary>   
    /// <param name="employeeID">To get all the years' leave for the specified employeeID</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveList(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveListByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveList(int employeeID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLeave By EmployeeID AND FinancialYear
    /// </summary>   
    /// <param name="employeeID">To get a financial year's leave for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveByEmployeeIDAndFinancialYear(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveByEmployeeIDAndFinancialYear";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "spGetEmployeeLeaveByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLoss of Pay By EmployeeID AND FinancialYear
    /// </summary>   
    /// <param name="employeeID">To get a financial year's leave for the specified employeeID</param>
    /// <param name="fromFinancialYear">From Financial Year Date</param>
    /// <param name="toFinancialYear">To Financial Year Date</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLossOfPayByEmployeeIDAndFinancialYear(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLossOfPayByEmployeeIDAndFinancialYear";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLossOfPayByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLeaveCount By EmployeeID AND FinancialYear AND LeaveStatus
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <param name="leaveStatusID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeLeaveCountByEmployeeIDANDFinancialYearANDLeaveStatus(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveCountByEmployeeIDANDFinancialYearANDLeaveStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "LeaveStatusID", DbType.Int32, leaveStatusID);

        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLossOfPayByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLeaveCount By EmployeeID AND FinancialYear AND LeaveStatus
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <param name="leaveStatusID"></param>
    /// <param name="leaveTypeID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeLeaveCountByEmployeeIDANDFinancialYearANDLeaveStatus(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear, int leaveStatusID, int leaveTypeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveCountByEmployeeIDANDFinancialYearANDLeaveStatusANDLeaveType";
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLossOfPayByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeLeaveCount By LeaveApplicationID
    /// </summary>
    /// <param name="leaveApplicationID"></param>
    /// <returns></returns>
    public decimal GetEmployeeLeaveCountByLeaveApplicationID(int leaveApplicationID)
    {
      DataSet ds = new DataSet();
      decimal leaveCout = 0;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveCountByLeaveApplicationID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "LeaveApplicationID", DbType.Int32, leaveApplicationID);
        leaveCout = Convert.ToDecimal(db.ExecuteScalar(dbCommand));
        return leaveCout;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLossOfPayByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return leaveCout;
    }

    /// <summary>
    /// Get Get Employee Recently Applied Leave List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACE Connection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeRecentlyAppliedLeaveList(int CompanyID)
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spGetEmployeeRecentlyAppliedLeaveList";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "CompanyID", DbType.String, CompanyID);
      return db.ExecuteDataSet(dbCommand);
    }

    /// <summary>
    /// Get EmployeeLeave By CompanyID AND FinancialYear
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public string GetEmployeeLeaveByCompanyIDANDFinancialYear(int employeeID)
    {
      DataSet ds = new DataSet();
      string eligibleLeave = "0";
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveByCompanyIDANDFinancialYear";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          // JobStatus ID, Description
          eligibleLeave = dRow["EligibleLeave"].ToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveByCompanyIDANDFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return eligibleLeave;
    }

    /// <summary>
    /// Get EmployeeLeave Report By EmployeeCode AND EventDate
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeCode"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <param name="statusID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeLeaveReportByEmployeeCodeANDEventDate(int companyID, int employeeCode, DateTime fromFinancialYear, DateTime toFinancialYear, int statusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveReportByEmployeeCodeANDEventDate";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, employeeCode);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, toFinancialYear);
        db.AddInParameter(dbCommand, "StatusID", DbType.Int32, statusID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveReportByEmployeeIDANDEventDate", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// To retrieve Employee Balance Leave List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACE Connection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveBalanceList(int employeeID, DateTime StartDate, DateTime EndDate)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveBalanceList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.String, employeeID);
        db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, StartDate);
        db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, EndDate);
        return db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveBalanceList(employeeID, StartDate, EndDate)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// To retrieve Employee Balance Leave total
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACE Connection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveBalanceTotal(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveBalanceTotal";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.String, employeeID);
        return db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveBalanceTotal(employeeID)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Update Leave Status Job
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    internal TransactionResult UpdateLeaveStatusJob(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spUpdateLeaveStatusJob";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.ExecuteNonQuery(dbCommand, transaction);
      return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
    }

    /// <summary>
    /// Add Edit EmployeeLeave
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeLeave(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditEmployeeLeave";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "EligibleLeave", DbType.Decimal, EligibleLeave);
      db.AddInParameter(dbCommand, "AuditUser", DbType.String, AuditUserID);
      db.AddInParameter(dbCommand, "CreditDescription", DbType.String, CreditDescription);
      db.AddInParameter(dbCommand, "Notes", DbType.String, Notes);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                     DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
      {
        return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
      }
      if (returnValue == 1)
      {
        return new TransactionResult(TransactionStatus.Failure, "Duplicate entry");
      }
      else
      {
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
      }
    }

    /// <summary>
    /// Get Employee Leave
    /// </summary>
    private void GetEmployeeLeave()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeave";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeLeaveID", DbType.Int32, EmployeeLeaveID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeLeaveID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeLeaveID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              FromFinancialYear = Convert.ToDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("FromFinancialYear")));
              ToFinancialYear = Convert.ToDateTime(dataReader.GetDateTime(dataReader.GetOrdinal("ToFinancialYear")));
              OriginalPL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("OriginalPL")));
              OriginalCL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("OriginalCL")));
              OriginalSL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("OriginalSL")));
              ActualPL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("ActualPL")));
              ActualCL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("ActualCL")));
              ActualSL = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("ActualSL")));
              OpeningBalance = Convert.ToDecimal(dataReader.GetInt32(dataReader.GetOrdinal("OpeningBalance")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeave", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// To retrieve Employee Balance Leave List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACE Connection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetEmployeeLeaveBalanceListForMobileApp(int employeeID, DateTime StartDate, DateTime EndDate)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveBalanceListForMobileApp";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.String, employeeID);
        db.AddInParameter(dbCommand, "StartDate", DbType.DateTime, StartDate);
        db.AddInParameter(dbCommand, "EndDate", DbType.DateTime, EndDate);
        return db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "GetEmployeeLeaveBalanceListForMobileApp(employeeID, StartDate, EndDate)", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeLeaveByEmployeeIDAndFinancialYearForMobile
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <returns></returns>
    public DataSet GetEmployeeLeaveByEmployeeIDAndFinancialYearForMobile(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLeaveByEmployeeIDANDFinancialYearForMobile";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLeaveDL.cs", "spGetEmployeeLeaveByEmployeeIDAndFinancialYear", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    #endregion
  }
}
