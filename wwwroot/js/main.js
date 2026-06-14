import { login, logout, getToken } from './auth.js';
import { cargarUsuarios } from './api/usuarios.js';
import { mostrarError } from './components/ui.js';

// Elementos del DOM
const loginSection = document.getElementById('seccion-login');
const contentSection = document.getElementById('seccion-contenido');
const loginForm = document.getElementById('formLogin');
const btnCargar = document.getElementById('btnCargarEnvios');
const btnCerrar = document.getElementById('btnCerrarSesion');

function mostrarLogin() {
    loginSection.style.display = 'block';
    contentSection.style.display = 'none';
}

function mostrarContenido() {
    loginSection.style.display = 'none';
    contentSection.style.display = 'block';
    // Al mostrar el panel, cargar automáticamente los usuarios
    cargarUsuarios();
}

// Estado inicial
if (getToken()) {
    mostrarContenido();
} else {
    mostrarLogin();
}

// Login
loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();
    const email = document.getElementById('inputEmail').value.trim();
    const password = document.getElementById('inputPassword').value;
    try {
        const datos = await login(email, password);
        mostrarError('');   // limpia errores
        console.log(`Bienvenido ${datos.correo}, rol: ${datos.rol}`);
        mostrarContenido();
    } catch (err) {
        mostrarError(err.message);
    }
});

// Botón "Cargar datos protegidos" (recarga manual)
btnCargar.addEventListener('click', cargarUsuarios);

// Cerrar sesión
btnCerrar.addEventListener('click', () => {
    logout();
    mostrarLogin();
    // Resetear lista
    const lista = document.getElementById('listaEnvios');
    if (lista) lista.innerHTML = '<li class="info-text">Inicia sesión y presiona "Cargar datos protegidos"</li>';
});