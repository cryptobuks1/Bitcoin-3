﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitcoin3.Protocol
{
	public class ProtocolException : Exception
	{
		public ProtocolException(string message)
			: base(message)
		{

		}
	}
}
