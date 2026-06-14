import { ENDPOINTS } from './config.js';

let token = localStorage.getItem('token_jwt') || '';

export function getToken() { return token; }

export async function login(correo, password) {
    const res = await fetch(ENDPOINTS.LOGIN, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ Correo: correo, Password: password })
    });
    if (!res.ok) {
        const error = await res.text();
        throw new Error(error || 'Credenciales inválidas');
    }
    const data = await res.json();
    token = data.Token;               
    localStorage.setItem('token_jwt', token);
    return data;
}

export function logout() {
    token = '';
    localStorage.removeItem('token_jwt');
}