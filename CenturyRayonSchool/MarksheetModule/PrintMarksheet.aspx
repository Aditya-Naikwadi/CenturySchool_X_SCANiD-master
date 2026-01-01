<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="PrintMarksheet.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.PrintMarksheet" Async="true" %>

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
                <a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Print Marksheet
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">

                <div class="row">
                    <div class="col-lg-3 col-md-6">
                     <label for="lbl" class="form-label mb-1">Academic Year</label>
                        <asp:DropDownList ID="cmbAcademicyear" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAcademicyear_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
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
                    <div class="col-md-2">
                        <label for="cmbexam" class="form-label mb-1">Exam Name </label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:CustomValidator ID="examCustomvalid" runat="server" ErrorMessage="Select Exam" ControlToValidate="cmbexam" OnServerValidate="examCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>

                    <div class="col-md-4">
                        <label for="cmbStduname" class="form-label mb-1">Select StudentName</label>
                        <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server" CausesValidation="false" AutoPostBack="false">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-2" style="padding-top: 16px;">
                        <button runat="server" id="FetchData" class="btn btn-saveData mt-2" onserverclick="FetchData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</button>
                    </div>

                    <div class="col-md-2" style="padding-top: 16px;">
                        <button runat="server" id="Printall" class="btn btn-saveData mt-2" onserverclick="Printall_ServerClick" onclientclick="showwaitingdialogue()"><i class="fas fa-angle-double-down mr-2"></i>Print All</button>
                    </div>
                    
                    <div class="col-md-1" style="padding-top: 32px;">
                        <div>
                            <asp:CheckBox Text="Print New" runat="server" ID="Chk9th" AutoPostBack="true" />
                        </div>
                    </div>
                    <div class="col-md-1" style="padding-top: 32px;">
                        <div>
                            <asp:CheckBox Text="" runat="server" ID="checkALL" OnCheckedChanged="checkALL_CheckedChanged" AutoPostBack="true" />
                            Tick All
                        </div>
                    </div>
                    <div class="col-md-3">
                        <label for="reopen" class="form-label mb-1">School Reopen Date :</label>
                        <asp:TextBox ID="txtreopen" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtreopen_TextChanged"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        <label for="result" class="form-label mb-1">School Result Date :</label>
                        <asp:TextBox ID="txtresult" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtresult_TextChanged"></asp:TextBox>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-lg table-striped table-responsive mt-3 ftrow" OnRowDataBound="GridCollection_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="Rollno" HeaderText=" Roll No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="10%"></asp:BoundField>
                                <asp:BoundField DataField="grno" HeaderText=" GRNO " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="20%" ItemStyle-Width="20%"></asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText=" Student Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" HeaderStyle-Width="30%" ItemStyle-Width="30%"></asp:BoundField>
                                <asp:BoundField DataField="std" HeaderText=" STD " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="div" HeaderText=" DIV " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="examname" HeaderText=" Exam " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false"></asp:BoundField>
                                <asp:BoundField DataField="academicyear" HeaderText=" Year " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false"></asp:BoundField>
                                <asp:TemplateField HeaderText=" Print" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<Button runat="server" ID="btnEdit" class="btn btn-edit mt-2" CommandName="editfeesheader"><i class="fas fa-edit mr-2"></i>Edit</Button>--%>

                                        <%--<asp:Button runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deletefeesheader" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" Text="Delete"/>--%>

                                        <asp:HyperLink NavigateUrl='<%#getDownloadUrl(Eval("grno").ToString(),Eval("div").ToString(),Eval("std").ToString(),Eval("examname").ToString(),Eval("academicyear").ToString())%>' runat="server" Text="Print" CssClass="btn btn-success" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Print New" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:HyperLink NavigateUrl='<%#getDownloadUrlNew(Eval("grno").ToString(),Eval("div").ToString(),Eval("std").ToString(),Eval("examname").ToString(),Eval("academicyear").ToString())%>' runat="server" Text="Print New" CssClass="btn btn-success" Target="_blank" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
            $("#menu_print").addClass("active");
            
            $(".select2").select2();
          
        });

        $('#<%=Printall.ClientID%>').click(function () {
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

            waitingDialog.show('Generating Marksheet PDF');

        }

    </script>

</asp:Content>
