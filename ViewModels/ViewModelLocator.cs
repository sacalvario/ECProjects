﻿
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Extensions.Configuration;
using ProjectManager.Contracts.Services;
using ProjectManager.Contracts.Views;
using ProjectManager.Models;
using ProjectManager.Services;
using ProjectManager.Views;
using System.Windows.Controls;

namespace ProjectManager.ViewModels
{
    public class ViewModelLocator
    {
        private IPageService PageService
            => SimpleIoc.Default.GetInstance<IPageService>();

        public ShellViewModel ShellViewModel
            => SimpleIoc.Default.GetInstance<ShellViewModel>();

        public HistoryViewModel HistoryViewModel
            => SimpleIoc.Default.GetInstance<HistoryViewModel>();

        public FrontCaptureViewModel FrontCaptureViewModel
            => SimpleIoc.Default.GetInstance<FrontCaptureViewModel>();

        public NumberPartsViewModel NumberPartsViewModel
            => SimpleIoc.Default.GetInstance<NumberPartsViewModel>();

        public HistoryDetailsViewModel HistoryDetailsViewModel
            => SimpleIoc.Default.GetInstance<HistoryDetailsViewModel>();

        public EmployeesViewModel EmployeesViewModel
            => SimpleIoc.Default.GetInstance<EmployeesViewModel>();

        public EcnRecordsViewModel EcnRecordsViewModel
            => SimpleIoc.Default.GetInstance<EcnRecordsViewModel>();

        public ChecklistViewModel ChecklistViewModel
            => SimpleIoc.Default.GetInstance<ChecklistViewModel>();

        public LoginViewModel LoginViewModel
            => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public EcnRegistrationViewModel EcnRegistrationViewModel
            => SimpleIoc.Default.GetInstance<EcnRegistrationViewModel>();

        public EcnSignedViewModel EcnSignedViewModel
            => SimpleIoc.Default.GetInstance<EcnSignedViewModel>();

        public ErrorViewModel ErrorViewModel
            => SimpleIoc.Default.GetInstance<ErrorViewModel>();

        public ApprovedViewModel ApprovedViewModel
            => SimpleIoc.Default.GetInstance<ApprovedViewModel>();

        public NumberPartHistoryViewModel NumberPartHistoryViewModel
            => SimpleIoc.Default.GetInstance<NumberPartHistoryViewModel>();

        public ReportViewModel ReportViewModel
            => SimpleIoc.Default.GetInstance<ReportViewModel>();

        public DashboardViewModel DashboardViewModel
            => SimpleIoc.Default.GetInstance<DashboardViewModel>();

        public ShellLoginViewModel ShellLoginViewModel
            => SimpleIoc.Default.GetInstance<ShellLoginViewModel>();

        public SignUpViewModel SignUpViewModel
            => SimpleIoc.Default.GetInstance<SignUpViewModel>();

        public NumberPartsPageViewModel NumberPartsPageViewModel
            => SimpleIoc.Default.GetInstance<NumberPartsPageViewModel>();

        public EmployeesPageViewModel EmployeesPageViewModel
            => SimpleIoc.Default.GetInstance<EmployeesPageViewModel>();

        public AddNumberPartViewModel AddNumberPartViewModel
            => SimpleIoc.Default.GetInstance<AddNumberPartViewModel>();

        public AddEmployeeViewModel AddEmployeeViewModel
            => SimpleIoc.Default.GetInstance<AddEmployeeViewModel>();

        public ConfirmationWindowViewModel ConfirmationWindowViewModel
            => SimpleIoc.Default.GetInstance<ConfirmationWindowViewModel>();

        public NumberPartHistoryChangeViewModel NumberPartHistoryChangeViewModel
            => SimpleIoc.Default.GetInstance<NumberPartHistoryChangeViewModel>();

        public AddCustomerViewModel AddCustomerViewModel
            => SimpleIoc.Default.GetInstance<AddCustomerViewModel>();

        public SearchViewModel SearchViewModel
            => SimpleIoc.Default.GetInstance<SearchViewModel>();

        public BasicFormatViewModel BasicFormatViewModel
            => SimpleIoc.Default.GetInstance<BasicFormatViewModel>();
        
        public AdvancedFormatViewModel AdvancedFormatViewModel
            => SimpleIoc.Default.GetInstance<AdvancedFormatViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Data Services
            SimpleIoc.Default.Register<IEcnDataService, EcnDataService>();
            SimpleIoc.Default.Register<INumberPartsDataService, NumberPartsDataService>();
            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();

            // Services
            SimpleIoc.Default.Register<IWindowManagerService, WindowManagerService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IOpenFileService, OpenFileService>();
            SimpleIoc.Default.Register<IMailService, MailService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ILoginWindow, ShellLogin>();
            SimpleIoc.Default.Register<IShellDialogWindow, ShellDialogWindow>();
            SimpleIoc.Default.Register<INumberPartsWindow, NumberParts>();
            SimpleIoc.Default.Register<IEmployeesWindow, Employees>();
            SimpleIoc.Default.Register<IConfirmationWindow, ConfirmationWindow>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<ShellDialogViewModel>();
            SimpleIoc.Default.Register<ShellLoginViewModel>();
            SimpleIoc.Default.Register<NumberPartsViewModel>();
            SimpleIoc.Default.Register<EmployeesViewModel>();
            SimpleIoc.Default.Register<AddNumberPartViewModel>();
            SimpleIoc.Default.Register<AddEmployeeViewModel>();
            SimpleIoc.Default.Register<AddCustomerViewModel>();
            SimpleIoc.Default.Register<ConfirmationWindowViewModel>();
            SimpleIoc.Default.Register<Report>();
            SimpleIoc.Default.Register<ReportViewModel>();
            SimpleIoc.Default.Register<Models.Ecn>();
            SimpleIoc.Default.Register<Employee>();
            SimpleIoc.Default.Register<Numberpart>();

            // Pages
            Register<HistoryViewModel, History>();
            Register<FrontCaptureViewModel, FrontCapture>();
            Register<HistoryDetailsViewModel, HistoryDetails>();
            Register<EcnRecordsViewModel, EcnRecords>();
            Register<ChecklistViewModel, Checklist>();
            Register<EcnRegistrationViewModel, EcnRegistration>();
            Register<EcnSignedViewModel, EcnSigned>();
            Register<ErrorViewModel, Error>();
            Register<ApprovedViewModel, Approved>();
            Register<NumberPartHistoryViewModel, NumberPartHistory>();
            Register<DashboardViewModel, Dashboard>();
            Register<LoginViewModel, Login>();
            Register<SignUpViewModel, SignUp>();
            Register<NumberPartsPageViewModel, NumberPartsPage>();
            Register<EmployeesPageViewModel, EmployeesPage>();
            Register<NumberPartHistoryChangeViewModel, NumberPartHistoryChange>();
            Register<SearchViewModel, Search>();
            Register<BasicFormatViewModel, BasicFormat>();
            Register<AdvancedFormatViewModel, AdvancedFormat>();
        }

        private void Register<VM, V>()
            where VM : ViewModelBase
            where V : Page
        {
            SimpleIoc.Default.Register<VM>();
            SimpleIoc.Default.Register<V>();
            PageService.Configure<VM, V>();
        }

        public void AddConfiguration(IConfiguration configuration)
        {
            var appConfig = configuration
                .GetSection(nameof(AppConfig))
                .Get<AppConfig>();

            SimpleIoc.Default.Register(() => configuration);
            SimpleIoc.Default.Register(() => appConfig);
        }

        public static void UnregisterNumberPartViewModel()
        {
            SimpleIoc.Default.Unregister<NumberPartsViewModel>();
            SimpleIoc.Default.Register<NumberPartsViewModel>();
        }

        public static void UnregisterEmployeesViewModel()
        {
            SimpleIoc.Default.Unregister<EmployeesViewModel>();
            SimpleIoc.Default.Register<EmployeesViewModel>();
        }

        public static void UnregisterShellViewModel()
        {
            SimpleIoc.Default.Unregister<ShellViewModel>();
            SimpleIoc.Default.Register<ShellViewModel>();
        }

        public static void UnregisterEmployeesPageViewModel()
        {
            SimpleIoc.Default.Unregister<ShellViewModel>();
            SimpleIoc.Default.Register<ShellViewModel>();
        }
    }
}
