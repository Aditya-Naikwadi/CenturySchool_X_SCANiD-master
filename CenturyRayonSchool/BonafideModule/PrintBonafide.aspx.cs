using CenturyRayonSchool.BonafideModule.Reports;
//using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.MarksheetModule;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.BonafideModule
{
    public partial class PrintBonafide : System.Web.UI.Page
    {
        DataTable studetails = new DataTable();
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            lblacademicyear.Text = year;

            if (!IsPostBack)
            {
                loadFormControl();

            }

            studetails.Columns.Add("Rollno");
            studetails.Columns.Add("StudentName");
            studetails.Columns.Add("grno");
            studetails.Columns.Add("Section");
            studetails.Columns.Add("std");
            studetails.Columns.Add("div");
        }
        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select std from std where std not in ('ALL','LEFT');";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);
                    std.Rows.Add("Select Std");
                    cmbStd.DataSource = std;
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    cmbStd.SelectedValue = "Select Std";

                    query = "select Div From Div where div Not IN ('ALL');";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);
                    div.Rows.Add("Select Div");
                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    cmbDiv.SelectedValue = "Select Div";
                }
            }
            catch (Exception ex)
            {
                Log.Error("PrintBonafide.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        protected void stdCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbStd.SelectedValue.ToString().Equals("Select Std"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void divCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbDiv.SelectedValue.ToString().Equals("Select Div"))
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void checkALL_CheckedChanged(object sender, EventArgs e)
        {
            if (checkALL.Checked == true)
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = true;
                }
            }
            else
            {
                foreach (GridViewRow row in GridCollection.Rows)
                {
                    ((CheckBox)row.FindControl("chkSelect")).Checked = false;
                }
            }
        }
        protected void cmbDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = null;
            try
            {
                DataTable studtable = new DataTable();
                using (con = Connection.getConnection())
                {
                    con.Open();

                    string query = "", select_std = "", select_div = "", academicyear = "";

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    academicyear = lblacademicyear.Text;

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "';";
                        SqlDataAdapter adap = new SqlDataAdapter(query, con);
                        adap.Fill(studtable);

                        studtable.Rows.Add("ALL", "ALL", "ALL");
                        cmbstudentname.DataSource = studtable;
                        cmbstudentname.DataBind();
                        cmbstudentname.DataTextField = "stuname";
                        cmbstudentname.DataValueField = "grno";
                        cmbstudentname.DataBind();
                        cmbstudentname.SelectedValue = "ALL";

                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("PrintBonafid.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        protected void GetData_ServerClick(object sender, EventArgs e)
        {
            String std = "", div = "", query = "", year = "", grno = "";
            std = cmbStd.SelectedValue.ToString();
            div = cmbDiv.SelectedValue.ToString();
            year = lblacademicyear.Text;
            grno = cmbstudentname.SelectedValue.ToString();
            fillgridview(std, div, year, grno);
        }
        public void fillgridview(string std, string div, string year, string grno)
        {
            int countlc = 0; string query = "";
            studetails.Rows.Clear();
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    if (grno == "ALL")
                    {
                        query = "Select rollno,(fname+' '+mname+' '+lname)as fullname,grno,schoolsection,std,div From studentmaster where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' order by cast(rollno as int) asc;";
                    }
                    else
                    {
                        query = "Select rollno,(fname+' '+mname+' '+lname)as fullname,grno,schoolsection,std,div From studentmaster where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' order by cast(rollno as int) asc;";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        studetails.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                    }
                    GridCollection.DataSource = studetails;
                    GridCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("PrintBonafide.fillgridview", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        public string getBonafideDownloadUrl(string grno, string div, string std, string section)
        {
            return "/BonafideModule/DownloadFile.aspx?action=BonafidePrint&grno=" + grno + "&div=" + div + "&std=" + std + "&Section=" + section;
        }

        public BonafideRepCRS PrintBonafidereport(SqlConnection con, string grno, string std, string div)
        {
            // Initialize academic year
            string academicyear = new FeesModel().setActiveAcademicYear();

            // Initialize the report and data set
            BonafideRepCRS bonafide = new BonafideRepCRS();
            DataSet _bonafideds = new DataSet();

            // SQL query with parameters
            string query = @"
        SELECT TOP 1
            b.serialno,
            b.cardid,
            b.grno,
            b.Lname AS surname,
            b.fname AS studentname,
            b.mname AS fathername,
            b.mothername,
            b.religion,
            b.caste,
            b.subcaste,
            b.nationality,
            CONVERT(VARCHAR(10), TRY_CAST(b.dob AS DATE), 103) AS dateofbirth,
            b.placeofbirth,
            b.saralid,
            b.aadharcard AS aadharno,
            b.shiftname,
            b.std,
            b.div,
            (b.Lname + ' ' + b.fname + ' ' + b.mname + ' ' + b.mothername) AS fullname,
            b.dobwords,
            b.academicyear,
            b.remark,
            ('CJHS-' + b.academicyear + '/' + b.serialno) AS serialno_str,
            CONVERT(VARCHAR(10), TRY_CAST(b.issuedate AS DATE), 103) AS issuedate,
            CONVERT(VARCHAR(10), TRY_CAST(b.doa AS DATE), 103) AS doa,
            b.schoolsection,
            b.validto
        FROM bonafide AS b
        WHERE b.STD = @std
          AND b.DIV = @div
          AND b.grno = @grno
          AND b.academicyear = @academicyear
        ORDER BY TRY_CAST(b.serialno AS INT) DESC;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Add parameters to avoid SQL Injection
                    cmd.Parameters.AddWithValue("@std", std);
                    cmd.Parameters.AddWithValue("@div", div);
                    cmd.Parameters.AddWithValue("@grno", grno);
                    cmd.Parameters.AddWithValue("@academicyear", academicyear);

                    using (SqlDataAdapter adap = new SqlDataAdapter(cmd))
                    {
                        // Fill the DataSet
                        adap.Fill(_bonafideds, "BonafideTable");
                    }
                }

                // Set data source for the report
                bonafide.SetDataSource(_bonafideds.Tables["BonafideTable"]);

                // Optionally set additional parameters for the report if needed
                // bonafide.SetParameterValue("todate", DateTime.Now.ToString("dd/MM/yyyy"));
                // bonafide.SetParameterValue("prigrno", "-");
                // bonafide.SetParameterValue("kggrno", "-");

                return bonafide;
            }
            catch (Exception ex)
            {
                Log.Error("PrintBonafide.PrintBonafidereport", ex);
                return null;
            }
        }

        protected async void Printall_ServerClick(object sender, EventArgs e)
        {
            string select_std = "", select_div = "", grno = "", filename = "", foldername = "";
            select_std = cmbStd.SelectedValue.ToString();
            select_div = cmbDiv.SelectedValue.ToString();
            grno = cmbstudentname.SelectedValue.ToString();
            foldername = select_std + "_" + select_div;
            filename = await startPrint(select_std, select_div, grno, foldername);

            if (filename != "error")
            {

                Response.Redirect("/BonafideModule/DownloadFile.aspx?action=BonafidePrintALL&grno=" + grno + "&div=" + select_div + " &std=" + select_std + "&filename=" + filename + "&foldername=" + foldername);

            }
            else
            {

                lblalertmessage.Text = "Erorr";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
        }

        public async Task<string> startPrint(string select_std, string select_div, string grno, string foldername)
        {
            string filename = "";
            return await Task.Run(() =>
            {
                try
                {

                    select_std = cmbStd.SelectedValue.ToString();
                    select_div = cmbDiv.SelectedValue.ToString();
                    grno = cmbstudentname.SelectedValue.ToString();

                    foldername = select_std + "_" + select_div;
                    filename = printallpdf(select_std, select_div, grno, foldername);


                    return filename;
                }
                catch (Exception ex)
                {
                    Log.Error("PrintBonafide.startPrint", ex);
                    return "error";
                }
            });
        }

        public string printallpdf(string select_std, string select_div, string grno, string foldername)
        {
            string section = "", filepath = "", filname = "";


            string path = Server.MapPath("BonafideFile");
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    BonafideRepCRS mkrep = new BonafideRepCRS();


                    //foldername = select_std + "_" + select_div + "_" + examname;
                    if (Directory.Exists(path))
                    {
                        DeleteDirectory(path);
                    }
                    path += "\\" + foldername;

                    Directory.CreateDirectory(path);

                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        grno = row.Cells[2].Text;
                        section = row.Cells[5].Text;
                        if (((CheckBox)row.FindControl("chkSelect")).Checked)
                        {

                            mkrep = PrintBonafidereport(con, grno, select_std, select_div);

                            filepath = path + @"\" + row.Cells[2].Text.ToString() + ".pdf";
                            if (File.Exists(filepath))
                            {

                                File.SetAttributes(filepath, FileAttributes.Normal);
                                File.Delete(filepath);
                            }
                            mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                            mkrep.Close();
                            mkrep.Dispose();//new code
                            Log.Info(grno + " " + " Pdf Created");


                        }

                    }

                    Pdfmerge pdfmerge = new Pdfmerge();


                    string[] filearray = pdfmerge.getAllpdffiles(path);

                    filepath = path + "\\" + select_std + "_" + select_div + "_Bonafide.pdf";

                    filname = select_std + "_" + select_div + "_Bonafide.pdf";
                    if (File.Exists(filepath))
                    {
                        File.SetAttributes(filepath, FileAttributes.Normal);
                        File.Delete(filepath);

                    }

                    pdfmerge.MergePDF(filepath, filearray);

                    pdfmerge.Dispose();
                    Log.Info("File Successfully Merged");

                    lblinfomsg.Text = "File Created Successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }
                return filname;
            }
            catch (Exception ex)
            {
                Log.Error("PrintBonafide.printallpdf", ex);
                return "error";
            }
        }

        private void DeleteDirectory(string path)
        {
            // Delete all files from the Directory  
            foreach (string filename in Directory.GetFiles(path))
            {
                File.Delete(filename);
            }
            // Check all child Directories and delete files  
            foreach (string subfolder in Directory.GetDirectories(path))
            {
                DeleteDirectory(subfolder);
            }
            Directory.Delete(path);
            Log.Info("Directory deleted successfully");
        }


    }
}