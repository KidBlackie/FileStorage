using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace FileStorage
{
    public interface IStorageLoc
    {
        public void CreateEntry<T>(T item) where T : class, ILocalFile;
        public T RetrieveEntry<T>(T item) where T : class, ILocalFile;
        public void UpdateEntry<T>(T item) where T : class, ILocalFile;
        public void DeleteEntry<T>(T item) where T : class, ILocalFile;
    }
}