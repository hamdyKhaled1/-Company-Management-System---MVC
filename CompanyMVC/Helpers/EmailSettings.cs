using FinalDAL.Models;
using System.Net;
using System.Net.Mail;

namespace CompanyMVC.Helpers
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("smtp.gmail.com", 587);
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("eng.hamdy.khaled@gmail.com", "jazn ambl wwmc ansc");
			client.Send("eng.hamdy.khaled@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
