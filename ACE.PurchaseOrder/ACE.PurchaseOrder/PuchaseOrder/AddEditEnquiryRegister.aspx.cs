using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACE.PurchaseOrder.PuchaseOrder
{
    public partial class AddEditEnquiryRegister : System.Web.UI.Page
    {
       
        string _dateFormat = "dd/MM/yyyy";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {

                    
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    string v = Request.QueryString["EnquiryRegisterID"];
                    if (v != null)
                    {
                        string EnquiryReviewCheckListID = Request.QueryString["EnquiryReviewCheckListID"];
                        hfEnquiryRegister.Value = v;
                        hfEnquiryReviewChecklistID.Value = EnquiryReviewCheckListID;
                        GetEnquiryRegisterDetails(Convert.ToInt32(hfEnquiryRegister.Value));

                    }
                    
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryRegister.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

       

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnEnquiryReviewChecklistAdd_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                string dtFormat = _dateFormat;
                DateTime dTime;

                EnquiryRegisterDL _currentEnquiryRegister = new EnquiryRegisterDL();
                bool bl = hfEnquiryRegister.Value.ToString() == "0";
                _currentEnquiryRegister.AddEditOption = !bl ? 1 : 0;

                _currentEnquiryRegister.EnquiryReviewCheckListID = Convert.ToInt32(hfEnquiryReviewChecklistID.Value.ToString());
                _currentEnquiryRegister.EnquiryRegisterID = Convert.ToInt32(hfEnquiryRegister.Value.ToString());

                _currentEnquiryRegister.RegretLetter = txtRegretLetter.Text;
                _currentEnquiryRegister.LegalReqmts = txtLegalReq.Text;
                _currentEnquiryRegister.Q1Reqmts = txtQ1Reqm.Text;
                _currentEnquiryRegister.API6AReqmts = txtAPI6A.Text;
                _currentEnquiryRegister.PODetails = txtPODetails.Text;
                _currentEnquiryRegister.Status = txtStatus.Text;

                string addZerosinValueDay = DateTime.Now.Day.ToString();

                if (addZerosinValueDay.Length <= 1)
                {
                    addZerosinValueDay = "0" + addZerosinValueDay;
                }

                string addZerosinValueMonth = DateTime.Now.Month.ToString();

                if (addZerosinValueMonth.Length <= 1)
                {
                    addZerosinValueMonth = "0" + addZerosinValueMonth;
                }

                string dateTimeVal = addZerosinValueDay + "/" + addZerosinValueMonth + "/" +
                                     DateTime.Now.Year.ToString();
                
                dTime = DateTime.ParseExact(dateTimeVal, dtFormat, null);
              
                _currentEnquiryRegister.AuditDate = dTime;
                _currentEnquiryRegister.AuditID = Convert.ToInt32(hfUserID.Value);


                _currentEnquiryRegister.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentEnquiryRegister.Commit();

                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    hfEnquiryRegister.Value = _currentEnquiryRegister.EnquiryRegisterID.ToString();
                    GetEnquiryRegisterDetails(Convert.ToInt32(hfEnquiryRegister.Value));
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                    stringBuilder.Append("</script>");
                    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);                  
                }
                else
                {
                    //txtAvailablecount.Text = "0";
                    //lblEnquiryReviewChecklistID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnEnquiryReviewChecklistCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../PuchaseOrder/ManageEnquiryRegister.aspx");                
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryRegister.aspx", "", "btnEnquiryReviewChecklistCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }            
        }

        private void GetEnquiryRegisterDetails(int enquiryRegisterID)
        {
            try
            {
                EnquiryRegisterDL _currentEnquiryRegister = new EnquiryRegisterDL();
                _currentEnquiryRegister.GetEnquiryRegisterByEnquiryRegisterID(Convert.ToInt32(hfEnquiryRegister.Value.ToString()));

                lblCustomerName.Text = _currentEnquiryRegister.CustomerName;
                lblEnquiryNo.Text = _currentEnquiryRegister.EnquiryNO;
                lblProductDescription.Text = _currentEnquiryRegister.PartDescription;
                txtAPI6A.Text = _currentEnquiryRegister.API6AReqmts;
                txtLegalReq.Text = _currentEnquiryRegister.LegalReqmts;
                txtPODetails.Text = _currentEnquiryRegister.PODetails;
                txtQ1Reqm.Text = _currentEnquiryRegister.Q1Reqmts;
                txtRegretLetter.Text = _currentEnquiryRegister.RegretLetter;
                txtStatus.Text = _currentEnquiryRegister.Status;


            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryRegister.aspx", "", "GetEnquiryReviewChecklistDetails(int enquiryReviewChecklistID)", ex.Message, new ACEConnection());
            }
        }

    }
}