<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeBehind="Officehours.aspx.cs" Inherits="CenturyRayonSchool.Officehours" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Modern CSS -->
    <link href="css/admin-modern.css" rel="stylesheet" />
    <link href="css/fontawesome-all.css" rel="stylesheet" />
  </asp:Content>

  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="admin-modern-section">
      <!-- Hero Header -->
      <div class="admin-hero-header">
        <h1 class="admin-hero-title">Office Hours</h1>
        <p class="admin-hero-subtitle">School Timing Information</p>
      </div>

      <div class="admin-container">

        <!-- Time Cards Container -->
        <div class="time-cards-container">

          <!-- Office Hours Card -->
          <div class="time-card office">
            <div class="time-card-icon">
              <i class="fas fa-building"></i>
            </div>
            <div class="time-card-label">Office Hours</div>
            <div class="time-card-time">10:30 AM - 5:30 PM</div>
          </div>

          <!-- Morning Shift Card -->
          <div class="time-card morning">
            <div class="time-card-icon">
              <i class="fas fa-sun"></i>
            </div>
            <div class="time-card-label">Morning Shift</div>
            <div class="time-card-time">7:00 AM - 12:35 PM</div>
          </div>

          <!-- Afternoon Shift Card -->
          <div class="time-card afternoon">
            <div class="time-card-icon">
              <i class="fas fa-moon"></i>
            </div>
            <div class="time-card-label">Afternoon Shift</div>
            <div class="time-card-time">12:30 PM - 6:05 PM</div>
          </div>

        </div>

        <!-- Additional Information Card -->
        <div class="admin-card" style="animation-delay: 0.4s;">
          <h2 class="admin-card-title">
            <i class="fas fa-info-circle"></i>
            Important Information
          </h2>

          <div class="info-item">
            <i class="fas fa-check-circle"></i>
            <span>The school operates in two shifts to accommodate all students effectively</span>
          </div>

          <div class="info-item">
            <i class="fas fa-check-circle"></i>
            <span>Office remains open throughout the day for administrative assistance</span>
          </div>

          <div class="info-item">
            <i class="fas fa-check-circle"></i>
            <span>Parents are requested to visit during office hours for any inquiries</span>
          </div>
        </div>

      </div>
    </section>

  </asp:Content>