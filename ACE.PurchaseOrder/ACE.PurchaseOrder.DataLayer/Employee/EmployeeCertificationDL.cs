using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeCertificationDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeCertificationID { get; set; }

    public int EmployeeID { get; set; }

    public string Certification { get; set; }

    public int TechnologyID { get; set; }

    public string TechnologyDescription { get; set; }

    public int YearOfPass { get; set; }

    public string IssuedBy { get; set; }

    public string ClassObtained { get; set; }

    public string TranscriptID { get; set; }

    #endregion

    #region Constructors

    public EmployeeCertificationDL()
    {
    }

    public EmployeeCertificationDL(int employeeCertificationID, bool getAllProperties)
    {
      EmployeeCertificationID = employeeCertificationID;
      if (getAllProperties)
      {
        GetEmployeeCertification();
      }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Transaction Result for Commit
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
              //Adding or Editing EmployeeCertification
              result = AddEditEmployeeCertification(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeCertification(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Certification
    /// </summary>
    private void GetEmployeeCertification()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeCertification";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeCertificationID", DbType.Int32, EmployeeCertificationID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeCertificationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeCertificationID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              Certification = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Certification")));
              TechnologyID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("TechnologyID")));
              TechnologyDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("TechnologyDescription")));
              YearOfPass = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("YearOfPass")));
              IssuedBy = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("IssuedBy")));
              ClassObtained = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ClassObtained")));
              TranscriptID = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("TranscriptID")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "GetEmployeeCertification", ex.Message.ToString(), _myConnection);
      }
    }

    /// <summary>
    /// Add Edit Employee Certification
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeCertification(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeCertification";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeCertificationID", DbType.Int32, EmployeeCertificationID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "Certification", DbType.String, Certification);
        if (TechnologyID != 0)
          db.AddInParameter(dbCommand, "TechnologyID", DbType.Int32, TechnologyID);
        db.AddInParameter(dbCommand, "YearOfPass", DbType.Int32, YearOfPass);
        db.AddInParameter(dbCommand, "IssuedBy", DbType.String, IssuedBy);
        db.AddInParameter(dbCommand, "ClassObtained", DbType.String, ClassObtained);
        db.AddInParameter(dbCommand, "TranscriptID", DbType.String, TranscriptID);

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
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "AddEditEmployeeCertification", ex.Message, _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Certification
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeCertification(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeCertification");
        db.AddInParameter(dbCommand, "EmployeeCertificationID", DbType.Int32, EmployeeCertificationID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeCertificationDL.cs", "DeleteEmployeeCertification", ex.Message, _myConnection);
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
