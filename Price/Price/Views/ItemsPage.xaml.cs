using System;

using Price.Models;
using Price.ViewModels;

using Xamarin.Forms;

namespace Price.Views
{
    public partial class ItemsPage : ContentPage
    {
        readonly ItemsVm _vm;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _vm = new ItemsVm();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailVm(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_vm.Items.Count == 0) _vm.LoadItemsCommand.Execute(null);
        }
    }
}
