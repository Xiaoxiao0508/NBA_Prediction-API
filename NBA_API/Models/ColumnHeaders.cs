using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace NBA_API.Models
{
	public class ColumnHeaders
    {
        [JsonProperty("COLUMN_NAME")]
        public string COLUMN_NAME { get; set; }

        public ColumnHeaders()
        {

        }

        public ColumnHeaders(string columnname)
        {
            this.COLUMN_NAME = columnname;
        }
    }

}
