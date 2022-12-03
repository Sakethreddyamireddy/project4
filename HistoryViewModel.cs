using Calculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class HistoryViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ExpressionHistoryModel> items;
        private DatabaseConnection database;
        public HistoryViewModel()
        {
            items = new ObservableCollection<ExpressionHistoryModel>();
            database = new DatabaseConnection();
            initiateData();
        }

        public async void Add(String item)
        {
            var historyModel = new ExpressionHistoryModel();
            historyModel.expression = item;
            await database.SaveItemAsync(historyModel);
            await initiateData();
            OnPropertyChanged("history");
            OnPropertyChanged("history");
            OnPropertyChanged();
            OnPropertyChanged();
        }

        public async Task initiateData()
        {
            var res = await database.GetHistoryAsync();
           items = new ObservableCollection<ExpressionHistoryModel>();
            res.ForEach(items.Add);
        }

        public async Task clearDatabase()
        {
            items = new ObservableCollection<ExpressionHistoryModel>();
            OnPropertyChanged("history");
            await database.DeleteAllAsync();
            await initiateData();
            OnPropertyChanged("history");
            OnPropertyChanged();
            OnPropertyChanged();
        }


        public ObservableCollection<ExpressionHistoryModel> history
        {
            get => items;
        }

        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
