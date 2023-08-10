using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Co.NamingLevelsPlugin.Contracts.DTO;
using Co.NamingLevelsPlugin.Contracts.EventArgs;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using Co.NamingLevelsPlugin.RevitServices.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Co.NamingLevelsPlugin.RevitServices
{
    public class DocumentListener : IDocumentListener
    {
        public event EventHandler<ChangedLevelsEventArgs> DocumentChanged;
        public DocumentListener(UIApplication app)
        {
            app.Application.DocumentChanged += RevitDocument_Changed;
        } 
        public IEnumerable<LevelDTO> GetChangedLevelsFromRevit(ICollection<ElementId> elements, Document doc) =>
            elements.Select(x => doc.GetElement(x)).OfType<Level>().Select(x => x.ToDTO());
        public void RevitDocument_Changed(object sender, DocumentChangedEventArgs e)
        {
            Document doc = e.GetDocument();
            IEnumerable<LevelDTO> addedElements = GetChangedLevelsFromRevit(e.GetAddedElementIds(), doc);
            IEnumerable<LevelDTO> deletedElements = GetChangedLevelsFromRevit(e.GetDeletedElementIds(), doc); 
            IEnumerable<LevelDTO> modifElements = GetChangedLevelsFromRevit(e.GetModifiedElementIds(), doc); 

            OnDocumentChanged(new ChangedLevelsEventArgs(modifElements, addedElements, deletedElements));
        }
        public void OnDocumentChanged(ChangedLevelsEventArgs e)
        {
            DocumentChanged?.Invoke(this, e);
        }
    }
}
