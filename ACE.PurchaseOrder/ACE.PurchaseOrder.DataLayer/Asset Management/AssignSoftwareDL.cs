using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class AssignSoftwareDL : CommonForAllDL
  {
    #region Properties

    public int ID { get; set; }

    public int HardwareID { get; set; }

    public int SoftwareProductID { get; set; }

    public string BatchNo { get; set; }

    public int AuditUserID { get; set; }

    #endregion

    #region Constructor

    public AssignSoftwareDL()
    {
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
              //Adding Or Editing Software Product
              result = AddEditAssignSoftware(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteSoftware(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a SoftwareProduct - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditAssignSoftware(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "AssignSoftware_MERGE";//spAddEditAssignSoftware
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "HardwareID", DbType.Int32, HardwareID);
        db.AddInParameter(dbCommand, "SoftwareID", DbType.Int32, SoftwareProductID);
        db.AddInParameter(dbCommand, "BatchNumber", DbType.String, BatchNo);
        db.AddInParameter(dbCommand, "AuditUserID", DbType.Int32, AuditUserID);
        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int32, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        SoftwareProductID = returnValue;

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
            return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Software Product Name");
          else
            return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Software Product Name");
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
        ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "AddEditSoftware", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// To Delete a SoftwareProduct
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    public TransactionResult DeleteSoftware(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteAssignSoftware");
        db.AddInParameter(dbCommand, "SoftwareID", DbType.Int32, SoftwareProductID);
        db.AddInParameter(dbCommand, "HardwareID", DbType.Int32, HardwareID);
        db.AddInParameter(dbCommand, "AuditUserID", DbType.Int32, AuditUserID);
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
        ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "DeleteSoftware", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// Get Software Product List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetAssignSoftwareList(int HardwareID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetAssignSoftwarelist";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "HardwareID", DbType.Int32, HardwareID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "GetAssignSoftwareList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// CheckAssignedSoftware
    /// </summary>
    /// <param name="HardwareID"></param>
    /// <param name="SoftwareID"></param>
    /// <returns></returns>
    public DataSet CheckAssignedSoftware(int HardwareID, int SoftwareID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spCheckAssignSoftware";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "HardwareID", DbType.Int32, HardwareID);
        db.AddInParameter(dbCommand, "SoftwareID", DbType.Int32, SoftwareID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "AssignSoftwareDL.cs", "CheckAssignedSoftware", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    #endregion
  }
}
   