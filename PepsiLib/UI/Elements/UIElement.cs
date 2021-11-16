using UnityEngine;

namespace PepsiLib
{

    /// <summary>
    /// The UIElement class provides a great template for Menu Controls.
    /// Credit to https://github.com/RequiDev/RemodCE
    /// </summary>
    public class UIElement
    {
        public string Name { get; }
        public GameObject GameObject { get; }
        public RectTransform RectTransform { get; }

        public UIElement(GameObject original, Transform parent, string name, bool defaultState = true)
        {
            GameObject = Object.Instantiate(original, parent);
            GameObject.name = name;
            Name = GameObject.name;

            GameObject.SetActive(defaultState);
            RectTransform = GameObject.GetComponent<RectTransform>();
        }
    }
}
