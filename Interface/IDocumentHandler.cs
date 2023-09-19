using NewTestProject.Models;

namespace NewTestProject.Interface
{
    public interface IDocumentHandler
    {
       public Task<string> UploadDocumentAsync(Document doc);

        public Task<string> DownloadDocumentAsync(string fileName);

        public Task<string> DeleteDocumentAsync(string fileName);
    }
}


