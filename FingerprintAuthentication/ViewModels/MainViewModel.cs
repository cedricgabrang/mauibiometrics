using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MauiBiometrics.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IFingerprint _fingerprint;

        private string _userName;
        private string _password;

        private ICommand _fingerprintCommand;

        public MainViewModel(IFingerprint fingerprint)
        {
            _fingerprint = fingerprint;

            FingerprintLoginAsync();
        }
        
        public string UserName 
        { 
            get => _userName;
            set 
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
       
        public ICommand FingerprintCommand => _fingerprintCommand ??= new Command(async () => await FingerprintLoginAsync());

        async Task FingerprintLoginAsync() 
        {
            var isBiometricsAvailable = await _fingerprint.IsAvailableAsync();

            if (isBiometricsAvailable)
            {
                var dialogConfig = new AuthenticationRequestConfiguration
                ("Login using biometrics", "Confirm login with your biometrics")
                {
                    FallbackTitle = "Use Password",
                    AllowAlternativeAuthentication = true,
                };

                var result = await _fingerprint.AuthenticateAsync(dialogConfig);

                if (result.Authenticated)
                {

                }
                else
                {

                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
