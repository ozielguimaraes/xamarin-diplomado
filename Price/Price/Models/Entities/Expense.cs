using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Price.ViewModels;

namespace Price.Models.Entities
{
    [DataTable("Expenses")]
    public class Expense
    {
        public Expense(string name, decimal value, DateTime dueDate, Guid userId)
        {
            ExpenseId = Guid.NewGuid();
            Name = name;
            Value = value;
            DueDate = dueDate;
            UserId = userId;
        }

        private Expense()
        {
            ExpenseId = Guid.NewGuid();
        }

        [JsonProperty("Id")]
        public Guid ExpenseId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime DueDate { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        [Version]
        public string Version { get; set; }

        public bool Set(ExpenseVm vm)
        {

            return true;
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name)) throw new Exception("Campo Nome é obrigatório");
            if (DateTime.MinValue == DueDate) throw new Exception("Campo Data de Pagamento é obrigatório");
            if (Value == 0) throw new Exception("Campo Valor deve ser maior que R$ 0,00");
        }
    }
}