using System.Numerics;
using System.Text.Json.Serialization;

namespace WinFormsApp2.GeoJSON
{
    public class FeatureCollection
    {
        [JsonInclude]
        public string type { get; set; } = "FeatureCollection";

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
            [JsonIgnore]
            private Vector3 coords;

            [JsonIgnore]
            private float[] floatCoordsArray = new float[3] {111f, 222f, 333f};

            [JsonInclude]
            public string type { get; set; } = "Point";

            [JsonIgnore]
            public Vector3 coordinates
            {
                get { return coords; }
                set { coords = value;
                    floatCoordsArray = new float[3] {
                        (float)Math.Round(value.X, 2),
                        (float)Math.Round(value.Y, 2),
                        (float)Math.Round(value.Z, 2)
                    };
                }
            }

            [JsonInclude]
            [JsonPropertyName("coordinates")]
            public float[] getCoordArray { get { return floatCoordsArray; } }
        }

        [JsonInclude]
        public string type { get; } = "Feature";

        [JsonInclude]
        public props properties { get; set; } = new();

        [JsonInclude]
        public geom geometry { get; set; } = new();
    }
}