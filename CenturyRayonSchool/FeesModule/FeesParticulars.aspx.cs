//using CenturyRayonSchool.FeesModule.Model;
//using CenturyRayonSchool.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CenturyRayonSchool.FeesModule
{
    public partial class FeesParticulars : System.Web.UI.Page
    {
        Label lblusercode = new Label();

        protected void Page_Load(object sender, EventArgs e)
        {

            lblusercode = (Label)Page.Master.FindControl("lblusercode");
            if (!IsPostBack)
            {


                string year = new FeesModel().setActiveAcademicYear();
                lblAcademicyear.Text = year;


                loadFormControl();
                cmbAcademicYear.SelectedValue = year;

                LoadFeesParticularBindGrid();
            }
        }

        public void LoadFeesParticularBindGrid()
        {
            SqlConnection con = null;
            try
            {
                DataTable FeeParticular_table = new DataTable();
                DataTable std_table = new DataTable();
                string academicyear = cmbAcademicYear.SelectedValue.ToString();

                DataTable uifeesparticulartable = new DataTable();
                uifeesparticulartable.Columns.Add("SrNo");
                uifeesparticulartable.Columns.Add("Academicyear");
                uifeesparticulartable.Columns.Add("STD");
                uifeesparticulartable.Columns.Add("Computer");
                uifeesparticulartable.Columns.Add("Interactive");
                uifeesparticulartable.Columns.Add("ELibrary");
                uifeesparticulartable.Columns.Add("OtherFees");
                uifeesparticulartable.Columns.Add("ReAdmissionFees");
                uifeesparticulartable.Columns.Add("NewAdmissionFees");
                uifeesparticulartable.Columns.Add("Administrative");

                uifeesparticulartable.Columns.Add("Total");

                using (con = Connection.getConnection())
                {
                    con.Open();
                    string query = "";

                    query = "select [std-div],particularname,fee_code,total,Academicyear from FeeParticular where Academicyear='" + academicyear + "';";
                    SqlDataAdapter adap = new SqlDataAdapter(query, con);
                    adap.Fill(FeeParticular_table);

                    query = "select std from std where std not in ('ALL','LEFT');";
                    adap = new SqlDataAdapter(query, con);
                    adap.Fill(std_table);

                    int srno = 1;
                    foreach (DataRow ro in std_table.Rows)
                    {
                        double Computer = 0, Interactive = 0, OtherFees = 0, ReAdmissionFees = 0, NewAdmissionFees = 0, Administrative = 0, ELibrary = 0, Total = 0;

                        //fetch computer fees
                        var dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Computer")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            Computer = Convert.ToDouble(dr["total"]);
                        }

                        //fetch Interactive fees
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Interactive")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            Interactive = Convert.ToDouble(dr["total"]);
                        }

                        //fetch E Library 
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("E Library")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            ELibrary = Convert.ToDouble(dr["total"]);
                        }

                        //fetch Other Fees fees
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Other Fees")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            OtherFees = Convert.ToDouble(dr["total"]);
                        }

                        //fetch Re Admission Fees
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Re Admission Fees")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            ReAdmissionFees = Convert.ToDouble(dr["total"]);
                        }

                        //fetch New Admission Fees 
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("New Admission Fees")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            NewAdmissionFees = Convert.ToDouble(dr["total"]);
                        }

                        //fetch Administrative 
                        dr = FeeParticular_table.AsEnumerable().Where(x => x.Field<string>("std-div").Equals(ro["std"].ToString()) && x.Field<string>("particularname").Equals("Administrative")).DefaultIfEmpty(null).FirstOrDefault();

                        if (dr != null)
                        {
                            Administrative = Convert.ToDouble(dr["total"]);
                        }





                        Total = Computer + Interactive + OtherFees + ReAdmissionFees + NewAdmissionFees + Administrative + ELibrary;


                        uifeesparticulartable.Rows.Add(srno, academicyear, ro["std"].ToString(), Computer, Interactive, ELibrary, OtherFees, ReAdmissionFees, NewAdmissionFees, Administrative, Total);
                        srno++;
                    }

                }


                GridParticulars.DataSource = uifeesparticulartable;
                GridParticulars.DataBind();


            }
            catch (Exception ex)
            {
                Log.Error("FeesParticulars.LoadFeesParticularBindGrid", ex);
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

                    query = "select Fee_Code,Fee_Header from FeeHeader;";
                    adap = new SqlDataAdapter(query, con);
                    DataTable feeheader = new DataTable();
                    adap.Fill(feeheader);
                    feeheader.Rows.Add("-1", "Select Fee Header");
                    cmbHeaders.DataSource = feeheader;
                    cmbHeaders.DataBind();
                    cmbHeaders.DataTextField = "Fee_Header";
                    cmbHeaders.DataValueField = "Fee_Code";
                    cmbHeaders.DataBind();
                    cmbHeaders.SelectedValue = "-1";

                    DataTable academictbl = new FeesModel().GetAcademicYearList(con);
                    cmbAcademicYear.DataSource = academictbl;
                    cmbAcademicYear.DataBind();
                    cmbAcademicYear.DataTextField = "year";
                    cmbAcademicYear.DataValueField = "year";
                    cmbAcademicYear.DataBind();
                    cmbAcademicYear.SelectedValue = "Select Academic Year";


                }
            }
            catch (Exception ex)
            {
                Log.Error("FeesParticulars.loadFormControl", ex);
            }
            finally
            {
                if (con != null) { con.Close(); }
            }
        }

        protected void SaveFeesParticulars_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SqlConnection con = null;
                try
                {
                    DateTime cdt = TimeZoneClass.getIndianTimeZoneValues();
                    string usercode = lblusercode.Text;

                    using (con = Connection.getConnection())
                    {
                        con.Open();
                        string query = "", stddiv = "", particularname = "", academicyear = "";
                        SqlCommand cmd = null;

                        stddiv = cmbStd.SelectedValue.ToString();
                        particularname = cmbHeaders.SelectedItem.Text;
                        academicyear = cmbAcademicYear.SelectedValue.ToString();

                        int count = 0;
                        query = "select Count(*) from FeeParticular where [std-div]='" + stddiv + "' and particularname='" + particularname + "' and academicyear='" + academicyear + "';";
                        cmd = new SqlCommand(query, con);
                        var rcnt = cmd.ExecuteScalar();
                        if (!string.IsNullOrEmpty(rcnt.ToString()))
                        {
                            count = Convert.ToInt32(rcnt);
                        }

                        if (count == 0)
                        {
                            query = "insert into FeeParticular([Std-Div],[fee_code],ParticularName,total,academicyear,createdby,createddatetime) values(@StdDiv,@fee_code,@ParticularName,@total,@academicyear,@createdby,@createddatetime);";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@StdDiv", stddiv);
                            cmd.Parameters.AddWithValue("@fee_code", cmbHeaders.SelectedValue.ToString());
                            cmd.Parameters.AddWithValue("@ParticularName", particularname);
                            cmd.Parameters.AddWithValue("@total", txtAmount.Text);
                            cmd.Parameters.AddWithValue("@academicyear", academicyear);
                            cmd.Parameters.AddWithValue("@createdby", usercode);
                            cmd.Parameters.AddWithValue("@createddatetime", cdt.ToString("yyyy/MM/dd HH:mm:ss"));
                            cmd.ExecuteNonQuery();

                            //open modal
                            lblinfomsg.Text = "Fees Particular Saved Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }
                        else
                        {
                            query = "update FeeParticular set total=@total,updatedby=@updatedby,updateddatetime=@updateddatetime where [std-div]=@StdDiv and particularname=@particularname and academicyear=@academicyear;";
                            cmd = new SqlCommand(query, con);
                            cmd.Parameters.AddWithValue("@StdDiv", stddiv);
                            cmd.Parameters.AddWithValue("@ParticularName", particularname);
                            cmd.Parameters.AddWithValue("@total", txtAmount.Text);
                            cmd.Parameters.AddWithValue("@academicyear", academicyear);
                            cmd.Parameters.AddWithValue("@updatedby", usercode);
                            cmd.Parameters.AddWithValue("@updateddatetime", cdt.ToString("yyyy/MM/dd HH:mm:ss"));
                            cmd.ExecuteNonQuery();

                            //open modal
                            lblinfomsg.Text = "Fees Particular Updated Successfully.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showInfoModal();", true);
                        }


                        LoadFeesParticularBindGrid();
                    }

                }
                catch (Exception ex)
                {
                    Log.Error("FeesParticulars.SaveFeesParticulars_ServerClick", ex);
                }
                finally
                {
                    if (con != null) { con.Close(); }
                }

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

        protected void headerCustomvalid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cmbHeaders.SelectedValue == "-1")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        protected void cmbAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFeesParticularBindGrid();
        }
    }
}