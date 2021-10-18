using System.Collections.Generic;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace WinDo.Utilities
{
    /// <summary>
    /// MachineInfo
    /// 获取机器信息公共类（MAC地址、IP地址等）    
    /// <author>
    ///		<name></name>
    ///		<date></date>
    /// </author>
    /// </summary>
    public class MachineInfoHelper
    {
        /// <summary>
        /// 获取当前使用的IPV4地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            System.Net.IPHostEntry ipHostEntrys = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            List<string> ipList = GetIPAddressList();
            foreach (string ip in ipList)
            {
                ipAddress = ip.ToString();
                break;
            }
            return ipAddress;
        }

        /// <summary>
        /// 获取IPv4地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetIPAddressList()
        {
            List<string> ipAddressList = new List<string>();

            IPHostEntry ipHostEntrys = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (IPAddress ip in ipHostEntrys.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddressList.Add(ip.ToString());
                }
            }
            return ipAddressList;
        }

        /// <summary>
        /// GetWirelessIPList 获得无线网络接口的IpV4 地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetWirelessIPAddressList()
        {
            List<string> ipAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                if (ni.Description.Contains("Wireless"))
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            ipAddressList.Add(ip.Address.ToString());

                        }
                    }
                }
            }
            return ipAddressList;
        }

        /// <summary>
        /// 获取MAC地址
        /// </summary>
        /// <returns>地址</returns>
        public static string GetMacAddress()
        {
            string macAddress = string.Empty;
            List<string> macAddressList = GetMacAddressList();

            foreach (string mac in macAddressList)
            {
                if (!string.IsNullOrEmpty(mac))
                {
                    macAddress = mac.ToString();
                    //格式化
                    macAddress = string.Format("{0}-{1}-{2}-{3}-{4}-{5}",
                        macAddress.Substring(0, 2),
                        macAddress.Substring(2, 2),
                        macAddress.Substring(4, 2),
                        macAddress.Substring(6, 2),
                        macAddress.Substring(8, 2),
                        macAddress.Substring(10, 2));
                    break;
                }
            }
            return macAddress;
        }

        /// <summary>
        /// 获取MAC地址列表，注意优先级高的放在了后面
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacAddressList()
        {
            List<string> macAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                //网卡描述中有wireless，则判断是无限网卡,过滤掉虚拟网卡和移动网卡

                if (!ni.Description.Contains("WiFi") && !ni.Description.Contains("Loopback") && !ni.Description.Contains("VMware") && ni.OperationalStatus == OperationalStatus.Up)
                {
                    macAddressList.Add(ni.GetPhysicalAddress().ToString());
                }
            }
            return macAddressList;
        }

        /// <summary>
        /// GetWirelessMacList 获得无线网络接口的MAC地址列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetWirelessMacAddressList()
        {
            List<string> macAddressList = new List<string>();
            NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in networkInterfaces)
            {
                //网卡描述中有wireless，则判断是无限 网卡

                if (ni.Description.Contains("Wireless") && ni.OperationalStatus == OperationalStatus.Up)
                {
                    macAddressList.Add(ni.GetPhysicalAddress().ToString());
                }
            }
            return macAddressList;
        }

        //CPU序列号
        public static string GetCPUSerialNo()
        {
            string cpuSerialNo = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_Processor");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                cpuSerialNo = managementObject.Properties["ProcessorId"].Value.ToString();
                break;
            }
            return cpuSerialNo;
        }

        public static string GetHardDiskInfo()
        {
            string hardDisk = string.Empty;
            ManagementClass managementClass = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                // 可能是有多个
                hardDisk = (string)managementObject.Properties["Model"].Value;
                break;
            }
            return hardDisk;
        }

        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc =
                 new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk =
                 new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }  

        public static string getMachineInfoNum()
        {
            string strNum = GetCPUSerialNo() + "|" + GetDiskVolumeSerialNumber() + "|" + GetMacAddress();//获得24位Cpu和硬盘序列号
            string strMNum = SecretHelper.AESEncrypt(strNum);
            return strMNum;
        }

        public static IList<string> GetPermanentMacAddresses()
        {
            List<string> naInfo = NetworkAdapter.GetNetworkAdapterInformation(true).Select(i => i.PermanentAddress).Where(a => a != null).ToList();
            IList<string> macAddresses = new List<string>();
            naInfo.ForEach(i => macAddresses.Add(i.Replace(":", "")));
            return macAddresses;
        }

        private class NetworkAdapterInformation
        {
            public String PNPDeviceID;      // 设备ID
            public UInt32 Index;            // 在系统注册表中的索引号
            public String ProductName;      // 产品名称
            public String ServiceName;      // 服务名称

            public String MACAddress;       // 网卡当前物理地址
            public String PermanentAddress; // 网卡原生物理地址

            public String IPv4Address;      // IP 地址
            public String IPv4Subnet;       // 子网掩码
            public String IPv4Gateway;      // 默认网关
            public Boolean IPEnabled;       // 有效状态      
        }

        /// <summary>
        /// 基于WMI获取本机真实网卡信息
        /// </summary>
        private static class NetworkAdapter
        {
            /// <summary>
            /// 获取本机真实网卡信息，包括物理地址和IP地址
            /// </summary>
            /// <param name="isIncludeUsb">是否包含USB网卡，默认为不包含</param>
            /// <returns>本机真实网卡信息</returns>
            public static NetworkAdapterInformation[] GetNetworkAdapterInformation(Boolean isIncludeUsb = false)
            {   // IPv4正则表达式
                const String IPv4RegularExpression = "^(?:(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(?:25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))$";

                // 注意：只获取已连接的网卡
                String NetworkAdapterQueryString;
                if (isIncludeUsb)
                    //NetworkAdapterQueryString = "SELECT * FROM Win32_NetworkAdapter WHERE (NetConnectionStatus = 2) AND (MACAddress IS NOT NULL) AND (NOT (PNPDeviceID LIKE 'ROOT%'))";
                    NetworkAdapterQueryString = "SELECT * FROM Win32_NetworkAdapter WHERE (MACAddress IS NOT NULL) AND (NOT (PNPDeviceID LIKE 'ROOT%'))";
                else
                    //NetworkAdapterQueryString = "SELECT * FROM Win32_NetworkAdapter WHERE (NetConnectionStatus = 2) AND (MACAddress IS NOT NULL) AND (NOT (PNPDeviceID LIKE 'ROOT%')) AND (NOT (PNPDeviceID LIKE 'USB%'))";
                    NetworkAdapterQueryString = "SELECT * FROM Win32_NetworkAdapter WHERE (MACAddress IS NOT NULL) AND (NOT (PNPDeviceID LIKE 'ROOT%')) AND (NOT (PNPDeviceID LIKE 'USB%'))";

                using (ManagementObjectSearcher ManagementObjectSearcher = new ManagementObjectSearcher(NetworkAdapterQueryString))
                {
                    ManagementObjectCollection NetworkAdapterQueryCollection = ManagementObjectSearcher.Get();
                    if (NetworkAdapterQueryCollection == null) return null;

                    List<NetworkAdapterInformation> NetworkAdapterInformationCollection = new List<NetworkAdapterInformation>(NetworkAdapterQueryCollection.Count);
                    foreach (ManagementObject mo in NetworkAdapterQueryCollection)
                    {
                        NetworkAdapterInformation NetworkAdapterItem = new NetworkAdapterInformation();
                        NetworkAdapterItem.PNPDeviceID = mo["PNPDeviceID"] as String;
                        NetworkAdapterItem.Index = (UInt32)mo["Index"];
                        NetworkAdapterItem.ProductName = mo["ProductName"] as String;
                        NetworkAdapterItem.ServiceName = mo["ServiceName"] as String;
                        NetworkAdapterItem.MACAddress = mo["MACAddress"] as String; // 网卡当前物理地址

                        // 网卡原生物理地址
                        NetworkAdapterItem.PermanentAddress = GetNetworkAdapterPermanentAddress(NetworkAdapterItem.PNPDeviceID);

                        // 获取网卡配置信息
                        String ConfigurationQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index = " + NetworkAdapterItem.Index.ToString();
                        using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(ConfigurationQueryString))
                        {
                            ManagementObjectCollection ConfigurationQueryCollection = managementObjectSearcher.Get();
                            if (ConfigurationQueryCollection == null) continue;

                            foreach (ManagementObject nacmo in ConfigurationQueryCollection)
                            {
                                String[] IPCollection = nacmo["IPAddress"] as String[]; // IP地址
                                if (IPCollection != null)
                                {
                                    foreach (String adress in IPCollection)
                                    {
                                        Match match = Regex.Match(adress, IPv4RegularExpression);
                                        if (match.Success) { NetworkAdapterItem.IPv4Address = adress; break; }
                                    }
                                }

                                IPCollection = nacmo["IPSubnet"] as String[];   // 子网掩码
                                if (IPCollection != null)
                                {
                                    foreach (String address in IPCollection)
                                    {
                                        Match match = Regex.Match(address, IPv4RegularExpression);
                                        if (match.Success) { NetworkAdapterItem.IPv4Subnet = address; break; }
                                    }
                                }

                                IPCollection = nacmo["DefaultIPGateway"] as String[];   // 默认网关
                                if (IPCollection != null)
                                {
                                    foreach (String address in IPCollection)
                                    {
                                        Match match = Regex.Match(address, IPv4RegularExpression);
                                        if (match.Success) { NetworkAdapterItem.IPv4Gateway = address; break; }
                                    }
                                }

                                NetworkAdapterItem.IPEnabled = (Boolean)nacmo["IPEnabled"];
                            }

                            NetworkAdapterInformationCollection.Add(NetworkAdapterItem);
                        }
                    }

                    if (NetworkAdapterInformationCollection.Count > 0) return NetworkAdapterInformationCollection.ToArray(); else return null;
                }
            }

            /// <summary>
            /// 获取网卡原生物理地址
            /// </summary>
            /// <param name="PNPDeviceID">设备ID</param>
            /// <returns>网卡原生物理地址</returns>
            private static String GetNetworkAdapterPermanentAddress(String PNPDeviceID)
            {
                const UInt32 FILE_SHARE_READ = 0x00000001;
                const UInt32 FILE_SHARE_WRITE = 0x00000002;
                const UInt32 OPEN_EXISTING = 3;
                const UInt32 OID_802_3_PERMANENT_ADDRESS = 0x01010101;
                const UInt32 IOCTL_NDIS_QUERY_GLOBAL_STATS = 0x00170002;
                IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

                // 生成设备路径名
                String DevicePath = "\\\\.\\" + PNPDeviceID.Replace('\\', '#') + "#{ad498944-762f-11d0-8dcb-00c04fc3358c}";

                // 获取设备句柄
                IntPtr hDeviceFile = NativeMethods.CreateFile(DevicePath, 0, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
                if (hDeviceFile != INVALID_HANDLE_VALUE)
                {
                    Byte[] ucData = new Byte[8];
                    Int32 nBytesReturned;

                    // 获取原生MAC地址
                    UInt32 dwOID = OID_802_3_PERMANENT_ADDRESS;
                    Boolean isOK = NativeMethods.DeviceIoControl(hDeviceFile, IOCTL_NDIS_QUERY_GLOBAL_STATS, ref dwOID, Marshal.SizeOf(dwOID), ucData, ucData.Length, out nBytesReturned, IntPtr.Zero);
                    NativeMethods.CloseHandle(hDeviceFile);
                    if (isOK)
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder(nBytesReturned * 3);
                        foreach (Byte b in ucData)
                        {
                            sb.Append(b.ToString("X2"));
                            sb.Append(':');
                        }
                        return sb.ToString(0, nBytesReturned * 3 - 1);
                    }
                }

                return null;
            }

            private static class NativeMethods
            {
                [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
                internal static extern IntPtr CreateFile(
                    [MarshalAs(UnmanagedType.LPWStr)]String lpFileName,
                    UInt32 dwDesiredAccess,
                    UInt32 dwShareMode,
                    IntPtr lpSecurityAttributes,
                    UInt32 dwCreationDisposition,
                    UInt32 dwFlagsAndAttributes,
                    IntPtr hTemplateFile
                    );

                [DllImport("kernel32.dll", SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                internal static extern Boolean CloseHandle(IntPtr hObject);

                [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                internal static extern Boolean DeviceIoControl(
                    IntPtr hDevice,
                    UInt32 dwIoControlCode,
                    ref UInt32 lpInBuffer,
                    Int32 nInBufferSize,
                    Byte[] lpOutBuffer,
                    Int32 nOutBufferSize,
                    out Int32 nBytesReturned,
                    IntPtr lpOverlapped
                    );
            }
        }

        public static string RemoveInvalidFileNameChars(string filePathName)
        {
            StringBuilder value = new StringBuilder(filePathName);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                value.Replace(c.ToString(), "");
            }
            return value.ToString();
        }

        public static bool PathIsNetworkPath(string pszPath)
        {
            return NativeMethods.PathIsNetworkPath(pszPath);
        }

        private static class NativeMethods
        {
            [DllImport("shlwapi.dll", BestFitMapping = false)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PathIsNetworkPath([MarshalAs(UnmanagedType.LPStr)]string pszPath);
        }
    }
}