public class Presupuesto
{
    int idPresupuesto;
    string nombreDestinatario;
    List<PresupuestoDetalle> detalle;

    public int IdPresupuesto1 { get => idPresupuesto; set => idPresupuesto = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

    public Presupuesto(int idPresupuesto, string nombreDestinatario, List<PresupuestoDetalle> detalles)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        detalle = detalles;

    }

    public double MontoPresupuesto()
    {
        int monto = detalle.Sum(d => d.Cantidad*d.Producto.Precio);
        return monto;

    }
    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto()*1.21;
    }
    public int CantidadProductos ()
    {
        return detalle.Sum(d => d.Cantidad);
    }


}