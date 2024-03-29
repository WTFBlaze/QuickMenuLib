﻿using MelonLoader;
using PepsiLib;
using VRC.Core;

namespace PepsiLibTestMod
{
    public class MyModMenu : ModMenu
    {
        public override string MenuName => "TestModMenu";
        public override void OnQuickMenuInitialized()
        {
            var category = MyModMenu.AddMenuCategory("TestModMenuCat", "Testing Stuff");
            category.AddButton("Test", "This is a test using QuickMenuLib!", () =>
            {
                MelonLogger.Msg("Test Button!");
            });

            MyModMenu.AddSlider("FunnySlider", "Test", (num) => MelonLogger.Msg(num));
        }
        public override void OnWingMenuLeftInitialized()
        {
            MyLeftWingMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
            {
                MelonLogger.Msg("Test Wing Button!");
            });
        } 
        public override void OnWingMenuRightInitialized()
        {
            MyRightWingMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
            {
                MelonLogger.Msg("Test Wing Button!");
            });
        }
        public override void OnTargetMenuInitialized()
        {
            MyTargetMenu.AddButton("Test", "Test using QuickMenuLib!", () =>
            {
                MelonLogger.Msg(SelectedUser.prop_String_0);
            });
        }
    }
}
