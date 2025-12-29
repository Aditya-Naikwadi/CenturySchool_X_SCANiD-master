using CenturyRayonSchool.FeesModule.Model;
using CenturyRayonSchool.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class StudentList : System.Web.UI.Page
    {
        DataTable uistudenttable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            uistudenttable.Columns.Add("Srno");
            uistudenttable.Columns.Add("Fullname");
            uistudenttable.Columns.Add("Grno");
            uistudenttable.Columns.Add("STD");
            uistudenttable.Columns.Add("DIV");
            uistudenttable.Columns.Add("DOB");
            uistudenttable.Columns.Add("CARDID");

            if (!IsPostBack)
            {
                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;

                BindGrid(year);

                loadFormControl();
                cmbAcademicyear.Text = year;
            }



            //allStudentsGrid.DataSource = uistudenttable;
            //allStudentsGrid.DataBind();
        }



        public void BindGrid(string academicyear)
        {
            SqlConnection con = null;
            try
            {

                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select ROW_NUMBER() OVER(ORDER BY grno ASC) as srno,fullname,grno,std,div,DOB,CARDID,academicyear from studentmaster where academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '');";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(uistudenttable);

                    allStudentsGrid.DataSource = uistudenttable;
                    allStudentsGrid.DataBind();

                    allStudentsGrid.UseAccessibleHeader = true;
                    allStudentsGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

                    studCount.InnerHtml = uistudenttable.Rows.Count.ToString();
                }


            }
            catch (Exception ex)
            {
                Log.Error("BindGrid", ex);

            }
            finally
            {
                if (con != null) { con.Close(); }
            }




        }

        public void loadFormControl()
        {
            SqlConnection con = null;
            try
            {
                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "select std from std where std not in ('LEFT');";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    DataTable std = new DataTable();
                    adap.Fill(std);

                    cmbStd.DataSource = std;
                    cmbStd.DataBind();
                    cmbStd.DataTextField = "std";
                    cmbStd.DataValueField = "std";
                    cmbStd.DataBind();
                    cmbStd.SelectedValue = "ALL";

                    query = "select Div From Div;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable div = new DataTable();
                    adap.Fill(div);

                    cmbDiv.DataSource = div;
                    cmbDiv.DataBind();
                    cmbDiv.DataTextField = "Div";
                    cmbDiv.DataValueField = "Div";
                    cmbDiv.DataBind();
                    cmbDiv.SelectedValue = "ALL";

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



                }
            }
            catch (Exception ex)
            {
                Log.Error("StudentList.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }


        public ExcelPackage generateStudentExcel(string std, string div, string academicyear)
        {
            SqlConnection con = null;
            try
            {
                //Creating excel sheet in file
                ExcelPackage excel = new ExcelPackage();
                var workSheet = excel.Workbook.Worksheets.Add("StudentMaster");

                // setting the properties
                // of the work sheet 
                workSheet.TabColor = System.Drawing.Color.Black;
                workSheet.DefaultRowHeight = 12;

                // Setting the properties
                // of the first row
                workSheet.Row(1).Height = 20;
                workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                workSheet.Row(1).Style.Font.Bold = true;

                workSheet.Cells[1, 1].Value = "FNAME";
                workSheet.Cells[1, 2].Value = "MNAME";
                workSheet.Cells[1, 3].Value = "LNAME";
                workSheet.Cells[1, 4].Value = "MOTHERNAME";
                workSheet.Cells[1, 5].Value = "SHIFTNAME";
                workSheet.Cells[1, 6].Value = "STD";
                workSheet.Cells[1, 7].Value = "DIV";
                workSheet.Cells[1, 8].Value = "ROLLNO";
                workSheet.Cells[1, 9].Value = "GRNO";
                workSheet.Cells[1, 10].Value = "GENDER";
                workSheet.Cells[1, 11].Value = "DOB";
                workSheet.Cells[1, 12].Value = "RELIGION";
                workSheet.Cells[1, 13].Value = "CASTE";
                workSheet.Cells[1, 14].Value = "subcaste";
                workSheet.Cells[1, 15].Value = "CATEGORY";
                workSheet.Cells[1, 16].Value = "MOBILE";
                workSheet.Cells[1, 17].Value = "contact2";
                workSheet.Cells[1, 18].Value = "EMAIL";
                workSheet.Cells[1, 19].Value = "CARDID";
                workSheet.Cells[1, 20].Value = "sms";
                workSheet.Cells[1, 21].Value = "ADDRESS";
                workSheet.Cells[1, 22].Value = "CITY";
                workSheet.Cells[1, 23].Value = "PIN";
                workSheet.Cells[1, 24].Value = "STATE";
                workSheet.Cells[1, 25].Value = "BLOODGROUP";
                workSheet.Cells[1, 26].Value = "fullname";
                //worksheet.Cells[2, 26] = "=CONCATENATE(A2,B2,C2,D2)";
                workSheet.Cells[1, 27].Value = "photopath";
                workSheet.Cells[1, 28].Value = "AadharCard";
                workSheet.Cells[1, 29].Value = "Saralid";
                workSheet.Cells[1, 30].Value = "bankname";
                workSheet.Cells[1, 31].Value = "bankacc";
                workSheet.Cells[1, 32].Value = "cid";
                workSheet.Cells[1, 33].Value = "ispromoted";
                workSheet.Cells[1, 34].Value = "fingerid";
                workSheet.Cells[1, 35].Value = "freeshiptype";
                workSheet.Cells[1, 36].Value = "status";
                workSheet.Cells[1, 37].Value = "admissiontype";

                workSheet.Cells[1, 38].Value = "subjects";
                workSheet.Cells[1, 39].Value = "placeofbirth";
                workSheet.Cells[1, 40].Value = "birthtaluka";
                workSheet.Cells[1, 41].Value = "birthdistrict";
                workSheet.Cells[1, 42].Value = "birthstate";
                workSheet.Cells[1, 43].Value = "birthcountry";
                workSheet.Cells[1, 44].Value = "mothertongue";
                workSheet.Cells[1, 45].Value = "Nationality";
                workSheet.Cells[1, 46].Value = "Lastschool";
                workSheet.Cells[1, 47].Value = "Progress";
                workSheet.Cells[1, 48].Value = "DateofLeaving";
                workSheet.Cells[1, 49].Value = "Reasonforleaving";
                workSheet.Cells[1, 50].Value = "LCNo";

                workSheet.Cells[1, 51].Value = "conduct";
                workSheet.Cells[1, 52].Value = "remark";
                workSheet.Cells[1, 53].Value = "dobwords";
                workSheet.Cells[1, 54].Value = "admissionstd";
                workSheet.Cells[1, 55].Value = "DOA";

                workSheet.Cells[1, 56].Value = "STUDENTID";
                workSheet.Cells[1, 57].Value = "accountname";
                workSheet.Cells[1, 58].Value = "IQLD";

                workSheet.Cells[1, 59].Value = "schoolsection";
                workSheet.Cells[1, 60].Value = "leftstatus";
                workSheet.Cells[1, 61].Value = "academicyear";
                workSheet.Cells[1, 62].Value = "stdstudying";
                workSheet.Cells[1, 63].Value = "house";
                workSheet.Cells[1, 64].Value = "feesinstallment";
                workSheet.Cells[1, 65].Value = "entrydate";
                workSheet.Cells[1, 66].Value = "uniformid";
                workSheet.Cells[1, 67].Value = "stdstudyingInWords";

                using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 67])
                {
                    //Rng.Value = "Text Color & Background Color";
                    //Rng.Merge = true;
                    Rng.Style.Font.Bold = true;
                    //Rng.Style.Font.Color.SetColor(Color.Red);
                    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Rng.Style.Fill.BackgroundColor.SetColor(Color.Green);
                }

                //filling excel sheet with data

                using (con = Connection.getConnection())
                {
                    con.Open();
                    String query = "";


                    if (std.ToLower().Equals("all") && div.ToLower().Equals("all"))
                    {
                        query = "Select [Fname],[Mname],[Lname],[Mothername],[Shiftname],[Std],[Div],[Rollno],[Grno],[Gender],Convert(nvarchar(10),[Dob]),[Religion],[Caste],[Subcaste],[Category],[Mobile],[Contact2]," +
                      "[Email],[Cardid],[Sms],[Address],[City],[Pin],[State],[Bloodgroup],[photopath],[aadharcard],[saralid],fullname,bankname,bankacc,cid,ispromoted,fingerid,freeshiptype,status,admissiontype,subjects,placeofbirth," +
                      "birthtaluka,birthdistrict,birthstate,birthcountry,mothertongue,Nationality,Lastschool,Progress,DateofLeaving,Reasonforleaving,LCNo,conduct,remark,dobwords,admissionstd,DOA,STUDENTID,accountname,IQLD,schoolsection,leftstatus,academicyear,stdstudying,house,feesinstallment,entrydate,uniformid,stdstudyingInWords From StudentMaster " +
                      "where academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') order by std,div,rollno;";

                    }
                    else if (div.ToLower().Equals("all"))
                    {
                        query = "Select [Fname],[Mname],[Lname],[Mothername],[Shiftname],[Std],[Div],[Rollno],[Grno],[Gender],Convert(nvarchar(10),[Dob]),[Religion],[Caste],[Subcaste],[Category],[Mobile],[Contact2]," +
                        "[Email],[Cardid],[Sms],[Address],[City],[Pin],[State],[Bloodgroup],[photopath],[aadharcard],[saralid],fullname,bankname,bankacc,cid,ispromoted,fingerid,freeshiptype,status,admissiontype,subjects,placeofbirth," +
                        "birthtaluka,birthdistrict,birthstate,birthcountry,mothertongue,Nationality,Lastschool,Progress,DateofLeaving,Reasonforleaving,LCNo,conduct,remark,dobwords,admissionstd,DOA,STUDENTID,accountname,IQLD,schoolsection,leftstatus,academicyear,stdstudying,house,feesinstallment,entrydate,uniformid,stdstudyingInWords From StudentMaster " +
                        "where std='" + std + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') order by std,div,rollno;";
                    }
                    else
                    {
                        query = "Select [Fname],[Mname],[Lname],[Mothername],[Shiftname],[Std],[Div],[Rollno],[Grno],[Gender],Convert(nvarchar(10),[Dob]),[Religion],[Caste],[Subcaste],[Category],[Mobile],[Contact2]," +
                           "[Email],[Cardid],[Sms],[Address],[City],[Pin],[State],[Bloodgroup],[photopath],[aadharcard],[saralid],fullname,bankname,bankacc,cid,ispromoted,fingerid,freeshiptype,status,admissiontype,subjects,placeofbirth," +
                           "birthtaluka,birthdistrict,birthstate,birthcountry,mothertongue,Nationality,Lastschool,Progress,DateofLeaving,Reasonforleaving,LCNo,conduct,remark,dobwords,admissionstd,DOA,STUDENTID,accountname,IQLD,schoolsection,leftstatus,academicyear,stdstudying,house,feesinstallment,entrydate,uniformid,stdstudyingInWords From StudentMaster " +
                           "where std='" + std + "' and div='" + div + "' and academicyear='" + academicyear + "' and (leftstatus IS NULL OR leftstatus = '') order by std,div,rollno;";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int i = 2;
                        while (reader.Read())
                        {

                            workSheet.Cells[i, 1].Value = reader[0].ToString();
                            workSheet.Cells[i, 2].Value = reader[1].ToString();
                            workSheet.Cells[i, 3].Value = reader[2].ToString();
                            workSheet.Cells[i, 4].Value = reader[3].ToString();
                            workSheet.Cells[i, 5].Value = reader[4].ToString();
                            workSheet.Cells[i, 6].Value = reader[5].ToString();
                            workSheet.Cells[i, 7].Value = reader[6].ToString();
                            workSheet.Cells[i, 8].Value = reader[7].ToString();
                            workSheet.Cells[i, 9].Value = reader[8].ToString();
                            workSheet.Cells[i, 10].Value = reader[9].ToString();
                            workSheet.Cells[i, 11].Value = reader[10].ToString();
                            workSheet.Cells[i, 12].Value = reader[11].ToString();
                            workSheet.Cells[i, 13].Value = reader[12].ToString();
                            workSheet.Cells[i, 14].Value = reader[13].ToString();
                            workSheet.Cells[i, 15].Value = reader[14].ToString();
                            workSheet.Cells[i, 16].Value = reader[15].ToString();
                            workSheet.Cells[i, 17].Value = reader[16].ToString();
                            workSheet.Cells[i, 18].Value = reader[17].ToString();
                            workSheet.Cells[i, 19].Value = reader[18].ToString();
                            workSheet.Cells[i, 20].Value = reader[19].ToString();
                            workSheet.Cells[i, 21].Value = reader[20].ToString();
                            workSheet.Cells[i, 22].Value = reader[21].ToString();
                            workSheet.Cells[i, 23].Value = reader[22].ToString();
                            workSheet.Cells[i, 24].Value = reader[23].ToString();
                            workSheet.Cells[i, 25].Value = reader[24].ToString();

                            workSheet.Cells[i, 27].Value = reader[25].ToString();
                            workSheet.Cells[i, 28].Value = "'" + reader[26].ToString();
                            workSheet.Cells[i, 29].Value = "'" + reader[27].ToString();
                            workSheet.Cells[i, 26].Value = reader[28].ToString();
                            workSheet.Cells[i, 30].Value = reader[29].ToString();
                            workSheet.Cells[i, 31].Value = reader[30].ToString();
                            workSheet.Cells[i, 32].Value = reader[31].ToString();
                            workSheet.Cells[i, 33].Value = reader[32].ToString();
                            workSheet.Cells[i, 34].Value = reader[33].ToString();
                            workSheet.Cells[i, 35].Value = reader[34].ToString();
                            workSheet.Cells[i, 36].Value = reader[35].ToString();

                            workSheet.Cells[i, 37].Value = reader[36].ToString();
                            workSheet.Cells[i, 38].Value = reader[37].ToString();
                            workSheet.Cells[i, 39].Value = reader[38].ToString();
                            workSheet.Cells[i, 40].Value = reader[39].ToString();
                            workSheet.Cells[i, 41].Value = reader[40].ToString();
                            workSheet.Cells[i, 42].Value = reader[41].ToString();
                            workSheet.Cells[i, 43].Value = reader[42].ToString();
                            workSheet.Cells[i, 44].Value = reader[43].ToString();
                            workSheet.Cells[i, 45].Value = reader[44].ToString();
                            workSheet.Cells[i, 46].Value = reader[45].ToString();
                            workSheet.Cells[i, 47].Value = reader[46].ToString();
                            workSheet.Cells[i, 48].Value = reader[47].ToString();
                            workSheet.Cells[i, 49].Value = reader[48].ToString();
                            workSheet.Cells[i, 50].Value = reader[49].ToString();
                            workSheet.Cells[i, 51].Value = reader[50].ToString();
                            workSheet.Cells[i, 52].Value = reader[51].ToString();
                            workSheet.Cells[i, 53].Value = reader[52].ToString();
                            workSheet.Cells[i, 54].Value = reader[53].ToString();
                            workSheet.Cells[i, 55].Value = reader[54].ToString();
                            workSheet.Cells[i, 56].Value = reader[55].ToString();
                            workSheet.Cells[i, 57].Value = reader[56].ToString();
                            workSheet.Cells[i, 58].Value = reader[57].ToString();
                            workSheet.Cells[i, 59].Value = reader[58].ToString();
                            workSheet.Cells[i, 60].Value = reader[59].ToString();
                            workSheet.Cells[i, 61].Value = reader[60].ToString();
                            workSheet.Cells[i, 62].Value = reader[61].ToString();
                            workSheet.Cells[i, 63].Value = reader[62].ToString();
                            workSheet.Cells[i, 64].Value = reader[63].ToString();

                            workSheet.Cells[i, 65].Value = reader[64].ToString();
                            workSheet.Cells[i, 66].Value = reader[65].ToString();
                            workSheet.Cells[i, 67].Value = reader[66].ToString();
                            i++;

                        }
                    }
                    reader.Close();

                }

                return excel;


            }
            catch (Exception ex)
            {
                Log.Error("StudentList.GenerateStudentExcel", ex);
                return null;
            }
            finally
            {
                if (con != null)
                { con.Close(); }
            }

        }

        protected void btnGenerateExcel_ServerClick(object sender, EventArgs e)
        {
            string academicyear = cmbAcademicyear.Text;
            string std = cmbStd.Text;
            string div = cmbDiv.Text;
            ExcelPackage excel = new ExcelPackage();

            excel = generateStudentExcel(std, div, academicyear);

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //here i have set filname as Students.xlsx

                Response.AddHeader("content-disposition", "attachment;  filename=StudentMaster_" + std + "_" + div + "_" + academicyear + ".xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnUploadExcel_ServerClick(object sender, EventArgs e)
        {
            try
            {

                if (FileUpload1.HasFile)
                {
                    string folderpath = Server.MapPath("~/FeesModule/Excel");
                    FileUpload1.SaveAs(folderpath + "//" + FileUpload1.FileName);

                    string filepath = folderpath + "//" + FileUpload1.FileName;

                    string resp = uploadStudent(filepath);

                    if (resp == "ok")
                    {
                        lblinfomsg.Text = "Student Excel Uploaded Successfully.";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                    }
                    else
                    {
                        lblalertmessage.Text = resp;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                    }

                }
                else
                {
                    //Label1.Text = "No File Uploaded.";
                    lblalertmessage.Text = "No File Uploaded.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showAlertModal();", true);
                }
            }
            catch (Exception ex)
            {
                Log.Error("StudentList.btnUploadExcel_ServerClick", ex);
            }
        }


        public string uploadStudent(String filepath)
        {
            Log.Info("uploadStudent process started");
            SqlConnection con = null;
            try
            {
                Boolean entry = true;

                String[] excelcolumn = new String[67] { "FNAME", "MNAME", "LNAME", "MOTHERNAME", "SHIFTNAME", "STD", "DIV", "ROLLNO", "GRNO", "GENDER", "DOB", "RELIGION", "CASTE", "subcaste", "CATEGORY", "MOBILE", "contact2", "EMAIL", "CARDID", "sms", "ADDRESS", "CITY", "PIN", "STATE", "BLOODGROUP", "fullname", "photopath", "AadharCard", "Saralid", "bankname", "bankacc", "cid", "ispromoted", "fingerid", "freeshiptype", "status", "admissiontype", "subjects", "placeofbirth", "birthtaluka", "birthdistrict", "birthstate", "birthcountry", "mothertongue", "Nationality", "Lastschool", "Progress", "DateofLeaving", "Reasonforleaving", "LCNo", "conduct", "remark", "dobwords", "admissionstd", "DOA", "STUDENTID", "accountname", "IQLD", "schoolsection", "leftstatus", "academicyear", "stdstudying", "house", "feesinstallment", "entrydate", "uniformid", "stdstudyingInWords" };
                String[] excelcell = new String[67];
                // int rCnt = 0;
                // int cCnt = 0;

                if (filepath.Length > 0)
                {
                    ExcelPackage package = null;
                    using (package = new ExcelPackage(filepath))
                    {

                        //get the first worksheet in the workbook
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["StudentMaster"];
                        int colCount = worksheet.Dimension.End.Column;  //get Column Count
                        int rowCount = worksheet.Dimension.End.Row;     //get row count

                        using (con = Connection.getConnection())
                        {
                            con.Open();
                            for (int rCnt = 2; rCnt <= rowCount; rCnt++)
                            {
                                entry = true;
                                for (int cCnt = 1; cCnt <= colCount; cCnt++)
                                {
                                    if (cCnt == 1 || cCnt == 2 || cCnt == 3 || cCnt == 4)// check name
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == null)
                                        {
                                        }
                                        else
                                        {
                                            if (text.Length > 0)
                                            {
                                                text = text.Trim();
                                            }
                                        }

                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            if (cCnt == 1 || cCnt == 2)
                                            {
                                                String nameerror = "Student:Compulsory field name";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + nameerror);
                                                //gridexcel.Rows.Add(cCnt, rCnt, text, nameerror);
                                                entry = false;
                                            }
                                            else if (cCnt == 3 || cCnt == 4)
                                            {
                                                text = "";
                                                excelcell[cCnt - 1] = text;
                                            }
                                        }
                                        else
                                        {
                                            Boolean nameStat = checkname(text);
                                            if (nameStat)
                                            {
                                                excelcell[cCnt - 1] = text;
                                            }
                                            else
                                            {
                                                String nameerror = "Student:Error in name";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + nameerror);
                                                entry = false;
                                            }

                                        }

                                    }

                                    if (cCnt == 11) // Checking Date DOB
                                    {
                                        DateTime excelDat;
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {

                                            String dateerror = "Student:Compulsory field Date";
                                            Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + dateerror);
                                            entry = false;
                                        }
                                        else
                                        {

                                            Boolean datestat = checkifDate(text);
                                            if (datestat)
                                            {
                                                if (text.Contains('/'))
                                                {
                                                    excelcell[cCnt - 1] = text;
                                                }
                                                else
                                                {
                                                    double d = double.Parse(text);
                                                    excelDat = DateTime.FromOADate(d);
                                                    excelcell[cCnt - 1] = excelDat.ToString("dd/MM/yyyy");
                                                }

                                            }
                                            else
                                            {
                                                String dateerror = "Student:Error in Date";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + dateerror);
                                                entry = false;
                                            }


                                        }

                                    }

                                    if (cCnt == 55 || cCnt == 48) // Checking Date DOA and DOL
                                    {
                                        DateTime excelDat;
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            excelcell[cCnt - 1] = text;

                                        }
                                        else
                                        {

                                            Boolean datestat = checkifDate(text);
                                            if (datestat)
                                            {
                                                if (text.Contains('/'))
                                                {
                                                    excelcell[cCnt - 1] = text;
                                                }
                                                else
                                                {
                                                    double d = double.Parse(text);
                                                    excelDat = DateTime.FromOADate(d);
                                                    excelcell[cCnt - 1] = excelDat.ToString("dd/MM/yyyy");
                                                }

                                            }
                                            else
                                            {
                                                String dateerror = "Student:Error in Date";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + dateerror);
                                                entry = false;
                                            }


                                        }
                                    }

                                    //Check Date of Entry Field
                                    if (cCnt == 65)
                                    {

                                        DateTime excelDat;
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            excelcell[cCnt - 1] = text;

                                        }
                                        else
                                        {

                                            Boolean datestat = checkifDate(text);
                                            if (datestat)
                                            {
                                                if (text.Contains('/'))
                                                {
                                                    excelcell[cCnt - 1] = text;
                                                }
                                                else
                                                {
                                                    double d = double.Parse(text);
                                                    excelDat = DateTime.FromOADate(d);
                                                    //excelcell[cCnt - 1] = excelDat.ToString("dd/MM/yyyy");
                                                    excelcell[cCnt - 1] = excelDat.ToString("yyyy/MM/dd");
                                                }

                                            }
                                            else
                                            {
                                                String dateerror = "Student:Error in Date";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + dateerror);
                                                entry = false;
                                            }


                                        }


                                    }

                                    if (cCnt == 16 || cCnt == 17)// checking contact
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            if (cCnt == 16)
                                            {
                                                String contactError = "Student:Compulsory field Contact";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + contactError);
                                                entry = false;
                                            }
                                            else if (cCnt == 17)
                                            {
                                                text = "";
                                                excelcell[cCnt - 1] = text.Trim();
                                            }

                                        }
                                        else
                                        {
                                            Boolean contactStat = checkifContact(text);

                                            if (contactStat)
                                            {

                                                excelcell[cCnt - 1] = text.Trim();

                                            }
                                            else
                                            {
                                                String contactError = "Student:Error in Contact";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + contactError);
                                                entry = false;
                                            }

                                        }
                                    }

                                    if (cCnt == 19 || cCnt == 66) // check rfid
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);

                                        if (text == "-1")
                                        {
                                            excelcell[cCnt - 1] = text;

                                        }
                                        else if (text == "" || text == null || text.Length == 0)
                                        {
                                            String contactError = "Student:Compulsory field RFID";
                                            Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + contactError);
                                            entry = false;

                                        }
                                        else
                                        {
                                            String rfid = checkRFID(text);
                                            //String rfid = text;
                                            if (rfid == "1")
                                            {
                                                String rfidError = "Student:Error in RFID";
                                                Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + rfidError);
                                                entry = false;


                                            }
                                            else
                                            {
                                                excelcell[cCnt - 1] = rfid.Trim();
                                            }

                                        }

                                    }

                                    if (cCnt == 5 || cCnt == 6 || cCnt == 7 || cCnt == 8 || cCnt == 9 || cCnt == 10 || cCnt == 20 || cCnt == 26)
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        Boolean stat = checkCompulsory(text);
                                        if (stat)
                                        {
                                            excelcell[cCnt - 1] = text.Trim();

                                        }
                                        else
                                        {
                                            String rfidError = "Student:Compulsory field";
                                            Log.Info("Column :" + cCnt + " Row Count :" + rCnt + " " + text + " " + rfidError);
                                            entry = false;

                                        }
                                    }

                                    if (cCnt == 12 || cCnt == 13 || cCnt == 14 || cCnt == 15 || cCnt == 18 || cCnt == 21 || cCnt == 22 || cCnt == 23 || cCnt == 24 || cCnt == 25 || cCnt == 27 || cCnt == 28 || cCnt == 29 || cCnt == 30 || cCnt == 31 || cCnt == 32 || cCnt == 33 || cCnt == 34 || cCnt == 35 || cCnt == 36 || cCnt == 37 || cCnt == 38 || cCnt == 39 || cCnt == 40 || cCnt == 41 || cCnt == 42 || cCnt == 43 || cCnt == 44 || cCnt == 45 || cCnt == 46 || cCnt == 47 || cCnt == 49 || cCnt == 50 || cCnt == 51 || cCnt == 52 || cCnt == 53)
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            text = "";
                                            excelcell[cCnt - 1] = text.Trim();

                                        }
                                        else
                                        {
                                            excelcell[cCnt - 1] = text.Trim();

                                        }

                                    }

                                    if (cCnt == 54 || cCnt == 56 || cCnt == 57 || cCnt == 58 || cCnt == 59 || cCnt == 60 || cCnt == 61 || cCnt == 62 || cCnt == 63 || cCnt == 64 || cCnt == 67)
                                    {
                                        String text = Convert.ToString(worksheet.Cells[rCnt, cCnt].Value);
                                        if (text == "" || text == null || text.Length == 0)
                                        {
                                            text = "";
                                            excelcell[cCnt - 1] = text.Trim();

                                        }
                                        else
                                        {
                                            excelcell[cCnt - 1] = text.Trim();

                                        }
                                    }

                                }
                      
                                if (entry == true || entry == false)
                                {
                                    int count = 0, contcid = 0;
                                    string query = "";
                                    query = "select Count(*) from studentmaster where std='" + excelcell[5] + "' and grno='" + excelcell[8] + "'and academicyear = '2025-2026';";
                                    SqlCommand cmd = new SqlCommand(query, con);
                                    SqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        count = Convert.ToInt32(reader[0]);
                                    }
                                    reader.Close();

                                    if (count == 0)
                                    {
                                        int newcid = 0;
                                        query = "SELECT MAX(CAST(cid AS INT)) FROM studentmaster WHERE TRY_CAST(cid AS INT) IS NOT NULL;";
                                        cmd = new SqlCommand(query, con);
                                        reader = cmd.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            contcid = Convert.ToInt32(reader[0]);
                                        }
                                        reader.Close();

                                        newcid = contcid + 1;

                                        query = "insert into StudentMaster(" + excelcolumn[0] + "," + excelcolumn[1] + "," + excelcolumn[2] + "," + excelcolumn[3] + "," + excelcolumn[4] + "," + excelcolumn[5] + "," + excelcolumn[6] + "," + excelcolumn[7] + "," + excelcolumn[8] + "," + excelcolumn[9] + "," + excelcolumn[10] + "," + excelcolumn[11] + "," + excelcolumn[12] + "," + excelcolumn[13] + "," + excelcolumn[14] + "," + excelcolumn[15] + "," + excelcolumn[16] + "," + excelcolumn[17] + "," + excelcolumn[18] + "," + excelcolumn[19] + "," + excelcolumn[20] + "," + excelcolumn[21] + "," + excelcolumn[22] + "," + excelcolumn[23] + "," + excelcolumn[24] + "," + excelcolumn[25] + "," + excelcolumn[26] + "," + excelcolumn[27] + "," + excelcolumn[28] + "," + excelcolumn[29] + "," + excelcolumn[30] + "," + excelcolumn[31] + "," + excelcolumn[32] + "," + excelcolumn[33] + "," + excelcolumn[34] + "," + excelcolumn[35] + "," + excelcolumn[36] + "," + excelcolumn[37] + "," + excelcolumn[38] + "," + excelcolumn[39] + "," + excelcolumn[40] + "," + excelcolumn[41] + "," + excelcolumn[42] + "," + excelcolumn[43] + "," + excelcolumn[44] + "," + excelcolumn[45] + "," + excelcolumn[46] + "," + excelcolumn[47] + "," + excelcolumn[48] + "," + excelcolumn[49] + "," + excelcolumn[50] + "," + excelcolumn[51] + "," + excelcolumn[52] + "," + excelcolumn[53] + "," + excelcolumn[54] + "," + excelcolumn[55] + "," + excelcolumn[56] + "," + excelcolumn[57] + "," + excelcolumn[58] + "," + excelcolumn[59] + "," + excelcolumn[60] + "," + excelcolumn[61] + "," + excelcolumn[62] + "," + excelcolumn[63] + "," + excelcolumn[64] + "," + excelcolumn[65] + "," + excelcolumn[66] + ")" +
                                       "values('" + excelcell[0].Replace("'", "''") + "','" + excelcell[1].Replace("'", "''") + "','" + excelcell[2].Replace("'", "''") + "','" + excelcell[3].Replace("'", "''") + "','" + excelcell[4] + "','" + excelcell[5] + "','" + excelcell[6] + "','" + excelcell[7] + "','" + excelcell[8] + "','" + excelcell[9] + "','" + Convert.ToDateTime(excelcell[10]).ToString("yyyy/MM/dd").Replace('-', '/') + "','" + excelcell[11] + "','" + excelcell[12] + "','" + excelcell[13] + "','" + excelcell[14] + "','" + excelcell[15] + "','" + excelcell[16] + "','" + excelcell[17] + "','" + excelcell[18] + "','" + excelcell[19] + "','" + excelcell[20].Replace("'", "''") + "','" + excelcell[21] + "','" + excelcell[22] + "','" + excelcell[23] + "','" + excelcell[24] + "','" + excelcell[25].Replace("'", "''") + "','" + excelcell[26] + "','" + excelcell[27] + "','" + excelcell[28].Replace("'", "''") + "','" + excelcell[29] + "','" + excelcell[30] + "','" + newcid + "','" + excelcell[32] + "','" + excelcell[33] + "','" + excelcell[34] + "','" + excelcell[35] + "','" + excelcell[36] + "','" + excelcell[37] + "','" + excelcell[38] + "','" + excelcell[39] + "','" + excelcell[40] + "','" + excelcell[41] + "','" + excelcell[42] + "','" + excelcell[43] + "','" + excelcell[44] + "','" + excelcell[45].Replace("'", "''") + "','" + excelcell[46] + "','" + excelcell[47] + "','" + excelcell[48] + "','" + excelcell[49] + "','" + excelcell[50] + "','" + excelcell[51] + "','" + excelcell[52] + "','" + excelcell[53] + "','" + Convert.ToDateTime(excelcell[54].Trim()).ToString("yyyy/MM/dd").Replace('-', '/') + "','" + excelcell[55] + "','" + excelcell[56] + "','" + excelcell[57] + "','" + excelcell[58] + "','" + excelcell[59] + "','" + excelcell[60] + "','" + excelcell[61] + "','" + excelcell[62] + "','" + excelcell[63] + "','" + excelcell[64] + "','" + excelcell[65] + "','" + excelcell[66] + "');";
                                        cmd = new SqlCommand(query, con);
                                        Log.Info("Executing SQL Query:\n" + query);
                                        cmd.ExecuteNonQuery();
                                    }



                                }
                            }


                        }


                    }
                    Log.Info("uploadStudent process completed");
                    return "ok";

                }
                else
                {
                    Log.Info("File Path Not Set");
                    return "File Path Not Set";
                }

            }
            catch (Exception ex)
            {
                Log.Error("StudentList.uploadStudent", ex);
                return ex.Message;

            }
            finally
            {
                if (con != null) { con.Close(); }
            }

        }

        public Boolean checkname(String text)
        {
            Boolean stat = true;

            if (Regex.Matches(text, @"[0-9#!@$%^/&]").Count > 0)
            {

                stat = false;
            }

            return stat;
        }

        public Boolean checkifDate(String text)
        {
            Boolean stat = true;
            if (text.Length > 0)
            {
                if (text.Contains('/'))
                {
                    stat = true;
                }
                else
                {

                    DateTime excelDat;

                    try
                    {
                        double d = double.Parse(text);
                        excelDat = DateTime.FromOADate(d);
                        DateTime datevalue;
                        stat = DateTime.TryParse(excelDat.ToString(), out datevalue);
                    }
                    catch (Exception ex)
                    {
                        stat = false;
                    }
                }

            }
            else
            {
                stat = false;
            }

            return stat;
        }

        public Boolean checkifContact(String text)
        {
            Boolean stat = true;

            if (text.Length > 11)
            {

                stat = false;
            }
            else if (Regex.Matches(text, @"[a-zA-Z#!@$%^/&]").Count > 0)
            {

                stat = false;
            }


            return stat;
        }

        public String checkRFID(String text)
        {

            if (text.Length <= 10)
            {
                if (Regex.Matches(text, @"[a-zA-Z#!@$%^/&]").Count > 0)
                {
                    text = "1";
                }
                else
                {
                    int length = text.Length;
                    int balancezero = 10 - length;

                    for (int i = 0; i < balancezero; i++)
                    {
                        text = "0" + text;
                    }
                }

            }
            else if (text.Length == 24)
            {
                //dont do anything
            }
            else
            {
                text = "1";
            }

            return text;
        }

        public Boolean checkCompulsory(String text)
        {
            Boolean stat = true;
            if (text == null || text.Length == 0)
            {
                stat = false;
            }
            return stat;
        }
    }
}