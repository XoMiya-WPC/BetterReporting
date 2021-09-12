using System;
using System.Reflection;
using Exiled.API.Features;
using BetterReporting.Handlers;
using ServerEvents = Exiled.Events.Handlers.Server;

namespace BetterReporting
{
    public class Plugin : Plugin<Config>
    {
        public ServerEventHandlers ServerEventHandlers;
        public HttpHandler HttpHandler;
        public override string Name { get; } = "BetterReporting (Port of Report-INtegration)";
        public override string Author { get; } = "SomewhatSane, XoMiya-WPC & Rin";
        public override string Prefix { get; } = "Better_Reporting";
        public override Version RequiredExiledVersion { get; } = new Version("3.0.0");
        public static readonly string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        internal const string lastModified = "2021/09/12";

        public override void OnEnabled()
        {
            //Log.Info($"{Name} V{version} by {Author}. Last modified: {lastModified}.");

            if (string.IsNullOrWhiteSpace(Config.Webhook))
            {
                Log.Error($"[Error Code 1] Webhook URL is not set. Halting plugin startup. - UPDATE {Server.Port}-config.yml");
                return;

            }
            try
            {
                var LocalReportColors = Config.LocalReportColor + 1;
            }
            catch (InvalidCastException e)
            {
                Log.Error(e);
                Log.Error("[Error Code 2] Colour is empty or contains nullspace. Halting plugin startup.");
                return;
            }
            try
            {
                var CheaterReportColors = Config.CheaterReportColor + 1;
            }
            catch (InvalidCastException e)
            {
                Log.Error(e);
                Log.Error("[Error Code 2] Colour is empty or contains nullspace. Halting plugin startup.");
                return;
            }
            if (string.IsNullOrEmpty(Config.CheaterRoleIds))
                Log.Warn("[Warn Code 1] You have not provided any Cheater Role Ids to be mentioned.");
            if (string.IsNullOrEmpty(Config.LocalRoleIds))
                Log.Warn("[Warn Code 2] You have not provided any Local Role Ids to be mentioned.");
            if (string.IsNullOrEmpty(Config.ManagementRoleIds))
                Log.Warn("[Warn Code 3] You have not provided any Management Role Ids to be mentioned.");
            Log.Info("Loading base scripts.");
            HttpHandler = new HttpHandler(this);

            Log.Info("Registering event handlers.");
            ServerEventHandlers = new ServerEventHandlers(this);
            ServerEvents.ReportingCheater += ServerEventHandlers.CheaterReport;
            ServerEvents.LocalReporting += ServerEventHandlers.LocalReport;

            if (Config.VerboseMode)
                Log.Info("Verbose Mode is enabled! Will print extra (debug) console messages.");

            Log.Info("Done.");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            ServerEvents.ReportingCheater -= ServerEventHandlers.CheaterReport;
            ServerEvents.LocalReporting -= ServerEventHandlers.LocalReport;
            ServerEventHandlers = null;

            Log.Info("Disabled");
        }
    }
}
