using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class SoftwareProductDL : CommonForAllDL
  {
    #region Properties

    public int HardwareID { get; set; }
    public int SoftwareProductID { get; set; }
    public int SoftwareProductName { get; set; }
    public string SoftwareOEMID { get; set; }
    public string SoftwareVersion { get; set; }
    public string AdditionalDescription { get; set; }
    public int NoOfCopies { get; set; }
    public string LicenseID { get; set; }
    public int Licensetype { get; set; }
    public string LANNo { get; set; }
    public Nullable<DateTime> LicenseRenewalDate { get; set; }
    public int LicenseOwnedBy { get; set; }
    public int EngagedBy { get; set; }
    public Nullable<DateTime> AcquisitionDate { get; set; }
    public int Supplier { get; set; }
    public string BillNo { get; set; }
    public decimal Value { get; set; }
    public int AuditUserID { get; set; }
    public bool IsLiveProduct { get; set; }
    public string BatchNumber { get; set; }
    public int AssignedEmpCnt { get; set; }

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
              //Adding Or Editing Software Product
              result = AddEditSoftwareProduct(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteSoftwareProduct(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// To Add / Edit a SoftwareProduct
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditSoftwareProduct(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditSoftwareProduct";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "BatchNo", DbType.String, BatchNumber);
        db.AddInParameter(dbCommand, "SoftwareProductID", DbType.Int32, SoftwareProductID);
        db.AddInParameter(dbCommand, "SoftwareProductName", DbType.Int32, SoftwareProductName);
        db.AddInParameter(dbCommand, "OEMID", DbType.String, SoftwareOEMID);
        db.AddInParameter(dbCommand, "ReleaseNo", DbType.String, SoftwareVersion);
        db.AddInParameter(dbCommand, "AdditionalDescription", DbType.String, AdditionalDescription);
        db.AddInParameter(dbCommand, "NoOfCopies", DbType.Int32, NoOfCopies);
        db.AddInParameter(dbCommand, "LicenseID", DbType.String, LicenseID);
        db.AddInParameter(dbCommand, "LANNo", DbType.String, LANNo);
        db.AddInParameter(dbCommand, "LicenseType", DbType.Int32, Licensetype);
        db.AddInParameter(dbCommand, "LicenseRenewalDate", DbType.Date, LicenseRenewalDate);
        db.AddInParameter(dbCommand, "LicenseOwnedBy", DbType.Int32, LicenseOwnedBy);
        db.AddInParameter(dbCommand, "EngagedBy", DbType.Int32, EngagedBy);
        db.AddInParameter(dbCommand, "AcquisitionDate", DbType.Date, AcquisitionDate);
        db.AddInParameter(dbCommand, "Supplier", DbType.Int32, Supplier);
        db.AddInParameter(dbCommand, "BillNo", DbType.String, BillNo);
        db.AddInParameter(dbCommand, "Value", DbType.Decimal, Value);
        db.AddInParameter(dbCommand, "AuditUserID", DbType.Int32, AuditUserID);
        db.AddInParameter(dbCommand, "IsLiveProduct", DbType.Int32, IsLiveProduct);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
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
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "AddEditSoftwareProduct", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// To Delete a SoftwareProduct
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteSoftwareProduct(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteSoftwareProduct");
        db.AddInParameter(dbCommand, "SoftwareProductID", DbType.Int32, SoftwareProductID);
        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
        else if (returnValue == 1)
          return new TransactionResult(TransactionStatus.Failure, "Unable to delete!This software is configured in Hardware Machine");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "DeleteSoftwareProduct", ex.Message.ToString(), _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleted");
      }
    }

    /// <summary>
    /// Get Software Product List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetSoftwareProductList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSoftwareProduct";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "SoftwareProductID", DbType.Int32, SoftwareProductID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSoftwareProductList", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// Get Software Product List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetSoftwareProductDetail()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSoftwareProductDetail";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "SoftwareProductID", DbType.Int32, SoftwareProductID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSoftwareProductDetail", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// GetSoftwareProductByProductID
    /// </summary>
    /// <param name="employeeID">employeeID</param> 
    public void GetSoftwareProductByProductID(int productID)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSoftwareProductList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProdcutID", DbType.Int32, productID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        DateTime dtDefault = new DateTime(1900, 1, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          SoftwareProductID = Common.CheckIntNull(Convert.ToInt32(dRow["ProductID"]));
          BatchNumber = dRow["BatchNumber"].cxToString();
          SoftwareProductName = Common.CheckIntNull(Convert.ToInt32(dRow["ProductName"]));
          SoftwareOEMID = dRow["OEMID"].cxToString();
          SoftwareVersion = dRow["ReleaseNo"].cxToString();
          AdditionalDescription = dRow["AdditionalDescription"].cxToString();
          NoOfCopies = dRow["NoOfCopies"].cxToInt32();
          LicenseID = dRow["LicenseID"].cxToString();
          LANNo = dRow["LANNo"].cxToString();
          LicenseRenewalDate = dRow["LicenseRenewalDate"].cxToDateTime(dtDefault);
          LicenseOwnedBy = dRow["LicenseOwnedBy"].cxToInt32();
          AcquisitionDate = dRow["AcquisitionDate"].cxToDateTime(dtDefault);
          Supplier = dRow["Supplier"].cxToInt32();
          BillNo = dRow["BillNo"].cxToString();
          Value = dRow["Value"].cxToInt32();
          Licensetype = dRow["LicenseType"].cxToInt32();
          SoftwareVersion = dRow["ReleaseNo"].cxToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSoftwareProductByProductID", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// Get Software Product List
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetSoftwareProductName(int _HardwareID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSoftwareProductName";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "HardwareID", DbType.Int32, _HardwareID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSoftwareProductName", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetSoftwareOEM
    /// </summary>
    /// <returns></returns>
    public DataSet GetSoftwareOEM()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSoftwareOEM";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSoftwareOEM", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// GetSupplier
    /// </summary>
    /// <returns></returns>
    public DataSet GetSupplier()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSupplier";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetSupplier", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// GetBatchNumberList
    /// </summary>
    /// <returns></returns>
    public DataSet GetBatchNumberList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetBatchNoByProductID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProductID", DbType.Int32, SoftwareProductID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetBatchNumberList", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// GetAssignedEmployeeList
    /// </summary>
    /// <returns></returns>
    public DataSet GetAssignedEmployeeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetAssignedSoftwareList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProductID", DbType.Int32, SoftwareProductID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetAssignedEmployeeList", ex.Message.ToString(), _myConnection);
        throw ex;
      }
      return ds;
    }

    /// <summary>
    /// CheckSoftwareStatus
    /// </summary>
    /// <returns></returns>
    public DataSet CheckSoftwareStatus()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spCheckSoftwareStatus";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProductID", DbType.Int32, SoftwareProductID);
        ds = db.ExecuteDataSet(dbCommand);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          AssignedEmpCnt = dRow["AssignedCount"].cxToInt32();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "CheckSoftwareStatus", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetBatchNumber
    /// </summary>
    /// <returns></returns>
    public DataSet GetBatchNumber()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetBatchNo";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ProductID", DbType.Int32, SoftwareProductID);
        db.AddInParameter(dbCommand, "BillDate", DbType.DateTime, AcquisitionDate);
        db.AddInParameter(dbCommand, "IsLiveProduct", DbType.Int32, IsLiveProduct);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "SoftwareProductDL.cs", "GetBatchNumber", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }
    
    #endregion
  }
}
