using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi_ClientForWCF.Controllers
{
    public class WebApiController : ApiController
    {
        private ServiceReference2.Service1Client proxy = new ServiceReference2.Service1Client();

        public IHttpActionResult Get()
        {
            // IEnumerable<string>
            var data = proxy.GetAllFiles();
            return Ok(data);
        }


        public IHttpActionResult Get(string name)
        {
            // string
            var data = proxy.GetByName(name);
            return Ok(data);
        }


        // DELETE: api/WebApi/5
        public void Delete(string filename)
        {
            proxy.DeleteFile(filename);

        }

        public void Post([FromUri]string name, [FromBody]string content)
        {
            proxy.Post(name, content);
        }

        public void Put([FromUri]string name, [FromBody]string content)
        {
            proxy.Put(name, content);
        }
    }
}
