using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class FeedbackReplyDL : CommonForAllDL
  {
    #region Properties

    public int FeedbackReplyID { get; set; }

    public string FeedbackReplyDescription { get; set; }

    public int FeedbackID { get; set; }

    public int RepliedByUserID { get; set; }

    public Nullable<DateTime> ReplyDate { get; set; }

    public Boolean IsDeleted { get; set; }

    public int FeedbackReplyStatusID { get; set; }

    public new int AddEditOption { get; set; }

    public int DeleteOption { get; set; }

    #endregion

    #region Constructors

    public FeedbackReplyDL()
    {
    }

    public FeedbackReplyDL(int feedbackReplyID, bool getAllProperties)
    {
      FeedbackReplyID = feedbackReplyID;
      if (getAllProperties)
      {
        GetFeedbackReply();
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
              //Adding or Editing FeedbackReply
              result = AddEditFeedbackReply(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteFeedbackReply(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get FeedbackReply List By FeedbackID 
    /// </summary>
    /// <param name="feedbackID">feedbackID</param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetFeedbackReplyListByFeedbackID(int feedbackID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackReplyListByFeedbackID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FeedbackID", DbType.Int32, feedbackID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "GetFeedbackReplyListByFeedbackID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get Feedback Reply 
    /// </summary>
    private void GetFeedbackReply()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetFeedbackReply";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "FeedbackReplyID", DbType.Int32, FeedbackReplyID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              FeedbackReplyID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FeedbackReplyID")));
              FeedbackReplyDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("FeedbackReplyDescription")));
              FeedbackID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FeedbackID")));
              RepliedByUserID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("RepliedByUserID")));

              if (dataReader.GetString(dataReader.GetOrdinal("ReplyDate")) != "")
              {
                ReplyDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("ReplyDate")));
              }

              IsDeleted = dataReader.GetBoolean(dataReader.GetOrdinal("IsDeleted"));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "GetFeedbackReply", ex.Message, _myConnection);
      }
    }

    /// <summary>
    /// Add Edit Feedback Reply
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditFeedbackReply(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditFeedbackReply";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "FeedbackReplyID", DbType.Int32, FeedbackReplyID);
        db.AddInParameter(dbCommand, "FeedbackReplyDescription", DbType.String, FeedbackReplyDescription);
        db.AddInParameter(dbCommand, "FeedbackID", DbType.Int32, FeedbackID);
        db.AddInParameter(dbCommand, "RepliedByUserID", DbType.Int32, RepliedByUserID);
        if (ReplyDate != null)
          db.AddInParameter(dbCommand, "ReplyDate", DbType.DateTime, ReplyDate);
        else
          db.AddInParameter(dbCommand, "ReplyDate", DbType.DateTime, DBNull.Value);

        db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, IsDeleted);

        db.AddInParameter(dbCommand, "FeedbackReplyStatusID", DbType.Int32, FeedbackReplyStatusID);
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
            return new TransactionResult(TransactionStatus.Success, "Reply Sent"); // "Successfully Added"
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "FeedbackReplyDL.cs", "AddEditFeedbackReply", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Feedback Reply
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteFeedbackReply(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteFeedbackReply");
      db.AddInParameter(dbCommand, "FeedbackReplyID", DbType.Int32, FeedbackReplyID);

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

    #endregion
  }
}
