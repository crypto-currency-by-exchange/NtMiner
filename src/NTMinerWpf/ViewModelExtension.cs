﻿using NTMiner.Views;
using NTMiner.Vms;
using System;

namespace NTMiner {
    public static class ViewModelExtension {
        public static void ShowDialog(this ViewModelBase vm, string icon = null,
            string title = null,
            string message = null,
            string helpUrl = null,
            Action onYes = null,
            Func<bool> onNo = null,
            string yesText = null,
            string noText = null) {
            DialogWindow.ShowDialog(icon, title, message, helpUrl, onYes, onNo, yesText, noText);
        }
    }
}
