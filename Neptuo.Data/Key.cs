using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Data
{
    public class Key
    {
        public int ID { get; protected set; }
        public byte[] Version { get; protected set; }
        public string EntityUri { get; protected set; }

        #region Construction

        public Key(int id, byte[] version, string entityUri = null)
        {
            ID = id;
            Version = version;
            EntityUri = entityUri;
        }

        public static implicit operator Key(int id)
        {
            return new Key(id, null);
        }

        public static bool TryParse(string value, out Key key)
        {
            if (String.IsNullOrEmpty(value))
            {
                key = null;
                return false;
            }

            StringBuilder part = new StringBuilder();

            string entityUri = null;
            int? id = null;
            byte[] version = null;
            foreach (char item in value)
            {
                if (item == ':' && entityUri == null)
                {
                    entityUri = part.ToString();
                    part.Clear();
                    continue;
                }
                else if (item == '[' && id == null)
                {
                    int idValue;
                    if (Int32.TryParse(part.ToString(), out idValue))
                        id = idValue;
                    else
                        break;
                }
                else if (item == ']' && id != null)
                {
                    string[] byteParts = part.ToString().Split(',');
                    List<byte> bytes = new List<byte>();
                    foreach (string bytePart in byteParts)
                    {
                        byte byteValue;
                        if (Byte.TryParse(bytePart, out byteValue))
                            bytes.Add(byteValue);
                        else
                            break;
                    }
                    version = bytes.ToArray();
                }
            }

            if (entityUri != null && id != null)
            {
                key = new Key(id.Value, version, entityUri);
                return true;
            }

            key = null;
            return false;
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{0}:{1}", EntityUri, ID);
        }

        public string ToLongString()
        {
            if (Version == null)
                return ToString();

            return String.Format("{0}:{1}:[{2}]", EntityUri, ID, String.Join(",", Version));
        }
    }
}
