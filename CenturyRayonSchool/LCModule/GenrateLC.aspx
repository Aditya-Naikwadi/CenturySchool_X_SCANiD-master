<%@ Page Title="" Language="C#" MasterPageFile="~/LCModule/LeavingMaster.Master" AutoEventWireup="true" CodeFile="GenrateLC.aspx.cs" Inherits="CenturyRayonSchool.LCModule.GenrateLC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .div-academicyear {
            position: absolute;
            top: 12px;
            right: 14px;
        }

        .ftrow {
            font-size: small;
            text-align: center;
        }

        .ftrow1 {
            padding: 4px;
            color: black;
            font-weight: bolder;
            height: 50px;
            width: 150px;
            text-align: center;
        }

        .c-visible {
            display: none;
        }

        .uppercase {
            text-transform: uppercase;
        }

        .btn-dark {
            border-radius: 50px !important;
        }

        .text-left {
            text-align: left;
        }

        .col-id-no {
            left: 0 !important;
            position: sticky !important;
            background-color: lightgray;
        }

        .col-id {
            background-color: lightblue;
        }

        .fixed-header {
            z-index: 50 !important;
        }

        .box {
            border: 2px solid #0000005e; /* Add a border around the "box" div */
            padding: 10px; /* Add padding for spacing */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile">
                <a href="LCDashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Genrate LC
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
               <div class="card-header card-mobile">
                
                 <div class="col-lg-3 col-md-6">
                     <label for="lbl" class="form-label mb-1">Academic Year</label>
                        <asp:DropDownList ID="cmbAcademicyear" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAcademicyear_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </div>
            </div>
            <div class="card-body margin-rows">
                <div class="box" style="margin: 10px">
                    <div class="row">
                        <div class="col-md-3">
                            <label for="cmbStd" class="form-label mb-1">Std</label>
                            <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" OnServerValidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>

                        </div>
                        <div class="col-md-2">
                            <label for="cmbdiv" class="form-label mb-1">Div</label>
                            <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" OnServerValidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                        </div>
                        <div class="col-md-4">
                            <label for="cmbStduname" class="form-label mb-1">Select StudentName</label>
                            <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server" CausesValidation="false" AutoPostBack="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3" style="margin-top: 16px;">
                            <button runat="server" id="searchstudent" class="btn btn-saveData mt-2" onserverclick="searchstudent_ServerClick" style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Get Data</button>
                        </div>

                    </div>
                </div>
                <div class="box">
                    <div class="row">
                        <div class="col-md-2">
                            <label for="lbllcno" class="form-label mb-1">Leaving Certificate No. :</label>
                            <asp:TextBox ID="txtlcno" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lbllcardid" class="form-label mb-1">Card ID :</label>
                            <asp:TextBox ID="txtcrdid" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="lblaadhar" class="form-label mb-1">Section:</label>
                            <asp:TextBox ID="txtsection" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblstd" class="form-label mb-1">STD :</label>
                            <asp:TextBox ID="txtstd" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lbldiv" class="form-label mb-1">DIV :</label>
                            <asp:TextBox ID="txtdiv" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblaadhar" class="form-label mb-1">Roll No. :</label>
                            <asp:TextBox ID="txtrollno" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblgrno" class="form-label mb-1">GR. No. :</label>
                            <asp:TextBox ID="txtgrno" runat="server" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label for="lblsaral" class="form-label mb-1">Saral Student ID :</label>
                            <asp:TextBox ID="Txtsaral" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="lblaadhar" class="form-label mb-1">AADHAR CARD :</label>
                            <asp:TextBox ID="Txtaadhar" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="lblsurname" class="form-label mb-1">SurName :</label>
                            <asp:TextBox ID="txtsurname" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="lblsaral" class="form-label mb-1">Student Name:</label>
                            <asp:TextBox ID="txtstuname" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="lblsaral" class="form-label mb-1">Father Name :</label>
                            <asp:TextBox ID="txtfather" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="lblmothername" class="form-label mb-1">Mother Name :</label>
                            <asp:TextBox ID="txtmother" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <label for="lblrelgon" class="form-label mb-1">Religion :</label>
                            <asp:TextBox ID="textrelgon" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblcaste" class="form-label mb-1">Caste :</label>
                            <asp:TextBox ID="txtcast" runat="server" type="text" class="form-control"  ></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <label for="lblsubcast" class="form-label mb-1">Subcaste :</label>
                            <asp:TextBox ID="txtsubcast" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblcatg" class="form-label mb-1">Category :</label>
                            <asp:TextBox ID="txtcateg" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblstd" class="form-label mb-1">Nationality :</label>
                            <asp:TextBox ID="txtnational" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblstd" class="form-label mb-1">Mother Tongue :</label>
                            <asp:TextBox ID="txtmothertong" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="form-label select-label" for="txt_dateofbirth">Date Of Birth :</label>
                            <input type="date" class="form-control" id="txt_dateofbirth" name="trip-start" onchange="setDateOnLabel()" />

                            <asp:TextBox ID="txtdob" CssClass="c-visible" runat="server" />
                        </div>
                        <div class="col-lg-9">
                            <label for="lblstd" class="form-label mb-1">DOB In Words</label>
                            <asp:TextBox ID="dobwords" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="lblpob" class="form-label mb-1">Place Of Birth :</label>
                            <asp:TextBox ID="txtpob" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lbltaluka" class="form-label mb-1">Taluka :</label>
                            <asp:TextBox ID="txttluka" runat="server" type="text" class="form-control"  ></asp:TextBox>

                        </div>
                        <div class="col-md-2">
                            <label for="lbldist" class="form-label mb-1">District :</label>
                            <asp:TextBox ID="txtdist" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="lblstat" class="form-label mb-1">State :</label>
                            <asp:TextBox ID="txtstat" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label for="blbcountry" class="form-label mb-1">Country :</label>
                            <asp:TextBox ID="txtconunt" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label for="blbcountry" class="form-label mb-1">Last School :</label>
                            <asp:TextBox ID="txtlstschool" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="blbcountry" class="form-label mb-1">Admission In Std : :</label>
                            <asp:TextBox ID="txtadmiinstd" runat="server" type="text" class="form-control"  ></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="blbcountry" class="form-label mb-1">APAAR ID :</label>
                            <asp:TextBox ID="txtapaarid" runat="server" type="text" class="form-control" ReadOnly="false"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            <label for="blbcountry" class="form-label mb-1">PEN NO :</label>
                            <asp:TextBox ID="txtpenno" runat="server" type="text" class="form-control" ReadOnly="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <label class="form-label select-label" for="txt_doa">Date Of Admission</label>
                            <input type="date" class="form-control" id="txt_doa" name="trip-start" onchange="syncDateofadmiValues()" />

                            <asp:TextBox ID="txtdoa" CssClass="c-visible" runat="server" Style="display: none;"/>
                        </div>
                        <div class="col-md-3">
                            <label class="form-label select-label" for="txt_dol">Date Of Leaving :</label>
                            <input type="date" class="form-control" id="txt_dol" name="trip-start" onchange="syncDateValues()" />
                            <asp:TextBox ID="dateofleaving" CssClass="c-visible" runat="server" Style="display: none;" />
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="blbcountry" class="form-label mb-1">Progress :</label>
                            <asp:TextBox ID="txtprogress" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label for="blbcountry" class="form-label mb-1">Conduct :</label>
                            <asp:TextBox ID="txtcond" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <label for="blbcountry" class="form-label mb-1">Std In Studying :</label>
                            <asp:TextBox ID="txtsis" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>

                        <div class="col-md-4">
                            <label for="blbcountry" class="form-label mb-1">Freeship:</label>
                            <asp:TextBox ID="txtfreeship" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <label for="blbcountry" class="form-label mb-1">Reason Of Leaving :</label>
                            <asp:TextBox ID="txtrol" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <label for="blbcountry" class="form-label mb-1">Remark :</label>
                            <asp:TextBox ID="txtreark" runat="server" type="text" class="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div style="display: flex; justify-content: center; align-items: center; height: 15vh;">
                        <button runat="server" id="SaveLC" class="btn btn-saveData mt-2 btn-large" onserverclick="SaveLC_ServerClick" style="width: 30%; height: 55px; font-size: 30px"><i class="fas fa-angle-double-down mr-2"></i>Save Data</button>
                    </div>

                </div>
            </div>

        </div>
    </section>
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

    <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Information Message.</h5>

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


    <script>
        $(document).ready(function (e) {
            $("#LCModuleSection").addClass("menu-open");
            $("#LCModuleItem").addClass("active");
            $("#menu_studentlist").addClass("active");

            $(".select2").select2();
            var dol = $("#ContentPlaceHolder1_dateofleaving").val();

            $("#txt_dol").val(dol.replace('/', '-').replace('/', '-'));

            var doa = $("#ContentPlaceHolder1_txtdoa").val();
            $("#txt_doa").val(doa.replace('/', '-').replace('/', '-'));

            var dob = $("#ContentPlaceHolder1_txtdob").val();
            $("#txt_dateofbirth").val(dob.replace('/', '-').replace('/', '-'));


        });

        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }
        function setDateOnLabel() {
            var dateInput = document.getElementById('txt_dateofbirth').value;
            document.getElementById('<%= txtdob.ClientID %>').value = dateInput;
        }

        // Synchronize initial value on page load
        window.onload = function () {
            var dateInput = document.getElementById('txt_dateofbirth');
            var hiddenField = document.getElementById('<%= txtdob.ClientID %>');
            dateInput.value = hiddenField.value;

        }
        function syncDateValues() {
            var dateInput = document.getElementById('txt_dol').value;
            document.getElementById('<%= dateofleaving.ClientID %>').value = dateInput;
        }

        // Synchronize initial value on page load
        window.onload = function () {
            var dateInput = document.getElementById('txt_dol');
            var hiddenField = document.getElementById('<%= dateofleaving.ClientID %>');
            dateInput.value = hiddenField.value;
        };
        function syncDateofadmiValues() {
            var dateInput = document.getElementById('txt_doa').value;
            document.getElementById('<%= txtdoa.ClientID %>').value = dateInput;
        }

        // Synchronize initial value on page load
        window.onload = function () {
            var dateInput = document.getElementById('txt_doa');
            var hiddenField = document.getElementById('<%= txtdoa.ClientID %>');
            dateInput.value = hiddenField.value;
        };
    </script>
</asp:Content>
