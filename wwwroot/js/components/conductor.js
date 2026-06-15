// js/components/conductor.js
import { apiFetch } from '../utils/api.js';
import { getCurrentUser } from '../utils/auth.js';
import { MAPBOX_TOKEN } from '../config.js';

let currentMap = null;
let currentMarker = null;
let currentTrackingInterval = null;
let activeSeguimientoId = null;

// Función para obtener o crear el conductor automáticamente
async function getOrCreateConductor(userId) {
    try {
        const conductores = await apiFetch('/Conductor');
        let conductor = conductores.find(c => c.usuarioId === userId);
        if (!conductor) {
            const nuevoConductor = await apiFetch('/Conductor', {
                method: 'POST',
                body: JSON.stringify({
                    UsuarioId: userId,          // ← PascalCase
                    NroLicencia: "PENDIENTE",   // ← PascalCase
                    TipoLicenciaId: 1           // ← PascalCase
                })
            });
            conductor = nuevoConductor;
        }
        return conductor?.id;
    } catch (e) {
        console.error('Error getOrCreateConductor:', e);
        return null;
    }
}

// Obtener seguimientos del conductor (asignaciones)
async function getSeguimientos(conductorId) {
    try {
        const seguimientos = await apiFetch(`/Seguimiento?conductorId=${conductorId}`);
        for (const seg of seguimientos) {
            const pedido = await apiFetch(`/Pedido/${seg.pedidoId}`).catch(() => null);
            if (pedido) {
                seg.estadoPedido = pedido.estadosPedidos?.[0]?.estado?.nombre || 'Pendiente';
                seg.destinoUbicacion = await apiFetch(`/Ubicacion/${pedido.ubicacionDestinoId}`).catch(() => null);
            }
            const estado = await apiFetch(`/Estado/${seg.estadoId}`).catch(() => null);
            seg.estadoNombre = estado?.nombre || 'Asignado';
        }
        return seguimientos.sort((a, b) => new Date(b.fecha) - new Date(a.fecha));
    } catch (e) {
        console.error('Error cargando seguimientos:', e);
        return [];
    }
}

// Calcular estadísticas
function calcularEstadisticas(seguimientos) {
    const total = seguimientos.length;
    const activos = seguimientos.filter(s => s.estadoNombre !== 'Entregado' && s.estadoNombre !== 'Cancelado').length;
    const completados = seguimientos.filter(s => s.estadoNombre === 'Entregado').length;
    const pendientes = seguimientos.filter(s => s.estadoNombre === 'Pendiente').length;
    return { total, activos, completados, pendientes };
}

// Notificación tipo toast
function showToast(message, type = 'success') {
    const toast = document.createElement('div');
    toast.className = `toast toast-${type}`;
    toast.innerHTML = `<i class="fas ${type === 'success' ? 'fa-check-circle' : 'fa-exclamation-circle'}"></i> ${message}`;
    document.body.appendChild(toast);
    setTimeout(() => toast.remove(), 3000);
}

// Renderizar estadísticas
function renderStats(stats) {
    return `
        <div class="stats-grid conductor-stats">
            <div class="stat-card">
                <i class="fas fa-tasks"></i>
                <h3>${stats.total}</h3>
                <p>Total Asignaciones</p>
            </div>
            <div class="stat-card stat-activo">
                <i class="fas fa-truck-moving"></i>
                <h3>${stats.activos}</h3>
                <p>En curso</p>
            </div>
            <div class="stat-card stat-completado">
                <i class="fas fa-check-circle"></i>
                <h3>${stats.completados}</h3>
                <p>Completados</p>
            </div>
            <div class="stat-card stat-pendiente">
                <i class="fas fa-clock"></i>
                <h3>${stats.pendientes}</h3>
                <p>Pendientes</p>
            </div>
        </div>
    `;
}

// Renderizar tabla de seguimientos
function renderSeguimientosTable(seguimientos, searchTerm = '') {
    const filtered = seguimientos.filter(s =>
        s.id.toString().includes(searchTerm) ||
        s.pedidoId.toString().includes(searchTerm) ||
        s.estadoNombre?.toLowerCase().includes(searchTerm.toLowerCase())
    );
    if (filtered.length === 0) {
        return '<div class="empty-state">No hay asignaciones que coincidan.</div>';
    }
    return `
        <div class="table-responsive">
            <table class="admin-table">
                <thead>
                    <tr>
                        <th>ID Seguimiento</th><th>Pedido ID</th><th>Fecha</th>
                        <th>Estado</th><th>Observación</th><th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${filtered.map(seg => `
                        <tr>
                            <td>${seg.id}</td>
                            <td>${seg.pedidoId}</td>
                            <td>${new Date(seg.fecha).toLocaleString()}</td>
                            <td><span class="status-badge status-${seg.estadoNombre?.toLowerCase().replace(/\s/g, '')}">${seg.estadoNombre || 'Desconocido'}</span></td>
                            <td>${seg.observacion || '-'}</td>
                            <td>
                                ${seg.estadoNombre !== 'Entregado' ? `
                                    <button class="btn-icon btn-update-location" data-id="${seg.id}" data-pedido="${seg.pedidoId}" title="Actualizar ubicación"><i class="fas fa-map-marker-alt"></i></button>
                                    <button class="btn-icon btn-change-status" data-id="${seg.id}" data-status="En Ruta" title="Marcar en ruta"><i class="fas fa-road"></i></button>
                                    <button class="btn-icon btn-complete" data-id="${seg.id}" data-pedido="${seg.pedidoId}" title="Entregar"><i class="fas fa-check-circle"></i></button>
                                ` : ''}
                                <button class="btn-icon btn-view-map" data-lat="${seg.destinoUbicacion?.latitud || ''}" data-lng="${seg.destinoUbicacion?.longitud || ''}" data-pedido="${seg.pedidoId}" title="Ver en mapa"><i class="fas fa-eye"></i></button>
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        </div>
    `;
}

// Inicializar mapa conductor
function initConductorMap(lat, lng, destinoLat, destinoLng) {
    if (currentMap) currentMap.remove();
    mapboxgl.accessToken = MAPBOX_TOKEN;
    currentMap = new mapboxgl.Map({
        container: 'conductorMap',
        style: 'mapbox://styles/mapbox/streets-v12',
        center: [lng, lat],
        zoom: 13
    });
    currentMap.on('load', () => {
        if (currentMarker) currentMarker.remove();
        currentMarker = new mapboxgl.Marker({ color: '#3b82f6' })
            .setLngLat([lng, lat])
            .addTo(currentMap);
        if (destinoLat && destinoLng) {
            new mapboxgl.Marker({ color: '#ef4444' })
                .setLngLat([destinoLng, destinoLat])
                .addTo(currentMap);
            fetch(`https://api.mapbox.com/directions/v5/mapbox/driving/${lng},${lat};${destinoLng},${destinoLat}?geometries=geojson&access_token=${MAPBOX_TOKEN}`)
                .then(res => res.json())
                .then(data => {
                    const route = data.routes[0].geometry;
                    currentMap.addSource('route', {
                        type: 'geojson',
                        data: { type: 'Feature', geometry: route, properties: {} }
                    });
                    currentMap.addLayer({
                        id: 'route',
                        type: 'line',
                        source: 'route',
                        layout: { 'line-join': 'round', 'line-cap': 'round' },
                        paint: { 'line-color': '#3b82f6', 'line-width': 4 }
                    });
                });
        }
    });
}

// Geocodificación inversa
async function getAddress(lat, lng) {
    try {
        const res = await fetch(`https://api.mapbox.com/geocoding/v5/mapbox.places/${lng},${lat}.json?access_token=${MAPBOX_TOKEN}&language=es`);
        const data = await res.json();
        return data.features?.[0]?.place_name || `${lat}, ${lng}`;
    } catch {
        return `${lat}, ${lng}`;
    }
}

// Actualizar ubicación
async function updateLocation(seguimientoId, pedidoId) {
    if (!navigator.geolocation) {
        showToast('Geolocalización no soportada', 'error');
        return;
    }
    showToast('Obteniendo ubicación...', 'info');
    navigator.geolocation.getCurrentPosition(async (position) => {
        const { latitude, longitude } = position.coords;
        try {
            const ubicacion = await apiFetch('/Ubicacion', {
                method: 'POST',
                body: JSON.stringify({ latitud: latitude, longitud: longitude })
            });
            const seguimientoActual = await apiFetch(`/Seguimiento/${seguimientoId}`);
            const nuevoSeguimiento = {
                fecha: new Date().toISOString(),
                observacion: 'Ubicación actualizada',
                pedidoId: pedidoId,
                conductorId: seguimientoActual.conductorId,
                vehiculoId: seguimientoActual.vehiculoId,
                ubicacionId: ubicacion.id,
                estadoId: seguimientoActual.estadoId
            };
            await apiFetch('/Seguimiento', { method: 'POST', body: JSON.stringify(nuevoSeguimiento) });
            showToast('Ubicación actualizada');
            refreshDashboard();
        } catch (err) {
            console.error(err);
            showToast('Error al actualizar ubicación', 'error');
        }
    }, (err) => {
        showToast('Error obteniendo ubicación: ' + err.message, 'error');
    });
}

// Cambiar estado
async function changeStatus(seguimientoId, pedidoId, nuevoEstadoNombre) {
    try {
        const estados = await apiFetch('/Estado');
        const estado = estados.find(e => e.nombre === nuevoEstadoNombre);
        if (!estado) throw new Error('Estado no encontrado');
        const seguimientoActual = await apiFetch(`/Seguimiento/${seguimientoId}`);
        const nuevoSeguimiento = {
            fecha: new Date().toISOString(),
            observacion: `Estado cambiado a ${nuevoEstadoNombre}`,
            pedidoId: pedidoId,
            conductorId: seguimientoActual.conductorId,
            vehiculoId: seguimientoActual.vehiculoId,
            ubicacionId: seguimientoActual.ubicacionId,
            estadoId: estado.id
        };
        await apiFetch('/Seguimiento', { method: 'POST', body: JSON.stringify(nuevoSeguimiento) });
        showToast(`Pedido marcado como ${nuevoEstadoNombre}`);
        refreshDashboard();
    } catch (err) {
        console.error(err);
        showToast('Error al cambiar estado', 'error');
    }
}

// Completar entrega
async function completeDelivery(seguimientoId, pedidoId) {
    if (confirm('¿Confirmas que has entregado el pedido?')) {
        await changeStatus(seguimientoId, pedidoId, 'Entregado');
    }
}

// Ver mapa modal
async function viewOnMap(pedidoId, destinoLat, destinoLng) {
    if (!destinoLat || !destinoLng) {
        showToast('No hay coordenadas de destino', 'error');
        return;
    }
    const modalHtml = `
        <div id="mapModal" class="modal" style="display:flex;">
            <div class="modal-content modal-large">
                <span class="close" id="closeMapModal">&times;</span>
                <h3>Ubicación del Pedido #${pedidoId}</h3>
                <div id="modalMap" style="height: 400px;"></div>
                <p id="addressInfo">Cargando dirección...</p>
            </div>
        </div>
    `;
    document.body.insertAdjacentHTML('beforeend', modalHtml);
    const modal = document.getElementById('mapModal');
    const closeBtn = document.getElementById('closeMapModal');
    closeBtn.onclick = () => modal.remove();
    modal.onclick = (e) => { if (e.target === modal) modal.remove(); };
    const address = await getAddress(destinoLat, destinoLng);
    document.getElementById('addressInfo').innerText = address;
    setTimeout(() => {
        mapboxgl.accessToken = MAPBOX_TOKEN;
        const map = new mapboxgl.Map({
            container: 'modalMap',
            style: 'mapbox://styles/mapbox/streets-v12',
            center: [destinoLng, destinoLat],
            zoom: 14
        });
        map.on('load', () => {
            new mapboxgl.Marker({ color: '#ef4444' })
                .setLngLat([destinoLng, destinoLat])
                .addTo(map);
        });
    }, 100);
}

// Refrescar dashboard
async function refreshDashboard() {
    const newHtml = await renderConductorDashboard();
    const container = document.getElementById('main-content');
    if (container) container.innerHTML = newHtml;
    attachConductorEvents();
}

// Render principal
export async function renderConductorDashboard() {
    const user = getCurrentUser();
    if (!user) return '<div class="card">Error: Usuario no encontrado</div>';
    const conductorId = await getOrCreateConductor(user.id);
    if (!conductorId) {
        return '<div class="card">Error: No se pudo crear el perfil de conductor. Contacta al soporte.</div>';
    }
    const seguimientos = await getSeguimientos(conductorId);
    const stats = calcularEstadisticas(seguimientos);
    let currentLat = -17.389577, currentLng = -66.157607;
    let destinoLat = null, destinoLng = null;
    const activo = seguimientos.find(s => s.estadoNombre !== 'Entregado' && s.estadoNombre !== 'Cancelado');
    if (activo && activo.destinoUbicacion) {
        destinoLat = activo.destinoUbicacion.latitud;
        destinoLng = activo.destinoUbicacion.longitud;
    }
    if (navigator.geolocation) {
        await new Promise((resolve) => {
            navigator.geolocation.getCurrentPosition(pos => {
                currentLat = pos.coords.latitude;
                currentLng = pos.coords.longitude;
                resolve();
            }, () => resolve());
        });
    }
    return `
        <div class="conductor-dashboard">
            <div class="welcome-message">
                <h2>Bienvenido, ${user.nombre || user.correo.split('@')[0]}</h2>
                <p>Gestiona tus entregas y actualiza tu ubicación en tiempo real.</p>
            </div>
            ${renderStats(stats)}
            <div class="card">
                <div class="card-header">
                    <h3><i class="fas fa-map-marked-alt"></i> Mapa de seguimiento</h3>
                    <button class="btn-secondary" id="refreshLocationBtn"><i class="fas fa-sync-alt"></i> Actualizar mi ubicación</button>
                </div>
                <div id="conductorMap" class="map-container" style="height: 400px;"></div>
            </div>
            <div class="card">
                <div class="card-header">
                    <h3><i class="fas fa-list"></i> Mis asignaciones</h3>
                </div>
                <div class="search-bar">
                    <input type="text" id="searchSeguimientos" placeholder="Buscar por ID, pedido o estado...">
                </div>
                <div id="seguimientosContainer">
                    ${renderSeguimientosTable(seguimientos)}
                </div>
            </div>
        </div>
    `;
}

// Adjuntar eventos
export function attachConductorEvents() {
    const mapContainer = document.getElementById('conductorMap');
    if (mapContainer) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(pos => {
                initConductorMap(pos.coords.latitude, pos.coords.longitude, null, null);
            }, () => {
                initConductorMap(-17.389577, -66.157607, null, null);
            });
        } else {
            initConductorMap(-17.389577, -66.157607, null, null);
        }
    }
    const refreshBtn = document.getElementById('refreshLocationBtn');
    if (refreshBtn) {
        refreshBtn.addEventListener('click', () => {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(pos => {
                    const { latitude, longitude } = pos.coords;
                    if (currentMap) {
                        currentMap.flyTo({ center: [longitude, latitude], zoom: 14 });
                        if (currentMarker) currentMarker.setLngLat([longitude, latitude]);
                        else currentMarker = new mapboxgl.Marker({ color: '#3b82f6' }).setLngLat([longitude, latitude]).addTo(currentMap);
                    }
                    showToast('Ubicación actualizada en el mapa');
                }, () => showToast('No se pudo obtener ubicación', 'error'));
            } else {
                showToast('Geolocalización no soportada', 'error');
            }
        });
    }
    const searchInput = document.getElementById('searchSeguimientos');
    if (searchInput) {
        searchInput.addEventListener('input', async (e) => {
            const user = getCurrentUser();
            const conductorId = await getOrCreateConductor(user.id);
            const seguimientos = await getSeguimientos(conductorId);
            const container = document.getElementById('seguimientosContainer');
            if (container) container.innerHTML = renderSeguimientosTable(seguimientos, e.target.value);
            attachTableButtons();
        });
    }
    attachTableButtons();
}

function attachTableButtons() {
    document.querySelectorAll('.btn-update-location').forEach(btn => {
        btn.removeEventListener('click', locationHandler);
        btn.addEventListener('click', locationHandler);
    });
    document.querySelectorAll('.btn-change-status').forEach(btn => {
        btn.removeEventListener('click', statusHandler);
        btn.addEventListener('click', statusHandler);
    });
    document.querySelectorAll('.btn-complete').forEach(btn => {
        btn.removeEventListener('click', completeHandler);
        btn.addEventListener('click', completeHandler);
    });
    document.querySelectorAll('.btn-view-map').forEach(btn => {
        btn.removeEventListener('click', viewMapHandler);
        btn.addEventListener('click', viewMapHandler);
    });
}
async function locationHandler(e) {
    const seguimientoId = e.currentTarget.dataset.id;
    const pedidoId = e.currentTarget.dataset.pedido;
    await updateLocation(seguimientoId, pedidoId);
}
async function statusHandler(e) {
    const seguimientoId = e.currentTarget.dataset.id;
    const nuevoEstado = e.currentTarget.dataset.status;
    const pedidoId = e.currentTarget.dataset.pedido || (await apiFetch(`/Seguimiento/${seguimientoId}`)).pedidoId;
    await changeStatus(seguimientoId, pedidoId, nuevoEstado);
}
async function completeHandler(e) {
    const seguimientoId = e.currentTarget.dataset.id;
    const pedidoId = e.currentTarget.dataset.pedido;
    await completeDelivery(seguimientoId, pedidoId);
}
async function viewMapHandler(e) {
    const pedidoId = e.currentTarget.dataset.pedido;
    const lat = e.currentTarget.dataset.lat;
    const lng = e.currentTarget.dataset.lng;
    await viewOnMap(pedidoId, parseFloat(lat), parseFloat(lng));
}