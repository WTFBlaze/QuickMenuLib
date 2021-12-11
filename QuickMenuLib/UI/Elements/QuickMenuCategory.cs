
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Object = UnityEngine.Object;

namespace QuickMenuLib.UI.Elements
{
    /// <summary>
    /// Credit to https://github.com/RequiDev/RemodCE
    /// </summary>
    public class QuickMenuHeader : UIElement
    {
        private static GameObject HeaderTemplate(bool expandable) => expandable ? QuickMenuTemplates.GetFoldoutHeaderTemplate() : QuickMenuTemplates.GetHeaderTemplate();

        public QuickMenuHeader(string title, Transform parent, bool expandable = false) : base(HeaderTemplate(expandable), (parent == null ? HeaderTemplate(expandable).transform.parent : parent), expandable ? $"QM_Foldout_{title}" : $"Header_{title}")
        {
            var tmp = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = title;
            tmp.richText = true;

            if (expandable)
            {
                GameObject.GetComponentInChildren<Toggle>().onValueChanged = new Toggle.ToggleEvent();
            }
            
        }
    }
    public class QuickMenuButtonContainer : UIElement
    {
        private static GameObject ButtonRowTemplate => QuickMenuTemplates.GetButtonRowTemplate();

        public QuickMenuButtonContainer(string name, Transform parent = null) : base(ButtonRowTemplate, (parent == null ? ButtonRowTemplate.transform.parent : parent), $"Buttons_{name}")
        {
            foreach (var button in RectTransform)
            {
                var control = button.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }
                Object.Destroy(control.gameObject);
            }

            var gridLayout = GameObject.GetComponent<GridLayoutGroup>();

            gridLayout.padding.top = 8;
            gridLayout.padding.left = 64;
        }
    }

    public class QuickMenuCategory
    {
        public QuickMenuHeader MyHeader;
        public QuickMenuButtonContainer MyButtonContainer;

        private readonly List<QuickMenuPage> SubPages = new List<QuickMenuPage>();

        public QuickMenuCategory(string title, Transform parent = null, bool expandable = false, int? siblingIndex = null)
        {
            MyHeader = new QuickMenuHeader(title, parent, expandable);
            MyButtonContainer = new QuickMenuButtonContainer(title, parent);

            if (expandable)
            {
                var toggle = MyHeader.GameObject.GetComponentInChildren<Toggle>();
                toggle.onValueChanged.AddListener(new Action<bool>(b =>
                {
                    MyButtonContainer.GameObject.SetActive(b);
                }));
            }
            
            if (siblingIndex != null)
            {
                MyHeader.RectTransform.SetSiblingIndex(siblingIndex.Value);
                MyButtonContainer.RectTransform.SetSiblingIndex(siblingIndex.Value + 1);
            }
        }

        public QuickMenuButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null)
        {
            var button = new QuickMenuButton(text, tooltip, onClick, MyButtonContainer.RectTransform, sprite);
            return button;
        }

        public QuickMenuToggleButton AddToggle(string text, string tooltip, System.Action<bool> onToggle, bool defaultPosition = false)
        {
            var toggle = new QuickMenuToggleButton(text, tooltip, onToggle, MyButtonContainer.RectTransform, defaultPosition);
            return toggle;
        }

        public QuickMenuPage AddMenuPage(string text, string tooltip = "", Sprite sprite = null, bool grid = false, bool button = true)
        {
            var menu = new QuickMenuPage(text, false, grid);
            if (button)
            {
                AddButton(text, string.IsNullOrEmpty(tooltip) ? $"Open the {text} menu" : tooltip, menu.Open, sprite);
            }
            SubPages.Add(menu);
            return menu;
        }
    }
}
