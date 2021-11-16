using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Object = UnityEngine.Object;

namespace PepsiLib.UI.Elements
{
    public class QuickMenuHeader : UIElement
    {
        private static GameObject HeaderTemplate => QuickMenuTemplates.GetHeaderTemplate();

        public QuickMenuHeader(string name, string title, Transform parent) : base(HeaderTemplate, (parent == null ? HeaderTemplate.transform.parent : parent), $"Header_{name}")
        {
            var tmp = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = title;
            tmp.richText = true;
        }
    }
    public class QuickMenuButtonContainer : UIElement
    {
        private static GameObject ButtonRowTemplate => QuickMenuTemplates.GetButtonRowTemplate();

        public QuickMenuButtonContainer(string name, Transform parent = null, bool vertical = false) : base(ButtonRowTemplate, (parent == null ? ButtonRowTemplate.transform.parent : parent), $"Buttons_{name}")
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

            if (vertical)
            {
                Object.DestroyImmediate(GameObject.GetComponent<GridLayoutGroup>());
                var vlg = ButtonRowTemplate.AddComponent<VerticalLayoutGroup>();
                vlg.childControlWidth = true;
            }
        }
    }

    public class QuickMenuCategory
    {
        public QuickMenuHeader MyHeader;
        public QuickMenuButtonContainer MyButtonContainer;

        private readonly List<QuickMenuPage> SubPages = new List<QuickMenuPage>();

        public string MyName { get; }

        public QuickMenuCategory(string name, string title, Transform parent = null, int? siblingIndex = null, bool vertical = false)
        {
            MyName = name;
            MyHeader = new QuickMenuHeader(name, title, parent);
            MyButtonContainer = new QuickMenuButtonContainer(name, parent, vertical);
            if (siblingIndex != null)
            {
                MyHeader.RectTransform.SetSiblingIndex(siblingIndex.Value);
                MyButtonContainer.RectTransform.SetSiblingIndex(siblingIndex.Value + 1);
            }
        }

        public QuickMenuButton AddButton(string name, string text, string tooltip, Action onClick, Sprite sprite = null)
        {
            var button = new QuickMenuButton(name, text, tooltip, onClick, MyButtonContainer.RectTransform, sprite);
            return button;
        }

        public QuickMenuToggleButton AddToggle(string name, string text, string tooltip, Action<bool> onToggle, bool defaultpos = false)
        {
            var toggle = new QuickMenuToggleButton(name, text, tooltip, onToggle, MyButtonContainer.RectTransform, defaultpos);
            return toggle;
        }

        public QuickMenuPage AddMenuPage(string name, string text, string tooltip = "", Sprite sprite = null, bool grid = false, bool button = true)
        {
            var menu = new QuickMenuPage(name, text, false, grid);
            if (button)
            {
                AddButton(name, text, string.IsNullOrEmpty(tooltip) ? $"Open the {text} menu" : tooltip, menu.Open, sprite);
            }
            SubPages.Add(menu);
            return menu;
        }

        public QuickMenuPage GetMenuPage(string name)
        {
            return SubPages.FirstOrDefault(m => m.Name == $"Menu_{name}");
        }
    }
}
