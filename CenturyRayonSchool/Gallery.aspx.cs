//using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CenturyRayonSchool
{
    public partial class Photogallery : System.Web.UI.Page
    {
        public string gallerynames = "";
        public string galleryimages = "";
        public string galleryname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {



                PhotoGalleryModel pgm = new PhotoGalleryModel();

                List<PhotoGalleryMenu> listpgm = pgm.GetPhotoGalleryMenuItems();


                DataTable gallerytable = new DataTable();
                gallerytable = new PhotoGalleryModel().GetPhotoGalleryList("all");

                foreach (PhotoGalleryMenu p in listpgm)
                {
                    galleryname = "c_" + p.galleryname.Replace(" ", "").Replace("'", "").Replace(".", "").Replace(",", "");

                    gallerynames += "<button type=\"button\" data-filter=\"." + galleryname + "\" onclick=\"setGalleryDesc('" + p.description + "')\">" + p.galleryname + "</button>";
                }

                foreach (DataRow ro in gallerytable.Rows)
                {
                    galleryname = "c_" + ro["galleryname"].ToString().Replace(" ", "").Replace("'", "").Replace(".", "").Replace(",", "");

                    galleryimages += "<div class=\"item " + galleryname + " effect-oscar\">" +
                    "<a class=\"img-magnify\" href=\"" + ro["filepath"].ToString() + "\" target=\"_blank\">" +
                    "<img src=\"" + ro["filepath"].ToString() + "\" class=\"attachment-gallery-thumb wp-post-image\" alt=\"" + ro["filename"].ToString() + "\"/>" +
                    "</a>" +
                        "<div class=\"item-description\">" +
                        "<div class=\"item-link\">" +
                        "</div>" +
                    "</div>" +
                    "</div>";

                }



            }
        }
    }
}