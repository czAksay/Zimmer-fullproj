using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimmerViewer.Model;
using System.Windows.Input;
using System.Windows;

namespace ZimmerViewer.ViewModel
{
    class PartsViewModel
    {
        private PartsModel model;

        //Строки-фильтры, заполненные пользователем во View. 
        //Доступны в виде свойств для соответствия паттерну MVVM.
        private String partCodeFilter = "";
        private String partNameFilter = "";
        private String linkPartCodeFilter = "";
        public String PartCode
        {
            get { return partCodeFilter; }
            set
            {
                if (value == partCodeFilter)
                    return;
                else
                {
                    partCodeFilter = value;
                    FiltersChanged();
                }
            }
        }
        public String PartName
        {
            get { return partNameFilter; }
            set
            {
                if (value == partNameFilter)
                    return;
                else
                {
                    partNameFilter = value;
                    FiltersChanged();
                }
            }
        }
        public String LinkPartCode
        {
            get { return linkPartCodeFilter; }
            set
            {
                if (value == linkPartCodeFilter)
                    return;
                else
                {
                    linkPartCodeFilter = value;
                    FiltersChanged();
                }
            }
        }

        public PartsModel Model { get { return model; } }

        //При изменении пользователем одного из фильтров, обновляем список, вызывая
        //соответствующую хранимую процедуру в БД и передавая эти фильтры в качестве аргументов.
        private void FiltersChanged()
        {
            model.RefreshParts(partCodeFilter, partNameFilter, linkPartCodeFilter);
        }

        public PartsViewModel()
        {
            model = new PartsModel();
        }
    }
}
