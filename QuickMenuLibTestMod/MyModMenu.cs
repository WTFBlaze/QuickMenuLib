using MelonLoader;
using QuickMenuLib;
using VRC.Core;

namespace PepsiLibTestMod
{
    public class MyModMenu : ModMenu
    {
        public override string MenuName => "TestModMenu";
        public override void OnQuickMenuInitialized()
        {
            var category = MyModMenu.AddMenuCategory("TestModMenuCat");
            category.AddButton("Test", "This is a test using QuickMenuLib!", () =>
            {
                MelonLogger.Msg("Test Button!");
            });

            MyModMenu.AddSlider("FunnySlider", "Slider Test", (num) => MelonLogger.Msg(num));
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
