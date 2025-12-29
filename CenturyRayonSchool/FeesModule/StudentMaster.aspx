<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeBehind="StudentMaster.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.StudentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            left: 0px;
            top: 0px;
        }

        .form-group .local-forms {
            margin-bottom: 20px;
        }



        .local-forms {
            position: relative;
        }

            .local-forms label {
                font-size: 13px;
                color: black !important;
                font-weight: 500;
                position: absolute;
                top: -10px;
                left: 10px;
                background: #fff;
                margin-bottom: 0;
                padding: 0px 5px;
                z-index: 99;
            }

        .c-visible {
            display: none;
         
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="p-2">
        <div class="card card-sh mb-3">
            <div class="card-header">
                <a href="StudentList.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i>
                    <h3 style="display: inline-block; margin-left: 10px;" runat="server" id="lblpagetitle">Student Master</h3>
                </a>
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="-" runat="server" ID="lblAcademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">

                <div class="row ">
                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Surname<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Surname Name" class="form-control txtboxs" ID="txt_surname" />
                        </div>
                    </div>

                    <div class="col-md-3 ">

                        <div class="form-group local-forms">
                            <label>First Name<span class="login-danger">*</span></label>
                            <asp:TextBox runat="server" placeholder="Enter First Name" class="form-control txtboxs" ID="txt_firstname" />
                            <asp:RequiredFieldValidator ID="err_firstname" runat="server" ErrorMessage="Enter First Name" ForeColor="Red" ControlToValidate="txt_firstname"></asp:RequiredFieldValidator>
                        </div>
                    </div>


                    <div class="col-md-3 ">

                        <div class="form-group local-forms">
                            <label>Father Name<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Father Name" class="form-control txtboxs" ID="txt_fathername" />
                            <asp:RequiredFieldValidator ID="err_fathername" runat="server" ErrorMessage="Enter Father Name" ControlToValidate="txt_fathername" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>


                    <div class="col-md-3 ">

                        <div class="form-group local-forms">
                            <label>Mother Name<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Mother Father" class="form-control txtboxs" ID="txt_mothername" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Standard</label>
                            <asp:DropDownList ID="cmb_std" runat="server" Class="form-control select2">
                                <%-- <asp:ListItem Value="0" class="form-control">Select Std</asp:ListItem>--%>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="err_std" runat="server" ErrorMessage="Select Standard" ControlToValidate="cmb_std" OnServerValidate="err_std_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>

                        <asp:Label Text="" runat="server" ID="lbloldstd" CssClass="c-visible" />

                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Division</label>
                            <asp:DropDownList ID="cmb_div" runat="server" Class="form-control select2">
                                <%-- <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:CustomValidator ID="err_div" runat="server" ErrorMessage="Select Div" ControlToValidate="cmb_div" OnServerValidate="err_div_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                        <asp:Label Text="" runat="server" ID="lblolddiv" CssClass="c-visible" />
                    </div>

                    <div class="col-md-3 ">
                        <div class="form-group local-forms">
                            <label>Roll No<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Roll No" class="form-control textboxnumber" ID="txtrollno" />
                            <asp:RequiredFieldValidator ID="err_rollno" runat="server" ErrorMessage="Enter Roll No." ControlToValidate="txtrollno" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Gr.No.<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Gr.No." class="form-control" ID="txtgrno" />
                            <asp:Label Text="" runat="server" ID="lbloldgrno" CssClass="c-visible" />
                            <asp:RequiredFieldValidator ID="err_grno" runat="server" ErrorMessage="Enter Grno" ControlToValidate="txtgrno" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>

                <%--Shiftname, Section,Gender,Date of birth--%>
                <div class="row">
                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>ShiftName</label>
                            <asp:DropDownList ID="cmb_shiftname" runat="server" Class="form-control select2">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="err_shiftname" runat="server" ErrorMessage="Select Shift" ControlToValidate="cmb_shiftname" OnServerValidate="err_shiftname_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>

                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Section</label>
                            <asp:DropDownList ID="cmb_section" runat="server" Class="form-control select2">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="err_section" runat="server" ErrorMessage="Select Section" ControlToValidate="cmb_section" OnServerValidate="err_section_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>

                    </div>


                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Gender</label>
                            <asp:DropDownList ID="cmb_gender" runat="server" Class="form-control select2">
                                <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                <asp:ListItem>M</asp:ListItem>
                                <asp:ListItem>F</asp:ListItem>
                                <asp:ListItem>T</asp:ListItem>

                            </asp:DropDownList>

                            <asp:CustomValidator ID="err_gender" runat="server" ErrorMessage="Select Gender" ControlToValidate="cmb_gender" OnServerValidate="err_gender_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                    </div>

                    <div class="col-md-3">

                        <div class="form-group local-forms calendar-icon">
                            <label>Date Of Birth <span class="login-danger">*</span></label>

                            <input type="date" class="form-control" id="txt_dob" onchange="setDOB()" />

                            <asp:TextBox runat="server" ID="lbldobtxt" CssClass="c-visible" />
                            <asp:RequiredFieldValidator ID="err_dob" runat="server" ErrorMessage="Enter Date of Birth" ControlToValidate="lbldobtxt" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                    </div>

                </div>


                <%--Religion, caste ,subcaste , category--%>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Religon</label>
                            <%--<asp:DropDownList ID="cmb_religon" runat="server" Class="form-control select2">
                            </asp:DropDownList>--%>
                            <asp:TextBox runat="server" placeholder="Enter Religon" class="form-control textReligon" ID="txt_religon" />

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Caste</label>
                            <%--<asp:DropDownList ID="cmb_caste" runat="server" Class="form-control select2">
                            </asp:DropDownList>--%>
                            <asp:TextBox runat="server" placeholder="Enter Caste" class="form-control textCaste" ID="txt_Caste" />

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Sub Caste</label>
                            <%--<asp:DropDownList ID="cmb_subcaste" runat="server" Class="form-control select2">
                            </asp:DropDownList>--%>
                            <asp:TextBox runat="server" placeholder="Enter Sub Caste" class="form-control textSubCaste" ID="txt_subcaste" />

                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group local-forms">
                            <label>Category</label>
                            <%--<asp:DropDownList ID="cmb_category" runat="server" Class="form-control select2">
                            </asp:DropDownList>--%>
                            <asp:TextBox runat="server" placeholder="Enter Category" class="form-control textCategory" ID="txt_Category" />
                        </div>
                    </div>
                </div>




                <div class="row">
                    <div class="col-md-3">

                        <div class="form-group local-forms calendar-icon">
                            <label>Admission Date<span class="login-danger">*</span></label>

                            <input type="date" class="form-control" id="txt_admission_date" onchange="setAdmDate()" />

                            <asp:TextBox runat="server" ID="lblAdmissiondatetxt" CssClass="c-visible" />

                        </div>
                    </div>


                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Admisssion Type</label>
                            <asp:DropDownList ID="cmb_admission_type" runat="server" Class="form-control select2">
                                <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                <asp:ListItem>regular</asp:ListItem>
                                <asp:ListItem>newadmission</asp:ListItem>
                                <asp:ListItem>readmission</asp:ListItem>

                            </asp:DropDownList>

                            <asp:CustomValidator ID="err_admistype" runat="server" ErrorMessage="Select Admission Type" ControlToValidate="cmb_admission_type" OnServerValidate="err_admistype_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                    </div>
                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Admission Std</label>
                            <asp:DropDownList ID="cmbadmstd" runat="server" Class="form-control select2">
                                <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Blood-group</label>
                            <asp:DropDownList ID="cmb_bloodgroup" runat="server" Class="form-control select2">
                                <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                <asp:ListItem>A+</asp:ListItem>
                                <asp:ListItem>B+</asp:ListItem>
                                <asp:ListItem>AB+</asp:ListItem>
                                <asp:ListItem>O</asp:ListItem>
                                <asp:ListItem>A-</asp:ListItem>
                                <asp:ListItem>-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="col-md-6">

                        <div class="form-group local-forms">
                            <label>Address </label>
                            <textarea runat="server" class="form-control" rows="2" type="text" placeholder="Enter Address" id="txt_address"></textarea>
                            <asp:RequiredFieldValidator ID="err_address" runat="server" ErrorMessage="Enter Address" ControlToValidate="txt_address" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>

                    </div>
                    <div class="auto-style1">

                        <div class="form-group local-forms">
                            <label>Contact No. 1<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Contact 1" class="form-control" ID="txt_contact1" />
                            <asp:RequiredFieldValidator ID="err_contact1" runat="server" ErrorMessage="Enter Contact 1" ControlToValidate="txt_contact1" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>Contact No. 2<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Contact 2" class="form-control textboxnumber" ID="txt_contact2" />
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3 ">
                        <div class="form-group local-forms">
                            <label>City<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter City" class="form-control" ID="txt_city" />
                        </div>
                    </div>

                    <div class="col-md-3">

                        <div class="form-group local-forms">
                            <label>State</label>
                            <asp:DropDownList ID="cmb_state" runat="server" Class="form-control select2">
                                <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                <asp:ListItem>Assam</asp:ListItem>
                                <asp:ListItem>Bihar</asp:ListItem>
                                <asp:ListItem>Chhattisgarh</asp:ListItem>
                                <asp:ListItem>Goa</asp:ListItem>
                                <asp:ListItem>Gujarat</asp:ListItem>
                                <asp:ListItem>Haryana</asp:ListItem>
                                <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                <asp:ListItem>Jharkhand</asp:ListItem>
                                <asp:ListItem>Karnataka</asp:ListItem>
                                <asp:ListItem>Kerala</asp:ListItem>
                                <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                <asp:ListItem>Maharashtra</asp:ListItem>
                                <asp:ListItem>Manipur</asp:ListItem>
                                <asp:ListItem>Meghalaya</asp:ListItem>
                                <asp:ListItem>Mizoram</asp:ListItem>
                                <asp:ListItem>Nagaland</asp:ListItem>
                                <asp:ListItem>Odisha</asp:ListItem>
                                <asp:ListItem>Punjab</asp:ListItem>
                                <asp:ListItem>Rajasthan</asp:ListItem>
                                <asp:ListItem>Sikkim</asp:ListItem>
                                <asp:ListItem>Tamil Nadu</asp:ListItem>
                                <asp:ListItem>Telangana</asp:ListItem>
                                <asp:ListItem>Tripura</asp:ListItem>
                                <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                <asp:ListItem>Uttarakhand</asp:ListItem>
                                <asp:ListItem>West Bengal</asp:ListItem>
                                <asp:ListItem>Andaman and Nicobar Islands</asp:ListItem>
                                <asp:ListItem>Chandigarh</asp:ListItem>
                                <asp:ListItem>Dadra and Nagar Haveli and Daman and Diu</asp:ListItem>
                                <asp:ListItem>Lakshadweep</asp:ListItem>
                                <asp:ListItem>Delhi</asp:ListItem>
                                <asp:ListItem>Puducherry</asp:ListItem>
                                <asp:ListItem>-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6 ">

                        <div class="form-group local-forms">
                            <label>Email Id<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Email ID" class="form-control" ID="txt_email" />
                        </div>
                    </div>

                </div>



                <div class="row">
                    <div class="col-md-4 ">

                        <div class="form-group local-forms">
                            <label>RFID No.<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter RFID " class="form-control" ID="txt_rfid" />
                            <asp:RequiredFieldValidator ID="err_rfid" runat="server" ErrorMessage="Enter RFID" ControlToValidate="txt_rfid" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">

                        <div class="form-group local-forms">
                            <label>Uniform ID<span class="login-danger"></span></label>

                            <asp:TextBox runat="server" placeholder="Enter Uniform ID " class="form-control" ID="txt_uniformid" />
                            <asp:RequiredFieldValidator ID="err_uniformid" runat="server" ErrorMessage="Enter Uniform ID" ControlToValidate="txt_uniformid" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-group local-forms">

                            <asp:CheckBox Text="" runat="server" ID="chk_sms" />&nbsp;&nbsp;<asp:Label Text="SMS" runat="server" />

                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group local-forms">

                            <asp:CheckBox Text="" runat="server" ID="chk_left" />&nbsp;&nbsp;<asp:Label Text="Student Left" runat="server" />
                        </div>
                    </div>

                </div>

                <div class="card mb-3">
                    <div class="card-header">
                        Additional details
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">

                                <div class="form-group local-forms">
                                    <label>Saral-ID<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Saral ID " class="form-control textboxnumber" ID="txt_saralid" />
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="form-group local-forms">
                                    <label>Aadhar Card No.<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Aadhar Card No. " class="form-control textboxnumber" ID="txt_aadhar" />
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="form-group local-forms">
                                    <label>Student App ID<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Student App ID " class="form-control" ID="txt_studentid" />
                                </div>


                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Subjects</label>
                                    <asp:DropDownList ID="cmbsubject" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem>IT</asp:ListItem>
                                        <asp:ListItem>Others</asp:ListItem>
                                        <asp:ListItem>Hindi</asp:ListItem>
                                        <asp:ListItem>Hindi_Sanskrit_Comp</asp:ListItem>
                                        <asp:ListItem>-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group local-forms">
                                    <label>Freeship Type</label>
                                    <asp:DropDownList ID="cmbfreeship" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem>-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group local-forms">
                                    <label>Fee Installment</label>
                                    <asp:DropDownList ID="cmbinstallment" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem Value="yearly" class="form-control">Yearly</asp:ListItem>
                                        <asp:ListItem Value="monthly" class="form-control">Monthly</asp:ListItem>
                                        <asp:ListItem>-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group local-forms">
                                    <label>Is Promoted</label>
                                    <asp:DropDownList ID="cmbispromted" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem Value="yes" class="form-control">Yes</asp:ListItem>
                                        <asp:ListItem Value="no" class="form-control">No</asp:ListItem>
                                        <asp:ListItem>-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">


                                <div class="form-group local-forms">
                                    <label>Birth Place<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Birth Place" class="form-control" ID="txtbirthplace" />
                                </div>
                            </div>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Taluka<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Taluka" class="form-control" ID="txttaluka" />
                                </div>
                            </div>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>District<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter District" class="form-control" ID="txtdistrict" />
                                </div>

                            </div>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Birth State</label>
                                    <asp:DropDownList ID="cmbbirthstate" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem>Andhra Pradesh</asp:ListItem>
                                        <asp:ListItem>Arunachal Pradesh</asp:ListItem>
                                        <asp:ListItem>Assam</asp:ListItem>
                                        <asp:ListItem>Bihar</asp:ListItem>
                                        <asp:ListItem>Chhattisgarh</asp:ListItem>
                                        <asp:ListItem>Goa</asp:ListItem>
                                        <asp:ListItem>Gujarat</asp:ListItem>
                                        <asp:ListItem>Haryana</asp:ListItem>
                                        <asp:ListItem>Himachal Pradesh</asp:ListItem>
                                        <asp:ListItem>Jharkhand</asp:ListItem>
                                        <asp:ListItem>Karnataka</asp:ListItem>
                                        <asp:ListItem>Kerala</asp:ListItem>
                                        <asp:ListItem>Madhya Pradesh</asp:ListItem>
                                        <asp:ListItem>Maharashtra</asp:ListItem>
                                        <asp:ListItem>Manipur</asp:ListItem>
                                        <asp:ListItem>Meghalaya</asp:ListItem>
                                        <asp:ListItem>Mizoram</asp:ListItem>
                                        <asp:ListItem>Nagaland</asp:ListItem>
                                        <asp:ListItem>Odisha</asp:ListItem>
                                        <asp:ListItem>Punjab</asp:ListItem>
                                        <asp:ListItem>Rajasthan</asp:ListItem>
                                        <asp:ListItem>Sikkim</asp:ListItem>
                                        <asp:ListItem>Tamil Nadu</asp:ListItem>
                                        <asp:ListItem>Telangana</asp:ListItem>
                                        <asp:ListItem>Tripura</asp:ListItem>
                                        <asp:ListItem>Uttar Pradesh</asp:ListItem>
                                        <asp:ListItem>Uttarakhand</asp:ListItem>
                                        <asp:ListItem>West Bengal</asp:ListItem>
                                        <asp:ListItem>Andaman and Nicobar Islands</asp:ListItem>
                                        <asp:ListItem>Chandigarh</asp:ListItem>
                                        <asp:ListItem>Dadra and Nagar Haveli and Daman and Diu</asp:ListItem>
                                        <asp:ListItem>Lakshadweep</asp:ListItem>
                                        <asp:ListItem>Delhi</asp:ListItem>
                                        <asp:ListItem>Puducherry</asp:ListItem>
                                        <asp:ListItem>-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">


                                <div class="form-group local-forms">
                                    <label>Birth Country<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Birth Country" class="form-control" ID="txtbirthcountry" />
                                </div>
                            </div>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Mother Tongue<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Mother Tongue" class="form-control" ID="txtmothertongue" />
                                </div>
                            </div>

                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Nationality</label>
                                    <asp:DropDownList ID="cmbnationality" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem class="form-control">Indian</asp:ListItem>
                                        <asp:ListItem class="form-control">Other</asp:ListItem>
                                        <asp:ListItem class="form-control">-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group local-forms">
                                    <label>House</label>
                                    <asp:DropDownList ID="cmbhouse" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem class="form-control">Blue</asp:ListItem>
                                        <asp:ListItem class="form-control">Green</asp:ListItem>
                                        <asp:ListItem class="form-control">Red</asp:ListItem>
                                        <asp:ListItem class="form-control">Yellow</asp:ListItem>
                                        <asp:ListItem class="form-control">-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>Last School<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter Last School" class="form-control" ID="txtlastschool" />
                                </div>

                            </div>


                            <div class="col-md-3">
                                <div class="form-group local-forms">
                                    <label>Account Name</label>
                                    <asp:DropDownList ID="cmbaccountname" runat="server" Class="form-control select2">
                                        <asp:ListItem Value="0" class="form-control">Please Select</asp:ListItem>
                                        <asp:ListItem Value="-" class="form-control">-</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

<%--                            <div class="col-sm-1">
                                <i class="fa "></i>
                            </div>--%>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>APAAR ID<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter APAAR ID " class="form-control" ID="txtapaarid" />
                                </div>

                            </div>
                            <div class="col-md-3">

                                <div class="form-group local-forms">
                                    <label>PEN NO<span class="login-danger"></span></label>

                                    <asp:TextBox runat="server" placeholder="Enter APAAR ID " class="form-control" ID="txtpenno" />
                                </div>

                            </div>
                        </div>
        

                    </div>
                </div>

                <div class="card mb-3 ">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-3">
                                <button runat="server" id="btn_save" class="btn btn-success btn-block" onserverclick="btn_save_Click"><i class="fas fa-save mr-2"></i>Save Data</button>

                            </div>
                            <div class="col-md-3">
                                <button runat="server" id="btnResetData" class="btn btn-primary btn-block" onserverclick="btnResetData_ServerClick" causesvalidation="false"><i class="fas fa-recycle mr-2"></i>Reset Data</button>


                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>
                    </div>
                </div>


            </div>
        </div>


        <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel" style="font-weight: bold;">Alert Message!!</h5>

                        <img src="../img/alertimage.png" style="height: 100px; width: auto;" />
                    </div>
                    <div class="modal-body">
                        <asp:Label Text="" runat="server" ID="lblalertmessage" Style="font-weight: normal;" />
                        <%--<label id="lblmessage" style="font-weight: normal;"></label>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel" style="font-weight: bold;">Information Message.</h5>

                        <img src="../img/information.png" style="height: 100px; width: auto;" />
                    </div>
                    <div class="modal-body">
                        <asp:Label Text="" runat="server" ID="lblinfomsg" Style="font-weight: normal;" />
                        <%--<label id="lblinfomsg" style="font-weight: normal;"></label>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-success" data-dismiss="modal">OK</button>
                    </div>
                </div>
            </div>
        </div>

    </section>

    <script>

        $(".select2").select2();

        setViewDOB();
        setViewDOA();

        $(".txtboxs").bind('keyup blur', function () {
            var node = $(this);
            node.val(node.val().replace(/[^a-zA-Z ]/g, ''));
        }
        );

        $(".textboxnumber").bind('keyup blur', function () {
            var node = $(this);

            node.val(node.val().replace(/[^0-9E]/g, ''));

        }
        );


        $("#ContentPlaceHolder1_txt_contact1").bind('keyup blur', function () {
            var node = $(this);

            node.val(node.val().replace(/[^0-9]/g, ''));

            if (node.val().length > 10) {
                node.val("");
            }
        });

        $("#ContentPlaceHolder1_txt_contact2").bind('keyup blur', function () {
            var node = $(this);

            node.val(node.val().replace(/[^0-9]/g, ''));

            if (node.val().length > 10) {
                node.val("");
            }
        });

        $("#ContentPlaceHolder1_txt_rfid").bind('keyup blur', function () {
            var node = $(this);

            node.val(node.val().replace(/[^0-9]/g, ''));

            if (node.val().length > 10) {
                node.val("");
            }
        });

        $("#ContentPlaceHolder1_txt_uniformid").bind('keyup blur', function () {
            var node = $(this);

            node.val(node.val().replace(/[^0-9E]/g, ''));

            if (node.val().length > 24) {
                node.val("");
            }
        });

        function setDOB() {
            var dobd = $("#txt_dob").val();
            $("#ContentPlaceHolder1_lbldobtxt").val(dobd.replace('-', '/').replace('-', '/'));
        }

        function setAdmDate() {
            var admdt = $("#txt_admission_date").val();
            $("#ContentPlaceHolder1_lblAdmissiondatetxt").val(admdt.replace('-', '/').replace('-', '/'));
        }

        function setViewDOB() {
            var dobd = $("#ContentPlaceHolder1_lbldobtxt").val();
            $("#txt_dob").val(dobd.replace('/', '-').replace('/', '-'));
        }

        function setViewDOA() {
            var dobd = $("#ContentPlaceHolder1_lblAdmissiondatetxt").val();
            $("#txt_admission_date").val(dobd.replace('/', '-').replace('/', '-'));
        }

        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }

    </script>
</asp:Content>
