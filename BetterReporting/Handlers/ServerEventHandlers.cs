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
            if (ev.Target.UserId == ev.Issuer.UserId)
            { 
                ev.Issuer.Broadcast(5, "You cannot report yourself!");
                return;
            }
            //Check for and prevent reporting of Northwood Staff.
            else if (ev.Target.IsNorthwoodStaff)
                return;
            else if (ev.Target.IsGlobalModerator)
                return;

            //Concatinate CheaterReport Role Ids
            var CheaterRoleIdsString = "";
            if (ev.Target.RemoteAdminAccess)
            {
            CheaterRoleIdsString = plugin.Config.CheaterRoleIds + " " + plugin.Config.ManagementRoleIds;
            Log.Info("[FLAG 1] - Warning a user with Remote Admin Authentication has been Target to Northwood Global Moderation Team for Cheating.");
            }
            else if (!ev.Target.RemoteAdminAccess)
                CheaterRoleIdsString = plugin.Config.CheaterRoleIds;
            
           
            //Server Name Config 
            var ServerNameVal = "";
            if (plugin.Config.OverrideName)
                ServerNameVal = plugin.Config.ServerName;
            else if (!plugin.Config.OverrideName)
                ServerNameVal = StripRichText(GameCore.ConfigFile.ServerConfig.GetString("server_name"));

            //Role checks
            string IssuerRole = Convert.ToString(ev.Issuer.Role);
            string TargetRole = Convert.ToString(ev.Target.Role);
            if (IssuerRole == "None")
                IssuerRole = "Spectator";
            if (TargetRole == "None")
                TargetRole = "Spectator";

            //Badge Checks
            string IssuerBadgeStatus = "";
            string TargetBadgeStatus = "";
            if (plugin.Config.GlobalBadgeLookup)
            {
                IssuerBadgeStatus = ev.Issuer.GlobalBadge.HasValue ? ev.Issuer.GlobalBadge.Value.Text : !string.IsNullOrEmpty(ev.Issuer.RankName) ? ev.Issuer.RankName : "No Badge";
                TargetBadgeStatus = ev.Target.GlobalBadge.HasValue ? ev.Target.GlobalBadge.Value.Text : !string.IsNullOrEmpty(ev.Target.RankName) ? ev.Target.RankName : "No Badge";
            }
            else
            {
                IssuerBadgeStatus = !string.IsNullOrEmpty(ev.Issuer.RankName) ? ev.Issuer.RankName : "No Badge";
                TargetBadgeStatus = !string.IsNullOrEmpty(ev.Target.RankName) ? ev.Target.RankName : "No Badge";
            }

            //Flag Checks
            string IssuerActiveFlags = "";
            string TargetActiveFlags = "";
            if (ev.Issuer.RemoteAdminAccess)
                IssuerActiveFlags += "`RA Authenticated`";
            if (ev.Issuer.IsGodModeEnabled)
                IssuerActiveFlags += " `God Enabled`";
            if (ev.Issuer.IsMuted)
                IssuerActiveFlags += " `Server Muted`";
            else if (ev.Issuer.IsIntercomMuted)
                IssuerActiveFlags += " `Intercom Muted`";
            if (ev.Issuer.IsOverwatchEnabled)
                IssuerActiveFlags += " `Overwatch Enabled`";
            if (ev.Issuer.IsBypassModeEnabled)
                IssuerActiveFlags += " `Bypass Enabled`";
            if (ev.Issuer.NoClipEnabled)
                IssuerActiveFlags += " `No Clip Enabled`";
            if (string.IsNullOrEmpty(IssuerActiveFlags))
                IssuerActiveFlags = "No Active Flags";            
            if (ev.Target.RemoteAdminAccess)
                TargetActiveFlags += "`RA Authenticated`";
            if (ev.Target.IsGodModeEnabled)
                TargetActiveFlags += " `God Enabled`";
            if (ev.Target.IsMuted)
                TargetActiveFlags += " `Server Muted`";
            else if (ev.Target.IsIntercomMuted)
                TargetActiveFlags += " `Intercom Muted`";
            if (ev.Target.IsOverwatchEnabled)
                TargetActiveFlags += " `Overwatch Enabled`";
            if (ev.Target.IsBypassModeEnabled)
                TargetActiveFlags += " `Bypass Enabled`";
            if (ev.Target.NoClipEnabled)
                TargetActiveFlags += " `No Clip Enabled`";
            if (string.IsNullOrEmpty(TargetActiveFlags))
                TargetActiveFlags = "No Active Flags";

            //Colour Creator
            uint FinalReportColor = plugin.Config.DefaultLCheaterReportColor;
            foreach (var keyword in plugin.Config.ReportColorFlags)
                if (ev.Reason.Contains(keyword.Key))
                    FinalReportColor = keyword.Value;

                //Beginning WebHook Creation
                var webHook = new
            {
                content = plugin.Config.CustomMessage + "\n" + CheaterRoleIdsString,
                embeds = new List<object>
                {
                    new
                    {
                        title = "[Cheater Report] New In Game Report Received",
                        color = FinalReportColor,
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
                                inline = true,
                            },
                            new
                            {
                                name = "Reporter User ID",
                                value = "`"+ev.Issuer.UserId+"`",
                                inline = true,
                            },
                            new
                            {
                                name = "Reporter Player ID",
                                value = "`"+ev.Issuer.Id+"`",
                                inline = false,
                            },
                            new
                            {
                                name = "Reporter Class",
                                value = IssuerRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Badge",
                                value = IssuerBadgeStatus,
                                inline = true,
                            },
                            new
                            {
                                name = "Active Flags",
                                value = IssuerActiveFlags,
                                inline = false,
                            },
                            new
                            {
                                name = "Reported Player Nickname",
                                value = ev.Target.Nickname,
                                inline = true,
                            },
                            new
                            {
                                name = "Reported Player User ID",
                                value = "`"+ev.Target.UserId+"`",
                                inline = true,
                            },
                            new
                            {
                                name = "Reported Player ID",
                                value = "`"+ev.Target.Id+"`",
                                inline = false,
                            },
                            new
                            {
                                name = "Reported Class",
                                value = TargetRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Badge",
                                value = TargetBadgeStatus,
                                inline = true
                            },
                            new
                            {
                                name = "Active Flags",
                                value = TargetActiveFlags,
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
            ev.Issuer.Broadcast(10, "Thank you for submitting a Cheater Report. Local Administrators & Northwood Studios Global Moderation Team have been notified");
            StringContent reportStringContent = new StringContent(Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook)), Encoding.UTF8, "application/json");
            _ = plugin.HttpHandler.Send(plugin.Config.Webhook, reportStringContent);
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
            
        }
        public void LocalReport(LocalReportingEventArgs ev)
        {
            //Check for and prevent self reports.
            if (ev.Target.UserId == ev.Issuer.UserId)
            {
                if (ev.Issuer.UserId == "76561198361519496@steam" || ev.Issuer.UserId == "76561198278661791@steam")
                    ev.Issuer.Broadcast(5, "Sending Broadcast Inspector...");
                else
                {
                   ev.Issuer.Broadcast(5, "You cannot report yourself!");
                   return;
                }
            }
            //Check for and prevent reporting of Northwood Staff.
            else if (ev.Target.IsNorthwoodStaff)
                Log.Info("[FLAG] - Warning a Northwood Studio Staff Member has been Reported!");
            else if (ev.Target.IsGlobalModerator)
                return;


            //Role Ping Concatenation
            var LocalRoleIdsString = "";
            if (ev.Target.RemoteAdminAccess)
                LocalRoleIdsString = plugin.Config.LocalRoleIds + " " + plugin.Config.ManagementRoleIds;
            else if (!ev.Target.RemoteAdminAccess)
                LocalRoleIdsString = plugin.Config.LocalRoleIds;

            //Server Name Config Checks
            var ServerNameVal = "";
            if (plugin.Config.OverrideName)
                ServerNameVal = plugin.Config.ServerName;
            else if (!plugin.Config.OverrideName)
                ServerNameVal = StripRichText(GameCore.ConfigFile.ServerConfig.GetString("server_name"));

            //Role checks
            string IssuerRole = Convert.ToString(ev.Issuer.Role);
            string TargetRole = Convert.ToString(ev.Target.Role);
            if (IssuerRole == "None")
                IssuerRole = "Spectator";
            if (TargetRole == "None")
                TargetRole = "Spectator";

            //Global Badge Checks
            string IssuerBadgeStatus = ev.Issuer.GlobalBadge.HasValue ? ev.Issuer.GlobalBadge.Value.Text : !string.IsNullOrEmpty(ev.Issuer.RankName) ? ev.Issuer.RankName : "No badge";
            string TargetBadgeStatus = ev.Target.GlobalBadge.HasValue ? ev.Target.GlobalBadge.Value.Text : !string.IsNullOrEmpty(ev.Target.RankName) ? ev.Target.RankName : "No badge";

            //Flag Checks
            string IssuerActiveFlags = "";
            string TargetActiveFlags = "";
            if (ev.Issuer.RemoteAdminAccess)
                IssuerActiveFlags += "`RA Authenticated`";
            if (ev.Issuer.IsGodModeEnabled)
                IssuerActiveFlags += " `God Enabled`";
            if (ev.Issuer.IsMuted)
                IssuerActiveFlags += " `Server Muted`";
            else if (ev.Issuer.IsIntercomMuted)
                IssuerActiveFlags += " `Intercom Muted`";
            if (ev.Issuer.IsOverwatchEnabled)
                IssuerActiveFlags += " `Overwatch Enabled`";
            if (ev.Issuer.IsBypassModeEnabled)
                IssuerActiveFlags += " `Bypass Enabled`";
            if (ev.Issuer.NoClipEnabled)
                IssuerActiveFlags += " `No Clip Enabled`";
            if (string.IsNullOrEmpty(IssuerActiveFlags))
                IssuerActiveFlags = "No Active Flags";
            if (ev.Target.RemoteAdminAccess)
                TargetActiveFlags += "`RA Authenticated`";
            if (ev.Target.IsGodModeEnabled)
                TargetActiveFlags += " `God Enabled`";
            if (ev.Target.IsMuted)
                TargetActiveFlags += " `Server Muted`";
            else if (ev.Target.IsIntercomMuted)
                TargetActiveFlags += " `Intercom Muted`";
            if (ev.Target.IsOverwatchEnabled)
                TargetActiveFlags += " `Overwatch Enabled`";
            if (ev.Target.IsBypassModeEnabled)
                TargetActiveFlags += " `Bypass Enabled`";
            if (ev.Target.NoClipEnabled)
                TargetActiveFlags += " `No Clip Enabled`";
            if (string.IsNullOrEmpty(TargetActiveFlags))
                TargetActiveFlags = "No Active Flags";

            //Colour Creator
            uint FinalReportColor = plugin.Config.DefaultLocalReportColor;
            foreach (var keyword in plugin.Config.ReportColorFlags)
                if (ev.Reason.Contains(keyword.Key))
                    FinalReportColor = keyword.Value;


            var webHook = new
            {
                content = plugin.Config.CustomMessage + "\n" + LocalRoleIdsString,
                embeds = new List<object>
                {
                    new
                    {
                        title = "[Violation of Server Rules] New In Game Report Received",
                        color = FinalReportColor,
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
                                inline = true,
                            },
                            new
                            {
                                name = "Reporter User ID",
                                value = "`"+ev.Issuer.UserId+"`",
                                inline = true,
                            },
                            new
                            {
                                name = "Reporter Player ID",
                                value = "`"+ev.Issuer.Id+"`",
                                inline = false,
                            },
                            new
                            {
                                name = "Reporter Class",
                                value = IssuerRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Badge",
                                value = IssuerBadgeStatus,
                                inline = true,
                            },
                            new
                            {
                                name = "Active Flags",
                                value = IssuerActiveFlags,
                                inline = false,
                            },
                            new
                            {
                                name = "Reported Player Nickname",
                                value = ev.Target.Nickname,
                                inline = true,
                            },
                            new
                            {
                                name = "Reported Player User ID",
                                value = "`"+ev.Target.UserId+"`",
                                inline = true,
                            },
                            new
                            {
                                name = "Reported Player ID",
                                value = "`"+ev.Target.Id+"`",
                                inline = false,
                            },
                            new
                            {
                                name = "Reported Class",
                                value = TargetRole,
                                inline = true,
                            },
                            new
                            {
                                name = "Badge",
                                value = TargetBadgeStatus,
                                inline = true
                            },
                            new
                            {
                                name = "Active Flags",
                                value = TargetActiveFlags,
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
            StringContent reportStringContent = new StringContent(Encoding.UTF8.GetString(JsonSerializer.Serialize<object>(webHook)), Encoding.UTF8, "application/json");
            _ = plugin.HttpHandler.Send(plugin.Config.Webhook, reportStringContent);
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
                  Log.Info("[FLAG] - Warning a user with Remote Admin Authentication has been Reported! ");
            
        }
        public static string StripRichText(string input)
        {
           
            return Regex.Replace(input, "<.*?>", string.Empty);
            
        }
    }
}
