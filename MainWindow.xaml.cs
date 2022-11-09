using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }

        private void BindCurrency()
        {
            DataTable currencyTable = new DataTable();
            currencyTable.Columns.Add("Text");
            currencyTable.Columns.Add("Value");

            currencyTable.Rows.Add("--SELECT--", 0);
            currencyTable.Rows.Add("GBP", 1);
            currencyTable.Rows.Add("USD", 1.15);
            currencyTable.Rows.Add("EUR", 1.14);
            currencyTable.Rows.Add("XCD", 3.11);
            currencyTable.Rows.Add("CAD", 1.55);

            cmbFrom.ItemsSource = currencyTable.DefaultView;
            cmbFrom.DisplayMemberPath = "Text";
            cmbFrom.SelectedValuePath = "Value";
            cmbFrom.SelectedIndex = 0;

            cmbTo.ItemsSource = currencyTable.DefaultView;
            cmbTo.DisplayMemberPath= "Text";
            cmbTo.SelectedValuePath= "Value";
            cmbTo.SelectedIndex = 0;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Convert_Button(object sender, RoutedEventArgs e)
        {
            double convertedValue;
            string quote = "\"";

            if (tbEnterAmount == null || tbEnterAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter An Amount To Convert", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);

                tbEnterAmount.Focus();
                return;
            }

            else if (cmbFrom.SelectedValue == null || cmbFrom.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select A " + quote + "From" + quote + " Currency", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);

                cmbFrom.Focus();
                return;
            }

            else if (cmbTo.SelectedValue == null || cmbTo.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select A " + quote + "To" + quote + " Currency", "Warning",
                                MessageBoxButton.OK, MessageBoxImage.Exclamation);

                cmbTo.Focus();
                return;
            }

            if (cmbFrom.Text == cmbTo.Text)
            {
                convertedValue = double.Parse(tbEnterAmount.Text);

                lbResults.Content = cmbTo.Text + " " + convertedValue.ToString("N3");
            }

            else
            {
                convertedValue = double.Parse(cmbFrom.SelectedValue.ToString()) *
                                 double.Parse(tbEnterAmount.Text) /
                                 double.Parse(cmbTo.SelectedValue.ToString());

                lbResults.Content = cmbTo.Text + " " + convertedValue.ToString("N3");
            }
        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            lbResults.Content = "Conversion Shows Here";
            tbEnterAmount.Text = String.Empty;
            tbEnterAmount.Focus();
            cmbFrom.SelectedIndex = 0;
            cmbTo.SelectedIndex = 0;
        }
    }
}
