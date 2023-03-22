using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace WorkitemTst.Models
{

    [XmlRoot("WITD", Namespace = "http://schemas.microsoft.com/VisualStudio/2008/workitemtracking/typedef")]
    public class XmlWorkitemType
    {

        public static XmlWorkitemType FromXmlString(string content)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlWorkitemType));
            using (StringReader stringReader = new StringReader(content))
            {

                return (XmlWorkitemType)xmlSerializer.Deserialize(stringReader);
            }
        }

        public static string ToXmlString(XmlWorkitemType wit)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(XmlWorkitemType));

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xmlSerializer.Serialize(writer, wit);
                    return sww.ToString();
                }
            }
        }



        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }


        public override int GetHashCode() => this.WorkItemType.Name.GetHashCode();

        public override bool Equals(object otherObj) => this.WorkItemType.Name == ((XmlWorkitemType)otherObj).WorkItemType.Name;
        public string GenerateHash() {
            var salt = "";
            var text = XmlWorkitemType.ToXmlString(this);

            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }







            //return XmlWorkitemType.ToXmlString(this).GetHashCode();
            //return this.ToJsonString().GetHashCode();
        }


        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; } 

        [XmlElement("WORKITEMTYPE", Namespace = "")]
        public WorkItemType WorkItemType { get; set; }




        
        //string jsonString = JsonSerializer.Serialize(weatherForecast);
    }

    
    public class WorkItemType
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "DESCRIPTION")]
        public string Description { get; set; }

        [XmlArray("FIELDS")]
        [XmlArrayItem("FIELD")]
        public Field[] Fields { get; set; }

        [XmlElement("WORKFLOW")]
        public Workflow2 Workflow { get; set; }

        [XmlElement("FORM")]
        public Form Form { get; set; }

    }

    //[XmlElement(ElementName = "FIELD")]
    public class Field
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName = "refname")]
        public string Reference { get; set; }

        [XmlAttribute(AttributeName = "reportable")]
        public string Reportable { get; set; }

        [XmlAttribute(AttributeName = "syncnamechanges")]
        public string SyncNameChanges { get; set; }

        [XmlElement(ElementName = "HELPTEXT")]
        public string HelpText { get; set; }

    }

    
    public class Workflow2
    {
        [XmlArray("STATES")]
        [XmlArrayItem("STATE")]
        public State[] States { get; set; }
        [XmlArray("TRANSITIONS")]
        [XmlArrayItem("TRANSITION")]
        public Transition2[] Transitions { get; set; }

    }

    public class Form
    {

        [JsonIgnore]
        [XmlAnyElement("Layout")]
        public XmlElement Layout { get; set; }

        [JsonIgnore]
        [XmlAnyElement("WebLayout")]
        public XmlElement WebLayout { get; set; }


        [XmlIgnore]
        public string LayoutXml
        {
            get
            {
                return Layout?.InnerXml;
            }
            set
            {
                if (string.IsNullOrEmpty(value)) {
                    return;
                }
                var newXml = $"<Layout>{value}</Layout>";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(newXml);
                Layout = xmlDocument.DocumentElement;
            }
        }

        [XmlIgnore]
        public string WebLayoutXml
        {
            get
            {
                return WebLayout?.InnerXml;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                var newXml = $"<WebLayout>{value}</WebLayout>";
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(newXml);
                WebLayout = xmlDocument.DocumentElement;
            }
        }


    }


    public class Transition2
    {
        [XmlAttribute(AttributeName = "from")]
        public string From { get; set; }
        [XmlAttribute(AttributeName = "to")]
        public string To { get; set; }

        [XmlArray("REASONS")]
        [XmlArrayItem("REASON", Type = typeof(ReasonItem))]
        [XmlArrayItem("DEFAULTREASON", Type = typeof(DefaultReason))]
        public Reason[] Reasons { get; set; }
    }


    public class State
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }


    public abstract class Reason
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
    }

    public sealed class ReasonItem : Reason { }
    public sealed class DefaultReason : Reason { }


}
