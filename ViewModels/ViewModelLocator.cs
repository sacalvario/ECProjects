
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

        public FrontCaptureViewModel FrontCaptureViewModel
            => SimpleIoc.Default.GetInstance<FrontCaptureViewModel>();

        public LoginViewModel LoginViewModel
            => SimpleIoc.Default.GetInstance<LoginViewModel>();

        public EcnSignedViewModel EcnSignedViewModel
            => SimpleIoc.Default.GetInstance<EcnSignedViewModel>();

        public ErrorViewModel ErrorViewModel
            => SimpleIoc.Default.GetInstance<ErrorViewModel>();

        public ShellLoginViewModel ShellLoginViewModel
            => SimpleIoc.Default.GetInstance<ShellLoginViewModel>();

        public SignUpViewModel SignUpViewModel
            => SimpleIoc.Default.GetInstance<SignUpViewModel>(); 
        
        public HistoryViewModel HistoryViewModel
            => SimpleIoc.Default.GetInstance<HistoryViewModel>();

        public BasicFormatViewModel BasicFormatViewModel
            => SimpleIoc.Default.GetInstance<BasicFormatViewModel>();
        
        public AdvancedFormatViewModel AdvancedFormatViewModel
            => SimpleIoc.Default.GetInstance<AdvancedFormatViewModel>();
        
        public ApplyMessageViewModel ApplyMessageViewModel
            => SimpleIoc.Default.GetInstance<ApplyMessageViewModel>();
        
        public ProjectDetailsViewModel ProjectDetailsViewModel
            => SimpleIoc.Default.GetInstance<ProjectDetailsViewModel>();

        public ViewModelLocator()
        {
            // App Host
            SimpleIoc.Default.Register<IApplicationHostService, ApplicationHostService>();

            // Data Services
            SimpleIoc.Default.Register<ILoginDataService, LoginDataService>();

            // Services
            SimpleIoc.Default.Register<IWindowManagerService, WindowManagerService>();
            SimpleIoc.Default.Register<IPageService, PageService>();
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IOpenFileService, OpenFileService>();
            SimpleIoc.Default.Register<IMailService, MailService>();
            SimpleIoc.Default.Register<IProjectsDataService, ProjectsDataService>();

            // Window
            SimpleIoc.Default.Register<IShellWindow, ShellWindow>();
            SimpleIoc.Default.Register<ILoginWindow, ShellLogin>();
            SimpleIoc.Default.Register<IShellDialogWindow, ShellDialogWindow>();
            SimpleIoc.Default.Register<ShellViewModel>();
            SimpleIoc.Default.Register<ShellDialogViewModel>();
            SimpleIoc.Default.Register<ShellLoginViewModel>();

            // Pages
            Register<FrontCaptureViewModel, FrontCapture>();
            Register<EcnSignedViewModel, EcnSigned>();
            Register<ErrorViewModel, Error>();
            Register<LoginViewModel, Login>();
            Register<SignUpViewModel, SignUp>();
            Register<BasicFormatViewModel, BasicFormat>();
            Register<AdvancedFormatViewModel, AdvancedFormat>();
            Register<ApplyMessageViewModel, ApplyMesagge>();
            Register<HistoryViewModel, History>();
            Register<ProjectDetailsViewModel, ProjectDetails>();
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

     

        public static void UnregisterShellViewModel()
        {
            SimpleIoc.Default.Unregister<ShellViewModel>();
            SimpleIoc.Default.Register<ShellViewModel>();
        }

    }
}
