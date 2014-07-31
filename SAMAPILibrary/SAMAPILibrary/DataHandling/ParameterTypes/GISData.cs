using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAMAPILibrary.DataObjects
{
    /// <summary>
    /// Data about the "roof" surface that an array will be placed on
    /// </summary>
    public class GISData
    {
        /// <summary>
        /// Surface tilt in degrees
        /// </summary>
        public readonly float tilt; //in degrees
        /// <summary>
        /// Surface azimuth in degrees
        /// </summary>
        public readonly float azimuth; //in degrees
        /// <summary>
        /// Surface latitude in degrees (+N/-S)
        /// </summary>
        public readonly float latitude; //in degrees
        /// <summary>
        /// Surface longitude in degrees (+E/-W)
        /// </summary>
        public readonly float longitude; //in degrees
        /// <summary>
        /// Width of the surface in meters
        /// </summary>
        public readonly float width; //in meters
        /// <summary>
        /// Height of the surface in meters
        /// </summary>
        public readonly float height; //in meters

        public GISData()
        {
            //defaults
            tilt = 20;
            azimuth = 180;
            latitude = 33;
            longitude = -112;

            width = 2.5f;
            height = 9.1f;
        }
    }

    public class GISDataBuilder
    {
        public GISDataBuilder()
        {
        }

        public GISData build()
        {
            GISData d = new GISData();
            return d;
        }
    }
}
