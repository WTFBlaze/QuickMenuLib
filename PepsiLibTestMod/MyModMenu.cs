using MelonLoader;
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
            category.AddButton("Test", "This is a test using PepsiLib!", () =>
            {
                MelonLogger.Msg("Test Button!");
            });

            MyModMenu.AddSlider("FunnySlider", "Test", (num) => MelonLogger.Msg(num));
        }
        public override void OnWingMenuLeftInitialized()
        {
            MyLeftWingMenu.AddButton("TestButton", "Test", "Test using PepsiLib!", () =>
            {
                MelonLogger.Msg("Test Wing Button!");
            });
        }
        public override void OnTargetMenuInitialized()
        {
            MyTargetMenu.AddButton("Test", "Test using PepsiLib!", () =>
            {
                MelonLogger.Msg(SelectedUser.prop_String_0);
            });
        }
    }
}
