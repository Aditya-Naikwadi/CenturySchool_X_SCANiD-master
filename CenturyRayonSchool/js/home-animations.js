/*
 * Century Rayon School - Home Page Animations
 * Version: 1.0
 * Description: Scroll-triggered animations and interactions for homepage
 */

(function () {
    'use strict';

    // ==========================================
    // INTERSECTION OBSERVER FOR SCROLL ANIMATIONS
    // ==========================================
    function initScrollAnimations() {
        if ('IntersectionObserver' in window) {
            const observerOptions = {
                root: null,
                rootMargin: '0px',
                threshold: 0.1
            };

            const observer = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('animate-in');
                        // Optionally unobserve after animation
                        observer.unobserve(entry.target);
                    }
                });
            }, observerOptions);

            // Observe all section wrappers
            const sections = document.querySelectorAll('.section-wrapper');
            sections.forEach(function (section) {
                observer.observe(section);
            });
        } else {
            // Fallback for browsers without IntersectionObserver
            const sections = document.querySelectorAll('.section-wrapper');
            sections.forEach(function (section) {
                section.classList.add('animate-in');
            });
        }
    }

    // ==========================================
    // PARALLAX EFFECT FOR HERO CAROUSEL
    // ==========================================
    function initParallaxEffect() {
        const heroCarousel = document.querySelector('#heroCarousel');
        if (!heroCarousel) return;

        window.addEventListener('scroll', function () {
            const scrolled = window.pageYOffset;
            const parallaxElements = heroCarousel.querySelectorAll('.carousel-item.active img');

            parallaxElements.forEach(function (element) {
                const speed = 0.5;
                element.style.transform = 'translateY(' + (scrolled * speed) + 'px) scale(1.05)';
            });
        });
    }

    // ==========================================
    // SMOOTH REVEAL FOR CARDS
    // ==========================================
    function initCardRevealAnimations() {
        if ('IntersectionObserver' in window) {
            const cardObserver = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry, index) {
                    if (entry.isIntersecting) {
                        // Add a slight delay for staggered effect
                        setTimeout(function () {
                            entry.target.style.opacity = '1';
                            entry.target.style.transform = 'translateY(0)';
                        }, index * 100);
                        cardObserver.unobserve(entry.target);
                    }
                });
            }, {
                threshold: 0.2
            });

            // Observe all cards
            const cards = document.querySelectorAll('.announcement-card, .event-card, .notice-card');
            cards.forEach(function (card, index) {
                // Set initial state
                card.style.opacity = '0';
                card.style.transform = 'translateY(30px)';
                card.style.transition = 'all 0.6s ease-out';
                cardObserver.observe(card);
            });
        }
    }

    // ==========================================
    // ENHANCED MARQUEE ANIMATION
    // ==========================================
    function enhanceMarqueeAnimation() {
        const marquees = document.querySelectorAll('marquee');

        marquees.forEach(function (marquee) {
            // Add smooth scroll on mouse wheel
            marquee.addEventListener('wheel', function (e) {
                e.preventDefault();
                const delta = e.deltaY;
                this.scrollTop += delta;
            });

            // Add touch support for mobile
            let startY = 0;
            marquee.addEventListener('touchstart', function (e) {
                startY = e.touches[0].pageY;
                this.stop();
            });

            marquee.addEventListener('touchmove', function (e) {
                const currentY = e.touches[0].pageY;
                const diff = startY - currentY;
                this.scrollTop += diff;
                startY = currentY;
            });

            marquee.addEventListener('touchend', function () {
                this.start();
            });
        });
    }

    // ==========================================
    // ICON ANIMATION ON HOVER
    // ==========================================
    function initIconAnimations() {
        const iconCards = document.querySelectorAll('.event-card, .announcement-card');

        iconCards.forEach(function (card) {
            card.addEventListener('mouseenter', function () {
                const icons = this.querySelectorAll('i[class*="bi-"]');
                icons.forEach(function (icon) {
                    icon.style.animation = 'none';
                    setTimeout(function () {
                        icon.style.animation = '';
                    }, 10);
                });
            });
        });
    }

    // ==========================================
    // GALLERY IMAGE LAZY LOADING WITH FADE
    // ==========================================
    function initGalleryLazyLoad() {
        if ('IntersectionObserver' in window) {
            const imageObserver = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        const img = entry.target;
                        img.style.opacity = '0';
                        img.style.transition = 'opacity 0.6s ease-in-out';

                        img.addEventListener('load', function () {
                            this.style.opacity = '1';
                        });

                        imageObserver.unobserve(img);
                    }
                });
            });

            const galleryImages = document.querySelectorAll('#carouselGallery img');
            galleryImages.forEach(function (img) {
                imageObserver.observe(img);
            });
        }
    }

    // ==========================================
    // SMOOTH SECTION TRANSITIONS
    // ==========================================
    function initSectionTransitions() {
        const sections = document.querySelectorAll('.section-wrapper');

        if ('IntersectionObserver' in window) {
            const sectionObserver = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        entry.target.style.opacity = '1';
                        entry.target.style.transform = 'translateY(0)';
                    }
                });
            }, {
                threshold: 0.1,
                rootMargin: '-50px'
            });

            sections.forEach(function (section) {
                section.style.transition = 'all 0.8s cubic-bezier(0.4, 0, 0.2, 1)';
                sectionObserver.observe(section);
            });
        }
    }

    // ==========================================
    // ENHANCED CAROUSEL TRANSITIONS
    // ==========================================
    function enhanceCarouselTransitions() {
        const heroCarousel = document.querySelector('#heroCarousel');
        if (!heroCarousel) return;

        heroCarousel.addEventListener('slide.bs.carousel', function (e) {
            const nextSlide = e.relatedTarget;
            const currentSlide = e.target.querySelector('.carousel-item.active');

            // Add fade effect
            if (currentSlide) {
                currentSlide.style.transition = 'opacity 0.8s ease-in-out';
                currentSlide.style.opacity = '0.5';
            }

            if (nextSlide) {
                nextSlide.style.transition = 'opacity 0.8s ease-in-out';
                nextSlide.style.opacity = '1';
            }
        });

        heroCarousel.addEventListener('slid.bs.carousel', function (e) {
            const activeSlide = e.target.querySelector('.carousel-item.active');
            if (activeSlide) {
                activeSlide.style.opacity = '1';
            }
        });
    }

    // ==========================================
    // BUTTON RIPPLE EFFECT
    // ==========================================
    function initButtonRipples() {
        const buttons = document.querySelectorAll('.btn, button');

        buttons.forEach(function (button) {
            button.addEventListener('click', function (e) {
                const ripple = document.createElement('span');
                const rect = this.getBoundingClientRect();
                const size = Math.max(rect.width, rect.height);
                const x = e.clientX - rect.left - size / 2;
                const y = e.clientY - rect.top - size / 2;

                ripple.style.width = ripple.style.height = size + 'px';
                ripple.style.left = x + 'px';
                ripple.style.top = y + 'px';
                ripple.classList.add('ripple-effect');

                const existingRipple = this.querySelector('.ripple-effect');
                if (existingRipple) {
                    existingRipple.remove();
                }

                this.appendChild(ripple);

                setTimeout(function () {
                    ripple.remove();
                }, 600);
            });
        });

        // Add ripple styles dynamically
        const style = document.createElement('style');
        style.textContent = `
            .ripple-effect {
                position: absolute;
                border-radius: 50%;
                background: rgba(255, 255, 255, 0.6);
                transform: scale(0);
                animation: ripple-animation 0.6s ease-out;
                pointer-events: none;
            }
            @keyframes ripple-animation {
                to {
                    transform: scale(2);
                    opacity: 0;
                }
            }
        `;
        document.head.appendChild(style);
    }

    // ==========================================
    // SOCIAL FLOAT SCROLL EFFECT
    // ==========================================
    function initSocialFloatEffect() {
        const socialFloat = document.querySelector('.social-float');
        if (!socialFloat) return;

        window.addEventListener('scroll', function () {
            const scrolled = window.pageYOffset;
            if (scrolled > 300) {
                socialFloat.style.opacity = '1';
                socialFloat.style.transform = 'translateY(-50%) translateX(0)';
            } else {
                socialFloat.style.opacity = '0.7';
                socialFloat.style.transform = 'translateY(-50%) translateX(10px)';
            }
        });

        // Set initial transition
        socialFloat.style.transition = 'all 0.3s ease';
    }

    // ==========================================
    // INITIALIZE ALL ENHANCEMENTS
    // ==========================================
    function init() {
        initScrollAnimations();
        initParallaxEffect();
        initCardRevealAnimations();
        enhanceMarqueeAnimation();
        initIconAnimations();
        initGalleryLazyLoad();
        initSectionTransitions();
        enhanceCarouselTransitions();
        initButtonRipples();
        initSocialFloatEffect();

        console.log('Home page animations initialized successfully');
    }

    // Execute on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
