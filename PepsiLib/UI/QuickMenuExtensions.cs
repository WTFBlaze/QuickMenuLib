using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

using NewQuickMenu = VRC.UI.Elements.QuickMenu;

namespace PepsiLib.UI
{
    public static class QuickMenuExtensions
    {
        private static NewQuickMenu _quickMenuInstance;

        public static NewQuickMenu Instance
        {
            get
            {
                _quickMenuInstance = Resources.FindObjectsOfTypeAll<NewQuickMenu>()[0];
                return _quickMenuInstance;
            }
        }

        public static void ShowAlert(this NewQuickMenu menu, string message)
        {
            menu.Method_Public_Virtual_Final_New_Void_String_2(message);
        }

        public static MenuStateController MenuStateCtrl => Instance.field_Protected_MenuStateController_0;

        private static Wing[] _wings;
        private static Wing _leftWing;
        private static Wing _rightWing;

        public static Wing[] Wings
        {
            get
            {
                _wings = Resources.FindObjectsOfTypeAll<Wing>();

                return _wings;
            }
        }

        public static Wing LeftWing
        {
            get
            {
                _leftWing = Wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Left);
                return _leftWing;
            }
        }

        public static Wing RightWing
        {
            get
            {
                _rightWing = Wings.FirstOrDefault(w => w.field_Public_WingPanel_0 == Wing.WingPanel.Right);
                return _rightWing;
            }
        }
    }

    internal class QuickMenuTemplates
    {
        private static GameObject pageButtonReference;
        private static GameObject headerReference;
        private static GameObject buttonRowReference;
        private static GameObject spacersReference;
        private static GameObject singleButtonReference;
        private static GameObject toggleButtonReference;
        private static GameObject nestedMenuReference;
        private static GameObject MenuReference;
        private static GameObject modalReference;
        private static GameObject WingMenuReference;
        private static GameObject WingButtonReference;
        private static Sprite ToggleOnReference;
        private static GameObject SliderReference;

        internal static GameObject GetSliderPrefab()
        {
            if (SliderReference == null)
            {
                SliderReference = QuickMenuExtensions.Instance.GetComponentsInChildren<Slider>(true).ToList().Find(s => s.transform.parent.name == "VolumeSlider_Master").transform.parent.gameObject;
            }
            return SliderReference;
        }

        internal static GameObject GetWingMenuTemplate()
        {
            if (WingMenuReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.LeftWing.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "WingMenu")
                    {
                        WingMenuReference = button.gameObject;

                        break;
                    }
                };
            }
            return WingMenuReference;
        }

        internal static GameObject GetWingButtonTemplate()
        {
            if (WingButtonReference == null)
            {
                Button[] buttons = QuickMenuExtensions.LeftWing.GetComponentsInChildren<Button>(true);

                foreach (Button button in buttons)
                {
                    if (button.name == "Button_Profile")
                    {
                        WingButtonReference = button.gameObject;

                        break;
                    }
                };
            }
            return WingButtonReference;
        }

        internal static GameObject GetPageButtonTemplate()
        {
            if (pageButtonReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Page_Settings")
                    {
                        pageButtonReference = button.gameObject;

                        break;
                    }
                };
            }

            return pageButtonReference;
        }

        internal static GameObject GetHeaderTemplate()
        {
            if (headerReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Header_QuickActions")
                    {
                        headerReference = button.gameObject;

                        break;
                    }
                };
            }

            return headerReference;
        }

        internal static GameObject GetButtonRowTemplate()
        {
            if (buttonRowReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Buttons_QuickActions")
                    {
                        buttonRowReference = button.gameObject;

                        break;
                    }
                };
            }

            return buttonRowReference;
        }

        internal static GameObject GetSpacersTemplate()
        {
            if (spacersReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Spacer_8pt")
                    {
                        spacersReference = button.gameObject;

                        break;
                    }
                };
            }

            return spacersReference;
        }

        internal static GameObject GetSingleButtonTemplate()
        {
            if (singleButtonReference == null)
            {
                Button[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Button>(true);

                foreach (Button button in buttons)
                {
                    if (button.name == "Button_Worlds")
                    {
                        singleButtonReference = button.gameObject;

                        break;
                    }
                };
            }

            return singleButtonReference;
        }

        internal static GameObject GetToggleButtonTemplate()
        {
            if (toggleButtonReference == null)
            {
                Toggle[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Toggle>(true);

                foreach (Toggle button in buttons)
                {
                    if (button.name == "Button_ToggleTooltips")
                    {
                        toggleButtonReference = button.gameObject;

                        break;
                    }
                };
            }

            return toggleButtonReference;
        }

        internal static Sprite GetToggleOnIconTemplate()
        {
            if (ToggleOnReference == null)
            {
                Image[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Image>(true);

                foreach (Image button in buttons)
                {
                    if (button.transform.parent.name == "Panel_NoNotifications_Message")
                    {
                        ToggleOnReference = button.sprite;

                        break;
                    }
                };
            }

            return ToggleOnReference;
        }

        internal static GameObject GetNestedMenuTemplate()
        {
            if (nestedMenuReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Menu_DevTools")
                    {
                        nestedMenuReference = button.gameObject;

                        break;
                    }
                };
            }

            return nestedMenuReference;
        }

        internal static GameObject GetMenuTemplate()
        {
            if (MenuReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Menu_DevTools")
                    {
                        MenuReference = button.gameObject;

                        break;
                    }
                };
            }

            return MenuReference;
        }

        internal static GameObject GetModalTemplate()
        {
            if (modalReference == null)
            {
                Transform[] buttons = QuickMenuExtensions.Instance.GetComponentsInChildren<Transform>(true);

                foreach (Transform button in buttons)
                {
                    if (button.name == "Modal_AddMessage")
                    {
                        modalReference = button.gameObject;

                        break;
                    }
                };
            }

            return modalReference;
        }
    }
}
