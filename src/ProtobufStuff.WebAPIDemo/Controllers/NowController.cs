using System.Web.Http;
using ProtobufStuff.Common.Entities;

namespace ProtobufStuff.WebAPIDemo.Controllers
{
    public class NowController : ApiController
    {
        // GET api/now
        public Now Get()
        {
            return new Now();
        }
    }
}
