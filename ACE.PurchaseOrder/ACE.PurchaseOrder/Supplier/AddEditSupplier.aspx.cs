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
    public partial class AddEditSupplier : System.Web.UI.Page
    {
        private CountryDL _country = new CountryDL();
        private StateDL _state = new StateDL();
        private City _city = new City();
        private TitlesDL _title = new TitlesDL();
        private SupplierDL _currentSupplier = new SupplierDL();

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
                    base.ViewState["SortExpression"] = "SupplierName";
                    LoadCountryDropDown();
                    LoadProductTypeDropDown();  
                    i = Convert.ToInt32(base.Request.QueryString["SupplierID"]);
                    bl = i == 0;
                    if (!bl)
                    {
                        hfSupplierID.Value = i.ToString();
                        GetSupplierDetails(i, Convert.ToInt32(hfCompanyID.Value), true);
                    }
                    txtSalesPersonName.Focus();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentSupplier = new SupplierDL();
                bool bl = false;
                if (txtSupplierID.Text.ToString() == "0")
                {
                    _currentSupplier.AddEditOption = 0;
                }
                else
                {
                    _currentSupplier.AddEditOption = 1;
                }
                _currentSupplier.SupplierID = Convert.ToInt32(txtSupplierID.Text);
                _currentSupplier.CompanyID = Convert.ToInt32(hfCompanyID.Value);
                _currentSupplier.SupplierCompanyName = txtSupplierCompanyName.Text;
                _currentSupplier.SupplierName = txtSupplierName.Text;
                _currentSupplier.SalesPersonName = txtSalesPersonName.Text;
                _currentSupplier.TinNo = txtTinNo.Text;
                _currentSupplier.CcrNo = txtCCRNo.Text;
                _currentSupplier.SupplierAddress.Address1 = txtAddress.Text;
                _currentSupplier.SupplierAddress.Address2 = txtAddress2.Text;
                _currentSupplier.SupplierAddress.Address3 = txtAddress3.Text;
                bl = (!(ddlCountry.SelectedValue.ToString() != "")) || (!(ddlCountry.SelectedValue.ToString() != "0")) ? true : ddlCountry.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentSupplier.SupplierAddress.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                }
                bl = (!(ddlState.SelectedValue.ToString() != "")) || (!(ddlState.SelectedValue.ToString() != "0")) ? true : ddlState.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentSupplier.SupplierAddress.StateID = Convert.ToInt32(ddlState.SelectedValue);
                }
                bl = (!(ddlCity.SelectedValue.ToString() != "")) || (!(ddlCity.SelectedValue.ToString() != "0")) ? true : ddlCity.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentSupplier.SupplierAddress.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                }
                _currentSupplier.SupplierAddress.PostalCode = txtZipCode.Text;
                _currentSupplier.HomeEmail = txtEmailID.Text;
                _currentSupplier.WorkEmail = txtSecondEmailID.Text;
                _currentSupplier.HomePhone = txtHomePhone.Text;
                _currentSupplier.WorkPhone = txtWorkPhone.Text;
                _currentSupplier.MobilePhone = txtMobilePhone.Text;

                bl = (!(ddlBank.SelectedValue.ToString() != "")) || (!(ddlBank.SelectedValue.ToString() != "0")) ? true : ddlBank.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentSupplier.BankId = Convert.ToInt32(ddlBank.SelectedValue);
                }
               
                _currentSupplier.BankRegion = txtBankRegion.Text;                
                _currentSupplier.BankAccountNumber = txtBankAccountNumber.Text;
                _currentSupplier.BankBranch = txtBankBranch.Text;
                _currentSupplier.BankBranchCode = txtBankCode.Text;
                _currentSupplier.BankIfsc = txtIFSCCode.Text;

                _currentSupplier.BankBranchAddress = txtBranchAddress.Text;


                bl = (!(ddlAgentName.SelectedValue.ToString() != "")) || (!(ddlAgentName.SelectedValue.ToString() != "0")) ? true : ddlAgentName.SelectedValue.ToString() == null;
                if (!bl)
                {
                    _currentSupplier.AgentId = Convert.ToInt32(ddlAgentName.SelectedValue);
                }

                _currentSupplier.Comments = txtComments.Text;
                _currentSupplier.PreferredSupplierId = ThaiRating.CurrentRating.ToString();
                _currentSupplier.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult;
                transactionResult = _currentSupplier.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);

                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetSupplierDetails(_currentSupplier.SupplierID, Convert.ToInt32(hfCompanyID.Value), true);
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "btnSave_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Response.Redirect("~/Supplier/ManageSuppliers.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        
        private void GetSupplierDetails(int SupplierID, int companyID, bool isProperties)
        {
            try
            {
                _currentSupplier = new SupplierDL(SupplierID, companyID, isProperties);
                AssignValues();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "GetSupplierDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        private void AssignValues()
        {
            try
            {
                txtSupplierID.Text = Convert.ToString(_currentSupplier.SupplierID);
                hfSupplierID.Value = Convert.ToString(_currentSupplier.SupplierID);
                txtSalesPersonName.Text = _currentSupplier.SalesPersonName;
                txtSupplierName.Text = _currentSupplier.SupplierName;
                txtSupplierCompanyName.Text = _currentSupplier.SupplierCompanyName;
                txtTinNo.Text = _currentSupplier.TinNo;
                txtCCRNo.Text = _currentSupplier.CcrNo;
                txtAddress.Text = _currentSupplier.SupplierAddress.Address1;
                txtAddress2.Text = _currentSupplier.SupplierAddress.Address2;
                txtAddress3.Text = _currentSupplier.SupplierAddress.Address3;
                bool bl = _currentSupplier.SupplierAddress.CountryID == 0;
                if (!bl)
                {
                    ddlCountry.SelectedValue = _currentSupplier.SupplierAddress.CountryID.ToString();
                    LoadStateDropDown();
                }
                bl = _currentSupplier.SupplierAddress.StateID == 0;
                if (!bl)
                {
                    ddlState.SelectedValue = _currentSupplier.SupplierAddress.StateID.ToString();
                    LoadCityDropDown();
                }
                bl = _currentSupplier.SupplierAddress.CityID == 0;
                if (!bl)
                {
                    ddlCity.SelectedValue = _currentSupplier.SupplierAddress.CityID.ToString();
                }
                txtZipCode.Text = _currentSupplier.SupplierAddress.PostalCode;
                txtEmailID.Text = _currentSupplier.HomeEmail;
                txtSecondEmailID.Text = _currentSupplier.WorkEmail;
                txtHomePhone.Text = _currentSupplier.HomePhone;
                txtWorkPhone.Text = _currentSupplier.WorkPhone;
                txtMobilePhone.Text = _currentSupplier.MobilePhone;

                bl = _currentSupplier.BankId == 0;
                if (!bl)
                {
                    ddlBank.SelectedValue = _currentSupplier.BankId.ToString();
                }
                txtBankAccountNumber.Text = _currentSupplier.BankAccountNumber;
                txtBankRegion.Text = _currentSupplier.BankRegion;
                txtBankBranch.Text = _currentSupplier.BankBranch;
                txtBankCode.Text = _currentSupplier.BankBranchCode;
                txtIFSCCode.Text = _currentSupplier.BankIfsc;
                txtBranchAddress.Text = _currentSupplier.BankBranchAddress;


                bl = _currentSupplier.AgentId == 0;
                if (!bl)
                {
                    ddlAgentName.SelectedValue = _currentSupplier.AgentId.ToString();
                }

                txtComments.Text = _currentSupplier.Comments;
                if (_currentSupplier.PreferredSupplierId != "")
                    ThaiRating.CurrentRating = Convert.ToInt32(_currentSupplier.PreferredSupplierId);           

            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "AssignValues", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "ddlCountry_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "ddlState_SelectedIndexChanged", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "LoadCountryDropDown", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "LoadStateDropDown", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "LoadCityDropDown", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditSupplier.aspx", "", "LoadCityDropDown", exception1.Message.ToString(), new ACEConnection());
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