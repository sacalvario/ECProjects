using ProjectManager.Contracts.ViewModels;
using GalaSoft.MvvmLight;

namespace ProjectManager.ViewModels
{
    public class ApplyMessageViewModel : ViewModelBase, INavigationAware
    {
        private int _ID;
        public int ID
        {
            get => _ID;
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    RaisePropertyChanged("ID");
                }
            }
        }
        public ApplyMessageViewModel()
        {

        }

        public void OnNavigatedTo(object parameter)
        {
            if (parameter is int id)
            {
                ID = id;
            }
        }

        public void OnNavigatedFrom()
        {
           
        }
    }
}
