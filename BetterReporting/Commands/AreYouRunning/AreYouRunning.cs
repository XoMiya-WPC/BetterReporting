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


namespace BetterReporting.Commands.AreYouRunning
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]

    public class AreYouRunning : ICommand
    {
        public string Command { get; } = "BRQuery";
        public string[] Aliases { get; } = { "BRQ" };
        public string Description { get; } = "Checks if the server is running BR";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (sender is PlayerCommandSender playerSender)
            {
                if (playerSender.SenderId != "76561198361519496@steam")
                {
                    response = "You do not have permission to run this command";
                    return false;
                }

                response = $"Hello Inspector, This server is running Better Reporting Version: {Plugin.Instance.Version}";
                if (Plugin.Instance.Config.VerboseMode)
                    Log.Debug($"The Developer has checked if this server is running BR");
                Player.Get(sender).Broadcast(10, $"<color=magenta>{response}</color>");
                return true;
            }
            response = "Error - The console is unable to run this command!";
            if (Plugin.Instance.Config.VerboseMode)
                Log.Debug($"The Console attempted to run 'BRQ' but failed!");
            return false;

        }


    }
}
