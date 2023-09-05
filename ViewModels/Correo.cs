using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ProjectManager.ViewModels
{
    class Correo
    {
        static void EnviarCorreo(string[] args)
        {
            // Información de autenticación del servidor SMTP
            string smtpServer = "smtp.example.com";
            int smtpPort = 587;
            string smtpUsername = "tu_correo@example.com";
            string smtpPassword = "tu_contraseña";

            // Crear el cliente SMTP
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true // Usar SSL/TLS para la conexión segura
            };

            // Crear el mensaje de correo
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tu_correo@example.com"),
                Subject = "Asignación de tarea en el sistema de gestión de proyectos",
                Body = "Has sido asignado(a) a una nueva tarea en el sistema de gestión de proyectos.",
                IsBodyHtml = false
            };

            // Agregar destinatarios (personas asignadas a la tarea)
            mailMessage.To.Add("destinatario1@example.com");
            mailMessage.To.Add("destinatario2@example.com");
            // Agregar más destinatarios si es necesario

            try
            {
                // Enviar el correo
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }

        static void Predecesora(string[] args)
        {
            // Información de autenticación del servidor SMTP
            string smtpServer = "smtp.example.com";
            int smtpPort = 587;
            string smtpUsername = "tu_correo@example.com";
            string smtpPassword = "tu_contraseña";

            // Crear el cliente SMTP
            SmtpClient smtpClient = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true // Usar SSL/TLS para la conexión segura
            };

            // Información de la tarea
            string tareaPredecesora = "Tarea_A";
            string tareaActual = "Tarea_B";
            string destinatario = "destinatario@example.com";

            // Crear el mensaje de correo
            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress("tu_correo@example.com"),
                Subject = $"Inicio de tarea: {tareaActual}",
                Body = $"La tarea predecesora '{tareaPredecesora}' ha sido completada. Ahora es tu turno de comenzar la tarea '{tareaActual}'.",
                IsBodyHtml = false
            };

            // Agregar destinatario
            mailMessage.To.Add(destinatario);

            try
            {
                // Enviar el correo
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo enviado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
        }


        //static void Add(string[] args)
        //{
        //    //string connectionString = "Tu_Cadena_De_Conexión"; // Reemplaza con tu cadena de conexión
        //    string insertQuery = "INSERT INTO Proyectos (NombreCliente, NumerosParte, RevisionParte, NumeroCotizacion, ArchivoCotizacion, Locacion, TipoIndustria, RutaArchivo, ProjectManager) VALUES (@NombreCliente, @NumerosParte, @RevisionParte, @NumeroCotizacion, @ArchivoCotizacion, @Locacion, @TipoIndustria, @RutaArchivo, @ProjectManager)";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        {
        //            // Asignar valores a los parámetros
        //            command.Parameters.AddWithValue("@NombreCliente", "Nombre del Cliente");
        //            command.Parameters.AddWithValue("@NumerosParte", "Número de Parte");
        //            command.Parameters.AddWithValue("@RevisionParte", "Revisión de Parte");
        //            command.Parameters.AddWithValue("@NumeroCotizacion", "Número de Cotización");
        //            command.Parameters.AddWithValue("@ArchivoCotizacion", "NombreArchivo.pdf");
        //            command.Parameters.AddWithValue("@Locacion", "Ubicación");
        //            command.Parameters.AddWithValue("@TipoIndustria", "Tipo de Industria");
        //            command.Parameters.AddWithValue("@RutaArchivo", "C:\\Ruta\\Archivo");
        //            command.Parameters.AddWithValue("@ProjectManager", "Nombre del Project Manager");

        //            try
        //            {
        //                connection.Open();
        //                int rowsAffected = command.ExecuteNonQuery();
        //                if (rowsAffected > 0)
        //                {
        //                    Console.WriteLine("Proyecto agregado exitosamente.");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("No se pudo agregar el proyecto.");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Error al agregar el proyecto: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        //static void InsertarTarea(string[] args)
        //{
        //    string connectionString = "Tu_Cadena_De_Conexión"; // Reemplaza con tu cadena de conexión
        //    string insertQuery = "INSERT INTO Tareas (ProyectoId, DuracionDias, FechaInicio, FechaCierre, TareaPredecesora, EmpleadoAsignado) VALUES (@ProyectoId, @DuracionDias, @FechaInicio, @FechaCierre, @TareaPredecesora, @EmpleadoAsignado)";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        //        {
        //            // Asignar valores a los parámetros
        //            command.Parameters.AddWithValue("@ProyectoId", 1); // ID del proyecto al que se asocia la tarea
        //            command.Parameters.AddWithValue("@DuracionDias", 5);
        //            command.Parameters.AddWithValue("@FechaInicio", DateTime.Now);
        //            command.Parameters.AddWithValue("@FechaCierre", DateTime.Now.AddDays(5));
        //            command.Parameters.AddWithValue("@TareaPredecesora", "Tarea_Pred");
        //            command.Parameters.AddWithValue("@EmpleadoAsignado", "Nombre del Empleado");

        //            try
        //            {
        //                connection.Open();
        //                int rowsAffected = command.ExecZuteNonQuery();
        //                if (rowsAffected > 0)
        //                {
        //                    Console.WriteLine("Tarea insertada exitosamente.");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("No se pudo insertar la tarea.");
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Error al insertar la tarea: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        //static void DuracionEstimada(string[] args)
        //{
        //    string connectionString = "Tu_Cadena_De_Conexión"; // Reemplaza con tu cadena de conexión
        //    int proyectoId = 1; // ID del proyecto del que deseas calcular el tiempo estimado

        //    string selectTasksQuery = "SELECT DuracionDias FROM Tareas WHERE ProyectoId = @ProyectoId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(selectTasksQuery, connection))
        //        {
        //            command.Parameters.AddWithValue("@ProyectoId", proyectoId);
        //            int totalDuracionDias = 0;

        //            try
        //            {
        //                connection.Open();
        //                SqlDataReader reader = command.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    int duracionDias = Convert.ToInt32(reader["DuracionDias"]);
        //                    totalDuracionDias += duracionDias;
        //                }

        //                reader.Close();

        //                Console.WriteLine($"El tiempo estimado de duración del proyecto es de {totalDuracionDias} días.");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine("Error al calcular el tiempo estimado de duración del proyecto: " + ex.Message);
        //            }
        //        }
        //    }
        //}

        static void TasaExitoEstimada(string[] args)
        {
            // Variables de ejemplo
            int tareasCompletadas = 10;
            int totalTareas = 15;

            // Calcular la tasa de éxito en porcentaje
            double tasaExito = (double)tareasCompletadas / totalTareas * 100;

            Console.WriteLine($"La tasa de éxito estimada del proyecto es del {tasaExito}%.");
        }
    }
}
