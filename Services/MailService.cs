﻿using ECN.Contracts.Services;
using ECN.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Windows;

namespace ECN.Services
{
    public class MailService : IMailService
    {
        public void SendEmail(string email, int id, string name)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");


            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);


            msg.Subject = "Nuevo ECN!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + name + "! </span></strong></span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'>&nbsp;</span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'> Se ha generado un nuevo<strong><span style = 'color:red'> ECN </span ></strong> con el folio<strong><span style = 'color:red'> " + id + " </span></strong></span></span></p>" +
              "<p><span style = 'font-family:Verdana,Geneva,sans-serif'><span style = 'font-size:12pt'> Generado por<strong><span style = 'color:#0066cc'> " + UserRecord.Employee.Name + "</span></strong><span style = 'color:black'>.</span ></strong></span></span></p> ";

            msg.IsBodyHtml = true;


            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;


            try
            {
                client.Send(msg);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SendSignEmail(string email, int id, string name)
        {
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");


            msg.From = new MailAddress("ecnsystem@outlook.com");
            msg.To.Add(email);


            msg.Subject = "Nuevo ECN!";
            msg.Body = "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'>Hola<strong><span style='color:black'> " + name + "!</span></strong></span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:12pt'> &nbsp;</span></span></p>" +
              "<p><span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'> Tienes un <span style = 'color:#ff0000'><strong> ECN </strong></span> pendiente de firmar</span></span>&nbsp;<span style='font-family:Verdana,Geneva,sans-serif'><span style='font-size:16px'>con el folio<span style= 'color:#ff0000'><strong> " + id + "&nbsp;</strong></span></span></span></p>" +
              "<p><span style='font-size:16px'><span style='font-family:Verdana,Geneva,sans-serif'> Generado por<strong><span style='color:#0066cc'> " + UserRecord.Employee.Name + "</span></strong>.</span></span></p>";

            msg.IsBodyHtml = true;

            client.Port = 587;
            client.Credentials = new NetworkCredential("ecnsystem@outlook.com", "ecmx-ecn");
            client.EnableSsl = true;

            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
