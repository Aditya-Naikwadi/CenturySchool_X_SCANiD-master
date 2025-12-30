/*
 * Century Rayon School - Enhanced Home Page Animations
 * Version: 2.0
 * Description: Enhanced scroll-triggered animations with mobile optimization
 */

(function () {
    'use strict';

    // ==========================================
    // MOBILE DETECTION
    // ==========================================
    const isMobile = window.innerWidth < 768;
    const isTablet = window.innerWidth >= 768 && window.innerWidth < 1025;
    const prefersReducedMotion = window.matchMedia('(prefers-reduced-motion: reduce)').matches;

    // ==========================================
    // RESPONSIVE SOCIAL ICONS
    // ==========================================
    function initResponsiveSocial() {
        const desktopSocial = document.querySelector('.social-float');
        const mobileSocial = document.querySelector('.mobile-social-footer');

        if (isMobile) {
            if (desktopSocial) desktopSocial.style.display = 'none';
            if (mobileSocial) mobileSocial.style.display = 'flex';
        } else {
            if (desktopSocial) desktopSocial.style.display = 'flex';
            if (mobileSocial) mobileSocial.style.display = 'none';
        }

        // Update on window resize
        window.addEventListener('resize', function () {
            const currentMobile = window.innerWidth < 768;
            if (currentMobile) {
                if (desktopSocial) desktopSocial.style.display = 'none';
                if (mobileSocial) mobileSocial.style.display = 'flex';
            } else {
                if (desktopSocial) desktopSocial.style.display = 'flex';
                if (mobileSocial) mobileSocial.style.display = 'none';
            }
        });
    }

    // ==========================================
    // IMPROVED MODAL TIMING FOR MOBILE
    // ==========================================
    function initResponsiveModal() {
        const admissionModal = document.getElementById('admissionFormPopup');
        if (!admissionModal) return;

        // Don't auto-show on mobile, too intrusive
        if (isMobile) {
            // Store that modal was suppressed
            sessionStorage.setItem('modalSuppressed', 'true');

            // Could add a floating button to open modal on mobile
            // For now, just don't auto-show
        } else {
            // Show after 5 seconds on desktop/tablet
            setTimeout(function () {
                if (!sessionStorage.getItem('modalDismissed')) {
                    const modalInstance = new bootstrap.Modal(admissionModal);
                    modalInstance.show();
                }
            }, 5000);
        }

        // Track when user dismisses
        admissionModal.addEventListener('hidden.bs.modal', function () {
            sessionStorage.setItem('modalDismissed', 'true');
        });
    }

    // ==========================================
    // INTERSECTION OBSERVER FOR SCROLL ANIMATIONS
    // ==========================================
    function initScrollAnimations() {
        if (!('IntersectionObserver' in window) || prefersReducedMotion) {
            // Fallback: show all content immediately
            const sections = document.querySelectorAll('.section-wrapper');
            sections.forEach(function (section) {
                section.classList.add('animate-in');
                section.style.opacity = '1';
                section.style.transform = 'translateY(0)';
            });
            return;
        }

        const observerOptions = {
            root: null,
            rootMargin: isMobile ? '-20px' : '-50px', // Trigger earlier on mobile
            threshold: isMobile ? 0.05 : 0.1
        };

        const observer = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    entry.target.classList.add('animate-in');
                    observer.unobserve(entry.target);
                }
            });
        }, observerOptions);

        const sections = document.querySelectorAll('.section-wrapper');
        sections.forEach(function (section) {
            observer.observe(section);
        });
    }

    // ==========================================
    // PARALLAX EFFECT (DESKTOP ONLY)
    // ==========================================
    function initParallaxEffect() {
        if (isMobile || prefersReducedMotion) return; // Skip on mobile for performance

        const heroCarousel = document.querySelector('#heroCarousel');
        if (!heroCarousel) return;

        let ticking = false;

        window.addEventListener('scroll', function () {
            if (!ticking) {
                window.requestAnimationFrame(function () {
                    const scrolled = window.pageYOffset;
                    const parallaxElements = heroCarousel.querySelectorAll('.carousel-item.active img');

                    parallaxElements.forEach(function (element) {
                        const speed = 0.5;
                        element.style.transform = 'translateY(' + (scrolled * speed) + 'px) scale(1.05)';
                    });

                    ticking = false;
                });

                ticking = true;
            }
        }, { passive: true });
    }

    // ==========================================
    // SMOOTH REVEAL FOR CARDS
    // ==========================================
    function initCardRevealAnimations() {
        if (!('IntersectionObserver' in window) || prefersReducedMotion) return;

        const cardObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry, index) {
                if (entry.isIntersecting) {
                    const delay = isMobile ? index * 50 : index * 100;
                    setTimeout(function () {
                        entry.target.style.opacity = '1';
                        entry.target.style.transform = 'translateY(0)';
                    }, delay);
                    cardObserver.unobserve(entry.target);
                }
            });
        }, {
            threshold: 0.2
        });

        const cards = document.querySelectorAll('.announcement-card, .event-card, .notice-card');
        cards.forEach(function (card) {
            card.style.opacity = '0';
            card.style.transform = 'translateY(30px)';
            card.style.transition = 'all 0.6s ease-out';
            cardObserver.observe(card);
        });
    }

    // ==========================================
    // ENHANCED MARQUEE WITH TOUCH SUPPORT
    // ==========================================
    function enhanceMarqueeAnimation() {
        const marquees = document.querySelectorAll('marquee');

        marquees.forEach(function (marquee) {
            // Mouse wheel support
            marquee.addEventListener('wheel', function (e) {
                e.preventDefault();
                const delta = e.deltaY;
                this.scrollTop += delta;
            }, { passive: false });

            // Touch support for mobile
            let startY = 0;
            let startScrollTop = 0;

            marquee.addEventListener('touchstart', function (e) {
                startY = e.touches[0].pageY;
                startScrollTop = this.scrollTop;
                this.stop();
            }, { passive: true });

            marquee.addEventListener('touchmove', function (e) {
                const currentY = e.touches[0].pageY;
                const diff = startY - currentY;
                this.scrollTop = startScrollTop + diff;
            }, { passive: true });

            marquee.addEventListener('touchend', function () {
                this.start();
            });
        });
    }

    // ==========================================
    // TOUCH-OPTIMIZED CAROUSEL SWIPE
    // ==========================================
    function initCarouselSwipe() {
        if (!isMobile) return; // Only for mobile

        const carousels = document.querySelectorAll('.carousel');

        carousels.forEach(function (carousel) {
            let startX = 0;
            let startY = 0;
            let moving = false;

            carousel.addEventListener('touchstart', function (e) {
                startX = e.touches[0].pageX;
                startY = e.touches[0].pageY;
                moving = true;
            }, { passive: true });

            carousel.addEventListener('touchmove', function (e) {
                if (!moving) return;

                const currentX = e.touches[0].pageX;
                const currentY = e.touches[0].pageY;
                const diffX = startX - currentX;
                const diffY = startY - currentY;

                // If horizontal swipe is more significant than vertical
                if (Math.abs(diffX) > Math.abs(diffY) && Math.abs(diffX) > 50) {
                    const carouselInstance = bootstrap.Carousel.getInstance(carousel);
                    if (carouselInstance) {
                        if (diffX > 0) {
                            carouselInstance.next();
                        } else {
                            carouselInstance.prev();
                        }
                    }
                    moving = false;
                }
            }, { passive: true });

            carousel.addEventListener('touchend', function () {
                moving = false;
            });
        });
    }

    // ==========================================
    // ICON ANIMATION ON HOVER (DESKTOP ONLY)
    // ==========================================
    function initIconAnimations() {
        if (isMobile) return; // Skip on mobile

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
    // GALLERY IMAGE LAZY LOADING
    // ==========================================
    function initGalleryLazyLoad() {
        if (!('IntersectionObserver' in window)) return;

        const imageObserver = new IntersectionObserver(function (entries) {
            entries.forEach(function (entry) {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.style.opacity = '0';
                    img.style.transition = 'opacity 0.6s ease-in-out';

                    if (img.complete) {
                        img.style.opacity = '1';
                    } else {
                        img.addEventListener('load', function () {
                            this.style.opacity = '1';
                        });
                    }

                    imageObserver.unobserve(img);
                }
            });
        }, {
            rootMargin: '50px'
        });

        const galleryImages = document.querySelectorAll('#carouselGallery img');
        galleryImages.forEach(function (img) {
            imageObserver.observe(img);
        });
    }

    // ==========================================
    // ENHANCED CAROUSEL TRANSITIONS
    // ==========================================
    function enhanceCarouselTransitions() {
        const heroCarousel = document.querySelector('#heroCarousel');
        if (!heroCarousel || prefersReducedMotion) return;

        heroCarousel.addEventListener('slide.bs.carousel', function (e) {
            const nextSlide = e.relatedTarget;
            const currentSlide = e.target.querySelector('.carousel-item.active');

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

        // Add ripple styles
        if (!document.getElementById('ripple-styles')) {
            const style = document.createElement('style');
            style.id = 'ripple-styles';
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
    }

    // ==========================================
    // SOCIAL FLOAT SCROLL EFFECT (DESKTOP)
    // ==========================================
    function initSocialFloatEffect() {
        if (isMobile) return; // Skip on mobile

        const socialFloat = document.querySelector('.social-float');
        if (!socialFloat) return;

        let ticking = false;

        window.addEventListener('scroll', function () {
            if (!ticking) {
                window.requestAnimationFrame(function () {
                    const scrolled = window.pageYOffset;
                    if (scrolled > 300) {
                        socialFloat.style.opacity = '1';
                        socialFloat.style.transform = 'translateY(-50%) translateX(0)';
                    } else {
                        socialFloat.style.opacity = '0.7';
                        socialFloat.style.transform = 'translateY(-50%) translateX(10px)';
                    }
                    ticking = false;
                });
                ticking = true;
            }
        }, { passive: true });

        socialFloat.style.transition = 'all 0.3s ease';
    }

    // ==========================================
    // PERFORMANCE MONITORING
    // ==========================================
    function logPerformance() {
        if (window.performance && window.performance.timing) {
            window.addEventListener('load', function () {
                setTimeout(function () {
                    const perfData = window.performance.timing;
                    const pageLoadTime = perfData.loadEventEnd - perfData.navigationStart;
                    console.log('Page Load Time: ' + pageLoadTime + 'ms');
                }, 0);
            });
        }
    }

    // ==========================================
    // INITIALIZE ALL ENHANCEMENTS
    // ==========================================
    function init() {
        // Core functionality
        initResponsiveSocial();
        initResponsiveModal();
        initScrollAnimations();

        // Progressive enhancement
        if (!isMobile || !prefersReducedMotion) {
            initParallaxEffect();
            initIconAnimations();
            initSocialFloatEffect();
        }

        // Universal enhancements
        initCardRevealAnimations();
        enhanceMarqueeAnimation();
        initGalleryLazyLoad();
        enhanceCarouselTransitions();
        initButtonRipples();

        // Mobile-specific
        if (isMobile) {
            initCarouselSwipe();
        }

        // Performance monitoring
        if (window.location.hostname === 'localhost') {
            logPerformance();
        }

        console.log('Home page enhancements initialized (v2.0) - Mobile: ' + isMobile + ', Tablet: ' + isTablet);
    }

    // Execute on DOM ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }
})();
