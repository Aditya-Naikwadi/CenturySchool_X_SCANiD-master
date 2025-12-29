<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeBehind="Consolidate9th.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.Consolidate9th" %>
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
                Teacher Mapping
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
                    <div class="col-md-2">
                        <label for="cmbexam" class="form-label mb-1">Exam Name </label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server" AutoPostBack="true">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="examCustomvalid" runat="server" ErrorMessage="Select Exam" ControlToValidate="cmbexam" onservervalidate="examCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                  
                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="printreport" class="btn btn-saveData mt-2"   onserverclick="printreport_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Print Report</Button>
                         
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

    <script type="text/javascript">
        $(document).ready(function (e) {
            $("#MarksheetReportSection").addClass("menu-open");
            $("#MarksheetReportItem").addClass("active");
            $("#menu_consolidate9").addClass("active");

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
