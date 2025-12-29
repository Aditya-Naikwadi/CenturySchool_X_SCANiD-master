<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="SchoolFaculty.aspx.cs" Inherits="CenturyRayonSchool.SchoolFaculty" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link
            href="https://fonts.googleapis.com/css2?family=Playfair+Display:wght@600;700;800&family=Raleway:wght@400;500;600;700&family=Poppins:wght@300;400;500;600&display=swap"
            rel="stylesheet">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">

        <style>
            :root {
                --maroon-primary: #7c3848;
                --maroon-light: #a24c58;
                --maroon-dark: #5a2a34;
                --gold: #ffc107;
                --gold-light: #ffd54f;
                --text-dark: #2c3e50;
                --text-light: #6c757d;
                --bg-light: #f5f3ee;
            }

            * {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
            }

            body {
                font-family: 'Poppins', sans-serif;
                background: linear-gradient(135deg, #f5f3ee 0%, #e8e4db 100%);
                overflow-x: hidden;
            }

            /* Hero Section */
            .faculty-hero {
                background: linear-gradient(135deg, var(--maroon-primary) 0%, var(--maroon-dark) 100%);
                padding: 100px 0 150px;
                margin-top: 70px;
                position: relative;
                overflow: hidden;
            }

            .faculty-hero::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background:
                    radial-gradient(circle at 25% 35%, rgba(255, 193, 7, 0.15) 0%, transparent 50%),
                    radial-gradient(circle at 75% 65%, rgba(255, 255, 255, 0.1) 0%, transparent 50%);
            }

            .faculty-hero::after {
                content: '';
                position: absolute;
                bottom: -2px;
                left: 0;
                right: 0;
                height: 120px;
                background: linear-gradient(to top, #f5f3ee, transparent);
            }

            .hero-content {
                position: relative;
                z-index: 2;
                text-align: center;
            }

            .hero-title {
                font-family: 'Playfair Display', serif;
                font-size: 4rem;
                font-weight: 800;
                color: white;
                margin-bottom: 1.5rem;
                text-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
                animation: fadeInDown 0.8s ease-out;
            }

            .hero-subtitle {
                font-family: 'Raleway', sans-serif;
                font-size: 1.4rem;
                color: rgba(255, 255, 255, 0.95);
                font-weight: 300;
                animation: fadeInUp 0.8s ease-out 0.2s both;
            }

            /* Coming Soon Section */
            .coming-soon-section {
                padding: 0px 0 100px;
                margin-top: -100px;
                position: relative;
                z-index: 10;
            }

            .coming-soon-card {
                background: white;
                border-radius: 30px;
                padding: 80px 60px;
                box-shadow: 0 25px 80px rgba(0, 0, 0, 0.15);
                text-align: center;
                position: relative;
                overflow: hidden;
                opacity: 0;
                transform: translateY(30px);
                animation: fadeInUp 0.8s ease-out 0.4s forwards;
            }

            .coming-soon-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 8px;
                background: linear-gradient(90deg, var(--maroon-primary), var(--gold), var(--maroon-light));
                background-size: 200% 100%;
                animation: gradientShift 3s ease-in-out infinite;
            }

            /* Icon with gears */
            .coming-soon-icon {
                font-size: 8rem;
                background: linear-gradient(135deg, var(--maroon-primary), var(--gold));
                -webkit-background-clip: text;
                -webkit-text-fill-color: transparent;
                background-clip: text;
                margin-bottom: 30px;
                animation: pulse 2s ease-in-out infinite;
            }

            .gears-container {
                position: relative;
                height: 150px;
                margin-bottom: 40px;
            }

            .gear {
                position: absolute;
                color: var(--maroon-light);
                opacity: 0.15;
            }

            .gear-1 {
                font-size: 5rem;
                left: 30%;
                top: 50%;
                transform: translate(-50%, -50%);
                animation: rotate-slow 20s linear infinite;
            }

            .gear-2 {
                font-size: 8rem;
                left: 50%;
                top: 50%;
                transform: translate(-50%, -50%);
                animation: rotate-reverse 15s linear infinite;
            }

            .gear-3 {
                font-size: 4rem;
                left: 70%;
                top: 50%;
                transform: translate(-50%, -50%);
                animation: rotate-slow 18s linear infinite;
            }

            /* Coming Soon Title */
            .coming-soon-title {
                font-family: 'Playfair Display', serif;
                font-size: 3.5rem;
                font-weight: 800;
                color: var(--maroon-primary);
                margin-bottom: 20px;
                position: relative;
                z-index: 2;
            }

            .coming-soon-subtitle {
                font-family: 'Raleway', sans-serif;
                font-size: 1.3rem;
                color: var(--text-light);
                margin-bottom: 40px;
                font-weight: 400;
            }

            /* Description Box */
            .description-box {
                background: linear-gradient(135deg, #f8f9fa 0%, #ffffff 100%);
                padding: 40px;
                border-radius: 20px;
                border-left: 5px solid var(--gold);
                margin: 40px auto 0;
                max-width: 700px;
            }

            .description-text {
                font-size: 1.1rem;
                line-height: 1.8;
                color: var(--text-dark);
                margin-bottom: 0;
            }

            /* Features List */
            .features-list {
                display: grid;
                grid-template-columns: repeat(3, 1fr);
                gap: 30px;
                margin-top: 50px;
            }

            .feature-item {
                background: linear-gradient(135deg, #f8f9fa, #ffffff);
                padding: 30px;
                border-radius: 15px;
                text-align: center;
                transition: all 0.3s ease;
            }

            .feature-item:hover {
                transform: translateY(-5px);
                box-shadow: 0 15px 40px rgba(124, 56, 72, 0.15);
            }

            .feature-icon {
                width: 60px;
                height: 60px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 50%;
                display: flex;
                align-items: center;
                justify-content: center;
                margin: 0 auto 20px;
            }

            .feature-icon i {
                font-size: 1.8rem;
                color: white;
            }

            .feature-title {
                font-family: 'Raleway', sans-serif;
                font-size: 1.1rem;
                font-weight: 600;
                color: var(--maroon-primary);
                margin: 0;
            }

            /* Animations */
            @keyframes fadeInDown {
                from {
                    opacity: 0;
                    transform: translateY(-40px);
                }

                to {
                    opacity: 1;
                    transform: translateY(0);
                }
            }

            @keyframes fadeInUp {
                from {
                    opacity: 0;
                    transform: translateY(40px);
                }

                to {
                    opacity: 1;
                    transform: translateY(0);
                }
            }

            @keyframes pulse {

                0%,
                100% {
                    transform: scale(1);
                }

                50% {
                    transform: scale(1.05);
                }
            }

            @keyframes rotate-slow {
                from {
                    transform: translate(-50%, -50%) rotate(0deg);
                }

                to {
                    transform: translate(-50%, -50%) rotate(360deg);
                }
            }

            @keyframes rotate-reverse {
                from {
                    transform: translate(-50%, -50%) rotate(360deg);
                }

                to {
                    transform: translate(-50%, -50%) rotate(0deg);
                }
            }

            @keyframes gradientShift {

                0%,
                100% {
                    background-position: 0% 50%;
                }

                50% {
                    background-position: 100% 50%;
                }
            }

            /* Responsive Design */
            @media (max-width: 1024px) {
                .features-list {
                    grid-template-columns: repeat(2, 1fr);
                }
            }

            @media (max-width: 768px) {
                .hero-title {
                    font-size: 2.8rem;
                }

                .hero-subtitle {
                    font-size: 1.1rem;
                }

                .faculty-hero {
                    padding: 80px 0 100px;
                }

                .coming-soon-card {
                    padding: 50px 35px;
                }

                .coming-soon-title {
                    font-size: 2.5rem;
                }

                .features-list {
                    grid-template-columns: 1fr;
                    gap: 20px;
                }

                .gears-container {
                    height: 100px;
                }

                .gear-1,
                .gear-3 {
                    font-size: 3rem;
                }

                .gear-2 {
                    font-size: 5rem;
                }
            }

            @media (max-width: 576px) {
                .hero-title {
                    font-size: 2.2rem;
                }

                .coming-soon-card {
                    padding: 40px 25px;
                }

                .coming-soon-title {
                    font-size: 2rem;
                }

                .description-box {
                    padding: 30px 25px;
                }
            }

            html {
                scroll-behavior: smooth;
            }

            .main-content {
                position: relative;
                z-index: 1;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Hero Section -->
        <section class="faculty-hero">
            <div class="container">
                <div class="hero-content">
                    <h1 class="hero-title">School Faculty</h1>
                    <p class="hero-subtitle">Meet Our Dedicated Team of Educators</p>
                </div>
            </div>
        </section>

        <!-- Coming Soon Section -->
        <section class="coming-soon-section main-content">
            <div class="container">

                <div class="coming-soon-card">

                    <!-- Animated Gears -->
                    <div class="gears-container">
                        <i class="fas fa-cog gear gear-1"></i>
                        <i class="fas fa-cog gear gear-2"></i>
                        <i class="fas fa-cog gear gear-3"></i>
                    </div>

                    <!-- Coming Soon Title -->
                    <h2 class="coming-soon-title">Coming Soon</h2>
                    <p class="coming-soon-subtitle">We're Building Something Great</p>

                    <!-- Description -->
                    <div class="description-box">
                        <p class="description-text">
                            We're currently working on creating an impressive faculty showcase page to introduce you to
                            our dedicated team of educators. Soon, you'll be able to explore detailed profiles of our
                            experienced teachers who are committed to providing quality education and shaping bright
                            futures.
                        </p>
                    </div>

                    <!-- Features Preview -->
                    <div class="features-list">
                        <div class="feature-item">
                            <div class="feature-icon">
                                <i class="fas fa-users"></i>
                            </div>
                            <p class="feature-title">Faculty Profiles</p>
                        </div>

                        <div class="feature-item">
                            <div class="feature-icon">
                                <i class="fas fa-graduation-cap"></i>
                            </div>
                            <p class="feature-title">Qualifications</p>
                        </div>

                        <div class="feature-item">
                            <div class="feature-icon">
                                <i class="fas fa-award"></i>
                            </div>
                            <p class="feature-title">Achievements</p>
                        </div>
                    </div>

                </div>

            </div>
        </section>

        <div class="nicdark_space40"></div>

    </asp:Content>