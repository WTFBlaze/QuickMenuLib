using RinButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC.DataModel;

namespace PepsiLib
{
    /// <summary>
    /// The ModMenu Class.
    /// </summary>
    public class ModMenu
    {
        /// <summary>
        /// The name of your Menu. This has to be changed.
        /// </summary>
        public virtual string MenuName => "Default Menu";

        public QuickMenuPage MyModMenu = null;
        public QuickMenuWingMenu MyLeftWingMenu = null;
        public QuickMenuWingMenu MyRightWingMenu = null;
        public QuickMenuPage MyTargetMenu = null;
        public IUser SelectedUser = null;

        public virtual void OnQuickMenuInitialized() { }
        public virtual void OnWingMenuLeftInitialized() { }
        public virtual void OnWingMenuRightInitialized() { }
        public virtual void OnTargetMenuInitialized() { }
        
    }
}
