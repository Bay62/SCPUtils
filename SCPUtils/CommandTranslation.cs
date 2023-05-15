namespace SCPUtils.Commands
{
    using System.ComponentModel;

    public class CommandTranslation
    {
        public string SenderError { get; set; } = "<color=red>You need \"<b>%permission%</b>\" to use this command!</color>";
        public string PlayerDatabaseError { get; set; } = "Player not found on Database!";
        [Description("%player% arguments are: Nickname [SteamID64]")]
        public string AsnWhitelist { get; set; } = "%player% has been added to Whitelist.";
        public string AsnWhitelistError { get; set; } = "%player% is already in Whitelist.";
        public string AsnUnwhitelist { get; set; } = "%player% has been removed to Whitelist.";
        public string AsnUnwhitelistError { get; set; } = "%player% is not in Whitelist.";

        [Description("Definitions")]
        public string Player { get; set; } = "[PlayerID, PlayerNickname or PlayerSteamID64]";
        public string Server { get; set; } = "[SERVER]";
        public string Plugin { get; set; } = "[SCPUtils]";
        public string Staff { get; set; } = "[STAFF]";
        public string Badge { get; set; } = "[Badge Name]";
        public string Broadcast { get; set; } = "[BroadcastID, BroadcastName]";

        [Description("ListOnlineCommand and ListStaffCommand")]
        public string Hp { get; set; } = "[HEALTH]";
        public string Ahp { get; set; } = "[ARTIFICIAL HEALTH]";
        public string Overwatch { get; set; } = "[OVERWATCH ENABLED]";
        public string Noclip { get; set; } = "[NOCLIP ENABLED]";
        public string God { get; set; } = "[GODMODE ENABLED]";
        public string Bypass { get; set; } = "[INTERCOM AND BYPASS ENABLED]";
        public string Intercom { get; set; } = "[INTERCOM MUTED]";
        public string VoiceChat { get; set; } = "[VOICE CHAT MUTED]";
        public string Dnt { get; set; } = "[DO NOT TRACK ENABLED]";
        public string Ra { get; set; } = "[REMOTED ADMIN AUTHENTICATED]";

        [Description("%role% is scpsl staff role")]
        public string SlStaff { get; set; } = "[SCP:SL STAFF: %role%]";
        public string NoPlayers { get; set; } = "No players online!";
        public string NoStaff { get; set; } = "No staffers online!";

        [Description("\"%arguments%\" will get usage this definition ↑")]
        public string UsageError { get; set; } = $"<color=yellow>Usage: %command% %arguments%</color>";
    }
}