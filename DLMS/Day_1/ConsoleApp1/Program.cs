/*Assignment - DLMS/COSEM communication example
in C# .net, demonstrating how to:

Define and pack an OBIS code

Construct a GET request APDU*/

using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // OBIS Code class
    public class ObisCode
    {
        public byte A { get; set; }
        public byte B { get; set; }
        public byte C { get; set; }
        public byte D { get; set; }
        public byte E { get; set; }
        public byte F { get; set; }

        public ObisCode(byte a, byte b, byte c, byte d, byte e, byte f)
        {
            A = a; B = b; C = c; D = d; E = e; F = f;
        }

        public byte[] ToByteArray()
        {
            return new byte[] { A, B, C, D, E, F };
        }

        public override string ToString()
        {
            return $"{A}-{B}:{C}.{D}.{E}.{F}";
        }
    }

    // GET Request APDU builder
    public class GetRequestApdu
    {
        public static byte[] CreateGetRequest(ObisCode obis, ushort classId, byte attributeId)
        {
            List<byte> apdu = new List<byte>();

            // GET-Request tag
            apdu.Add(0xC0); // GET-Request
            apdu.Add(0x01); // Get-Request-Normal
            // Invoke ID and Priority
            apdu.Add(0x01); // Example: Invoke ID = 1, normal priority

            // Class ID (2 bytes)
            apdu.Add((byte)(classId >> 8)); // High byte
            apdu.Add((byte)(classId & 0xFF)); // Low byte

            // OBIS code
            apdu.AddRange(obis.ToByteArray());

            // Attribute ID
            apdu.Add(attributeId);

            // Access Selector (0 = no selective access)
            apdu.Add(0x00);

            return apdu.ToArray();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // OBIS code for Active Energy Import (1-0:1.8.0.255)
            var obis = new ObisCode(1, 0, 1, 8, 0, 255);

            // Class ID for Register object is 3
            ushort classId = 3;
            byte attributeId = 2; // e.g., value attribute

            byte[] apdu = GetRequestApdu.CreateGetRequest(obis, classId, attributeId);

            Console.WriteLine("OBIS Code: " + obis);
            Console.WriteLine("GET Request APDU: " + BitConverter.ToString(apdu));
        }
    }
}
