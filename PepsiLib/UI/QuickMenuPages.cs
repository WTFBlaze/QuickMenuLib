using System.Collections.Generic;

namespace PepsiLib.UI
{
    public class QuickMenuPages
    {
        public enum QMPages
        {
            Dashboard,
            SelectedUser,
            SelectedUserRemote,
            Emoji,
            Here,
            Camera,
            AudioSettings,
            UIElements,
            Invites,
            DevMenu
        }

        public static readonly Dictionary<QMPages, string> QuickMenuPagesPath = new Dictionary<QMPages, string>()
        {
            { QMPages.Dashboard, "Container/Window/QMParent/Menu_Dashboard"},
            { QMPages.SelectedUser, "Container/Window/QMParent/Menu_SelectedUser_Local"},
            { QMPages.SelectedUserRemote, "Container/Window/QMParent/Menu_SelectedUser_Remote"},
            { QMPages.Emoji, "Container/Window/QMParent/Menu_QM_Emojis"},
            { QMPages.Here, "Container/Window/QMParent/Menu_Here"},
            { QMPages.Camera, "Container/Window/QMParent/Menu_Camera"},
            { QMPages.AudioSettings, "Container/Window/QMParent/Menu_AudioSettings"},
            { QMPages.UIElements, "Container/Window/QMParent/Menu_Settings"},
            { QMPages.Invites, "Container/Window/QMParent/Menu_Notifications"},
            { QMPages.DevMenu, "Container/Window/QMParent/Menu_DevTools" }
        };
    }
}
