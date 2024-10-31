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
    [HttpPost("api/Presupuesto")]
    public IActionResult CrearPresupuesto(Presupuesto presupuesto)
    {
        if(!repoPresupuestos.CrearPresupuesto(presupuesto)) return BadRequest();
        return Created();
    }

    [HttpPost("api/Presupuesto/{id}/ProductoDetalle")]
    public IActionResult AgregarProductoAlPresupuesto(int idPresupuesto, int idProducto, int cantidad)
    {
        if(!repoPresupuestos.AgregarProducto(idPresupuesto, idProducto, cantidad)) return BadRequest();
        return Created();
    }

    [HttpGet("api/Presupuestos")]
    public ActionResult<List<Presupuesto>> GetPresupuestos()
    {
        return Ok(repoPresupuestos.ObtenerPresupuestos());
    }

    [HttpGet("api/Presupuestos/{id}")]
    public ActionResult<Presupuesto> GetPresupuestoPorId(int id)
    {
        return Ok(repoPresupuestos.ObtenerPresupuestoPorId(id));
    }

    [HttpDelete]

    public IActionResult BorrarPresupuesto(int id)
    {
        repoPresupuestos.EliminarPresupuestoPorId(id);
        return Ok();
    }



}