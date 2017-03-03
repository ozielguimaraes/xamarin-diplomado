using Price.Droid;
using Price.Services.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(AccountService))]
namespace Price.Droid
{
    public class AccountService : IAccount
    {
        public void Logout()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}