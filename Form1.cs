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

            Feature feature = new Feature();
            feature.properties.name = "TheFuture";
            feature.geometry.coordinates = new Vector3(123.4f, 345.678f, 567.321f);
            featureCollection.features.Add(feature);

            string jsonString = JsonSerializer.Serialize(featureCollection, serializerOptions);
            textBox1.Text = jsonString;
        }

        void deserialize()
        {
            FeatureCollection? featureCollection;
            string serializedText = textBox1.Text;

            try
            {
                featureCollection = JsonSerializer.Deserialize<FeatureCollection>(serializedText);

                if (featureCollection == null) return;
                foreach (Feature feature in featureCollection.features)
                {
                    Vector3 pos = feature.geometry.coordinates;
                    //Debug.WriteLine(pos.ToString("0.00", CultureInfo.InvariantCulture));
                    //Debug.WriteLine($"x:{pos.X}, y:{pos.Y}, z:{pos.Z}");
                    textBox2.Text = pos.ToString("0.00", CultureInfo.InvariantCulture);
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