<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeFile="Officehours.aspx.cs" Inherits="CenturyRayonSchool.Officehours" %>
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

        <!-- Morning Shift Card -->
        <div class="admin-card" style="animation-delay: 0.1s;">
          <h2 class="admin-card-title">
            <i class="fas fa-sun"></i>
            Morning Shift
          </h2>

          <div class="time-details-grid">
            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-user-graduate"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Students Time</div>
                <div class="time-detail-value">7:00 AM - 11:45 AM</div>
              </div>
            </div>

            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Teachers Time</div>
                <div class="time-detail-value">6:55 AM - 12:35 PM</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Afternoon Shift Card -->
        <div class="admin-card" style="animation-delay: 0.2s;">
          <h2 class="admin-card-title">
            <i class="fas fa-moon"></i>
            Afternoon Shift
          </h2>

          <div class="time-details-grid">
            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-user-graduate"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Students Time</div>
                <div class="time-detail-value">12:45 PM - 5:20 PM</div>
              </div>
            </div>

            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Teachers Time</div>
                <div class="time-detail-value">12:15 PM - 5:55 PM</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Parent's Visiting Hours Card -->
        <div class="admin-card" style="animation-delay: 0.3s;">
          <h2 class="admin-card-title">
            <i class="fas fa-users"></i>
            Parent's Visiting Hours
          </h2>

          <div class="time-details-grid">
            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-clock"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Morning Session</div>
                <div class="time-detail-value">12:00 PM</div>
              </div>
            </div>

            <div class="time-detail-item">
              <div class="time-detail-icon">
                <i class="fas fa-clock"></i>
              </div>
              <div class="time-detail-content">
                <div class="time-detail-label">Afternoon Session</div>
                <div class="time-detail-value">5:30 PM</div>
              </div>
            </div>
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
            <span>Teachers arrive 5 minutes before students and stay after for administrative work</span>
          </div>

          <div class="info-item">
            <i class="fas fa-check-circle"></i>
            <span>Parents are requested to visit only during the designated visiting hours</span>
          </div>
        </div>

      </div>
    </section>

  </asp:Content>