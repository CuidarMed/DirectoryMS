// Funcionalidades específicas del panel médico
document.addEventListener('DOMContentLoaded', function() {
    initializeDoctorPanel();
});

function initializeDoctorPanel() {
    // Inicializar navegación del sidebar
    initializeSidebarNavigation();
    
    // Inicializar botones de atención
    initializeAttendButtons();
    
    // Inicializar acciones rápidas
    initializeQuickActions();
    
    // Inicializar modal de receta
    initializePrescriptionModal();
    
    // Simular datos en tiempo real
    startDataSimulation();
}

// Navegación del sidebar
function initializeSidebarNavigation() {
    const navItems = document.querySelectorAll('.sidebar-nav .nav-item');
    
    navItems.forEach(item => {
        item.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Remover clase active de todos los elementos
            navItems.forEach(navItem => navItem.classList.remove('active'));
            
            // Agregar clase active al elemento clickeado
            this.classList.add('active');
            
            // Obtener la sección
            const section = this.getAttribute('data-section');
            handleSectionNavigation(section);
        });
    });
}

function handleSectionNavigation(section) {
    const messages = {
        'agenda': 'Navegando a la agenda médica',
        'consultas': 'Navegando a las consultas de hoy',
        'historia': 'Navegando al historial clínico',
        'recetas': 'Navegando a la gestión de recetas',
        'configuracion': 'Navegando a la configuración'
    };
    
    if (messages[section]) {
        showNotification(messages[section]);
    }
}

// Botones de atención
function initializeAttendButtons() {
    const attendButtons = document.querySelectorAll('.attend-btn');
    
    attendButtons.forEach(button => {
        button.addEventListener('click', function() {
            const patientName = this.getAttribute('data-patient');
            attendConsultation(patientName);
        });
    });
}

function attendConsultation(patientName) {
    // Simular inicio de videollamada
    showNotification(`Iniciando consulta con ${patientName}...`);
    
    // Cambiar estado del botón
    const button = document.querySelector(`[data-patient="${patientName}"]`);
    if (button) {
        button.innerHTML = '<i class="fas fa-video"></i> En consulta';
        button.classList.add('in-consultation');
        button.disabled = true;
    }
    
    // Actualizar contador de consultas activas
    updateActiveConsultations(1);
    
    // Simular finalización de consulta después de 5 segundos
    setTimeout(() => {
        finishConsultation(patientName);
    }, 5000);
}

function finishConsultation(patientName) {
    showNotification(`Consulta con ${patientName} finalizada`);
    
    // Restaurar botón
    const button = document.querySelector(`[data-patient="${patientName}"]`);
    if (button) {
        button.innerHTML = '<i class="fas fa-play"></i> Atender';
        button.classList.remove('in-consultation');
        button.disabled = false;
    }
    
    // Actualizar contador
    updateActiveConsultations(-1);
    updatePrescriptionsToday(1);
}

// Acciones rápidas
function initializeQuickActions() {
    const issuePrescriptionBtn = document.getElementById('issue-prescription-btn');
    const viewPatientsBtn = document.getElementById('view-patients-btn');
    const manageScheduleBtn = document.getElementById('manage-schedule-btn');
    
    if (issuePrescriptionBtn) {
        issuePrescriptionBtn.addEventListener('click', function() {
            openPrescriptionModal();
        });
    }
    
    if (viewPatientsBtn) {
        viewPatientsBtn.addEventListener('click', function() {
            showNotification('Cargando lista de pacientes...');
            // Aquí se cargaría la lista de pacientes
        });
    }
    
    if (manageScheduleBtn) {
        manageScheduleBtn.addEventListener('click', function() {
            showNotification('Abriendo gestor de agenda...');
            // Aquí se abriría el gestor de agenda
        });
    }
}

// Modal de receta
function initializePrescriptionModal() {
    const modal = document.getElementById('prescription-modal');
    const closeModal = document.querySelector('.close-modal');
    const cancelBtn = document.getElementById('cancel-prescription');
    const form = document.getElementById('prescription-form');
    
    if (closeModal) {
        closeModal.addEventListener('click', closePrescriptionModal);
    }
    
    if (cancelBtn) {
        cancelBtn.addEventListener('click', closePrescriptionModal);
    }
    
    if (form) {
        form.addEventListener('submit', handlePrescriptionSubmit);
    }
    
    // Cerrar modal al hacer clic fuera
    if (modal) {
        modal.addEventListener('click', function(e) {
            if (e.target === modal) {
                closePrescriptionModal();
            }
        });
    }
}

function openPrescriptionModal() {
    const modal = document.getElementById('prescription-modal');
    if (modal) {
        modal.classList.remove('hidden');
        // Limpiar formulario
        document.getElementById('prescription-form').reset();
    }
}

function closePrescriptionModal() {
    const modal = document.getElementById('prescription-modal');
    if (modal) {
        modal.classList.add('hidden');
    }
}

function handlePrescriptionSubmit(e) {
    e.preventDefault();
    
    const formData = new FormData(e.target);
    const prescription = {
        patient: formData.get('patient-name'),
        medication: formData.get('medication'),
        dosage: formData.get('dosage'),
        instructions: formData.get('instructions')
    };
    
    // Simular emisión de receta
    showNotification(`Receta emitida para ${prescription.patient}`);
    
    // Actualizar contador
    updatePrescriptionsToday(1);
    
    // Cerrar modal
    closePrescriptionModal();
    
    // Aquí se enviaría la receta al backend
    console.log('Prescripción emitida:', prescription);
}

// Simulación de datos en tiempo real
function startDataSimulation() {
    // Actualizar datos cada 30 segundos
    setInterval(() => {
        updateDashboardData();
    }, 30000);
    
    // Actualizar datos iniciales
    updateDashboardData();
}

function updateDashboardData() {
    // Simular cambios en los datos
    const patientsToday = document.getElementById('patients-today');
    const weeklyAppointments = document.getElementById('weekly-appointments');
    const activeConsultation = document.getElementById('active-consultation');
    const prescriptionsToday = document.getElementById('prescriptions-today');
    
    if (patientsToday) {
        const currentValue = parseInt(patientsToday.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 3) - 1);
        patientsToday.textContent = newValue;
    }
    
    if (weeklyAppointments) {
        const currentValue = parseInt(weeklyAppointments.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 5) - 2);
        weeklyAppointments.textContent = newValue;
    }
    
    if (prescriptionsToday) {
        const currentValue = parseInt(prescriptionsToday.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 3) - 1);
        prescriptionsToday.textContent = newValue;
    }
}

function updateActiveConsultations(change) {
    const activeConsultation = document.getElementById('active-consultation');
    if (activeConsultation) {
        const currentValue = parseInt(activeConsultation.textContent);
        const newValue = Math.max(0, currentValue + change);
        activeConsultation.textContent = newValue;
    }
}

function updatePrescriptionsToday(change) {
    const prescriptionsToday = document.getElementById('prescriptions-today');
    if (prescriptionsToday) {
        const currentValue = parseInt(prescriptionsToday.textContent);
        const newValue = Math.max(0, currentValue + change);
        prescriptionsToday.textContent = newValue;
    }
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

// Funciones para manejar eventos de la agenda semanal
function initializeWeeklySchedule() {
    const scheduleItems = document.querySelectorAll('.schedule-item');
    
    scheduleItems.forEach(item => {
        item.addEventListener('click', function() {
            const day = this.querySelector('span:first-of-type').textContent;
            showNotification(`Abriendo agenda del ${day}`);
        });
    });
}

// Inicializar agenda semanal
document.addEventListener('DOMContentLoaded', function() {
    initializeWeeklySchedule();
});

// Funciones para futuras implementaciones con backend
function loadDoctorSchedule() {
    // Placeholder para cargar agenda del médico
    console.log('Cargando agenda del médico...');
}

function loadTodayConsultations() {
    // Placeholder para cargar consultas del día
    console.log('Cargando consultas de hoy...');
}

function loadPatientHistory(patientId) {
    // Placeholder para cargar historial del paciente
    console.log('Cargando historial del paciente:', patientId);
}

function savePrescription(prescriptionData) {
    // Placeholder para guardar receta en el backend
    console.log('Guardando receta:', prescriptionData);
}

function updateConsultationStatus(consultationId, status) {
    // Placeholder para actualizar estado de consulta
    console.log('Actualizando estado de consulta:', consultationId, status);
}

// Exportar funciones para uso global
window.DoctorPanel = {
    attendConsultation,
    finishConsultation,
    openPrescriptionModal,
    closePrescriptionModal,
    showNotification,
    updateDashboardData
};
