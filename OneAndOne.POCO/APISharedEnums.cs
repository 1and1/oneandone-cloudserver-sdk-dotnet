using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO
{

    public enum ServerAction
    {
        POWER_ON, POWER_OFF, REBOOT
    }

    public enum ServerActionMethod
    {
        SOFTWARE, HARDWARE
    }

    public enum OSImageType
    {
        Standard, Minimal, Application, Personal, ISO_OS
    }

    public enum OSType
    {
        CentOS,
        Debian,
        Ubuntu,
        [EnumMember(Value = "Red Hat")]
        RedHat,
        [EnumMember(Value = "Windows 2008")]
        Windows2008,
        [EnumMember(Value = "Windows 2012")]
        Windows2012,
        WindowsDatacenter,
        Null
    }

    public enum OSFamliyType
    {
        Windows, Linux, Others
    }

    public enum ImageType
    {
        IMAGES, MY_IMAGE, NULL
    }

    public enum ArchitectureType
    {
        [EnumMember(Value = "32")]
        Bits32 = 32,
        [EnumMember(Value = "64")]
        Bits64 = 64
    }
    public enum PeriodType
    {
        LAST_HOUR, LAST_24H, LAST_7D, LAST_30D, LAST_365D, CUSTOM
    }
    public enum ProtocolType
    {
        TCP, UDP
    }
    public enum AlertIfType
    {
        RESPONDING, NOT_RESPONDING
    }
    public enum ProcessAlertType
    {
        RUNNING, NOT_RUNNING
    }
}
