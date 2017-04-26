using System.Web.Http;

namespace WebApiClientForWcf.Controllers
{
    public class WPFController : ApiController
    {
        private ServiceRef.WcfServiceClient _proxy = new ServiceRef.WcfServiceClient();
        // GET api/WPF
        public IHttpActionResult GetValue()
        {
            var data = _proxy.GetValue();
            return Ok(data);
        }

        public IHttpActionResult GetValueWithId(int id)
        {
            var data = _proxy.GetData(id);
            return Ok(data);
        }
    }
}
