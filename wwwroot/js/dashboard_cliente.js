
// NOMBRE USUARIO LOGUEADO

// Cambiar después por los datos reales del login
const nombreUsuario = localStorage.getItem("nombre") || "Cliente";

document.getElementById("nombreCliente").textContent =
    nombreUsuario;

//mapbox
// token: pk.
//eyJ1IjoibWFuYW1pLWhlcnJlcmEiLCJhIjoiY21jOTl2bnJyMXV2czJtb21jbnNtYXhybCJ9.2wdxUAFqIEvv8Lx8eQbhDg", git no deja subirlo
//pegar el ey... despues del .pk
mapboxgl.accessToken = "PEGA_AQUI_XD";

const map = new mapboxgl.Map({
    container: "map",
    style: "mapbox://styles/mapbox/streets-v12",

    // La Paz - Bolivia
    center: [-68.1193, -16.4897],

    zoom: 12
});

// Controles de zoom
map.addControl(new mapboxgl.NavigationControl());


const marcadorCliente = new mapboxgl.Marker({
    color: "#303E8C"
})
    .setLngLat([-68.1193, -16.4897])
    .setPopup(
        new mapboxgl.Popup()
            .setHTML("<h4>Tu ubicación</h4>")
    )
    .addTo(map);


// ==============================
// EJEMPLO DESTINO
// ==============================

// BORRAR CUANDO USES TU API

const marcadorDestino = new mapboxgl.Marker({
    color: "#F2884B"
})
    .setLngLat([-68.1322, -16.5034])
    .setPopup(
        new mapboxgl.Popup()
            .setHTML("<h4>Destino</h4>")
    )
    .addTo(map);


// ==============================
// TRAZAR RUTA EJEMPLO
// ==============================

// BORRAR CUANDO IMPLEMENTES
// LA RUTA REAL DEL PEDIDO

map.on("load", () => {

    map.addSource("ruta", {
        type: "geojson",
        data: {
            type: "Feature",
            properties: {},
            geometry: {
                type: "LineString",
                coordinates: [
                    [-68.1193, -16.4897],
                    [-68.1322, -16.5034]
                ]
            }
        }
    });

    map.addLayer({
        id: "ruta",
        type: "line",
        source: "ruta",
        layout: {
            "line-join": "round",
            "line-cap": "round"
        },
        paint: {
            "line-color": "#303E8C",
            "line-width": 5
        }
    });
});


document
    .getElementById("btnCerrarSesion")
    .addEventListener("click", () => {

        localStorage.removeItem("token");
        localStorage.removeItem("nombre");

        window.location.href = "login.html";
    });


// ==============================
// FUTURO:
// SOLICITAR PEDIDO
// ==============================

document
    .getElementById("btnSolicitarPedido")
    .addEventListener("click", () => {

        alert("Aquí irá la llamada al API de Pedidos");

        /*
        fetch('/api/pedido', {
            method: 'POST'
        })
        */
    });

//marcadores vehiculos cercanos
const conductores = [
    [-68.1193, -16.4897],
    [-68.1322, -16.5034]
];

conductores.forEach(posicion => {

    //const auto = document.createElement('div');

    const auto = document.createElement("div");

    auto.innerHTML =
        '<i class="fi fi-rr-car"></i>';

    auto.style.fontSize = "28px";
    auto.style.color = "#F2B807";

});

