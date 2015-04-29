using System;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ACE.Order.CommonLayer;

namespace ACE.Order.DataLayer
{
  public class EmployeeReportDL : CommonForAllDL
  {
    #region Methods

    /// <summary>
    /// Get Employee BloodGroup Report - (1)
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeBloodGroupReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeBloodGroupReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeBloodGroupReport_1", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee BloodGroup Report - (2)
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <param name="bloodGroupID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeBloodGroupReport(int companyID, int employeeID, int bloodGroupID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeBloodGroupReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "BloodGroupID", DbType.Int32, bloodGroupID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeBloodGroupReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee BirthDay Report - (1) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeBirthDayReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeBirthDayReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeBirthDayReport_1", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee BirthDay Report - (2) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <param name="monthNumber"></param>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeBirthDayReport(int companyID, int employeeID, int monthNumber)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeBirthDayReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "MonthNumber", DbType.Int32, monthNumber);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeBirthDayReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee WeddingDay Report - (1) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeWeddingDayReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeWeddingDayReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeWeddingDayReport_1", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee WeddingDay Report - (2) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <param name="monthNumber"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeWeddingDayReport(int companyID, int employeeID, int monthNumber)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeWeddingDayReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "MonthNumber", DbType.Int32, monthNumber);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeWeddingDayReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Job Status with DOJ Report - (1) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeJobStatusWithDOJReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeStatusWithDOJReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeJobStatusWithDOJReport_1", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Job Status with DOJ Report - (2) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <param name="jobStatusID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeJobStatusWithDOJReport(int companyID, int employeeID, int jobStatusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeStatusWithDOJReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "JobStatusID", DbType.Int32, jobStatusID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeJobStatusWithDOJReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Contact Details Report - (1) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeContactDetailsReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeesContactDetailReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeContactDetailsReport", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Contact Details Report - (2) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeContactDetailsReport(int companyID, int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeesContactDetailReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeContactDetailsReport", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Emergency Contact Details Report - (1) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeEmergencyContactDetailsReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeEmergencyContactDetailsReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeEmergencyContactDetailsReport", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Employee Emergency Contact Details Report - (2) 
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet EmployeeEmergencyContactDetailsReport(int companyID, int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spEmployeeEmergencyContactDetailsReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "EmployeeEmergencyContactDetailsReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Technologies Known By The Employee Report - (1)  
    /// </summary>
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet TechnologiesKnownByTheEmployeeReport(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spTechnologiesKnownByTheEmployeeReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "TechnologiesKnownByTheEmployeeReport_1", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Technologies Known By The Employee Report - (2)  
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="employeeID"></param>
    /// <param name="technologyID"></param>
    /// <param name="skillLevelID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet TechnologiesKnownByTheEmployeeReport(int companyID, int employeeID, int technologyID, int skillLevelID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spTechnologiesKnownByTheEmployeeReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "TechnologyID", DbType.Int32, technologyID);
        db.AddInParameter(dbCommand, "SkillLevelID", DbType.Int32, skillLevelID);

        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "TechnologiesKnownByTheEmployeeReport_2", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Report For EmployeePF By EmployeeID 
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="currentMonth"></param>
    /// <returns></returns>
    public DataSet GetReportForEmployeePFByEmployeeID(int employeeID, DateTime currentMonth)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetReportForEmployeePFByEmployeeID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "MonthYear", DbType.DateTime, currentMonth);

        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "GetReportForEmployeePFByEmployeeID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the list of PF report based on month-year selection
    /// </summary>
    /// <param name="monthYear">month-year</param>
    /// <returns>DataSet Containing the List of PF based on month-year</returns>
    public DataSet GetPFReportByMonthYearID(DateTime monthYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spPFReport");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "MonthYear", DbType.Date, monthYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ShowPFReportDL.cs", "spPFReport", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get EmployeeList For The FinancialYear
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <returns></returns>
    public DataSet GetEmployeeListForTheFinancialYear(int companyID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListForEPF";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "GetEmployeeListForTheFinancialYear", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Remittance Report For EPF
    /// </summary>
    /// <param name="employeeID"></param>
    /// <param name="fromFinancialYear"></param>
    /// <param name="toFinancialYear"></param>
    /// <returns></returns>
    public DataSet GetRemittanceReportForEPF(int employeeID, DateTime fromFinancialYear, DateTime toFinancialYear)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetRemittanceReportForEPF";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        db.AddInParameter(dbCommand, "FromFinancialYear", DbType.DateTime, fromFinancialYear);
        db.AddInParameter(dbCommand, "ToFinancialYear", DbType.DateTime, toFinancialYear);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeReportDL.cs", "GetRemittanceReportForEPF", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
