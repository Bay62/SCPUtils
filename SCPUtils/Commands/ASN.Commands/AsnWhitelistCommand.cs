namespace SCPUtils.Commands.ASN.Commands
{
    using CommandSystem;
    using System;

    public class AsnWhitelistCommand : ICommand
    {
        public string Command { get; } = "whitelist";
        public string[] Aliases { get; } = new[]
        {
            "add", "wl", "w"
        };

        public string Description { get; } = "Whitelist a player, ie GFN member, to enter in this server.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (ScpUtils.StaticInstance.Functions.CheckCommandCooldown(sender) == true)
            {
                response = ScpUtils.StaticInstance.configs.CooldownMessage;
                return false;
            }

            if (!sender.CheckPermission("scputils.asn.whitelist"))
            {
                response = ScpUtils.StaticInstance.commandTranslation.SenderError;
                return false;
            }
            else if (arguments.Count != 1)
            {
                response = ScpUtils.StaticInstance.commandTranslation.UsageError.Replace("%command%", $"{arguments.Array[0]} {arguments.Array[1]} {arguments.Array[2]}").Replace("%arguments%", ScpUtils.StaticInstance.commandTranslation.Player);
                return false;
            }
            else
            {
                string target = arguments.Array[3];
                Player databasePlayer = target.GetDatabasePlayer();

                if (databasePlayer == null)
                {
                    response = ScpUtils.StaticInstance.commandTranslation.PlayerDatabaseError;
                    return false;
                }
                else if (databasePlayer.ASNWhitelisted is true)
                {
                    response = ScpUtils.StaticInstance.commandTranslation.AsnWhitelistError.Replace("%player%", $"{databasePlayer.Name} {databasePlayer.Id}@{databasePlayer.Authentication}");
                    return false;
                }

                databasePlayer.ASNWhitelisted = true;
                Database.LiteDatabase.GetCollection<Player>().Update(databasePlayer);
                response = ScpUtils.StaticInstance.commandTranslation.AsnWhitelist.Replace("%player%", $"{databasePlayer.Name} {databasePlayer.Id}@{databasePlayer.Authentication}");
                return true;
            }
        }
    }
}
