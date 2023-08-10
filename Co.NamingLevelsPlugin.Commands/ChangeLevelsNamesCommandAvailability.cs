using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Co.NamingLevelsPlugin.Commands
{
    public class ChangeLevelsNamesCommandAvailability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories) =>
                   applicationData.ActiveUIDocument?.ActiveView is not ViewSchedule
                && applicationData.ActiveUIDocument?.ActiveView.ViewType != ViewType.DrawingSheet
                && applicationData.ActiveUIDocument?.ActiveView.ViewType != ViewType.DraftingView
                && applicationData.ActiveUIDocument?.ActiveView.ViewType != ViewType.Legend
                && applicationData.ActiveUIDocument?.ActiveView.ViewType != ViewType.ProjectBrowser;
            
    }
}
