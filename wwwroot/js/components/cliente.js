// js/components/cliente.js
import { apiFetch } from '../utils/api.js';
import { getCurrentUser } from '../utils/auth.js';
import { MAPBOX_TOKEN } from '../config.js';

let currentMap = null;
let currentTrackingMap = null;

// Obtener o crear cliente asociado al usuario
async function getOrCreateCliente(userId) {
    try {
        // 1. Buscar cliente existente
        const clientes = await apiFetch('/Cliente');
        let cliente = clientes.find(c => c.usuarioId === userId);
        if (cliente) return cliente.id;

        // 2. No existe → crear nuevo cliente
        const nuevoCliente = {
            usuarioId: userId,
            tipoClienteId: 1,      // 1 = Natural (ajusta según tu BD)
            clienteNaturalId: null,
            clienteJuridicoId: null,
            fechaRegistro: new Date().toISOString()
        };
        const response = await apiFetch('/Cliente', {
            method: 'POST',
            body: JSON.stringify(nuevoCliente)
        });
        if (response && response.id) {
            console.log('Cliente creado automáticamente:', response);
            return response.id;
        }
        throw new Error('No se pudo crear el cliente');
    } catch (error) {
        console.error('Error en getOrCreateCliente:', error);
        return null;
    }
}

// Obtener pedidos del cliente
async function getPedidos(clienteId) {
    try {
        const pedidos = await apiFetch(`/Pedido?clienteId=${clienteId}`);
        for (const pedido of pedidos) {
            const seguimientos = await apiFetch(`/Seguimiento?pedidoId=${pedido.id}`).catch(() => []);
            if (seguimientos.length > 0) {
                const ultimo = seguimientos.sort((a, b) => new Date(b.fecha) - new Date(a.fecha))[0];
                const estado = await apiFetch(`/Estado/${ultimo.estadoId}`).catch(() => null);
                pedido.estadoActual = estado?.nombre || 'Pendiente';
            } else {
                pedido.estadoActual = 'Pendiente';
            }
        }
        return pedidos;
    } catch (e) {
        console.error('Error cargando pedidos:', e);
        return [];
    }
}

// Calcular estadísticas
function calcularEstadisticas(pedidos) {
    const total = pedidos.length;
    const enCurso = pedidos.filter(p => ['Pendiente', 'En Ruta', 'Asignado'].includes(p.estadoActual)).length;
    const entregados = pedidos.filter(p => p.estadoActual === 'Entregado').length;
    const cancelados = pedidos.filter(p => p.estadoActual === 'Cancelado').length;
    return { total, enCurso, entregados, cancelados };
}

// Renderizar estadísticas
function renderStats(stats) {
    return `
        <div class="stats-grid cliente-stats">
            <div class="stat-card">
                <i class="fas fa-box"></i>
                <h3>${stats.total}</h3>
                <p>Total Pedidos</p>
            </div>
            <div class="stat-card stat-curso">
                <i class="fas fa-truck-moving"></i>
                <h3>${stats.enCurso}</h3>
                <p>En curso</p>
            </div>
            <div class="stat-card stat-entregado">
                <i class="fas fa-check-circle"></i>
                <h3>${stats.entregados}</h3>
                <p>Entregados</p>
            </div>
            <div class="stat-card stat-cancelado">
                <i class="fas fa-times-circle"></i>
                <h3>${stats.cancelados}</h3>
                <p>Cancelados</p>
            </div>
        </div>
    `;
}

// Renderizar tabla de pedidos
function renderPedidosTable(pedidos, searchTerm = '') {
    const filtered = pedidos.filter(p =>
        p.id.toString().includes(searchTerm) ||
        p.estadoActual?.toLowerCase().includes(searchTerm.toLowerCase())
    );
    if (filtered.length === 0) {
        return '<div class="empty-state">No hay pedidos que coincidan con la búsqueda.</div>';
    }
    return `
        <div class="table-responsive">
            <table class="admin-table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Peso (kg)</th>
                        <th>Distancia (km)</th>
                        <th>Costo (Bs)</th>
                        <th>Estado</th>
                        <th>Fecha</th>
                        <th>Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    ${filtered.map(pedido => `
                        <tr>
                            <td>${pedido.id}</td>
                            <td>${pedido.pesokg}</td>
                            <td>${pedido.distanciakm}</td>
                            <td>${pedido.costototal?.toFixed(2) || '0.00'}</td>
                            <td><span class="status-badge status-${pedido.estadoActual?.toLowerCase().replace(/\s/g, '')}">${pedido.estadoActual || 'Pendiente'}</span></td>
                            <td>${new Date(pedido.fechaCreacion || Date.now()).toLocaleDateString()}</td>
                            <td>
                                <button class="btn-icon btn-track" data-id="${pedido.id}" title="Rastrear"><i class="fas fa-map-marker-alt"></i></button>
                                ${pedido.estadoActual === 'Pendiente' ? `<button class="btn-icon btn-cancel" data-id="${pedido.id}" title="Cancelar"><i class="fas fa-ban"></i></button>` : ''}
                            </td>
                        </tr>
                    `).join('')}
                </tbody>
            </table>
        </div>
    `;
}

// Modal de nuevo pedido (resumido, igual que antes pero sin errores)
function showNewPedidoModal() {
    const modalHtml = `
        <div id="newPedidoModal" class="modal" style="display:flex;">
            <div class="modal-content modal-large">
                <span class="close" id="closeNewPedidoModal">&times;</span>
                <h3>Nuevo Pedido</h3>
                <form id="newPedidoForm">
                    <div class="form-row">
                        <div class="form-group"><label>Peso (kg)</label><input type="number" id="peso" step="0.1" required></div>
                        <div class="form-group"><label>Distancia (km)</label><input type="number" id="distancia" step="0.1" required></div>
                        <div class="form-group"><label>Frágil</label><select id="fragil"><option value="false">No</option><option value="true">Sí</option></select></div>
                        <div class="form-group"><label>Tipo Vehículo</label><select id="tipoVehiculoId"></select></div>
                    </div>
                    <div class="form-row">
                        <div class="form-group"><label>Origen (lat,lon)</label><input id="origenCoords" placeholder="lat,lon"></div>
                        <div class="form-group"><label>Destino (lat,lon)</label><input id="destinoCoords" placeholder="lat,lon"></div>
                    </div>
                    <div class="form-actions">
                        <button type="submit" class="btn-primary">Crear Pedido</button>
                        <button type="button" class="btn-secondary" id="cancelNewPedido">Cancelar</button>
                    </div>
                </form>
            </div>
        </div>
    `;
    document.body.insertAdjacentHTML('beforeend', modalHtml);
    const modal = document.getElementById('newPedidoModal');
    const closeBtn = document.getElementById('closeNewPedidoModal');
    const cancelBtn = document.getElementById('cancelNewPedido');
    const form = document.getElementById('newPedidoForm');

    closeBtn.onclick = () => modal.remove();
    cancelBtn.onclick = () => modal.remove();
    modal.onclick = (e) => { if (e.target === modal) modal.remove(); };

    // Cargar tipos de vehículo
    apiFetch('/TipoVehiculo').then(tipos => {
        const select = document.getElementById('tipoVehiculoId');
        select.innerHTML = tipos.map(t => `<option value="${t.id}">${t.nombre}</option>`).join('');
    }).catch(() => {
        document.getElementById('tipoVehiculoId').innerHTML = '<option value="1">Moto</option><option value="2">Auto</option>';
    });

    form.onsubmit = async (e) => {
        e.preventDefault();
        const fragil = document.getElementById('fragil').value === 'true';
        const pesokg = parseFloat(document.getElementById('peso').value);
        const distanciakm = parseFloat(document.getElementById('distancia').value);
        const tipoVehiculoId = parseInt(document.getElementById('tipoVehiculoId').value);
        const origen = document.getElementById('origenCoords').value.split(',');
        const destino = document.getElementById('destinoCoords').value.split(',');
        if (origen.length !== 2 || destino.length !== 2) {
            alert('Ingresa coordenadas válidas (lat,lon)');
            return;
        }
        const origenLat = parseFloat(origen[0]);
        const origenLng = parseFloat(origen[1]);
        const destinoLat = parseFloat(destino[0]);
        const destinoLng = parseFloat(destino[1]);

        // Crear ubicaciones
        const ubicOrigen = await apiFetch('/Ubicacion', { method: 'POST', body: JSON.stringify({ latitud: origenLat, longitud: origenLng }) });
        const ubicDestino = await apiFetch('/Ubicacion', { method: 'POST', body: JSON.stringify({ latitud: destinoLat, longitud: destinoLng }) });

        const user = getCurrentUser();
        const clienteId = await getOrCreateCliente(user.id);
        if (!clienteId) {
            alert('Error: No se pudo identificar el cliente');
            return;
        }

        const pedidoData = {
            fragil,
            pesokg,
            distanciakm,
            costototal: 0,
            tipoVehiculoId,
            clienteId,
            ubicacionOrigenId: ubicOrigen.id,
            ubicacionDestinoId: ubicDestino.id,
            estadoIds: [1]
        };
        try {
            const nuevoPedido = await apiFetch('/Pedido', { method: 'POST', body: JSON.stringify(pedidoData) });
            if (nuevoPedido && nuevoPedido.id) {
                alert('Pedido creado exitosamente');
                modal.remove();
                renderClienteDashboard(); // refrescar
            } else {
                alert('Error al crear pedido');
            }
        } catch (err) {
            alert('Error: ' + err.message);
        }
    };
}

// Rastrear pedido
async function showTrackingModal(pedidoId) {
    try {
        const seguimientos = await apiFetch(`/Seguimiento?pedidoId=${pedidoId}`);
        if (!seguimientos || seguimientos.length === 0) {
            alert('No hay seguimientos para este pedido');
            return;
        }
        const ultimo = seguimientos.sort((a, b) => new Date(b.fecha) - new Date(a.fecha))[0];
        const ubicacion = await apiFetch(`/Ubicacion/${ultimo.ubicacionId}`);
        const modalHtml = `
            <div id="trackingModal" class="modal" style="display:flex;">
                <div class="modal-content modal-large">
                    <span class="close" id="closeTrackingModal">&times;</span>
                    <h3>Rastreo de Pedido #${pedidoId}</h3>
                    <div id="trackingMap" style="height: 400px; margin-bottom: 1rem;"></div>
                    <div id="trackingInfo">
                        <p><strong>Estado:</strong> ${ultimo.observacion || 'Sin observación'}</p>
                        <p><strong>Fecha:</strong> ${new Date(ultimo.fecha).toLocaleString()}</p>
                    </div>
                </div>
            </div>
        `;
        document.body.insertAdjacentHTML('beforeend', modalHtml);
        const modal = document.getElementById('trackingModal');
        const closeBtn = document.getElementById('closeTrackingModal');
        closeBtn.onclick = () => modal.remove();
        modal.onclick = (e) => { if (e.target === modal) modal.remove(); };
        setTimeout(() => {
            if (currentTrackingMap) currentTrackingMap.remove();
            mapboxgl.accessToken = MAPBOX_TOKEN;
            currentTrackingMap = new mapboxgl.Map({
                container: 'trackingMap',
                style: 'mapbox://styles/mapbox/streets-v12',
                center: [ubicacion.longitud, ubicacion.latitud],
                zoom: 14
            });
            currentTrackingMap.on('load', () => {
                new mapboxgl.Marker().setLngLat([ubicacion.longitud, ubicacion.latitud]).addTo(currentTrackingMap);
            });
        }, 100);
    } catch (err) {
        console.error(err);
        alert('Error al cargar el mapa');
    }
}

// Cancelar pedido
async function cancelPedido(pedidoId) {
    if (!confirm('¿Cancelar este pedido?')) return;
    try {
        await apiFetch(`/Pedido/${pedidoId}`, { method: 'DELETE' });
        alert('Pedido cancelado');
        renderClienteDashboard();
    } catch (err) {
        alert('Error al cancelar pedido');
    }
}

// Render principal
export async function renderClienteDashboard() {
    const user = getCurrentUser();
    if (!user) return '<div class="card">Error: Usuario no encontrado</div>';

    const clienteId = await getOrCreateCliente(user.id);
    if (!clienteId) {
        return '<div class="card">Error: No se pudo crear el cliente. Contacta al soporte.</div>';
    }
    const pedidos = await getPedidos(clienteId);
    const stats = calcularEstadisticas(pedidos);

    return `
        <div class="cliente-dashboard">
            <div class="welcome-message">
                <h2>Bienvenido, ${user.nombre || user.correo.split('@')[0]}</h2>
                <p>Aquí puedes gestionar tus envíos y realizar seguimiento en tiempo real.</p>
            </div>
            ${renderStats(stats)}
            <div class="card">
                <div class="card-header">
                    <h3>Mis Pedidos</h3>
                    <button class="btn-primary" id="nuevoPedidoBtn"><i class="fas fa-plus"></i> Nuevo Pedido</button>
                </div>
                <div class="search-bar">
                    <input type="text" id="searchPedidos" placeholder="Buscar por ID o estado...">
                </div>
                <div id="pedidosContainer">
                    ${renderPedidosTable(pedidos)}
                </div>
            </div>
        </div>
    `;
}

// Eventos
export function attachClienteEvents() {
    document.getElementById('nuevoPedidoBtn')?.addEventListener('click', () => showNewPedidoModal());
    const searchInput = document.getElementById('searchPedidos');
    if (searchInput) {
        searchInput.addEventListener('input', async (e) => {
            const user = getCurrentUser();
            const clienteId = await getOrCreateCliente(user.id);
            const pedidos = await getPedidos(clienteId);
            const container = document.getElementById('pedidosContainer');
            if (container) container.innerHTML = renderPedidosTable(pedidos, e.target.value);
            attachPedidoEvents();
        });
    }
    attachPedidoEvents();
}

function attachPedidoEvents() {
    document.querySelectorAll('.btn-track').forEach(btn => {
        btn.removeEventListener('click', trackHandler);
        btn.addEventListener('click', trackHandler);
    });
    document.querySelectorAll('.btn-cancel').forEach(btn => {
        btn.removeEventListener('click', cancelHandler);
        btn.addEventListener('click', cancelHandler);
    });
}
function trackHandler(e) {
    const id = e.currentTarget.dataset.id;
    showTrackingModal(id);
}
function cancelHandler(e) {
    const id = e.currentTarget.dataset.id;
    cancelPedido(id);
}