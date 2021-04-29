using Microsoft.EntityFrameworkCore;

class CustomAutoHistory : AutoHistory
{
    public int? AccountId { get; set; }
}