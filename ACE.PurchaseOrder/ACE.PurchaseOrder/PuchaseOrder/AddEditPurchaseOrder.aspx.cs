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
    public partial class AddEditPurchaseOrder : System.Web.UI.Page
    {
        string _dateFormat = "dd/MM/yyyy";
        PurchaseWorkOrderDL _currentPurchaseWorkOrder = new PurchaseWorkOrderDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {

                    GridViewProperties.AssignGridViewProperties(gvPurchaseWorkOrder);
                    hfCompanyID.Value = "11";
                    hfUserID.Value = "19";
                    gvPurchaseWorkOrder.Width = Unit.Percentage(97);
                    LoadContactDropDown();
                    string v = Request.QueryString["PurchaseOrderID"];
                    trEnablePurchaseWorkOrder.Visible = false;
                    if (v != null)
                    {                        
                        hfPurchaseOrderID.Value = v;
                        GetPurchaseOrderDetails(Convert.ToInt32(hfPurchaseOrderID.Value));
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditPurchaseOrder.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
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
                ErrorLog.LogErrorMessageToDB("AddEditPurchaseOrder.aspx", "", "LoadContactDropDown", ex.Message.ToString(), new ACEConnection());
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnPurchaseOrderAdd_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                string dtFormat = _dateFormat;
                DateTime dTime;

                PurchaseOrderDL _currentPurchaseOrder = new PurchaseOrderDL();
                bool bl = hfPurchaseOrderID.Value.ToString() == "0";
                _currentPurchaseOrder.AddEditOption = !bl ? 1 : 0;
                _currentPurchaseOrder.PurchaseOrderID = Convert.ToInt32(hfPurchaseOrderID.Value.ToString());
                dTime = DateTime.ParseExact(txtPODate.Text, dtFormat, null);
                _currentPurchaseOrder.PurchaseOrderNo = txtPONO.Text;

                _currentPurchaseOrder.BuyerID = Convert.ToInt32(ddlContact.SelectedValue);
                _currentPurchaseOrder.SendToID = Convert.ToInt32(hfCompanyID.Value);
                _currentPurchaseOrder.PurchaseOrderDate = dTime;
                _currentPurchaseOrder.Currency = Convert.ToString(ddlCurrency.SelectedValue);
                _currentPurchaseOrder.Shipment = Convert.ToString(ddlShipment.SelectedValue);
                _currentPurchaseOrder.TelNo = Convert.ToString(txtTel.Text);
                _currentPurchaseOrder.GrandTotal = Convert.ToDecimal(lblTotalCost.Text);
                dTime = GetTodayDate(dtFormat, dTime);

                _currentPurchaseOrder.AuditDate = dTime;
                _currentPurchaseOrder.AuditID = Convert.ToInt32(hfUserID.Value);


                _currentPurchaseOrder.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentPurchaseOrder.Commit();

                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    hfPurchaseOrderID.Value = _currentPurchaseOrder.PurchaseOrderID.ToString();

                    trEnablePurchaseWorkOrder.Visible = true;

                    //OrderAcceptanceParticularsListDL listDL;

                    //foreach (GridViewRow innerRow in gvPurchaseWorkOrder.Rows)
                    //{

                    //    listDL = new OrderAcceptanceParticularsListDL();

                    //    if (innerRow.Cells[3].Text.ToString() == "")
                    //        innerRow.Cells[3].Text = "0";

                    //    bool b2 = innerRow.Cells[3].Text.ToString() == "0";
                    //    listDL.AddEditOption = !b2 ? 1 : 0;

                    //    listDL.OrderAcceptanceParticularsListID = Convert.ToInt32(innerRow.Cells[3].Text.ToString());
                    //    listDL.PurchaseOrderID = Convert.ToInt32(hfPurchaseOrderID.Value.ToString());
                    //    listDL.ParticularsID = Convert.ToInt32(innerRow.Cells[1].Text.ToString());

                    //    RadioButton rbYes = (RadioButton)innerRow.Cells[4].FindControl("rbCheckYes");
                    //    RadioButton rbNo = (RadioButton)innerRow.Cells[4].FindControl("rbCheckNo");                        
                    //    TextBox txtRemarks = (TextBox)innerRow.Cells[5].FindControl("txtRemarks");
                    //    listDL.StatusYes = rbYes.Checked;
                    //    listDL.StatusNo = rbNo.Checked;                        
                    //    listDL.Remarks = txtRemarks.Text;

                    //    listDL.ScreenMode = ScreenMode.Add;
                    //    listDL.Commit();
                    //}

                    GetPurchaseOrderDetails(Convert.ToInt32(hfPurchaseOrderID.Value));


                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                    stringBuilder.Append("</script>");
                    ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                }
                else
                {
                    //txtAvailablecount.Text = "0";
                    //lblPurchaseOrderID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void txtQty_TextChanged(object sender, System.EventArgs e)
        {

            if (txtQty.Text != "" && txtUnitPrice.Text != "")
                txtTotalPrice.Text = Convert.ToString(Convert.ToInt32(txtUnitPrice.Text) * Convert.ToInt32(txtQty.Text));

            mpeEdit.Show();
        }
        protected void txtUnitPrice_TextChanged(object sender, System.EventArgs e)
        {

            if (txtQty.Text != "" && txtUnitPrice.Text != "")
                txtTotalPrice.Text = Convert.ToString(Convert.ToInt32(txtUnitPrice.Text) * Convert.ToInt32(txtQty.Text));

            mpeEdit.Show();
        }

        private static DateTime GetTodayDate(string dtFormat, DateTime dTime)
        {
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

            return dTime;
        }
        protected void btnPurchaseOrderCancel_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                Response.Redirect("../PuchaseOrder/ManagePurchaseOrder.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("AddEditPurchaseOrder.aspx", "", "btnPurchaseOrderCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }

            //try
            //{
            //    mpeEdit.Hide();
            //    GetPurchaseOrderDetails();
            //    txtAvailablecount.Text = "0";
            //    lblPurchaseOrderID.Text = "0";
            //}
            //catch (Exception exception1)
            //{
            //    ErrorLog.LogErrorMessageToDB("ManagePurchaseOrder.aspx", "", "btnPurchaseOrderCancel_Click", exception1.Message.ToString(), new ACEConnection());
            //    throw;
            //}
        }

        //To add the identifier of row
        protected void gvPurchaseWorkOrders_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.CssClass = "row";
            }
        }

        protected void gvPurchaseWorkOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    //    OrderAcceptanceParticularsListDL rlDL = new OrderAcceptanceParticularsListDL();
                    //    rlDL.GetOrderAcceptanceParticularsListByParticularsID(Convert.ToInt32(hfPurchaseOrderID.Value), Convert.ToInt32(e.Row.Cells[1].Text));

                }
                if (e.Row.RowType == DataControlRowType.Header)
                {

                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditPurchaseOrder.aspx", "", "gvPurchaseWorkOrder_RowDataBound", ex.Message.ToString(), new ACEConnection());
                throw;
            }

        }

        private void GetPurchaseOrderDetails(int PurchaseOrderID)
        {
            try
            {
                PurchaseOrderDL _currentPurchaseOrder = new PurchaseOrderDL();
                _currentPurchaseOrder.GetPurchaseOrderByPurchaseOrderID(Convert.ToInt32(hfPurchaseOrderID.Value.ToString()));

                ddlContact.SelectedValue = _currentPurchaseOrder.BuyerID.ToString();
                txtPONO.Text = _currentPurchaseOrder.PurchaseOrderNo;
                txtPODate.Text = _currentPurchaseOrder.PurchaseOrderDate.ToString();
                ddlCurrency.SelectedValue = _currentPurchaseOrder.Currency;
                ddlShipment.SelectedValue = _currentPurchaseOrder.Shipment;
                txtTel.Text = _currentPurchaseOrder.TelNo;
                lblTotalCost.Text = _currentPurchaseOrder.GrandTotal.ToString();

                if (hfPurchaseOrderID.Value.ToString() != "0")
                    trEnablePurchaseWorkOrder.Visible = true;

                _currentPurchaseWorkOrder = new PurchaseWorkOrderDL();

                gvPurchaseWorkOrder.DataSource = _currentPurchaseWorkOrder.GetPurchaseWorkOrderByPurchaseOrderID(Convert.ToInt32(hfPurchaseOrderID.Value.ToString())).Tables[0];
                gvPurchaseWorkOrder.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("AddEditPurchaseOrder.aspx", "", "GetPurchaseOrderDetails(int PurchaseOrderID)", ex.Message, new ACEConnection());
            }
        }

        protected void lnkAddPurchaseWorkOrder_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                hfPurchaseWorkOrderID.Value = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseWorkOrder.aspx", "", "lnkAddPurchaseWorkOrder_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void gvPurchaseWorkOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPurchaseWorkOrder_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void gvPurchaseWorkOrder_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnPurchaseWorkOrderAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                _currentPurchaseWorkOrder = new PurchaseWorkOrderDL();
                bool bl = hfPurchaseWorkOrderID.Value.ToString() == "0";
                _currentPurchaseWorkOrder.AddEditOption = !bl ? 1 : 0;

                _currentPurchaseWorkOrder.PurchaseOrderID = Convert.ToInt32(hfPurchaseOrderID.Value.ToString());

                _currentPurchaseWorkOrder.PurchaseWorkOrderID = Convert.ToInt32(hfPurchaseWorkOrderID.Value.ToString());
                string dtFormat = _dateFormat;
                DateTime dTime;

                _currentPurchaseWorkOrder.WorkerNo = txtPurchaseWorkOrder.Text;
                _currentPurchaseWorkOrder.ItemNo = txtItemNo.Text;
                _currentPurchaseWorkOrder.PartNo = txtPartNo.Text;
                _currentPurchaseWorkOrder.Description = txtDescription.Text;
                _currentPurchaseWorkOrder.Qty = Convert.ToInt32(txtQty.Text);
                _currentPurchaseWorkOrder.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                _currentPurchaseWorkOrder.TotalPrice = Convert.ToDecimal(txtTotalPrice.Text);

                dTime = DateTime.ParseExact(txtReqatSpore.Text, dtFormat, null);
                _currentPurchaseWorkOrder.ReqatSpore = Convert.ToDateTime(txtReqatSpore.Text);
                dTime = DateTime.ParseExact(txtDTofStock.Text, dtFormat, null);
                _currentPurchaseWorkOrder.DTofStock = Convert.ToDateTime(txtDTofStock.Text);
                dTime = DateTime.ParseExact(txtDTofDispatch.Text, dtFormat, null);
                _currentPurchaseWorkOrder.DTofDispatch = Convert.ToDateTime(txtDTofDispatch.Text);

                _currentPurchaseWorkOrder.Remarks = txtRemarks.Text;
                _currentPurchaseWorkOrder.AuthorisedSignatureID = Convert.ToInt32(ddlReviewedBy.SelectedValue);
                _currentPurchaseWorkOrder.AuditID = Convert.ToInt32(hfUserID.Value);

                _currentPurchaseWorkOrder.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = _currentPurchaseWorkOrder.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    //  GetPurchaseWorkOrderDetails();
                    txtPurchaseWorkOrder.Text = "";
                    hfPurchaseWorkOrderID.Value = "0";
                }
                else
                {
                    txtPurchaseWorkOrder.Text = "";
                    hfPurchaseWorkOrderID.Value = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseWorkOrder.aspx", "", "btnPurchaseWorkOrderAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnPurchaseWorkOrderCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                mpeEdit.Hide();
                //  GetPurchaseWorkOrderDetails();
                txtPurchaseWorkOrder.Text = "";
                hfPurchaseWorkOrderID.Value = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManagePurchaseWorkOrder.aspx", "", "btnPurchaseWorkOrderCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
    }
}