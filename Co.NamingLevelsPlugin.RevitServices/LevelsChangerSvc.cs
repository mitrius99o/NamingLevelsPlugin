using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Co.NamingLevelsPlugin.Contracts.DTO;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using System.Collections.Generic;

namespace Co.NamingLevelsPlugin.RevitServices
{
    public class LevelsChangerSvc : ILevelsChanger
    {
        private readonly UIApplication app;
        private readonly DocumentEventHandler eventHandler;
        private readonly ExternalEvent externalEvent;
        public LevelsChangerSvc(UIApplication app)
        {
            this.app = app;
            eventHandler = new DocumentEventHandler();
            externalEvent = ExternalEvent.Create(eventHandler);
        }

        public void ChangeLevels(IEnumerable<LevelDTO> levelsData)
        {
            //изменение через через транзакции в eventhandler
            Document doc = app.ActiveUIDocument.Document;

            eventHandler.TransactionName = "Изменение названий уровней";
            eventHandler.Func = () =>
            {
                //новые имена из LevelsData мы изменяем в ревите
                foreach(var level in levelsData)
                {
                    var element = doc.GetElement(new ElementId(level.LevelId));
                    if (element is Level lvl) lvl.Name = level.Name;
                }
                return true;
            };

            externalEvent.Raise();
        }
    }
}
