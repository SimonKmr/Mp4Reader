using System.Text;

namespace Mp4Reader
{
    public partial class Atom
    {
        public class SampleDescription
        {
            public Byte Version { get; set; }
            public UInt32 Entries { get; set; }
            public Entry[] SampleDescriptionTable { get; set; }


            public SampleDescription(byte[] bytes)
            {
                Version = bytes[0];
                Entries = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));

                List<Entry> entries = new List<Entry>();
                int index = 8;


                while(index < bytes.Length)
                {
                    int end = index + BitConverter.ToInt32(Utility.ReverseRange(bytes[(index + 0)..(index + 4)]));
                    entries.Add(new Entry(bytes[index..end]));
                    index = end;
                }
                SampleDescriptionTable = entries.ToArray();
            }

            public class Entry
            {
                public UInt32 Size { get; set; }
                public UInt32 DataFormat { get; set; }
                public UInt16 DataReferenceIndex { get; set; }
                public object MediaData { get; set; }

                public Entry(byte[] bytes)
                {
                    Size = BitConverter.ToUInt32(Utility.ReverseRange(bytes[0..4]));
                    DataFormat = BitConverter.ToUInt32(Utility.ReverseRange(bytes[4..8]));
                    DataReferenceIndex = BitConverter.ToUInt16(Utility.ReverseRange(bytes[14..16]));
                    //throw new NotImplementedException();
                }
            }
        }
    }
}
