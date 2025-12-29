<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeBehind="Visitinghours.aspx.cs" Inherits="CenturyRayonSchool.Visitinghours" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Modern CSS -->
    <link href="css/admin-modern.css" rel="stylesheet" />
    <link href="css/fontawesome-all.css" rel="stylesheet" />
  </asp:Content>

  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="admin-modern-section">
      <!-- Hero Header -->
      <div class="admin-hero-header">
        <h1 class="admin-hero-title">Visiting Hours</h1>
        <p class="admin-hero-subtitle">For Parents & Guardians</p>
      </div>

      <div class="admin-container">

        <!-- Info Sections Container -->
        <div class="info-sections-container">

          <!-- Office Hours Section -->
          <div class="info-section">
            <div class="info-section-header">
              <div class="info-section-icon">
                <i class="fas fa-clock"></i>
              </div>
              <h3 class="info-section-title">Office Hours for Parents</h3>
            </div>
            <div class="info-item">
              <i class="fas fa-calendar-check"></i>
              <span><strong>Office Hours:</strong> 10:30 AM to 1:00 PM only</span>
            </div>
          </div>

          <!-- Fees Information Section -->
          <div class="info-section">
            <div class="info-section-header">
              <div class="info-section-icon">
                <i class="fas fa-money-bill-wave"></i>
              </div>
              <h3 class="info-section-title">Fees Payment</h3>
            </div>
            <div class="info-item">
              <i class="fas fa-university"></i>
              <span><strong>Fees:</strong> Fees to be deposited in UCO Bank, as per the date given by school
                authority</span>
            </div>
          </div>

          <!-- H.M. Visiting Hours Section -->
          <div class="info-section">
            <div class="info-section-header">
              <div class="info-section-icon">
                <i class="fas fa-user-tie"></i>
              </div>
              <h3 class="info-section-title">Head Master Availability</h3>
            </div>
            <div class="info-item">
              <i class="fas fa-calendar-alt"></i>
              <span><strong>H.M.:</strong> H.M. will be available between 11:00 AM to 12:30 PM only on Saturday</span>
            </div>
          </div>

          <!-- Teachers & Supervisors Section -->
          <div class="info-section">
            <div class="info-section-header">
              <div class="info-section-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <h3 class="info-section-title">Supervisors & Teachers</h3>
            </div>
            <div class="info-item">
              <i class="fas fa-clock"></i>
              <span><strong>Availability:</strong> 20 minutes after school hours</span>
            </div>
          </div>

          <!-- Monthly Contact Timings Section -->
          <div class="info-section">
            <div class="info-section-header">
              <div class="info-section-icon">
                <i class="fas fa-calendar-week"></i>
              </div>
              <h3 class="info-section-title">Monthly Contact Timings</h3>
            </div>
            <p class="info-item">
              <i class="fas fa-info-circle"></i>
              <span>Parents should contact the class teachers regarding their wards on the last date of every month
                between the following timings:</span>
            </p>

            <div class="timing-grid">
              <div class="timing-item">
                <span class="timing-label">English Medium</span>
                <span class="timing-value">10:00 AM - 11:00 AM</span>
              </div>
              <div class="timing-item">
                <span class="timing-label">Hindi/Marathi Medium</span>
                <span class="timing-value">4:00 PM - 5:00 PM</span>
              </div>
            </div>
          </div>

          <!-- Important Notice -->
          <div class="highlight-box">
            <i class="fas fa-exclamation-triangle"></i>
            <div class="highlight-box-content">
              <strong>Important:</strong> It is compulsory for parents to see the answer paper of their wards on open
              day.
            </div>
          </div>

        </div>

      </div>
    </section>

  </asp:Content>