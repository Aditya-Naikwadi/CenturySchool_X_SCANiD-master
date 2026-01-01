<%@ Page Title="Admin Login" Language="C#" MasterPageFile="~/AdmissionModule/MasterPage.Master" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="CenturyRayonSchool.AdmissionModule.AdminLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <section class="p-4 font-sans mb-5">
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card card-shw mb-3">
                        <h5 class="card-header border-btm">Admin Login</h5>
                        <div class="card-body">
                            <div class="row form-fields mt-2">
                                <div class="col-md-12">
                                    <label for="adminEmailID" class="form-label mb-1">Enter Email ID</label>
                                    <asp:TextBox class="form-control" ID="adminEmailID" name="adminEmailID" runat="server" required></asp:TextBox>
                                    <%--TextMode="Email"--%>
                                    <div class="valid-feedback">Valid Email ID.</div>
                                    <div class="invalid-feedback">Please enter first name</div>
                                </div>
                                <div class="col-md-12">
                                    <label for="adminPassword" class="form-label mb-1">Enter Password</label>
                                    <asp:TextBox TextMode="Password" class="form-control" ID="adminPassword" name="adminPassword" runat="server" required></asp:TextBox>
                                    <div class="valid-feedback">Valid password</div>
                                    <div class="invalid-feedback">Please enter password</div>
                                </div>
                                <div class="col-md-6">
                                    <asp:Button runat="server" OnClick="AdminLoginBtn_Click" type="button" ID="AdminLoginBtn" class="btn btn-info" Text="Login.." />
                                </div>
                                <div class="col-md-12">
                                    <asp:Label runat="server" class="text-danger fw-bold mb-2" ID="lblMessage"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <%-- basappa--%><%-- basappa--%>

    <script>
        $("#navadminlink").addClass("active");
    </script>
</asp:Content>
