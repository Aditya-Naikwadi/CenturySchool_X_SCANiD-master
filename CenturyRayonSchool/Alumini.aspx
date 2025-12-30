<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Alumini.aspx.cs" Inherits="CenturyRayonSchool.Alumini" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Font Awesome -->
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
        <!-- Custom Alumni CSS -->
        <link href="css/alumni-modern.css" rel="stylesheet" />
        <!-- Animate on Scroll -->
        <link href="https://unpkg.com/aos@2.3.1/dist/aos.css" rel="stylesheet">
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Hero Section -->
        <section class="alumni-hero">
            <div class="alumni-hero-content" data-aos="fade-up">
                <h1>Welcome Home, Alumni</h1>
                <p>Connect • Share • Inspire</p>
                <div class="mt-4">
                    <a href="#register" class="btn btn-warning btn-lg rounded-pill px-5 fw-bold">Join the Network</a>
                </div>
            </div>
        </section>

        <!-- Features Section -->
        <section class="container" style="margin-top: -50px; position: relative; z-index: 2;">
            <div class="row g-4 justify-content-center">
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100">
                        <div class="feature-icon">
                            <i class="fas fa-handshake"></i>
                        </div>
                        <h3>Reconnect</h3>
                        <p class="text-muted">Find old friends, teachers, and batchmates. Rebuild connections that
                            matter.</p>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100">
                        <div class="feature-icon">
                            <i class="fas fa-calendar-alt"></i>
                        </div>
                        <h3>Events & Reunions</h3>
                        <p class="text-muted">Stay updated with upcoming alumni meets, school events, and networking
                            sessions.</p>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100">
                        <div class="feature-icon">
                            <i class="fas fa-briefcase"></i>
                        </div>
                        <h3>Career Network</h3>
                        <p class="text-muted">Mentor juniors, share job opportunities, and grow your professional
                            network.</p>
                    </div>
                </div>
            </div>
        </section>

        <!-- Registration Section -->
        <section id="register" class="registration-section">
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <!-- Removing AOS to ensure visibility -->
                        <div class="registration-card">
                            <div class="registration-header">
                                <h3><i class="fas fa-user-graduate me-2"></i> Alumni Registration</h3>
                                <p class="mb-0 text-white-50">Join our growing community today</p>
                            </div>
                            <div class="registration-body">
                                <div class="row g-3">
                                    <!-- Personal Info -->
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"
                                                placeholder="First Name"></asp:TextBox>
                                            <label for="txtFirstName">First Name</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"
                                                placeholder="Last Name"></asp:TextBox>
                                            <label for="txtLastName">Last Name</label>
                                        </div>
                                    </div>

                                    <!-- Contact Info -->
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                                                TextMode="Email" placeholder="Email Address"></asp:TextBox>
                                            <label for="txtEmail">Email Address</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"
                                                TextMode="Phone" placeholder="Phone Number"></asp:TextBox>
                                            <label for="txtPhone">Phone Number</label>
                                        </div>
                                    </div>

                                    <!-- Current Status -->
                                    <div class="col-12">
                                        <div class="form-floating mb-3">
                                            <asp:TextBox ID="txtCurrentPosition" runat="server" CssClass="form-control"
                                                placeholder="Current Position / Company"></asp:TextBox>
                                            <label for="txtCurrentPosition">Current Position / Company</label>
                                        </div>
                                    </div>

                                    <!-- Passing Year -->
                                    <div class="col-md-6">
                                        <div class="form-floating mb-3">
                                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-select">
                                                <asp:ListItem Text="Select Passing Year" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                                                <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                                                <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                                                <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                <asp:ListItem Text="Earlier" Value="Earlier"></asp:ListItem>
                                            </asp:DropDownList>
                                            <label for="ddlYear">Year of Passing</label>
                                        </div>
                                    </div>

                                    <!-- Message -->
                                    <div class="col-12">
                                        <div class="form-floating mb-4">
                                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control"
                                                TextMode="MultiLine" Height="100px" placeholder="Share a memory...">
                                            </asp:TextBox>
                                            <label for="txtMessage">Share a memory from school...</label>
                                        </div>
                                    </div>

                                    <!-- Submit Button -->
                                    <div class="col-12 text-center">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Register Now"
                                            CssClass="btn btn-alumni"
                                            OnClientClick="return confirm('Thank you for registering! We will review your details.');" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


        <!-- AOS Script -->
        <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script>
        <script>
            AOS.init({
                duration: 800,
                once: true
            });
        </script>

    </asp:Content>