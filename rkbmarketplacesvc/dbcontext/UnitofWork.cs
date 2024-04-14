using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReferigenatorSvc.dbcontext.Repositories;

namespace ReferigenatorSvc.dbcontext
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext context;
		private readonly IDbContextTransaction _transaction;
		public  UnitOfWork(StoreDbContext _context)
		{
			context = _context;
			//_transaction = _context.Database.BeginTransaction();
		}
		private IItemEntityRepo _ItemEntityRepo;
		private IItemEntityHistoryRepo _ItemEntityHistoryRepo;

		public IItemEntityRepo ItemEntityRepo
		{

			get
			{
				if (_ItemEntityRepo == null)
					_ItemEntityRepo = new ItemEntityRepo(context);
				return _ItemEntityRepo;
			}

		}

		public IItemEntityHistoryRepo ItemEntityHistoryRepo
		{

			get
			{
				if (_ItemEntityHistoryRepo == null)
					_ItemEntityHistoryRepo = new ItemEntityHistoryRepo(context);
				return _ItemEntityHistoryRepo;
			}

		}

		public async Task SaveChangesAsync()
		{
			await context.SaveChangesAsync();
		}
		public async Task<IDbConnection> GetDbConnection()
		{
			IDbConnection dbConnection = context.Database.GetDbConnection();
				if (dbConnection.State == ConnectionState.Closed)
					dbConnection.Open();
				return dbConnection;
			
		}
	}
}
