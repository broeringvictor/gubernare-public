using Gubernare.Domain.Contexts.AccountContext.Entities;
using Gubernare.Domain.Contexts.ClientContext.Entities;
using Gubernare.Domain.Contexts.SharedContext.Entities;

namespace Gubernare.Domain.Contexts.LegalProceeding.Entities;

public class ToDo : Entity
{
    protected internal ToDo()
    {
    }
    
 

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public bool   IsCompleted { get; set; }


    public Guid? ProcessId { get; set; }
    public Guid UserId    { get; set; }

        // Navegação
    public LegalProceeding? Process { get; set; }
    public User User    { get; set; } = null!;
    
    
    


}