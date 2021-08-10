using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.Permissions
{
    public static class ShopPermissions
    {
        //Product
        public const int ListProducts = 10;
        public const int SearchProducts = 11;
        public const int CreateProduct = 12;
        public const int EditProduct = 13;


        //ProductCategory
        public const int ListProductCategories = 20;
        public const int SearchProductCategories = 21;
        public const int CreateProductCategory = 22;
        public const int EditProductCategory = 23;

        //ProductPicture
        public const int ListProductPicture = 24;
        public const int SearchProductPicture = 25;
        public const int CreateProductPicture = 26;
        public const int EditProductPicture = 27;
        public const int RemoveProductPicture = 28;
        public const int RestoreProductPicture = 29;
    }
}
