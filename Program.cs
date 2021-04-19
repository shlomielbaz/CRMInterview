using CRMInterview.Model;
using CRMInterview.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRMInterview
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var events = new EventService(new InMemoryEventRepository());
            var manager = new MessageService(events, new EmailSendingService());

            do
            {
                // Deckare an execution plan, which is a list of event type and time in minuts
                List<Tuple<EventTypeEnum, int>> executionPlan = new List<Tuple<EventTypeEnum, int>>()
                {
                    new Tuple<EventTypeEnum, int>(EventTypeEnum.VisitedPlans, 20),
                    new Tuple<EventTypeEnum, int>(EventTypeEnum.ClickedBuyNow, 15),
                    new Tuple<EventTypeEnum, int>(EventTypeEnum.FailedToPay, 10)
                };

                await manager.ExecAsync(executionPlan);

                await Task.Delay(TimeSpan.FromSeconds(1));
            } while (true);
        }
    }
}
