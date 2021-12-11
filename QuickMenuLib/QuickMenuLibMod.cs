using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using QuickMenuLib.UI;
using QuickMenuLib.UI.Elements;
using VRC.UI.Core;
using UnityEngine;

namespace QuickMenuLib
{
    public class QuickMenuLibMod : MelonMod
    {
        /// <summary>
        /// Every Mod Registered in QuickMenuLib
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
                PagePreparer.PrepareEverything();
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

        private void OnUIManagerInitialized(Action code)
        {
            MelonCoroutines.Start(OnUiManagerInitCoroutine(code));
        }

        private IEnumerator OnUiManagerInitCoroutine(Action code)
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;

            string ModValue = ModMenus.Count == 1 ? "Mod" : "Mods";
            LoggerInstance.Msg($"Found {ModMenus.Count} {ModValue} using QuickMenuLib.");

            while (UIManager.field_Private_Static_UIManager_0 == null) 
                yield return null;
            while (GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(true) == null) 
                yield return null;
            while (QuickMenuExtensions.MenuStateController == null) 
                yield return null;
            code();
        }
        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            LoggerInstance.Msg("Hi");
            base.OnSceneWasInitialized(buildIndex, sceneName);
        }
    }

    public static class Logger
    {
        private static MelonLogger.Instance MyLogger = new MelonLogger.Instance("QuickMenuLib", ConsoleColor.Magenta);

        public static void Msg(string msg) => MyLogger.Msg(msg);
        public static void Error(string msg) => MyLogger.Error(msg);
    }
}
