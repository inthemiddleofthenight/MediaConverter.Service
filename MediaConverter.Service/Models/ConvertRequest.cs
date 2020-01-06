
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MediaConverter.Service.Models
{
    public class ConvertRequest
    {
        [Required, JsonProperty("data")]
        public byte[] Data { get; set; }

        [Required, JsonProperty("inFormat")]
        public string InFormat { get; set; }

        [Required, JsonProperty("outFormat")]
        public string OutFormat { get; set; }
    }
}