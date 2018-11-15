using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.ObjectModel;

namespace pt_lab_2
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class NorthwndViewModel : ViewModel
    {
        #region var
        #region fields
        bool isOrdersLoaded = false;
        bool isCustomersLoaded = false;
        #endregion fields

        #region properties
        public NORTHWNDEntities1 Entities { get; private set; }
        public ObservableCollection<Order> Orders
        {
            get
            {
                if (!isOrdersLoaded)
                {
                    Entities.Orders.Load();
                    isOrdersLoaded = true;
                }

                return Entities.Orders.Local;
            }
        }

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                if (_selectedOrder != value)
                {
                    _selectedOrder = value;
                    NotifyPropertyChanged("SelectedOrder");
                }
            }
        }

        public ObservableCollection<Customer> Customers
        {
            get
            {
                if (!isCustomersLoaded)
                {
                    Entities.Customers.Load();
                    //Console.WriteLine(Entities.Customers.Count());
                    foreach (var item in Entities.Customers.Local) Console.WriteLine(item);
                    isCustomersLoaded = true;
                }
                return Entities.Customers.Local;
            }

        }
        #endregion properties
        #endregion var

        public NorthwndViewModel()
        {
            Entities = new NORTHWNDEntities1();
        }
    }
}
