using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeePresentEmployerProjectDL : CommonForAllDL
  {
    #region Properties

    public int EmployeePresentEmployerProjectID { get; set; }

    public int EmployeeID { get; set; }

    public ProjectDL ProjectDone { get; set; }

    public ProjectDL ProjectDetail { get; set; }

    public int ProjectID { get; set; }

    public string ProjectName { get; set; }

    public string ProjectDescription { get; set; }

    public int ClientID { get; set; }

    public string ClientName { get; set; }

    public string Domain { get; set; }

    public string Technology { get; set; }

    public Nullable<DateTime> FromDate { get; set; }

    public Nullable<DateTime> ToDate { get; set; }

    public int JobRoleID { get; set; }

    public string JobRoleDescription { get; set; }

    #endregion

    #region Constructors

    public EmployeePresentEmployerProjectDL()
    {
      ProjectDetail = new ProjectDL();
    }

    public EmployeePresentEmployerProjectDL(int employeePresentEmployerProjectID, bool getAllProperties)
    {
      EmployeePresentEmployerProjectID = employeePresentEmployerProjectID;
      ProjectDetail = new ProjectDL();
      if (getAllProperties)
      {
        GetEmployeePresentEmployerProject();
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
              //Adding or Editing EmployeePresentEmployerProject
              result = AddEditEmployeePresentEmployerProject(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeePresentEmployerProject(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// GetEmployeePresentEmployerProject
    /// </summary>
    private void GetEmployeePresentEmployerProject()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeePresentEmployerProject";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeePresentEmployerProjectID", DbType.Int32, EmployeePresentEmployerProjectID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeePresentEmployerProjectID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeePresentEmployerProjectID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              ProjectDetail.ProjectID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ProjectID")));
              ProjectDetail.ProjectName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ProjectName")));
              ProjectDetail.ProjectDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ProjectDescription")));
              ProjectDetail.ClientID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ClientID")));
              ProjectDetail.ClientName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ClientName")));
              ProjectDetail.Domain = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Domain")));
              ProjectDetail.Technology = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Technology")));
              if (dataReader.GetString(dataReader.GetOrdinal("FromDate")) != "")
              {
                FromDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("FromDate")));
              }
              else
              {
                FromDate = null;
              }
              if (dataReader.GetString(dataReader.GetOrdinal("ToDate")) != "")
              {
                ToDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("ToDate")));
              }
              else
              {
                ToDate = null;
              }
              JobRoleID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("JobRoleID")));
              JobRoleDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("JobRoleDescription")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "GetEmployeePresentEmployerProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// AddEditEmployeePresentEmployerProject
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeePresentEmployerProject(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeePresentEmployerProject";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeePresentEmployerProjectID", DbType.Int32, EmployeePresentEmployerProjectID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "ToDate", DbType.DateTime, ToDate);
        if (JobRoleID != 0)
          db.AddInParameter(dbCommand, "JobRoleID", DbType.Int32, JobRoleID);
        else
          db.AddInParameter(dbCommand, "JobRoleID", DbType.Int32, DBNull.Value);

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
        ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "AddEditEmployeePresentEmployerProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// DeleteEmployeePresentEmployerProject
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeePresentEmployerProject(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeePresentEmployerProject");
        db.AddInParameter(dbCommand, "EmployeePresentEmployerProjectID", DbType.Int32, EmployeePresentEmployerProjectID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "DeleteEmployeePresentEmployerProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// VerifyEmployeeProjectDate
    /// </summary>
    /// <returns></returns>
    public DataTable VerifyEmployeeProjectDate()
    {
      DataTable dt = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetProjectDate";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        dt = ds.Tables[0];
        return dt;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeePresentEmployerProjectDL.cs", "DeleteEmployeePresentEmployerProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return dt;
      }
    }

    #endregion
  }
}
