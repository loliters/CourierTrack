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
            //tablas DEPENDIENTES:
            //usuario
            CreateMap<UsuarioCreaDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
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
                .ForMember(x => x.Estados,
                    options => options.MapFrom(MapFromEstadoPedidoToEstadoDTO));

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

    }
}
