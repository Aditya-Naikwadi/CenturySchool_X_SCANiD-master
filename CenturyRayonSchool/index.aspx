<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
  CodeBehind="index.aspx.cs" Inherits="CenturyRayonSchool.index" %>
  <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <head>

      <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
      <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
      <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
      <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.10.0/font/bootstrap-icons.css" rel="stylesheet">
      <link href="css/home-enhancements.css?v=2.0" rel="stylesheet">
      <link href="css/card-enhancements.css?v=1.0" rel="stylesheet">
      <link href="css/responsive-overrides.css?v=2.0" rel="stylesheet">
    </head>

    <style>
      :root {
        --maroon-primary: #7c3848;
        --maroon-medium: #a24c58;
        --maroon-dark: #9b3e3e;
        --bg-light: #f5f3ee;
        --warning-color: #ffc107;
        --school-maroon: #7c3848;
        --school-gold: #ffc107;
        --school-blue: #003366;
        --form-bg: #f9f6f2;
        --input-bg: #e6ede7;
      }

      .section-wrapper {
        padding: 4rem 0;
      }

      .section-wrapper:nth-child(odd) {
        background-color: var(--bg-light);
      }

      .section-wrapper:nth-child(even) {
        background-color: #fff;
      }

      .section-title {
        text-align: center;
        margin-bottom: 2rem;
      }

      .section-title h2 {
        color: var(--maroon-primary);
        font-weight: bold;
        font-size: 2.5rem;
        margin-bottom: 0.5rem;
      }

      .section-title p {
        color: #6c757d;
        font-size: 1.1rem;
      }

      .section-divider {
        width: 80px;
        height: 4px;
        background-color: var(--warning-color);
        margin: 0 auto 2.5rem;
        border-radius: 2px;
      }

      .announcement-card {
        border-radius: 15px;
        overflow: hidden;
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        height: 100%;
        border: none;
      }

      .announcement-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
      }

      .announcement-card.card-maroon-1 {
        background-color: var(--maroon-primary);
      }

      .announcement-card.card-maroon-2 {
        background-color: var(--maroon-medium);
      }

      .announcement-card.card-maroon-3 {
        background-color: var(--maroon-dark);
      }

      .announcement-card .card-body {
        color: white;
        padding: 2rem;
      }

      .announcement-card h4 {
        color: var(--warning-color);
        font-weight: bold;
        margin-bottom: 1.5rem;
      }

      .announcement-card a {
        color: var(--warning-color);
        text-decoration: none;
      }

      .announcement-card a:hover {
        text-decoration: underline;
      }

      .scrolling-content {
        height: 220px;
        overflow: hidden;
      }

      .scrolling-content:hover {
        animation-play-state: paused;
      }

      @keyframes scroll-up {
        0% {
          transform: translateY(0);
        }

        100% {
          transform: translateY(-50%);
        }
      }

      .event-card,
      .notice-card {
        border-radius: 15px;
        overflow: hidden;
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        height: 100%;
        border: none;
      }

      .event-card:hover,
      .notice-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
      }

      .event-card {
        background: white;
        border: 2px solid var(--maroon-primary);
      }

      .event-card .card-body {
        padding: 1.5rem;
      }

      .event-card h5 {
        color: var(--maroon-primary);
        font-weight: bold;
        margin-bottom: 0.75rem;
      }

      .event-date {
        background-color: var(--maroon-primary);
        color: white;
        padding: 0.5rem 1rem;
        border-radius: 8px;
        display: inline-block;
        margin-bottom: 1rem;
        font-weight: bold;
      }

      .notice-card.card-maroon-1 {
        background-color: var(--maroon-primary);
      }

      .notice-card.card-maroon-2 {
        background-color: var(--maroon-medium);
      }

      .notice-card.card-maroon-3 {
        background-color: var(--maroon-dark);
      }

      .notice-card .card-body {
        color: white;
      }

      .notice-card h5 {
        color: var(--warning-color);
        font-weight: bold;
      }

      .notice-card hr {
        border-color: var(--warning-color);
        opacity: 1;
      }

      .notice-card img {
        height: 200px;
        object-fit: cover;
      }

      .gallery-carousel {
        border-radius: 15px;
        overflow: hidden;
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
      }

      .gallery-carousel img {
        height: 450px;
        object-fit: contain;

      }

      #Glrimg {
        padding: 5px !important;
        border-radius: 10px !important;
      }

      .carousel-control-prev-icon,
      .carousel-control-next-icon {
        filter: invert(1) drop-shadow(0 0 5px rgba(0, 0, 0, 0.5));
      }

      .social-float {
        position: fixed;
        top: 50%;
        right: 15px;
        transform: translateY(-50%);
        display: flex;
        flex-direction: column;
        gap: 12px;
        z-index: 9999;
      }

      .social-float a {
        width: 42px;
        height: 42px;
        display: flex;
        align-items: center;
        justify-content: center;
        background: #7c3848;
        /* school theme */
        color: #fff;
        border-radius: 50%;
        font-size: 20px;
        text-decoration: none;
        transition: all 0.3s ease;
        box-shadow: 0 3px 8px rgba(0, 0, 0, 0.2);
      }

      .social-float a:hover {
        transform: scale(1.1);
        color: #fff;
      }

      .facebook {
        background: #3b5998;
      }

      .instagram {
        background: radial-gradient(circle at 30% 107%, #fdf497 0%, #fdf497 5%, #fd5949 45%, #d6249f 60%, #285AEB 90%);
      }

      .whatsapp {
        background: #25D366;
      }


      /* ==== FORM STYLES ==== */
      .modal-content {
        border-radius: 20px;
        background-color: var(--form-bg);
        box-shadow: 0 6px 25px rgba(0, 0, 0, 0.25);
        overflow: hidden;
        border: none;
      }

      .modal-header {
        background-color: var(--school-gold);
        border-bottom: none;
        text-align: center;
      }

      .modal-title {
        color: var(--school-maroon);
        font-weight: 700;
        font-size: 1.25rem;
        text-transform: uppercase;
        width: 100%;
      }

      .btn-close {
        background-color: #fff !important;
        opacity: 1;
        border-radius: 50%;
        box-shadow: 0 0 3px rgba(0, 0, 0, 0.2);
      }

      .form-field {
        background-color: var(--input-bg);
        border: none;
        border-radius: 8px;
        padding: 10px 12px;
        font-size: 15px;
        color: #333;
        transition: all 0.3s ease;
      }

      .form-field:focus {
        outline: none;
        box-shadow: 0 0 5px var(--school-maroon);
      }

      .btn-submit {
        background-color: var(--school-maroon);
        color: #fff;
        font-weight: 600;
        border-radius: 50px;
        padding: 10px 35px;
        transition: 0.3s ease;
      }

      .btn-submit:hover {
        background-color: var(--school-gold);
        color: var(--school-maroon);
      }

      .bi-cake2 {
        animation: glowCake 1.4s ease-in-out infinite alternate;
      }

      @keyframes glowCake {
        from {
          text-shadow: 0 0 5px #ffc107;
        }

        to {
          text-shadow: 0 0 15px #ffe066;
        }
      }

      /* ==== SCROLL ANIMATIONS ==== */
      /* Disabled for now - was causing content to be invisible 
      .announcement-card,
      .event-card,
      .notice-card {
        opacity: 0;
        transform: translateY(30px);
        transition: opacity 0.6s ease, transform 0.6s ease;
      }

      .announcement-card.animate-in,
      .event-card.animate-in,
      .notice-card.animate-in {
        opacity: 1;
        transform: translateY(0);
      }
      */

      /* ==== MOBILE SOCIAL FOOTER ==== */
      .mobile-social-footer {
        position: fixed;
        bottom: 0;
        left: 0;
        width: 100%;
        display: none;
        /* Hidden by default, shown via JS on mobile */
        justify-content: space-around;
        background: rgba(124, 56, 72, 0.95);
        backdrop-filter: blur(10px);
        padding: 12px 0;
        box-shadow: 0 -3px 15px rgba(0, 0, 0, 0.2);
        z-index: 9999;
      }

      .mobile-social-footer a {
        width: 45px;
        height: 45px;
        display: flex;
        align-items: center;
        justify-content: center;
        color: #fff;
        border-radius: 50%;
        font-size: 22px;
        text-decoration: none;
        transition: all 0.3s ease;
      }

      .mobile-social-footer a:hover {
        transform: scale(1.1);
      }

      /* ==== FORM VALIDATION STYLES ==== */
      .form-control.is-invalid {
        border: 2px solid #dc3545 !important;
        box-shadow: 0 0 8px rgba(220, 53, 69, 0.3) !important;
      }

      .form-control.is-valid {
        border: 2px solid #28a745 !important;
        box-shadow: 0 0 8px rgba(40, 167, 69, 0.3) !important;
      }

      /* ==== RESPONSIVE SOCIAL BARS ==== */
      @media (max-width: 768px) {
        .social-float {
          display: none !important;
        }

        .mobile-social-footer {
          display: flex !important;
        }
      }
    </style>
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- ========== HERO CAROUSEL SECTION ========== -->
    <!-- ========== HERO CAROUSEL SECTION ========== -->
    <section class="section-wrapper" style="padding: 0;">
      <div class="tp-banner-container">
        <div id="heroCarousel" class="carousel slide" data-bs-ride="carousel">
          <div class="carousel-inner">
            <div class="carousel-item active">
              <img src="img/Carosyle/1.jpg" class="d-block w-100" alt="School Banner 1"
                style="height: 630px; object-fit: cover;">
            </div>
            <div class="carousel-item">
              <img src="img/Carosyle/2.jpg" class="d-block w-100" alt="School Banner 2"
                style="height: 630px; object-fit: cover;">
            </div>
            <div class="carousel-item">
              <img src="img/Carosyle/3.jpeg" class="d-block w-100" alt="School Banner 3"
                style="height: 630px; object-fit: cover;">
            </div>
            <div class="carousel-item">
              <img src="img/Carosyle/4.jpeg" class="d-block w-100" alt="School Banner 4"
                style="height: 630px; object-fit: cover;">
            </div>
            <div class="carousel-item">
              <img src="img/Carosyle/5.jpeg" class="d-block w-100" alt="School Banner 5"
                style="height: 630px; object-fit: cover;">
            </div>
            <div class="carousel-item">
              <img src="img/Carosyle/6.jpg" class="d-block w-100" alt="School Banner 6"
                style="height: 630px; object-fit: cover;">
            </div>
          </div>

          <!-- Navigation Buttons -->
          <button class="carousel-control-prev" type="button" data-bs-target="#heroCarousel" data-bs-slide="prev">
            <span class="carousel-control-prev-icon"></span>
            <span class="visually-hidden">Previous</span>
          </button>
          <button class="carousel-control-next" type="button" data-bs-target="#heroCarousel" data-bs-slide="next">
            <span class="carousel-control-next-icon"></span>
            <span class="visually-hidden">Next</span>
          </button>
        </div>
      </div>
    </section>


    <!-- ========== ANNOUNCEMENTS & UPDATES SECTION (Final Polished Version) ========== -->
    <section class="section-wrapper py-5" style="background-color:#f8f5f1;">
      <div class="container">
        <div class="text-center mb-5">
          <h2 class="fw-bold" style="color:#7c3848;">Announcements & Updates</h2>
          <p class="text-muted">Stay connected with daily updates and important information</p>
          <div class="section-divider mx-auto" style="width:80px; height:4px; background:#ffc107; border-radius:2px;">
          </div>
        </div>

        <div class="row g-4">

          <!-- 🎂 Today's Birthdays (Dynamic + Cake Icons) -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow-lg border-0 h-100 announcement-card"
              style="background:#7c3848; border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white py-3" style="background:#a24c58; font-weight:600;">
                <i class="bi bi-cake2-fill me-2 text-warning" style="font-size:1.7rem;"></i> Today's Birthdays
              </div>
              <div class="card-body text-white">
                <div class="scrolling-content" style="height:220px; overflow:hidden;">
                  <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();">

                    <!-- 🧒 Students -->
                    <h6 class="text-warning mb-1">
                      <i class="bi bi-person-badge me-1"></i> Students
                    </h6>
                    <ul class="list-unstyled text-start ms-2">
                      <asp:Literal ID="litStudentBirthday" runat="server"></asp:Literal>
                    </ul>

                    <!-- 👩‍🏫 Staff -->
                    <h6 class="text-warning mt-3 mb-1">
                      <i class="bi bi-mortarboard-fill me-1"></i> Teachers
                    </h6>
                    <ul class="list-unstyled text-start ms-2">
                      <asp:Literal ID="litStaffBirthday" runat="server"></asp:Literal>
                    </ul>

                  </marquee>
                </div>
              </div>
            </div>
          </div>

          <!-- 📢 Notices -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow-lg border-0 h-100 announcement-card"
              style="background:#a24c58; border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white py-3" style="background:#7c3848; font-weight:600;">
                <i class="bi bi-exclamation-triangle-fill me-2 text-warning" style="font-size:1.6rem;"></i>
                <a href="News.aspx" class="text-white text-decoration-none">Important Notices</a>
              </div>
              <div class="card-body text-white">
                <div class="scrolling-content" style="height:220px; overflow:hidden;">
                  <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();">
                    <asp:ListView ID="ListViewNews" runat="server">
                      <LayoutTemplate>
                        <ul class="list-unstyled ms-2">
                          <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </ul>
                      </LayoutTemplate>
                      <ItemTemplate>
                        <li class="mb-2 d-flex align-items-start">
                          <i class="bi bi-bell-fill text-warning me-2"></i>
                          <a href='NewsDescripition.aspx?id=<%# Eval("id") %>' target="_blank"
                            class="text-white text-decoration-none">
                            <%# Eval("TopicName") %>
                          </a>
                        </li>
                      </ItemTemplate>
                      <EmptyDataTemplate>
                        <p class="text-light">No notices found</p>
                      </EmptyDataTemplate>
                    </asp:ListView>
                  </marquee>
                </div>
              </div>
            </div>
          </div>

          <!-- 🎊 Events -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow-lg border-0 h-100 announcement-card"
              style="background:#9b3e3e; border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white py-3" style="background:#7c3848; font-weight:600;">
                <i class="bi bi-balloon-fill text-warning me-2" style="font-size:1.6rem;"></i>
                <a href="Event.aspx" class="text-white text-decoration-none">Upcoming Events</a>
              </div>
              <div class="card-body text-white">
                <div class="scrolling-content" style="height:220px; overflow:hidden;">
                  <marquee direction="up" scrollamount="3" onmouseover="this.stop();" onmouseout="this.start();">
                    <asp:ListView ID="ListViewEvent" runat="server">
                      <LayoutTemplate>
                        <ul class="list-unstyled ms-2">
                          <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </ul>
                      </LayoutTemplate>
                      <ItemTemplate>
                        <li class="mb-2 d-flex align-items-start">
                          <i class="bi bi-calendar2-event-fill text-warning me-2"></i>
                          <a href='EventDescripition.aspx?id=<%# Eval("id") %>' target="_blank"
                            class="text-white text-decoration-none">
                            <%# Eval("eventName") %>
                          </a>
                        </li>
                      </ItemTemplate>
                      <EmptyDataTemplate>
                        <p class="text-light">No events available</p>
                      </EmptyDataTemplate>
                    </asp:ListView>
                  </marquee>
                </div>
              </div>
            </div>
          </div>

        </div>
      </div>
    </section>

    <!-- ========== UPCOMING EVENTS SECTION (Static Data Example) ========== -->
    <section class="section-wrapper" style="background-color:#f5f3ee;">
      <div class="container">
        <div class="text-center mb-4">
          <h2 class="fw-bold" style="color:#7c3848;">Upcoming Events</h2>
          <p class="text-muted">Don't miss our exciting upcoming activities and celebrations</p>
          <div class="section-divider"
            style="width:80px;height:4px;background-color:#ffc107;margin:0 auto 2rem;border-radius:3px;"></div>
        </div>

        <div class="row g-4 justify-content-center">
          <!-- Event 1 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 event-card" style="border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white"
                style="background-color:#7c3848; border:none; padding:1.2rem 0;">
                <div class="fw-bold" style="font-size:2rem;">28</div>
                <div style="text-transform:uppercase; font-size:1rem;">MAY</div>
              </div>
              <div class="card-body text-center p-4" style="background-color:white;">
                <h5 class="fw-bold mb-3" style="color:#7c3848;">SSC Result 2023–2024</h5>
                <p class="text-muted mb-2">
                  <i class="bi bi-geo-alt-fill text-warning"></i> Century Rayon High School, Shahad
                </p>
                <p class="text-muted mb-3">
                  <i class="bi bi-clock text-warning"></i> 10:00 AM – 12:00 PM
                </p>
                <p class="text-secondary">Students can check their SSC results on the school notice board or website.
                </p>
                <a href="#" class="btn btn-sm mt-2"
                  style="background-color:#a24c58; color:white; border-radius:20px;">View Details</a>
              </div>
            </div>
          </div>

          <!-- Event 2 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 event-card" style="border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white"
                style="background-color:#7c3848; border:none; padding:1.2rem 0;">
                <div class="fw-bold" style="font-size:2rem;">12</div>
                <div style="text-transform:uppercase; font-size:1rem;">JUN</div>
              </div>
              <div class="card-body text-center p-4" style="background-color:white;">
                <h5 class="fw-bold mb-3" style="color:#7c3848;">Annual Science Exhibition</h5>
                <p class="text-muted mb-2">
                  <i class="bi bi-geo-alt-fill text-warning"></i> School Auditorium
                </p>
                <p class="text-muted mb-3">
                  <i class="bi bi-clock text-warning"></i> 09:30 AM – 02:00 PM
                </p>
                <p class="text-secondary">Explore creative science models and projects made by our talented students.
                </p>
                <a href="#" class="btn btn-sm mt-2"
                  style="background-color:#a24c58; color:white; border-radius:20px;">View Details</a>
              </div>
            </div>
          </div>

          <!-- Event 3 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 event-card" style="border-radius:15px; overflow:hidden;">
              <div class="card-header text-center text-white"
                style="background-color:#7c3848; border:none; padding:1.2rem 0;">
                <div class="fw-bold" style="font-size:2rem;">25</div>
                <div style="text-transform:uppercase; font-size:1rem;">JUL</div>
              </div>
              <div class="card-body text-center p-4" style="background-color:white;">
                <h5 class="fw-bold mb-3" style="color:#7c3848;">Cultural Day Celebration</h5>
                <p class="text-muted mb-2">
                  <i class="bi bi-geo-alt-fill text-warning"></i> Century Rayon School Grounds
                </p>
                <p class="text-muted mb-3">
                  <i class="bi bi-clock text-warning"></i> 03:00 PM – 06:00 PM
                </p>
                <p class="text-secondary">An evening filled with music, dance, and performances celebrating diversity.
                </p>
                <a href="#" class="btn btn-sm mt-2"
                  style="background-color:#a24c58; color:white; border-radius:20px;">View Details</a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- ========== IMPORTANT NOTICES SECTION ========== -->
    <section class="section-wrapper" style="background-color:#fff;">
      <div class="container">
        <div class="text-center mb-4">
          <h2 class="fw-bold" style="color:#7c3848;">Important Notices</h2>
          <p class="text-muted">Stay informed about our latest announcements and updates</p>
          <div class="section-divider"></div>
        </div>

        <div class="row g-4">
          <!-- Notice Card 1 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 notice-card"
              style="background-color:#7c3848; color:white; border-radius:15px; overflow:hidden;">
              <img src="img/News/ganpati.png" class="card-img-top" alt="Holiday Notice"
                style="height:340px; object-fit:cover;">
              <div class="card-body">
                <h5 class="text-warning fw-bold mb-2">Holiday On Ganesh Visarjan</h5>
                <hr class="text-warning" />
                <p style="color: white !important">Dear Parents, there will be a Holiday on 10th Sept 2025 on account of
                  Ganesh Visarjan. School will resume on 11th at the usual time.</p>
              </div>
            </div>
          </div>

          <!-- Notice Card 2 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 notice-card"
              style="background-color:#a24c58; color:white; border-radius:15px; overflow:hidden;">
              <img src="img/News/Exam.png" class="card-img-top" alt="Unit Test Notice"
                style="height:340px; object-fit:cover;">
              <div class="card-body">
                <h5 class="text-warning fw-bold mb-2">Unit Test Schedule</h5>
                <hr class="text-warning" />
                <p style="color: white !important">Unit Test for Primary Section (Std 1 to 4) will begin from
                  01/10/2025. The detailed timetable will be available on the school website soon.</p>
              </div>
            </div>
          </div>

          <!-- Notice Card 3 -->
          <div class="col-lg-4 col-md-6">
            <div class="card shadow border-0 h-100 notice-card"
              style="background-color:#9b3e3e; color:white; border-radius:15px; overflow:hidden;">
              <img src="img/News/ParentTeacher.png" class="card-img-top" alt="Parent Meeting Notice"
                style="height:340px; object-fit:cover;">
              <div class="card-body">
                <h5 class="text-warning fw-bold mb-2">Parent-Teacher Meeting</h5>
                <hr class="text-warning" />
                <p style="color: white !important">Dear Parents, a Parent-Teacher Meeting will be held on 25/10/2025 at
                  10:00 AM for Std VIII in their respective classrooms.</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- ========== PHOTO GALLERY SECTION (Final Refined Design) ========== -->


    <!-- Gallery Carousel -->
    <!-- ========== PHOTO GALLERY SECTION (2 Images per Slide) ========== -->
    <section class="section-wrapper" style="background-color:#fff; padding:60px 0;">
      <div class="container">
        <!-- Section Header -->
        <div class="text-center mb-5">
          <h2 class="fw-bold mb-2" style="color:#7c3848;">Photo Gallery</h2>
          <p class="text-muted">Cherished moments from our school activities</p>
          <div class="d-flex justify-content-center align-items-center mt-2">
            <div style="width:80px;height:4px;background-color:#ffc107;border-radius:3px;"></div>
          </div>
        </div>

        <!-- Gallery Carousel -->
        <div id="carouselGallery" class="carousel slide shadow-lg rounded-4 overflow-hidden" data-bs-ride="carousel"
          data-bs-interval="4000">

          <div class="carousel-inner">
            <!-- Slide 1 -->
            <div class="carousel-item active">
              <div class="d-flex">
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/1.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Science Exhibition</h6>
                    </div>
                  </div>
                </div>
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/2.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Science Exhibition</h6>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Slide 2 -->
            <div class="carousel-item">
              <div class="d-flex">
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/3.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Raksha Bandhan</h6>
                    </div>
                  </div>
                </div>
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/4.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Cultural Dance Program</h6>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Slide 3 -->
            <div class="carousel-item">
              <div class="d-flex">
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/5.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Annual Function</h6>
                    </div>
                  </div>
                </div>
                <div class="w-50 position-relative">
                  <img src="img/index_gallery/6.jpg" class="d-block w-100" style="height:400px; object-fit:cover;"
                    id="Glrimg">
                  <div class="carousel-caption d-none d-md-block">
                    <div class="bg-dark bg-opacity-50 rounded px-3 py-2 d-inline-block">
                      <h6 class="text-warning mb-0">Annual Function</h6>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Controls -->
          <button class="carousel-control-prev" type="button" data-bs-target="#carouselGallery" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" style="filter: invert(1) drop-shadow(0 0 5px #7c3848);"
              aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
          </button>
          <button class="carousel-control-next" type="button" data-bs-target="#carouselGallery" data-bs-slide="next">
            <span class="carousel-control-next-icon" style="filter: invert(1) drop-shadow(0 0 5px #7c3848);"
              aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
          </button>

          <!-- Indicators -->
          <div class="carousel-indicators mt-3">
            <button type="button" data-bs-target="#carouselGallery" data-bs-slide-to="0"
              class="active bg-warning"></button>
            <button type="button" data-bs-target="#carouselGallery" data-bs-slide-to="1" class="bg-warning"></button>
            <button type="button" data-bs-target="#carouselGallery" data-bs-slide-to="2" class="bg-warning"></button>
          </div>
        </div>
      </div>
    </section>

    <div class="modal fade" id="admissionFormPopup" tabindex="-1" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">

          <!-- Header -->
          <div class="modal-header">
            <h5 class="modal-title">Admission Enquiry Form</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
          </div>

          <!-- Body -->
          <div class="modal-body px-4 py-3">
            <form id="admissionForm">
              <div class="row g-3">
                <div class="col-md-6">
                  <input type="text" class="form-control form-field" placeholder="Child’s Name" required />
                </div>
                <div class="col-md-6">
                  <input type="date" class="form-control form-field" placeholder="Date of Birth" required />
                </div>

                <div class="col-md-6">
                  <input type="text" class="form-control form-field" placeholder="Father’s Name" required />
                </div>
                <div class="col-md-6">
                  <input type="text" class="form-control form-field" placeholder="Mother’s Name" required />
                </div>

                <div class="col-md-6">
                  <input type="text" class="form-control form-field" placeholder="Father's Phone" required />
                </div>
                <div class="col-md-6">
                  <input type="text" class="form-control form-field" placeholder="Mother's Phone" required />
                </div>

                <div class="col-12">
                  <input type="email" class="form-control form-field" placeholder="Email Id" required />
                </div>

                <div class="col-12">
                  <input type="text" class="form-control form-field" placeholder="Seeking Admission in Grade"
                    required />
                </div>

                <div class="col-12 text-center">
                  <button type="submit" class="btn btn-submit mt-2">Submit</button>
                </div>
              </div>
            </form>
          </div>

        </div>
      </div>
    </div>

    <div class="social-float">
      <a href="https://facebook.com" target="_blank" class="facebook">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="https://instagram.com" target="_blank" class="instagram">
        <i class="fab fa-instagram"></i>
      </a>
      <a href="https://wa.me/919920878141" target="_blank" class="whatsapp">
        <i class="fab fa-whatsapp"></i>
      </a>
    </div>

    <!-- Mobile Social Footer (only visible on mobile) -->
    <div class="mobile-social-footer" style="display: none;">
      <a href="https://facebook.com" target="_blank" class="facebook" style="background: #3b5998;">
        <i class="fab fa-facebook-f"></i>
      </a>
      <a href="https://instagram.com" target="_blank" class="instagram"
        style="background: radial-gradient(circle at 30% 107%, #fdf497 0%, #fdf497 5%, #fd5949 45%, #d6249f 60%, #285AEB 90%);">
        <i class="fab fa-instagram"></i>
      </a>
      <a href="https://wa.me/919920878141" target="_blank" class="whatsapp" style="background: #25D366;">
        <i class="fab fa-whatsapp"></i>
      </a>
    </div>



    <script src="js/jssor.slider-28.1.0.min.js" type="text/javascript"></script>
    <!-- Modal and carousel initialization now handled in home-animations.js v2.0 -->



    <script src="js/home-animations.js?v=2.0"></script>

  </asp:Content>