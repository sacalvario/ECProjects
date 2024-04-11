
using System;
using ProjectManager.ViewModels;

namespace ModernWpf.Controls
{
    public static class NavigationViewItemExtensions
    {

        public static Type SetTargetPageType(this NavigationViewItem navigationViewItem)
        {
            return navigationViewItem != null
                ? navigationViewItem.Tag.ToString() switch
                {
                    "FrontCapture" => typeof(FrontCaptureViewModel),
                    "History" => typeof(HistoryViewModel),
                    "Tasks" => typeof(TasksViewModel),
                    _ => null,
                }
                : null;
        }
    }
}
