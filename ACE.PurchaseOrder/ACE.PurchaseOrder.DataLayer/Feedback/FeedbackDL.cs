using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class FeedbackDL : CommonForAllDL
  {
    #region Properties

    public int FeedbackID { get; set; }

    public string FeedbackDescription { get; set; }

    public int UserID { get; set; }

    public int FeedbackTypeID { get; set; }

    public string FeedbackTypeDescription { get; set; }

    public int FeedbackStatusID { get; set; }

    public string FeedbackStatusDescription { get; set; }

    public DateTime CreatedDate { get; set; }

    public Nullable<DateTime> ClosedDate { get; set; }

    public int FeedbackPriorityID { get; set; }

    public string FeedbackPriorityDescription { get; set; }

    public Boolean IsDeleted { get; set; }

    public int CompanyID { get; set; }

    public List<FeedbackReplyDL> FeedbackReplies { get; set; }

    public new int AddEditOption { get; set; }

    public int DeleteOption { get; set; }

    #endregion

    #region Constructor

    public FeedbackDL()
    {
      FeedbackReplies = new List<FeedbackReplyDL>();
    }

    public FeedbackDL(int feedbackID, bool getAllProperties)
    {
      FeedbackID = feedbackID;
      FeedbackReplies = new List<FeedbackReplyDL>();

      if (getAllProperties)
      {
        GetFeedback();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Commit
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
              //Adding Or Editing Feedback
              result = AddEditFeedback(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteFeedback(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Feedback
    /// </summary>
    private void GetFeedback()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedback";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FeedbackID", DbType.Int32, FeedbackID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Feedback Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          // ID, Description
          FeedbackID = Common.CheckIntNull(Convert.ToInt32(dRow["FeedbackID"]));
          FeedbackDescription = Common.CheckNull(Convert.ToString(dRow["FeedbackDescription"]));

          // User
          UserID = Common.CheckIntNull(Convert.ToInt32(dRow["UserID"]));

          // Feedback Type ID, Description
          FeedbackTypeID = Common.CheckIntNull(Convert.ToInt32(dRow["FeedbackTypeID"]));
          FeedbackTypeDescription = Common.CheckNull(Convert.ToString(dRow["FeedbackTypeDescription"]));

          // Feedback Status ID, Description
          FeedbackStatusID = Common.CheckIntNull(Convert.ToInt32(dRow["FeedbackStatusID"]));
          FeedbackStatusDescription = Common.CheckNull(Convert.ToString(dRow["FeedbackStatusDescription"]));

          // Created, Closed Date
          if (dRow["CreatedDate"] != DBNull.Value)
            CreatedDate = Convert.ToDateTime(dRow["CreatedDate"]);
          if (dRow["ClosedDate"] != DBNull.Value)
            ClosedDate = Convert.ToDateTime(dRow["ClosedDate"]);
          else
            ClosedDate = null;

          // Feedback Priority ID, Description
          FeedbackPriorityID = Common.CheckIntNull(Convert.ToInt32(dRow["FeedbackPriorityID"]));
          FeedbackPriorityDescription = Common.CheckNull(Convert.ToString(dRow["FeedbackPriorityDescription"]));

          // Deleted status
          if (dRow["IsDeleted"] != DBNull.Value)
            IsDeleted = Convert.ToBoolean(dRow["IsDeleted"]);
          else
            IsDeleted = false;

          // CompanyID
          CompanyID = Common.CheckIntNull(Convert.ToInt32(dRow["CompanyID"]));
        }

        // Load Feedback Replies
        FeedbackReplyDL fReply;
        foreach (DataRow dRow in ds.Tables[1].Rows)
        {
          fReply = new FeedbackReplyDL();
          fReply.FeedbackReplyID = Common.CheckIntNull(dRow["FeedbackReplyID"]);
          fReply.FeedbackReplyDescription = Common.CheckNull(dRow["FeedbackReplyDescription"]);
          fReply.FeedbackID = Common.CheckIntNull(dRow["FeedbackID"]);
          fReply.RepliedByUserID = Common.CheckIntNull(dRow["RepliedByUserID"]);

          if (dRow["ReplyDate"] != DBNull.Value)
            fReply.ReplyDate = Convert.ToDateTime(dRow["ReplyDate"]);

          if (dRow["IsDeleted"] != DBNull.Value)
            fReply.IsDeleted = Convert.ToBoolean(dRow["IsDeleted"]);
          else
            fReply.IsDeleted = false;

          FeedbackReplies.Add(fReply);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "GetFeedback", ex.Message, _myConnection);
      }
    }

    /// <summary>
    /// Add Edit Feedback
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditFeedback(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditFeedback";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "FeedbackID", DbType.Int32, FeedbackID);
        db.AddInParameter(dbCommand, "FeedbackDescription", DbType.String, FeedbackDescription);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, UserID);
        db.AddInParameter(dbCommand, "FeedbackTypeID", DbType.Int32, FeedbackTypeID);
        db.AddInParameter(dbCommand, "FeedbackStatusID", DbType.Int32, FeedbackStatusID);
        if (ClosedDate != null)
          db.AddInParameter(dbCommand, "ClosedDate", DbType.DateTime, ClosedDate);
        db.AddInParameter(dbCommand, "FeedbackPriorityID", DbType.Int32, FeedbackPriorityID);
        db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, IsDeleted);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);

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
            return new TransactionResult(TransactionStatus.Success, "Feedback Successfully Sent");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "AddEditFeedback", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Feedback
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteFeedback(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteFeedback");
        db.AddInParameter(dbCommand, "FeedbackID", DbType.Int32, FeedbackID);

        db.AddInParameter(dbCommand, "DeleteOption", DbType.Int16, DeleteOption);

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
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "DeleteFeedback", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed deleting");
      }
    }

    /// <summary>
    /// Get Feedback List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetFeedbackList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "GetFeedbackList", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Feedback List By CompanyID
    /// </summary>
    /// <param name="companyID">The feedbacks of the specified Company ID</param>
    /// <param name="feedbackStatusID">The FeedbackStatusID (if 0 - get all status, else - the specified status)</param>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetFeedbackListByCompanyID(int companyID, int feedbackStatusID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "FeedbackStatusID", DbType.Int32, feedbackStatusID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "GetFeedbackListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Feedback List By CompanyID And UserID
    /// </summary>
    /// <param name="companyID">The feedbacks of the specified Company ID</param>
    /// <param name="feedbackStatusID">The FeedbackStatusID (if 0 - get all status, else - the specified status)</param>
    /// <param name="userID">The feedbacks of the specified userID - the specified status)</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetFeedbackListByCompanyIDAndUserID(int companyID, int feedbackStatusID, int userID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackListByCompanyIDAndUserID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "FeedbackStatusID", DbType.Int32, feedbackStatusID);
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, userID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackDL.cs", "GetFeedbackListByCompanyIDAndUserID", ex.Message, _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
