using System;
using Xamarin.Forms;

namespace Price.Views.Expenses
{
    public partial class ExpensesPage : ContentPage
    {
        private readonly ViewModels.ExpensesPage _page;
        public ExpensesPage()
        {
            InitializeComponent();
            BindingContext = _page = new ViewModels.ExpensesPage();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_page.Items.Count == 0) _page.LoadItemsCommand.Execute(null);
        }

        async void GoToNewExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewExpansePage());
        }

        async void GoToAppInformation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}