<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeFile="Alumini.aspx.cs" Inherits="CenturyRayonSchool.Alumini" %>
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
                    <div class="mt-4">
                        <!-- Button removed as per request -->
                    </div>
                </div>
            </div>
        </section>

        <!-- Features Section -->
        <section class="container alumni-features-container">
            <div class="row g-4 justify-content-center">
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100" data-aos="fade-up" data-aos-delay="100">
                        <div class="feature-icon">
                            <i class="fas fa-handshake"></i>
                        </div>
                        <h3>Reconnect</h3>
                        <p class="text-muted">Find old friends, teachers, and batchmates. Rebuild connections that
                            matter.</p>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100" data-aos="fade-up" data-aos-delay="200">
                        <div class="feature-icon">
                            <i class="fas fa-calendar-alt"></i>
                        </div>
                        <h3>Events & Reunions</h3>
                        <p class="text-muted">Stay updated with upcoming alumni meets, school events, and networking
                            sessions.</p>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6 d-flex align-items-stretch">
                    <div class="alumni-feature-card w-100" data-aos="fade-up" data-aos-delay="300">
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

        <!-- Registration Section Removed as per request -->

        <!-- AOS & Interactive Script -->
        <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script>
        <script>
            // Initialize AOS
            AOS.init({
                duration: 800,
                once: true,
                offset: 100
            });
        </script>

    </asp:Content>