using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ViewModels
{
    public enum ProductGroupTypes
    {
        [Display(Name = "Finished Goods")]
        Finished_Goods = 1,
        [Display(Name = "Semi Finished Goods")]
        Semi_Finished_Goods = 2,
        [Display(Name = "Raw Material")]
        Raw_Material = 3,
        [Display(Name = "Finish Product Packing Drums")]
        Finish_Product_Packing_Drums = 4
    }
}
