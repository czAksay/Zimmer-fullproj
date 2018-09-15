using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace ZimmerViewer.Model
{
    //Класс запчастей для хранения их в коллекции.
    //Содержат три поля для каждой колонки во View.
    public class Parts
    {
        public string PartCodeAndBrand { get; set; }
        public string PartName { get; set; }
        public string LinkPartCodeAndBrand { get; set; }
        public Parts(string _partCodeAndBrand, string _name, string _linkPartCodeAndBrand)
        {
            PartCodeAndBrand = _partCodeAndBrand;
            PartName = _name;
            LinkPartCodeAndBrand = _linkPartCodeAndBrand;
        }
    }
    
    class PartsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Parts> partsList;

        public ObservableCollection<Parts> PartsList
        {
            get { return partsList; }
            set {
                partsList = value;
                OnPropertyChanged();
            }
        }

        public PartsModel()
        {
            partsList = new ObservableCollection<Parts>();
            RefreshParts("","","");
        }

        //Обновление коллекции деталей с помощью вызова соответствующей хранимой процедуры
        public void RefreshParts(String partCodeFilter, String partNameFilter, String linkPartCodeFilter)
        {
            partsList.Clear();
            PartsList = Sql.GetData(partCodeFilter, partNameFilter, linkPartCodeFilter);
        }

        //при изменении свойства (коллекции) - оповещаем View.
        private void OnPropertyChanged ([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
