using log4net;
using MediaConverter.FFMPEGProvider.Interfaces;
using MediaConverter.Service.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MediaConverter.Service.Controllers
{
    [RoutePrefix("api")]
    public class ConverterController : ApiController
    {
        private readonly IMediaProvider _mediaProvider;
        private readonly ILog _log;
        public ConverterController(IMediaProvider mediaProvider, ILog log)
        {
            _mediaProvider = mediaProvider;
            _log = log;
        }

        [HttpPost, Route("convert")]
        public async Task<HttpResponseMessage> Convert([FromBody] ConvertRequest convertRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    byte[] data = await _mediaProvider.ConvertAsync(convertRequest.Data, convertRequest.InFormat, convertRequest.OutFormat);
                    return Request.CreateResponse(
                       new { data, convertRequest.OutFormat });
                }
                else
                {
                    var messages = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                    return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, 
                        new { messages });
                }
            }
            catch (Exception ex)
            {
                _log?.Error($"{nameof(ConverterController)} {nameof(Convert)} ({convertRequest.InFormat}, {convertRequest.OutFormat}) error: {ex.Message} {ex.InnerException?.Message}");
                Debug.WriteLine(ex.Message);
                return Request.CreateResponse(System.Net.HttpStatusCode.InternalServerError, new { ex.Message });
            }
        }
    }
}
