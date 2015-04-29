using ACE.PurchaseOrder.CommonLayer;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ACE.PurchaseOrder.DataLayer
{
    public class PurchaseOrderDL
    {
        #region Private Variables

        private ACEConnection _myConnection;
        private ScreenMode _screenMode;

        private int _addEditOption;

        private int _purchaseOrderID;
        private string _purchaseOrderNo;

        public string PurchaseOrderNo
        {
            get { return _purchaseOrderNo; }
            set { _purchaseOrderNo = value; }
        }
        private int _sendToID;

        public int SendToID
        {
            get { return _sendToID; }
            set { _sendToID = value; }
        }
        private Nullable<DateTime> _purchaseOrderDate;

        public Nullable<DateTime> PurchaseOrderDate
        {
            get { return _purchaseOrderDate; }
            set { _purchaseOrderDate = value; }
        }
        private string _currency;

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }
        private string _shipment;

        public string Shipment
        {
            get { return _shipment; }
            set { _shipment = value; }
        }
        private int _buyerID;

        public int BuyerID
        {
            get { return _buyerID; }
            set { _buyerID = value; }
        }
        private string _telNo;

        public string TelNo
        {
            get { return _telNo; }
            set { _telNo = value; }
        }
        private decimal _grandTotal;

        public decimal GrandTotal
        {
            get { return _grandTotal; }
            set { _grandTotal = value; }
        }

        private Nullable<DateTime> _auditDate;
        private int _auditID;

        #endregion

        #region Public Variables

        public int PurchaseOrderID
        {
            get { return _purchaseOrderID; }
            set { _purchaseOrderID = value; }
        }

        public Nullable<DateTime> AuditDate
        {
            get { return _auditDate; }
            set { _auditDate = value; }
        }


        public PurchaseOrderDL()
        {
            _myConnection = new ACEConnection();
            AddEditOption = 0;
        }

        public int AddEditOption
        {
            get { return _addEditOption; }
            set { _addEditOption = value; }
        }

        public int AuditID
        {
            get { return _auditID; }
            set { _auditID = value; }
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

        #endregion

        private TransactionResult AddEditPurchaseOrder(Database db, System.Data.Common.DbTransaction transaction)
        {
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spAddEditPurchaseOrder");
            db.AddInParameter(dbCommand, "PurchaseOrderID", System.Data.DbType.Int32, _purchaseOrderID);

            db.AddInParameter(dbCommand, "PurchaseOrderNo", System.Data.DbType.String, _purchaseOrderNo);
            db.AddInParameter(dbCommand, "SendToID", System.Data.DbType.Int32, _sendToID);
            db.AddInParameter(dbCommand, "PurchaseOrderDate", System.Data.DbType.DateTime, _purchaseOrderDate);
            db.AddInParameter(dbCommand, "Currency", System.Data.DbType.String, _currency);
            db.AddInParameter(dbCommand, "Shipment", System.Data.DbType.String, _shipment);
            db.AddInParameter(dbCommand, "BuyerID", System.Data.DbType.Int32, _buyerID);
            db.AddInParameter(dbCommand, "TelNo", System.Data.DbType.String, _telNo);
            db.AddInParameter(dbCommand, "GrandTotal", System.Data.DbType.Decimal, _grandTotal);
            db.AddInParameter(dbCommand, "AuditDate", System.Data.DbType.DateTime, _auditDate);
            db.AddInParameter(dbCommand, "AuditID", System.Data.DbType.Int32, _auditID);

            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, _addEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            _purchaseOrderID = (int)db.GetParameterValue(dbCommand, "Return Value");
            if (_purchaseOrderID == -1)
            {
                if (_addEditOption == 1)
                    return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
                else
                    return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
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
                            result = AddEditPurchaseOrder(db, transaction);
                            if (result.Status == TransactionStatus.Failure)
                            {
                                transaction.Rollback();
                                return result;
                            }

                            break;
                        case ScreenMode.Edit:
                            break;
                        case ScreenMode.Delete:
                            result = DeletePurchaseOrder(db, transaction);
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


        private TransactionResult DeletePurchaseOrder(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeletePurchaseOrder");
            db.AddInParameter(dbCommand, "PurchaseOrderID", System.Data.DbType.Int32, _purchaseOrderID);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new TransactionResult(TransactionStatus.Failure, "Failure Deleted") : new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        public void GetPurchaseOrderByPurchaseOrderID(int PurchaseOrderID)
        {
            try
            {
                Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                string sqlCommand = "spGetPurchaseOrderByPurchaseOrderID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "PurchaseOrderID", DbType.Int32, PurchaseOrderID);
                DataSet ds = db.ExecuteDataSet(dbCommand);

                // Load Employee Info
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dRow = ds.Tables[0].Rows[0];

                    PurchaseOrderID = Common.CheckIntNull(Convert.ToInt32(dRow["PurchaseOrderID"]));
                    PurchaseOrderNo = Common.CheckNull(Convert.ToString(dRow["PurchaseOrderNo"]));
                    SendToID = Common.CheckIntNull(Convert.ToInt32(dRow["SendToID"]));
                    PurchaseOrderDate = Convert.ToDateTime(dRow["PurchaseOrderDate"]);
                    Currency = Common.CheckNull(Convert.ToString(dRow["Currency"]));
                    Shipment = Common.CheckNull(Convert.ToString(dRow["Shipment"]));
                    BuyerID = Common.CheckIntNull(Convert.ToInt32(dRow["BuyerID"]));
                    TelNo = Common.CheckNull(Convert.ToString(dRow["TelNo"]));
                    GrandTotal = Convert.ToDecimal(dRow["GrandTotal"]);

                    AuditID = Common.CheckIntNull(Convert.ToInt32(dRow["AuditID"]));
                    AuditDate = Convert.ToDateTime(dRow["AuditDate"]);
                }
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "PurchaseOrderDL.cs", "GetPurchaseOrderByPurchaseOrderID", exception1.Message.ToString(), _myConnection);
            }

        }

        public System.Data.DataSet GetPurchaseOrder()
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                dataSet = database.ExecuteDataSet(database.GetStoredProcCommand("spGetPurchaseOrder"));
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "PurchaseOrderDL.cs", "GetPurchaseOrder", exception1.Message.ToString(), _myConnection);
                throw;
            }
            return dataSet;
        }

    }
}
