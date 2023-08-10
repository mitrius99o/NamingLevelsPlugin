using Autodesk.Revit.DB;
using Co.NamingLevelsPlugin.Contracts.DTO;

namespace Co.NamingLevelsPlugin.RevitServices.Mappers
{
    internal static class LevelsExtension
    {
        internal static LevelDTO ToDTO(this Level level) =>
            new LevelDTO()
            {
                Name = level.Name,
                Elevation = level.Elevation,
                LevelId = level.Id.IntegerValue
            };
    }
}
