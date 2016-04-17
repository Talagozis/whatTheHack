using Microsoft.Web.WebSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace whatTheHack2.SocketControllers.Controllers
{
    public class SocketClientController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler());
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class ChatWebSocketHandler : Microsoft.Web.WebSockets.WebSocketHandler
    {
        private static WebSocketCollection _chatClients = new WebSocketCollection();

        public override void OnOpen()
        {
            _chatClients.Add(this);
        }

        public override void OnMessage(string recordType)
        {
            _chatClients.Broadcast("Hello socket");
        }
    }


}