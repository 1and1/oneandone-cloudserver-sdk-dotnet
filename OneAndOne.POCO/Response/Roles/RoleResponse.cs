using Newtonsoft.Json;
using OneAndOne.POCO.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.Roles
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Servers
    {
        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("start")]
        public bool Start { get; set; }

        [JsonProperty("restart")]
        public bool Restart { get; set; }

        [JsonProperty("shutdown")]
        public bool Shutdown { get; set; }

        [JsonProperty("resize")]
        public bool Resize { get; set; }

        [JsonProperty("reinstall")]
        public bool Reinstall { get; set; }

        [JsonProperty("clone")]
        public bool Clone { get; set; }

        [JsonProperty("manage_snapshot")]
        public bool ManageSnapshot { get; set; }

        [JsonProperty("assign_ip")]
        public bool AssignIp { get; set; }

        [JsonProperty("manage_dvd")]
        public bool ManageDvd { get; set; }

        [JsonProperty("access_kvm_console")]
        public bool AccessKvmConsole { get; set; }
    }

    public class Images
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("disable_automatic_creation")]
        public bool DisableAutomaticCreation { get; set; }
    }

    public class SharedStorages
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("manage_attached_servers")]
        public bool ManageAttachedServers { get; set; }

        [JsonProperty("access")]
        public bool Access { get; set; }

        [JsonProperty("resize")]
        public bool Resize { get; set; }
    }

    public class FirewallPolicies
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("manage_rules")]
        public bool ManageRules { get; set; }

        [JsonProperty("manage_attached_server_ips")]
        public bool ManageAttachedServerIps { get; set; }

        [JsonProperty("clone")]
        public bool Clone { get; set; }
    }

    public class LoadBalancers
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("manage_rules")]
        public bool ManageRules { get; set; }

        [JsonProperty("manage_attached_server_ips")]
        public bool ManageAttachedServerIps { get; set; }

        [JsonProperty("modify")]
        public bool Modify { get; set; }
    }

    public class PublicIps
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("release")]
        public bool Release { get; set; }

        [JsonProperty("set_reverse_dns")]
        public bool SetReverseDns { get; set; }
    }

    public class PrivateNetwork
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("set_network_info")]
        public bool SetNetworkInfo { get; set; }

        [JsonProperty("manage_attached_servers")]
        public bool ManageAttachedServers { get; set; }
    }

    public class Vpn
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("download_file")]
        public bool DownloadFile { get; set; }
    }

    public class MonitoringCenter
    {

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class MonitoringPolicies
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("set_email")]
        public bool SetEmail { get; set; }

        [JsonProperty("modify_resources")]
        public bool ModifyResources { get; set; }

        [JsonProperty("manage_ports")]
        public bool ManagePorts { get; set; }

        [JsonProperty("manage_processes")]
        public bool ManageProcesses { get; set; }

        [JsonProperty("manage_attached_servers")]
        public bool ManageAttachedServers { get; set; }

        [JsonProperty("clone")]
        public bool Clone { get; set; }
    }

    public class Backups
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }
    }

    public class Logs
    {

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class Users
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("set_email")]
        public bool SetEmail { get; set; }

        [JsonProperty("set_password")]
        public bool SetPassword { get; set; }

        [JsonProperty("manage_api")]
        public bool ManageApi { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("disable")]
        public bool Disable { get; set; }

        [JsonProperty("change_role")]
        public bool ChangeRole { get; set; }
    }

    public class Roles
    {

        [JsonProperty("show")]
        public bool Show { get; set; }

        [JsonProperty("create")]
        public bool Create { get; set; }

        [JsonProperty("delete")]
        public bool Delete { get; set; }

        [JsonProperty("set_name")]
        public bool SetName { get; set; }

        [JsonProperty("set_description")]
        public bool SetDescription { get; set; }

        [JsonProperty("manage_users")]
        public bool ManageUsers { get; set; }

        [JsonProperty("modify")]
        public bool Modify { get; set; }

        [JsonProperty("clone")]
        public bool Clone { get; set; }
    }

    public class Usages
    {

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class InteractiveInvoices
    {

        [JsonProperty("show")]
        public bool Show { get; set; }
    }

    public class Permissions
    {

        [JsonProperty("servers")]
        public Servers Servers { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("shared_storages")]
        public SharedStorages SharedStorages { get; set; }

        [JsonProperty("firewall_policies")]
        public FirewallPolicies FirewallPolicies { get; set; }

        [JsonProperty("load_balancers")]
        public LoadBalancers LoadBalancers { get; set; }

        [JsonProperty("public_ips")]
        public PublicIps PublicIps { get; set; }

        [JsonProperty("private_network")]
        public PrivateNetwork PrivateNetwork { get; set; }

        [JsonProperty("vpn")]
        public Vpn Vpn { get; set; }

        [JsonProperty("monitoring_center")]
        public MonitoringCenter MonitoringCenter { get; set; }

        [JsonProperty("monitoring_policies")]
        public MonitoringPolicies MonitoringPolicies { get; set; }

        [JsonProperty("backups")]
        public Backups Backups { get; set; }

        [JsonProperty("logs")]
        public Logs Logs { get; set; }

        [JsonProperty("users")]
        public Users Users { get; set; }

        [JsonProperty("roles")]
        public Roles Roles { get; set; }

        [JsonProperty("usages")]
        public Usages Usages { get; set; }

        [JsonProperty("interactive_invoices")]
        public InteractiveInvoices InteractiveInvoices { get; set; }
    }

    public class RoleResponse
    {

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        public UserState State { get; set; }

        [JsonProperty("default")]
        public int Default { get; set; }

        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }

        [JsonProperty("users")]
        public List<User> Users { get; set; }

        [JsonProperty("permissions")]
        public Permissions Permissions { get; set; }
    }


}
