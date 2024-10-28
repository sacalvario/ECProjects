using ProjectManager.Contracts.Services;
using ProjectManager.Models;

using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace ProjectManager.Services
{
    public class MailService : IMailService
    {
        public void SendApprovedECN(int id, string generatorname, string generatoemail)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add("controldedocumentos@electri-cord.com.mx");
            msg.CC.Add(generatoemail);

            msg.Subject = "ECN Aprobado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> Control de documentos!</span></strong></span></span></p>" +
              "<p> &nbsp;</p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El<span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span>generado por <span style= 'color:#0066cc'><strong>" + generatorname + " </strong></span> ha sido <span style ='color:#339933'><strong> aprobado </strong></span> y se encuentra pendiente de cerrar.</span></span></p> ";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendCloseECN(string email, int id, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);
            msg.CC.Add("controldedocumentos@electri-cord.com.mx");

            msg.Subject = "ECN Cerrado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + generatorname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El <span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span> generado por ti, ha sido <span style='color:#339933'><strong> cerrado </strong></span> por control de documentos.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendCloseECO(int id, string generatorname, string generatoremail)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add("alvarado@electri-cord.com.mx");
            msg.CC.Add(generatoremail);

            msg.Subject = "ECO Cerrado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> Erika Alvarado Flores! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> El <span style='color:#ff0000'><strong> ECN </strong></span> con folio <span style='color:#ff0000'><strong> " + id + " </strong></span> generado por <span ><strong>" + generatorname + "</strong></span> ha sido <span style='color:#339933'><strong> cerrado </strong></span> por control de documentos. </span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendAssignedManagerEmail(string manageremail, string managername, string generatorname, int id, string customer)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecmprojects@outlook.com");
            msg.To.Add(manageremail);

            msg.Subject = "New project!";
            msg.Body = "<p><span style='font-family:Tahoma, Geneva, sans-serif'> Hello <strong>" + managername + "</strong></span></p>" +
                       "<p><span style='font-family:Tahoma, Geneva, sans-serif'> You have been assigned as manager on project</span><span style='color:#689f38;font-family:Tahoma, Geneva, sans-serif'><strong> " + id +  " </strong></span></p>" +
                       "<p><span style='font-family:Tahoma, Geneva, sans-serif'> Project generated by: </span><span style='color:#689f38;font-family:Tahoma, Geneva, sans-serif'><strong>" +  generatorname + "</strong></span></p>" +
                       "<p><span style='font-family:Tahoma, Geneva, sans-serif'> Customer: </span><span style='color:#689f38;font-family:Tahoma, Geneva, sans-serif'><strong>" + customer + "</strong></span></p>" +
                       "<p><span style='font-family:Tahoma, Geneva, sans-serif'> Greetings.</span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecmprojects@outlook.com", "ecm_sysprojects");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }


        public void SendRefuseECNEmail(string email, int id, string signedname, string generatorname)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            msg.Subject = "ECN firmado o pendiente de firmar rechazado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + signedname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Un <span style='color:#ff0000'><strong> ECN </strong></span> en el que firmaste o estabas proximo a firmar, ha sido rechazado.</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Folio: <strong><span style='color:#ff0000'> " + id + " </span></strong></span></span></p>" +
               "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Generado por<strong><span style='color:#2980b9'> " + generatorname + "<span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendRefuseECNToGeneratorEmail(string email, int id, string refusedname, string generatorname, List<string> emails)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);

            foreach (var item in emails)
            {
                msg.CC.Add(item);
            }

            msg.Subject = "ECN rechazado!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + generatorname + "! </span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Un <span style='color:#ff0000'><strong> ECN </strong></span> generado por ti, ha sido rechazado.</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Folio: <strong><span style='color:#ff0000'> " + id + " </span></strong></span></span></p>" +
              "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Rechazado por<strong><span style='color:#2980b9'> " + refusedname + " </span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }

        public void SendNewTaskEmail(string email, string generatoremail, int id, string responsiblename, string generatorname, string targetdate, string customer)
        {
            MailMessage msg = new MailMessage();    
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;

            msg.From = new MailAddress("projectssystemsecm@gmail.com");
            msg.To.Add(email);
            msg.CC.Add(generatoremail);

            msg.Subject = "New pending task!";
            msg.Body = "<p><span style='font-family:Tahoma, Geneva, sans-serif'>Hello<strong> " + responsiblename +  "</strong></span></p>" +
                       "<p><span style='font-family:Tahoma, Geneva, sans-serif'> You have a pending task to complete from the project </span><span style= 'color:#558b2f;font-family:Tahoma, Geneva, sans-serif'><strong>" + id + "</strong></span><span style= 'font-family:Tahoma, Geneva, sans-serif'> generated by <span style= 'color:#558b2f;font-family:Tahoma, Geneva, sans-serif'><strong>" +  generatorname + "</strong></span></p>" +
                        "<p><span style= 'font-family:Tahoma, Geneva, sans-serif'> Target date: </span><span style= 'color:#558b2f;font-family:Tahoma, Geneva, sans-serif'><strong> " + targetdate + " </strong></span></p>" +
                        "<p><span style='font-family:Tahoma, Geneva, sans-serif'> Customer: </span><span style='color:#689f38;font-family:Tahoma, Geneva, sans-serif'><strong>" + customer + "</strong></span></p>" +
                        "<p><span style= 'font-family:Tahoma, Geneva, sans-serif'> Greetings. </span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("projectssystemsecm@gmail.com", "rpob ljal ztxj tipo");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch
            {
                return;
            }
        }
    }
}