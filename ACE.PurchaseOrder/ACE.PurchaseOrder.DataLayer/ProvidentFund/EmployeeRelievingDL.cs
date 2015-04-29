using System;
using System.Data;
using System.Data.Common;
using ACE.Order.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace ACE.Order.DataLayer
{
  public class EmployeeRelievingDL : CommonForAllDL
  {
    #region Properties

    public int EmployeeID { get; set; }

    public DateTime RelievingDate { get; set; }

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
          result = UpdateEmployeeRelieving(db, transaction);
          if (result.Status == TransactionStatus.Failure)
          {
            transaction.Rollback();
          }
          else
          {
            transaction.Commit();
          }
          return result;
        }
        catch (Exception ex)
        {
          transaction.Rollback();
          ErrorLog.LogErrorMessageToDB("", "EmployeeRelievingDL.cs", "Commit for Add", ex.Message, _myConnection);
        }
      }
      return result;
    }

    /// <summary>
    /// Update Employee Relieving
    /// </summary>
    /// <param name="db"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private TransactionResult UpdateEmployeeRelieving(Database db, DbTransaction transaction)
    {
      int returnValue = 0;
      string spName = "spUpdateEmployeeRelieving";
      DbCommand dbCommand = db.GetStoredProcCommand(spName);
      db.AddInParameter(dbCommand, "EmployeeID", DbType.Int32, EmployeeID);
      db.AddInParameter(dbCommand, "RelievingDate", DbType.Date, RelievingDate);
      db.AddParameter(dbCommand, "Return Value", DbType.Int32, ParameterDirection.ReturnValue, "Return Value", DataRowVersion.Default, returnValue);
      db.ExecuteNonQuery(dbCommand, transaction);
      returnValue = Convert.ToInt32(db.GetParameterValue(dbCommand, "Return Value"));

      if (returnValue == -1)
      {
        return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
      }
      else
      {
        return new TransactionResult(TransactionStatus.Success, "Successfully Added");
      }
    }

    #endregion
  }
}
