﻿using Dotseed.Domain;
using Library.Domain.Aggregates.Account;
using Library.Domain.Aggregates.Account.Enums;
using Library.Domain.Aggregates.Account.Values;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure;

public class AccountRepo : IAccountRepo
{
    private readonly Context _db;

    public AccountRepo(Context db) => _db = db;

    public IUnitOfWork UnitOfWork => _db;

    public async Task AddAsync(Account account) => await _db.Accounts.AddAsync(account);

    public async Task<Account> FindByActivationCodeAsync(string ActivationCode) => await _db.Accounts.FirstOrDefaultAsync(acc => acc.ActivationCode == ActivationCode);

    public async Task<Account> FindByEmailAsync(string Email) => await _db.Accounts.FirstOrDefaultAsync(acc => acc.Email == Email);

    public Task<Account> FindByIdAsync(Guid Id) => _db.Accounts.FirstOrDefaultAsync(acc => acc.Id == Id);

    public Task<Account> FindByNickNameAsync(string Nick) => _db.Accounts.FirstOrDefaultAsync(acc => acc.Nickname == Nick);

    public async Task RemoveById(Guid Id) => _db.Accounts.Remove(await _db.Accounts
        .FirstOrDefaultAsync(acc => acc.Id == Id)
        ?? throw new());
}
