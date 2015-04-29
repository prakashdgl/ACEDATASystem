using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class PolicyDL : CommonForAllDL
  {
    #region Properties

    public int PolicyID { get; set; }

    public int CompanyID { get; set; }

    public int PolicyCategoryID { get; set; }

    public string PolicyCategoryDescription { get; set; }

    public string PolicyNumber { get; set; }

    public string PolicyNoToEdit { get; set; }

    public string PolicyName { get; set; }

    public DateTime EffectiveDate { get; set; }

    public byte[] PolicyBinaryData { get; set; }

    public string PolicyFileName { get; set; }

    public bool IsPolicyFileChanged { get; set; }

    public new int AddEditOption { get; set; }

    public DateTime FinalRevisions { get; set; }

    #endregion

    #region Constructors

    public PolicyDL()
    {
    }

    public PolicyDL(int policyID, bool getAllProperties)
    {
      PolicyID = policyID;
      if (getAllProperties)
      {
        GetPolicy();
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
              //Adding Or Editing Policy
              result = AddEditPolicy(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Delete:
              result = DeletePolicy(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// GetMaxPolicyID
    /// </summary>
    /// <returns></returns>
    public DataTable GetMaxPolicyID()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetMaxPolicyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetMaxPolicyID", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      DataTable dt = ds.Tables[0];
      return dt;
    }

    /// <summary>
    /// Get Policy List By CompanyID
    /// </summary>
    /// <param name="companyID">The Policies of the specified Company ID</param>
    /// <param name="policyCategoryID">The PolicyCategoryID (if 0 - get all status, else - the specified status)</param>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetPolicyListByCompanyID(int companyID, int policyCategoryID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicyListByCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "PolicyCategoryID", DbType.Int32, policyCategoryID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetPolicyListByCompanyID", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetPolicySet
    /// </summary>
    /// <returns></returns>
    public DataTable GetPolicySet()
    {
      DataTable dt = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicy";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, PolicyNumber);
        DataSet ds = db.ExecuteDataSet(dbCommand);
        dt = ds.Tables[0];
        return dt;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetPolicySet", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return dt;
      }
    }

    /// <summary>
    /// GetPolicy
    /// </summary>
    /// <param name="PolicyNumber"></param>
    /// <param name="PageNo"></param>
    /// <returns></returns>
    public DataTable GetPolicy(string PolicyNumber, int PageNo)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicy";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, PolicyNumber);
        db.AddInParameter(dbCommand, "PageNo", DbType.Int32, PageNo);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetPolicy(string PolicyNumber, int PageNo)", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds.Tables[0];
    }

    /// <summary>
    /// Get Policy List
    /// </summary>                        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetPolicyList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicyList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetPolicyList", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
      return ds;
    }

    /// <summary>
    /// GetPolicy
    /// </summary>
    private void GetPolicy()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetPolicy";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "PolicyID", DbType.Int32, PolicyID);
        DataSet ds = db.ExecuteDataSet(dbCommand);

        // Load Policy Info
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dRow = ds.Tables[0].Rows[0];

          PolicyID = Common.CheckIntNull(Convert.ToInt32(dRow["PolicyID"]));
          if (dRow["CompanyID"] != DBNull.Value)
            CompanyID = Convert.ToInt32(dRow["CompanyID"]);

          PolicyCategoryID = Common.CheckIntNull(Convert.ToInt32(dRow["PolicyCategoryID"]));
          PolicyCategoryDescription = Common.CheckNull(Convert.ToString(dRow["PolicyCategoryDescription"]));

          PolicyNumber = Common.CheckNull(Convert.ToString(dRow["PolicyNumber"]));
          PolicyName = Common.CheckNull(Convert.ToString(dRow["PolicyName"]));

          if (dRow["EffectiveDate"] != DBNull.Value)
            EffectiveDate = Convert.ToDateTime(dRow["EffectiveDate"]);

          PolicyBinaryData = (byte[])dRow["PolicyBinaryData"];
          PolicyFileName = Common.CheckNull(Convert.ToString(dRow["PolicyFileName"]));
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "PolicyDL.cs", "GetPolicy()", ex.Message, _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// AddEditPolicy
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditPolicy(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditPolicy";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      if (CompanyID != 0)
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
      db.AddInParameter(dbCommand, "PolicyCategoryID", DbType.Int32, PolicyCategoryID);
      db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, PolicyNumber);
      db.AddInParameter(dbCommand, "PolicyNoToEdit", DbType.String, PolicyNoToEdit);
      db.AddInParameter(dbCommand, "PolicyName", DbType.String, PolicyName);
      db.AddInParameter(dbCommand, "EffectiveDate", DbType.DateTime, EffectiveDate);
      db.AddInParameter(dbCommand, "PolicyBinaryData", DbType.Binary, PolicyBinaryData);
      db.AddInParameter(dbCommand, "PolicyFileName", DbType.String, PolicyFileName);
      db.AddInParameter(dbCommand, "IsPolicyFileChanged", DbType.Boolean, IsPolicyFileChanged);
      db.AddInParameter(dbCommand, "FinalRevisions", DbType.DateTime, FinalRevisions);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

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
    /// DeletePolicy
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeletePolicy(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeletePolicy");
      db.AddInParameter(dbCommand, "PolicyNumber", DbType.String, PolicyNumber);
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
