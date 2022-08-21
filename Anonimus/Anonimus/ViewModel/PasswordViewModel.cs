using Anonimus.Model;

namespace Anonimus.ViewModel
{
    public class PasswordViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public PasswordViewModel() { }

        public PasswordViewModel(Password password)
        {
            Id = password.Id;
            Name = password.Name;
            Mail = password.Mail;
            Password = password.Sifre;
            Date = password.Date;
            Color = password.Color;
            UserName = password.UserName;
            Dob = password.DOB;
            Length = password.Length;
            NameChecked = password.isNameChecked;
            DobChecked = password.isDobChecked;
            NumberChecked = password.isNumberChecked;
            SpecialCharChecked = password.isSpecialCharChecked;
            CharChecked = password.isCharChecked;

        }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set
            {
                SetValue(ref _mail, value);
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                SetValue(ref _name, value);
            }
        }
        private string _sifre;
        public string Password
        {
            get { return _sifre; }
            set
            {
                SetValue(ref _sifre, value);
            }
        }
        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                SetValue(ref _date, value);
            }
        }
        private string _color;
        public string Color
        {
            get { return _color; }
            set
            {
                SetValue(ref _color, value);
            }
        }
        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                SetValue(ref _username, value);
            }
        }
        private string _dob;
        public string Dob
        {
            get { return _dob; }
            set
            {
                SetValue(ref _dob, value);
            }
        }
        private int _length;
        public int Length
        {
            get { return _length; }
            set
            {
                SetValue(ref _length, value);
            }
        }

        private bool _namechecked;
        public bool NameChecked
        {
            get { return _namechecked; }
            set
            {
                SetValue(ref _namechecked, value);
            }
        }

        private bool _dobchecked;
        public bool DobChecked
        {
            get { return _dobchecked; }
            set
            {
                SetValue(ref _dobchecked, value);
            }
        }

        private bool _numberchecked;
        public bool NumberChecked
        {
            get { return _numberchecked; }
            set
            {
                SetValue(ref _numberchecked, value);
            }
        }
        private bool _specialcharchecked;
        public bool SpecialCharChecked
        {
            get { return _specialcharchecked; }
            set
            {
                SetValue(ref _specialcharchecked, value);
            }
        }

        private bool _charchecked;
        public bool CharChecked
        {
            get { return _charchecked; }
            set
            {
                SetValue(ref _charchecked, value);
            }
        }



    }
}