<%@ Page Title="" Language="C#" MasterPageFile="~/BonafideModule/BonafideMaster.Master" AutoEventWireup="true" CodeBehind="BulkBonafide.aspx.cs" Inherits="CenturyRayonSchool.BonafideModule.BulkBonafide" %>
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
                <a href="BonafideDashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Bulk  Bonafide Certificate Genrate
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-2">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" OnServerValidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>

                    </div>
                    <div class="col-md-2">
                        <label for="cmbdiv" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" OnServerValidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2" style="margin-top: 16px;">
                        <button runat="server" id="GetData" class="btn btn-saveData mt-2" onserverclick="GetData_ServerClick" style="width:80%;"><i class="fas fa-angle-double-down mr-2"></i>Get Data</button>
                    </div>

                    <div class="col-md-2" style="margin-top: 28px;">
                        <asp:CheckBox Text="" runat="server" ID="checkALL" OnCheckedChanged="checkALL_CheckedChanged" AutoPostBack="true" />
                        Tick All
                    </div>
                    <div class="col-md-3" style="margin-top: 16px;">
                        <button runat="server" id="btnbulkbona" class="btn btn-saveData mt-2" onserverclick="btnbulkbona_ServerClick" onclientclick="showwaitingdialogue();" style="width:60%;"><i class="fas fa-angle-double-down mr-2"></i>Generate Bonafide Certificates</button>
                    </div>

                </div>
                 <div class="row">
                    
                    <%--<div class="col-md-3">
                        <label class="form-label select-label" for="start">Date Of Leaving :</label>
                        <input type="date" class="form-control" id="txt_doa" name="trip-start" value="" />

                        <asp:TextBox ID="dateofleaving" CssClass="c-visible" runat="server" ReadOnly="true" />
                    </div>--%>
                    
                    
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-lg table-striped table-responsive mt-3 ftrow">
                            <Columns>
                                <asp:BoundField DataField="Rollno" HeaderText=" Roll No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText=" Student Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" HeaderStyle-Width="30%" ItemStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="Grno" HeaderText=" GRNO " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="20%" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="Section" HeaderText=" Section" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="20%" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:TemplateField HeaderText=" Tick " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" class="form-check" ID="chkSelect" CssClass="ftrow1" AutoPostBack="true" />
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

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

    <div class="modal fade" id="progressModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content" style="background-color: transparent; box-shadow: none; border: 0px; margin-top: 50%;">

                <div class="modal-body">
                    <img src="~/Images/output-onlinegiftools.gif" alt="Alternate Text" style="width: 100px; height: auto; margin: auto; display: block;" />
                    <div style="margin: auto; text-align: center;">
                        <label style="color: wheat; font-size: x-large; font-weight: 100;">Loading in</label>&nbsp;<label id="idcounter" style="color: wheat; font-size: x-large; font-weight: 100;">0</label>&nbsp;<label style="color: wheat; font-size: x-large; font-weight: 100;"> Sec(s)</label>
                    </div>

                </div>

            </div>
        </div>
    </div>

    <script src="../Scripts/bootstrap-waitingfor.js"></script>
    <script src="../Scripts/bootstrap-waitingfor.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function (e) {
            $("#BonafideModuleSection").addClass("menu-open");
            $("#BonafideModuleItem").addClass("active");
            $("#menu_Bonagenrate").addClass("active");

            $(".select2").select2();

            var doa = $("#ContentPlaceHolder1_dateofleaving").val();


          $("#txt_doa").val(doa.replace('/', '-').replace('/', '-'));

        });
        $('#<%=btnbulkbona.ClientID%>').click(function () {
            //window.document.forms[0].target = '_blank';
            showwaitingdialogue();
        });
        function showInfoModal() {
            var myModal = new bootstrap.Modal(document.getElementById('infomessagemodal'))
            myModal.show()
        }

        function showAlertModal() {
            var myModal = new bootstrap.Modal(document.getElementById('alertmessagemodal'))
            myModal.show()
        }

        function showwaitingdialogue() {

            waitingDialog.show('Generating Bonafide');

        }

        function setDateOnLabel() {
            var txtdoa = $("#txt_doa").val();


            $("#ContentPlaceHolder1_dateofleaving").val(txtdoa.replace('-', '/').replace('-', '/'));

        }
    </script>
</asp:Content>
