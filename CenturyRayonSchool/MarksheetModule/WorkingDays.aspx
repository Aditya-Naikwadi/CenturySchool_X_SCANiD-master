<%@ Page Title="" Language="C#" MasterPageFile="~/MarksheetModule/Marksheet.Master" AutoEventWireup="true" CodeBehind="WorkingDays.aspx.cs" Inherits="CenturyRayonSchool.MarksheetModule.WorkingDays" %>
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
    <section class="p-2">
        <div class="card card-sh">
            <div class="card-header card-mobile"><a href="Dashboard.aspx" class="mr-2 text-secondary"><i class="fas fa-arrow-left"></i></a>
               Working Days
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
                         <label for="month" class="form-label mb-1">MONTH :</label>
                        <asp:DropDownList ID="month" class="form-control select2" runat="server">
                            <asp:ListItem Value="">Select Month </asp:ListItem>  
                                <asp:ListItem Value="Jan">January </asp:ListItem>  
                                <asp:ListItem Value="Feb">February</asp:ListItem>  
                                <asp:ListItem Value="Mar">March</asp:ListItem>  
                                <asp:ListItem Value="Apr">April</asp:ListItem>  
                                <asp:ListItem Value="May">May</asp:ListItem>  
                                <asp:ListItem Value="Jun">June</asp:ListItem>  
                                <asp:ListItem Value="Jul">July</asp:ListItem>  
                                <asp:ListItem Value="Aug">August</asp:ListItem>  
                                <asp:ListItem Value="Sep">September</asp:ListItem>  
                                <asp:ListItem Value="Oct">October</asp:ListItem>  
                                <asp:ListItem Value="Nov">November</asp:ListItem>  
                                <asp:ListItem Value="Dec">December</asp:ListItem>  
                        </asp:DropDownList>
                        <asp:CustomValidator ID="MonthCustomValid" runat="server" ErrorMessage="Select Month" ControlToValidate="month" onservervalidate="MonthCustomvalid_ServerValidate" ForeColor="Red"></asp:CustomValidator>
                    </div>
                     
                    <div class="col-md-4">
                        
                        <label for="txtworking" class="form-label mb-1">Total  Working Days</label>
                        <asp:TextBox ID="txtworkday" runat="server" type="text" class="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-2" style="padding-top: 16px;">
                        <Button runat="server" ID="SaveData" class="btn btn-saveData mt-2"  onserverclick="SaveData_ServerClick"><i class="fas fa-angle-double-down mr-2"></i>Save Data</Button>
                        
                    </div>
                    <div class="col-md-12">
                        <asp:GridView CssClass="table table-sm table-bordered mt-3" AutoGenerateColumns="false" ID="gridHeaders"  OnRowCommand="gridHeaders_RowCommand" runat="server" Width="100%" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField DataField="std" HeaderText=" STD " ItemStyle-Width="10%" HeaderStyle-Width="30%" />
                                <asp:BoundField DataField="Month" HeaderText=" MONTH " ItemStyle-Width="10%" HeaderStyle-Width="30%" />
                                <asp:BoundField DataField="TotalDays" HeaderText=" WORKING DAY " ItemStyle-Width="10%" HeaderStyle-Width="30%" />
                                 <asp:TemplateField HeaderText=" Action"  ItemStyle-Width="30%" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <%--<Button runat="server" ID="btnEdit" class="btn btn-edit mt-2" CommandName="editfeesheader"><i class="fas fa-edit mr-2"></i>Edit</Button>--%>
                                       
                                        <%--<asp:Button runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deletefeesheader" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" Text="Delete"/>--%>
                                        
                                        <asp:LinkButton runat="server" ID="btnDelete" CssClass="btn btn-delete mt-2" CommandName="deletedays" CommandArgument="<%# ((GridViewRow) Container).RowIndex%>" OnClientClick="Confirm()"><i class="fas fa-trash mr-2"></i>Delete</asp:LinkButton>
                                    </ItemTemplate>
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
            $("#menu_Working").addClass("active");

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
              if (confirm("Are you sure you want to Delete Working Days. Please Contact Administrator For Changes in Working Days.")) {
                  confirm_value.value = "Yes";
              } else {
                  confirm_value.value = "No";
              }
              document.forms[0].appendChild(confirm_value);
          }
        
      </script>
</asp:Content>
