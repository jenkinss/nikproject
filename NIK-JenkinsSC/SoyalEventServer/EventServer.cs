using System;
using System.Linq;
using SoyalBISAPI;
using SoyalBISAPI.Common;
using SoyalEventServer.CorrectionHelpers;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.Models;

namespace SoyalEventServer
{
    public class EventServer
    {
        public static void GetOneEvent<TContext>() where TContext : WorkTimeManagerContext
        {
            try
            {
                var worker = new EFUnitOfWork<TContext>();
                var sites = worker.SiteRepository.Get(includeProperties: "Node");
                if (sites != null || sites.Any())
                {
                    foreach (var site in sites)
                    {
                        if (site.Node != null)
                        {
                            using (var commander = Commander.Get(Series.V721Hv3, site.Node.IP, site.Node.Port, site.Node.Number))
                            {
                                var soyalEvent = commander.GetEvent();
                                if (soyalEvent != null && soyalEvent.UserAddress == 1024)
                                {
                                    commander.DeleteEvent();
                                }
                                if (soyalEvent != null && soyalEvent.UserAddress > 0 && soyalEvent.UserAddress < 1024)
                                {
                                    try
                                    {
                                        EventDetails.Correction(soyalEvent, site, worker);
                                        if (commander.DeleteEvent())
                                        {
                                            throw new SoyalEventLogException(soyalEvent.ToString());
                                        }
                                    }
                                    catch (SoyalEventLogException e)
                                    {
                                        throw new SoyalEventLogException(e.EvetMessage);
                                    }
                                    catch (Exception e)
                                    {
                                        throw new Exception("Commander in EventServer >> " + e.Message);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SoyalEventLogException e)
            {
                throw new SoyalEventLogException(e.EvetMessage);
            }
            catch (Exception e)
            {
                throw new Exception("EventServer >> "+e.Message);
            }

        }


    }
}
