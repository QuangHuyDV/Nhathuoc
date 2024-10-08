using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nhathuoc.Models;

namespace Nhathuoc.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var db = serviceProvider.GetRequiredService<PharmacyContext>())
            {
                db.Database.EnsureCreated();
                if (db.Categories.Any())
                {
                    return;
                }
                // Lấy thời gian hiện tại dưới dạng Unix timestamp
                long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                var categories = new Category[] {
                    new Category { Name = "Thuốc kê đơn", Description = "Thuốc cần có đơn thuốc từ bác sĩ để mua, thường dùng để điều trị các bệnh lý nặng hoặc mãn tính.", Created = unixTimestamp},
                    new Category { Name = "Thuốc không kê đơn", Description = "Thuốc có thể mua mà không cần đơn, dùng để điều trị các vấn đề sức khỏe nhẹ như đau đầu, cảm lạnh, dị ứng.", Created = unixTimestamp},
                    new Category { Name = "Thực phẩm chức năng", Description = "Bao gồm vitamin, khoáng chất, và các sản phẩm hỗ trợ sức khỏe khác.", Created = unixTimestamp},
                    new Category { Name = "Vật tư y tế", Description = "Bao gồm bông băng, gạc, băng cá nhân, găng tay y tế, nhiệt kế, và các thiết bị y tế khác.", Created = unixTimestamp},
                    new Category { Name = "Sản phẩm chăm sóc da", Description = "Kem chống nắng, kem dưỡng ẩm, sản phẩm điều trị mụn, và các sản phẩm chăm sóc da khác.", Created = unixTimestamp},
                    new Category { Name = "Sản phẩm cho trẻ em", Description = "Thuốc, thực phẩm chức năng, và các sản phẩm chăm sóc sức khỏe dành riêng cho trẻ em.", Created = unixTimestamp},
                    new Category { Name = "Sản phẩm cho bà bầu", Description = "Vitamin dành cho bà bầu, sản phẩm chăm sóc sức khỏe cho phụ nữ mang thai.", Created = unixTimestamp},
                };
                foreach (var category in categories)
                {
                    db.Categories.Add(category);
                }
                db.SaveChanges();

                if (db.Products.Any())
                {
                    return;
                }
                var products = new Product[] {
                    new Product { Name = "Amoxicillin", CategoryId = 1 , Price = 8000, Unit = "viên", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Metformin", CategoryId = 1 , Price = 5000, Unit = "viên", QuantityInStock = 5000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm b", Created = unixTimestamp},
                    new Product { Name = "Lisinopril", CategoryId = 1 , Price = 3500, Unit = "viên", QuantityInStock = 4000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Paracetamol", CategoryId = 2 , Price = 2000, Unit = "viên", QuantityInStock = 2000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Ibuprofen", CategoryId = 2 , Price = 2500, Unit = "viên", QuantityInStock = 1500, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm C", Created = unixTimestamp},
                    new Product { Name = "Vitamin C", CategoryId = 3 , Price = 900, Unit = "viên", QuantityInStock = 5000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm D", Created = unixTimestamp},
                    new Product { Name = "Omega-3", CategoryId = 3 , Price = 2500, Unit = "viên", QuantityInStock = 2000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm D", Created = unixTimestamp},
                    new Product { Name = "Bông băng", CategoryId = 4 , Price = 10000, Unit = "100gam", QuantityInStock = 1000, ExpiryDate = "3 đến 5 năm", Mannufacurer = "Công ty Dược phẩm E", Created = unixTimestamp},
                    new Product { Name = "Gạc", CategoryId = 4 , Price = 15000, Unit = "10 cái", QuantityInStock = 10000, ExpiryDate = "3 đến 5 năm", Mannufacurer = "Công ty Dược phẩm E", Created = unixTimestamp},
                    new Product { Name = "Kem dưỡng ẩm", CategoryId = 5 , Price = 120000, Unit = "50ml", QuantityInStock = 1500, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Gel trị mụn", CategoryId = 5 , Price = 80000, Unit = "20g", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm H", Created = unixTimestamp},
                    new Product { Name = "Siro giảm ho", CategoryId = 6 , Price = 60000, Unit = "100ml", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Dầu gió cho trẻ em", CategoryId = 6 , Price = 35000, Unit = "50ml", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Vitamin tổng hợp cho trẻ em", CategoryId = 6 , Price = 4000, Unit = "viên", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Vitamin B9 (Folic Acid)", CategoryId = 1 , Price = 8000, Unit = "viên", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                    new Product { Name = "Kem trị rạn da", CategoryId = 1 , Price = 150000, Unit = "100ml", QuantityInStock = 1000, ExpiryDate = "1 đến 3 năm kể từ ngày sản xuất", Mannufacurer = "Công ty Dược phẩm A", Created = unixTimestamp},
                };
                foreach (var product in products)
                {
                    db.Products.Add(product);
                }
                db.SaveChanges();

                if (db.Customers.Any())
                {
                    return;
                }
                var customers = new Customer[] {
                    new Customer { CustomerName = "Huy", CustomerPhone = "0983192540", CustomerAddress = "Duyên Hà, Thanh Trì, Hà Nội", Dob = DateTime.Parse("2001-10-20"), Created = unixTimestamp},
                    new Customer { CustomerName = "Khánh", CustomerPhone = "0983192541", CustomerAddress = "Long Biên, Hà Nội", Dob = DateTime.Parse("2002-12-02"), Created = unixTimestamp}
                };
                foreach (var customer in customers)
                {
                    db.Customers.Add(customer);
                }
                db.SaveChanges();

                var suppliers = new Supplier[] {
                    new Supplier {SupplierName = "Kho 1", Address = "Long Biên, Hà Nội", Email="kho1@gmail.com", Phone="0123229992", Created = unixTimestamp},
                    new Supplier {SupplierName = "Kho 2", Address = "Bắc Ninh", Email="kho2@gmail.com", Phone="08981129992", Created = unixTimestamp},
                    new Supplier {SupplierName = "Kho 3", Address = "Cầu Giấy, Hà Nội", Email="kho3@gmail.com", Phone="0981222888", Created = unixTimestamp},
                };
                foreach (var supplier in suppliers)
                {
                    db.Suppliers.Add(supplier);
                }
                db.SaveChanges();

                var stocks = new Stock[] {
                    new Stock { ProductId = 1, SupplierId = 1, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 2, SupplierId = 1, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 3, SupplierId = 2, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 4, SupplierId = 3, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 5, SupplierId = 1, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 6, SupplierId = 2, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-08"), Created = unixTimestamp },
                    new Stock { ProductId = 1, SupplierId = 1, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-09"), Created = unixTimestamp },
                    new Stock { ProductId = 3, SupplierId = 1, QuantityReceived = 1000, ReceivedDate = DateTime.Parse("2024-10-09"), Created = unixTimestamp },
                };
                foreach (var stock in stocks)
                {
                    db.Stocks.Add(stock);
                }
                db.SaveChanges();
            }
        }
    }
}