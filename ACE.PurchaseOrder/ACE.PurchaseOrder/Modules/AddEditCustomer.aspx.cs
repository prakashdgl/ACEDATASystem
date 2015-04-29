using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System.Text;
using System.Data;
using System.Globalization;


namespace ACE.PurchaseOrder
{
    public partial class AddEditCustomer : System.Web.UI.Page
    {
        private CountryDL _country = new CountryDL();
        private StateDL _state = new StateDL();
        private City _city = new City();
        private TitlesDL _title = new TitlesDL();
        private ContactDL _currentContact = new ContactDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            int i;
            bool bl;
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                bl = base.IsPostBack;
                if (!bl)
                {
                    hfCompanyID.Value = Convert.ToString(base.Session["CompanyID"]);
                    bl = hfCompanyID.Value != "";
                    if (!bl)
                    {
                        hfCompanyID.Value = "0";                       

                    }
                    base.ViewState["SortDirection"] = "ASC";
                    base.ViewState["SortExpression"] = "ContactName";
                    LoadCountryDropDown();
                    LoadProductTypeDropDown();  
                    i = Convert.ToInt32(base.Request.QueryString["ContactID"]);
                    bl = i == 0;
                    if (!bl)
                    {
                        hfContactID.Value = i.ToString();
                        GetContactDetails(i, Convert.ToInt32(hfCompanyID.Value), true);
                    }
                    txtLastName.Focus();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCustomer.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentContact = new ContactDL();
                bool bl = false;
                if (txtContactID.Text.ToString() == "0")
                {
                    _currentContact.AddEditOption = 0;
                }
                else
                {
                    _currentContact.AddEditOption = 1;
                }
                _currentContact.ContactID = Convert.ToInt32(txtContactID.Text);
                _currentContact.CompanyID = Convert.ToInt32(hfCompanyID.Value);
                _currentContact.CompanyName = txtCompanyName.Text;
                _currentContact.FName = txtFirstName.Text;
                _currentContact.LName = txtLastName.Text;
                _currentContact.TinNo = txtTinNo.Text;
                _currentContact.CcrNo = txtCCRNo.Text;
                _currentContact.ContactAddress.Address1 = txtAddress.Text;
                _currentContact.ContactAddress.Address2 = txtAddress2.Text;
                _currentContact.ContactAddress.Address3 = txtAddress3.Text;
                bl = (!(ddlCountry.SelectedValue.ToString() != "")) || (!(ddlCountry.SelectedValue.ToString() != "0")) ? true : ddlCountry.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentContact.ContactAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                }
                bl = (!(ddlState.SelectedValue.ToString() != "")) || (!(ddlState.SelectedValue.ToString() != "0")) ? true : ddlState.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentContact.ContactAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                }
                bl = (!(ddlCity.SelectedValue.ToString() != "")) || (!(ddlCity.SelectedValue.ToString() != "0")) ? true : ddlCity.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentContact.ContactAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                }
                _currentContact.ContactAddress.PostalCode = txtZipCode.Text;
                _currentContact.HomeEmail = txtEmailID.Text;
                _currentContact.WorkEmail = txtSecondEmailID.Text;
                _currentContact.HomePhone = txtHomePhone.Text;
                _currentContact.WorkPhone = txtWorkPhone.Text;
                _currentContact.MobilePhone = txtMobilePhone.Text;

                bl = (!(ddlBank.SelectedValue.ToString() != "")) || (!(ddlBank.SelectedValue.ToString() != "0")) ? true : ddlBank.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentContact.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                }
                _currentContact.BankIfsc = txtIFSCCode.Text;

                bl = (!(ddlAgentName.SelectedValue.ToString() != "")) || (!(ddlAgentName.SelectedValue.ToString() != "0")) ? true : ddlAgentName.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentContact.AgentId = Convert.ToInt32(ddlAgentName.SelectedValue);
                }

                _currentContact.AreaCode = txtAreacode.Text;
                _currentContact.BankCompanyName = txtBankCompanyName.Text;
                _currentContact.BankAccountNumber = txtBankAccountNumber.Text;

                _currentContact.Comments = txtComments.Text;
                _currentContact.PreferredContactId = ThaiRating.CurrentRating.ToString();
                _currentContact.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult;
                transactionResult = _currentContact.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);

                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetContactDetails(_currentContact.ContactID, Convert.ToInt32(hfCompanyID.Value), true);
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCustomer.aspx", "", "btnSave_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Response.Redirect("~/Modules/ManageCustomers.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCustomer.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        
        private void GetContactDetails(int contactID, int companyID, bool isProperties)
        {
            try
            {
                _currentContact = new ContactDL(contactID, companyID, isProperties);
                AssignValues();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCustomer.aspx", "", "GetContactDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        private void AssignValues()
        {
            try
            {
                txtContactID.Text = Convert.ToString(_currentContact.ContactID);
                hfContactID.Value = Convert.ToString(_currentContact.ContactID);
                txtLastName.Text = _currentContact.LName;
                txtFirstName.Text = _currentContact.FName;
                txtCompanyName.Text = _currentContact.CompanyName;
                txtTinNo.Text = _currentContact.TinNo;
                txtCCRNo.Text = _currentContact.CcrNo;
                txtAddress.Text = _currentContact.ContactAddress.Address1;
                txtAddress2.Text = _currentContact.ContactAddress.Address2;
                txtAddress3.Text = _currentContact.ContactAddress.Address3;
                bool bl = _currentContact.ContactAddress.CountryID == 0;
                if (!bl)
                {
                    ddlCountry.SelectedValue = _currentContact.ContactAddress.CountryID.ToString();
                    LoadStateDropDown();
                }
                bl = _currentContact.ContactAddress.StateID == 0;
                if (!bl)
                {
                    ddlState.SelectedValue = _currentContact.ContactAddress.StateID.ToString();
                    LoadCityDropDown();
                }
                bl = _currentContact.ContactAddress.CityID == 0;
                if (!bl)
                {
                    ddlCity.SelectedValue = _currentContact.ContactAddress.CityID.ToString();
                }
                txtZipCode.Text = _currentContact.ContactAddress.PostalCode;
                txtEmailID.Text = _currentContact.HomeEmail;
                txtSecondEmailID.Text = _currentContact.WorkEmail;
                txtHomePhone.Text = _currentContact.HomePhone;
                txtWorkPhone.Text = _currentContact.WorkPhone;
                txtMobilePhone.Text = _currentContact.MobilePhone;

                bl = _currentContact.BankId == 0;
                if (!bl)
                {
                    ddlBank.SelectedValue = _currentContact.BankId.ToString();
                }
                txtIFSCCode.Text = _currentContact.BankIfsc;
                bl = _currentContact.AgentId == 0;
                if (!bl)
                {
                    ddlAgentName.SelectedValue = _currentContact.AgentId.ToString();
                }

                txtAreacode.Text = _currentContact.AreaCode;
                txtBankCompanyName.Text = _currentContact.BankCompanyName;
                txtBankAccountNumber.Text = _currentContact.BankAccountNumber;


                txtComments.Text = _currentContact.Comments;
                if (_currentContact.PreferredContactId != "")
                    ThaiRating.CurrentRating = Convert.ToInt32(_currentContact.PreferredContactId);           

            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCustomer.aspx", "", "AssignValues", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStateDropDown();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "ddlCountry_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadCityDropDown();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "ddlState_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        private void LoadCountryDropDown()
        {
            try
            {
                ddlCountry.DataSource = _country.GetCountryList().Tables[0];
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "LoadCountryDropDown", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        private void LoadStateDropDown()
        {
            try
            {
                if (!((ddlCountry.SelectedValue.ToString() == null ? true : !(ddlCountry.SelectedValue.ToString() != ""))))
                {
                    ddlState.Items.Clear();
                    ddlState.DataSource = _state.GetStateListByCountryID(Convert.ToInt32(ddlCountry.SelectedValue)).Tables[0];
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateID";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, "-- Select One --");
                    ddlState.Items[0].Value = "";
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, "-- Select One --");
                    ddlCity.Items[0].Value = "";
                }
                else
                {
                    ddlState.Items.Clear();
                    ddlState.Items.Insert(0, "-- Select One --");
                    ddlState.Items[0].Value = "";
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, "-- Select One --");
                    ddlCity.Items[0].Value = "";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "LoadStateDropDown", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        private void LoadCityDropDown()
        {
            try
            {
                if (!((ddlState.SelectedValue.ToString() == null ? true : !(ddlState.SelectedValue.ToString() != ""))))
                {
                    ddlCity.Items.Clear();
                    ddlCity.DataSource = _city.GetCityListByStateID(Convert.ToInt32(ddlState.SelectedValue)).Tables[0];
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, "-- Select One --");
                    ddlCity.Items[0].Value = "";
                }
                else
                {
                    ddlCity.Items.Clear();
                    ddlCity.Items.Insert(0, "-- Select One --");
                    ddlCity.Items[0].Value = "";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "LoadCityDropDown", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

       
        private void LoadProductTypeDropDown()
        {
            try
            {
                CommonDL cl = new CommonDL();

                ddlBank.Items.Clear();
                ddlBank.DataSource = cl.GetBankList().Tables[0];
                ddlBank.DataTextField = "BankName";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
                ddlBank.Items.Insert(0, "-- Select One --");
                ddlBank.Items[0].Value = "";

                AgentDL agentOBJ = new AgentDL();
                ddlAgentName.Items.Clear();
                ddlAgentName.DataSource = agentOBJ.GetAgentList().Tables[0];
                ddlAgentName.DataTextField = "AgentName";
                ddlAgentName.DataValueField = "AgentID";
                ddlAgentName.DataBind();
                ddlAgentName.Items.Insert(0, "-- Select One --");
                ddlAgentName.Items[0].Value = "";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditContact.aspx", "", "LoadCityDropDown", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }


        private string ReturnExtension(string fileExtension)
        {
            switch (fileExtension)
            {
                case ".htm":
                case ".html":
                case ".log":
                    return "text/HTML";
                case ".txt":
                    return "text/plain";
                case ".doc":
                    return "application/ms-word";
                case ".tiff":
                case ".tif":
                    return "image/tiff";
                case ".asf":
                    return "video/x-ms-asf";
                case ".avi":
                    return "video/avi";
                case ".zip":
                    return "application/zip";
                case ".xls":
                case ".csv":
                    return "application/vnd.ms-excel";
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case "jpeg":
                    return "image/jpeg";
                case ".bmp":
                    return "image/bmp";
                case ".wav":
                    return "audio/wav";
                case ".mp3":
                    return "audio/mpeg3";
                case ".mpg":
                case "mpeg":
                    return "video/mpeg";
                case ".rtf":
                    return "application/rtf";
                case ".asp":
                    return "text/asp";
                case ".pdf":
                    return "application/pdf";
                case ".fdf":
                    return "application/vnd.fdf";
                case ".ppt":
                    return "application/mspowerpoint";
                case ".dwg":
                    return "image/vnd.dwg";
                case ".msg":
                    return "application/msoutlook";
                case ".xml":
                case ".sdxl":
                    return "application/xml";
                case ".xdp":
                    return "application/vnd.adobe.xdp+xml";
                default:
                    return "application/octet-stream";
            }
        }

        private void DownloadFile(string fname, bool forceDownload)
        {
            string path = fname; //MapPath(fname);
            string name = Path.GetFileName(path);
            string ext = Path.GetExtension(path);
            string type = "";
            // set known types based on file extension  
            if (ext != null)
            {
                switch (ext.ToLower())
                {
                    case ".htm":
                    case ".html":
                        type = "text/HTML";
                        break;

                    case ".txt":
                        type = "text/plain";
                        break;

                    case ".doc":
                    case ".rtf":
                        type = "Application/msword";
                        break;
                    case ".pdf":
                        type = "Application/pdf";
                        break;
                }
            }
            if (forceDownload)
            {
                Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            if (type != "")
                Response.ContentType = type;
            Response.WriteFile(path);
            Response.End();
        }

    }
}