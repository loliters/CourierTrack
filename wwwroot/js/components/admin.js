// components/admin.js
import { apiFetch } from '../utils/api.js';
import { MAPBOX_TOKEN } from '../config.js'; 

let currentMap = null;
let currentEntity = 'resumen';
let allData = [];
let currentPage = 1;
let currentUser = null;
const itemsPerPage = 10;

// Configuración completa de las 33 tablas
const entityConfig = {
    pedidos: { endpoint: '/Pedido', displayName: 'Pedidos' },
    usuarios: { endpoint: '/Usuario', displayName: 'Usuarios' },
    conductores: { endpoint: '/Conductor', displayName: 'Conductores' },
    vehiculos: { endpoint: '/Vehiculo', displayName: 'Vehículos' },
    rol: { endpoint: '/Rol', displayName: 'Roles' },
    genero: { endpoint: '/Genero', displayName: 'Géneros' },
    tipocliente: { endpoint: '/TipoCliente', displayName: 'Tipos Cliente' },
    tipodocumento: { endpoint: '/TipoDocumento', displayName: 'Tipos Documento' },
    extensionci: { endpoint: '/ExtensionCI', displayName: 'Extensiones CI' },
    estado: { endpoint: '/Estado', displayName: 'Estados Pedido' },
    estadopago: { endpoint: '/EstadoPago', displayName: 'Estados Pago' },
    tipolicencia: { endpoint: '/TipoLicencia', displayName: 'Tipos Licencia' },
    marca: { endpoint: '/Marca', displayName: 'Marcas' },
    tipovehiculo: { endpoint: '/TipoVehiculo', displayName: 'Tipos Vehículo' },
    metodopago: { endpoint: '/MetodoPago', displayName: 'Métodos Pago' },
    color: { endpoint: '/Color', displayName: 'Colores' },
    aniovehiculo: { endpoint: '/AnioVehiculo', displayName: 'Años Vehículo' },
    ubicacion: { endpoint: '/Ubicacion', displayName: 'Ubicaciones' },
    tarifa: { endpoint: '/Tarifa', displayName: 'Tarifas' },
    cliente: { endpoint: '/Cliente', displayName: 'Clientes' },
    clientejuridico: { endpoint: '/ClienteJuridico', displayName: 'Clientes Jurídicos' },
    clientenatural: { endpoint: '/ClienteNatural', displayName: 'Clientes Naturales' },
    detallepedido: { endpoint: '/DetallePedido', displayName: 'Detalles Pedido' },
    seguimiento: { endpoint: '/Seguimiento', displayName: 'Seguimientos' },
    calificacion: { endpoint: '/Calificacion', displayName: 'Calificaciones' },
    estadopedido: { endpoint: '/EstadoPedido', displayName: 'Estado Pedido' },
    direccionorigen: { endpoint: '/DireccionOrigen', displayName: 'Dirección Origen' },
    direcciondestino: { endpoint: '/DireccionDestino', displayName: 'Dirección Destino' },
    historialubicacion: { endpoint: '/HistorialUbicacion', displayName: 'Historial Ubicación' },
    modelo: { endpoint: '/Modelo', displayName: 'Modelos' },
    notificacion: { endpoint: '/Notificacion', displayName: 'Notificaciones' },
    pago: { endpoint: '/Pago', displayName: 'Pagos' },
    usuarioubicacion: { endpoint: '/UsuarioUbicacion', displayName: 'Usuario Ubicación' }
};

// ---------- RENDERIZADO INICIAL (sin bloqueos) ----------
export async function renderAdminDashboard(usuario) {
    currentUser = usuario;
    const sidebarLinks = Object.entries(entityConfig).map(([key, config]) => `
        <li><a href="#" data-entity="${key}" class="sidebar-link">${config.displayName}</a></li>
    `).join('');

    const html = `
        <div class="admin-layout">
            <aside class="admin-sidebar">
                <h3>📋 Navegación</h3>
                <ul>
                    <li><a href="#" data-entity="resumen" class="sidebar-link active">🏠 Resumen General</a></li>
                </ul>
                <div class="stats-mini">
                    <strong>Estadísticas rápidas</strong>
                    <div id="statsMiniContent">Cargando estadísticas...</div>
                </div>
                <hr>
                <ul class="full-table-list">
                    ${sidebarLinks}
                </ul>
            </aside>
            <div class="admin-content">
                <div id="adminMainPanel">Cargando panel...</div>
            </div>
        </div>
        <button class="btn-floating" id="fabAdd">+</button>
        <div id="genericModal" class="modal" style="display:none;">
            <div class="modal-content">
                <span class="close">&times;</span>
                <h3 id="modalTitle">Título</h3>
                <div id="modalBody"></div>
                <button id="modalSaveBtn" class="btn-primary">Guardar</button>
            </div>
        </div>
        <div id="trackingModal" class="modal" style="display:none;">
            <div class="modal-content">
                <span class="close">&times;</span>
                <h3>Rastreo del pedido</h3>
                <div id="trackingMap" class="map-container"></div>
                <div id="trackingInfo"></div>
            </div>
        </div>
    `;

    // Cargar estadísticas en segundo plano
    setTimeout(() => cargarEstadisticasEnSegundoPlano(), 0);
    return html;
}

// ---------- FUNCIONES AUXILIARES ----------
async function cargarEstadisticasEnSegundoPlano() {
    const entries = Object.entries(entityConfig);
    const results = await Promise.allSettled(
        entries.map(async ([key, config]) => {
            const data = await apiFetch(config.endpoint).catch(() => []);
            return { key, displayName: config.displayName, count: data.length };
        })
    );
    const statsArray = results.filter(r => r.status === 'fulfilled').map(r => r.value);

    // Actualizar sidebar
    const statsMiniHtml = statsArray.slice(0, 6).map(s => `
        <p><i class="fas fa-database"></i> ${s.displayName}: ${s.count}</p>
    `).join('');
    const statsDiv = document.querySelector('#statsMiniContent');
    if (statsDiv) statsDiv.innerHTML = statsMiniHtml;

    // Si el resumen ya está visible, actualizar tarjetas
    const resumenGrid = document.getElementById('resumenGrid');
    if (resumenGrid) {
        statsArray.forEach(stat => {
            const card = resumenGrid.querySelector(`.resumen-card[data-entity="${stat.key}"] .resumen-count`);
            if (card) card.textContent = stat.count;
        });
    }
}

async function updateStatsInSidebar() {
    const entries = Object.entries(entityConfig);
    const results = await Promise.allSettled(
        entries.map(async ([key, config]) => {
            const data = await apiFetch(config.endpoint).catch(() => []);
            return { key, displayName: config.displayName, count: data.length };
        })
    );
    const statsArray = results.filter(r => r.status === 'fulfilled').map(r => r.value);
    const statsMiniHtml = statsArray.slice(0, 6).map(s => `
        <p><i class="fas fa-database"></i> ${s.displayName}: ${s.count}</p>
    `).join('');
    const statsDiv = document.querySelector('#statsMiniContent');
    if (statsDiv) statsDiv.innerHTML = statsMiniHtml;
}

async function renderResumen(usuario) {
    if (!usuario) return '<div class="card">Error: Usuario no disponible</div>';
    const nombreCompleto = `${usuario.nombre || ''} ${usuario.apPat || ''} ${usuario.apMat || ''}`.trim();
    const cardsPlaceholder = Object.entries(entityConfig).map(([key, config]) => `
        <div class="stat-card-admin resumen-card" data-entity="${key}">
            <i class="fas fa-table"></i>
            <h3 class="resumen-count" data-key="${key}">---</h3>
            <p>${config.displayName}</p>
        </div>
    `).join('');

    return `
        <div class="dashboard-resumen">
            <div class="welcome-message">
                <h2>Bienvenido, ${nombreCompleto || 'Administrador'}</h2>
                <p>Panel de control - ${Object.keys(entityConfig).length} tablas gestionadas</p>
            </div>
            <div class="stats-grid resumen-grid" id="resumenGrid">
                ${cardsPlaceholder}
            </div>
        </div>
    `;
}

async function loadEntityData(entity) {
    const config = entityConfig[entity];
    if (!config) return [];
    const data = await apiFetch(config.endpoint).catch(() => []);
    allData = data;
    return data;
}

function renderEntityTable(entity, data, searchTerm = '') {
    const config = entityConfig[entity];
    if (!config) return '<p>Entidad no soportada</p>';
    let filtered = data.filter(item =>
        JSON.stringify(item).toLowerCase().includes(searchTerm.toLowerCase())
    );
    const totalItems = filtered.length;
    const totalPages = Math.ceil(totalItems / itemsPerPage);
    const start = (currentPage - 1) * itemsPerPage;
    const paginated = filtered.slice(start, start + itemsPerPage);
    let tableHtml = `<div class="table-wrapper"><div class="responsive-table"><table class="admin-table"><thead><tr>`;
    if (entity === 'usuarios') {
        tableHtml += `<th>ID</th><th>Nombre</th><th>Correo</th><th>Rol</th><th>Acciones</th>`;
    } else if (entity === 'pedidos') {
        tableHtml += `<th>ID</th><th>Cliente</th><th>Conductor</th><th>Peso (kg)</th><th>Dist. (km)</th><th>Costo ($)</th><th>Estado</th><th>Acciones</th>`;
    } else if (entity === 'conductores') {
        tableHtml += `<th>ID</th><th>N° Licencia</th><th>Usuario ID</th><th>Tipo Licencia</th><th>Acciones</th>`;
    } else if (entity === 'vehiculos') {
        tableHtml += `<th>ID</th><th>Placa</th><th>Modelo</th><th>Color</th><th>Año</th><th>Conductor</th><th>Acciones</th>`;
    } else {
        tableHtml += `<th>ID</th><th>Nombre / Valor</th><th>Acciones</th>`;
    }
    tableHtml += `</tr></thead><tbody>`;
    if (paginated.length === 0) {
        tableHtml += `<tr><td colspan="5" class="empty-message">No hay registros que coincidan</td></tr>`;
    } else {
        for (const item of paginated) {
            if (entity === 'usuarios') {
                let roleClass = '';
                let roleText = item.rolNombre || 'Sin rol';
                if (roleText === 'ADMINISTRADOR') roleClass = 'badge-admin';
                else if (roleText === 'CONDUCTOR') roleClass = 'badge-conductor';
                else if (roleText === 'CLIENTE') roleClass = 'badge-cliente';
                tableHtml += `<tr><td>${item.id}</td><td>${escapeHtml(item.nombre || '')}</td><td>${escapeHtml(item.correo || '')}</td><td><span class="role-badge ${roleClass}">${roleText}</span></td><td class="actions-cell"><button class="btn-icon btn-edit" data-id="${item.id}" title="Editar"><i class="fas fa-edit"></i></button><button class="btn-icon btn-delete" data-id="${item.id}" title="Eliminar"><i class="fas fa-trash-alt"></i></button></td></tr>`;
            } else if (entity === 'pedidos') {
                tableHtml += `<tr><td>${item.id}</td><td>${item.clienteId}</td><td>${item.conductorId || '—'}</td><td>${item.pesokg}</td><td>${item.distanciakm}</td><td>$${item.costototal?.toFixed(2) || '0.00'}</td><td><span class="status-badge">${item.estadosPedidos?.[0]?.estado?.nombre || 'Pendiente'}</span></td><td class="actions-cell"><button class="btn-icon btn-rastrear" data-id="${item.id}" title="Rastrear"><i class="fas fa-map-marker-alt"></i></button><button class="btn-icon btn-edit" data-id="${item.id}" title="Editar"><i class="fas fa-edit"></i></button><button class="btn-icon btn-delete" data-id="${item.id}" title="Eliminar"><i class="fas fa-trash-alt"></i></button></td></tr>`;
            } else if (entity === 'conductores') {
                tableHtml += `<tr><td>${item.id}</td><td>${item.nroLicencia || '—'}</td><td>${item.usuarioId}</td><td>${item.tipoLicenciaId}</td><td class="actions-cell"><button class="btn-icon btn-edit" data-id="${item.id}" title="Editar"><i class="fas fa-edit"></i></button><button class="btn-icon btn-delete" data-id="${item.id}" title="Eliminar"><i class="fas fa-trash-alt"></i></button></td></tr>`;
            } else if (entity === 'vehiculos') {
                tableHtml += `<tr><td>${item.id}</td><td>${item.placa}</td><td>${item.modeloId}</td><td>${item.colorId}</td><td>${item.anioVehiculoId}</td><td>${item.conductorId || '—'}</td><td class="actions-cell"><button class="btn-icon btn-edit" data-id="${item.id}" title="Editar"><i class="fas fa-edit"></i></button><button class="btn-icon btn-delete" data-id="${item.id}" title="Eliminar"><i class="fas fa-trash-alt"></i></button></td></tr>`;
            } else {
                let displayValue = item.nombre || item.categoria || item.anio || `${item.latitud}, ${item.longitud}` || item.descripcion || `ID: ${item.id}`;
                tableHtml += `<tr><td>${item.id}</td><td>${escapeHtml(displayValue)}</td><td class="actions-cell"><button class="btn-icon btn-edit" data-id="${item.id}" title="Editar"><i class="fas fa-edit"></i></button><button class="btn-icon btn-delete" data-id="${item.id}" title="Eliminar"><i class="fas fa-trash-alt"></i></button></td></tr>`;
            }
        }
    }
    tableHtml += `</tbody></table></div>`;
    if (totalPages > 1) {
        tableHtml += `<div class="pagination-controls">`;
        for (let i = 1; i <= totalPages; i++) {
            tableHtml += `<button class="page-btn ${i === currentPage ? 'active' : ''}" data-page="${i}">${i}</button>`;
        }
        tableHtml += `</div>`;
    }
    tableHtml += `</div>`;
    return tableHtml;
}

function escapeHtml(str) {
    if (!str) return '';
    return str.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;').replace(/'/g, '&#39;');
}

// ---------- MANEJO DE PANELES Y EVENTOS ----------
async function showEntityPanel(entity) {
    currentEntity = entity;
    currentPage = 1;
    if (entity === 'resumen') {
        const resumenHtml = await renderResumen(currentUser);
        document.getElementById('adminMainPanel').innerHTML = resumenHtml;
        attachResumenCardEvents();
        await updateStatsInSidebar();
        return;
    }
    const data = await loadEntityData(entity);
    const searchHtml = `<div class="search-bar"><input type="text" id="searchInput" placeholder="Buscar..."></div>`;
    const tableHtml = renderEntityTable(entity, data);
    document.getElementById('adminMainPanel').innerHTML = searchHtml + tableHtml;
    await updateStatsInSidebar();
    const searchInput = document.getElementById('searchInput');
    if (searchInput) {
        searchInput.oninput = (e) => {
            currentPage = 1;
            const filtered = allData.filter(item => JSON.stringify(item).toLowerCase().includes(e.target.value.toLowerCase()));
            document.getElementById('adminMainPanel').innerHTML = searchInput.outerHTML + renderEntityTable(entity, filtered);
            attachTableButtons();
            attachPaginationEvents();
        };
    }
    attachTableButtons();
    attachPaginationEvents();
}

function attachResumenCardEvents() {
    document.querySelectorAll('.resumen-card').forEach(card => {
        card.removeEventListener('click', resumenCardHandler);
        card.addEventListener('click', resumenCardHandler);
    });
}
async function resumenCardHandler(e) {
    const card = e.currentTarget;
    const entity = card.dataset.entity;
    if (entity) {
        await showEntityPanel(entity);
        document.querySelectorAll('.sidebar-link').forEach(l => l.classList.remove('active'));
        const sidebarLink = document.querySelector(`.sidebar-link[data-entity="${entity}"]`);
        if (sidebarLink) sidebarLink.classList.add('active');
    }
}

function attachPaginationEvents() {
    document.querySelectorAll('.page-btn').forEach(btn => {
        btn.removeEventListener('click', paginationHandler);
        btn.addEventListener('click', paginationHandler);
    });
}
async function paginationHandler(e) {
    currentPage = parseInt(e.target.dataset.page);
    const data = await loadEntityData(currentEntity);
    const searchInput = document.getElementById('searchInput');
    let filtered = data;
    if (searchInput && searchInput.value) {
        filtered = data.filter(item => JSON.stringify(item).toLowerCase().includes(searchInput.value.toLowerCase()));
    }
    const searchHtml = searchInput ? searchInput.outerHTML : '';
    document.getElementById('adminMainPanel').innerHTML = searchHtml + renderEntityTable(currentEntity, filtered);
    attachTableButtons();
    attachPaginationEvents();
}

function attachTableButtons() {
    document.querySelectorAll('.btn-edit').forEach(btn => {
        btn.removeEventListener('click', editHandler);
        btn.addEventListener('click', editHandler);
    });
    document.querySelectorAll('.btn-delete').forEach(btn => {
        btn.removeEventListener('click', deleteHandler);
        btn.addEventListener('click', deleteHandler);
    });
    document.querySelectorAll('.btn-rastrear').forEach(btn => {
        btn.removeEventListener('click', rastrearHandler);
        btn.addEventListener('click', rastrearHandler);
    });
}
async function editHandler(e) {
    const id = e.currentTarget.dataset.id;
    await showEditModal(currentEntity, id);
}
async function deleteHandler(e) {
    const id = e.currentTarget.dataset.id;
    if (!confirm('¿Eliminar permanentemente?')) return;
    const config = entityConfig[currentEntity];
    await apiFetch(`${config.endpoint}/${id}`, { method: 'DELETE' });
    await showEntityPanel(currentEntity);
    await updateStatsInSidebar();
}
async function rastrearHandler(e) {
    const pedidoId = e.currentTarget.dataset.id;
    await rastrearPedido(pedidoId);
}

async function rastrearPedido(pedidoId) {
    try {
        const todosSeguimientos = await apiFetch('/Seguimiento').catch(() => []);
        const seguimientos = todosSeguimientos.filter(s => s.pedidoId == pedidoId);
        let ubicacion;
        if (!seguimientos.length) {
            const ubicaciones = await apiFetch('/Ubicacion').catch(() => []);
            if (ubicaciones.length) {
                ubicacion = ubicaciones[0];
            } else {
                ubicacion = { latitud: -17.389577, longitud: -66.157607 };
            }
            document.getElementById('trackingInfo').innerHTML = `<p><em>No hay seguimientos registrados para este pedido. Mostrando ubicación por defecto.</em></p>`;
        } else {
            const ultimo = seguimientos[seguimientos.length - 1];
            ubicacion = await apiFetch(`/Ubicacion/${ultimo.ubicacionId}`).catch(() => null);
            if (!ubicacion) {
                alert('No se encontró la ubicación del seguimiento');
                return;
            }
            document.getElementById('trackingInfo').innerHTML = `<p><strong>Último seguimiento:</strong> ${ultimo.observacion || 'Sin observación'} - ${new Date(ultimo.fecha).toLocaleString()}</p>`;
        }
        const modal = document.getElementById('trackingModal');
        modal.style.display = 'flex';
        setTimeout(() => {
            if (currentMap) currentMap.remove();
            mapboxgl.accessToken = MAPBOX_TOKEN;
            currentMap = new mapboxgl.Map({
                container: 'trackingMap',
                style: 'mapbox://styles/mapbox/streets-v12',
                center: [ubicacion.longitud, ubicacion.latitud],
                zoom: 14
            });
            currentMap.on('load', () => {
                new mapboxgl.Marker().setLngLat([ubicacion.longitud, ubicacion.latitud]).addTo(currentMap);
            });
        }, 150);
        const closeModal = () => {
            modal.style.display = 'none';
            if (currentMap) {
                currentMap.remove();
                currentMap = null;
            }
        };
        modal.querySelector('.close').onclick = closeModal;
        window.onclick = (e) => { if (e.target === modal) closeModal(); };
    } catch (err) {
        console.error(err);
        alert('Error al cargar el mapa: ' + err.message);
    }
}

// ---------- MODALES ----------
async function showCreateModal(entity) {
    const config = entityConfig[entity];
    if (!config) return;
    let formHtml = '';
    if (entity === 'pedidos') {
        formHtml = `
            <div class="form-group"><label>Cliente ID</label><input id="clienteId" type="number" required></div>
            <div class="form-group"><label>Peso kg</label><input id="peso" type="number" step="0.1" required></div>
            <div class="form-group"><label>Distancia km</label><input id="distancia" type="number" step="0.1" required></div>
            <div class="form-group"><label>Frágil</label><select id="fragil"><option value="true">Sí</option><option value="false">No</option></select></div>
            <div class="form-group"><label>Tipo Vehículo ID</label><input id="tipoVehiculoId" type="number" required></div>
            <div class="form-group"><label>Detalle Pedido ID</label><input id="detallePedidoId" type="number" required></div>
        `;
    } else if (entity === 'usuarios') {
        formHtml = `
            <div class="form-group"><label>Nombre</label><input id="nombre" required></div>
            <div class="form-group"><label>Email</label><input id="correo" type="email" required></div>
            <div class="form-group"><label>Contraseña</label><input id="password" type="password" required></div>
            <div class="form-group"><label>Rol ID</label><input id="rolId" type="number" required></div>
        `;
    } else {
        formHtml = `<div class="form-group"><label>Nombre</label><input id="nombre" required></div>`;
    }
    const modal = document.getElementById('genericModal');
    document.getElementById('modalTitle').innerText = `Crear ${config.displayName}`;
    document.getElementById('modalBody').innerHTML = formHtml;
    modal.style.display = 'flex';
    const saveBtn = document.getElementById('modalSaveBtn');
    saveBtn.onclick = async () => {
        let data = {};
        if (entity === 'pedidos') {
            data = {
                fragil: document.getElementById('fragil').value === 'true',
                pesokg: parseFloat(document.getElementById('peso').value),
                distanciakm: parseFloat(document.getElementById('distancia').value),
                costototal: 0,
                tipoVehiculoId: parseInt(document.getElementById('tipoVehiculoId').value),
                clienteId: parseInt(document.getElementById('clienteId').value),
                detallePedidoId: parseInt(document.getElementById('detallePedidoId').value),
                estadoIds: [1]
            };
        } else if (entity === 'usuarios') {
            data = {
                nombre: document.getElementById('nombre').value,
                apPat: '',
                apMat: '',
                correo: document.getElementById('correo').value,
                telefono: '',
                password: document.getElementById('password').value,
                rolId: parseInt(document.getElementById('rolId').value),
                ubicacionesIds: []
            };
        } else {
            data = { nombre: document.getElementById('nombre').value };
        }
        try {
            await apiFetch(config.endpoint, { method: 'POST', body: JSON.stringify(data) });
            modal.style.display = 'none';
            await showEntityPanel(currentEntity);
            await updateStatsInSidebar();
        } catch (err) { alert('Error: ' + err.message); }
    };
    modal.querySelector('.close').onclick = () => modal.style.display = 'none';
    window.onclick = (e) => { if (e.target === modal) modal.style.display = 'none'; };
}

async function showEditModal(entity, id) {
    const config = entityConfig[entity];
    const item = allData.find(x => x.id == id);
    if (!item) return;
    let formHtml = '';
    if (entity === 'pedidos') {
        formHtml = `
            <div class="form-group"><label>Cliente ID</label><input id="clienteId" value="${item.clienteId}" type="number"></div>
            <div class="form-group"><label>Peso kg</label><input id="peso" value="${item.pesokg}" step="0.1"></div>
            <div class="form-group"><label>Distancia km</label><input id="distancia" value="${item.distanciakm}" step="0.1"></div>
            <div class="form-group"><label>Frágil</label><select id="fragil"><option value="true" ${item.fragil ? 'selected' : ''}>Sí</option><option value="false" ${!item.fragil ? 'selected' : ''}>No</option></select></div>
            <div class="form-group"><label>Tipo Vehículo ID</label><input id="tipoVehiculoId" value="${item.tipoVehiculoId}" type="number"></div>
            <div class="form-group"><label>Detalle Pedido ID</label><input id="detallePedidoId" value="${item.detallePedidoId}" type="number"></div>
        `;
    } else if (entity === 'usuarios') {
        formHtml = `
            <div class="form-group"><label>Nombre</label><input id="nombre" value="${item.nombre}"></div>
            <div class="form-group"><label>Email</label><input id="correo" value="${item.correo}" type="email"></div>
            <div class="form-group"><label>Rol ID</label><input id="rolId" value="${item.rolId}" type="number"></div>
        `;
    } else {
        let prop = 'nombre';
        let value = item[prop];
        if (entity === 'aniovehiculo') { prop = 'anio'; value = item.anio; }
        else if (entity === 'ubicacion') { prop = 'latitud'; value = item.latitud; }
        formHtml = `<div class="form-group"><label>${prop}</label><input id="${prop}" value="${value}"></div>`;
    }
    const modal = document.getElementById('genericModal');
    document.getElementById('modalTitle').innerText = `Editar ${config.displayName}`;
    document.getElementById('modalBody').innerHTML = formHtml;
    modal.style.display = 'flex';
    const saveBtn = document.getElementById('modalSaveBtn');
    saveBtn.onclick = async () => {
        let updated = { ...item };
        if (entity === 'pedidos') {
            updated.clienteId = parseInt(document.getElementById('clienteId').value);
            updated.pesokg = parseFloat(document.getElementById('peso').value);
            updated.distanciakm = parseFloat(document.getElementById('distancia').value);
            updated.fragil = document.getElementById('fragil').value === 'true';
            updated.tipoVehiculoId = parseInt(document.getElementById('tipoVehiculoId').value);
            updated.detallePedidoId = parseInt(document.getElementById('detallePedidoId').value);
        } else if (entity === 'usuarios') {
            updated.nombre = document.getElementById('nombre').value;
            updated.correo = document.getElementById('correo').value;
            updated.rolId = parseInt(document.getElementById('rolId').value);
        } else {
            let prop = 'nombre';
            if (entity === 'aniovehiculo') prop = 'anio';
            else if (entity === 'ubicacion') prop = 'latitud';
            updated[prop] = document.getElementById(prop).value;
            if (prop === 'anio') updated[prop] = parseInt(updated[prop]);
            else if (prop === 'latitud') updated[prop] = parseFloat(updated[prop]);
        }
        try {
            await apiFetch(`${config.endpoint}/${id}`, { method: 'PUT', body: JSON.stringify(updated) });
            modal.style.display = 'none';
            await showEntityPanel(currentEntity);
            await updateStatsInSidebar();
        } catch (err) { alert('Error: ' + err.message); }
    };
    modal.querySelector('.close').onclick = () => modal.style.display = 'none';
    window.onclick = (e) => { if (e.target === modal) modal.style.display = 'none'; };
}

// ---------- EXPORTAR EVENTOS PRINCIPALES ----------
export async function attachAdminEvents(usuario) {
    console.log('attachAdminEvents recibió usuario:', usuario);
    currentUser = usuario;
    await showEntityPanel('resumen');

    document.querySelectorAll('.sidebar-link').forEach(link => {
        link.addEventListener('click', async (e) => {
            e.preventDefault();
            document.querySelectorAll('.sidebar-link').forEach(l => l.classList.remove('active'));
            link.classList.add('active');
            const entity = link.dataset.entity;
            await showEntityPanel(entity);
        });
    });

    document.getElementById('fabAdd')?.addEventListener('click', () => {
        if (currentEntity !== 'resumen') {
            showCreateModal(currentEntity);
        } else {
            alert('Para crear, selecciona una tabla específica');
        }
    });
}