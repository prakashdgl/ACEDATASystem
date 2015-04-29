using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ACE.PurchaseOrder.CommonLayer;
using System.Drawing;

namespace ACE.PurchaseOrder.UserControls
{
    public partial class UserMenu : System.Web.UI.UserControl
    {
        #region Variables

        EmployeeBL _employeeBLPhoto;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            int _empID = 0;
            if (Session["LoginName"].ToString() != "")
            {
                //TreeMenu.DataSource = Session["TreeMenu"];
                //TreeMenu.DataBind();
                gvTreeMenu.DataSource = Session["TreeMenu"];
                gvTreeMenu.DataBind();
            }
            _empID = Convert.ToInt32(Session["CreatedByID"].ToString());

            _employeeBLPhoto = new EmployeeBL(_empID);

            if (_employeeBLPhoto.Photo.ToString() != "")
                imgPicture.ImageUrl = _employeeBLPhoto.Photo;
            else
                imgPicture.ImageUrl = "~/Images/No_photo.JPG";
           // lblEmpName.Text = "Welcome " + Convert.ToString(Session["PersonName"].ToString());

            //btnEcafeImage.Attributes.Add("onMouseOver", "this.style.left = parseInt(this.style.top) + -350 + 'px';");
            //btnEcafeImage.Attributes.Add("OnMouseOut", "this.style.top = parseInt(this.style.top) + 350 + 'px';");

            btnEcafeImage.Attributes.Add("onMouseOver", "this.style.left = parseInt(this.style.left) + -2000 + 'px';");
        }

        protected void gvTreeMenu_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                if (Session["SelectedMenuItem"] != null)
                {
                    if (e.Item.ItemIndex == Convert.ToInt32(Session["SelectedMenuItem"].ToString()))
                    {
                        TableCell _tdMenu2 = (TableCell)e.Item.FindControl("tdMenu2");
                        _tdMenu2.CssClass = "menu_blue_text menu_selected";
                        LinkButton _lnkWebSite = (LinkButton)e.Item.FindControl("lnkWebSite");
                        _lnkWebSite.CssClass = "menu_white_text"; 
                    }
                }
            }
        }

        protected void gvTreeMenu_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Session["SelectedMenuItem"] = "";
                Session["SelectedMenuItem"] = e.Item.ItemIndex.ToString();

                string _siteUrl = e.CommandArgument.ToString();
                Response.Redirect(_siteUrl);   
            }
        }
    }
}