using MelonLoader;
using PepsiLib.UI;
using PepsiLib.UI.Patches;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VRC.UI.Elements;
using PepsiLib.UI.Elements;

using static MelonLoader.MelonLogger;

namespace PepsiLib
{
    public class PepsiLibMod : MelonMod
    {
        /// <summary>
        /// Every Mod Registered in PepsiLib
        /// </summary>
        public static List<ModMenu> ModMenus = new List<ModMenu>();

        //We expose these to the Mods in case they want to add content to these pages. Unlikely but it can't hurt.
        public static QuickMenuPage MainMenu = null;
        public static QuickMenuPage TargetMenu = null;
        public static QuickMenuWingMenu LeftWingMenu = null;
        public static QuickMenuWingMenu RightWingMenu = null;

        public override void OnApplicationStart()
        {
            Msg("Preparing QuickMenu Hooks");
            QuickMenuPatch.PatchQuickMenu();

            OnUIManagerInitialized(delegate
            {
                string ModValue = ModMenus.Count == 1 ? "Mod" : "Mods";

                Msg($"Found {ModMenus.Count} {ModValue} using PepsiLib.");

                FixLaunchpadScrolling();
            });
        }

        //https://github.com/RequiDev/RemodCE
        private void FixLaunchpadScrolling()
        {
            var dashboard = QuickMenuExtensions.Instance.field_Public_Transform_0.Find("Window/QMParent/Menu_Dashboard").GetComponent<UIPage>();
            var scrollRect = dashboard.GetComponentInChildren<ScrollRect>();
            var dashboardScrollbar = scrollRect.transform.Find("Scrollbar").GetComponent<Scrollbar>();

            var dashboardContent = scrollRect.content;
            dashboardContent.GetComponent<VerticalLayoutGroup>().childControlHeight = true;

            scrollRect.enabled = true;
            scrollRect.verticalScrollbar = dashboardScrollbar;
            scrollRect.viewport.GetComponent<RectMask2D>().enabled = true;
        }

        /// <summary>
        /// This Method registers your Mod Menu. You should call this OnApplicationStart
        /// </summary>
        /// <param name="menu">Your Mod's class, should inherit and override methods of ModMenu</param>
        public static void RegisterModMenu(ModMenu menu)
        {
            ModMenus.Add(menu);
        }

        internal void OnUIManagerInitialized(Action code)
        {
            MelonCoroutines.Start(OnUiManagerInitCoro(code));
        }

        internal IEnumerator OnUiManagerInitCoro(Action code)
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null)
                yield return null;
            code();
        }

    }
}
