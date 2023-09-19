using Microsoft.AspNetCore.Mvc;
using NewTestProject.Models;
using NewTestProject.Interface;


namespace NewTestProject.Controllers;

    [Route("api/[controller]")]

public class ValuesController : ControllerBase
{
    private readonly IDocumentHandler _documentHandler;

    public ValuesController(IDocumentHandler documentHandler)
    {
        _documentHandler = documentHandler;
    }

    // GET api/values
    [HttpGet]
    public IEnumerable<string> Get()
    {
        //Util.UploadDocumentToS3Bucket("document2023", "C:\\Users\\User\\Desktop\\CodeOrg.jpg", "abcd");
       // Util.UploadDocumentToS3Bucket("document2023", "C:\\Users\\User\\Downloads\\NMMS MAT.pdf", "Questions.pdf");

        return new string[] { "value1", "value2" };
    }

    //// GET api/values/5
    //[HttpGet("{id}")]
    //public string Get(int id)
    //{
    //    Util.UploadDocumentToS3Bucket("document2023", "C:\\Users\\User\\Downloads\\NMMS MAT.pdf", "Test1");
    //    Util.UploadDocumentToS3Bucket("mozhitest", "C:\\Users\\User\\Downloads\\School_Image.jfif", "Image");

    //    return "value";
    //}

    // GET api/values/fileName
    [HttpGet("{fileName}")]
    public async Task<string> DownloadDocumentAsync(string fileName)
    {
        try
        {
            string RequestID = await _documentHandler.DownloadDocumentAsync(fileName);

            return RequestID;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // POST api/values
    [HttpPost]
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    public async Task<string> UploadDocumentAsync([FromForm] Document doc)
    {
        try
        {
           string RequsestID =  await _documentHandler.UploadDocumentAsync(doc);
           
            return RequsestID;      
        }
        catch (Exception)
        {
            throw;
        }

    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{fileName}")]

    public async Task<string> DeleteDocumentAsync(string fileName)
    {
        try
        {
            string RequestID = await _documentHandler.DeleteDocumentAsync(fileName);

            return RequestID;
        }
        catch (Exception)
        {
            throw;
        }
    }
}