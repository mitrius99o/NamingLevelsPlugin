using Co.NamingLevelsPlugin.Contracts.DTO;
using System.Collections.Generic;

namespace Co.NamingLevelsPlugin.Contracts.Interfaces
{
    public interface ILevelsLoader
    {
        IEnumerable<LevelDTO> LoadLevels();
    }
}
