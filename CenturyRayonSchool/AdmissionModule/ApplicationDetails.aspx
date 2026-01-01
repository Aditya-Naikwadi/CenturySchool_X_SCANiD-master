<%@ Page Title="Application Details" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="ApplicationDetails.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.ApplicationDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            
        });
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('appDetails'))
            myModal.show()
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <section class="p-4 font-pt mb-5">
            <div class="row mt-2">
                <div class="col-md-12">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Student Details - <span runat="server" id="appId"></span></h5>
                        <asp:Button Text="Go To List" runat="server" CssClass="btn btn-danger" Width="100" Style="position:absolute;right:0;" ID="btngotolist" OnClick="btngotolist_Click" /> <%--PostBackUrl="ApplicationDetails.aspx" --%>
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Image runat="server" ID="childPhoto" class="mb-2 w-100"/> <br />
                                    <a runat="server" target="_blank" id="BirthCert">View Birth Certificate</a><br />
                                    <a runat="server" target="_blank" id="ResidentialProof">View Residential Proof </a><br />
                                    <a runat="server" target="_blank" id="transfercert">View Transfer Certificate </a><br />
                                    <a runat="server" target="_blank" id="otherproof">View Other  Proof </a><br />
                                    <a runat="server" target="_blank" id="castcerfificate">View Caste Certificate </a>
                                   
                                </div>
                                <div class="col-md-4">
                                    <h5 class="fw-bold">Basic Details</h5><hr />
                                    <p runat="server" id="studName">Student Name - </p>
                                    <p runat="server" id="academicYear">Academic Year - </p>
                                    <p runat="server" id="gender">Gender - </p>
                                    <p runat="server" id="laststandarpassed">Last Standard passed - </p>
                                    <p runat="server" id="std">STD - </p>
                                    <p runat="server" id="dob">DOB - </p>
                                    <p runat="server" id="POB">Place of Birth - </p>
                                    <p runat="server" id="Taluka">Taluka - </p>
                                    <p runat="server" id="District">District - </p>
                                    <p runat="server" id="State">State - </p>
                                    <p runat="server" id="nationality">Nationality - </p>
                                    <p runat="server" id="motherTounge">Mother Tounge - </p>
                                    <p runat="server" id="religion">Religion - </p>
                                    <p runat="server" id="Caste">Caste - </p>
                                    <p runat="server" id="Category">Category - </p>
                                    <p runat="server" id="Aadharno">Aadhar no - </p>
                                    <p runat="server" id="LastSchoolAttend">Last School Attend - </p>

                                    <%--<p runat="server" id="P1">Religion - </p><%-- basappa--%>

                                    <h5 runat="server" id="siblingHeader" class="fw-bold mt-4">Sibling Details</h5>
                                    <hr runat="server" id="hrTag"/>
                                    <p runat="server" id="sbGRNO1"></p>
                                    <p runat="server" id="sbName1"></p>
                                    <p runat="server" id="sbSTD1"></p>
                                    <p runat="server" id="sbGRNO2"></p>
                                    <p runat="server" id="sbName2"></p>
                                    <p runat="server" id="sbSTD2"></p>
                                </div>
                                <div class="col-md-4">
                                    <h5 class="fw-bold">Family Details</h5><hr />
                                    <p runat="server" id="fatherName">Father Name - </p>
                                    <p runat="server" id="fatherQualification">Father Qualification - </p>
                                    <p runat="server" id="fatherOccupation">Father Occupation - </p>
                                    <p runat="server" id="fatherEmailID">Father EmailID - </p>
                                    <p runat="server" id="fatherContact">Father Contact - </p>
                                    <p runat="server" id="isemployee"> IS Century Rayon High School Employee ? - </p>
                                    <p runat="server" id="Department">Department - </p>
                                    <p runat="server" id="TicketNo">Ticket No - </p>
                                    <p runat="server" id="motherName">Mother Name - </p>
                                    <p runat="server" id="motherQualification">Mother Qualification - </p>
                                    <p runat="server" id="motherOccupation">Mother Occupation - </p>
                                    <p runat="server" id="motherEmailID">Mother EmailID - </p>
                                    <p runat="server" id="motherContact">Mother Contact - </p>
                                    <p runat="server" id="address">Address - </p>
                                    <p runat="server" id="locality">Locality - </p>
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                            <!-- Modal -->
            <div class="modal fade" id="appDetails" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
</asp:Content>
