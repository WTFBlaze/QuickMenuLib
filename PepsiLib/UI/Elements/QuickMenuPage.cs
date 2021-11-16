using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

using Object = UnityEngine.Object;

namespace PepsiLib.UI.Elements
{
    public class QuickMenuPage : UIElement
    {

        private static GameObject MenuTemplate => QuickMenuTemplates.GetMenuTemplate();

        private static int SiblingIndex => QuickMenuTemplates.GetModalTemplate().transform.GetSiblingIndex();

        public event Action OnOpen;
        private readonly string MyName;
        private readonly bool IsRootPage;

        private readonly Transform MyContainer;

        public UIPage MyPage { get; }

        public QuickMenuPage(string name, string text, bool isRoot = false, bool grid = false) : base(MenuTemplate, MenuTemplate.transform.parent, $"Menu_{name}", false)
        {
            RectTransform.SetSiblingIndex(SiblingIndex);

            MyName = name;
            IsRootPage = isRoot;
            var headerTransform = RectTransform.GetChild(0);
            var titleText = headerTransform.GetComponentInChildren<TextMeshProUGUI>();
            titleText.text = text;
            titleText.richText = true;

            var backButton = headerTransform.GetComponentInChildren<Button>(true);
            backButton.gameObject.SetActive(true);

            var components = new Il2CppSystem.Collections.Generic.List<Behaviour>();
            backButton.GetComponents(components);

            var buttonContainer = RectTransform.Find("Scrollrect/Viewport/VerticalLayoutGroup/Buttons");
            foreach (var button in buttonContainer)
            {
                var control = button.Cast<Transform>();
                if (control == null)
                {
                    continue;
                }
                Object.Destroy(control.gameObject);
            }

            MyPage = GameObject.GetComponent<UIPage>();
            MyPage.name = $"QuickMenu{name}";
            MyPage.field_Public_String_0 = $"QuickMenu{name}";
            MyPage.field_Private_Boolean_1 = true;
            MyPage.field_Private_MenuStateController_0 = QuickMenuExtensions.MenuStateCtrl;
            MyPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            MyPage.field_Private_List_1_UIPage_0.Add(MyPage);


            var scrollRect = RectTransform.Find("Scrollrect").GetComponent<ScrollRect>();
            MyContainer = scrollRect.content;

            if (grid)
            {

                var gridLayoutGroup = MyContainer.Find("Buttons").GetComponent<GridLayoutGroup>();
                Object.DestroyImmediate(MyContainer.GetComponent<VerticalLayoutGroup>());
                var glp = MyContainer.gameObject.AddComponent<GridLayoutGroup>();
                glp.spacing = gridLayoutGroup.spacing;
                glp.cellSize = gridLayoutGroup.cellSize;
                glp.constraint = gridLayoutGroup.constraint;
                glp.constraintCount = gridLayoutGroup.constraintCount;
                glp.startAxis = gridLayoutGroup.startAxis;
                glp.startCorner = gridLayoutGroup.startCorner;
                glp.childAlignment = gridLayoutGroup.childAlignment;
                glp.padding = gridLayoutGroup.padding;
                glp.padding.top = 8;
            }


            Object.DestroyImmediate(MyContainer.Find("Buttons").gameObject);
            Object.DestroyImmediate(MyContainer.Find("Spacer_8pt").gameObject);


            var scrollbar = scrollRect.transform.Find("Scrollbar");
            scrollbar.gameObject.SetActive(true);

            scrollRect.enabled = true;
            scrollRect.verticalScrollbar = scrollbar.GetComponent<Scrollbar>();
            scrollRect.verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
            scrollRect.viewport.GetComponent<RectMask2D>().enabled = true;
            QuickMenuExtensions.MenuStateCtrl.field_Private_Dictionary_2_String_UIPage_0.Add(MyPage.field_Public_String_0, MyPage);

            if (isRoot)
            {
                backButton.gameObject.SetActive(false);
                var RootPages = QuickMenuExtensions.MenuStateCtrl.field_Public_ArrayOf_UIPage_0.ToList();
                RootPages.Add(MyPage);
                QuickMenuExtensions.MenuStateCtrl.field_Public_ArrayOf_UIPage_0 = RootPages.ToArray();
            }
        }

        public void Open()
        {
            if (IsRootPage)
            {
                QuickMenuExtensions.MenuStateCtrl.Method_Public_Void_String_UIContext_Boolean_Boolean_0($"QuickMenu{MyName}");
            }
            else
            {
                QuickMenuExtensions.MenuStateCtrl.Method_Public_Void_String_UIContext_Boolean_0($"QuickMenu{MyName}");
            }

            OnOpen?.Invoke();
        }

        public QuickMenuButton AddButton(string name, string text, string tooltip, Action onClick, Sprite image = null)
        {
            return new QuickMenuButton(name, text, tooltip, onClick, MyContainer, image);
        }

        public QuickMenuSlider AddSlider(string name, string text, Action<float> onValueChanged, float minValue = 0, float maxValue = 1)
        {
            return new QuickMenuSlider(name, text, onValueChanged, MyContainer, minValue, maxValue);
        }

        public QuickMenuToggleButton AddToggle(string name, string text, string tooltip, Action<bool> onToggle, bool defaultValue = false)
        {
            return new QuickMenuToggleButton(name, text, tooltip, onToggle, MyContainer, defaultValue);
        }

        public QuickMenuPage AddSubMenu(string name, string text, string tooltip, bool grid = true, Sprite image = null)
        {
            var menu = new QuickMenuPage(name, text, false, grid);
            AddButton($"{name}Menu", text, tooltip, menu.Open, image);
            return menu;
        }

        public QuickMenuCategory AddMenuCategory(string name, string text)
        {
            return new QuickMenuCategory(name, text, MyContainer, null);
        }
    }
}
