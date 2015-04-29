using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    public int ProjectID { get; set; }

    public int ClientID { get; set; }

    public string ProjectName { get; set; }

    public string ClientName { get; set; }

    public string EmployeeCode { get; set; }

    public string FName { get; set; }

    public string Initial { get; set; }

    public string LName { get; set; }

    public int SNo { get; set; }

    public string EmployeeName
    {
      get
      {
        StringBuilder eName = new StringBuilder();

        eName.Append(FName + " " + LName + " " + Initial);
        return eName.ToString();
      }
    }

    public string EmployeeCodeAndName
    {
      get
      {
        StringBuilder eName = new StringBuilder();

        eName.Append(EmployeeCode + "-");

        if (Initial != null)
          if (Initial.Trim().Length != 0)
            eName.Append(Initial.Trim() + " ");
          else
            eName.Append("");
        if (FName != null)
          if (FName.Trim().Length != 0)
            eName.Append(FName);
          else
            eName.Append("");
        if (LName != null)
          if (LName.Trim().Length != 0)
            eName.Append(" " + LName);
          else
            eName.Append("");

        return eName.ToString();
      }
    }

    public int ReportingToEmployeeID { get; set; }
    public string ReportingToEmployeeName { get; set; }

    public int GenderID { get; set; }

    public string GenderDescription { get; set; }

    public int TitleID { get; set; }

    public string TitleDescription { get; set; }

    public int MaritalStatusID { get; set; }

    public int RoleID { get; set; }

    public string RoleDescription { get; set; }

    public string MaritalStatusDescription { get; set; }

    public int NationalityID { get; set; }

    public string NationalityDescription { get; set; }

    public int ReligionID { get; set; }

    public string ReligionDescription { get; set; }

    public Nullable<DateTime> Dob { get; set; }

    public Nullable<DateTime> Doj { get; set; }

    public Nullable<DateTime> Dol { get; set; }

    public Nullable<DateTime> FromDate { get; set; }

    public Nullable<DateTime> ToDate { get; set; }

    public int BloodGroupID { get; set; }

    public string BloodGroupDescription { get; set; }

    public Nullable<DateTime> WeddingDate { get; set; }

    public string Photo { get; set; }

    public string PassportNo { get; set; }

    public Nullable<DateTime> PassportDateIssue { get; set; }

    public Nullable<DateTime> PassportDateExpiry { get; set; }

    public string PassportIssuePlace { get; set; }

    public string Pan { get; set; }

    public string BankAccountNumber { get; set; }

    public string BankName { get; set; }

    public string HomeTown { get; set; }

    public string Mobile { get; set; }

    public string OfficeEmailID { get; set; }

    public string PersonalEmailID { get; set; }

    public string MessengerID { get; set; }

    public string IceName { get; set; }

    public int IceRelationshipID { get; set; }

    public string IceRelationshipDescription { get; set; }

    public string IceMobile { get; set; }

    public string IceName2 { get; set; }

    public int IceRelationshipID2 { get; set; }

    public string IceRelationshipDescription2 { get; set; }

    public string IceMobile2 { get; set; }

    public int CompanyID { get; set; }

    public string CompanyName { get; set; }

    public int JobStatusID { get; set; }

    public string JobStatusDescription { get; set; }

    public int DepartmentID { get; set; }

    public string DepartmentDescription { get; set; }

    public bool IsPresentAndPermanentAddressSame { get; set; }

    public int DesignationID { get; set; }

    public string DesignationDescription { get; set; }

    public string LeaveApprovar { get; set; }

    public int designationStatus { get; set; }

    public int EmpClientProjStatus { get; set; }

    public int CreatorUserID { get; set; }

    public Nullable<DateTime> CreatedDate { get; set; }

    public int ModifierUserID { get; set; }

    public Nullable<DateTime> ModifiedDate { get; set; }

    public Nullable<DateTime> JobStartTime { get; set; }

    public List<EmployeeAddressDL> EmployeeAddresses { get; set; }

    public List<EmployeeFamilyDL> EmployeeFamilyMembers { get; set; }

    public List<EmployeeLanguageKnownDL> EmployeeLanguages { get; set; }

    public List<EmployeeEducationDL> EmployeeEducationDetails { get; set; }

    public List<EmployeeCertificationDL> EmployeeCertifications { get; set; }

    public List<EmployeeSkillDL> EmployeeSkills { get; set; }

    public List<EmployeeExperienceDL> EmployeeExperienceDetails { get; set; }

    public List<EmployeePreviousEmployersProjectDL> EmployeePreviousProjects { get; set; }

    public List<EmployeePresentEmployerProjectDL> EmployeePresentProjects { get; set; }

    public List<EmployeeJobStatusDL> EmployeeJobStatuses { get; set; }

    #endregion

    #region Constructors

    public EmployeeDL()
    {
      EmployeeAddresses = new List<EmployeeAddressDL>();
      EmployeeFamilyMembers = new List<EmployeeFamilyDL>();
      EmployeeLanguages = new List<EmployeeLanguageKnownDL>();
      EmployeeEducationDetails = new List<EmployeeEducationDL>();
      EmployeeCertifications = new List<EmployeeCertificationDL>();
      EmployeeSkills = new List<EmployeeSkillDL>();
      EmployeeExperienceDetails = new List<EmployeeExperienceDL>();
      EmployeePreviousProjects = new List<EmployeePreviousEmployersProjectDL>();
      EmployeePresentProjects = new List<EmployeePresentEmployerProjectDL>();
      EmployeeJobStatuses = new List<EmployeeJobStatusDL>();
    }

    public EmployeeDL(int employeeID, bool getAllProperties)
    {
      EmployeeID = employeeID;
      EmployeeAddresses = new List<EmployeeAddressDL>();
      EmployeeFamilyMembers = new List<EmployeeFamilyDL>();
      EmployeeLanguages = new List<EmployeeLanguageKnownDL>();
      EmployeeEducationDetails = new List<EmployeeEducationDL>();
      EmployeeCertifications = new List<EmployeeCertificationDL>();
      EmployeeSkills = new List<EmployeeSkillDL>();
      EmployeeExperienceDetails = new List<EmployeeExperienceDL>();
      EmployeePreviousProjects = new List<EmployeePreviousEmployersProjectDL>();
      EmployeePresentProjects = new List<EmployeePresentEmployerProjectDL>();

      if (getAllProperties)
      {
        GetEmployee();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    ///Commit
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
              //Adding Or Editing Employee
              result = AddEditEmployee(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            //case ScreenMode.Delete:
            //    if (result.Status == TransactionStatus.Failure)
            //    {
            //        transaction.Rollback();
            //        return result;
            //    }
            //    break;
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    #region Get Employee List

    /// <summary>
    /// Get Employee List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeList", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Employee List By CompanyID

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListByCompanyID(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeListByEmployeeCode
    /// </summary>
    /// <param name="employeeCode"></param>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeListByEmployeeCode(string employeeCode, int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByEmployeeCode";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, employeeCode);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByEmployeeCode", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeListByCompanyIDAttendance
    /// </summary>
    /// <param name="userID"></param>
    /// <param name="companyID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeListByCompanyIDAttendance(int userID, int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByCompanyIDAttendance";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, userID);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);

        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Lower Level Employee List By CompanyID

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetLowerLevelEmployeeListByCompanyIDandEmployeeID(int companyID, int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetLowerLevelEmployeeListByCompanyIDandEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Employee List By CompanyID - Not in VisitorCardAllocation Table

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListByCompanyIDNotInVisitorCardAllocation(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByCompanyIDNotInVisitorCardAllocation";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyIDNotInVisitorCardAllocation", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Employee List By CompanyID And DepartmentID

    /// <summary>
    /// Get Employee List By CompanyID 
    /// </summary>
    /// <param name="companyID">Company</param>
    /// <param name="departmentID">Department</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListByCompanyIDAndDepartmentID(int companyID, int departmentID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByCompanyIDAndDepartmentID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, departmentID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyIDAndDepartmentID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Employee List By Company wise DepartmentID

    /// <summary>
    /// Get Employee List By Company wise DepartmentID -- [For Attendance Module] 
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListByCompanyWiseDepartmentID(int companyID, int departmentID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListByCompanywiseDepartmentID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, departmentID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListByCompanyWiseDepartmentID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    #region Get Employee List By Company wise DepartmentID

    /// <summary>
    /// Get Employee List By Company wise DepartmentID -- [For Attendance Module] 
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetEmployeeListFromAttedanceByCompanywiseDepartmentID(int companyID, int departmentID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeListFromAttedanceByCompanywiseDepartmentID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, departmentID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeListFromAttedanceByCompanywiseDepartmentID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion

    /// <summary>
    /// Gets the list of employees that match the specified search text (in any of the selected fields) By CompanyID
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="searchText"></param>
    /// <returns>DataSet Containing the List of Employees By CompanyID</returns>
    public DataSet GetEmployeeByCompanyID(int companyID, string searchText, bool activatedDeactivated)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeByCompanyID");
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "SearchText", DbType.String, searchText);
        db.AddInParameter(dbCommand, "ActivatedDeactivated", DbType.Boolean, activatedDeactivated);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeByCompanyID", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeProfileCount
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public int GetEmployeeProfileCount(int employeeID)
    {
      int profileCount = 0;
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeProfileCount";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[3].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[4].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[5].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[6].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[7].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[8].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
        if (ds.Tables[9].Rows.Count > 0)
        {
          profileCount = profileCount + 10;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeByCompanyID", ex.Message.ToString(), _myConnection);
      }
      return profileCount;
    }

    /// <summary>
    /// Generate a UserName and Password
    /// </summary>
    /// <param name="aUser">User Object</param>        
    public void GenerateUserNameAndPassword(UsersDL aUser)
    {
      try
      {
        // Generate the user name - company id + employee code
        aUser.UserName = Convert.ToInt32(CompanyID.ToString().Trim() + EmployeeCode.Trim());

        // Generate the user password - 
        Utilities objPwd = new Utilities();

        DateTime dob = (DateTime)this.Dob;
        aUser.Password = objPwd.EncryptText(Convert.ToString(dob.ToString("dd") + EmployeeCode.ToString() + dob.ToString("MM")));
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GenerateUserNameAndPassword", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// Get the Employee Details - Administrative based on the EmployeeID
    /// </summary>
    /// <param name="employeeID">employeeID</param> 
    public void GetEmployeeAdditional(int employeeID)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeAdditional";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          // ID, Code
          EmployeeID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeID"]));
          EmployeeCode = Common.CheckNull(Convert.ToString(dRow["EmployeeCode"]));

          // Name Details
          FName = Common.CheckNull(Convert.ToString(dRow["FName"]));
          Initial = Common.CheckNull(Convert.ToString(dRow["Initial"]));

          // Email IDs
          OfficeEmailID = Common.CheckNull(Convert.ToString(dRow["OfficeEmailID"]));

          // Department
          if (dRow["DepartmentID"] != DBNull.Value)
          {
            DepartmentID = Common.CheckIntNull(Convert.ToInt32(dRow["DepartmentID"]));
          }
          else
          {
            DepartmentID = 0;
          }
          DepartmentDescription = Common.CheckNull(Convert.ToString(dRow["DepartmentDescription"]));

          // Department
          if (dRow["DesignationID"] != DBNull.Value)
          {
            DesignationID = Common.CheckIntNull(Convert.ToInt32(dRow["DesignationID"]));
          }
          else
          {
            DesignationID = 0;
          }
          DesignationDescription = Common.CheckNull(Convert.ToString(dRow["DesignationDescription"]));
          LeaveApprovar = Common.CheckNull(Convert.ToString(dRow["LeaveApprovar"]));

          if (dRow["JobStartingTime"] != DBNull.Value)
          {
            JobStartTime = Convert.ToDateTime(dRow["JobStartingTime"]);
          }
        }

        // Load Employee Job Status
        EmployeeJobStatusDL eJobStatus;
        foreach (DataRow dRow in ds.Tables[1].Rows)
        {
          eJobStatus = new EmployeeJobStatusDL();
          eJobStatus.EmployeeJobStatusID = Common.CheckIntNull(dRow["EmployeeJobStatusID"]);
          eJobStatus.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eJobStatus.JobStatusID = Common.CheckIntNull(dRow["JobStatusID"]);
          eJobStatus.JobStatusDescription = Common.CheckNull(dRow["JobStatusDescription"]);
          eJobStatus.FromDate = Convert.ToDateTime(dRow["FromDate"]);
          eJobStatus.AuditDate = Convert.ToDateTime(dRow["AuditDate"]);
          eJobStatus.AuditUserID = Common.CheckIntNull(dRow["AuditUserID"]);

          EmployeeJobStatuses.Add(eJobStatus);
        }

        if (ds.Tables[2].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[2].Rows[0];

          // ID, Code
          ReportingToEmployeeID = Common.CheckIntNull(Convert.ToInt32(dRow["ReportingToEmployeeID"]));
          ReportingToEmployeeName = Common.CheckNull(Convert.ToString(dRow["ReportingToEmployeeName"]));
        }
        else
        {
          ReportingToEmployeeID = 0;
          ReportingToEmployeeName = "";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeAdditional", ex.Message, _myConnection);
      }
    }

    /// <summary>
    /// Get Employee's Client Details
    /// </summary>        
    /// <returns>Return Client Details</returns> 
    public DataSet GetEmployeeClientDetails()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeClientDetails";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
        return ds;
      }

      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeClienttDetails", ex.Message.ToString(), _myConnection);
        return ds;
      }
    }

    /// <summary>
    /// Get Employee's Client's Project Details
    /// </summary>        
    /// <returns>Return Client's Project Details</returns> 
    public DataSet GetEmployeeClientProjectDetails(int clientID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeProjectDetails";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ClientID", DbType.Int32, clientID);
        ds = db.ExecuteDataSet(dbCommand);
        return ds;
      }

      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeClienttDetails", ex.Message.ToString(), _myConnection);
        return ds;
      }
    }

    /// <summary>
    /// Get Employee's Client's Project Details
    /// </summary>        
    /// <returns>Return Client's Project Details</returns> 
    public TransactionResult AddEmployeeClientProjectDetails()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeClientProjectHistory";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "SNo", DbType.Int32, SNo);
        db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
        db.AddInParameter(dbCommand, "ClientName", DbType.String, ClientName);
        db.AddInParameter(dbCommand, "ProjectName", DbType.String, ProjectName);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                   DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
      }

      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployeeClienttDetails", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Setting job starting time
    /// </summary>        
    /// <returns>Return Transaction Result</returns>  
    public TransactionResult SettingJobStartTime()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spUpdateEmployeeAdditionalInformation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "JobStartingTime", DbType.DateTime, JobStartTime);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
    }

    /// <summary>
    /// Setting job starting time
    /// </summary>        
    /// <returns>Return Transaction Result</returns>  
    public TransactionResult UpdateEmployeeDesignation()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spUpdateEmployeeDesignation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "DesignationID", DbType.Int32, DesignationID);
      db.AddInParameter(dbCommand, "Description", DbType.String, DesignationDescription);
      db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
      db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
    }

    /// <summary>
    /// Get Employee Designation History
    /// </summary>        
    /// <returns>None</returns>  
    public DataTable GetEmployeeDesignationHistory()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spGetEmployeeDesignationHistory";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      DataSet ds = db.ExecuteDataSet(dbCommand);
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Get Employee Client/Project History
    /// </summary>        
    /// <returns>None</returns>  
    public DataTable GetEmployeeClientProjectHistory()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spGetEmployeeClientProjectHistory";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      DataSet ds = db.ExecuteDataSet(dbCommand);
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Get Employee Current Client Name
    /// </summary>        
    /// <returns>None</returns>  
    public DataTable GetEmployeeCurrentClient()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spGetEmployeeCurrentClientName";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      DataSet ds = db.ExecuteDataSet(dbCommand);
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Get Employee Designation History
    /// </summary>        
    /// <returns>Return Transaction Result</returns>  
    public DataTable CheckEmployeePastDesignationPeriod()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spCheckPastDesignationPeriod";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
      db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
      DataSet ds = db.ExecuteDataSet(dbCommand);
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Check Employee Duplicate Designation
    /// </summary>        
    /// <returns>None</returns>  
    public DataTable CheckEmployeeDuplicateDesignation()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spCheckDuplicateEmployeeDesignationHistory";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "DesignationID", DbType.Int32, DesignationID);
      DataSet ds = db.ExecuteDataSet(dbCommand);
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Setting job starting time
    /// </summary>        
    /// <returns>Return Transaction Result</returns>  
    public TransactionResult UpdateLeaveApprovar()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spUpdateLeaveApprovar";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "LeaveApprovar", DbType.String, LeaveApprovar);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
    }

    /// <summary>
    /// AddPastDesignation
    /// </summary>
    /// <returns></returns>
    public TransactionResult AddPastDesignation()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spAddEditEmployeePastDesignation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "Status", DbType.Int32, designationStatus);
      db.AddInParameter(dbCommand, "DesignationID", DbType.Int32, DesignationID);
      db.AddInParameter(dbCommand, "Description", DbType.String, DesignationDescription);
      db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
      db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
    }

    /// <summary>
    /// DeletePastDesignation
    /// </summary>
    /// <returns></returns>
    public TransactionResult DeletePastDesignation()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spDeleteEmployeePastDesignation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "DesignationID", DbType.Int32, DesignationID);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    /// <summary>
    /// DeleteClientProjectInfo
    /// </summary>
    /// <returns></returns>
    public TransactionResult DeleteClientProjectInfo()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spDeleteEmployeeClientProjectHistory";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "SNo", DbType.Int32, SNo);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
      {
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
      }
      if (returnValue == 0)
      {
        return new TransactionResult(TransactionStatus.Failure, "Unable to delete, because the Project is being associated with Employee.'");
      }
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    /// <summary>
    /// GetEmployeeID
    /// </summary>
    public int GetEmployeeID(string employeeCode)
    {
      int _employeeID = 0;
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      string sqlCommand = "spGetEmployeeID";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, employeeCode);
      DataSet ds = db.ExecuteDataSet(dbCommand);

      if (ds.Tables[0].Rows.Count > 0)
      {
        DataRow dRow = ds.Tables[0].Rows[0];

        // ID, Code
        _employeeID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeID"]));
      }
      return _employeeID;
    }

    /// <summary>
    /// GetEmployeeIDByEmployeeCode
    /// </summary>
    /// <param name="employeeCode"></param>
    /// <returns></returns>
    public int GetEmployeeIDByEmployeeCode(int employeeCode)
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
      DbCommand dbCommand = db.GetStoredProcCommand("spGetEmployeeIDByEmployeeCode");
      dbCommand.Parameters.Clear();
      dbCommand.CommandTimeout = 300;
      db.AddInParameter(dbCommand, "EmployeeCode", DbType.Int32, employeeCode);
      int employeeID = Convert.ToInt32(db.ExecuteScalar(dbCommand).ToString());

      return employeeID;
    }

    /// <summary>
    /// Gets the Employee Details based on the EmployeeID
    /// </summary>
    private void GetEmployee()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployee";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          // ID, Code
          EmployeeID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeID"]));
          EmployeeCode = Common.CheckNull(Convert.ToString(dRow["EmployeeCode"]));

          // Name Details
          FName = Common.CheckNull(Convert.ToString(dRow["FName"]));
          Initial = Common.CheckNull(Convert.ToString(dRow["Initial"]));
          LName = Common.CheckNull(Convert.ToString(dRow["LName"]));

          // Gender
          if (dRow["GenderID"] != DBNull.Value)
          {
            GenderID = Common.CheckIntNull(Convert.ToInt32(dRow["GenderID"]));
          }
          else
          {
            GenderID = 0;
          }
          GenderDescription = Common.CheckNull(Convert.ToString(dRow["GenderDescription"]));

          // Title
          if (dRow["TitleID"] != DBNull.Value)
          {
            TitleID = Common.CheckIntNull(Convert.ToInt32(dRow["TitleID"]));
          }
          else
          {
            TitleID = 0;
          }
          TitleDescription = Common.CheckNull(Convert.ToString(dRow["TitleDescription"]));

          // Marital Status
          if (dRow["MaritalStatusID"] != DBNull.Value)
          {
            MaritalStatusID = Common.CheckIntNull(Convert.ToInt32(dRow["MaritalStatusID"]));
          }
          else
          {
            MaritalStatusID = 0;
          }
          MaritalStatusDescription = Common.CheckNull(Convert.ToString(dRow["MaritalStatusDescription"]));

          // Nationality
          if (dRow["NationalityID"] != DBNull.Value)
          {
            NationalityID = Common.CheckIntNull(Convert.ToInt32(dRow["NationalityID"]));
          }
          else
          {
            NationalityID = 0;
          }
          NationalityDescription = Common.CheckNull(Convert.ToString(dRow["NationalityDescription"]));

          // Religion
          if (dRow["ReligionID"] != DBNull.Value)
          {
            ReligionID = Common.CheckIntNull(Convert.ToInt32(dRow["ReligionID"]));
          }
          else
          {
            ReligionID = 0;
          }
          ReligionDescription = Common.CheckNull(Convert.ToString(dRow["ReligionDescription"]));

          // Date of Birth, Date of Join, Date of Leave
          if (dRow["DOB"] != DBNull.Value)
            Dob = Convert.ToDateTime(dRow["DOB"]);
          else
            Dob = null;

          if (dRow["DOJ"] != DBNull.Value)
            Doj = Convert.ToDateTime(dRow["DOJ"]);
          else
            Doj = null;

          if (dRow["DOL"] != DBNull.Value)
            Dol = Convert.ToDateTime(dRow["DOL"]);
          else
            Dol = null;

          // Blood Group
          if (dRow["BloodGroupID"] != DBNull.Value)
          {
            BloodGroupID = Common.CheckIntNull(Convert.ToInt32(dRow["BloodGroupID"]));
          }
          else
          {
            BloodGroupID = 0;
          }
          BloodGroupDescription = Common.CheckNull(Convert.ToString(dRow["BloodGroupDescription"]));

          // Wedding Date
          if (dRow["WeddingDate"] != DBNull.Value)
            WeddingDate = Convert.ToDateTime(dRow["WeddingDate"]);
          else
            WeddingDate = null;

          // Photo
          Photo = Common.CheckNull(Convert.ToString(dRow["Photo"]));

          // Passport Details
          PassportNo = Common.CheckNull(Convert.ToString(dRow["PassportNo"]));
          PassportIssuePlace = Common.CheckNull(Convert.ToString(dRow["PassportIssuePlace"]));

          if (dRow["PassportDateIssue"] != DBNull.Value)
            PassportDateIssue = Convert.ToDateTime(dRow["PassportDateIssue"]);
          else
            PassportDateIssue = null;

          if (dRow["PassportDateExpiry"] != DBNull.Value)
            PassportDateExpiry = Convert.ToDateTime(dRow["PassportDateExpiry"]);
          else
            PassportDateExpiry = null;

          // PAN
          Pan = Common.CheckNull(Convert.ToString(dRow["PAN"]));

          // Bank Details
          BankAccountNumber = Common.CheckNull(Convert.ToString(dRow["BankAccountNumber"]));
          BankName = Common.CheckNull(Convert.ToString(dRow["BankName"]));

          // Home Town
          HomeTown = Common.CheckNull(Convert.ToString(dRow["HomeTown"]));

          // Mobile
          Mobile = Common.CheckNull(Convert.ToString(dRow["Mobile"]));

          // Email IDs
          OfficeEmailID = Common.CheckNull(Convert.ToString(dRow["OfficeEmailID"]));
          PersonalEmailID = Common.CheckNull(Convert.ToString(dRow["PersonalEmailID"]));
          MessengerID = Common.CheckNull(Convert.ToString(dRow["MessengerID"]));

          // ICE Details
          IceName = Common.CheckNull(Convert.ToString(dRow["ICEName"]));
          if (dRow["ICERelationshipID"] != DBNull.Value)
          {
            IceRelationshipID = Common.CheckIntNull(Convert.ToInt32(dRow["ICERelationshipID"]));
          }
          else
          {
            IceRelationshipID = 0;
          }
          IceRelationshipDescription = Common.CheckNull(Convert.ToString(dRow["ICERelationshipDescription"]));
          IceMobile = Common.CheckNull(Convert.ToString(dRow["ICEMobile"]));

          IceName2 = Common.CheckNull(Convert.ToString(dRow["ICEName2"]));
          if (dRow["ICERelationshipID2"] != DBNull.Value)
          {
            IceRelationshipID2 = Common.CheckIntNull(Convert.ToInt32(dRow["ICERelationshipID2"]));
          }
          else
          {
            IceRelationshipID2 = 0;
          }
          IceRelationshipDescription2 = Common.CheckNull(Convert.ToString(dRow["ICERelationshipDescription2"]));
          IceMobile2 = Common.CheckNull(Convert.ToString(dRow["ICEMobile2"]));

          // Company Details
          if (dRow["CompanyID"] != DBNull.Value)
          {
            CompanyID = Common.CheckIntNull(Convert.ToInt32(dRow["CompanyID"]));
          }
          else
          {
            CompanyID = 0;
          }
          CompanyName = Common.CheckNull(Convert.ToString(dRow["CompanyName"]));

          // Job Status
          if (dRow["JobStatusID"] != DBNull.Value)
          {
            JobStatusID = Common.CheckIntNull(Convert.ToInt32(dRow["JobStatusID"]));
          }
          else
          {
            JobStatusID = 0;
          }
          JobStatusDescription = Common.CheckNull(Convert.ToString(dRow["JobStatusDescription"]));

          // Department
          if (dRow["DepartmentID"] != DBNull.Value)
          {
            DepartmentID = Common.CheckIntNull(Convert.ToInt32(dRow["DepartmentID"]));
          }
          else
          {
            DepartmentID = 0;
          }

          DepartmentDescription = Common.CheckNull(Convert.ToString(dRow["DepartmentDescription"]));

          if (dRow["JobStartingTime"] != DBNull.Value)
          {
            JobStartTime = Convert.ToDateTime(dRow["JobStartingTime"]);
          }

          // Present and Permanent Address - Is it same?
          if (dRow["IsPresentAndPermanentAddressSame"] != DBNull.Value)
            IsPresentAndPermanentAddressSame = Convert.ToBoolean(dRow["IsPresentAndPermanentAddressSame"]);
          else
            IsPresentAndPermanentAddressSame = false;

          // Designation
          if (dRow["DesignationID"] != DBNull.Value)
          {
            DesignationID = Common.CheckIntNull(Convert.ToInt32(dRow["DesignationID"]));
          }
          else
          {
            DesignationID = 0;
          }
          DesignationDescription = Common.CheckNull(Convert.ToString(dRow["DesignationDescription"]));


          LeaveApprovar = Common.CheckNull(Convert.ToString(dRow["LeaveApprovar"]));

          // Designation
          if (dRow["RoleID"] != DBNull.Value)
          {
            RoleID = Common.CheckIntNull(Convert.ToInt32(dRow["RoleID"]));
          }
          else
          {
            RoleID = 0;
          }

          RoleDescription = Common.CheckNull(Convert.ToString(dRow["RoleName"]));

          // Creator UserID
          if (dRow["CreatorUserID"] != DBNull.Value)
          {
            CreatorUserID = Common.CheckIntNull(Convert.ToInt32(dRow["CreatorUserID"]));
          }
          else
          {
            CreatorUserID = 0;
          }
          if (dRow["CreatedDate"] != DBNull.Value)
            CreatedDate = Convert.ToDateTime(dRow["CreatedDate"]);
          else
            CreatedDate = null;


          // Modifier UserID
          if (dRow["ModifierUserID"] != DBNull.Value)
          {
            ModifierUserID = Common.CheckIntNull(Convert.ToInt32(dRow["ModifierUserID"]));
          }
          else
          {
            ModifierUserID = 0;
          }
          if (dRow["ModifiedDate"] != DBNull.Value)
            ModifiedDate = Convert.ToDateTime(dRow["ModifiedDate"]);
          else
            ModifiedDate = null;
        }

        // Load Employee Addresses
        EmployeeAddressDL eAddress;
        foreach (DataRow dRow in ds.Tables[1].Rows)
        {
          eAddress = new EmployeeAddressDL();
          eAddress.EmployeeAddressID = Common.CheckIntNull(dRow["EmployeeAddressID"]);
          eAddress.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eAddress.AddressTypeID = Common.CheckIntNull(dRow["AddressTypeID"]);
          eAddress.AddressTypeDescription = Common.CheckNull(dRow["AddressTypeDescription"]);
          eAddress.AddressInfo.Address1 = Common.CheckNull(dRow["Address1"]);
          eAddress.AddressInfo.Address2 = Common.CheckNull(dRow["Address2"]);
          eAddress.AddressInfo.Address3 = Common.CheckNull(dRow["Address3"]);
          eAddress.AddressInfo.CityDescription = Common.CheckNull(dRow["City"]);
          eAddress.AddressInfo.CityID = Common.CheckIntNull(dRow["CityID"]);
          eAddress.AddressInfo.CityName = Common.CheckNull(dRow["CityName"]);
          eAddress.AddressInfo.DistrictID = Common.CheckIntNull(dRow["DistrictID"]);
          eAddress.AddressInfo.DistrictName = Common.CheckNull(dRow["DistrictName"]);
          eAddress.AddressInfo.StateID = Common.CheckIntNull(dRow["StateID"]);
          eAddress.AddressInfo.StateName = Common.CheckNull(dRow["StateName"]);
          eAddress.AddressInfo.CountryID = Common.CheckIntNull(dRow["CountryID"]);
          eAddress.AddressInfo.CountryName = Common.CheckNull(dRow["CountryName"]);
          eAddress.AddressInfo.PostalCode = Common.CheckNull(dRow["PostalCode"]);
          eAddress.Phone = Common.CheckNull(dRow["Phone"]);

          EmployeeAddresses.Add(eAddress);
        }

        // Load Employee Languages
        EmployeeLanguageKnownDL eLanguage;
        foreach (DataRow dRow in ds.Tables[2].Rows)
        {
          eLanguage = new EmployeeLanguageKnownDL();
          eLanguage.EmployeeLanguageKnownID = Common.CheckIntNull(dRow["EmployeeLanguageKnownID"]);
          eLanguage.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eLanguage.LanguageID = Common.CheckIntNull(dRow["LanguageID"]);
          eLanguage.LanguageDescription = Common.CheckNull(dRow["LanguageDescription"]);
          eLanguage.IsRead = Convert.ToBoolean(dRow["IsRead"]);
          eLanguage.IsWrite = Convert.ToBoolean(dRow["IsWrite"]);
          eLanguage.IsSpeak = Convert.ToBoolean(dRow["IsSpeak"]);

          EmployeeLanguages.Add(eLanguage);
        }

        // Load Employee Family
        EmployeeFamilyDL eFamily;
        foreach (DataRow dRow in ds.Tables[3].Rows)
        {
          eFamily = new EmployeeFamilyDL();
          eFamily.EmployeeFamilyID = Common.CheckIntNull(dRow["EmployeeFamilyID"]);
          eFamily.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eFamily.Name = Common.CheckNull(dRow["Name"]);
          eFamily.RelationshipID = Common.CheckIntNull(dRow["RelationshipID"]);
          eFamily.RelationshipDescription = Common.CheckNull(dRow["RelationshipDescription"]);
          eFamily.GenderID = Common.CheckIntNull(dRow["GenderID"]);
          eFamily.GenderDescription = Common.CheckNull(dRow["GenderDescription"]);
          if (dRow["DOB"] != DBNull.Value)
            eFamily.DOB = Convert.ToDateTime(dRow["DOB"]);
          else
            eFamily.DOB = null;

          EmployeeFamilyMembers.Add(eFamily);
        }

        // Load Employee Education
        EmployeeEducationDL eEducation;
        foreach (DataRow dRow in ds.Tables[4].Rows)
        {
          eEducation = new EmployeeEducationDL();
          eEducation.EmployeeEducationID = Common.CheckIntNull(dRow["EmployeeEducationID"]);
          eEducation.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eEducation.QualificationID = Common.CheckIntNull(dRow["QualificationID"]);
          eEducation.QualificationDescription = Common.CheckNull(dRow["QualificationDescription"]);
          eEducation.MajorID = Common.CheckIntNull(dRow["MajorID"]);
          eEducation.MajorDescription = Common.CheckNull(dRow["MajorDescription"]);
          eEducation.YearOfPass = Common.CheckIntNull(dRow["YearOfPass"]);
          eEducation.UniversityID = Common.CheckIntNull(dRow["UniversityID"]);
          eEducation.UniversityDescription = Common.CheckNull(dRow["UniversityDescription"]);
          eEducation.ClassObtained = Common.CheckNull(dRow["ClassObtained"]);
          eEducation.InstitutionDescription = Common.CheckNull(dRow["InstitutionDescription"]);

          EmployeeEducationDetails.Add(eEducation);
        }

        // Load Employee Certifications
        EmployeeCertificationDL eCertification;
        foreach (DataRow dRow in ds.Tables[5].Rows)
        {
          eCertification = new EmployeeCertificationDL();
          eCertification.EmployeeCertificationID = Common.CheckIntNull(dRow["EmployeeCertificationID"]);
          eCertification.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eCertification.Certification = Common.CheckNull(dRow["Certification"]);
          eCertification.TechnologyID = Common.CheckIntNull(dRow["TechnologyID"]);
          eCertification.TechnologyDescription = Common.CheckNull(dRow["TechnologyDescription"]);
          eCertification.YearOfPass = Common.CheckIntNull(dRow["YearOfPass"]);
          eCertification.IssuedBy = Common.CheckNull(dRow["IssuedBy"]);
          eCertification.ClassObtained = Common.CheckNull(dRow["ClassObtained"]);
          eCertification.TranscriptID = Common.CheckNull(dRow["TranscriptID"]);

          EmployeeCertifications.Add(eCertification);
        }

        // Load Employee Skills
        EmployeeSkillDL eSkill;
        foreach (DataRow dRow in ds.Tables[6].Rows)
        {
          eSkill = new EmployeeSkillDL();
          eSkill.EmployeeSkillID = Common.CheckIntNull(dRow["EmployeeSkillID"]);
          eSkill.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eSkill.TechnologyID = Common.CheckIntNull(dRow["TechnologyID"]);
          eSkill.TechnologyDescription = Common.CheckNull(dRow["TechnologyDescription"]);
          eSkill.SkillLevelID = Common.CheckIntNull(dRow["SkillLevelID"]);
          eSkill.SkillLevelDescription = Common.CheckNull(dRow["SkillLevelDescription"]);
          eSkill.ExperienceInYears = Common.CheckIntNull(dRow["ExperienceInYears"]);
          eSkill.ExperienceInMonths = Common.CheckIntNull(dRow["ExperienceInMonths"]);

          EmployeeSkills.Add(eSkill);
        }

        // Load Employee Experiences
        EmployeeExperienceDL eExperience;
        foreach (DataRow dRow in ds.Tables[7].Rows)
        {
          eExperience = new EmployeeExperienceDL();
          eExperience.EmployeeExperienceID = Common.CheckIntNull(dRow["EmployeeExperienceID"]);
          eExperience.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          eExperience.OrganizationName = Common.CheckNull(dRow["OrganizationName"]);
          eExperience.Location = Common.CheckNull(dRow["Location"]);
          eExperience.Designation = Common.CheckNull(dRow["Designation"]);
          eExperience.FromMonth = Common.CheckIntNull(dRow["FromMonth"]);
          eExperience.FromYear = Common.CheckIntNull(dRow["FromYear"]);
          eExperience.ToMonth = Common.CheckIntNull(dRow["ToMonth"]);
          eExperience.ToYear = Common.CheckIntNull(dRow["ToYear"]);
          eExperience.CTC = Convert.ToDecimal(dRow["CTC"]);
          eExperience.JobProfile = Common.CheckNull(dRow["JobProfile"]);

          EmployeeExperienceDetails.Add(eExperience);
        }

        // Load Employee Previous Projects
        EmployeePreviousEmployersProjectDL ePreviousProject;
        foreach (DataRow dRow in ds.Tables[8].Rows)
        {
          ePreviousProject = new EmployeePreviousEmployersProjectDL();
          ePreviousProject.EmployeePreviousEmployersProjectID = Common.CheckIntNull(dRow["EmployeePreviousEmployersProjectID"]);
          ePreviousProject.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          ePreviousProject.ProjectName = Common.CheckNull(dRow["ProjectName"]);
          ePreviousProject.ProjectDescription = Common.CheckNull(dRow["ProjectDescription"]);
          ePreviousProject.ClientName = Common.CheckNull(dRow["ClientName"]);
          ePreviousProject.Technology = Common.CheckNull(dRow["Technology"]);
          ePreviousProject.Domain = Common.CheckNull(dRow["Domain"]);
          ePreviousProject.FromMonth = Common.CheckIntNull(dRow["FromMonth"]);
          ePreviousProject.FromYear = Common.CheckIntNull(dRow["FromYear"]);
          ePreviousProject.ToMonth = Common.CheckIntNull(dRow["ToMonth"]);
          ePreviousProject.ToYear = Common.CheckIntNull(dRow["ToYear"]);
          ePreviousProject.TeamSize = Common.CheckIntNull(dRow["TeamSize"]);
          ePreviousProject.RolePlayed = Common.CheckNull(dRow["RolePlayed"]);
          ePreviousProject.IsOnsite = Convert.ToBoolean(dRow["IsOnsite"]);
          ePreviousProject.OnsiteLocation = Common.CheckNull(dRow["OnsiteLocation"]);

          EmployeePreviousProjects.Add(ePreviousProject);
        }

        // Load Employee Present Projects
        EmployeePresentEmployerProjectDL ePresentProject;
        foreach (DataRow dRow in ds.Tables[9].Rows)
        {
          ePresentProject = new EmployeePresentEmployerProjectDL();
          ePresentProject.EmployeePresentEmployerProjectID = Common.CheckIntNull(dRow["EmployeePresentEmployerProjectID"]);
          ePresentProject.EmployeeID = Common.CheckIntNull(dRow["EmployeeID"]);
          ePresentProject.ProjectID = Common.CheckIntNull(dRow["ProjectID"]);
          ePresentProject.ProjectName = Common.CheckNull(dRow["ProjectName"]);
          ePresentProject.ProjectDescription = Common.CheckNull(dRow["ProjectDescription"]);
          ePresentProject.ClientID = Common.CheckIntNull(dRow["ClientID"]);
          ePresentProject.ClientName = Common.CheckNull(dRow["ClientName"]);
          ePresentProject.Domain = Common.CheckNull(dRow["Domain"]);

          if (dRow["FromDate"] != DBNull.Value)
            ePresentProject.FromDate = Convert.ToDateTime(dRow["FromDate"]);
          else
            ePresentProject.FromDate = null;
          if (dRow["ToDate"] != DBNull.Value)
            ePresentProject.ToDate = Convert.ToDateTime(dRow["ToDate"]);
          else
            ePresentProject.ToDate = null;
          ePresentProject.JobRoleID = Common.CheckIntNull(dRow["JobRoleID"]);
          ePresentProject.JobRoleDescription = Common.CheckNull(dRow["JobRoleDescription"]);

          EmployeePresentProjects.Add(ePresentProject);
        }

        foreach (DataRow dRow in ds.Tables[10].Rows)
        {
          ClientName = Common.CheckNull(Convert.ToString(dRow["ClientName"]));
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployee", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// GetEmployeeByMailID
    /// </summary>
    /// <param name="mailID"></param>
    /// <returns></returns>
    public string GetEmployeeByMailID(string mailID)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeByMailID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "MailID", DbType.String, mailID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        if (ds.Tables[0].Rows.Count > 0)
        {
          return ds.Tables[0].Rows[0][0].ToString();
        }
        else
        {
          return "";
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeDL.cs", "GetEmployee", ex.Message.ToString(), _myConnection);
        return "";
      }
    }

    /// <summary>
    /// AddEditEmployee
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployee(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditEmployee";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      // ID, Code
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, EmployeeCode);

      // Name Details
      db.AddInParameter(dbCommand, "FName", DbType.String, FName);
      db.AddInParameter(dbCommand, "Initial", DbType.String, Initial);
      if ((LName != "") && (LName != null))
        db.AddInParameter(dbCommand, "LName", DbType.String, LName);

      // Gender
      if (GenderID != 0)
      {
        db.AddInParameter(dbCommand, "GenderID", DbType.Int32, GenderID);
      }

      // Title
      if (TitleID != 0)
      {
        db.AddInParameter(dbCommand, "TitleID", DbType.Int32, TitleID);
      }

      // Marital Status
      if (MaritalStatusID != 0)
      {
        db.AddInParameter(dbCommand, "MaritalStatusID", DbType.Int32, MaritalStatusID);
      }

      // Nationality
      if (NationalityID != 0)
      {
        db.AddInParameter(dbCommand, "NationalityID", DbType.Int32, NationalityID);
      }

      // Religion
      if (ReligionID != 0)
      {
        db.AddInParameter(dbCommand, "ReligionID", DbType.Int32, ReligionID);
      }

      // Date of Birth, Date of Join, Date of Leave
      db.AddInParameter(dbCommand, "DOB", DbType.DateTime, Dob);
      db.AddInParameter(dbCommand, "DOJ", DbType.DateTime, Doj);

      if (Dol != null)
        db.AddInParameter(dbCommand, "DOL", DbType.DateTime, Dol);

      // Blood Group
      if (BloodGroupID != 0)
      {
        db.AddInParameter(dbCommand, "BloodGroupID", DbType.Int32, BloodGroupID);
      }

      // Wedding Date
      if (WeddingDate != null)
        db.AddInParameter(dbCommand, "WeddingDate", DbType.DateTime, WeddingDate);

      // Photo
      if ((Photo != "") && (Photo != null))
        db.AddInParameter(dbCommand, "Photo", DbType.String, Photo);

      // Passport Details
      if ((PassportNo != "") && (PassportNo != null))
        db.AddInParameter(dbCommand, "PassportNo", DbType.String, PassportNo);
      if ((PassportIssuePlace != "") && (PassportIssuePlace != null))
        db.AddInParameter(dbCommand, "PassportIssuePlace", DbType.String, PassportIssuePlace);
      if (PassportDateIssue != null)
        db.AddInParameter(dbCommand, "PassportDateIssue", DbType.DateTime, PassportDateIssue);
      if (PassportDateExpiry != null)
        db.AddInParameter(dbCommand, "PassportDateExpiry", DbType.DateTime, PassportDateExpiry);

      // PAN
      if ((Pan != "") && (Pan != null))
        db.AddInParameter(dbCommand, "PAN", DbType.String, Pan);

      // Bank Details
      if ((BankAccountNumber != "") && (BankAccountNumber != null))
        db.AddInParameter(dbCommand, "BankAccountNumber", DbType.String, BankAccountNumber);
      if ((BankName != "") && (BankName != null))
        db.AddInParameter(dbCommand, "BankName", DbType.String, BankName);

      // Home Town
      if ((HomeTown != "") && (HomeTown != null))
        db.AddInParameter(dbCommand, "HomeTown", DbType.String, HomeTown);

      // Mobile
      if ((Mobile != "") && (Mobile != null))
        db.AddInParameter(dbCommand, "Mobile", DbType.String, Mobile);

      // Email IDs
      if ((OfficeEmailID != "") && (OfficeEmailID != null))
        db.AddInParameter(dbCommand, "OfficeEmailID", DbType.String, OfficeEmailID);
      if ((PersonalEmailID != "") && (PersonalEmailID != null))
        db.AddInParameter(dbCommand, "PersonalEmailID", DbType.String, PersonalEmailID);
      if ((MessengerID != "") && (MessengerID != null))
        db.AddInParameter(dbCommand, "MessengerID", DbType.String, MessengerID);

      // ICE Details
      if ((IceName != "") && (IceName != null))
        db.AddInParameter(dbCommand, "ICEName", DbType.String, IceName);
      if (IceRelationshipID != 0)
        db.AddInParameter(dbCommand, "ICERelationshipID", DbType.Int32, IceRelationshipID);
      if ((IceMobile != "") && (IceMobile != null))
        db.AddInParameter(dbCommand, "ICEMobile", DbType.String, IceMobile);

      if ((IceName2 != "") && (IceName2 != null))
        db.AddInParameter(dbCommand, "ICEName2", DbType.String, IceName2);
      if (IceRelationshipID2 != 0)
        db.AddInParameter(dbCommand, "ICERelationshipID2", DbType.Int32, IceRelationshipID2);

      if ((IceMobile2 != "") && (IceMobile2 != null))
        db.AddInParameter(dbCommand, "ICEMobile2", DbType.String, IceMobile2);


      // Company Details
      if (CompanyID != 0)
      {
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
      }

      // Job Status
      if (JobStatusID != 0)
      {
        db.AddInParameter(dbCommand, "JobStatusID", DbType.Int32, JobStatusID);
      }

      // Department
      if (DepartmentID != 0)
      {
        db.AddInParameter(dbCommand, "DepartmentID", DbType.Int32, DepartmentID);
      }


      // Present and Permanent Address - Is it same?
      db.AddInParameter(dbCommand, "IsPresentAndPermanentAddressSame", DbType.Boolean, IsPresentAndPermanentAddressSame);

      // Designation
      if (DesignationID != 0)
      {
        db.AddInParameter(dbCommand, "DesignationID", DbType.Int32, DesignationID);
      }

      // Creator UserID
      if (CreatorUserID != 0)
      {
        db.AddInParameter(dbCommand, "CreatorUserID", DbType.Int32, CreatorUserID);
      }

      // Modifier UserID
      if (ModifierUserID != 0)
      {
        db.AddInParameter(dbCommand, "ModifierUserID", DbType.Int32, ModifierUserID);
      }

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      EmployeeID = returnValue;

      if (AddEditOption == 0)
      {
        // Add a User Login For the Employee

        // Create a new User Object
        UsersDL aUser = new UsersDL();
        aUser.AddEditOption = 0;
        GenerateUserNameAndPassword(aUser);
        aUser.EmployeeID = EmployeeID;
        aUser.IsValid = false;

        CompanyXUsersDL uCompany = new CompanyXUsersDL();
        uCompany.CompanyID = CompanyID;
        uCompany.RoleID = RoleID;
        //uCompany.RoleID = (assign the role here)
        uCompany.IsDefault = true;
        aUser.UserCompanies.Add(uCompany);


        TransactionResult result = null;
        result = aUser.AddEditUser(db, transaction);
        if (result.Status == TransactionStatus.Failure)
        {
          transaction.Rollback();
          return result;
        }
        if (aUser.UserCompanies != null)
        {
          result = aUser.AddCompanyXUsers(db, transaction);
          if (result.Status == TransactionStatus.Failure)
          {
            transaction.Rollback();
            return result;
          }
        }
      }

      foreach (EmployeeAddressDL eAddr in EmployeeAddresses)
      {
        TransactionResult result = null;
        if (eAddr.AddEditOption == 0 || eAddr.AddEditOption == 1)
        {
          result = eAddr.AddEditEmployeeAddress(db, transaction);
          if (result.Status == TransactionStatus.Failure)
          {
            transaction.Rollback();
            return result;
          }
        }
        else if (eAddr.AddEditOption == 2)
        {
          result = eAddr.DeleteEmployeeAddress(db, transaction);
          if (result.Status == TransactionStatus.Failure)
          {
            transaction.Rollback();
            return result;
          }
        }
      }

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding");

      }
      else
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Added");
      }
    }

    #endregion
  }
}