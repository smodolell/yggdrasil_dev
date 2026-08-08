using Yggdrasil.Module.Auth.Constants;
using Yggdrasil.Module.Auth.Features.Rol.DTOs;

namespace Yggdrasil.Module.Auth.Features.Rol.Commands;

public class SaveMenuRolCommand : ICommand<Result>
{
    public required int RolId { get; set; }
    public required List<MenuTreeItemDto> Model { get; set; }
}

public class SaveMenuRolCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<SaveMenuRolCommand, Result>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> HandleAsync(SaveMenuRolCommand message, CancellationToken cancellationToken = default)
    {
        try
        {
            var idList = new List<Guid>();
            var roleMenuList = _context.SYS_RolAccessPoint.Where(rm => rm.RolId == message.RolId).ToList();

            foreach (var item in message.Model)
            {
                idList.AddRange(item.Childs.Where(r => r.IsChecked).Select(r => r.Id));
            }

            var deletedMenuList = roleMenuList.Where(rm => !idList.Contains(rm.AccessPointId));
            _context.SYS_RolAccessPoint.RemoveRange(deletedMenuList);

            var addMenuIds = idList
                .Where(id => roleMenuList.All(rm => rm.AccessPointId != id))
                .Select(id => new SYS_RolAccessPoint
                {
                    RolId = message.RolId,
                    AccessPointId = id
                });

            _context.SYS_RolAccessPoint.AddRange(addMenuIds);

            await _context.SaveChangesAsync(cancellationToken);
            return Result.SuccessWithMessage(ResponseMessages.OperationSuccessful);
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}
