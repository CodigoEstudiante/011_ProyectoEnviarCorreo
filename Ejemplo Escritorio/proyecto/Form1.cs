using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace proyecto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnEnviarCorreo_Click(object sender, EventArgs e)
        {
      
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:62413/api/Correo");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                Request oRequest = new Request()
                {
                    Para = txtPara.Text.Split(','),
                    Asunto = txtAsunto.Text,
                    isHtml = false,
                    Body = txtMensaje.Text
                };

                streamWriter.Write(JsonConvert.SerializeObject(oRequest));
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = bool.Parse(streamReader.ReadToEnd());

                if (result)
                {
                    MessageBox.Show("Mensaje Enviado");
                }
            }

        }

    }

    public class Request
    {
        public string[] Para { get; set; }
        public string Asunto { get; set; }
        public bool isHtml { get; set; }
        public string Body { get; set; }
    }
}
