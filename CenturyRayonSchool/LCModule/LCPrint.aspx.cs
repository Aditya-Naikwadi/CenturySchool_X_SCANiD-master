using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.LCModule.Dataset;
using CenturyRayonSchool.LCModule.LCFile;
//using CenturyRayonSchool.LCModule.LCFile;
using CenturyRayonSchool.LCModule.Reports;
using CenturyRayonSchool.MarksheetModule;
using CenturyRayonSchool.Model;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.LCModule
{
    public partial class LCPrint : System.Web.UI.Page
    {
        DataTable studetails = new DataTable();
        Label lblusercode = new Label();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            string year = new FeesModel().setActiveAcademicYear();
            //lblacademicyear.Text = year;

            if (!IsPostBack)
            {
                loadFormControl(year);

            }

            studetails.Columns.Add("Rollno");
            studetails.Columns.Add("StudentName");
            studetails.Columns.Add("grno");
            studetails.Columns.Add("Section");
            studetails.Columns.Add("std");
            studetails.Columns.Add("div");
            studetails.Columns.Add("academicyear");

        }

        public void loadFormControl(string year)
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


                    query = "select [year] From Academicyear order by [status] asc;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable academicyear = new DataTable();
                    adap.Fill(academicyear);
                    academicyear.Rows.Add("Select Year");
                    cmbAcademicyear.DataSource = academicyear;
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.DataTextField = "year";
                    cmbAcademicyear.DataValueField = "year";
                    cmbAcademicyear.DataBind();
                    cmbAcademicyear.SelectedValue = "Select Year";
                    cmbAcademicyear.SelectedValue = year;


                }
            }
            catch (Exception ex)
            {
                Log.Error("LCPrint.loadFormControl", ex);
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

        protected void cmbAcademicyear_SelectedIndexChanged(object sender, EventArgs e)
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
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

                    if (select_std != "Select Std" && select_div != "Select Div")
                    {
                        query = "select grno,fullname,(grno +' / '+ fullname) as stuname from studentmaster where std='" + select_std + "' and div='" + select_div + "' and academicyear='" + academicyear + "' ;";
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
                Log.Error("SubjectMarks.cmbDiv_SelectedIndexChanged", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
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
                    academicyear = cmbAcademicyear.SelectedValue.ToString();

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
                Log.Error("LCPrint.cmbDiv_SelectedIndexChanged", ex);
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
            year = cmbAcademicyear.SelectedValue.ToString();
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
                        query = "Select rollno,(fname+' '+mname+' '+lname)as fullname,grno,schoolsection,std,div,academicyear From LeavingCertificate where std='" + std + "' and div='" + div + "' and academicyear='" + year + "' order by cast(rollno as int) asc;";
                    }
                    else
                    {
                        query = "Select rollno,(fname+' '+mname+' '+lname)as fullname,grno,schoolsection,std,div,academicyear From LeavingCertificate where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "' order by cast(rollno as int) asc;";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        studetails.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                    }
                    GridCollection.DataSource = studetails;
                    GridCollection.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Error("LCPrint.fillgridview", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }
        public string getDownloadUrl(string grno, string div, string std, string section, string year)
        {
            return "/LCModule/DownloadFile.aspx?action=LCPrint&grno=" + grno + "&div=" + div + "&std=" + std + "&Section=" + section + "&academicyear=" + year;
        }



        public async Task<string> startPrint(string select_std, string select_div, string grno, string foldername, string year)
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
                    filename = printallpdf(select_std, select_div, grno, foldername, year);


                    return filename;
                }
                catch (Exception ex)
                {
                    Log.Error("PrintMarksheet.startPrint", ex);
                    return "error";
                }
            });
        }

        public string printallpdf(string select_std, string select_div, string grno, string foldername, string year)
        {
            string section = "", filepath = "", filname = "";


            string path = Server.MapPath("LCFile");
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                   // LeavingCertificate_CRHS mkrep = new LeavingCertificate_CRHS();
                    LeavingCertificate_CRPS mkrep1 = new LeavingCertificate_CRPS();

                    //foldername = select_std + "_" + select_div + "_" + examname;
                    //if (Directory.Exists(path))
                    //{
                    //    DeleteDirectory(path);
                    //}
                    path += "\\" + foldername;
                    if (Directory.Exists(path))
                    {
                        DeleteDirectory(path);
                    }
                    Directory.CreateDirectory(path);

                    foreach (GridViewRow row in GridCollection.Rows)
                    {
                        grno = row.Cells[2].Text;
                        section = row.Cells[5].Text;
                        if (((CheckBox)row.FindControl("chkSelect")).Checked)
                        {
                            if (section == "Primary")
                            {

                                mkrep1 = ShowLCReportPrimary(con, grno, select_std, select_div, year);

                                filepath = path + @"\" + row.Cells[2].Text.ToString() + ".pdf";
                                if (File.Exists(filepath))
                                {

                                    File.SetAttributes(filepath, FileAttributes.Normal);
                                    File.Delete(filepath);
                                }
                                mkrep1.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);
                                mkrep1.Close();
                                mkrep1.Dispose();//new code
                                Log.Info(grno + " " + " Pdf Created");
                            }
                            else
                            {
                                //mkrep = ShowLCReportSecondary(con, grno, select_std, select_div, year);

                                //filepath = path + @"\" + row.Cells[2].Text.ToString() + ".pdf";
                                //if (File.Exists(filepath))
                                //{

                                //    File.SetAttributes(filepath, FileAttributes.Normal);
                                //    File.Delete(filepath);
                                //}
                                ////mkrep.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, filepath);

                                ////mkrep.Close();
                                ////mkrep.Dispose();//new code

                                //// Export the report to a stream (e.g., MemoryStream)
                                //Stream stream = (Stream)mkrep.ExportToStream(ExportFormatType.PortableDocFormat);
                                //// Save the stream to a file
                                //using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
                                //{
                                //    stream.CopyTo(fileStream);
                                //}
                                //// Clean up resources
                                //mkrep.Close();
                                //mkrep.Dispose();
                                //stream.Close();
                                //stream.Dispose();

                                //Log.Info(grno + " " + " Pdf Created");
                                using (ReportDocument mkrep = ShowLCReportSecondary(con, grno, select_std, select_div, year))
                                {
                                    filepath = Path.Combine(path, grno + ".pdf");
                                    if (File.Exists(filepath))
                                    {
                                        File.SetAttributes(filepath, FileAttributes.Normal);
                                        File.Delete(filepath);
                                    }

                                    using (Stream stream = mkrep.ExportToStream(ExportFormatType.PortableDocFormat))
                                    using (FileStream fileStream = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                                    {
                                        stream.CopyTo(fileStream);
                                    }
                                }
                                Log.Info(grno + " Pdf Created");

                            }
                        }

                    }

                    Pdfmerge pdfmerge = new Pdfmerge();


                    string[] filearray = pdfmerge.getAllpdffiles(path);

                    filepath = path + "\\" + select_std + "_" + select_div + "_LC.pdf";

                    filname = select_std + "_" + select_div + "_LC.pdf";
                    if (File.Exists(filepath))
                    {
                        File.SetAttributes(filepath, FileAttributes.Normal);
                        File.Delete(filepath);

                    }
                    try
                    {
                        pdfmerge.MergePDF(filepath, filearray);
                    }
                    catch (Exception ex)
                    {
                        Log.Error("Error during PDF merge", ex);
                    }

                    //     pdfmerge.MergePDF(filepath, filearray);

                    pdfmerge.Dispose();
                    Log.Info("File Successfully Merged");
                    lblinfomsg.Text = "File Created Successfully.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                }
                return filname;
            }
            catch (Exception ex)
            {
                Log.Error("PrintMarksheet.printallpdf", ex);
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
        public LeavingCertificate_CRPS ShowLCReportPrimary(SqlConnection con, string grno, string std, string div, string year)
        {
            //string year = new FeesModel().setActiveAcademicYear(); 
            Leavingds lcds = new Leavingds();
            LeavingCertificate_CRPS mkrep = new LeavingCertificate_CRPS();
            try
            {
                string query = "Select (format (Convert(int,LCNo), '0###')+'/P/'+ academicyear) as LCNo,Cardid,Grno,Lname as [Surname],Fname as [Studentname],Mname as [Fathername],Mothername,Religion,Caste,Category,Nationality,CONVERT(VARCHAR(10),Cast(dob as Date), 103) as [DateofBirth],PlaceofBirth,Lastschool,CONVERT(VARCHAR(10),Cast(DOA as Date), 103) as [DateofAdmission],Progress,CONVERT(VARCHAR(10),Cast(DateofLeaving as Date), 103) as DateofLeaving,stdstudying as [Stdstudying],Reasonforleaving,saralid,aadharcard as [aadharno],Subcaste,birthtaluka as [taluka],birthdistrict as [district],birthstate as [state],birthcountry as [country],dobwords,admissionstd,conduct,remark,mothertongue,freeshiptype,photopath,apaar_id,pen_no " +
                               "From LeavingCertificate where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(lcds.Tables["Leaving"]);

                mkrep.SetDataSource(lcds.Tables["Leaving"]);
                return mkrep;
            }
            catch (Exception ex)
            {
                Log.Error("LCPrint.ShowLCReport", ex);
                return null;
            }
        }

        public LeavingCertificate_CRHS ShowLCReportSecondary(SqlConnection con, string grno, string std, string div, string year)
        {
            //string year = new FeesModel().setActiveAcademicYear();
            Leavingds lcds = new Leavingds();
            LeavingCertificate_CRHS mkrep = new LeavingCertificate_CRHS();
            try
            {
                string query = "Select (format (Convert(int,LCNo), '0###')+'/S/'+ academicyear) as LCNo,Cardid,Grno,Lname as [Surname],Fname as [Studentname],Mname as [Fathername],Mothername,Religion,Caste,Category,Nationality,CONVERT(VARCHAR(10),Cast(dob as Date), 103) as [DateofBirth],PlaceofBirth,Lastschool,CONVERT(VARCHAR(10),Cast(DOA as Date), 103) as [DateofAdmission],Progress,CONVERT(VARCHAR(10),Cast(DateofLeaving as Date), 103) as DateofLeaving,stdstudying as [Stdstudying],Reasonforleaving,saralid,aadharcard as [aadharno],Subcaste,birthtaluka as [taluka],birthdistrict as [district],birthstate as [state],birthcountry as [country],dobwords,admissionstd,conduct,remark,mothertongue,freeshiptype,photopath,apaar_id,pen_no " +
                              "From LeavingCertificate where std='" + std + "' and div='" + div + "' and grno='" + grno + "' and academicyear='" + year + "';";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(lcds.Tables["Leaving"]);
                mkrep.SetDataSource(lcds.Tables["Leaving"]);
                return mkrep;
            }
            catch (Exception ex)
            {
                Log.Error("LCPrint.ShowLCReportSecondary", ex);
                return null;
            }
        }

        protected async void Printall_ServerClick(object sender, EventArgs e)
        {
            string select_std = "", select_div = "", grno = "", filename = "", foldername = "", year = "";
            select_std = cmbStd.SelectedValue.ToString();
            select_div = cmbDiv.SelectedValue.ToString();
            grno = cmbstudentname.SelectedValue.ToString();
            year = cmbAcademicyear.SelectedValue.ToString();
            foldername = select_std + "_" + select_div;
            filename = await startPrint(select_std, select_div, grno, foldername, year);

            if (filename != "error")
            {
                Response.Redirect("/LCModule/DownloadFile.aspx?action=LCPrintAll&grno=" + grno + "&div=" + select_div + " &std=" + select_std + "&filename=" + filename + "&foldername=" + foldername);

            }
            else
            {

                lblalertmessage.Text = "Erorr";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
            }
        }
    }
}