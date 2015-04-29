using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using ACE.PurchaseOrder;

namespace ACE.PurchaseOrder
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            if (Server.GetLastError() != null)
            {
                //  SendmailtoAdmin();
                //  Server.ClearError();
                Response.Redirect("~/OrderErrorPage.aspx", false);
            }
        }
    }
}
