// Muestra una lista de objetos en el <ul id="listaEnvios">
export function mostrarLista(elementos, idLista = 'listaEnvios') {
    const lista = document.getElementById(idLista);
    if (!lista) return;
    lista.innerHTML = '';
    if (!elementos || elementos.length === 0) {
        lista.innerHTML = '<li class="info-text">No hay datos disponibles</li>';
        return;
    }
    elementos.forEach(item => {
        const li = document.createElement('li');
        // Personaliza según los campos que devuelve UsuarioDTO
        li.innerHTML = `<strong>${item.nombre || item.correo}</strong> - Rol: ${item.rol?.nombre || 'Sin rol'}`;
        lista.appendChild(li);
    });
}

export function mostrarError(mensaje, idContenedor = 'loginError') {
    const contenedor = document.getElementById(idContenedor);
    if (contenedor) contenedor.textContent = mensaje;
}

export function mostrarMensajeEnLista(mensaje, esError = false) {
    const lista = document.getElementById('listaEnvios');
    if (lista) lista.innerHTML = `<li class="info-text" style="color:${esError ? '#dc3545' : '#6c757d'}">${mensaje}</li>`;
}