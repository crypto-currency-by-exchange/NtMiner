﻿using NTMiner.Core;
using NTMiner.Vms;
using System.Windows.Controls;

namespace NTMiner.Views.Ucs {
    public partial class WalletSelect : UserControl, IVmFrameworkElement<WalletSelectViewModel> {
        public WalletSelectViewModel Vm { get; set; }

        public WalletSelect(WalletSelectViewModel vm) {
            this.Init(vm);
            InitializeComponent();
            this.OnLoaded(window => {
                window.BuildEventPath<WalletVmAddedEvent>("添加了钱包后，如果添加的钱包是当前选中的币种的钱包则刷新钱包选择下拉列表的Vm内存", LogEnum.DevConsole, this.GetType(), PathPriority.Normal, path: message => {
                    if (message.Event.Source.CoinId == vm.Coin.Id) {
                        vm.OnPropertyChanged(nameof(vm.QueryResults));
                    }
                });
                window.BuildEventPath<WalletVmRemovedEvent>("删除了钱包后，如果删除的钱包是当前选中的币种的钱包则刷新钱包选择下拉列表的Vm内存", LogEnum.DevConsole, this.GetType(), PathPriority.Normal, path: message => {
                    if (message.Event.Source.CoinId == vm.Coin.Id) {
                        vm.OnPropertyChanged(nameof(vm.QueryResults));
                    }
                });
            });
        }

        private void KbButtonManageWallets_Click(object sender, System.Windows.RoutedEventArgs e) {
            Vm.HideView?.Execute(null);
        }

        private void DataGrid_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Vm.OnOk?.Invoke(Vm.SelectedResult);
        }

        private void DataGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
            if (e.Key == System.Windows.Input.Key.Enter) {
                Vm.OnOk?.Invoke(Vm.SelectedResult);
                e.Handled = true;
            }
        }
    }
}
