﻿using Assets._Scripts.Dissonance;
using HarmonyLib;
using System.Linq;
using Exiled.Permissions.Extensions;

namespace SCPUtils
{
    [HarmonyPatch(typeof(DissonanceUserSetup), nameof(DissonanceUserSetup.CallCmdAltIsActive))]
    public class SCPSpeak
    {

        public static void Prefix(DissonanceUserSetup __instance, bool value)
        {
            var player = Exiled.API.Features.Player.Get(__instance.gameObject);
            if (string.IsNullOrEmpty(player?.UserId) || player.Team != Team.SCP) return;
            else if (string.IsNullOrEmpty(ServerStatic.GetPermissionsHandler()._groups.FirstOrDefault(g => g.Value == player.ReferenceHub.serverRoles.Group).Key) && !string.IsNullOrEmpty(player.ReferenceHub.serverRoles.MyText)) return;
            else if (player.CheckPermission($"scputils_speak.{player.Role.ToString().ToLower()}"))
            {
                __instance.MimicAs939 = value;
            }
        }
    }
}