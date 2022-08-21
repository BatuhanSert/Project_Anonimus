using Anonimus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Anonimus.ViewModel
{
    public class PasswordsDetailViewModel
    {
        private readonly IPasswordStore _passwordStore;
        private readonly IPageService _pageService;

        private string kucuk_buyuk_harf = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string rakam = "1234567890";
        private string ozel_karakter = "!#$%&()*+,-./:;<=>?@[]^_{|}~";

        public Password Password { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public PasswordsDetailViewModel(PasswordViewModel viewModel, IPasswordStore contactStore, IPageService pageService)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(viewModel));

            _pageService = pageService;
            _passwordStore = contactStore;

            SaveCommand = new Command(async () => await Save());

            Password = new Password
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Mail = viewModel.Mail,
                Sifre = viewModel.Password,
                Date = viewModel.Date,
                Color = viewModel.Color,
                UserName = viewModel.UserName,
                DOB = viewModel.Dob,
                Length = viewModel.Length,
                isNameChecked = viewModel.NameChecked,
                isDobChecked = viewModel.DobChecked,
                isNumberChecked = viewModel.NumberChecked,
                isCharChecked = viewModel.CharChecked,
                isSpecialCharChecked = viewModel.SpecialCharChecked

            };
        }

        async Task Save()
        {
            if (String.IsNullOrWhiteSpace(Password.Name))
            {
                await _pageService.DisplayAlert("Error", "Lütfen isim giriniz..", "OK");
                return;
            }
            if (generatePassword())
            {
                await _pageService.DisplayAlert("Başarılı", $"{Password.Sifre} --> şifreniz", "OK");
                if (Password.Id == 0)
                {
                    await _passwordStore.AddPassword(Password);
                    MessagingCenter.Send(this, Events.PasswordAdded, Password);
                }
                else
                {
                    await _passwordStore.UpdatePassword(Password);
                    MessagingCenter.Send(this, Events.PasswordUpdated, Password);
                }
                await _pageService.PopAsync();
            }
            else
            {
                await _pageService.DisplayAlert("Uyarı", "Lütfen boş alan bırakmayınız..", "OK");
            }
        }

        public bool generatePassword()
        {
            int length = Convert.ToInt32(Password.Length);
            int counter = 0;
            string buyuk_harf_dizisi = "";
            string rakam_dizisi = "";
            string ozel_karakter_dizisi = "";
            string parola = "";

            if (Password.isCharChecked || Password.isNumberChecked || Password.isSpecialCharChecked)
            {
                if (Password.isCharChecked)
                {
                    counter++;
                }
                if (Password.isNumberChecked)
                {
                    counter++;
                }
                if (Password.isSpecialCharChecked)
                {
                    counter++;
                }

                switch (counter)
                {
                    case 3:
                        if (length % 3 != 1)
                        {
                            buyuk_harf_dizisi = sifreOlustur(length / 3, kucuk_buyuk_harf);
                            rakam_dizisi = sifreOlustur(length / 3, rakam);
                            ozel_karakter_dizisi = sifreOlustur(length / 3, ozel_karakter);
                        }
                        else
                        {
                            buyuk_harf_dizisi = sifreOlustur(length / 3 + 1, kucuk_buyuk_harf);
                            rakam_dizisi = sifreOlustur(length / 3, rakam);
                            ozel_karakter_dizisi = sifreOlustur(length / 3, ozel_karakter);
                        }
                        break;



                    case 2:
                        if (length % 2 != 1)
                        {
                            if (Password.isCharChecked)
                            {
                                buyuk_harf_dizisi = sifreOlustur(length / 2, kucuk_buyuk_harf);
                            }
                            if (Password.isNumberChecked)
                            {
                                rakam_dizisi = sifreOlustur(length / 2, rakam);
                            }
                            if (Password.isSpecialCharChecked)
                            {
                                ozel_karakter_dizisi = sifreOlustur(length / 2, ozel_karakter);
                            }
                        }
                        else
                        {
                            if (Password.isCharChecked)
                            {
                                buyuk_harf_dizisi = sifreOlustur(length / 2 + 1, kucuk_buyuk_harf);
                            }
                            if (Password.isNumberChecked)
                            {
                                if (Password.isCharChecked)
                                {
                                    rakam_dizisi = sifreOlustur(length / 2, rakam);
                                }
                                else
                                {
                                    rakam_dizisi = sifreOlustur(length / 2 + 1, rakam);
                                }
                            }
                            if (Password.isSpecialCharChecked)
                            {
                                ozel_karakter_dizisi = sifreOlustur(length / 2, ozel_karakter);
                            }
                        }
                        break;


                    case 1:
                        if (Password.isCharChecked)
                        {
                            buyuk_harf_dizisi = sifreOlustur(length, kucuk_buyuk_harf);
                        }
                        if (Password.isNumberChecked)
                        {
                            rakam_dizisi = sifreOlustur(length, rakam);
                        }
                        if (Password.isSpecialCharChecked)
                        {
                            ozel_karakter_dizisi = sifreOlustur(length, ozel_karakter);
                        }
                        break;
                }

                parola = sifreOlustur(length, buyuk_harf_dizisi + rakam_dizisi + ozel_karakter_dizisi);
                if (parola == "False")
                {
                    return false;
                }
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                var random = new Random();
                if (Password.Id == 0)
                {
                    var color = String.Format("#{0:X6}", random.Next(0x1000000));
                    Password.Color = color;
                }

                Password.Sifre = parola;
                Password.Date = date;

                return true;
            }
            else
            {
                return false;
            }
        }

        private readonly static Random _rand = new Random();

        private string sifreOlustur(int length, string valid)
        {
            StringBuilder res = new StringBuilder();
            var bytes = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            foreach (byte b in bytes)
            {
                res.Append(valid[b % valid.Count()]);
            }
            if (Password.isNameChecked)
            {
                if (Password.UserName != null)
                {
                    if (!int.TryParse(Password.UserName, out _) && Password.UserName.Length >= 3)
                    {
                        if (res.ToString().Contains(Password.UserName))
                        {
                            sifreOlustur(length, valid);
                        }
                    }
                    else
                    {
                        res.Clear();
                        res.Append("False");
                    }
                }
                else
                {
                    res.Clear();
                    res.Append("False");
                }
            }

            if (Password.isDobChecked)
            {
                if (Password.DOB != null)
                {
                    if (int.TryParse(Password.DOB, out _) && Password.DOB.Length == 4)
                    {
                        if (res.ToString().Contains(Password.DOB))
                        {
                            sifreOlustur(length, valid);
                        }
                    }
                    else
                    {
                        res.Clear();
                        res.Append("False");
                    }

                }
                else
                {
                    res.Clear();
                    res.Append("False");
                }
            }

            return res.ToString();

        }
    }
}
