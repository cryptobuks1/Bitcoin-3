﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin3.Payment
{
	[Obsolete("BIP70 is obsolete")]
	public interface IChainChecker
	{
		bool VerifyChain(byte[] certificate, byte[][] additionalCertificates);
	}
}
