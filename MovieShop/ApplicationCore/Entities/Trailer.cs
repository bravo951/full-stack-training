using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    [Table("Trailer")]
    public class Trailer
    {
        public int Id { get; set; }
        public string TrailerUrl { get; set; }
        [MaxLength(2084)]
        public string Name { get; set; }
        [MaxLength(2084)]
        public int MovieId { get; set; }
        //navigational property
        public Movie Movie { get; set; }
    }
}
