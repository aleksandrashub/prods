using Avalonia.Controls;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ProductsManufacturer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsManufacturer
{
    public partial class MainWindow : Window
    {
        public List<Tovar> tovar = new List<Tovar>();
        public List<Sale> sales = new List<Sale>();
        public List<TovarDop> tovarDops = new List<TovarDop>();
        public List<string> manufacturers = new List<string>();
        public List<Status> statuses = new List<Status>();
        public MainWindow()
        {
            InitializeComponent();
            loadServices();
        }

        public void loadServices()
        {
            manufacturers.Add("Все производители");
            manufacturers.AddRange(Helper.User724Context.Manufacturers.Select(x => x.NameManufacturer).ToList());
            
            filter.ItemsSource = manufacturers;

            tovar = Helper.User724Context.Tovars.Include(z => z.IdManufacturerNavigation).ToList();
            sales = Helper.User724Context.Sales.ToList();
            statuses = Helper.User724Context.Statuses.ToList();



            string searchText = search.Text ?? "";
            int count = searchText.Split(' ').Length;
            string[] values = new string[count];

            values = searchText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in values)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    tovar = tovar.Where(x => x.NameTovar.Contains(s)
                    || x.Description.Contains(s)).ToList();
                }
                else
                {
                    continue;
                }
            }

            if (filter.SelectedIndex == 0)
            {
            }
            else if (filter.SelectedValue != null)
            {
                string selectedMan = filter.SelectedValue.ToString();
                tovar = (List<Tovar>)tovar.Where(x => x.IdManufacturerNavigation.NameManufacturer.ToString() == selectedMan).ToList();

            }


            switch (sort.SelectedIndex)
            { 
            case 0:
                    tovar = tovar.OrderBy(x => x.Cost).ToList();
                    break;
            case 1:
                    tovar = tovar.OrderByDescending(x => x.Cost).ToList();
                    break;
            }



        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            loadServices ();
        }

        private void ComboBox_SelectionChanged_1(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            loadServices();
        }

        private void search_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {

            loadServices();

        }

    }
}