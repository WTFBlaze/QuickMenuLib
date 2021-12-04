using MelonLoader;
using PepsiLib.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using VRC.UI.Elements;
using PepsiLib.UI.Elements;

using static MelonLoader.MelonLogger;
using VRC.UI.Core;
using UnityEngine;

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
            OnUIManagerInitialized(delegate
            {
                string ModValue = ModMenus.Count == 1 ? "Mod" : "Mods";
                Msg($"Found {ModMenus.Count} {ModValue} using PepsiLib.");
                
                PagePreparer.PrepareEverything();
                Utils.RemoveVrcPlus();
            });
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
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;

            //early init

            while (UIManager.field_Private_Static_UIManager_0 == null) 
                yield return null;
            while (GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(true) == null) 
                yield return null;
            while (QuickMenuExtensions.MenuStateController == null) 
                yield return null;
            code();
        }

    }
}
