using System;

namespace CapaDTO
{
    public class Venta
    {
        private int idVenta;
        private EstadoVenta estadoVenta;
        private MedioPago medioPago;
        private DateTime fechaPago;
        private int monto;

        public int IdVenta { get => idVenta; set => idVenta = value; }
        public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
        public int Monto { get => monto; set => monto = value; }
        public EstadoVenta EstadoVenta { get => estadoVenta; set => estadoVenta = value; }
        public MedioPago MedioPago { get => medioPago; set => medioPago = value; }
    }
}