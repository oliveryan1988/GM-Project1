using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.IO;
using System.Web.Hosting;
using LincolnBrandChampion.Model.Models;
namespace LincolnBrandChampion.Web.Helpers
{
    public static class EmailHelper
    {
        //string CheckoutCode,string ModelNum,       

        public static void SendErrorEMail(Exception Ex, string userId)
        {
            MailMessage mail = new MailMessage();

            mail.IsBodyHtml = false;
            mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["ErrorEmailTo"]));
            mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
            mail.Subject = "Lincoln Brand Champion - Error Notification";

            string machine = string.Format("Machine:{0}\n", Environment.MachineName);
            string userName = string.Format("User ID:{0}\n", userId);

            mail.Body = string.Concat(userName, machine, GetExceptionDetails(Ex));


            SendMailError(mail);

        }

        public static void SendConfEMail(string email_id)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email_id));
                mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
                mail.Subject = "You've been successfully registered for a Lincoln Brand Champion Immersion event.";
                //string Body = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"));

                string emailUrl = ConfigurationManager.AppSettings["emailUrl"].ToString();
                string physicalPath = HttpContext.Current.Server.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html");
                string myString = "";
                using (System.IO.StreamReader myFile = new System.IO.StreamReader(physicalPath))
                {
                    myString = myFile.ReadToEnd();
                }
                string htmlbody = myString;
                htmlbody = htmlbody.Replace("%emailUrl%", emailUrl);
                mail.Body = htmlbody;
                SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
       
        }

        public static void SendReturnDraftEMail(string email_id,string reason,string mailbody)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email_id));
                mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
                mail.Subject = "Lincoln Brand Champion Dashboard – Your Story has been returned to DRAFT mode";

                //string emailUrl = ConfigurationManager.AppSettings["emailUrl"].ToString();
                //string physicalPath = HttpContext.Current.Server.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html");
                string myString = "Your Story has been returned to DRAFT mode for the following reasons:"+ reason+"</br>"+mailbody+"<p/>"
                +"Please contact PHQ if you have any questions at PHQ@LincolnBC.com or 800-816-6130.";
                //using (System.IO.StreamReader myFile = new System.IO.StreamReader(physicalPath))
                //{
                //    myString = myFile.ReadToEnd();
                //}
                string htmlbody = myString;
                //htmlbody = htmlbody.Replace("%emailUrl%", emailUrl);
                mail.Body = htmlbody;
                SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void SendCancelEMail(RegistrationModel model, string eventname)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailCancelTo"].ToString()));
                mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
                mail.Subject = "Immersion Registration Cancellation";
                //string Body = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"));

                //using (System.IO.StreamReader myFile = new System.IO.StreamReader("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"))
                //{
                //    myString = myFile.ReadToEnd();
                //}
                string htmlbody = "<body><p>The Following person have cancel the Event Registration <p>" +
                    "<b>First Last Name: " + model.FIRST_NAME + " " + model.LAST_NAME + "</b><br>" +
                     "<b>Dealership Name: " + model.DLR_NAME + "</b><br>" +
                     "<b>Email Address: " + model.EMAIL_ID + "</b><br>" +
                     "<b>Phone No: " + model.PHONE + "</b><br>" +
                     "<b>P&A Code: " + model.PA_CODE + "</b><br>" +
                      "<b>Event: " + eventname + "</b><br>" +
                      "<b>Reason for cancellation:</b><br>" + model.CANCEL_REASON +
                    "</body>";
                //htmlbody = htmlbody.Replace("%emailUrl%", emailUrl);
                mail.Body = htmlbody;
                SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void SendResumeEMail(RegistrationModel model, string eventname)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailCancelTo"].ToString()));
            mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
            mail.Subject = "Immersion Registration Resume";
            //string Body = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"));

            //using (System.IO.StreamReader myFile = new System.IO.StreamReader("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"))
            //{
            //    myString = myFile.ReadToEnd();
            //}
            string htmlbody = "<body><p>The Following person have cancel the Event Registration <p>" +
                "<b>First Last Name: " + model.FIRST_NAME + " " + model.LAST_NAME + "</b><br>" +
                 "<b>Dealership Name: " + model.DLR_NAME + "</b><br>" +
                 "<b>Email Address: " + model.EMAIL_ID + "</b><br>" +
                 "<b>Phone No: " + model.PHONE + "</b><br>" +
                 "<b>P&A Code: " + model.PA_CODE + "</b><br>" +
                  "<b>Event: " + eventname + "</b><br>" +
                "</body>";
            //htmlbody = htmlbody.Replace("%emailUrl%", emailUrl);
            mail.Body = htmlbody;
            SendMail(mail);

        }

        public static void SendCheckpointEmailAdmin(CheckpointReportModel model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(ConfigurationManager.AppSettings["EmailCancelTo"].ToString()));
                mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
                mail.Subject = "Checkpoint One Order Submission";
                //string Body = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"));

                //using (System.IO.StreamReader myFile = new System.IO.StreamReader("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"))
                //{
                //    myString = myFile.ReadToEnd();
                //}
                string htmlbody = "<body><p>The Following person has placed a toolkit order. <p>" +
                     "<b>First Last Name: " + model.FIRST_NAME + " " + model.LAST_NAME + "</b><br>" +
                     "<b>Dealership Name: " + model.DLR_NAME + "</b><br>" +
                     "<b>Dealership Address: " + model.DLR_ADDRESS + "</b><br>" +
                     "<b>Dealership Address: " + model.DLR_CITY + " " + model.DLR_STATE + " " + model.DLR_ZIP + "</b><br>" +
                     //"<b>Email Address: " + model.EMAIL_ID + "</b><br>" +
                     "<b>Dealership Phone: " + model.DLR_PHONE + "</b><br>" +
                     "<b>Order Time: " + model.CREATED_DATE + "</b><br>" +
                     "<b>Checkpoint: " + model.CHECKPOINT_ID + "</b><br>" +
                    "</body>";
                //htmlbody = htmlbody.Replace("%emailUrl%", emailUrl);
                mail.Body = htmlbody;
                SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void SendCheckpointEmail(string email_id, string tool)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(email_id));
                mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
                mail.Subject = "Lincoln Brand Champion Check Point toolkit successfully selected.";
                //string Body = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/confirmation-email/LBC_Confirm.html"));

                string emailUrl = ConfigurationManager.AppSettings["emailUrl"].ToString();
                string physicalPath = HttpContext.Current.Server.MapPath("~/data/lbc-emails/checkpoint-email/LBC_Checkpoint1.html");
                string myString = "";
                using (System.IO.StreamReader myFile = new System.IO.StreamReader(physicalPath))
                {
                    myString = myFile.ReadToEnd();
                }
                string htmlbody = myString;
                htmlbody = htmlbody.Replace("%tool%", tool);
                mail.Body = htmlbody;
                SendMail(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public static void SendTravelEMail(string email_id)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(email_id));
            mail.From = new MailAddress("auto.mailer@lincolnbrandchampion.com", "Lincoln Brand Champion");
            mail.Subject = "You've been successfully registered for a Lincoln Brand Champion immersion event.";
            string travelUrl = ConfigurationManager.AppSettings["travelUrl"].ToString();
            string myString = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/data/lbc-emails/travel-email/LBC_Travel.html"));
            string htmlbody = myString;
            htmlbody = htmlbody.Replace("%travelUrl%", travelUrl);
            mail.Body = htmlbody;
            SendMail(mail);

        }

        public static string GetExceptionDetails(this Exception exception)
        {
            PropertyInfo[] properties = exception.GetType().GetProperties();
            List<string> fields = new List<string>();
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(exception, null);
                fields.Add(String.Format(
                                 "{0} = {1}",
                                 property.Name,
                                 value != null ? value.ToString() : String.Empty
                ));
            }
            string inner = "*************************************************\n";
            if (exception.InnerException != null)
                inner = string.Concat(inner, GetExceptionDetails(exception.InnerException));


            inner = string.Concat(String.Join("\n", fields), inner);

            return inner;
        }  
      
        public static void SendMailError(MailMessage emailMessage)
        {
            SmtpClient smtpClient = new SmtpClient();

            bool isValidEmail = true;

            #region "STAGE addon"
            if (ConfigurationManager.AppSettings["Environment"] != null &&
                        (
                            ConfigurationManager.AppSettings["Environment"].ToString().Equals("PRODUCTION") ||
                            ConfigurationManager.AppSettings["Environment"].ToString().Equals("STAGE") ||
                            ConfigurationManager.AppSettings["Environment"].ToString().Equals("DEV")
                        )
                )
            {

                emailMessage.Body += "<br/>  " + ConfigurationManager.AppSettings["Environment"].ToString() + ": ";
                foreach (MailAddress em in emailMessage.To)
                {
                    emailMessage.Body += String.Format("{0}<{1}>  ", em.DisplayName, em.Address);
                }

                emailMessage.To.Clear();
                if (ConfigurationManager.AppSettings["EmailTo"] != null)
                {
                    string[] testRecipients = ConfigurationManager.AppSettings["EmailTo"].ToString().Split(',');
                    foreach (string em in testRecipients)
                    {
                        emailMessage.To.Add(new MailAddress(em, em));
                    }
                }
            }
            #endregion

            //must send to someone
            if (emailMessage.To.Count == 0)
            {
                isValidEmail = false;
            }

            //must be from someone
            SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            if (String.IsNullOrEmpty(smtpSec.From))
            {
                isValidEmail = false;
            }
            else
            {
                emailMessage.From = new MailAddress(smtpSec.From);
            }


            //must have content
            if (String.IsNullOrEmpty(emailMessage.Body))
            {
                isValidEmail = false;
            }

            if (isValidEmail)
            {
                smtpClient.Send(emailMessage);
            }
        }

        public static void SendMail(MailMessage emailMessage)
        {
            SmtpClient smtpClient = new SmtpClient();

            bool isValidEmail = true;

            #region "STAGE addon"
            if (ConfigurationManager.AppSettings["Environment"] != null &&
                        (
                            
                            ConfigurationManager.AppSettings["Environment"].ToString().Equals("STAGE") ||
                            ConfigurationManager.AppSettings["Environment"].ToString().Equals("DEV")
                        )
                )
            {

                emailMessage.Body += "<br/>  " + ConfigurationManager.AppSettings["Environment"].ToString() + ": ";
                foreach (MailAddress em in emailMessage.To)
                {
                    emailMessage.Body += String.Format("{0}<{1}>  ", em.DisplayName, em.Address);
                }

                emailMessage.To.Clear();
                if (ConfigurationManager.AppSettings["EmailTo"] != null)
                {
                    string[] testRecipients = ConfigurationManager.AppSettings["EmailTo"].ToString().Split(',');
                    foreach (string em in testRecipients)
                    {
                        emailMessage.To.Add(new MailAddress(em, em));
                    }
                }
            }
            #endregion

            //must send to someone
            if (emailMessage.To.Count == 0)
            {
                isValidEmail = false;
            }

            //must be from someone
            SmtpSection smtpSec = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            if (String.IsNullOrEmpty(smtpSec.From))
            {
                isValidEmail = false;
            }
            else
            {
                emailMessage.From = new MailAddress(smtpSec.From);
            }


            //must have content
            if (String.IsNullOrEmpty(emailMessage.Body))
            {
                isValidEmail = false;
            }

            if (isValidEmail)
            {
                smtpClient.Send(emailMessage);
            }
        }



    }
}