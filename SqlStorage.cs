using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FileStorage
{
    public class SqlStorage : IStorageLoc
    {
        public DbContextOptions ContextOptions { get; set; }

        public SqlStorage(DbContextOptions options)
        {
            ContextOptions = options;
        }

        public void CreateEntry<T>(T item) where T : class, ILocalFile
        {
            using var context = new DbContext(ContextOptions);
            context.Add(item);
            context.SaveChanges();
        }

        public T RetrieveEntry<T>(T item) where T : class, ILocalFile
        {
            using var contex = new DbContext(ContextOptions);
            var retItem = contex.Find<T>((object)item.Id);
            return retItem;
        }

        public void UpdateEntry<T>(T item) where T : class, ILocalFile
        {
            using var context = new DbContext(ContextOptions);
            context.Update(item);
            context.SaveChanges();
        }

        public void DeleteEntry<T>(T item) where T : class, ILocalFile
        {
            using var context = new DbContext(ContextOptions);
            context.Remove(item);
            context.SaveChanges();
        }
    }
}
