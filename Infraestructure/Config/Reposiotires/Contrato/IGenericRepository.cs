using System.Linq.Expressions;

namespace IA.GymAdmin.HelpDesk.API.Infraestructure.Config.Reposiotires.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> Obtener(Expression<Func<T, bool>> filtro);
        Task<T> Crear(T entidad);
        Task<bool> Editar(T entidad);
        Task<bool> Eliminar(T entidad);
        Task<List<T>> EjecutarSPListAsync(string sqlQuery, params object[] parametros);
        Task<IQueryable<T>> Consultar(Expression<Func<T, bool>>? filtro = null);
    }
}
