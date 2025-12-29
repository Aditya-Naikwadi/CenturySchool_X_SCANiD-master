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

        <!-- Trustees Section -->
        <div class="admin-card">
          <h2 class="admin-card-title">
            <i class="fas fa-shield-alt"></i>
            Board of Trustees
          </h2>

          <div class="admin-members-grid">
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-shield"></i>
              </div>
              <div class="member-name">Mr. O.R. Chitlange</div>
              <span class="member-role trustee">Trustee</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-shield"></i>
              </div>
              <div class="member-name">Mr. Subodh Dave</div>
              <span class="member-role trustee">Trustee</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-shield"></i>
              </div>
              <div class="member-name">Mr. Yogesh R. Shah</div>
              <span class="member-role trustee">Trustee</span>
            </div>
          </div>
        </div>

        <!-- School Managing Committee Section -->
        <div class="admin-card">
          <h2 class="admin-card-title">
            <i class="fas fa-users-cog"></i>
            School Managing Committee
          </h2>

          <div class="admin-members-grid">
            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-crown"></i>
              </div>
              <div class="member-name">Mr. Yogesh R. Shah</div>
              <span class="member-role chairman">Chairman</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-tie"></i>
              </div>
              <div class="member-name">Mr. Milind Bhandarkar</div>
              <span class="member-role vice-chairman">Vice-Chairman</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user-cog"></i>
              </div>
              <div class="member-name">Mr. Anil Sewaney</div>
              <span class="member-role administrator">Administrator</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-coins"></i>
              </div>
              <div class="member-name">Mr. Milind H. Patil</div>
              <span class="member-role">Treasurer</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. B.P. Karwa</div>
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
              <div class="member-name">Mr. Anand N. Thakur</div>
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
              <div class="member-name">Mr. Manjeetsingh Kocchar</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Leonardo D'Souza</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mrs. Ranjna Jhangra</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. R.B. Singh</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-user"></i>
              </div>
              <div class="member-name">Mr. Krishna Yadav</div>
              <span class="member-role">Member</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Mrs. Rachna Mathur</div>
              <span class="member-role">H.M. (Toddlers)</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Mrs. Ritu Bhagat</div>
              <span class="member-role">H.M. (Ex-Officio Secy.)</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Mrs. Babita Singh</div>
              <span class="member-role">H.M. Primary</span>
            </div>

            <div class="member-card">
              <div class="member-icon">
                <i class="fas fa-chalkboard-teacher"></i>
              </div>
              <div class="member-name">Mrs. Sarita Borkar</div>
              <span class="member-role">Asst. H.M Secondary</span>
            </div>
          </div>
        </div>

      </div>
    </section>

  </asp:Content>