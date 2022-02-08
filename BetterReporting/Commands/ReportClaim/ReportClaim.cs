using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using CommandSystem;
using RemoteAdmin;


namespace BetterReporting.Commands.ReportClaim
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]

    public class ReportClaim : ICommand
    {
        //private readonly Plugin plugin;
        //public ReportClaim(Plugin plugin) => this.plugin = plugin;

        public string Command { get; } = "Claim";
        public string[] Aliases { get; } = { "ClaimReport" };
        public string Description { get; } = "Notify other staff you have claimed a recent report";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (sender is PlayerCommandSender playerSender)
            {
                if (!playerSender.CheckPermission("BetterReporting.Claim"))
                {
                    response = "You do not have permission to Claim Reports!";
                    return false;
                }
                string DisplayName = playerSender.Nickname;
                response = "You have claimed the most recent report!";
                /*if (plugin.Config.VerboseMode)
                    Log.Debug($"{DisplayName} has claimed a ticket");*/
                foreach (var user in Player.List)
                {
                    if (user.ReferenceHub.serverRoles.AdminChatPerms)
                    {
                        user.Broadcast(5, $"{DisplayName} has claimed the the most recent Report!", Broadcast.BroadcastFlags.AdminChat);
                        /*if (plugin.Config.VerboseMode)
                        {
                            Log.Debug($"Sent Admin Chat alerting claim report to: {user.Nickname}");
                        }*/
                    }
                }
                return true;
            }
            response = "Error - The console is unable to Claim Reports!";
            //if (plugin.Config.VerboseMode)
            //    Log.Debug($"The Console attempted to run 'Claim' but failed!");
            return false;

        }


    }
}
