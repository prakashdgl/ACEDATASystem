using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class MachineTypeDL : CommonForAllDL
  {
    #region Properties

    public int TypeID { get; set; }

    public string TypeName { get; set; }

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
              //Adding Or Editing MachineType
              result = AddEditType(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteType(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a MachineType - Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditType(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "Type_MERGE";//spAddEditType
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "TypeID", DbType.Int32, TypeID);
        db.AddInParameter(dbCommand, "TypeName", DbType.String, TypeName);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        TypeID = returnValue;

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
            return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Type Name");
          else
            return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Type Name");
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
        ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "AddEditType", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// To Delete a MachineType - Changed new procedure name on 14/07/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteType(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("Type_MERGE");//spDeleteType
        db.AddInParameter(dbCommand, "TypeID", DbType.Int32, TypeID);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
          if (returnValue == 99)
          return new TransactionResult(TransactionStatus.Failure, "Unable to delete! This Machine Type is configured in Hardware");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "DeleteType", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// Get MachineType List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetTypeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "Type_SELECT";//spGetType
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineTypeDL.cs", "GetTypeList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
