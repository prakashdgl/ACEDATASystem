using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
    public class MaterialDL
    {
        private int _addEditOption;
        private string _materialDescription;
        private int _materialID;
        private ACEConnection _myConnection;
        private ScreenMode _screenMode;


        public MaterialDL()
            : base()
        {
            _myConnection = new ACEConnection();
            AddEditOption = 0;
        }

        public MaterialDL(string MaterialDescription)
            : base()
        {
            _myConnection = new ACEConnection();
            AddEditOption = 0;
            _materialDescription = MaterialDescription;
        }

        public int AddEditOption
        {
            get { return _addEditOption; }
            set { _addEditOption = value; }
        }

        public string MaterialDescription
        {
            get { return _materialDescription; }
            set { _materialDescription = value; }
        }

        public int MaterialID
        {
            get { return _materialID; }
            set { _materialID = value; }
        }

        public ScreenMode ScreenMode
        {
            get
            {
                return _screenMode;
            }
            set
            {
                _screenMode = value;
            }
        }

        private TransactionResult AddEditMaterial(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            bool bl;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spAddEditMaterial");
            db.AddInParameter(dbCommand, "MaterialID", System.Data.DbType.Int32, MaterialID);
            db.AddInParameter(dbCommand, "MaterialName", System.Data.DbType.String, MaterialDescription);
            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, AddEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            MaterialID = (int)db.GetParameterValue(dbCommand, "Return Value");
            if (MaterialID == -1)
            {
                if (_addEditOption == 1)
                    return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
                else
                    return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
            }
            else if (MaterialID == -99)
            {
                if (_addEditOption == 1)
                {
                    return new TransactionResult(TransactionStatus.Success, "Record already exists");
                }
                else
                {
                    return new TransactionResult(TransactionStatus.Success, "Record already exists");
                }
            }
            else
            {
                if (_addEditOption == 1)
                {
                    return new TransactionResult(TransactionStatus.Success, "Successfully Updated");
                }
                else
                {
                    return new TransactionResult(TransactionStatus.Success, "Successfully Added");
                }
            }
        }


        #region Commit Add/Update/Delete Transactions

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
                    switch (_screenMode)
                    {
                        case ScreenMode.Add:
                            result = AddEditMaterial(db, transaction);
                            if (result.Status == TransactionStatus.Failure)
                            {
                                transaction.Rollback();
                                return result;
                            }

                            break;
                        case ScreenMode.Edit:
                            break;
                        case ScreenMode.Delete:
                            result = DeleteMaterial(db, transaction);
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
                    if (_screenMode == ScreenMode.Add)
                    {

                        return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
                    }
                    if (_screenMode == ScreenMode.Edit)
                    {

                        return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
                    }
                    if (_screenMode == ScreenMode.Delete)
                    {

                        return new TransactionResult(TransactionStatus.Failure, "Failure Deleting");
                    }
                }
            }
            return null;
        }
        #endregion


        private TransactionResult DeleteMaterial(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeleteMaterial");
            db.AddInParameter(dbCommand, "MaterialID", System.Data.DbType.Int32, _materialID);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new TransactionResult(TransactionStatus.Failure, "Failure Deleted") : new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        public int GetMaterialID(string MaterialDescription)
        {
            int i;
            System.Data.SqlClient.SqlDataReader sqlDataReader;
            bool bl;
            i = 0;
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetMaterialID");
                database.AddInParameter(dbCommand, "MaterialName", System.Data.DbType.String, MaterialDescription);
                sqlDataReader = database.ExecuteReader(dbCommand) as System.Data.SqlClient.SqlDataReader;
                try
                {
                    bl = !sqlDataReader.HasRows;
                    if (!bl)
                    {
                        while (sqlDataReader.Read())
                        {
                            i = Common.CheckIntNull(sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("MaterialID")));
                        }
                    }
                }
                finally
                {
                    bl = sqlDataReader == null;
                    if (!bl)
                    {
                        sqlDataReader.Dispose();
                    }
                }
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Material.cs", "GetMaterialID", exception1.Message.ToString(), _myConnection);
            }
            return i;
        }

        public System.Data.DataSet GetMaterialList()
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                dataSet = database.ExecuteDataSet(database.GetStoredProcCommand("spGetMaterialList"));
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Material.cs", "GetMaterialList", exception1.Message.ToString(), _myConnection);
                throw;
            }
            return dataSet;
        }





    }
}
