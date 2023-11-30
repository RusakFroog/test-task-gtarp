using GTANetworkAPI;
using Newtonsoft.Json;
using System.Timers;

namespace lessons.Test
{
    public class Wanted
    {
        public int ValueStars 
        { 
            get => _valueStars; 
            private set => _valueStars = value;
        }

        private int _valueStars;
        private Timer _timer = null;

        public Wanted()
        {
            ValueStars = 0;
        }

        [JsonConstructor]
        public Wanted(int valueStars)
        {
            ValueStars = valueStars;
        }

        public void SetStars(Player player, int newValueStars)
        {
            if (player == null)
                return;

            ValueStars = newValueStars;

            player.SetSharedData("WANTED_STARS", newValueStars);

            if (_timer == null)
            {
                _timer = new Timer(10_000);

                _timer.Elapsed += (s, e) =>
                {
                    if (ValueStars - 1 < 0 || player == null)
                    {
                        _timer.Stop();
                        
                        _timer = null;

                        return;
                    }

                    SetStars(player, ValueStars - 1);
                };

                _timer.Start();
            }
            else
            {
                _timer.Stop();

                _timer.Start();
            }

            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "client.wanted.viewStars", newValueStars);
            });
        }

        public void ClearStars(Player player)
        {
            ValueStars = 0;

            player.SetSharedData("WANTED_STARS", 0);

            NAPI.Task.Run(() =>
            {
                NAPI.ClientEvent.TriggerClientEvent(player, "client.wanted.viewStars", 0);
            });
        }
    }
}
