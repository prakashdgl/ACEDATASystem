using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class ProjectTaskDL : CommonForAllDL
  {
    #region Properties

    public int ProjectTaskID { get; set; }

    public int ProjectID { get; set; }

    public string TaskNumber { get; set; }

    public string Description { get; set; }

    public Nullable<DateTime> PlannedStartDate { get; set; }

    public Nullable<DateTime> PlannedEndDate { get; set; }

    public Nullable<DateTime> RevisedStartDate { get; set; }

    public Nullable<DateTime> RevisedEndDate { get; set; }

    public Nullable<DateTime> ActualStartDate { get; set; }

    public Nullable<DateTime> ActualEndDate { get; set; }

    public Decimal PlannedEffort { get; set; }

    public Decimal RevisedEffort { get; set; }

    public Decimal ActualEffort { get; set; }

    public int ProjectTaskStatusID { get; set; }

    #endregion

    #region Methods

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
              //Adding Or Editing Resource
              result = AddEditProjectTask(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteProjectTask(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "ProjectTaskDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "ProjectTaskDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "ProjectTaskDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get ProjectTask List By ProjectID
    /// </summary>        /// 
    /// <param name="companyID"></param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetProjectTaskListByProjectID(int projectID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetProjectTaskListByProjectID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, projectID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ProjectTaskDL.cs", "GetProjectTaskListByProjectID", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// To Add / Edit a ProjectXResource
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditProjectTask(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditProjectTask";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "ProjectTaskID", DbType.Int32, ProjectTaskID);
      db.AddInParameter(dbCommand, "ProjectID", DbType.Int32, ProjectID);
      db.AddInParameter(dbCommand, "TaskNumber", DbType.String, TaskNumber);
      db.AddInParameter(dbCommand, "Description", DbType.String, Description);
      if (PlannedStartDate != null)
        db.AddInParameter(dbCommand, "PlannedStartDate", DbType.DateTime, PlannedStartDate);
      if (PlannedEndDate != null)
        db.AddInParameter(dbCommand, "PlannedEndDate", DbType.DateTime, PlannedEndDate);

      if (RevisedStartDate != null)
        db.AddInParameter(dbCommand, "RevisedStartDate", DbType.DateTime, RevisedStartDate);
      if (RevisedEndDate != null)
        db.AddInParameter(dbCommand, "RevisedEndDate", DbType.DateTime, RevisedEndDate);

      if (ActualStartDate != null)
        db.AddInParameter(dbCommand, "ActualStartDate", DbType.DateTime, ActualStartDate);
      if (ActualEndDate != null)
        db.AddInParameter(dbCommand, "ActualEndDate", DbType.DateTime, ActualEndDate);

      db.AddInParameter(dbCommand, "PlannedEffort", DbType.Decimal, PlannedEffort);
      db.AddInParameter(dbCommand, "RevisedEffort", DbType.Decimal, RevisedEffort);
      db.AddInParameter(dbCommand, "ActualEffort", DbType.Decimal, ActualEffort);

      if (ProjectTaskStatusID != 0)
        db.AddInParameter(dbCommand, "ProjectTaskStatusID", DbType.Int32, ProjectTaskStatusID);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      ProjectTaskID = returnValue;

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

    /// <summary>
    /// To Delete a ProjectTask
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteProjectTask(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteProjectTask");
      db.AddInParameter(dbCommand, "ProjectTaskID", DbType.Int32, ProjectTaskID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    #endregion
  }
}
