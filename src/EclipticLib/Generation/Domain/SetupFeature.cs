using System.Text;

namespace EclipticLib.Generation.Domain
{
    public class SetupFeature : Feature
    {
        private readonly Feature feature;

        public SetupFeature(Feature feature)
        {
            this.feature = feature;
            foreach (var scenario in feature.Scenarios)
            {
                scenario.IsSetup = true;
            }
        }

        public override string Title
        {
            get { return "Setup: " + feature.Title; }
            set { feature.Title = value; }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach (var scenario in feature.Scenarios)
            {
                scenario.WriteOn(builder);
            }

            return builder.ToString();
        }
    }
}