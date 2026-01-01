<%@ Page Title="Application List" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="ApplicationList.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.ApplicationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //$("#ContentPlaceHolder1_admissionList").DataTable();
            $("#ContentPlaceHolder1_admissionList").prepend($("<thead></thead>").append($(this).find("tr:first"))).removeAttr('width').dataTable({

            });

        });
        function showModal() {
            var myModal = new bootstrap.Modal(document.getElementById('appList'))
            myModal.show()
        }




    </script>
    <script>
        $(document).ready(function () {
            $('#chkApproved').change(function () {
                if ($(this).is(':checked')) {
                    $('#chkReject').prop('checked', false);
                }
            });

            $('#chkReject').change(function () {
                if ($(this).is(':checked')) {
                    $('#chkApproved').prop('checked', false);
                }
            });
        });
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <section class="p-4 font-pt mb-5">
            <div class="row">
                <div class="col-md-12">
                    <%--Style="position:absolute;right:0;" Width="100"--%>
                    <%-- basappa holiday/--%>

                    <asp:Button Text="Downlaod" runat="server" CssClass="mb-3 float-start btn-sm btn btn-info" style="margin-right:5px" ID="btndownload" OnClick="btndownload_Click"/> 

                    <asp:Button runat="server" CssClass="mb-3 btn float-start btn-sm btn-secondary" ID="UploadAprrovallist" Text="Upload Approval List" OnClick="btnUploadAprrovallist_Click" />
                    <asp:FileUpload ID="FileUploadControl" runat="server" CssClass="mb-2 btn float-start btn-sm" />
                    
                    <asp:Button Text="Mastar Acadamic Year" runat="server" CssClass="mb-2 float-end btn-sm btn btn-dark" ID="btnmasterclick" OnClick="btnmasterclick_Click" />
                    <asp:Button runat="server" Text="Generate Excel" CssClass="mb-2 btn float-end btn-sm btn-success" ID="GenExcel" OnClick="GenExcel_Click" style="margin-right:5px" />

                    <%-- PostBackUrl="AcadamicYearMaster.aspx"--%>
                    <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" CssClass="mb-2 btn float-end btn-sm" ClientIDMode="Static" />
                    <asp:CheckBox ID="chkReject" runat="server" Text="Reject" CssClass="mb-2 btn float-end btn-sm b" ClientIDMode="Static" />


                </div>
                <div class="col-md-12">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Admission Form List</h5>
                        <div class="card-body">
                            <div class="row">
                                <asp:GridView ID="admissionList" runat="server" AutoGenerateColumns="false" class="table table-striped border-0 table-hover" OnRowCommand="admissionList_RowCommand" OnRowDataBound="admissionList_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="AdmissionID" HeaderText=" Admission ID " />
                                        <asp:BoundField DataField="studentname" HeaderText=" Student Name " />
                                        <asp:BoundField DataField="Surname" HeaderText=" Surname " />
                                        <asp:BoundField DataField="STD" HeaderText=" STD " />
                                        <asp:BoundField DataField="Date" HeaderText=" Submission Date " />
                                        <asp:BoundField DataField="ApprovalStatus" HeaderText=" Status" />
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:Button Text="Confirm" class="btn btn-sm btn-secondary" runat="server" CommandName="confirm" CommandArgument="<%# Container.DataItemIndex %>" Enabled='<%# Eval("ApprovalStatus").ToString() == "Pending" ? true : false %>' />

                                                <asp:Button Text="Reject" class="btn btn-sm btn-danger" runat="server" CommandName="reject" CommandArgument="<%# Container.DataItemIndex %>" Enabled='<%# Eval("ApprovalStatus").ToString() == "Pending" ? true : false %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Button Text="View Details" runat="server" class="btn btn-sm btn-warning" CommandName="View" CommandArgument="<%# Container.DataItemIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <!-- Modal -->
                                --
                                <div class="modal fade" id="appList" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
