// Smooth scrolling for anchor links
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        
        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        });
    });
});

// Login button animation
const loginBtn = document.querySelector('.login-btn');
if (loginBtn) {
    loginBtn.addEventListener('mouseenter', () => {
        loginBtn.style.backgroundPosition = 'right center';
    });
    
    loginBtn.addEventListener('mouseleave', () => {
        loginBtn.style.backgroundPosition = 'left center';
    });
}

// Form submission


// Animate feature cards on scroll
const featureCards = document.querySelectorAll('.feature-card');

function checkScroll() {
    featureCards.forEach(card => {
        const cardTop = card.getBoundingClientRect().top;
        const windowHeight = window.innerHeight;
        
        if (cardTop < windowHeight - 100) {
            card.style.opacity = '1';
            card.style.transform = 'translateY(0)';
        }
    });
}

// Initialize
window.addEventListener('load', () => {
    featureCards.forEach(card => {
        card.style.opacity = '0';
        card.style.transform = 'translateY(20px)';
        card.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
    });
    
    checkScroll();
});

window.addEventListener('scroll', checkScroll); 