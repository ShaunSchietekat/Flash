using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sanitize.Models;
using System.Text;
using Sanitize.Interface;

namespace Sanitize.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SanitizeWordController : ControllerBase
    {
        readonly IWordRepository _wordRepository;
        public SanitizeWordController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        // Get all
        [HttpGet()]
        public JsonResult GetAll()
        {
            return new JsonResult(Ok(_wordRepository.GetWords()));
        }

        //Create/Update
        [HttpPost]
        public JsonResult CreateUpdate(WordModel model)
        {

            return new JsonResult(Ok(_wordRepository.CreateUpdateNewWord(model)));
        }


        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            return new JsonResult(Ok(_wordRepository.DeleteWord(id)));
        }

        [HttpGet]
        public JsonResult SanitizeUserWord(string word)
        {

            return new JsonResult(Ok(_wordRepository.SanitizeWord(word)));

        }


    }
}
