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
    /// <summary>
    /// https://github.com/loukylor/VRC-Mods/blob/main/VRChatUtilityKit/Utilities/XrefUtils.cs
    /// </summary>
    internal static class XrefUtils
    {
        /// <summary>
        /// Checks the methods the given method is used by.
        /// Note: the methods passed into the predicate may be false.
        /// </summary>
        /// <param name="method">The method to check</param>
        /// <param name="predicate">The predicate to check the methods against</param>
        /// <returns>true if the predicate returned true any times, otherwise false</returns>
        internal static bool CheckUsedBy(MethodBase method, Func<MethodBase, bool> predicate)
        {
            foreach (XrefInstance instance in XrefScanner.UsedBy(method))
                if (instance.Type == XrefType.Method && predicate.Invoke(instance.TryResolve()))
                    return true;
            return false;
        }
        
        public static bool CheckUsedBy(MethodBase method, string methodName, Type type = null)
            => CheckUsedBy(method, usedByMethod => usedByMethod != null && (type == null || usedByMethod.DeclaringType == type) && usedByMethod.Name.Contains(methodName));
    }
    public static class Utils
    {
        internal static object _selectedUserManagerObject;
        internal static MethodInfo _selectUserMethod;
        internal static PropertyInfo _activeUserInUserSelectMenuField;

        internal static APIUser GetSelectedUser()
        {
            if(_selectedUserManagerObject == null)  _selectedUserManagerObject = GameObject.Find("_Application/UIManager/SelectedUserManager").GetComponent<UserSelectionManager>();

            if(_selectUserMethod == null) _selectUserMethod = typeof(UserSelectionManager).GetMethods()
                .First(method => method.Name.StartsWith("Method_Public_Void_APIUser_") && !method.Name.Contains("_PDM_") && XrefUtils.CheckUsedBy(method, "Method_Public_Virtual_Final_New_Void_IUser_"));
            if(_activeUserInUserSelectMenuField == null) _activeUserInUserSelectMenuField = typeof(UserSelectionManager).GetProperty("field_Private_APIUser_1");
            return (APIUser)_activeUserInUserSelectMenuField.GetValue(_selectedUserManagerObject);
            
        }
        
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
