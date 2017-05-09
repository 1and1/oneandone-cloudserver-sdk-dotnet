using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Common
{
    public class Hdd
    {

        [JsonProperty("size")]
        public int Size { get; set; }
    }

    public class Hardware
    {

        [JsonProperty("vcore")]
        public int Vcore { get; set; }

        [JsonProperty("ram")]
        public int Ram { get; set; }

        [JsonProperty("hdds")]
        public List<Hdd> Hdds { get; set; }
    }

    public class FixedServer
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("hardware")]
        public Hardware Hardware { get; set; }
    }

    public class FlexibleSever
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Server
    {

        [JsonProperty("fixed_servers")]
        public List<FixedServer> FixedServers { get; set; }

        [JsonProperty("flexible_sever")]
        public List<FlexibleSever> FlexibleSever { get; set; }
    }

    public class PublicIp
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public int PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class Image
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class SharedStorage
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class SoftwareLicens
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("pice_gross")]
        public double? PiceGross { get; set; }
    }

    public class Backups
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price_net")]
        public double PriceNet { get; set; }

        [JsonProperty("price_gross")]
        public double PriceGross { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }
    }

    public class PricingPlans
    {

        [JsonProperty("server")]
        public Server Server { get; set; }

        [JsonProperty("public_ip")]
        public List<PublicIp> PublicIp { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("shared_storage")]
        public SharedStorage SharedStorage { get; set; }

        [JsonProperty("software_licenses")]
        public List<SoftwareLicens> SoftwareLicenses { get; set; }

        [JsonProperty("backups")]
        public Backups Backups { get; set; }
    }

    public class PricingResponse
    {

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("vat")]
        public int Vat { get; set; }

        [JsonProperty("pricing_plans")]
        public PricingPlans PricingPlans { get; set; }
    }
}
