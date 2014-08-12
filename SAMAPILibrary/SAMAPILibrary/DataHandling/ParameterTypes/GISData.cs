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

        public GISData(GISDataBuilder b)
        {
            tilt = b.mtilt;
            azimuth = b.mazimuth;
            latitude = b.mlatitude;
            longitude = b.mlongitude;
            width = b.mwidth;
            height = b.mheight;
        }
    }

    /// <summary>
    /// Builder for creating a GISData object containing the data needed to represent the building's
    /// rooftop for a potential solar install site.
    /// </summary>
    public class GISDataBuilder
    {
        public float mtilt = 20;
        public float mazimuth = 180;
        public float mlatitude = 33; 
        public float mlongitude = -112;
        public float mwidth = 2.5f; 
        public float mheight = 9.1f; 

        public GISDataBuilder()
        {
        }

        /// <summary>
        /// The tilt/pitch of the rooftop, in degrees
        /// </summary>
        /// <param name="value"></param>
        public void tilt(float value)
        {
            mtilt = value;
        }
        /// <summary>
        /// The azimuth/aspect of the rooftop, in degrees
        /// </summary>
        /// <param name="value"></param>
        public void azimuth(float value)
        {
            mazimuth = value;
        }
        /// <summary>
        /// The location's latitude, in degrees (+N/-S)
        /// </summary>
        /// <param name="value"></param>
        public void latitude(float value)
        {
            mlatitude = value;
        }
        /// <summary>
        /// The location's longitude, in degrees (+E/-W)
        /// </summary>
        /// <param name="value"></param>
        public void longitude(float value)
        {
            mlongitude = value;
        }
        /// <summary>
        /// The rooftop width, in meters
        /// </summary>
        /// <param name="value"></param>
        public void width(float value)
        {
            mwidth = value;
        }
        /// <summary>
        /// The rooftop height, in meters
        /// </summary>
        /// <param name="value"></param>
        public void height(float value)
        {
            mheight = value;
        }

        public GISData build()
        {
            GISData d = new GISData(this);
            return d;
        }
    }
}
