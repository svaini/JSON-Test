using System.Text.Json;
using System.Numerics;
using System.Diagnostics;
using WinFormsApp2.GeoJSON;
using System.Globalization;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        void serialize()
        {
            FeatureCollection featureCollection = new();

            Feature feature1 = new();
            feature1.properties.name = "Feature1";
            feature1.geometry.coordinates = new Vector3(123.4f, 345.678f, 567.321f);
            featureCollection.features.Add(feature1);

            Feature feature2 = new();
            feature2.properties.name = "Feature2";
            feature2.geometry.coordinates = new Vector3(222.222f, 333.333f, 444.444f);
            featureCollection.features.Add(feature2);

            string jsonString = JsonSerializer.Serialize(featureCollection, serializerOptions);
            textBox1.Text = jsonString;
        }

        void deserialize()
        {
            FeatureCollection? featureCollection;
            string serializedText = textBox1.Text;
            textBox2.Clear();

            try
            {
                featureCollection = JsonSerializer.Deserialize<FeatureCollection>(serializedText);

                if (featureCollection == null) return;
                foreach (Feature feature in featureCollection.features)
                {
                    Vector3 position = feature.geometry.coordinates;
                    string name = feature.properties.name;
                    string strPos = position.ToString("0.00", CultureInfo.InvariantCulture);
                    Debug.WriteLine($"{name} {strPos}");
                    textBox2.Text += $"{name} {strPos}\r\n";
                }
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.Message;
                return;
            }
        }

        private void buttonSerialize(object sender, EventArgs e)
        {
            serialize();
        }

        private void buttonDeserialize(object sender, EventArgs e)
        {
            deserialize();
        }

        private void buttonInputClear(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void buttonOutputClear(object sender, EventArgs e)
        {
            textBox2.Clear();
        }
    }
}