<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeFile="FeesParticulars.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.FeesParticulars" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .div-academicyear{
                position: absolute;
                top: 12px;
                right: 14px;
        }

        .ftrow{
            font-size: small;text-align: center;
        }

          .uppercase{
            text-transform:uppercase;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header">
                <a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>Fee Particulars

                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblAcademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-4">
                         
                        <label for="cmbAcademicYear" class="form-label mb-1">Academic Year</label>
                        <asp:DropDownList ID="cmbAcademicYear" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAcademicYear_SelectedIndexChanged">
                            <asp:ListItem Selected="True">Select Academic Year</asp:ListItem>
                           
                        </asp:DropDownList>
                   
                    </div>
                </div>
                <div class="row">
                   
                    <div class="col-md-3 mt-2">
                        <label for="cmbStd" class="form-label mb-1">Standard</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" onservervalidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-3 mt-2">
                        <label for="cmbHeaders" class="form-label mb-1">Fees Header</label>
                        <asp:DropDownList ID="cmbHeaders" class="form-control select2" runat="server">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="headerCustomvalid" runat="server" ErrorMessage="Select Fees Header" ControlToValidate="cmbHeaders" OnServerValidate="headerCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-3 mt-2">
                        <label for="txtAmount" class="form-label mb-1">Amount</label>
                        <asp:TextBox ID="txtAmount" runat="server" type="text" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="amountrequired" runat="server" ErrorMessage="Enter Header Amount" ControlToValidate="txtAmount" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3 d-flex align-items-end mt-0">
                        <Button runat="server" ID="SaveFeesParticulars" class="btn btn-save" onserverclick="SaveFeesParticulars_ServerClick" style="margin-bottom:22px;"><i class="fas fa-download mr-2"></i>Save Fee Particulars</Button>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView class="table table-bordered table-responsive mt-3" AutoGenerateColumns="false" ID="GridParticulars" runat="server" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText=" Sr. No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow" />
                                <asp:BoundField DataField="AcademicYear" HeaderText=" Academic Year " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="STD" HeaderText=" Standard " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="Computer" HeaderText=" Computer " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="Interactive" HeaderText=" Interactive " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="ELibrary" HeaderText=" E Library " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="OtherFees" HeaderText=" Other Fees " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="ReAdmissionFees" HeaderText=" Re Admission Fees " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="NewAdmissionFees" HeaderText=" New Admission Fees  "  HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                <asp:BoundField DataField="Administrative" HeaderText=" Administrative " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                                
                               <asp:BoundField DataField="Total" HeaderText=" Total " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow"/>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="modal fade" id="alertmessagemodal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel" style="font-weight: bold;">Alert Message!!</h5>
                    
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
                    <h5 class="modal-title" id="exampleModalLabel" style="font-weight: bold;">Information Message.</h5>
                  
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
            $("#FeesModuleSection").addClass("menu-open");
            $("#FeesModuleItem").addClass("active");
            $("#menu_feesParticular").addClass("active");

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
