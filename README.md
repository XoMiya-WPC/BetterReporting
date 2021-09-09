# BetterReporting
An SCP: Secret Laboratory Plugin that runs on the [EXILED Framework](https://github.com/Exiled-Team/EXILED "Exiled"). It improves the reporting feature of SL by adding mentions, and more information into Discord Webhooks to ensure staff are efficient when dealing with reports.

<h1>Coming Soon:tm:</h1>

<h1>Requirements</h1>

This plugin requires [EXILED](https://github.com/Exiled-Team/EXILED/releases "Exiled Releases") 3.0.0 
This plugin **WILL NOT WORK** on previous versions
<h1>Config</h1>

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
| LocalReportColor  | String  | 255  |
| CheaterReportColor  | String | 16711680  |
| AcMessage  | String | A new report has been submitted - Check Discord  |
| VerboseMode  | Boolean  | false  |

* **Is_Enabled:** Defines if the plugin will be enabled or not. Only enter `true` or `false`.

* **Override_Name:** Defines if the plugin will use the overriden Server Name instead of the one pinched from config_gameplay.txt.

* **Server_Name:** Defines the name you wish the embed to refer to the server as instead of the default. 

* **Local_Role_Ids + Cheater_Role_Ids:** A list of pingable role ids in the format of <@&id> <@&id>. The @ defines it as a mention and the & specifies it is a role. These will be sent for local reports and cheater reports respectively. To get an ID of a role see help section.

* **Webhook:** The webhook URL. Make sure to paste the entire thing. For help see below.

* **Custom_Message:** This is a simple Message that can be added alongside the embed. String format ofc.

* **Local_Report_Color + Cheater_Report_Color:** These are decimal values of colours. To convert your hex to decimal you can use [this website](https://www.mathsisfun.com/hexadecimal-decimal-colors.html "Convert Hexadecimal to decimal"). Despite being string you must only use decimal!!! These will be changed respectively.

* **Ac_Message:** Defines the message that will be sent ingame to anyone that has remote admin chat.

* **Verbose_Mode:** Outputs more console information if enabled. Only enter `true` or `false`.


<h2>Webhook Help</h2>

**Webhook on Discord**
1. Go to **Server settings** -> **Webhooks** -> **Create Webhook**.
2. Setup name, avatar and the channel, where it will be posted. `Copy *Webhook URL*`.
3. Click **`Save`** and then the **`Done`** button.

<h2>Discord Role ID Help</h2>

**Enabling Dev Mode**
1. Visit this link [here](https://support.discord.com/hc/en-us/articles/206346498-Where-can-I-find-my-User-Server-Message-ID "Where can I find my User/Server/Message ID?")
2. Once you have done that right click on your role and copy it. Then replace the `id` part of <@&id> with your copied id.
3. Use my ploogin and have fun!

<h2>Error and Warn Codes Help</h2>

<h4>Errors</h4>

* **[Error Code 1]** - *Webhook URL is not set. Halting plugin startup.* Error code 1 refers to someone leaving the Webhook Value in the config file blank or using whitespace characters. Ensure your webhook URL is exactly as above^^^*
* **[Error Code 2]** - *Colour is empty or contains nullspace. Halting plugin startup.* Error code 2 indicates you have not entered a value into either the `LocalReportColor` or `CheaterReportColor` configs. The plugin will not run until it has been fixed.

<h4>Warns</h4>

* **[Warn Code 1]** - *You have not provided any Cheater Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for cheater reports. `LocalRoleIds` value is empty.
* **[Warn Code 2]** - *You have not provided any Local Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for local reports. `CheaterRoleIds` value is empty.
* **[Warn Code 3]** - *You have not provided any Management Role Ids to be mentioned.* This will not prevent the plugin from working but will mean no mentions will be sent for reports against remote admin authenticated users. `RoleIds` value is empty.

<h1>Features</h1>

* Role Mentions for both Local and Cheater Reports allowing you to alert staff as soon as a report is made.
* Custom Colours for both Local and Cheater Reports.
* Optional ping for a staff manager like position if someone with remote admin access gets reported.
* Custom In Game message to admin chat to notify of a report.
* Option to have a custom name used in the embed as some servers have alternate things that rich text cannot strip i.e. \n or other.
* Prevents reporting of self and Northwood Staff reducing spam of reports.
* Sends players class in report information.

<h1>Info & Contact</h1>
This plugin was created due to extraordinary request by server owners running exiled servers. 

BetterReporting is a port of Report-INtegration.
This plugin was made in collaboration by [Somewhatsane](https://github.com/SomewhatSane "SomewhatSane") & Myself using Rin's original Report-INtegration. 

For help or issues Contact me on Discord @ XoMiya#0113 or join my [discord](https://discord.gg/DxWXw9jmXn "The Lab").
