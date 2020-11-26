using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.Models
{
    public class LinkDto
    {
        /// <summary>
        /// Link url
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Rel { get; private set; }

        /// <summary>
        /// GET, POST etc.
        /// </summary>
        public string Method { get; private set; }


        public LinkDto(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
