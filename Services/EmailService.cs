using System;
using System.Threading.Tasks;

namespace CRMInterview.Services
{
    public interface INotificationService
    {
        Task<bool> SendAsync(string email, string content);
    }

    public class EmailSendingService : INotificationService
    {
        public async Task<bool> SendAsync(string email, string content)
        {
            Console.WriteLine($"Sent {content} to {email}.");

            return await Task.Run(() => true);
        }
    }
}