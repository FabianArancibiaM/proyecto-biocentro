namespace CapaDTO
{
    public class EstadoVenta
    {
        private int idEstadoVenta;
        private string descripcion;

        public int IdEstadoVenta { get => idEstadoVenta; set => idEstadoVenta = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}