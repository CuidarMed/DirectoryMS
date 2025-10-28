// Funcionalidades específicas del panel del paciente
document.addEventListener('DOMContentLoaded', function() {
    initializePatientPanel();
});

function initializePatientPanel() {
    // Inicializar navegación del sidebar
    initializeSidebarNavigation();
    
    // Inicializar botones de videollamada
    initializeVideoCallButtons();
    
    // Inicializar botón de agendar turno
    initializeScheduleAppointment();
    
    // Inicializar botones de ver receta
    initializeViewPrescriptionButtons();
    
    // Inicializar modales
    initializeModals();
    
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
        'inicio': 'Navegando al inicio',
        'turnos': 'Navegando a mis turnos',
        'historial': 'Navegando al historial médico',
        'pagos': 'Navegando a pagos',
        'perfil': 'Navegando al perfil'
    };
    
    if (messages[section]) {
        showNotification(messages[section]);
    }
}

// Botones de videollamada
function initializeVideoCallButtons() {
    const videoCallButtons = document.querySelectorAll('.video-call-btn');
    
    videoCallButtons.forEach(button => {
        button.addEventListener('click', function() {
            const doctorName = this.getAttribute('data-doctor');
            startVideoCall(doctorName);
        });
    });
}

function startVideoCall(doctorName) {
    // Simular inicio de videollamada
    showNotification(`Iniciando videollamada con ${doctorName}...`);
    
    // Cambiar estado del botón
    const button = document.querySelector(`[data-doctor="${doctorName}"]`);
    if (button) {
        button.innerHTML = '<i class="fas fa-video"></i> En llamada';
        button.classList.add('in-call');
        button.disabled = true;
    }
    
    // Simular finalización de llamada después de 10 segundos
    setTimeout(() => {
        endVideoCall(doctorName);
    }, 10000);
}

function endVideoCall(doctorName) {
    showNotification(`Videollamada con ${doctorName} finalizada`);
    
    // Restaurar botón
    const button = document.querySelector(`[data-doctor="${doctorName}"]`);
    if (button) {
        button.innerHTML = '<i class="fas fa-video"></i> Videollamada';
        button.classList.remove('in-call');
        button.disabled = false;
    }
}

// Agendar turno
function initializeScheduleAppointment() {
    const scheduleBtn = document.getElementById('schedule-appointment-btn');
    
    if (scheduleBtn) {
        scheduleBtn.addEventListener('click', function() {
            openAppointmentModal();
        });
    }
}

function openAppointmentModal() {
    const modal = document.getElementById('appointment-modal');
    if (modal) {
        modal.classList.remove('hidden');
        // Limpiar formulario
        document.getElementById('appointment-form').reset();
        // Cargar médicos según especialidad
        loadDoctorsBySpecialty();
    }
}

function closeAppointmentModal() {
    const modal = document.getElementById('appointment-modal');
    if (modal) {
        modal.classList.add('hidden');
    }
}

function loadDoctorsBySpecialty() {
    const specialtySelect = document.getElementById('specialty');
    const doctorSelect = document.getElementById('doctor');
    
    const doctorsBySpecialty = {
        'cardiologia': [
            { value: 'dr-maria-gonzalez', text: 'Dr. María González' },
            { value: 'dr-carlos-rodriguez', text: 'Dr. Carlos Rodríguez' }
        ],
        'dermatologia': [
            { value: 'dr-ana-martinez', text: 'Dra. Ana Martínez' },
            { value: 'dr-juan-lopez', text: 'Dr. Juan López' }
        ],
        'traumatologia': [
            { value: 'dr-pedro-garcia', text: 'Dr. Pedro García' },
            { value: 'dr-laura-fernandez', text: 'Dra. Laura Fernández' }
        ],
        'pediatria': [
            { value: 'dr-carmen-ruiz', text: 'Dra. Carmen Ruiz' },
            { value: 'dr-miguel-torres', text: 'Dr. Miguel Torres' }
        ],
        'ginecologia': [
            { value: 'dr-isabel-morales', text: 'Dra. Isabel Morales' },
            { value: 'dr-antonio-vargas', text: 'Dr. Antonio Vargas' }
        ]
    };
    
    if (specialtySelect && doctorSelect) {
        specialtySelect.addEventListener('change', function() {
            const specialty = this.value;
            doctorSelect.innerHTML = '<option value="">Seleccionar médico</option>';
            
            if (doctorsBySpecialty[specialty]) {
                doctorsBySpecialty[specialty].forEach(doctor => {
                    const option = document.createElement('option');
                    option.value = doctor.value;
                    option.textContent = doctor.text;
                    doctorSelect.appendChild(option);
                });
            }
        });
    }
}

// Ver recetas
function initializeViewPrescriptionButtons() {
    const viewPrescriptionButtons = document.querySelectorAll('.view-prescription-btn');
    
    viewPrescriptionButtons.forEach(button => {
        button.addEventListener('click', function() {
            const consultationDate = this.getAttribute('data-consultation');
            viewPrescription(consultationDate);
        });
    });
}

function viewPrescription(consultationDate) {
    const modal = document.getElementById('prescription-modal');
    const content = document.getElementById('prescription-content');
    
    if (modal && content) {
        // Simular contenido de receta
        const prescriptionData = getPrescriptionData(consultationDate);
        content.innerHTML = generatePrescriptionHTML(prescriptionData);
        
        modal.classList.remove('hidden');
    }
}

function getPrescriptionData(consultationDate) {
    // Simular datos de receta según la fecha
    const prescriptions = {
        '2025-09-20': {
            doctor: 'Dr. Ana Martinez',
            date: '2025-09-20',
            patient: 'Juan Pérez',
            medications: [
                {
                    name: 'Paracetamol 500mg',
                    dosage: '1 comprimido cada 8 horas',
                    instructions: 'Tomar con alimentos. No exceder 4 comprimidos por día.'
                },
                {
                    name: 'Ibuprofeno 400mg',
                    dosage: '1 comprimido cada 12 horas',
                    instructions: 'Tomar con leche. Suspender si hay molestias gástricas.'
                }
            ]
        },
        '2025-08-15': {
            doctor: 'Dr. Juan López',
            date: '2025-08-15',
            patient: 'Juan Pérez',
            medications: [
                {
                    name: 'Cremas hidratantes',
                    dosage: 'Aplicar 2 veces al día',
                    instructions: 'Aplicar en la zona afectada después del baño.'
                }
            ]
        }
    };
    
    return prescriptions[consultationDate] || {
        doctor: 'Dr. Desconocido',
        date: consultationDate,
        patient: 'Juan Pérez',
        medications: []
    };
}

function generatePrescriptionHTML(data) {
    return `
        <div class="prescription-header">
            <h4>Receta Médica</h4>
            <p>Fecha: ${data.date}</p>
        </div>
        
        <div class="prescription-info">
            <div>
                <p><strong>Médico:</strong> ${data.doctor}</p>
                <p><strong>Paciente:</strong> ${data.patient}</p>
            </div>
            <div>
                <p><strong>Fecha de emisión:</strong> ${data.date}</p>
                <p><strong>Válida por:</strong> 30 días</p>
            </div>
        </div>
        
        <div class="medication-list">
            <h5>Medicamentos:</h5>
            ${data.medications.map(med => `
                <div class="medication-item">
                    <div class="medication-name">${med.name}</div>
                    <div class="medication-details">Dosis: ${med.dosage}</div>
                    <div class="medication-instructions">Instrucciones: ${med.instructions}</div>
                </div>
            `).join('')}
        </div>
    `;
}

function closePrescriptionModal() {
    const modal = document.getElementById('prescription-modal');
    if (modal) {
        modal.classList.add('hidden');
    }
}

// Inicializar modales
function initializeModals() {
    // Modal de agendar turno
    const appointmentModal = document.getElementById('appointment-modal');
    const closeAppointmentModal = document.querySelector('#appointment-modal .close-modal');
    const cancelAppointment = document.getElementById('cancel-appointment');
    const appointmentForm = document.getElementById('appointment-form');
    
    if (closeAppointmentModal) {
        closeAppointmentModal.addEventListener('click', closeAppointmentModal);
    }
    
    if (cancelAppointment) {
        cancelAppointment.addEventListener('click', closeAppointmentModal);
    }
    
    if (appointmentForm) {
        appointmentForm.addEventListener('submit', handleAppointmentSubmit);
    }
    
    // Modal de receta
    const prescriptionModal = document.getElementById('prescription-modal');
    const closePrescriptionModal = document.querySelector('#prescription-modal .close-modal');
    const closePrescription = document.getElementById('close-prescription');
    const downloadPrescription = document.getElementById('download-prescription');
    
    if (closePrescriptionModal) {
        closePrescriptionModal.addEventListener('click', closePrescriptionModal);
    }
    
    if (closePrescription) {
        closePrescription.addEventListener('click', closePrescriptionModal);
    }
    
    if (downloadPrescription) {
        downloadPrescription.addEventListener('click', function() {
            showNotification('Descargando receta en PDF...');
            // Aquí se implementaría la descarga del PDF
        });
    }
    
    // Cerrar modales al hacer clic fuera
    [appointmentModal, prescriptionModal].forEach(modal => {
        if (modal) {
            modal.addEventListener('click', function(e) {
                if (e.target === modal) {
                    modal.classList.add('hidden');
                }
            });
        }
    });
}

function handleAppointmentSubmit(e) {
    e.preventDefault();
    
    const formData = new FormData(e.target);
    const appointment = {
        specialty: formData.get('specialty'),
        doctor: formData.get('doctor'),
        date: formData.get('date'),
        time: formData.get('time'),
        reason: formData.get('reason')
    };
    
    // Simular agendamiento
    showNotification('Turno agendado exitosamente');
    
    // Actualizar contador
    updateConfirmedAppointments(1);
    
    // Cerrar modal
    closeAppointmentModal();
    
    // Aquí se enviaría la cita al backend
    console.log('Turno agendado:', appointment);
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
    const confirmedAppointments = document.getElementById('confirmed-appointments');
    const consultationsYear = document.getElementById('consultations-year');
    const activePrescriptions = document.getElementById('active-prescriptions');
    
    if (confirmedAppointments) {
        const currentValue = parseInt(confirmedAppointments.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 3) - 1);
        confirmedAppointments.textContent = newValue;
    }
    
    if (consultationsYear) {
        const currentValue = parseInt(consultationsYear.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 2));
        consultationsYear.textContent = newValue;
    }
    
    if (activePrescriptions) {
        const currentValue = parseInt(activePrescriptions.textContent);
        const newValue = Math.max(0, currentValue + Math.floor(Math.random() * 3) - 1);
        activePrescriptions.textContent = newValue;
    }
}

function updateConfirmedAppointments(change) {
    const confirmedAppointments = document.getElementById('confirmed-appointments');
    if (confirmedAppointments) {
        const currentValue = parseInt(confirmedAppointments.textContent);
        const newValue = Math.max(0, currentValue + change);
        confirmedAppointments.textContent = newValue;
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

// Funciones para futuras implementaciones con backend
function loadPatientAppointments() {
    // Placeholder para cargar turnos del paciente
    console.log('Cargando turnos del paciente...');
}

function loadPatientHistory() {
    // Placeholder para cargar historial del paciente
    console.log('Cargando historial del paciente...');
}

function scheduleAppointment(appointmentData) {
    // Placeholder para agendar turno en el backend
    console.log('Agendando turno:', appointmentData);
}

function downloadPrescriptionPDF(prescriptionId) {
    // Placeholder para descargar receta en PDF
    console.log('Descargando receta PDF:', prescriptionId);
}

// Exportar funciones para uso global
window.PatientPanel = {
    startVideoCall,
    endVideoCall,
    openAppointmentModal,
    closeAppointmentModal,
    viewPrescription,
    closePrescriptionModal,
    showNotification,
    updateDashboardData
};
