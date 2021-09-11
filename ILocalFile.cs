using System.Diagnostics.CodeAnalysis;

namespace FileStorage
{
    public interface ILocalFile
    {
        [NotNull]
        public string Id { get; set; }
    }
}