using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dicer
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        readonly DiceSite _site;
        #endregion

        public LoginViewModel(DiceSite Site)
        {

            _site = Site;

            Title = String.Format("{0} login", Site.SiteTitle);
            SignIn = new Command(async () => await ExecuteLogin());
        }

        #region Properties
        string _username = string.Empty;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        string _twofa = string.Empty;
        public string TwoFa
        {
            get => _twofa;
            set => SetProperty(ref _twofa, value);
        }

        public ICommand SignIn { get; set; }
		#endregion

		#region methods
		async Task ExecuteLogin()
		{
            if (IsBusy)
                return;

            IsBusy = true;

            var result = await _site.Login(Username, Password, TwoFa);

            if (result)
                MessagingCenter.Send(this, "AddAccount", _site);

            IsBusy = false;
		}
        #endregion

    }
}
