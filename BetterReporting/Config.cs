using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace BetterReporting
{
    public class Config : IConfig
    {
        [Description("Whether or not the plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Whether or not to override the server name with the Server_Name Config Value - Accepts Bool.")]
        public bool OverrideName { get; set; } = false;
        [Description("Defines the overriden server name - Acepts String.")]
        public string ServerName { get; set; }
        [Description("List of role IDs that will be pinged when a Local Report is sent. - Accepts String (Example '<@&modid> <@&adminid>').")]
        public string LocalRoleIds { get; set; }
        [Description("List of role IDs that will be pinged when a Cheater Report is sent. - Accepts String (Example '<@&modid> <@&adminid>').")]
        public string CheaterRoleIds { get; set; }
        [Description("List of role IDs that will be pinged if a user with remote admin authentication is reported. This role should probably mention some management or something. - Accepts String (Example '<@&managerid> <@&ownerid> ').")]
        public string ManagementRoleIds { get; set; }
        /*[Description("Local Report Channel ID that will dictate where to send Local Reports - Accepts Integer")]
        public ulong LocalReportChannelId { get; set; } = 123456789;
        [Description("Cheater Report Channel ID that will dictate where to send Cheater Reports - Accepts Integer")]
        public ulong CheaterReportChannelId { get; set; } = 123456789;
        [Description("Archived Reports Channel ID that will dictate where to send Reports once they have been marcked as complete. It is recommended to make this different from Local and Cheater Report Channel IDs - Accepts Integer")]
        public ulong ReportArchivesChannelId { get; set; } = 123456789;*/
        [Description("Your Webhook URL from Discord. Copy and paste it in its entirity. - Example - https://discord.com/api/webhooks/XXXX/XXXX")]
        public string Webhook { get; set; }
        [Description("A Custom Message that will send with the webhook alongside the role mentions. This is a string and can work with discord formatting i.e ('A new **Report** has been summitted :pencil:').")]
        public string CustomMessage { get; set; } = "A new **Report** has been summitted. :pencil:";
        [Description("You can visit this website for help converting your colours: https://www.mathsisfun.com/hexadecimal-decimal-colors.html. This is the custom DECIMAL colour of your embed for Local Reports if no Report Colour Flags are apparent. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal")]
        public uint DefaultLocalReportColor { get; set; } = 16711680;
        [Description("This is the custom DECIMAL colour of your embed for Cheater Reports if no Report Colour Flags are apparent. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal")]
        public uint DefaultCheaterReportColor { get; set; } = 16711680;
        [Description("Report Colour Flags - Have report auto change colour based on report message contents (string: uint)")]

        public Dictionary<string, uint> ReportColorFlags { get; private set; } = new Dictionary<string, uint>
            {
                {"Rule 1", 16473600}
            };
        [Description("Message sent to ingame staff when a report is submitted. - Accepts String")]
        public string AcMessage { get; set; } = "A new report has been submitted. - Check Discord";
        [Description("Show Global Badges on reports? - Accepts Bool (Def: true)")]
        public bool GlobalBadgeLookup { get; set; } = true;

        [Description("Verbose mode. Prints more Debug console messages.")]
        public bool VerboseMode { get; set; } = false;
    }
}
