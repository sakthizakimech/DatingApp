using System;
using DatingApp.Entities;

namespace DatingApp.interfaces;

public interface IMemberRepository
{
void Update(Member member);
    Task<bool> SaveAllAsync();
    Task<IReadOnlyList<Member>> GetMembersAsync();
    Task<Member?> GetMemberByIdAsync(string id);
    Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId);
}
