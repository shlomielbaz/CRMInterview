using CRMInterview.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data;

namespace CRMInterview.Services
{
    public interface IEventService
    {
        Task<List<CrmEvent>> FilterEvents(EventTypeEnum eventType, int minutes);
        void setIsSent(CrmEvent item);
    }

    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;

        public EventService(IEventRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CrmEvent>> FilterEvents(EventTypeEnum eventType, int minutes)
        {
            var query = _repository.Query();
            query = query.Where(x => (x.EventType == eventType) && (x.IsSent == false) && ((DateTime.Now.Minute - x.Timestamp.Minute) < minutes));

            return await Task.Run(() => query.ToList());
        }

        public void setIsSent(CrmEvent item)
        {
            item.IsSent = true;
            _repository.UpdateEventAsync(item);
        }

        public async Task<List<CrmEvent>> GetVisitedPlans(int minutes)
        {
            return await FilterEvents(EventTypeEnum.VisitedPlans, minutes);
        }

        public async Task<List<CrmEvent>> GetClickedBuyNow(int minutes)
        {
            return await FilterEvents(EventTypeEnum.ClickedBuyNow, minutes);
        }

        public async Task<List<CrmEvent>> GetFailedToPay(int minutes)
        {
            return await FilterEvents(EventTypeEnum.FailedToPay, minutes);
        }

        public async Task<List<CrmEvent>> GetCompletedPayment(int minutes)
        {
            return await FilterEvents(EventTypeEnum.CompletedPayment, minutes);
        }
    }
}
