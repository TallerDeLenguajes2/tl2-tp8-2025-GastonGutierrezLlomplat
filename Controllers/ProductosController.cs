using Microsoft.AspNetCore.Mvc;
using espacioProductos;

public class ProductosController : Controller
{
    private ProductoRepository productoRepository;
    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(); // Solo mostramos el formulario
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Crear(Productos nuevoProducto)
    {
        if (!ModelState.IsValid)
        {
            return View(nuevoProducto); // Si hay errores, volvemos al formulario
        }

        productoRepository.Alta(nuevoProducto); // Guardamos el producto
        return RedirectToAction("Index"); // Volvemos a la lista
    }

    [HttpGet("Productos")]
    public IActionResult Index()
    {
        List<Productos> listProductos;
        listProductos = productoRepository.GetAll();
        return View(listProductos);
    }

    [HttpDelete("BajaProducto")]
    public ActionResult<string> BajaProducto(int id)
    {
        int resultado = productoRepository.Baja(id);
        if (resultado == 0)
        {
            return NotFound("No se encontró el producto");
        }
        else
        {
            return Ok("Producto dado de baja exitosamente");
        }
    }

    [HttpGet]   
    public IActionResult Editar(int id)
    {
        var producto = productoRepository.Detalles(id);
        if (producto == null) return NotFound();
        return View(producto); // envía el producto a la vista
    }

    [HttpPost]
    public IActionResult Editar(int id, Productos producto)
    {
        int resultado = productoRepository.ModificarProducto(id, producto);
        if (resultado == 0)
        {
            return NotFound("No se encontró el producto");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    [HttpGet("Detalles")]
    public ActionResult<Productos> Detalles(int id)
    {
        Productos producto = productoRepository.Detalles(id);
        if (producto == null)
        {
            return NotFound("No se encontró el producto");
        }
        else
        {
            return Ok(producto);
        }
    }
}