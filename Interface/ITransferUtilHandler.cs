using NewTestProject.Models;


namespace NewTestProject.Interface
{
    public interface ITransferUtilHandler
    {
        public Task<string> UploadDocumentAsync(Document doc);

        public Task<string> DownloadDocumentAsync(string fileName);

    }
}

