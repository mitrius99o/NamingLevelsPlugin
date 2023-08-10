using Co.NamingLevelsPlugin.Contracts.DTO;
using System;
using System.Collections.Generic;

namespace Co.NamingLevelsPlugin.Contracts.InterfacesUI
{
    public interface IViewModel
    {
        IViewModel ConfigData();
        void SetData(IEnumerable<LevelDTO> levelsData);
        event EventHandler<object> OnWindowEndEvent;
    }
}
