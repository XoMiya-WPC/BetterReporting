# BetterReporting
An SCP: Secret Laboratory Plugin that runs on the [EXILED Framework](https://github.com/Exiled-Team/EXILED "Exiled"). It improves the reporting feature of SL by adding mentions, and more information into Discord Webhooks to ensure staff are efficient when dealing with reports.

<h2>Requirements</h2>

This plugin requires [EXILED](https://github.com/Exiled-Team/EXILED/releases "Exiled Releases") `4.2.3`
This plugin **WILL NOT WORK** on previous versions
<h2>Config</h2>

| Config  | Type | Value |
| ------------- | ------------- | ------------- |
| Is_Enabled  | Boolean  | true  |
| Override_Name  | Boolean  | false  |
| Server_Name  | String  | Empty  |
| Local_Role_Ids  | String  | Empty  |
| Cheater_Role_Ids  | String  | Empty  |
| Management_Role_Ids  | String  | Empty  |
| Webhook  | String  | Empty  |
| Custom_Message  | String  | A new **Report** has been summitted :pencil:  |
| Default_Local_Report_Color  | uint  | 16711680  |
| Default_Cheater_Report_Color  | uint | 16711680  |
| Report_Color_Flags | Dictionary | See Below |
| Ac_Message  | String | A new report has been submitted - Check Discord  |
| Global_Badge_Lookup | Boolean | true |
| Verbose_Mode  | Boolean  | false  |

* **Is_Enabled:** Defines if the plugin will be enabled or not. Only enter `true` or `false`.

* **Override_Name:** Defines if the plugin will use the overriden Server Name instead of the one pinched from config_gameplay.txt.

* **Server_Name:** Defines the name you wish the embed to refer to the server as instead of the default. 

* **Local_Role_Ids + Cheater_Role_Ids:** A list of pingable role ids in the format of <@&id> <@&id>. The `@` defines it as a mention and the `&` specifies it is a role. These will be sent for local reports and cheater reports respectively. To get an ID of a role see help section.

* **Management_Role_Ids:** A list of pingable role ids in the format off <@&id> <@&id>. The `@` defines it as a mention and `&` specifies it is a role. These will be sent for staff getting reported and when a report is not claimed.

* **Webhook:** The webhook URL. Make sure to paste the entire thing. For help see below.

* **Custom_Message:** This is a simple Message that can be added alongside the embed. String format ofc.

* **Default_Local_Report_Color + Default_Cheater_Report_Color:** These are decimal values of colours. To convert your hex to decimal you can use [this website](https://www.mathsisfun.com/hexadecimal-decimal-colors.html "Convert Hexadecimal to decimal"). Despite being string you must only use decimal!!! These will be changed respectively.
* **Report_Color_Flags:** This is a dictionary following the format of `"Text": Decimalcolor` and allows a phrase in the form of a string (left) to be checked to see if it is present within the report reason. If it is the report embed colour will be changed to the decimal color (right). See Example of Config below for more help.

* **Ac_Message:** Defines the message that will be sent ingame to anyone that has remote admin chat.

* **Global_Badge_Lookup:** Is a bool (true/false) defining whether you want the plugin to check for Players Global Badges when sending reports. If enabled and a user has a Global Badge then that Badge will be sent in reports over server ones.

* **Verbose_Mode:** Outputs more console information if enabled. Only enter `true` or `false`.


<h3>Webhook Help</h3>

**Webhook on Discord**
1. Go to **Server settings** -> **Webhooks** -> **Create Webhook**.
2. Setup name, avatar and the channel, where it will be posted. `Copy Webhook URL`.
3. Click **`Save`** and then the **`Done`** button.

<h3>Discord Role ID Help</h3>

**Enabling Dev Mode**
1. Visit this link [here](https://support.discord.com/hc/en-us/articles/206346498-Where-can-I-find-my-User-Server-Message-ID "Where can I find my User/Server/Message ID?")
2. Once you have done that right click on your role and copy it. Then replace the `id` part of <@&id> with your copied id.
3. Use my ploogin and have fun!

<h3>Error and Warn Codes Help</h3>

<h4>Errors</h4>

* **[Error Code 1]** - *Webhook URL is not set. Halting plugin startup.* Error code 1 refers to someone leaving the Webhook Value in the config file blank or using whitespace characters. Ensure your webhook URL is exactly as above^^^
* **[Error Code 5/6]** - *Colour is empty or contains nullspace. Halting plugin startup.* Error code 2 indicates you have not entered a value into either the `Default_Local_Report_Color` or `Default_Cheater_Report_Color` configs. The plugin will not run until it has been fixed.

<h4>Warns</h4>

* **[Warn Code 1]** - *You have not provided any Cheater Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for cheater reports. `LocalRoleIds` value is empty.
* **[Warn Code 2]** - *You have not provided any Local Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for local reports. `CheaterRoleIds` value is empty.
* **[Warn Code 3]** - *You have not provided any Management Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for reports against remote admin authenticated users. `RoleIds` value is empty.

<h2>Features</h2>

* Role Mentions for both Local and Cheater Reports allowing you to alert staff as soon as a report is made.
* Custom Colours for both Local and Cheater Reports & the ability to have the colour change depending on report reason contents.
* Optional ping for a staff manager like position if someone with remote admin access gets reported.
* Custom In Game message to admin chat to notify of a report.
* Option to have a custom name used in the embed as some servers have alternate things that rich text cannot strip i.e. \n or other.
* Prevents reporting of self and Northwood Staff reducing spam of reports.
* Sends players class in report information.
* Sends both Reporter and Reported players Remote Admin Flags such as server mutes and RA Auth Status.
* Global Badge and Server Badge Status are shown in reports if they are present.
* Northwood Staff will not be reported for cheating to reduce spam reports sent the Global Moderation Team.
* Plugin will send Flags to the console if a player with Remote Admin Authentication is reported.

<h1>Info & Contact</h1>
This plugin was created due to extraordinary request by server owners running exiled servers. 

This plugin was made in collaboration by [Somewhatsane](https://github.com/SomewhatSane "SomewhatSane") & Myself.


For help or issues check out the [Setup Guide](https://www.betterreporting.net/index.php/setup-guide "Setup Guide | Better Reporting") or alternatively Contact me by joining my [discord](https://discord.gg/DxWXw9jmXn "XoMiya's Kitchen").

<h2>Default Config Generated</h2>

```yaml
Better_Reporting:
# Whether or not the plugin is enabled.
  is_enabled: true
  # Whether or not to override the server name with the Server_Name Config Value - Accepts Bool.
  override_name: false
  # Defines the overriden server name - Acepts String.
  server_name: 
  # List of role IDs that will be pinged when a Local Report is sent. - Accepts String (Example '<@&modid> <@&adminid>').
  local_role_ids: 
  # List of role IDs that will be pinged when a Cheater Report is sent. - Accepts String (Example '<@&modid> <@&adminid>').
  cheater_role_ids: 
  # List of role IDs that will be pinged if a user with remote admin authentication is reported. This role should probably mention some management or something. - Accepts String (Example '<@&managerid> <@&ownerid> ').
  management_role_ids: 
  # Your Webhook URL from Discord. Copy and paste it in its entirity. - Example - https://discord.com/api/webhooks/XXXX/XXXX
  webhook: 
  # A Custom Message that will send with the webhook alongside the role mentions. This is a string and can work with discord formatting i.e ('A new **Report** has been summitted :pencil:').
  custom_message: 'A new **Report** has been summitted. :pencil:'
  # You can visit this website for help converting your colours: https://www.mathsisfun.com/hexadecimal-decimal-colors.html. This is the custom DECIMAL colour of your embed for Local Reports if no Report Colour Flags are apparent. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal
  default_local_report_color: 16711680
  # This is the custom DECIMAL colour of your embed for Cheater Reports if no Report Colour Flags are apparent. If you have a hex colour code, please convert it first to decimal. - Accepts Decimal
  default_l_cheater_report_color: 16711680
  # Report Colour Flags - Have report auto change colour based on report message contents (string: uint)
  report_color_flags:
    Rule 1: 16473600
  # Message sent to ingame staff when a report is submitted. - Accepts String
  ac_message: A new report has been submitted. - Check Discord
  # Show Global Badges on reports? - Accepts Bool (Def: true)
  global_badge_lookup: true
  # Verbose mode. Prints more Debug console messages.
  verbose_mode: false
```

![img](https://img.shields.io/github/downloads/XoMiya-WPC/BetterReporting/total?style=for-the-badge)

