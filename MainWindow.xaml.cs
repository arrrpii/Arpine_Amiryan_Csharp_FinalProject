using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace projectC
{

    public partial class MainWindow : Window
    {
        //_context is instance of NW class
        private readonly  NW _context = new NW();

        //creating CollectionViewSource for every table for faciliating data binding
        private CollectionViewSource productViewSource;
        private CollectionViewSource supplierViewSource;
        private CollectionViewSource orderDetailsViewSource;
        

        // creating empty property that doesn't contain any value;
        private AddProductWindow addProductPopupWindow;



        public MainWindow()
        {
            InitializeComponent(); //is responsible for initializing and setting up the UI components
            NW context = new NW();

            // These instances serve as intermediaries between the data source and UI controls
            productViewSource = (CollectionViewSource)FindResource(nameof(productViewSource));
            supplierViewSource = (CollectionViewSource)FindResource(nameof(supplierViewSource));
            orderDetailsViewSource = (CollectionViewSource)FindResource(nameof(orderDetailsViewSource));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _context.Database.EnsureCreated(); // being sure that the database specified in the _context DbContext instance is created and if it does not already exist

            LoadAllProducts();
            LoadAllSuppliers();
            LoadAllOrders();
        }

        // this method is responsible for loading all products from the database and binding them to the UI
        private void LoadAllProducts()
        {
            _context.Products.Load(); //loading Products from DB to context
            productViewSource.Source = _context.Products.Local.ToObservableCollection(); // binding with productViewSource and showing it in WPF
        }


        // this method is responsible for loading all orders from the database and binding them to the UI
        private void LoadAllOrders()
        {
            _context.OrderDetails.Load();
            orderDetailsViewSource.Source = _context.OrderDetails.Local.ToObservableCollection();
        }


        // this method is responsible for loading all suppliers from the database and binding them to the UI
        private void LoadAllSuppliers()
        {
            _context.Suppliers.Load();
            supplierViewSource.Source = _context.Suppliers.Local.ToObservableCollection();
        }




        // this search button calls PerformSearch() method which filteres and gives back searcged products
        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }


        // search in Product's table
        private void PerformSearch()
        {
            //creating new Product list for saving filtered and searched product's data
            List<Product> filtereddata = new List<Product>();
            string selecteditem = searchField.Text as string; // taking selected text from combobox
            if (string.IsNullOrEmpty(selecteditem))
                return;

            //taking text from textbox
            string searchTerm = searchText.Text;
            int intSearchTerm;       //for insuring that it is not string but int 
            bool isInt = false;

            switch (selecteditem)
            {

                case "Product Name":
                    filtereddata = _context.Products.Where(p => p.ProductName != null && p.ProductName.Contains(searchTerm)).ToList();
                    break;


                case "Product ID":
                    isInt = int.TryParse(searchTerm, out intSearchTerm);
                    if (isInt)
                    {
                        filtereddata = _context.Products.Where(p => p.ProductID.Equals(intSearchTerm)).ToList();
                    }
                    break;


                case "Supplier ID":
                    isInt = int.TryParse(searchTerm, out intSearchTerm);
                    if (isInt)
                    {
                        filtereddata = _context.Products.Where(p => p.SupplierID.Equals(intSearchTerm)).ToList();
                    }
                    break;


                case "Category ID":
                    isInt = int.TryParse(searchTerm, out intSearchTerm);
                    if (isInt)
                    {
                        filtereddata = _context.Products.Where(p => p.CategoryID.Equals(intSearchTerm)).ToList();
                    }
                    break;
            }

            //showing in product data grid already filtered and searched data
            productDataGrid.ItemsSource = filtereddata;
        }




        //button for adding new product and opening new xaml popup window
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            // creating new object (value) and assigning it to property.
            // now property has value.
            addProductPopupWindow = new AddProductWindow();

            // connecting Close event of child window(AddProduct) to function of main window //Subscribing to the Closed event
            addProductPopupWindow.Closed += AddProductPopupWindow_Closed;  
            addProductPopupWindow.Show();
        }

   
        //adding and saving added product into _context
        private void AddProductPopupWindow_Closed(object sender, EventArgs e)
        {
            Product addedProduct = addProductPopupWindow.GetProduct();

            try
            {
                if (addedProduct != null)
                {
                    _context.Products.Add(addedProduct);
                    _context.SaveChanges();

                }
            }catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }




        //button that calls  PerformCountrySearch();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PerformCountrySearch();
        }


        //this method searfches suppliers and shows them with selected country 
        private void PerformCountrySearch()
        {
            //crrating new supplier list for adding there filtered data
            List<Supplier> filteredcountry = new List<Supplier>();
            string selectedcountry = searchcountry.Text as string;
            if (string.IsNullOrEmpty(selectedcountry))
                return;

            switch (selectedcountry)
            {
                case "Australia":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Brazil":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Canada":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Denmark":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Finland":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "France":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Germany":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Italy":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Japan":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Netherlands":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Norway":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Syngapore":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Spain":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "Sweden":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "UK":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;

                case "USA":
                    filteredcountry = _context.Suppliers.Where(s => s.Country != null && s.Country.Contains(selectedcountry)).ToList();
                    break;
            }
            //showing filtered data in datagrid
            supplierDataGrid.ItemsSource = filteredcountry;
        }





        //button for calling PerformIDSearch();
        private void Button_Click_SearchID(object sender, RoutedEventArgs e)
        {
            PerformIDSearch();
        }

        private void PerformIDSearch()
        {
            //creating OrderDetail new list for filtered data
            List<OrderDetail> filteredID = new List<OrderDetail>();
            int productId;
            int totalQuantity = 0;
            decimal totalPrice = 0;
            if (int.TryParse(searchID.Text, out productId)) // converting texbox's content into  int 
            {
                filteredID = _context.OrderDetails.Where(od => od.ProductID.Equals(productId)).ToList(); //finding order with searcged product ID
                totalQuantity = filteredID.Sum(od => od.Quantity); //summorizing and giving total quantity of current product, that have been already sold
                totalPrice = filteredID.Sum(od => od.UnitPrice);
            }
            orderDataGrid.ItemsSource = filteredID;
            System.Windows.Forms.MessageBox.Show($"Total quantity sold for Product ID {productId}: {totalQuantity} - Total amount sold {totalPrice}");//informing the user about quantity via message box
        }





        //cancle buttons for all tables, that after queries user could call back full tables of products, suppliers and oder details

        private void Button_Click_Cancle_Product(object sender, RoutedEventArgs e)
        {
            productDataGrid.ItemsSource = _context.Products.ToList();
            searchText.Text = ""; // Clearing searchtext textBox
        }

        private void Button_Click_Cancle_Supplier(object sender, RoutedEventArgs e)
        {
            supplierDataGrid.ItemsSource = _context.Suppliers.ToList();
            searchcountry.Text = ""; // Clearing searchcountry combobox 
        }

        private void Button_Click_Cancle_Order(object sender, RoutedEventArgs e)
        {
            orderDataGrid.ItemsSource = _context.OrderDetails.ToList();
            searchID.Text = ""; // Clearing searchID textBox
        }
    }
}
