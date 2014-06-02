using System;
using System.Linq;
using SoyalBISAPI.Common;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;
using WorkTimeModel;

namespace SoyalEventServer.CorrectionHelpers
{
    public class EventDetails
    {
        public static bool HasOne<TContext>(Event cevent, EFUnitOfWork<TContext> worker, DirectionType direction) where TContext : WorkTimeManagerContext
        {
            bool result = false;

            var person = worker.PersonRepository.GetByID(cevent.UserAddress);
            int personid = person.ID;

            DateTime s = cevent.EventDate.Date;
            DateTime e = s.AddDays(1);
            string simpleDirection = direction.ToString();
            var events = worker.WorkTimeRepository.Get(_ => _.PersonID == personid && _.TimeStamp > s && _.TimeStamp < e && _.Direction == simpleDirection);

            if (events != null && events.Count()==1)
                result = true;

            return result;
        }

        

        public static void Correction<TContext>(Event cevent, Site site, EFUnitOfWork<TContext> worker) where TContext : WorkTimeManagerContext
        {
            try
            {
                if (cevent.DirectionType==DirectionType.Entry && cevent.TransactionType!=TransactionType.AntipassbackError && EventQuery.IsFirst(cevent, worker, DirectionType.Entry))
                {
                    //első belépés, belépési idő módosítva
                    ComeCorrection.Correction(cevent, site, worker);
                }
                else
                {
                    //nem első belépés az adott napon, loggolás, de a külső táblába nem kerül be
                    EventDeepLogger.Log(cevent, site, worker, true, false);
                }

                
                
                if ( (cevent.DirectionType == DirectionType.Exit && 
                    cevent.EventDate.Hour <= 2 && 
                    cevent.TransactionType != TransactionType.AntipassbackError && 
                    HasOne(cevent, worker, DirectionType.Entry)) || 
                    
                    (cevent.DirectionType == DirectionType.Exit &&
                    cevent.EventDate.Hour >= 15 &&
                    cevent.TransactionType != TransactionType.AntipassbackError &&
                    HasOne(cevent, worker, DirectionType.Entry))
                    
                    )
                {
                    //első kilépés, belépési idő módosítva
                    LeaveCorrection.Correction(cevent, site, worker);
                }
                else
                {
                    //nem első kilépés az adott napon, loggolás, de a külső táblába nem kerül be
                    EventDeepLogger.Log(cevent, site, worker, true, false);
                }
            }
            catch (Exception e)
            {

                throw new Exception("Corrector >> " + e.Message);
            }

        }
    }
}
