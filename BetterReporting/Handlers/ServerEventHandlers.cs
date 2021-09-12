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
    public class ServerEventHandlers
    {
        private readonly Plugin plugin;
        public ServerEventHandlers(Plugin plugin) => this.plugin = plugin;

        public void CheaterReport(ReportingCheaterEventArgs ev)
        {
            //Check for and prevent self reports.
            if (ev.Reported.UserId == ev.Reporter.UserId)
            {
                ev.Reporter.Broadcast(5, "You cannot report yourself!");
                return;
            }
            //Check for and prevent reporting of Northwood Staff.
            else if (ev.Reported.IsNorthwoodStaff)
            {
                ev.Reporter.Broadcast(5, "You cannot report Northwood Staff!");
                return;
            }
            var CheaterRoleIdsString = "";
            if (ev.Reported.RemoteAdminAccess)
                CheaterRoleIdsString = plugin.Config.CheaterRoleIds + " " + plugin.Config.ManagementRoleIds;
            else if (!ev.Reported.RemoteAdminAccess)
                CheaterRoleIdsString = plugin.Config.CheaterRoleIds;
            Log.Info("[FLAG] - Warning a user with Remote Admin Authentication has been reported to Northwood for Cheating.");
            string ReporterRole = Convert.ToString(ev.Reporter.Role);
            string ReportedRole = Convert.ToString(ev.Reported.Role);
            if (ReporterRole == "None")
                ReporterRole = "Spectator";
            if (ReportedRole == "None")
                ReportedRole = "Spectator";
            var ServerNameVal = "";
            if (plugin.Config.OverrideName)
                ServerNameVal = plugin.Config.ServerName;
            else if (!plugin.Config.OverrideName)
                ServerNameVal = StripRichText(GameCore.ConfigFile.ServerConfig.GetString("server_name"));
            var webHook = new
            {
                content = plugin.Config.CustomMessage + "\n" + CheaterRoleIdsString,
                embeds = new List<object>
                {
                    new
                    {
                        title = "New In-game report (Cheater)",
                        color = plugin.Config.CheaterReportColor,
                        fields = new List<object>
                        {
                            new
                            {
                                name = "Server Name",
                                value = ServerNameVal,
                            },
                            new
                            {
                                name = "Server Endpoint",
                                value = Server.IpAddress+":"+Server.Port,
                            },
                            new
                            {
                                name = "Reporter Nickname",
                                value = ev.Reporter.Nickname,
                            },
                            new
                            {
                                name = "Reporter User ID",
                                value = "`"+ev.Reporter.UserId+"`",
                            },
                            new
                            {
                                name = "Reporter Class",
                                value = ReporterRole,
                            },
                            new
                            {
                                name = "Reported Player Nickname",
                                value = ev.Reported.Nickname,
                            },
                            new
                            {
                                name = "Reported Player User ID",
                                value = "`"+ev.Reported.UserId+"`",
                            },
                            new
                            {
                                name = "Reported Player ID",
                                value = "`"+ev.Reported.Id+"`",
                            },
                            new
                            {
                                name = "Reported Class",
                                value = ReportedRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Report Reason",
                                value = ev.Reason,
                            }
                        },
                        timestamp = DateTime.UtcNow.ToString("o")
                    }
                }
            };
            if (plugin.Config.VerboseMode)
                Log.Debug($"A new cheater report has been made. Sending report to Discord... JSON: {Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook))}");
            ev.Reporter.Broadcast(5, "Thank you for submitting a Cheater Report.");
            foreach (var user in Player.List)
            {
                if (user.ReferenceHub.serverRoles.AdminChatPerms)
                {
                    user.Broadcast(10, plugin.Config.AcMessage, Broadcast.BroadcastFlags.AdminChat);
                    if (plugin.Config.VerboseMode)
                    {
                        Log.Debug($"Sent Admin Chat alerting to report to: {user.Nickname}");
                    }
                }
            }
            if (ev.Reported.RemoteAdminAccess)
                Log.Info("Attention - A Staff Member was Reported!");
            StringContent reportStringContent = new StringContent(Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook)), Encoding.UTF8, "application/json");
            _ = plugin.HttpHandler.Send(plugin.Config.Webhook, reportStringContent);
        }
        public void LocalReport(LocalReportingEventArgs ev)
        {
            //Check for and prevent self reports.
            if (ev.Target.UserId == ev.Issuer.UserId)
            {
                ev.Issuer.Broadcast(5, "You cannot report yourself!");
                return;
            }
            //Check for and prevent reporting of Northwood Staff.
            else if (ev.Target.IsNorthwoodStaff)
            {
                ev.Issuer.Broadcast(5, "You cannot report Northwood Staff!");
                return;
            }
            var LocalRoleIdsString = "";
            if (ev.Target.RemoteAdminAccess)
                LocalRoleIdsString = plugin.Config.LocalRoleIds + " " + plugin.Config.ManagementRoleIds;
            else if (!ev.Target.RemoteAdminAccess)
                LocalRoleIdsString = plugin.Config.LocalRoleIds;
            string ReporterRole = Convert.ToString(ev.Issuer.Role);
            string ReportedRole = Convert.ToString(ev.Target.Role);
            if (ReporterRole == "None")
                ReporterRole = "Spectator";
            if (ReportedRole == "None")
                ReportedRole = "Spectator";
            var ServerNameVal = "";
            if (plugin.Config.OverrideName)
                ServerNameVal = plugin.Config.ServerName;
            else if (!plugin.Config.OverrideName)
                ServerNameVal = StripRichText(GameCore.ConfigFile.ServerConfig.GetString("server_name"));
            var webHook = new
            {
                content = plugin.Config.CustomMessage + "\n" + LocalRoleIdsString,
                embeds = new List<object>
                {
                    new
                    {
                        title = "New In-game Report (Rule Breaking)",
                        color = plugin.Config.LocalReportColor,
                        fields = new List<object>
                        {
                            new
                            {
                                name = "Server Name",
                                value = ServerNameVal,
                            },
                            new
                            {
                                name = "Server Endpoint",
                                value = Server.IpAddress+":"+Server.Port,
                            },
                            new
                            {
                                name = "Reporter Nickname",
                                value = ev.Issuer.Nickname,
                            },
                            new
                            {
                                name = "Reporter User ID",
                                value = "`"+ev.Issuer.UserId+"`",
                            },
                            new
                            {
                                name = "Reporter Class",
                                value = ReporterRole,
                            },
                            new
                            {
                                name = "Reported Player Nickname",
                                value = ev.Target.Nickname,
                            },
                            new
                            {
                                name = "Reported Player User ID",
                                value = "`"+ev.Target.UserId+"`",
                            },
                            new
                            {
                                name = "Reported Player ID",
                                value = "`"+ev.Target.Id+"`",
                            },
                            new
                            {
                                name = "Reported Class",
                                value = ReportedRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Report Reason",
                                value = ev.Reason,
                            }
                        },
                        timestamp = DateTime.UtcNow.ToString("o")
                    }
                }
            };
            if (plugin.Config.VerboseMode)
                Log.Debug($"A new local report has been made. Sending report to Discord... JSON: {Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook))}");
            ev.Issuer.Broadcast(5, "Thank you for submitting a Report.");
            foreach (var user in Player.List)
            {
                if (user.ReferenceHub.serverRoles.AdminChatPerms)
                {
                    user.Broadcast(10, plugin.Config.AcMessage, Broadcast.BroadcastFlags.AdminChat);
                    if (plugin.Config.VerboseMode)
                    {
                        Log.Debug($"Sent Admin Chat alerting to report to: {user.Nickname}");
                    }
                }
            }
            if (ev.Target.RemoteAdminAccess)
                Log.Info("Attention - A Staff Member was Reported!");
            StringContent reportStringContent = new StringContent(Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook)), Encoding.UTF8, "application/json");
            _ = plugin.HttpHandler.Send(plugin.Config.Webhook, reportStringContent);
        }
        public static string StripRichText(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
