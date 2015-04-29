using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.DataLayer
{
    public class ContactDL
    {
        private int _addEditOption;
        private string _comments;
        private int _companyID;
        private string _companyName;
        private int _contactID;
        private MailingAddressDL _contactAddress;
        private ACEConnection _myConnection;
        private ScreenMode _screenMode;
        private string _fName;
        private string _homeEmail;
        private string _homePhone;
        private string _lName;
        private string _mobilePhone;
        private string _workEmail;
        private string _workPhone;
        private string _preferredContactID;
        private int _bankID;
        private string _bankIFSC;
        private int _agentID;
        private string _tinNo;
        private string _ccrNo;
        private string _areaCode;
        private string _bankCompanyName;
        private string _bankAccountNumber;


        public int AddEditOption
        {
            get
            {
                return this._addEditOption;
            }
            set
            {
                this._addEditOption = value;
            }
        }

        public string Comments
        {
            get
            {
                return this._comments;
            }
            set
            {
                this._comments = value;
            }
        }

        public int CompanyID
        {
            get
            {
                return this._companyID;
            }
            set
            {
                this._companyID = value;
            }
        }

        public int ContactID
        {
            get
            {
                return this._contactID;
            }
            set
            {
                this._contactID = value;
            }
        }

        public MailingAddressDL ContactAddress
        {
            get
            {
                return this._contactAddress;
            }
            set
            {
                this._contactAddress = value;
            }
        }

        public string FName
        {
            get
            {
                return this._fName;
            }
            set
            {
                this._fName = value;
            }
        }

        public string HomeEmail
        {
            get
            {
                return this._homeEmail;
            }
            set
            {
                this._homeEmail = value;
            }
        }

        public string HomePhone
        {
            get
            {
                return this._homePhone;
            }
            set
            {
                this._homePhone = value;
            }
        }

        public string LName
        {
            get
            {
                return this._lName;
            }
            set
            {
                this._lName = value;
            }
        }

        public string MobilePhone
        {
            get
            {
                return this._mobilePhone;
            }
            set
            {
                this._mobilePhone = value;
            }
        }

        public string WorkEmail
        {
            get
            {
                return this._workEmail;
            }
            set
            {
                this._workEmail = value;
            }
        }

        public string WorkPhone
        {
            get
            {
                return this._workPhone;
            }
            set
            {
                this._workPhone = value;
            }
        }

        public ScreenMode ScreenMode
        {
            get
            {
                return this._screenMode;
            }
            set
            {
                this._screenMode = value;
            }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public string PreferredContactId
        {
            get { return _preferredContactID; }
            set { _preferredContactID = value; }
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

        public string AreaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
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


        public ContactDL()
            : base()
        {
            this._myConnection = new ACEConnection();
            this._fName = "";
            this._lName = "";
            this._homeEmail = "";
            this._workEmail = "";
            this._homePhone = "";
            this._workPhone = "";
            this._mobilePhone = "";
            this._comments = "";
            this._addEditOption = 0;
            this._contactAddress = new MailingAddressDL();
        }

        public ContactDL(int contactID, int companyID, bool allProperties)
            : base()
        {
            this._myConnection = new ACEConnection();
            this._fName = "";
            this._lName = "";
            this._homeEmail = "";
            this._workEmail = "";
            this._homePhone = "";
            this._workPhone = "";
            this._mobilePhone = "";
            this._comments = "";
            this._addEditOption = 0;
            this._contactID = contactID;
            this._companyID = companyID;
            if (allProperties)
            {
                this._contactAddress = new MailingAddressDL();
                GetContact();
            }
        }

        public ContactDL(string fName, string lName, MailingAddressDL contactAddress, string homeEmail, string workEmail, string homePhone, string workPhone, string mobilePhone, string comments)
            : base()
        {
            this._myConnection = new ACEConnection();
            this._fName = "";
            this._lName = "";
            this._homeEmail = "";
            this._workEmail = "";
            this._homePhone = "";
            this._workPhone = "";
            this._mobilePhone = "";
            this._comments = "";
            this._addEditOption = 0;
            this._fName = fName;
            this._lName = lName;
            this._contactAddress = contactAddress;
            this._homeEmail = homeEmail;
            this._workEmail = workEmail;
            this._homePhone = homePhone;
            this._workPhone = workPhone;
            this._mobilePhone = mobilePhone;
            this._comments = comments;
        }

        private TransactionResult AddEditContact(Database db, System.Data.Common.DbTransaction transaction)
        {
            System.Data.Common.DbCommand dbCommand;
            TransactionResult transactionResult;
            bool bl;
            int i = 0;
            dbCommand = db.GetStoredProcCommand("spAddEditContact");
            db.AddInParameter(dbCommand, "ContactID", System.Data.DbType.Int32, this._contactID);
            db.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, _companyID);
            db.AddInParameter(dbCommand, "FName", System.Data.DbType.String, this._fName);
            db.AddInParameter(dbCommand, "LName", System.Data.DbType.String, this._lName);
            db.AddInParameter(dbCommand, "CompanyName", System.Data.DbType.String, this._companyName);

            db.AddInParameter(dbCommand, "TinNo", System.Data.DbType.String, _tinNo);
            db.AddInParameter(dbCommand, "CCRNo", System.Data.DbType.String, _ccrNo);


            db.AddInParameter(dbCommand, "Address1", System.Data.DbType.String, this._contactAddress.Address1);
            db.AddInParameter(dbCommand, "Address2", System.Data.DbType.String, this._contactAddress.Address2);
            db.AddInParameter(dbCommand, "Address3", System.Data.DbType.String, this._contactAddress.Address3);
            bl = this._contactAddress.CountryID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "CountryID", System.Data.DbType.Int32, this._contactAddress.CountryID);
            }
            bl = this._contactAddress.StateID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "StateID", System.Data.DbType.Int32, this._contactAddress.StateID);
            }
            bl = this._contactAddress.CityID == 0;
            if (!bl)
            {
                db.AddInParameter(dbCommand, "CityID", System.Data.DbType.Int32, this._contactAddress.CityID);
            }
            db.AddInParameter(dbCommand, "ZipCode", System.Data.DbType.String, this._contactAddress.PostalCode);
            db.AddInParameter(dbCommand, "HomeEmail", System.Data.DbType.String, this._homeEmail);
            db.AddInParameter(dbCommand, "WorkEmail", System.Data.DbType.String, this._workEmail);
            db.AddInParameter(dbCommand, "HomePhone", System.Data.DbType.String, this._homePhone);
            db.AddInParameter(dbCommand, "WorkPhone", System.Data.DbType.String, this._workPhone);
            db.AddInParameter(dbCommand, "MobilePhone", System.Data.DbType.String, this._mobilePhone);
            
            db.AddInParameter(dbCommand, "Areacode", System.Data.DbType.String, _areaCode);
            db.AddInParameter(dbCommand, "BankCompanyName", System.Data.DbType.String, _bankCompanyName);
            db.AddInParameter(dbCommand, "BankAccountNo", System.Data.DbType.String, _bankAccountNumber);

            db.AddInParameter(dbCommand, "BankID", System.Data.DbType.Int32, Convert.ToInt32(_bankID));
            db.AddInParameter(dbCommand, "BankIFSC", System.Data.DbType.String, _bankIFSC);
            db.AddInParameter(dbCommand, "AgentID", System.Data.DbType.Int32, Convert.ToInt32(_agentID));

            db.AddInParameter(dbCommand, "Comments", System.Data.DbType.String, this._comments);
            db.AddInParameter(dbCommand, "PreferredContactID", System.Data.DbType.Int32, Convert.ToInt32(_preferredContactID));

            db.AddInParameter(dbCommand, "AddEditOption", System.Data.DbType.Int16, this._addEditOption);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            _contactID = (int)db.GetParameterValue(dbCommand, "Return Value");
            if (_contactID == -1)
            {
                if (_addEditOption == 1)
                    return new TransactionResult(TransactionStatus.Failure, "Failure Updating");
                else
                    return new TransactionResult(TransactionStatus.Failure, "Failure Adding");
            }
            else if (_contactID == -99)
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
                            result = AddEditContact(db, transaction);
                            if (result.Status == TransactionStatus.Failure)
                            {
                                transaction.Rollback();
                                return result;
                            }

                            break;
                        case ScreenMode.Edit:
                            break;
                        case ScreenMode.Delete:
                            result = DeleteContact(db, transaction);
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

        private TransactionResult DeleteContact(Database db, System.Data.Common.DbTransaction transaction)
        {
            TransactionResult transactionResult;
            int i = 0;
            System.Data.Common.DbCommand dbCommand = db.GetStoredProcCommand("spDeleteContact");
            db.AddInParameter(dbCommand, "ContactID", System.Data.DbType.Int32, this._contactID);
            db.AddParameter(dbCommand, "Return Value", System.Data.DbType.Int32, System.Data.ParameterDirection.ReturnValue, "Return Value", System.Data.DataRowVersion.Default, i);
            db.ExecuteNonQuery(dbCommand, transaction);
            i = (int)db.GetParameterValue(dbCommand, "Return Value");
            transactionResult = i == -1 ? new TransactionResult(TransactionStatus.Failure, "Failure Deleted") : new TransactionResult(TransactionStatus.Success, "Successfully Deleted");
            return transactionResult;
        }

        private void GetContact()
        {
            System.Data.DataSet dataSet;
            try
            {
                Database database = DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetContact");
                database.AddInParameter(dbCommand, "ContactID", System.Data.DbType.Int32, ContactID);
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, CompanyID);
                dataSet = database.ExecuteDataSet(dbCommand);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    System.Data.DataRow dataRow = dataSet.Tables[0].Rows[0];
                    this._fName = Common.CheckNull(dataRow["FName"]);
                    this._lName = Common.CheckNull(dataRow["LName"]);
                    CompanyID = Common.CheckIntNull(dataRow["CompanyID"]);
                    this._companyName = Common.CheckNull(dataRow["CompanyName"]);
                    _tinNo = Common.CheckNull(dataRow["TinNo"]);
                    _ccrNo = Common.CheckNull(dataRow["CCRNo"]);
                    this._contactAddress.Address1 = Common.CheckNull(dataRow["Address1"]);
                    this._contactAddress.Address2 = Common.CheckNull(dataRow["Address2"]);
                    this._contactAddress.Address3 = Common.CheckNull(dataRow["Address3"]);
                    this._contactAddress.CityID = Common.CheckIntNull(dataRow["CityID"]);
                    this._contactAddress.CityName = Common.CheckNull(dataRow["CityName"]);
                    this._contactAddress.StateID = Common.CheckIntNull(dataRow["StateID"]);
                    this._contactAddress.StateName = Common.CheckNull(dataRow["StateName"]);
                    this._contactAddress.CountryID = Common.CheckIntNull(dataRow["CountryID"]);
                    this._contactAddress.CountryName = Common.CheckNull(dataRow["CountryName"]);
                    this._contactAddress.PostalCode = Common.CheckNull(dataRow["ZipCode"]);
                    this._homeEmail = Common.CheckNull(dataRow["HomeEmail"]);
                    this._workEmail = Common.CheckNull(dataRow["WorkEmail"]);
                    this._homePhone = Common.CheckNull(dataRow["HomePhone"]);
                    this._workPhone = Common.CheckNull(dataRow["WorkPhone"]);
                    this._mobilePhone = Common.CheckNull(dataRow["MobilePhone"]);
                    this._comments = Common.CheckNull(dataRow["Comments"]);
                    PreferredContactId = Common.CheckNull(dataRow["PreferredContactID"]);
                    _bankID = Common.CheckIntNull(dataRow["BankID"]);
                    _bankIFSC = Common.CheckNull(dataRow["BankIFSC"]);
                    _agentID = Common.CheckIntNull(dataRow["AgentID"]);

                    _areaCode = Common.CheckNull(dataRow["Areacode"]);
                    _bankCompanyName = Common.CheckNull(dataRow["BankCompanyName"]);
                    _bankAccountNumber = Common.CheckNull(dataRow["BankAccountNo"]);
                    
                }
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Contact.cs", "GetContact", exception1.Message.ToString(), this._myConnection);
                throw;
            }
        }

        public System.Data.DataSet GetContactDetails(int contactID, int companyID, string searchText)
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetContact");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;
                database.AddInParameter(dbCommand, "ContactID", System.Data.DbType.Int32, contactID);
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
                database.AddInParameter(dbCommand, "SearchText", System.Data.DbType.String, searchText);
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Contact.cs", "GetContactDetails", exception1.Message.ToString(), this._myConnection);
                throw;
            }
            return dataSet;
        }

        public System.Data.DataSet GetContactList(int companyID)
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetContactList");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Contact.cs", "GetContactList", exception1.Message.ToString(), this._myConnection);
                throw;
            }
            return dataSet;
        }


        public System.Data.DataSet GetContactListByLocation(int companyID, int countryID, int stateID, int cityID)
        {
            System.Data.DataSet dataSet;
            dataSet = new System.Data.DataSet();
            try
            {
                Database database = DatabaseFactory.CreateDatabase(this._myConnection.DatabaseName);
                System.Data.Common.DbCommand dbCommand = database.GetStoredProcCommand("spGetContactListByLocation");
                dbCommand.Parameters.Clear();
                dbCommand.CommandTimeout = 300;
                database.AddInParameter(dbCommand, "CompanyID", System.Data.DbType.Int32, companyID);
                database.AddInParameter(dbCommand, "CountryID", System.Data.DbType.Int32, countryID);
                database.AddInParameter(dbCommand, "StateID", System.Data.DbType.Int32, stateID);
                database.AddInParameter(dbCommand, "CityID", System.Data.DbType.Int32, cityID);
                dataSet = database.ExecuteDataSet(dbCommand);
            }
            catch (System.Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("", "Contact.cs", "GetContactListByLocation", exception1.Message.ToString(), this._myConnection);
                throw;
            }
            return dataSet;
        }
    }
}
