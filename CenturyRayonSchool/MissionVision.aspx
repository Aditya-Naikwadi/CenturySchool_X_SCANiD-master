<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="MissionVision.aspx.cs" Inherits="CenturyRayonSchool.MissionVision" %>
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
                --white: #ffffff;
            }

            * {
                margin: 0;
                padding: 0;
                box-sizing: border-box;
            }

            body {
                font-family: 'Raleway', sans-serif;
                background: linear-gradient(135deg, #f5f3ee 0%, #e8e4db 100%);
                overflow-x: hidden;
            }

            /* Hero Section */
            .mission-hero {
                background: linear-gradient(135deg, var(--maroon-primary) 0%, var(--maroon-dark) 100%);
                padding: 100px 0 80px;
                margin-top: 70px;
                position: relative;
                overflow: hidden;
            }

            .mission-hero::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background:
                    radial-gradient(circle at 10% 20%, rgba(255, 193, 7, 0.15) 0%, transparent 40%),
                    radial-gradient(circle at 90% 80%, rgba(255, 255, 255, 0.1) 0%, transparent 40%);
            }

            .mission-hero::after {
                content: '';
                position: absolute;
                bottom: -2px;
                left: 0;
                right: 0;
                height: 100px;
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
                letter-spacing: 1px;
                animation: fadeInUp 0.8s ease-out 0.2s both;
            }

            /* Main Content Section */
            .content-section {
                padding: 80px 0;
                position: relative;
            }

            /* Mission & Vision Cards - Side by Side */
            .mv-container {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 40px;
                margin-bottom: 60px;
            }

            .mv-card {
                background: white;
                border-radius: 25px;
                padding: 50px 40px;
                box-shadow: 0 15px 50px rgba(0, 0, 0, 0.1);
                position: relative;
                overflow: hidden;
                transition: all 0.4s cubic-bezier(0.4, 0, 0.2, 1);
                opacity: 0;
                transform: translateY(30px);
            }

            .mv-card.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            .mv-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 6px;
                background: linear-gradient(90deg, var(--maroon-primary), var(--gold), var(--maroon-light));
            }

            .mv-card:hover {
                transform: translateY(-15px) scale(1.02);
                box-shadow: 0 25px 70px rgba(124, 56, 72, 0.25);
            }

            /* Icon Background */
            .mv-card::after {
                content: '';
                position: absolute;
                top: -50px;
                right: -50px;
                width: 200px;
                height: 200px;
                border-radius: 50%;
                opacity: 0.05;
                transition: all 0.4s ease;
            }

            .mission-card::after {
                background: var(--gold);
            }

            .vision-card::after {
                background: var(--maroon-primary);
            }

            .mv-card:hover::after {
                transform: scale(1.5);
                opacity: 0.1;
            }

            /* Card Icon */
            .mv-icon {
                width: 90px;
                height: 90px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 20px;
                display: flex;
                align-items: center;
                justify-content: center;
                margin-bottom: 30px;
                box-shadow: 0 15px 40px rgba(124, 56, 72, 0.3);
                transition: all 0.4s ease;
                position: relative;
            }

            .vision-card .mv-icon {
                background: linear-gradient(135deg, var(--gold), var(--gold-light));
            }

            .mv-card:hover .mv-icon {
                transform: rotateY(360deg) scale(1.1);
            }

            .mv-icon i {
                font-size: 2.5rem;
                color: white;
            }

            .vision-card .mv-icon i {
                color: var(--maroon-dark);
            }

            /* Card Title */
            .mv-title {
                font-family: 'Playfair Display', serif;
                font-size: 2.2rem;
                font-weight: 700;
                color: var(--maroon-primary);
                margin-bottom: 25px;
                position: relative;
                display: inline-block;
            }

            .mv-title::after {
                content: '';
                position: absolute;
                bottom: -10px;
                left: 0;
                width: 60px;
                height: 4px;
                background: var(--gold);
                border-radius: 2px;
            }

            /* Card Text */
            .mv-text {
                font-family: 'Poppins', sans-serif;
                font-size: 1.1rem;
                line-height: 1.9;
                color: var(--text-light);
                font-weight: 400;
            }

            /* Core Competencies Section */
            .competencies-section {
                background: white;
                border-radius: 25px;
                padding: 60px 50px;
                box-shadow: 0 20px 60px rgba(0, 0, 0, 0.12);
                position: relative;
                opacity: 0;
                transform: translateY(30px);
                transition: all 0.5s ease;
            }

            .competencies-section.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            .competencies-section::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 6px;
                background: linear-gradient(90deg, var(--gold), var(--maroon-primary), var(--gold));
            }

            .competencies-header {
                text-align: center;
                margin-bottom: 50px;
            }

            .competencies-title {
                font-family: 'Playfair Display', serif;
                font-size: 2.5rem;
                font-weight: 700;
                color: var(--maroon-primary);
                margin-bottom: 15px;
            }

            .competencies-subtitle {
                font-size: 1.1rem;
                color: var(--text-light);
                font-weight: 400;
            }

            /* Competency Items Grid */
            .competencies-grid {
                display: grid;
                grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
                gap: 30px;
            }

            .competency-item {
                display: flex;
                align-items: flex-start;
                gap: 20px;
                padding: 25px;
                background: linear-gradient(135deg, #f8f9fa 0%, #ffffff 100%);
                border-radius: 15px;
                border-left: 4px solid var(--gold);
                transition: all 0.3s ease;
                opacity: 0;
                transform: translateX(-20px);
            }

            .competency-item.animate-in {
                opacity: 1;
                transform: translateX(0);
            }

            .competency-item:hover {
                transform: translateX(10px);
                box-shadow: 0 10px 30px rgba(124, 56, 72, 0.15);
                border-left-width: 6px;
            }

            .competency-icon {
                width: 50px;
                height: 50px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 12px;
                display: flex;
                align-items: center;
                justify-content: center;
                flex-shrink: 0;
                transition: all 0.3s ease;
            }

            .competency-item:hover .competency-icon {
                transform: rotate(360deg) scale(1.1);
            }

            .competency-icon i {
                font-size: 1.3rem;
                color: white;
            }

            .competency-text {
                font-family: 'Poppins', sans-serif;
                font-size: 1rem;
                line-height: 1.7;
                color: var(--text-dark);
                font-weight: 400;
                flex: 1;
            }

            /* Decorative Elements */
            .floating-shapes {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                pointer-events: none;
                z-index: 0;
                overflow: hidden;
            }

            .shape {
                position: absolute;
                opacity: 0.03;
            }

            .shape-1 {
                top: 20%;
                left: 10%;
                font-size: 8rem;
                color: var(--maroon-primary);
                animation: float-1 8s ease-in-out infinite;
            }

            .shape-2 {
                top: 60%;
                right: 10%;
                font-size: 6rem;
                color: var(--gold);
                animation: float-2 10s ease-in-out infinite;
            }

            .shape-3 {
                bottom: 10%;
                left: 15%;
                font-size: 7rem;
                color: var(--maroon-light);
                animation: float-3 12s ease-in-out infinite;
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

            @keyframes float-1 {

                0%,
                100% {
                    transform: translate(0, 0) rotate(0deg);
                }

                50% {
                    transform: translate(30px, -30px) rotate(180deg);
                }
            }

            @keyframes float-2 {

                0%,
                100% {
                    transform: translate(0, 0) rotate(0deg);
                }

                50% {
                    transform: translate(-40px, 40px) rotate(-180deg);
                }
            }

            @keyframes float-3 {

                0%,
                100% {
                    transform: translate(0, 0) rotate(0deg);
                }

                50% {
                    transform: translate(40px, -20px) rotate(180deg);
                }
            }

            /* Responsive Design */
            @media (max-width: 1024px) {
                .mv-container {
                    gap: 30px;
                }

                .competencies-grid {
                    grid-template-columns: 1fr;
                }
            }

            @media (max-width: 768px) {
                .hero-title {
                    font-size: 2.8rem;
                }

                .hero-subtitle {
                    font-size: 1.1rem;
                }

                .mission-hero {
                    padding: 80px 0 60px;
                }

                .mv-container {
                    grid-template-columns: 1fr;
                    gap: 30px;
                }

                .mv-card {
                    padding: 40px 30px;
                }

                .mv-title {
                    font-size: 1.8rem;
                }

                .mv-text {
                    font-size: 1rem;
                }

                .competencies-section {
                    padding: 40px 30px;
                }

                .competencies-title {
                    font-size: 2rem;
                }

                .competencies-grid {
                    gap: 20px;
                }
            }

            @media (max-width: 576px) {
                .hero-title {
                    font-size: 2.2rem;
                }

                .hero-subtitle {
                    font-size: 1rem;
                }

                .mv-card {
                    padding: 30px 20px;
                }

                .mv-icon {
                    width: 70px;
                    height: 70px;
                }

                .mv-icon i {
                    font-size: 2rem;
                }

                .competencies-section {
                    padding: 30px 20px;
                }

                .competency-item {
                    padding: 20px;
                }
            }

            /* Smooth Scroll */
            html {
                scroll-behavior: smooth;
            }

            /* Content Wrapper */
            .main-content {
                position: relative;
                z-index: 1;
            }

            /* Print Styles */
            @media print {

                .mission-hero,
                .floating-shapes {
                    display: none;
                }

                .mv-card,
                .competencies-section {
                    box-shadow: none;
                    page-break-inside: avoid;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Floating Decorative Shapes -->
        <div class="floating-shapes">
            <i class="fas fa-bullseye shape shape-1"></i>
            <i class="fas fa-lightbulb shape shape-2"></i>
            <i class="fas fa-trophy shape shape-3"></i>
        </div>

        <!-- Hero Section -->
        <section class="mission-hero">
            <div class="container">
                <div class="hero-content">
                    <h1 class="hero-title">Mission & Vision</h1>
                    <p class="hero-subtitle">Empowering Future Leaders Through Excellence in Education</p>
                </div>
            </div>
        </section>

        <!-- Main Content Section -->
        <section class="content-section main-content">
            <div class="container">

                <!-- Mission & Vision Cards -->
                <div class="mv-container">

                    <!-- Mission Card -->
                    <div class="mv-card mission-card">
                        <div class="mv-icon">
                            <i class="fas fa-bullseye"></i>
                        </div>
                        <h2 class="mv-title">Our Mission</h2>
                        <div class="mv-text">
                            We at Century Rayon High School Shahad strive to provide our students the best opportunities
                            for enhancing their inherent and acquired potentials, instilling in them a belief in
                            life-long learning and thereby motivating them to be responsible citizens and productive
                            participants in the growth of family, society and country.
                        </div>
                    </div>

                    <!-- Vision Card -->
                    <div class="mv-card vision-card">
                        <div class="mv-icon">
                            <i class="fas fa-eye"></i>
                        </div>
                        <h2 class="mv-title">Our Vision</h2>
                        <div class="mv-text">
                            We are committed to continually improve in terms of technology, curriculum, human resources
                            and infrastructure to meet the future advancements in education. We envision a learning
                            environment that empowers every student to achieve their full potential.
                        </div>
                    </div>

                </div>

                <!-- Core Competencies Section -->
                <div class="competencies-section">
                    <div class="competencies-header">
                        <h2 class="competencies-title">Core Competencies</h2>
                        <p class="competencies-subtitle">Excellence Through Innovation and Dedication</p>
                    </div>

                    <div class="competencies-grid">

                        <div class="competency-item">
                            <div class="competency-icon">
                                <i class="fas fa-users"></i>
                            </div>
                            <div class="competency-text">
                                Working together towards a common goal of taking the school to greater heights.
                            </div>
                        </div>

                        <div class="competency-item">
                            <div class="competency-icon">
                                <i class="fas fa-chalkboard-teacher"></i>
                            </div>
                            <div class="competency-text">
                                Regular faculty development through trainings and workshops.
                            </div>
                        </div>

                        <div class="competency-item">
                            <div class="competency-icon">
                                <i class="fas fa-lightbulb"></i>
                            </div>
                            <div class="competency-text">
                                Adopting new teaching learning methods.
                            </div>
                        </div>

                        <div class="competency-item">
                            <div class="competency-icon">
                                <i class="fas fa-rocket"></i>
                            </div>
                            <div class="competency-text">
                                Innovations for excellent results.
                            </div>
                        </div>

                        <div class="competency-item">
                            <div class="competency-icon">
                                <i class="fas fa-balance-scale"></i>
                            </div>
                            <div class="competency-text">
                                Blend of scholastic & co-scholastic activities for the holistic development of the
                                students.
                            </div>
                        </div>

                    </div>
                </div>

            </div>
        </section>

        <div class="nicdark_space40"></div>

        <!-- Animation Script -->
        <script>
            document.addEventListener('DOMContentLoaded', function () {
                // Intersection Observer for scroll animations
                const observerOptions = {
                    root: null,
                    rootMargin: '0px',
                    threshold: 0.15
                };

                const observer = new IntersectionObserver(function (entries) {
                    entries.forEach(function (entry) {
                        if (entry.isIntersecting) {
                            entry.target.classList.add('animate-in');
                            observer.unobserve(entry.target);
                        }
                    });
                }, observerOptions);

                // Observe mission/vision cards
                const mvCards = document.querySelectorAll('.mv-card');
                mvCards.forEach(function (card, index) {
                    setTimeout(function () {
                        observer.observe(card);
                    }, index * 100);
                });

                // Observe competencies section
                const competenciesSection = document.querySelector('.competencies-section');
                if (competenciesSection) {
                    observer.observe(competenciesSection);

                    // Staggered animation for competency items
                    const competencyItems = document.querySelectorAll('.competency-item');
                    const competencyObserver = new IntersectionObserver(function (entries) {
                        entries.forEach(function (entry, index) {
                            if (entry.isIntersecting) {
                                setTimeout(function () {
                                    entry.target.classList.add('animate-in');
                                }, index * 100);
                                competencyObserver.unobserve(entry.target);
                            }
                        });
                    }, observerOptions);

                    competencyItems.forEach(function (item) {
                        competencyObserver.observe(item);
                    });
                }

                // Add interactive glow effect
                document.querySelectorAll('.mv-card, .competency-item').forEach(function (card) {
                    card.addEventListener('mousemove', function (e) {
                        const rect = card.getBoundingClientRect();
                        const x = e.clientX - rect.left;
                        const y = e.clientY - rect.top;

                        card.style.setProperty('--mouse-x', x + 'px');
                        card.style.setProperty('--mouse-y', y + 'px');
                    });
                });
            });
        </script>

    </asp:Content>