using Anonimus.Persistence;
using Anonimus.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Anonimus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordPage : ContentPage
    {
        public PasswordPage()
        {
            var passwordStore = new SQLitePasswordStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();

            ViewModel = new PasswordsPageViewModel(passwordStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnPasswordSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ViewModel.SelectPasswordCommand.Execute(e.SelectedItem);
        }

        public PasswordsPageViewModel ViewModel
        {
            get { return BindingContext as PasswordsPageViewModel; }
            set { BindingContext = value; }
        }
    }
}