using System;
using System.Linq;
using System.Reflection;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.Core;
using VRC.DataModel;
using static MelonLoader.MelonLogger;

namespace PepsiLib
{
    public static class Utils
    {
        /// <summary>
        /// This Method Removes the VRCPlus Banner on the Home Page of the QuickMenu. Credit to tetra-fox
        /// https://github.com/tetra-fox/VRCMods/blob/master/AdBlocker/AdBlockerMod.cs
        /// </summary>
        public static void RemoveVrcPlus()
        {
            try
            {
                GameObject carousel = FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners");
                GameObject vrcPlus = FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/VRC+_Banners");

                GameObject.DestroyImmediate(carousel);
                GameObject.DestroyImmediate(vrcPlus);
            }
            catch (Exception e) 
            { 
                //A Failure here is not catastrophic. Other Mods may completely remove the Carousel Banners before this method gets called by a ModMenu.
            }
        }
        
        // https://github.com/knah/
        public static GameObject? FindInactive(string path)
        {
            var split = path.Split(new char[]{'/'}, 2);
            var rootObject = GameObject.Find($"/{split[0]}")?.transform;
            if (rootObject == null) return null;
            return Transform.FindRelativeTransformWithPath(rootObject, split[1], false)?.gameObject;
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
