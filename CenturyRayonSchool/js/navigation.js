/**
 * Navigation JavaScript
 * Handles mobile menu toggle and scroll-to-top functionality
 * Version: 1.0
 */

(function () {
    'use strict';

    // Mobile Menu Elements
    let mobileNav = null;
    let mobileBackdrop = null;
    let mobileToggleBtn = null;
    let mobileCloseBtn = null;
    let scrollTopBtn = null;

    /**
     * Toggle mobile navigation menu
     */
    function toggleMobileMenu() {
        if (!mobileNav || !mobileBackdrop) return;

        const isOpen = mobileNav.classList.contains('show');

        if (isOpen) {
            closeMobileMenu();
        } else {
            openMobileMenu();
        }
    }

    /**
     * Open mobile menu
     */
    function openMobileMenu() {
        if (!mobileNav || !mobileBackdrop) return;

        mobileNav.classList.add('show');
        mobileBackdrop.classList.add('show');
        document.body.classList.add('mobile-menu-open');
    }

    /**
     * Close mobile menu
     */
    function closeMobileMenu() {
        if (!mobileNav || !mobileBackdrop) return;

        mobileNav.classList.remove('show');
        mobileBackdrop.classList.remove('show');
        document.body.classList.remove('mobile-menu-open');
    }

    /**
     * Handle backdrop click
     */
    function handleBackdropClick(e) {
        if (e.target === mobileBackdrop) {
            closeMobileMenu();
        }
    }

    /**
     * Handle scroll for scroll-to-top button
     */
    function handleScroll() {
        if (!scrollTopBtn) return;

        if (window.pageYOffset > 300) {
            scrollTopBtn.classList.add('visible');
        } else {
            scrollTopBtn.classList.remove('visible');
        }
    }

    /**
     * Scroll to top smoothly
     */
    function scrollToTop(e) {
        e.preventDefault();
        window.scrollTo({
            top: 0,
            behavior: 'smooth'
        });
    }

    /**
     * Close mobile menu when clicking a link
     */
    function handleMenuLinkClick() {
        closeMobileMenu();
    }

    /**
     * Initialize navigation
     */
    function initNavigation() {
        // Get elements
        mobileNav = document.querySelector('.mobile-nav-modern');
        mobileBackdrop = document.querySelector('.mobile-nav-backdrop');
        mobileToggleBtn = document.querySelector('.modern-mobile-toggle');
        mobileCloseBtn = document.querySelector('.mobile-nav-close');
        scrollTopBtn = document.getElementById('scrollTopBtn');

        // Mobile menu toggle
        if (mobileToggleBtn) {
            mobileToggleBtn.addEventListener('click', toggleMobileMenu);
        }

        // Mobile menu close button
        if (mobileCloseBtn) {
            mobileCloseBtn.addEventListener('click', closeMobileMenu);
        }

        // Backdrop click
        if (mobileBackdrop) {
            mobileBackdrop.addEventListener('click', handleBackdropClick);
        }

        // Close menu when clicking a link
        if (mobileNav) {
            const menuLinks = mobileNav.querySelectorAll('a');
            menuLinks.forEach(link => {
                link.addEventListener('click', handleMenuLinkClick);
            });
        }

        // Scroll-to-top button
        if (scrollTopBtn) {
            scrollTopBtn.addEventListener('click', scrollToTop);

            // Handle scroll with throttling
            let scrollTimeout;
            window.addEventListener('scroll', function () {
                if (scrollTimeout) {
                    window.cancelAnimationFrame(scrollTimeout);
                }
                scrollTimeout = window.requestAnimationFrame(handleScroll);
            }, { passive: true });
        }

        // Close menu on ESC key
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && mobileNav && mobileNav.classList.contains('show')) {
                closeMobileMenu();
            }
        });

        console.log('Navigation initialized successfully');
    }

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initNavigation);
    } else {
        initNavigation();
    }

})();
