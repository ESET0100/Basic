using System.Runtime.InteropServices;

using static Program.cs.Program;

namespace Day2_assignment

{

    internal class Program

    {

        // Meter object structure and array

        public struct MeterObject

        {

            public byte[] obis;

            public string name;

            public int value;

        }

        private static MeterObject[] meterobjects;

        private static int meterObjectCount;

        // Initialize meter objects (this would typically be called once at startup)

        public static void InitializeMeterObjects()

        {

            meterObjectCount = 5;

            meterobjects = new MeterObject[meterObjectCount];

            // Sample meter objects with OBIS codes

            meterobjects[0] = new MeterObject { obis = new byte[] { 1, 0, 1, 8, 0, 255 }, name = "Active Energy Import", value = 12345 };

            meterobjects[1] = new MeterObject { obis = new byte[] { 1, 0, 2, 8, 0, 255 }, name = "Active Energy Export", value = 6789 };

            meterobjects[2] = new MeterObject { obis = new byte[] { 1, 0, 3, 8, 0, 255 }, name = "Reactive Energy Import", value = 5432 };

            meterobjects[3] = new MeterObject { obis = new byte[] { 1, 0, 4, 8, 0, 255 }, name = "Reactive Energy Export", value = 9876 };

            meterobjects[4] = new MeterObject { obis = new byte[] { 0, 0, 96, 1, 0, 255 }, name = "Device ID", value = 1001 };

        }

        // ====GET SERVICE====

        public static void dlms_get(byte[] obis)

        {

            for (int i = 0; i < meterObjectCount; i++)

            {

                if (obis_match(obis, meterobjects[i].obis))

                {

                    Console.WriteLine("[GET] " + meterobjects[i].name + " = " + meterobjects[i].value);

                    return;

                }

            }

            Console.WriteLine("[GET] OBIS not found.");

        }

        //===SET Service===

        public static void dlms_set(byte[] obis, int newvalue)

        {

            for (int i = 0; i < meterObjectCount; i++)

            {

                if (obis_match(obis, meterobjects[i].obis))

                {

                    meterobjects[i].value = newvalue;

                    Console.WriteLine("[SET] " + meterobjects[i].name + " updated to " + newvalue);

                    return;

                }

            }

            Console.WriteLine("[GET] OBIS not found.");

        }

        // ====ACTION Service====

        public static void dlms_action(byte[] obis, string method)

        {

            if (string.Compare(method, "RESET") == 0)

            {

                Console.WriteLine("[ACTION] Resetting meter values...");

                for (int i = 0; i < meterObjectCount; i++)

                {

                    meterobjects[i].value = 0;

                }

                Console.WriteLine("[ACTION] Reset complete.");

            }

            else if (string.Compare(method, "SYNC_TIME") == 0)

            {

                Console.WriteLine("[ACTION] Synchronizing meter clock to system time...");

            }

            else

            {

                Console.WriteLine("[ACTION] Unknown method.");

            }

        }

        // ==== READ (Low-Level) Service – Short Name referencing ====

        public static void dlms_read_sn(ushort shortName)

        {

            Console.WriteLine("[READ-SN] Reading object with Short Name: 0x" + shortName.ToString("X4"));

        }

        // ==== WRITE (Low-Level) Service – Short Name referencing ====

        public static void dlms_write_sn(ushort shortName, int newValue)

        {

            Console.WriteLine("[WRITE-SN] writing value " + newValue + " to Short Name: 0x" + shortName.ToString("X4"));

        }

        // OBIS match function

        private static bool obis_match(byte[] obis1, byte[] obis2)

        {

            if (obis1 == null || obis2 == null || obis1.Length != 6 || obis2.Length != 6)

                return false;

            for (int i = 0; i < 6; i++)

            {

                if (obis1[i] != obis2[i])

                    return false;

            }

            return true;

        }

        // Helper method to create byte array from OBIS string (e.g., "1.0.1.8.0.255")

        public static byte[] ParseObis(string obisString)

        {

            string[] parts = obisString.Split('.');

            if (parts.Length != 6)

                throw new ArgumentException("OBIS code must have 6 parts");

            byte[] obis = new byte[6];

            for (int i = 0; i < 6; i++)

            {

                obis[i] = byte.Parse(parts[i]);

            }

            return obis;

        }

        static void Main(string[] args)

        {

            // Initialize meter objects

            InitializeMeterObjects();

            // Test GET service

            byte[] testObis = new byte[] { 1, 0, 1, 8, 0, 255 };

            dlms_get(testObis);

            // Test SET service

            dlms_set(testObis, 9999);

            dlms_get(testObis);

            // Test ACTION service

            dlms_action(testObis, "RESET");

            dlms_get(testObis);

            // Test unknown action

            dlms_action(testObis, "UNKNOWN");

            // Test READ-SN service

            dlms_read_sn(0x1234);

            // Test WRITE-SN service

            dlms_write_sn(0x5678, 200);

            // Test OBIS not found

            byte[] unknownObis = new byte[] { 9, 9, 9, 9, 9, 9 };

            dlms_get(unknownObis);

            dlms_set(unknownObis, 100);

        }

    }

}

