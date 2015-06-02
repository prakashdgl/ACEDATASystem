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
    public class PurchaseWorkOrderDL
    {
        #region Private Variables

        private ACEConnection _myConnection;
        private ScreenMode _screenMode;

        private int _addEditOption;

        private int _purchaseWorkOrderID;

        public int PurchaseWorkOrderID
        {
            get { return _purchaseWorkOrderID; }
            set { _purchaseWorkOrderID = value; }
        }
        private int _purchaseOrderID;

        public int PurchaseOrderID
        {
            get { return _purchaseOrderID; }
            set { _purchaseOrderID = value; }
        }
        private string _workerNo;

        public string WorkerNo
        {
            get { return _workerNo; }
            set { _workerNo = value; }
        }
        private string _itemNo;

        public string ItemNo
        {
            get { return _itemNo; }
            set { _itemNo = value; }
        }
        private string _partNo;

        public string PartNo
        {
            get { return _partNo; }
            set { _partNo = value; }
        }
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        private int _qty;

        public int Qty
        {
            get { return _qty; }
            set { _qty = value; }
        }
        private decimal _unitPrice;

        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        private decimal _totalPrice;

        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        private Nullable<DateTime> _reqatSpore;

        public Nullable<DateTime> ReqatSpore
        {
            get { return _reqatSpore; }
            set { _reqatSpore = value; }
        }
        private Nullable<DateTime> _dTofStock;

        public Nullable<DateTime> DTofStock
        {
            get { return _dTofStock; }
            set { _dTofStock = value; }
        }
        private Nullable<DateTime> _dTofDispatch;

        public Nullable<DateTime> DTofDispatch
        {
            get { return _dTofDispatch; }
            set { _dTofDispatch = value; }
        }
        private string _remarks;

        public string Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }
        private int _authorisedSignatureID;

        public int AuthorisedSignatureID
        {
            get { return _authorisedSignatureID; }
            set { _authorisedSignatureID = value; }
        }

        private Nullable<DateTime> _auditDate;
        private int _auditID;

        #endregion

        #region Public Variables

        public PurchaseWorkOrderDL()
        {
            _myConnection = new ACEConnection();
            AddEditOption = 0;
        }

        public Nullable<DateTime> AuditDate
        {
            get { return _auditDate; }
            set { _auditDate = value; }
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

        private TransactionResult AddEditPurchaseWorkOrder(Database db, System.Data.Common.DbTransaction transaction)
        {
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spAddEditPurchaseWorkOrder");
            db.AddInParameter(dbCommand, "PurchaseWorkOrderID", System.Data.DbType.Int32, _purchaseWorkOrderID);
            db.AddInParameter(dbCommand, "PurchaseOrderID", System.Data.DbType.Int32, _purchaseOrderID);
            db.AddInParameter(dbCommand, "WorkerNo", System.Data.DbType.String, WorkerNo);
            db.AddInParameter(dbCommand, "ItemNo", System.Data.DbType.String, ItemNo);
            db.AddInParameter(dbCommand, "PartNo", System.Data.DbType.String, PartNo);

            db.AddInParameter(dbCommand, "Description", System.Data.DbType.String, Description);
            db.AddInParameter(dbCommand, "Qty", System.Data.DbType.Int32, Qty);
            db.AddInParameter(dbCommand, "UnitPrice", System.Data.DbType.Decimal, UnitPrice);
            db.AddInParameter(dbCommand, "TotalPrice", System.Data.DbType.Decimal, TotalPrice);
            db.AddInParameter(dbCommand, "ReqatSpore", System.Data.DbType.DateTime, ReqatSpore);
            db.AddInParameter(dbCommand, "DTofStock", System.Data.DbType.DateTime, DTofStock);
            db.AddInParameter(dbCommand, "DTofDispatch", System.Data.DbType.DateTime, DTofDispatch);
            db.AddInParameter(dbCommand, "Remarks", System.Data.DbType.String, Remarks);
            db.AddInParameter(dbCommand, "AuthorisedSignatureID", System.Data.DbType.Int32, AuthorisedSignatureID);
            db.AddInParameter(dbCommand, "AuditDate", System.Data.DbType.DateTime, _auditDate);
            db.AddInParameter(dbCommand, "AuditID", System.Data.DbType.Int32, _auditID);

            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, _addEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            _purchaseWorkOrderID = (int)db.GetParameterValue(dbCommand, "Return Value");
            if (_purchaseWorkOrderID == -1)
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
                            result = AddEditPurchaseWorkOrder(db, transaction);
                            if (result.Status == TransactionStatus.Failure)
                            {
                                transaction.Rollback();
                                return result;
                            }

                            break;
                        case ScreenMode.Edit:
                            break;
                        case ScreenMode.Delete:
                            result = DeletePurchaseWorkOrder(db, transaction);
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


        private TransactionResult DeletePurchaseWorkOrder(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeletePurchaseWorkOrder");
            db.AddInParameter(dbCommand, "PurchaseWorkOrderID", System.Data.DbType.Int32, _purchaseWorkOrderID);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new TransactionResult(TransactionStatus.Failure, "Failure Deleted") : new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        //public void GetPurchaseWorkOrderByPurchaseWorkOrderID(int PurchaseWorkOrderID)
        //{
        //    try
        //    {
        //        Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        //        string sqlCommand = "spGetPurchaseWorkOrderByPurchaseWorkOrderID";
        //        DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
        //        db.AddInParameter(dbCommand, "PurchaseWorkOrderID", DbType.Int32, PurchaseWorkOrderID);
        //        DataSet ds = db.ExecuteDataSet(dbCommand);

        //        // Load Employee Info
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            DataRow dRow = ds.Tables[0].Rows[0];

        //            PurchaseWorkOrderID = Common.CheckIntNull(Convert.ToInt32(dRow["PurchaseWorkOrderID"]));
        //            //PurchaseWorkOrderNo = Common.CheckNull(Convert.ToString(dRow["PurchaseWorkOrderNo"]));
        //            //SendToID = Common.CheckIntNull(Convert.ToInt32(dRow["SendToID"]));
        //            //PurchaseWorkOrderDate = Convert.ToDateTime(dRow["PurchaseWorkOrderDate"]);
        //            //Currency = Common.CheckNull(Convert.ToString(dRow["Currency"]));
        //            //Shipment = Common.CheckNull(Convert.ToString(dRow["Shipment"]));
        //            //BuyerID = Common.CheckIntNull(Convert.ToInt32(dRow["BuyerID"]));
        //            //TelNo = Common.CheckNull(Convert.ToString(dRow["TelNo"]));
        //            //GrandTotal = Convert.ToDecimal(dRow["GrandTotal"]);

        //            AuditID = Common.CheckIntNull(Convert.ToInt32(dRow["AuditID"]));
        //            AuditDate = Convert.ToDateTime(dRow["AuditDate"]);
        //        }
        //    }
        //    catch (System.Exception exception1)
        //    {
        //        ErrorLog.LogErrorMessageToDB("", "PurchaseWorkOrderDL.cs", "GetPurchaseWorkOrderByPurchaseWorkOrderID", exception1.Message.ToString(), _myConnection);
        //    }

        //}

        public DataSet GetPurchaseWorkOrderByPurchaseOrderID(int sPurchaseOrderID)
        {
            DataSet ds = new DataSet();
            try
            {
                Database db = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                string sqlCommand = "spGetPurchaseWorkOrderByPurchaseOrderID";
                DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);
                db.AddInParameter(dbCommand, "PurchaseOrderID", DbType.Int32, sPurchaseOrderID);
                ds = db.ExecuteDataSet(dbCommand);                
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "PurchaseWorkOrderDL.cs", "GetPurchaseWorkOrderByPurchaseOrderID", exception1.Message.ToString(), _myConnection);
            }
            return ds;
        }

        public System.Data.DataSet GetPurchaseWorkOrderList()
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetPurchaseWorkOrderList");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;                
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "PurchaseWorkOrderDL.cs", "GetPurchaseWorkOrderList", exception1.Message.ToString(), this._myConnection);
                throw;
            }
            return dataSet;
        }
    }
}
