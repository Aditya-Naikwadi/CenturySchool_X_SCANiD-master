<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeBehind="StudentList.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.StudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /*-----------------
	2. Table
-----------------------*/

        .table {
            color: #333;
            max-width: 100%;
            margin-bottom: 0;
            width: 100%;
        }

            .table > :not(:first-child) {
                border-top: transparent;
            }

            .table > :not(caption) > * > * {
                box-shadow: unset;
                padding: 0.75rem;
            }

            .table thead th {
                vertical-align: bottom;
                border-bottom: 1px solid #dee2e6;
            }

        .table-responsive {
            white-space: nowrap;
        }

        .table-striped > tbody > tr:nth-of-type(2n+1) {
            background-color: #f8f9fa;
        }

        .table.no-border > tbody > tr > td,
        .table > tbody > tr > th,
        .table.no-border > tfoot > tr > td,
        .table.no-border > tfoot > tr > th,
        .table.no-border > thead > tr > td,
        .table.no-border > thead > tr > th {
            border-top: 0;
            padding: 10px 8px;
        }

        .table-nowrap td,
        .table-nowrap th {
            white-space: nowrap
        }

        .table.dataTable {
            border-collapse: collapse !important;
            border: 1px solid rgba(0, 0, 0, 0.05);
        }

        table.table td h2 {
            display: inline-block;
            font-size: inherit;
            font-weight: 400;
            margin: 0;
            padding: 0;
            vertical-align: middle;
        }

            table.table td h2.table-avatar {
                align-items: center;
                display: inline-flex;
                font-size: inherit;
                font-weight: 400;
                margin: 0;
                padding: 0;
                vertical-align: middle;
                white-space: nowrap;
            }

            table.table td h2 a {
                color: #333;
            }

                table.table td h2 a:hover {
                    color: #3d5ee1;
                }

            table.table td h2 span {
                color: #888;
                display: block;
                font-size: 12px;
                margin-top: 3px;
            }

        .table thead tr th {
            font-weight: 600;
        }

        .table tbody tr {
            border-bottom: 1px solid #dee2e6;
        }

        .table.table-center td,
        .table.table-center th {
            vertical-align: middle;
        }

        .table-hover tbody tr:hover {
            background-color: #f7f7f7;
        }

            .table-hover tbody tr:hover td {
                color: #474648;
            }

        .table-striped thead tr {
            border-color: transparent;
        }

        .table-striped tbody tr {
            border-color: transparent;
        }

            .table-striped tbody tr:nth-of-type(even) {
                background-color: rgba(255, 255, 255, 0.3);
            }

            .table-striped tbody tr:nth-of-type(odd) {
                background-color: rgba(235, 235, 235, 0.4);
            }

        .table-bordered {
            border: 1px solid rgba(0, 0, 0, 0.05) !important;
        }

            .table-bordered th,
            .table-bordered td {
                border-color: rgba(0, 0, 0, 0.05);
            }

        .card-table .card-body .table > thead > tr > th {
            border-top: 0;
        }

        .card-table .card-body .table tr td:first-child,
        .card-table .card-body .table tr th:first-child {
            padding-left: 1.5rem;
        }

        .card-table .card-body .table tr td:last-child,
        .card-table .card-body .table tr th:last-child {
            padding-right: 1.5rem;
        }

        .card-table .table td, .card-table .table th {
            border-top: 1px solid #e2e5e8;
            padding: 1rem 0.75rem;
            white-space: nowrap;
        }

        .table .thead-light th {
            color: #495057;
            background-color: #f8f9fa;
            border-color: #eff2f7;
        }

        .table .student-thread th {
            color: #000;
            background-color: #f8f9fa;
            border-color: #eff2f7;
        }

        .float-end {
            float: right !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="student-group-form">
        <div class="row">
            <div class="col-lg-3 col-md-6">
                <label for="cmbStd" class="form-label mb-1">Academic Year</label>
                <asp:DropDownList ID="cmbAcademicyear" class="form-control select2" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2 col-md-3">
                <label for="cmbStd" class="form-label mb-1">Std</label>
                <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2 col-md-3">
                <label for="cmbStd" class="form-label mb-1">Div</label>
                <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-lg-2 col-md-6">
                <button runat="server" id="btnGenerateExcel" class="btn btn-dark mt-4" onserverclick="btnGenerateExcel_ServerClick" causesvalidation="false"><i class="fas fa-excel mr-2"></i>Generate Excel</button>
            </div>
            <div class="col-lg-2 col-md-6">
                <asp:FileUpload ID="FileUpload1" Style="display: none" runat="server" onchange="upload()" />
                <input type="button" value="Upload Excel" onclick="showBrowseDialog()" class="btn btn-primary mt-4" />
                <button runat="server" id="btnUploadExcel" class="btn btn-primary mt-4" style="display: none;" onserverclick="btnUploadExcel_ServerClick" causesvalidation="false"><i class="fas fa-excel mr-2"></i>Upload Excel</button>
            </div>
        </div>
    </div>

    <div class="student-group-form mt-2">
        <h6>*Press Enter Key for Searching Student Data *</h6>
        <div class="row">
            <div class="col-lg-3 col-md-6">
                <div class="form-group">
                    <input type="text" class="form-control" id="studName" placeholder="Search by Fullname ...">
                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="form-group">
                    <input type="text" class="form-control" id="studRFID" placeholder="Search by RFID ...">
                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="form-group">
                    <input type="text" class="form-control" id="studGrno" placeholder="Search by GRNO ...">
                </div>
            </div>

        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">

            <div class="card card-table comman-shadow">
                <div class="card-body">

                    <!-- Page Header -->
                    <div class="page-header">
                        <div class="row align-items-center">
                            <div class="col-md-4">
                                <h3 class="page-title">Students - <span runat="server" id="studCount"></span></h3>
                            </div>
                            <div class="col-md-4">
                                <h3 class="page-title">Academic Year -
                                    <asp:Label Text="0" runat="server" ID="lblAcademicyear" /></h3>
                            </div>
                            <div class="col-md-4 text-end  ms-auto download-grp">

                                <a href="StudentMaster.aspx?mode=add" class="btn btn-primary float-end"><i class="fas fa-plus"></i>Add Student</a>
                            </div>
                        </div>
                    </div>
                    <!-- /Page Header -->

                    <div class="table-responsive">
                        <asp:GridView ID="allStudentsGrid" runat="server" AutoGenerateColumns="false" class="table border-0 star-student table-hover table-center mb-0 datatable table-striped" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:BoundField DataField="Srno" HeaderText="Srno" ItemStyle-CssClass="srno" />
                                <asp:BoundField DataField="Fullname" HeaderText="Student Name" ItemStyle-CssClass="Fullname" />
                                <asp:BoundField DataField="Grno" HeaderText="GRNO" ItemStyle-CssClass="Grno" />
                                <asp:BoundField DataField="STD" HeaderText="STD" ItemStyle-CssClass="STD" />
                                <asp:BoundField DataField="DIV" HeaderText="DIV" ItemStyle-CssClass="DIV" />
                                <asp:BoundField DataField="DOB" HeaderText="DOB" ItemStyle-CssClass="DOB" />
                                <asp:BoundField DataField="CARDID" HeaderText="RFID Number" ItemStyle-CssClass="CARDID" />
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <div class="actions">

                                            <a href="<%# String.Format("StudentMaster.aspx?mode=edit&std={0}&grno={1}", Eval("STD"),Eval("Grno")) %>" class="btn btn-sm bg-danger-light">
                                                <i class="fa fa-edit"></i>Edit
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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


    <script>

        function showBrowseDialog() {
            var fileuploadctrl = document.getElementById('<%= FileUpload1.ClientID %>');
            fileuploadctrl.click();
        }

        function upload() {
            var btn = document.getElementById('<%= btnUploadExcel.ClientID %>');
            btn.click();
        }

        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }

        $(document).ready(function () {


            $("#studName").on("keypress", function (e) {
                if (e.keyCode == 13) {

                    var table, tr, i, t_status, tvalue_status;
                    var value = $("#studName").val().trim().toLowerCase();
                    //alert(value);
                    table = document.getElementById("ContentPlaceHolder1_allStudentsGrid");
                    tr = table.getElementsByTagName("tr");

                    if (value.length > 3) {

                        for (i = 0; i < tr.length; i++) {
                            t_status = tr[i].getElementsByTagName("td")[1];
                            if (t_status) {

                                tvalue_status = t_status.textContent || t_status.innerText;
                                if (tvalue_status.toLowerCase().indexOf(value) > -1) {
                                    tr[i].style.display = "";
                                } else {
                                    tr[i].style.display = "none";
                                }

                            }
                        }

                    }
                    else {
                        for (i = 0; i < tr.length; i++) {
                            tr[i].style.display = "";
                        }
                    }


                }

            });


            $("#studRFID").on("keypress", function (e) {

                if (e.keyCode == 13) {

                    var table, tr, i, t_status, tvalue_status;
                    var value = $("#studRFID").val().trim();
                    //alert(value);
                    table = document.getElementById("ContentPlaceHolder1_allStudentsGrid");
                    tr = table.getElementsByTagName("tr");

                    if (value.length > 3) {

                        for (i = 0; i < tr.length; i++) {
                            t_status = tr[i].getElementsByTagName("td")[6];
                            if (t_status) {

                                tvalue_status = t_status.textContent || t_status.innerText;
                                if (tvalue_status.indexOf(value) > -1) {
                                    tr[i].style.display = "";
                                } else {
                                    tr[i].style.display = "none";
                                }

                            }
                        }

                    }
                    else {
                        for (i = 0; i < tr.length; i++) {
                            tr[i].style.display = "";
                        }
                    }


                }


            });

            $("#studGrno").on("keypress", function (e) {

                if (e.keyCode == 13) {

                    var table, tr, i, t_status, tvalue_status;
                    var value = $("#studGrno").val().trim();
                    //alert(value);
                    table = document.getElementById("ContentPlaceHolder1_allStudentsGrid");
                    tr = table.getElementsByTagName("tr");

                    if (value.length > 3) {

                        for (i = 0; i < tr.length; i++) {
                            t_status = tr[i].getElementsByTagName("td")[2];
                            if (t_status) {

                                tvalue_status = t_status.textContent || t_status.innerText;
                                if (tvalue_status.indexOf(value) > -1) {
                                    tr[i].style.display = "";
                                } else {
                                    tr[i].style.display = "none";
                                }

                            }
                        }

                    }
                    else {
                        for (i = 0; i < tr.length; i++) {
                            tr[i].style.display = "";
                        }
                    }


                }

            });



            $("form").bind("keypress", function (e) {
                if (e.keyCode == 13) {
                    //add more buttons here
                    return false;
                }
            });

        });



        //function searchStudentName(t) {
        //    var table, tr, i, t_status, tvalue_status;
        //    var value = t.val().trim().toLowerCase();
        //    //alert(value);

        //    if (value.length > 3) {

        //        table = document.getElementById("ContentPlaceHolder1_allStudentsGrid");
        //        tr = table.getElementsByTagName("tr");
        //        if (value == "") {
        //            for (i = 0; i < tr.length; i++) {
        //                tr[i].style.display = "";
        //            }


        //        }
        //        else {

        //            for (i = 0; i < tr.length; i++) {
        //                t_status = tr[i].getElementsByTagName("td")[1];
        //                if (t_status) {

        //                    tvalue_status = t_status.textContent || t_status.innerText;
        //                    if (tvalue_status.toLowerCase().indexOf(value) > -1) {
        //                        tr[i].style.display = "";
        //                    } else {
        //                        tr[i].style.display = "none";
        //                    }

        //                }
        //            }
        //        }



        //    }

        //}

    </script>

</asp:Content>
