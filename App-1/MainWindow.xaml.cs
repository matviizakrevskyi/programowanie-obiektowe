using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
using System.Xml.Linq;

namespace App_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class TableRates
        {
            [JsonPropertyName("table")]
            public string Table { get; set; }
            [JsonPropertyName("no")]
            public string Number { get; set; }
            [JsonPropertyName("tradingDate")]
            public DateTime TradingDate { get; set; }
            [JsonPropertyName("effectiveDate")]
            public DateTime EffectiveDate { get; set; }
            [JsonPropertyName("rates")]
            public List<JsonRate> Rates { get; set; }
        }

        record Rate(string Currency, string Code, decimal Ask, decimal Bid);
      
        record JsonRate
        {
            
            [JsonPropertyName("currency")]
            public string Currency { get; set; }
            [JsonPropertyName("code")]
            public string Code { get; set; }

            [JsonPropertyName("ask")]
            public decimal Ask { get; set; }
            [JsonPropertyName("bid")]
            public decimal Bid { get; set; }
        }
        Dictionary<string, Rate> Rates = new Dictionary<string, Rate>();
        private void DownloadDataJson()
        {
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/json");
            string json = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            List<TableRates> tableRates = JsonSerializer.Deserialize<List<TableRates>>(json);
            if (tableRates.Count == 1)
            {
                tableRates[0].Rates.Add(new JsonRate() { Currency = "złoty", Code = "PLN", Ask = 1, Bid = 1 });
                foreach (JsonRate rate in tableRates[0].Rates)
                {
                    Rates.Add(rate.Code, new Rate(rate.Currency, rate.Code, rate.Ask, rate.Bid));
                }
            }
        }
        private void DownloadData()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-En");
            WebClient client = new WebClient();
            client.Headers.Add("Accept", "application/xml");
            string xml = client.DownloadString("http://api.nbp.pl/api/exchangerates/tables/C");
            XDocument doc = XDocument.Parse(xml);
            List<Rate> list = doc
                .Elements("ArrayOfExchangeRatesTable")
                .Elements("ExchangeRatesTable")
                .Elements("Rates")
                .Elements("Rate")
                .Select(n =>
                    new Rate(
                        n.Element("Currency").Value,
                        n.Element("Code").Value,
                        decimal.Parse(n.Element("Ask").Value, culture),
                        decimal.Parse(n.Element("Bid").Value, culture)
                    )
                ).ToList();
            foreach(Rate rate in list)
            {
                Rates.Add(rate.Code, rate);
            }
            Rates.Add("PLN", new Rate("złoty", "PLN", 1, 1));
        }

        public MainWindow()
        {
            InitializeComponent();
            DownloadDataJson();
            UpdateGui();

        }

        private void UpdateGui()
        {
            InputCurrencyCode.Items.Clear();
            OutputCurrencyCode.Items.Clear();
            foreach (string code in Rates.Keys)
            {
                InputCurrencyCode.Items.Add(code);
                OutputCurrencyCode.Items.Add(code);
            }
            InputCurrencyCode.SelectedIndex = 0;
            OutputCurrencyCode.SelectedIndex = 1;
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = " Wczytaj plik z notowaniami";
            dialog.Filter = "Plik tesktowy (*.txt)|*.txt";
            dialog.DefaultExt = "*.txt";
            if (dialog.ShowDialog() == true)
            {
                if (File.Exists(dialog.FileName))
                {
                    string[] lines = File.ReadAllLines(dialog.FileName);
                    Rates.Clear();
                    foreach(string line in lines)
                    {
                        string[] tokens = line.Split(";");
                        Rate rate = new Rate(
                            tokens[0],
                            tokens[1],
                            decimal.Parse(tokens[2]),
                            decimal.Parse(tokens[3])
                        );
                        Rates.Add(rate.Code, rate);
                    }
                    UpdateGui();
                }
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Dokument w formacie json (*.json)|*.json";
            dialog.Title = "Zapisz notowania do pliku JSON";
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, JsonSerializer.Serialize(Rates));
            }
        }

        private void CalcOutputValue(object sender, RoutedEventArgs e)
        {
            string inputCode = (string) InputCurrencyCode.SelectedItem;
            string outputCode = (string)OutputCurrencyCode.SelectedItem;
            string amountStr = InputValue.Text;
            if (decimal.TryParse(amountStr, out decimal amount))
            {
                Rate inputRate = Rates[inputCode];
                Rate outputRate = Rates[outputCode];
                decimal output = amount * inputRate.Ask / outputRate.Ask;
                OutputValue.Text = output.ToString("N2");
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(InputValue.Text + e.Text, out decimal values);
        }
    }
}
