namespace espacioPresupuestos;
using espacioPresupuestosDetalle;

public class Presupuestos
{
    public int IdPresupuesto { get; set; }
    public string? NombreDestinatario { get; set; }
    public DateOnly FechaCreacion { get; set; }
    public List<PresupuestosDetalle> Detalle { get; set; } = new List<PresupuestosDetalle>();

    // Método: suma total sin IVA
    public decimal MontoPresupuesto()
    {
        decimal total = 0;
        foreach (var item in Detalle)
        {
            total += (item.Producto?.precio ?? 0) * item.Cantidad;
        }
        return total;
    }


    // Método: suma total con IVA 21%
    public decimal MontoPresupuestoConIva()
    {
        return MontoPresupuesto() * 1.21m; // 'm' indica decimal
    }

    // Método: cantidad total de productos del detalle
    public int CantidadProductos()
    {
        int total = 0;
        foreach (var item in Detalle)
        {
            total += item.Cantidad;
        }
        return total;
    }
}
