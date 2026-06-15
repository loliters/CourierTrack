// auth.js
import { apiFetch } from './api.js';

export async function login(correo, password) {
    const data = await apiFetch('/Auth/login', {
        method: 'POST',
        body: JSON.stringify({ correo, password }),
    });
    if (data.token) {
        localStorage.setItem('token', data.token);
        localStorage.setItem('user', JSON.stringify({
            id: data.usuarioId,
            correo: data.correo,
            rol: data.rol,
        }));
        return true;
    }
    return false;
}

export function logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    window.location.reload();
}

export function getCurrentUser() {
    const userStr = localStorage.getItem('user');
    if (!userStr) return null;
    try {
        return JSON.parse(userStr);
    } catch {
        return null;
    }
}

export function getToken() {
    return localStorage.getItem('token');
}

export function isAuthenticated() {
    return !!getToken();
}