using System.Threading;
using Price.iOS;
using Price.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AccountService))]
namespace Price.iOS
{
	public class AccountService : IAccount
	{
		public void Logout()
		{
			Thread.CurrentThread.Abort();
		}
	}
}