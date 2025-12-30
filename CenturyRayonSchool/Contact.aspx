<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="Contact.aspx.cs" Inherits="CenturyRayonSchool.Contact" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <!-- Font Awesome -->
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
        <!-- Custom Contact CSS -->
        <link href="css/contact-modern.css" rel="stylesheet" />
        <!-- Animate on Scroll -->
        <link href="https://unpkg.com/aos@2.3.1/dist/aos.css" rel="stylesheet">
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Hero Section -->
        <section class="contact-hero">
            <div class="content" data-aos="fade-up">
                <h1>Get in Touch</h1>
                <p>WE'D LOVE TO HEAR FROM YOU</p>
            </div>
        </section>

        <div class="container pb-5">

            <!-- Info Cards -->
            <div class="row g-4 mb-5">
                <!-- Address -->
                <div class="col-md-4" data-aos="fade-up" data-aos-delay="100">
                    <div class="contact-info-card">
                        <div class="icon-box">
                            <i class="fas fa-map-marker-alt"></i>
                        </div>
                        <h4>Visit Us</h4>
                        <p>Century Rayon High School<br>Shahad, Ulhasnagar<br>Maharashtra 421103</p>
                    </div>
                </div>

                <!-- Phone -->
                <div class="col-md-4" data-aos="fade-up" data-aos-delay="200">
                    <div class="contact-info-card">
                        <div class="icon-box">
                            <i class="fas fa-phone-alt"></i>
                        </div>
                        <h4>Call Us</h4>
                        <p><a href="tel:02512566389">0251-256 6389</a></p>
                        <p class="text-muted small mt-1">Mon-Fri, 9am - 4pm</p>
                    </div>
                </div>

                <!-- Email -->
                <div class="col-md-4" data-aos="fade-up" data-aos-delay="300">
                    <div class="contact-info-card">
                        <div class="icon-box">
                            <i class="fas fa-envelope"></i>
                        </div>
                        <h4>Email Us</h4>
                        <p><a href="mailto:crsshahad@gmail.com">crsshahad@gmail.com</a></p>
                        <p class="text-muted small mt-1">We reply within 24 hours</p>
                    </div>
                </div>
            </div>

            <!-- Map & Form Section -->
            <div class="row g-4 align-items-stretch">

                <!-- Map -->
                <div class="col-lg-6" data-aos="fade-right">
                    <div class="map-container">
                        <iframe
                            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3766.852136697808!2d73.1665786149038!3d19.245274886991034!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x3be7941c9fdc2557%3A0x7069f8e8cc8ded70!2sCentury%20Rayon%20High%20School%2C%20Shahad!5e0!3m2!1sen!2sin!4v1662353329785!5m2!1sen!2sin"
                            allowfullscreen="" loading="lazy" referrerpolicy="no-referrer-when-downgrade"></iframe>
                    </div>
                </div>

                <!-- Contact Form -->
                <div class="col-lg-6" data-aos="fade-left">
                    <div class="contact-form-wrapper">
                        <div class="form-header mb-4">
                            <h3>Send us a Message</h3>
                            <p class="text-muted">Have a question? Fill out the form below.</p>
                        </div>

                        <div class="row g-3">
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"
                                        placeholder="Your Name"></asp:TextBox>
                                    <label for="txtName">Your Name</label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"
                                        placeholder="Phone Number"></asp:TextBox>
                                    <label for="txtPhone">Phone Number</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"
                                        placeholder="Email Address"></asp:TextBox>
                                    <label for="txtEmail">Email Address</label>
                                </div>
                            </div>
                            <div class="col-12">
                                <div class="form-floating">
                                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control"
                                        TextMode="MultiLine" Height="120px" placeholder="Message"></asp:TextBox>
                                    <label for="txtMessage">Your Message</label>
                                </div>
                            </div>
                            <div class="col-12 mt-4">
                                <asp:Button ID="btnSend" runat="server" Text="Send Message"
                                    CssClass="btn btn-contact w-100"
                                    OnClientClick="return confirm('Message Sent! We will get back to you shortly.');" />
                            </div>
                        </div>

                    </div>
                </div>

            </div>

        </div>

        <!-- AOS Script -->
        <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script>
        <script>
            AOS.init({
                duration: 800,
                once: true,
                offset: 100
            });
        </script>
    </asp:Content>