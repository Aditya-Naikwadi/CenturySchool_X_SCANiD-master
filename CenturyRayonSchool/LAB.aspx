<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true" CodeFile="LAB.aspx.cs"
    Inherits="CenturyRayonSchool.LAB" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="https://fonts.googleapis.com/css?family=Montserrat:400,400i,600,700,700i&display=swap"
            rel="stylesheet" />
        <link href='https://fonts.googleapis.com/css2?family=Quintessential&display=swap' rel='stylesheet' />
        <link href="../css/fontawesome-all.css" rel="stylesheet" />
        <link href="css/infrastructure-modern.css" rel="stylesheet" />
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <section class="infra-section">
            <div class="infra-container">
                <header class="infra-header animate-up">
                    <div class="infra-title">Our Laboratories</div>
                    <p class="infra-subtitle">
                        Practical learning is at the heart of our curriculum. Our laboratories are designed to encourage
                        experimentation, critical thinking, and hands-on discovery.
                    </p>
                </header>

                <div class="lab-grid">
                    <!-- Computer Lab -->
                    <div class="lab-card animate-up delay-1">
                        <div class="lab-image"
                            style="background-image: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%); display: flex; align-items: center; justify-content: center;">
                            <i class="fas fa-desktop" style="font-size: 5rem; color: rgba(255,255,255,0.3);"></i>
                        </div>
                        <div class="lab-details">
                            <div class="card-icon"
                                style="color: var(--primary-color); margin-bottom: 15px; font-size: 2rem;">
                                <i class="fas fa-laptop-code"></i>
                            </div>
                            <h3 class="card-title">Computer Laboratory</h3>
                            <p class="card-desc">
                                Our advanced computer labs are equipped with the latest hardware and software to ensure
                                students stay ahead in the digital age.
                            </p>
                            <ul style="color: var(--text-light); margin-bottom: 20px; padding-left: 20px;">
                                <li>High-speed internet connectivity</li>
                                <li>Latest programming software</li>
                                <li>1:1 Student-Computer ratio</li>
                                <li>Interactive smart boards</li>
                            </ul>
                        </div>
                    </div>

                    <!-- Science Lab -->
                    <div class="lab-card animate-up delay-2">
                        <div class="lab-image"
                            style="background-image: linear-gradient(135deg, #11998e 0%, #38ef7d 100%); display: flex; align-items: center; justify-content: center;">
                            <i class="fas fa-flask" style="font-size: 5rem; color: rgba(255,255,255,0.3);"></i>
                        </div>
                        <div class="lab-details">
                            <div class="card-icon"
                                style="color: var(--primary-color); margin-bottom: 15px; font-size: 2rem;">
                                <i class="fas fa-microscope"></i>
                            </div>
                            <h3 class="card-title">Science Laboratory</h3>
                            <p class="card-desc">
                                A fully stocked science lab that brings textbooks to life, complying with all safety
                                standards for Physics, Chemistry, and Biology experiments.
                            </p>
                            <ul style="color: var(--text-light); margin-bottom: 20px; padding-left: 20px;">
                                <li>Modern apparatus & chemicals</li>
                                <li>Safety-first design</li>
                                <li>Separate zones for PCBs</li>
                                <li>Research projects guidance</li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="nicdark_space60"></div>

                <!-- Additional Info -->
                <div class="infra-card animate-up delay-3" style="text-align: center; padding: 40px;">
                    <h3 class="card-title" style="margin-bottom: 20px;">Safety & Guidance</h3>
                    <p class="card-desc" style="max-width: 800px; margin: 0 auto;">
                        All laboratory sessions are conducted under the strict supervision of qualified instructors and
                        lab assistants.
                        Students are trained in safety protocols before handling any equipment or chemicals.
                    </p>
                </div>
            </div>
        </section>
    </asp:Content>