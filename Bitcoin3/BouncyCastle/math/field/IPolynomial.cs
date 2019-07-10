﻿namespace Bitcoin3.BouncyCastle.Math.Field
{
	internal interface IPolynomial
	{
		int Degree
		{
			get;
		}

		//BigInteger[] GetCoefficients();

		int[] GetExponentsPresent();

		//Term[] GetNonZeroTerms();
	}
}
