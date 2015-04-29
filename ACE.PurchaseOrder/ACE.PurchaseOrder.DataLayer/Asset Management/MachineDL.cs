using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class MachineDL : CommonForAllDL
  {
    #region Public Properties

    public int MachineID { get; set; }
    public int UserID { get; set; }
    public int Type { get; set; }
    public int Make { get; set; }
    public int Model { get; set; }
    public int Processor { get; set; }
    public Nullable<int> ProcessorUpgradeableTo { get; set; }
    public int Memory1 { get; set; }
    public Nullable<int> Memory1UpgradeableTo { get; set; }
    public Nullable<int> Memory2 { get; set; }
    public Nullable<int> Memory2UpgradeableTo { get; set; }
    public Nullable<int> Memory3 { get; set; }
    public Nullable<int> Memory3UpgradeableTo { get; set; }
    public Nullable<int> Memory4 { get; set; }
    public Nullable<int> Memory4UpgradeableTo { get; set; }
    public Nullable<int> EmployeesClient { get; set; }
    public int HDD1 { get; set; }
    public Nullable<int> HDD1UpgradeableTo { get; set; }
    public Nullable<int> HDD2 { get; set; }
    public Nullable<int> HDD2UpgradeableTo { get; set; }
    public int Supplier { get; set; }
    public string BillNo { get; set; }
    public Nullable<DateTime> BillDate { get; set; }
    public string Value { get; set; }
    public int AcquiredFor { get; set; }
    public string ProductSerialNo { get; set; }
    public string BarcodeID { get; set; }
    public int Status { get; set; }
    public Nullable<int> AssignedTo { get; set; }
    public int AcquiredBy { get; set; }
    public string Notes { get; set; }
    public Nullable<DateTime> UnitAcquisitionDate { get; set; }
    public Nullable<int> UnitWarrantyPeriod { get; set; }
    public Nullable<DateTime> UnitWarrantyTill { get; set; }
    public Nullable<DateTime> ProcessorAcquisitionDate { get; set; }
    public Nullable<int> ProcessorWarrantyPeriod { get; set; }
    public Nullable<DateTime> ProcessorWarrantyTill { get; set; }
    public Nullable<DateTime> Memory1AcquisitionDate { get; set; }
    public Nullable<int> Memory1WarrantyPeriod { get; set; }
    public Nullable<DateTime> Memory1WarrantyTill { get; set; }
    public Nullable<DateTime> Memory2AcquisitionDate { get; set; }
    public Nullable<int> Memory2WarrantyPeriod { get; set; }
    public Nullable<DateTime> Memory2WarrantyTill { get; set; }
    public Nullable<DateTime> Memory3AcquisitionDate { get; set; }
    public Nullable<int> Memory3WarrantyPeriod { get; set; }
    public Nullable<DateTime> Memory3WarrantyTill { get; set; }
    public Nullable<DateTime> Memory4AcquisitionDate { get; set; }
    public Nullable<int> Memory4WarrantyPeriod { get; set; }
    public Nullable<DateTime> Memory4WarrantyTill { get; set; }
    public Nullable<DateTime> HDD1AcquisitionDate { get; set; }
    public Nullable<int> HDD1WarrantyPeriod { get; set; }
    public Nullable<DateTime> HDD1WarrantyTill { get; set; }
    public Nullable<DateTime> HDD2AcquisitionDate { get; set; }
    public Nullable<int> HDD2WarrantyPeriod { get; set; }
    public Nullable<DateTime> HDD2WarrantyTill { get; set; }
    public string MachineCode { get; set; }
    public string AcquiredYear { get; set; }

    public string ProcessorUpgradeableToBillNo { get; set; }
    public Nullable<DateTime> ProcessorUpgradeableToBillDate { get; set; }
    public Nullable<int> ProcessorUpgradeableToSupplier { get; set; }

    public string PrimaryMemroySlotOneUpgradeableToBillNo { get; set; }
    public string PrimaryMemroySlotTwoUpgradeableToBillNo { get; set; }
    public string PrimaryMemorySlotThreeUpgradeableToBillNo { get; set; }
    public string PrimaryMemorySlotFourUpgradeableToBillNo { get; set; }

    public Nullable<DateTime> PrimaryMemroySlotOneUpgradeableToBillDate { get; set; }
    public Nullable<DateTime> PrimaryMemroySlotTwoUpgradeableToBillDate { get; set; }
    public Nullable<DateTime> PrimaryMemorySlotThreeUpgradeableToBillDate { get; set; }
    public Nullable<DateTime> PrimaryMemorySlotFourUpgradeableToBillDate { get; set; }

    public Nullable<int> PrimaryMemroySlotOneUpgradeableToSupplier { get; set; }
    public Nullable<int> PrimaryMemroySlotTwoUpgradeableToSupplier { get; set; }
    public Nullable<int> PrimaryMemroySlotThreeUpgradeableToSupplier { get; set; }
    public Nullable<int> PrimaryMemroySlotFourUpgradeableToSupplier { get; set; }

    public string PrimaryHDDOneUpgradeableToBillNo { get; set; }
    public string PrimaryHDDTwoUpgradeableToBillNo { get; set; }

    public Nullable<DateTime> PrimaryHDDOneUpgradeableToBillDate { get; set; }
    public Nullable<DateTime> PrimaryHDDTwoUpgradeableToBillDate { get; set; }

    public string PrimaryHDDOneUpgradeableToSupplier { get; set; }
    public string PrimaryHDDTwoUpgradeableToSupplier { get; set; }

    public Nullable<DateTime> UnitWarrantyExpiry { get; set; }
    public Nullable<DateTime> ProcessorWarrantyExpiry { get; set; }
    public Nullable<DateTime> HDD1WarrantyExpiry { get; set; }
    public Nullable<DateTime> HDD2WarrantyExpiry { get; set; }
    public Nullable<DateTime> Memory1WarrantyExpiry { get; set; }
    public Nullable<DateTime> Memory2WarrantyExpiry { get; set; }
    public Nullable<DateTime> Memory3WarrantyExpiry { get; set; }
    public Nullable<DateTime> Memory4WarrantyExpiry { get; set; }

    public string SupplierName { get; set; }
    public string MachineTypeName { get; set; }
    public string MachineMakeName { get; set; }
    public string HDDName { get; set; }
    public string MemoryName { get; set; }
    public string ProcessorName { get; set; }

    #endregion

    #region Constructor

    public MachineDL()
    {
    }

    public MachineDL(int machineID, bool getAllProperties)
    {
      MachineID = machineID;

      if (getAllProperties)
      {
        GetHardware();
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
              //Adding Or Editing Computer
              result = AddEditComputer(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            //case ScreenMode.Edit:
            //  break;
            //case ScreenMode.Delete:
            //  result = DeleteMonitor(db, transaction);
            //  if (result.Status == TransactionStatus.Failure)
            //  {
            //    transaction.Rollback();
            //    return result;
            //  }
            //  break;
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
    /// To Add / Edit Computer
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditComputer(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "Computer_MERGE";//spAddEditComputer
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "MachineID", DbType.Int32, MachineID);
      db.AddInParameter(dbCommand, "MachineCode", DbType.String, MachineCode);
      db.AddInParameter(dbCommand, "AcquiredYear", DbType.String, AcquiredYear);
      db.AddInParameter(dbCommand, "MachineType", DbType.Int32, Type);
      db.AddInParameter(dbCommand, "MachineMake", DbType.Int32, Make);
      ///db.AddInParameter(dbCommand, "MachineModel", DbType.Int32, Model);
      db.AddInParameter(dbCommand, "MachineProcessor", DbType.Int32, Processor);
      db.AddInParameter(dbCommand, "MachineProcessorUpgradeableTo", DbType.Int32, ProcessorUpgradeableTo);
      db.AddInParameter(dbCommand, "Memory1", DbType.Int32, Memory1);
      db.AddInParameter(dbCommand, "Memory1UpgradeableTo", DbType.Int32, Memory1UpgradeableTo);
      db.AddInParameter(dbCommand, "Memory2", DbType.Int32, Memory2);
      db.AddInParameter(dbCommand, "Memory2UpgradeableTo", DbType.Int32, Memory2UpgradeableTo);
      db.AddInParameter(dbCommand, "Memory3", DbType.Int32, Memory3);
      db.AddInParameter(dbCommand, "Memory3UpgradeableTo", DbType.Int32, Memory3UpgradeableTo);
      db.AddInParameter(dbCommand, "Memory4", DbType.Int32, Memory4);
      db.AddInParameter(dbCommand, "Memory4UpgradeableTo", DbType.Int32, Memory4UpgradeableTo);
      db.AddInParameter(dbCommand, "HDD1", DbType.Int32, HDD1);
      db.AddInParameter(dbCommand, "HDD1UpgardeableTo", DbType.Int32, HDD1UpgradeableTo);
      db.AddInParameter(dbCommand, "HDD2", DbType.Int32, HDD2);
      db.AddInParameter(dbCommand, "HDD2UpgardeableTo", DbType.Int32, HDD2UpgradeableTo);
      db.AddInParameter(dbCommand, "Supplier", DbType.Int32, Supplier);
      db.AddInParameter(dbCommand, "BillNo", DbType.String, BillNo);
      db.AddInParameter(dbCommand, "BillDate", DbType.Date, BillDate);
      db.AddInParameter(dbCommand, "Value", DbType.String, Value);
      db.AddInParameter(dbCommand, "AcquiredBy", DbType.Int32, AcquiredBy);
      db.AddInParameter(dbCommand, "AcquiredFor", DbType.Int32, AcquiredFor);
      db.AddInParameter(dbCommand, "ProductSerialNo", DbType.String, ProductSerialNo);
      db.AddInParameter(dbCommand, "BarcodeID", DbType.String, BarcodeID);
      db.AddInParameter(dbCommand, "Status", DbType.Int32, Status);
      db.AddInParameter(dbCommand, "AssignedTo", DbType.Int32, AssignedTo);
      db.AddInParameter(dbCommand, "EmployeesClient", DbType.Int32, EmployeesClient);
      db.AddInParameter(dbCommand, "AdditionalNotes", DbType.String, Notes);
      db.AddInParameter(dbCommand, "UnitAcquisitionDate", DbType.Date, UnitAcquisitionDate);
      db.AddInParameter(dbCommand, "UnitWarrantyExpiry", DbType.Date, UnitWarrantyTill);
      db.AddInParameter(dbCommand, "ProcessorAcquisitionDate", DbType.Date, ProcessorAcquisitionDate);
      db.AddInParameter(dbCommand, "ProcessorWarrantyExpiry", DbType.Date, ProcessorWarrantyTill);
      db.AddInParameter(dbCommand, "Memory1AcquisitionDate", DbType.Date, Memory1AcquisitionDate);
      db.AddInParameter(dbCommand, "Memory1WarrantyExpiry", DbType.Date, Memory1WarrantyTill);
      db.AddInParameter(dbCommand, "Memory2AcquisitionDate", DbType.Date, Memory2AcquisitionDate);
      db.AddInParameter(dbCommand, "Memory2WarrantyExpiry", DbType.Date, Memory2WarrantyTill);
      db.AddInParameter(dbCommand, "Memory3AcquisitionDate", DbType.Date, Memory3AcquisitionDate);
      db.AddInParameter(dbCommand, "Memory3WarrantyExpiry", DbType.Date, Memory3WarrantyTill);
      db.AddInParameter(dbCommand, "Memory4AcquisitionDate", DbType.Date, Memory4AcquisitionDate);
      db.AddInParameter(dbCommand, "Memory4WarrantyExpiry", DbType.Date, Memory4WarrantyTill);
      db.AddInParameter(dbCommand, "HDD1AcquisitionDate", DbType.Date, HDD1AcquisitionDate);
      db.AddInParameter(dbCommand, "HDD1WarrantyExpiry", DbType.Date, HDD1WarrantyTill);
      db.AddInParameter(dbCommand, "HDD2AcquisitionDate", DbType.Date, HDD2AcquisitionDate);
      db.AddInParameter(dbCommand, "HDD2WarrantyExpiry", DbType.Date, HDD2WarrantyTill);
      db.AddInParameter(dbCommand, "AuditUserId", DbType.Int32, UserID);

      db.AddInParameter(dbCommand, "ProcessorUpgradeableToBillNo", DbType.String, ProcessorUpgradeableToBillNo);
      db.AddInParameter(dbCommand, "ProcessorUpgradeableToBillDate", DbType.Date, ProcessorUpgradeableToBillDate);
      db.AddInParameter(dbCommand, "ProcessorUpgradeableToSupplier", DbType.Int32, ProcessorUpgradeableToSupplier);

      db.AddInParameter(dbCommand, "PrimaryMemroySlotOneUpgradeableToBillNo", DbType.String, PrimaryMemroySlotOneUpgradeableToBillNo);
      db.AddInParameter(dbCommand, "PrimaryMemroySlotTwoUpgradeableToBillNo", DbType.String, PrimaryMemroySlotTwoUpgradeableToBillNo);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotThreeUpgradeableToBillNo", DbType.String, PrimaryMemorySlotThreeUpgradeableToBillNo);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotFourUpgradeableToBillNo", DbType.String, PrimaryMemorySlotFourUpgradeableToBillNo);

      db.AddInParameter(dbCommand, "PrimaryMemroySlotOneUpgradeableToBillDate", DbType.Date, PrimaryMemroySlotOneUpgradeableToBillDate);
      db.AddInParameter(dbCommand, "PrimaryMemroySlotTwoUpgradeableToBillDate", DbType.Date, PrimaryMemroySlotTwoUpgradeableToBillDate);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotThreeUpgradeableToBillDate", DbType.Date, PrimaryMemorySlotThreeUpgradeableToBillDate);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotFourUpgradeableToBillDate", DbType.Date, PrimaryMemorySlotFourUpgradeableToBillDate);

      db.AddInParameter(dbCommand, "PrimaryMemroySlotOneUpgradeableToSupplier", DbType.Int32, PrimaryMemroySlotOneUpgradeableToSupplier);
      db.AddInParameter(dbCommand, "PrimaryMemroySlotTwoUpgradeableToSupplier", DbType.Int32, PrimaryMemroySlotTwoUpgradeableToSupplier);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotThreeUpgradeableToSupplier", DbType.Int32, PrimaryMemroySlotThreeUpgradeableToSupplier);
      db.AddInParameter(dbCommand, "PrimaryMemorySlotFourUpgradeableToSupplier", DbType.Int32, PrimaryMemroySlotFourUpgradeableToSupplier);

      db.AddInParameter(dbCommand, "PrimaryHDDOneUpgradeableToBillNo", DbType.String, PrimaryHDDOneUpgradeableToBillNo);
      db.AddInParameter(dbCommand, "PrimaryHDDTwoUpgradeableToBillNo", DbType.String, PrimaryHDDTwoUpgradeableToBillNo);

      db.AddInParameter(dbCommand, "PrimaryHDDOneUpgradeableToBillDate", DbType.Date, PrimaryHDDOneUpgradeableToBillDate);
      db.AddInParameter(dbCommand, "PrimaryHDDTwoUpgradeableToBillDate", DbType.Date, PrimaryHDDTwoUpgradeableToBillDate);

      db.AddInParameter(dbCommand, "PrimaryHDDOneUpgradeableToSupplier", DbType.Int32, PrimaryHDDOneUpgradeableToSupplier);
      db.AddInParameter(dbCommand, "PrimaryHDDTwoUpgradeableToSupplier", DbType.Int32, PrimaryHDDTwoUpgradeableToSupplier);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      MachineID = returnValue;

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
    /// GetMachineList - Added standard sp name on 20/06/2014
    /// </summary>
    /// <returns></returns>
    public DataSet GetMachineList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "Computers_SELECT";//spGetComputers
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "Status", DbType.Int32, Status);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetTypeList
    /// </summary>
    /// <returns></returns>
    public DataSet GetTypeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineType";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetMakeList
    /// </summary>
    /// <returns></returns>
    public DataSet GetMakeList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineMake";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetModelList
    /// </summary>
    /// <returns></returns>
    public DataSet GetModelList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineModel";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetProcessorList
    /// </summary>
    /// <returns></returns>
    public DataSet GetProcessorList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineProcessor";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetMemoryList
    /// </summary>
    /// <returns></returns>
    public DataSet GetMemoryList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineMemory";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetHDDList
    /// </summary>
    /// <returns></returns>
    public DataSet GetHDDList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMachineHDD";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetMachineType", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// GetSupplierList
    /// </summary>
    /// <returns></returns>
    public DataSet GetSupplierList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetSuppliers";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetSupplierList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the Hardware Details based on the MachineID
    /// </summary>
    private void GetHardware()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetHaradwareList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "MachineID", DbType.Int32, MachineID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        DateTime dtDefault = new DateTime(1900, 1, 1);
        // Load Company Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          AcquiredYear = dRow["AcquiredYear"].cxToString();
          Type = Convert.ToInt32(Common.CheckNull(dRow["MachineType"]));
          Make = Common.CheckIntNull(dRow["MachineMake"]);
          Processor = Convert.ToInt32(dRow["MachineProcessor"]);
          ProcessorUpgradeableTo = Common.CheckIntNull(dRow["MachineProcessorUpgradeableTo"]);
          Memory1 = Common.CheckIntNull(dRow["Memory1"]);
          Memory1UpgradeableTo = Common.CheckIntNull(dRow["Memory1UpgradeableTo"]);
          HDD1 = Common.CheckIntNull(dRow["HDD1"]);
          HDD1UpgradeableTo = Common.CheckIntNull(dRow["HDD1UpgradeableTo"]);
          Supplier = Common.CheckIntNull(dRow["Supplier"]);
          BillNo = dRow["BillNo"].cxToString();
          BillDate = dRow["BillDate"].cxToDateTime(dtDefault);
          Value = dRow["Value"].cxToString();
          AcquiredBy = Common.CheckIntNull(dRow["AcquiredBy"]);
          AcquiredFor = Common.CheckIntNull(dRow["AcquiredFor"]);
          ProductSerialNo = dRow["ProductSerialNo"].cxToString();
          BarcodeID = (dRow["BarcodeID"]).ToString();
          Status = Common.CheckIntNull(dRow["Status"]);
          AssignedTo = Common.CheckIntNull(dRow["AssignedTo"]);
          MachineCode = dRow["MachineCode"].ToString();
          EmployeesClient = Common.CheckIntNull(dRow["EmployeeClient"]);
          Notes = dRow["AdditionalNotes"].cxToString();
          UnitAcquisitionDate = dRow["UnitAcquisitionDate"].cxToDateTime(dtDefault);
          UnitWarrantyExpiry = dRow["UnitWarrantyExpiry"].cxToDateTime(dtDefault);
          ProcessorAcquisitionDate = dRow["ProcessorAcquisitionDate"].cxToDateTime(dtDefault);
          ProcessorWarrantyExpiry = dRow["ProcessorWarrantyExpiry"].cxToDateTime(dtDefault);
          Memory1AcquisitionDate = dRow["Memory1AcquisitionDate"].cxToDateTime(dtDefault);
          Memory1WarrantyExpiry = dRow["Memory1WarrantyExpiry"].cxToDateTime(dtDefault);
          Memory2AcquisitionDate = dRow["Memory2AcquisitionDate"].cxToDateTime(dtDefault);
          Memory2WarrantyExpiry = dRow["Memory2WarrantyExpiry"].cxToDateTime(dtDefault);
          Memory3AcquisitionDate = dRow["Memory3AcquisitionDate"].cxToDateTime(dtDefault);
          Memory3WarrantyExpiry = dRow["Memory3WarrantyExpiry"].cxToDateTime(dtDefault);
          Memory4AcquisitionDate = dRow["Memory4AcquisitionDate"].cxToDateTime(dtDefault);
          Memory4WarrantyExpiry = dRow["Memory4WarrantyExpiry"].cxToDateTime(dtDefault);
          HDD1AcquisitionDate = dRow["HDD1AcquisitionDate"].cxToDateTime(dtDefault);
          HDD1WarrantyExpiry = dRow["HDD1WarrantyExpiry"].cxToDateTime(dtDefault);
          HDD2AcquisitionDate = dRow["HDD2AcquisitionDate"].cxToDateTime(dtDefault);
          HDD2WarrantyExpiry = dRow["HDD2WarrantyExpiry"].cxToDateTime(dtDefault);
          Memory2 = dRow["Memory2"].cxToInt32(0);
          Memory3 = dRow["Memory3"].cxToInt32(0);
          Memory4 = dRow["Memory4"].cxToInt32(0);
          Memory2UpgradeableTo = dRow["Memory2UpgradeableTo"].cxToInt32(0);
          Memory3UpgradeableTo = dRow["Memory3UpgradeableTo"].cxToInt32(0);
          Memory4UpgradeableTo = dRow["Memory4UpgradeableTo"].cxToInt32(0);
          HDD2 = dRow["HDD2"].cxToInt32(0);
          HDD2UpgradeableTo = dRow["HDD2UpgradeableTo"].cxToInt32();
          ProcessorUpgradeableToBillNo = dRow["ProcessorUpgradeableToBillNo"].cxToString("");
          ProcessorUpgradeableToBillDate = dRow["ProcessorUpgradeableToBillDate"].cxToDateTime(dtDefault);
          ProcessorUpgradeableToSupplier = dRow["ProcessorUpgradeableToSupplier"].cxToInt32(0);
          PrimaryMemroySlotOneUpgradeableToBillNo = dRow["PrimaryMemroySlotOneUpgradeableToBillNo"].cxToString("");
          PrimaryMemroySlotTwoUpgradeableToBillNo = dRow["PrimaryMemroySlotTwoUpgradeableToBillNo"].cxToString("");
          PrimaryMemorySlotThreeUpgradeableToBillNo = dRow["PrimaryMemorySlotThreeUpgradeableToBillNo"].cxToString("");
          PrimaryMemorySlotFourUpgradeableToBillNo = dRow["PrimaryMemorySlotFourUpgradeableToBillNo"].cxToString("");
          PrimaryMemroySlotOneUpgradeableToBillDate = dRow["PrimaryMemroySlotOneUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryMemroySlotTwoUpgradeableToBillDate = dRow["PrimaryMemroySlotTwoUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryMemorySlotThreeUpgradeableToBillDate = dRow["PrimaryMemorySlotThreeUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryMemorySlotFourUpgradeableToBillDate = dRow["PrimaryMemorySlotFourUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryMemroySlotOneUpgradeableToSupplier = dRow["PrimaryMemroySlotOneUpgradeableToSupplier"].cxToInt32(0);
          PrimaryMemroySlotTwoUpgradeableToSupplier = dRow["PrimaryMemroySlotTwoUpgradeableToSupplier"].cxToInt32(0);
          PrimaryMemroySlotThreeUpgradeableToSupplier = dRow["PrimaryMemorySlotThreeUpgradeableToSupplier"].cxToInt32(0);
          PrimaryMemroySlotFourUpgradeableToSupplier = dRow["PrimaryMemorySlotFourUpgradeableToSupplier"].cxToInt32(0);
          PrimaryHDDOneUpgradeableToBillNo = dRow["PrimaryHDDOneUpgradeableToBillNo"].cxToString("");
          PrimaryHDDTwoUpgradeableToBillNo = dRow["PrimaryHDDTwoUpgradeableToBillNo"].cxToString("");
          PrimaryHDDOneUpgradeableToBillDate = dRow["PrimaryHDDOneUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryHDDTwoUpgradeableToBillDate = dRow["PrimaryHDDTwoUpgradeableToBillDate"].cxToDateTime(dtDefault);
          PrimaryHDDOneUpgradeableToSupplier = dRow["PrimaryHDDOneUpgradeableToSupplier"].cxToString("");
          PrimaryHDDTwoUpgradeableToSupplier = dRow["PrimaryHDDTwoUpgradeableToSupplier"].cxToString("");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetHardware", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// Gets the Hardware Details based on the MachineID
    /// </summary>
    public void GetHardwareView(int MachineID)
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetHaradwareView";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "MachineID", DbType.Int32, MachineID);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        DateTime dtDefault = new DateTime(1900, 1, 1);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          SupplierName = dRow["SupplierName"].cxToString();
          BillNo = dRow["BillNo"].cxToString();
          BillDate = dRow["BillDate"].cxToDateTime(dtDefault);
          Value = (Convert.ToDecimal(dRow["Value"])).cxToString();
          MachineCode = dRow["MachineCode"].ToString();
          MachineTypeName = dRow["MachineTypeName"].cxToString();
          MachineMakeName = dRow["MachineMakeName"].cxToString();
          MemoryName = dRow["Memory"].cxToString();
          HDDName = dRow["HDDName"].cxToString();
          ProcessorName = dRow["ProcessorName"].cxToString();
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "MachineDL.cs", "GetHardware", ex.Message.ToString(), _myConnection);
      }
    }
    #endregion
  }
}
