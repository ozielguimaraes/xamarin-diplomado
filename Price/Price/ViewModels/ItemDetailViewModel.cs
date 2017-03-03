using Price.Models;

namespace Price.ViewModels
{
    public class ItemDetailVm : BaseVm<Item>
    {
        public Item Item { get; set; }
        public ItemDetailVm(Item item = null)
        {
            if (item == null) return;
            Title = item.Text;
            Item = item;
        }

        int _quantity = 1;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }
    }
}