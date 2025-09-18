using System;
using System.Runtime.InteropServices;

public class DlmsService
{
    // Assuming these are defined elsewhere in your C# project
    private static MeterObject[] meterObjects;
    private static int meterObjectCount;
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MeterObject
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] obis;
        public string name;
        public int value;
    }

    // ==== GET Service ====
    public static void DlmsGet(byte[] obis)
    {
        for (int i = 0; i < meterObjectCount; i++)
        {
            if (ObisMatch(obis, meterObjects[i].obis))
            {
                Console.WriteLine($"[GET] {meterObjects[i].name} = {meterObjects[i].value}");
                return;
            }
        }
        Console.WriteLine("[GET] OBIS not found.");
    }

    // ==== SET Service ====
    public static void DlmsSet(byte[] obis, int newValue)
    {
        for (int i = 0; i < meterObjectCount; i++)
        {
            if (ObisMatch(obis, meterObjects[i].obis))
            {
                meterObjects[i].value = newValue;
                Console.WriteLine($"[SET] {meterObjects[i].name} updated to {newValue}");
                return;
            }
        }
        Console.WriteLine("[SET] OBIS not found."); // Fixed the message from "[GET]" to "[SET]"
    }

    // ==== ACTION Service ====
    public static void DlmsAction(byte[] obis, string method)
    {
        if (string.Equals(method, "RESET", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("[ACTION] Resetting meter values...");
            for (int i = 0; i < meterObjectCount; i++)
            {
                meterObjects[i].value = 0;
            }
            Console.WriteLine("[ACTION] Reset complete.");
        }
        else if (string.Equals(method, "SYNC_TIME", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("[ACTION] Synchronizing meter clock to system time...");
            // Time sync logic here
        }
        else
        {
            Console.WriteLine("[ACTION] Unknown method.");
        }
    }

    // ==== READ (Low-Level) Service – Short Name referencing ====
    public static void DlmsReadSn(ushort shortName)
    {
        Console.WriteLine($"[READ-SN] Reading object with Short Name: 0x{shortName:X4}");
        // Here, you'd map SN to OBIS and return the value
    }

    // ==== WRITE (Low-Level) Service – Short Name referencing ====
    public static void DlmsWriteSn(ushort shortName, int newValue)
    {
        Console.WriteLine($"[WRITE-SN] Writing value {newValue} to Short Name: 0x{shortName:X4}");
        // Here, you'd map SN to OBIS and set the value
    }

    // Helper method for OBIS matching (assuming implementation)
    private static bool ObisMatch(byte[] obis1, byte[] obis2)
    {
        if (obis1.Length != 6 || obis2.Length != 6)
            return false;
        for (int i = 0; i < 6; i++)
        {
            if (obis1[i] != obis2[i])
                return false;
        }
        return true;
    }
}

// Example usage:
public class Program
{
    public static void Main()
    {
        // Initialize your meter objects array here
        // DlmsService.meterObjects = new MeterObject[10];
        // DlmsService.meterObjectCount = 5;
        byte[] testObis = new byte[6] { 1, 0, 1, 8, 0, 255 };
        // Test the services
        DlmsService.DlmsGet(testObis);
        DlmsService.DlmsSet(testObis, 100);
        DlmsService.DlmsAction(testObis, "RESET");
        DlmsService.DlmsReadSn(0x1234);
        DlmsService.DlmsWriteSn(0x5678, 200);
    }
}