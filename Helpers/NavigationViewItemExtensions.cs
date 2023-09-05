﻿
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
                    "History" => typeof(HistoryViewModel),
                    "FrontCapture" => typeof(FrontCaptureViewModel),
                    "Records" => typeof(EcnRecordsViewModel),
                    "Checklist" => typeof(ChecklistViewModel),
                    "Approved" => typeof(ApprovedViewModel),
                    "NumberPartHistory" => typeof(NumberPartHistoryViewModel),
                    "Dashboard" => typeof(DashboardViewModel),
                    "NumberParts" => typeof(NumberPartsPageViewModel),
                    "Employees" => typeof(EmployeesPageViewModel),
                    "Search" => typeof(SearchViewModel),
                    _ => null,
                }
                : null;
        }
    }
}
