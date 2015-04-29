using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
    public class SupplierDL
    {
        private MailingAddressDL _supplierAddress;
        private ACEConnection _myConnection;
        private ScreenMode _screenMode;

        private int _addEditOption;

        private int _supplierID;
        private string _supplierName;
        private int _companyID;
        private string _companyName;
        private string _supplierCompanyName;
        private string _salesPersonName;

        public string SalesPersonName
        {
            get { return _salesPersonName; }
            set { _salesPersonName = value; }
        }

        private string _bankBranchCode;
        public string BankBranchCode
        {
            get { return _bankBranchCode; }
            set { _bankBranchCode = value; }
        }

        private string _homeEmail;
        private string _workEmail;

        private string _homePhone;
        private string _workPhone;

        private string _mobilePhone;

        private string _preferredSupplierID;

        private string _comments;

        private int _bankID;

        private string _bankRegion;

        public string BankRegion
        {
            get { return _bankRegion; }
            set { _bankRegion = value; }
        }
        private string _bankCompanyName;
        private string _bankAccountNumber;
        private string _bankBranch;

        public string BankBranch
        {
            get { return _bankBranch; }
            set { _bankBranch = value; }
        }
        private string _bankIFSC;

        public string BankIFSC
        {
            get { return _bankIFSC; }
            set { _bankIFSC = value; }
        }
        private string _bankBranchAddress;

        public string BankBranchAddress
        {
            get { return _bankBranchAddress; }
            set { _bankBranchAddress = value; }
        }

        private int _agentID;
        private string _tinNo;
        private string _ccrNo;

        public int AddEditOption
        {
            get
            {
                return _addEditOption;
            }
            set
            {
                _addEditOption = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }

        public int SupplierID
        {
            get
            {
                return _supplierID;
            }
            set
            {
                _supplierID = value;
            }
        }

        public MailingAddressDL SupplierAddress
        {
            get
            {
                return _supplierAddress;
            }
            set
            {
                _supplierAddress = value;
            }
        }

        public string SupplierName
        {
            get
            {
                return _supplierName;
            }
            set
            {
                _supplierName = value;
            }
        }

        public string HomeEmail
        {
            get
            {
                return _homeEmail;
            }
            set
            {
                _homeEmail = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return _homePhone;
            }
            set
            {
                _homePhone = value;
            }
        }

        public string SupplierCompanyName
        {
            get
            {
                return _supplierCompanyName;
            }
            set
            {
                _supplierCompanyName = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                return _mobilePhone;
            }
            set
            {
                _mobilePhone = value;
            }
        }

        public string WorkEmail
        {
            get
            {
                return _workEmail;
            }
            set
            {
                _workEmail = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return _workPhone;
            }
            set
            {
                _workPhone = value;
            }
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

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string PreferredSupplierId
        {
            get { return _preferredSupplierID; }
            set { _preferredSupplierID = value; }
        }

        public int BankId
        {
            get { return _bankID; }
            set { _bankID = value; }
        }

        public string BankIfsc
        {
            get { return _bankIFSC; }
            set { _bankIFSC = value; }
        }

        public int AgentId
        {
            get { return _agentID; }
            set { _agentID = value; }
        }

        public string TinNo
        {
            get { return _tinNo; }
            set { _tinNo = value; }
        }

        public string CcrNo
        {
            get { return _ccrNo; }
            set { _ccrNo = value; }
        }

        public string BankCompanyName
        {
            get { return _bankCompanyName; }
            set { _bankCompanyName = value; }
        }

        public string BankAccountNumber
        {
            get { return _bankAccountNumber; }
            set { _bankAccountNumber = value; }
        }


        public SupplierDL()
            : base()
        {
            _myConnection = new ACEConnection();
            _supplierName = "";
            _supplierCompanyName = "";
            _homeEmail = "";
            _workEmail = "";
            _homePhone = "";
            _workPhone = "";
            _mobilePhone = "";
            _comments = "";
            _addEditOption = 0;
            _supplierAddress = new MailingAddressDL();
        }

        public SupplierDL(int SupplierID, int companyID, bool allProperties)
            : base()
        {
            _myConnection = new ACEConnection();
            _supplierName = "";
            _supplierCompanyName = "";
            _homeEmail = "";
            _workEmail = "";
            _homePhone = "";
            _workPhone = "";
            _mobilePhone = "";
            _comments = "";
            _addEditOption = 0;
            _supplierID = SupplierID;
            _companyID = companyID;
            if (allProperties)
            {
                _supplierAddress = new MailingAddressDL();
                GetSupplier();
            }
        }

        private TransactionResult AddEditSupplier(Database db, System.Data.Common.DbTransaction transaction)
        {
            System.Data.Common.DbCommand dbCommand;
            TransactionResult transactionResult;
            bool bl;
            int i = 0;
            dbCommand = db.GetStoredProcCommand("spAddEditSupplier");
            db.AddInParameter(dbCommand, "SupplierID", System.Data.DbType.Int32, _supplierID);
            db.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, _companyID);
            db.AddInParameter(dbCommand, "SupplierName", System.Data.DbType.String, _supplierName);
            db.AddInParameter(dbCommand, "SupplierCompanyName", System.Data.DbType.String, _supplierCompanyName);
            db.AddInParameter(dbCommand, "SalesPersonName", System.Data.DbType.String, _salesPersonName);

            db.AddInParameter(dbCommand, "TinNo", System.Data.DbType.String, _tinNo);
            db.AddInParameter(dbCommand, "CCRNo", System.Data.DbType.String, _ccrNo);


            db.AddInParameter(dbCommand, "Address1", System.Data.DbType.String, _supplierAddress.Address1);
            db.AddInParameter(dbCommand, "Address2", System.Data.DbType.String, _supplierAddress.Address2);
            db.AddInParameter(dbCommand, "Address3", System.Data.DbType.String, _supplierAddress.Address3);
            bl = _supplierAddress.CountryID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "CountryID", System.Data.DbType.Int32, _supplierAddress.CountryID);
            }
            bl = _supplierAddress.StateID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "StateID", System.Data.DbType.Int32, _supplierAddress.StateID);
            }
            bl = _supplierAddress.CityID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "CityID", System.Data.DbType.Int32, _supplierAddress.CityID);
            }
            db.AddInParameter(dbCommand, "ZipCode", System.Data.DbType.String, _supplierAddress.PostalCode);
            db.AddInParameter(dbCommand, "HomeEmail", System.Data.DbType.String, _homeEmail);
            db.AddInParameter(dbCommand, "WorkEmail", System.Data.DbType.String, _workEmail);
            db.AddInParameter(dbCommand, "HomePhone", System.Data.DbType.String, _homePhone);
            db.AddInParameter(dbCommand, "WorkPhone", System.Data.DbType.String, _workPhone);
            db.AddInParameter(dbCommand, "MobilePhone", System.Data.DbType.String, _mobilePhone);




            db.AddInParameter(dbCommand, "BankID", System.Data.DbType.Int32, Convert.ToInt32(_bankID));
            db.AddInParameter(dbCommand, "BankRegion", System.Data.DbType.String, _bankRegion);
            db.AddInParameter(dbCommand, "BankAccountNo", System.Data.DbType.String, _bankAccountNumber);
            db.AddInParameter(dbCommand, "BankBranch", System.Data.DbType.String, _bankBranch);
            db.AddInParameter(dbCommand, "BankBranchCode", System.Data.DbType.String, _bankBranchCode);
            db.AddInParameter(dbCommand, "BankIFSC", System.Data.DbType.String, _bankIFSC);
            db.AddInParameter(dbCommand, "BankBranchAddress", System.Data.DbType.String, _bankBranchAddress);
            db.AddInParameter(dbCommand, "AgentID", System.Data.DbType.Int32, Convert.ToInt32(_agentID));

            db.AddInParameter(dbCommand, "Comments", System.Data.DbType.String, _comments);
            db.AddInParameter(dbCommand, "PreferredSupplierID", System.Data.DbType.Int32, Convert.ToInt32(_preferredSupplierID));

            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, _addEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            _supplierID = (int)db.GetParameterValue(dbCommand, "Return Value");
            if (_supplierID == -1)
            {
                if (_addEditOption == 1)
                    return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
                else
                    return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
            }
            else if (_supplierID == -99)
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
                            result = AddEditSupplier(db, transaction);
                            if (result.Status == TransactionStatus.Failure)
                            {
                                transaction.Rollback();
                                return result;
                            }

                            break;
                        case ScreenMode.Edit:
                            break;
                        case ScreenMode.Delete:
                            result = DeleteSupplier(db, transaction);
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

        private TransactionResult DeleteSupplier(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeleteSupplier");
            db.AddInParameter(dbCommand, "SupplierID", System.Data.DbType.Int32, _supplierID);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new TransactionResult(TransactionStatus.Failure, "Failure Deleted") : new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        private void GetSupplier()
        {
            System.Data.DataSet dataSet;
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetSupplier");
                database.AddInParameter(dbCommand, "SupplierID", System.Data.DbType.Int32, SupplierID);
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, CompanyID);
                dataSet = database.ExecuteDataSet(dbCommand);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    System.Data.DataRow dataRow = dataSet.Tables[0].Rows[0];
                    _supplierName = Common.CheckNull(dataRow["SupplierName"]);
                    _supplierCompanyName = Common.CheckNull(dataRow["SupplierCompanyName"]);
                    _salesPersonName = Common.CheckNull(dataRow["SalesPersonName"]);

                    CompanyID = Common.CheckIntNull(dataRow["CompanyID"]);
                    
                    _tinNo = Common.CheckNull(dataRow["TinNo"]);
                    _ccrNo = Common.CheckNull(dataRow["CCRNo"]);
                    _supplierAddress.Address1 = Common.CheckNull(dataRow["Address1"]);
                    _supplierAddress.Address2 = Common.CheckNull(dataRow["Address2"]);
                    _supplierAddress.Address3 = Common.CheckNull(dataRow["Address3"]);
                    _supplierAddress.CityID = Common.CheckIntNull(dataRow["CityID"]);
                    _supplierAddress.CityName = Common.CheckNull(dataRow["CityName"]);
                    _supplierAddress.StateID = Common.CheckIntNull(dataRow["StateID"]);
                    _supplierAddress.StateName = Common.CheckNull(dataRow["StateName"]);
                    _supplierAddress.CountryID = Common.CheckIntNull(dataRow["CountryID"]);
                    _supplierAddress.CountryName = Common.CheckNull(dataRow["CountryName"]);
                    _supplierAddress.PostalCode = Common.CheckNull(dataRow["ZipCode"]);
                    _homeEmail = Common.CheckNull(dataRow["HomeEmail"]);
                    _workEmail = Common.CheckNull(dataRow["WorkEmail"]);
                    _homePhone = Common.CheckNull(dataRow["HomePhone"]);
                    _workPhone = Common.CheckNull(dataRow["WorkPhone"]);
                    _mobilePhone = Common.CheckNull(dataRow["MobilePhone"]);
                    _comments = Common.CheckNull(dataRow["Comments"]);
                    _preferredSupplierID = Common.CheckNull(dataRow["PreferredSupplierID"]);
                    _bankID = Common.CheckIntNull(dataRow["BankID"]);                    
                    _bankRegion = Common.CheckNull(dataRow["BankRegion"]);
                    _bankAccountNumber = Common.CheckNull(dataRow["BankAccountNo"]);
                    _bankBranch = Common.CheckNull(dataRow["BankBranch"]);
                    _bankBranchCode = Common.CheckNull(dataRow["BankBranchcode"]);
                    _bankIFSC = Common.CheckNull(dataRow["BankIFSC"]);
                    _bankBranchAddress = Common.CheckNull(dataRow["BankBranchAddress"]);
                    _agentID = Common.CheckIntNull(dataRow["AgentID"]);                   
                }
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Supplier.cs", "GetSupplier", exception1.Message.ToString(), _myConnection);
                throw;
            }
        }

        public System.Data.DataSet GetSupplierDetails(int SupplierID, int companyID, string searchText)
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetSupplier");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;
                database.AddInParameter(dbCommand, "SupplierID", System.Data.DbType.Int32, SupplierID);
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
                database.AddInParameter(dbCommand, "SearchText", System.Data.DbType.String, searchText);
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Supplier.cs", "GetSupplierDetails", exception1.Message.ToString(), _myConnection);
                throw;
            }
            return dataSet;
        }

        public System.Data.DataSet GetSupplierList(int companyID)
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetSupplierList");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Supplier.cs", "GetSupplierList", exception1.Message.ToString(), _myConnection);
                throw;
            }
            return dataSet;
        }


        //public System.Data.DataSet GetSupplierListByLocation(int companyID, int countryID, int stateID, int cityID)
        //{
        //    System.Data.DataSet dataSet;
        //    dataSet = new System.Data.DataSet();
        //    try
        //    {
        //        Database database = DatabaseFactory.CreateDatabase(_myConnection.DatabaseName);
        //        System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetSupplierListByLocation");
        //        dbCommand.Parameters.Clear();
        //        dbCommand.CommandTimeout = 300;
        //        database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
        //        database.AddInParameter(dbCommand, "CountryID", System.Data.DbType.Int32, countryID);
        //        database.AddInParameter(dbCommand, "StateID", System.Data.DbType.Int32, stateID);
        //        database.AddInParameter(dbCommand, "CityID", System.Data.DbType.Int32, cityID);
        //        dataSet = database.ExecuteDataSet(dbCommand);
        //    }
        //    catch (System.Exception exception1)
        //    {
        //        ErrorLog.LogErrorMessageToDB("", "Supplier.cs", "GetSupplierListByLocation", exception1.Message.ToString(), _myConnection);
        //        throw;
        //    }
        //    return dataSet;
        //}
    }
}
