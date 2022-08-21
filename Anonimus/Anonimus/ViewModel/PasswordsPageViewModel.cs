using Anonimus.Model;
using Anonimus.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Anonimus.ViewModel
{
    public class PasswordsPageViewModel : BaseViewModel
    {
        private PasswordViewModel _selectedPassword;
        private IPasswordStore _passwordStore;
        private IPageService _pageService;

        private bool _isDataLoaded;

        public ObservableCollection<PasswordViewModel> Passwords { get; private set; }
            = new ObservableCollection<PasswordViewModel>();

        public PasswordViewModel SelectedPassword
        {
            get { return _selectedPassword; }
            set { SetValue(ref _selectedPassword, value); }
        }

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddPasswordCommand { get; private set; }
        public ICommand SelectPasswordCommand { get; private set; }
        public ICommand DeletePasswordCommand { get; private set; }
        public ICommand CopyPasswordCommand { get; private set; }

        public PasswordsPageViewModel(IPasswordStore passwordStore, IPageService pageService)
        {
            _passwordStore = passwordStore;
            _pageService = pageService;

            LoadDataCommand = new Command(async () => await LoadData());
            AddPasswordCommand = new Command(async () => await AddPassword());
            SelectPasswordCommand = new Command<PasswordViewModel>(async c => await SelectPassword(c));
            DeletePasswordCommand = new Command<PasswordViewModel>(async c => await DeletePassword(c));
            CopyPasswordCommand = new Command<PasswordViewModel>(async c => await CopyPassword(c));

            MessagingCenter.Subscribe<PasswordsDetailViewModel, Password>
                (this, Events.PasswordAdded, OnPasswordAdded);

            MessagingCenter.Subscribe<PasswordsDetailViewModel, Password>
            (this, Events.PasswordUpdated, OnPasswordUpdated);
        }

        private void OnPasswordAdded(PasswordsDetailViewModel source, Password password)
        {
            Passwords.Add(new PasswordViewModel(password));
        }

        private void OnPasswordUpdated(PasswordsDetailViewModel source, Password password)
        {
            var passwordInList = Passwords.Single(c => c.Id == password.Id);

            passwordInList.Id = password.Id;
            passwordInList.Name = password.Name;
            passwordInList.Mail = password.Mail;
            passwordInList.Password = password.Sifre;
            passwordInList.Color = password.Color;
            passwordInList.Date = password.Date;

            passwordInList.UserName = password.UserName;
            passwordInList.Dob = password.DOB;
            passwordInList.Length = password.Length;
            passwordInList.NameChecked = password.isNameChecked;
            passwordInList.DobChecked = password.isDobChecked;
            passwordInList.NumberChecked = password.isNumberChecked;
            passwordInList.SpecialCharChecked = password.isSpecialCharChecked;
            passwordInList.CharChecked = password.isCharChecked;
        }

        private async Task LoadData()
        {
            if (_isDataLoaded)
                return;

            _isDataLoaded = true;
            var passwords = await _passwordStore.GetPasswordsAsync();
            foreach (var password in passwords)
                Passwords.Add(new PasswordViewModel(password));
        }

        private async Task AddPassword()
        {
            await _pageService.PushAsync(new PasswordsDetailPage(new PasswordViewModel()));
        }

        private async Task SelectPassword(PasswordViewModel password)
        {
            if (password == null)
                return;

            SelectedPassword = null;
            await _pageService.PushAsync(new PasswordsDetailPage(password));
        }

        private async Task DeletePassword(PasswordViewModel passwordViewModel)
        {
            if (await _pageService.DisplayAlert("Uyarı", $"{passwordViewModel.Name} silmek istediğinizden emin misiniz?", "Evet", "Hayır"))
            {
                Passwords.Remove(passwordViewModel);

                var password = await _passwordStore.GetPassword(passwordViewModel.Id);
                await _passwordStore.DeletePassword(password);
            }
        }

        private async Task CopyPassword(PasswordViewModel passwordViewModel)
        {
            if (passwordViewModel != null)
            {
                var message = "Şifre: " + passwordViewModel.Password;
                bool isCopy = await _pageService.DisplayAlert("Gerçekten kopyalamak istiyor musunuz?", message, "Kopyala", "İptal");
                if (isCopy)
                {
                    await Clipboard.SetTextAsync(passwordViewModel.Password.ToString());
                }
            }
        }
    }
}
