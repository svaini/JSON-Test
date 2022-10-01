using System.Numerics;
using System.Text.Json.Serialization;

namespace WinFormsApp2.GeoJSON
{
    public class FeatureCollection
    {
        [JsonInclude]
        public string type { get; } = "FeatureCollection";

        [JsonInclude]
        public string name { get; set; } = string.Empty;

        [JsonInclude]
        public List<Feature> features { get; set; } = new();
    }

    public class Feature
    {
        public class props
        {
            [JsonInclude]
            public string name { get; set; } = string.Empty;
        }

        public class geom
        {
            public string type { get; set; } = "Point";

            [JsonPropertyName("coordinates")]
            public float[] coords { get; set; } = new float[3] {0f, 0f, 0f};

            [JsonIgnore]
            public Vector3 coordinates
            {
                get { return new Vector3(coords[0], coords[1], coords[2]); }
                set { coords = new float[3] {
                        (float)Math.Round(value.X, 2),
                        (float)Math.Round(value.Y, 2),
                        (float)Math.Round(value.Z, 2)
                    };
                }
            }
        }


        [JsonInclude]
        public string type { get; } = "Feature";

        [JsonInclude]
        public props properties { get; set; } = new();

        [JsonInclude]
        public geom geometry { get; set; } = new();
    }
}