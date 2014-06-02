using System;
using System.Collections.Generic;
using System.Linq;
using SoyalBISAPI;
using SoyalBISAPI.Common;
using SoyalBISAPI.HexCommands;
using SoyalWorkTimeWebManager.DAL;
using SoyalWorkTimeWebManager.ViewModels.Helpers;
using WorkTimeModel;
using WorkTimeModel.APIModels;

namespace SoyalWorkTimeWebManager.Models.Helpers
{
    public class CardSender
    {
        public static void DownloadCards<TContext>(EFUnitOfWork<TContext> worker, int id = 0 ) where TContext : WorkTimeManagerContext
        {
            var DestGroup = worker.GroupRepository.GetByID(id, "Persons");
            if (DestGroup == null)
            {
                throw new Exception("CardSender >> DownloadCards: A keresett csoport nem található. A letöltés nem sikerült");
            }
            foreach (var person in DestGroup.Persons)
            {

                var groups = person.PersonGroups;

                var locations = groups.Select(gr => gr.Location).ToList();

                var nodes = new List<Node>();

                foreach (FacilityLocation location in locations)
                    foreach (Site site in location.Sites)
                    {
                        int? siteId = site.NodeID;
                        Node currentNode = worker.NodeRepository.FirstOrDefault(node => siteId != null && node.ID == siteId);
                        nodes.Add(currentNode);
                    }
                foreach (var node in nodes)
                {
                    try
                    {
                        using (ISoyalCommandClient soyalcommander = Commander.Get(Series.V721Hv3, node.IP, node.Port, node.Number))
                        {
                            AccessMode mode = AccessConverter.GetMode(person.Card.Mode);
                            soyalcommander.AddCardContent(node.Number,
                                person.Card.Code,
                                person.Card.UserAddress,
                                person.Card.SiteCode,
                                person.Card.PinCode,
                                mode,
                                person.Card.AntiPassBack,
                                person.Card.TimeZone);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("CardSender >> DownloadCards: A letöltés nem sikerült --"+ex.Message);
                    }
                }
            }
        }
    }
}