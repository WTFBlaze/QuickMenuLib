using PepsiLib.UI;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements;

using static MelonLoader.MelonLogger;

namespace PepsiLib
{
    public static class Utils
    {
        /// <summary>
        /// This Method Removes the VRCPlus Banner on the Home Page of the QuickMenu.
        /// </summary>
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
            catch (Exception e) 
            { 
                //A Failure here is not catastrophic. Other Mods may completely remove the Carousel Banners before this method gets called by a ModMenu.
            }
        }

    }

    //https://github.com/RequiDev/ReMod.Core/blob/1d00b095a1ab8255fb58b1df53df216ea24d4b15/Unity/EnableDisableListener.cs
    internal class EnableDisableListener : MonoBehaviour
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
