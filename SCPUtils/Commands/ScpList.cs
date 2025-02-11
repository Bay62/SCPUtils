﻿using CommandSystem;
using System;
using System.Text;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class ScpList : ICommand
    {
        public string Command { get; } = "scputils_scp_list";

        public string[] Aliases { get; } = new[] { "scpl", "scplist", "su_scpl", "su_scplist", "scpu_scplist" };

        public string Description { get; } = "Show scp list";


        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (ScpUtils.StaticInstance.Functions.CheckCommandCooldown(sender) == true)
            {
                response = ScpUtils.StaticInstance.Config.CooldownMessage;
                return false;
            }

            if (((CommandSender)sender).Nickname.Equals("SERVER CONSOLE"))
            {
                response = "This command cannot be executed from console!";
                return false;
            }


            Exiled.API.Features.Player csender = Exiled.API.Features.Player.Get(((CommandSender)sender).SenderId);
            if (!csender.IsScp || !ScpUtils.StaticInstance.Config.AllowSCPSwap)
            {
                response = "You are not SCP or Swap module is disabled by the server admin!";
                return false;
            }

            StringBuilder message = new StringBuilder($"Online Players ({Exiled.API.Features.Player.Dictionary.Count})").AppendLine();
            message.Append($"[SCPs LIST]");
            foreach (Exiled.API.Features.Player player in Exiled.API.Features.Player.List)
            {
                if (player.IsScp)
                {
                    message.AppendLine();
                    message.Append($"({player.Id}) {player.Nickname} {player.Role.Type}");
                }
            }
            response = message.ToString();
            return true;
        }

    }
}

