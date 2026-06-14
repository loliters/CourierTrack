import { ENDPOINTS } from '../config.js';
import { getToken } from '../auth.js';
import { mostrarLista, mostrarMensajeEnLista } from '../components/ui.js';

export async function cargarUsuarios() {
    const token = getToken();
    if (!token) {
        mostrarMensajeEnLista('No autenticado. Inicia sesión primero.', true);
        return;
    }
    const res = await fetch(ENDPOINTS.USUARIOS, {
        headers: { 'Authorization': `Bearer ${token}` }
    });
    if (res.status === 401) {
        mostrarMensajeEnLista('Sesión expirada. Vuelve a iniciar sesión.', true);
        setTimeout(() => window.location.reload(), 2000);
        return;
    }
    if (res.status === 403) {
        mostrarMensajeEnLista('No tienes permisos (se requiere rol ADMINISTRADOR).', true);
        return;
    }
    if (!res.ok) {
        const error = await res.text();
        mostrarMensajeEnLista(`Error ${res.status}: ${error}`, true);
        return;
    }
    const data = await res.json();
    mostrarLista(data);
}