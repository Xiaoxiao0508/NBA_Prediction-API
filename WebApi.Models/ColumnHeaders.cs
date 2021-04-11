using System;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
	public class ColumnHeaders
    {
        [JsonProperty("COLUMN_NAME")]
        public string COLUMN_NAME { get; set; }

        public ColumnHeaders()
        {

        }

        public ColumnHeaders(string cOLUMN_NAME)
        {
            this.COLUMN_NAME = cOLUMN_NAME;
        }
    }

}
