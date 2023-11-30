using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GTANetworkAPI;

namespace lessons.Test
{
    public sealed class Commands : Script
    {
        [Command("setstar")]
        private static void _setStarsToPlayer(Player player, int idPlayer, int value)
        {
            Player target = NAPI.Pools.GetAllPlayers().FirstOrDefault(p => p.Id == idPlayer);

            if (target == null)
            {
                player.SendNotification("Player not found");
                return;
            }

            Character characterTarget = Character.Characters[target];
            characterTarget.Wanted.SetStars(target, value);
        }

        [Command("setmecop")]
        private static void _setCopPlayer(Player player)
        {
            if (player.HasSharedData("IS_COP") && player.GetSharedData<bool>("IS_COP") == true)
            {
                player.SetSharedData("IS_COP", false);
                player.SendNotification("~r~Now u not cop");
            }
            else
            {
                player.SetSharedData("IS_COP", true);
                player.SendNotification("~g~Now u cop!");
            }
        }

        [Command("checkwanted")]
        private static void _checkWantedInPlayer(Player player, int id)
        {
            Player target = NAPI.Pools.GetAllPlayers().FirstOrDefault(p => p.Id == id);
            
            if (target == null)
            {
                player.SendNotification("Player not found");
                return;
            }

            Character characterTarget = Character.Characters[target];

            int stars = characterTarget.Wanted.ValueStars;

            player.SendChatMessage($"{player.Name} has a {stars} stars");
        }
    }
}
