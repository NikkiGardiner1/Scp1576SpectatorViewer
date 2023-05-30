using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Exiled.API.Features;
using HarmonyLib;
using InventorySystem.Items.Usables.Scp1576;
using NorthwoodLib.Pools;
using PlayerRoles;
using static HarmonyLib.AccessTools;

namespace Scp1576SpectatorViewer;

[HarmonyPatch(typeof(Scp1576Item), nameof(Scp1576Item.OnUsingStarted))]
public class ItemUsingPatch
{
    [HarmonyTranspiler]
    private static IEnumerable<CodeInstruction> OnUsingStarted(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

        int index = newInstructions.FindIndex(i => i.opcode == OpCodes.Callvirt) - 2;
        
        newInstructions.InsertRange(index, new CodeInstruction[]
        {
            new(OpCodes.Ldarg_0),
            new(OpCodes.Call, Method(typeof(ItemUsingPatch), nameof(ViewSpectators))),
        });
        
        foreach (var instruction in newInstructions)
            yield return instruction;
        
        ListPool<CodeInstruction>.Shared.Return(newInstructions);
    }

    private static void ViewSpectators(Scp1576Item scp1576Item)
    {
        var player = Player.Get(scp1576Item.Owner);

        var spectators = Player.List.Where(p => p.Role.Type == RoleTypeId.Spectator && p != player).ToList();
        
        player.ShowHint($"<size=24><align=left><b>S<lowercase>pectators</lowercase> {spectators.Count} </b></align></size>", 15f);
    }
}