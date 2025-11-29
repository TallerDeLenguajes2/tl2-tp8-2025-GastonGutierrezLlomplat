using Microsoft.AspNetCore.Mvc;
using espacioPresupuestos;
using espacioPresupuestosDetalle;

public class PresupuestosController : Controller
{
    private PresupuestosRepository presupuestoRepository;

    public PresupuestosController()
    {
        presupuestoRepository = new PresupuestosRepository();
    }

    [HttpGet]
    public IActionResult Crear() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Presupuestos nuevoPresupuesto)
    {
        presupuestoRepository.Alta(nuevoPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Index()
    {
        var lista = presupuestoRepository.GetAll();
        return View(lista);
    }

    [HttpGet]
    public IActionResult Detalles(int id)
    {
        var presupuesto = presupuestoRepository.Detalles(id);
        ViewBag.ProductosDisponibles = new ProductoRepository().GetAll(); // Para el select
        return View(presupuesto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AgregarProducto(int id, int idProducto, int cantidad)
    {
        presupuestoRepository.AgregarProducto(id, idProducto, cantidad);
        return RedirectToAction("Detalles", new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Eliminar(int id)
    {
        presupuestoRepository.Baja(id);
        return RedirectToAction("Index");
    }
}
