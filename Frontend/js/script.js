// Funcionalidades específicas de la landing page
document.addEventListener('DOMContentLoaded', function() {
    initializeLandingPage();
});

function initializeLandingPage() {
    // Inicializar navegación suave
    initializeSmoothNavigation();
    
    // Inicializar botones de la landing page
    initializeLandingButtons();
    
    // Inicializar efectos de scroll
    initializeScrollEffects();
}

// Navegación suave para enlaces internos
function initializeSmoothNavigation() {
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            e.preventDefault();
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);
            
            if (targetElement) {
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Botones de la landing page
function initializeLandingButtons() {
    const loginBtn = document.querySelector('.nav-actions .btn-secondary');
    const registerBtn = document.querySelector('.nav-actions .btn-primary');
    const scheduleBtn = document.querySelector('.hero-actions .btn-primary');
    const moreInfoBtn = document.querySelector('.hero-actions .btn-outline');
    
    if (loginBtn) {
        loginBtn.addEventListener('click', function() {
            showNotification('Redirigiendo al login...', 'info');
            // Aquí se redirigiría a la página de login
            setTimeout(() => {
                window.location.href = 'patient.html'; // Demo: redirigir a vista del paciente
            }, 1000);
        });
    }
    
    if (registerBtn) {
        registerBtn.addEventListener('click', function() {
            showNotification('Redirigiendo al registro...', 'info');
            // Aquí se redirigiría a la página de registro
            setTimeout(() => {
                window.location.href = 'patient.html'; // Demo: redirigir a vista del paciente
            }, 1000);
        });
    }
    
    if (scheduleBtn) {
        scheduleBtn.addEventListener('click', function() {
            showNotification('Redirigiendo para agendar consulta...', 'info');
            // Aquí se redirigiría a la página de agendar consulta
            setTimeout(() => {
                window.location.href = 'patient.html'; // Demo: redirigir a vista del paciente
            }, 1000);
        });
    }
    
    if (moreInfoBtn) {
        moreInfoBtn.addEventListener('click', function() {
            // Scroll suave a la sección de beneficios
            document.getElementById('servicios').scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        });
    }
}

// Efectos de scroll
function initializeScrollEffects() {
    // Mostrar/ocultar header al hacer scroll
    let lastScrollTop = 0;
    const header = document.querySelector('.header');
    
    window.addEventListener('scroll', function() {
        const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
        
        if (scrollTop > lastScrollTop && scrollTop > 100) {
            // Scrolling down
            header.style.transform = 'translateY(-100%)';
        } else {
            // Scrolling up
            header.style.transform = 'translateY(0)';
        }
        
        lastScrollTop = scrollTop;
    });
    
    // Animaciones al hacer scroll
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.style.opacity = '1';
                entry.target.style.transform = 'translateY(0)';
            }
        });
    }, observerOptions);
    
    // Observar elementos para animaciones
    const animatedElements = document.querySelectorAll('.benefit-card, .step');
    animatedElements.forEach(el => {
        el.style.opacity = '0';
        el.style.transform = 'translateY(30px)';
        el.style.transition = 'opacity 0.6s ease, transform 0.6s ease';
        observer.observe(el);
    });
}

// Sistema de notificaciones
function showNotification(message, type = 'info') {
    // Crear elemento de notificación
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <div class="notification-content">
            <i class="fas fa-info-circle"></i>
            <span>${message}</span>
        </div>
    `;
    
    // Agregar estilos si no existen
    if (!document.querySelector('#notification-styles')) {
        const styles = document.createElement('style');
        styles.id = 'notification-styles';
        styles.textContent = `
            .notification {
                position: fixed;
                top: 20px;
                right: 20px;
                background: white;
                border-radius: 8px;
                box-shadow: 0 4px 20px rgba(0,0,0,0.15);
                padding: 1rem;
                z-index: 1001;
                animation: slideIn 0.3s ease-out;
                max-width: 300px;
            }
            
            .notification-content {
                display: flex;
                align-items: center;
                gap: 0.5rem;
                color: #1f2937;
            }
            
            .notification-content i {
                color: #2563eb;
            }
            
            @keyframes slideIn {
                from {
                    transform: translateX(100%);
                    opacity: 0;
                }
                to {
                    transform: translateX(0);
                    opacity: 1;
                }
            }
            
            .notification-info {
                border-left: 4px solid #2563eb;
            }
            
            .notification-success {
                border-left: 4px solid #10b981;
            }
            
            .notification-error {
                border-left: 4px solid #dc2626;
            }
        `;
        document.head.appendChild(styles);
    }
    
    // Agregar al DOM
    document.body.appendChild(notification);
    
    // Remover después de 3 segundos
    setTimeout(() => {
        notification.style.animation = 'slideIn 0.3s ease-out reverse';
        setTimeout(() => {
            if (notification.parentNode) {
                notification.parentNode.removeChild(notification);
            }
        }, 300);
    }, 3000);
}

// Funciones para futuras implementaciones con backend
function handleLogin() {
    // Placeholder para funcionalidad de login
    console.log('Login functionality will be implemented here');
    showNotification('Funcionalidad de login en desarrollo', 'info');
}

function handleRegister() {
    // Placeholder para funcionalidad de registro
    console.log('Register functionality will be implemented here');
    showNotification('Funcionalidad de registro en desarrollo', 'info');
}

function handleScheduleAppointment() {
    // Placeholder para funcionalidad de agendar cita
    console.log('Schedule appointment functionality will be implemented here');
    showNotification('Funcionalidad de agendar cita en desarrollo', 'info');
}

// Exportar funciones para uso global
window.LandingPage = {
    showNotification,
    handleLogin,
    handleRegister,
    handleScheduleAppointment
};