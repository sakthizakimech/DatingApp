using System;
using DatingApp.Entities;
using DatingApp.interfaces;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.DataContext;

public class MemberRepository : IMemberRepository
{
    private AppDbContext context;
    public MemberRepository(AppDbContext _context)
    {
        context = _context;
    }
    public async Task<Member?> GetMemberByIdAsync(string id)
    {
        return await context.Members.FindAsync(id);
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        return await context.Members
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId)
    {
        return await context.Members.Where(x => x.Id == memberId).SelectMany(x => x.Photos).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
       return await context.SaveChangesAsync() > 0;
    }

    public void Update(Member member)
    {
       context.Entry(member).State = EntityState.Modified;
    }
}
