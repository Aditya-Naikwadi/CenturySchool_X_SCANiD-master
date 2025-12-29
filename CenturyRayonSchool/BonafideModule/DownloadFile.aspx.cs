using CenturyRayonSchool.BonafideModule.Reports;
using CenturyRayonSchool.Model;
using CenturyRayonSchool.Properties;
using System;
using System.Data.SqlClient;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        public string fileurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string filename = "", section = "", action = "", grno = "", std = "", div = "", foldername = "";

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

            if (action == "BonafidePrint")
            {
                ShowBonafideReport(grno, std, div);
            }
            else if (action == "BonafidePrintALL")
            {
                PrintAllBonafide(filename, foldername);
            }
        }

        public void ShowBonafideReport(string grno, string std, string div)
        {
            SqlConnection con = null;
            BonafideRepCRS mkrep = new BonafideRepCRS();
            string total = "0", balamt = "0";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    mkrep = new PrintBonafide().PrintBonafidereport(con, grno, std, div);

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Log.Error("DownloadFile.ShowBonafideReport", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
                string folderpath = Server.MapPath("BonafideFile");
                string filename = "Bonafide_" + grno + ".pdf";

                mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/BonafideModule/BonafideFile/" + filename;
            }
        }

        public void PrintAllBonafide(string filename, string foldername)
        {
            string folderpath = Server.MapPath("BonafideFile");
            string weburl = Settings.Default.weburl;
            fileurl = weburl + "/BonafideModule/BonafideFile/" + foldername + "/" + filename;
        }


    }
}