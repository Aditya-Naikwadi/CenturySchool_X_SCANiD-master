<%@ Page Title="" Language="C#" MasterPageFile="~/LCModule/LeavingMaster.Master" AutoEventWireup="true" CodeBehind="LCSettings.aspx.cs" Inherits="CenturyRayonSchool.LCModule.LCSettings" %>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile">
                <a href="LCDashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Leaving Cerificate's Settings
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-4">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" OnServerValidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>

                    </div>
                </div>
                <div class=" row">
                    <div class="col-md-4">
                        <label for="lbllcno" class="form-label mb-1">Progress :</label>
                        <asp:TextBox ID="txtprogress" runat="server" type="text" class="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label for="lbllcardid" class="form-label mb-1">Conduct :</label>
                        <asp:TextBox ID="txtconduct" runat="server" type="text" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-3" style="margin-top: 16px;">
                        <button runat="server" id="updateprogress" class="btn btn-saveData mt-2" onserverclick="updateprogress_ServerClick" style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update ALL</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="lbllcno" class="form-label mb-1">Reason Of Leaving :</label>
                        <asp:TextBox ID="txtrol" runat="server" type="text" class="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4" style="margin-top: 16px;">
                        <button runat="server" id="btnrol" class="btn btn-saveData mt-2" onserverclick="btnrol_ServerClick" style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update ALL</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="lbllcno" class="form-label mb-1">Remarks :</label>
                        <asp:TextBox ID="txtremrk" runat="server" type="text" class="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4" style="margin-top: 16px;">
                        <button runat="server" id="btnremrk" class="btn btn-saveData mt-2" onserverclick="btnremrk_ServerClick"   style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update ALL</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="lbllcno" class="form-label mb-1">Freeship :</label>
                        <asp:TextBox ID="txtfree" runat="server" type="text" class="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4" style="margin-top: 16px;">
                        <button runat="server" id="btnfreeship" class="btn btn-saveData mt-2" onserverclick="btnfreeship_ServerClick"   style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update ALL</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <label for="lbllcno" class="form-label mb-1">Std In Which Studying :</label>
                        <asp:TextBox ID="txtsis" runat="server" type="text" class="form-control" ></asp:TextBox>
                    </div>
                    <div class="col-md-4" style="margin-top: 16px;">
                        <button runat="server" id="btnsis" class="btn btn-saveData mt-2" onserverclick="btnsis_ServerClick" style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update ALL</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4" style="margin-top: 16px;">
                        <button runat="server" id="btndob" class="btn btn-saveData mt-2" onserverclick="btndob_ServerClick" style="width: 80%;"><i class="fas fa-angle-double-down mr-2"></i>Update DOB Words ALL</button>
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
            $("#menu_seting").addClass("active");

            $(".select2").select2();

           
        });

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
