using System;
using System.Windows.Input;

namespace Co.NamingLevelsPlugin.ViewModels.Commands
{
    internal class RelayCommand : ICommand
    {
        private Action execute;
        private Func<bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter = null) =>
            canExecute == null || canExecute();

        public void Execute(object parameter = null) =>
            execute();
    }
}
