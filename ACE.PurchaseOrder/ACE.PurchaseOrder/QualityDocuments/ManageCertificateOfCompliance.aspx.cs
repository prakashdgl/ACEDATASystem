using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ACE.PurchaseOrder.QualityDocuments
{
    public partial class ManageCertificateOfCompliance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {

            }
        }

        protected void lnkAddCertificateOfCompliance_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/QualityDocuments/AddEditCertificateOfCompliance.aspx");
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

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}