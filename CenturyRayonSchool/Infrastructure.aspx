<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeFile="Infrastructure.aspx.cs" Inherits="CenturyRayonSchool.Infrastructure" %>

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
          <div class="infra-title">School Infrastructure</div>
          <p class="infra-subtitle">
            Century Rayon High School provides state-of-the-art facilities designed to foster academic excellence,
            creative thinking, and holistic development.
          </p>
        </header>

        <!-- Key Facilities Grid -->
        <div class="feature-grid">
          <!-- IT Classrooms -->
          <div class="infra-card animate-up delay-1">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-chalkboard-teacher"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">IT Enabled Classrooms</h3>
              <p class="card-desc">Modern classrooms equipped with digital learning tools to provide proper ambience and
                interactive learning experiences.</p>
              <span class="card-stat">39 Classrooms</span>
            </div>
          </div>

          <!-- Computer Labs -->
          <div class="infra-card animate-up delay-2">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-desktop"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Computer Labs</h3>
              <p class="card-desc">Advanced computer labs introducing students to computer science and programming from
                primary to secondary levels.</p>
              <span class="card-stat">2 Labs</span>
            </div>
          </div>

          <!-- Science Labs -->
          <div class="infra-card animate-up delay-3">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-flask"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Science Labs</h3>
              <p class="card-desc">Fully equipped laboratories providing an open environment for experimentation,
                research, and practical learning.</p>
              <span class="card-stat">2 Labs</span>
            </div>
          </div>

          <!-- E-Library -->
          <div class="infra-card animate-up delay-1">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-book-reader"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">E-Library</h3>
              <p class="card-desc">Vast collection of digital and physical books providing access to a rich array of
                stories, ideas, and information.</p>
              <span class="card-stat">1 Facility</span>
            </div>
          </div>

          <!-- Training Room -->
          <div class="infra-card animate-up delay-2">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-users-cog"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Training Room</h3>
              <p class="card-desc">Dedicated space for staff presentations, student workshops, and expert training
                sessions to enhance skills.</p>
              <span class="card-stat">1 Room</span>
            </div>
          </div>

          <!-- Assembly Hall -->
          <div class="infra-card animate-up delay-3">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-theater-masks"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Assembly Hall</h3>
              <p class="card-desc">A vibrant gathering place hosting morning assemblies, cultural events, and
                celebrations.</p>
              <span class="card-stat">1 Hall</span>
            </div>
          </div>

          <!-- Sports Facilities -->
          <div class="infra-card animate-up delay-1">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-running"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Sports & Playground</h3>
              <p class="card-desc">Indoor and outdoor facilities for Football, Cricket, Volleyball, Kabaddi, Kho-Kho,
                Chess, and more.</p>
              <span class="card-stat">2 Grounds</span>
            </div>
          </div>

          <!-- Staffrooms -->
          <div class="infra-card animate-up delay-2">
            <div class="card-icon-wrapper">
              <i class="card-icon fas fa-coffee"></i>
            </div>
            <div class="card-content">
              <h3 class="card-title">Staff Rooms</h3>
              <p class="card-desc">Comfortable teachers' lounge equipped with modern amenities for effective planning
                and relaxation.</p>
              <span class="card-stat">1 Lounge</span>
            </div>
          </div>
        </div>
      </div>
    </section>
  </asp:Content>