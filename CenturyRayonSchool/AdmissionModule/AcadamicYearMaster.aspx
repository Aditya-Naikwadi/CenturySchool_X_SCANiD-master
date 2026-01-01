<%@ Page Title="" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="AcadamicYearMaster.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.AcadamicYearMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <script>
     function isNumeric(evt) {
         // Get the key code of the pressed key
         var charCode = (evt.which) ? evt.which : event.keyCode;

         // Allow numbers (0-9), minus sign (-), and backspace (8)
         if ((charCode >= 48 && charCode <= 57) || charCode === 45 || charCode === 8) {
             return true;
         } else {
             // Prevent the input of any other characters
             evt.preventDefault ? evt.preventDefault() : (evt.returnValue = false);
             return false;
         }
     }
    
     function ApproveRecord() {
       
         var user=confirm("Do you really want to activate the selected year?");
         if (user) {
             
             return true;
         } else {
            
             return false;
         }
     }

     function DeApproveRecord() {

         var user = confirm("Do you really want to deactive the selected year?");
         if (user) {

             return true;
         } else {

             return false;
         }
     }

 </script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <section class="p-4 font-sans mb-5">
            <div class="row justify-content-center">
                <div class="row mt-2">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Acadamic Year Master</h5>
                        <div class="card-body">
                            <div class="row form-fields mt-2">
                                
                                <div class="col-md-6">
                                    <label for="lblacadamicyear" class="form-label mb-1">Acdamic Year</label>
                                    <asp:TextBox onkeypress="return isNumeric(event)" class="form-control" ID="txtacadmicyear" name="txtacadmicyear" runat="server" ></asp:TextBox> 
                                  <%--  <h5 style="font:bold; text-underline-position">Note:-</h5> <h6>Please enter acadamic year like 2024-2025 and also check the checkbox for active year and deactive the previous year.</h6>
                               --%>
                                </div>

                                <div class="col-md-6">
                                    <label for="lblacadamicyear" class="form-label mb-5"></label>
                                <asp:CheckBox type="checkbox" class="form-check-asp:TextBox" ID="chkyear"  runat="server" ></asp:CheckBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button runat="server"  OnClick="btn_save_Click" type="button" ID="btn_save" class="btn btn-info" Text="Save" />
                                    <lable runat="server">Total Year:-</lable> <lable runat="server" id="lbltotal"></lable>
                                </div>
                             
                                 <div class="col-md-12">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Acadamic Year List</h5>
                        <div class="card-body">
                            <div class="row">
                                <asp:GridView ID="gvacadamicyearList" runat="server" AutoGenerateColumns="false" class="table table-striped border-0 table-hover" OnRowCommand="acadamicyearList_RowCommand" OnRowDataBound="gvacadamicyearList_RowDataBound" >
                                    <Columns> 
                                        <asp:BoundField DataField="ID" HeaderText=" ID " />
                                        <asp:BoundField DataField="academicyear" HeaderText=" Academic Year " />
                                        <asp:BoundField DataField="active" HeaderText=" Active " />
                                        <asp:BoundField DataField="createdatetime" HeaderText=" Create DateTime " />
                                       
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Button Text="Activate" onclientclick="return ApproveRecord()" ID="btnactive"  runat="server"  CssClass="btn btn-success" CommandName="Activate" CommandArgument="<%# Container.DataItemIndex %>" />

                                                 <asp:Button Text="Deactivate" onclientclick="return DeApproveRecord()" ID="btndeactive"  runat="server"  CssClass="btn btn-danger" CommandName="Deactivate" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                                <!-- Modal -->
                            </div>
                        </div>
                    </div>
                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
