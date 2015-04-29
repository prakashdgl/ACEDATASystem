using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.PurchaseOrder.DataLayer
{
  public class EmployeePreviousEmployersProjectDL : CommonForAllDL
  {
    #region Properties

    public int EmployeePreviousEmployersProjectID { get; set; }

    public int EmployeeID { get; set; }

    public string ProjectName { get; set; }

    public string ProjectDescription { get; set; }

    public string ClientName { get; set; }

    public string Technology { get; set; }

    public string Domain { get; set; }

    public int FromMonth { get; set; }

    public int FromYear { get; set; }

    public int ToMonth { get; set; }

    public int ToYear { get; set; }

    public int TeamSize { get; set; }

    public string RolePlayed { get; set; }

    public bool IsOnsite { get; set; }

    public string OnsiteLocation { get; set; }

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

    public EmployeePreviousEmployersProjectDL()
    {
    }

    public EmployeePreviousEmployersProjectDL(int employeePreviousEmployersProjectID, bool getAllProperties)
    {
      EmployeePreviousEmployersProjectID = employeePreviousEmployersProjectID;
      if (getAllProperties)
      {
        GetEmployeePreviousEmployersProject();
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
              //Adding or Editing EmployeePreviousEmployersProject
              result = AddEditEmployeePreviousEmployersProject(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteEmployeePreviousEmployersProject(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProjectDL.cs", "Commit For Add", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProjectDL.cs", "Commit For Edit", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProjectDL.cs", "Commit For Delete", ex.Message.ToString(), _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get Employee Previous Employers Project
    /// </summary>
    private void GetEmployeePreviousEmployersProject()
    {
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "spGetEmployeePreviousEmployersProject";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "EmployeePreviousEmployersProjectID", DbType.Int32, EmployeePreviousEmployersProjectID);
        using (SqlDataReader dataReader = (SqlDataReader)db.ExecuteReader(dbCommand))
        {
          if (dataReader.HasRows)
          {
            while (dataReader.Read())
            {
              EmployeePreviousEmployersProjectID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeePreviousEmployersProjectID")));
              EmployeeID = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("EmployeeID")));
              ProjectName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ProjectName")));
              ProjectDescription = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ProjectDescription")));
              ClientName = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("ClientName")));
              Technology = Common.CheckNull(dataReader.GetInt32(dataReader.GetOrdinal("Technology")));
              Domain = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("Domain")));
              FromMonth = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FromMonth")));
              FromYear = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("FromYear")));
              ToMonth = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ToMonth")));
              ToYear = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("ToYear")));
              TeamSize = Common.CheckIntNull(dataReader.GetInt32(dataReader.GetOrdinal("TeamSize")));
              RolePlayed = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("RolePlayed")));
              IsOnsite = dataReader.GetBoolean(dataReader.GetOrdinal("IsOnsite"));
              OnsiteLocation = Common.CheckNull(dataReader.GetString(dataReader.GetOrdinal("OnsiteLocation")));

            }
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProject.cs", "GetEmployeePreviousEmployersProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
      }
    }

    /// <summary>
    /// Add Edit Employee Previous Employers Project
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditEmployeePreviousEmployersProject(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        string sqlCommand = "spAddEditEmployeePreviousEmployersProject";
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

        db.AddInParameter(dbCommand, "EmployeePreviousEmployersProjectID", DbType.Int32, EmployeePreviousEmployersProjectID);
        db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
        db.AddInParameter(dbCommand, "ProjectName", DbType.String, ProjectName);
        db.AddInParameter(dbCommand, "ProjectDescription", DbType.String, ProjectDescription);
        db.AddInParameter(dbCommand, "ClientName", DbType.String, ClientName);
        db.AddInParameter(dbCommand, "Technology", DbType.String, Technology);
        db.AddInParameter(dbCommand, "Domain", DbType.String, Domain);
        db.AddInParameter(dbCommand, "FromMonth", DbType.Int32, FromMonth);
        db.AddInParameter(dbCommand, "FromYear", DbType.Int32, FromYear);
        db.AddInParameter(dbCommand, "ToMonth", DbType.Int32, ToMonth);
        db.AddInParameter(dbCommand, "ToYear", DbType.Int32, ToYear);
        db.AddInParameter(dbCommand, "TeamSize", DbType.Int32, TeamSize);
        db.AddInParameter(dbCommand, "RolePlayed", DbType.String, RolePlayed);
        db.AddInParameter(dbCommand, "IsOnsite", DbType.Boolean, IsOnsite);
        db.AddInParameter(dbCommand, "OnsiteLocation", DbType.String, OnsiteLocation);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProjectDL.cs", "AddEditEmployeePreviousEmployersProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    /// <summary>
    /// Delete Employee Previous Employers Project
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteEmployeePreviousEmployersProject(Database db, DbTransaction transaction)
    {
      try
      {
        int returnValue = 0;
        DbCommand dbCommand = db.GetStoredProcCommand("spDeleteEmployeePreviousEmployersProject");
        db.AddInParameter(dbCommand, "EmployeePreviousEmployersProjectID", DbType.Int32, EmployeePreviousEmployersProjectID);
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
        ErrorLog.LogErrorMessageToDB("", "EmployeePreviousEmployersProjectDL.cs", "DeleteEmployeePreviousEmployersProject", ex.Message.ToString(), _myConnection);
        System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
        return new TransactionResult(TransactionStatus.Failure, "Failed Updating");
      }
    }

    #endregion
  }
}
