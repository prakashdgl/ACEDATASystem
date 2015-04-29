using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class MachineMemoryDL : CommonForAllDL
  {
    #region Properties

    public int MemoryID { get; set; }

    public string MemoryName { get; set; }

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
              //Adding Or Editing MachineMemory
              result = AddEditMemory(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteMemory(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a MachineMemory  - Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditMemory(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "Memory_MERGE";//spAddEditMemory
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "MemoryID", DbType.Int32, MemoryID);
        db.AddInParameter(dbCommand, "MemoryName", DbType.String, MemoryName);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        MemoryID = returnValue;

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
            return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Memory Name");
          else
            return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Memory Name");
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
        ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "AddEditMemory", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// To Delete a MachineMemory  - Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteMemory(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("Memory_MERGE");//spDeleteMemory
        db.AddInParameter(dbCommand, "MemoryID", DbType.Int32, MemoryID);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
        if (returnValue == 99)
          return new TransactionResult(TransactionStatus.Failure, "Unable to delete!This processor is configured in Hardware");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "DeleteMemory", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// Get MachineMemory List  - Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetMemoryList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "Memory_SELECT";//spGetMemory
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineMemoryDL.cs", "GetMemoryList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
