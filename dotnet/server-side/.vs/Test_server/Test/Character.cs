using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace lessons.Test
{
    public class Character
    {
        public static Dictionary<Player, Character> Characters = new Dictionary<Player, Character>();

        public readonly string SocialClub;
        public readonly Wanted Wanted;

        public Character(Player player)
        {
            SocialClub = player.SocialClubName;

            Wanted = new Wanted();

            Console.WriteLine("create new character");

            Characters.Add(player, this);
        }

        private class Events : Script
        {
            [ServerEvent(Event.PlayerConnected)]
            public static void OnLoad(Player player)
            {
                new Character(player);

                // check if player has data in DB
                // if (false)
                //{
                //    new Character(player);
                //}

                // else
                // Get Character from DB
                // Load all data
                // Wanted = JsonConvert.DeserializeObject<Wanted>(row["wanted"].ToString())
                // etc
            }
        }
    }
}
