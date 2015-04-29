using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class CompanyDL : CommonForAllDL
  {
    #region Properties

    public int CompanyID { get; set; }

    public string CompanyName { get; set; }

    public string CompanyShortCode { get; set; }

    public MailingAddressDL CompanyAddress { get; set; }

    public string Phone1 { get; set; }

    public string Phone2 { get; set; }

    public string Fax { get; set; }

    public string WebSite { get; set; }

    public string Comments { get; set; }

    public int AuditUserID { get; set; }

    public Nullable<DateTime> AuditDate { get; set; }

    public List<DepartmentDL> CompanyDepartments { get; set; }

    public string CompanyEstCode { get; set; }

    public string CompanyEstExtn { get; set; }

    #endregion

    #region Contructors

    public CompanyDL()
    {
      CompanyAddress = new MailingAddressDL();
      CompanyDepartments = new List<DepartmentDL>();
    }

    public CompanyDL(int companyID, bool getAllProperties)
    {
      CompanyID = companyID;
      CompanyAddress = new MailingAddressDL();
      CompanyDepartments = new List<DepartmentDL>();
      if (getAllProperties)
      {
        GetCompany();
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
              //Adding Or Editing Company
              result = AddEditCompany(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteCompany(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Gets the list of Companies that match the specified search text (in any of the selected fields)
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="searchText"></param>
    /// <returns>DataSet Containing the List of Companies</returns>
    public DataSet GetCompanyDetails(int companyID, string searchText)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetCompany");//spGetCompany
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "SearchText", DbType.String, searchText);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "GetCompanyDetails", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the list of Companies
    /// </summary>
    /// <returns>DataSet Containing the List of Companies</returns>
    public DataSet GetCompanyList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetCompanyList");//spGetCompanyList - Added standard sp name on 20/06/2014
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "GetCompanyList", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the list of Companies By UserID
    /// </summary>
    /// <returns>DataSet Containing the List of Companies</returns>
    public DataSet GetCompanyListByUserID(int userID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetCompanyListByUserID");//spGetCompanyListByUserID - Added standard sp name on 20/06/2014
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, userID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "GetCompanyListByUserID", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the list of Companies not in CompanyXUsers 
    /// </summary>
    /// <param name="userID">For the user</param>
    /// <returns>DataSet Containing the List of Companies</returns>
    public DataSet GetCompanyListNotInCompanyXUsers(int userID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        DbCommand dbCommand = db.GetStoredProcCommand("spGetCompanyListNotInCompanyXUsers");//spGetCompanyListNotInCompanyXUsers - Added standard sp name on 20/06/2014
        dbCommand.Parameters.Clear();
        dbCommand.CommandTimeout = 300;
        db.AddInParameter(dbCommand, "UserID", DbType.Int32, userID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "spGetCompanyListNotInCompanyXUsers", ex.Message.ToString(), _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Gets the Company Details based on the CompanyID
    /// </summary>
    private void GetCompany()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetCompany";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Company Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];
          CompanyName = Common.CheckNull(dRow["CompanyName"]);
          CompanyEstCode = dRow["CompanyEst_Code"].ToString();
          CompanyEstExtn = dRow["CompanyEst_Extn"].ToString();

          CompanyShortCode = Common.CheckNull(dRow["CompanyShortCode"]);
          CompanyAddress.Address1 = Common.CheckNull(dRow["Address1"]);
          CompanyAddress.Address2 = Common.CheckNull(dRow["Address2"]);
          CompanyAddress.Address3 = Common.CheckNull(dRow["Address3"]);
          CompanyAddress.CityDescription = Common.CheckNull(dRow["City"]);
          CompanyAddress.StateID = Common.CheckIntNull(dRow["StateID"]);
          CompanyAddress.StateName = Common.CheckNull(dRow["StateName"]);
          CompanyAddress.CountryID = Common.CheckIntNull(dRow["CountryID"]);
          CompanyAddress.CountryName = Common.CheckNull(dRow["CountryName"]);
          CompanyAddress.PostalCode = Common.CheckNull(dRow["PostalCode"]);
          Phone1 = Common.CheckNull(dRow["Phone1"]);
          Phone2 = Common.CheckNull(dRow["Phone2"]);
          Fax = Common.CheckNull(dRow["Fax"]);
          WebSite = Common.CheckNull(dRow["WebSite"]);
          Comments = Common.CheckNull(dRow["Comments"]);
          AuditUserID = Common.CheckIntNull(dRow["AuditUserID"]);
          if (dRow["AuditDate"] != DBNull.Value)
            AuditDate = Convert.ToDateTime(dRow["AuditDate"]);
          else
            AuditDate = null;
        }

        // Load Company Departments
        DepartmentDL cDepartment;
        foreach (DataRow dRow in ds.Tables[1].Rows)
        {
          cDepartment = new DepartmentDL();
          cDepartment.DepartmentID = Common.CheckIntNull(dRow["DepartmentID"]);
          cDepartment.DepartmentDescription = Common.CheckNull(dRow["DepartmentDescription"]);
          cDepartment.CompanyID = Common.CheckIntNull(dRow["CompanyID"]);

          CompanyDepartments.Add(cDepartment);
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "CompanyDL.cs", "GetCompany", ex.Message, _myConnection);
        throw;
      }
    }

    /// <summary>
    /// To Add / Edit a Company
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult AddEditCompany(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditCompany";//spAddEditCompany
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
      db.AddInParameter(dbCommand, "CompanyName", DbType.String, CompanyName);
      db.AddInParameter(dbCommand, "Address1", DbType.String, CompanyAddress.Address1);
      db.AddInParameter(dbCommand, "Address2", DbType.String, CompanyAddress.Address2);
      db.AddInParameter(dbCommand, "Address3", DbType.String, CompanyAddress.Address3);
      db.AddInParameter(dbCommand, "City", DbType.String, CompanyAddress.CityDescription);
      if (CompanyAddress.CountryID != 0)
        db.AddInParameter(dbCommand, "CountryID", DbType.Int32, CompanyAddress.CountryID);
      if (CompanyAddress.StateID != 0)
        db.AddInParameter(dbCommand, "StateID", DbType.Int32, CompanyAddress.StateID);
      db.AddInParameter(dbCommand, "PostalCode", DbType.String, CompanyAddress.PostalCode);
      db.AddInParameter(dbCommand, "Phone1", DbType.String, Phone1);
      db.AddInParameter(dbCommand, "Phone2", DbType.String, Phone2);
      db.AddInParameter(dbCommand, "Fax", DbType.String, Fax);
      db.AddInParameter(dbCommand, "WebSite", DbType.String, WebSite);
      db.AddInParameter(dbCommand, "Comments", DbType.String, Comments);
      db.AddInParameter(dbCommand, "AuditUserID", DbType.String, AuditUserID);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      CompanyID = returnValue;

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
    /// To Delete a Company
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns>TransactionResult - Success / Failure</returns>
    private TransactionResult DeleteCompany(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteCompany");//spDeleteCompany
      db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
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
