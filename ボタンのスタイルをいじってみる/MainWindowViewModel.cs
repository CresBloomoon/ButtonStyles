using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ボタンのスタイルをいじってみる.Resources.Windows;
using ボタンのスタイルをいじってみる.Validations;

namespace ボタンのスタイルをいじってみる {
    internal class MainWindowViewModel : INotifyPropertyChanged {

        private readonly Window _window;

        public ReactiveCommandSlim ExecuteCommand { get; }
        public ReactiveCommandSlim CancelCommand { get; }

        [Required(ErrorMessage = "Required.")]
        public ReactiveProperty<string> InputText1 { get; }

        [Required(ErrorMessage = "Required")]
        [IntValidation(ErrorMessage = "整数を入力してください")]
        [Range(-100, 100, ErrorMessage = "範囲内の値を入力してください")]
        public ReactiveProperty<string> InputText2 { get; }

        [Required(ErrorMessage = "Required")]
        [DoubleValidation(ErrorMessage = "実数を入力してください")]
        [Range(-100.0, 100.0, ErrorMessage = "範囲内の値を入力してください")]
        public ReactiveProperty<string> InputText3 { get; }

        public ReadOnlyReactiveProperty<string> ErrorText1 { get; }
        public ReadOnlyReactiveProperty<string> ErrorText2 { get; }
        public ReadOnlyReactiveProperty<string> ErrorText3 { get; }

        public MainWindowViewModel(Window window = null) {

            _window = window;

            InputText1 = new ReactiveProperty<string>(mode: ReactivePropertyMode.Default | ReactivePropertyMode.IgnoreInitialValidationError).SetValidateAttribute(() => InputText1);
            InputText2 = new ReactiveProperty<string>(mode: ReactivePropertyMode.Default | ReactivePropertyMode.IgnoreInitialValidationError).SetValidateAttribute(() => InputText2);
            InputText3 = new ReactiveProperty<string>(mode: ReactivePropertyMode.Default | ReactivePropertyMode.IgnoreInitialValidationError).SetValidateAttribute(() => InputText3);

            ErrorText1 = InputText1.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();
            ErrorText2 = InputText2.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();
            ErrorText3 = InputText3.ObserveErrorChanged
                .Select(x => x?.Cast<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();

            ExecuteCommand = new[] {
                InputText1.ObserveHasErrors,
                InputText2.ObserveHasErrors,
                InputText3.ObserveHasErrors,
            }.CombineLatestValuesAreAllFalse()
             .ToReactiveCommandSlim();

            ExecuteCommand.Subscribe(() => ExecuteButtonExecute());

            CancelCommand = new ReactiveCommandSlim();
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
