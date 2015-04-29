using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using System.Net;

namespace ACE.PurchaseOrder
{
    public partial class DownloadFiles : System.Web.UI.Page
    {
        string clientFileID;
        string downloadFileType;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clientFileID = Request.QueryString["File"].ToString();
                downloadFileType = Request.QueryString["System"].ToString();
                hfFileID.Value = clientFileID.ToString();

            }
            if (downloadFileType == "documentdownload")
            {
                DownloadDocumentFile();
            }


        }

        private void DownloadDocumentFile()
        {
            PODocumentUploadDL _fileStatus = new PODocumentUploadDL();

            _fileStatus.GetWorkOrderDocumentByWorkOrderDocumentID(Convert.ToInt32(hfFileID.Value.ToString()));

            string path = MapPath(_fileStatus.FilePathandFileName);
            byte[] bts = System.IO.File.ReadAllBytes(path);

            Response.Clear();
            Response.ClearHeaders();

            Response.AddHeader("Content-Type", "Application/octet-stream");
            Response.AddHeader("Content-Length", bts.Length.ToString());
            Response.AddHeader("Content-Disposition", "attachment; filename=" + _fileStatus.FileName);
            Response.BinaryWrite(bts);
            Response.Flush();
            Response.End();



            //WebClient client = new WebClient();
            //client.Credentials = CredentialCache.DefaultCredentials;
            //client.DownloadFileAsync(new Uri(url), @"c:\temp\image35.png");
            ////object documentFile = _fileStatus.DocumentFile;
            ////byte[] bytFile = (byte[])documentFile;

            ////Response.ContentType = "application/vnd.ms-word";
            ////Response.AppendHeader("Content-Disposition", "inline;filename=" + _clientFileStatus.DocumentFileName);
            ////Response.BinaryWrite(bytFile);
            ////Response.End();
        }
    }
}