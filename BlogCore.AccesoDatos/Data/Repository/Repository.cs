using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);  
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;//Se crea una consulta Iqueryable a partir del dbset del contexto
            if (filter != null)
            {
                //Se aplica el fitlro si se proporciona
                query = query.Where(filter);
            }
            //Se incluyen propiedades de navegación si se proporcionan datos relacionados
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            //Retorna lista de categorias
            //Se aplica el ordenamiento si se proporicona
            if (orderBy != null)
            {
                //Se ejecuta la funcion de ordenamiento y se convierte en una lista
                return orderBy(query).ToList();
            }
            //Se convierte la consulta en una lista
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                //Se aplica el fitlro si se proporciona
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();

        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
