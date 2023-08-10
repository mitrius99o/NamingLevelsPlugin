using Co.NamingLevelsPlugin.Contracts.DTO;
using Co.NamingLevelsPlugin.ViewModels.Models;

namespace Co.NamingLevelsPlugin.ViewModels.Mappers
{
    public static class LevelsExtension
    {
        public static LevelDTO ToDTO(this LevelModel levelModel)
        {
            return new LevelDTO()
            {
                Name = levelModel.NewName,
                LevelId = levelModel.LevelId,
                Elevation = levelModel.Elevation
            };
        }
        public static LevelModel ToModel(this LevelDTO levelDTO)
        {
            return new LevelModel(levelDTO.LevelId, levelDTO.Name, levelDTO.Elevation);
        }

    }
}
