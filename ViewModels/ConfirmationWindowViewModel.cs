using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManager.ViewModels
{
    public class ConfirmationWindowViewModel
    {
        public Action<bool?> SetResult { get; set; }
        public ConfirmationWindowViewModel()
        {
            
        }
        private void OnClose()
        {
            bool result = true;
            SetResult(result);
        }
    }
}
