using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class MonitorDL : CommonForAllDL
  {
    #region Properties

    public int MonitorID { get; set; }

    public int NoOfCopy { get; set; }

    public int MonitorMake { get; set; }

    public string MonitorCode { get; set; }

    public int MonitorSize { get; set; }

    public string Billno { get; set; }

    public Nullable<DateTime> BillDate { get; set; }

    public int AssignedTo { get; set; }

    public decimal value { get; set; }

    public string ProductSerialNo { get; set; }

    public string BarCodeID { get; set; }

    public string AcquiredYear { get; set; }

    public int Status { get; set; }

    public int AcquiredFor { get; set; }

    public int AcquiredBy { get; set; }

    public int Supplier { get; set; }

    public DateTime AcquiredDate { get; set; }

    public string Notes { get; set; }

    public Nullable<DateTime> WarrantyExpiry { get; set; }

    public int AuditUserId { get; set; }

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
              //Adding Or Editing Monitor
              result = AddEditMonitor(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteMonitor(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a Monitor
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditMonitor(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditMonitor";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "MonitorID", DbType.Int32, MonitorID);
        db.AddInParameter(dbCommand, "NoOfCopy", DbType.Int32, NoOfCopy);
        db.AddInParameter(dbCommand, "AcquiredYear", DbType.String, AcquiredYear);
        db.AddInParameter(dbCommand, "MonitorMake", DbType.Int32, MonitorMake);
        db.AddInParameter(dbCommand, "MonitorSize", DbType.Int32, MonitorSize);
        db.AddInParameter(dbCommand, "Supplier", DbType.Int32, Supplier);
        db.AddInParameter(dbCommand, "Status", DbType.Int32, Status);
        db.AddInParameter(dbCommand, "Billno", DbType.String, Billno);
        db.AddInParameter(dbCommand, "BillDate", DbType.String, BillDate);
        db.AddInParameter(dbCommand, "Value", DbType.String, value);
        db.AddInParameter(dbCommand, "ProductSerialNo", DbType.String, ProductSerialNo);
        //db.AddInParameter(dbCommand, "BarCodeID", DbType.String, BarCodeID);
        db.AddInParameter(dbCommand, "AdditionalNotes", DbType.String, Notes);
        db.AddInParameter(dbCommand, "WarrantyExpiry", DbType.Date, WarrantyExpiry);
        db.AddInParameter(dbCommand, "AcquiredFor", DbType.Int32, AcquiredFor);
        db.AddInParameter(dbCommand, "AcquiredBy", DbType.Int32, AcquiredBy);
        db.AddInParameter(dbCommand, "AuditUserID", DbType.Int32, AuditUserId);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        MonitorID = returnValue;

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
        ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "AddEditMonitor", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "AddEditMonitor Failed");
      }
    }

    /// <summary>
    /// To Delete a Monitor
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteMonitor(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteMonitor");
        db.AddInParameter(dbCommand, "MonitorID", DbType.Int32, MonitorID);
        db.AddInParameter(dbCommand, "AcquiredYear", DbType.String, AcquiredYear);
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
        ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "AddEditMonitor", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// GetMonitorList
    /// </summary>
    /// <returns></returns>
    public DataSet GetMonitorList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMonitorList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "GetMonitorList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetHMonitorList
    /// </summary>
    /// <param name="MonitorID"></param>
    public void GetHMonitorList(int MonitorID)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMonitorListByMonitorID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "MonitorID", DbType.Int32, MonitorID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        DateTime dtDefault = new DateTime(1900, 1, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          MonitorCode = dRow["MonitorCode"].cxToString();
          MonitorMake = dRow["MonitorMake"].cxToInt32();
          MonitorSize = dRow["MonitorSize"].cxToInt32();
          Billno = dRow["BillNo"].cxToString();
          BillDate = dRow["BillDate"].cxToDateTime(dtDefault);
          value = dRow["Value"].cxToDecimal();
          AcquiredBy = dRow["AcquiredBy"].cxToInt32();
          AcquiredFor = dRow["AcquiredFor"].cxToInt32();
          ProductSerialNo = dRow["ProductSerialNo"].cxToString();
          BarCodeID = dRow["BarcodeID"].cxToString();
          Status = dRow["Status"].cxToInt32();
          Notes = dRow["AdditionalNotes"].cxToString();
          WarrantyExpiry = dRow["WarrantyExpiry"].cxToDateTime(dtDefault);
          AuditUserId = dRow["AuditUserID"].cxToInt32();
          AcquiredYear = dRow["AcquiredYear"].cxToString();
          Supplier = dRow["Supplier"].cxToInt32();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MonitorDL.cs", "GetHMonitorList", ex.Message.ToString(), _myConnection);
      }
    }

    #endregion
  }
}
