using System.ComponentModel;
using Exiled.API.Interfaces;

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
        [Description("Your Webhook URL from Discord. Copy and paste it in its entirity.")]
        public string Webhook { get; set; } = "https://discord.com/api/webhooks/XXXX/XXXX";
        [Description("A Custom Message that will send with the webhook alongside the role mentions. This is a string and can work with discord formatting i.e ('A new **Report** has been summitted :pencil:').")]
        public string CustomMessage { get; set; } = "A new **Report** has been summitted. :pencil:";
        [Description("You can visit this website for help converting your colours: https://www.mathsisfun.com/hexadecimal-decimal-colors.html. This is the custom DECIMAL colour of your embed for Local Reports. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal")]
        public int LocalReportColor { get; set; } = 255;
        [Description("This is the custom DECIMAL colour of your embed for Cheater Reports. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal")]
        public int CheaterReportColor { get; set; } = 16711680;
        [Description("Message sent to ingame staff when a report is submitted. - Accepts String")]
        public string AcMessage { get; set; } = "A new report has been submitted. - Check Discord";
        [Description("Verbose mode. Prints more Debug console messages.")]
        public bool VerboseMode { get; set; } = false;
    }
}
