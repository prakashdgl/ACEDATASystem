using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeFamilyDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeFamilyID { get; set; }

    public int EmployeeID { get; set; }

    public string Name { get; set; }

    public int RelationshipID { get; set; }

    public string RelationshipDescription { get; set; }

    public int GenderID { get; set; }

    public string GenderDescription { get; set; }

    public Nullable<DateTime> DOB { get; set; }

    public new int AddEditOption { get; set; }

    #endregion

    #region Constructors

    public EmployeeFamilyDL()
    {
    }

    public EmployeeFamilyDL(int employeeFamilyID, bool getAllProperties)
    {
      EmployeeFamilyID = employeeFamilyID;
      if (getAllProperties)
      {
        GetEmployeeFamily();
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
              //Adding or Editing EmployeeFamily
              result = AddEditEmployeeFamily(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeFamily(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Family
    /// </summary>
    private void GetEmployeeFamily()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeFamily";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeFamilyID", DbType.Int32, EmployeeFamilyID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeFamilyID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeFamilyID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              Name = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Name")));
              RelationshipID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("RelationshipID")));
              RelationshipDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("RelationshipDescription")));
              GenderID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("GenderID")));
              GenderDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("GenderDescription")));
              if (dataReader.GetString(dataReader.GetOrdinal("DOB")) != "")
              {
                DOB = Convert.ToDateTime(dataReader.GetString(dataReader.GetOrdinal("DOB")));
              }
              else
              {
                DOB = null;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "GetEmployeeFamily", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit Employee Family 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeFamily(Database db, DbTransaction transaction)
    {

      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeFamily";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeFamilyID", DbType.Int32, EmployeeFamilyID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "Name", DbType.String, Name);
        db.AddInParameter(dbCommand, "RelationshipID", DbType.Int32, RelationshipID);
        db.AddInParameter(dbCommand, "GenderID", DbType.Int32, GenderID);
        db.AddInParameter(dbCommand, "DOB", DbType.DateTime, DOB);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "AddEditEmployeeFamily", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Family 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeFamily(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeFamily");
        db.AddInParameter(dbCommand, "EmployeeFamilyID", DbType.Int32, EmployeeFamilyID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeFamilyDL.cs", "DeleteEmployeeFamily", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
