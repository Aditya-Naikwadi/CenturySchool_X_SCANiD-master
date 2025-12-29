<%@ Page Title="Fees Form" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('exampleModal'))
            myModal.show()
        }

        $(document).ready(function () {
        });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-page">
       
        <asp:Image ImageUrl="Resources/Images/28466.jpg" runat="server" />
        <!-- Button trigger modal -->
        <button type="button" id="btnShowModal" class="btn btn-warning btn-track" data-bs-toggle="modal" data-bs-target="#exampleModal">
            Track Application
        </button>
        <h1>Welcome to Century Rayon High School, Shahad</h1>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="exampleModalLabel">Track Application</h1>
                    <asp:Button runat="server" OnClick="closeModal_Click" ID="closeModal" class="btn-close" data-bs-dismiss="modal" aria-label="Close" />
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-8">
                            <label for="applicationNumber" class="form-label mb-1">Enter Application Number</label>
                            <asp:TextBox type="text" class="form-control" id="applicationNumber" name="applicationNumber" runat="server" required></asp:TextBox>
                        </div>
                        <div class="col-md-12">
                            <asp:Label runat="server" class="text-success fw-bolder mb-2" ID="statusMsg"></asp:Label>
                            <%--<p class="mt-3">Status of application - #number is - Approved</p>--%>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>--%>
                    <asp:Button runat="server" OnClick="getDetails_Click" ID="getDetails" class="btn btn-info" Text="Get Application Details" />
                    <asp:Button runat="server" OnClick="downloadForm_Click" ID="downloadForm" class="btn btn-success" Text="Download Form" />
                </div>
            </div>
        </div>
    </div>

    <script>
        $("#navhomelink").addClass("active");
    </script>
</asp:Content>
