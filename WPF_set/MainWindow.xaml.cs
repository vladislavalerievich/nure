using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessInput()) // if no errors, then we can proceed
            {
                CalculateResult();
            }
        }

        int[] setA;
        int[] setB;

        private bool ProcessInput()
        {
            bool flag = true;
            string stringSetA = inputSetA.Text;
            string stringSetB = inputSetB.Text;

            // separate by white-space and remove white-space between characters
            string[] arrStrSetA = stringSetA.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries); 
            string[] arrStrSetB = stringSetB.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                // check if one or both of inputs are empty
                if (String.IsNullOrEmpty(stringSetA) || String.IsNullOrEmpty(stringSetB))
                {
                    throw new ArgumentNullException("Both sets should have at least one integer");
                }

                // check if any of RadioButtons were chosen 
                if (!(radioButtonStackPanel.Children.OfType<RadioButton>().Any(rb => rb.IsChecked == true)))
                {
                    throw new ArgumentNullException("You have to select an option");
                }

                // try to convert an array of strings into int
                setA = Array.ConvertAll(arrStrSetA, int.Parse);
                setB = Array.ConvertAll(arrStrSetB, int.Parse);
            }
            catch (Exception ex)
            {
                flag = false;
                MessageBox.Show(ex.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return flag;
        }

        public void CalculateResult()
        {
            string msg = "";
            HashSet<int> hashSetA = new HashSet<int>(setA);
            HashSet<int> hashSetB = new HashSet<int>(setB);

            if (radioBtnUnion.IsChecked == true)
            {
                hashSetA.UnionWith(hashSetB);
            }
            else if (radioBtnDifference.IsChecked == true)
            {
                hashSetA.ExceptWith(hashSetB);
            }
            else if (radioBtnIntersection.IsChecked == true)
            {
                hashSetA.IntersectWith(hashSetB);
            }

            msg = string.Join(" ", hashSetA);
            MessageBox.Show(msg);
        }      
    }
}
