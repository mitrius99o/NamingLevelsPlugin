using Co.NamingLevelsPlugin.Contracts.DTO;
using System.Collections.Generic;

namespace Co.NamingLevelsPlugin.Contracts.EventArgs
{
    public class ChangedLevelsEventArgs : System.EventArgs
    {
        public IEnumerable<LevelDTO> ModifiedLevels { get; }
        public IEnumerable<LevelDTO> DeletedLevels { get; }
        public IEnumerable<LevelDTO> AddedLevels { get; }
        public ChangedLevelsEventArgs(IEnumerable<LevelDTO> modifiedLevels, IEnumerable<LevelDTO> addedLevels, IEnumerable<LevelDTO> deletedLevels)
        {
            ModifiedLevels = modifiedLevels;
            DeletedLevels = deletedLevels;
            AddedLevels = addedLevels;
        }
    }
}
