using NUnit.Framework;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Bogosoft.Hashing.Cryptography.Tests
{
    [TestFixture, Category("Unit")]
    public class UnitTests
    {
        [TestCase]
        public void ComputedHashesMatchThoseOfTheirUnderlyingCounterparts()
        {
            var hashable = new MockHashableObject();

            var hash = hashable.GetHashBytes();

            var tests = new Dictionary<Type, IHash>
            {
                { typeof(MD5CryptoServiceProvider), CryptoHashStrategy.MD5 },
                { typeof(RIPEMD160Managed), CryptoHashStrategy.RIPEMD160 },
                { typeof(SHA1CryptoServiceProvider), CryptoHashStrategy.SHA1 },
                { typeof(SHA256CryptoServiceProvider), CryptoHashStrategy.SHA256 },
                { typeof(SHA384CryptoServiceProvider), CryptoHashStrategy.SHA384 },
                { typeof(SHA512CryptoServiceProvider), CryptoHashStrategy.SHA512 }
            };

            foreach(var x in tests)
            {
                using(var algorithm = Activator.CreateInstance(x.Key) as HashAlgorithm)
                {
                    x.Value.Compute(hashable).SequenceEqual(algorithm.ComputeHash(hash)).ShouldBeTrue();
                }
            }
        }
    }
}