using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using CommandSystem;
using RemoteAdmin;
using System.Net.Http;
using Utf8Json;


namespace BetterReporting.Commands.ReportClaim
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]

    public class ReportClaim : ICommand
    {
        public string Command { get; } = "Claim";
        public string[] Aliases { get; } = { "ClaimReport" };
        public string Description { get; } = "Notify other staff you have claimed a recent report";
        private const string UsageHelp = "Usage: claim ticketid";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (sender is PlayerCommandSender playerSender)
            {
                if (!playerSender.CheckPermission("BetterReporting.Claim"))
                {
                    response = "You do not have permission to Claim Reports!";
                    return false;
                }
                if (arguments.Count < 1)
                {
                    response = UsageHelp;
                    return false;
                }
                string ticketNum = arguments.At(0);
                string DisplayName = playerSender.Nickname;
                response = $"You have claimed report id {ticketNum}!";
                if (Plugin.Instance.Config.VerboseMode)
                    Log.Debug($"{DisplayName} has claimed a ticket");
                foreach (var user in Player.List)
                {
                    if (user.ReferenceHub.serverRoles.AdminChatPerms)
                    {
                        user.Broadcast(10, $"{DisplayName} has claimed report ID: {ticketNum}!", Broadcast.BroadcastFlags.AdminChat);
                        if (Plugin.Instance.Config.VerboseMode)
                        {
                            Log.Debug($"Sent Admin Chat alerting claim report to: {user.Nickname}");
                        }
                    }
                }
                var webHook = new
                {
                    content = $"__**Report Claimed:**__\nReport ID `{ticketNum}` claimed by `{DisplayName}` @ **{DateTime.UtcNow.ToString()}** from RA.",
                    
                };
                StringContent ClaimStringContent = new StringContent(Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook)), Encoding.UTF8, "application/json");
                _ = Plugin.Instance.HttpHandler.Send(Plugin.Instance.Config.Webhook, ClaimStringContent);
                return true;
            }
            response = "Error - The console is unable to Claim Reports!";
            if (Plugin.Instance.Config.VerboseMode)
                Log.Debug($"The Console attempted to run 'Claim' but failed!");
            return false;

        }


    }
}
