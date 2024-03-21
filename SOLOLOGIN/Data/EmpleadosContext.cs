using Microsoft.EntityFrameworkCore;
using SOLOLOGIN.Models;

namespace SOLOLOGIN.Data
{
    public class EmpleadosContext : DbContext
    {
        public EmpleadosContext(DbContextOptions<EmpleadosContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set;}
    }
}
