using System;
using System.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class ClientDL : CommonForAllDL
  {
    #region Properties

    public int ClientID { get; set; }

    public string ClientName { get; set; }

    public string ClientDescription { get; set; }

    public MailingAddressDL AddressInfo { get; set; }

    public string Contact1Name { get; set; }

    public string Contact2Name { get; set; }

    public string Contact1Phone { get; set; }

    public string Contact2Phone { get; set; }

    public string Contact1Email { get; set; }

    public string Contact2Email { get; set; }

    public string Contact1InstantMessenger { get; set; }

    public string Contact2InstantMessenger { get; set; }

    public string WebSite { get; set; }

    public string IsValid { get; set; }

    #endregion

    #region Constructors

    public ClientDL()
    {
      AddressInfo = new MailingAddressDL();
    }

    public ClientDL(int clientID, bool getAllProperties,string isValid)
    {
      ClientID = clientID;
      AddressInfo = new MailingAddressDL();
      IsValid = isValid;
      if (getAllProperties)
      {
        GetClient();
      }
    }

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
              //Adding Or Editing Client
              result = AddEditClient(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteClient(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Gets the list of Clients that match the specified search text (in any of the selected fields) - As requested and standard, new procedure name created on 19/Jun/2014.
    /// </summary>
    /// <param name="clientID"></param>
    /// <param name="searchText"></param>
    /// <returns>DataSet Containing the List of Companies</returns>
    public DataSet GetClientDetails(int clientID, string searchText,string isValid)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("Client_SELECT");//spGetClient
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "ClientID", DbType.Int32, clientID);
        db.AddInParameter(dbCommand, "SearchText", DbType.String, searchText);
        db.AddInParameter(dbCommand, "IsValid", DbType.String, isValid);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "GetClientDetails", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Get Client Getting in Dropdown List Control - As requested and standard, new procedure name created on 19/Jun/2014.
    /// </summary>
    /// <param name="ACEConnection">Instance of the ACEConnection Class</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetClientList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "ClientList_SELECT";//spGetClientList
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "GetClientList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetEmployeeXClientListByEmployeeID
    /// </summary>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public DataSet GetEmployeeXClientListByEmployeeID(int employeeID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeXClientByEmployeeID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, employeeID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "GetEmployeeXClientListByEmployeeID", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// Gets the Client Details based on the ClientID - As requested and standard, new procedure name created on 19/Jun/2014.
    /// </summary>
    private void GetClient()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "Client_SELECT";//spGetClient
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "ClientID", DbType.Int32, ClientID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Client Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          ClientName = Common.CheckNull(dRow["ClientName"]);
          ClientDescription = Common.CheckNull(dRow["ClientDescription"]);
          AddressInfo.Address1 = Common.CheckNull(dRow["Address1"]);
          AddressInfo.Address2 = Common.CheckNull(dRow["Address2"]);
          AddressInfo.CityID = Common.CheckIntNull(dRow["CityID"]);
          AddressInfo.CityName = Common.CheckNull(dRow["CityName"]);
          AddressInfo.StateID = Common.CheckIntNull(dRow["StateID"]);
          AddressInfo.StateName = Common.CheckNull(dRow["StateName"]);
          AddressInfo.CountryID = Common.CheckIntNull(dRow["CountryID"]);
          AddressInfo.CountryName = Common.CheckNull(dRow["CountryName"]);
          AddressInfo.PostalCode = Common.CheckNull(dRow["PostalCode"]);
          Contact1Name = Common.CheckNull(dRow["Contact1Name"]);
          Contact2Name = Common.CheckNull(dRow["Contact2Name"]);
          Contact1Phone = Common.CheckNull(dRow["Contact1Phone"]);
          Contact2Phone = Common.CheckNull(dRow["Contact2Phone"]);
          Contact1Email = Common.CheckNull(dRow["Contact1Email"]);
          Contact2Email = Common.CheckNull(dRow["Contact2Email"]);
          Contact2InstantMessenger = Common.CheckNull(dRow["Contact1InstantMessenger"]);
          Contact2InstantMessenger = Common.CheckNull(dRow["Contact2InstantMessenger"]);
          WebSite = Common.CheckNull(dRow["WebSite"]);
          IsValid = Common.CheckNull(dRow["IsValid"]);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "ClientDL.cs", "GetClient", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// To Add / Edit a Client - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditClient(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "Client_MERGE";//spAddEditClient
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "ClientID", DbType.Int32, ClientID);
      db.AddInParameter(dbCommand, "ClientName", DbType.String, ClientName);
      db.AddInParameter(dbCommand, "ClientDescription", DbType.String, ClientDescription);
      db.AddInParameter(dbCommand, "Address1", DbType.String, AddressInfo.Address1);
      db.AddInParameter(dbCommand, "Address2", DbType.String, AddressInfo.Address2);
      if (AddressInfo.CountryID != 0)
        db.AddInParameter(dbCommand, "CountryID", DbType.Int32, AddressInfo.CountryID);
      if (AddressInfo.StateID != 0)
        db.AddInParameter(dbCommand, "StateID", DbType.Int32, AddressInfo.StateID);
      if (AddressInfo.CityID != 0)
        db.AddInParameter(dbCommand, "CityID", DbType.Int32, AddressInfo.CityID);
      db.AddInParameter(dbCommand, "PostalCode", DbType.String, AddressInfo.PostalCode);
      db.AddInParameter(dbCommand, "Contact1Name", DbType.String, Contact1Name);
      db.AddInParameter(dbCommand, "Contact2Name", DbType.String, Contact2Name);
      db.AddInParameter(dbCommand, "Contact1Phone", DbType.String, Contact1Phone);
      db.AddInParameter(dbCommand, "Contact2Phone", DbType.String, Contact2Phone);
      db.AddInParameter(dbCommand, "Contact1Email", DbType.String, Contact1Email);
      db.AddInParameter(dbCommand, "Contact2Email", DbType.String, Contact2Email);
      db.AddInParameter(dbCommand, "Contact1InstantMessenger", DbType.String, Contact1InstantMessenger);
      db.AddInParameter(dbCommand, "Contact2InstantMessenger", DbType.String, Contact2InstantMessenger);
      db.AddInParameter(dbCommand, "WebSite", DbType.String, WebSite);
      db.AddInParameter(dbCommand, "IsValid", DbType.String, IsValid);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      ClientID = returnValue;

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      if (returnValue == -99)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Client Name");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Client Name");
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
    /// To Delete a Client - As requested and standard, new procedure name created on 18/Jun/2014.
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteClient(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("Client_MERGE");//spDeleteClient
      db.AddInParameter(dbCommand, "ClientID", DbType.Int32, ClientID);

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
