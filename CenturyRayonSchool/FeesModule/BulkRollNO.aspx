<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeBehind="BulkRollNO.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.BulkRollNO" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
               Roll No updation
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                
                <div class="row">
                     <div class="col-md-3">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" onservervalidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-3">
                        <label for="cmbdiv" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" onservervalidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                     <div class="col-md-4">
                        <label for="cmbStd" class="form-label mb-1">Select StudentName</label>
                        <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server" CausesValidation="false" AutoPostBack="false">
                            
                        </asp:DropDownList>
                     </div>
                     <div class="col-md-2">
                        <Button runat="server" ID="getdata" class="btn btn-primary mt-4" onserverclick="getdata_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</Button>
                    </div>
                    </div>

                 <div class="row">
                     <div class="col-md-12">
                        
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <Triggers>
                                     
                                    <asp:AsyncPostBackTrigger ControlID="GridCollection" EventName="PageIndexChanging" />
                                     
                                 </Triggers>
                                <ContentTemplate>
                                    <asp:GridView  AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-lg table-striped table-responsive mt-3 ftrow" >
                            <Columns>
                                <asp:BoundField DataField="GRNO" HeaderText=" GRNO " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                 
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText=" Student Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="std" HeaderText=" STD " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="div" HeaderText=" DIV " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="RollNo" HeaderText="OLD Roll No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                               
                                </asp:BoundField>
                                                               
                                <asp:TemplateField HeaderText="NEW Roll NO" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1"  HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="newrollno" Text='<%#Eval("newrollno").ToString()%>' OnTextChanged="newrollno_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Status" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="rolstats" Text='<%#Eval("rolstats").ToString()%>' AutoPostBack="true" Width="200px" Style="text-align:center;" ReadOnly="true"></asp:TextBox>
                                         </div>                            
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
            $("#FeesModuleSection").addClass("menu-open");
            $("#FeesModuleItem").addClass("active");
            $("#menu_roll").addClass("active");

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
