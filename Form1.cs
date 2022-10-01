using System.Text.Json;
using WinFormsApp2.GeoJSON;
using System.Numerics;
using System.Diagnostics;

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
            feature.geometry.decimals = 2;
            featureCollection.features.Add(feature);

            string jsonString = JsonSerializer.Serialize(featureCollection, serializerOptions);
            textBoxInput.Text= jsonString;
        }

        void deserialize()
        {
            FeatureCollection? featureCollection;

            try
            {
                featureCollection = JsonSerializer.Deserialize<FeatureCollection>(textBoxInput.Text, new JsonSerializerOptions() {WriteIndented = true});

                if (featureCollection == null) return;
                foreach (Feature feature in featureCollection.features)
                {
                    Vector3 pos = feature.geometry.coordinates;
                    Debug.WriteLine($"x:{pos.X}, y:{pos.Y}, z:{pos.Z}");
                    textBoxOutput.Text = $"{pos.X}, {pos.Y}, {pos.Z}";
                }
            }
            catch (Exception ex)
            {
                textBoxOutput.Text = ex.Message;
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
            textBoxInput.Clear();
        }

        private void buttonOutputClear(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
        }
    }
}