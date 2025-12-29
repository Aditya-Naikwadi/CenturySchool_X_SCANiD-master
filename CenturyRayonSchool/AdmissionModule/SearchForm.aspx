<%@ Page Title="Search Form" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeBehind="SearchForm.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.SearchForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <script type="text/javascript">
        $(document).ready(function () {
            
        });
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('searchModal'))
            myModal.show()
        }
      </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <section class="p-4 font-sans mb-5">
            <div class="row justify-content-center mt-2">
                <div class="col-md-8">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Search Admission Form</h5>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="searchApplicationNumber" class="form-label mb-1">Enter Application Number</label>
                                    <asp:TextBox type="text" class="form-control" id="searchApplicationNumber" name="searchApplicationNumber" runat="server" required></asp:TextBox>
                                </div>
                               <%-- <div class="col-md-4">
                                    <label for="emailID" class="form-label mb-1">Enter Phone Number</label>
                                    <asp:TextBox type="text" class="form-control" id="emailID" name="emailID" runat="server" required></asp:TextBox>                           
                                </div>--%>
                                <div class="col-md-4 d-flex align-end">
                                    <asp:Button runat="server" OnClick="SearchAndGoToModifyForm_Click" type="button" ID="SearchAndGoToModifyForm" class="btn btn-info" Text="Search Application" />
                                </div>
                            </div>
                            <div class="row mt-4" runat="server" id="OTPSection">
                                <div class="col-md-4">
                                    <label for="otp" class="form-label mb-1">Enter OTP</label>
                                    <asp:TextBox type="text" class="form-control" id="otp" name="otp" runat="server" MaxLength="6"></asp:TextBox>                           
                                </div>
                                <div class="col-md-4 d-flex align-end">
                                    <asp:Button runat="server" OnClick="GoToModify_Click" type="button" ID="GoToModify" class="btn btn-info" Text="Modify Form" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                <!-- Modal -->
            <div class="modal fade" id="searchModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="exampleModalLabel">Search Results</h1>
                            <asp:Button runat="server" ID="closeModal" class="btn-close" data-bs-dismiss="modal" aria-label="Close" />
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label runat="server" class="text-success fw-bolder mb-2" ID="statusMsg"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>

    <script>
        $("#navmodifylink").addClass("active");
    </script>
</asp:Content>
