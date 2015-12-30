﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Requests.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.PublicIPs
{
    public class PublicIPsResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string type;
        [JsonConverter(typeof(StringEnumConverter))]
        public IPType Type
        {
            get { return (IPType)Enum.Parse(typeof(IPType), type); }
            set
            {
                type = ((IPType)value).ToString();
            }
        }
        private AssignedTo assigned_to;

        public AssignedTo AssignedTo
        {
            get { return assigned_to; }
            set { assigned_to = value; }
        }
        private string reverse_dns;

        public string ReverseDns
        {
            get { return reverse_dns; }
            set { reverse_dns = value; }
        }
        private bool is_dhcp;

        public bool IsDhcp
        {
            get { return is_dhcp; }
            set { is_dhcp = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
    }

    public enum OwnerType
    {
        SERVER, LOAD_BALANCER
    }

    public class AssignedTo
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
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
