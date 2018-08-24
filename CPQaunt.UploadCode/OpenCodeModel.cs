using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysOpenCode
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class xml
    {

        private List<xmlRow> rowField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("row")]
        public List<xmlRow> row
        {
            get
            {
                return this.rowField;
            }
            set
            {
                this.rowField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class xmlRow
    {

        private string expectField;

        private string opencodeField;

        private string opentimeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string expect
        {
            get
            {
                return this.expectField;
            }
            set
            {
                this.expectField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string opencode
        {
            get
            {
                return this.opencodeField;
            }
            set
            {
                this.opencodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string opentime
        {
            get
            {
                return this.opentimeField;
            }
            set
            {
                this.opentimeField = value;
            }
        }
    }


}
