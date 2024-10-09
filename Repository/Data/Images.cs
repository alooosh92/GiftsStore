using GiftsStore.Models;
using Microsoft.Extensions.Hosting.Internal;
using System.ComponentModel;

namespace GiftsStore.Repository.Data
{
    public class Images
    {
        public bool DeleteStoreImage(StoreImages image) 
        {
            try
            {
                string filePath = Path.GetFullPath("wwwRoot\\Images", "Store");
                string fullPath = filePath + image.URL;                
                System.IO.File.Delete(fullPath);
                return true;                
            }
            catch { throw; }
        }
        public bool DeleteGiftImage(GiftImages image)
        {
            try
            {
                string filePath = Path.GetFullPath("wwwRoot\\Images", "Gift");
                string fullPath = filePath + image.URL;
                System.IO.File.Delete(fullPath);
                return true;
            }
            catch { throw; }
        }
        public async Task<StoreImages> AddStoreImage(IFormFile file, Store store, bool isIcon)
        {
            try
            {
                Guid id = Guid.NewGuid();
                var name = await AddImage(file,id,true);
                StoreImages storeImage = new()
                {
                    Id = id,
                    Store = store,
                    Type = isIcon ? "Icon" : "Image",
                    URL = name,
                };
                return storeImage;
            }
            catch { throw; }
        }
        public async Task<GiftImages> AddGiftImage(IFormFile file, Gift gift, bool isIcon)
        {
            try
            {
                Guid id = Guid.NewGuid();
                var name = await AddImage(file, id, false);
                GiftImages giftImage = new()
                {
                    Id = id,
                    Gift = gift,
                    Type = isIcon ? "Icon" : "Image",
                    URL = name,
                };
                return giftImage;
            }
            catch { throw; }
        }
        private async Task<string> AddImage(IFormFile file,Guid id, bool isStore)
        {
            try
            {
                if (!Directory.Exists("wwwRoot\\Images\\Store")) { Directory.CreateDirectory("Images\\Store"); }
                if (!Directory.Exists("wwwRoot\\Images\\Gift")) { Directory.CreateDirectory("Images\\Gift"); }
                string type = file.FileName.Split('.').Last();
                string name = $"{id}.{type}";
                string p = isStore ? "Store" : "Gift";
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwRoot\\Images", p,name);
                using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);
                return name;
            }
            catch { throw; }
        }
    }
}
