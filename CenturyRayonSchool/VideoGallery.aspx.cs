using CenturyRayonSchool.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace CenturyRayonSchool
{
    public partial class VideoGallery : System.Web.UI.Page
    {
        public string gallerynames = "";
        public string galleryimages = "";
        public string galleryname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                PhotoGalleryModel pgm = new PhotoGalleryModel();

                List<PhotoGalleryMenu> listpgm = pgm.GetVideoGalleryMenuItems();


                DataTable gallerytable = new DataTable();
                gallerytable = new PhotoGalleryModel().GetVideoGalleryList("all");

                foreach (PhotoGalleryMenu p in listpgm)
                {
                    galleryname = p.galleryname.Replace(" ", "").Replace("'", "").Replace(".", "").Replace(",", "");
                    gallerynames += "<button data-filter=\"." + galleryname + "\">" + p.galleryname + "</button>";
                }

                foreach (DataRow ro in gallerytable.Rows)
                {
                    galleryname = ro["galleryname"].ToString().Replace(" ", "").Replace("'", "").Replace(".", "").Replace(",", "");

                    galleryimages += "<div class=\"item " + galleryname + " effect-oscar\">" +
                    "<a class='img-magnify' href=\"" + ro["filepath"].ToString() + "\" target=\"_blank\">" +
                    "<video width=\"320\" height=\"240\" controls class=\"attachment-gallery-thumb wp-post-image\" alt=\"" + ro["filename"].ToString() + ">" +
                    "<source src=\"" + ro["filepath"].ToString() + "\" type=\"video/mp4\" >" +
                    "</ video >" +

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