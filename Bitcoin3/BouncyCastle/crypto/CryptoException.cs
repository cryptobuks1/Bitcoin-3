using System;

namespace Bitcoin3.BouncyCastle.Crypto
{
	internal class CryptoException
		: Exception
	{
		public CryptoException()
		{
		}

		public CryptoException(
			string message)
			: base(message)
		{
		}

		public CryptoException(
			string message,
			Exception exception)
			: base(message, exception)
		{
		}
	}
}
