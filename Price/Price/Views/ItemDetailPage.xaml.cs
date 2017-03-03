
using Price.ViewModels;

using Xamarin.Forms;

namespace Price.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailVm _vm;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailVm vm)
        {
            InitializeComponent();

            BindingContext = _vm = vm;
        }
    }
}
