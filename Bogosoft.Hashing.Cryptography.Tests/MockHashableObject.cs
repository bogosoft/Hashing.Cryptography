using System;
using System.Collections.Generic;
using System.Linq;

namespace Bogosoft.Hashing.Cryptography.Tests
{
    class MockHashableObject : IHashable
    {
        static IEnumerable<byte> GetRandomByteSequence()
        {
            var rng = new Random();

            var length = rng.Next(4096);

            var index = 0;

            while(index++ < length)
            {
                yield return (byte)rng.Next(0, 255);
            }
        }

        byte[] bytes;

        internal MockHashableObject()
        {
            bytes = GetRandomByteSequence().ToArray();
        }

        public byte[] GetHashBytes()
        {
            return bytes;
        }
    }
}