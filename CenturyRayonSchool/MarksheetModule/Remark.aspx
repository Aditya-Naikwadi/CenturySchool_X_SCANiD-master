<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="Remark.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.Remark" %>
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
                Remark's
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
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" onservervalidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbexam" class="form-label mb-1">Exam Name </label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server" AutoPostBack="true">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="examCustomvalid" runat="server" ErrorMessage="Select Exam" ControlToValidate="cmbexam" onservervalidate="examCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                     
                     <div class="col-md-4">
                        <label for="cmbStd" class="form-label mb-1">Select StudentName</label>
                        <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server" CausesValidation="false" AutoPostBack="false">
                            
                        </asp:DropDownList>
                     </div>
                  

                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="FetchData" class="btn btn-saveData mt-2"  onserverclick="FetchData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</Button>
                        
                    </div>
                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="SaveREMARK" class="btn btn-saveData mt-2"   onserverclick="SaveRemark_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Save Data</Button>
                         
                    </div>
                    
                </div>
                
                 <div class="row">
                    
                    <div class="col-md-12">
                        <div style="width:100%;">

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                 <Triggers>
                                     
                                    <asp:AsyncPostBackTrigger ControlID="GridCollection" EventName="PageIndexChanging" />
                                     
                                 </Triggers>
                                <ContentTemplate>

                                    <asp:GridView  AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-sm table-striped table-responsive mt-3 ftrow" >
                            <Columns>
                                <asp:BoundField DataField="RollNo" HeaderText=" Roll No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no">
                               
                                </asp:BoundField>
                                <asp:BoundField DataField="GRNO" HeaderText=" GRNO " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no">
                                 
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText=" Student Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="std" HeaderText=" STD " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false">
                                
                                </asp:BoundField>
                                <asp:BoundField DataField="div" HeaderText=" DIV " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no" Visible="false">
                                
                                </asp:BoundField>
                               
                                <asp:TemplateField HeaderText="Special Progress " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="special" Text='<%#Eval("special").ToString()%>'   AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Liking Hobby " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="liking" Text='<%#Eval("liking").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Needs And Improvements" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="needs" Text='<%#Eval("needs").ToString()%>'   AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
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
            $("#MarksheetMarkSection").addClass("menu-open");
            $("#MarksheetMarksItem").addClass("active");
            $("#menu_remark").addClass("active");

            $(".select2").select2();


          

        });
        
         //function setDateOnLabel(t) {
         //    var txtrcptdate = t.closest("tr").find(".txt_receiptdate").val();

            

         //    var lblreceiptdate = t.closest("tr").find(".lblreceiptdate");

         //    lblreceiptdate.val(txtrcptdate);
            
             
         //}


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
