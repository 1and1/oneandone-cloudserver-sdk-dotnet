# .NET SDK
This guide will show you how to programmatically perform common management tasks using the .NET SDK for the 1&1 REST API.

For more information on the 1&1 .NET SDK see the [1&1 Community Portal](https://www.1and1.com/cloud-community/).

## Table of Contents

* [Overview](#overview)
* [Getting Started](#getting-started)
  * [Installation](#installation)
  * [Configuration](#configuration)
  * [Using the Driver](#using-the-driver)
  * [Additional Documentation and Support](#additional-documentation-and-support)
* [Operations](#operations)
  * [Servers](#servers)
  * [Images](#images)
  * [Shared Storages](#shared-storages)
  * [Firewall Policies](#firewall-policies)
  * [Load Balancers](#load-balancers)
  * [Public IPs](#public-ips)
  * [Private Networks](#private-networks)
  * [VPN](#vpn)
  * [Monitoring Center](#monitoring-center)
  * [Monitoring Policies](#monitoring-policies)
  * [Logs](#logs)
  * [Users](#users)
  * [Roles](#roles)
  * [Usages](#usages)
  * [Server Appliances](#server-appliances)
  * [DVD ISO](#dvd-iso)
  * [Ping](#ping)
  * [Pricing](#pricing)
  * [Datacenters](#datacenters)
  * [Block Storages](#block-storages)


## Overview
***
This .NET library is a wrapper for the 1&1 REST API. All API operations are performed over SSL and authenticated using your 1&1 token key. The API can be accessed within an instance running in 1&1 or directly over the Internet from any application that can send an HTTPS request and receive an HTTPS response.

## Getting Started
***
Before you begin you will need to have signed up for a 1&1 account. The credentials you create during sign-up will be used to authenticate against the API. 

To create a user and generate an API token that will be used to authenticate against the REST API, log into your 1&1 control panel. Go to the Server section -> Management -> Users. 

##### Installation

The official .NET library is available from the 1&1 GitHub account found [here](https://github.com/StackPointCloud/oneandone-cloudserver-sdk-dotnet). To install the latest stable version, clone the repository, then add the binaries to your project.

##### Configuration

Depending on the type of project, you have the option to either create an App.config or Web.config file to interact with the service before you begin, This file should contain the following values:

```<appSettings>
    <add key="APIToken" value="api token goes here"/>
    <add key="APIURL" value="https://xxxxxx.1and1.com/v1"/>
  </appSettings>
```

Or you can pass the required values when initializing the client through your code.
```
OneAndOneClient client = OneAndOneClient.Instance(new Client.RESTHelpers.Configuration()
        {
            ApiKey="token",
            ApiUrl="url"
        });
```

##### Using the Driver
Here is a simple example on how to use the library.

List all Servers:

`var servers=OneAndOneClient.Instance().Servers.Get();`


This will list all servers under your 1&1 account.
##### Additional Documentation and Support

You can engage with us in the community and we'll be more than happy to answer any questions you might have.

## Operations

- [Servers](#servers)
- [Images](#images)
- [Shared Storages](#shared-storages)
- [Firewall Policies](#firewall-policies)
- [Load Balancers](#load-balancers)
- [Public IPs](#public-ips)
- [Private Networks](#private-networks)
- [VPN](#vpn)
- [Monitoring Center](#monitoring-center)
- [Monitoring Policies](#monitoring-policies)
- [Logs](#logs)
- [Users](#users)
- [Roles](#roles)
- [Usages](#usages)
- [Server Appliances](#server-appliances)
- [DVD ISO](#dvd-iso)
- [Ping](#ping)
- [Pricing](#pricing)
- [Datacenters](#datacenters)
- [Block Storages](#block-storages)
  

There are two ways to initialize the 1&1 client. You can either have the API URL and token key in your app/web config, or you can pass the values in the constructor of the 1&1 client.

`OneAndOneClient client = OneAndOneClient.Instance("https://xxxxxx.1and1.com/v1", apiToken); //API values passed through code`

`OneAndOneClient client = OneAndOneClient.Instance(); // API values in the config file`

## Servers

**List all servers:**

`var servers = client.Servers.Get();`

**Show a single server:**

`var server = client.Servers.Show(serverId);`

**List available server flavors:**

`var serverFlavours = client.Servers.GetAvailableFixedServers();`

**Show a single server flavor:**

`var serverFlavour=client.Servers.GetFlavorInformation(serverFlavourId);`

**Create a server:**

`//get a server appliance for example here it's windows and it automatically installs,
var appliance = client.ServerAppliances.Get().Where(app => app.OsFamily == OSFamliyType.Windows && app.AutomaticInstallation == true).FirstOrDefault();`

`//get a public IP that is not assigned to any server yet
var publicIP = client.PublicIPs.Get().FirstOrDefault(ip => ip.AssignedTo == null);`



    var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
                {
            ApplianceId = appliance.Id,
                Name = "My server",
                Description = "Example description",
                Hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    CoresPerProcessor = 1,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
								//make sure that the hard drive meets the appliance minimum requirements 
                                Size=appliance.MinHddSize,
                            }}
                        },
                    Ram = 4,
                    Vcore = 2
                },
                PowerOn = true,
                Password = "Test123!",
                IpId = publicIP.Id
            });`

**Update a server:**		
	
    var result = client.Servers.Update(new UpdateServerRequest()
			{
				Description = "my server updated",
				Name = "my server updated"
			}, serverId);
			
**Delete a server:**

`// the bool parameter is for Set true for keeping server IPs after deleting a server (false by default).
var result = client.Servers.Delete(serverToDelete.Id, false);`

**Show a server's hardware:**

`var result = client.ServersHardware.Show(serverId);`

**Update a server's hardware:**

    var result = client.ServersHardware.Update(new POCO.Requests.Servers.UpdateHardwareRequest()
                    {
                        CoresPerProcessor = 2,
                        Ram = 8,
                        Vcore = 4
                    }, serverId);`
					
**Get server's hard drives:**

`var result = client.ServerHdds.Get(serverId);`

**Show a server's hard drive:**

`var result = client.ServerHdds.Show(serverId, hardDriveId);`

**Add a hard drive to a server:**

    var result = client.ServerHdds.Create(new POCO.Requests.Servers.AddHddRequest()
                    {
                        Hdds = new System.Collections.Generic.List<POCO.Requests.Servers.HddRequest>()
                    {
                        { new POCO.Requests.Servers.HddRequest()
                        {Size=20,IsMain=false}},
                        {new POCO.Requests.Servers.HddRequest()
                        {Size=30,IsMain=false}
                    }}
                    }, serverId);
					
**Update a server’s hard drive size:**

    var result = client.ServerHdds.Update(new POCO.Requests.Servers.UpdateHddRequest()
                {
                    Size = updatedSize
                }, serverId, harddriveId);
				
**Remove a hard drive from server:**

`var result = client.ServerHdds.Delete(serverId, harddriveId);`

**Show the server's loaded DVD:**

`var result = client.ServersHardware.ShowDVD(serverId);`

**Load a DVD into the server's unit:**

`var result = client.ServersHardware.UpdateDVD(serverId, dvdId);`

**Unload the server's loaded DVD:**

`var result = client.ServersHardware.DeleteDVD(serverId);`

**Show the server's image:**

`var image = client.ServerImage.Show(serverId);`

**Reinstall a new image into a server:**

    var result = client.ServerImage.Update(new POCO.Requests.Servers.UpdateServerImageRequest()
                    {
                        Id = imageId,
                        Password = "Test123!"
                    }, serverId);
					
**List the server's IP addresses:**

`var result = client.ServerIps.Get(serverId);`

**Show a server's IP address:**

`var result = client.ServerIps.Show(serverId, ipId);`

**Assign an IP to the server:**

    var result = client.ServerIps.Create(new POCO.Requests.Servers.CreateServerIPRequest()
                {
                    Type = IPType.IPV4
                }, serverId);
				
**Un-Assign an IP from the server:**

`//the bool parameter Set true for releasing the IP without removing it
var result = client.ServerIps.Delete(serverId, ipId, true);`

**List server's firewall policies:**

`var result = client.ServerIps.GetFirewallPolicies(serverId, ipId);`

**Adds a new firewall policy to the server's IP:**

`var policyresult = client.FirewallPolicies.Show(firewallPolicyId);
var result = client.ServerIps.UpdateFirewallPolicy(serverId, ipId, policyresult.Id);`

**Remove a firewall policy from server's IP:**

`var result = client.ServerIps.DeleteFirewallPolicy(serverId, ipId);`

**List all server's IP address load balancers:**

`var result = client.ServerIps.GetLoadBalancer(serverId, ipId);`

**Add a new load balancer to the IP:**

`var result = client.ServerIps.CreateLoadBalancer(serverId, ipId, loadBalancerId);`

**Remove load balancer from the IP:**

`var result = client.ServerIps.DeleteLoadBalancer(serverId, ipId, loadBalancerId);`

**Get server status:**

`var result = client.Servers.GetStatus(serverId);`

**Change server status:**

    var result = client.Servers.UpdateStatus(new UpdateStatusRequest()
            {
                Action = ServerAction.REBOOT,
                Method = ServerActionMethod.SOFTWARE
            }, serverId);
			
**List server's private networks:**

`var result = client.Servers.GetPrivateNetworks(serverId);`

**Show a server's private network:**

`var result = client.Servers.ShowPrivateNetworks(serverId, privateNetworkId);`

**Add a server's private network:**

`var result = client.Servers.CreatePrivateNetwork(serverId, privateNetworkId);`

**Remove a server's private network:**

`var result = client.Servers.DeletePrivateNetwork(serverId, privateNetworkId);`

**List server's snapshots:**

`var result = client.Servers.GetSnapshots(serverId);`

**Creates a new snapshot of the server:**

`var result = client.Servers.CreateSnapshot(serverId);`

**Restore a snapshot into server:**

`var result = client.Servers.UpdateSnapshot(serverId, snapshotId);`

**Remove a snapshot from server:**

`var result = client.Servers.DeleteSnapshot(serverId, snapshotId);`

**Create a server clone:**

`var result = client.Servers.CreateClone(serverId, "Clone Name");`
 
	
## Images

**List all images:**

`var images = client.Images.Get();`

**Get a single image:**

`var image = client.Images.Show(imageId);`

**Create an image:**

    var image = client.Images.Create(new POCO.Requests.Images.CreateImageRequest()
                {
                    ServerId = serverId,
                    Description = "describe image",
                    Frequency = ImageFrequency.DAILY,
                    Name = "testImage",
                    NumIimages = 44// Max number of images
                });

**Update an image:**


    var result = client.Images.Update(new UpdateImageRequest()
                {
                    Description = "updated",
                    Frequency = ImageFrequency.ONCE,
                    Name = "updaeted API Image"
                }, image.Id);

**Delete an image:**

`var result = client.Images.Delete(imageId);`


## Shared Storages

**List shared storages:**

`var result = client.SharedStorages.Get();`

**Get a single shared storage:**

`var result = client.SharedStorages.Show(sharedStorageId);`

**Create a shared storage:**

    var result = client.SharedStorages.Create(new POCO.Requests.SharedStorages.CreateSharedStorage()
                {
                    Description = "description",
                    Name = "TestStorage",
                    Size = 50
                });
				
**Update a shared storage:**

    var result = client.SharedStorages.Update(new POCO.Requests.SharedStorages.UpdateSharedStorageRequest()
            {
                Description = "description",
                Name = "TestStorageupdated",
                Size = 70
            }, sharedStorageId);
			
**Remove a shared storage:**

`var result = client.SharedStorages.Delete(sharedStorageId);`

**List a shared storage servers:**

`var result = client.SharedStorages.GetSharedStorageServers(sharedStorageId);`

**Get a shared storage single server:**

`var result = client.SharedStorages.ShowSharedStoragesServer(sharedStorageId, serverId);`

**Attaches servers to a shared storage:**

`//serverstoAdd is a list of string that represents the server id
var result = client.SharedStorages.CreateServerSharedStorages(new POCO.Requests.SharedStorages.AttachSharedStorageServerRequest()
                {
                    Servers = serverstoAdd
                }, sharedStorageId);`
				
**Unattaches a server from a shared storage:**

`var result = client.SharedStorages.DeleteSharedStoragesServer(sharedStorageId, serverId);`

**Return the credentials for accessing the shared storages:**

`var result = client.SharedStorages.ShowSharedStorageAccess();`

**Change the password for accessing the shared storages:**

`var result = client.SharedStorages.UpdateSharedStorageAccess("test123!");`



## Firewall Policies

**List firewall policies:**

`var result = client.FirewallPolicies.Get();`

**Get a single firewall policy:**

`var result = client.FirewallPolicies.Show(firewallId);`

**Create a firewall policy:**

    var newRules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule>();
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
                {
                    PortTo = 80,
                    PortFrom = 80,
                    Protocol = RuleProtocol.TCP,
                    Source = "0.0.0.0"
                });
    var result = client.FirewallPolicies.Create(new POCO.Requests.FirewallPolicies.CreateFirewallPolicyRequest()
            {
                Description = "TestFirewall",
                Name = "TestFW",
                Rules = newRules
            });
			
**Update a firewall policy:**

    var result = client.FirewallPolicies.Update(new POCO.Requests.FirewallPolicies.UpdateFirewallPolicyRequest()
            {
                Name = "Updated",
                Description = "UpdDesc",
            }, firewallId);
			
**Delete a firewall policy:**

`var result = client.FirewallPolicies.Delete(firewallId);`

**Return a list of the servers/IPs attached to a firewall policy:**

`var result = client.FirewallPolicies.GetFirewallPolicyServerIps(firewallpolicyId);`

**Return information about a server/IP assigned to a firewall policy:**

`var result = client.FirewallPolicies.ShowFirewallPolicyServerIp(firewallpolicyId, serverIpId);`

**Assign servers/IPs to a firewall policy:**

`// ServerIps is a string list of server id
var result = client.FirewallPolicies.CreateFirewallPolicyServerIPs(new POCO.Requests.FirewallPolicies.AssignFirewallServerIPRequest() { ServerIps = iptoAdd }, firewallpolicyId);`

**Unassign a server/IP from a firewall policy:**

`var result = client.FirewallPolicies.DeleteFirewallPolicyServerIP(firewallpolicyId, serverIpId);`

**Return a list of the rules of a firewall policy:**

`var result = client.FirewallPolicies.GetFirewallPolicyRules(firewallpolicyId);`

**Return information about a rule of a firewall policy:**

`var result = client.FirewallPolicies.ShowFirewallPolicyRule(firewallpolicyId, firewallpolicyRuleId);`

**Adds new rules to a firewall policy:**

    var result = client.FirewallPolicies.CreateFirewallPolicyRule(new POCO.Requests.FirewallPolicies.AddFirewallPolicyRuleRequest()
                {
                    Rules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.RuleRequest>()
                {
                    {new OneAndOne.POCO.Requests.FirewallPolicies.RuleRequest()
                    {
                        PortFrom =8080,
			PortTo = 8070,
			Protocol = RuleProtocol.TCP,
			Source = "0.0.0.0"
                    }}
                }
                }, firewallpolicyId);
				
**Remove a rule from a firewall policy:**

`var result = client.FirewallPolicies.DeleteFirewallPolicyRules(firewallpolicyId, firewallpolicyRulesId);`


## Load Balancers

**Return a list of your load balancers:**

`var result = client.LoadBalancer.Get();`

**Return information about a load balancer:**

`var result = client.LoadBalancer.Show(loadBalancerId);`

**Create a new load balancer:**

    var result = client.LoadBalancer.Create(new POCO.Requests.LoadBalancer.CreateLoadBalancerRequest()
                {
                    Name = "LBTest",
                    Description = "LBdesc",
                    HealthCheckInterval = 1,
                    Persistence = true,
                    PersistenceTime = 30,
                    HealthCheckTest = HealthCheckTestTypes.NONE,
                    Method = LoadBalancerMethod.ROUND_ROBIN,
                    Rules = new System.Collections.Generic.List<POCO.Requests.LoadBalancer.LoadBalancerRuleRequest>()
                    {
                        {new POCO.Requests.LoadBalancer.LoadBalancerRuleRequest()
                        {
                            PortBalancer=80,
                            Protocol=LBRuleProtocol.TCP,
                            Source="0.0.0.0",
                            PortServer=80
                        }
                        }
                    }
                });
				
**Modify a load balancer:**

    var result = client.LoadBalancer.Update(new POCO.Requests.LoadBalancer.UpdateLoadBalancerRequest()
            {
                HealthCheckInterval = 100,
                HealthCheckTest = HealthCheckTestTypes.TCP,
                Method = LoadBalancerMethod.ROUND_ROBIN,
                Persistence = false,
                Name = "UpdatedLB" + loadBalancer.Name,
                HealthCheckPathParse = "regex",
                HealthCheckPath = "google.com"
            }, loadBalancerId);

**Removes a load balancer:**	

`var result = client.LoadBalancer.Delete(loadBalancer.Id);`

**Return a list of the servers/IPs attached to a load balancer:**	

`var result = client.LoadBalancer.GetLoadBalancerServerIps(loadBalancerId);`

**Return information about a server/IP assigned to a load balancer:**	

`var result = client.LoadBalancer.ShowLoadBalancerServerIp(loadBalancerId, serverIpId);`

**Assign servers/IPs to a load balancer:**	

    // iptoAdd is a list of string contains IDs of server Ips
    var result = client.LoadBalancer.CreateLoadBalancerServerIPs(new POCO.Requests.LoadBalancer.AssignLoadBalancerServerIpsRequest()
                        {
                            ServerIps = iptoAdd
                        }, loadBalancerId);

**Unassign a server/IP from a load balancer:**	

`var result = client.LoadBalancer.DeleteLoadBalancerServerIP(loadBalancerId, serverIpId);`

**Return a list of the rules of a load balancer:**	

`var result = client.LoadBalancer.GetLoadBalancerRules(loadBalancerId);`

**Returns information about a rule of a load balancer:**	

`var result = client.LoadBalancer.ShowLoadBalancerRule(loadBalancerId, ruleId);`

**Add new rules to a load balancer:**	

    var result = client.LoadBalancer.CreateLoadBalancerRule(new POCO.Requests.LoadBalancer.AddLoadBalancerRuleRequest()
            {
                Rules = new System.Collections.Generic.List<POCO.Requests.LoadBalancer.RuleRequest>()
                {
                    {new OneAndOne.POCO.Requests.LoadBalancer.RuleRequest()
                    {
                        PortBalancer =8080,
                    PortServer = 8080,
                    Protocol = LBRuleProtocol.TCP,
                    Source = "0.0.0.0"
                    }}
                }
            }, loadBalancerId);

**Removes a rule from a load balancer:**	

`var result = client.LoadBalancer.DeleteLoadBalancerRules(loadBalancerId, ruleId);`	

## Public IPs

**Return a list of your public IPs:**

`var result = client.PublicIPs.Get();`

**Return information about a public IP:**

`var result = client.PublicIPs.Show(publicIpId);`

**Creates a new public IP:**

    var result = client.PublicIPs.Create(new POCO.Requests.PublicIPs.CreatePublicIPRequest()
                {
                    ReverseDns = "dnsNameTest",
                    Type = IPType.IPV4
                });

**Modify the reverse DNS of a public IP:**

`var result = client.PublicIPs.Update("dnsNameTest", publicIpId);`

**Remove a public IP:**

`var result = client.PublicIPs.Delete(publicIpId);`


## Private Networks

**Return a list of your private networks:**

`var result = client.PrivateNetworks.Get();`

**Return information about a private network:**

`var result = client.PrivateNetworks.Show(privateNetworkId);`

**Create a new private network:**

    var result = client.PrivateNetworks.Create(new POCO.Requests.PrivateNetworks.CreatePrivateNetworkRequest()
                {
                    Name ="testPrivateNetwork",
                    Description = "test description",
                    NetworkAddress = "192.168.1.0",
                    SubnetMask = "255.255.255.0"
                });

**Modify a private network:**

    var result = client.PrivateNetworks.Update(new POCO.Requests.PrivateNetworks.UpdatePrivateNetworkRequest()
                {
                    Name = "updated testPrivateNetwork",
                    NetworkAddress = "192.168.1.0",
                    SubnetMask = "255.255.255.0",
                }, privateNetworkId);

**Remove a private network:**

`var result = client.PrivateNetworks.Delete(privateNetworkId);`

## VPN

**Return a list of your vpns:**

`var result = client.Vpn.Get()`

**Return information about a vpn:**

`var result = client.Vpn.Show(vpn.Id);`

**Download your VPN configuration file, a string with base64 format with the configuration for OpenVPN. It is a zip file:**

`var result = client.Vpn.ShowConfiguration(vpnId);`

**Create a new vpn:**

```
var result = client.Vpn.Create(new POCO.Requests.Vpn.CreateVpnRequest
            {
                Name = "vpn test",
                Description = "desc",
                Datacenterid = datacenterId
            });
```

**Modify a vpn:**

```
var result = client.Vpn.Update(new POCO.Requests.Vpn.UpdateVpnRequest
            {
                Name = "updated name",
                Description = "desc update"
            }, vpnId);
```

**Remove a vpn:**

`var result = client.Vpn.Delete(vpnId);`


## Monitoring Center

**List usages and alerts of monitoring servers:**

`var mCenters = client.MonitoringCenter.Get();`


**Return the usage of the resources for the specified time range:**

`var mCenters = client.MonitoringCenter.Show(serverId, PeriodType.CUSTOM, DateTime.Today.AddMonths(-2), DateTime.Today);`



## Monitoring Policies

**Return a list of your monitoring policies:**

`var result = client.MonitoringPolicies.Get();`

**Return information about a monitoring policy:**

`var result = client.MonitoringPolicies.Show(monitoringPolicyId);`

**Create a new monitoring policy:**

    var ports = new List<POCO.Requests.MonitoringPolicies.Ports>();
            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 22,
                    Protocol = ProtocolType.TCP
                });
            var processes = new List<POCO.Requests.MonitoringPolicies.Processes>();
            processes.Add(new Processes()
                {
                    EmailNotification = true,
                    AlertIf = ProcessAlertType.NOT_RUNNING,
                    Process = "test",
                });
            var request = new POCO.Requests.MonitoringPolicies.CreateMonitoringPolictRequest()
                {
                    Name = ".netMP",
                    Description = ".net decription",
                    Agent = true,
                    Ports = ports,
                    Processes = processes,
                    Thresholds = new POCO.Requests.MonitoringPolicies.Thresholds()
                    {
                        Cpu = new POCO.Requests.MonitoringPolicies.Cpu()
                        {
                            Critical = new POCO.Requests.MonitoringPolicies.Critical()
                            {
                                Alert = false,
                                Value = 95
                            },
                            Warning = new POCO.Requests.MonitoringPolicies.Warning()
                            {
                                Alert = false,
                                Value = 90
                            }
                        },
                        Ram = new POCO.Requests.MonitoringPolicies.Ram()
                        {
                            Critical = new POCO.Requests.MonitoringPolicies.Critical()
                            {
                                Alert = false,
                                Value = 95
                            },
                            Warning = new POCO.Requests.MonitoringPolicies.Warning()
                            {
                                Alert = false,
                                Value = 90
                            }
                        },
                        Disk = new POCO.Requests.MonitoringPolicies.Disk()
                        {
                            Critical = new POCO.Requests.MonitoringPolicies.DiskCritical()
                            {
                                Alert = false,
                                Value = 90
                            },
                            Warning = new POCO.Requests.MonitoringPolicies.DiskWarning()
                            {
                                Alert = false,
                                Value = 80
                            }
                        },
                        InternalPing = new POCO.Requests.MonitoringPolicies.InternalPing()
                        {
                            Critical = new POCO.Requests.MonitoringPolicies.InternalPingCritical()
                            {
                                Alert = false,
                                Value = 100
                            },
                            Warning = new POCO.Requests.MonitoringPolicies.InternalPingWarning()
                            {
                                Alert = false,
                                Value = 50
                            }
                        },
                        Transfer = new POCO.Requests.MonitoringPolicies.Transfer()
                        {
                            Critical = new POCO.Requests.MonitoringPolicies.TransferCritical()
                            {
                                Alert = false,
                                Value = 2000
                            },
                            Warning = new POCO.Requests.MonitoringPolicies.Warning()
                            {
                                Alert = false,
                                Value = 1000
                            }
                        }
                    }
                };
            var result = client.MonitoringPolicies.Create(request);

**Modifiy a monitoring policy:**

    var request = new UpdateMonitoringPolicyRequest()
                {
                    Name = "updated" + monitoringPolicy.Name,
                    Thresholds = new POCO.Requests.MonitoringPolicies.Thresholds()
                   {
                       Cpu = new POCO.Requests.MonitoringPolicies.Cpu()
                       {
                           Critical = new POCO.Requests.MonitoringPolicies.Critical()
                           {
                               Alert = false,
                               Value = 95
                           },
                           Warning = new POCO.Requests.MonitoringPolicies.Warning()
                           {
                               Alert = false,
                               Value = 90
                           }
                       },
                       Ram = new POCO.Requests.MonitoringPolicies.Ram()
                       {
                           Critical = new POCO.Requests.MonitoringPolicies.Critical()
                           {
                               Alert = false,
                               Value = 95
                           },
                           Warning = new POCO.Requests.MonitoringPolicies.Warning()
                           {
                               Alert = false,
                               Value = 90
                           }
                       },
                       Disk = new POCO.Requests.MonitoringPolicies.Disk()
                       {
                           Critical = new POCO.Requests.MonitoringPolicies.DiskCritical()
                           {
                               Alert = false,
                               Value = 90
                           },
                           Warning = new POCO.Requests.MonitoringPolicies.DiskWarning()
                           {
                               Alert = false,
                               Value = 80
                           }
                       },
                       InternalPing = new POCO.Requests.MonitoringPolicies.InternalPing()
                       {
                           Critical = new POCO.Requests.MonitoringPolicies.InternalPingCritical()
                           {
                               Alert = false,
                               Value = 100
                           },
                           Warning = new POCO.Requests.MonitoringPolicies.InternalPingWarning()
                           {
                               Alert = false,
                               Value = 50
                           }
                       },
                       Transfer = new POCO.Requests.MonitoringPolicies.Transfer()
                       {
                           Critical = new POCO.Requests.MonitoringPolicies.TransferCritical()
                           {
                               Alert = false,
                               Value = 2000
                           },
                           Warning = new POCO.Requests.MonitoringPolicies.Warning()
                           {
                               Alert = false,
                               Value = 1000
                           }
                       }
                   }
                };
            var result = client.MonitoringPolicies.Update(request, monitoringPolicy.Id);

**Remove a monitoring policy:**

`var result = client.MonitoringPolicies.Delete(monitoringPolicyId);`

**Return a list of the ports of a monitoring policy:**

`var result = client.MonitoringPoliciesPorts.Get(monitoringPolicyId);`

**Returns information about a port of a monitoring policy:**

`var result = client.MonitoringPoliciesPorts.Show(monitoringPolicy.Id, port.Id);`

**Add new ports to a monitoring policy:**

    var ports = new List<POCO.Requests.MonitoringPolicies.Ports>();
            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 97,
                    Protocol = ProtocolType.TCP
                });
            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 98,
                    Protocol = ProtocolType.TCP
                });
    var result = client.MonitoringPoliciesPorts.Create(ports, monitoringPolicyId);

**Modify a port from a monitoring policy:**

    var request = new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 23,
                    Protocol = ProtocolType.TCP
                };
    var result = client.MonitoringPoliciesPorts.Update(request, monitoringPolicyId, portId);`

**Remove a port from a monitoring policy:**

`var result = client.MonitoringPoliciesPorts.Delete(monitoringPolicyId, portId);`

**Return a list of the processes of a monitoring policy:**

`var result = client.MonitoringPoliciesProcesses.Get(monitoringPolicyId);`

**Return information about a process of a monitoring policy:**

`var result = client.MonitoringPoliciesProcesses.Show(monitoringPolicyId, processId);`

**Add new processes to a monitoring policy:**

    var processes = new List<POCO.Requests.MonitoringPolicies.Processes>();
            processes.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "iexplorer"
            });
            processes.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "test"
            });
    var result = client.MonitoringPoliciesProcesses.Create(processes, monitoringPolicyId);

**Modify a process from a monitoring policy:**

    var result = client.MonitoringPoliciesProcesses.Update(new POCO.Requests.MonitoringPolicies.Processes()
                {
                    EmailNotification = true,
                    AlertIf = ProcessAlertType.RUNNING,
                    Process = "test"
                }, monitoringPolicyId, processId);

**Remove a process from a monitoring policy:**

`var result = client.MonitoringPoliciesProcesses.Delete(monitoringPolicyId, processId);`

**Return a list of the servers attached to a monitoring policy:**

` var result = client.MonitoringPoliciesServers.Get(monitoringPolicyId);`

**Return information about a server attached to a monitoring policy:**

`var result = client.MonitoringPoliciesServers.Show(monitoringPolicyId, serverId);`

**Attach servers to a monitoring policy:**

`//servers are a list of string of the servers IDs
var result = client.MonitoringPoliciesServers.Create(servers, monitoringPolicyId);`

**Unattach a server from a monitoring policy:**

`var result = client.MonitoringPoliciesServers.Delete(monitoringPolicyId, serverId);`


## Logs

**Return a list with logs:**

`var result = client.Logs.Get(PeriodType.LAST_24H);`

**Return information about a log:**

`var result = client.Logs.Show(logId);`


## Users

**Return a list with all users:**

`var result = client.Users.Get();`

**Return information about a user:**

`var result = client.Users.Show(UserId);`

**Creates a new user:**

    var result = client.Users.Create(new POCO.Requests.Users.CreateUserRequest()
            {
                Name = "test user",
                Password = "Test123!",
                Description = "description",
            });

**Modify user information:**

    var result = client.Users.Update(new POCO.Requests.Users.UpdateUserRequest()
            {
                Description = "description",
                State = UserState.ACTIVE
            }, UserId);

**Remove a user:**

`var result = client.Users.Delete(UserId);`

**Information about API:**

`var result = client.UserAPI.ShowUserAPI(UserId);`

**Allow to enable or disable the API:**

` var result = client.UserAPI.UpdateUserAPI(UserId, true);`

**Show the API key:**

`var result = client.UserAPI.ShowUserAPIKey(UserId);`

**Change the API key:**

` var result = client.UserAPI.UpdateAPIKey(UserId);`

**Return IPs from which access to API is allowed:**

`var result = client.UserAPI.GetUserIps(UserId);`

**Allow a new IP:**

    var listOfIps = new List<string>();
    listOfIps.Add("185.13.243.86");
    var result = client.UserAPI.UpdateAPIIps(listOfIps, UserId);`

**Delete an IP and forbid API access for it:**

`var result = client.UserAPI.DeleteUserIp(UserId, allowedIp);`

## Roles

**Return a list with all roles:**

`var result = client.Roles.Get();`

**Return information about a role:**

`var result = client.Roles.Show(roleId);`

**Creates a new role:**

```
var result = client.Roles.Create(roleName);
```

**Modify role information:**

`var result = client.Roles.Update(roleName, description, POCO.Requests.Users.UserState.ACTIVE, role.Id);`

**Remove a role:**

`var result = client.Roles.Delete(roleId);`

**Lists role's permissions:**

`var result = client.Roles.GetPermissions(role.Id);`

**Adds permissions to the role:**

```
var result = client.Roles.UpdatePermissions(new Permissions
            {
                Servers = new POCO.Response.Roles.Servers
                {
                    Show = true,
                    SetName = false,
                    Shutdown = true
                }
            }, roleId);
```

**Returns users assigned to role:**

`var result = client.Roles.GetRoleUsers(roleId);`

**Add users to role:**

`var result=client.Roles.CreateRoleUsers(new System.Collections.Generic.List<string> { { userId } }, roleId);`

**Returns information about a user:**

`var userInfo = client.Roles.ShowRoleUser(roleId, userId);`

**Removes user from role:**

```
var removed = client.Roles.DeleteRoleUser(roleId, userId);
```

**Clones a role:**

`var clone = client.Roles.CreateRoleClone(cloneName, roleId);`

## Usages

**Return a list of your usages:**

`var result = client.Usages.Get(PeriodType.LAST_24H);`


## Server Appliances

**Return a list of all the appliances that you can use for creating a server:**

`var result = client.ServerAppliances.Get();`

**Return Information about specific appliance:**

`var result = client.ServerAppliances.Show(appliancesId);`


## DVD ISO

**Return a list of all the operative systems and tools that you can load into your virtual DVD unit:**

`var result = client.DVDs.Get();`

**Information about specific ISO image:**

`var result = client.DVDs.Show(dvdId);`


## Ping

**Ping the API, returns true if the API is running:**

`var result = client.Common.Ping();`

**Ping the API Authentication, returns true if the API token is valid**

`var result = client.Common.PingAuthentication();`

## Pricing

**Returns prices for all available resources in Cloud Panel:**

`var result = client.Common.GetPricing();`

## Datacenters

**Returns information about available datacenters to create your resources:**

`var result = client.DataCenters.Get();`

**Returns information about a datacenter:**

`var result = client.DataCenters.Show(dcId);`


## Block Storages

**List block storages:**

`var result = client.BlockStorages.Get();`

**Get a single block storage:**

`var result = client.BlockStorages.Show(blockStorageId);`

**Create a block storage:**

    var result = client.BlockStorages.Create(new POCO.Requests.BlockStorages.CreateBlockStorageRequest()
                {
                    Description = "TestBlockStorage description",
                    Name = "TestBlockStorage",
                    Size = 60,
                    DatacenterId = datacenterId
                });
				
**Update a block storage:**

    var result = client.BlockStorages.Update(new POCO.Requests.BlockStorages.UpdateBlockStorageRequest()
            {
                Description = "TestBlockStorage description updated",
                Name = "TestBlockStorageUpdated",
                Size = 70
            }, blockStorageId);
			
**Remove a block storage:**

`var result = client.BlockStorages.Delete(blockStorageId);`

**Attach a servers to a block storage:**

    var serverBlockStorage = new BlockStorageServerRequest()
            {
                ServerId = serverId
            };

    var result = client.BlockStorages.CreateServerBlockStorage(serverBlockStorage, blockStorageId);

**Get the attached server from a block storage:**

`var result = client.BlockStorages.GetBlockStorageServer(blockStorageId);`
				
**Detach a server from a block storage:**

`var result = client.BlockStorages.DeleteBlockStorageServer(blockStorageId);`



Copyright (c) 2016 1&1 Internet SE

