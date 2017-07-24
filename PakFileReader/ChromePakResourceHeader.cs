using System;

namespace PakFileReader
{
    /// <summary>
    /// Resource Information
    /// </summary>
    public class ChromePakResourceHeader : IEquatable<ChromePakResourceHeader>
    {
        /// <summary>
        /// Resource Id
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Resource Position in pak file.
        /// </summary>
        public int Offset { get; }

        public ChromePakResourceHeader(int id, int offset)
        {
            Id = id;
            Offset = offset;
        }

        public bool Equals(ChromePakResourceHeader other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Offset == other.Offset;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ChromePakResourceHeader)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ Offset;
            }
        }
    }
}
