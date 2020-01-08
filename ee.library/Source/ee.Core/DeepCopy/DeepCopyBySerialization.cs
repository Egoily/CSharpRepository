using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ee.Core.DeepCopy
{
    public static class DeepCopyBySerialization
    {
        public static T DeepCopy<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
    }
}