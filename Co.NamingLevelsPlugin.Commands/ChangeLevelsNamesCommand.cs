using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using Co.NamingLevelsPlugin.Contracts.InterfacesUI;
using Co.NamingLevelsPlugin.DI;
using System;

namespace Co.NamingLevelsPlugin.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class ChangeLevelsNamesCommand : IExternalCommand
    {
        IView view;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Получаем DI
                ServiceModule serviceModule = ServiceModule.GetInstance();

                // Получаем интерфейсы конкретных сервисов loader'a и chsnger'a
                ILevelsLoader levelsLoader = serviceModule.GetLevelsLoader(commandData.Application);
                ILevelsChanger levelsChanger = serviceModule.GetLevelsChanger(commandData.Application);

                // Получаем интерфейс V-M
                IDocumentListener docListener = serviceModule.GetDocumentListener(commandData.Application);
                IViewModel viewModel = serviceModule.GetViewModel(levelsLoader, levelsChanger, docListener);
                viewModel.OnWindowEndEvent += ViewModel_OnWindowEndEvent;

                // Запускаю инициализацию данных из Revit
                viewModel.ConfigData();

                // Получаем интерфейс V и запускаем V
                view = serviceModule.GetView(viewModel);
                view.SetOwner(commandData.Application.MainWindowHandle);
                view.Show();


                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog taskDialog = new TaskDialog(ex.GetType().ToString())
                {
                    MainContent = ex.Message,
                    ExpandedContent = ex.StackTrace
                };

                taskDialog.Show();
                return Result.Failed;
            }

        }
        // Делегат, который закрывает V
        private void ViewModel_OnWindowEndEvent(object sender, object e)
        {
            if (e is bool result)
            {
                if (result) { view?.Close(); }
                else { throw new Exception(); }
            }
            else 
            { 
                throw new Exception(); 
            }
        }
    }
}
