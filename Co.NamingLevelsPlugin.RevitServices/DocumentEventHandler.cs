using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;

namespace Co.NamingLevelsPlugin.RevitServices
{
    public class DocumentEventHandler : IExternalEventHandler
    {
        private Func<object> _func;
        private string _trans;
        /// <summary>
		/// исполняемый в транзакции метод
		/// </summary>
        public Func<object> Func 
        {
            get => _func;
            set => _func = value ??
                throw new ArgumentNullException();
        }

        /// <summary>
		/// Исключение, возникшее в процессе работы метода Func
		/// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
		/// имя транзакции, по умолчанию - 
		/// </summary>
        public string TransactionName 
        {
            get => string.IsNullOrEmpty(_trans) ? GetName() : _trans;
            set => _trans = value;
        }

        /// <summary>
		/// информирует о завершении работы метода Execute
		/// </summary>
        public event EventHandler<object> EventCompleted;

        public void Execute(UIApplication app)
        {
            object result = null;

            string transactionName = string.IsNullOrEmpty(TransactionName) ? GetName() : TransactionName;
            using (Transaction t = new Transaction(app.ActiveUIDocument.Document, transactionName))
            {
                try
                {
                    t.Start();
                    result = Func();
                    t.Commit();
                }
                catch (Exception ex)
                {
                    t.RollBack();
                    Exception = ex;
                }
            }
            EventCompleted?.Invoke(this, result);
        }

        public string GetName() => nameof(DocumentEventHandler);

    }
}
