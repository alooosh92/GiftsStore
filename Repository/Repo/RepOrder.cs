using GiftsStore.Data;
using GiftsStore.DataModels.OrderData;
using GiftsStore.DataModels.OrderItem;
using GiftsStore.Models;
using GiftsStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GiftsStore.Repository.Repo
{
    public class RepOrder : IRepositoryOrder<AddOrder ,ViewOrder>
    {
        public RepOrder(ApplicationDbContext Db) => this.Db = Db;

        public ApplicationDbContext Db { get; }

        public async Task<ViewOrder?> Create(AddOrder element)
        {
            try
            {
                Order? order = await Search(element.Person!, element.Store);
                if (order == null)
                {
                    order = element.ToOrder();
                    order.Store = await Db.Stores.FindAsync(element.Store);
                    order.Person = await Db.Users.FindAsync(element.Person);
                    await Db.Orders.AddAsync(order);
                }
                List<ViewOrderItem> orderItems = new() { };
                foreach (var item in element.OrderItems!)
                {
                    OrderItems items = new()
                    {
                        Gift = await Db.Gifts.FindAsync(item),
                        Id = Guid.NewGuid(),
                        Order = order,
                    };
                    Db.OrderItems.Add(items);
                    orderItems.Add(items.ToViewOrderItem());
                }
                await Db.SaveChangesAsync();
                ViewOrder o = order.ToViewOrder();
                o.Items = orderItems;
                return o;
            }
            catch { throw; }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                OrderItems? item = await Db.OrderItems.Include(a => a.Order).Where(a => a.Id == id).SingleOrDefaultAsync();
                List<OrderItems> items = await Db.OrderItems.Include(a => a.Order).Where(a => a.Order!.Id == item!.Order!.Id).ToListAsync();                
                if (item == null) { return false; }
                Db.OrderItems.Remove(item);
                items.Remove(item);
                if(items.Count == 0) { Db.Orders.Remove(item.Order!); }
                await Db.SaveChangesAsync();
                return true;
            }
            catch { throw; }
        }

        public async Task<ViewOrder?> Get(Guid id)
        {
            try
            {
                Order? order = await Search(id);
                if(order == null) { return null; }
                ViewOrder? o = order.ToViewOrder();
                List<OrderItems> items = await SearchItem(order.Id);
                List<ViewOrderItem> viewitems = new() { };
                foreach (var item in items)
                {
                    ViewOrderItem i = item.ToViewOrderItem();
                    GiftImages? giftImages = await Db.GiftImages.Include(a => a.Gift).Where(a => a.Gift!.Id == item.Gift!.Id && a.Type == "Icom").SingleOrDefaultAsync();
                    if(giftImages == null) { giftImages = new() { URL =""}; }
                    i.Url = giftImages!.URL;                    
                    viewitems.Add(i);
                }
                StoreImages? im = await Db.StoreImages.Include(a=>a.Store).Where(a => a.Store!.Id == order.Store!.Id && a.Type == "Icon").SingleOrDefaultAsync();
                if (im == null) { im = new() { URL = ""}; }
                o.Url = im!.URL;
                o.Items = viewitems;
                return o;
            }
            catch { throw; }
        }

        public async Task<List<ViewOrder>> GetAll(string id)
        {
            try
            {
                List<Order> orders = await SearchAll(id);
                List<ViewOrder> items = new List<ViewOrder>();
                foreach (var item in orders)
                {
                    items.Add(item.ToViewOrder());   
                }
                return items;
            }
            catch { throw; }
        }
        private async Task<Order?> Search(Guid storeId)
        {
            try
            {
                Order? order = await Db.Orders.Include(a => a.DeliveryCompanies!.Region).Include(a => a.Person).Include(a => a.Store!.Region).Where(a => a.Id == storeId).SingleOrDefaultAsync();
                return order;
            }
            catch { throw; }
        }
        private async Task<Order?> Search(string id,Guid storeId) 
        {
            try
            {
                Order? order = await Db.Orders.Include(a => a.DeliveryCompanies!.Region).Include(a => a.Person).Include(a=>a.Store!.Region).Where(a => a.DeliveryStatus == "UnConfirm" && a.Person!.Id == id && a.Store!.Id == storeId).SingleOrDefaultAsync();
                return order;
            }
            catch { throw; }
        }
        private async Task<List<Order>> SearchAll(string id)
        {
            try
            {
                List<Order> list = await Db.Orders
                    .Include(a => a.Store!.Region)
                    .Include(a=>a.DeliveryCompanies)
                    .Include(a=>a.Person)
                    .Where(a=>a.Person!.Id == id)
                    .ToListAsync();
                return list;
            }
            catch { throw; }
        }
        private async Task<List<OrderItems>> SearchItem(Guid id)
        {
            try
            {
                List<OrderItems> items = await Db.OrderItems
                    .Include(a=>a.Order!.Person)
                    .Include(a=>a.Order!.Store!.Region)
                    .Include(a=>a.Gift!.Store!.Region)
                    .Where(a=>a.Order!.Id == id)
                    .ToListAsync();
                return items;
            }
            catch { throw; }
        }

    }
}
