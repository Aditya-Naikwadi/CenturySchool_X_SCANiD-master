/*
 * Century Rayon School - Enhanced Interactions JavaScript
 * Version: 2.0
 * Description: Progressive JS enhancements for better UX
 */

(function () {
    'use strict';

    // ==========================================
    // MOBILE NAVIGATION
    // ==========================================
    function initMobileNav() {
        const toggle = document.querySelector('.modern-mobile-toggle');
        const mobileNav = document.querySelector('.mobile-nav-modern');
        const backdrop = document.querySelector('.mobile-nav-backdrop');
        const closeBtn = document.querySelector('.mobile-nav-close');

        if (!toggle || !mobileNav) return;

        function openNav() {
            mobileNav.classList.add('open');
            if (backdrop) backdrop.classList.add('show');
            document.body.style.overflow = 'hidden';
        }

        function closeNav() {
            mobileNav.classList.remove('open');
            if (backdrop) backdrop.classList.remove('show');
            document.body.style.overflow = '';
        }

        toggle.addEventListener('click', openNav);
        if (closeBtn) closeBtn.addEventListener('click', closeNav);
        if (backdrop) backdrop.addEventListener('click', closeNav);
    }

    // ==========================================
    // SCROLL TO TOP BUTTON
    // ==========================================
    function initScrollToTop() {
        const scrollBtn = document.querySelector('.scroll-to-top');
        if (scrollBtn) {
            window.addEventListener('scroll', function () {
                if (window.scrollY > 300) {
                    scrollBtn.classList.add('visible');
                } else {
                    scrollBtn.classList.remove('visible');
                }
            });

            scrollBtn.addEventListener('click', function () {
                window.scrollTo({
                    top: 0,
                    behavior: 'smooth'
                });
            });
        }
    }

    // ==========================================
    // HEADER SCROLL EFFECT
    // ==========================================
    function initHeaderScroll() {
        const header = document.querySelector('.modern-header');
        if (header) {
            window.addEventListener('scroll', function () {
                if (window.scrollY > 50) {
                    header.classList.add('scrolled');
                } else {
                    header.classList.remove('scrolled');
                }
            });
        }
    }

    // ==========================================
    // LAZY LOADING IMAGES
    // ==========================================
    function initLazyLoading() {
        if ('IntersectionObserver' in window) {
            const imageObserver = new IntersectionObserver(function (entries, observer) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        const img = entry.target;
                        const src = img.getAttribute('data-src');
                        if (src) {
                            img.src = src;
                            img.classList.add('loaded');
                            observer.unobserve(img);
                        }
                    }
                });
            });

            const lazyImages = document.querySelectorAll('img[data-src]');
            lazyImages.forEach(function (img) {
                imageObserver.observe(img);
            });
        } else {
            // Fallback for browsers that don't support IntersectionObserver
            const lazyImages = document.querySelectorAll('img[data-src]');
            lazyImages.forEach(function (img) {
                img.src = img.getAttribute('data-src');
            });
        }
    }

    // ==========================================
    // ACCORDION FUNCTIONALITY
    // ==========================================
    function initAccordion() {
        const accordionHeaders = document.querySelectorAll('.accordion-header-modern');

        accordionHeaders.forEach(function (header) {
            header.addEventListener('click', function () {
                const isActive = this.classList.contains('active');

                // Close all accordions
                accordionHeaders.forEach(function (h) {
                    h.classList.remove('active');
                    const body = h.nextElementSibling;
                    if (body && body.classList.contains('accordion-body-modern')) {
                        body.classList.remove('show');
                    }
                });

                // Open clicked accordion if it wasn't active
                if (!isActive) {
                    this.classList.add('active');
                    const body = this.nextElementSibling;
                    if (body && body.classList.contains('accordion-body-modern')) {
                        body.classList.add('show');
                    }
                }
            });
        });
    }

    // ==========================================
    // FORM VALIDATION ENHANCEMENT
    // ==========================================
    function initFormValidation() {
        const forms = document.querySelectorAll('.form-modern');

        forms.forEach(function (form) {
            form.addEventListener('submit', function (e) {
                let isValid = true;
                const inputs = form.querySelectorAll('.form-control-modern[required]');

                inputs.forEach(function (input) {
                    const feedback = input.nextElementSibling;

                    if (!input.value.trim()) {
                        input.classList.add('is-invalid');
                        input.classList.remove('is-valid');
                        if (feedback && feedback.classList.contains('form-feedback-modern')) {
                            feedback.classList.add('invalid');
                            feedback.textContent = 'This field is required';
                        }
                        isValid = false;
                    } else {
                        input.classList.remove('is-invalid');
                        input.classList.add('is-valid');
                        if (feedback && feedback.classList.contains('form-feedback-modern')) {
                            feedback.classList.remove('invalid');
                            feedback.textContent = '';
                        }
                    }
                });

                if (!isValid) {
                    e.preventDefault();
                }
            });

            // Real-time validation
            const inputs = form.querySelectorAll('.form-control-modern');
            inputs.forEach(function (input) {
                input.addEventListener('blur', function () {
                    if (this.hasAttribute('required')) {
                        const feedback = this.nextElementSibling;

                        if (!this.value.trim()) {
                            this.classList.add('is-invalid');
                            this.classList.remove('is-valid');
                            if (feedback && feedback.classList.contains('form-feedback-modern')) {
                                feedback.classList.add('invalid');
                                feedback.textContent = 'This field is required';
                            }
                        } else {
                            this.classList.remove('is-invalid');
                            this.classList.add('is-valid');
                            if (feedback && feedback.classList.contains('form-feedback-modern')) {
                                feedback.classList.remove('invalid');
                                feedback.textContent = '';
                            }
                        }
                    }
                });
            });
        });
    }

    // ==========================================
    // MODAL FUNCTIONALITY
    // ==========================================
    function initModals() {
        const modals = document.querySelectorAll('.modal-modern');

        modals.forEach(function (modal) {
            const closeBtn = modal.querySelector('.modal-close-modern');
            const backdrop = modal.querySelector('.modal-backdrop-modern');

            if (closeBtn) {
                closeBtn.addEventListener('click', function () {
                    modal.classList.remove('show');
                    document.body.style.overflow = '';
                });
            }

            if (backdrop) {
                backdrop.addEventListener('click', function () {
                    modal.classList.remove('show');
                    document.body.style.overflow = '';
                });
            }
        });
    }

    // ==========================================
    // SMOOTH ANCHOR SCROLLING
    // ==========================================
    function initSmoothScroll() {
        const links = document.querySelectorAll('a[href^="#"]');

        links.forEach(function (link) {
            link.addEventListener('click', function (e) {
                const href = this.getAttribute('href');
                if (href === '#') return;

                const target = document.querySelector(href);
                if (target) {
                    e.preventDefault();
                    const offsetTop = target.offsetTop - 80; // Account for fixed header

                    window.scrollTo({
                        top: offsetTop,
                        behavior: 'smooth'
                    });
                }
            });
        });
    }

    // ==========================================
    // ANIMATION ON SCROLL
    // ==========================================
    function initScrollAnimations() {
        if ('IntersectionObserver' in window) {
            const animateElements = document.querySelectorAll('[data-animate]');

            const observer = new IntersectionObserver(function (entries) {
                entries.forEach(function (entry) {
                    if (entry.isIntersecting) {
                        const animationClass = entry.target.getAttribute('data-animate');
                        entry.target.classList.add(animationClass);
                        observer.unobserve(entry.target);
                    }
                });
            }, {
                threshold: 0.1
            });

            animateElements.forEach(function (el) {
                observer.observe(el);
            });
        }
    }

    // ==========================================
    // INITIALIZE ALL ON DOM READY
    // ==========================================
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

    function init() {
        initMobileNav();
        initScrollToTop();
        initHeaderScroll();
        initLazyLoading();
        initAccordion();
        initFormValidation();
        initModals();
        initSmoothScroll();
        initScrollAnimations();
    }
})();
