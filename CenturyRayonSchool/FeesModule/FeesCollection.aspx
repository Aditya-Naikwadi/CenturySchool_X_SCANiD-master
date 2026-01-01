<%@ Page Title="" Language="C#" MasterPageFile="~/FeesModule/Master.Master" AutoEventWireup="true" CodeFile="FeesCollection.aspx.cs" Inherits="CenturyRayonSchool.FeesModule.FeesCollection" %>
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
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
                Fee Collections 
                <div class="div-academicyear">
                    <asp:Label Text="Academic Year: " runat="server" />
                    <asp:Label Text="" runat="server" ID="lblacademicyear" />
                </div>
            </div>
            <div class="card-body margin-rows">
                <div class="row <%=isFeesAdmin %>">
                    <div class="col-lg-3 col-md-6">
                     <label for="cmbStd" class="form-label mb-1">Academic Year</label>
                        <asp:DropDownList ID="cmbAcademicyear" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbAcademicyear_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <label for="cmbStd" class="form-label mb-1">Std</label>
                        <asp:DropDownList ID="cmbStd" class="form-control select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbStd_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="stdCustomvalid" runat="server" ErrorMessage="Select Std" ControlToValidate="cmbStd" onservervalidate="stdCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                    <div class="col-md-2">
                        <label for="cmbStd" class="form-label mb-1">Div</label>
                        <asp:DropDownList ID="cmbDiv" class="form-control select2" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="cmbDiv_SelectedIndexChanged">
                            
                        </asp:DropDownList>
                        <asp:CustomValidator ID="divCustomvalid" runat="server" ErrorMessage="Select Div" ControlToValidate="cmbDiv" onservervalidate="divCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                     <div class="col-md-4">
                        <label for="cmbStd" class="form-label mb-1">Select StudentName</label>
                        <asp:DropDownList ID="cmbstudentname" class="form-control select2" runat="server">
                            
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="FetchData" class="btn btn-saveData mt-2" onserverclick="FetchData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Get Data</Button>
                        
                    </div>
                    <div class="col-md-1" style="padding-top: 32px;">
                        <div>
                             <asp:CheckBox Text="" runat="server" ID="checkALL" OnCheckedChanged="checkALL_CheckedChanged" AutoPostBack="true" /> Tick All
                        </div>
                        
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2" style="padding-top:10px;">
                        <asp:Label Text="Total Students :" runat="server" style="font-weight:bold;color:blue"/> <asp:Label ID="lbltotalstudents" runat="server" style="font-weight:bold;color:green">0</asp:Label>
                    </div>
                     <div class="col-md-2" style="padding-top:10px;">
                        <asp:Label Text="Total Paid Students :" runat="server" style="font-weight:bold;color:blue"/> <asp:Label ID="lblpaidstud" runat="server" style="font-weight:bold;color:green">0</asp:Label>
                    </div>
                    <div class="col-md-2" style="padding-top:10px;">
                        <asp:Label Text="Total UnPaid Students :" runat="server" style="font-weight:bold;color:blue"/> <asp:Label ID="lblunpaidstud" runat="server" style="font-weight:bold;color:green">0</asp:Label>
                    </div>
                    <div class="col-md-3">
                        <Button runat="server" ID="btnrefresh" class="btn btn-dark mt-2" onserverclick="btnrefresh_ServerClick" causesvalidation="false"><i class="fas fa-recycle mr-2"></i>Refresh</Button>
                    </div>
                      <div class="col-md-3">
                        <Button runat="server" ID="BtnExcel" class="btn btn-dark mt-2"  causesvalidation="false"><i class="fas fa-excel mr-2"></i>Genrate Excel</Button>
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

                           <asp:GridView  AutoGenerateColumns="False" ID="GridCollection" runat="server" ShowHeaderWhenEmpty="True" CssClass="table table-sm table-striped table-responsive mt-3 ftrow" OnRowCommand="GridCollection_RowCommand" >
                            <Columns>
                                <asp:BoundField DataField="RollNo" HeaderText=" Roll No " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no">
                               
                                </asp:BoundField>
                                <asp:BoundField DataField="GRNO" HeaderText=" GRNO " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="col-id-no">
                                 
                                </asp:BoundField>
                                <asp:BoundField DataField="StudentName" HeaderText=" Student Name " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 fixed-header col-id-no" ItemStyle-CssClass="text-left col-id-no">
                                
                                </asp:BoundField>
                                 <asp:BoundField DataField="STD" HeaderText="STD" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 c-visible" ItemStyle-CssClass="c-visible">
                                    
                                   
                                </asp:BoundField>
                                <asp:BoundField DataField="DIV" HeaderText=" DIV " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 c-visible" ItemStyle-CssClass="c-visible">
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="Academicyear" HeaderText="Academicyear" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 c-visible" ItemStyle-CssClass="c-visible" >
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="admissiontype" HeaderText="Admission Type" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1 c-visible" ItemStyle-CssClass="c-visible">
                                    
                                </asp:BoundField>
                                <asp:BoundField DataField="Computer" HeaderText=" Computer " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Interactive" HeaderText=" Interactive " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                 <asp:BoundField DataField="ELibrary" HeaderText=" E-Library " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                     <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="OtherFees" HeaderText=" Other Fees " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ReAdmissionFees" HeaderText=" Re Admission " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="NewAdmissionFees" HeaderText=" New Admission " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Administrative" HeaderText=" Administrative " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:BoundField>
                               
                                <asp:TemplateField HeaderText=" Freeship Amount " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1" >
                                    <ItemTemplate>
                                        <div>
                                            <%--<asp:TextBox runat="server" class="form-control" ID="txtFreeship" Text='<%#Eval("freeshipamount").ToString()%>' Enabled='<%#setEnabled(Eval("feestatus").ToString())%>'   OnTextChanged="txtFreeship_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>--%>

                                            <asp:TextBox runat="server" class="form-control" ID="txtFreeship" Text='<%#Eval("freeshipamount").ToString()%>' Enabled="false"   OnTextChanged="txtFreeship_TextChanged" AutoPostBack="true" Width="100px" Style="text-align:center"></asp:TextBox>
                                        </div>
                                        <div>
                                            <asp:RadioButton Text="Enable Freeship" runat="server" GroupName="freeship" ID="radioFreeship"  OnCheckedChanged="radioFreeship_CheckedChanged"  Enabled='<%#setEnabled(Eval("feestatus").ToString())%>' Checked='<%#setRadioButtonChecked(Eval("freeshiptype").ToString(),"freeship")%>' AutoPostBack="true"/>
                                        </div>
                                        <div>
                                             <asp:RadioButton Text="25% Freeship" runat="server" GroupName="freeship" ID="radio25freeship" OnCheckedChanged="radio25freeship_CheckedChanged"  Enabled='<%#setEnabled(Eval("feestatus").ToString())%>'  Checked='<%#setRadioButtonChecked(Eval("freeshiptype").ToString(),"25%")%>' AutoPostBack="true"/>
                                        </div>
                                        
                                    
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Amount Paid" HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                        <ItemTemplate>
                                           
                                            <asp:Label ID="lblAmountpaid" Text='<%#Eval("AmountPaid").ToString()%>' runat="server" Font-Bold="true"/>
                                        </ItemTemplate>

                                    
                                    </asp:TemplateField>

                                 <asp:TemplateField HeaderText=" Total Amount " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <ItemTemplate>
                                           
                                            
                                        <asp:Label ID="lbltotalfees" Text='<%#Eval("Total").ToString()%>' runat="server" Font-Bold="true"/>
                                    </ItemTemplate>

                                    
                                </asp:TemplateField>

                               

                                 <asp:TemplateField HeaderText=" FeeStatus " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <ItemTemplate>
                                          
                                            
                                        <asp:Label ID="lblfeesstatus" Text='<%#Eval("feestatus").ToString()%>' runat="server" ForeColor='<%#setForeColor(Eval("feestatus").ToString())%>' Font-Bold="true"/>
                                    </ItemTemplate>

                                    
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText=" Tick " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" class="form-check" ID="chkSelect" CssClass="ftrow1" Checked='<%#setChecked(Eval("feestatus").ToString())%>' Enabled='<%#setEnabled(Eval("feestatus").ToString())%>' OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true"/>
                                    </ItemTemplate>

                                    <HeaderStyle BackColor="#1CB5E0" CssClass="ftrow1"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Receipt Date " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <ItemTemplate>
                                        
                                        <%--<input type="date" class="form-control txt_receiptdate" name="trip-start" value="<%#Eval("ReceiptDate").ToString()%>" onchange="setDateOnLabel($(this))"  <%#setDisabled(Eval("feestatus").ToString())%> />--%>


                                        <asp:TextBox type="date" runat="server" CssClass="form-control lblreceiptdate" ID="ReceiptDate"  Text='<%#Eval("ReceiptDate").ToString()%>' Enabled='<%#setEnabled(Eval("feestatus").ToString())%>'/>
                                        <asp:RequiredFieldValidator ID="errReceiptDate" runat="server" ErrorMessage="Selecte Receipt Date" ControlToValidate="ReceiptDate" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                        <%--<asp:Label CssClass="lblreceiptdate" ID="ReceiptDate" Text='<%#Eval("ReceiptDate").ToString()%>' runat="server" Font-Bold="true"/>--%>
                                    </ItemTemplate>

                                    
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText=" Print " HeaderStyle-BackColor="#1CB5E0" HeaderStyle-CssClass="ftrow1">
                                    <ItemTemplate>
                                            <%--<asp:Button ID="printbuttonrcpt" runat="server" Text="Receipt" CssClass="btn btn-success" Visible='<%#setChecked(Eval("feestatus").ToString())%>' CommandName="printreceipt" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>"/>--%>
                                             
                                        <%--<asp:Button ID="printbuttonrcpt" runat="server" Text="Receipt" CssClass="btn btn-success" Visible='<%#setChecked(Eval("feestatus").ToString())%>' OnClick="printbuttonrcpt_Click"/>--%>

                                        <asp:HyperLink NavigateUrl='<%#getDownloadUrl(Eval("Academicyear").ToString(),Eval("grno").ToString(),Eval("std").ToString())%>' runat="server" Text="Receipt" CssClass="btn btn-success" Visible='<%#setChecked(Eval("feestatus").ToString())%>' Target="_blank"/>

                                    </ItemTemplate>

                                    
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4 col-sm-12">
                        <Button runat="server" ID="btnSave" style="width: 100%;margin-top:5px;position:relative;right:0;" class="btn btn-save" onserverclick="btnSave_ServerClick"><i class="fas fa-download mr-2"></i>Save</Button>
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
            $("#FeesModuleSection").addClass("menu-open");
            $("#FeesModuleItem").addClass("active");
            $("#menu_feesCollection").addClass("active");

            $(".select2").select2();
          

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
         var filename = `FeeCollection_${timestamp}.xlsx`;

         // Export to Excel
         XLSX.writeFile(workbook, filename);
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
