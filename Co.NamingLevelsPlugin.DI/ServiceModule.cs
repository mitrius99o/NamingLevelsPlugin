using Autodesk.Revit.UI;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using Co.NamingLevelsPlugin.Contracts.InterfacesUI;
using Co.NamingLevelsPlugin.RevitServices;
using Co.NamingLevelsPlugin.ViewModels;
using Co.NamingLevelsPlugin.Views;
using System;

namespace Co.NamingLevelsPlugin.DI
{
    public class ServiceModule
    {
        private static readonly Lazy<ServiceModule> lazy = new Lazy<ServiceModule>(() => new ServiceModule());
        public static ServiceModule GetInstance() => lazy.Value;

        // Получение ссылки на VM
        public IViewModel GetViewModel() => new ViewModel();
        public IDocumentListener GetDocumentListener(UIApplication app) => new DocumentListener(app);
        public IViewModel GetViewModel(ILevelsLoader loader, ILevelsChanger changer) => new ViewModel(loader, changer);
        public IViewModel GetViewModel(ILevelsLoader loader, ILevelsChanger changer, IDocumentListener listener) => new ViewModel(loader, changer, listener);

        // Получение ссылки на V
        public IView GetView(IViewModel viewModel) => new MainWindowView(viewModel);

        // Получение ссылок на сервисы Revit
        public ILevelsLoader GetLevelsLoader(UIApplication app) => new LevelsLoaderSvc(app);
        public ILevelsChanger GetLevelsChanger(UIApplication app) => new LevelsChangerSvc(app);
    }
}
