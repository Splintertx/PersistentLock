using HarmonyLib;
using System;
using System.Windows.Forms;
using TaleWorlds.MountAndBlade;

namespace PersistentLock
{
    class SubModule : MBSubModuleBase
    {
        public static readonly string ModuleFolderName = "PersistentLock";

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            try
            {
                var harmony = new Harmony("mod.bannerlord.splintert");
                harmony.PatchAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Initialising PersistentLock:\n\n{ex.Message}");
            }
        }
    }
}
