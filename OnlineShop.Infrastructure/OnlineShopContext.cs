using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.AggregatesModel.ShippingAggregate;
using OnlineShop.Domain.SeedWork;
using OnlineShop.Infrastructure.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure
{
    public class OnlineShopContext : DbContext, IUnitOfWork
    {
        private IDbContextTransaction _currentTransaction;
        public OnlineShopContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ProductItem> ProductItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<Basket> Basket { get; set; }
        //public DbSet<ProductType> ProductType { get; set; }
       // public DbSet<DiscountType> DiscountType { get; set; }
        public DbSet<ShippingType> ShippingType { get; set; }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        public bool HasActiveTransaction => _currentTransaction != null;
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> SaveWithTransactionEntitiesAsync(CancellationToken cancellationToken = default)
        {
            using (var transaction = await this.BeginTransactionAsync())
            { 
                await CommitTransactionAsync(transaction);
            }
            return true;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BuyerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BasketEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BasketItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductItemEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityTypeConfiguration());
            //modelBuilder.Entity<ProductItem>().HasData(new ProductItem(name: "Bag", 123, DiscountType.Amount, 3, 1, ProductType.Normal, "description"),
            //                                            new ProductItem(name: "Glass", 321, DiscountType.Amount, 2, 1, ProductType.Fragile, "description"),
            //                                            new ProductItem(name: "Wallet", 511, DiscountType.Amount, 1, 1, ProductType.Normal, "description"),
            //                                            new ProductItem(name: "HDD", 611, DiscountType.Amount, 2, 1, ProductType.Normal, "description"));
            modelBuilder.Entity<ProductType>().HasData(new ProductType(id: 1, name: "Fragile"),
                                                       new ProductType(id: 2, name: "Normal"));
            modelBuilder.Entity<DiscountType>().HasData(new DiscountType(id: 1, name: "Percentage"),
                                                        new DiscountType(id: 2, name: "Amount"));
            modelBuilder.Entity<ShippingType>().HasData(new ShippingType(id: 1, name: "Express"),
                                                        new ShippingType(id: 2, name: "Post"));
        }
    }
}
