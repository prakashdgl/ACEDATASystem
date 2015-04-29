using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeLanguageKnownDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeLanguageKnownID { get; set; }

    public int EmployeeID { get; set; }

    public int LanguageID { get; set; }

    public string LanguageDescription { get; set; }

    public bool IsRead { get; set; }

    public bool IsWrite { get; set; }

    public bool IsSpeak { get; set; }

    #endregion

    #region Constructors

    public EmployeeLanguageKnownDL()
    {
    }

    public EmployeeLanguageKnownDL(int employeeLanguageKnownID, bool getAllProperties)
    {
      EmployeeLanguageKnownID = employeeLanguageKnownID;
      if (getAllProperties)
      {
        GetEmployeeLanguageKnown();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Commit
    /// </summary>
    /// <returns></returns>
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
              //Adding or Editing EmployeeLanguageKnown
              result = AddEditEmployeeLanguageKnown(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeLanguageKnown(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Language Known
    /// </summary>
    private void GetEmployeeLanguageKnown()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeLanguageKnown";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeLanguageKnownID", DbType.Int32, EmployeeLanguageKnownID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeLanguageKnownID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeLanguageKnownID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              LanguageID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("LanguageID")));
              LanguageDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("LanguageDescription")));
              IsRead = dataReader.GetBoolean(dataReader.GetOrdinal("IsRead"));
              IsWrite = dataReader.GetBoolean(dataReader.GetOrdinal("IsWrite"));
              IsSpeak = dataReader.GetBoolean(dataReader.GetOrdinal("IsSpeak"));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "GetEmployeeLanguageKnown", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit Employee Language Known
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeLanguageKnown(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeLanguageKnown";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeLanguageKnownID", DbType.Int32, EmployeeLanguageKnownID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "LanguageID", DbType.Int32, LanguageID);
        db.AddInParameter(dbCommand, "IsRead", DbType.Boolean, IsRead);
        db.AddInParameter(dbCommand, "IsWrite", DbType.Boolean, IsWrite);
        db.AddInParameter(dbCommand, "IsSpeak", DbType.Boolean, IsSpeak);

        db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

        db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                        DataRowVersion.Default, returnValue);

        db.ExecuteNonQuery(dbCommand, transaction);
        returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

        if (returnValue == -1)
        {
          if (AddEditOption == 1)
            return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
          else if (AddEditOption == 0)
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          else
            return new TransactionResult(TransactionStatus.Failure, "Failure Importing");
        }
        else
        {
          if (AddEditOption == 1)
            return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
          else if (AddEditOption == 0)
            return new TransactionResult(TransactionStatus.Success, "Successfully Added");
          else
            return new TransactionResult(TransactionStatus.Success, "Successfully Imported");
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "AddEditEmployeeLanguageKnown", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Language Known
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeLanguageKnown(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeLanguageKnown");
        db.AddInParameter(dbCommand, "EmployeeLanguageKnownID", DbType.Int32, EmployeeLanguageKnownID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeLanguageKnownDL.cs", "DeleteEmployeeLanguageKnown", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
