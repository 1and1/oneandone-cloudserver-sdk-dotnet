using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Usages
{
    public class UsageResponse
    {
        private List<ServerUsage> servers;
        [JsonProperty(PropertyName = "SERVERS")]
        public List<ServerUsage> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
        private List<SharedStoragesUsage> shared_storages;
        [JsonProperty(PropertyName = "SHARED STORAGE")]
        public List<SharedStoragesUsage> SharedStorages
        {
            get { return shared_storages; }
            set { shared_storages = value; }
        }
        private List<ImageUsage> images;
        public List<ImageUsage> Images
        {
            get { return images; }
            set { images = value; }
        }
        private List<PublicIPUsage> public_ips;
        [JsonProperty(PropertyName = "PUBLIC IP")]
        public List<PublicIPUsage> PublicIPs
        {
            get { return public_ips; }
            set { public_ips = value; }
        }
        private List<LoadBalancers> loadbalancers;
        [JsonProperty(PropertyName = "LOAD BALANCERS")]
        public List<LoadBalancers> LoadBalancers
        {
            get { return loadbalancers; }
            set { loadbalancers = value; }
        }

    }
    public class Detail
    {
        private object avg_amount;

        public object AvgAmount
        {
            get { return avg_amount; }
            set { avg_amount = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private object start_date;

        public object StartDate
        {
            get { return start_date; }
            set { start_date = value; }
        }
        private object end_date;

        public object EndDate
        {
            get { return end_date; }
            set { end_date = value; }
        }
        private int usage;

        public int Usage
        {
            get { return usage; }
            set { usage = value; }
        }
    }
    public class Service
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        private object avg_amount;

        public object AvgAmount
        {
            get { return avg_amount; }
            set { avg_amount = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private int usage;

        public int Usage
        {
            get { return usage; }
            set { usage = value; }
        }
        private List<Detail> detail;

        public List<Detail> Detail
        {
            get { return detail; }
            set { detail = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
    public class ServerUsage
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int site;

        public int Site
        {
            get { return site; }
            set { site = value; }
        }
        private List<Service> services;

        public List<Service> Services
        {
            get { return services; }
            set { services = value; }
        }
    }
    public class SharedStoragesUsage
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int site;

        public int Site
        {
            get { return site; }
            set { site = value; }
        }
        private List<Service> services;

        public List<Service> Services
        {
            get { return services; }
            set { services = value; }
        }
    }
    public class ImageUsage
    {

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int site;

        public int Site
        {
            get { return site; }
            set { site = value; }
        }
       
        private List<Service> services;

        public List<Service> Services
        {
            get { return services; }
            set { services = value; }
        }
    }
    public class PublicIPUsage
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int site;

        public int Site
        {
            get { return site; }
            set { site = value; }
        }
        private List<Service> services;

        public List<Service> Services
        {
            get { return services; }
            set { services = value; }
        }
    }

    public class LoadBalancers
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int site;

        public int Site
        {
            get { return site; }
            set { site = value; }
        }
        private List<Service> services;

        public List<Service> Services
        {
            get { return services; }
            set { services = value; }
        }
    }
}
