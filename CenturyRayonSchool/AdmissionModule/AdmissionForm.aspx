<%@ Page Title="Registration Form" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="AdmissionForm.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.AdmissionForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bgclass {
            background-color: cadetblue;
        }

        .c-visible {
            display: none;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            getDate();
        });
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('successModal'))
            myModal.show()
        }
        function getDate() {
            var todaydate = new Date();
            var day = String(todaydate.getDate()).padStart(2, '0'); //basappa
            var month = String(todaydate.getMonth() + 1).padStart(2, '0');//basappa
            var year = todaydate.getFullYear();
            var datestring = day + "/" + month + "/" + year;
            document.getElementById("dateOfSub").innerHTML = "Date : " + datestring;
        }

        function validatePhoneNumber(input, showValidationMessage) {
            // Remove non-numeric characters
            var phoneNumber = input.value.replace(/\D/g, '');

            // Limit the input to 10 digits
            if (phoneNumber.length > 10) {
                phoneNumber = phoneNumber.slice(0, 10);
            }

            // Update the input value
            input.value = phoneNumber;

            // Check for exactly 10 digits on blur
            if (showValidationMessage && phoneNumber.length !== 10) {
                alert("Please enter a 10-digit phone number.");
                // You can also add additional handling, such as clearing the input or setting a custom validation message.
            }
        }

        function validationonclick() {
            var main = "Please select/enter ";
            debugger
            var studentValue = document.getElementById('<%= studName.ClientID %>').value;
            var middleValue = document.getElementById('<%= middleName.ClientID %>').value;
            var surname = document.getElementById('<%= surName.ClientID %>').value;
            var Standard = document.getElementById('<%= std.ClientID %>').value;
         //var Division = document.getElementById('<%= div.ClientID %>').value;
            var DOB = document.getElementById('<%= DOB.ClientID %>').value;
            var Placeofbirth = document.getElementById('<%= placeOfB.ClientID %>').value;
            var Nationality = document.getElementById('<%= nationality.ClientID %>').value;
            var Mothertongue = document.getElementById('<%= motherTounge.ClientID %>').value;
            var Religion = document.getElementById('<%= religion.ClientID %>').value;
           <%-- var GRNO1 = document.getElementById('<%= grno1.ClientID %>').value;
            var stdname1 = document.getElementById('<%= siblingName1.ClientID %>').value;
            var STD1 = document.getElementById('<%= siblingStd1.ClientID %>').value;
            var GRNO2 = document.getElementById('<%= grno2.ClientID %>').value;
            var stdname2 = document.getElementById('<%= siblingName2.ClientID %>').value;
            var STD2 = document.getElementById('<%= siblingStd2.ClientID %>').value;--%>
            var Parentsfirstname = document.getElementById('<%= fatherFirstName.ClientID %>').value;
            var Parentslastname = document.getElementById('<%= fatherLastName.ClientID %>').value;
            var ParentsNationality = document.getElementById('<%= fatherNationality.ClientID %>').value;
            var ParentsQualification = document.getElementById('<%= fatherQualification.ClientID %>').value;
            var ParentsOccupation = document.getElementById('<%= fatherOccupation.ClientID %>').value;
            var ParentsAnnualIncome = document.getElementById('<%= fatherIncome.ClientID %>').value;
            var ParentsPANNumber = document.getElementById('<%= fatherPAN.ClientID %>').value;
            var ParentsOfficeAddress = document.getElementById('<%= fatherOfficeAddress.ClientID %>').value;
            var ParentsEmailid = document.getElementById('<%= fatherEmailAddress.ClientID %>').value;
            var ParentsMobileNumber = document.getElementById('<%= fatherPhoneNumber.ClientID %>').value;
           <%-- var Parentsfirstname2 = document.getElementById('<%= motherFirstName.ClientID %>').value;
            var Parentslastname2 = document.getElementById('<%= motherLastName.ClientID %>').value;
            var ParentsNationality2 = document.getElementById('<%= motherNationality.ClientID %>').value;
            var ParentsQualification2 = document.getElementById('<%= motherQualification.ClientID %>').value;
            var ParentsOccupation2 = document.getElementById('<%= motherOccupation.ClientID %>').value;
            var ParentsAnnualIncome2 = document.getElementById('<%= motherIncome.ClientID %>').value;
            var ParentsPANNumber2 = document.getElementById('<%= motherPAN.ClientID %>').value;
            var ParentsOfficeAddress2 = document.getElementById('<%= motherOfficeAddress.ClientID %>').value;
            var ParentsEmailid2 = document.getElementById('<%= motherEmailAddress.ClientID %>').value;
            var ParentsMobileNumber2 = document.getElementById('<%= motherPhoneNumber.ClientID %>').value;--%>
            var Residentialadd = document.getElementById('<%= residentialAddress.ClientID %>').value;
            //var Locality = document.getElementById('<%= locality.ClientID %>').value;
            var PhotoofChild = document.getElementById('<%= childPhoto.ClientID %>').value;
            var birthCertificate = document.getElementById('<%= birthCertificate.ClientID %>').value;
            var residentialProof = document.getElementById('<%= residentialProof.ClientID %>').value;
            <%--var TCertificate = document.getElementById('<%= tcproff.ClientID %>').value;
            var otherProof = document.getElementById('<%= otherfile.ClientID %>').value;--%>

            var lblPhotoofChild = document.getElementById('<%= lblphotopath.ClientID %>').value;
            var lblbirthCertificate = document.getElementById('<%= lblbirthcertificate.ClientID %>').value;
            var lblresidentialProof = document.getElementById('<%= lblresidentialpath.ClientID %>').value;
           <%-- var lblTCertificate = document.getElementById('<%= lbltcpath.ClientID %>').value;
            var lblotherProof = document.getElementById('<%= otherpf.ClientID %>').value;--%>


            if (studentValue === '') {
                main += 'Student, ';
            } if (middleValue === '') {
                main += 'Middle Name, ';
            } if (surname === '') {
                main += 'Surname Name, ';
            } if (Standard === 'Select Std') {
                main += 'Standard, ';
            }
            //if (Division === '0') {
            //    main += 'Division, ';
            //} 
            if (DOB === '') {
                main += 'Date of Birth, ';
            } if (Placeofbirth === '') {
                main += 'Place of birth, ';
            } if (Nationality === '') {
                main += 'Nationality, ';
            } if (Mothertongue === '') {
                main += 'Mother tongue, ';
            } if (Religion === 'Select Religion') {
                main += 'Religion, ';
            }
            //if (GRNO1 === '') {
            //    main += 'Sibling GRNO, ';
            //} if (stdname1 === '') {
            //    main += 'Sibling Student Name, ';
            //} if (STD1 === 'Select Std') {
            //    main += 'Sibling STD, ';
            //} if (GRNO2 === '') {
            //    main += 'Sibling GRNO2, ';
            //} if (stdname2 === '') {
            //    main += 'Sibling Student Name2, ';
            //} if (STD2 === 'Select Std') {
            //    main += 'Sibling STD2, ';
            //}
            if (Parentsfirstname === '') {
                main += 'Father First Name, ';
            } if (Parentslastname === '') {
                main += 'Father Last Name, ';
            } if (ParentsNationality === '') {
                main += 'Father Nationality, ';
            } if (ParentsQualification === '') {
                main += 'Father Qualification, ';
            } if (ParentsOccupation === '') {
                main += 'Father Occupation, ';
            } if (ParentsAnnualIncome === '') {
                main += 'Father Annual Income, ';
            } if (ParentsPANNumber === '') {
                main += 'Father PAN Number, ';
            } if (ParentsOfficeAddress === '') {
                main += 'Father Office Address, ';
            } if (ParentsEmailid === '') {
                main += 'Father Email id, ';
            } if (ParentsMobileNumber === '') {
                main += 'Father Mobile no, ';
            }
            //if (Parentsfirstname2 === '') {
            //    main += 'Mother first name, ';
            //} if (Parentslastname2 === '') {
            //    main += 'Mother Last name, ';
            //} if (ParentsNationality2 === '') {
            //    main += 'Mother Nationality, ';
            //} if (ParentsQualification2 === '') {
            //    main += 'Mother Qualification, ';
            //} if (ParentsOccupation2 === '') {
            //    main += 'Mother Occupation, ';
            //} if (ParentsAnnualIncome2 === '') {
            //    main += 'Mother Annual Income, ';
            //} if (ParentsPANNumber2 === '') {
            //    main += 'Mother PAN Number, ';
            //} if (ParentsOfficeAddress2 === '') {
            //    main += 'Mother Office Address, ';
            //} if (ParentsEmailid2 === '') {
            //    main += 'Mother Emailid, ';
            //} if (ParentsMobileNumber2 === '') {
            //    main += 'Mother Mobile Number, ';
            //}
            if (Residentialadd === '') {
                main += 'Residential Address, ';
            }
            //if (Locality === '') {
            //    main += 'Locality, ';
            //} 
            var admissionID = '<%= Session["AdmissionID"] %>';
            console.log(admissionID)
            //if (admissionID === 'Modify') {

            //    if (lblPhotoofChild === '') {
            //        main += 'PhotoofChild, ';
            //    }
            //    if (lblbirthCertificate === '') {
            //        main += 'Birth Certificate, ';
            //    } if (lblresidentialProof === '') {
            //        main += 'Residential Proof, ';
            //    }


            //    //if (lblTCertificate === '') {
            //    //    main += ' Test Certificate, ';
            //    //} if (lblotherProof === '') {
            //    //    main += 'Other Proof, ';
            //    //}

            //}
            //else {


            //    if (PhotoofChild === '') {
            //        main += 'PhotoofChild, ';
            //    }
            //    if (birthCertificate === '') {
            //        main += 'Birth Certificate, ';
            //    } if (residentialProof === '') {
            //        main += 'Residential Proof, ';
            //    }
            //    //if (TCertificate === '') {
            //    //    main += ' Test Certificate, ';
            //    //} if (otherProof === '') {
            //    //    main += 'Other Proof, ';
            //    //}


            //}

            if (main.endsWith(', ')) {
                main = main.slice(0, -2);
            }

            if (main !== "Please select/enter ") {
                alert(main);
                //return false;
                return false;
            }
            localStorage.removeItem("AdmissionID");
            // return true;
            return true;

        }


        function resetclick() {
            location.reload();

        }

      <%--  function checkboxchecked() {
            debugger;
            var approved = document.getElementById('<%=chkemployeecheeck.ClientID%>');
            var reject = document.getElementById('<%=chkNo.ClientID%>');
            var department = document.getElementById('<%=ddldepartment.ClientID%>');

            if (approved == false) {
                department.show = false;
            }

        }--%>
        //function myFunction() {
        //    document.getElementById("aa").style.display = "block";
        //}
    </script>

    <script>
        $(document).ready(function () {

            $('#chkemployeecheeck').change(function () {
                debugger
                if ($(this).is(':checked')) {
                    $('#chkNo').prop('checked', false);
                    $("#ContentPlaceHolder1_pnlemployeepanel").css("display", "inline");
                   
                }
            });


            $('#chkNo').change(function () {
                debugger
                if ($(this).is(':checked')) {
                    $('#chkemployeecheeck').prop('checked', false);
                    
                    $("#ContentPlaceHolder1_pnlemployeepanel").css("display", "none");

                   
                }
            });
        });

         <%-- $('#<%= chkemployeecheeck.ClientID %>').change(function () {
                 if ($(this).is(':checked')) {
                     debugger
                     $('#chkNo').prop('checked', false); // Uncheck chkNo if needed
                     $('#ddldepartment').hide();
                 }
                 else {
                     $('#ddldepartment').show();
                 }
             });--%>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--   <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>--%>
    <div class="container-fluid">
        <section class="p-4 font-sans mb-5">

            <div>
                <h2 align="center">Application for Admission Form For Academic Year 
                    <asp:Label ID="lblyear" runat="server" />
                </h2>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="card card-shw mb-3">

                        <h5 class="card-header border-btm">
                            <asp:Label ID="formlbltitle" Text="Add New Admission Form Details" runat="server" />
                            <span id="dateOfSub" class="float-end">Date:</span>
                        </h5>

                        <div style="display: none;">
                            <asp:Label Text="Admission ID :" runat="server" />
                            <asp:Label ID="lblAdmissionid" Text="0" runat="server" />
                        </div>



                        <%--<h5 class="card-header border-btm">Admission Form <span id="dateOfSub" class="float-end">Date:</span></h5>--%>
                        <div class="card-body <%=bgcss%>">
                            <div class="row form-fields">
                                <div class="col-md-3">
                                    <label for="studName" class="form-label mb-1">Name of Student</label>
                                    <asp:TextBox type="text" class="form-control" ID="studName" name="studName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredstudentname" runat="server" ErrorMessage="Kindly Enter Student Name" ControlToValidate="studName" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid Name.</div>
                                    <div class="invalid-feedback">Please enter student name.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="middleName" class="form-label mb-1">Father's Name</label>
                                    <asp:TextBox type="text" class="form-control" ID="middleName" name="middleName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredstudentmidname" runat="server" ErrorMessage="Kindly Enter Middle Name" ControlToValidate="middleName" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid middle name.</div>
                                    <div class="invalid-feedback">Please enter middle name.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="surName" class="form-label mb-1">Surname</label>
                                    <asp:TextBox type="text" class="form-control" ID="surName" name="surName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredstudentSurnamename" runat="server" ErrorMessage="Kindly Enter Surname Name" ControlToValidate="surName" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid surname.</div>
                                    <div class="invalid-feedback">Please enter surname.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="surName" class="form-label mb-1">Mother's Name</label>
                                    <%-- basappa holiday --%>
                                    <asp:TextBox type="text" class="form-control" ID="txtmothername" name="mothername" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Kindly Enter Mother Name" ControlToValidate="txtmothername" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid surname.</div>
                                    <div class="invalid-feedback">Please enter surname.</div>
                                </div>
                                <div class="col-md-3 mt-auto mb-auto">
                                    <label class="form-label mb-1" for="gender">Gender</label>
                                    <div class="d-flex">
                                        <asp:RadioButton Checked="true" ID="radMale" class="form-check" runat="server" Text="Male" GroupName="gender" />
                                        <asp:RadioButton ID="radFemale" class="form-check" runat="server" Text="Female" GroupName="gender" />

                                        <%--<asp:RequiredFieldValidator ID="rfvGender" runat="server" ControlToValidate="radMale" ErrorMessage="Please select a gender"  ForeColor="Red"> </asp:RequiredFieldValidator>--%>

                                        <%--<asp:RequiredFieldValidator ID="rfvFemaleGender" runat="server" ControlToValidate="radFemale" 
    InitialValue="" ErrorMessage="Please select a gender" Display="Dynamic" ForeColor="Red" />  --%>
                                    </div>
                                    <div class="valid-feedback">Valid selection.</div>
                                    <div class="invalid-feedback">Please select a gender.</div>
                                </div>

                                <div class="col-md-3">
                                    <%-- basappa holiday --%>
                                    <label for="std" class="form-label mb-1">Last Standard passed</label>
                                    <asp:DropDownList class="form-select" ID="laststdpassed" aria-describedby="laststdpassed" runat="server">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" InitialValue="Select Std" runat="server" ErrorMessage="Please Select last Standard passed" ControlToValidate="laststdpassed" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="valid-feedback">Valid standard.</div>
                                    <div class="invalid-feedback">Please select standard.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="std" class="form-label mb-1">Standard in which to be admitted</label>
                                    <asp:DropDownList class="form-select" ID="std" aria-describedby="std" runat="server">
                                        <%--   <asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                        <asp:ListItem value="PlaySchool">PlaySchool</asp:ListItem> 
                                        <asp:ListItem value="Jr.Kg">Jr.Kg</asp:ListItem>
                                        <asp:ListItem value="Sr.Kg">Sr.Kg</asp:ListItem>
                                        <asp:ListItem value="I">I</asp:ListItem>
                                        <asp:ListItem value="II">II</asp:ListItem>
                                        <asp:ListItem value="III">III</asp:ListItem>
                                        <asp:ListItem value="IV">IV</asp:ListItem>
                                        <asp:ListItem value="V">V</asp:ListItem>
                                        <asp:ListItem value="VI">VI</asp:ListItem>
                                        <asp:ListItem value="VII">VII</asp:ListItem>
                                        <asp:ListItem value="VIII">VIII</asp:ListItem>
                                        <asp:ListItem value="IX">IX</asp:ListItem>
                                        <asp:ListItem value="X">X</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Requiredstd" InitialValue="Select Std" runat="server" ErrorMessage="Please Select Standard" ControlToValidate="std" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="valid-feedback">Valid standard.</div>
                                    <div class="invalid-feedback">Please select standard.</div>
                                </div>
                                <%--Basappa--%>
                                <div class="col-md-4" id="division" runat="server">
                                    <label for="div" class="form-label mb-1">Division</label>
                                    <asp:DropDownList class="form-select" ID="div" aria-describedby="div" AppendDataBoundItems="True" runat="server" Enabled="False">
                                        <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="Requireddiv" InitialValue="0" runat="server" ErrorMessage="Please Select Division" ControlToValidate="div" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    <div class="invalid-feedback">Please select Division.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="DOB" class="form-label mb-1">Date of Birth</label>
                                    <asp:TextBox type="date" class="form-control" ID="DOB" name="DOB" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredDateOfbirth" runat="server" ErrorMessage="Kindly Enter Date of Birth" ControlToValidate="DOB" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="valid-feedback">Valid Date.</div>
                                    <div class="invalid-feedback">Please enter DOB.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">Place of Birth</label>
                                    <asp:TextBox type="text" class="form-control" ID="placeOfB" name="placeOfB" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requireddob" runat="server" ErrorMessage="Kindly Enter Place of Birth" ControlToValidate="placeOfB" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter place of birth.</div>
                                </div>

                                <%--basappa holiday--%>

                                <div class="col-md-3">
                                    <label for="lbltaluka" class="form-label mb-1">Taluka</label>
                                    <asp:TextBox type="text" class="form-control" ID="txttaluka" name="txttaluka" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Kindly Enter Taluka" ControlToValidate="txttaluka" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter Taluka.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">District</label>
                                    <asp:TextBox type="text" class="form-control" ID="txtdistrict" name="txtdistrict" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Kindly Enter District" ControlToValidate="txtdistrict" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter District.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">State</label>
                                    <asp:TextBox type="text" class="form-control" ID="txtstate" name="txtstate" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Kindly Enter State" ControlToValidate="txtstate" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter state.</div>
                                </div>

                                <%--basappa holiday--%>

                                <div class="col-md-3">
                                    <label for="nationality" class="form-label mb-1">Nationality</label>
                                    <asp:DropDownList class="form-select" ID="nationality" aria-describedby="nationality" runat="server">
                                        <asp:ListItem Selected="True" Value="">Choose...</asp:ListItem>
                                        <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                        <asp:ListItem Value="Indian">Other</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Requirednationality" InitialValue="" runat="server" ErrorMessage="Please Select Nationality" ControlToValidate="nationality" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="invalid-feedback">Please select a valid option.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="motherTounge" class="form-label mb-1">Mother Tounge</label>
                                    <asp:TextBox type="text" class="form-control" ID="motherTounge" name="motherTounge" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="requiredmotherTounge" runat="server" ErrorMessage="Kindly Enter Mother Tounge" ControlToValidate="motherTounge" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid asp:TextBox.</div>
                                    <div class="invalid-feedback">Please enter mother tounge.</div>
                                </div>
                                <div class="col-md-3">
                                    <label for="religion" class="form-label mb-1">Religion</label>
                                    <asp:DropDownList class="form-select" ID="religion" aria-describedby="religion" runat="server">
                                        <%--    <asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                        <asp:ListItem value="Hindu">Hindu</asp:ListItem>
                                        <asp:ListItem value="Muslim">Muslim</asp:ListItem>
                                        <asp:ListItem value="Christian">Christian</asp:ListItem>
                                        <asp:ListItem value="Sikh">Sikh</asp:ListItem>
                                        <asp:ListItem value="Jain">Jain</asp:ListItem>
                                        <asp:ListItem value="Others">Others</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="Requiredreligion" InitialValue="Select Religion" runat="server" ErrorMessage="Please Select Religion" ControlToValidate="religion" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="invalid-feedback">Please select a valid option.</div>
                                </div>

                                <%--basappa holiday--%>

                                <div class="col-md-3">
                                    <label for="lbltaluka" class="form-label mb-1">Caste</label>
                                    <asp:TextBox type="text" class="form-control" ID="txtcaste" name="txtcaste" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Kindly Enter Caste" ControlToValidate="txtcaste" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter caste.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">Category</label>
                                    <asp:TextBox type="text" class="form-control" ID="txtcategory" name="txtcategory" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Kindly Enter Category" ControlToValidate="txtcategory" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter category.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">Aadhar No</label>
                                    <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" type="text" class="form-control" MaxLength="15"  ID="txtaddharno" name="txtaddharno" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Kindly Enter Aadhar No" ControlToValidate="txtaddharno" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter aadhar.</div>
                                </div>

                                <div class="col-md-3">
                                    <label for="placeOfB" class="form-label mb-1">Last School Attend</label>
                                    <asp:TextBox type="text" class="form-control" ID="txtlastschoolattend" name="txtlastschoolattend" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Kindly Enter Last School Attend" ControlToValidate="txtlastschoolattend" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <div class="valid-feedback">Valid place.</div>
                                    <div class="invalid-feedback">Please enter Last School Attend.</div>
                                </div>

                                <%--basappa holiday--%>
                            </div>
                        </div>
                    </div>
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Name of siblings studying in our school (if any)</h5>
                        <%--<span class="float-end"><button type="button" class="btn btn-outline-info btn-add" id="addSibling"><i class="fa-solid fa-plus fa-lg"></i></button></span>--%>
                        <div class="card-body <%=bgcss%>">
                            <div class="row form-fields appending_div">
                                <div class="col-md-4">
                                    <label for="grno1" class="form-label mb-1">Enter GRNO</label>
                                    <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" class="form-control" ID="grno1" name="grno1" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="requiedgrno1" runat="server" ErrorMessage="Kindly Enter GRN" ControlToValidate="grno1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4">
                                    <label for="siblingName1" class="form-label mb-1">Student Name</label>
                                    <asp:TextBox type="text" class="form-control" ID="siblingName1" name="siblingName1" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="requiredsiblingName1" runat="server" ErrorMessage="Kindly Enter Student Name" ControlToValidate="siblingName1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4">
                                    <label for="siblingStd1" class="form-label mb-1">STD</label>
                                    <%--<asp:TextBox type="text" class="form-control" id="siblingStd1" name="siblingStd1" runat="server"></asp:TextBox>--%>

                                    <asp:DropDownList class="form-select" ID="siblingStd1" aria-describedby="siblingStd1" runat="server">
                                        <%--<asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                        <asp:ListItem value="PlaySchool">PlaySchool</asp:ListItem> 
                                        <asp:ListItem value="Jr.Kg">Jr.Kg</asp:ListItem>
                                        <asp:ListItem value="Sr.Kg">Sr.Kg</asp:ListItem>
                                        <asp:ListItem value="I">I</asp:ListItem>
                                        <asp:ListItem value="II">II</asp:ListItem>
                                        <asp:ListItem value="III">III</asp:ListItem>
                                        <asp:ListItem value="IV">IV</asp:ListItem>
                                        <asp:ListItem value="V">V</asp:ListItem>
                                        <asp:ListItem value="VI">VI</asp:ListItem>
                                        <asp:ListItem value="VII">VII</asp:ListItem>
                                        <asp:ListItem value="VIII">VIII</asp:ListItem>
                                        <asp:ListItem value="IX">IX</asp:ListItem>
                                        <asp:ListItem value="X">X</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredsiblingStd1" InitialValue="Select Std" runat="server" ErrorMessage="Please Select STD" ControlToValidate="siblingStd1" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4">
                                    <label for="grno2" class="form-label mb-1">Enter GRNO</label>
                                    <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" class="form-control" ID="grno2" name="grno2" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="reuiredgrno2" runat="server" ErrorMessage="Kindly Enter GRNO" ControlToValidate="grno2" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4">
                                    <label for="siblingName2" class="form-label mb-1">Student Name</label>
                                    <asp:TextBox type="text" class="form-control" ID="siblingName2" name="siblingName2" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="requiredsiblingName2" runat="server" ErrorMessage="Kindly Enter Student Name" ControlToValidate="siblingName2" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <div class="col-md-4">
                                    <label for="siblingStd2" class="form-label mb-1">STD</label>
                                    <%--<asp:TextBox type="text" class="form-control" id="siblingStd2" name="siblingStd2" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList class="form-select" ID="siblingStd2" aria-describedby="siblingStd2" runat="server">
                                        <%--<asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                         <asp:ListItem value="PlaySchool">PlaySchool</asp:ListItem> 
                                        <asp:ListItem value="Jr.Kg">Jr.Kg</asp:ListItem>
                                        <asp:ListItem value="Sr.Kg">Sr.Kg</asp:ListItem>
                                        <asp:ListItem value="I">I</asp:ListItem>
                                        <asp:ListItem value="II">II</asp:ListItem>
                                        <asp:ListItem value="III">III</asp:ListItem>
                                        <asp:ListItem value="IV">IV</asp:ListItem>
                                        <asp:ListItem value="V">V</asp:ListItem>
                                        <asp:ListItem value="VI">VI</asp:ListItem>
                                        <asp:ListItem value="VII">VII</asp:ListItem>
                                        <asp:ListItem value="VIII">VIII</asp:ListItem>
                                        <asp:ListItem value="IX">IX</asp:ListItem>
                                        <asp:ListItem value="X">X</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="RequiredsiblingStd2" InitialValue="Select Std" runat="server" ErrorMessage="Please Select STD" ControlToValidate="siblingStd2" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="card card-shw mb-3">
                        <div class="row">
                            <div class="col-md-6 border-r">
                                <h5 class="card-header border-btm">Parents Details (Father/ Guardian)</h5>
                                <div class="card-body <%=bgcss%>">
                                    <span class="note">Note - If no details present, please fill NA</span>
                                    <div class="row form-fields mt-2">
                                        <div class="col-md-6">
                                            <label for="fatherFirstName" class="form-label mb-1">Enter First Name</label>
                                            <asp:TextBox type="text" class="form-control" ID="fatherFirstName" name="fatherFirstName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherFirstName" runat="server" ErrorMessage="Kindly Enter First Name" ControlToValidate="fatherFirstName" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid name.</div>
                                            <div class="invalid-feedback">Please enter first name</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherLastName" class="form-label mb-1">Enter Last Name</label>
                                            <asp:TextBox type="text" class="form-control" ID="fatherLastName" name="fatherLastName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reuiredfatherLastName" runat="server" ErrorMessage="Kindly Enter Last Name" ControlToValidate="fatherLastName" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid name.</div>
                                            <div class="invalid-feedback">Please enter last name</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherNationality" class="form-label">Nationality</label>
                                            <asp:DropDownList class="form-select" ID="fatherNationality" aria-describedby="fatherNationality" runat="server">
                                                <asp:ListItem Selected="True" disabled Value="">Choose...</asp:ListItem>
                                                <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredfatherNationality" InitialValue="" runat="server" ErrorMessage="Please Select Nationality" ControlToValidate="fatherNationality" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="invalid-feedback">Please select a valid option.</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherQualification" class="form-label mb-1">Qualification</label>
                                            <%--<asp:DropDownList class="form-select" id="fatherQualification" aria-describedby="fatherQualification" runat="server" required>
                                                <asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                                <asp:ListItem value="B.E">B.E</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:TextBox type="text" class="form-control" ID="fatherQualification" name="fatherQualification" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherQualification" runat="server" ErrorMessage="Kindly Enter Qualification" ControlToValidate="fatherQualification" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="invalid-feedback">Please select a valid option.</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherOccupation" class="form-label mb-1">Occupation</label>
                                            <asp:TextBox type="text" class="form-control" ID="fatherOccupation" name="fatherOccupation" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredOccupation" runat="server" ErrorMessage="Kindly Enter Occupation" ControlToValidate="fatherOccupation" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid.</div>
                                            <div class="invalid-feedback">Please enter occupation</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherIncome" class="form-label mb-1">Annual Income</label>
                                            <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" class="form-control" ID="fatherIncome" name="fatherIncome" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherIncome" runat="server" ErrorMessage="Kindly Enter Annual Income" ControlToValidate="fatherIncome" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid.</div>
                                            <div class="invalid-feedback">Please enter income</div>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="fatherPAN" class="form-label mb-1">PAN Number</label>
                                            <asp:TextBox type="text" class="form-control" ID="fatherPAN" name="fatherPAN" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherPAN" runat="server" ErrorMessage="Kindly Enter PAN Number" ControlToValidate="fatherPAN" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid PAN.</div>
                                            <div class="invalid-feedback">Please enter PAN number.</div>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="fatherOfficeAddress" class="form-label mb-1">Office Address</label>
                                            <asp:TextBox Rows="3" class="form-control" ID="fatherOfficeAddress" name="fatherOfficeAddress" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherOfficeAddress" runat="server" ErrorMessage="Kindly Enter Office Address" ControlToValidate="fatherOfficeAddress" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid address.</div>
                                            <div class="invalid-feedback">Please enter office address.</div>
                                        </div>

                                        <%--basappa holiday--%>

                                        <div style="display: inline-block;">
                                            <lable>
                                                Are You Employee of Century ?
                                           <%-- <asp:CheckBox ID="chkemployeecheeck" runat="server" Text="Yes" AutoPostBack="true" OnCheckedChanged="chkemployeecheeck_CheckedChanged" />
                                            <asp:CheckBox ID="chkNo" runat="server" Text="No" Checked="true" AutoPostBack="true" OnCheckedChanged="chkNo_CheckedChanged" />--%>
                                                <asp:CheckBox ID="chkemployeecheeck" runat="server" Text="Yes"  CssClass="mb-2 btn float-end btn-sm" ClientIDMode="Static"   /> <%--onclick="myFunction()"--%>
                                                <%--onclick="checkboxchecked()"--%>
                                                <asp:CheckBox ID="chkNo" runat="server" Text="No" CssClass="mb-2 btn float-end btn-sm b" ClientIDMode="Static" Checked />

                                            </lable>
                                        </div>
                                        <%--<br />--%>
                                        <asp:Panel runat="server" ID="pnlemployeepanel"  style="display:none" >
                                            <%--style="display:none"--%>
                                            <div class="row" runat="server"  >
                                                <div class="col-md-6" runat="server"  >
                                                    <label for="lbldepartment" class="form-label">Department</label>
                                                    <asp:TextBox  class="form-control" ID="ddldepartment"   runat="server"></asp:TextBox> <%--name="ddldepartment"--%>

                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" InitialValue="" runat="server" ErrorMessage="Please Select Department" ControlToValidate="ddldepartment" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                                    <div class="invalid-feedback">Please select department.</div>
                                                </div>

                                                <div class="col-md-6">
                                                    <label for="lbltktno" class="form-label mb-1">Ticket No</label>
                                                    <asp:TextBox class="form-control" ID="txttktno"   runat="server"></asp:TextBox> <%--name="txttktno"--%>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Kindly Enter Ticket no" ControlToValidate="txttktno" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                                    <div class="valid-feedback">Valid address.</div>
                                                    <div class="invalid-feedback">Please enter ticket no.</div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <%--basappa holiday--%>
                                        <div class="col-md-6" runat="server" id="A">
                                            <label for="fatherEmailAddress" class="form-label mb-1">Enter Email ID</label>
                                            <asp:TextBox type="email" class="form-control" ID="fatherEmailAddress" name="fatherEmailAddress" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="requiredfatherEmailAddress" runat="server" ErrorMessage="Kindly Enter Email ID" ControlToValidate="fatherEmailAddress" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid email.</div>
                                            <div class="invalid-feedback">Please enter Email ID</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="fatherPhoneNumber" class="form-label mb-1">Enter Mobile Number</label>
                                            <%-- <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" oninput="validatePhoneNumber(this)" MaxLength="10" class="form-control" ID="fatherPhoneNumber" name="fatherPhoneNumber" runat="server" required></asp:TextBox>--%>
                                            <asp:TextBox oninput="validatePhoneNumber(this)" onblur="validatePhoneNumber(this, true)" MaxLength="10" class="form-control" ID="fatherPhoneNumber" name="fatherPhoneNumber" runat="server"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="requiredfatherPhoneNumber" runat="server" ErrorMessage="Kindly Enter Mobile Number" ControlToValidate="fatherPhoneNumber" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <div class="valid-feedback">Valid phone number.</div>
                                            <div class="invalid-feedback">Please enter phone number</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <h5 class="card-header border-btm">Parents Details - Mother</h5>
                                <div class="card-body <%=bgcss%>">
                                    <span class="note">Note - If no details present, please fill NA</span>
                                    <div class="row form-fields mt-2">
                                        <div class="col-md-6">
                                            <label for="motherFirstName" class="form-label mb-1">Enter First Name</label>
                                            <asp:TextBox type="text" class="form-control" ID="motherFirstName" name="motherFirstName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="requiredmotherFirstName" runat="server" ErrorMessage="Kindly Enter First Name" ControlToValidate="motherFirstName" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid name.</div>
                                            <div class="invalid-feedback">Please enter first name</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherLastName" class="form-label mb-1">Enter Last Name</label>
                                            <asp:TextBox type="text" class="form-control" ID="motherLastName" name="motherLastName" runat="server" Style="text-transform: uppercase;"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherLastName" runat="server" ErrorMessage="Kindly Enter Last Name" ControlToValidate="motherLastName" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid name.</div>
                                            <div class="invalid-feedback">Please enter last name</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherNationality" class="form-label mb-1">Nationality</label>
                                            <asp:DropDownList class="form-select" ID="motherNationality" aria-describedby="motherNationality" runat="server">
                                                <asp:ListItem Selected="True" disabled Value="">Choose...</asp:ListItem>
                                                <asp:ListItem Value="Indian">Indian</asp:ListItem>
                                                <asp:ListItem Value="Other">Other</asp:ListItem>
                                            </asp:DropDownList>

                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherNationality" InitialValue="" runat="server" ErrorMessage="Please Select Nationality" ControlToValidate="motherNationality" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="invalid-feedback">Please select a valid option.</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherQualification" class="form-label mb-1">Qualification</label>
                                            <%-- <asp:DropDownList class="form-select" id="motherQualification" aria-describedby="motherQualification" runat="server" required>
                                                <asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                                <asp:ListItem value="B.E">B.E</asp:ListItem>
                                            </asp:DropDownList>--%>

                                            <asp:TextBox type="text" class="form-control" ID="motherQualification" name="motherQualification" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherQualification" runat="server" ErrorMessage="Kindly Enter Qualification" ControlToValidate="motherQualification" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="invalid-feedback">Please select a valid option.</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherOccupation" class="form-label mb-1">Occupation</label>
                                            <asp:TextBox type="text" class="form-control" ID="motherOccupation" name="motherOccupation" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherOccupation" runat="server" ErrorMessage="Kindly Enter Occupation" ControlToValidate="motherOccupation" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid.</div>
                                            <div class="invalid-feedback">Please enter occupation</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherIncome" class="form-label mb-1">Annual Income</label>
                                            <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" class="form-control" ID="motherIncome" name="motherIncome" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherIncome" runat="server" ErrorMessage="Kindly Enter Annual Income" ControlToValidate="motherIncome" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid.</div>
                                            <div class="invalid-feedback">Please enter income</div>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="motherPAN" class="form-label mb-1">PAN Number</label>
                                            <asp:TextBox type="text" class="form-control" ID="motherPAN" name="motherPAN" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherPAN" runat="server" ErrorMessage="Kindly Enter PAN Number" ControlToValidate="motherPAN" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid PAN.</div>
                                            <div class="invalid-feedback">Please enter PAN number.</div>
                                        </div>
                                        <div class="col-md-12">
                                            <label for="motherOfficeAddress" class="form-label mb-1">Office Address</label>
                                            <asp:TextBox Rows="3" class="form-control" ID="motherOfficeAddress" name="motherOfficeAddress" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherOfficeAddress" runat="server" ErrorMessage="Kindly Enter Office Address" ControlToValidate="motherOfficeAddress" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid address.</div>
                                            <div class="invalid-feedback">Please enter office address.</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherEmailAddress" class="form-label mb-1">Enter Email ID</label>
                                            <asp:TextBox type="email" class="form-control" ID="motherEmailAddress" name="motherEmailAddress" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherEmailAddress" runat="server" ErrorMessage="Kindly Enter Email ID" ControlToValidate="motherEmailAddress" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid email.</div>
                                            <div class="invalid-feedback">Please enter Email ID</div>
                                        </div>
                                        <div class="col-md-6">
                                            <label for="motherPhoneNumber" class="form-label mb-1">Enter Mobile Number</label>
                                            <%--onblur="validatePhoneNumber(this, true)"--%>
                                            <asp:TextBox onkeypress="return /[0-9]/i.test(event.key)" MaxLength="10" class="form-control" ID="motherPhoneNumber" name="motherPhoneNumber" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredmotherPhoneNumber" runat="server" ErrorMessage="Kindly Enter Phone/ Mobile Number" ControlToValidate="motherPhoneNumber" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                            <div class="valid-feedback">Valid phone number.</div>
                                            <div class="invalid-feedback">Please enter phone number</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="row form-fields <%=bgcss%>">
                                <div class="col-md-12">
                                    <label for="residentialAddress" class="form-label mb-1">Residential Address</label>
                                    <asp:TextBox Rows="3" class="form-control" ID="residentialAddress" name="residentialAddress" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredResidentialAddress" runat="server" ErrorMessage="Kindly Enter Residential Address" ControlToValidate="residentialAddress" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <div class="valid-feedback">Valid address.</div>
                                    <div class="invalid-feedback">Please enter address.</div>
                                </div>
                                <div class="col-md-6">
                                    <label for="locality" class="form-label mb-1">Enter Locality</label>
                                    <%-- <asp:DropDownList class="form-select" id="locality" aria-describedby="locality" runat="server" required>
                                        <asp:ListItem selected="True" disabled value="">Choose...</asp:ListItem>
                                        <asp:ListItem value="ABC">ABC</asp:ListItem>
                                        <asp:ListItem value="PQR">PQR</asp:ListItem>
                                    </asp:DropDownList>--%>
                                    <asp:TextBox type="text" class="form-control" ID="locality" name="locality" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="Requiredlocality" runat="server" ErrorMessage="Kindly Enter Locality" ControlToValidate="locality" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                    <div class="valid-feedback">Valid option.</div>
                                    <div class="invalid-feedback">Please Enter locality.</div>
                                </div>

                                <%--<a href="#">Click here to see the fees structure.</a>--%>
                                <%-- <div class="col-md-12 mt-2">
                                    <asp:CheckBox type="checkbox" class="form-check-asp:TextBox" ID="checkFeeStructure" runat="server" required></asp:CheckBox>
                                    <label class="form-check-label" for="checkFeeStructure">The above filled details are true to the best of my knowledge.</label>
                                    <div class="invalid-feedback">Please select the checkbox</div>
                                    <div class="valid-feedback">Checked.</div>
                                </div>--%>
                            </div>
                        </div>
                    </div>


   <div class="card card-shw mb-3">
                        <div class="row">
                            <div class="col-md-12">
                                <h5 class="card-header border-btm">Upload Documents</h5>
                                <div class="card-body <%=bgcss%>">

                                    <div class="row form-fields mt-2">

                                        <div class="col-md-4">
                                            <label for="childPhoto" class="form-label mb-1">Recent Passport Photo of Child </label>
                                            <%--(Max 500 KB - .jpg, .png, .jpeg allowed)--%>
                                            <asp:FileUpload class="form-control" accept=".png,.jpg,.jpeg,.gif" type="file" ID="childPhoto" name="childPhoto" runat="server" onchange="childPhotoChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Photo uploaded.</div>
                                            <div class="invalid-feedback">Please select a photo.</div>
                                            <asp:Label Text="" runat="server" ID="lblphotopath" Style="display: inline;" />
                                            <asp:Image ID="imgPhoto" ImageUrl="Resources/Images/imageupload.png" runat="server" Style="width: 200px; height: 200px; margin: auto; display: block;" />

                                        </div>


                                        <div class="col-md-4">
                                            <label for="birthCertificate" class="form-label mb-1" style="display: inline-block">Birth Certificate (Muncipal / Gram Panchayat)</label>
                                            <%-- (Max 1 MB - .pdf allowed)--%>
                                            <%--<asp:FileUpload class="form-control" accept="application/pdf" type="file" ID="birthCertificate" name="birthCertificate" runat="server" onchange="birthCertChange(event)"></asp:FileUpload>--%>
                                            <asp:FileUpload class="form-control" accept=".png,.jpg,.jpeg,.gif" type="file" ID="birthCertificate" name="birthCertificate" runat="server" onchange="birthCertChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Certificate uploaded.</div>
                                            <div class="invalid-feedback">Please upload a birth certificate.</div>
                                            <asp:Label Text="" runat="server" ID="lblbirthcertificate" Style="display: inline;" />

                                            <%--<asp:Image ID="imgBirthCertificate" ImageUrl="Resources/Images/freepdf.png" runat="server" Style="width: 200px; height: 200px; margin: auto; display: block;" />--%>
                                            <asp:Image ID="imgBirthCertificate" ImageUrl="Resources/Images/imageupload.png" runat="server" Style="width: 200px; height: 200px; margin: auto; display: block;" />
                                            <a runat="server" target="_blank" id="BirthCert" visible="false" style="color: red">View Birth Certificate</a>
                                        </div>



                                        <div class="col-md-4">
                                            <label for="residentialProof" class="form-label mb-1">Residential Proof (Aadhar Card)</label>
                                            <%--<asp:FileUpload class="form-control" type="file" accept="application/pdf" ID="residentialProof" name="residentialProof" runat="server" onchange="residentialProofChange(event)"></asp:FileUpload>--%>
                                            <asp:FileUpload class="form-control" type="file" accept=".png,.jpg,.jpeg,.gif" ID="residentialProof" name="residentialProof" runat="server" onchange="residentialProofChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Document uploaded.</div>
                                            <div class="invalid-feedback">Please upload document.</div>
                                            <asp:Label Text="" runat="server" ID="lblresidentialpath" Style="display: inline;" />

                                            <%--<asp:Image ID="imgResidential" runat="server" ImageUrl="Resources/Images/freepdf.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />--%>
                                            <asp:Image ID="imgResidential" runat="server" ImageUrl="Resources/Images/imageupload.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />
                                            <a runat="server" target="_blank" id="A1" visible="false" style="color: red">View Residential Proof </a>
                                        </div>

                                        <div class="col-md-4">
                                            <label for="birthCertificate" class="form-label mb-1">Leaving Certificate </label>
                                            <%--<asp:FileUpload class="form-control" accept="application/pdf" type="file" ID="tcproff" name="transferCertificate" runat="server" onchange="tcCertChange(event)"></asp:FileUpload>--%>
                                            <asp:FileUpload class="form-control" accept=".png,.jpg,.jpeg,.gif" type="file" ID="tcproff" name="transferCertificate" runat="server" onchange="tcCertChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Certificate uploaded.</div>
                                            <div class="invalid-feedback">Please upload a Transfer certificate.</div>
                                            <asp:Label Text="" runat="server" ID="lbltcpath" Style="display: inline;" />

                                            <%--<asp:Image ID="imgtc" ImageUrl="Resources/Images/freepdf.png" runat="server" Style="width: 200px; height: 200px; margin: auto; display: block;" />--%>
                                            <asp:Image ID="imgtc" ImageUrl="Resources/Images/imageupload.png" runat="server" Style="width: 200px; height: 200px; margin: auto; display: block;" />
                                            <a runat="server" target="_blank" id="transfercert" visible="false" style="color: red">View Transfer Certificate </a>
                                        </div>

                                        <div class="col-md-4">
                                            <label for="residentialProof" class="form-label mb-1">ID of Father</label>
                                            <%--<asp:FileUpload class="form-control" type="file" accept="application/pdf" ID="otherfile" name="otherProof" runat="server" onchange="otherProofChange(event)"></asp:FileUpload>--%>
                                            <asp:FileUpload class="form-control" type="file" accept=".png,.jpg,.jpeg,.gif" ID="otherfile" name="otherProof" runat="server" onchange="otherProofChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Document uploaded.</div>
                                            <div class="invalid-feedback">Please upload document.</div>
                                            <asp:Label Text="" runat="server" ID="otherpf" Style="display: inline;" />

                                            <%--<asp:Image ID="otherimg" runat="server" ImageUrl="Resources/Images/freepdf.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />--%>
                                            <asp:Image ID="otherimg" runat="server" ImageUrl="Resources/Images/imageupload.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />
                                            <a runat="server" target="_blank" id="otherproof" visible="false" style="color: red">View Other  Proof </a>
                                        </div>

                                        <%--basappa holiday--%>
                                        <div class="col-md-4">
                                            <label for="residentialProof" class="form-label mb-1">Caste Certificate</label>
                                            <%--<asp:FileUpload class="form-control" type="file" accept="application/pdf" ID="castefile" name="castefile" runat="server" onchange="othercasteChange(event)"></asp:FileUpload>--%>
                                            <asp:FileUpload class="form-control" type="file" accept=".png,.jpg,.jpeg,.gif" ID="castefile" name="castefile" runat="server" onchange="othercasteChange(event)"></asp:FileUpload>
                                            <div class="valid-feedback">Certificate uploaded.</div>
                                            <div class="invalid-feedback">Please upload Caste Certificate.</div>
                                            <asp:Label Text="" runat="server" ID="ccerticate" Style="display: inline;" />

                                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="Resources/Images/freepdf.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />--%>
                                            <asp:Image ID="Image1" runat="server" ImageUrl="Resources/Images/imageupload.png" Style="width: 200px; height: 200px; margin: auto; display: block;" />
                                            <a runat="server" target="_blank" id="Castefiles" visible="false" style="color: red">View Caste Certificate </a>
                                        </div>
                                        <%--basappa holiday--%>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>


                </div>
            </div>
            <div class="float-end">
                <div class=" <%=isFeesAdmin %>">
                    <asp:RadioButton runat="server" ID="confirmadmission" Text="Confirm Admission" class="form-check-input" GroupName="admission" value="Confirm" />
                    <asp:RadioButton runat="server" ID="pendingadmission" Text="Pending Admission" class="form-check-input" GroupName="admission" value="Pending" Checked />
                </div>
                <asp:Button runat="server" OnClick="submitAdmissionForm_Click" ID="submitAdmissionForm" class="btn btn-warning btn-lg" Text="Submit Form" CausesValidation="true" />
                <%--OnClientClick="validationonclick();"--%>

                <asp:Button runat="server" OnClientClick="return resetclick();" OnClick="resetAdmissionFrom_Click" ID="resetAdmissionFrom" class="btn btn-success btn-lg" Text="Reset" CausesValidation="false" />

            </div>



            <!-- Modal -->
            <div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">Application Submitted</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label runat="server" class="text-success fw-bolder mb-2" ID="successMsg"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" style="font-weight: bold;">Alert Message!!</h5>

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

            <div class="modal fade" id="fileInfoModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content" style="background-color: transparent; box-shadow: none; border: 0px;">
                        <div class="modal-header">

                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <iframe src="#" id="iframeFileInfo" style="display: block; width: 100%; height: 600px; margin: auto;"></iframe>

                        </div>

                    </div>
                </div>
            </div>


            <div class="modal fade" id="noFileModal" tabindex="-1" role="dialog" aria-labelledby="noFileModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="noFileModalLabel">No File Available</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <p>There is no file available for the selected record.</p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


        </section>
    </div>

    <script>

        $(document).ready(function () {

            $("#navadmissionlink").addClass("active");


            $('#ContentPlaceHolder1_motherTounge').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_studName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_middleName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_surName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_siblingName1').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_siblingName2').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });



            $('#ContentPlaceHolder1_fatherFirstName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_fatherLastName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_motherFirstName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { // || e.altKey
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

            $('#ContentPlaceHolder1_motherLastName').keydown(function (e) {
                if (e.shiftKey || e.ctrlKey) { //|| e.altKey    
                    e.preventDefault();
                } else {
                    var key = e.keyCode;
                    if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                        e.preventDefault();
                    }
                }
            });

        });


        var childPhotoChange = function (event) {

            $("#ContentPlaceHolder1_lblphotopath").text("");

            for (var i = 0; i < event.target.files.length; i++) {


                var trow = "", gbase = "";

                var promise = getBase64(event.target.files[i]);
                promise.then(function (result) {
                    //console.log(result);
                    gbase = result;

                    //$("#imgbase64").text(gbase);
                    $("#ContentPlaceHolder1_imgPhoto").attr("src", gbase);

                });

            }



        };

        var birthCertChange = function (event) {
            $("#ContentPlaceHolder1_lblbirthcertificate").text("");
        };

        var residentialProofChange = function (event) {
            $("#ContentPlaceHolder1_lblresidentialpath").text("");
        }

        var TransferProofChange = function (event) {
            $("#ContentPlaceHolder1_lbltcpath").text("");
        }
        var otherProofChange = function (event) {
            $("#ContentPlaceHolder1_otherpf").text("");
        }

        var othercasteChange = function (event) { //basappa holiday
            $("#ContentPlaceHolder1_ccerticate").text("");
        }


        function getBase64(file, onLoadCallback) {
            return new Promise(function (resolve, reject) {
                var reader = new FileReader();
                reader.onload = function () { resolve(reader.result); };
                reader.onerror = reject;
                reader.readAsDataURL(file);
            });
        }
        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }

<%--        function ViewRecord() {

            var imageid =  document.getElementById('<%= lblbirthcertificate.ClientID %>').text;
          //  document.getElementById('<%= fatherIncome.ClientID %>').value;
            console.log(imageid)
            if (imageid) {


                var url = weburl + imageid;
                //document.getElementById('<%= iframeFileInfo.ClientID %>').attr(""src", url")
                $("#iframeFileInfo").attr("src", url);

                $("#fileInfoModal").modal("show");
            }
            else {
                $("#noFileModal").modal("show");
            }
        }
    --%>



    </script>
</asp:Content>
