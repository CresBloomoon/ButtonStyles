using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ボタンのスタイルをいじってみる {
    internal class MainWindowViewModel : INotifyPropertyChanged {

        public ReactiveCommandSlim ExecuteCommand { get; } = new ReactiveCommandSlim();
        public ReactiveCommandSlim CancelCommand { get; } = new ReactiveCommandSlim();

        public MainWindowViewModel() {
            ExecuteCommand.Subscribe(_ => ExecuteButtonExecute());
            CancelCommand.Subscribe(_ => CancelButtonExecute());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ExecuteButtonExecute() {
            MessageBox.Show("Executed.");
        }

        private void CancelButtonExecute() {
            MessageBox.Show("Canceled.");
        }
    }
}
