using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeSkillDL : CommonForAllDL
  {
    #region Properties

    public new ScreenMode ScreenMode { get; set; }

    public int EmployeeSkillID { get; set; }

    public int EmployeeID { get; set; }

    public int TechnologyID { get; set; }

    public string TechnologyDescription { get; set; }

    public int SkillLevelID { get; set; }

    public string SkillLevelDescription { get; set; }

    public int ExperienceInYears { get; set; }

    public int ExperienceInMonths { get; set; }

    #endregion

    #region Constructors

    public EmployeeSkillDL()
    {
    }

    public EmployeeSkillDL(int employeeSkillID, bool getAllProperties)
    {
      EmployeeSkillID = employeeSkillID;
      if (getAllProperties)
      {
        GetEmployeeSkill();
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
              //Adding or Editing EmployeeSkill
              result = AddEditEmployeeSkill(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeSkill(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Skill
    /// </summary>
    private void GetEmployeeSkill()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeSkill";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeSkillID", DbType.Int32, EmployeeSkillID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeSkillID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeSkillID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              TechnologyID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("TechnologyID")));
              TechnologyDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("TechnologyDescription")));
              SkillLevelID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("SkillLevelID")));
              SkillLevelDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("SkillLevelDescription")));
              ExperienceInYears = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ExperienceInYears")));
              ExperienceInMonths = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ExperienceInMonths")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "GetEmployeeSkill", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit Employee Skill
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeSkill(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeSkill";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeSkillID", DbType.Int32, EmployeeSkillID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "TechnologyID", DbType.Int32, TechnologyID);
        db.AddInParameter(dbCommand, "SkillLevelID", DbType.Int32, SkillLevelID);
        db.AddInParameter(dbCommand, "ExperienceInYears", DbType.Int32, ExperienceInYears);
        db.AddInParameter(dbCommand, "ExperienceInMonths", DbType.Int32, ExperienceInMonths);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "AddEditEmployeeSkill", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Skill
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeSkill(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeSkill");
        db.AddInParameter(dbCommand, "EmployeeSkillID", DbType.Int32, EmployeeSkillID);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeSkillDL.cs", "DeleteEmployeeSkill", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
