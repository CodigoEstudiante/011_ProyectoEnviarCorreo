using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiCorreo.Models
{
    public class Correo
    {
        public string[] Para { get; set; }
        public string Asunto { get; set; }
        public bool isHtml { get; set; }
        public string Body { get; set; }
    }
}