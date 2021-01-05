using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using XamTrg.MVVMexercise.Models;
using XamTrg.MVVMexercise.Services;

namespace XamTrg.MVVMexercise.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private User _User;
        private ObservableCollection<User> _Users;
        private UserService userService;
        private bool _IsAddEnabled;
        public UserViewModel()
        {
            User = new User();
            Users = new ObservableCollection<User>();
            userService = new UserService();
            IsAddEnabled = false;

            GetUsersCommand = new Command(GetUser);
            AddUserCommand = new Command(AddUser);
            NavigateCommand = new Command(Navigate);
        }
        public ICommand GetUsersCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand NavigateCommand { get; private set; }
        public User User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnProprtyChanged("User");
            }
        }
        public ObservableCollection<User> Users
        {
            get
            {
                return _Users;
            }
            set
            {
                _Users = value;
                OnProprtyChanged("Users");
            }
        }
        public bool IsAddEnabled
        {
            get
            {
                return _IsAddEnabled;
            }
            set
            {
                _IsAddEnabled = value;
                OnProprtyChanged("IsAddEnabled");
            }
        }
        private async void GetUser()
        {
            Users.Clear();
            var usersResponse = await userService.GetUserAsync();
            foreach(User user in usersResponse)
            {
                _Users.Add(user);
            }
            if(_Users.Count >= 0)
            {
                IsAddEnabled = true;
            }
        }

        private async void AddUser()
        {
            User = await userService.PostUserAync(User);
            if (User.UserRowId > 0) 
            {
                await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }

        private async void Navigate()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new Views.RegisterUser());
        }
    }
}
