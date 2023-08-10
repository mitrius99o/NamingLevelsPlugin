using Co.NamingLevelsPlugin.ViewModels.Abstract;

namespace Co.NamingLevelsPlugin.ViewModels.Models
{
    public class LevelModel : Notifier
    {
        private int levelId;
        private string currentName;
        private string newName;
        private double elevation;

        public int LevelId
        {
            get { return levelId; }
            set 
            { 
                levelId = value;
                OnPropertyChanged(nameof(LevelId));
            }
        }
        public string CurrentName
        {
            get { return currentName; }
            set 
            { 
                currentName = value;
                OnPropertyChanged(nameof(CurrentName));
            }
        }
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }
        public double Elevation
        {
            get { return elevation; }
            set 
            { 
                elevation = value;
                OnPropertyChanged(nameof(Elevation));
                OnPropertyChanged(nameof(ElevationText));
            }
        }
        public string ElevationText
        {
            get
            {
                double formula = elevation * 0.3048;
                if (formula > 0) return string.Format("+{0:0.000}", formula).Replace(",", ".");
                else if (formula < 0) return string.Format("{0:0.000}", formula).Replace(",", ".");
                else return string.Format("{0:0.000}", elevation).Replace(",", ".");
            }
        }
        public LevelModel(int id, string name, double el)
        {
            levelId = id;
            currentName = name;
            elevation = el;
        }
    }
}
