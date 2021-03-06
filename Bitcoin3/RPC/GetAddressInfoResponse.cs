#if !NOJSONNET
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Bitcoin3.RPC
{
	public class GetAddressInfoResponse : GetAddressInfoScriptInfoResponse
	{
		public bool IsMine { get; private set; }
		public bool? Solvable { get; private set; }
		public ScanTxoutDescriptor Desc { get; private set; }

		// present only in p2sh-nested case
		public GetAddressInfoScriptInfoResponse Embedded { get; private set; }
		public string Label { get; private set; }
		public bool? IsChange { get; private set; }
		public bool IsWatchOnly { get; private set; }
		public DateTimeOffset? Timestamp { get; private set; }
		public KeyPath HDKeyPath { get; private set; }
		public uint160 HDSeedID { get; private set; }
		public uint160 HDMasterKeyID { get; private set; }
		public List<Dictionary<string, string>> Labels { get; private set; } = new List<Dictionary<string, string>>();

		public bool? IsCompressed { get; private set; }

		public static GetAddressInfoResponse FromJsonResponse(JObject raw, Network network)
		{
			var result = new GetAddressInfoResponse();
			SetSubInfo(result, raw, network);
			result.IsMine = raw.Property("ismine").Value.Value<bool>();
			result.Solvable = raw.Property("solvable")?.Value.Value<bool>();
			result.Desc = raw.Property("desc") == null ? null : new ScanTxoutDescriptor(raw.Property("desc").Value.Value<string>());
			result.IsWatchOnly = raw.Property("iswatchonly").Value.Value<bool>();
			result.IsScript = raw.Property("isscript").Value.Value<bool>();
			result.IsWitness = raw.Property("iswitness").Value.Value<bool>();
			result.Script = raw.Property("script")?.Value.Value<string>();
			result.Hex = raw.Property("hex")?.Value.Value<string>();
			var jEmbedded = raw.Property("embedded");
			if (jEmbedded != null)
			{
				var j = jEmbedded.Value.Value<JObject>();
				var e = new GetAddressInfoScriptInfoResponse();
				SetSubInfo(e, j, network);
				result.Embedded = e;
			}
			result.IsCompressed = raw.Property("iscompressed")?.Value.Value<bool>();
			result.Label = raw.Property("label").Value.Value<string>();
			result.IsChange = raw.Property("ischange")?.Value.Value<bool>();
			result.Timestamp = raw.Property("timestamp") == null ? (DateTimeOffset?)null : Utils.UnixTimeToDateTime(raw.Property("timestamp").Value.Value<ulong>());
			result.HDKeyPath = raw.Property("hdkeypath") == null ? null : KeyPath.Parse(raw.Property("hdkeypath").Value.Value<string>());
			result.HDSeedID = raw.Property("hdseedid") == null ? null : uint160.Parse(raw.Property("hdseedid").Value.Value<string>());
			result.HDMasterKeyID = raw.Property("hdmasterkeyid") == null ? null : uint160.Parse(raw.Property("hdmasterkeyid").Value.Value<string>());
			var jlabels = raw.Property("labels");
			if (jlabels != null)
			{
				var labelObjects = jlabels.Value.Value<JArray>();
				foreach (var jObj in labelObjects)
				{
					result.Labels.Add(((JObject)jObj).ToObject<Dictionary<string, string>>());
				}
			}

			return result;
		}

		private static void SetSubInfo(GetAddressInfoScriptInfoResponse target, JObject raw, Network network)
		{
			target.IsWitness = raw.Property("iswitness").Value.Value<bool>();
			target.IsScript = raw.Property("isscript").Value.Value<bool>();
			target.Address = BitcoinAddress.Create(raw.Property("address").Value.Value<string>(), network);
			target.ScriptPubKey = new Script(raw.Property("scriptPubKey").Value.Value<string>());
			target.PubKey = raw.Property("pubkey") == null ? null : new PubKey(raw.Property("pubkey").Value.Value<string>());
			var pubkeys = raw.Property("pubkeys");
			if (pubkeys != null)
			{
				foreach (var pk in pubkeys.Value.Values<string>())
					target.PubKeys.Add(new PubKey(pk));
			}
			target.SigsRequired = raw.Property("sigsrequired")?.Value.Value<uint>();
			target.WitnessVersion = raw.Property("witness_version")?.Value.Value<int>();
			target.WitnessProgram = raw.Property("witness_program")?.Value.Value<string>();
		}
	}

	public class GetAddressInfoScriptInfoResponse
	{
		public BitcoinAddress Address { get; internal set; }
		public Script ScriptPubKey { get; internal set; }
		public bool IsScript { get; internal set; }
		public bool IsWitness { get; internal set; }

		// present only in a witness address.
		public int? WitnessVersion { get; internal set; }
		// present only in a witness address.
		public string WitnessProgram { get; internal set; }
		public string Script { get; internal set; }
		public string Hex { get; internal set; }
		public PubKey PubKey { get; internal set; }
		public List<PubKey> PubKeys { get; internal set; }
		public uint? SigsRequired { get; internal set; }
	}
}
#endif