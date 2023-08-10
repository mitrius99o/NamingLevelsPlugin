using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Co.NamingLevelsPlugin.RevitApp
{
    public class Application : IExternalApplication
    {
        private readonly string iconsDirectoryPath = $@"/{typeof(Application).Assembly.GetName().Name};component/Resources/icons/";
        private readonly string picCLNC = @"change_levels.png";
        private readonly string picToolTipCLNC = @"change_levels.png";
        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string cmdPath = Path.Combine(Path.GetDirectoryName(assemblyLocation), @"Co.NamingLevelsPlugin.Commands.dll");
            string tabName = "BIM Academy";

            try { application.CreateRibbonTab(tabName); }
            catch { }

            try
            {
                RibbonPanel panel = application.CreateRibbonPanel(tabName, "������ � ��������");

                // �������� ������ ������
                List<PushButtonData> buttonData = new List<PushButtonData>()
                {
                    new PushButtonData("CLNC", "��������\n����� �������", cmdPath, "Co.NamingLevelsPlugin.Commands.ChangeLevelsNamesCommand")
                    {
                        LargeImage = new BitmapImage(new Uri(Path.Combine(iconsDirectoryPath, picCLNC), UriKind.RelativeOrAbsolute)),
                        ToolTipImage = new BitmapImage(new Uri(Path.Combine(iconsDirectoryPath, picToolTipCLNC), UriKind.RelativeOrAbsolute)),
                        AvailabilityClassName = "Co.NamingLevelsPlugin.Commands.ChangeLevelsNamesCommandAvailability",
                        ToolTip = "������ ������������ ��� ��������������� ���������� ������� � ������������ � �� ���������� � ������� ������������� ��������"
                    }
                };

                foreach (PushButtonData bd in buttonData) panel.AddItem(bd);

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
            finally
            {
                //����������� ����������
            }
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
