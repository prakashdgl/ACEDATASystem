using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeAddressDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeAddressID { get; set; }

    public int EmployeeID { get; set; }

    public int AddressTypeID { get; set; }

    public string AddressTypeDescription { get; set; }

    public MailingAddressDL AddressInfo { get; set; }

    public string Phone { get; set; }

    #endregion

    #region Constructors

    public EmployeeAddressDL()
    {
      AddressInfo = new MailingAddressDL();
    }

    public EmployeeAddressDL(int employeeAddressID, bool getAllProperties)
    {
      EmployeeAddressID = employeeAddressID;
      AddressInfo = new MailingAddressDL();
      if (getAllProperties)
      {
        GetEmployeeAddress();
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
      try
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
                //Adding or Editing EmployeeAddress
                result = AddEditEmployeeAddress(db, transaction);
                if (result.Status == TransactionStatus.Failure)
                {
                  transaction.Rollback();
                  return result;
                }
                break;
              case ScreenMode.Edit:
                break;
              case ScreenMode.Delete:
                result = DeleteEmployeeAddress(db, transaction);
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
              ErrorLog.LogErrorMessageToDB("", "EmployeeAddressDL.cs", "Commit For Add", ex.Message, _myConnection);
              return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
            }
            if (ScreenMode == ScreenMode.Edit)
            {
              ErrorLog.LogErrorMessageToDB("", "EmployeeAddressDL.cs", "Commit For Edit", ex.Message, _myConnection);
              return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
            }
            if (ScreenMode == ScreenMode.Delete)
            {
              ErrorLog.LogErrorMessageToDB("", "EmployeeAddressDL.cs", "Commit For Delete", ex.Message, _myConnection);
              return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
            }
          }
        }
        return null;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeAddressDL.cs", "Commit", ex.Message, _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Gets the Employee Address Details based on the EmployeeAddressID
    /// </summary>
    private void GetEmployeeAddress()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeAddress123";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeAddressID", DbType.Int32, EmployeeAddressID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Employee Address Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          EmployeeAddressID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeAddressID"]));
          EmployeeID = Common.CheckIntNull(Convert.ToInt32(dRow["EmployeeID"]));
          AddressTypeID = Common.CheckIntNull(Convert.ToInt32(dRow["AddressTypeID"]));
          AddressTypeDescription = Common.CheckNull(Convert.ToString(dRow["AddressTypeDescription"]));
          AddressInfo.Address1 = Common.CheckNull(dRow["Address1"]);
          AddressInfo.Address2 = Common.CheckNull(dRow["Address2"]);
          AddressInfo.Address3 = Common.CheckNull(dRow["Address3"]);
          AddressInfo.CityDescription = Common.CheckNull(dRow["City"]);
          AddressInfo.StateID = Common.CheckIntNull(dRow["StateID"]);
          AddressInfo.StateName = Common.CheckNull(dRow["StateName"]);
          AddressInfo.CountryID = Common.CheckIntNull(dRow["CountryID"]);
          AddressInfo.CountryName = Common.CheckNull(dRow["CountryName"]);
          AddressInfo.PostalCode = Common.CheckNull(dRow["PostalCode"]);
          Phone = Common.CheckNull(Convert.ToString(dRow["Phone"]));
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeAddressDL.cs", "GetEmployeeAddress", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// To Add and edit the Address of an Employee
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    internal TransactionResult AddEditEmployeeAddress(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "EmployeeAddress_MERGE";//spAddEditEmployeeAddress
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "EmployeeAddressID", DbType.Int32, EmployeeAddressID);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "AddressTypeID", DbType.Int32, AddressTypeID);
      db.AddInParameter(dbCommand, "Address1", DbType.String, AddressInfo.Address1);
      db.AddInParameter(dbCommand, "Address2", DbType.String, AddressInfo.Address2);
      db.AddInParameter(dbCommand, "Address3", DbType.String, AddressInfo.Address3);
      db.AddInParameter(dbCommand, "CityDescription", DbType.String, AddressInfo.CityDescription);
      if (AddressInfo.CountryID != 0)
        db.AddInParameter(dbCommand, "CountryID", DbType.Int32, AddressInfo.CountryID);
      if (AddressInfo.StateID != 0)
        db.AddInParameter(dbCommand, "StateID", DbType.Int32, AddressInfo.StateID);
      if (AddressInfo.DistrictID != 0)
        db.AddInParameter(dbCommand, "DistrictID", DbType.Int32, AddressInfo.DistrictID);

      db.AddInParameter(dbCommand, "PostalCode", DbType.String, AddressInfo.PostalCode);
      db.AddInParameter(dbCommand, "Phone", DbType.String, Phone);

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
          return new TransactionResult(TransactionStatus.Success, "Successfully Added");
      }
    }

    /// <summary>
    /// To Delete the Address of an Employee
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    internal TransactionResult DeleteEmployeeAddress(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("EmployeeAddress_MERGE");//spDeleteEmployeeAddress
      db.AddInParameter(dbCommand, "EmployeeAddressID", DbType.Int32, EmployeeAddressID);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);
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
