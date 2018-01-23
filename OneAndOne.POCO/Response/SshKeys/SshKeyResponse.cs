using System;
using System.Collections.Generic;

namespace OneAndOne.POCO.Response.SshKeys
{
    public class SshKeyResponse
    {
        private string id;
        ///<summary>
        ///Ssh key ID
        ///</summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        ///<summary>
        ///Ssh key name
        ///</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string description;
        ///<summary>
        ///Ssh key description
        ///</summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string state;
        ///<summary>
        ///Ssh key state
        ///</summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        private List<Server> servers;
        ///<summary>
        ///Servers using the ssh key
        ///</summary>
        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }

        private string md5;
        ///<summary>
        ///Ssh key md5
        ///</summary>
        public String Md5
        {
            get { return md5; }
            set { md5 = value; }
        }

        private string publicKey;
        ///<summary>
        ///Ssh public key
        ///</summary>
        public string PublicKey
        {
            get { return publicKey; }
            set { publicKey = value; }
        }

        private string creationDate;
        ///<summary>
        ///Ssh key creation date
        ///</summary>
        public string CreationDate
        {
            get { return creationDate; }
            set { creationDate = value; }
        }
    }

    public class Server
    {
        private string id;
        ///<summary>
        ///Server identifier
        ///</summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        ///<summary>
        ///Server name
        ///</summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
