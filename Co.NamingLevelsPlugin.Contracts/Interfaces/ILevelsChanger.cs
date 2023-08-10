using Co.NamingLevelsPlugin.Contracts.DTO;
using System.Collections.Generic;

namespace Co.NamingLevelsPlugin.Contracts.Interfaces
{
    public interface ILevelsChanger
    {
        void ChangeLevels(IEnumerable<LevelDTO> levelsData);
    }
}
