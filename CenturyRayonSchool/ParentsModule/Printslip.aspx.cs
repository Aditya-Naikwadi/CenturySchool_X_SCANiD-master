using CenturyRayonSchool.Model;
using CenturyRayonSchool.Properties;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CenturyRayonSchool.ParentsModule
{
    public partial class Printslip : System.Web.UI.Page
    {
        public string fileurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string academicyear = "", examname = "", action = "", grno = "", std = "", div = "", foldername = "";

            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["GRNO"] != null)
            {
                grno = Request.QueryString["GRNO"].ToString();
            }
            if (Request.QueryString["std"] != null)
            {
                std = Request.QueryString["std"].ToString();
            }
            if (Request.QueryString["academicyear"] != null)
            {
                academicyear = Request.QueryString["academicyear"].ToString();
            }

            if (action == "rcpt")
            {
                showFeeReceipt(academicyear, grno, std);
            }
        }

        public void showFeeReceipt(string academicyear, string grno, string std)
        {
            SqlConnection con = null;
            DataTable dttable = new DataTable();
            string total = "0", balamt = "0";
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();

                    dttable = new ParentsDashboard().printReceiptSlip(con, academicyear, std, grno);
                    foreach (DataRow ro in dttable.Rows)
                    {
                        total = ro["amtpaid"].ToString();
                        balamt = ro["balanceamt"].ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Printslip.showFeeReceipt", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }

                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath("~/FeesModule/Reports"), "Cash_receipt.rpt"));
                rd.SetDataSource(dttable);
                rd.SetParameterValue("Total", total);
                rd.SetParameterValue("balance", balamt);

                string folderpath = Server.MapPath("DownloadReceipt");
                string filename = "FeeReceipt_" + grno + ".pdf";

                rd.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, folderpath + "\\" + filename);

                string weburl = Settings.Default.weburl;

                fileurl = weburl + "/ParentsModule/DownloadReceipt/" + filename;

                //Response.ContentType = "Application/pdf";
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                //Response.TransmitFile(Server.MapPath("~/FeesModule/DownloadFile/" + filename));
                //Response.End();
            }
        }


    }
}