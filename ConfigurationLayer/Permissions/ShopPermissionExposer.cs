using _0_FrameWork.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.Permissions
{
    public class ShopPermissionExposer : IPermissonExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Product", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.ListProducts, "ListProducts"),
                        new PermissionDto(ShopPermissions.SearchProducts, "SearchProducts"),
                        new PermissionDto(ShopPermissions.CreateProduct, "CreateProduct"),
                        new PermissionDto(ShopPermissions.EditProduct, "EditProduct"),
                    }
                },
                {
                    "ProductCategory", new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.SearchProductCategories, "SearchProductCategories"),
                        new PermissionDto(ShopPermissions.ListProductCategories, "ListProductCategories"),
                        new PermissionDto(ShopPermissions.CreateProductCategory, "CreateProductCategory"),
                        new PermissionDto(ShopPermissions.EditProductCategory, "EditProductCategory"),
                    }
                },
                {
                    "ProductPicture",new List<PermissionDto>
                    {
                        new PermissionDto(ShopPermissions.SearchProductPicture, "SearchProductPicture"),
                        new PermissionDto(ShopPermissions.ListProductPicture, "ListProductPicture"),
                        new PermissionDto(ShopPermissions.CreateProductPicture, "CreateProductPicture"),
                        new PermissionDto(ShopPermissions.EditProductPicture, "EditProductPicture"),
                        new PermissionDto(ShopPermissions.RemoveProductPicture, "RemoveProductPicture"),
                        new PermissionDto(ShopPermissions.RestoreProductPicture, "RestoreProductPicture"),
                    }
                }
            };
        }
    }
}
