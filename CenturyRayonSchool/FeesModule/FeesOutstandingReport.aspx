<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeFile="FeesOutstandingReport.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.FeesOutstandingReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .div-academicyear{
                position: absolute;
                top: 12px;
                right: 14px;
        }

        .ftrow{
            font-size:small;text-align: center;
        }

        .ftrow1{
               padding: 4px;
                color: black;
                font-weight: bolder;
                height: 50px;
                text-align: center;
        }

        .c-visible{
            display:none;
        }

          .uppercase{
            text-transform:uppercase;
        }

          .btn-dark{
              border-radius: 50px !important;
          }

         .text-left{
             text-align:left;
         }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Fees Outstanding Report 
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
                                <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbStd_SelectedIndexChanged">
                            
                                </asp:DropDownList>
                        
                            </div>
                            <div class="col-md-2">
                                <label for="cmbStd" class="form-label mb-1">Div</label>
                                <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged">
                            
                                </asp:DropDownList>
                        
                            </div>
                                 <div class="col-md-4">
                                    <label for="cmbStd" class="form-label mb-1">Select StudentName</label>
                                    <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server">
                            
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2" style="padding-top: 16px;">
                                    <Button runat="server" ID="FetchData" class="btn btn-saveData mt-2" onserverclick="FetchData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</Button>
                        
                                </div>
                                <div class="col-md-2" style="padding-top: 16px;">
                                    <Button runat="server" ID="btnprintreport" class="btn btn-primary mt-2" onserverclick="btnprint_ServerClick"><i class="fas fa-print mr-2"></i> Print Report</Button>
                        
                                </div>
                              <div class="col-md-2">
                        <Button runat="server" ID="BtnExcel" class="btn btn-dark mt-2"  causesvalidation="false"><i class="fas fa-excel mr-2"></i>Genrate Excel</Button>
                    </div>
                
                    <div class="col-md-3" style="padding-top:10px;">
                        <asp:Label Text="Total Students :" runat="server" style="font-weight:bold;color:blue"/> <asp:Label ID="lbltotalstudents" runat="server" style="font-weight:bold;color:green">0</asp:Label>
                    </div>
                     
                   
                </div>
               <div class="row">
                    
                    <div class="col-md-12">
                        <div style="width:100%;">

                        <asp:GridView  AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-sm table-striped  mt-3 ftrow" OnRowCommand="GridCollection_RowCommand" >
                            <Columns>
                                
                                <asp:BoundField DataField="rollno" HeaderText="Roll No" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="std" HeaderText="Std" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                 
                                </asp:BoundField>
                                  <asp:BoundField DataField="div" HeaderText="Div" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                 
                                </asp:BoundField>
                                <asp:BoundField DataField="grno" HeaderText="GrNo" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-CssClass="text-left">
                                
                                </asp:BoundField>
                                 <asp:BoundField DataField="studentname" HeaderText="StudentName" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                  
                                </asp:BoundField>
                               
                                <asp:BoundField DataField="bookedfees" HeaderText="Total Fees" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="paidfees" HeaderText="Paid Fees" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    
                                </asp:BoundField>
                                 <asp:BoundField DataField="outstandingfees" HeaderText="Balance Fees" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    
                                </asp:BoundField>
                                
                            </Columns>
                        </asp:GridView>
                         
                        </div>
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

    <div class="modal fade" id="infomessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
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
    <script src="https://cdnjs.cloudflare.com/ajax/libs/xlsx/0.18.5/xlsx.full.min.js"></script>



     <script type="text/javascript">
        $(document).ready(function (e) {
            $("#FeesReportSection").addClass("menu-open");
            $("#FeesReportItem").addClass("active");
            $("#menu_feeoutstandingreport").addClass("active");

            $(".select2").select2();

            var fromdate = $("#ContentPlaceHolder1_asptxtfromdate").val();
            var todate = $("#ContentPlaceHolder1_asptxttodate").val();

            $("#txt_Fromdate").val(fromdate.replace('/', '-').replace('/', '-'));
            $("#txt_Todate").val(todate.replace('/', '-').replace('/', '-'));
        });

         document.getElementById("<%= BtnExcel.ClientID %>").addEventListener("click", function () {
                var table = document.getElementById("<%= GridCollection.ClientID %>"); // Get GridView by its actual client-side ID

                if (!table) {
                    alert("No data to export!");
                    return;
                }

                // Convert GridView to a Workbook using SheetJS
                var workbook = XLSX.utils.table_to_book(table, { sheet: "CL" });

                // Generate a timestamp for the filename
                var now = new Date();
                var timestamp = now.getFullYear() + "-" +
                    String(now.getMonth() + 1).padStart(2, '0') + "-" +
                    String(now.getDate()).padStart(2, '0') + "_" +
                    String(now.getHours()).padStart(2, '0') + "-" +
                    String(now.getMinutes()).padStart(2, '0') + "-" +
                    String(now.getSeconds()).padStart(2, '0');

                // Define filename
                var filename = `FeeOutstanding_${timestamp}.xlsx`;

                // Export to Excel
                XLSX.writeFile(workbook, filename);
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


