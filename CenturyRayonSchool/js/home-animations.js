// Home Animations v2.0 - Fixed null safety issues
// Handles carousel initialization and modal popup behavior

document.addEventListener('DOMContentLoaded', function () {

    // ========== AUTO-ADVANCE HERO CAROUSEL ==========
    const heroCarousel = document.getElementById('heroCarousel');
    if (heroCarousel) {
        const bsCarousel = new bootstrap.Carousel(heroCarousel, {
            interval: 4000,
            ride: 'carousel',
            pause: 'hover'
        });
    }

    // ========== AUTO-ADVANCE GALLERY CAROUSEL ==========
    const galleryCarousel = document.getElementById('carouselGallery');
    if (galleryCarousel) {
        const bsGalleryCarousel = new bootstrap.Carousel(galleryCarousel, {
            interval: 4000,
            ride: 'carousel',
            pause: 'hover'
        });
    }

    // ========== ADMISSION MODAL AUTO-POPUP (Commented out by default) ==========
    // Uncomment the code below to enable auto-popup
    /*
    const admissionModal = document.getElementById('admissionFormPopup');
    if (admissionModal) {
      const modalInstance = new bootstrap.Modal(admissionModal);
      
      // Show modal after 3 seconds
      setTimeout(function() {
        modalInstance.show();
      }, 3000);
    }
    */

    // ========== FORM VALIDATION ==========
    const admissionForm = document.getElementById('admissionForm');
    if (admissionForm) {
        admissionForm.addEventListener('submit', function (e) {
            e.preventDefault();

            // Basic validation
            const inputs = admissionForm.querySelectorAll('[required]');
            let isValid = true;

            inputs.forEach(function (input) {
                if (!input.value.trim()) {
                    isValid = false;
                    input.classList.add('is-invalid');
                } else {
                    input.classList.remove('is-invalid');
                }
            });

            if (isValid) {
                alert('Thank you for your enquiry! We will contact you soon.');
                admissionForm.reset();

                // Close modal if it exists
                const modalElement = document.getElementById('admissionFormPopup');
                if (modalElement) {
                    const modalInstance = bootstrap.Modal.getInstance(modalElement);
                    if (modalInstance) {
                        modalInstance.hide();
                    }
                }
            }
        });
    }

    // ========== SMOOTH SCROLL FOR ANCHOR LINKS ==========
    document.querySelectorAll('a[href^="#"]').forEach(function (anchor) {
        anchor.addEventListener('click', function (e) {
            const href = this.getAttribute('href');
            if (href !== '#' && href.length > 1) {
                e.preventDefault();
                const target = document.querySelector(href);
                if (target) {
                    target.scrollIntoView({
                        behavior: 'smooth',
                        block: 'start'
                    });
                }
            }
        });
    });

    // ========== MOBILE SOCIAL FOOTER TOGGLE ==========
    function toggleSocialBars() {
        const mobileSocialFooter = document.querySelector('.mobile-social-footer');
        const socialFloat = document.querySelector('.social-float');

        if (window.innerWidth <= 768) {
            // Mobile: show bottom bar, hide side bar
            if (mobileSocialFooter) {
                mobileSocialFooter.style.display = 'flex';
            }
            if (socialFloat) {
                socialFloat.style.display = 'none';
            }
        } else {
            // Desktop: show side bar, hide bottom bar
            if (mobileSocialFooter) {
                mobileSocialFooter.style.display = 'none';
            }
            if (socialFloat) {
                socialFloat.style.display = 'flex';
            }
        }
    }

    // Run on load
    toggleSocialBars();

    // Run on resize
    window.addEventListener('resize', toggleSocialBars);

    // ========== INTERSECTION OBSERVER FOR SCROLL ANIMATIONS ==========
    // Disabled for now - was causing content to be invisible on page load
    // Can be re-enabled once proper implementation is in place

    /*
    const observerOptions = {
      threshold: 0.1,
      rootMargin: '0px 0px -100px 0px'
    };
  
    const observer = new IntersectionObserver(function(entries) {
      entries.forEach(function(entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add('animate-in');
        }
      });
    }, observerOptions);
  
    // Observe all cards and sections
    const animateElements = document.querySelectorAll('.announcement-card, .event-card, .notice-card, .card');
    animateElements.forEach(function(el) {
      if (el) {
        observer.observe(el);
      }
    });
    */

});

// ========== CONSOLE LOG TO CONFIRM SCRIPT LOADED ==========
console.log('✅ home-animations.js v2.0 loaded successfully');
