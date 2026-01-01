<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="TeacherMapping.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.TeacherMapping" %>
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
            width:250px;
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

        .text-center{
            text-align: center;
            font-size: 20px;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
                        <label for="cmbdiv" class="form-label mb-1">Teachername</label>
                        <asp:DropDownList ID="cmbtrname" class="form-control select2" runat="server" AutoPostBack="true" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="trCustomvalid" runat="server" ErrorMessage="Select Teacher Name" ControlToValidate="cmbtrname" onservervalidate="trCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>


                  
                    <div class="col-md-2" style="padding-top: 16px;">
                       <Button runat="server" ID="Savedetais" class="btn btn-saveData mt-2"   onserverclick="Savedetais_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Save Data</Button>

                    </div>
                    
                </div>
         
                 <div class="row">
                    
                    <div class="col-md-12">
                        <div style="width:100%;">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                                 <%--<Triggers>
                                     
                                    <asp:AsyncPostBackTrigger ControlID="GridCollection" EventName="PageIndexChanging" />
                                     
                                 </Triggers>--%>
                                <ContentTemplate>

                                    <asp:GridView  AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-lg table-striped table-responsive mt-3 ftrow" OnRowCommand="GridCollection_RowCommand">
                            <Columns>
                                
                                <asp:BoundField DataField="STD" HeaderText=" STD " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " >
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="DIV" HeaderText=" DIV " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " >
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="TeacherName" HeaderText=" Class Teacher " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-center " >
                                
                                </asp:BoundField>
                                
                                <asp:TemplateField HeaderText=" Action"  ItemStyle-Width="30%" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        
<%--                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="deleteteacher" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()" />--%>
                                <%--<asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deleteteacher" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()"><i class="fas fa-trash mr-2"></i>Delete</asp:LinkButton>--%>

                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-delete mt-2" CommandName="deleteteacher" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()" />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                            </ContentTemplate>
                        <%--</asp:UpdatePanel>--%>
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



     <script type="text/javascript">
        $(document).ready(function (e) {
            $("#MarksheetModuleSection").addClass("menu-open");
            $("#MarksheetModuleItem").addClass("active");
            $("#menu_teachermap").addClass("active");

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
             if (confirm("Do you really want to Delete this Record?")) {
                 confirm_value.value = "Yes";
             } else {
                 confirm_value.value = "No";
             }
             document.forms[0].appendChild(confirm_value);
         }
     </script>
</asp:Content>
