using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestLuisApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LUISButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(inputText.Text)) await LUISParse(inputText.Text);
        }

        private async Task LUISParse(string queryString)
        {
            using (var client = new HttpClient())
            {
                string uri = "https://api.projectoxford.ai/luis/v1/application?id=<YOUR LUIS APP ID> &subscription-key=<YOUR LUIS APP KEY>&q=" + queryString;
                HttpResponseMessage msg = await client.GetAsync(uri);

                if (msg.IsSuccessStatusCode)
                {
                    var jsonResponse = await msg.Content.ReadAsStringAsync();
                    var _Data = JsonConvert.DeserializeObject<LUISResponse>(jsonResponse);

                    string entityFound = "";
                    string topIntent = "";

                    if (_Data.entities.Count() != 0) entityFound = _Data.entities[0].entity;
                    if (_Data.intents.Count() !=0 ) topIntent = _Data.intents[0].intent;
                    statusText.Text = $"I detected the intent \"{topIntent}\" for \"{entityFound}\".";
                }
            }
        }
    }

    public class LUISResponse
    {
        public string query { get; set; }
        public lIntent[] intents { get; set; }
        public lEntity[] entities { get; set; }
    }

    public class lIntent
    {
        public string intent { get; set; }
        public float score { get; set; }
    }

    public class lEntity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }
}
