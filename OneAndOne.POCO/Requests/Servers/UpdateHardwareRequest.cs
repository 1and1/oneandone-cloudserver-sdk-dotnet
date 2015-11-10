﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateHardwareRequest
    {
        /// <summary>
        /// Required: Total amount of processors minimum: "1",maximum: "16",multipleOf: "1",.
        /// </summary>
        /// 
        private int vcore;
        [JsonProperty(PropertyName = "vcore")]
        public int Vcore
        {
            get { return vcore; }
            set { vcore = value; }
        }

        /// <summary>
        /// Required: Number of cores per processor minimum: "1",maximum: "16",multipleOf: "1",
        /// </summary>
        /// 
        private int cores_per_processor;
        [JsonProperty(PropertyName = "cores_per_processor")]
        public int CoresPerProcessor
        {
            get { return cores_per_processor; }
            set { cores_per_processor = value; }
        }

        /// <summary>
        /// Required: RAM memory size minimum: "1",maximum: "128",multipleOf: "0.5",.
        /// </summary>
        private int ram;
        [JsonProperty(PropertyName = "ram")]
        public int Ram
        {
            get { return ram; }
            set { ram = value; }
        }
    }
}