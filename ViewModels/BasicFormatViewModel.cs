using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using ProjectManager.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjectManager.ViewModels
{
    public class BasicFormatViewModel : ViewModelBase, INavigationAware
    {
        //private readonly INavigationService _navigationService;

        public BasicFormatViewModel()
        {
            //_navigationService = navigationService;

            Options = new ObservableCollection<Option>();
        }

        public partial class Option
        {
            public int OptionId { get; set; }
            public string OptionName { get; set; }

            public Option()
            {

            }
        }

        private ICommand _AddCommand;
        public ICommand AddCommand => _AddCommand ??= new RelayCommand(AddControl);

        private ObservableCollection<Option> _Options;
        public ObservableCollection<Option> Options
        {
            get => _Options;
            set
            {
                if (_Options != value)
                {
                    _Options = value;
                    RaisePropertyChanged("Options");
                }
            }
        }


        private void AddControl()
        {
            Options.Add(new Option());
        }


        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter)
        {
            //throw new NotImplementedException();
        }
    }
}
