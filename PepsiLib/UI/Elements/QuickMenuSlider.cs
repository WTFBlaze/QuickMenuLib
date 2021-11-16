using PepsiLib.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PepsiLib.UI.Elements
{
    public class QuickMenuSlider : UIElement
    {

        private static GameObject SliderTemplate => QuickMenuTemplates.GetSliderPrefab();

        private readonly TextMeshProUGUI MyText;

        public string Text
        {
            get => MyText.text;
            set => MyText.SetText(value);
        }

        private readonly Slider MySlider;

        public QuickMenuSlider(string name, string text, Action<float> onValueChanged, Transform parent, float minValue = 0, float maxValue = 1) : base(SliderTemplate, parent, $"Slider_{name}")
        {
            MyText = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            MyText.text = text;
            MyText.richText = true;

            MySlider = GameObject.GetComponentInChildren<Slider>();
            MySlider.minValue = minValue;
            MySlider.maxValue = maxValue;
            MySlider.onValueChanged = new Slider.SliderEvent();
            MySlider.onValueChanged.AddListener(new Action<float>(onValueChanged));
        }
    }
}
