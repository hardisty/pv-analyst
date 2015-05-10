using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAMAPILibrary.SAMAPI;
using System.Reflection;

namespace SAMAPILibrary.DataHandling.Parameters
{
    public abstract class ISettings
    {

        public void applySettings(Data data)
        {
            Type type = this.GetType();
            // Iterate through all the methods of the class. 
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public |  BindingFlags.NonPublic |  BindingFlags.Instance))
            {
                // Iterate through all the Attributes for each method. 
                foreach (Attribute attr in Attribute.GetCustomAttributes(field))
                {
                    // Check for the AnimalType attribute. 
                    if (attr.GetType() == typeof(ParamAttribute))
                    {
                        ParamAttribute pa = (ParamAttribute)attr;
                        if (pa.Type.Equals("float"))
                        {
                            data.SetNumber(pa.StringID, (float)field.GetValue(this));
                        }
                        else if (pa.Type.Equals("array"))
                        {
                            data.SetArray(pa.StringID, (float[])field.GetValue(this));
                        }
                        else if (pa.Type.Equals("string"))
                        {
                            data.SetString(pa.StringID, (string)field.GetValue(this));
                        }
                        else if (pa.Type.Equals("composite"))
                        {
                            ISettings fld = (ISettings)field.GetValue(this);
                            fld.applySettings(data);
                        }
                    }
                }

            }
        }



        public class ParamAttribute : Attribute
        {
            protected String mStringID;
            protected String mType;
            private List<String> validTypes = new List<String>() {"float","string","array","composite" };
            public ParamAttribute(String StringID, String type)
            {
                mStringID = StringID;
                mType = validate(type);
            }

            public String StringID
            {
                get { return mStringID; }
                set { mStringID = value; }
            }

            public String Type
            {
                get { return mType; }
                set { mType = validate(value); }
            }

            private string validate(string value)
            {
                if (validTypes.Contains(value))
                {
                    return value;
                }
                else
                {
                    throw new Exception("Invalid type");
                }
            }
        }
    }
}
