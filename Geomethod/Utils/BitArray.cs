using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Geomethod
{
	public struct BitArray32
	{
		int data;
		public BitArray32(int data){this.data=data;}
        public bool this[int i] { get { i = 1 << i; return (data & i) == i; } set { i = 1 << i; data |= i; if (!value)  data ^= i; } }
		public int Data{get{return data;}set{data=value;}}
		public void Clear(){data=0;}
		public bool IsEmpty{get{return data==0;}}
		public bool NonEmpty{get{return data!=0;}}
		public static implicit operator int(BitArray32 ba){return (int)ba.Data;}
		public static implicit operator BitArray32(int i){return new BitArray32(i);}
    }

	public struct BitArray64
	{
		long data;
		public BitArray64(long data){this.data=data;}
        public bool this[int i] { get { long l = 1L << i; return (data & l) == l; } set { long l = 1L << i; data |= l; if (!value) data ^= l; } }
        public long Data{get{return data;}set{data=value;}}
		public void Clear(){data=0;}
		public bool IsEmpty{get{return data==0;}}
		public bool NonEmpty{get{return data!=0;}}
		public static implicit operator long(BitArray64 ba){return (long)ba.Data;}
		public static implicit operator BitArray64(long i){return new BitArray64(i);}
	}
}
