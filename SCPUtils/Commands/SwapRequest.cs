﻿using CommandSystem;
using System;
using System.Linq;
using Eplayer = PluginAPI.Core.Player;

namespace SCPUtils.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    internal class SwapRequest : ICommand
    {
        public string Command { get; } = ScpUtils.StaticInstance.configs.SwapRequestCommand;

        public string[] Aliases { get; } = ScpUtils.StaticInstance.configs.SwapRequestCommandAliases;

        public string Description { get; } = "Send a SCP swap request to a player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (ScpUtils.StaticInstance.Functions.CheckCommandCooldown(sender) == true)
            {
                response = ScpUtils.StaticInstance.configs.CooldownMessage;
                return false;
            }

            PluginAPI.Core.Player player = PluginAPI.Core.Player.Get(((CommandSender)sender).SenderId);

            if (!ScpUtils.StaticInstance.configs.AllowSCPSwap)
            {
                response = $"<color=yellow>SCP swap module is disabled on this server!</color>";
                return false;
            }

            if (!player.IsSCP)
            {
                response = $"<color=yellow>Only SCPs are allowed to use this command!</color>";
                return false;
            }

            else
            {

                if (arguments.Count < 1)
                {
                    response = $"<color=yellow>Usage: {Command} <player id/nickname or SCP-NUMBER (example: swap SCP-939)></color>";
                    return false;
                }
                else if (PluginAPI.Core.Round.Duration.TotalSeconds >= ScpUtils.StaticInstance.configs.MaxAllowedTimeScpSwapRequest)
                {
                    response = $"<color=red>The round has been started from too much time due this reason our system decided to deny this SCP swap request, you are too slow next time try to be faster</color>";
                    return false;
                }
                else if (ScpUtils.StaticInstance.configs.AllowSCPSwapOnlyFullHealth && player.Health < player.MaxHealth)
                {
                    response = $"<color=red>You have taken damage due this reason our system decided to deny this SCP swap request, next time be more careful and play better</color>";
                    return false;
                }
                else
                {
                    Eplayer target;
                    switch (arguments.Array[1].ToString().ToUpper())
                    {
                        case "SCP-939":
                        case "SCP939":
                        case "939":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp939);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-049":
                        case "SCP049":
                        case "049":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp049);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-0492":
                        case "SCP0492":
                        case "0492":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp0492);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-106":
                        case "SCP106":
                        case "106":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp106);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-096":
                        case "SCP096":
                        case "096":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp096);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-079":
                        case "SCP079":
                        case "079":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp079);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        case "SCP-173":
                        case "SCP173":
                        case "173":

                            target = Eplayer.GetPlayers().FirstOrDefault(x => x.Role == PlayerRoles.RoleTypeId.Scp173);
                            if (target == null)
                            {
                                response = $"<color=red>{arguments.Array[1].ToString().ToUpper() } didn't spawned!</color>";
                                return false;
                            }
                            break;

                        default:
                            target = Eplayer.Get(arguments.Array[1].ToString());
                            break;

                    }

                    var seconds = Math.Round(ScpUtils.StaticInstance.configs.MaxAllowedTimeScpSwapRequestAccept - PluginAPI.Core.Round.Duration.TotalSeconds);

                    if (target == null)
                    {
                        response = $"<color=red>Invalid player nickname/id or invalid SCP name!</color>";
                        return false;
                    }

                    if (ScpUtils.StaticInstance.configs.AllowSCPSwapOnlyFullHealth && target.Health < target.MaxHealth)
                    {
                        response = $"<color=red>Target has not full health!</color>";
                        return false;
                    }

                    if (ScpUtils.StaticInstance.EventHandlers.SwapRequest.ContainsKey(target) || ScpUtils.StaticInstance.EventHandlers.SwapRequest.ContainsValue(target))
                    {
                        response = $"<color=red>Target already sent a request or has a pending request</color>";
                        return false;
                    }

                    if (ScpUtils.StaticInstance.EventHandlers.SwapRequest.ContainsKey(player) || ScpUtils.StaticInstance.EventHandlers.SwapRequest.ContainsValue(player))
                    {
                        response = $"<color=red>You already sent or received a swap request, verify that</color>";
                        return false;
                    }

                    if (target == player)
                    {
                        response = $"<color=red>You can't send swap request to yourself!</color>";
                        return false;
                    }

                    if (!target.IsSCP)
                    {
                        response = $"<color=red>Target is not SCP!</color>";
                        return false;
                    }

                    if (target.Role == player.Role)
                    {
                        response = $"<color=red>You have the same role of another player!</color>";
                        return false;
                    }
                    if (target.CustomInfo != null && ScpUtils.StaticInstance.configs.DeniedSwapCustomInfo?.Any() == true)
                    {
                        if (ScpUtils.StaticInstance.configs.DeniedSwapCustomInfo.Contains(target.CustomInfo.ToString()))
                        {
                            response = $"<color=red>Target is using a custom SCP therefore swap is denied!</color>";
                            return false;
                        }
                    }

                    if (player.CustomInfo != null && ScpUtils.StaticInstance.configs.DeniedSwapCustomInfo?.Any() == true)
                    {
                        if (ScpUtils.StaticInstance.configs.DeniedSwapCustomInfo.Contains(player.CustomInfo.ToString()))
                        {
                            response = $"<color=red>You are using a custom SCP therefore swap is denied!</color>";
                            return false;
                        }
                    }

                    ScpUtils.StaticInstance.EventHandlers.SwapRequest.Add(player, target);

                    target.ClearBroadcasts();

                    var message = ScpUtils.StaticInstance.configs.SwapRequestBroadcast.Content;

                    message = message.Replace("%player%", player.DisplayNickname).Replace("%scp%", player.RoleName).Replace("%seconds%", seconds.ToString());
                    target.SendBroadcast(message, ScpUtils.StaticInstance.configs.SwapRequestBroadcast.Duration, ScpUtils.StaticInstance.configs.SwapRequestBroadcast.Type, false);

                    response = $"<color=green>Request has been sent successfully to {target.DisplayNickname}, player has {seconds} seconds to accept the request. Use .cancel to cancel the request</color>";

                    return true;
                }
            }
        }
    }
}
