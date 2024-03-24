using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ボタンのスタイルをいじってみる.Resources.Windows;

namespace ボタンのスタイルをいじってみる {
    internal class MainWindowViewModel : INotifyPropertyChanged {

        private readonly Window _window;

        public ReactiveCommandSlim ExecuteCommand { get; } = new ReactiveCommandSlim();
        public ReactiveCommandSlim CancelCommand { get; } = new ReactiveCommandSlim();

        public MainWindowViewModel(Window window = null) {
            _window = window;
            ExecuteCommand.Subscribe(_ => ExecuteButtonExecute());
            CancelCommand.Subscribe(_ => CancelButtonExecute());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteButtonExecute() {
            MessageEx.ShowInformationDialog("information.", MessageBoxButton.OKCancel, _window);
        }

        private void CancelButtonExecute() {
            MessageBox.Show("Canceled.");
        }
    }
}
