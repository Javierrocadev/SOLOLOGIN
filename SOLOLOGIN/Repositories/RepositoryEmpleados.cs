using Microsoft.EntityFrameworkCore;
using SOLOLOGIN.Data;
using SOLOLOGIN.Models;

namespace SOLOLOGIN.Repositories
{
    public class RepositoryEmpleados
    {
        private EmpleadosContext context;
        public RepositoryEmpleados(EmpleadosContext context)
                {
                    this.context = context;
                }

        //------------metodos------------

        //GetAllAsync
        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            return await this.context.Empleados.ToListAsync();
        }

        //Findasync by id
        public async Task<Empleado> FindEmpleadoAsync(int idEmpleado)
        {
            return await this.context.Empleados
                .FirstOrDefaultAsync(x => x.IdEmpleado == idEmpleado);
        }

        //devolver empleados por departamento
        public async Task<List<Empleado>> GetEmpleadosDepartamentoAsync(int idDepartamento)
        {
            return await this.context.Empleados
                .Where(x => x.Departamento == idDepartamento).ToListAsync();
        }

        //ACTUALIZAR UN VALOR 
        public async Task UpdateSalarioEmpleadosDepartamentoAsync
            (int idDepartamento, int incremento)
        {
            List<Empleado> empleados = await
                this.GetEmpleadosDepartamentoAsync(idDepartamento);
            foreach (Empleado empleado in empleados)
            {
                empleado.Salario += incremento;
            }
            await this.context.SaveChangesAsync();
        }

        //----------VALIDAR USUARIO----------------

        public async Task<Empleado> LogInEmpleadoAsync
            (string apellido, int idEmpleado)
        {
            Empleado empleado =
                await this.context.Empleados
                .Where(z => z.Apellido == apellido
                && z.IdEmpleado == idEmpleado).FirstOrDefaultAsync();
            return empleado;
        }


    }
}
