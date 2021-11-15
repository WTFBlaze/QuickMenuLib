using PepsiLib.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;
using Object = UnityEngine.Object;

namespace RinButtonAPI
{
    public class QuickMenuWingMenu : UIElement
    {
        private static GameObject WingMenuTemplate => QuickMenuTemplates.GetWingMenuTemplate();

        private readonly Wing MyWing;
        private readonly string MyName;
        private readonly Transform MyContainer;

        public QuickMenuWingMenu(string name, string text, bool left = true) : base(WingMenuTemplate, (left ? QuickMenuExtensions.LeftWing : QuickMenuExtensions.RightWing).field_Public_RectTransform_0, name, false)
        {
            MyName = name;
            MyWing = left ? QuickMenuExtensions.LeftWing : QuickMenuExtensions.RightWing;

            var headerTransform = RectTransform.GetChild(0);
            var titleText = headerTransform.GetComponentInChildren<TextMeshProUGUI>();
            titleText.text = text;
            titleText.richText = true;

            var backButton = headerTransform.GetComponentInChildren<Button>(true);
            backButton.gameObject.SetActive(true);

            var backIcon = backButton.transform.Find("Icon");
            backIcon.gameObject.SetActive(true);
            var components = new Il2CppSystem.Collections.Generic.List<Behaviour>();
            backButton.GetComponents(components);

            foreach (var comp in components)
            {
                comp.enabled = true;
            }

            var content = RectTransform.GetComponentInChildren<ScrollRect>().content;
            foreach (var obj in content)
            {
                var control = obj.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }

                Object.Destroy(control.gameObject);
            }

            MyContainer = content;

            var uiPage = GameObject.GetComponent<UIPage>();
            uiPage.name = name;
            uiPage.field_Public_String_0 = name;
            uiPage.field_Public_Boolean_0 = true;
            uiPage.field_Private_MenuStateController_0 = MyWing.field_Private_MenuStateController_0;
            uiPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            uiPage.field_Private_List_1_UIPage_0.Add(uiPage);

            MyWing.field_Private_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(uiPage.field_Public_String_0, uiPage);
        }

        public void Open()
        {
            MyWing.field_Private_MenuStateController_0.Method_Public_Void_String_UIContext_Boolean_0(MyName);
        }

        public QuickMenuWingButton AddButton(string name, string text, string tooltip, Action onClick, Sprite sprite = null, bool arrow = true, bool background = true, bool seperator = false)
        {
            return new QuickMenuWingButton(name, text, tooltip, onClick, MyContainer, sprite, arrow, background, seperator);
        }

        public QuickMenuWingMenu AddSubMenu(string name, string text, string tooltip, Sprite image = null)
        {
            var menu = new QuickMenuWingMenu(name, text, MyWing.field_Public_WingPanel_0 == Wing.WingPanel.Left);
            AddButton(name, text, tooltip, menu.Open, image);
            return menu;
        }
    }

    public class QuickMenuWingButton : UIElement
    {
        private static GameObject WingButtonTemplate => QuickMenuTemplates.GetWingButtonTemplate();

        public QuickMenuWingButton(string name, string text, string tooltip, Action onClick, Sprite sprite = null, bool left = true, bool arrow = true, bool background = true,
            bool seperator = false) : base(WingButtonTemplate, (left ? QuickMenuExtensions.LeftWing : QuickMenuExtensions.RightWing).field_Public_RectTransform_0.Find("WingMenu/ScrollRect/Viewport/VerticalLayoutGroup"), $"Button_{name}")
        {
            var container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(background);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            RectTransform.Find("Separator").gameObject.SetActive(seperator);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            if (sprite != null)
            {
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
            }
            else
            {
                iconImage.gameObject.SetActive(false);
            }

            var tmp = container.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
            tmp.richText = true;

            var button = GameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(onClick));

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;
        }

        public QuickMenuWingButton(string name, string text, string tooltip, Action onClick, Transform parent, Sprite sprite = null, bool arrow = true, bool background = true,
            bool seperator = false) : base(WingButtonTemplate, parent, $"Button_{name}")
        {
            var container = RectTransform.Find("Container").transform;
            container.Find("Background").gameObject.SetActive(background);
            container.Find("Icon_Arrow").gameObject.SetActive(arrow);
            RectTransform.Find("Separator").gameObject.SetActive(seperator);
            var iconImage = container.Find("Icon").GetComponent<Image>();
            if (sprite != null)
            {
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
            }
            else
            {
                iconImage.gameObject.SetActive(false);
            }

            var tmp = container.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
            tmp.richText = true;

            var button = GameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(onClick));

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;
        }
    }
}
