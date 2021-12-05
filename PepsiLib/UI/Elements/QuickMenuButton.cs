using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

using Object = UnityEngine.Object;

namespace PepsiLib.UI.Elements
{
    /// <summary>
    /// Credit to https://github.com/RequiDev/RemodCE
    /// </summary>
    public class QuickMenuButton : UIElement
    {
        private static GameObject ButtonTemplate => QuickMenuTemplates.GetSingleButtonTemplate();

        private readonly TextMeshProUGUI MyText;

        private readonly Button MyButton;

        public QuickMenuButton( string text, string tooltip, Action onClick, Transform parent, Sprite sprite = null) : base(ButtonTemplate, parent,
            $"Button_{text}")
        {
            MyText = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            MyText.text = text;
            MyText.richText = true;

            if (sprite == null)
            {
                //_text.fontSize = 35;
                MyText.enableAutoSizing = true;
                MyText.color = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                MyText.m_fontColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                MyText.m_htmlColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                MyText.transform.localPosition = new Vector3(MyText.transform.localPosition.x, -30f);

                var layoutElement = RectTransform.Find("Background").gameObject.AddComponent<LayoutElement>();
                layoutElement.ignoreLayout = true;

                var horizontalLayout = GameObject.AddComponent<HorizontalLayoutGroup>();
                horizontalLayout.padding.right = 10;
                horizontalLayout.padding.left = 10;
                Object.DestroyImmediate(RectTransform.Find("Icon").gameObject);
            }
            else
            {
                var iconImage = RectTransform.Find("Icon").GetComponent<Image>();
                iconImage.sprite = sprite;
                iconImage.overrideSprite = sprite;
            }

            Object.DestroyImmediate(RectTransform.Find("Icon_Secondary").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_Close").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_MMJump").gameObject);

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;

            MyButton = GameObject.GetComponent<Button>();
            MyButton.onClick = new Button.ButtonClickedEvent();
            MyButton.onClick.AddListener(new Action(onClick));
        }
    }

    public class QuickMenuToggleButton : UIElement
    {
        private static GameObject ToggleButtonTemplate => QuickMenuTemplates.GetToggleButtonTemplate();



        private static Sprite OnIconSprite => QuickMenuTemplates.GetToggleOnIconTemplate();

        private readonly Toggle MyToggle;
        private readonly ToggleIcon MyToggleIcon;

        private bool LastValue;

        public QuickMenuToggleButton(string text, string tooltip, Action<bool> onToggle, Transform parent, bool defaultValue = false) : base(ToggleButtonTemplate, parent, $"Button_Toggle{text}")
        {
            var iconOn = RectTransform.Find("Icon_On").GetComponent<Image>();
            iconOn.sprite = OnIconSprite;

            MyToggleIcon = GameObject.GetComponent<ToggleIcon>();

            MyToggle = GameObject.GetComponent<Toggle>();
            MyToggle.onValueChanged = new Toggle.ToggleEvent();
            MyToggle.onValueChanged.AddListener(new Action<bool>(MyToggleIcon.Method_Private_Void_Boolean_PDM_0));
            MyToggle.onValueChanged.AddListener(new Action<bool>(onToggle));

            var tmp = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = text;
            tmp.richText = true;
            tmp.color = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
            tmp.m_fontColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
            tmp.m_htmlColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiToggleTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;

            Toggle(defaultValue);

            var edl = GameObject.AddComponent<EnableDisableListener>();
            edl.OnEnableEvent += UpdateToggle;
        }


        public void Toggle(bool value, bool callback = true)
        {
            LastValue = value;
            MyToggle.Set(value, callback);
        }

        private void UpdateToggle()
        {
            MyToggleIcon.Method_Private_Void_Boolean_PDM_0(LastValue);
        }
    }
}
