namespace WebAppCourierTrack.DTO
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroTransaccion { get; set; }
        public string CuentaBancaria { get; set; }
        public string Banco { get; set; }
    }
}
