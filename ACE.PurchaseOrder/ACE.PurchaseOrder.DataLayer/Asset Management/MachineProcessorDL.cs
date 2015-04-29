using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class MachineProcessorDL : CommonForAllDL
  {
    #region Properties

    public int ProcessorID { get; set; }

    public string ProcessorName { get; set; }

    #endregion

    #region  Methods

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
              //Adding Or Editing MachineProcessor
              result = AddEditProcessor(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteProcessor(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a MachineProcessor- Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditProcessor(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "Processor_MERGE";//spAddEditProcessor
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "ProcessorID", DbType.Int32, ProcessorID);
        db.AddInParameter(dbCommand, "ProcessorName", DbType.String, ProcessorName);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        ProcessorID = returnValue;

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
            return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Processor Name");
          else
            return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Processor Name");
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
        ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "AddEditProcessor", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
      }
    }

    /// <summary>
    /// To Delete a MachineProcessor- Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteProcessor(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("Processor_MERGE");//spDeleteProcessor
        db.AddInParameter(dbCommand, "ProcessorID", DbType.Int32, ProcessorID);
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
        ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "DeleteProcessor", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// Get MachineProcessor List- Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetProcessorList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "Processor_SELECT";//spGetProcessor
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineProcessorDL.cs", "GetProcessorList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
