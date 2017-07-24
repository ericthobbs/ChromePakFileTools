using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace PakFileReader
{

    /// <summary>
    /// Chrome Pak File
    /// </summary>
    public class ChromePakfile
    {
        public string PakFilename { get; }

        public int Version { get; private set; }
        public int ResourceCount { get; private set; }
        public int Encoding { get; private set; }

        public List<ChromePakResourceHeader> ResourceHeaders { get; }


        /// <summary>
        /// Chrome Pak file archive utility
        /// </summary>
        /// <param name="filename"></param>
        public ChromePakfile(string filename)
        {
            ResourceHeaders = new List<ChromePakResourceHeader>();
            PakFilename = filename;
            Load();
        }

        /// <summary>
        /// Get the specified resource
        /// </summary>
        /// <param name="resourceId"></param>
        /// <returns>Raw Resource Bytes</returns>
        public byte[] GetResource(int resourceId)
        {
            if (resourceId == 0)
                return null;

            var resource = ResourceHeaders.Single(x => x.Id == resourceId);
            var nextResource = ResourceHeaders[ResourceHeaders.IndexOf(resource)+1];

            var resourceLength = nextResource.Offset - resource.Offset;
            
            Debug.Assert(resourceLength > 0, "Resource Length must be larger then zero.");

            using (var fs = File.OpenRead(PakFilename))
            {
                var resourceBytes = new byte[resourceLength];

                fs.Seek(resource.Offset, SeekOrigin.Begin);

                var readLen = fs.Read(resourceBytes, 0, resourceLength);

                if(readLen != resourceLength)
                    throw new IOException("Failed to read resource.");

                return resourceBytes;
            }
        }

        /// <summary>
        /// Update the specified resource.
        /// </summary>
        /// <param name="resourceId">Resource Id to update.</param>
        /// <param name="newBytes">new resource bytes.</param>
        public void UpdateResource(int resourceId, byte[] newBytes)
        {
            var oldData = GetResource(resourceId);

            var resource = ResourceHeaders.Single(x => x.Id == resourceId);

            if (newBytes.Length != oldData.Length)
                throw new ArgumentOutOfRangeException(nameof(resourceId), "Data lengths must be equal.");

            using (var fs = File.OpenWrite(PakFilename))
            {
                var position = fs.Seek(resource.Offset, SeekOrigin.Begin);

                if(position != resource.Offset)
                    throw new IOException("Failed to seek to the correction position in the pack archive.");

                fs.Write(newBytes, 0, oldData.Length);
            }
        }

        /// <summary>
        /// Load the pak file
        /// </summary>
        private void Load()
        {
            using (var fs = File.OpenRead(PakFilename))
            {

                //
                // Read Pak Header
                //

                var buffer = new byte[4];
                var readLen = fs.Read(buffer, 0, 4);

                if (readLen != buffer.Length)
                {
                    throw new IOException("header is invalid. Not a pak file. Length is too short.");
                }

                Version = BitConverter.ToInt32(buffer, 0);

                buffer = new byte[4];
                readLen = fs.Read(buffer, 0, 4);

                if (readLen != buffer.Length)
                {
                    throw new IOException("header is invalid. Not a pak file. Length is too short.");
                }

                ResourceCount = BitConverter.ToInt32(buffer, 0);

                Encoding = fs.ReadByte();

                //
                // Read Resource Headers
                //

                while (ResourceHeaders.Count <= ResourceCount)
                {
                    buffer = new byte[6];
                    readLen = fs.Read(buffer, 0, 6);

                    if (readLen != 6)
                    {
                        throw new IOException("header is invalid. Not a valid pak file. Length is too short.");
                    }

                    var resourceId = BitConverter.ToChar(buffer, 0);
                    var resourceOffset = BitConverter.ToInt32(buffer, 2);

                    ResourceHeaders.Add(new ChromePakResourceHeader(resourceId, resourceOffset));
                }
            }
        }
    }
}
