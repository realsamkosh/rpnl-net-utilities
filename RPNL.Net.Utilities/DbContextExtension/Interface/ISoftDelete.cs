using System;

namespace RPNL.Net.Utilities.DbContextExtension.Interface
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
