# PepsiLib
A QuickMenu API inspired by UiExpansionKit, and partially based on ReModCE's first API

# QuickStart

Create a Class that Inherits from ModMenu.
```cs
using MelonLoader;
using PepsiLib;

namespace PepsiLibTestMod
{
    public class MyModMenu : ModMenu
    {
        public override string MenuName => "TestModMenu"; //You have to change this name. It'll conflict with other Mod's pages
        
        //This Method gets called when the Main Page of the QuickMenu initializes.
        public override void OnQuickMenuInitialized()
        {
            var category = MyModMenu.AddMenuCategory("TestModMenuCat", "Testing Stuff"); //Every Mod Menu gets its own pages.
            category.AddButton("TestButton", "Test", "This is a test using PepsiLib!", () =>
            {
                MelonLogger.Msg("Test Button!");
            });

            MyModMenu.AddSlider("FunnySlider", "Test", (num) => MelonLogger.Msg(num));
        }
        
        //This Method gets called when the Left Wing Menu Initializes.
        public override void OnWingMenuLeftInitialized()
        {
            MyLeftWingMenu.AddButton("TestButton", "Test", "Test using PepsiLib!", () =>
            {
                MelonLogger.Msg("Test Wing Button!");
            });
        }
        
        //This Method gets called when the Selected User Menu Initializes.
        public override void OnTargetMenuInitialized()
        {
            MyTargetMenu.AddButton("TestButton", "Test", "Test using PepsiLib!", () =>
            {
                MelonLogger.Msg(SelectedUser.prop_String_0); //PepsiLib runs a Coroutine and provides the Selected IUser for every Mod Menu.
            });
        }
    }
}
```

Then simply register your custom Mod Menu Class:
```cs
        public override void OnApplicationStart()
        {
            PepsiLib.PepsiLibMod.RegisterModMenu(new MyModMenu());
        }
```

A Look at what PepsiLib provides your Menu with, by looking at the ModMenu Class.
```cs
using PepsiLib.UI.Elements;
using VRC.DataModel;

namespace PepsiLib
{
    /// <summary>
    /// The ModMenu Class.
    /// </summary>
    public class ModMenu
    {
        /// <summary>
        /// The name of your Menu. This has to be changed.
        /// </summary>
        public virtual string MenuName => "Default Menu";

        public QuickMenuPage MyModMenu = null;
        public QuickMenuWingMenu MyLeftWingMenu = null;
        public QuickMenuWingMenu MyRightWingMenu = null;
        public QuickMenuPage MyTargetMenu = null;
        public IUser SelectedUser = null;

        public virtual void OnQuickMenuInitialized() { }
        public virtual void OnWingMenuLeftInitialized() { }
        public virtual void OnWingMenuRightInitialized() { }
        public virtual void OnTargetMenuInitialized() { }  
    }
}
```

The Pages PepsiLib creates for itself are exposed to your Mod as well. 
```cs
        //We expose these to the Mods in case they want to add content to these pages. Unlikely but it can't hurt.
        public static QuickMenuPage MainMenu = null;
        public static QuickMenuPage TargetMenu = null;
        public static QuickMenuWingMenu LeftWingMenu = null;
        public static QuickMenuWingMenu RightWingMenu = null;
```

# Planned Features
* Proper Documentation Page
* Support for Client Logos to either:
    - Have Logo + text
    - Replace the button with the Logo completely

# Credit
The way we add controls is based on how RequiDev does it in [ReModCE](https://github.com/RequiDev/ReModCE) <br>
This Project is inspired by Knah's [UiExpansionKit](https://github.com/knah/vrcmods)
