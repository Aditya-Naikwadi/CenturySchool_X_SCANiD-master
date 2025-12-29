<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="ChairmanMessage.aspx.cs" Inherits="CenturyRayonSchool.ChairmanMessage" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link
            href="https://fonts.googleapis.com/css2?family=Playfair+Display:ital,wght@0,600;0,700;0,800;1,600&family=Merriweather:ital,wght@0,300;0,400;0,700;1,400&family=Poppins:wght@300;400;500;600&display=swap"
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
            .chairman-hero {
                background: linear-gradient(135deg, var(--maroon-dark) 0%, var(--maroon-primary) 50%, var(--maroon-light) 100%);
                padding: 100px 0 120px;
                margin-top: 70px;
                position: relative;
                overflow: hidden;
            }

            .chairman-hero::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background:
                    radial-gradient(circle at 20% 30%, rgba(255, 193, 7, 0.15) 0%, transparent 50%),
                    radial-gradient(circle at 80% 70%, rgba(255, 255, 255, 0.1) 0%, transparent 50%);
                pointer-events: none;
            }

            .chairman-hero::after {
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

            .hero-icon {
                width: 100px;
                height: 100px;
                background: linear-gradient(135deg, var(--gold), var(--gold-light));
                border-radius: 50%;
                display: flex;
                align-items: center;
                justify-content: center;
                margin: 0 auto 30px;
                box-shadow: 0 20px 50px rgba(255, 193, 7, 0.4);
                animation: float 3s ease-in-out infinite;
            }

            .hero-icon i {
                font-size: 3rem;
                color: var(--maroon-dark);
            }

            .hero-title {
                font-family: 'Playfair Display', serif;
                font-size: 3.8rem;
                font-weight: 800;
                color: white;
                margin-bottom: 1rem;
                text-shadow: 2px 2px 8px rgba(0, 0, 0, 0.3);
                animation: fadeInDown 0.8s ease-out;
            }

            .hero-subtitle {
                font-family: 'Merriweather', serif;
                font-size: 1.3rem;
                color: rgba(255, 255, 255, 0.95);
                font-weight: 300;
                font-style: italic;
                animation: fadeInUp 0.8s ease-out 0.2s both;
            }

            /* Main Content */
            .content-section {
                padding: 0px 0 80px;
                margin-top: -80px;
                position: relative;
                z-index: 10;
            }

            /* Message Card */
            .message-card {
                background: white;
                border-radius: 30px;
                padding: 60px 70px;
                box-shadow: 0 25px 80px rgba(0, 0, 0, 0.15);
                position: relative;
                overflow: hidden;
                opacity: 0;
                transform: translateY(30px);
                transition: all 0.6s ease;
            }

            .message-card.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            /* Decorative Quote Mark */
            .quote-mark {
                position: absolute;
                top: 30px;
                left: 50px;
                font-size: 10rem;
                color: rgba(124, 56, 72, 0.05);
                font-family: 'Playfair Display', serif;
                line-height: 1;
                pointer-events: none;
            }

            .quote-mark-end {
                position: absolute;
                bottom: 30px;
                right: 50px;
                font-size: 10rem;
                color: rgba(124, 56, 72, 0.05);
                font-family: 'Playfair Display', serif;
                line-height: 1;
                pointer-events: none;
                transform: rotate(180deg);
            }

            /* Top Accent Bar */
            .message-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 8px;
                background: linear-gradient(90deg, var(--maroon-primary), var(--gold), var(--maroon-light), var(--gold), var(--maroon-primary));
                background-size: 200% 100%;
                animation: gradientShift 3s ease-in-out infinite;
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

            /* Message Header */
            .message-header {
                text-align: center;
                margin-bottom: 40px;
                position: relative;
            }

            .message-title {
                font-family: 'Playfair Display', serif;
                font-size: 2.5rem;
                font-weight: 700;
                color: var(--maroon-primary);
                margin-bottom: 15px;
            }

            .title-underline {
                width: 100px;
                height: 4px;
                background: linear-gradient(90deg, transparent, var(--gold), transparent);
                margin: 0 auto;
            }

            /* Message Body */
            .message-body {
                position: relative;
                z-index: 2;
            }

            .message-text {
                font-family: 'Merriweather', serif;
                font-size: 1.15rem;
                line-height: 2;
                color: var(--text-dark);
                margin-bottom: 30px;
                text-align: justify;
                text-indent: 50px;
            }

            .message-text::first-letter {
                font-size: 4em;
                font-weight: 700;
                color: var(--maroon-primary);
                float: left;
                line-height: 0.9;
                margin: 10px 15px 0 0;
                font-family: 'Playfair Display', serif;
            }

            /* Signature Section */
            .signature-section {
                margin-top: 50px;
                padding-top: 30px;
                border-top: 2px solid #f0f0f0;
                text-align: right;
            }

            .wishes-text {
                font-family: 'Merriweather', serif;
                font-size: 1.1rem;
                font-style: italic;
                color: var(--text-light);
                margin-bottom: 20px;
            }

            .signature-box {
                display: inline-flex;
                align-items: center;
                gap: 20px;
                background: linear-gradient(135deg, #f8f9fa 0%, #ffffff 100%);
                padding: 25px 35px;
                border-radius: 15px;
                border-left: 5px solid var(--gold);
                box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
            }

            .signature-icon {
                width: 60px;
                height: 60px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 12px;
                display: flex;
                align-items: center;
                justify-content: center;
                flex-shrink: 0;
            }

            .signature-icon i {
                font-size: 1.8rem;
                color: white;
            }

            .signature-details {
                text-align: left;
            }

            .signature-role {
                font-family: 'Playfair Display', serif;
                font-size: 1.4rem;
                font-weight: 700;
                color: var(--maroon-primary);
                margin: 0;
            }

            .signature-org {
                font-size: 0.95rem;
                color: var(--text-light);
                margin: 5px 0 0 0;
            }

            /* Decorative Elements */
            .floating-elements {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                pointer-events: none;
                z-index: 0;
                overflow: hidden;
            }

            .float-shape {
                position: absolute;
                opacity: 0.03;
            }

            .shape-1 {
                top: 15%;
                left: 8%;
                font-size: 8rem;
                color: var(--maroon-primary);
                animation: float-1 10s ease-in-out infinite;
            }

            .shape-2 {
                top: 65%;
                right: 8%;
                font-size: 7rem;
                color: var(--gold);
                animation: float-2 12s ease-in-out infinite;
            }

            .shape-3 {
                bottom: 15%;
                left: 12%;
                font-size: 6rem;
                color: var(--maroon-light);
                animation: float-3 14s ease-in-out infinite;
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

            @keyframes float {

                0%,
                100% {
                    transform: translateY(0);
                }

                50% {
                    transform: translateY(-20px);
                }
            }

            @keyframes float-1 {

                0%,
                100% {
                    transform: translate(0, 0) rotate(0deg);
                }

                50% {
                    transform: translate(30px, -40px) rotate(180deg);
                }
            }

            @keyframes float-2 {

                0%,
                100% {
                    transform: translate(0, 0) rotate(0deg);
                }

                50% {
                    transform: translate(-40px, 45px) rotate(-180deg);
                }
            }

            @keyframes float-3 {

                0%,
                100% {
                    transform: translate(0, 0) scale(1);
                }

                50% {
                    transform: translate(40px, -30px) scale(1.1);
                }
            }

            /* Hover Effect */
            .message-card:hover {
                box-shadow: 0 30px 90px rgba(124, 56, 72, 0.2);
            }

            /* Responsive Design */
            @media (max-width: 1024px) {
                .message-card {
                    padding: 50px 50px;
                }
            }

            @media (max-width: 768px) {
                .hero-title {
                    font-size: 2.8rem;
                }

                .chairman-hero {
                    padding: 80px 0 100px;
                }

                .message-card {
                    padding: 40px 35px;
                    border-radius: 20px;
                }

                .message-title {
                    font-size: 2rem;
                }

                .message-text {
                    font-size: 1.05rem;
                    line-height: 1.8;
                    text-indent: 30px;
                }

                .quote-mark,
                .quote-mark-end {
                    font-size: 6rem;
                }

                .signature-section {
                    text-align: center;
                }

                .signature-box {
                    display: flex;
                    flex-direction: column;
                    text-align: center;
                }

                .signature-details {
                    text-align: center;
                }
            }

            @media (max-width: 576px) {
                .hero-title {
                    font-size: 2.2rem;
                }

                .hero-subtitle {
                    font-size: 1.1rem;
                }

                .hero-icon {
                    width: 80px;
                    height: 80px;
                }

                .hero-icon i {
                    font-size: 2.4rem;
                }

                .message-card {
                    padding: 30px 25px;
                }

                .message-title {
                    font-size: 1.7rem;
                }

                .message-text {
                    font-size: 1rem;
                }

                .quote-mark,
                .quote-mark-end {
                    display: none;
                }
            }

            /* Smooth Scroll */
            html {
                scroll-behavior: smooth;
            }

            /* Main Content Wrapper */
            .main-content {
                position: relative;
                z-index: 1;
            }

            /* Print Styles */
            @media print {

                .chairman-hero,
                .floating-elements {
                    display: none;
                }

                .message-card {
                    box-shadow: none;
                    page-break-inside: avoid;
                }

                .content-section {
                    margin-top: 0;
                }
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Floating Decorative Elements -->
        <div class="floating-elements">
            <i class="fas fa-quote-right float-shape shape-1"></i>
            <i class="fas fa-graduation-cap float-shape shape-2"></i>
            <i class="fas fa-book-open float-shape shape-3"></i>
        </div>

        <!-- Hero Section -->
        <section class="chairman-hero">
            <div class="container">
                <div class="hero-content">
                    <div class="hero-icon">
                        <i class="fas fa-user-tie"></i>
                    </div>
                    <h1 class="hero-title">From the Chairman's Desk</h1>
                    <p class="hero-subtitle">"Leadership is not about being in charge. It's about taking care of those
                        in your charge."</p>
                </div>
            </div>
        </section>

        <!-- Main Content Section -->
        <section class="content-section main-content">
            <div class="container">

                <!-- Message Card -->
                <div class="message-card">
                    <!-- Decorative Quote Marks -->
                    <div class="quote-mark">"</div>
                    <div class="quote-mark-end">"</div>

                    <!-- Message Header -->
                    <div class="message-header">
                        <h2 class="message-title">A Message of Inspiration</h2>
                        <div class="title-underline"></div>
                    </div>

                    <!-- Message Body -->
                    <div class="message-body">
                        <div class="message-text">
                            Education is the process of facilitating learning or the acquisition of knowledge, skills,
                            values, morals and personal development. Quality education refers to the knowledge received
                            through schooling. The main purpose of education is the integral development of a child. It
                            decides a child's behaviour, aspirations, introspections and learning to achieve success.
                        </div>

                        <div class="message-text">
                            We, at Century Rayon School, follow this philosophy to stimulate thinking skills, improve
                            children's critical and creative thinking, and enhance communication skills to meet the
                            ever-growing challenges being faced in a competitive environment. The values and environment
                            we provide in the school to your children are sustainable and see them successful in their
                            formative years.
                        </div>

                        <div class="message-text">
                            As the Chairman of the school, I assure you that the future of your child is secure and we
                            would continue to put efforts to realise greater success.
                        </div>

                        <!-- Signature Section -->
                        <div class="signature-section">
                            <div class="wishes-text">With best wishes...</div>
                            <div class="signature-box">
                                <div class="signature-icon">
                                    <i class="fas fa-feather-alt"></i>
                                </div>
                                <div class="signature-details">
                                    <p class="signature-role">Chairman</p>
                                    <p class="signature-org">Century Rayon High School</p>
                                </div>
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
                // Intersection Observer for scroll animation
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

                // Observe message card
                const messageCard = document.querySelector('.message-card');
                if (messageCard) {
                    observer.observe(messageCard);
                }

                // Add parallax effect to quote marks
                document.addEventListener('mousemove', function (e) {
                    const card = document.querySelector('.message-card');
                    if (!card) return;

                    const rect = card.getBoundingClientRect();
                    const x = (e.clientX - rect.left) / rect.width - 0.5;
                    const y = (e.clientY - rect.top) / rect.height - 0.5;

                    const quoteMarks = document.querySelectorAll('.quote-mark, .quote-mark-end');
                    quoteMarks.forEach(function (mark) {
                        const moveX = x * 20;
                        const moveY = y * 20;
                        mark.style.transform = `translate(${moveX}px, ${moveY}px)`;
                    });
                });
            });
        </script>

    </asp:Content>