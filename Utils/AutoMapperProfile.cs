using AutoMapper;
using WebAppCourierTrack.DTO;
using WebAppCourierTrack.Entidades;

namespace WebAppCourierTrack.Utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //automapper
            //ROL, tablas INDEPENDIENTES:
            //Aqui van las reglas de mapeo <origen,destino>
            CreateMap<RolCreaDTO, Rol>();
            CreateMap<Rol, RolDTO>();
            //GENERO
            CreateMap<GeneroCreaDTO, Genero>();
            CreateMap<Genero,  GeneroDTO>();
            //tipo cliente
            CreateMap<TipoClienteCreaDTO, TipoCliente>();
            CreateMap<TipoCliente, TipoClienteDTO>();
            //TIPO DOCUMENTO
            CreateMap<TipoDocumentoCreaDTO, TipoDocumento>();
            CreateMap<TipoDocumento, TipoDocumentoDTO>();
            //extension ci
            CreateMap<ExtensionCICrearDTO, ExtensionCI>();
            CreateMap<ExtensionCI, ExtensionCIDTO>();
            //estado
            CreateMap<EstadoCreaDTO, Estado>();
            CreateMap<Estado, EstadoDTO>();

            CreateMap<Estado, EstadoConPedidosDTO>()
                .ForMember(x => x.Pedidos,
                    options => options.MapFrom(MapFromEstadoPedidoToPedidoDTO));
            //estado pago
            CreateMap<EstadoPagoCreaDTO, EstadoPago>();
            CreateMap<EstadoPago, EstadoPagoDTO>();
            // TipoLicencia
            CreateMap<TipoLicenciaCreaDTO, TipoLicencia>();
            CreateMap<TipoLicencia, TipoLicenciaDTO>();
            //Marca
            CreateMap<MarcaCreaDTO, MarcaDTO>();
            CreateMap<Marca, MarcaDTO>();
            //TipoVehiculo
            CreateMap<TipoVehiculoCreaDTO, TipoVehiculo>();
            CreateMap<TipoVehiculo, TipoVehiculoDTO>();
            //MetodoPago
            CreateMap<MetodoPagoCreaDTO, MetodoPago>();
            CreateMap<MetodoPago, MetodoPagoDTO>();
            //Color
            CreateMap<ColorCreaDTO, Color>();
            CreateMap<Color, ColorDTO>();
            //AnioVehiculos
            CreateMap<AnioVehiculoCreaDTO, AnioVehiculo>();
            CreateMap<AnioVehiculo, AnioVehiculoDTO>();
            //Ubicacion
            CreateMap<UbicacionCreaDTO, Ubicacion>();
            CreateMap<Ubicacion, UbicacionDTO>();

            //tablas DEPENDIENTES:
            //usuario
            /*CreateMap<UsuarioCreaDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();*/

            // Usuario
            CreateMap<UsuarioCreaDTO, Usuario>()
    .ForMember(dest => dest.UsuariosUbicaciones,
               opt => opt.MapFrom(src => MapUbicacionesIdsToUsuarioUbicacion(src.UbicacionesIds)));

            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.RolNombre, opt => opt.MapFrom(src => src.Rol.Nombre));

            CreateMap<Usuario, UsuarioConUbicacionesDTO>()
                .IncludeBase<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Ubicaciones, opt => opt.MapFrom(src => MapUsuarioUbicacionToUbicacionDTO(src.UsuariosUbicaciones)));

            //cliente
            CreateMap<ClienteCreaDTO, Cliente>();
            CreateMap<Cliente, ClienteDTO>();
            //cliente natural
            CreateMap<ClienteNaturalCreaDTO, ClienteNatural>();
            CreateMap<ClienteNatural, ClienteNaturalDTO>();
            //cliente juridico
            CreateMap<ClienteJuridicoCreaDTO, ClienteJuridico>();
            CreateMap<ClienteJuridico, ClienteJuridicoDTO>();
            //direccion origen
            CreateMap<DireccionOrigenCreaDTO, DireccionOrigen>();
            CreateMap<DireccionOrigen, DireccionOrigenDTO>();
            //direccion destino
            CreateMap<DireccionDestinoCreaDTO, DireccionDestino>();
            CreateMap<DireccionDestino,  DireccionDestinoDTO>();
            //calificacion
            CreateMap<CalificacionCreaDTO, Calificacion>();
            CreateMap<Calificacion,  CalificacionDTO>();
            //pedido
            // pedido
            CreateMap<PedidoCreaDTO, Pedido>()
                .ForMember(x => x.EstadosPedidos,
                    options => options.MapFrom(MapIntToEstadoPedido));

            CreateMap<Pedido, PedidoDTO>();

            CreateMap<Pedido, PedidoConEstadosDTO>()
            .ForMember(dest => dest.Estados, opt => opt.MapFrom(src => src.EstadosPedidos.Select(ep => ep.Estado)))
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.Usuario.Nombre + " " + src.Cliente.Usuario.ApPat))
            .ForMember(dest => dest.ConductorNombre, opt => opt.MapFrom(src => src.Conductor != null ? src.Conductor.Usuario.Nombre + " " + src.Conductor.Usuario.ApPat : null));

            //Conductor
            CreateMap<ConductorCreaDTO, Conductor>();
            CreateMap<Conductor, ConductorDTO>();
            //Modelo
            CreateMap<ModeloCreaDTO, Modelo>();
            CreateMap<Modelo, ModeloDTO>();
            //Vehiculo
            CreateMap<VehiculoCreaDTO, Vehiculo>();
            CreateMap<Vehiculo, VehiculoDTO>()
                .ForMember(dest => dest.ModeloNombre,
                           opt => opt.MapFrom(src => src.Modelo.Nombre))
                .ForMember(dest => dest.ColorNombre,
                           opt => opt.MapFrom(src => src.Color.Nombre))
                .ForMember(dest => dest.Anio,
                           opt => opt.MapFrom(src => src.AnioVehiculo != null ? src.AnioVehiculo.Anio : 0))
                .ForMember(dest => dest.ConductorNroLicencia,
                           opt => opt.MapFrom(src => src.Conductor != null ? src.Conductor.NroLicencia : null))
                .ForMember(dest => dest.ConductorNombre,
                           opt => opt.MapFrom(src => src.Conductor != null && src.Conductor.Usuario != null ?
                                                     src.Conductor.Usuario.Nombre : null));
            //Tarifa
            CreateMap<TarifaCreaDTO, Tarifa>();
            CreateMap<Tarifa, TarifaDTO>();
            //DetallePedido
            CreateMap<DetallePedidoCreaDTO, DetallePedido>();
            CreateMap<DetallePedido, DetallePedidoDTO>();
            //Pago
            CreateMap<PagoCreaDTO, Pago>();
            CreateMap<Pago, PagoDTO>();
            //Seguimiento
            CreateMap<SeguimientoCreaDTO, Seguimiento>();
            CreateMap<Seguimiento, SeguimientoDTO>();
            //HistorialUbicacion
            CreateMap<HistorialUbicacionCreaDTO, HistorialUbicacion>();
            CreateMap<HistorialUbicacion, HistorialUbicacionDTO>();
            //Notificacion
            CreateMap<NotificacionCreaDTO, Notificacion>();
            CreateMap<Notificacion, NotificacionDTO>();

        }
        //estado -> pedido mostrar
        private List<PedidoDTO> MapFromEstadoPedidoToPedidoDTO(Estado estado,EstadoConPedidosDTO estadoDTO)
        {
            List<PedidoDTO> response = new List<PedidoDTO>();

            if (estado.EstadosPedidos == null)
                return response;

            foreach (var item in estado.EstadosPedidos)
            {
                response.Add(new PedidoDTO
                {
                    Id = item.PedidoId,
                    Fragil = item.Pedido.Fragil,
                    PesoKg = item.Pedido.PesoKg,
                    DistanciaKm = item.Pedido.DistanciaKm,
                    CostoTotal = item.Pedido.CostoTotal
                });
            }

            return response;
        }
        //pedido -> estados mostrar
        private List<EstadoDTO> MapFromEstadoPedidoToEstadoDTO( Pedido pedido, PedidoConEstadosDTO pedidoDTO)
        {
            List<EstadoDTO> response = new List<EstadoDTO>();

            if (pedido.EstadosPedidos == null)
                return response;

            foreach (var item in pedido.EstadosPedidos)
            {
                response.Add(new EstadoDTO
                {
                    Id = item.EstadoId,
                    Nombre = item.Estado.Nombre
                });
            }

            return response;
        }
        //estados pedidos
        private List<EstadoPedido> MapIntToEstadoPedido(PedidoCreaDTO pedidoCreaDTO, Pedido pedido)
        {
            List<EstadoPedido> response = new List<EstadoPedido>();

            if (pedidoCreaDTO.EstadoIds == null)
                return response;

            foreach (int id in pedidoCreaDTO.EstadoIds)
            {
                response.Add(new EstadoPedido
                {
                    EstadoId = id
                });
            }

            return response;
        }
        private List<UsuarioUbicacion> MapUbicacionesIdsToUsuarioUbicacion(List<int> ids)
        {
            var lista = new List<UsuarioUbicacion>();
            if (ids == null) return lista;
            foreach (int id in ids)
                lista.Add(new UsuarioUbicacion { UbicacionId = id });
            return lista;
        }

        private List<UbicacionDTO> MapUsuarioUbicacionToUbicacionDTO(ICollection<UsuarioUbicacion> usuariosUbicaciones)
        {
            var resultado = new List<UbicacionDTO>();
            if (usuariosUbicaciones == null) return resultado;
            foreach (var uu in usuariosUbicaciones)
            {
                if (uu.Ubicacion != null)
                    resultado.Add(new UbicacionDTO { Id = uu.Ubicacion.Id, Latitud = uu.Ubicacion.Latitud, Longitud = uu.Ubicacion.Longitud });
            }
            return resultado;
        }

        private List<UsuarioDTO> MapUbicacionUsuariosToUsuarioDTO(ICollection<UsuarioUbicacion> usuariosUbicaciones)
        {
            var resultado = new List<UsuarioDTO>();
            if (usuariosUbicaciones == null) return resultado;
            foreach (var uu in usuariosUbicaciones)
            {
                if (uu.Usuario != null)
                    resultado.Add(new UsuarioDTO { Id = uu.Usuario.Id, Nombre = uu.Usuario.Nombre, Correo = uu.Usuario.Correo, RolNombre = uu.Usuario.Rol?.Nombre });
            }
            return resultado;
        }

    }
}
