using System.Reflection;
using RimWorld;
using Verse;
using Verse.AI;
using HarmonyLib;

namespace BromsElectricalRepairKits
{
    [StaticConstructorOnStartup]
    public class Patcher
    {
        static Patcher()
        {
            var harmony = new Harmony("Broms.ElectricRepairKits");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(WorkGiver_FixBrokenDownBuilding))]
    [HarmonyPatch("FindClosestComponent")]
    class Patch_ComponentRepair
    {
        static void Postfix(Pawn pawn, ref Thing __result)
        {
            Thing lookForThing = GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(DefDatabase<ThingDef>.GetNamed("Broms_RepairKit")), PathEndMode.InteractionCell, TraverseParms.For(pawn, pawn.NormalMaxDanger()), 9999f, (Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x));

            if (lookForThing != null)
            {
                __result = lookForThing;
            }
        }
    }
}

