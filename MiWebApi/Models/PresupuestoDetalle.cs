public class PresupuestoDetalle
{
    Producto producto;
    int cantidad;

    public PresupuestoDetalle(Producto producto, int cantidad)
    {
        this.producto = producto;
        this.cantidad = cantidad;
    }

    public int Cantidad { get => cantidad; set => cantidad = value; }
    internal Producto Producto { get => producto; set => producto = value; }
}