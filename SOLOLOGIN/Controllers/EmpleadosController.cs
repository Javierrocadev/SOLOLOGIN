using Microsoft.AspNetCore.Mvc;
using SOLOLOGIN.Filters;
using SOLOLOGIN.Models;
using SOLOLOGIN.Repositories;

namespace SOLOLOGIN.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleados repo;

        public EmpleadosController(RepositoryEmpleados repo)
        {
            this.repo = repo;
        }


        //-------los controllers de las vistas---------

        //index que pilla todos los empleados
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await this.repo.GetEmpleadosAsync();
            return View(empleados);
        }

        //detalles de empleados
        public async Task<IActionResult> Details(int idempleado)
        {
            Empleado empleado = await this.repo.FindEmpleadoAsync(idempleado);
            return View(empleado);
        }



        //----------------SEGURIDAD--------------------


        //VALIDA SI ESTA EN LA BASE DE DATOS
        [AuthorizeEmpleados]
        public IActionResult PerfilEmpleado()
        {
            return View();
        }







    }
}
