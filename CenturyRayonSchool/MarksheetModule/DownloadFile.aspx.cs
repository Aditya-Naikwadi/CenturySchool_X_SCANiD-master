
using CenturyRayonSchool.MarksheetModule.Reports;
//using CenturyRayonSchool.Model;
using CenturyRayonSchool.Properties;
using System;
using System.Data.SqlClient;
using System.IO;

namespace CenturyRayonSchool.MarksheetModule
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        public string fileurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = "", examname = "", action = "", grno = "", std = "", div = "", foldername = "", openingdate = "", resultdate = "", year = "";
            if (Session["ReopenDate"] != null)
            {
                openingdate = Session["ReopenDate"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            if (Session["Resultdate"] != null)
            {
                resultdate = Session["Resultdate"].ToString();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
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
            if (Request.QueryString["examname"] != null)
            {
                examname = Request.QueryString["examname"].ToString();
            }
            if (Request.QueryString["academicyear"] != null)
            {
                year = Request.QueryString["academicyear"].ToString();
            }
            if (Request.QueryString["filename"] != null)
            {
                filename = Request.QueryString["filename"].ToString();
            }
            if (Request.QueryString["foldername"] != null)
            {
                foldername = Request.QueryString["foldername"].ToString();
            }

            if (action == "MarksheetPrint")
            {
                if (std == "IX" || std == "IX (Mar)" || std == "IX (Hindi)" || std == "X" || std == "X (Mar)" || std == "X (Hindi)")
                {
                    show8to9Marksheet(grno, std, div, examname, openingdate, resultdate, year);
                }
                else
                {
                    showPrimaryMarksheet(grno, std, div, examname, openingdate, resultdate, year);
                }
            }
            else if (action == "MarksheetPrintAll")
            {
                printallmarksheet(filename, foldername);
            }
            else if (action == "MarksheetPrint9")
            {
                if (std == "IX" || std == "IX (Mar)" || std == "IX (Hindi)" || std == "X" || std == "X (Mar)" || std == "X (Hindi)")
                {
                    showmarksheet9(grno, std, div, examname, openingdate, resultdate, year);
                }
                if (std == "V" || std == "V (SE)" || std == "VIII" || std == "VIII (Mar)" || std == "VIII (Hindi)" || std == "VIII (SE)")
                {
                    showmarksheet5_8(grno, std, div, examname, openingdate, resultdate, year);
                }
            }

        }

        public void showPrimaryMarksheet(string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            SqlConnection con = null;
            Marksheet1to4 mkrep = new Marksheet1to4();
            Marksheet1to4Sem1 mkrep1 = new Marksheet1to4Sem1();
            string total = "0", filename = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (examname == "Second Semester")
                    {
                        mkrep = new PrintMarksheet().PrintMarksheetPrimary(con, grno, std, div, examname, openingdate, resultdate, year);
                    }
                    else if (examname == "First Semester")
                    {
                        mkrep1 = new PrintMarksheet().PrintMarksheetPrimarySem1(con, grno, std, div, examname, resultdate, year);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.showPrimaryMarksheet", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                //ReportDocument rd = new ReportDocument();
                //rd.Load(Path.Combine(Server.MapPath("~/Marksheetmodule/Reports"), "Marksheet1to4.rpt"));
                //rd.SetDataSource(mkrep);


                string folderpath = Server.MapPath("MarksheetFile");

                if (examname == "Second Semester")
                {
                    filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                    mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                }
                else if (examname == "First Semester")
                {
                    filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                    mkrep1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                }

                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/MarksheetModule/MarksheetFile/" + filename;

              

                //Response.ContentType = "application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/MarksheetFile/" + filename));
                //Response.End();

                if (mkrep != null)
                {
                    mkrep.Close();
                    mkrep.Dispose();
                }
                if (mkrep1 != null)
                {
                    mkrep1.Close();
                    mkrep1.Dispose();
                }

                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                //Response.End();



            }
        }

        public void show8to9Marksheet(string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            SqlConnection con = null;
            ExamReportStd8to9 mkrep = new ExamReportStd8to9();
            ExamReportStd8to9Sem1 mkrep1 = new ExamReportStd8to9Sem1();
            string total = "0", filename = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (examname == "Second Semester")
                    {
                        mkrep = new PrintMarksheet().PrintMarksheet8to9(con, grno, std, div, examname, resultdate, year);
                    }
                    else if (examname == "First Semester")
                    {
                        mkrep1 = new PrintMarksheet().PrintMarksheet8to9Sem1(con, grno, std, div, examname, resultdate, year);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.show8to9Marksheet", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                //ReportDocument rd = new ReportDocument();
                //rd.Load(Path.Combine(Server.MapPath("~/Marksheetmodule/Reports"), "Marksheet1to4.rpt"));
                //rd.SetDataSource(mkrep);


                string folderpath = Server.MapPath("MarksheetFile");

                if (examname == "Second Semester")
                {
                    filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                    mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                }
                else if (examname == "First Semester")
                {
                    filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                    mkrep1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                }
                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/MarksheetModule/MarksheetFile/" + filename;


                if (mkrep != null)
                {
                    mkrep.Close();
                    mkrep.Dispose();
                }
                if (mkrep1 != null)
                {
                    mkrep1.Close();
                    mkrep1.Dispose();
                }
                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                //Response.End();

            }
        }

        public void showmarksheet9(string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            SqlConnection con = null;
            ExamReportStd8to9Sem2 mkrep = new ExamReportStd8to9Sem2();
            string total = "0", filename = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    mkrep = new PrintMarksheet().PrintMarksheet9(con, grno, std, div, examname, resultdate, year);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.show8to9Marksheet", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                string folderpath = Server.MapPath("MarksheetFile");
                filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/MarksheetModule/MarksheetFile/" + filename;


                if (mkrep != null)
                {
                    mkrep.Close();
                    mkrep.Dispose();
                }

            }
        }

        public void printallmarksheet(string filename, string foldername)
        {
            string folderpath = Server.MapPath("MarksheetFile");
            string weburl = Settings.Default.weburl;
            fileurl = weburl + "/MarksheetModule/MarksheetFile/" + foldername + "/" + filename;
        }

        public void showmarksheet5_8(string grno, string std, string div, string examname, string openingdate, string resultdate, string year)
        {
            SqlConnection con = null;
            Marksheet5_8 mkrep = new Marksheet5_8();
            string total = "0", filename = "";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    mkrep = new PrintMarksheet().PrintMarksheet5_8(con, grno, std, div, examname, openingdate, resultdate, year);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.showmarksheet5_8", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                string folderpath = Server.MapPath("MarksheetFile");
                filename = "Marksheet_" + grno + '_' + examname + ".pdf";
                mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);
                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/MarksheetModule/MarksheetFile/" + filename;

                if (mkrep != null)
                {
                    mkrep.Close();
                    mkrep.Dispose();
                }

            }
        }

    }
}