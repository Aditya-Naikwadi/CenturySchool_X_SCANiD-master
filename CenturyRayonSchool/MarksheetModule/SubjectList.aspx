<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="SubjectList.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.SubjectList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
      
        .uppercase{
            text-transform:uppercase;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header">
                <a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>Subjects
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblAcademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row">
                    <div class="col-md-4">
                        <asp:Label Text="0" runat="server" ID="lblfeecode" Style="display:none;"/>
                        <label for="txtsubject" class="form-label mb-1">Subject Name</label>
                        <asp:TextBox ID="txtsubj" runat="server" type="text" class="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4 d-flex align-items-end">
                        <Button runat="server" ID="SaveSubject" class="btn btn-save mt-2" OnServerClick="SaveSubject_ServerClick"><i class="fas fa-save mr-2"></i>Save Subject</Button>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView CssClass="table table-sm table-bordered mt-3" AutoGenerateColumns="false" ID="gridHeaders"  OnRowCommand="gridHeaders_RowCommand" runat="server" Width="100%" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="Subject" HeaderText=" Subject Name " ItemStyle-Width="10%" HeaderStyle-Width="30%" />
                                 <asp:TemplateField HeaderText=" Action"  ItemStyle-Width="30%" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<Button runat="server" ID="btnEdit" class="btn btn-edit mt-2" CommandName="editfeesheader"><i class="fas fa-edit mr-2"></i>Edit</Button>--%>
                                       
                                        <%--<asp:Button runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deletefeesheader" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" Text="Delete"/>--%>
                                        
                                        <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deleteSubject" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()"><i class="fas fa-trash mr-2"></i>Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal -->
<div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>


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
            $("#MarksheetModuleSection").addClass("menu-open");
            $("#MarksheetModuleItem").addClass("active");
            $("#menu_subjectlist").addClass("active");

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
