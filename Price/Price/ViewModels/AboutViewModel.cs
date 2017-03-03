using System;
using System.Windows.Input;
using Price.Models;
using Xamarin.Forms;

namespace Price.ViewModels
{
    public class AboutVm : BaseVm<Item>
    {
        public AboutVm()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://about.me/ozielguimaraes")));
        }

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}