using PepsiLib.UI;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

namespace PepsiLib
{
    public static class Utils
    {
        public static void RemoveVRCPlus()
        {
            try
            {
                var dashboard = QuickMenuExtensions.Instance.transform.Find("Container/Window/QMParent/Menu_Dashboard").GetComponent<UIPage>();
                var dashboardScrollrect = dashboard.GetComponentInChildren<ScrollRect>();
                var dashboardScrollbar = dashboardScrollrect.transform.Find("Scrollbar").GetComponent<Scrollbar>();
                var dashboardContent = dashboardScrollrect.content;
                dashboardContent.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
                dashboardContent.Find("Carousel_Banners").gameObject.SetActive(false);
            }
            catch (Exception e) { }
        }

    }

    //https://github.com/RequiDev/ReMod.Core/blob/1d00b095a1ab8255fb58b1df53df216ea24d4b15/Unity/EnableDisableListener.cs
    public class EnableDisableListener : MonoBehaviour
    {
        [method: HideFromIl2Cpp]
        public event Action OnEnableEvent;
        [method: HideFromIl2Cpp]
        public event Action OnDisableEvent;

        public EnableDisableListener(IntPtr obj) : base(obj) { }
        public void OnEnable() => OnEnableEvent?.Invoke();
        public void OnDisable() => OnDisableEvent?.Invoke();
    }
}
