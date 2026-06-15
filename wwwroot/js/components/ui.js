export function renderizarUsuarios(usuarios, tablaBodyId = 'listaUsuarios') {
    const tbody = document.getElementById(tablaBodyId);
    if (!tbody) return;
    tbody.innerHTML = '';
    if (!usuarios.length) {
        tbody.innerHTML = '<tr><td colspan="5" class="text-center text-muted">No hay usuarios</td></tr>';
        return;
    }
    usuarios.forEach(u => {
        const rolNombre = u.rol?.nombre || 'Sin rol';
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${u.id}</td>
            <td>${u.nombre || ''}</td>
            <td>${u.correo}</td>
            <td>${rolNombre}</td>
            <td><button class="btn btn-sm btn-outline-primary editar-usuario" data-id="${u.id}">Editar</button></td>
        `;
        tbody.appendChild(tr);
    });
}

export function renderizarPedidos(pedidos, tablaBodyId = 'listaPedidos') {
    const tbody = document.getElementById(tablaBodyId);
    if (!tbody) return;
    tbody.innerHTML = '';
    if (!pedidos.length) {
        tbody.innerHTML = '<tr><td colspan="4" class="text-center">No hay pedidos</td></tr>';
        return;
    }
    pedidos.forEach(p => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${p.id}</td><td>${p.cliente?.nombre || 'N/A'}</td><td>${p.estado || 'Pendiente'}</td><td>$${p.total || 0}</td>`;
        tbody.appendChild(tr);
    });
}

// Similar para vehiculos y roles...
export function renderizarVehiculos(vehiculos, tablaBodyId = 'listaVehiculos') {
    const tbody = document.getElementById(tablaBodyId);
    if (!tbody) return;
    tbody.innerHTML = '';
    if (!vehiculos.length) {
        tbody.innerHTML = '<tr><td colspan="4" class="text-center">No hay vehículos</td></tr>';
        return;
    }
    vehiculos.forEach(v => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${v.id}</td><td>${v.placa}</td><td>${v.modelo?.nombre || ''}</td><td>${v.conductor?.nombre || 'Sin asignar'}</td>`;
        tbody.appendChild(tr);
    });
}

export function renderizarRoles(roles, tablaBodyId = 'listaRoles') {
    const tbody = document.getElementById(tablaBodyId);
    if (!tbody) return;
    tbody.innerHTML = '';
    if (!roles.length) {
        tbody.innerHTML = '<tr><td colspan="2" class="text-center">No hay roles</td></tr>';
        return;
    }
    roles.forEach(r => {
        const tr = document.createElement('tr');
        tr.innerHTML = `<td>${r.id}</td><td>${r.nombre}</td>`;
        tbody.appendChild(tr);
    });
}