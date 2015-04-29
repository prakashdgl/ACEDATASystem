using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeEducationDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeEducationID { get; set; }

    public int EmployeeID { get; set; }

    public int QualificationID { get; set; }

    public string QualificationDescription { get; set; }

    public int MajorID { get; set; }

    public string MajorDescription { get; set; }

    public int YearOfPass { get; set; }

    public int UniversityID { get; set; }

    public string UniversityDescription { get; set; }

    public string ClassObtained { get; set; }

    public string InstitutionDescription { get; set; }

    #endregion

    #region Constructors

    public EmployeeEducationDL()
    {
    }

    public EmployeeEducationDL(int employeeEducationID, bool getAllProperties)
    {
      EmployeeEducationID = employeeEducationID;
      if (getAllProperties)
      {
        GetEmployeeEducation();
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
              //Adding or Editing EmployeeEducation
              result = AddEditEmployeeEducation(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeEducation(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    #region Get Employee Education

    /// <summary>
    /// Get Employee Education
    /// </summary>
    private void GetEmployeeEducation()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeEducation";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeEducationID", DbType.Int32, EmployeeEducationID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeEducationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeEducationID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              QualificationID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("QualificationID")));
              QualificationDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("QualificationDescription")));
              MajorID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("MajorID")));
              MajorDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("MajorDescription")));
              YearOfPass = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("YearOfPass")));
              UniversityID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("UniversityID")));
              UniversityDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("UniversityDescription")));
              ClassObtained = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ClassObtained")));
              InstitutionDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("InstitutionDescription")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "GetEmployeeEducation", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
      }
    }

    #endregion

    #region Add Edit Employee Education

    /// <summary>
    /// Add Edit Employee Education
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeEducation(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeEducation";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeEducationID", DbType.Int32, EmployeeEducationID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "QualificationID", DbType.Int32, QualificationID);
        if (MajorID != 0)
        {
          db.AddInParameter(dbCommand, "MajorID", DbType.Int32, MajorID);
        }
        db.AddInParameter(dbCommand, "YearOfPass", DbType.Int32, YearOfPass);
        if (UniversityID != 0)
        {
          db.AddInParameter(dbCommand, "UniversityID", DbType.Int32, UniversityID);
        }
        db.AddInParameter(dbCommand, "ClassObtained", DbType.String, ClassObtained);
        db.AddInParameter(dbCommand, "InstitutionDescription", DbType.String, InstitutionDescription);

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
        ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "AddEditEmployeeEducation", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion

    #region Delete Employee Education

    /// <summary>
    /// Delete Employee Education
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeEducation(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeEducation");
        db.AddInParameter(dbCommand, "EmployeeEducationID", DbType.Int32, EmployeeEducationID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeEducationDL.cs", "DeleteEmployeeEducation", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/Construction.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion

    #endregion
  }
}
