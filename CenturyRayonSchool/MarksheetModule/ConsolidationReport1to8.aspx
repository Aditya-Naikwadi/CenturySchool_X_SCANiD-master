<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="ConsolidationReport1to8.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.ConsolidationReport1to8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .div-academicyear {
            position: absolute;
            top: 12px;
            right: 14px;
        }

        .ftrow {
            font-size: 20px;
            text-align: center;
        }

        .ftrow1 {
            padding: 4px;
            color: black;
            font-weight: bolder;
            height: 50px;
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
            background-color:lightblue;
        }

        .fixed-header {
            z-index: 50 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
               Consolidation Report
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                
                <div class="row">
                    <div class="col-md-2">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" onservervalidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbdiv" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" onservervalidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>

                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="printreport" class="btn btn-saveData mt-2"   onserverclick="printreport_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Print Report</Button>
                    </div>

                     <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="Consolidationnew" class="btn btn-saveData mt-2"   onserverclick="Consolidationnew_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Print Report New</Button>
                    </div>
                       <div class="col-md-3" style="padding-top: 16px;">
                        <Button runat="server" ID="Button2" class="btn btn-success mt-2"   onserverclick="printclassteacherreport_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Print Class Teacher Report </Button>
                     </div>
                </div>
            </div>
            </div>
       
    </section>

    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
              Subject Sheet Report
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-2">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmb_std" class="form-control select2" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbdiv" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmb_div" class="form-control select2" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="exam" class="form-label mb-1">Exam Name</label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbexam_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="exam" class="form-label mb-1">Subject Name</label>
                        <asp:DropDownList ID="cmbsubject" class="form-control select2" runat="server" AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="btnsubjshet" class="btn btn-saveData mt-2"   onserverclick="btnsubjshet_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Print Subject Sheet</Button>
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
                    
                    <img src="../img/alertimage.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblalertmessage" style="font-weight: normal;" />
                    <%--<label id="lblmessage" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade modal-lg" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" style="font-weight: bold;">Information Message.</h5>
                  
                    <img src="../img/information.png" style="height:100px;width:auto;" />
                </div>
                <div class="modal-body">
                    <asp:Label Text="" runat="server" ID="lblinfomsg" style="font-weight: normal;" />
                    <%--<label id="lblinfomsg" style="font-weight: normal;"></label>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>
    <script src="../Scripts/bootstrap-waitingfor.js"></script>
    <script src="../Scripts/bootstrap-waitingfor.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function (e) {
            $("#MarksheetReportSection").addClass("menu-open");
            $("#MarksheetReportItem").addClass("active");
            $("#menu_consolidatet").addClass("active");

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
