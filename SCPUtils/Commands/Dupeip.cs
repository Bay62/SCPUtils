﻿using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Text;


namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Dupeip : ICommand
    {
        public string Command { get; } = "scputils_dupeip";

        public string[] Aliases { get; } = new[] { "dupeip", "su_dupeip", "scpu_dupeip" };

        public string Description { get; } = "Check if player has another account on same IP";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (ScpUtils.StaticInstance.Functions.CheckCommandCooldown(sender) == true)
            {
                response = ScpUtils.StaticInstance.Config.CooldownMessage;
                return false;
            }

            if (!sender.CheckPermission("scputils.dupeip"))
            {
                response = "<color=red> You need a higher administration level to use this command!</color>";
                return false;
            }
            if (arguments.Count != 1)
            {
                response = $"<color=yellow>Usage: {Command} <player name/id> [Command may cause lag]</color>";
                return false;
            }
            string targetName = arguments.Array[1];
            Player databasePlayer = targetName.GetDatabasePlayer();

            if (databasePlayer == null)
            {
                response = "<color=yellow>Player not found on Database or Player is loading data!</color>";
                return false;
            }
            var databaseIp = GetIp.GetIpAddress(databasePlayer.Ip);
            if (databaseIp == null)
            {
                response = "<color=yellow>Invalid IP!</color>";
                return false;
            }

            StringBuilder message = new StringBuilder($"<color=green>[Accounts associated with the same IP ({databasePlayer.Ip} - {databasePlayer.Name} {databasePlayer.Id}@{databasePlayer.Authentication})]</color>").AppendLine();
            foreach (var userId in databaseIp.UserIds)
            {
                var databasePlayer2 = DatabasePlayer.GetDatabasePlayer(userId);
                if (databasePlayer2 != null)
                {

                    message.AppendLine();
                    message.Append(
                            $"Player: <color=yellow>{databasePlayer2.Name} ({databasePlayer2.Id}@{databasePlayer2.Authentication})</color>\nFirst Join: <color=yellow>{databasePlayer2.FirstJoin}</color>\nLast seen: <color=yellow>{databasePlayer2.LastSeen}</color>\nIsRestricted: <color=yellow>{databasePlayer2.IsRestricted()}</color>\nIsBanned: <color=yellow>{databasePlayer2.IsBanned()}</color>\nMuted: <color=yellow>{VoiceChat.VoiceChatMutes.QueryLocalMute(userId)}</color>\nTotal played as SCP: <color=yellow>{databasePlayer2.TotalScpGamesPlayed}</color>\nTotal suicide: <color=yellow>{databasePlayer2.ScpSuicideCount}</color>\nRound(s) ban left: <color=yellow>{databasePlayer2.RoundBanLeft}</color>")
                        .AppendLine();

                }
            }
            response = message.ToString();
            return true;
        }
    }
}