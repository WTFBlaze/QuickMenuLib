using HarmonyLib;
using MelonLoader;
using System.Collections.Generic;
using System.Reflection;

namespace PepsiLib.UI.Patches
{
    internal class Patch
    {
        public static readonly HarmonyLib.Harmony harmonyInstance = new HarmonyLib.Harmony(BuildInfo.Name);

        private static readonly List<MethodBase> patchedMethods = new List<MethodBase>();

        internal static void PatchMethod(MethodBase targetMethod, HarmonyMethod preMethod, HarmonyMethod postMethod)
        {
            harmonyInstance.Patch(targetMethod, preMethod, postMethod);

            patchedMethods.Add(targetMethod);
        }

        internal static void UnpatchAllMethods()
        {
            for (int i = 0; i < patchedMethods.Count; i++)
            {
                harmonyInstance.Unpatch(patchedMethods[i], HarmonyPatchType.All, harmonyInstance.Id);
            }

            patchedMethods.Clear();
        }
    }
}
