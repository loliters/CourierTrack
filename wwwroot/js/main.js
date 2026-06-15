// js/main.js
import { apiFetch } from './utils/api.js';
import { renderAdminDashboard, attachAdminEvents } from './components/admin.js';
import { renderLanding, attachLandingFooterEvents } from './components/landing.js';
import { renderClienteDashboard, attachClienteEvents } from './components/cliente.js';
import { renderConductorDashboard, attachConductorEvents } from './components/conductor.js';

const mainContent = document.getElementById('main-content');
const navLinksContainer = document.getElementById('navLinks');

// ---------- UTILS ----------
function getCurrentUser() {
    const userStr = localStorage.getItem('user');
    if (!userStr) return null;
    try {
        return JSON.parse(userStr);
    } catch (e) {
        console.error('Error parsing user', e);
        return null;
    }
}

function clearSession() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
}

function updateNavbar(user) {
    if (!navLinksContainer) return;
    if (!user) {
        navLinksContainer.innerHTML = `
            <a href="#" id="navLogin">Iniciar Sesión</a>
            <a href="#" id="navRegister">Registrarse</a>
        `;
        const navLogin = document.getElementById('navLogin');
        const navRegister = document.getElementById('navRegister');
        if (navLogin) navLogin.onclick = (e) => { e.preventDefault(); window.showLoginForm(); };
        if (navRegister) navRegister.onclick = (e) => { e.preventDefault(); window.showRegisterForm(); };
    } else {
        navLinksContainer.innerHTML = `
            <span class="user-welcome">Hola, ${user.nombre || user.correo}</span>
            <a href="#" id="navLogout">Cerrar sesión</a>
        `;
        const navLogout = document.getElementById('navLogout');
        if (navLogout) navLogout.onclick = (e) => { e.preventDefault(); logout(); };
    }
}

function logout() {
    clearSession();
    renderApp();
}

// ---------- VALIDACIÓN DE CONTRASEÑA ----------
function validarPassword(password) {
    const regex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&_\-]).{8,}$/;
    if (!regex.test(password)) {
        return "La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial (@$!%*?&_-).";
    }
    return null;
}

// ---------- MODALES ----------
function openModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) modal.style.display = 'flex';
}

function closeModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) modal.style.display = 'none';
}

function setupModals() {
    const closeLogin = document.getElementById('closeLoginModal');
    const closeRegister = document.getElementById('closeRegisterModal');
    if (closeLogin) closeLogin.onclick = () => closeModal('loginModal');
    if (closeRegister) closeRegister.onclick = () => closeModal('registerModal');

    window.onclick = (e) => {
        if (e.target.classList && e.target.classList.contains('modal')) {
            e.target.style.display = 'none';
        }
    };

    // Lógica de campos dinámicos según rol
    const rolSelect = document.getElementById('regRolId');
    const clienteFields = document.getElementById('clienteFields');
    const conductorFields = document.getElementById('conductorFields');
    const tipoDocSelect = document.getElementById('regTipoDocumentoId');
    const extensionField = document.getElementById('extensionCIField');
    const tipoClienteSelect = document.getElementById('regTipoClienteId');
    const naturalFields = document.getElementById('naturalFields');
    const juridicoFields = document.getElementById('juridicoFields');

    if (rolSelect) {
        rolSelect.addEventListener('change', () => {
            const rol = parseInt(rolSelect.value);
            if (rol === 3) { // Cliente
                if (clienteFields) clienteFields.style.display = 'block';
                if (conductorFields) conductorFields.style.display = 'none';
                if (tipoDocSelect) tipoDocSelect.dispatchEvent(new Event('change'));
                if (tipoClienteSelect) tipoClienteSelect.dispatchEvent(new Event('change'));
            } else if (rol === 2) { // Conductor
                if (clienteFields) clienteFields.style.display = 'none';
                if (conductorFields) conductorFields.style.display = 'block';
            } else {
                if (clienteFields) clienteFields.style.display = 'none';
                if (conductorFields) conductorFields.style.display = 'none';
            }
        });
        rolSelect.dispatchEvent(new Event('change'));
    }

    if (tipoDocSelect) {
        tipoDocSelect.addEventListener('change', () => {
            const tipoDoc = parseInt(tipoDocSelect.value);
            if (extensionField) extensionField.style.display = tipoDoc === 1 ? 'block' : 'none';
        });
        tipoDocSelect.dispatchEvent(new Event('change'));
    }

    if (tipoClienteSelect) {
        tipoClienteSelect.addEventListener('change', () => {
            const tipoCliente = parseInt(tipoClienteSelect.value);
            if (naturalFields) naturalFields.style.display = tipoCliente === 2 ? 'block' : 'none';
            if (juridicoFields) juridicoFields.style.display = tipoCliente === 1 ? 'block' : 'none';
        });
        tipoClienteSelect.dispatchEvent(new Event('change'));
    }

    // Formulario de login
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.onsubmit = async (e) => {
            e.preventDefault();
            const email = document.getElementById('loginEmail').value;
            const password = document.getElementById('loginPassword').value;
            await login(email, password);
            closeModal('loginModal');
        };
    } else {
        console.error('No se encontró el formulario #loginForm');
    }

    // Formulario de registro
    const registerForm = document.getElementById('registerForm');
    if (registerForm) {
        registerForm.onsubmit = async (e) => {
            e.preventDefault();

            const nombre = document.getElementById('regNombre').value.trim();
            const apPat = document.getElementById('regApPat').value.trim();
            const apMat = document.getElementById('regApMat').value.trim();
            const email = document.getElementById('regEmail').value.trim();
            let telefonoRaw = document.getElementById('regTelefono').value.trim();
            let telefono = telefonoRaw.replace(/\D/g, '');
            if (telefono.length !== 8) {
                alert(`El teléfono debe tener exactamente 8 dígitos (ingresaste: "${telefonoRaw}")`);
                return;
            }
            const password = document.getElementById('regPassword').value;
            const confirm = document.getElementById('regConfirmPassword').value;
            if (password !== confirm) {
                alert('Las contraseñas no coinciden');
                return;
            }

            // Validar contraseña antes de enviar
            const errorPassword = validarPassword(password);
            if (errorPassword) {
                alert(errorPassword);
                return;
            }

            const rolId = parseInt(document.getElementById('regRolId').value);

            const usuarioData = {
                Nombre: nombre,
                ApPat: apPat,
                ApMat: apMat,
                Correo: email,
                Telefono: telefono,
                Password: password,
                RolId: rolId,
                UbicacionesIds: []
            };

            let datosExtra = null;
            if (rolId === 3) { // CLIENTE
                const tipoDocumentoId = parseInt(document.getElementById('regTipoDocumentoId').value);
                const nroDocumento = document.getElementById('regNroDocumento').value.trim();
                const extensionCIId = tipoDocumentoId === 1 ? parseInt(document.getElementById('regExtensionCIId')?.value || '0') : null;
                const tipoClienteId = parseInt(document.getElementById('regTipoClienteId').value);
                datosExtra = {
                    TipoDocumentoId: tipoDocumentoId,
                    NroDocumento: nroDocumento,
                    ExtensionCIId: extensionCIId,
                    TipoClienteId: tipoClienteId
                };
                if (tipoClienteId === 2) {
                    datosExtra.FechaNac = document.getElementById('regFechaNac').value;
                    datosExtra.GeneroId = parseInt(document.getElementById('regGeneroId').value);
                } else {
                    datosExtra.RazonSocial = document.getElementById('regRazonSocial').value.trim();
                    datosExtra.Nit = document.getElementById('regNit').value.trim();
                }
            } else if (rolId === 2) { // CONDUCTOR
                datosExtra = {
                    NroLicencia: document.getElementById('regNroLicencia').value.trim(),
                    TipoLicenciaId: parseInt(document.getElementById('regTipoLicenciaId').value)
                };
            }

            await register(usuarioData, datosExtra, rolId);
            closeModal('registerModal');
        };
    } else {
        console.error('No se encontró el formulario #registerForm');
    }
}

// ---------- REGISTRO ----------
async function register(usuarioData, datosExtra, rolId) {
    try {
        const response = await apiFetch('/Usuario', {
            method: 'POST',
            body: JSON.stringify(usuarioData)
        });
        if (response && response.id) {
            const usuarioId = response.id;

            if (rolId === 3) { // Cliente
                const clienteData = {
                    UsuarioId: usuarioId,
                    NroDocumento: datosExtra.NroDocumento,
                    TipoDocumentoId: datosExtra.TipoDocumentoId,
                    ExtensionCIId: datosExtra.ExtensionCIId,
                    TipoClienteId: datosExtra.TipoClienteId
                };
                const clienteCreado = await apiFetch('/Cliente', {
                    method: 'POST',
                    body: JSON.stringify(clienteData)
                });
                if (clienteCreado && clienteCreado.id) {
                    if (datosExtra.TipoClienteId === 2) {
                        await apiFetch('/ClienteNatural', {
                            method: 'POST',
                            body: JSON.stringify({
                                ClienteId: clienteCreado.id,
                                FechaNac: datosExtra.FechaNac,
                                GeneroId: datosExtra.GeneroId
                            })
                        });
                    } else {
                        await apiFetch('/ClienteJuridico', {
                            method: 'POST',
                            body: JSON.stringify({
                                ClienteId: clienteCreado.id,
                                RazonSocial: datosExtra.RazonSocial,
                                Nit: datosExtra.Nit
                            })
                        });
                    }
                }
            } else if (rolId === 2) { // Conductor
                await apiFetch('/Conductor', {
                    method: 'POST',
                    body: JSON.stringify({
                        UsuarioId: usuarioId,
                        NroLicencia: datosExtra.NroLicencia,
                        TipoLicenciaId: datosExtra.TipoLicenciaId
                    })
                });
            }

            alert('Registro exitoso. Ahora inicia sesión.');
            window.showLoginForm();
        } else {
            // Si la respuesta no tiene id, mostrar errores de validación si existen
            if (response && response.errors) {
                const errores = Object.values(response.errors).flat();
                alert('Error en el registro:\n' + errores.join('\n'));
            } else {
                alert('Error en el registro del usuario');
            }
        }
    } catch (err) {
        console.error(err);
        // Intentar extraer errores de validación del mensaje
        let errorMsg = err.message;
        try {
            const parsed = JSON.parse(err.message);
            if (parsed.errors) {
                const errores = Object.values(parsed.errors).flat();
                errorMsg = errores.join('\n');
            }
        } catch (e) { /* no hacer nada */ }
        alert('Error al registrar: ' + errorMsg);
    }
}

// ---------- LOGIN ----------
async function login(email, password) {
    try {
        const response = await apiFetch('/Auth/login', {
            method: 'POST',
            body: JSON.stringify({ Correo: email, Password: password })
        });
        console.log('Respuesta del login:', response);
        if (response && response.token) {
            const user = {
                id: response.usuarioId,
                correo: response.correo,
                rolNombre: response.rol,
                nombre: response.correo.split('@')[0]
            };
            localStorage.setItem('token', response.token);
            localStorage.setItem('user', JSON.stringify(user));
            renderApp();
        } else {
            alert('Credenciales incorrectas. Verifica correo y contraseña.');
        }
    } catch (err) {
        console.error('Error en login:', err);
        alert('Error al iniciar sesión: ' + (err.message || 'Error de red o servidor'));
    }
}

window.showLoginForm = () => openModal('loginModal');
window.showRegisterForm = () => openModal('registerModal');

// ---------- RENDER SEGÚN ROL ----------
function showLanding() {
    const landingHtml = renderLanding();
    mainContent.innerHTML = landingHtml;
    attachLandingFooterEvents();
    setupModals();
    updateNavbar(null);
}

async function showAdminDashboard(user) {
    try {
        const dashboardHtml = await renderAdminDashboard(user);
        mainContent.innerHTML = dashboardHtml;
        await attachAdminEvents(user);
        updateNavbar(user);
    } catch (err) {
        console.error(err);
        mainContent.innerHTML = '<div class="error">Error al cargar panel de administrador</div>';
    }
}

async function showClienteDashboard(user) {
    try {
        const dashboardHtml = await renderClienteDashboard();
        mainContent.innerHTML = dashboardHtml;
        attachClienteEvents();
        updateNavbar(user);
    } catch (err) {
        console.error(err);
        mainContent.innerHTML = '<div class="error">Error al cargar panel de cliente</div>';
    }
}

async function showConductorDashboard(user) {
    try {
        const dashboardHtml = await renderConductorDashboard();
        mainContent.innerHTML = dashboardHtml;
        attachConductorEvents();
        updateNavbar(user);
    } catch (err) {
        console.error(err);
        mainContent.innerHTML = '<div class="error">Error al cargar panel de conductor</div>';
    }
}

function showAccessDenied() {
    mainContent.innerHTML = `
        <div class="access-denied">
            <h2>⛔ Acceso Denegado</h2>
            <p>No tienes permisos para acceder a esta sección.</p>
            <button onclick="logout()">Cerrar sesión</button>
        </div>
    `;
    updateNavbar(getCurrentUser());
}

async function renderApp() {
    const user = getCurrentUser();
    if (!user) {
        showLanding();
        return;
    }
    const rol = user.rolNombre?.toUpperCase();
    switch (rol) {
        case 'ADMINISTRADOR':
            await showAdminDashboard(user);
            break;
        case 'CLIENTE':
            await showClienteDashboard(user);
            break;
        case 'CONDUCTOR':
            await showConductorDashboard(user);
            break;
        default:
            showAccessDenied();
            break;
    }
}

renderApp();