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
    public partial class AddEditOrderAcceptanceCheckList : System.Web.UI.Page
    {
        TemplateField _fieldYes = new TemplateField();
        TemplateField _fieldNo = new TemplateField();
        TemplateField _fieldNA = new TemplateField();
        TemplateField _fieldRemarks = new TemplateField();
        string _dateFormat = "MM/dd/yyyy";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {

                    GridViewProperties.AssignGridViewProperties(gvOrderAcceptanceParticularsList);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvOrderAcceptanceParticularsList.Width = Unit.Percentage(97);
                    LoadContactDropDown();
                    string v = Request.QueryString["OrderAcceptanceCheckListID"];
                    if (v != null)
                    {
                        hfOrderAcceptanceCheckListID.Value = v;
                        GetOrderAcceptanceCheckListDetails(Convert.ToInt32(hfOrderAcceptanceCheckListID.Value));
                    }                    
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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

                ddlApprovedBy.Items.Clear();
                ddlApprovedBy.DataSource = new EmployeeDL().GetEmployeeListByCompanyID(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
                ddlApprovedBy.DataTextField = "EmployeeName";
                ddlApprovedBy.DataValueField = "EmployeeID";
                ddlApprovedBy.DataBind();
                ddlApprovedBy.Items.Insert(0, "--Select One--");
                ddlApprovedBy.Items[0].Value = "";


                OrderAcceptanceParticularsListDL rlDL = new OrderAcceptanceParticularsListDL();

                gvOrderAcceptanceParticularsList.DataSource = rlDL.GetParticularsList().Tables[0];
                gvOrderAcceptanceParticularsList.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "LoadContactDropDown", ex.Message.ToString(), new ACEConnection());
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnOrderAcceptanceCheckListAdd_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                string dtFormat = _dateFormat;
                DateTime dTime;

                OrderAcceptanceChecklistDL _currentOrderAcceptanceCheckList = new OrderAcceptanceChecklistDL();
                bool bl = hfOrderAcceptanceCheckListID.Value.ToString() == "0";
                _currentOrderAcceptanceCheckList.AddEditOption = !bl ? 1 : 0;
                _currentOrderAcceptanceCheckList.OrderAcceptanceCheckListID = Convert.ToInt32(hfOrderAcceptanceCheckListID.Value.ToString());


                _currentOrderAcceptanceCheckList.ContactID = Convert.ToInt32(ddlContact.SelectedValue);
                dTime = DateTime.ParseExact(txtPODate.Text, dtFormat, null);

                _currentOrderAcceptanceCheckList.QuotationDate = dTime;
                _currentOrderAcceptanceCheckList.PartDescription = txtPartDescription.Text;
                _currentOrderAcceptanceCheckList.PONO = txtPONo.Text;

                dTime = DateTime.ParseExact(txtPODate.Text, dtFormat, null);
                _currentOrderAcceptanceCheckList.PODate = dTime;

                _currentOrderAcceptanceCheckList.POAmendRef = txtPOAmendRef.Text;
                _currentOrderAcceptanceCheckList.DrawingNo_Issue = txtDrawingNo_Issue.Text;
                _currentOrderAcceptanceCheckList.QuotationRef= txtQuotationRef.Text;

                if (rbFPO.Checked)
                    _currentOrderAcceptanceCheckList.NatureOfReview = rbFPO.Text;
                if (rbRPO.Checked)
                    _currentOrderAcceptanceCheckList.NatureOfReview = rbRPO.Text;
                if (rbAmend.Checked)
                    _currentOrderAcceptanceCheckList.NatureOfReview = rbAmend.Text;


                if (rbCheck1.Checked)
                    _currentOrderAcceptanceCheckList.CheckForOption1 = true;
                if (rbCheck2.Checked)
                    _currentOrderAcceptanceCheckList.CheckForOption2 = true;
                if (rbCheck3.Checked)
                    _currentOrderAcceptanceCheckList.CheckForOption3 = true;
                if (rbCheck4.Checked)
                    _currentOrderAcceptanceCheckList.CheckForOption4 = true;


                _currentOrderAcceptanceCheckList.Comments = txtComments.Text;

                if (rbToBeResolved.Checked)
                    _currentOrderAcceptanceCheckList.ReviewStatus = rbToBeResolved.Text;
                if (rbNotToBeResolved.Checked)
                    _currentOrderAcceptanceCheckList.ReviewStatus = rbNotToBeResolved.Text;
                if (rbAccepted.Checked)
                    _currentOrderAcceptanceCheckList.ReviewStatus = rbAccepted.Text;
                if (rbNotAccepted.Checked)
                    _currentOrderAcceptanceCheckList.ReviewStatus = rbNotAccepted.Text;


                if (ddlReviewedBy.SelectedValue != "")
                {
                    _currentOrderAcceptanceCheckList.ReviewedByID = Convert.ToInt32(ddlReviewedBy.SelectedValue);
                }

                if (txtReviewedDate.Text != null || txtReviewedDate.Text != "")
                {
                    dTime = DateTime.ParseExact(txtReviewedDate.Text, dtFormat, null);
                    _currentOrderAcceptanceCheckList.ReviewedDate = dTime;
                }

                if (ddlApprovedBy.SelectedValue != "")
                {
                    _currentOrderAcceptanceCheckList.ApprovedByID = Convert.ToInt32(ddlApprovedBy.SelectedValue);
                    _currentOrderAcceptanceCheckList.ApprovedDate = dTime;
                }
                
                _currentOrderAcceptanceCheckList.AuditDate = dTime;
                _currentOrderAcceptanceCheckList.AuditID = Convert.ToInt32(hfUserID.Value);

                _currentOrderAcceptanceCheckList.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentOrderAcceptanceCheckList.Commit();

                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    hfOrderAcceptanceCheckListID.Value = _currentOrderAcceptanceCheckList.OrderAcceptanceCheckListID.ToString();
                    OrderAcceptanceParticularsListDL listDL;

                    foreach (GridViewRow innerRow in gvOrderAcceptanceParticularsList.Rows)
                    {

                        listDL = new OrderAcceptanceParticularsListDL();

                        if (innerRow.Cells[3].Text.ToString() == "")
                            innerRow.Cells[3].Text = "0";

                        bool b2 = innerRow.Cells[3].Text.ToString() == "0";
                        listDL.AddEditOption = !b2 ? 1 : 0;

                        listDL.OrderAcceptanceParticularsListID = Convert.ToInt32(innerRow.Cells[3].Text.ToString());
                        listDL.OrderAcceptanceCheckListID = Convert.ToInt32(hfOrderAcceptanceCheckListID.Value.ToString());
                        listDL.ParticularsID = Convert.ToInt32(innerRow.Cells[1].Text.ToString());

                        RadioButton rbYes = (RadioButton)innerRow.Cells[4].FindControl("rbCheckYes");
                        RadioButton rbNo = (RadioButton)innerRow.Cells[4].FindControl("rbCheckNo");                        
                        TextBox txtRemarks = (TextBox)innerRow.Cells[5].FindControl("txtRemarks");
                        listDL.StatusYes = rbYes.Checked;
                        listDL.StatusNo = rbNo.Checked;                        
                        listDL.Remarks = txtRemarks.Text;

                        listDL.ScreenMode = ScreenMode.Add;
                        listDL.Commit();
                    }

                    GetOrderAcceptanceCheckListDetails(Convert.ToInt32(hfOrderAcceptanceCheckListID.Value));


                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                    stringBuilder.Append("</script>");
                    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);                    
                }
                else
                {
                    //txtAvailablecount.Text = "0";
                    //lblOrderAcceptanceCheckListID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceCheckListAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnOrderAcceptanceCheckListCancel_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                Response.Redirect("../PuchaseOrder/ManageOrderAcceptanceCheckList.aspx");                
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceCheckListCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    mpeEdit.Hide();
            //    GetOrderAcceptanceCheckListDetails();
            //    txtAvailablecount.Text = "0";
            //    lblOrderAcceptanceCheckListID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManageOrderAcceptanceCheckList.aspx", "", "btnOrderAcceptanceCheckListCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }

        //To add the identifier of row
        protected void gvOrderAcceptanceParticularsLists_RowCreated(object sender, GridViewRowEventArgs e)
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
        //        ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "gvContactDetails_RowDataBound", exception1.Message.ToString(), new ACEConnection());
        //        throw;
        //    }
        //}

        protected void gvOrderAcceptanceParticularsLists_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    OrderAcceptanceParticularsListDL rlDL = new OrderAcceptanceParticularsListDL();
                    rlDL.GetOrderAcceptanceParticularsListByParticularsID(Convert.ToInt32(hfOrderAcceptanceCheckListID.Value), Convert.ToInt32(e.Row.Cells[1].Text));

                    

                    e.Row.Cells[3].Text = rlDL.OrderAcceptanceParticularsListID.ToString();
                    RadioButton rbYes = (RadioButton)e.Row.FindControl("rbCheckYes");
                    RadioButton rbNo = (RadioButton)e.Row.FindControl("rbCheckNo");
                    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");

                    if (e.Row.Cells[2].Text.ToLower() == "price")
                    {
                        rbYes.Text = "AGREEABLE";
                        rbNo.Text = "AGREEABLE";
                    }

                    if (e.Row.Cells[2].Text.ToLower() == "quantity")
                    {
                        rbYes.Text = "OPEN ORDER";
                        rbNo.Text = "CLOSED";
                    }

                    if (e.Row.Cells[2].Text.ToLower() != "price" && e.Row.Cells[2].Text.ToLower() != "quantity")
                    {
                        rbYes.Text = "YES";
                        rbNo.Text = "NO";
                    }
                    rbYes.Checked = rlDL.StatusYes;
                    rbNo.Checked = rlDL.StatusNo;
                    txtRemarks.Text = rlDL.Remarks;                 
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "gvOrderAcceptanceParticularsList_RowDataBound", ex.Message.ToString(), new ACEConnection());
                throw;
            }

        }

        private void GetOrderAcceptanceCheckListDetails(int OrderAcceptanceCheckListID)
        {
            try
            {
                OrderAcceptanceChecklistDL _currentOrderAcceptanceCheckList = new OrderAcceptanceChecklistDL();
                _currentOrderAcceptanceCheckList.GetOrderAcceptanceCheckListByOrderAcceptanceCheckListID(Convert.ToInt32(hfOrderAcceptanceCheckListID.Value.ToString()));

                ddlContact.SelectedValue = _currentOrderAcceptanceCheckList.ContactID.ToString();
                txtPartDescription.Text = _currentOrderAcceptanceCheckList.PartDescription;
                txtPONo.Text = _currentOrderAcceptanceCheckList.PONO;
                txtPODate.Text = _currentOrderAcceptanceCheckList.PODate.ToString();
                txtPOAmendRef.Text = _currentOrderAcceptanceCheckList.POAmendRef;
                txtDrawingNo_Issue.Text = _currentOrderAcceptanceCheckList.DrawingNo_Issue;
                txtQuotationRef.Text = _currentOrderAcceptanceCheckList.QuotationRef;

                if (_currentOrderAcceptanceCheckList.NatureOfReview.ToString() == "FIRST PURCHASE ORDER")
                    rbFPO.Checked = true;
                if (_currentOrderAcceptanceCheckList.NatureOfReview.ToString() == "REGULAR PURCHASE ORDER")
                    rbRPO.Checked = true;
                if (_currentOrderAcceptanceCheckList.NatureOfReview.ToString() == "AMENDMENTS")
                    rbAmend.Checked = true;


                if (_currentOrderAcceptanceCheckList.CheckForOption1)
                    rbCheck1.Checked = true;
                if (_currentOrderAcceptanceCheckList.CheckForOption2)
                    rbCheck2.Checked = true;
                if (_currentOrderAcceptanceCheckList.CheckForOption3)
                    rbCheck3.Checked = true;
                if (_currentOrderAcceptanceCheckList.CheckForOption4)
                    rbCheck4.Checked = true;

                txtComments.Text = _currentOrderAcceptanceCheckList.Comments;

                if (_currentOrderAcceptanceCheckList.ReviewStatus == "TO BE RESOLVED")
                    rbToBeResolved.Checked = true;
                if (_currentOrderAcceptanceCheckList.ReviewStatus == "NOT TO BE RESOLVED")
                    rbToBeResolved.Checked = true;
                if (_currentOrderAcceptanceCheckList.ReviewStatus == "ACCEPTED")
                    rbToBeResolved.Checked = true;
                if (_currentOrderAcceptanceCheckList.ReviewStatus == "NOT ACCEPTED")
                    rbToBeResolved.Checked = true;

                txtReviewedDate.Text = _currentOrderAcceptanceCheckList.ReviewedDate.ToString();
                ddlReviewedBy.SelectedValue = _currentOrderAcceptanceCheckList.ReviewedByID.ToString();
                ddlApprovedBy.SelectedValue = _currentOrderAcceptanceCheckList.ApprovedByID.ToString();


                OrderAcceptanceParticularsListDL rlDL = new OrderAcceptanceParticularsListDL();

                gvOrderAcceptanceParticularsList.DataSource = rlDL.GetParticularsList().Tables[0];
                gvOrderAcceptanceParticularsList.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditOrderAcceptanceCheckList.aspx", "", "GetOrderAcceptanceCheckListDetails(int OrderAcceptanceCheckListID)", ex.Message, new ACEConnection());
            }
        }

    }
}