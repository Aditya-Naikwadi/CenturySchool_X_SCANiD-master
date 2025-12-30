<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeBehind="Management.aspx.cs" Inherits="CenturyRayonSchool.Management" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Modern CSS -->
    <link href="css/admin-modern.css" rel="stylesheet" />
    <link href="css/fontawesome-all.css" rel="stylesheet" />
  </asp:Content>

  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="admin-modern-section">
      <!-- Hero Header -->
      <div class="admin-hero-header">
        <h1 class="admin-hero-title">Management</h1>
        <p class="admin-hero-subtitle">Our Dedicated Leadership Team</p>
      </div>

      <div class="admin-container">

        <!-- School Managing Committee Section -->
        <div class="admin-card">
          <h2 class="admin-card-title">
            <i class="fas fa-users-cog"></i>
            School Managing Committee
          </h2>

          <div class="admin-members-grid">
            <!-- Chairman -->
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-crown"></i>
              </div>
              <div class="member-name">Mr. O. R. Chitlange</div>
              <span class="member-role chairman">Chairman</span>
            </div>

            <!-- Vice Chairman -->
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-tie"></i>
              </div>
              <div class="member-name">Mr. Digvijay Pandey</div>
              <span class="member-role vice-chairman">Vice Chairman</span>
            </div>

            <!-- Members -->
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Milind Bhandarkar</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Shrikant Gore</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Ms. Shilpa Shah</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Ajit Patil</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Sameer Saini</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Sudhakar Musale</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-md"></i>
              </div>
              <div class="member-name">Dr. Naresh Chandra</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-md"></i>
              </div>
              <div class="member-name">Dr. Avinash Patil</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Ms. Ranjana Jangra</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Ms. Esmita Gupta</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Milind Patil</div>
              <span class="member-role">Member</span>
            </div>

            <!-- Head Mistresses -->
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Ms. Vandana Bhadane</div>
              <span class="member-role">I/C HM Secondary</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Ms. Babita Singh</div>
              <span class="member-role">HM Primary</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Ms. Rachna Mathur</div>
              <span class="member-role">HM Toddlers</span>
            </div>

            <!-- Administrative -->
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-cog"></i>
              </div>
              <div class="member-name">Mr. Prakash Panchal</div>
              <span class="member-role administrator">Administrative</span>
            </div>
          </div>
        </div>

        <!-- Special Invitees Section -->
        <div class="admin-card">
          <h2 class="admin-card-title">
            <i class="fas fa-user-friends"></i>
            Special Invitees
          </h2>

          <div class="admin-members-grid">
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-star"></i>
              </div>
              <div class="member-name">Mr. Harish Dubey</div>
              <span class="member-role">Special Invitee</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-star"></i>
              </div>
              <div class="member-name">Mr. Anil Sahal</div>
              <span class="member-role">Special Invitee</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-star"></i>
              </div>
              <div class="member-name">Ms. Ruchita Chauhan</div>
              <span class="member-role">Special Invitee</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-star"></i>
              </div>
              <div class="member-name">Mr. Govind Jha</div>
              <span class="member-role">Special Invitee</span>
            </div>
          </div>
        </div>

      </div>
    </section>

  </asp:Content>