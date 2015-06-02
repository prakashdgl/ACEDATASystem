using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.CommonLayer;
using ACE.PurchaseOrder.DataLayer;
using System.Text;

namespace ACE.PurchaseOrder.PuchaseOrder
{
    public partial class ManageDocumentUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            base.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!IsPostBack)
            {
                //lnkAddEnquiryRegisterList.Visible = false;
                //base.ViewState["SortDirection"] = "ASC";
                //base.ViewState["SortExpression"] = "EnquiryRegisterList";
                GridViewProperties.AssignGridViewProperties(gvWorkOrderDocument);
                hfCompanyID.Value = "11";
                hfUserID.Value = "19";
                gvWorkOrderDocument.Width = Unit.Percentage(97);
                LoadDropDownList();
            }
        }

        protected void PI_lbtnSave_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
                if (fileUpload.FileName.ToString() != "")
                {
                    PODocumentUploadDL fileDL = new PODocumentUploadDL();
                    fileDL.WorkOrderDocumentId = 0;
                    fileDL.WorkOrderId = 1;
                    fileDL.ContactId = Convert.ToInt32(ddlListContact.SelectedValue);
                    fileDL.PONo = Convert.ToString(ddlPONO.SelectedValue).ToString();
                    fileDL.FileName = fileUpload.FileName;
                    fileDL.FileExtension = getFileExtension(fileUpload.FileName);
                    fileDL.FileDescription = txtFileDescription.Text;

                    StreamReader sr = new StreamReader(fileUpload.PostedFile.InputStream);
                    string filePath = fileUpload.PostedFile.FileName;

                    byte[] fileUploadFile;
                    fileUploadFile = GetByteData();
                    string serverMapPath = Server.MapPath("~");

                    string fileContinuePath = "";
                    fileContinuePath = @"PODocuments\" + Convert.ToInt32(ddlListContact.SelectedValue).ToString() + "\\" + Convert.ToInt32(ddlPONO.SelectedValue).ToString() + "\\" + "" + DateTime.Now.ToString("MMddyyyy") +
                                       DateTime.Now.Date.Second.ToString() + "" + DateTime.Now.Date.Hour.ToString() + "" +
                                       fileUpload.FileName.ToString();

                    SaveFile(fileUploadFile, serverMapPath + "" + fileContinuePath);
                    fileDL.FilePathandFileName = fileContinuePath;

                    fileDL.ScreenMode = ScreenMode.Add;

                    TransactionResult transactionResult = fileDL.Commit();

                    bool bl = transactionResult.Status != TransactionStatus.Success;
                    if (!bl)
                    {
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
        
        }

        public static void SaveFile(Byte[] fileBytes, string fileName)
        {


            string removeFilename = Path.GetFileName(fileName);

            string getDirectory = fileName.Replace(removeFilename, "");

            //string createDirectory = LeftString(getDirectory, "PODocuments")

            //string directoryPath = HttpContext.Current.Server.MapPath(getDirectory));
            if (!Directory.Exists(getDirectory))
            {
                Directory.CreateDirectory(getDirectory);

                if (Directory.Exists(getDirectory))
                {
                    FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                    fileStream.Write(fileBytes, 0, fileBytes.Length);
                    fileStream.Close();
                }
            }
            else
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(fileBytes, 0, fileBytes.Length);
                fileStream.Close();
            }

        }

        #region RightRemove(string fullString, string splitString)

        /// <summary>
        /// Example: RightRemove("Entry_Letdata_Letdata", "_") ==> "Entry_Letdata"
        /// </summary>
        /// <param name="fullString">Full String</param>
        /// <param name="splitString">Split String</param>
        /// <returns>Remove right portion of the text</returns>
        public static string RightRemove(string fullString, string splitString)
        {
            fullString = string.IsNullOrEmpty(fullString) ? string.Empty : fullString;
            splitString = string.IsNullOrEmpty(splitString) ? string.Empty : splitString;
            string result = fullString;

            if (fullString != string.Empty && splitString != string.Empty)
            {
                int startPos = fullString.LastIndexOf(splitString);
                result = startPos == -1 ? fullString : fullString.Substring(0, startPos);
            }
            return result;
        }

        #endregion

        #region LeftRemove(string fullString, string splitString)

        /// <summary>
        /// Example: RightRemove("Entry_Letdata", "_") ==> "Letdata"
        /// </summary>
        /// <param name="fullString">Full String</param>
        /// <param name="splitString">Split String</param>
        /// <returns>Remove left portion of the text</returns>
        public static string LeftRemove(string fullString, string splitString)
        {
            fullString = string.IsNullOrEmpty(fullString) ? string.Empty : fullString;
            splitString = string.IsNullOrEmpty(splitString) ? string.Empty : splitString;
            string result = fullString;

            if (fullString != string.Empty && splitString != string.Empty)
            {
                int startPos = fullString.IndexOf(splitString) + splitString.Length;
                result = startPos == -1 ? fullString : fullString.Substring(startPos);
            }
            return result;
        }

        #endregion

        #region LeftString(string fullString, string splitString)

        /// <summary>
        /// Example: LeftString("Entry_Letdata", "_Letdata") returns "Entry"
        /// </summary>
        /// <param name="fullString">Full String</param>
        /// <param name="splitString">Split String</param>
        /// <returns>Left portion of the Text</returns>
        public static string LeftString(string fullString, string splitString)
        {
            fullString = string.IsNullOrEmpty(fullString) ? string.Empty : fullString;
            splitString = string.IsNullOrEmpty(splitString) ? string.Empty : splitString;
            string result = fullString;

            if (fullString != string.Empty && splitString != string.Empty)
            {
                int startPos = fullString.IndexOf(splitString);
                result = startPos == -1 ? fullString : fullString.Substring(0, startPos);
            }
            return result;
        }

        #endregion

        #region RightString(string fullString, string splitString)

        /// <summary>
        /// Example: RightString("Entry_Letdata_Letdata", "_") ==> "Letdata_Letdata"
        /// </summary>
        /// <param name="fullString">Full String</param>
        /// <param name="splitString">Split String</param>
        /// <returns>Right portion of the Text</returns>
        public static string RightString(string fullString, string splitString)
        {
            fullString = string.IsNullOrEmpty(fullString) ? string.Empty : fullString;
            splitString = string.IsNullOrEmpty(splitString) ? string.Empty : splitString;
            string result = fullString;

            if (fullString != string.Empty && splitString != string.Empty)
            {
                int startPos = fullString.IndexOf(splitString);
                result = (startPos == -1 || startPos == 0) ? fullString : fullString.Substring(startPos + 1);
            }
            return result;
        }

        #endregion

        public string GetFilePath()
        {
            return HttpContext.Current.Server.MapPath("/UploadedFiles");
        }

        private byte[] GetByteData()
        {
            Stream _oStream;
            byte[] _fileData = new byte[fileUpload.PostedFile.ContentLength];

            try
            {
                _oStream = fileUpload.PostedFile.InputStream;
                _oStream.Read(_fileData, 0, fileUpload.PostedFile.ContentLength);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
            return _fileData;
        }

        private string getFileExtension(string fileName)
        {

            string extension = "";
            char[] arr = fileName.ToCharArray();
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == '.')
                {
                    index = i;
                }
            }

            for (int x = index + 1; x < arr.Length; x++)
            {
                extension = extension + arr[x];
            }
            return extension;
        }

        protected void lbtnBack_Click(object sender, EventArgs e)
        {

            Response.Redirect(string.Concat("Default.aspx"));
        }

        private void LoadDropDownList()
        {
            PODocumentUploadDL ddlDL = new PODocumentUploadDL();

            ddlListContact.Items.Clear();
            ddlListContact.DataSource = new ContactDL().GetContactList(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
            ddlListContact.DataTextField = "FName";
            ddlListContact.DataValueField = "ContactID";
            ddlListContact.DataBind();
            ddlListContact.Items.Insert(0, "-- Select One --");
            ddlListContact.Items[0].Value = "";


            ddlCustomer.Items.Clear();
            ddlCustomer.DataSource = new ContactDL().GetContactList(Convert.ToInt32(hfCompanyID.Value)).Tables[0];
            ddlCustomer.DataTextField = "FName";
            ddlCustomer.DataValueField = "ContactID";
            ddlCustomer.DataBind();
            ddlCustomer.Items.Insert(0, "-- Select One --");
            ddlCustomer.Items[0].Value = "";

            ddlPONO.Items.Clear();
            ddlPONO.DataSource = new PurchaseWorkOrderDL().GetPurchaseWorkOrderList().Tables[0];
            ddlPONO.DataTextField = "WorkOrder";
            ddlPONO.DataValueField = "WorkOrder";
            ddlPONO.DataBind();
            ddlPONO.Items.Insert(0, "-- Select One --");
            ddlPONO.Items[0].Value = "";


            //ddlCustomer.ClearSelection();
        }

        protected void gvWorkOrderDocument_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;

                    // Set the Serial Number
                    Label lblSerial = (Label)e.Row.FindControl("lblSerial");
                    lblSerial.Text =
                        ((gvWorkOrderDocument.PageIndex * gvWorkOrderDocument.PageSize) + e.Row.RowIndex + 1).ToString();



                    PODocumentUploadDL _fileStatus = new PODocumentUploadDL();

                    _fileStatus.GetWorkOrderDocumentByWorkOrderDocumentID(Convert.ToInt32(e.Row.Cells[1].Text));

                    string path = MapPath(_fileStatus.FilePathandFileName);
                    //byte[] bts = System.IO.File.ReadAllBytes(path);

                    //Response.Clear();
                    //Response.ClearHeaders();

                    //Response.AddHeader("Content-Type", "Application/octet-stream");
                    //Response.AddHeader("Content-Length", bts.Length.ToString());
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileStatus.FileName);
                    //Response.BinaryWrite(bts);
                    //Response.Flush();
                    //Response.End();



                    HyperLink hlDocumentDownload = (HyperLink)e.Row.FindControl("hlDocumentDownload");
                    hlDocumentDownload.NavigateUrl = "~\\" + _fileStatus.FilePathandFileName;

                }
                else if (e.Row.RowType == DataControlRowType.Header)
                {
                    // Hide Column Headers
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;

                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[3].Visible = false;

                }
            }
            catch (Exception ex)
            {
                ErrorLog.LogErrorMessageToDB("DocumentUpload.aspx", "", "gvWorkOrderDocument_RowDataBound", ex.Message.ToString(), new ACEConnection());
                throw;
            }
        }

        protected void gvWorkOrderDocument_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int fileIDToEdit = Convert.ToInt32(gvWorkOrderDocument.DataKeys[e.NewEditIndex].Value);
            ResponseHelper.Redirect(
                "../DownloadFiles.aspx?File=" + fileIDToEdit.ToString() + "&System=" + "documentdownload" + "", "_blank",
                "scrollbars=yes,menubar=200,width=600,height=300");

        }

        protected void gvWorkOrderDocument_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCustomer.SelectedValue != "")
            {
                PODocumentUploadDL filesDL = new PODocumentUploadDL();
                DataView dView = filesDL.GetWorkOrderDocumentListByContactID(Convert.ToInt32(ddlCustomer.SelectedValue)).Tables[0].DefaultView;
                gvWorkOrderDocument.DataSource = dView;
                gvWorkOrderDocument.DataBind();
            }
            else
            {
                gvWorkOrderDocument.DataSource = null;
                gvWorkOrderDocument.DataBind();
            }
        }

        protected void PI_lbtnCancel_Click(object sender, EventArgs e)
        {
            ddlListContact.ClearSelection();
        }

        protected void ddlListContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
