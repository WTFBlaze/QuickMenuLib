using HarmonyLib;
using MelonLoader;
using PepsiLib;
using PepsiLib.UI.Elements;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine.UI;
using VRC.UI.Elements.Menus;
using static MelonLoader.MelonLogger;
using static PepsiLib.UI.QuickMenuExtensions;

namespace PepsiLib.UI.Patches
{
    internal class QuickMenuPatch
    {
        internal static HarmonyMethod GetLocalPatch(Type patchType, string name)
        {
            if (patchType == null) return null;

            MethodInfo patchMethod = patchType.GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);

            if (patchMethod == null) return null;

            return new HarmonyMethod(patchMethod);
        }
        public static void PatchQuickMenu()
        {
            try
            {
                Patch.PatchMethod(AccessTools.Method(typeof(VRC.UI.Elements.QuickMenu), "OnEnable", null, null), null, GetLocalPatch(typeof(QuickMenuPatch), nameof(QMOnEnable)));
                Patch.PatchMethod(AccessTools.Method(typeof(VRC.UI.Elements.QuickMenu), "OnDisable", null, null), null, GetLocalPatch(typeof(QuickMenuPatch), nameof(QMOnDisable)));

                Msg("Successfully Hooked QuickMenu");
            } catch(Exception e)
            {
                Error("Failed to Hook QuickMenu!");
                Error(e);
            }
        }

        private static bool Initialized = false;
        private static object WaitingForSelectedUser;

        private static void QMOnEnable()
        {
            if (!Initialized)
            {
                MelonCoroutines.Start(InitializeQuickMenu());
                MelonCoroutines.Start(InitializeLeftWing());
                MelonCoroutines.Start(InitializeRightWing());
                MelonCoroutines.Start(InitializeTargetMenu());
                Initialized = true;
            }

            //Waits until the user selects someone. Could probably directly patch into the Select User method.
            WaitingForSelectedUser = MelonCoroutines.Start(SetSelectedUser());
        }

        private static void QMOnDisable()
        {
            if(WaitingForSelectedUser != null)
            {
                MelonCoroutines.Stop(WaitingForSelectedUser);
                WaitingForSelectedUser = null;
            }
        }

        private static IEnumerator InitializeQuickMenu()
        {
            //Wait for Everything to initialize properly. This new Menu seems fragile.
            //Maybe fetching things in a different way would allow for initialization on Application Start.
            while (MenuStateCtrl == null) yield return null;

            Msg("Setting Up Launchpad.");

            var MainCategory = new QuickMenuCategory("PepsiLib_FrontPage", "PepsiLib");

            PepsiLibMod.MainMenu = new QuickMenuPage("PepsiLibHome", "PepsiLib", false, true);

            MainCategory.AddButton("Mods", "Mods", "Mod Menus using PepsiLib", PepsiLibMod.MainMenu.Open);

            foreach (var menu in PepsiLibMod.ModMenus)
            {
                menu.MyModMenu = PepsiLibMod.MainMenu.AddSubMenu(menu.MenuName, menu.MenuName, $"Content for {menu.MenuName}", false);
                menu.OnQuickMenuInitialized();
            }

            yield break;

        }

        private static IEnumerator InitializeRightWing()
        {
            while (RightWing.field_Private_MenuStateController_0 == null) yield return null;

            Msg("Setting Up Right Wing.");

            PepsiLibMod.RightWingMenu = new QuickMenuWingMenu("PepsiLibRight", "Mods", false);
            new QuickMenuWingButton("PepsiLib_RightWing_Button", "Mods", "Mod Menus using PepsiLib", PepsiLibMod.RightWingMenu.Open, null, false);
            foreach (var menu in PepsiLibMod.ModMenus)
            {
                menu.MyRightWingMenu = PepsiLibMod.RightWingMenu.AddSubMenu($"{menu.MenuName}_Right", menu.MenuName, $"Content for {menu.MenuName}");
                menu.OnWingMenuRightInitialized();
            }

            yield break;
        }

        private static IEnumerator InitializeLeftWing()
        {
            while (LeftWing.field_Private_MenuStateController_0 == null) yield return null;

            Msg("Setting Up Left Wing.");

            PepsiLibMod.LeftWingMenu = new QuickMenuWingMenu("PepsiLib_Left", "PepsiLib", true);
            new QuickMenuWingButton("PepsiLib_LeftWing_Button", "Mods", "Mod Menus using PepsiLib", PepsiLibMod.LeftWingMenu.Open);

            foreach (var menu in PepsiLibMod.ModMenus)
            {
                menu.MyLeftWingMenu = PepsiLibMod.LeftWingMenu.AddSubMenu($"{menu.MenuName}_Left", menu.MenuName, $"Content for {menu.MenuName}");
                menu.OnWingMenuLeftInitialized();
            }

            yield break;
        }

        private static IEnumerator InitializeTargetMenu()
        {
            while (Instance.GetComponentInChildren<SelectedUserMenuQM>(true) == null) yield return null;

            Msg("Setting Up Target Menu.");

            var MainCategory = new QuickMenuCategory("PepsiLib_TargetPage", "PepsiLib", Instance.GetComponentInChildren<SelectedUserMenuQM>(true).GetComponentInChildren<ScrollRect>().content.Find("Buttons_UserActions").parent);

            PepsiLibMod.TargetMenu = new QuickMenuPage("PepsiLibTarget", "PepsiLib", false, true);

            MainCategory.AddButton("Mods", "Mods", "Mod Menus using PepsiLib", PepsiLibMod.TargetMenu.Open);

            foreach (var menu in PepsiLibMod.ModMenus)
            {
                menu.MyTargetMenu = PepsiLibMod.TargetMenu.AddSubMenu($"{menu.MenuName}_Target", menu.MenuName, $"Content for {menu.MenuName}", false);
                menu.OnTargetMenuInitialized();
            }
        }

        private static IEnumerator SetSelectedUser()
        {
            while (Instance.GetComponentInChildren<SelectedUserMenuQM>(true).field_Private_IUser_0 == null) yield return null;
            foreach(var menu in PepsiLibMod.ModMenus)
            {
                menu.SelectedUser = Instance.GetComponentInChildren<SelectedUserMenuQM>(true).field_Private_IUser_0;
            }
        }
    }
}
