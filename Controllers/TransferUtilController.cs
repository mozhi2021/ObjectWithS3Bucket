using Microsoft.AspNetCore.Mvc;
using NewTestProject.Interface;
using NewTestProject.Models;



namespace NewTestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferUtilController : ControllerBase
    {
        private readonly ITransferUtilHandler _TransferUtilHandler;

        public TransferUtilController(ITransferUtilHandler TransferUtilHandler)
        {
            _TransferUtilHandler = TransferUtilHandler;
        }

        // GET: api/<TransferUtilController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TransferUtilController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TransferUtilController>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Produces("application/json")]

        public async Task<string> UploadDocumentAsync([FromForm] Document doc)
        {
            try
            {

                string RequsestID = await _TransferUtilHandler.UploadDocumentAsync(doc);

                return RequsestID;

            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT api/<TransferUtilController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransferUtilController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
