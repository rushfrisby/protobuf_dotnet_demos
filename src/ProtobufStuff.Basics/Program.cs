using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using ProtobufStuff.Common.Entities;
using ProtoBuf;
using ProtoBuf.Meta;

namespace ProtobufStuff.Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new User
            {
                Id = 22,
                FirstName = "Rush",
                LastName = "Frisby",
                EmailAddress = "rfrisby@lsq.com",
                IsActive = true,
                Age = 35
            };

            //SerializeExample(user);
            //SizeExample(user);
            //HexEditor(user);
            //GetProtoExample();
            SurrogateExample();
        }

        private static void SerializeExample(User user)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, user);
                data = ms.ToArray();
            }

            User user2;
            using (var ms = new MemoryStream(data))
            {
                user2 = Serializer.Deserialize<User>(ms);
            }

            Console.WriteLine(JsonConvert.SerializeObject(user));
            Console.WriteLine(JsonConvert.SerializeObject(user2));

            Console.ReadLine();
        }

        private static void SizeExample(User user)
        {
            byte[] data;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, user);
                data = ms.ToArray();
            }

            var json = JsonConvert.SerializeObject(user);

            Console.WriteLine("Protobuf: {0} bytes", data.Length);
            Console.WriteLine("    JSON: {0} bytes", Encoding.ASCII.GetBytes(json).Length);

            Console.ReadLine();
        }

        private static void HexEditor(User user)
        {
            var exampleFile = new FileInfo("protobuf_example.bin");

            using (var fs = new FileStream(exampleFile.FullName, FileMode.Create, FileAccess.Write))
            {
                Serializer.Serialize(fs, user);
            }

            Process.Start(@"C:\Program Files (x86)\HexEdit\HexEdit.exe", exampleFile.FullName);
        }

        private static void GetProtoExample()
        {
            Console.WriteLine(Serializer.GetProto<User>(ProtoSyntax.Proto3));

            Console.ReadLine();
        }

        private static void SurrogateExample()
        {
            RuntimeTypeModel.Default.Add(typeof(DateTimeOffset), true).SetSurrogate(typeof(DateTimeOffsetSurrogate));

            Console.WriteLine(Serializer.GetProto<LoginRequest>(ProtoSyntax.Proto2));

            Console.ReadLine();
        }
    }
}

