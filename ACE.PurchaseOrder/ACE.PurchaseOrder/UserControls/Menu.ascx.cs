
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACE.PurchaseOrder.DataLayer;
using ACE.PurchaseOrder.CommonLayer;
using System.Web;

namespace ACE.PurchaseOrder
{
  public partial class Menu : System.Web.UI.UserControl
  {
    #region Private Variables

    MenuDL _menuDL = new MenuDL();
    public string sPageName = "";
    UsersPhotoDL _currentUserPhoto = new UsersPhotoDL();
    int _userID = 0;
    int _companyID = 0;
    int _employeeID = 0;
    #endregion

    #region Private Event(s)

    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
      try
      {
        _userID = Convert.ToInt32(Session["UserID"]);
        _companyID = Convert.ToInt32(Session["CompanyID"]);
        _employeeID = Convert.ToInt32(Session["EmployeeID"]);
        if (!IsPostBack)
        {
          // GetUserPhoto(); commented on 25/06/2014

          if (_userID == 0 && _companyID == 0)
          {
            Response.Redirect("~/Login.aspx", false);
          }
          else
          {
            DataTable dataTable = new DataTable();
            //dataTable.Tables.Add("Table");
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("ParentID", typeof(int));
            dataTable.Columns.Add("Text", typeof(string));
            dataTable.Columns.Add("Url", typeof(string));
            DataTable tree = new DataTable();
            //tree = _menuDL.GetMenuParentItems(_userID, _companyID);
            tree = _menuDL.GetMenuItems(_userID, _companyID);

            foreach (DataRow row in tree.Rows)
            {
              DataRow newrow = dataTable.NewRow();

              newrow["ID"] = row["id"];
              newrow["Url"] = row["Url"].ToString().Replace("..", "~");
              newrow["Text"] = row["Title"];
              if (row["Parent"] != null && row["Title"].ToString() != "Select Company") // For Select company menu removing
              {
                if (row["Parent"].ToString() == "1" || row["Parent"].ToString() == "")
                {
                  newrow["ParentID"] = 0;
                }
                else
                {
                  newrow["ParentID"] = row["Parent"];
                }
              }
              dataTable.Rows.Add(newrow);
            }
            AddItems(ECMenu.Items, 0, dataTable);
          }
          if (_employeeID != 0)
          {
            EmployeeDL employeeDL = new EmployeeDL();
            lblProfileCount.Text = Convert.ToString(employeeDL.GetEmployeeProfileCount(_employeeID));
            imgEmployeeProfileCount.AlternateText = String.Format("{0} Percent Complete", lblProfileCount.Text);
            imgEmployeeProfileCount.Width = Unit.Percentage(Convert.ToInt32(lblProfileCount.Text));
            trProfileCount.Visible = true;
          }
          else
          {
            trProfileCount.Visible = false;
          }
          sPageName = GetPagePath();
          //btnOrderImage.Visible = false;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "Page_Load", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// AddItems
    /// </summary>
    /// <param name="items"></param>
    /// <param name="level"></param>
    /// <param name="dt"></param>
    private void AddItems(MenuItemCollection items, int level, System.Data.DataTable dt)
    {
      try
      {
        string filterExp = string.Format("ParentID='{0}'", level);
        foreach (System.Data.DataRow r in dt.Select(filterExp))
        {
          MenuItem item = new MenuItem()
          {
            Text = r[2].ToString(),
            NavigateUrl = r[3].ToString()
          };
          this.AddItems(item.ChildItems, int.Parse(r[0].ToString()), dt);
          items.Add(item);

        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "AddItems", ex.Message.ToString(), new ACEConnection());
      }

    }

    /// <summary>
    /// ECMenu_MenuItemDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ECMenu_MenuItemDataBound(object sender, MenuEventArgs e)
    {
      try
      {
        //2.map with each node, if yes need to highlight the paritcular node.
        string str = sPageName.Trim();

        str = ".." + str;
        //if (e.Item.ChildItems.Count > 0) e.Item.Selectable = false;

        //if (SiteMap.CurrentNode != null)
        //{
        //    if (SiteMap.CurrentNode.ParentNode.Url == e.Item.NavigateUrl)
        //    {
        //        e.Item.Selected = true;
        //    }
        //}

        if (SiteMap.CurrentNode != null)
        {
          if (e.Item.Selected)
          {
            if (e.Item.Depth == 1)
            {
              e.Item.Parent.Selectable = true;
              e.Item.Parent.Selected = true;
            }
            if (e.Item.Depth == 2)
            {
              e.Item.Parent.Parent.Selectable = true;
              e.Item.Parent.Parent.Selected = true;
            }
            if (e.Item.Depth == 3)
            {
              e.Item.Parent.Parent.Parent.Selectable = true;
              e.Item.Parent.Parent.Parent.Selected = true;
            }
          }
        }
        if (e.Item.NavigateUrl.Trim() == str)
        {
          e.Item.Text = String.Format("<span class=\"selected\">{0}</span>", e.Item.Text);
        }

        if (e.Item.NavigateUrl.IndexOf("ECTranscription/Login.aspx") != -1)
        {
          if (Request.Url.AbsoluteUri.IndexOf("192.2.200.2") != -1)
            e.Item.NavigateUrl = "http://192.2.200.2/ECTranscription/Login.aspx" + "?UserID=" + _userID;
          else
            e.Item.NavigateUrl = "http://125.22.196.83/ECTranscription/Login.aspx" + "?UserID=" + _userID;
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "ECMenu_MenuItemDataBound", ex.Message.ToString(), new ACEConnection());
      }

    }

    /// <summary>
    /// btnUploadPhotoSave_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadPhotoSave_Click(object sender, ImageClickEventArgs e)
    {
      try
      {
        // Create a new Employee Object
        _currentUserPhoto = new UsersPhotoDL();

        // Set whether Add / Edit
        //if (hfUserPhotoID.Value.ToString() != "0")
        //{
        //_currentUserPhoto.AddEditOption = 1;
        //_currentUserPhoto.UserPhotoID = Convert.ToInt32(hfUserPhotoID.Value.ToString());

        //}
        //else
        //{
        //_currentUserPhoto.AddEditOption = 0;
        //_currentUserPhoto.UserPhotoID = 0;
        //}

        _currentUserPhoto.UserID = _userID;

        _currentUserPhoto.Photo = txtImageURL.Value.ToString();

        // Add / Edit the User Photo
        TransactionResult result;
        _currentUserPhoto.ScreenMode = ScreenMode.Add;

        result = _currentUserPhoto.Commit();

        // Display the Status - Whether successfully saved or not
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("<script>alert('" + result.Message.ToString() + ".');");
        sb.Append("</script>");

        ScriptManager.RegisterStartupScript(this.Page, typeof(string), "MyScript", sb.ToString(), false);

        //// If successful get and display the saved Company
        if (result.Status == TransactionStatus.Success)
        {
          GetUserPhoto();
        }

      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "btnUploadPhotoSave_Click", ex.Message.ToString(), new ACEConnection());
      }
    }

    /// <summary>
    /// btnUploadCancel_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnUploadCancel_Click(object sender, ImageClickEventArgs e)
    {
      //mpeAddPhoto.Hide();
    }

    /// <summary>
    /// btnSelect_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
      try
      {

        string str = GetPageName();
        string path;
        if (str == "Default.aspx")
        {
          // path = String.Format(Server.MapPath("~/ImageCropping/AdminImageGalleryPicker.aspx?ControlID={0}&ImageID={1}"), txtImageURL.ClientID, imgAddPhoto.ClientID);
          path = String.Format("/ImageCropping/AdminImageGalleryPicker.aspx?ControlID={0}&ImageID={1}", txtImageURL.ClientID, imgAddPhoto.ClientID);

        }
        else
        {
          path = String.Format("/ImageCropping/AdminImageGalleryPicker.aspx?ControlID={0}&ImageID={1}", txtImageURL.ClientID, imgAddPhoto.ClientID);
        }


        string feature = "alwaysLowered=yes,alwaysRaised=yes,left=3, top=30, height=610, width=1010, status=yes, resizable=no, scrollbars=yes, toolbar=no,location=no, menubar=no";
        string sScript = String.Format("window.open('{0}',null,'{1}');\n", path, feature);
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(String), "someKey", sScript, true);

        //mpeAddPhoto.Show();
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "btnSelect_Click", ex.Message.ToString(), new ACEConnection());
      }

    }

    /// <summary>
    /// btnClear_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClear_Click(object sender, EventArgs e)
    {
      imgAddPhoto.ImageUrl = "../Images/No_photo.jpg";
      txtImageURL.Value = "No Image Selected";
      //mpeAddPhoto.Show();
    }

    #endregion

    #region Private Method(s)

    /// <summary>
    /// GetUserPhoto
    /// </summary>
    private void GetUserPhoto()
    {
      try
      {
        _currentUserPhoto = new UsersPhotoDL(_userID, true);
        //hfUserPhotoID.Value = _currentUserPhoto.UserPhotoID.ToString();
        string picPath = "";
        //if (hfUserPhotoID.Value != "0" && _currentUserPhoto.Photo.ToString() != "")
        //{
        picPath = _currentUserPhoto.Photo.ToString().Replace("../", "");
        // }
        System.IO.FileInfo fi = new System.IO.FileInfo(Server.MapPath(@"~/" + picPath));

        if (!fi.Exists)
        {
          imgAddPhoto.ImageUrl = "~/images/No_photo.jpg";
          // imgPicture.ImageUrl = "~/images/No_photo.jpg";
          // btnAddPhoto.Text = "Add Photo";
          return;
        }
        else
        {
          if (_currentUserPhoto.Photo.Trim().Length != 0)
          {
            imgAddPhoto.ImageUrl = @"~/" + picPath;
            //imgPicture.ImageUrl = @"~/" + picPath;
            // btnAddPhoto.Text = "Change Photo";
          }
          else
          {
            imgAddPhoto.ImageUrl = "~/images/No_photo.jpg";
            // imgPicture.ImageUrl = "~/images/No_photo.jpg";
            //  btnAddPhoto.Text = "Add Photo";
          }
        }
      }
      catch (Exception ex)
      {
        ErrorLog.LogErrorMessageToDB("Menu.ascx", "", "GetUserPhoto", ex.Message.ToString(), new ACEConnection());
      }
    }

    #endregion

    #region Public Method(s)

    /// <summary>
    /// GetPagePath
    /// </summary>
    /// <returns></returns>
    public static string GetPagePath()
    {
      string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
      System.IO.FileInfo fi = new System.IO.FileInfo(path);
      return path;
    }

    /// <summary>
    /// GetPageName
    /// </summary>
    /// <returns></returns>
    public static string GetPageName()
    {
      string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
      System.IO.FileInfo fi = new System.IO.FileInfo(path);
      return fi.Name;
    }
    #endregion
  }
}