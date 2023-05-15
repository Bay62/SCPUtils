namespace SCPUtils
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class Permissions
    {

        [Description("A list with permissions for SCPUtils")]
        public Dictionary<string, List<string>> PermissionsList { get; set; } = new Dictionary<string, List<string>>() {
            {
                "founder",
                new List<string>
                {
                    "scputils.*"
                }
            },
            { 
                "staff",
                new List<string> 
                {
                    "scputils.asn.*"
                }
            }, 
            { 
                "user",
                new List<string>
                { 
                    "scputils.example", "scputils.example2"
                }
            }
        };     
    }
}
