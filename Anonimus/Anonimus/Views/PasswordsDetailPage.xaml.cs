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
    public partial class PasswordsDetailPage : ContentPage
    {
        public PasswordsDetailPage(PasswordViewModel viewModel)
        {
            InitializeComponent();

            var passwordStore = new SQLitePasswordStore(DependencyService.Get<ISQLiteDb>());
            var pageService = new PageService();
            Title = (viewModel.Name == null) ? "Yeni Şifre" : "Şifre Düzenle";
            _buton.Text = (viewModel.Name == null) ? "Oluştur" : "Düzenle";
            BindingContext = new PasswordsDetailViewModel(viewModel ?? new PasswordViewModel(), passwordStore, pageService);
        }

        private void _Uzunluk_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            _UzunlukDeger.Text = Convert.ToInt32(_Uzunluk.Value).ToString();
        }

        private void _Isim_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _IsimLbl.IsVisible = _Isim.IsChecked;
        }

        private void _DOB_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _DobLbl.IsVisible = _DOB.IsChecked;
        }
    }
}