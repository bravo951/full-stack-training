using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    //1. data annotation way
    //2. fluent API
    [Table("Genre")]
    public class Genre
    {
        public int Id { get; set; }
        [MaxLength(24)]
        public string Name { get; set; }
        public ICollection<MovieGenre> Movies { get; set; }
    }
}
