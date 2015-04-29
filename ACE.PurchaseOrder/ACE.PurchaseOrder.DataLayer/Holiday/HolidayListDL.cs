using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class HolidayListDL : CommonForAllDL
  {
    #region Properties

    public int HolidayListID { get; set; }

    public DateTime HolidayDate { get; set; }

    public string HolidayDescription { get; set; }

    public bool Optional { get; set; }

    public int CompanyID { get; set; }

    public int HolidayTypeID { get; set; }

    public string HolidayTypeDescription { get; set; }

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
              //Adding Or Editing HolidayList
              result = AddEditHolidayList(db, transaction);
              if (result.Status == TransactionStatus.Failure)
              {
                transaction.Rollback();
                return result;
              }
              break;
            case ScreenMode.Edit:
              break;
            case ScreenMode.Delete:
              result = DeleteHolidayList(db, transaction);
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
            ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "Commit For Add", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
          }
          if (ScreenMode == ScreenMode.Edit)
          {
            ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "Commit For Edit", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
          }
          if (ScreenMode == ScreenMode.Delete)
          {
            ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "Commit For Delete", ex.Message, _myConnection);
            System.Web.HttpContext.Current.Response.Redirect("~/OrderErrorPage.aspx");
            return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
          }
        }
      }
      return null;
    }

    /// <summary>
    /// Get HolidayList By CompanyID - Added new stored procedure name on 20/06/2014
    /// </summary>
    /// <param name="companyID">Company ID</param>       
    /// <returns>Return the data as Dataset</returns>        
    public DataSet GetHolidayListByCompanyID(int companyID)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "HolidayListByCompanyID_SELECT";//spGetHolidayListByCompanyID
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        ds = db.ExecuteDataSet(dbCommand);
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "GetHolidayListByCompanyID", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get HolidayList By CompanyID And Year - Added new stored procedure name on 20/06/2014
    /// </summary>
    /// <param name="companyID">Company ID</param>
    /// <param name="yearNumber">For the specified year</param>     
    /// <returns>Return the data as Dataset</returns>        
    public DataTable GetHolidayListByCompanyIDAndYear(int companyID, int yearNumber)
    {
      DataTable ds = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "HolidayListByCompanyIDAndYear_SELECT";//spGetHolidayListByCompanyIDAndYear
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "YearNumber", DbType.Int32, yearNumber);
        ds = db.ExecuteDataSet(dbCommand).Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "GetHolidayListByCompanyIDAndYear", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// Get HolidayList By CompanyID And Year - Added new stored procedure name on 20/06/2014
    /// </summary>
    /// <param name="companyID">Company ID</param>
    /// <param name="yearNumber">For the specified year</param>     
    /// <returns>Return the data as Dataset</returns>        
    public DataTable GetHolidayListByCompanyIDAndYearForMobileApp(int companyID, int yearNumber)
    {
      DataTable ds = new DataTable();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "HolidayListByCompanyIDAndYearForMobileApp_SELECT";//spGetHolidayListByCompanyIDAndYearForMobileApp
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "YearNumber", DbType.Int32, yearNumber);
        ds = db.ExecuteDataSet(dbCommand).Tables[0];
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "GetHolidayListByCompanyIDAndYearForMobileApp", ex.Message, _myConnection);
      }
      return ds;
    }

    /// <summary>
    /// IsHolidayExists - Added new stored procedure name on 20/06/2014
    /// </summary>
    /// <param name="companyID"></param>
    /// <param name="holidayDate"></param>
    /// <returns></returns>
    public bool IsHolidayExists(int companyID, DateTime holidayDate)
    {
      DataSet ds = new DataSet();
      try
      {
        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        string sqlCommand = "HolidayByCompanyIDAndDate_SELECT";//spGetHolidayByCompanyIDAndDate
        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, companyID);
        db.AddInParameter(dbCommand, "HolidayDate", DbType.DateTime, holidayDate);
        ds = db.ExecuteDataSet(dbCommand);
        if (ds.Tables[0].Rows.Count > 0)
          return true;
        else
          return false;
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("", "HolidayListDL.cs", "GetHolidayListByCompanyIDAndYear", ex.Message, _myConnection);
        return false;
      }
    }

    /// <summary>
    /// Add Edit Holiday List - Added new stored procedure name on 20/06/2014 
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult AddEditHolidayList(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string sqlCommand = "HolidayList_MERGE";//spAddEditHolidayList
      DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);

      // ID, Date, Description
      db.AddInParameter(dbCommand, "HolidayListID", DbType.Int32, HolidayListID);
      db.AddInParameter(dbCommand, "HolidayDate", DbType.DateTime, HolidayDate);
      db.AddInParameter(dbCommand, "HolidayDescription", DbType.String, HolidayDescription);
      db.AddInParameter(dbCommand, "Optional", DbType.Boolean, Optional);
      db.AddInParameter(dbCommand, "CompanyID", DbType.Int32, CompanyID);
      db.AddInParameter(dbCommand, "HolidayTypeID", DbType.Int32, HolidayTypeID);

      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption);

      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value",
                      DataRowVersion.Default, returnValue);

      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = (Int32)db.GetParameterValue(dbCommand, "Return Value");
      HolidayListID = returnValue;

      if (returnValue == -1)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failure Updated");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      else if (returnValue == -99)
      {
        if (AddEditOption == 1)
          return new TransactionResult(TransactionStatus.Failure, "Failed Updating - Duplicate Holiday Date");
        else
          return new TransactionResult(TransactionStatus.Failure, "Failed Adding - Duplicate Holiday Date");
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
    /// Delete Holiday List - Added new stored procedure name on 20/06/2014
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult DeleteHolidayList(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      DbCommand dbCommand = db.GetStoredProcCommand("HolidayList_MERGE");//spDeleteHolidayList
      db.AddInParameter(dbCommand, "HolidayListID", DbType.Int32, HolidayListID);
      db.AddInParameter(dbCommand, "AddEditOption", DbType.Int16, AddEditOption); //Added on 20/06/2014
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
