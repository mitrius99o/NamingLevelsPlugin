using Co.NamingLevelsPlugin.Contracts.EventArgs;
using System;

namespace Co.NamingLevelsPlugin.Contracts.Interfaces
{
    public interface IDocumentListener
    {
        event EventHandler<ChangedLevelsEventArgs> DocumentChanged;
        void OnDocumentChanged(ChangedLevelsEventArgs e);
    }
}
