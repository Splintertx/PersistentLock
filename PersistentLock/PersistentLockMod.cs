using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace PersistentLock
{
    public class Globals
    {
        public static bool firstLoad = true;
        public static HashSet<string> lockedItems = new HashSet<string>();
    }

    [HarmonyPatch(typeof(SPInventoryVM), "SaveItemLockStates")]
    public class SaveItemLockStatesMod
    {        
        static void Postfix(SPInventoryVM __instance)
        {
            foreach (SPItemVM item in (__instance as SPInventoryVM).RightItemListVM)
            {
                if (item.IsLocked || (Globals.lockedItems.Contains(item.StringId)))
                    Globals.lockedItems.Add(item.StringId);
                else
                    Globals.lockedItems.Remove(item.StringId);
            }

            Globals.firstLoad = false;
        }
    }

    [HarmonyPatch(typeof(SPInventoryVM), "InitializeInventory")]
    public class InitializeInventoryMod
    {
        static void Postfix(SPInventoryVM __instance)
        {
            if (Globals.firstLoad)
                return;

            foreach (SPItemVM item in (__instance as SPInventoryVM).RightItemListVM)
                item.IsLocked = Globals.lockedItems.Contains(item.StringId);
        }
    }
}
