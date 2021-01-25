using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Singleton
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SingletonInformation SI = SingletonInformation.SI_Inst;
            TBInformace.Text = "";
            TBInformace.Text = SI.View(0);
            TBInformace.Text = SI.View(1);
            TBInformace.Text = SI.View(2);
        }

        public class SingletonInformation 
        {
            static readonly object LockingObject = new object();
            public static Dictionary<int, string[]> Seznam;
            static SingletonInformation SI = null;
            string[] SeznamInformace;
            string Text = string.Empty;
            string datumAlex = "3.1.2002";
            string datumPetr = "15.4.2002";
            string datumJirka = "24.1.2002";
            string[] Datum;
            char[] Rok;
            Random nc = new Random();
            string[] Create(string Name, string LastName, string DateOB, string PIN) 
            {
                SeznamInformace = new string[] { Name, LastName, DateOB, PIN };
                return SeznamInformace;
            }
            SingletonInformation() 
            {
                Seznam = new Dictionary<int, string[]>();
                Seznam[0] = Create("Alexandr", "Sekera", datumAlex, CalculatePIN(datumAlex));
                Seznam[1] = Create("Petr", "Jelínek", datumPetr, CalculatePIN(datumPetr));
                Seznam[2] = Create("Jirka", "Měřínský", datumJirka, CalculatePIN(datumJirka));
            }
            string CalculatePIN(string datum) 
            {
                Datum = datum.Split('.');
                Rok = Datum[2].ToCharArray();
                return $"{Rok[2]}{Rok[3]}{(Datum[1].Length == 2 ? Datum[1] : $"0{Datum[1]}")}{(Datum[0].Length == 2 ? Datum[0] : $"0{Datum[0]}")}/{nc.Next(0, 10)}{nc.Next(0, 10)}{nc.Next(0, 10)}{nc.Next(0, 10)}";
            }
            public static SingletonInformation SI_Inst 
            {
                get 
                {
                    lock (LockingObject) 
                    {
                        if (SI == null) SI = new SingletonInformation();
                        else MessageBox.Show("Instance již byla vytvořena!", "NEPŘÍSTUPNÉ");
                    }
                    return SI;
                }
            }

            public string View(int Klic) 
            {
                foreach (string Item in Seznam[Klic])
                {
                    Text += $"{Item} ";
                }
                return $"{Text}\n";
            }
        }
    }
}
