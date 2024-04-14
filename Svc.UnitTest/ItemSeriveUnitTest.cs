
using FileProcessorUnitTest.ZigZagArray;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using ReferigenatorSvc.dbcontext;
using ReferigenatorSvc.dbcontext.Repositories;
using ReferigenatorSvc.Models;
using ReferigenatorSvc.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace TestProject1
{
   
    public class ItemSeriveUnitTest
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IOptions<List<StorageTypes>>> _mockstorageTypes;
        private readonly IRefrigenatorService _refrigenatorService;
        private readonly Mock<AppDbContext> _mockAppDbContext;

        public static void miniMaxSum(List<int> arr)
        {
            long maximum = 0;
            long minimum = 0;
            for (int i = 0; i < arr.Count; i++)
            {

                if (i < arr.Count - 1)
                {
                    minimum = minimum + arr[i];
                }
                if (i != 0)
                {
                    if (i < arr.Count)
                    {
                        maximum = maximum + arr[i];
                    }
                }
            }
            Console.WriteLine($"{minimum} {maximum}");
        }
        public ItemSeriveUnitTest()
        {
//            var s = "";
//           if( DateTime.TryParse(s,out DateTime parsedDAte))
//            {

//                parsedDAte.ToString("HH:mm:ss");
//            }
            

//            var arr = new List<int> { 396285104, 573261094, 759641832, 819230764, 364801279 };

//            miniMaxSum(arr);

//            _mockstorageTypes = new Mock<IOptions<List<StorageTypes>>>();
//            _mockUnitOfWork = new Mock<IUnitOfWork>();
//            _mockAppDbContext = new Mock<AppDbContext>(new DbContextOptionsBuilder<AppDbContext>().Options);
//           // _mockAppDbContext.Setup(x => x.Database).Verifiable();

//            _mockstorageTypes.SetupGet(mock => mock.Value).Returns(new List<StorageTypes>
//            {
//                new StorageTypes{ 
//                    Code="KILLO",
//                    Name = "Killo"
//                },
//                new StorageTypes{
//                    Code ="LTR",
//                    Name ="Liter"
//                }
//            });

            
//            List<ItemsEntity> list = new List<ItemsEntity>
//{
//   new ItemsEntity { ItemName = "Logan" },
//   new ItemsEntity { ItemName = "George" }
//};

//            // Convert the IEnumerable list to an IQueryable list
//            IQueryable<ItemsEntity> queryableList = list.AsQueryable();

//            // Force DbSet to return the IQueryable members of our converted list object as its data source
//            var mockSet = new Mock<DbSet<ItemsEntity>>();
//            mockSet.As<IQueryable<ItemsEntity>>().Setup(m => m.Provider).Returns(queryableList.Provider);
//            mockSet.As<IQueryable<ItemsEntity>>().Setup(m => m.Expression).Returns(queryableList.Expression);
//            mockSet.As<IQueryable<ItemsEntity>>().Setup(m => m.ElementType).Returns(queryableList.ElementType);
//            mockSet.As<IQueryable<ItemsEntity>>().Setup(m => m.GetEnumerator()).Returns(queryableList.GetEnumerator());

//            _mockAppDbContext.Setup(c => c.itemsEntities).Returns(mockSet.Object);
//            _mockUnitOfWork.Setup(x => x.ItemEntityRepo).Returns(new ItemEntityRepo(_mockAppDbContext.Object));
//            _refrigenatorService = new ReferigenatorSvc.Services.RefrigenatorService(_mockUnitOfWork.Object, _mockstorageTypes.Object);
        }


        [Fact]
        public void GetItems_Return_Items()
       {

            ZigZagArray.ZigZagArrayV1();
            //var items = _refrigenatorService.GetActiveRefrigenationItems();
        }
    }
}
