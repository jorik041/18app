﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Italia.DiciottoApp.ViewModels
{
    class NearToYouShopsViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        public string PageTitle { get; set; } = "Negozi";

        private string userCredit;
        public string UserCredit
        {
            get
            {
                return userCredit;
            }

            set
            {
                if (value != userCredit)
                {
                    userCredit = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string unreadMessages;
        public string UnreadMessages
        {
            get
            {
                return unreadMessages;
            }

            set
            {
                if (value != unreadMessages)
                {
                    unreadMessages = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        public NearToYouShopsViewModel()
        {
            GetUserStatus();
        }

        private void GetUserStatus()
        {
            // TODO: get data from service
            UserCredit = "174,00€";
            UnreadMessages = "2";
        }

    }
}
