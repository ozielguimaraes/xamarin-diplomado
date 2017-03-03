using System;
using System.Diagnostics;
using Price.Helpers;
using Price.Models.Entities;
using Price.Services;
using Xamarin.Forms;

namespace Price.ViewModels
{
    public class ExpenseVm : BaseVm<ExpenseVm>
    {
        private readonly ExpensesAzureRest _service;

        public Command NewExpenseCommand { get; set; }

        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public decimal Total { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Version { get; set; }

        public ExpenseVm()
        {
            NewExpenseCommand = new Command(AddExpense);
            _service = new ExpensesAzureRest();
        }

        public async void AddExpense()
        {
            try
            {
                var entity = new Expense(Name, Total, DueDate, UserId);
                entity.Validate();
                await _service.AddAsync(entity);

                MessagingCenter.Send(this, "AddExpense", entity);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Erro",
                    Message = "Não foi possível adicionar a despesa.",
                    Cancel = "OK"
                }, "message");
            }
            finally { IsBusy = false; }
        }
    }
}