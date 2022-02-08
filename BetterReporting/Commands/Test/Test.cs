using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using CommandSystem;
using RemoteAdmin;


namespace BetterReporting.Commands.Test
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Test : ICommand 
    {
        public string Command { get; } = "Test";
        public string[] Aliases { get; } = {"brt"};
        public string Description { get; } = "Testing my funny";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            if (sender is PlayerCommandSender playerSender)
            {
                if (!playerSender.CheckPermission("BetterReporting.Test"))
                {
                    response = "No ( ͡° ͜ʖ ͡°)";
                    return false;
                }
                string DisplayName = playerSender.Nickname;
                response = "Tested the funny";
                Cassie.Message("Test", false, false);
                Log.Info($"{DisplayName}Has run Test");
                return true;
            }
            else
            {
                response = "Tested the funny";
                Log.Info($"The Console Has run Test");
                return true;
            }


        }

        
    }
}
