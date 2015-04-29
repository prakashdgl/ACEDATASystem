using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class VisitorCardAllocationDL : CommonForAllDL
  {
    #region Properties

    public int VisitorCardAllocationID { get; set; }

    public int VisitorCardID { get; set; }

    public string VisitorCardName { get; set; }

    public int CompanyID { get; set; }

    public int EmployeeID { get; set; }

    public string EmployeeCode { get; set; }

    public string EmployeeName { get; set; }

    public string VisitorName { get; set; }

    public VisitorCardAllocationType VisitorType { get; set; }

    public string VisitorCompanyName { get; set; }

    public Nullable<DateTime> EventDate { get; set; }

    public Nullable<DateTime> IssueDate { get; set; }

    public Nullable<DateTime> ReturnDate { get; set; }

    public VisitorCardAllocationStatus Status { get; set; }

    #endregion

    #region Constructors

    public VisitorCardAllocationDL()
    {
    }

    public VisitorCardAllocationDL(int visitorCardAllocationID, bool getAllProperties)
    {
      VisitorCardAllocationID = visitorCardAllocationID;
      if (getAllProperties)
      {
        GetVisitorCardAllocation();
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
              //Adding or Editing VisitorCardAllocation
              result = AddEditVisitorCardAllocation(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteVisitorCardAllocation(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "Commit For Add", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Assigning");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "Commit For Edit", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "Commit For Delete", ex.Message, _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get VisitorCardAllocation List
    /// </summary>        
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetVisitorCardAllocationList()
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardAllocationList";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "GetVisitorCardAllocationList", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get VisitorCardAllocation List  By Event Date
    /// </summary> 
    /// <param name="eventDate">The event date </param>
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetVisitorCardAllocationListByEventDate(DateTime eventDate)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardAllocationListByEventDate";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EventDate", DbType.DateTime, eventDate);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "GetVisitorCardAllocationListByEventDate", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Return VisitorCardAllocation 
    /// </summary>        
    /// <returns>Return Transaction Result</returns>  
    public TransactionResult ReturnVisitorCardAllocation()
    {
      Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);

      int returnValue = 0;
      string sqlCommand = "spReturnVisitorCardAllocation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
      db.AddInParameter(dbCommand, "VisitorCardAllocationID", DbType.Int32, VisitorCardAllocationID);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Returning");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Returned");
    }

    /// <summary>
    /// Get VisitorCard Allocation
    /// </summary>
    private void GetVisitorCardAllocation()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetVisitorCardAllocation";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "VisitorCardAllocationID", DbType.Int32, VisitorCardAllocationID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              VisitorCardAllocationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("VisitorCardAllocationID")));
              VisitorCardID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("VisitorCardID")));
              VisitorCardName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("VisitorCardName")));
              VisitorType = (VisitorCardAllocationType)Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("VisitorType")));
              if (VisitorType == VisitorCardAllocationType.Employee)
              {
                CompanyID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("CompanyID")));
                EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
                EmployeeCode = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("EmployeeCode")));
                EmployeeName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("EmployeeName")));
                VisitorName = "";
                VisitorCompanyName = "";
              }
              else if (VisitorType == VisitorCardAllocationType.Visitor)
              {
                CompanyID = 0;
                EmployeeID = 0;
                EmployeeCode = "";
                EmployeeName = "";
                VisitorName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("VisitorName")));
                VisitorCompanyName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("VisitorCompanyName")));
              }

              EventDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("EventDate")));
              IssueDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("IssueDate")));
              if (dataReader.GetString(dataReader.GetOrdinal("ReturnDate")) != "")
              {
                ReturnDate = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("ReturnDate")));
              }
              else
              {
                ReturnDate = null;
              }
              Status = (VisitorCardAllocationStatus)Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("Status")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "VisitorCardAllocationDL.cs", "GetVisitorCardAllocation", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// Add/Edit VisitorCard Allocation
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditVisitorCardAllocation(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "spAddEditVisitorCardAllocation";
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      db.AddInParameter(dbCommand, "VisitorCardAllocationID", DbType.Int32, VisitorCardAllocationID);
      db.AddInParameter(dbCommand, "VisitorCardID", DbType.Int32, VisitorCardID);

      if (VisitorType == VisitorCardAllocationType.Employee)
      {
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "EmployeeCode", DbType.String, EmployeeCode);
      }
      else if (VisitorType == VisitorCardAllocationType.Visitor)
      {
        db.AddInParameter(dbCommand, "VisitorName", DbType.String, VisitorName);
        db.AddInParameter(dbCommand, "VisitorCompanyName", DbType.String, VisitorCompanyName);
      }

      db.AddInParameter(dbCommand, "VisitorType", DbType.Int16, VisitorType);
      if (EventDate != null)
      {
        db.AddInParameter(dbCommand, "EventDate", DbType.DateTime, EventDate);
      }
      if (IssueDate != null)
      {
        db.AddInParameter(dbCommand, "IssueDate", DbType.DateTime, IssueDate);
      }
      if (ReturnDate != null)
      {
        db.AddInParameter(dbCommand, "ReturnDate", DbType.DateTime, ReturnDate);
      }

      db.AddInParameter(dbCommand, "Status", DbType.Int16, Status);

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
          return new TransactionResult(TransactionStatus.Failure, "Failure Assigning");
      }
      else
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
        else
          return new TransactionResult(TransactionStatus.Success, "Successfully Assigned");
      }
    }

    /// <summary>
    /// Delete VisitorCard Allocation
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteVisitorCardAllocation(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("spDeleteVisitorCardAllocation");
      db.AddInParameter(dbCommand, "VisitorCardAllocationID", DbType.Int32, VisitorCardAllocationID);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");

      if (returnValue == -1)
        return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
      else
        return new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
    }

    #endregion
  }
}
