using System;
using System.Reflection;
using Exiled.API.Features;
using BetterReporting.Handlers;
using ServerEvents = Exiled.Events.Handlers.Server;
using PlayerEvents = Exiled.Events.Handlers.Player;

namespace BetterReporting
{
    public class Plugin : Plugin<Config>
    {
        public ServerEventHandlers ServerEventHandlers;
        public HttpHandler HttpHandler;
        public PlayerEventHandlers PlayerEventHandlers;
        public override string Name { get; } = "BetterReporting";
        public override string Author { get; } = "XoMiya-WPC, SomewhatSane";
        public override string Prefix { get; } = "Better_Reporting";
        public override Version RequiredExiledVersion { get; } = new Version("4.2.3");
        public static readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public override Version Version { get; } = new Version("1.3.0");
        internal const string lastModified = "04/02/2022";

        public override void OnEnabled()
        {
            Log.Info("BetterReporting is Starting... Checking Configs...");
            //Log.Info($"{Name} V{version} by {Author}. Last modified: {lastModified}.");
            if (string.IsNullOrWhiteSpace(Config.Webhook))
            {
                Log.Error($"[Error Code 1] Webhook URL is not set. Halting plugin startup.");
                return;
            }
            
            /*if (ulong.Equals(123456789, Config.LocalReportChannelId))
            { 
                Log.Error($"[Error Code 2] No Local Report Channel ID set - Halting plugin startup.");
                return;
            }
            if (ulong.Equals(123456789, Config.CheaterReportChannelId))
            {
                Log.Error($"[Error Code 3] No Cheater Report Channel ID set - Halting plugin startup.");
                return;
            }
            if (ulong.Equals(123456789, Config.ReportArchivesChannelId))
            {
                Log.Warn($"[Error Code 4] No Report Archives Channel ID Set - Warning plugin will not delete completed reports whilst this config remains blank.");
            }
            */
            try
            {
                var DefaultLocalReportColors = Config.DefaultLocalReportColor + 1;
            }
            catch (InvalidCastException e)
            {
                Log.Error(e);
                Log.Error("[Error Code 5] Colour is empty or contains nullspace. Halting plugin startup.");
                return;
            }
            try
            {
                var DefaultLCheaterReportColors = Config.DefaultLCheaterReportColor + 1;
            }
            catch (InvalidCastException e)
            {
                Log.Error(e);
                Log.Error("[Error Code 6] Colour is empty or contains nullspace. Halting plugin startup.");
                return;
            }
            if (string.IsNullOrEmpty(Config.LocalRoleIds))
                Log.Warn("[Warn Code 1] You have not provided any Local Role Ids to be mentioned.");
            if (string.IsNullOrEmpty(Config.CheaterRoleIds))
                Log.Warn("[Warn Code 2] You have not provided any Cheater Role Ids to be mentioned.");
            if (string.IsNullOrEmpty(Config.ManagementRoleIds))
                Log.Warn("[Warn Code 3] You have not provided any Management Role Ids to be mentioned.");
            HttpHandler = new HttpHandler(this);

            ServerEventHandlers = new ServerEventHandlers(this);
            ServerEvents.ReportingCheater += ServerEventHandlers.CheaterReport;
            ServerEvents.LocalReporting += ServerEventHandlers.LocalReport;
            PlayerEventHandlers = new PlayerEventHandlers(this);
            PlayerEvents.Verified += PlayerEventHandlers.OnVerified;
            if (Config.VerboseMode)
                Log.Info("Verbose Mode is enabled! Will print extra (debug) console messages.");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvents.ReportingCheater -= ServerEventHandlers.CheaterReport;
            ServerEvents.LocalReporting -= ServerEventHandlers.LocalReport;
            PlayerEvents.Verified -= PlayerEventHandlers.OnVerified;
            PlayerEventHandlers = null;
            ServerEventHandlers = null;

            Log.Info("Disabled");
        }
    }
}
