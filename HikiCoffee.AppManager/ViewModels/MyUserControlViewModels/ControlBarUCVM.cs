using Prism.Mvvm;
using Prism.Commands;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System;

namespace HikiCoffee.AppManager.ViewModels.MyUserControlViewModels
{
    public class ControlBarUCVM : BindableBase
    {
        #region variable 

        private string? _colorChangeButtonClose;
        public string? ColorChangeButtonClose
        {
            get { return _colorChangeButtonClose; }
            set { SetProperty(ref _colorChangeButtonClose, value); }
        }

        private string? _packIconMaxnime;
        public string? PackIconMaxnime
        {
            get { return _packIconMaxnime; }
            set { SetProperty(ref _packIconMaxnime, value); }
        }

        #endregion

        #region DelegateCommand

        public ICommand CloseWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MouseMoveWindowCommand { get; set; }
        public ICommand MouseDoubleWindowCommand { get; set; }

        public ICommand MouseMoveChangeBackgroundButtonCloseCommand { get; set; }
        public ICommand MouseLeaveChangeBackgroundButtonCloseCommand { get; set; }

        #endregion

        public ControlBarUCVM()
        {
            ColorChangeButtonClose = "Black";
            PackIconMaxnime = "WindowMaximize";


            MouseMoveChangeBackgroundButtonCloseCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                ColorChangeButtonClose = "Red";
            });

            MouseLeaveChangeBackgroundButtonCloseCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                ColorChangeButtonClose = "Black";
            });

            CloseWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            });

            MaximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                SizeWindow(p);
            });

            MinimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else
                        w.WindowState = WindowState.Maximized;
                }
            });

            MouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });

            MouseDoubleWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                SizeWindow(p);
            });


        }



        public FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement? parent = p;

            while (parent!.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        public void SizeWindow(UserControl p)
        {
            FrameworkElement window = GetWindowParent(p);
            var w = window  as Window;

            if (w != null)
            {
                w.ResizeMode = ResizeMode.NoResize;
                if (w.WindowState != WindowState.Maximized)
                {
                    w.WindowStyle = WindowStyle.None;
                    w.WindowState = WindowState.Maximized;
                    PackIconMaxnime = "WindowRestore";
                }
                else
                {
                    w.ResizeMode = ResizeMode.CanResize;
                    w.WindowState = WindowState.Normal;
                    PackIconMaxnime = "WindowMaximize";
                }
            }
        }
    }

    public class RelayCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public RelayCommand(Predicate<T> canExecute, Action<T> execute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _canExecute = canExecute;
            _execute = execute;
        }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public bool CanExecute(object parameter)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            try
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }
            catch
            {
                return true;
            }
        }

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public void Execute(object parameter)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
