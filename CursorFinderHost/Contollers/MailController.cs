using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CursorFinderHost.Contollers
{
    /// <summary>
    /// Контроллер для отправки уведомлений по почте
    /// </summary>
    public class MailController
    {
        private readonly MailAddress serviceAddress;
        private readonly MailAddress addressToReceiveMessage;
        private readonly SmtpClient smtpClient;
        internal MailController()
        {
            serviceAddress = new MailAddress("tetsprojectfrom@posteo.net");
            addressToReceiveMessage = new MailAddress("testprojectto@posteo.net");
            smtpClient = new SmtpClient("posteo.de", 587);
            smtpClient.Credentials = new NetworkCredential(serviceAddress.Address, "Hive228394=");
        }
        /// <summary>
        /// Метод закоментирован тк Сервис блочит такие частые отправки сообщений 
        /// </summary>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(string messageBody)
        {
            try
            {
                //var message = new MailMessage(serviceAddress, addressToReceiveMessage);
                //message.Subject = "Database added new 50 records";
                //message.Body = messageBody;
                //message.IsBodyHtml = true;
                //smtpClient.EnableSsl = true;
                //await smtpClient.SendMailAsync(message);
                //System.Console.WriteLine($"Message Sent to{addressToReceiveMessage}");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                return;
            }


        }
    }
}
