using Co.NamingLevelsPlugin.Contracts.DTO;
using Co.NamingLevelsPlugin.Contracts.EventArgs;
using Co.NamingLevelsPlugin.Contracts.Interfaces;
using Co.NamingLevelsPlugin.Contracts.InterfacesUI;
using Co.NamingLevelsPlugin.ViewModels.Abstract;
using Co.NamingLevelsPlugin.ViewModels.Commands;
using Co.NamingLevelsPlugin.ViewModels.Mappers;
using Co.NamingLevelsPlugin.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Co.NamingLevelsPlugin.ViewModels
{
    public class ViewModel : Notifier, IViewModel
    {
        public event EventHandler<object> OnWindowEndEvent;
        private ObservableCollection<CheckableItemViewModel<LevelModel>> items;

        private string upperPrefix;
        private string lowerPrefix;
        private ILevelsChanger levelsChanger;
        private ILevelsLoader levelsLoader;
        private IDocumentListener docListener;
        public ObservableCollection<CheckableItemViewModel<LevelModel>> Items
        {
            get { return items; }
            set 
            { 
                items = value;
                OnPropertyChanged();
            }
        }
        public string UpperPrefix
        {
            get { return upperPrefix; }
            set
            {
                upperPrefix = value;
                OnPropertyChanged(nameof(UpperPrefix));
            }
        }
        public string LowerPrefix
        {
            get { return lowerPrefix; }
            set
            {
                lowerPrefix = value;
                OnPropertyChanged(nameof(LowerPrefix));
            }
        }
        public ICommand AcceptCommand { get; set; }
        public ICommand PreviewCommand { get; set; }
        public ViewModel() { }
        // Этот конструктор используется в DI. Оставил, чтобы не менять DI в этой ветке
        public ViewModel(ILevelsLoader loader, ILevelsChanger changer)
        {
            levelsLoader = loader;
            levelsChanger = changer;
        }
        public ViewModel(ILevelsLoader loader, ILevelsChanger changer, IDocumentListener listener)
        {
            levelsLoader = loader;
            levelsChanger = changer;
            docListener = listener;
            // Здесь подписаться на событие ChangeLevel, а так же проинициализировать docListener

            docListener.DocumentChanged += DocumentListener_DocumentChanged;
        }
        public void SetData(IEnumerable<LevelDTO> levelsData)
        {
            Items = new ObservableCollection<CheckableItemViewModel<LevelModel>>
                (levelsData.Select(l => new CheckableItemViewModel<LevelModel>(LevelsExtension.ToModel(l))));

            AcceptCommand = new RelayCommand(AcceptActionChanges);
            PreviewCommand = new RelayCommand(AcceptActionPreviewChanges);
        }

        public IViewModel ConfigData()
        {
            SetData(levelsLoader.LoadLevels());
            return this;
        }
        private void AcceptActionChanges() =>
            // здесь работа с changer 
            levelsChanger.ChangeLevels(Items.Where(i => i.IsChecked).Select(i => LevelsExtension.ToDTO(i.Item)));
        // Starts when pushed button "Preview"
        private void AcceptActionPreviewChanges()
            // здесь отображение в textblock-ах "Новое имя" без присвоения
        {
            // update ElevationText only
            foreach (var item in Items.Where(i => i.IsChecked))
            {
                if (item.Item.Elevation >= 0)
                    item.Item.NewName = UpperPrefix + item.Item.ElevationText;
                else
                    item.Item.NewName = LowerPrefix + item.Item.ElevationText;
            }
            foreach (var item in Items.Where(i => !i.IsChecked)) item.Item.NewName = "";
        }

        // Apears when changer works and user changes in Revit
        private void DocumentListener_DocumentChanged(object sender, ChangedLevelsEventArgs e)
            // выполняется при возникновении изменения в document listener
        {
            // DELETE --
            // readyToDelete created from Items and e.DeletedLevels
            var readyToDelete = Items.Where(i => e.DeletedLevels.Select(l => l.LevelId).Contains(i.Item.LevelId));
            // delete readyToDelete from Items
            foreach (var i in Items.Where(o => readyToDelete.Any(l => l.Item.LevelId == o.Item.LevelId)))
                Items.Remove(i);

            // UPDATE --
            // update by newName
            Items.Where(i => e.ModifiedLevels.Select(l => l.LevelId)
                 .Contains(i.Item.LevelId))
                 .ToList().ForEach(c => 
                 { 
                     //new name
                     c.Item.CurrentName = c.Item.NewName; 
                     //new elevation
                     c.Item.Elevation = e.ModifiedLevels
                                         .ToList()
                                         .Find(m => m.LevelId == c.Item.LevelId).Elevation;
                 });
            AcceptActionPreviewChanges();

            // CREATE --
            // add e.AddedLevels
            foreach (var i in e.AddedLevels)
                Items.Add(new CheckableItemViewModel<LevelModel>(LevelsExtension.ToModel(i)));

        }
    }
}
