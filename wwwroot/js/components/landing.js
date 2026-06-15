// js/components/landing.js
export function renderLanding() {
    return `
  <!-- Modal de Registro -->
<div id="registerModal" class="modal" style="display:none;">
    <div class="modal-content modal-large">
        <span class="close-modal" id="closeRegisterModal">&times;</span>
        <h3>Registrarse</h3>
        <form id="registerForm">
            <!-- Datos personales básicos (obligatorios para todos) -->
            <div class="form-group">
                <label>Nombre *</label>
                <input type="text" id="regNombre" required>
            </div>
            <div class="form-row">
                <div class="form-group">
                    <label>Apellido Paterno *</label>
                    <input type="text" id="regApPat" required>
                </div>
                <div class="form-group">
                    <label>Apellido Materno *</label>
                    <input type="text" id="regApMat" required>
                </div>
            </div>
            <div class="form-group">
                <label>Correo electrónico *</label>
                <input type="email" id="regEmail" required>
            </div>
            <div class="form-group">
                <label>Teléfono * (8 dígitos)</label>
                <input type="tel" id="regTelefono" pattern="\d{8}" required>
            </div>
            <div class="form-group">
                <label>Contraseña *</label>
                <input type="password" id="regPassword" required>
            </div>
            <div class="form-group">
                <label>Confirmar contraseña *</label>
                <input type="password" id="regConfirmPassword" required>
            </div>
            <div class="form-group">
                <label>Rol *</label>
                <select id="regRolId">
                    <option value="3">Cliente</option>
                    <option value="2">Conductor</option>
                </select>
                <small>Nota: Administradores solo pueden ser creados por otro administrador.</small>
            </div>

            <!-- Campos específicos para CLIENTE -->
            <div id="clienteFields" style="display: none;">
                <hr>
                <h4>Datos de Cliente</h4>
                <div class="form-group">
                    <label>Tipo de documento</label>
                    <select id="regTipoDocumentoId">
                        <option value="1">CEDULA DE IDENTIDAD</option>
                        <option value="2">NIT</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Número de documento</label>
                    <input type="text" id="regNroDocumento" placeholder="Ej: 1234567 o 1020304011">
                </div>
                <div class="form-group" id="extensionCIField" style="display: none;">
                    <label>Extensión de CI</label>
                    <select id="regExtensionCIId">
                        <option value="1">LP</option><option value="2">CB</option><option value="3">SC</option>
                        <option value="4">OR</option><option value="5">PT</option><option value="6">CH</option>
                        <option value="7">TJ</option><option value="8">BN</option><option value="9">PD</option>
                    </select>
                </div>
                <div class="form-group">
                    <label>Tipo de cliente</label>
                    <select id="regTipoClienteId">
                        <option value="2">NATURAL</option>
                        <option value="1">JURÍDICO</option>
                    </select>
                </div>
                <div id="naturalFields" style="display: none;">
                    <div class="form-group">
                        <label>Fecha de nacimiento</label>
                        <input type="date" id="regFechaNac">
                    </div>
                    <div class="form-group">
                        <label>Género</label>
                        <select id="regGeneroId">
                            <option value="1">FEMENINO</option>
                            <option value="2">MASCULINO</option>
                            <option value="3">OTRO</option>
                        </select>
                    </div>
                </div>
                <div id="juridicoFields" style="display: none;">
                    <div class="form-group">
                        <label>Razón Social</label>
                        <input type="text" id="regRazonSocial">
                    </div>
                    <div class="form-group">
                        <label>NIT</label>
                        <input type="text" id="regNit">
                    </div>
                </div>
            </div>

            <!-- Campos específicos para CONDUCTOR -->
            <div id="conductorFields" style="display: none;">
                <hr>
                <h4>Datos de Conductor</h4>
                <div class="form-group">
                    <label>Número de Licencia</label>
                    <input type="text" id="regNroLicencia">
                </div>
                <div class="form-group">
                    <label>Tipo de Licencia</label>
                    <select id="regTipoLicenciaId">
                        <option value="1">M</option>
                        <option value="2">P</option>
                        <option value="3">C</option>
                    </select>
                </div>
            </div>

            <button type="submit" class="btn-primary">Registrarse</button>
        </form>
    </div>
</div>

        <!-- Sección Hero -->
        <section class="hero-modern">
            <div class="hero-content-modern">
                <h1 class="hero-title">CourierTrack</h1>
                <p class="hero-subtitle">La revolución en logística y seguimiento de pedidos</p>
                <div class="hero-buttons">
                    <button class="btn-glow" id="landingLogin">Comenzar</button>
                    <button class="btn-outline-light" id="landingRegistro">Registrarse</button>
                </div>
            </div>
            <div class="wave">
                <svg viewBox="0 0 1440 120" preserveAspectRatio="none">
                    <path d="M0,64L80,58.7C160,53,320,43,480,48C640,53,800,75,960,80C1120,85,1280,75,1360,69.3L1440,64L1440,120L1360,120C1280,120,1120,120,960,120C800,120,640,120,480,120C320,120,160,120,80,120L0,120Z"></path>
                </svg>
            </div>
        </section>

        <!-- Características -->
        <section class="features-modern">
            <h2 class="section-title">¿Por qué elegirnos?</h2>
            <div class="features-grid-modern">
                <div class="feature-card-modern">
                    <div class="feature-icon-modern"><i class="fas fa-map-marker-alt"></i></div>
                    <h3>Seguimiento GPS</h3>
                    <p>Rastrea cada paquete en tiempo real desde la recogida hasta la entrega final.</p>
                </div>
                <div class="feature-card-modern">
                    <div class="feature-icon-modern"><i class="fas fa-tachometer-alt"></i></div>
                    <h3>Optimización de rutas</h3>
                    <p>Nuestro algoritmo inteligente reduce tiempos y costos de envío.</p>
                </div>
                <div class="feature-card-modern">
                    <div class="feature-icon-modern"><i class="fas fa-shield-alt"></i></div>
                    <h3>Seguridad total</h3>
                    <p>Envíos asegurados y seguimiento con verificación de entregas.</p>
                </div>
            </div>
        </section>

        <!-- Misión y Visión -->
        <section class="mv-modern">
            <div class="mv-card-modern">
                <i class="fas fa-bullseye"></i>
                <h2>Misión</h2>
                <p>Brindar soluciones logísticas eficientes y tecnológicas que conecten personas y empresas, optimizando la entrega de paquetes con transparencia y rapidez.</p>
            </div>
            <div class="mv-card-modern">
                <i class="fas fa-eye"></i>
                <h2>Visión</h2>
                <p>Ser la plataforma líder en gestión de entregas en Latinoamérica, destacando por nuestra innovación en geolocalización y satisfacción del cliente.</p>
            </div>
        </section>

        <!-- Footer -->
        <footer class="footer-premium">
            <div class="footer-premium-container">
                <div class="footer-premium-col">
                    <h4>CourierTrack</h4>
                    <p>La solución inteligente para la logística moderna. Conectamos empresas y personas con eficiencia y transparencia.</p>
                    <div class="footer-premium-social">
                        <a href="#" aria-label="Facebook"><i class="fab fa-facebook-f"></i></a>
                        <a href="#" aria-label="Twitter"><i class="fab fa-twitter"></i></a>
                        <a href="#" aria-label="Instagram"><i class="fab fa-instagram"></i></a>
                        <a href="#" aria-label="LinkedIn"><i class="fab fa-linkedin-in"></i></a>
                    </div>
                </div>
                <div class="footer-premium-col">
                    <h4>Enlaces rápidos</h4>
                    <ul>
                        <li><a href="#" id="footerInicio">Inicio</a></li>
                        <li><a href="#" id="footerLogin">Iniciar Sesión</a></li>
                        <li><a href="#" id="footerRegistro">Registrarse</a></li>
                        <li><a href="#">Soporte</a></li>
                        <li><a href="#">Términos y condiciones</a></li>
                    </ul>
                </div>
                <div class="footer-premium-col">
                    <h4>Contacto</h4>
                    <ul class="contact-info">
                        <li><i class="fas fa-envelope"></i> info@couriertrack.com</li>
                        <li><i class="fas fa-phone-alt"></i> +591 2 123 4567</li>
                        <li><i class="fas fa-map-marker-alt"></i> La Paz, Bolivia</li>
                    </ul>
                </div>
                <div class="footer-premium-col">
                    <h4>Horario de atención</h4>
                    <ul class="schedule">
                        <li><i class="far fa-calendar-alt"></i> Lunes a Viernes: 8:00 - 20:00</li>
                        <li><i class="far fa-calendar-alt"></i> Sábados: 9:00 - 14:00</li>
                        <li><i class="far fa-clock"></i> Soporte 24/7</li>
                    </ul>
                </div>
            </div>
            <div class="footer-premium-bottom">
                <p>&copy; 2026 CourierTrack - Todos los derechos reservados. | <a href="#">Política de privacidad</a> | <a href="#">Mapa del sitio</a></p>
            </div>
        </footer>
    `;
}

// Conectar eventos del footer y los botones principales del landing
// Esta función se debe llamar DESPUÉS de que el landing esté en el DOM
export function attachLandingFooterEvents() {
    // Botones principales del landing
    const btnLogin = document.getElementById('landingLogin');
    const btnRegistro = document.getElementById('landingRegistro');
    if (btnLogin) {
        btnLogin.onclick = (e) => {
            e.preventDefault();
            if (typeof window.showLoginForm === 'function') window.showLoginForm();
        };
    }
    if (btnRegistro) {
        btnRegistro.onclick = (e) => {
            e.preventDefault();
            if (typeof window.showRegisterForm === 'function') window.showRegisterForm();
        };
    }

    // Footer
    const footerInicio = document.getElementById('footerInicio');
    const footerLogin = document.getElementById('footerLogin');
    const footerRegistro = document.getElementById('footerRegistro');

    if (footerInicio) {
        footerInicio.onclick = (e) => {
            e.preventDefault();
            window.scrollTo({ top: 0, behavior: 'smooth' });
        };
    }
    if (footerLogin) {
        footerLogin.onclick = (e) => {
            e.preventDefault();
            if (typeof window.showLoginForm === 'function') window.showLoginForm();
        };
    }
    if (footerRegistro) {
        footerRegistro.onclick = (e) => {
            e.preventDefault();
            if (typeof window.showRegisterForm === 'function') window.showRegisterForm();
        };
    }
}