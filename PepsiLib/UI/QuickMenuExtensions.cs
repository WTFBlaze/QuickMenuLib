using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using VRC.UI.Elements.Menus;
using NewQuickMenu = VRC.UI.Elements.QuickMenu;

namespace PepsiLib.UI
{
    public static class QuickMenuExtensions
    {
        private static NewQuickMenu _quickMenuInstance;

        public static NewQuickMenu GetQuickMenu
        {
            get
            {
                if(_quickMenuInstance == null)
                {
                    _quickMenuInstance = Utils.FindInactive("UserInterface/Canvas_QuickMenu(Clone)").GetComponent<NewQuickMenu>();
                }
                return _quickMenuInstance;
            }
        }

        private static MenuStateController _menuStateController;
        public static MenuStateController MenuStateController => GetQuickMenu.prop_MenuStateController_0;

        private static SelectedUserMenuQM _selectedUserMenuQM;

        public static SelectedUserMenuQM SelectedUserMenu
        {
            get
            {
                if(_selectedUserMenuQM == null)
                {
                    _selectedUserMenuQM = GetQuickMenu.field_Public_Transform_0.Find("Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
                }

                return _selectedUserMenuQM;
            }
        }

        private static Wing[] _wings;
        private static Wing _leftWing;
        private static Wing _rightWing;

        public static Wing[] Wings
        {
            get
            {
                if (_wings == null || _wings.Length == 0)
                {
                    _wings = GameObject.Find("UserInterface").GetComponentsInChildren<Wing>(true);
                }

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
        private static Sprite toggleOnReference;
        private static GameObject SliderReference;

        internal static GameObject GetSliderPrefab()
        {
            if (SliderReference == null)
            {
                SliderReference = QuickMenuExtensions.GetQuickMenu.GetComponentsInChildren<Slider>(true).ToList().Find(s => s.transform.parent.name == "VolumeSlider_Master").transform.parent.gameObject;
            }
            return SliderReference;
        }

        internal static GameObject GetWingMenuTemplate()
        {
            if (WingMenuReference == null)
            {
                WingMenuReference = QuickMenuExtensions.LeftWing.field_Public_RectTransform_0.Find("WingMenu").gameObject;
            }
            return WingMenuReference;
        }

        internal static GameObject GetWingButtonTemplate()
        {
            if (WingButtonReference == null)
            {
                WingButtonReference = QuickMenuExtensions.LeftWing.transform.Find("Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Profile").gameObject;
            }
            return WingButtonReference;
        }

        internal static GameObject GetPageButtonTemplate()
        {
            if (pageButtonReference == null)
            {
                pageButtonReference =
                    Utils.FindInactive(
                        "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings");
            }

            return pageButtonReference;
        }

        internal static GameObject GetHeaderTemplate()
        {
            if (headerReference == null)
            {
                headerReference =
                    Utils.FindInactive(
                        "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions");
            }

            return headerReference;
        }

        internal static GameObject GetButtonRowTemplate()
        {
            if (buttonRowReference == null)
            {
                buttonRowReference = 
                    Utils.FindInactive(
                    "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions");
            }

            return buttonRowReference;
        }

        internal static GameObject GetSingleButtonTemplate()
        {
            if (singleButtonReference == null)
            {
                singleButtonReference = Utils.FindInactive(
                    "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn");
            }

            return singleButtonReference;
        }

        internal static GameObject GetToggleButtonTemplate()
        {
            if (toggleButtonReference == null)
            {
                toggleButtonReference =
                    Utils.FindInactive(
                        "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo");
            }

            return toggleButtonReference;
        }

        internal static Sprite GetToggleOnIconTemplate()
        {
            if (toggleOnReference == null)
            {
                toggleOnReference = Utils.FindInactive(
                    "UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<Image>().sprite;
            }

            return toggleOnReference;
        }

        internal static GameObject GetMenuTemplate()
        {
            if (MenuReference == null)
            {
                MenuReference = Utils.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_DevTools");
            }

            return MenuReference;
        }

        internal static GameObject GetModalTemplate()
        {
            if (modalReference == null)
            {
                modalReference = Utils.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Modal_AddMessage");
            }

            return modalReference;
        }
    }
}
