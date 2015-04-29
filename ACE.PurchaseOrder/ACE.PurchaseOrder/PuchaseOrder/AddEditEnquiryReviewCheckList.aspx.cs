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
    public partial class AddEditEnquiryReviewCheckList : System.Web.UI.Page
    {
        TemplateField _fieldYes = new TemplateField();
        TemplateField _fieldNo = new TemplateField();
        TemplateField _fieldNA = new TemplateField();
        TemplateField _fieldRemarks = new TemplateField();
        string _dateFormat = "dd/MM/yyyy";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {

                    GridViewProperties.AssignGridViewProperties(gvReviewDetails);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvReviewDetails.Width = Unit.Percentage(97);
                    LoadContactDropDown();
                    string v = Request.QueryString["EnquiryReviewChecklistID"];
                    if (v != null)
                    {
                        hfEnquiryReviewChecklistID.Value = v;
                        GetEnquiryReviewChecklistDetails(Convert.ToInt32(hfEnquiryReviewChecklistID.Value));

                    }
                    
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        /// <summary>
        /// Load the Contact Drop Down List
        /// </summary>
        private void LoadContactDropDown()
        {
            try
            {
                // Load Contact

                ddlContact.Items.Clear();
                ddlContact.DataSource = new ContactDL().GetContactList(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
                ddlContact.DataTextField = "FName";
                ddlContact.DataValueField = "ContactID";
                ddlContact.DataBind();
                ddlContact.Items.Insert(0, "--Select One--");
                ddlContact.Items[0].Value = "";


                ddlReviewedBy.Items.Clear();
                ddlReviewedBy.DataSource = new EmployeeDL().GetEmployeeListByCompanyID(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
                ddlReviewedBy.DataTextField = "EmployeeName";
                ddlReviewedBy.DataValueField = "EmployeeID";
                ddlReviewedBy.DataBind();
                ddlReviewedBy.Items.Insert(0, "--Select One--");
                ddlReviewedBy.Items[0].Value = "";               
              
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "LoadContactDropDown", ex.Message.ToString(), new ACEConnection());
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

                EnquiryReviewChecklistDL _currentEnquiryReviewChecklist = new EnquiryReviewChecklistDL();
                bool bl = hfEnquiryReviewChecklistID.Value.ToString() == "0";
                _currentEnquiryReviewChecklist.AddEditOption = !bl ? 1 : 0;
                _currentEnquiryReviewChecklist.EnquiryReviewCheckListID = Convert.ToInt32(hfEnquiryReviewChecklistID.Value.ToString());


                _currentEnquiryReviewChecklist.ContactID = Convert.ToInt32(ddlContact.SelectedValue);
                dTime = DateTime.ParseExact(txtEnquiryDate.Text, dtFormat, null);

                _currentEnquiryReviewChecklist.QuotationDate = dTime;
                _currentEnquiryReviewChecklist.PartDescription = txtPartDescription.Text;
                _currentEnquiryReviewChecklist.EnquiryNO = txtEnquiryNo.Text;

                dTime = DateTime.ParseExact(txtEnquiryDate.Text, dtFormat, null);
                _currentEnquiryReviewChecklist.EnquiryDate = dTime;

                _currentEnquiryReviewChecklist.CustEnqRef = txtCustEnqRef.Text;
                _currentEnquiryReviewChecklist.DrawingNo_Issue = txtDrawingNo_Issue.Text;
                _currentEnquiryReviewChecklist.ScopeofSupply = txtScopeofSupply.Text;
                _currentEnquiryReviewChecklist.SpecialTermsConditions = txtSpecialTermsConditions.Text;
                _currentEnquiryReviewChecklist.EnquiryReviewStatus = true;
                _currentEnquiryReviewChecklist.IsStatusAccepted = rbAccepted.Checked;
                _currentEnquiryReviewChecklist.QuoteRef = txtQuoteRef.Text;
                _currentEnquiryReviewChecklist.IsNotAccepted = rbNotAccepted.Checked;
                _currentEnquiryReviewChecklist.IsClarifications = rbClarifications.Checked;
                _currentEnquiryReviewChecklist.Comments = txtComments.Text;
                _currentEnquiryReviewChecklist.ReviewedBy = Convert.ToInt32(ddlReviewedBy.SelectedValue);
                _currentEnquiryReviewChecklist.ReviewedDate = dTime;
                _currentEnquiryReviewChecklist.QuoatationSendBy = Convert.ToString(hfUserID.Value);
                _currentEnquiryReviewChecklist.AuditDate = dTime;
                _currentEnquiryReviewChecklist.AuditID = Convert.ToInt32(hfUserID.Value);


                _currentEnquiryReviewChecklist.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentEnquiryReviewChecklist.Commit();

                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    hfEnquiryReviewChecklistID.Value = _currentEnquiryReviewChecklist.EnquiryReviewCheckListID.ToString();
                    EnquiryReviewListDL listDL;

                    foreach (GridViewRow innerRow in gvReviewDetails.Rows)
                    {

                        listDL = new EnquiryReviewListDL();

                        if (innerRow.Cells[4].Text.ToString() == "")
                            innerRow.Cells[4].Text = "0";

                        bool b2 = innerRow.Cells[4].Text.ToString() == "0";
                        listDL.AddEditOption = !b2 ? 1 : 0;

                        listDL.EnquiryReviewListID = Convert.ToInt32(innerRow.Cells[4].Text.ToString());
                        listDL.EnquiryReviewCheckListID = Convert.ToInt32(hfEnquiryReviewChecklistID.Value.ToString());
                        listDL.ReviewDetailsID = Convert.ToInt32(innerRow.Cells[1].Text.ToString());

                        RadioButton rbYes = (RadioButton)innerRow.Cells[5].FindControl("rbCheckYes");
                        RadioButton rbNo = (RadioButton)innerRow.Cells[6].FindControl("rbCheckNo");
                        RadioButton rbNA = (RadioButton)innerRow.Cells[7].FindControl("rbCheckNA");
                        TextBox txtRemarks = (TextBox)innerRow.Cells[8].FindControl("txtRemarks");
                        listDL.IsYes = rbYes.Checked;
                        listDL.IsNo = rbNo.Checked;
                        listDL.IsNA = rbNA.Checked;
                        listDL.Remarks = txtRemarks.Text;

                        listDL.ScreenMode = ScreenMode.Add;
                        listDL.Commit();
                    }

                    GetEnquiryReviewChecklistDetails(Convert.ToInt32(hfEnquiryReviewChecklistID.Value));

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                    stringBuilder.Append("</script>");
                    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                    //GetEnquiryReviewChecklistDetails();
                    //txtAvailablecount.Text = "0";
                    //lblEnquiryReviewChecklistID.Text = "0";
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
                Response.Redirect("../PuchaseOrder/ManageEnquiryReviewCheckList.aspx");                
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "btnEnquiryReviewChecklistCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    mpeEdit.Hide();
            //    GetEnquiryReviewChecklistDetails();
            //    txtAvailablecount.Text = "0";
            //    lblEnquiryReviewChecklistID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageEnquiryReviewChecklist.aspx", "", "btnEnquiryReviewChecklistCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }

        //To add the identifier of row
        protected void gvReviewDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "row";
            }
        }

        //protected void gvContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            (e.Row.FindControl("lblSerial") as Label).Text = (((this.gvContactDetails.PageIndex * this.gvContactDetails.PageSize) + e.Row.RowIndex) + 1).ToString();
        //            //string str = (e.Row.Cells[1].Controls[0] as LinkButton).Text;
        //            //str = str.Replace("\'", "\\\'");
        //            //(e.Row.FindControl("ibtnDeleteContact") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
        //            ////(e.Row.FindControl("ibtnDeleteContact") as ImageButton).Visible = false;
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "gvContactDetails_RowDataBound", exception1.Message.ToString(), new ACEConnection());
        //        throw;
        //    }
        //}

        protected void gvReviewDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[4].Visible = false;

                    EnquiryReviewListDL rlDL = new EnquiryReviewListDL();
                    rlDL.GetEnquiryReviewListByReviewDetailsID(Convert.ToInt32(hfEnquiryReviewChecklistID.Value), Convert.ToInt32(e.Row.Cells[1].Text));


                    e.Row.Cells[4].Text = rlDL.EnquiryReviewListID.ToString();
                    RadioButton rbYes = (RadioButton)e.Row.FindControl("rbCheckYes");
                    RadioButton rbNo = (RadioButton)e.Row.FindControl("rbCheckNo");
                    RadioButton rbNA = (RadioButton)e.Row.FindControl("rbCheckNA");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                    rbYes.Checked = rlDL.IsYes;
                    rbNo.Checked = rlDL.IsNo;
                    rbNA.Checked = rlDL.IsNA;
                    txtRemarks.Text = rlDL.Remarks;

                    //if (Convert.ToBoolean(e.Row.Cells[4].Text.ToString()) == true && Convert.ToBoolean(e.Row.Cells[5].Text.ToString()) == true)
                    //{
                    //    rbYes.Checked = true;
                    //    rbNo.Checked = true;
                    //    rbNA.Checked = true;
                    //}
                    //else
                    //{
                    //    rbYes.Checked = false;
                    //    rbNo.Checked = false;
                    //    rbNA.Checked = false;
                    //}
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "gvReviewDetails_RowDataBound", ex.Message.ToString(), new ACEConnection());
                throw;
            }

        }

        private void GetEnquiryReviewChecklistDetails(int enquiryReviewChecklistID)
        {
            try
            {
                EnquiryReviewChecklistDL _currentEnquiryReviewChecklist = new EnquiryReviewChecklistDL();
                _currentEnquiryReviewChecklist.GetEnquiryReviewCheckListByEnquiryReviewCheckListID(Convert.ToInt32(hfEnquiryReviewChecklistID.Value.ToString()));

                ddlContact.SelectedValue = _currentEnquiryReviewChecklist.ContactID.ToString();
                txtPartDescription.Text = _currentEnquiryReviewChecklist.PartDescription;
                txtEnquiryNo.Text = _currentEnquiryReviewChecklist.EnquiryNO;
                txtEnquiryDate.Text = _currentEnquiryReviewChecklist.EnquiryDate.ToString();
                txtCustEnqRef.Text = _currentEnquiryReviewChecklist.CustEnqRef;
                txtDrawingNo_Issue.Text = _currentEnquiryReviewChecklist.DrawingNo_Issue;
                txtScopeofSupply.Text = _currentEnquiryReviewChecklist.ScopeofSupply;
                txtSpecialTermsConditions.Text = _currentEnquiryReviewChecklist.SpecialTermsConditions;
                //txtEnquiryReviewStatus.Text= _currentEnquiryReviewChecklist.EnquiryReviewStatus;
                rbAccepted.Checked = _currentEnquiryReviewChecklist.IsStatusAccepted;
                txtQuoteRef.Text = _currentEnquiryReviewChecklist.QuoteRef;
                rbNotAccepted.Checked = _currentEnquiryReviewChecklist.IsNotAccepted;
                rbClarifications.Checked = _currentEnquiryReviewChecklist.IsClarifications;
                txtComments.Text = _currentEnquiryReviewChecklist.Comments;
                ddlReviewedBy.SelectedValue = _currentEnquiryReviewChecklist.ReviewedBy.ToString();
                //= _currentEnquiryReviewChecklist.ReviewedDate
                //= _currentEnquiryReviewChecklist.QuoatationSendBy

                EnquiryReviewListDL rlDL = new EnquiryReviewListDL();

                gvReviewDetails.DataSource = rlDL.GetReviewDetailList().Tables[0];
                gvReviewDetails.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditEnquiryReviewCheckList.aspx", "", "GetEnquiryReviewChecklistDetails(int enquiryReviewChecklistID)", ex.Message, new ACEConnection());
            }
        }

    }
}