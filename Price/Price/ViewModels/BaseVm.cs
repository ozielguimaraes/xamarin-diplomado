using Price.Helpers;
using Price.Services;
using Price.Services.Interfaces;
using Price.Views;
using Xamarin.Forms;

namespace Price.ViewModels
{
    public class BaseVm<TObject> : ObservableObject
    {
        protected Command GoToAppInformation { get; set; }
        public IDataStore<TObject> DataStore => DependencyService.Get<IDataStore<TObject>>();

        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        string _title = Constants.AppName;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}