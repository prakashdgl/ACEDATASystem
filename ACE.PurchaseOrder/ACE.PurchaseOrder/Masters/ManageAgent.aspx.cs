using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder.Masters
{
    public partial class ManageAgent : System.Web.UI.Page
    {
        private AgentDL _currentAgent = new AgentDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                if (!base.IsPostBack)
                {
                    base.ViewState["SortDirection"] = "ASC";
                    base.ViewState["SortExpression"] = "Agent";
                    GridViewProperties.AssignGridViewProperties(this.gvAgent);
                    this.gvAgent.Width = Unit.Percentage(97);
                    GetAgentDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "Page_Load", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                base.Response.Redirect("~/Default.aspx");
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "btnCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnAgentAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this._currentAgent = new AgentDL();
                bool bl = this.txtAgentID.Text.ToString() == "0";
                this._currentAgent.AddEditOption = !bl ? 1 : 0;
                this._currentAgent.AgentID = Convert.ToInt32(this.txtAgentID.Text.ToString());
                this._currentAgent.AgentDescription = this.txtAgent.Text.ToString();
                this._currentAgent.ScreenMode = ScreenMode.Add;
                TransactionResult transactionResult = this._currentAgent.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                bl = transactionResult.Status != TransactionStatus.Success;
                if (!bl)
                {
                    GetAgentDetails();
                    this.txtAgent.Text = "";
                    this.txtAgentID.Text = "0";
                }
                else
                {
                    this.txtAgent.Text = "";
                    this.txtAgentID.Text = "0";
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "btnAgentAdd_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void btnAgentCancel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.mpeEdit.Hide();
                GetAgentDetails();
                this.txtAgent.Text = "";
                this.txtAgentID.Text = "0";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "btnAgentCancel_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void lnkAddAgent_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.txtAgentID.Text = "0";
                this.txtAgent.Text = "";
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "lnkAddAgent_Click", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvAgent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            bool bl;
            try
            {
                bl = e.Row.RowType != DataControlRowType.DataRow;
                if (!bl)
                {
                    (e.Row.FindControl("lblSerial") as Label).Text = (((this.gvAgent.PageIndex * this.gvAgent.PageSize) + e.Row.RowIndex) + 1).ToString();
                    string str = e.Row.Cells[2].Text;
                    str = str.Replace("\'", "\\\'");
                    (e.Row.FindControl("ibtnDeleteAgent") as ImageButton).Attributes.Add("OnClick", string.Concat("return confirm(\'Are you sure you want to delete \\\'", str, "\\\'?\');"));
                    e.Row.Cells[1].Visible = false;
                    //(e.Row.FindControl("ibtnDeleteAgent") as ImageButton).Visible = false;
                }
                else
                {
                    bl = e.Row.RowType != DataControlRowType.Header;
                    if (!bl)
                    {
                        e.Row.Cells[1].Visible = false;
                    }
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "gvAgent_RowDataBound", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvAgent_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                this.txtAgentID.Text = Convert.ToString(this.gvAgent.DataKeys[e.NewEditIndex].Value);
                this.txtAgent.Text = this.gvAgent.Rows[e.NewEditIndex].Cells[2].Text.ToString();
                this.mpeEdit.Show();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "gvAgent_RowEditing", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        protected void gvAgent_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                this._currentAgent = new AgentDL();
                this._currentAgent.AgentID = Convert.ToInt32(this.gvAgent.DataKeys[e.RowIndex].Value);
                this._currentAgent.ScreenMode = ScreenMode.Delete;
                TransactionResult transactionResult = this._currentAgent.Commit();
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(string.Concat("<script>alert(\'", transactionResult.Message.ToString(), ".\');"));
                stringBuilder.Append("</script>");
                ScriptManager.RegisterStartupScript(base.Page, typeof(string), "MyScript", stringBuilder.ToString(), false);
                if (transactionResult.Status == TransactionStatus.Success)
                {
                    GetAgentDetails();
                }
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "gvAgent_RowDeleting", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
        private void GetAgentDetails()
        {
            try
            {
                this.gvAgent.DataSource = this._currentAgent.GetAgentList().Tables[0];
                this.gvAgent.DataBind();
            }
            catch (Exception exception1)
            {
                ErrorLog.LogErrorMessageToDB("ManageAgent.aspx", "", "GetAgentDetails", exception1.Message.ToString(), new ACEConnection());
                throw;
            }
        }
    }
}