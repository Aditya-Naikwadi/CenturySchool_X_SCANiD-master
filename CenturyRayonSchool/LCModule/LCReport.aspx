<%@ Page Title="" Language="C#" MasterPageFile="~/LCModule/LeavingMaster.Master" AutoEventWireup="true" CodeBehind="LCReport.aspx.cs" Inherits="CenturyRayonSchool.LCModule.LCReport" %>
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
            <div class="card-header card-mobile"><a href="LCDashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
               Leaving Certificate Report
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                     <div class="row">
                    <div class="col-md-3"> 
                       <label class="form-label select-label" for="start">From Date</label>
                       <input type="date" class="form-control" id="txt_Fromdate" name="trip-start" value="" onchange="setDateOnLabel()"/>
                       
                        <asp:TextBox ID="asptxtfromdate" CssClass="c-visible" runat="server" />
                    </div>

                    <div class="col-md-3"> 
                       <label class="form-label select-label" for="start">To Date</label>
                       <input type="date" class="form-control" id="txt_Todate" name="trip-start" value="" onchange="setDateOnLabel()"/>
                         
                        <asp:TextBox ID="asptxttodate" CssClass="c-visible" runat="server" />
                    </div>
                         </div>
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
                         

                       <%-- <asp:HyperLink NavigateUrl='<%#getDownloadUrl()%>' runat="server" Text="Print" CssClass="btn btn-success" Target="_blank" />--%>
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
            $("#lcReportSection").addClass("menu-open");
            $("#lcReportItem").addClass("active");
            $("#menu_lcrep").addClass("active");

            $(".select2").select2();

            var fromdate = $("#ContentPlaceHolder1_asptxtfromdate").val();
            var todate = $("#ContentPlaceHolder1_asptxttodate").val();

            $("#txt_Fromdate").val(fromdate.replace('/', '-').replace('/', '-'));
            $("#txt_Todate").val(todate.replace('/', '-').replace('/', '-'));
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
            var txtfromdate = $("#txt_Fromdate").val();
            var txttodate = $("#txt_Todate").val();

            $("#ContentPlaceHolder1_asptxtfromdate").val(txtfromdate.replace('-', '/').replace('-', '/'));
            $("#ContentPlaceHolder1_asptxttodate").val(txttodate.replace('-', '/').replace('-', '/'));
        }

        
    </script>
</asp:Content>
