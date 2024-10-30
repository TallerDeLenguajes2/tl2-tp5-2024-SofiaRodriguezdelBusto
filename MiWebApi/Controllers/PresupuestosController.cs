using Microsoft.AspNetCore.Mvc;

namespace MiWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{

    private readonly ILogger<PresupuestosController> _logger;

    private PresupuestosRepository repoPresupuestos;

    public PresupuestosController(ILogger<PresupuestosController> logger)
    {
        _logger = logger;
        repoPresupuestos = new PresupuestosRepository();
    }
    /*
    [HttpPost("api/Producto")]
    public IActionResult CrearProductos(Producto producto)
    {
        repoProductos.CrearProducto(producto);
        return Created();

    }
*/
    [HttpGet("api/Presupuestos")]
    public ActionResult<List<Producto>> GetPresupuestos()
    {
        return Ok(repoPresupuestos.ObtenerPresupuestos());
    }

    /*
    [HttpPut("api/Producto/{id}")]
    public IActionResult ModificarProductos(int id, Producto producto)
    {
        repoProductos.ModificarProducto(id, producto);
        return Ok();

    }
    */

}