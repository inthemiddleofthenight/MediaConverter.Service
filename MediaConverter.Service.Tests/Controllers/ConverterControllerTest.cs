using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaConverter.Service.Controllers;
using MediaConverter.FFMPEGProvider;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using MediaConverter.Service.Models;
using Newtonsoft.Json;

namespace MediaConverter.Service.Tests.Controllers
{
    [TestClass]
    public class ConverterControllerTest
    {
        [TestMethod]
        public void Convert()
        {
            // Упорядочение
            using (ConverterController controller = new ConverterController(new MediaProvider(Directory.GetCurrentDirectory(), null), null))
            {
                controller.Configuration = new HttpConfiguration();
                controller.Request = new HttpRequestMessage();
                HttpResponseMessage result = controller.Convert(new ConvertRequest()
                {
                    Data = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "demo.wav")),
                    InFormat = "wav",
                    OutFormat = "mp3"
                }).Result;

                // Утверждение

                Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

                string content = result.Content.ReadAsStringAsync().Result;
                ConvertRequest convertRequest = JsonConvert.DeserializeObject<ConvertRequest>(content);

                Assert.IsTrue((convertRequest?.Data?.Length ?? 0) != 0);
            }
            // Действие
        }
    }
}