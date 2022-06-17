using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;
        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || 
                _context.Seller.Any() || 
                _context.SalesRercord.Any())
            {
                return; //Db has  been seeded
            }
            Department d1 = new Department(1, "Computer");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Vitor Lima", "devvi.oficial@gmail.com", new DateTime(2022, 6, 17), 8000.0, d1);
            Seller s2 = new Seller(2, "Andrea Biscate", "deaagahta@gmail.com", new DateTime(2021, 5, 17), 3000.0, d2);
            Seller s3 = new Seller(3, "Fernanda Brandao", "nanda@hotmail.com", new DateTime(2020, 4, 16), 2000.0, d3);
            Seller s4 = new Seller(4, "Milena Oliveira", "mileunicornia@uol.com", new DateTime(2019, 3, 15), 1000.00, d4);

            SalesRecord r1 = new SalesRecord(1, new DateTime(2022, 6, 15), 11000.0, SaleStatus.Billed, s1);
            SalesRecord r2 = new SalesRecord(2, new DateTime(2022, 6, 10), 1000.0, SaleStatus.Billed, s1);
            SalesRecord r3 = new SalesRecord(3, new DateTime(2022, 6, 09), 10000.0, SaleStatus.Pending, s2);
            SalesRecord r4 = new SalesRecord(4, new DateTime(2022, 6, 12), 7000.0, SaleStatus.Billed, s2);
            SalesRecord r5 = new SalesRecord(5, new DateTime(2022, 6, 11), 6000.0, SaleStatus.Billed, s3);
            SalesRecord r6 = new SalesRecord(6, new DateTime(2022, 6, 8), 5000.0, SaleStatus.Canceled, s3);
            SalesRecord r7 = new SalesRecord(7, new DateTime(2022, 6, 7), 4000.0, SaleStatus.Billed, s4);
            SalesRecord r8 = new SalesRecord(8, new DateTime(2022, 6, 6), 3000.0, SaleStatus.Pending, s4);
            SalesRecord r9 = new SalesRecord(9, new DateTime(2022, 6, 5), 2000.0, SaleStatus.Billed, s1);
            SalesRecord r10 = new SalesRecord(10, new DateTime(2022, 6, 4), 8000.0, SaleStatus.Billed, s2);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4);
            _context.SalesRercord.AddRange(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10);
            _context.SaveChanges();
        }
    }
}