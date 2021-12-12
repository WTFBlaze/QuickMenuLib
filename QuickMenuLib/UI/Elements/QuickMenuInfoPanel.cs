using QuickMenuLib;
using QuickMenuLib.UI;
using UnityEngine;
using UnityEngine.UI;

namespace PepsiLib.UI.Elements
{
    public class QuickMenuInfoPanel : UIElement
    {
        private static GameObject PanelTemplate => QuickMenuTemplates.GetInfoPanelTemplate();
        private readonly Text InfoText;
        private readonly Image InfoBackground;

        public QuickMenuInfoPanel(Transform location, float PanelHeight, string PanelText) : base(PanelTemplate, location, $"Info_Panel_{Utils.RandomNumbers()}")
        {
            InfoText = GameObject.GetComponentInChildren<Text>();
            InfoBackground = GameObject.GetComponentInChildren<Image>();
            SetSize(new Vector2(0, PanelHeight));
            SetText(PanelText);
        }
        
        public void SetSize(Vector2 size)
        {
            GameObject.GetComponent<RectTransform>().sizeDelta = size;
            GameObject.transform.Find("Text").GetComponent<RectTransform>().sizeDelta = new Vector2(-25, 0);
        }

        public void SetTextColor(Color newColor)
        {
            InfoText.color = newColor;
        }

        public void SetFontSize(int newFontSize)
        {
            InfoText.fontSize = newFontSize;
        }
        
        public void SetText(string text)
        {
            InfoText.text = text;
        }

        public void SetBackgroundImage(Sprite newBackgroundImage)
        {
            GameObject.GetComponentInChildren<Image>().sprite = newBackgroundImage;
            GameObject.GetComponentInChildren<Image>().overrideSprite = newBackgroundImage;
        }
        
        public void ToggleBackground(bool state)
        {
            GameObject.GetComponentInChildren<Image>().enabled = state;
        }

        public void SetActive(bool state)
        {
            GameObject.SetActive(state);
        }
    }
}