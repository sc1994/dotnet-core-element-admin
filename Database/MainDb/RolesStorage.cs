using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.MainDb;

namespace Database.MainDb
{
    /// <summary>接口</summary>
    public interface IRolesStorage : IBaseStorage<RolesModel>
    {
        Task<int> DeleteAsync(RolesModel model);
    }

    /// <summary></summary>
    public partial class RolesStorage : IRolesStorage
    {
        public async Task<int> DeleteAsync(RolesModel model)
        {
            using (var context = new MainDbContext())
            {
                context.Entry(model).State = EntityState.Deleted;
                return await context.SaveChangesAsync();
            }
        }
    }
}
