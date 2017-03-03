using System;
using Price.ViewModels;
using Xamarin.Forms;

namespace Price.Views.Expenses
{
    public partial class NewExpansePage : ContentPage
    {
        public ExpenseVm Expense { get; set; }

        public NewExpansePage()
        {
            InitializeComponent();
            Expense = new ExpenseVm();
            BindingContext = this;
        }

        async void SaveExpanse_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddExpense", Expense);
            await Navigation.PushAsync(new ExpensesPage());
        }

        async void GoToAppInformation_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}