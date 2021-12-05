using PepsiLib.UI.Elements;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static MelonLoader.MelonLogger;

namespace PepsiLib.UI
{
    internal class PagePreparer
    {
        internal static void PrepareEverything()
        {
            InitializeQuickMenu();
            InitializeTargetMenu();
            InitializeWingMenus();
        }

        private static void InitializeQuickMenu()
        {
            try
            {
                Msg("Setting Up Launchpad.");

                var MainCategory = new QuickMenuCategory("PepsiLib");

                PepsiLibMod.MainMenu = new QuickMenuPage("PepsiLib", false, true);

                MainCategory.AddButton("Mods", "Mod Menus using PepsiLib", PepsiLibMod.MainMenu.Open);

                foreach (var menu in PepsiLibMod.ModMenus)
                {
                    menu.MyModMenu = PepsiLibMod.MainMenu.AddSubMenu(menu.MenuName, $"Content for {menu.MenuName}", false, menu.Logo);
                    try
                    {
                        menu.OnQuickMenuInitialized();
                    }
                    catch (Exception e)
                    {
                        Error($"Failed to Initialize QuickMenu for {menu.MenuName}!");
                        Error(e);
                    }
                }
            } catch(Exception e)
            {
                Error($"Failed to initialize Launchpad! Exception: {e}");
            }
        }

        private static void InitializeTargetMenu()
        {
            try
            {
                Msg("Setting Up Target Menu.");

                var MainCategory = new QuickMenuCategory("PepsiLib-Targets", QuickMenuExtensions.SelectedUserMenu.transform.Find("ScrollRect").GetComponent<ScrollRect>().content);

                PepsiLibMod.TargetMenu = new QuickMenuPage("PepsiLib-Targets", false, true);

                MainCategory.AddButton("Mods", "Mod Menus using PepsiLib", PepsiLibMod.TargetMenu.Open);

                foreach (var menu in PepsiLibMod.ModMenus)
                {
                    menu.MyTargetMenu = PepsiLibMod.TargetMenu.AddSubMenu($"{menu.MenuName}-Targets", $"Content for {menu.MenuName}", false, menu.Logo);
                    try
                    {
                        menu.OnTargetMenuInitialized();
                    }
                    catch (Exception e)
                    {
                        Error($"Failed to Initialize TargetMenu for {menu.MenuName}!");
                        Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                Error($"Failed to initialize Target Menu! Exception: {e}");
            }
        }

        private static void InitializeWingMenus()
        {
            try
            {
                PepsiLibMod.LeftWingMenu = new QuickMenuWingMenu("PepsiLib_Left");
                new QuickMenuWingButton("Mods", "Mod Menus using PepsiLib", PepsiLibMod.LeftWingMenu.Open);

                PepsiLibMod.RightWingMenu = new QuickMenuWingMenu("PepsiLib_Right", false);
                new QuickMenuWingButton("Mods", "Mod Menus using PepsiLib", PepsiLibMod.RightWingMenu.Open, null,
                    false);

                foreach (var menu in PepsiLibMod.ModMenus)
                {
                    menu.MyLeftWingMenu = PepsiLibMod.LeftWingMenu.AddSubMenu($"{menu.MenuName}_Left",
                        $"Functions for {menu.MenuName}", menu.Logo);
                    menu.MyRightWingMenu = PepsiLibMod.RightWingMenu.AddSubMenu($"{menu.MenuName}_Right",
                        $"Functions for {menu.MenuName}", menu.Logo);
                    try
                    {
                        menu.OnWingMenuLeftInitialized();
                        menu.OnWingMenuRightInitialized();
                    }
                    catch (Exception e)
                    {
                        Error($"Failed to Initialize Wing Menus for {menu.MenuName}!");
                        Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                Error($"Failed to initialize Wing Menus! Exception: {e}");
            }
        }
    }
}
