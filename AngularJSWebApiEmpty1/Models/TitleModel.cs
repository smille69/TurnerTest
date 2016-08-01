using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJSWebApiEmpty.Models
{
    public class TitleModel
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
        public string TitleNameSortable { get; set; }
        public int? TitleTypeId { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime ProcessDateTimeUTC { get; set; }
        public string GenreName { get; set; }
    }
}