namespace ApiVentas.Models.DTOs
{
    public class VentaDTO
    {
        public string Fecha { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDto> DetalleVentas {get;set;}
        public VentaDTO()
        {
            this.DetalleVentas = new List<DetalleVentaDto>();
        }
        public class DetalleVentaDto
        {
            public int ProductoId { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioU { get; set; }
            public decimal Importe { get; set; }
        }
    }
}