using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;

namespace CoreMobileInternals
{
	public class DataCache
	{
		private Dictionary<string, string> cache;

		public DataCache ()
		{
			try{
				using(Stream fs = new IsolatedStorageFileStream("cache", FileMode.Open)){
					BinaryReader br = new BinaryReader(fs);
					int count = br.ReadInt32();
					cache = new Dictionary<string, string>(count);
					for(int i=0; i < count; i++){
						string key = br.ReadString ();
						string val = br.ReadString ();
						cache.Add (key, val);
					}
					br.Close ();
				}
			}
			catch(Exception){
				cache = new Dictionary<string, string> ();
			}
		}

		~DataCache(){
			expireData ();
			flush ();
		}

		public void flush(){
			try {
				using(Stream fs = new IsolatedStorageFileStream("cache", FileMode.Create)){
					BinaryWriter bw = new BinaryWriter(fs);
					bw.Write(cache.Count);
					foreach(var entry in cache){
						bw.Write (entry.Key);
						bw.Write (entry.Value);
					}
					bw.Flush ();
					bw.Close ();
				}
			}
			catch(Exception){
			}
		}

		public string get(string key){
			if (!cache.ContainsKey (key))
				return null;

			string val = cache [key];
			string[] parts = val.Split (new char[] { ':' }, 2);

			if (parts.Length != 2) {
				cache.Remove (key);
				return null;
			}

			int expires = int.Parse (parts [0]);
			val = parts [1];

			int now = (int)(DateTime.Now - new DateTime (1970, 1, 1).ToLocalTime ()).TotalSeconds;
			if (expires < now){
				cache.Remove (key);
				return null;
			}

			return val;
		}

		public void set(string key, string value){
			set (key, value, DateTime.Now.AddDays (1));
		}

		public void set(string key, string value, DateTime dt){
			int expires = (int)(dt - new DateTime (1970, 1, 1).ToLocalTime ()).TotalSeconds;
			cache [key] = expires.ToString ()+":"+value;
		}

		public void expireData(){
			int now = (int)(DateTime.Now - new DateTime (1970, 1, 1).ToLocalTime ()).TotalSeconds;
			foreach (var entry in cache) {
				string[] parts = entry.Value.Split (new char[] { ':' }, 2);
				if (parts.Length != 2)
					cache.Remove (entry.Key);
				else if (int.Parse (parts[0]) < now)
					cache.Remove (entry.Key);
			}
		}
	}
}

