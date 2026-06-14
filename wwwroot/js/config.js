export const API_BASE = '/api';
export const ENDPOINTS = {
    LOGIN: `${API_BASE}/Auth/login`,
    USUARIOS: `${API_BASE}/Usuario`,    // Endpoint protegido (requiere ADMINISTRADOR)
};