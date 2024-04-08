using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace projectC
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private Product product;

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct() { return product; }  //This method returns the product field, and allows other parts of your application to access the product after it's been added

        public AddProductWindow()
        {
            InitializeComponent();
            product = new Product(); //Instantiating new Product object and assigning it to the product field.
        }

        
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            try
            {
                product = new Product  // to create a new Product object based on the data entered by the user in the textboxes.
                {
                    ProductName = addName.Text,
                    SupplierID = int.Parse(addSupID.Text),
                    CategoryID = int.Parse(addCatID.Text),
                    UnitPrice = decimal.Parse(addPrice.Text),
                    UnitsInStock = short.Parse(addStock.Text),
                    UnitsOnOrder = short.Parse(addOrder.Text),
                    ReorderLevel = short.Parse(addReorder.Text),
                    Discontinued = bool.Parse(addDis.Text)
                };

                this.Close();
            }
            catch(Exception ex)
            {
                product = null;
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            
        }
        //button for cancling the product adding
        private void Button_Click_Cancle(object sender, RoutedEventArgs e)
        {
            product = null;
            this.Close();
        }

    }
}
