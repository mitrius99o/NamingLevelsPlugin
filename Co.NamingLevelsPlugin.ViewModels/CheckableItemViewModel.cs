using Co.NamingLevelsPlugin.ViewModels.Abstract;

namespace Co.NamingLevelsPlugin.ViewModels.Models
{
    public class CheckableItemViewModel<T> : Notifier
    {
        private bool isChecked = true;
        private T item;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public T Item
        {
            get { return item; }
            set
            {
                item = Item;
                OnPropertyChanged(nameof(Item));
            }
        }
        public CheckableItemViewModel(T item)
        {
            this.item = item;
        }

    }
}
