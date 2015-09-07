using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACE.PurchaseOrder.QualityDocuments
{
    public partial class AddEditCertificateOfCompliance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {

                    GridViewProperties.AssignGridViewProperties(gvCOC);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvCOC.Width = Unit.Percentage(97);
                    LoadContactDropDown();
                    string v = Request.QueryString["COCID"];
                    if (v != null)
                    {
                        hfCOCID.Value = v;
                        //GetOrderAcceptanceCheckListDetails(Convert.ToInt32(hfOrderAcceptanceCheckListID.Value));
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


                //ddlReviewedBy.Items.Clear();
                //ddlReviewedBy.DataSource = new EmployeeDL().GetEmployeeListByCompanyID(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
                //ddlReviewedBy.DataTextField = "EmployeeName";
                //ddlReviewedBy.DataValueField = "EmployeeID";
                //ddlReviewedBy.DataBind();
                //ddlReviewedBy.Items.Insert(0, "--Select One--");
                //ddlReviewedBy.Items[0].Value = "";

                //ddlApprovedBy.Items.Clear();
                //ddlApprovedBy.DataSource = new EmployeeDL().GetEmployeeListByCompanyID(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
                //ddlApprovedBy.DataTextField = "EmployeeName";
                //ddlApprovedBy.DataValueField = "EmployeeID";
                //ddlApprovedBy.DataBind();
                //ddlApprovedBy.Items.Insert(0, "--Select One--");
                //ddlApprovedBy.Items[0].Value = "";


                //OrderAcceptanceParticularsListDL rlDL = new OrderAcceptanceParticularsListDL();

                //gv.DataSource = rlDL.GetParticularsList().Tables[0];
                //gvOrderAcceptanceParticularsList.DataBind();
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditCertificateOfCompliance.aspx", "", "LoadContactDropDown", ex.Message.ToString(), new ACEConnection());
            }
        }

        protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkAddCOCOrder_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCOCAdd_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCOCCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void gvCOC_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCOC_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvCOC_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnCOCReferenceAdd_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void btnCOCReferenceCancel_Click(object sender, ImageClickEventArgs e)
        {

        }
      
        
    }
}