using System;

namespace CenturyRayonSchool
{
    public partial class WebsiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //List<PhotoGalleryMenu> listgallerymenu = new List<PhotoGalleryMenu>();
                //listgallerymenu = new PhotoGalleryModel().GetPhotoGalleryMenuItems();




                //foreach (PhotoGalleryMenu pgm in listgallerymenu)
                //{
                //    photogalleryhtml += " <a class=\"dropdown-item\" href=\"" + pgm.urlpage + "\"><span class=\"item-text\">" + pgm.galleryname + "</span></a> " +
                //        "<div class=\"dropdown-divider\"></div>";
                //}



            }
        }
    }
}