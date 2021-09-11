using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FileStorage
{
    public class LocalStorage : IStorageLoc
    {
        public string FolderPath { get; }

        public LocalStorage(string dirFolderPath)
        {
            FolderPath = dirFolderPath;
            if (!Directory.Exists(dirFolderPath))
            {
                Directory.CreateDirectory(dirFolderPath);
            }
        }

        public void CreateEntry<T>(T item) where T : class, ILocalFile
        {
            using var fs = File.Create(Path.Combine(FolderPath, item.Id));
            using var sw = new StreamWriter(fs);
            sw.WriteLine(JsonConvert.SerializeObject(item));
        }

        public T RetrieveEntry<T>(T item) where T : class, ILocalFile
        {
            var request = item.Id;
            if (File.Exists(request))
            {
                using var fr = File.OpenRead(request);
                using var sr = new StreamReader(fr);
                var json = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                return json;
            }

            throw new FileNotFoundException();
        }

        public void UpdateEntry<T>(T item) where T : class, ILocalFile
        {
            if (!File.Exists(Path.Combine(FolderPath, item.Id))) return;
            using var fs = File.Create(Path.Combine(FolderPath, item.Id));
            using var sw = new StreamWriter(fs);
            sw.WriteLine(JsonConvert.SerializeObject(item));
        }

        public void DeleteEntry<T>(T item) where T : class, ILocalFile
        {
            if (File.Exists(Path.Combine(FolderPath, item.Id)))
                File.Delete(Path.Combine(FolderPath, item.Id));
        }
    }
}
