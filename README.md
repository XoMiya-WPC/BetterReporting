# BetterReporting
An SCP: Secret Laboratory Plugin that runs on the E.X.I.L.E.D Framework. It improves the reporting feature of SL by adding mentions, and more information into Discord Webhooks to ensure staff are efficient when dealing with reports.

<h1>Coming Soon:tm:</h1>

<h1>Requirements</h1>
This plugin requires EXILED 3.0.0 
This plugin WILL NOT WORK on previous versions
<h1>Config</h1>

| Config  | Type | Value |
| ------------- | ------------- | ------------- |
| IsEnabled  | Boolean  | true  |
| LocalRoleIds  | String  | Empty  |
| CheaterRoleIds  | String  | Empty  |
| Webhook  | String  | Empty  |
| CustomMessage  | String  | A new **Report** has been summitted :pencil:  |
| LocalReportColor  | String  | 215  |
| CheaterReportColor  | String | 16711680  |
| ACMessage  | String | A new report has been submitted - Check Discord  |
| VerboseMode  | Bool  | false  |

* **IsEnabled:** Defines if the plugin will be enabled or not.

* **LocalRoleIds + CheaterRoleIds:** A list of pingable role ids in the format of <@&id> <@&id>. The @ defines it as a mention and the & specifies it is a role. These will be sent for local reports and cheater reports respectively.

* **Webhook:** The webhook URL. Make sure to paste the entire thing. For help see below.

* **CustomMessage:** This is a simple Message that can be added alongside the embed. String format ofc.

* **LocalReportColor + CheaterReportColor:** These are decimal values of colours. To convert your hex to decimal you can use [this website](https://www.mathsisfun.com/hexadecimal-decimal-colors.html "Convert Hexadecimal to decimal"). Despite being string you must only use decimal!!! These will be changed respectively.

* **VerboseMode:** Outputs more console information

<h1>Features</h1>

* Role Mentions for both Local and Cheater reports allowing you to alert staff as soon as a report is made

<h1>Info & Contact</h1>
This plugin was created due to extraordinary request by server owners running exiled servers. 

BetterReporting is a port of Report-INtegration.
This plugin was made in collaboration by Somewhatsane, Rin & Myself. 

For help or issues Contact me on Discord @ XoMiya#0113 or join my [discord](https://discord.gg/DxWXw9jmXn "The Lab").

<h2>Webhook Help</h2>

**Webhook on Discord**
1. Go to **Server settings** -> **Webhooks** -> **Create Webhook**.
2. Setup name, avatar and the channel, where it will be posted. Copy *Webhook URL*.
3. Click **`Save`** and then the **`Done`** button.
