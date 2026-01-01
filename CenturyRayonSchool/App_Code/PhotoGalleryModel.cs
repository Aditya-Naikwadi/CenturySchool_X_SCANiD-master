using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace CenturyRayonSchool.Model
{
    public class PhotoGalleryModel
    {
        public string updatePhotogallery(List<PhotoGalleryTbl> listphoto, string username)
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;
                    DateTime dt = TimeZoneClass.getIndianTimeZoneValues();

                    foreach (PhotoGalleryTbl pgt in listphoto)
                    {
                        query = "insert into PhotoGallery(GalleryName,filename,Filepath,CreatedDate,CreatedBy,description,status) values(@GalleryName,@filename,@Filepath,@CreatedDate,@CreatedBy,@description,@status);";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@GalleryName", pgt.galleryname);
                        cmd.Parameters.AddWithValue("@filename", pgt.filename);
                        cmd.Parameters.AddWithValue("@Filepath", pgt.filepath);
                        cmd.Parameters.AddWithValue("@CreatedDate", dt.ToString("yyyy/MM/dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@CreatedBy", username);
                        cmd.Parameters.AddWithValue("@description", pgt.description);
                        cmd.Parameters.AddWithValue("@status", "active");
                        cmd.ExecuteNonQuery();
                    }

                }

                return "ok";
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.updatePhotogallery", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public DataTable GetPhotoGalleryList(string galname)
        {
            SqlConnection con = null;
            DataTable _photogallery = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    if (galname != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate from PhotoGallery where galleryname='" + galname + "' and [status]='active' order by createddate desc;";
                    }
                    else
                    {
                        query = "select id,galleryname,filename,filepath,createddate from PhotoGallery where [status]='active' order by createddate desc;";
                    }
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_photogallery);



                }

                return _photogallery;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetPhotoGalleryList", ex);
                return _photogallery;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public DataTable GetPhotoGalleryList(string galname, string date)
        {
            SqlConnection con = null;
            DataTable _photogallery = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    if (galname != "all" && date != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate,description,status " +
                            "from PhotoGallery " +
                            "where galleryname='" + galname + "' and Convert(nvarchar(10),Cast(CreatedDate as Date),103)='" + date + "' " +
                            "order by createddate desc;";
                    }
                    else if (galname == "all" && date != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate,description,status " +
                           "from PhotoGallery " +
                           "where Convert(nvarchar(10),Cast(CreatedDate as Date),103)='" + date + "' " +
                           "order by createddate desc;";
                    }
                    else
                    {
                        query = "select id,galleryname,filename,filepath,createddate,description,status from PhotoGallery order by createddate desc;";
                    }
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_photogallery);



                }

                return _photogallery;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetPhotoGalleryList", ex);
                return _photogallery;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }


        public string removeImageFromGallery(string id)
        {
            SqlConnection con = null;
            try
            {

                string query = "";
                using (con = Connection.getConnection())
                {
                    con.Open();
                    query = "Delete from PhotoGallery where id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                }

                return "ok";

            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.removeImageFromGallery", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }

        }


        public List<PhotoGalleryMenu> GetPhotoGalleryMenuItems()
        {
            SqlConnection con = null;
            List<PhotoGalleryMenu> plist = new List<PhotoGalleryMenu>();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    // query = "select Distinct Galleryname from PhotoGallery;";


                    query = "select Distinct GalleryName,Convert(nvarchar(10),cast(CreatedDate as Date),103) as createddate,[description] " +
                           "from PhotoGallery where [status]='active' order by CreatedDate desc";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        plist.Add(new PhotoGalleryMenu()
                        {
                            galleryname = reader["Galleryname"].ToString(),
                            urlpage = "/Gallery.aspx?name=" + reader["Galleryname"].ToString(),
                            description = reader["description"].ToString()

                        }); ;
                    }
                    reader.Close();
                }

                return plist;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetPhotoGalleryMenuItems", ex);
                return plist;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        public DataTable GetVideoGalleryList(string galname)
        {
            SqlConnection con = null;
            DataTable _photogallery = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    if (galname != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate from Videogallery where galleryname='" + galname + "' order by createddate desc;";
                    }
                    else
                    {
                        query = "select id,galleryname,filename,filepath,createddate from Videogallery order by createddate desc;";
                    }
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_photogallery);



                }

                return _photogallery;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetVideoGalleryList", ex);
                return _photogallery;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public DataTable GetVideoGalleryList(string galname, string date)
        {
            SqlConnection con = null;
            DataTable _photogallery = new DataTable();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    if (galname != "all" && date != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate " +
                            "from Videogallery " +
                            "where galleryname='" + galname + "' and Convert(nvarchar(10),Cast(CreatedDate as Date),103)='" + date + "' " +
                            "order by createddate desc;";
                    }
                    else if (galname == "all" && date != "all")
                    {
                        query = "select id,galleryname,filename,filepath,createddate " +
                           "from Videogallery " +
                           "where Convert(nvarchar(10),Cast(CreatedDate as Date),103)='" + date + "' " +
                           "order by createddate desc;";
                    }
                    else
                    {
                        query = "select id,galleryname,filename,filepath,createddate from VideoGallary order by createddate desc;";
                    }
                    cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(_photogallery);



                }

                return _photogallery;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetVideoGalleryList", ex);
                return _photogallery;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public List<PhotoGalleryMenu> GetVideoGalleryMenuItems()
        {
            SqlConnection con = null;
            List<PhotoGalleryMenu> plist = new List<PhotoGalleryMenu>();
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";
                    SqlCommand cmd = null;

                    query = "select Distinct Galleryname from Videogallery;";
                    cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        plist.Add(new PhotoGalleryMenu()
                        {
                            galleryname = reader["Galleryname"].ToString(),
                            urlpage = "/VideoGallery.aspx?name=" + reader["Galleryname"].ToString()

                        });
                    }
                    reader.Close();
                }

                return plist;
            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.GetVideoGalleryMenuItems", ex);
                return plist;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

        public string DisableGallery(string galleryname)
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "update Photogallery set [status]='inactive' where GalleryName=@galleryname;";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@galleryname", galleryname);
                    cmd.ExecuteNonQuery();

                    return "ok";
                }

            }
            catch (Exception ex)
            {
                Log.Error("PhotoGalleryModel.DisableGallery", ex);
                return ex.Message;
            }
            finally
            {
                if (con != null) con.Close();
            }
        }

    }
    public class PhotoGalleryTbl
    {
        public string id { get; set; }
        public string galleryname { get; set; }
        public string filename { get; set; }
        public string filepath { get; set; }
        public string description { get; set; }
    }

    public class PhotoGalleryMenu
    {
        public string galleryname { get; set; }
        public string urlpage { get; set; }

        public string description { get; set; }
    }
}
