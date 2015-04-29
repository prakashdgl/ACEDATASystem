using System;
using System.Net.Mail;
using ACE.PurchaseOrder.CommonLayer;

namespace ACE.PurchaseOrder
{
  public partial class OrderErrorPage : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      //SendmailtoAdmin();
    }

    private void SendmailtoAdmin()
    {
      if (Request.QueryString["aspxerrorpath"] == null) return;
      SmtpClient smtpClient = new SmtpClient();
      MailMessage sm = new MailMessage();

      sm.To.Add("nareindiren.tamizhmani@ACE.in");

      sm.IsBodyHtml = true;
      sm.From = new MailAddress("donotreply@ACE.in");
      sm.Subject = "Error on " + Request.QueryString["aspxerrorpath"];

      sm.Body += "<p><font face='Times New Roman' font-size='12'>" + Server.GetLastError().ToString() + "</font></p>";

      sm.Priority = MailPriority.Normal;
      try
      {
        if (OrderSettings.SendMail)
        {
          smtpClient.Send(sm);
        }
        
      }
      catch (Exception ex)
      {
        String err_str = "Send Email Failed." + ex.Message;
      }
      smtpClient = null;
      sm = null;

    }

  }
}

