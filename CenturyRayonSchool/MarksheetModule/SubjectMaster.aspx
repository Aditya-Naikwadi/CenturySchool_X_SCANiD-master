<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="SubjectMaster.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.SubjectMaster" %>

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

        .fixed-header {
            z-index: 50 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header">
                <a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>Subject Master
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblAcademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbStd" class="form-label mb-1">STD :</label>
                        <asp:DropDownList ID="cmbstd" class="form-control select2" runat="server">
                        </asp:DropDownList>
                        <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbstd"  ForeColor="Red"></asp:CustomValidator>--%>
                    </div>

                    <div class="col-md-3">
                        <label for="cmbexamname" class="form-label mb-1">Exam Name :</label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server">
                        </asp:DropDownList>
                        <%--<asp:CustomValidator ID="examCustomvalid" runat="server" ErrorMessage="Select Exam" ControlToValidate="cmbexam" ForeColor="Red"></asp:CustomValidator>--%>
                    </div>

                    <div class="col-md-3 d-flex align-items-end">
                        <button runat="server" id="ShowSubj" class="btn btn-save mt-2"   onserverclick="ShowSubj_ServerClick"><i class="fas fa-save mr-2"></i>Show Subjects</button>
                    </div>
                    <div class="col-md-3">
                        <button runat="server" id="Savesubj" class="btn btn-save mt-2" onserverclick="Savesubj_ServerClick"><i class="fas fa-save mr-2"></i>Save Subjects</button>
                    </div>
                    <div class="col-md-12">
                        <div style="width: 100%;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="SubjectCollection" EventName="PageIndexChanging" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:GridView CssClass="table table-sm table-striped table-responsive mt-3 ftrow" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" ID="SubjectCollection" runat="server" Width="100%">
                                        <Columns>
                                             <asp:TemplateField HeaderText="Sr. No." HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <div>
                                                      <asp:TextBox runat="server" class="form-control" ID="txtsrno" Text='<%#Eval("Srno").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align: center; font-size:20px"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-BackColor="#1CB5E0" ItemStyle-Width="10%" HeaderStyle-Width="30%"   ItemStyle-Font-Size="Large"/>
                                            <asp:TemplateField HeaderText="MIN Marks" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div>
                                                      <asp:TextBox runat="server" class="form-control" ID="txtminmarks" Text='<%#Eval("minmarks").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align: center; font-size:20px"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MAX Marks" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div>
                                                      <asp:TextBox runat="server" class="form-control" ID="txtmaxmarks"  Text='<%#Eval("maxmarks").ToString()%>' AutoPostBack="true" Width="100px" Style="text-align: center; font-size:20px"></asp:TextBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Check " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" class="form-check" ID="chkSelect" Checked='<%#setChecked(Eval("check").ToString())%>' CssClass="ftrow1"  />
                                                </ItemTemplate>

                                                <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" Grade " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-Width="20%" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" class="form-check" ID="gradeSelect" Checked='<%#setgradeCheck(Eval("grade").ToString())%>'  CssClass="ftrow1"  AutoPostBack="true" />
                                                </ItemTemplate>

                                                <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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

    <script type="text/javascript">
        $(document).ready(function (e) {
            $("#MarksheetModuleSection").addClass("menu-open");
            $("#MarksheetModuleItem").addClass("active");
            $("#menu_SubjectMaster").addClass("active");

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

        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to Delete Exam. Please Contact Administrator For Changes in Exam.")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
