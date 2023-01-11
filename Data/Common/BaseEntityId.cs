using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Common
{
    public partial class BaseEntityId
    {
        [Key]
        public virtual int Id { get; set; }
    }
}
