using CRMInterview.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInterview.Helpers
{
    public  class MessageHelper
    {

        public static string GetMessage(EventTypeEnum EventType)
        {

            switch(EventType)
            {
                case EventTypeEnum.VisitedPlans:
                    return "Your registration is just around the corner!";
                case EventTypeEnum.ClickedBuyNow:
                    return "Your TipRanks plan is still in your cart!";
                case EventTypeEnum.FailedToPay:
                    return "Complete your purchase with this 20 % coupon.";
            }

            return string.Empty;
        }
    }
}
