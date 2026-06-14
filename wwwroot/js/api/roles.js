import { ENDPOINTS } from '../config.js';
import { getToken } from '../auth.js';
import { mostrarDatos } from '../components/ui.js';

export async function cargarDatosProtegidos() {
    const token = getToken();
    if (!token) {
        alert('No estás autenticado');
        return;
    }
    const res = await fetch(ENDPOINTS.DATOS_PROTEGIDOS, {
        headers: { 'Authorization': `Bearer ${token}` }
    });
    if (res.status === 401) {
        alert('Sesión expirada. Vuelve a iniciar sesión.');
        window.location.reload();
        return;
    }
    if (res.status === 403) {
        alert('No tienes permisos (se requiere rol ADMINISTRADOR)');
        return;
    }
    if (!res.ok) {
        alert(`Error ${res.status}: ${await res.text()}`);
        return;
    }
    const data = await res.json();
    mostrarDatos(data);
}