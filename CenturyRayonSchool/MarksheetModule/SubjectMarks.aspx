<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeFile="SubjectMarks.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.SubjectMarks" %>
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
                Subject'S Marks
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
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true" >
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" onservervalidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbdiv" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" onservervalidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbexam" class="form-label mb-1">Exam Name </label>
                        <asp:DropDownList ID="cmbexam" class="form-control select2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="cmbexam_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="examCustomvalid" runat="server" ErrorMessage="Select Exam" ControlToValidate="cmbexam" onservervalidate="examCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                     <div class="col-md-2">
                        <label for="cmbStd" class="form-label mb-1">Select Subject</label>
                        <asp:DropDownList ID="cmbsubject" class="form-control select2" runat="server">
                            
                        </asp:DropDownList>
                    </div>
                     <div class="col-md-2">
                        <Button runat="server" ID="Generate_excel" class="btn btn-primary mt-4"   onserverclick="Generate_excel_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Generate Excel</Button>
                    </div>

                    <div class="col-md-2">
                        <asp:FileUpload ID="FileUpload1" Style="display: none" runat="server" onchange="upload()" />
                        <input type="button" value="Upload Excel" onclick="showBrowseDialog()" class="btn btn-primary mt-4" />
                        <button runat="server" id="btnUploadExcel" class="btn btn-primary mt-4" style="display: none;" onserverclick="btnUploadExcel_ServerClick" CausesValidation="false"><i class="fas fa-excel mr-2"></i>Upload Excel</button>
                    </div>
                  
                    
                </div>
                <div class="row">
                    
                    <div class="col-md-4">
                        <label for="cmbStd" class="form-label mb-1">Select StudentName</label>
                        <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server" CausesValidation="false" AutoPostBack="false">
                            
                        </asp:DropDownList>
                     </div>
                   
                     <div class="col-md-4">
                         </div>
                      <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="FetchData" class="btn btn-saveData mt-2"  onserverclick="FetchData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</Button>
                        
                    </div>
                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="SaveMarks" class="btn btn-saveData mt-2"   onserverclick="SaveMarks_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Save Data</Button>
                         
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
                               
                                <asp:TemplateField HeaderText="Daily Observation " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="DailyObser" Text='<%#Eval("DailyObser").ToString()%>'  OnTextChanged="DailyObser_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Orals " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Orals" Text='<%#Eval("Orals").ToString()%>' OnTextChanged="Orals_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Practical/Experiments" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="PracExp" Text='<%#Eval("PracExp").ToString()%>' OnTextChanged="PracExp_TextChanged"  AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Activity " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Activity" Text='<%#Eval("Activity").ToString()%>' OnTextChanged="Activity_TextChanged"  AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Project " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Project" Text='<%#Eval("Project").ToString()%>' OnTextChanged="Project_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Test (Written) " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Unittest" Text='<%#Eval("Unittest").ToString()%>' OnTextChanged="Unittest_TextChanged"  AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Self Study/Class Work " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Selfstudy" Text='<%#Eval("Selfstudy").ToString()%>' OnTextChanged="Selfstudy_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Others " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Others" Text='<%#Eval("Others").ToString()%>'  OnTextChanged="Others_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total @ 70 " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1"  ItemStyle-CssClass="col-id" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Total" Text='<%#Eval("Total").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align:center; background-color:#fef6db"></asp:TextBox>
                                            
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Summative Orals @ 10 " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="sumorals" Text='<%#Eval("sumorals").ToString()%>' OnTextChanged="sumorals_TextChanged" AutoPostBack="true"  Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Summative Written @ 20 " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="sumwritten" Text='<%#Eval("sumwritten").ToString()%>' OnTextChanged="sumwritten_TextChanged" AutoPostBack="true"  Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Summative Total @ 30 " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-CssClass="col-id"  >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="sumtotal" Text='<%#Eval("sumtotal").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align:center;background-color:#fef6db"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Grand Total @ 100 " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-CssClass="col-id" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="GrandTotal" Text='<%#Eval("GrandTotal").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align:center;background-color:#fef6db"></asp:TextBox>
                                        </div>                            
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Grade" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" ItemStyle-CssClass="col-id" >
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox runat="server" class="form-control" ID="Grade" Text='<%#Eval("Grade").ToString()%>'  AutoPostBack="true" Width="100px" Style="text-align:center;background-color:#fef6db"></asp:TextBox>
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
            $("#menu_SubjectMarks").addClass("active");

            $(".select2").select2();

            
           

        });
         function showBrowseDialog() {
             var fileuploadctrl = document.getElementById('<%= FileUpload1.ClientID %>');
            fileuploadctrl.click();
        }

        function upload() {
            var btn = document.getElementById('<%= btnUploadExcel.ClientID %>');
            btn.click();
        }
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
