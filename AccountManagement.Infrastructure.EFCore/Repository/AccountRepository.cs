﻿using System.Runtime.InteropServices;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository;

public class AccountRepository:RepositoryBase<long,Account>, IAccountRepository
{
    private readonly AccountContext _context;

    public AccountRepository(AccountContext context):base(context)
    {
        _context = context;
    }

    public Account GetBy(string mobile)
    {
        return _context.Accounts.FirstOrDefault(a => a.Mobile == mobile);
    }

    public EditAccount GetDetails(long id)
    {
        return _context.Accounts.Select(x => new EditAccount()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Address = x.Address,
            Mobile = x.Mobile,
            RoleId = x.RoleId,
            PostalCode=x.PostalCode
        }).FirstOrDefault(x => x.Id == id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        var query = _context.Accounts
            .Include(x=>x.Role)
            .Select(x => new AccountViewModel()
        {
            Id = x.Id,
            Fullname = x.Fullname,
            Address = x.Address,
            Role = x.Role.Name,
            RoleId = x.RoleId,
            Mobile = x.Mobile,
            ProfilePhoto = x.ProfilePhoto,
            CreationDate = x.CreationDate.ToFarsi(),
            PostalCode=x.PostalCode
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
        {
            query = query.Where(x => x.Fullname.Contains(searchModel.Fullname));
        }

        if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
        {
            query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));
        }

        if (searchModel.RoleId > 0)
        {
            query = query.Where(x => x.RoleId == searchModel.RoleId);
        }

        return query.OrderByDescending(x => x.Id).ToList();
    }
}