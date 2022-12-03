using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Mvc.Models
{

    public class UpdateItemModel
    {

        [StringLength(256, MinimumLength = 5)]
        public string Description { get; set; }

        public bool Done { get; set; }

    }

}

