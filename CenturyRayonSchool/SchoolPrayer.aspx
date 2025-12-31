<%@ Page Title="" Language="C#" MasterPageFile="~/WebsiteMaster.Master" AutoEventWireup="true"
    CodeBehind="SchoolPrayer.aspx.cs" Inherits="CenturyRayonSchool.SchoolPrayer" %>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link
            href="https://fonts.googleapis.com/css2?family=Quintessential&family=Crimson+Text:ital,wght@0,400;0,600;1,400&family=Playfair+Display:wght@600;700;800&display=swap"
            rel="stylesheet">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">

        <style>
            :root {
                --maroon-primary: #7c3848;
                --maroon-light: #a24c58;
                --gold: #ffc107;
                --text-dark: #2c3e50;
                --bg-light: #f5f3ee;
            }

            body {
                background: linear-gradient(135deg, #f5f3ee 0%, #e8e4db 100%);
                min-height: 100vh;
            }

            /* Hero Section */
            .prayer-hero {
                background: linear-gradient(135deg, var(--maroon-primary) 0%, var(--maroon-light) 100%);
                padding: 80px 0 60px;
                margin-top: 70px;
                position: relative;
                overflow: hidden;
            }

            .prayer-hero::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                bottom: 0;
                background:
                    radial-gradient(circle at 20% 50%, rgba(255, 193, 7, 0.1) 0%, transparent 50%),
                    radial-gradient(circle at 80% 80%, rgba(255, 255, 255, 0.1) 0%, transparent 50%);
                pointer-events: none;
            }

            .prayer-title {
                font-family: 'Playfair Display', serif;
                font-size: 3.5rem;
                font-weight: 800;
                color: white;
                text-align: center;
                margin-bottom: 1rem;
                text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);
                position: relative;
                animation: fadeInDown 0.8s ease-out;
            }

            .prayer-subtitle {
                font-family: 'Crimson Text', serif;
                font-size: 1.3rem;
                color: rgba(255, 255, 255, 0.9);
                text-align: center;
                font-style: italic;
                position: relative;
            }

            /* Prayer Cards Container */
            .prayers-container {
                padding: 60px 0;
                position: relative;
            }

            /* Prayer Card Styles */
            .prayer-card {
                background: white;
                border-radius: 20px;
                padding: 50px 40px;
                margin-bottom: 40px;
                box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
                transition: all 0.4s cubic-bezier(0.4, 0, 0.2, 1);
                position: relative;
                overflow: hidden;
                opacity: 0;
                transform: translateY(30px);
            }

            .prayer-card.animate-in {
                opacity: 1;
                transform: translateY(0);
            }

            .prayer-card::before {
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                right: 0;
                height: 5px;
                background: linear-gradient(90deg, var(--maroon-primary), var(--gold), var(--maroon-light));
            }

            .prayer-card:hover {
                transform: translateY(-10px);
                box-shadow: 0 20px 60px rgba(124, 56, 72, 0.2);
            }

            /* Icon Circle */
            .prayer-icon {
                width: 80px;
                height: 80px;
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                border-radius: 50%;
                display: flex;
                align-items: center;
                justify-content: center;
                margin: 0 auto 30px;
                box-shadow: 0 10px 30px rgba(124, 56, 72, 0.3);
                transition: all 0.3s ease;
            }

            .prayer-card:hover .prayer-icon {
                transform: scale(1.1) rotate(360deg);
            }

            .prayer-icon i {
                font-size: 2rem;
                color: white;
            }

            /* Prayer Headers */
            .prayer-header {
                font-family: 'Playfair Display', serif;
                font-size: 2rem;
                font-weight: 700;
                color: var(--maroon-primary);
                text-align: center;
                margin-bottom: 30px;
                position: relative;
                padding-bottom: 15px;
            }

            .prayer-header::after {
                content: '';
                position: absolute;
                bottom: 0;
                left: 50%;
                transform: translateX(-50%);
                width: 80px;
                height: 3px;
                background: linear-gradient(90deg, transparent, var(--gold), transparent);
            }

            /* Prayer Text */
            .prayer-text {
                font-family: 'Crimson Text', serif;
                font-size: 1.2rem;
                line-height: 2;
                color: var(--text-dark);
                text-align: center;
                position: relative;
            }

            /* Special Styling for Pledge */
            .pledge-card .prayer-text {
                text-align: justify;
                text-align-last: center;
            }

            /* Decorative Elements */
            .decorative-line {
                width: 60px;
                height: 3px;
                background: var(--gold);
                margin: 30px auto;
                position: relative;
            }

            .decorative-line::before,
            .decorative-line::after {
                content: '';
                position: absolute;
                width: 8px;
                height: 8px;
                background: var(--gold);
                border-radius: 50%;
                top: 50%;
                transform: translateY(-50%);
            }

            .decorative-line::before {
                left: -15px;
            }

            .decorative-line::after {
                right: -15px;
            }

            /* Animations */
            @keyframes fadeInDown {
                from {
                    opacity: 0;
                    transform: translateY(-30px);
                }

                to {
                    opacity: 1;
                    transform: translateY(0);
                }
            }

            @keyframes fadeInUp {
                from {
                    opacity: 0;
                    transform: translateY(30px);
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

            /* Floating Icons Background */
            .floating-icons {
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                pointer-events: none;
                z-index: 0;
                overflow: hidden;
            }

            .floating-icon {
                position: absolute;
                font-size: 3rem;
                color: rgba(124, 56, 72, 0.05);
                animation: float 6s ease-in-out infinite;
            }

            .floating-icon:nth-child(1) {
                top: 10%;
                left: 10%;
                animation-delay: 0s;
            }

            .floating-icon:nth-child(2) {
                top: 60%;
                left: 80%;
                animation-delay: 2s;
            }

            .floating-icon:nth-child(3) {
                top: 80%;
                left: 20%;
                animation-delay: 4s;
            }

            /* Audio Player Style */
            .audio-controls {
                text-align: center;
                margin: 40px 0;
            }

            .play-button {
                background: linear-gradient(135deg, var(--maroon-primary), var(--maroon-light));
                color: white;
                border: none;
                border-radius: 50px;
                padding: 15px 40px;
                font-size: 1.1rem;
                font-weight: 600;
                cursor: pointer;
                box-shadow: 0 10px 30px rgba(124, 56, 72, 0.3);
                transition: all 0.3s ease;
                display: inline-flex;
                align-items: center;
                gap: 10px;
            }

            .play-button:hover {
                transform: translateY(-3px);
                box-shadow: 0 15px 40px rgba(124, 56, 72, 0.4);
            }

            .play-button:active {
                transform: translateY(-1px);
            }

            /* Responsive Design */
            @media (max-width: 768px) {
                .prayer-title {
                    font-size: 2.5rem;
                }

                .prayer-card {
                    padding: 30px 20px;
                    margin-bottom: 30px;
                }

                .prayer-header {
                    font-size: 1.5rem;
                }

                .prayer-text {
                    font-size: 1.1rem;
                    line-height: 1.8;
                }

                .prayer-hero {
                    padding: 60px 0 40px;
                    margin-top: 60px;
                }
            }

            @media (max-width: 576px) {
                .prayer-title {
                    font-size: 2rem;
                }

                .prayer-subtitle {
                    font-size: 1.1rem;
                }

                .prayer-icon {
                    width: 60px;
                    height: 60px;
                }

                .prayer-icon i {
                    font-size: 1.5rem;
                }
            }

            /* Print Styles */
            @media print {

                .prayer-hero,
                .audio-controls {
                    display: none;
                }

                .prayer-card {
                    box-shadow: none;
                    page-break-inside: avoid;
                }
            }

            /* Smooth Scroll */
            html {
                scroll-behavior: smooth;
            }

            /* Content Wrapper */
            .prayer-content {
                position: relative;
                z-index: 1;
            }
        </style>
    </asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <!-- Floating Background Icons -->
        <div class="floating-icons">
            <i class="floating-icon fas fa-om"></i>
            <i class="floating-icon fas fa-place-of-worship"></i>
            <i class="floating-icon fas fa-praying-hands"></i>
        </div>

        <!-- Hero Section -->
        <section class="prayer-hero">
            <div class="container">
                <h1 class="prayer-title">School Prayer</h1>
                <p class="prayer-subtitle">"In unity we pray, in prayer we find strength"</p>
            </div>
        </section>

        <!-- Prayers Container -->
        <section class="prayers-container prayer-content">
            <div class="container">

                <!-- National Anthem Card -->
                <div class="prayer-card" data-aos="fade-up">
                    <div class="prayer-icon">
                        <i class="fas fa-flag"></i>
                    </div>
                    <h2 class="prayer-header">National Anthem</h2>
                    <div class="decorative-line"></div>
                    <div class="prayer-text">
                        Jana-gana-mana-adhinayaka jaya he<br>
                        Bharata-bhagya-vidhata<br>
                        Panjaba-Sindhu-Gujarat-Maratha<br>
                        Dravida-Utkala-Banga<br>
                        Vindhya-Himachala-Yamuna-Ganga<br>
                        uchchhal-jaladhi-taranga<br>
                        Tava Subha name jage, tava subha ashish mange,<br>
                        gahe tava jaya-gatha.<br>
                        Jana-gana-mangala-dayaka jaya he<br>
                        Bharata-bhagya-vidhata.<br>
                        Jaya he, Jaya he, Jaya he,<br>
                        jaya jaya jaya jaya he.
                    </div>
                </div>

                <!-- Vande Mataram Card -->
                <div class="prayer-card" data-aos="fade-up" data-aos-delay="200">
                    <div class="prayer-icon">
                        <i class="fas fa-heart"></i>
                    </div>
                    <h2 class="prayer-header">Vande Mataram</h2>
                    <div class="decorative-line"></div>
                    <div class="prayer-text">
                        Vande Mataram… Vande Mataram…<br>
                        Sujalam Suphalam Malayaja Shitalam<br>
                        Sasyashyamalam Mataram<br>
                        Vande mataram<br>
                        Shubhrajyotsana Pulakityamineem<br>
                        Phullakusumit Drumadal Shobhineem<br>
                        Suhasineem, Sumadhur Bhashineem<br>
                        Sukhadam, Vardam Mataram<br>
                        Vande Mataram
                    </div>
                </div>

                <!-- Pledge Card -->
                <div class="prayer-card pledge-card" data-aos="fade-up" data-aos-delay="400">
                    <div class="prayer-icon">
                        <i class="fas fa-hands-praying"></i>
                    </div>
                    <h2 class="prayer-header">Pledge</h2>
                    <div class="decorative-line"></div>
                    <div class="prayer-text">
                        India is my country. All Indians are my brothers and sisters. I love my country and I am proud
                        of its rich and varied heritage. I shall always strive to be worthy of it. I shall give my
                        parents, teachers, and all elders respect and treat everyone with courtesy. To my country and my
                        people, I pledge my devotion. In their well-being and prosperity alone, lies my happiness.
                    </div>
                </div>

            </div>
        </section>

        <div class="nicdark_space40"></div>

        <!-- Animation Script -->
        <script>
            document.addEventListener('DOMContentLoaded', function () {
                // Scroll Animation Observer
                const observerOptions = {
                    root: null,
                    rootMargin: '0px',
                    threshold: 0.1
                };

                const observer = new IntersectionObserver(function (entries) {
                    entries.forEach(function (entry) {
                        if (entry.isIntersecting) {
                            entry.target.classList.add('animate-in');
                            observer.unobserve(entry.target);
                        }
                    });
                }, observerOptions);

                // Observe all prayer cards
                const prayerCards = document.querySelectorAll('.prayer-card');
                prayerCards.forEach(function (card) {
                    observer.observe(card);
                });

                // Add hover sound effect (optional)
                prayerCards.forEach(function (card) {
                    card.addEventListener('mouseenter', function () {
                        this.style.borderLeft = '5px solid var(--gold)';
                    });

                    card.addEventListener('mouseleave', function () {
                        this.style.borderLeft = 'none';
                    });
                });
            });
        </script>

    </asp:Content>