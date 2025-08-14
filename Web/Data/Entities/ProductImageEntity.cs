﻿using System.ComponentModel.DataAnnotations;

namespace RowingApp.Web.Data.Entities
{
    public class ProductImageEntity
    {
        public int Id { get; set; }

        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }

        public ProductEntity Product { get; set; }

        public string ImageFullPath => string.IsNullOrEmpty(ImagePath)
           ? $"http://keypress.serveftp.net:88/RowingAppApi/images/Products/noimage.png"
        : $"http://keypress.serveftp.net:88/RowingAppApi{ImagePath.Substring(1)}";
        //? $"https://localhost:44390/images/Products/noimage.png"
        //: $"https://localhost:44390/{ImagePath.Substring(1)}";

    }
}