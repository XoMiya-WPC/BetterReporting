using System;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Http;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Utf8Json;

namespace BetterReporting.Handlers
{
    public class PlayerEventHandlers
    {
        private readonly Plugin plugin;
        public PlayerEventHandlers(Plugin plugin) => this.plugin = plugin;

        public void OnVerified(VerifiedEventArgs ev)
        {
            string playerid = ev.Player.UserId;

            //Here we will compare a players userid against the watchlist
        }
    }
}