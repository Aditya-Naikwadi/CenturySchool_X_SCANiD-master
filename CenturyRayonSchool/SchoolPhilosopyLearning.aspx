<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeFile="SchoolPhilosopyLearning.aspx.cs" Inherits="CenturyRayonSchool.SchoolPhilosopyLearning" %>
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
                font-family: 'Raleway', sans-serif;
                background: linear-gradient(135deg, #f5f3ee 0%, #e8e4db 100%);
                overflow-x: hidden;
            }

            /* Hero Section */
            .philosophy-hero {
                background: linear-gradient(135deg, var(--maroon-dark) 0%, var(--maroon-primary) 100%);
                padding: 100px 0 80px;
                margin-top: 70px;
                position: relative;
                overflow: hidden;
            }

            .philosophy-hero::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background:
                    radial-gradient(circle at 30% 40%, rgba(255, 193, 7, 0.15) 0%, transparent 50%),
                    radial-gradient(circle at 70% 60%, rgba(255, 255, 255, 0.1) 0%, transparent 50%);
            }

            .philosophy-hero::after {
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

            /* Content Section */
            .content-section {
                padding: 80px 0;
                position: relative;
            }

            /* Quote Section */
            .quote-section {
                background: white;
                border-radius: 25px;
                padding: 50px;
                margin-bottom: 60px;
                box-shadow: 0 15px 50px rgba(0, 0, 0, 0.1);
                border-left: 6px solid var(--gold);
                position: relative;
                opacity: 0;
                transform: translateY(30px);
                transition: all 0.5s ease;
            }

            .quote-section.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            .quote-icon {
                font-size: 3rem;
                color: var(--gold);
                margin-bottom: 20px;
            }

            .quote-text {
                font-family: 'Playfair Display', serif;
                font-size: 1.5rem;
                font-style: italic;
                color: var(--text-dark);
                line-height: 1.8;
                margin-bottom: 15px;
            }

            .quote-author {
                font-family: 'Raleway', sans-serif;
                font-size: 1.1rem;
                color: var(--maroon-primary);
                font-weight: 600;
            }

            /* Philosophy Cards Grid */
            .philosophy-grid {
                display: grid;
                grid-template-columns: repeat(2, 1fr);
                gap: 40px;
                margin-bottom: 40px;
            }

            .philosophy-card {
                background: white;
                border-radius: 25px;
                padding: 45px 40px;
                box-shadow: 0 15px 50px rgba(0, 0, 0, 0.1);
                position: relative;
                overflow: hidden;
                transition: all 0.4s cubic-bezier(0.4, 0, 0.2, 1);
                opacity: 0;
                transform: translateY(30px);
            }

            .philosophy-card.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            .philosophy-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 6px;
                background: linear-gradient(90deg, var(--maroon-primary), var(--gold));
            }

            .philosophy-card:hover {
                transform: translateY(-10px);
                box-shadow: 0 25px 70px rgba(124, 56, 72, 0.2);
            }

            /* Card Icon */
            .card-icon-box {
                width: 80px;
                height: 80px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 20px;
                display: flex;
                align-items: center;
                justify-content: center;
                margin-bottom: 25px;
                box-shadow: 0 10px 30px rgba(124, 56, 72, 0.3);
                transition: all 0.4s ease;
            }

            .philosophy-card:hover .card-icon-box {
                transform: rotateY(360deg) scale(1.1);
            }

            .card-icon-box i {
                font-size: 2.2rem;
                color: white;
            }

            /* Card Title */
            .card-title {
                font-family: 'Playfair Display', serif;
                font-size: 1.8rem;
                font-weight: 700;
                color: var(--maroon-primary);
                margin-bottom: 20px;
                position: relative;
                padding-bottom: 15px;
            }

            .card-title::after {
                content: '';
                position: absolute;
                bottom: 0;
                left: 0;
                width: 60px;
                height: 4px;
                background: var(--gold);
                border-radius: 2px;
            }

            /* Card Text */
            .card-text {
                font-family: 'Poppins', sans-serif;
                font-size: 1.05rem;
                line-height: 1.8;
                color: var(--text-light);
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

            /* Responsive Design */
            @media (max-width: 1024px) {
                .philosophy-grid {
                    gap: 30px;
                }
            }

            @media (max-width: 768px) {
                .hero-title {
                    font-size: 2.8rem;
                }

                .hero-subtitle {
                    font-size: 1.1rem;
                }

                .philosophy-hero {
                    padding: 80px 0 60px;
                }

                .philosophy-grid {
                    grid-template-columns: 1fr;
                    gap: 30px;
                }

                .philosophy-card {
                    padding: 35px 30px;
                }

                .quote-section {
                    padding: 35px 30px;
                }

                .card-title {
                    font-size: 1.5rem;
                }
            }

            @media (max-width: 576px) {
                .hero-title {
                    font-size: 2.2rem;
                }

                .quote-text {
                    font-size: 1.2rem;
                }

                .philosophy-card {
                    padding: 30px 25px;
                }

                .card-icon-box {
                    width: 65px;
                    height: 65px;
                }

                .card-icon-box i {
                    font-size: 1.8rem;
                }
            }

            html {
                scroll-behavior: smooth;
            }

            .main-content {
                position: relative;
                z-index: 1;
            }

            @media print {
                .philosophy-hero {
                    display: none;
                }

                .philosophy-card,
                .quote-section {
                    box-shadow: none;
                    page-break-inside: avoid;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Hero Section -->
        <section class="philosophy-hero">
            <div class="container">
                <div class="hero-content">
                    <h1 class="hero-title">School's Philosophy About Learning</h1>
                    <p class="hero-subtitle">Nurturing Minds, Shaping Futures</p>
                </div>
            </div>
        </section>

        <!-- Main Content Section -->
        <section class="content-section main-content">
            <div class="container">

                <!-- Quote Section -->
                <div class="quote-section">
                    <div class="quote-icon">
                        <i class="fas fa-quote-left"></i>
                    </div>
                    <p class="quote-text">
                        "Education is a meaningless ritual unless it moulds the character of students and imparts in
                        them strong sense of values."
                    </p>
                    <p class="quote-author">— Dr. Sarala Birla</p>
                </div>

                <!-- Philosophy Cards Grid -->
                <div class="philosophy-grid">

                    <!-- Card 1: Our Philosophy -->
                    <div class="philosophy-card">
                        <div class="card-icon-box">
                            <i class="fas fa-lightbulb"></i>
                        </div>
                        <h3 class="card-title">Our Philosophy About Learning</h3>
                        <p class="card-text">
                            The School strongly believes in her philosophy that the students have to be developed in all
                            aspects to build a unique nation. Students should grow into responsible and confident global
                            citizens, who know to learn, unlearn and relearn.
                        </p>
                    </div>

                    <!-- Card 2: Student-Centered Learning -->
                    <div class="philosophy-card">
                        <div class="card-icon-box">
                            <i class="fas fa-user-graduate"></i>
                        </div>
                        <h3 class="card-title">Student-Centered Learning</h3>
                        <p class="card-text">
                            All our learning activities center around students. We ensure that students are active
                            participants in the learning process and eventually mould them to take ownership of the
                            learning. We attempt to attain this by providing a wide range of interconnected learning
                            opportunities that help children discover and develop their strengths.
                        </p>
                    </div>

                    <!-- Card 3: Stress-Free Environment -->
                    <div class="philosophy-card">
                        <div class="card-icon-box">
                            <i class="fas fa-spa"></i>
                        </div>
                        <h3 class="card-title">A Stress-Free Environment Fosters Learning</h3>
                        <p class="card-text">
                            We firmly believe that maximum learning happens when the mind is fearless. We take utmost
                            care that children feel safe and stress-free in the school. A play-based active learning
                            environment is created by the staff members, which enables children to enjoy & enrich
                            learning experiences.
                        </p>
                    </div>

                    <!-- Card 4: Collaboration and Teamwork -->
                    <div class="philosophy-card">
                        <div class="card-icon-box">
                            <i class="fas fa-users"></i>
                        </div>
                        <h3 class="card-title">Collaborations and Team Work</h3>
                        <p class="card-text">
                            Children learn best when they learn together. A lot of collaborative projects and group
                            projects are given to the students. These projects help in developing the art of organizing,
                            taking initiatives and vital skills like critical thinking, problem solving, etc. The right
                            blend of scholastic and co-scholastic activities ensures holistic development of the
                            student.
                        </p>
                    </div>

                </div>

            </div>
        </section>

        <div class="nicdark_space40"></div>

        <!-- Animation Script -->
        <script>
            document.addEventListener('DOMContentLoaded', function () {
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

                // Observe quote section
                const quoteSection = document.querySelector('.quote-section');
                if (quoteSection) {
                    observer.observe(quoteSection);
                }

                // Observe and stagger philosophy cards
                const cards = document.querySelectorAll('.philosophy-card');
                cards.forEach(function (card, index) {
                    setTimeout(function () {
                        observer.observe(card);
                    }, index * 100);
                });
            });
        </script>

    </asp:Content>