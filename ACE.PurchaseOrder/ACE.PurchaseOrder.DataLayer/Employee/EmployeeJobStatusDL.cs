using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeJobStatusDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeJobStatusID { get; set; }

    public int EmployeeID { get; set; }

    public int JobStatusID { get; set; }

    public string JobStatusDescription { get; set; }

    public DateTime FromDate { get; set; }

    public int AuditUserID { get; set; }

    public Nullable<DateTime> AuditDate { get; set; }

    public new int AddEditOption { get; set; }

    #endregion

    #region Constructors

    public EmployeeJobStatusDL()
    {
    }

    public EmployeeJobStatusDL(int employeeJobStatusID, bool getAllProperties)
    {
      EmployeeJobStatusID = employeeJobStatusID;
      if (getAllProperties)
      {
        GetEmployeeJobStatus();
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
              //Adding Or Editing Employee JobStatus
              result = AddEditEmployeeJobStatus(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeJobStatus(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Job Status 
    /// </summary>
    private void GetEmployeeJobStatus()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeJobStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeJobStatusID", DbType.Int32, EmployeeJobStatusID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Job Status Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          // EmployeeJobStatusID, EmployeeID
          EmployeeJobStatusID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeJobStatusID"]));
          EmployeeID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeID"]));

          // JobStatus ID, Description
          JobStatusID = Common.CheckIntNull(Convert.ToInt32(dRow["JobStatusID"]));
          JobStatusDescription = Common.CheckNull(Convert.ToString(dRow["JobStatusDescription"]));

          // From Date
          if (dRow["FromDate"] != DBNull.Value)
            FromDate = Convert.ToDateTime(dRow["FromDate"]);

          // Audit User ID, Date
          AuditUserID = Common.CheckIntNull(Convert.ToInt32(dRow["AuditUserID"]));
          if (dRow["AuditDate"] != DBNull.Value)
            AuditDate = Convert.ToDateTime(dRow["AuditDate"]);

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "GetEmployeeJobStatus", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Get Employee JobStatus By EmployeeID 
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public string GetEmployeeJobStatusByEmployeeID(int employeeID)
    {
      string jobStatus = "";
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeJobStatusByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Job Status Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          // JobStatus ID, Description
          jobStatus = Common.CheckNull(Convert.ToString(dRow["JobStatus"]));
        }
        return jobStatus;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "GetEmployeeJobStatus", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return jobStatus;
    }

    /// <summary>
    /// Add Edit Employee JobStatus
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeJobStatus(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeJobStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeJobStatusID", DbType.Int32, EmployeeJobStatusID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "JobStatusID", DbType.Int32, JobStatusID);
        db.AddInParameter(dbCommand, "FromDate", DbType.DateTime, FromDate);
        db.AddInParameter(dbCommand, "AuditUserID", DbType.Int32, AuditUserID);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
        {
          if (AddEditOption == 1)
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "AddEditEmployeeJobStatus", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee JobStatus
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeJobStatus(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeJobStatus");
        db.AddInParameter(dbCommand, "EmployeeJobStatusID", DbType.Int32, EmployeeJobStatusID);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeJobStatusDL.cs", "DeleteEmployeeJobStatus", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
