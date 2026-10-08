using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.DBContext;
using IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DBContextConfiguration _context;

        public GenericRepository(DBContextConfiguration context)
        {
            _context = context;
        }


        public async Task<List<T>> EjecutarSPListAsync(string sqlQuery, params object[] parametros)
        {
            try
            {
                return await _context.Set<T>()
                    .FromSqlRaw(sqlQuery, parametros)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch
            {
                throw;
            }
        }



        public async Task<T?> Obtener(Expression<Func<T, bool>> filtro)
        {
            try
            {
                T? entidad = await _context.Set<T>().FirstOrDefaultAsync(filtro);
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<T> Crear(T entidad)
        {
            try
            {
                _context.Set<T>().Add(entidad);
                await _context.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Editar(T entidad)
        {
            try
            {
                _context.Set<T>().Update(entidad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Eliminar(T entidad)
        {
            try
            {
                _context.Set<T>().Remove(entidad);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }

        }


        public Task<IQueryable<T>> Consultar(Expression<Func<T, bool>>? filtro = null)
        {
            try
            {
                IQueryable<T> query = filtro == null ? _context.Set<T>() : _context.Set<T>().Where(filtro);
                return Task.FromResult(query);
            }
            catch
            {
                throw;
            }

        }


    }

}
