using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class VisitorCardDL : CommonForAllDL
  {
    #region Properties

    public int VisitorCardID { get; set; }

    public string VisitorCardName { get; set; }

    public bool IsAvailable { get; set; }

    #endregion

    #region Constructors

    public VisitorCardDL()
    {
    }

    public VisitorCardDL(int visitorCardID, bool getAllProperties)
    {
      VisitorCardID = visitorCardID;
      if (getAllProperties)
      {
        GetVisitorCard();
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
              //Adding or Editing VisitingCard
              result = AddEditVisitorCard(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteVisitorCard(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get VisitorCard List (All)
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetVisitorCardList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "GetVisitorCardList", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get VisitorCard List By (IsAvailable) Availablity
    /// </summary>
    /// <param name="isAvailable">To get all the available / unavailable</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetVisitorCardListByIsAvailable(bool isAvailable)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardListByIsAvailable";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "IsAvailable", DbType.Boolean, isAvailable);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "GetVisitorCardListByIsAvailable", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get VisitorCard List By (IsAvailable) Availablity And CompanyID
    /// </summary>
    /// <param name="isAvailable">To get all the available / unavailable</param>
    /// <returns>Return the data as Dataset</returns>
    public DataSet GetVisitorCardListByIsAvailableAndCompanyID(bool isAvailable, int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardListByIsAvailableAndCompanyID";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "IsAvailable", DbType.Boolean, isAvailable);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "GetVisitorCardListByIsAvailableAndCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get VisitorCard
    /// </summary>
    private void GetVisitorCard()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCard";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "VisitorCardID", DbType.Int32, VisitorCardID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              VisitorCardID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("VisitorCardID")));
              VisitorCardName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("VisitorCardName")));
              IsAvailable = Convert.ToBoolean(dataReader.GetInt32(dataReader.GetOrdinal("IsAvailable")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardDL.cs", "GetVisitorCard", ex.Message, _myConnection);
      }
    }

    /// <summary>
    /// Add/Edit VisitorCard
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditVisitorCard(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditVisitorCard";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "VisitorCardID", DbType.Int32, VisitorCardID);
      db.AddInParameter(dbCommand, "VisitorCardName", DbType.String, VisitorCardName);
      db.AddInParameter(dbCommand, "IsAvailable", DbType.Boolean, IsAvailable);

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
    /// Delete VisitorCard
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteVisitorCard(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteVisitorCard");
      db.AddInParameter(dbCommand, "VisitorCardID", DbType.Int32, VisitorCardID);
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
