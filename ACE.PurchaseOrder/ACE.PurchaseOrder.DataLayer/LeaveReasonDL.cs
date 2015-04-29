using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class LeaveReasonDL : CommonForAllDL
  {
    #region Properties

    public int ReasonID { get; set; }

    public string Reason { get; set; }

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
              //Adding Or Editing Technology
              result = AddEditLeaveReason(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteLeaveReason(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "LeaveReasonDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveReasonDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "LeaveReasonDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a Technology - Added sp name 23/06/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditLeaveReason(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "LeaveReasons_MERGE";//spAddEditLeaveReasons
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "ReasonID", DbType.Int32, ReasonID);
      db.AddInParameter(dbCommand, "Reason", DbType.String, Reason);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      ReasonID = returnValue;

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      else if (returnValue == -99)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Leave Reason");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Leave Reason");
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
    /// To Delete a Leave Reason - added sp name on 23/06/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteLeaveReason(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("LeaveReasons_MERGE");//spDeleteLeaveReason
      db.AddInParameter(dbCommand, "ReasonID", DbType.Int32, ReasonID);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption); //added on 23/06/2014
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    /// <summary>
    /// Get Language List - added sp name on 23/06/2014
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetLeaveReasonList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "LeaveReasonList_SELECT";//spGetLeaveReasonList
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "LeaveReasonDL.cs", "GetLeaveReasonList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
