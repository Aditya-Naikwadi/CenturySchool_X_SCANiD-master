using CenturyRayonSchool.LCModule.Reports;
//using CenturyRayonSchool.Model;
using CenturyRayonSchool.Properties;
using CrystalDecisions.Shared;
using System;
using System.Data.SqlClient;
using System.IO;

namespace CenturyRayonSchool.LCModule
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        public string fileurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = "", section = "", action = "", grno = "", std = "", div = "", foldername = "", academicyear = "";

            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["grno"] != null)
            {
                grno = Request.QueryString["grno"].ToString();
            }
            if (Request.QueryString["std"] != null)
            {
                std = Request.QueryString["std"].ToString();
            }
            if (Request.QueryString["div"] != null)
            {
                div = Request.QueryString["div"].ToString();
            }
            if (Request.QueryString["Section"] != null)
            {
                section = Request.QueryString["Section"].ToString();
            }
            if (Request.QueryString["filename"] != null)
            {
                filename = Request.QueryString["filename"].ToString();
            }
            if (Request.QueryString["foldername"] != null)
            {
                foldername = Request.QueryString["foldername"].ToString();
            }
            if (Request.QueryString["academicyear"] != null)
            {
                academicyear = Request.QueryString["academicyear"].ToString();
            }

            if (action == "LCPrint")
            {
                if (section == "Primary")
                {
                    showPrimarylc(grno, std, div, academicyear);
                }
                else if (section == "Secondary" || section == "Secondary (Hindi)" || section == "Secondary (Marathi)" || section == "Secondary (SE)" || section == "Secondary(Marathi)" || section == "Secondary(Hindi)")
                {
                    showSecondarylc(grno, std, div, academicyear);
                }
            }
            else if (action == "LCPrintAll")
            {
                PrintAllLC(filename, foldername);
            }
        }

        public void showPrimarylc(string grno, string std, string div, string year)
        {
            SqlConnection con = null;
            LeavingCertificate_CRPS mkrep = new LeavingCertificate_CRPS();
            string total = "0", balamt = "0";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    mkrep = new LCPrint().ShowLCReportPrimary(con, grno, std, div, year);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.showPrimarylc", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                string folderpath = Server.MapPath("LCFile");
                string filename = "PrimaryLC_" + grno + ".pdf";

                //mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                //string weburl = Settings.Default.weburl;

                //fileurl = weburl + "/LCModule/LCFile/" + filename;

                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/LCModule/LCFile/" + filename));
                //Response.End();
                if (mkrep != null)
                {
                    using (var stream = new System.IO.MemoryStream())
                    {
                        mkrep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, $"PrimaryLC_{grno}");
                    }
                }
            }
        }


        public void showSecondarylc(string grno, string std, string div, string year)
        {
            SqlConnection con = null;
            LeavingCertificate_CRHS mkrep = null;
            try
            {
                con = Connection.getConnection();
                con.Open();

                mkrep = new LCPrint().ShowLCReportSecondary(con, grno, std, div, year);
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.showSecondarylc", ex);
                return; // Exit the method if there's an error
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }

            try
            {
                string folderpath = Server.MapPath("~/LCFile");
                string filename = $"SecondaryLC_{grno}.pdf";
                string filepath = System.IO.Path.Combine(folderpath, filename);

                if (mkrep != null)
                {
                    //mkrep.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);

                    //string weburl = Settings.Default.weburl;
                    //string fileurl = $"{weburl}/LCModule/LCFile/{filename}";

                    //Response.ContentType = "application/pdf";
                    //Response.AppendHeader("Content-Disposition", $"attachment; filename={filename}");
                    //Response.TransmitFile(filepath);
                    //Response.End();
                    using (var stream = new System.IO.MemoryStream())
                    {
                        mkrep.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, $"SecondaryLC_{grno}");
                    }
                }
                else
                {
                    // Log or handle the situation when mkrep is null
                    Log.Error("DownloadFile.showSecondarylc", new Exception("Report generation failed."));
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.showSecondarylc - Export/Transmit", ex);
            }
        }

        public void PrintAllLC(string filename, string foldername)
        {
            string folderpath = Server.MapPath("LCFile");
            string weburl = Settings.Default.weburl;
            fileurl = weburl + "/LCModule/LCFile/" + foldername + "/" + filename;
        }
        //public void PrintAllLC(string filename, string foldername)
        //{
        //    string folderpath = Server.MapPath("LCFile") + "\\" + foldername;

        //    // Make sure folder exists
        //    if (!Directory.Exists(folderpath))
        //    {
        //        Directory.CreateDirectory(folderpath);
        //    }

        //    string fullpath = Path.Combine(folderpath, filename);

        //    // Replace with your actual LC report generation logic
        //    // Example: create a Crystal Report and export it
        //    LeavingCertificate_CRHS mkrep = new LeavingCertificate_CRHS();
        //    // You may need to pass parameters to generate the report properly (grno, std, etc.)
        //    // mkrep = new LCPrint().ShowLCReportSecondary(con, grno, std, div, year);

        //    // Export report to disk
        //    mkrep.ExportToDisk(ExportFormatType.PortableDocFormat, fullpath);

        //    string weburl = Settings.Default.weburl;
        //    fileurl = weburl + "/LCModule/LCFile/" + foldername + "/" + filename;

        //    // Optionally return or redirect to the file
        //    Response.Redirect(fileurl, false);
        //}

    }
}