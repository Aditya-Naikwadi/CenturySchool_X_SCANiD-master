<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="CampusSafety.aspx.cs" Inherits="CenturyRayonSchool.CampusSafety" %>

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
                    <div class="infra-title">Campus Safety</div>
                    <p class="infra-subtitle">
                        Century Rayon High School has inculcated a systematic procedure for an orderly, clean, and
                        secured environment. We prioritize the mental and physical well-being of every student.
                    </p>
                </header>

                <!-- Safety Infrastructure -->
                <h2 class="safety-section-title animate-up delay-1">Secure Infrastructure</h2>
                <div class="safety-list-grid animate-up delay-1">
                    <div class="safety-item">
                        <i class="safety-icon fas fa-shield-alt"></i>
                        <div>
                            <strong>Boundary Walls</strong>
                            <p>8-feet high boundary walls reinforced with MS-Jallai fencing for enhanced perimeter
                                security.</p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-video"></i>
                        <div>
                            <strong>CCTV Surveillance</strong>
                            <p>Comprehensive camera coverage in classrooms, corridors, main gates, and parking areas.
                            </p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-user-secret"></i>
                        <div>
                            <strong>Security Personnel</strong>
                            <p>Efficient security guards appointed for 24/7 patrolling and campus monitoring.</p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-wheelchair"></i>
                        <div>
                            <strong>Accessibility</strong>
                            <p>Wheelchair ramps provided at entrances to ensure ease of access for everyone.</p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-window-maximize"></i>
                        <div>
                            <strong>Classroom Safety</strong>
                            <p>All classrooms are secured with window grills and rigorous safety checks.</p>
                        </div>
                    </div>
                </div>

                <!-- Emergency Prep -->
                <h2 class="safety-section-title animate-up delay-2">Emergency Preparedness</h2>
                <div class="safety-list-grid animate-up delay-2">
                    <div class="safety-item">
                        <i class="safety-icon fas fa-fire-extinguisher"></i>
                        <div>
                            <strong>Fire Safety</strong>
                            <p>Fire extinguishers and hydrants placed at strategic locations and maintained regularly.
                            </p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-people-arrows"></i>
                        <div>
                            <strong>Evacuation Plans</strong>
                            <p>Clear evacuation maps displayed throughout the building with regular mock drills for
                                students.</p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-bell"></i>
                        <div>
                            <strong>Alarm System</strong>
                            <p>Sound-modulated alarm system for different contingencies and emergencies.</p>
                        </div>
                    </div>
                    <div class="safety-item">
                        <i class="safety-icon fas fa-user-md"></i>
                        <div>
                            <strong>Health & First Aid</strong>
                            <p>Well-equipped sports room and laboratories to handle common injuries and emergencies.</p>
                        </div>
                    </div>
                </div>

                <!-- Visitor Management -->
                <h2 class="safety-section-title animate-up delay-3">Visitor Management</h2>
                <div class="feature-grid animate-up delay-3">
                    <div class="infra-card">
                        <div class="card-content" style="text-align: center;">
                            <i class="fas fa-id-card-alt"
                                style="font-size: 3rem; color: var(--primary-color); margin-bottom: 20px;"></i>
                            <h3 class="card-title">Digital Attendance</h3>
                            <p class="card-desc">Electronic ID scanners mark student attendance and instantly notify
                                parents via SMS.</p>
                        </div>
                    </div>
                    <div class="infra-card">
                        <div class="card-content" style="text-align: center;">
                            <i class="fas fa-user-check"
                                style="font-size: 3rem; color: var(--primary-color); margin-bottom: 20px;"></i>
                            <h3 class="card-title">Visitor Registry</h3>
                            <p class="card-desc">Strict visitor management system with mandatory registration and ID
                                verification for all entrants.</p>
                        </div>
                    </div>
                    <div class="infra-card">
                        <div class="card-content" style="text-align: center;">
                            <i class="fas fa-id-badge"
                                style="font-size: 3rem; color: var(--primary-color); margin-bottom: 20px;"></i>
                            <h3 class="card-title">Staff Identification</h3>
                            <p class="card-desc">All support staff, lunch providers, and drivers are issued verified
                                School Identity Cards.</p>
                        </div>
                    </div>
                </div>

                <!-- Workshops -->
                <div class="infra-card animate-up delay-3"
                    style="margin-top: 50px; border-left: 5px solid var(--secondary-color);">
                    <div class="card-content">
                        <h3 class="card-title"><i class="fas fa-chalkboard-teacher"
                                style="margin-right: 15px;"></i>Workshops & Seminars</h3>
                        <p class="card-desc">
                            We believe in proactive education. Right from Primary Section, children are taught about
                            "Good Touch and Bad Touch".
                            Regular self-defense workshops, first-aid training, and health checkups are organized for
                            students and staff.
                            Martial arts are included as a compulsory activity to empower our students physically and
                            mentally.
                        </p>
                    </div>
                </div>

            </div>
        </section>
    </asp:Content>