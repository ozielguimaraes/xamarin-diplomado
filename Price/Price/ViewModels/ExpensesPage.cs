using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Price.Helpers;
using Price.Models.Entities;
using Price.Services;
using Price.Views.Expenses;
using Xamarin.Forms;

namespace Price.ViewModels
{
    public class ExpensesPage : BaseVm<Expense>
    {
        public ObservableRangeCollection<Expense> Items { get; set; }
        private readonly ExpensesAzureRest _service;

        public Command LoadItemsCommand { get; set; }
        public Command RefreshCommand { get; set; }

        public ExpensesPage()
        {
            Title = "Despesas";
            Items = new ObservableRangeCollection<Expense>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            RefreshCommand = new Command(Load);
            _service = new ExpensesAzureRest();

            MessagingCenter.Subscribe<NewExpansePage, Expense>(this, "AddExpense", async (obj, item) =>
            {
                Items.Add(item);
                await DataStore.AddItemAsync(item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível ler os items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void Load()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var result = await _service.GetAll();

                foreach (var item in result) Items.Add(item);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível ler os items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}