using System.Windows.Input;

namespace VappsMobile.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        private readonly Action<AsyncCommandBase, Exception> _onException;
        public event EventHandler CanExecuteChanged;

        private bool _isExecuting;

        public bool IsExecuting
        {
            get => _isExecuting;
            set { _isExecuting = value; CanExecuteChanged?.Invoke(this, new EventArgs()); }
        }

        public AsyncCommandBase(Action<AsyncCommandBase, Exception> onException)
        {
            _onException = onException;
        }

        public bool CanExecute(object parameter)
        {
            return !IsExecuting;
        }

        protected abstract Task ExecuteAsync(object parameter);

        public async void Execute(object parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                if (_onException != null)
                {
                    _onException?.Invoke(this, ex);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                IsExecuting = false;
            }
        }
    }
}
