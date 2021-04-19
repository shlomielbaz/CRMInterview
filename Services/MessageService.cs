using CRMInterview.Helpers;
using CRMInterview.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMInterview.Services
{
    public class MessageService
    {

        private readonly IEventService _events;
        private readonly INotificationService _meseanger;

        public MessageService(IEventService events, INotificationService meseanger)
        {
            _events = events;
            _meseanger = meseanger;
        }

        /// <summary>
        /// ExecAsync - gets an execution plan
        /// </summary>
        /// <param name="executionPlan"></param>
        /// <returns></returns>

        internal async Task ExecAsync(List<Tuple<EventTypeEnum, int>> executionPlan)
        {
            await Task.WhenAll(executionPlan.Select(async action =>
            {
                var list = await _events.FilterEvents(action.Item1, action.Item2);

                await Task.WhenAll(list.Select(async item =>
                {
                    bool isSuccess = await _meseanger.SendAsync(item.UserEmail, MessageHelper.GetMessage(item.EventType));
                    if (isSuccess)
                    {
                        _events.setIsSent(item);
                    }
                }));
            }));
        }
    }
}
