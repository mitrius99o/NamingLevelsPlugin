using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Co.NamingLevelsPlugin.Contracts.DTO;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using Co.NamingLevelsPlugin.RevitServices.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace Co.NamingLevelsPlugin.RevitServices
{
    public class LevelsLoaderSvc : ILevelsLoader
    {
        private readonly UIApplication app;
        public LevelsLoaderSvc(UIApplication app)
        {
            this.app = app;
        }
        public IEnumerable<LevelDTO> LoadLevels()
        {
            // Получаем коллектор
            FilteredElementCollector collector = 
                new FilteredElementCollector(app.ActiveUIDocument.Document, app.ActiveUIDocument.ActiveView.Id);
            // Фильтруем через коллектор
            IEnumerable<Level> lvls = collector.OfClass(typeof(Level)).OfType<Level>();
            // Кастим коллекцию в LevelDTO
            return lvls.Select(e => e.ToDTO());
        }
    }
}
