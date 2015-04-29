using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeeExperienceDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeExperienceID { get; set; }

    public int EmployeeID { get; set; }

    public string OrganizationName { get; set; }

    public string Location { get; set; }

    public string Designation { get; set; }

    public int FromMonth { get; set; }

    public int FromYear { get; set; }

    public int ToMonth { get; set; }

    public int ToYear { get; set; }

    public decimal CTC { get; set; }

    public string JobProfile { get; set; }

    public string FromMonthAndYear
    {
      get
      {
        string strMonthName = "";
        if (FromMonth >= 1 && FromMonth <= 12)
        {
          System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
          strMonthName = mfi.GetMonthName(FromMonth).ToString();
          return strMonthName + " " + FromYear.ToString();
        }
        else
        {
          return FromYear.ToString();
        }
      }
    }

    public string ToMonthAndYear
    {
      get
      {
        string strMonthName = "";
        if (ToMonth >= 1 && ToMonth <= 12)
        {
          System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
          strMonthName = mfi.GetMonthName(ToMonth).ToString();
          return strMonthName + " " + ToYear.ToString();
        }
        else
        {
          return ToYear.ToString();
        }
      }
    }

    #endregion

    #region Constructors

    public EmployeeExperienceDL()
    {
    }

    public EmployeeExperienceDL(int employeeExperienceID, bool getAllProperties)
    {
      EmployeeExperienceID = employeeExperienceID;
      if (getAllProperties)
      {
        GetEmployeeExperience();
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
              //Adding or Editing EmployeeExperience
              result = AddEditEmployeeExperience(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeeExperience(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    ///  Get Employee Experience
    /// </summary>
    private void GetEmployeeExperience()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeeExperience";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeeExperienceID", DbType.Int32, EmployeeExperienceID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeeExperienceID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeExperienceID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              OrganizationName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("OrganizationName")));
              Location = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Location")));
              Designation = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Designation")));
              FromMonth = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FromMonth")));
              FromYear = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FromYear")));
              ToMonth = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ToMonth")));
              ToYear = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ToYear")));
              CTC = Common.CheckIntNull(dataReader.GetDecimal(dataReader.GetOrdinal("CTC")));
              JobProfile = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("JobProfile")));
            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "GetEmployeeExperience", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit Employee Experience
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeeExperience(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeeExperience";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeeExperienceID", DbType.Int32, EmployeeExperienceID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "OrganizationName", DbType.String, OrganizationName);
        db.AddInParameter(dbCommand, "Location", DbType.String, Location);
        db.AddInParameter(dbCommand, "Designation", DbType.String, Designation);
        db.AddInParameter(dbCommand, "FromMonth", DbType.Int32, FromMonth);
        db.AddInParameter(dbCommand, "FromYear", DbType.Int32, FromYear);
        db.AddInParameter(dbCommand, "ToMonth", DbType.Int32, ToMonth);
        db.AddInParameter(dbCommand, "ToYear", DbType.Int32, ToYear);
        db.AddInParameter(dbCommand, "CTC", DbType.Decimal, CTC);
        db.AddInParameter(dbCommand, "JobProfile", DbType.String, JobProfile);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "AddEditEmployeeExperience", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Experience
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeeExperience(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeeExperience");
        db.AddInParameter(dbCommand, "EmployeeExperienceID", DbType.Int32, EmployeeExperienceID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeeExperienceDL.cs", "DeleteEmployeeExperience", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
