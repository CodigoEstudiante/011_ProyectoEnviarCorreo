using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using WebApiCorreo.Models;

namespace WebApiCorreo.Data
{
    public class CorreoData
    {
        public bool Enviar(Correo oCorreo)
        {
            try
            {
                //Obtenemos el servidor smtp del archivo de configuración.
                var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
                string strHost = smtpSection.Network.Host;
                int port = smtpSection.Network.Port;
                string strUserName = smtpSection.Network.UserName;
                string strFromPass = smtpSection.Network.Password;

                //Proporcionamos la información de autenticación al servidor de Gmail
                SmtpClient smtp = new SmtpClient(strHost, port);
                MailMessage msg = new MailMessage();


                //Creamos el contenido del correo. 
                msg.From = new MailAddress(smtpSection.From, "SERVIDOR CORREO");
                foreach (string para in oCorreo.Para) msg.To.Add(new MailAddress(para));
                msg.Subject = oCorreo.Asunto;
                msg.IsBodyHtml = oCorreo.isHtml;
                msg.Body = oCorreo.Body;


                //Enviamos el correo
                smtp.Credentials = new NetworkCredential(strUserName, strFromPass);
                smtp.EnableSsl = true;
                smtp.Send(msg);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

        }

    }
}